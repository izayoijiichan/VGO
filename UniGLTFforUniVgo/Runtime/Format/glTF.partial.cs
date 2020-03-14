using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;


namespace UniGLTFforUniVgo
{
    public partial class glTF : IEquatable<glTF>
    {
        #region Buffer
        public int AddBuffer(IBytesBuffer bytesBuffer)
        {
            var index = buffers.Count;
            buffers.Add(new glTFBuffer(bytesBuffer));
            return index;
        }
        public int AddBufferView(glTFBufferView view)
        {
            var index = bufferViews.Count;
            bufferViews.Add(view);
            return index;
        }

        T[] GetAttrib<T>(glTFAccessor accessor, glTFBufferView view) where T : struct
        {
            return GetAttrib<T>(accessor.count, accessor.byteOffset, view);
        }
        T[] GetAttrib<T>(int count, int byteOffset, glTFBufferView view) where T : struct
        {
            var attrib = new T[count];
            var segment = buffers[view.buffer].GetBytes();
            var bytes = new ArraySegment<Byte>(segment.Array, segment.Offset + view.byteOffset + byteOffset, count * view.byteStride);
            bytes.MarshalCopyTo(attrib);
            return attrib;
        }

        public ArraySegment<Byte> GetViewBytes(int bufferView)
        {
            var view = bufferViews[bufferView];
            var segment = buffers[view.buffer].GetBytes();
            return new ArraySegment<byte>(segment.Array, segment.Offset + view.byteOffset, view.byteLength);
        }

        IEnumerable<int> _GetIndices(glTFAccessor accessor, out int count)
        {
            count = accessor.count;
            var view = bufferViews[accessor.bufferView];
            switch ((glComponentType)accessor.componentType)
            {
                case glComponentType.UNSIGNED_BYTE:
                    {
                        return GetAttrib<Byte>(accessor, view).Select(x => (int)(x));
                    }

                case glComponentType.UNSIGNED_SHORT:
                    {
                        return GetAttrib<UInt16>(accessor, view).Select(x => (int)(x));
                    }

                case glComponentType.UNSIGNED_INT:
                    {
                        return GetAttrib<UInt32>(accessor, view).Select(x => (int)(x));
                    }
            }
            throw new NotImplementedException("GetIndices: unknown componenttype: " + accessor.componentType);
        }

        IEnumerable<int> _GetIndices(glTFBufferView view, int count, int byteOffset, glComponentType componentType)
        {
            switch (componentType)
            {
                case glComponentType.UNSIGNED_BYTE:
                    {
                        return GetAttrib<Byte>(count, byteOffset, view).Select(x => (int)(x));
                    }

                case glComponentType.UNSIGNED_SHORT:
                    {
                        return GetAttrib<UInt16>(count, byteOffset, view).Select(x => (int)(x));
                    }

                case glComponentType.UNSIGNED_INT:
                    {
                        return GetAttrib<UInt32>(count, byteOffset, view).Select(x => (int)(x));
                    }
            }
            throw new NotImplementedException("GetIndices: unknown componenttype: " + componentType);
        }

        public int[] GetIndices(int accessorIndex)
        {
            int count;
            var result = _GetIndices(accessors[accessorIndex], out count);
            var indices = new int[count];

            // flip triangles
            var it = result.GetEnumerator();
            {
                for (int i = 0; i < count; i += 3)
                {
                    it.MoveNext(); indices[i + 2] = it.Current;
                    it.MoveNext(); indices[i + 1] = it.Current;
                    it.MoveNext(); indices[i] = it.Current;
                }
            }

            return indices;
        }

        public T[] GetArrayFromAccessor<T>(int accessorIndex) where T : struct
        {
            var vertexAccessor = accessors[accessorIndex];

            if (vertexAccessor.count <= 0) return new T[] { };

            var result = (vertexAccessor.bufferView != -1)
                ? GetAttrib<T>(vertexAccessor, bufferViews[vertexAccessor.bufferView])
                : new T[vertexAccessor.count]
                ;

            var sparse = vertexAccessor.sparse;
            if (sparse != null && sparse.count > 0)
            {
                // override sparse values
                var indices = _GetIndices(bufferViews[sparse.indices.bufferView], sparse.count, sparse.indices.byteOffset, sparse.indices.componentType);
                var values = GetAttrib<T>(sparse.count, sparse.values.byteOffset, bufferViews[sparse.values.bufferView]);

                var it = indices.GetEnumerator();
                for (int i = 0; i < sparse.count; ++i)
                {
                    it.MoveNext();
                    result[it.Current] = values[i];
                }
            }
            return result;
        }
        #endregion

        #region Texture

        public glTFTextureSampler GetSampler(int index)
        {
            if (samplers.Count == 0)
            {
                samplers.Add(new glTFTextureSampler()); // default sampler
            }

            return samplers[index];
        }

        public int GetImageIndexFromTextureIndex(int textureIndex)
        {
            return textures[textureIndex].source;
        }

        public glTFImage GetImageFromTextureIndex(int textureIndex)
        {
            return images[GetImageIndexFromTextureIndex(textureIndex)];
        }

        public glTFTextureSampler GetSamplerFromTextureIndex(int textureIndex)
        {
            var samplerIndex = textures[textureIndex].sampler;
            return GetSampler(samplerIndex);
        }

        public ArraySegment<Byte> GetImageBytes(IStorage storage, int imageIndex, out string textureName)
        {
            var image = images[imageIndex];
            if (string.IsNullOrEmpty(image.uri))
            {
                //
                // use buffer view (GLB)
                //
                //m_imageBytes = ToArray(byteSegment);
                textureName = !string.IsNullOrEmpty(image.name) ? image.name : string.Format("{0:00}#GLB", imageIndex);
                return GetViewBytes(image.bufferView);
            }
            else
            {
                if (image.uri.StartsWith("data:"))
                {
                    textureName = !string.IsNullOrEmpty(image.name) ? image.name : string.Format("{0:00}#Base64Embedded", imageIndex);
                }
                else
                {
                    textureName = !string.IsNullOrEmpty(image.name) ? image.name : Path.GetFileNameWithoutExtension(image.uri);
                }
                return storage.Get(image.uri);
            }
        }

        #endregion

        #region Meterial

        public string GetUniqueMaterialName(int index)
        {
            if (materials.Any(x => string.IsNullOrEmpty(x.name))
                || materials.Select(x => x.name).Distinct().Count() != materials.Count)
            {
                return String.Format("{0:00}_{1}", index, materials[index].name);
            }
            else
            {
                return materials[index].name;
            }
        }

        public bool MaterialHasVertexColor(glTFMaterial material)
        {
            if (material == null)
            {
                return false;
            }

            var materialIndex = materials.IndexOf(material);
            if (materialIndex == -1)
            {
                return false;
            }

            return MaterialHasVertexColor(materialIndex);
        }

        public bool MaterialHasVertexColor(int materialIndex)
        {
            if (materialIndex < 0 || materialIndex >= materials.Count)
            {
                return false;
            }

            if (meshes == null)
            {
                return false;
            }

            var hasVertexColor = meshes.SelectMany(x => x.primitives).Any(x => x.material == materialIndex && x.HasVertexColor);
            return hasVertexColor;
        }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return string.Format("{0}", asset);
        }

        public bool Equals(glTF other)
        {
            return
                textures.SequenceEqual(other.textures)
                && samplers.SequenceEqual(other.samplers)
                && images.SequenceEqual(other.images)
                && materials.SequenceEqual(other.materials)
                && meshes.SequenceEqual(other.meshes)
                && nodes.SequenceEqual(other.nodes)
                && skins.SequenceEqual(other.skins)
                && scene == other.scene
                && scenes.SequenceEqual(other.scenes)
                && animations.SequenceEqual(other.animations)
                ;
        }


        public byte[] ToGlbBytes()
        {
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new VgoContractResolver(),
                NullValueHandling = NullValueHandling.Ignore,
            };

            string json = JsonConvert.SerializeObject(value: this, settings: settings);

            return Glb.ToBytes(json, buffers[0].GetBytes());
        }

        #endregion
    }
}
