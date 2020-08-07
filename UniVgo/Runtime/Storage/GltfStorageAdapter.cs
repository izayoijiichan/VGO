// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : GltfStorageAdapter
// ----------------------------------------------------------------------
namespace UniVgo
{
    using NewtonGltf;
    using NewtonGltf.Serialization;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using VgoGltf;
    using VgoGltf.Buffers;

    /// <summary>
    /// glTF Storage Adapter
    /// </summary>
    public class GltfStorageAdapter
    {
        #region Constructors

        /// <summary>
        /// Create a new instance of GltfStorageAdapter.
        /// </summary>
        /// <param name="storage"></param>
        public GltfStorageAdapter(GltfStorage storage)
        {
            GltfStorage = storage;
        }

        #endregion

        #region Fields

        /// <summary>
        /// The accessor type info collection.
        /// </summary>
        /// <remarks>
        /// Attributes.Positions : UnityEngine.Vector3[]
        /// Attributes.Normals   : UnityEngine.Vector3[]
        /// Attributes.Tangents  : UnityEngine.Vector4[]
        /// Attributes.UVs       : UnityEngine.Vector2[]
        /// Attributes.Colors    : UnityEngine.Color[]
        /// Attributes.Joints    : UniVgo.Vector4Ushort[]
        /// Attributes.Weights   : UnityEngine.Vector4[]
        /// BlendShapes.Positions: UnityEngine.Vector3[]
        /// BlendShapes.Normals  : UnityEngine.Vector3[]
        /// BlendShapes.Tangents : UnityEngine.Vector3[]
        /// </remarks>
        protected readonly AccessorTypeInfoCollection AccessorTypeInfoColection = new AccessorTypeInfoCollection
        {
            new AccessorTypeInfo(typeof(sbyte), GltfAccessorType.SCALAR, GltfComponentType.BYTE),
            new AccessorTypeInfo(typeof(byte), GltfAccessorType.SCALAR, GltfComponentType.UNSIGNED_BYTE),
            new AccessorTypeInfo(typeof(short), GltfAccessorType.SCALAR, GltfComponentType.SHORT),
            new AccessorTypeInfo(typeof(ushort), GltfAccessorType.SCALAR, GltfComponentType.UNSIGNED_SHORT),
            new AccessorTypeInfo(typeof(uint), GltfAccessorType.SCALAR, GltfComponentType.UNSIGNED_INT),
            new AccessorTypeInfo(typeof(float), GltfAccessorType.SCALAR, GltfComponentType.FLOAT),
            new AccessorTypeInfo(typeof(UnityEngine.Vector2), GltfAccessorType.VEC2, GltfComponentType.FLOAT),
            new AccessorTypeInfo(typeof(UnityEngine.Vector3), GltfAccessorType.VEC3, GltfComponentType.FLOAT),
            new AccessorTypeInfo(typeof(UnityEngine.Vector4), GltfAccessorType.VEC4, GltfComponentType.FLOAT),
            new AccessorTypeInfo(typeof(VgoGltf.Matrix2), GltfAccessorType.MAT2, GltfComponentType.FLOAT),
            new AccessorTypeInfo(typeof(VgoGltf.Matrix3), GltfAccessorType.MAT3, GltfComponentType.FLOAT),
            new AccessorTypeInfo(typeof(UnityEngine.Matrix4x4), GltfAccessorType.MAT4, GltfComponentType.FLOAT),
            new AccessorTypeInfo(typeof(VgoGltf.Vector4Ushort), GltfAccessorType.VEC4, GltfComponentType.UNSIGNED_SHORT),
            new AccessorTypeInfo(typeof(VgoGltf.Vector4Uint), GltfAccessorType.VEC4, GltfComponentType.UNSIGNED_INT),
            new AccessorTypeInfo(typeof(VgoGltf.Color3), GltfAccessorType.VEC3, GltfComponentType.FLOAT),
            new AccessorTypeInfo(typeof(UnityEngine.Color), GltfAccessorType.VEC4, GltfComponentType.FLOAT),
        };

        /// <summary>The JSON serializer settings</summary>
        protected readonly VgoJsonSerializerSettings _JsonSerializerSettings = new VgoJsonSerializerSettings();

        #endregion

        #region Properties

        /// <summary>glTF Storage</summary>
        public GltfStorage GltfStorage { get; }

        /// <summary>glTF</summary>
        public Gltf Gltf => GltfStorage.Gltf;

        /// <summary>glTF Buffer Data</summary>
        protected List<IByteBuffer> BufferData => GltfStorage.BufferData;

        #endregion

        #region Buffer & BufferView

        /// <summary>
        /// Get a bufferView from storage.
        /// </summary>
        /// <param name="bufferViewIndex">The index of the bufferView.</param>
        /// <returns>gltf.bufferView</returns>
        public GltfBufferView GetBufferView(int bufferViewIndex)
        {
            if (Gltf.bufferViews.TryGetValue(bufferViewIndex, out GltfBufferView bufferView) == false)
            {
                throw new IndexOutOfRangeException($"gltf.bufferViews[{bufferViewIndex}]");
            }

            return bufferView;
        }

        #endregion

        #region Buffer & BufferView (Import)

        /// <summary>
        /// Gets buffer bytes from storage.
        /// </summary>
        /// <param name="bufferIndex">The index of the buffer.</param>
        /// <returns>Byte array of buffer.</returns>
        public ArraySegment<byte> GetBufferBytes(int bufferIndex)
        {
            if (bufferIndex.IsInRangeOf(Gltf.buffers) == false)
            {
                throw new IndexOutOfRangeException($"gltf.buffers[{bufferIndex}]");
            }

            if (BufferData.TryGetValue(bufferIndex, out IByteBuffer buffer) == false)
            {
                throw new IndexOutOfRangeException($"BufferData[{bufferIndex}]");
            }

            return buffer.GetBytes();
        }

        /// <summary>
        /// Gets bufferView bytes from storage.
        /// </summary>
        /// <param name="bufferViewIndex">The index of the bufferView.</param>
        /// <returns>Byte array of bufferView.</returns>
        public ArraySegment<byte> GetBufferViewBytes(int bufferViewIndex)
        {
            if (Gltf.bufferViews.TryGetValue(bufferViewIndex, out GltfBufferView bufferView) == false)
            {
                throw new IndexOutOfRangeException($"gltf.bufferViews[{bufferViewIndex}]");
            }

            return GetBufferBytes(bufferView.buffer).Slice(bufferView.byteOffset, bufferView.byteLength);
        }

        /// <summary>
        /// Get URI data.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns>Byte array.</returns>
        public byte[] GetUriData(string uri)
        {
            return GltfStorage.GetUriData(uri);
        }

        #endregion

        #region Buffer & BufferView (Export)

        /// <summary>
        /// Add the specified buffer to storage.
        /// </summary>
        /// <param name="buffer">The buffer to add.</param>
        /// <returns>The index of gltf.buffer.</returns>
        public int AddBuffer(IByteBuffer buffer)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            if (Gltf.buffers.Count != BufferData.Count)
            {
                throw new Exception();
            }

            var gltfBuffer = new GltfBuffer
            {
                byteLength = buffer.Length,
            };

            Gltf.buffers.Add(gltfBuffer);

            BufferData.Add(buffer);

            int bufferIndex = Gltf.buffers.Count - 1;

            return bufferIndex;
        }

        /// <summary>
        /// Adds the specified arrayData to buffer through the bufferView..
        /// </summary>
        /// <typeparam name="T">Type of arrayData.</typeparam>
        /// <param name="bufferIndex">The index of the buffer.</param>
        /// <param name="arrayData"></param>
        /// <param name="stride">The stride, in bytes.</param>
        /// <param name="target">The target that the GPU buffer should be bound to.</param>
        /// <returns>The index of gltf.bufferView.</returns>
        public int AddBufferView<T>(int bufferIndex, T[] arrayData, int stride = 0, GltfBufferViewTarget target = GltfBufferViewTarget.NONE)
            where T : struct
        {
            if (bufferIndex.IsInRangeOf(Gltf.buffers) == false)
            {
                throw new IndexOutOfRangeException(nameof(bufferIndex));
            }

            if (arrayData == null)
            {
                return -1;
            }

            if (arrayData.Length == 0)
            {
                return -1;
            }

            ArraySegment<T> segment = new ArraySegment<T>(arrayData);

            int byteOffset = BufferData[bufferIndex].Length;

            int byteLength = BufferData[bufferIndex].Append(segment, stride);

            GltfBufferView bufferView = new GltfBufferView
            {
                buffer = 0,
                byteOffset = byteOffset,
                byteLength = byteLength,
                byteStride = (stride == 0) ? 4 : stride,  // @notice default is 4
                target = target,
            };

            Gltf.bufferViews.Add(bufferView);

            Gltf.buffers[bufferIndex].byteLength = BufferData[bufferIndex].Length;

            int bufferViewIndex = Gltf.bufferViews.Count - 1;

            return bufferViewIndex;
        }

        #endregion

        #region Accessor

        /// <summary>
        /// Get a accessor from buffer.
        /// </summary>
        /// <param name="accessorIndex">The index of the accessor.</param>
        /// <returns>gltf.accessor</returns>
        public GltfAccessor GetAccessor(int accessorIndex)
        {
            if (Gltf.accessors.TryGetValue(accessorIndex, out GltfAccessor accessor) == false)
            {
                throw new IndexOutOfRangeException($"gltf.accessors[{accessorIndex}]");
            }

            return accessor;
        }

        #endregion

        #region Accessor (Import)

        /// <summary>
        /// Gets array data from the buffer through the accessor.
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <param name="accessorIndex">The index of the accessor.</param>
        /// <returns>Array data.</returns>
        public T[] GetAccessorArrayData<T>(int accessorIndex) where T : struct
        {
            GltfAccessor accessor = GetAccessor(accessorIndex);

            if (AccessorTypeInfoColection.IsMatch(typeof(T), accessor.type, accessor.componentType) == false)
            {
                throw new Exception($"type: {typeof(T)}, accessor.type:{accessor.type}, accessor.componentType: {accessor.componentType}");
            }

            if (accessor.count == 0)
            {
                return new T[] { };
            }

            if (accessor.HasSparse)
            {
                return GetAccessorArrayDataWithSparse<T>(accessor);
            }
            else
            {
                return GetAccessorArrayDataWithoutSparse<T>(accessor);
            }
        }

        /// <summary>
        /// Gets array data from the buffer through the accessor (non sparse).
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <param name="accessor">An accessor.</param>
        /// <returns>Array data.</returns>
        protected T[] GetAccessorArrayDataWithoutSparse<T>(GltfAccessor accessor) where T : struct
        {
            if (accessor.HasSparse)
            {
                throw new Exception();
            }

            GltfBufferView bufferView = GetBufferView(accessor.bufferView);

            // @notice Loosen the check.
            //if (bufferView.target == GltfBufferViewTarget.NONE)
            //{
            //    throw new FormatException($"bufferView.target: {bufferView.target}");
            //}

            ArraySegment<byte> accessorBytes = BufferData[bufferView.buffer].GetBytes()
                .Slice(bufferView.byteOffset, bufferView.byteLength)
                .Slice(accessor.byteOffset, accessor.ByteLength);

            T[] data = new T[accessor.count];

            accessorBytes.MarshalCopyTo(data);

            return data;
        }

        /// <summary>
        /// Gets array data from the buffer through the accessor with sparse.
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <param name="accessor">An accessor.</param>
        /// <returns>Array data.</returns>
        protected T[] GetAccessorArrayDataWithSparse<T>(GltfAccessor accessor) where T : struct
        {
            if (accessor.HasSparse == false)
            {
                throw new Exception();
            }

            GltfAccessorSparse sparse = accessor.sparse;

            if (sparse.count > accessor.count)
            {
                throw new FormatException($"accessor.count: {accessor.count}, sparse.count: {sparse.count}");
            }

            T[] data = new T[accessor.count];

            T[] values = GetAccessorSparseValues<T>(sparse);

            if (sparse.indices.componentType == GltfComponentType.UNSIGNED_BYTE)
            {
                byte[] indices = GetAccessorSparseIndices<byte>(sparse);

                if ((indices.Length != sparse.count) || (values.Length != sparse.count))
                {
                    throw new FormatException($"sparse.count: {sparse.count}, indices.length: {indices.Length}, values.length: {values.Length}");
                }

                for (int i = 0; i < sparse.count; i++)
                {
                    data[indices[i]] = values[i];
                }
            }
            else if (sparse.indices.componentType == GltfComponentType.UNSIGNED_SHORT)
            {
                ushort[] indices = GetAccessorSparseIndices<ushort>(sparse);

                if ((indices.Length != sparse.count) || (values.Length != sparse.count))
                {
                    throw new FormatException($"sparse.count: {sparse.count}, indices.length: {indices.Length}, values.length: {values.Length}");
                }

                for (int i = 0; i < sparse.count; i++)
                {
                    data[indices[i]] = values[i];
                }
            }
            else if (sparse.indices.componentType == GltfComponentType.UNSIGNED_INT)
            {
                uint[] indices = GetAccessorSparseIndices<uint>(sparse);

                if ((indices.Length != sparse.count) || (values.Length != sparse.count))
                {
                    throw new FormatException($"sparse.count: {sparse.count}, indices.length: {indices.Length}, values.length: {values.Length}");
                }

                for (int i = 0; i < sparse.count; i++)
                {
                    data[indices[i]] = values[i];
                }
            }
            else
            {
                throw new NotSupportedException($"sparse.indices.componentType: {sparse.indices.componentType}");
            }

            return data;
        }

        /// <summary>
        /// Gets sparse indices from the buffer through the accessor sparse.
        /// </summary>
        /// <typeparam name="T">Type of indices.</typeparam>
        /// <param name="sparse">The accessor sparse.</param>
        /// <returns>Tha sparse indices array.</returns>
        protected T[] GetAccessorSparseIndices<T>(GltfAccessorSparse sparse) where T : struct
        {
            if (Gltf.bufferViews.TryGetValue(sparse.indices.bufferView, out GltfBufferView bufferView) == false)
            {
                throw new IndexOutOfRangeException($"sparse.indices.bufferView: {sparse.indices.bufferView}");
            }

            if (bufferView.buffer.IsInRangeOf(Gltf.buffers) == false)
            {
                throw new IndexOutOfRangeException($"gltf.bufferViews[{sparse.indices.bufferView}].buffer: {bufferView.buffer}");
            }

            if (bufferView.target != GltfBufferViewTarget.ELEMENT_ARRAY_BUFFER)
            {
                if (bufferView.target == GltfBufferViewTarget.NONE)
                {
                    // @notice Loosen the check.
                }
                else
                {
                    throw new FormatException($"gltf.bufferViews[{sparse.indices.bufferView}].target: {bufferView.target}");
                }
            }

            if ((sparse.indices.componentType != GltfComponentType.UNSIGNED_BYTE) &&
                (sparse.indices.componentType != GltfComponentType.UNSIGNED_SHORT) &&
                (sparse.indices.componentType != GltfComponentType.UNSIGNED_INT))
            {
                throw new FormatException($"sparse.indices.componentType: {sparse.indices.componentType}");
            }

            int elementSize = Marshal.SizeOf(typeof(T));

            ArraySegment<byte> sparseIndicesBytes = BufferData[bufferView.buffer].GetBytes()
                .Slice(bufferView.byteOffset, bufferView.byteLength)
                .Slice(sparse.indices.byteOffset, elementSize * sparse.count);

            T[] indices = new T[sparse.count];

            sparseIndicesBytes.MarshalCopyTo(indices);

            return indices;
        }

        /// <summary>
        /// Gets sparse values from the buffer through the accessor sparse.
        /// </summary>
        /// <typeparam name="T">Type of values.</typeparam>
        /// <param name="sparse">The accessor sparse.</param>
        /// <returns>Tha sparse values array.</returns>
        protected T[] GetAccessorSparseValues<T>(GltfAccessorSparse sparse) where T : struct
        {
            if (Gltf.bufferViews.TryGetValue(sparse.values.bufferView, out GltfBufferView bufferView) == false)
            {
                throw new IndexOutOfRangeException($"sparse.values.bufferView: {sparse.values.bufferView}");
            }

            if (bufferView.buffer.IsInRangeOf(Gltf.buffers) == false)
            {
                throw new IndexOutOfRangeException($"gltf.bufferViews[{sparse.values.bufferView}].buffer: {bufferView.buffer}");
            }

            int elementSize = Marshal.SizeOf(typeof(T));

            ArraySegment<byte> sparseValuesBytes = BufferData[bufferView.buffer].GetBytes()
                .Slice(bufferView.byteOffset, bufferView.byteLength)
                .Slice(sparse.values.byteOffset, elementSize * sparse.count);

            T[] values = new T[sparse.count];

            sparseValuesBytes.MarshalCopyTo(values);

            return values;
        }

        #endregion

        #region Accessor (Export)

        /// <summary>
        /// Add an accessor (non sparse) to buffer.
        /// </summary>
        /// <typeparam name="T">Type of arrayData.</typeparam>
        /// <param name="bufferIndex">The index of the buffer.</param>
        /// <param name="arrayData"></param>
        /// <param name="target">The target that the GPU buffer should be bound to.</param>
        /// <param name="min">Minimum value of each component in this attribute.</param>
        /// <param name="max">Maximum value of each component in this attribute.</param>
        /// <returns>The index of gltf.accessor.</returns>
        public int AddAccessorWithoutSparse<T>(int bufferIndex, T[] arrayData, GltfBufferViewTarget target, float[] min = null, float[] max = null)
            where T : struct
        {
            if (bufferIndex.IsInRangeOf(Gltf.buffers) == false)
            {
                throw new IndexOutOfRangeException(nameof(bufferIndex));
            }

            if (target == GltfBufferViewTarget.NONE)
            {
                throw new ArgumentException(nameof(target));
            }

            if (arrayData == null)
            {
                return -1;
            }

            if (arrayData.Length == 0)
            {
                return -1;
            }

            if (AccessorTypeInfoColection.TryGetValue(typeof(T), out AccessorTypeInfo accessorTypeInfo) == false)
            {
                throw new NotImplementedException(typeof(T).Name);
            }

            int stride = Marshal.SizeOf(typeof(T));

            int bufferViewIndex = AddBufferView(bufferIndex, arrayData, stride, target);

            var gltfAccessor = new GltfAccessor
            {
                bufferView = bufferViewIndex,
                byteOffset = 0,
                componentType = accessorTypeInfo.ComponentType,
                type = accessorTypeInfo.AccessorType,
                count = arrayData.Length,
                max = max,
                min = min,
            };

            Gltf.accessors.Add(gltfAccessor);

            int accessorIndex = Gltf.accessors.Count - 1;

            return accessorIndex;
        }

        /// <summary>
        /// Add an accessor (with sparse) to buffer.
        /// </summary>
        /// <typeparam name="TValue">Type of sparseValues.</typeparam>
        /// <param name="bufferIndex">The index of the buffer.</param>
        /// <param name="target">The target that the GPU buffer should be bound to.</param>
        /// <param name="accessorCount">The number of attributes referenced by this accessor.</param>
        /// <param name="sparseIndices">Array data of sparce indeces.</param>
        /// <param name="sparseValues">Array data of sparce values.</param>
        /// <param name="min">Minimum value of each component in this attribute.</param>
        /// <param name="max">Maximum value of each component in this attribute.</param>
        /// <returns>The index of gltf.accessor.</returns>
        public int AddAccessorWithSparse<TValue>(int bufferIndex, GltfBufferViewTarget target, int accessorCount,
            int[] sparseIndices, TValue[] sparseValues, float[] min = null, float[] max = null)
            where TValue : struct
        {
            if (bufferIndex.IsInRangeOf(Gltf.buffers) == false)
            {
                throw new IndexOutOfRangeException(nameof(bufferIndex));
            }

            if (target == GltfBufferViewTarget.NONE)
            {
                throw new ArgumentException(nameof(target));
            }

            if (accessorCount == 0)
            {
                return -1;
            }

            if ((sparseValues == null) || (sparseValues.Length == 0))
            {
                return -1;
            }

            if ((sparseIndices == null) || (sparseIndices.Length == 0))
            {
                return -1;
            }

            if (sparseIndices.Length != sparseValues.Length)
            {
                throw new Exception();
            }

            if (AccessorTypeInfoColection.TryGetValue(typeof(TValue), out AccessorTypeInfo valueTypeInfo) == false)
            {
                throw new NotImplementedException(typeof(TValue).Name);
            }

            int sparseIndicesViewIndex;

            GltfComponentType sparseIndicesComponentType;

            if (sparseIndices.Length <= byte.MaxValue)
            {
                byte[] byteSparseIndices = sparseIndices.Select(x => (byte)x).ToArray();

                sparseIndicesViewIndex = AddBufferView(bufferIndex, byteSparseIndices, stride: 0, GltfBufferViewTarget.ELEMENT_ARRAY_BUFFER);

                sparseIndicesComponentType = GltfComponentType.UNSIGNED_BYTE;
            }
            else if (sparseIndices.Length <= ushort.MaxValue)
            {
                ushort[] ushortSparseIndices = sparseIndices.Select(x => (ushort)x).ToArray();

                sparseIndicesViewIndex = AddBufferView(bufferIndex, ushortSparseIndices, stride: 0, GltfBufferViewTarget.ELEMENT_ARRAY_BUFFER);

                sparseIndicesComponentType = GltfComponentType.UNSIGNED_SHORT;
            }
            else
            {
                uint[] uintSparseIndices = sparseIndices.Select(x => (uint)x).ToArray();

                sparseIndicesViewIndex = AddBufferView(bufferIndex, uintSparseIndices, stride: 0, GltfBufferViewTarget.ELEMENT_ARRAY_BUFFER);

                sparseIndicesComponentType = GltfComponentType.UNSIGNED_INT;
            }

            int sparseValuesViewIndex = AddBufferView(bufferIndex, sparseValues, stride: 0, target);

            GltfAccessor accessor = new GltfAccessor
            {
                bufferView = -1,
                byteOffset = 0,
                componentType = valueTypeInfo.ComponentType,
                type = valueTypeInfo.AccessorType,
                count = accessorCount,
                max = max,
                min = min,
                sparse = new GltfAccessorSparse
                {
                    count = sparseIndices.Length,
                    indices = new GltfAccessorSparseIndices
                    {
                        bufferView = sparseIndicesViewIndex,
                        componentType = sparseIndicesComponentType,
                    },
                    values = new GltfAccessorSparseValues
                    {
                        bufferView = sparseValuesViewIndex,
                    }
                }
            };

            Gltf.accessors.Add(accessor);

            int accessorIndex = Gltf.accessors.Count - 1;

            return accessorIndex;
        }

        #endregion

        #region Texture (Export)

        /// <summary>
        /// Add an texture to buffer.
        /// </summary>
        /// <param name="bufferIndex">The index of the buffer.</param>
        /// <param name="textureItem">The texture item.</param>
        /// <returns>The index of gltf.texture.</returns>
        public int AddTexture(int bufferIndex, ExportTextureItem textureItem)
        {
            if (bufferIndex.IsInRangeOf(Gltf.buffers) == false)
            {
                throw new IndexOutOfRangeException(nameof(bufferIndex));
            }

            if (textureItem == null)
            {
                throw new ArgumentNullException(nameof(textureItem));
            }

            int bufferViewIndex = AddBufferView(bufferIndex, textureItem.imageBytes, stride: 0, GltfBufferViewTarget.NONE);

            var gltfImage = new GltfImage
            {
                name = textureItem.imageName,
                mimeType = textureItem.mimeType,
                bufferView = bufferViewIndex,
            };

            Gltf.images.Add(gltfImage);

            int imageIndex = Gltf.images.Count - 1;

            var gltfSampler = new GltfTextureSampler
            {
                magFilter = textureItem.magFilter,
                minFilter = textureItem.minFilter,
                wrapS = textureItem.wrapS,
                wrapT = textureItem.wrapT,
            };

            Gltf.samplers.Add(gltfSampler);

            int samplerIndex = Gltf.samplers.Count - 1;

            var gltfTexture = new GltfTexture
            {
                name = textureItem.name,
                sampler = samplerIndex,
                source = imageIndex,
            };

            if ((textureItem.offset != System.Numerics.Vector2.Zero) ||
                (textureItem.scale != System.Numerics.Vector2.One))
            {
                var khrTransform = new KHR_texture_transform
                {
                    offset = textureItem.offset,
                    scale = textureItem.scale,
                };

                gltfTexture.extensions = new GltfExtensions(_JsonSerializerSettings)
                {
                    { KHR_texture_transform.ExtensionName, khrTransform }
                };

                if (Gltf.extensionsUsed.Contains(KHR_texture_transform.ExtensionName) == false)
                {
                    Gltf.extensionsUsed.Add(KHR_texture_transform.ExtensionName);
                }
            }

            Gltf.textures.Add(gltfTexture);

            int textureIndex = Gltf.textures.Count - 1;

            return textureIndex;
        }

        #endregion
    }
}
