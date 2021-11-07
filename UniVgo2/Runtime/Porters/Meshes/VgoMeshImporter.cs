// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : VgoMeshImporter
// ----------------------------------------------------------------------
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    /// <summary>
    /// VGO Mesh Importer
    /// </summary>
    public class VgoMeshImporter
    {
        #region Fields

        /// <summary>Whether import tangents.</summary>
        /// <remarks>setting</remarks>
        protected bool _ImportTangents = false;

        #endregion

        #region Properties

        /// <summary>The VGO storage adapter.</summary>
        public VgoStorageAdapter StorageAdapter { get; set; }

        /// <summary>List of unity material.</summary>
        public List<Material> UnityMaterialList { get; set; }

        /// <summary>List of scriptable object.</summary>
        public List<ScriptableObject> ScriptableObjectList { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Create a mesh asset.
        /// </summary>
        /// <param name="meshIndex">The index of vgo mesh.</param>
        /// <returns>A mesh asset.</returns>
        public virtual MeshAsset CreateMeshAsset(int meshIndex)
        {
            if (StorageAdapter == null)
            {
                throw new Exception();
            }

            if (UnityMaterialList == null)
            {
                throw new Exception();
            }

            MeshContext meshContext = ReadMesh(meshIndex, out BlendShapeConfiguration blendShapeConfig);

            Mesh mesh = BuildMesh(meshContext);

            MeshAsset meshAsset = new MeshAsset
            {
                Mesh = mesh,
                Materials = meshContext.materialIndices.Select(x => UnityMaterialList[x]).ToArray(),
                BlendShapeConfiguration = blendShapeConfig,
            };

            ScriptableObjectList.Add(blendShapeConfig);

            return meshAsset;
        }

        #endregion

        #region ReadMesh

        /// <summary>
        /// Read a mesh.
        /// </summary>
        /// <param name="meshIndex">The index of vgo mesh.</param>
        /// <param name="blendShapeConfig">A blend shape configuration.</param>
        /// <returns>A mesh context.</returns>
        protected virtual MeshContext ReadMesh(int meshIndex, out BlendShapeConfiguration blendShapeConfig)
        {
            VgoMesh vgoMesh = StorageAdapter.Layout.meshes[meshIndex];

            string meshName = (string.IsNullOrEmpty(vgoMesh.name) == false) ? vgoMesh.name : string.Format($"mesh_{meshIndex}");

            var meshContext = new MeshContext(meshName);

            // Attributes
            SetPrimitiveAttributes(meshContext, vgoMesh.attributes, out int positionsCount);

            // SubMeshes
            meshContext.subMeshes = CreateSubMeshes(vgoMesh.subMeshes, positionsCount);

            // Materials
            meshContext.materialIndices = vgoMesh.materials;

            // BlendShapes
            meshContext.blendShapes = CreateBlendShapes(vgoMesh.blendShapes, out blendShapeConfig);

            // BlendShapeConfiguration
            if ((vgoMesh.blendShapeKind != VgoBlendShapeKind.None) ||
                (vgoMesh.blendShapePesets != null))
            {
                if (blendShapeConfig == null)
                {
                    blendShapeConfig = ScriptableObject.CreateInstance<BlendShapeConfiguration>();
                }

                blendShapeConfig.name = vgoMesh.name;

                blendShapeConfig.kind = vgoMesh.blendShapeKind;

                if (vgoMesh.blendShapePesets != null)
                {
                    blendShapeConfig.presets = vgoMesh.blendShapePesets;
                }
            }
            else
            {
                blendShapeConfig = null;
            }

            return meshContext;
        }

        #endregion

        #region ReadMesh (Attributes)

        /// <summary>
        /// Set primitives.
        /// </summary>
        /// <param name="meshContext">The mesh context.</param>
        /// <param name="attributes">The mesh primitive attributes.</param>
        /// <param name="positionsCount">The count of positions.</param>
        protected virtual void SetPrimitiveAttributes(MeshContext meshContext, VgoMeshPrimitiveAttributes attributes, out int positionsCount)
        {
            positionsCount = 0;

            // Positions
            if (attributes.POSITION != -1)
            {
                Vector3[] positions = StorageAdapter.GetAccessorArrayData<Vector3>(attributes.POSITION);

                if (StorageAdapter.GeometryCoordinate == VgoGeometryCoordinate.RightHanded)
                {
                    meshContext.positions = new Vector3[positions.Length];

                    for (int idx = 0; idx < positions.Length; idx++)
                    {
                        meshContext.positions[idx] = positions[idx].ReverseZ();
                    }
                }
                else
                {
                    meshContext.positions = positions;
                }

                positionsCount = positions.Length;
            }

            // Normals
            if (attributes.NORMAL != -1)
            {
                Vector3[] normals = StorageAdapter.GetAccessorArrayData<Vector3>(attributes.NORMAL);

                if (StorageAdapter.GeometryCoordinate == VgoGeometryCoordinate.RightHanded)
                {
                    meshContext.normals = new Vector3[normals.Length];

                    for (int idx = 0; idx < normals.Length; idx++)
                    {
                        meshContext.normals[idx] = normals[idx].ReverseZ();
                    }
                }
                else
                {
                    meshContext.normals = normals;
                }
            }

            // Tangents
            if (_ImportTangents)
            {
                if (attributes.TANGENT != -1)
                {
                    VgoResourceAccessor tangentAccessor = StorageAdapter.GetAccessor(attributes.TANGENT);

                    Vector4[] tangents;

                    if (tangentAccessor.dataType == VgoResourceAccessorDataType.Vector4Float)
                    {
                        tangents = StorageAdapter.GetAccessorArrayData<Vector4>(attributes.TANGENT);
                    }
                    else
                    {
                        throw new NotImplementedException(string.Format("unknown tangentAccessor.type: {0}", tangentAccessor.dataType));
                    }

                    if (StorageAdapter.GeometryCoordinate == VgoGeometryCoordinate.RightHanded)
                    {
                        meshContext.tangents = new Vector4[tangents.Length];

                        for (int idx = 0; idx < tangents.Length; idx++)
                        {
                            meshContext.tangents[idx] = tangents[idx].ReverseZ();
                        }
                    }
                    else
                    {
                        meshContext.tangents = tangents;
                    }
                }
            }

            // UVs
            {
                meshContext.uv0s = ReadUV(attributes.TEXCOORD_0, positionsCount);
                meshContext.uv1s = ReadUV(attributes.TEXCOORD_1, -1);
                meshContext.uv2s = ReadUV(attributes.TEXCOORD_2, -1);
                meshContext.uv3s = ReadUV(attributes.TEXCOORD_3, -1);
            }

            // Colors
            if (attributes.COLOR_0 != -1)
            {
                VgoResourceAccessor colorAccessor = StorageAdapter.GetAccessor(attributes.COLOR_0);

                if (colorAccessor.dataType == VgoResourceAccessorDataType.Vector4UInt8)
                {
                    // @notice Vector4(byte) = Color32
                    Color32[] colors = StorageAdapter.GetAccessorArrayData<Color32>(attributes.COLOR_0);

                    meshContext.color32s = colors;
                }
                else if (colorAccessor.dataType == VgoResourceAccessorDataType.Vector4Float)
                {
                    // @notice Vector4(float) = Color
                    Color[] colors = StorageAdapter.GetAccessorArrayData<Color>(attributes.COLOR_0);

                    meshContext.colors = colors;
                }
                else
                {
                    throw new NotImplementedException(string.Format("unknown colorAccessor.dataType: {0}", colorAccessor.dataType));
                }
            }

            // Joints & Weights -> BoneWeights
            if ((attributes.JOINTS_0 != -1) && (attributes.WEIGHTS_0 != -1))
            {
                // Joints
                VgoResourceAccessor jointsAccessor = StorageAdapter.GetAccessor(attributes.JOINTS_0);

                Vector4Ushort[] joints;

                if (jointsAccessor.dataType == VgoResourceAccessorDataType.Vector4UInt8)
                {
                    ReadOnlySpan<Vector4Ubyte> vec4byteSpan = StorageAdapter.GetAccessorSpan<Vector4Ubyte>(attributes.JOINTS_0);

                    joints = new Vector4Ushort[vec4byteSpan.Length];

                    for (int i = 0; i < vec4byteSpan.Length; i++)
                    {
                        joints[i] = new Vector4Ushort(
                            vec4byteSpan[i].X,
                            vec4byteSpan[i].Y,
                            vec4byteSpan[i].Z,
                            vec4byteSpan[i].W);
                    }
                }
                else if (jointsAccessor.dataType == VgoResourceAccessorDataType.Vector4UInt16)
                {
                    joints = StorageAdapter.GetAccessorArrayData<Vector4Ushort>(attributes.JOINTS_0);
                }
                else
                {
                    throw new NotImplementedException(string.Format("unknown jointsAccessor.dataType: {0}", jointsAccessor.dataType));
                }

                // Weights
                Vector4[] weights = StorageAdapter.GetAccessorArrayData<Vector4>(attributes.WEIGHTS_0);

                // BoneWeights
                if (joints.Length == weights.Length)
                {
                    meshContext.boneWeights = new BoneWeight[joints.Length];

                    for (int idx = 0; idx < joints.Length; idx++)
                    {
                        meshContext.boneWeights[idx] = ConvertJointAndWeightToBoneWeight(joints[idx], weights[idx]);
                    }
                }
            }
        }

        /// <summary>
        /// Read UV.
        /// </summary>
        /// <param name="texcoord">The accessor index of texture coord.</param>
        /// <param name="positionsCount">The positions count.</param>
        /// <returns>An array of UV.</returns>
        protected virtual Vector2[] ReadUV(int texcoord, int positionsCount)
        {
            Vector2[] uvs;

            if (texcoord == -1)
            {
                if (positionsCount >= 0)
                {
                    uvs = new Vector2[positionsCount];
                }
                else
                {
                    uvs = null;
                }
            }
            else
            {
                Vector2[] resourceUvs = StorageAdapter.GetAccessorArrayData<Vector2>(texcoord);

                if (StorageAdapter.UVCoordinate == VgoUVCoordinate.TopLeft)
                {
                    uvs = new Vector2[resourceUvs.Length];

                    for (int idx = 0; idx < resourceUvs.Length; idx++)
                    {
                        uvs[idx] = resourceUvs[idx].ReverseUV();
                    }
                }
                else
                {
                    uvs = resourceUvs;
                }
            }

            return uvs;
        }

        #endregion

        #region ReadMesh (SubMesh)

        /// <summary>
        /// Create sub meshes.
        /// </summary>
        /// <param name="vgoSubMeshes">The vgo subMesh accessor indices.</param>
        /// <param name="positionsLength"></param>
        /// <returns>List of blend shape.</returns>
        protected virtual List<int[]> CreateSubMeshes(List<int> vgoSubMeshes, int positionsLength)
        {
            if ((vgoSubMeshes == null) || (vgoSubMeshes.Any() == false))
            {
                return null;
            }

            List<int[]> subMeshes = new List<int[]>(vgoSubMeshes.Count);

            for (int subMeshIndex = 0; subMeshIndex < vgoSubMeshes.Count; subMeshIndex++)
            {
                if (vgoSubMeshes[subMeshIndex] == -1)
                {
                    subMeshes.Add(Enumerable.Range(0, positionsLength).ToArray());
                }
                else
                {
                    int[] indices = GetSubMeshIndices(vgoSubMeshes[subMeshIndex]);

                    if (StorageAdapter.GeometryCoordinate == VgoGeometryCoordinate.RightHanded)
                    {
                        indices = indices.FlipTriangle();
                    }

                    subMeshes.Add(indices);
                }
            }

            return subMeshes;
        }

        /// <summary>
        /// Get submesh indices.
        /// </summary>
        /// <param name="accessorIndex">The index of accessor.</param>
        /// <returns>The indices.</returns>
        protected virtual int[] GetSubMeshIndices(int accessorIndex)
        {
            VgoResourceAccessor accessor = StorageAdapter.GetAccessor(accessorIndex);

            int[] indices;

            switch (accessor.dataType)
            {
                case VgoResourceAccessorDataType.UnsignedByte:
                    //indices = VgoStorageAdapter.GetAccessorArrayData<byte>(accessorIndex).Select(x => (int)x);
                    {
                        ReadOnlySpan<byte> indexSpan = StorageAdapter.GetAccessorSpan<byte>(accessorIndex);

                        indices = new int[indexSpan.Length];

                        for (int i = 0; i < indexSpan.Length; i++)
                        {
                            indices[i] = indexSpan[i];
                        }
                    }
                    break;

                case VgoResourceAccessorDataType.UnsignedShort:
                    //indices = VgoStorageAdapter.GetAccessorArrayData<ushort>(accessorIndex).Select(x => (int)x);
                    {
                        ReadOnlySpan<ushort> indexSpan = StorageAdapter.GetAccessorSpan<ushort>(accessorIndex);

                        indices = new int[indexSpan.Length];

                        for (int i = 0; i < indexSpan.Length; i++)
                        {
                            indices[i] = indexSpan[i];
                        }
                    }
                    break;

                case VgoResourceAccessorDataType.UnsignedInt:
                    //indices = VgoStorageAdapter.GetAccessorArrayData<uint>(accessorIndex).Select(x => (int)x);
                    {
                        ReadOnlySpan<uint> indexSpan = StorageAdapter.GetAccessorSpan<uint>(accessorIndex);

                        indices = new int[indexSpan.Length];

                        for (int i = 0; i < indexSpan.Length; i++)
                        {
                            indices[i] = (int)indexSpan[i];
                        }
                    }
                    break;

                default:
                    throw new NotSupportedException($"accessor.dataType: {accessor.dataType}");
            }

            return indices;
        }

        #endregion

        #region ReadMesh (BlendShapes)

        /// <summary>
        /// Create blend shapes.
        /// </summary>
        /// <param name="vgoBlendShapes">List of vgo blend shape.</param>
        /// <param name="blendShapeConfig">A blend shape configuration.</param>
        /// <returns>List of blend shape.</returns>
        protected virtual List<BlendShape> CreateBlendShapes(List<VgoMeshBlendShape> vgoBlendShapes, out BlendShapeConfiguration blendShapeConfig)
        {
            if ((vgoBlendShapes == null) || (vgoBlendShapes.Any() == false))
            {
                blendShapeConfig = null;

                return null;
            }

            blendShapeConfig = ScriptableObject.CreateInstance<BlendShapeConfiguration>();

            List<BlendShape> blendShapes = new List<BlendShape>(vgoBlendShapes.Count);

            for (int shapeIndex = 0; shapeIndex < vgoBlendShapes.Count; shapeIndex++)
            {
                VgoMeshBlendShape vgoBlendShape = vgoBlendShapes[shapeIndex];

                string name = (string.IsNullOrEmpty(vgoBlendShape.name) == false) ? vgoBlendShape.name : shapeIndex.ToString();

                BlendShape blendShape = new BlendShape(name);

                VgoMeshPrimitiveAttributes attributes = vgoBlendShape.attributes;

                if (attributes.POSITION != -1)
                {
                    Vector3[] positions = StorageAdapter.GetAccessorArrayData<Vector3>(attributes.POSITION);

                    if (StorageAdapter.GeometryCoordinate == VgoGeometryCoordinate.RightHanded)
                    {
                        blendShape.positions = new Vector3[positions.Length];

                        for (int idx = 0; idx < positions.Length; idx++)
                        {
                            blendShape.positions[idx] = positions[idx].ReverseZ();
                        }
                    }
                    else
                    {
                        blendShape.positions = positions;
                    }
                }

                if (attributes.NORMAL != -1)
                {
                    Vector3[] normals = StorageAdapter.GetAccessorArrayData<Vector3>(attributes.NORMAL);

                    if (StorageAdapter.GeometryCoordinate == VgoGeometryCoordinate.RightHanded)
                    {
                        blendShape.normals = new Vector3[normals.Length];

                        for (int idx = 0; idx < normals.Length; idx++)
                        {
                            blendShape.normals[idx] = normals[idx].ReverseZ();
                        }
                    }
                    else
                    {
                        blendShape.normals = normals;
                    }
                }

                if (attributes.TANGENT != -1)
                {
                    Vector3[] tangents = StorageAdapter.GetAccessorArrayData<Vector3>(attributes.TANGENT);

                    if (StorageAdapter.GeometryCoordinate == VgoGeometryCoordinate.RightHanded)
                    {
                        blendShape.tangents = new Vector3[tangents.Length];

                        for (int idx = 0; idx < tangents.Length; idx++)
                        {
                            blendShape.tangents[idx] = tangents[idx].ReverseZ();
                        }
                    }
                    else
                    {
                        blendShape.tangents = tangents;
                    }
                }

                blendShapes.Add(blendShape);

                if (vgoBlendShape.facePartsType != VgoBlendShapeFacePartsType.None)
                {
                    var facePart = new BlendShapeFacePart
                    {
                        index = shapeIndex,
                        type = vgoBlendShape.facePartsType,
                    };

                    blendShapeConfig.faceParts.Add(facePart);
                }

                if (vgoBlendShape.blinkType != VgoBlendShapeBlinkType.None)
                {
                    var blink = new BlendShapeBlink
                    {
                        index = shapeIndex,
                        type = vgoBlendShape.blinkType,
                    };

                    blendShapeConfig.blinks.Add(blink);
                }

                if (vgoBlendShape.visemeType != VgoBlendShapeVisemeType.None)
                {
                    var viseme = new BlendShapeViseme
                    {
                        index = shapeIndex,
                        type = vgoBlendShape.visemeType,
                    };

                    blendShapeConfig.visemes.Add(viseme);
                }
            }

            return blendShapes;
        }

        #endregion

        #region BuildMesh

        /// <summary>
        /// Build a unity mesh.
        /// </summary>
        /// <param name="meshContext">The mesh context.</param>
        /// <returns>A unity mesh.</returns>
        protected virtual Mesh BuildMesh(MeshContext meshContext)
        {
            if (meshContext.materialIndices.Any() == false)
            {
                meshContext.materialIndices.Add(0);
            }

            Mesh mesh = new Mesh
            {
                name = meshContext.name
            };

            // Positions
            if (meshContext.positions.Length > UInt16.MaxValue)
            {
                // UNITY_2017_3_OR_NEWER
                mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            }
            mesh.SetVertices(meshContext.positions);

            // Normals
            bool recalculateNormals = true;

            if ((meshContext.normals != null) && meshContext.normals.Any())
            {
                mesh.SetNormals(meshContext.normals);

                recalculateNormals = false;
            }

            // Tangents
            bool recalculateTangents = true;

            if (_ImportTangents)
            {
                if ((meshContext.tangents != null) && meshContext.tangents.Any())
                {
                    mesh.SetTangents(meshContext.tangents);

                    recalculateTangents = false;
                }
            }

            // UVs
            if ((meshContext.uv0s != null) && meshContext.uv0s.Any())
            {
                mesh.SetUVs(channel: 0, meshContext.uv0s);
            }
            if ((meshContext.uv1s != null) && meshContext.uv1s.Any())
            {
                mesh.SetUVs(channel: 1, meshContext.uv1s);
            }
            if ((meshContext.uv2s != null) && meshContext.uv2s.Any())
            {
                mesh.SetUVs(channel: 2, meshContext.uv2s);
            }
            if ((meshContext.uv3s != null) && meshContext.uv3s.Any())
            {
                mesh.SetUVs(channel: 3, meshContext.uv3s);
            }

            // Colors
            if ((meshContext.color32s != null) && meshContext.color32s.Any())
            {
                mesh.SetColors(meshContext.color32s);
            }
            else if ((meshContext.colors != null) && meshContext.colors.Any())
            {
                mesh.SetColors(meshContext.colors);
            }

            // BoneWeights
            if ((meshContext.boneWeights != null) && meshContext.boneWeights.Any())
            {
                mesh.boneWeights = meshContext.boneWeights;
            }

            // SubMesh
            mesh.subMeshCount = meshContext.subMeshes.Count;

            for (int i = 0; i < meshContext.subMeshes.Count; ++i)
            {
                mesh.SetTriangles(meshContext.subMeshes[i], i);
            }

            // Normals
            if (recalculateNormals)
            {
                mesh.RecalculateNormals();
            }

            // Tangents
            if (recalculateTangents)
            {
                mesh.RecalculateTangents();
            }

            // BlendShape
            if (meshContext.blendShapes != null)
            {
                Vector3[] emptyVertices = null;

                foreach (BlendShape blendShape in meshContext.blendShapes)
                {
                    if (blendShape.positions?.Length > 0)
                    {
                        if (blendShape.positions.Length == mesh.vertexCount)
                        {
                            Vector3[] deltaPositions = blendShape.positions;

                            Vector3[] deltaNormals = null;
                            if ((meshContext.normals != null) &&
                                (meshContext.normals.Length == mesh.vertexCount) &&
                                (blendShape.normals.Count() == blendShape.positions.Count()))
                            {
                                deltaNormals = blendShape.normals;
                            }

                            mesh.AddBlendShapeFrame(blendShape.name, frameWeight: 100.0f, deltaPositions, deltaNormals, deltaTangents: null);
                        }
                        else
                        {
                            Debug.LogWarningFormat("May be partial primitive has blendShape. Require separate mesh or extend blend shape, but not implemented: {0}", blendShape.name);
                        }
                    }
                    else
                    {
                        if (emptyVertices == null)
                        {
                            emptyVertices = new Vector3[mesh.vertexCount];
                        }

                        // add empty blend shape for keep blend shape index
                        mesh.AddBlendShapeFrame(blendShape.name, frameWeight: 100.0f, emptyVertices, deltaNormals: null, deltaTangents: null);
                    }
                }
            }

            return mesh;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Convert joint and weight to bone weight.
        /// </summary>
        /// <param name="joint">A joint.</param>
        /// <param name="weight">A weight.</param>
        /// <returns>A bone weight.</returns>
        protected virtual BoneWeight ConvertJointAndWeightToBoneWeight(Vector4Ushort joint, Vector4 weight)
        {
            float sum = weight.x + weight.y + weight.z + weight.w;
            float f = 1.0f / sum;

            BoneWeight boneWeight = new BoneWeight
            {
                boneIndex0 = joint.X,
                boneIndex1 = joint.Y,
                boneIndex2 = joint.Z,
                boneIndex3 = joint.W,
                weight0 = weight.x * f,
                weight1 = weight.y * f,
                weight2 = weight.z * f,
                weight3 = weight.w * f,
            };

            return boneWeight;
        }

        #endregion
    }
}
