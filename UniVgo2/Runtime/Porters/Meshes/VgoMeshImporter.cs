// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : VgoMeshImporter
// ----------------------------------------------------------------------
#nullable enable
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
    public class VgoMeshImporter : IVgoMeshImporter
    {
        #region Fields

        /// <summary>The mesh importer option.</summary>
        protected readonly MeshImporterOption _Option;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of VgoMeshImporter.
        /// </summary>
        public VgoMeshImporter() : this(new MeshImporterOption()) { }

        /// <summary>
        /// Create a new instance of VgoMeshImporter with option.
        /// </summary>
        /// <param name="option"></param>
        public VgoMeshImporter(MeshImporterOption option)
        {
            _Option = option;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Create a mesh asset.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="meshIndex">The index of vgo mesh.</param>
        /// <param name="unityMaterialList">List of unity material.</param>
        /// <returns>A mesh asset.</returns>
        public virtual MeshAsset CreateMeshAsset(IVgoStorage vgoStorage, int meshIndex, IList<Material?>? unityMaterialList = null)
        {
            if (vgoStorage == null)
            {
                throw new ArgumentNullException(nameof(vgoStorage));
            }

            MeshContext meshContext = ReadMesh(vgoStorage, meshIndex);

            Mesh mesh = BuildMesh(meshContext);

            MeshAsset meshAsset = new MeshAsset(mesh);

            if (vgoStorage.IsSpecVersion_2_4_orLower)
            {
                if (unityMaterialList == null)
                {
                    throw new ArgumentNullException(nameof(unityMaterialList));
                }

                meshAsset.Materials = meshContext.MaterialIndices.Select(x => unityMaterialList[x]).ToArray();
            }

            if ((meshContext.BlendShapesContext != null) &&
                (meshContext.BlendShapesContext.BlendShapeConfig != null))
            {
                meshAsset.BlendShapeConfig = meshContext.BlendShapesContext.BlendShapeConfig;
            }

            return meshAsset;
        }

        #endregion

        #region ReadMesh

        /// <summary>
        /// Read a mesh.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="meshIndex">The index of vgo mesh.</param>
        /// <param name="blendShapeConfig">A blend shape configuration.</param>
        /// <returns>A mesh context.</returns>
        protected virtual MeshContext ReadMesh(IVgoStorage vgoStorage, int meshIndex)
        {
            if (vgoStorage.Layout.meshes == null)
            {
                throw new Exception();
            }

            VgoMesh? vgoMesh = vgoStorage.Layout.meshes[meshIndex];

            if (vgoMesh is null)
            {
                throw new Exception();
            }

            string meshName = ((vgoMesh.name is null) || (vgoMesh.name == string.Empty))
                ? string.Format($"mesh_{meshIndex}")
                : vgoMesh.name;

            var meshContext = new MeshContext(meshName);

            if (vgoMesh.attributes == null)
            {
                throw new Exception();
            }

            // Attributes
            SetPrimitiveAttributes(vgoStorage, meshContext, vgoMesh.attributes, out int positionsCount);

            // SubMeshes
            meshContext.SubMeshes = CreateSubMeshes(vgoStorage, vgoMesh.subMeshes, positionsCount);

            if (vgoStorage.IsSpecVersion_2_4_orLower)
            {
                // Materials
                meshContext.MaterialIndices = vgoMesh.materials ?? new List<int>();

                if (meshContext.MaterialIndices.Any() == false)
                {
                    meshContext.MaterialIndices.Add(0);
                }
            }

            // BlendShapes
            meshContext.BlendShapesContext = CreateBlendShapes(vgoStorage, vgoMesh.blendShapes);

            if ((meshContext.BlendShapesContext != null) &&
                (meshContext.BlendShapesContext.BlendShapeConfig != null))
            {
                BlendShapeConfig blendShapeConfig = meshContext.BlendShapesContext.BlendShapeConfig;

                blendShapeConfig.Name = vgoMesh.name;

                if (vgoStorage.IsSpecVersion_2_4_orLower)
                {
                    blendShapeConfig.Kind = vgoMesh.blendShapeKind;

                    if (vgoMesh.blendShapePesets != null)
                    {
                        blendShapeConfig.Presets = vgoMesh.blendShapePesets;
                    }
                }
            }

            return meshContext;
        }

        #endregion

        #region ReadMesh (Attributes)

        /// <summary>
        /// Set primitives.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="meshContext">A mesh context.</param>
        /// <param name="attributes">The mesh primitive attributes.</param>
        /// <param name="positionsCount">The count of positions.</param>
        protected virtual void SetPrimitiveAttributes(IVgoStorage vgoStorage, MeshContext meshContext, VgoMeshPrimitiveAttributes attributes, out int positionsCount)
        {
            positionsCount = 0;

            // Positions
            if (attributes.POSITION != -1)
            {
                Vector3[] positions = vgoStorage.GetAccessorArrayData<Vector3>(attributes.POSITION);

                if (vgoStorage.GeometryCoordinate == VgoGeometryCoordinate.RightHanded)
                {
                    meshContext.Positions = new Vector3[positions.Length];

                    for (int idx = 0; idx < positions.Length; idx++)
                    {
                        meshContext.Positions[idx] = positions[idx].ReverseZ();
                    }
                }
                else
                {
                    meshContext.Positions = positions;
                }

                positionsCount = positions.Length;
            }

            // Normals
            if (attributes.NORMAL != -1)
            {
                Vector3[] normals = vgoStorage.GetAccessorArrayData<Vector3>(attributes.NORMAL);

                if (vgoStorage.GeometryCoordinate == VgoGeometryCoordinate.RightHanded)
                {
                    meshContext.Normals = new Vector3[normals.Length];

                    for (int idx = 0; idx < normals.Length; idx++)
                    {
                        meshContext.Normals[idx] = normals[idx].ReverseZ();
                    }
                }
                else
                {
                    meshContext.Normals = normals;
                }
            }

            // Tangents
            if (_Option.ImportTangents)
            {
                if (attributes.TANGENT != -1)
                {
                    VgoResourceAccessor tangentAccessor = vgoStorage.GetAccessor(attributes.TANGENT);

                    Vector4[] tangents;

                    if (tangentAccessor.dataType == VgoResourceAccessorDataType.Vector4Float)
                    {
                        tangents = vgoStorage.GetAccessorArrayData<Vector4>(attributes.TANGENT);
                    }
                    else
                    {
                        throw new NotImplementedException(string.Format("unknown tangentAccessor.type: {0}", tangentAccessor.dataType));
                    }

                    if (vgoStorage.GeometryCoordinate == VgoGeometryCoordinate.RightHanded)
                    {
                        meshContext.Tangents = new Vector4[tangents.Length];

                        for (int idx = 0; idx < tangents.Length; idx++)
                        {
                            meshContext.Tangents[idx] = tangents[idx].ReverseZ();
                        }
                    }
                    else
                    {
                        meshContext.Tangents = tangents;
                    }
                }
            }

            // UVs
            {
                meshContext.UV0s = ReadUV(vgoStorage, attributes.TEXCOORD_0, positionsCount);
                meshContext.UV1s = ReadUV(vgoStorage, attributes.TEXCOORD_1, -1);
                meshContext.UV2s = ReadUV(vgoStorage, attributes.TEXCOORD_2, -1);
                meshContext.UV3s = ReadUV(vgoStorage, attributes.TEXCOORD_3, -1);
            }

            // Colors
            if (attributes.COLOR_0 != -1)
            {
                VgoResourceAccessor colorAccessor = vgoStorage.GetAccessor(attributes.COLOR_0);

                if (colorAccessor.dataType == VgoResourceAccessorDataType.Vector4UInt8)
                {
                    // @notice Vector4(byte) = Color32
                    Color32[] colors = vgoStorage.GetAccessorArrayData<Color32>(attributes.COLOR_0);

                    meshContext.Color32s = colors;
                }
                else if (colorAccessor.dataType == VgoResourceAccessorDataType.Vector4Float)
                {
                    // @notice Vector4(float) = Color
                    Color[] colors = vgoStorage.GetAccessorArrayData<Color>(attributes.COLOR_0);

                    meshContext.Colors = colors;
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
                VgoResourceAccessor jointsAccessor = vgoStorage.GetAccessor(attributes.JOINTS_0);

                Vector4Ushort[] joints;

                if (jointsAccessor.dataType == VgoResourceAccessorDataType.Vector4UInt8)
                {
                    ReadOnlySpan<Vector4Ubyte> vec4byteSpan = vgoStorage.GetAccessorSpan<Vector4Ubyte>(attributes.JOINTS_0);

                    joints = new Vector4Ushort[vec4byteSpan.Length];

                    for (int idx = 0; idx < vec4byteSpan.Length; idx++)
                    {
                        joints[idx] = new Vector4Ushort(
                            vec4byteSpan[idx].X,
                            vec4byteSpan[idx].Y,
                            vec4byteSpan[idx].Z,
                            vec4byteSpan[idx].W);
                    }
                }
                else if (jointsAccessor.dataType == VgoResourceAccessorDataType.Vector4UInt16)
                {
                    joints = vgoStorage.GetAccessorArrayData<Vector4Ushort>(attributes.JOINTS_0);
                }
                else
                {
                    throw new NotImplementedException(string.Format("unknown jointsAccessor.dataType: {0}", jointsAccessor.dataType));
                }

                // Weights
                Vector4[] weights = vgoStorage.GetAccessorArrayData<Vector4>(attributes.WEIGHTS_0);

                // BoneWeights
                if (joints.Length == weights.Length)
                {
                    meshContext.BoneWeights = new BoneWeight[joints.Length];

                    for (int idx = 0; idx < joints.Length; idx++)
                    {
                        meshContext.BoneWeights[idx] = ConvertJointAndWeightToBoneWeight(joints[idx], weights[idx]);
                    }
                }
            }
        }

        /// <summary>
        /// Read UV.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="texcoord">The accessor index of texture coord.</param>
        /// <param name="positionsCount">The positions count.</param>
        /// <returns>An array of UV.</returns>
        protected virtual Vector2[]? ReadUV(IVgoStorage vgoStorage, int texcoord, int positionsCount)
        {
            Vector2[]? uvs;

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
                Vector2[] resourceUvs = vgoStorage.GetAccessorArrayData<Vector2>(texcoord);

                if (vgoStorage.UVCoordinate == VgoUVCoordinate.TopLeft)
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
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="vgoSubMeshes">The vgo subMesh accessor indices.</param>
        /// <param name="positionsLength"></param>
        /// <returns>List of blend shape.</returns>
        protected virtual List<int[]>? CreateSubMeshes(IVgoStorage vgoStorage, List<int>? vgoSubMeshes, int positionsLength)
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
                    int[] indices = GetSubMeshIndices(vgoStorage, vgoSubMeshes[subMeshIndex]);

                    if (vgoStorage.GeometryCoordinate == VgoGeometryCoordinate.RightHanded)
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
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="accessorIndex">The index of accessor.</param>
        /// <returns>The indices.</returns>
        protected virtual int[] GetSubMeshIndices(IVgoStorage vgoStorage, int accessorIndex)
        {
            VgoResourceAccessor accessor = vgoStorage.GetAccessor(accessorIndex);

            int[] indices;

            switch (accessor.dataType)
            {
                case VgoResourceAccessorDataType.UnsignedByte:
                    //indices = vgoStorage.GetAccessorArrayData<byte>(accessorIndex).Select(x => (int)x);
                    {
                        ReadOnlySpan<byte> indexSpan = vgoStorage.GetAccessorSpan<byte>(accessorIndex);

                        indices = new int[indexSpan.Length];

                        for (int idx = 0; idx < indexSpan.Length; idx++)
                        {
                            indices[idx] = indexSpan[idx];
                        }
                    }
                    break;

                case VgoResourceAccessorDataType.UnsignedShort:
                    //indices = vgoStorage.GetAccessorArrayData<ushort>(accessorIndex).Select(x => (int)x);
                    {
                        ReadOnlySpan<ushort> indexSpan = vgoStorage.GetAccessorSpan<ushort>(accessorIndex);

                        indices = new int[indexSpan.Length];

                        for (int idx = 0; idx < indexSpan.Length; idx++)
                        {
                            indices[idx] = indexSpan[idx];
                        }
                    }
                    break;

                case VgoResourceAccessorDataType.UnsignedInt:
                    //indices = vgoStorage.GetAccessorArrayData<uint>(accessorIndex).Select(x => (int)x);
                    {
                        ReadOnlySpan<uint> indexSpan = vgoStorage.GetAccessorSpan<uint>(accessorIndex);

                        indices = new int[indexSpan.Length];

                        for (int idx = 0; idx < indexSpan.Length; idx++)
                        {
                            indices[idx] = (int)indexSpan[idx];
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
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="vgoMeshBlendShapes">List of vgo mesh blend shape.</param>
        /// <returns>List of blend shapes context.</returns>
        protected virtual BlendShapesContext? CreateBlendShapes(IVgoStorage vgoStorage, List<VgoMeshBlendShape>? vgoMeshBlendShapes)
        {
            if ((vgoMeshBlendShapes == null) || (vgoMeshBlendShapes.Any() == false))
            {
                return null;
            }

            BlendShapesContext context = new BlendShapesContext()
            {
                BlendShapeContexts = new List<BlendShapeContext>(vgoMeshBlendShapes.Count),
            };

            for (int shapeIndex = 0; shapeIndex < vgoMeshBlendShapes.Count; shapeIndex++)
            {
                VgoMeshBlendShape vgoBlendShape = vgoMeshBlendShapes[shapeIndex];

                string name = ((vgoBlendShape.name is null) || (vgoBlendShape.name == string.Empty))
                    ? shapeIndex.ToString()
                    : vgoBlendShape.name;

                BlendShapeContext blendShape = new BlendShapeContext(name);

                if (vgoBlendShape.attributes is null)
                {
                    continue;
                }

                VgoMeshPrimitiveAttributes attributes = vgoBlendShape.attributes;

                if (attributes.POSITION != -1)
                {
                    Vector3[] positions = vgoStorage.GetAccessorArrayData<Vector3>(attributes.POSITION);

                    if (vgoStorage.GeometryCoordinate == VgoGeometryCoordinate.RightHanded)
                    {
                        blendShape.Positions = new Vector3[positions.Length];

                        for (int idx = 0; idx < positions.Length; idx++)
                        {
                            blendShape.Positions[idx] = positions[idx].ReverseZ();
                        }
                    }
                    else
                    {
                        blendShape.Positions = positions;
                    }
                }

                if (attributes.NORMAL != -1)
                {
                    Vector3[] normals = vgoStorage.GetAccessorArrayData<Vector3>(attributes.NORMAL);

                    if (vgoStorage.GeometryCoordinate == VgoGeometryCoordinate.RightHanded)
                    {
                        blendShape.Normals = new Vector3[normals.Length];

                        for (int idx = 0; idx < normals.Length; idx++)
                        {
                            blendShape.Normals[idx] = normals[idx].ReverseZ();
                        }
                    }
                    else
                    {
                        blendShape.Normals = normals;
                    }
                }

                if (attributes.TANGENT != -1)
                {
                    Vector3[] tangents = vgoStorage.GetAccessorArrayData<Vector3>(attributes.TANGENT);

                    if (vgoStorage.GeometryCoordinate == VgoGeometryCoordinate.RightHanded)
                    {
                        blendShape.Tangents = new Vector3[tangents.Length];

                        for (int idx = 0; idx < tangents.Length; idx++)
                        {
                            blendShape.Tangents[idx] = tangents[idx].ReverseZ();
                        }
                    }
                    else
                    {
                        blendShape.Tangents = tangents;
                    }
                }

                context.BlendShapeContexts.Add(blendShape);

                if (vgoBlendShape.facePartsType != VgoBlendShapeFacePartsType.None)
                {
                    var facePart = new BlendShapeFacePart
                    {
                        index = shapeIndex,
                        type = vgoBlendShape.facePartsType,
                    };

                    context.BlendShapeConfig.FaceParts.Add(facePart);
                }

                if (vgoBlendShape.blinkType != VgoBlendShapeBlinkType.None)
                {
                    var blink = new BlendShapeBlink
                    {
                        index = shapeIndex,
                        type = vgoBlendShape.blinkType,
                    };

                    context.BlendShapeConfig.Blinks.Add(blink);
                }

                if (vgoBlendShape.visemeType != VgoBlendShapeVisemeType.None)
                {
                    var viseme = new BlendShapeViseme
                    {
                        index = shapeIndex,
                        type = vgoBlendShape.visemeType,
                    };

                    context.BlendShapeConfig.Visemes.Add(viseme);
                }
            }

            return context;
        }

        #endregion

        #region BuildMesh

        /// <summary>
        /// Build a unity mesh.
        /// </summary>
        /// <param name="meshContext">A mesh context.</param>
        /// <returns>A unity mesh.</returns>
        protected virtual Mesh BuildMesh(MeshContext meshContext)
        {
            Mesh mesh = new Mesh
            {
                name = meshContext.Name
            };

            // Positions
            if ((meshContext.Positions != null) && meshContext.Positions.Any())
            {
                if (meshContext.Positions.Length > UInt16.MaxValue)
                {
                    // UNITY_2017_3_OR_NEWER
                    mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
                }
                mesh.SetVertices(meshContext.Positions);
            }

            // Normals
            bool recalculateNormals = true;

            if ((meshContext.Normals != null) && meshContext.Normals.Any())
            {
                mesh.SetNormals(meshContext.Normals);

                recalculateNormals = false;
            }

            // Tangents
            bool recalculateTangents = true;

            if (_Option.ImportTangents)
            {
                if ((meshContext.Tangents != null) && meshContext.Tangents.Any())
                {
                    mesh.SetTangents(meshContext.Tangents);

                    recalculateTangents = false;
                }
            }

            // UVs
            if ((meshContext.UV0s != null) && meshContext.UV0s.Any())
            {
                mesh.SetUVs(channel: 0, meshContext.UV0s);
            }
            if ((meshContext.UV1s != null) && meshContext.UV1s.Any())
            {
                mesh.SetUVs(channel: 1, meshContext.UV1s);
            }
            if ((meshContext.UV2s != null) && meshContext.UV2s.Any())
            {
                mesh.SetUVs(channel: 2, meshContext.UV2s);
            }
            if ((meshContext.UV3s != null) && meshContext.UV3s.Any())
            {
                mesh.SetUVs(channel: 3, meshContext.UV3s);
            }

            // Colors
            if ((meshContext.Color32s != null) && meshContext.Color32s.Any())
            {
                mesh.SetColors(meshContext.Color32s);
            }
            else if ((meshContext.Colors != null) && meshContext.Colors.Any())
            {
                mesh.SetColors(meshContext.Colors);
            }

            // BoneWeights
            if ((meshContext.BoneWeights != null) && meshContext.BoneWeights.Any())
            {
                mesh.boneWeights = meshContext.BoneWeights;
            }

            // SubMesh
            if (meshContext.SubMeshes != null)
            {
                mesh.subMeshCount = meshContext.SubMeshes.Count;

                for (int i = 0; i < meshContext.SubMeshes.Count; ++i)
                {
                    mesh.SetTriangles(meshContext.SubMeshes[i], i);
                }
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
            if (meshContext.BlendShapesContext != null)
            {
                Vector3[]? emptyVertices = null;

                foreach (BlendShapeContext blendShape in meshContext.BlendShapesContext.BlendShapeContexts)
                {
                    if (blendShape.Positions?.Length > 0)
                    {
                        if (blendShape.Positions.Length == mesh.vertexCount)
                        {
                            Vector3[] deltaPositions = blendShape.Positions;

                            Vector3[]? deltaNormals = null;
                            if ((meshContext.Normals != null) &&
                                (meshContext.Normals.Length == mesh.vertexCount) &&
                                (blendShape.Normals.Count() == blendShape.Positions.Count()))
                            {
                                deltaNormals = blendShape.Normals;
                            }

                            mesh.AddBlendShapeFrame(blendShape.Name, frameWeight: 100.0f, deltaPositions, deltaNormals, deltaTangents: null);
                        }
                        else
                        {
                            Debug.LogWarningFormat("May be partial primitive has blendShape. Require separate mesh or extend blend shape, but not implemented: {0}", blendShape.Name);
                        }
                    }
                    else
                    {
                        if (emptyVertices == null)
                        {
                            emptyVertices = new Vector3[mesh.vertexCount];
                        }

                        // add empty blend shape for keep blend shape index
                        mesh.AddBlendShapeFrame(blendShape.Name, frameWeight: 100.0f, emptyVertices, deltaNormals: null, deltaTangents: null);
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
