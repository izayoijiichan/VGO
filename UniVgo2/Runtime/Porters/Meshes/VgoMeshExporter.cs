// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : VgoMeshExporter
// ----------------------------------------------------------------------
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using UnityEngine;

    /// <summary>
    /// VGO Mesh Exporter
    /// </summary>
    public class VgoMeshExporter : IVgoMeshExporter
    {
        #region Fields

        /// <summary>The mesh exporter option.</summary>
        protected readonly MeshExporterOption _Option;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of VgoMeshExporter.
        /// </summary>
        public VgoMeshExporter() : this(new MeshExporterOption()) { }

        /// <summary>
        /// Create a new instance of VgoMeshExporter with option.
        /// </summary>
        /// <param name="option">The mesh exporter option.</param>
        public VgoMeshExporter(MeshExporterOption option)
        {
            _Option = option;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Export meshes.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="unityMeshAssetList">List of unity mesh asset.</param>
        /// <param name="unityMaterialList">List of unity material.</param>
        public virtual void ExportMeshes(IVgoStorage vgoStorage, IList<MeshAsset> unityMeshAssetList, IList<Material> unityMaterialList = null)
        {
            if (vgoStorage == null)
            {
                throw new ArgumentNullException(nameof(vgoStorage));
            }

            if (unityMeshAssetList == null)
            {
                throw new ArgumentNullException(nameof(unityMeshAssetList));
            }

            if (vgoStorage.IsSpecVersion_2_4_orLower)
            {
                if (unityMaterialList == null)
                {
                    throw new ArgumentNullException(nameof(unityMaterialList));
                }
            }

            vgoStorage.Layout.meshes = new List<VgoMesh>(unityMeshAssetList.Count);

            for (int meshIndex = 0; meshIndex < unityMeshAssetList.Count; meshIndex++)
            {
                VgoMesh vgoMesh = CreateVgoMesh(vgoStorage, unityMeshAssetList[meshIndex], unityMaterialList);

                vgoStorage.Layout.meshes.Add(vgoMesh);
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Create a vgo mesh.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="meshAsset">A mesh asset.</param>
        /// <param name="unityMaterialList">List of unity material.</param>
        /// <returns>A vgo mesh.</returns>
        protected virtual VgoMesh CreateVgoMesh(IVgoStorage vgoStorage, MeshAsset meshAsset, IList<Material> unityMaterialList)
        {
            Mesh mesh = meshAsset.Mesh;

            BlendShapeConfiguration blendShapeConfig = meshAsset.BlendShapeConfiguration;

            VgoMesh vgoMesh = new VgoMesh(mesh.name);

            // Attribetes
            vgoMesh.attributes = CreatePrimitiveAttributes(vgoStorage, mesh);

            // SubMeshes
            if (mesh.subMeshCount > 0)
            {
                vgoMesh.subMeshes = new List<int>(mesh.subMeshCount);

                for (int subMeshIndex = 0; subMeshIndex < mesh.subMeshCount; subMeshIndex++)
                {
                    int[] meshIndices = mesh.GetIndices(subMeshIndex);

                    int indicesAccessorIndex = AddAccessorForSubMesh(vgoStorage, meshIndices);

                    vgoMesh.subMeshes.Add(indicesAccessorIndex);

                    if (vgoStorage.IsSpecVersion_2_4_orLower)
                    {
                        // Materials
                        if ((unityMaterialList != null) &&
                            (meshAsset.Renderer != null) &&
                            (meshAsset.Renderer.sharedMaterial != null))
                        {
                            int materialIndex = unityMaterialList.IndexOf(meshAsset.Renderer.sharedMaterials[subMeshIndex]);

                            if (materialIndex >= 0)
                            {
                                if (vgoMesh.materials == null)
                                {
                                    vgoMesh.materials = new List<int>();
                                }

                                vgoMesh.materials.Add(materialIndex);
                            }
                        }
                    }
                }
            }

            // BlendShapes
            if (mesh.blendShapeCount > 0)
            {
                vgoMesh.blendShapes = new List<VgoMeshBlendShape>(mesh.blendShapeCount);

                for (int shapeIndex = 0; shapeIndex < mesh.blendShapeCount; shapeIndex++)
                {
                    string blendShapeName = mesh.GetBlendShapeName(shapeIndex);

                    VgoMeshPrimitiveAttributes attributes = CreateBlendShapeAttributes(vgoStorage, mesh, shapeIndex);

                    VgoMeshBlendShape vgoMeshBlendShape = new VgoMeshBlendShape
                    {
                        name = blendShapeName,
                        attributes = attributes,
                    };

                    if (blendShapeConfig != null)
                    {
                        BlendShapeFacePart facePart = blendShapeConfig.faceParts.Where(x => x.index == shapeIndex).FirstOrDefault();

                        if (facePart != null)
                        {
                            vgoMeshBlendShape.facePartsType = facePart.type;
                        }

                        BlendShapeBlink blink = blendShapeConfig.blinks.Where(x => x.index == shapeIndex).FirstOrDefault();

                        if (blink != null)
                        {
                            vgoMeshBlendShape.blinkType = blink.type;
                        }

                        BlendShapeViseme viseme = blendShapeConfig.visemes.Where(x => x.index == shapeIndex).FirstOrDefault();

                        if (viseme != null)
                        {
                            vgoMeshBlendShape.visemeType = viseme.type;
                        }
                    }

                    vgoMesh.blendShapes.Add(vgoMeshBlendShape);
                }

                if (vgoStorage.IsSpecVersion_2_4_orLower)
                {
                    if (blendShapeConfig != null)
                    {
                        vgoMesh.blendShapeKind = blendShapeConfig.kind;
                        vgoMesh.blendShapePesets = blendShapeConfig.presets;
                    }
                }
            }

            return vgoMesh;
        }

        #endregion

        #region Attributes

        /// <summary>
        /// Create a vgo mesh primitive attributes.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="mesh">A unity mesh.</param>
        /// <returns>A vgo mesh primitive attributes.</returns>
        protected virtual VgoMeshPrimitiveAttributes CreatePrimitiveAttributes(IVgoStorage vgoStorage, Mesh mesh)
        {
            var attributes = new VgoMeshPrimitiveAttributes();

            // Positions
            if (mesh.vertices.Length > 0)
            {
                Vector3[] positions;

                if (vgoStorage.GeometryCoordinate == VgoGeometryCoordinate.RightHanded)
                {
                    positions = mesh.vertices.Select(x => x.ReverseZ()).ToArray();
                }
                else
                {
                    positions = mesh.vertices;
                }

                //CalculateVector3MinAndMax(positions, out float[] min, out float[] max);

                int accessorIndex = vgoStorage.AddAccessorWithoutSparse(positions, VgoResourceAccessorDataType.Vector3Float, VgoResourceAccessorKind.MeshData);

                attributes.Add(VertexKey.Position, accessorIndex);
            }

            // Normals
            if (mesh.normals.Length > 0)
            {
                Vector3[] normals;

                if (vgoStorage.GeometryCoordinate == VgoGeometryCoordinate.RightHanded)
                {
                    normals = mesh.normals.Select(x => x.normalized.ReverseZ()).ToArray();
                }
                else
                {
                    normals = mesh.normals.Select(x => x.normalized).ToArray();
                }

                int accessorIndex = vgoStorage.AddAccessorWithoutSparse(normals, VgoResourceAccessorDataType.Vector3Float, VgoResourceAccessorKind.MeshData);

                attributes.Add(VertexKey.Normal, accessorIndex);
            }

            // Tangents
            if (_Option.ExportTangents)
            {
                if (mesh.tangents.Length > 0)
                {
                    Vector4[] tangents;

                    if (vgoStorage.GeometryCoordinate == VgoGeometryCoordinate.RightHanded)
                    {
                        tangents = mesh.tangents.Select(x => x.ReverseZ()).ToArray();
                    }
                    else
                    {
                        tangents = mesh.tangents;
                    }

                    int accessorIndex = vgoStorage.AddAccessorWithoutSparse(tangents, VgoResourceAccessorDataType.Vector4Float, VgoResourceAccessorKind.MeshData);

                    attributes.Add(VertexKey.Tangent, accessorIndex);
                }
            }

            // UVs
            {
                SetUV(vgoStorage, attributes, VertexKey.TexCoord0, mesh.uv);
                SetUV(vgoStorage, attributes, VertexKey.TexCoord1, mesh.uv2);
                SetUV(vgoStorage, attributes, VertexKey.TexCoord2, mesh.uv3);
                SetUV(vgoStorage, attributes, VertexKey.TexCoord3, mesh.uv4);
            }

            // Colors
            if (_Option.RemoveVertexColor == false)
            {
                //if (mesh.colors.Length > 0)
                //{
                //    int accessorIndex = vgoStorage.AddAccessorWithoutSparse(mesh.colors, VgoResourceAccessorDataType.Vector4Float, VgoResourceAccessorKind.MeshData);

                //    attributes.Add(VertexKey.Color0, accessorIndex);
                //}
                if (mesh.colors32.Length > 0)
                {
                    int accessorIndex = vgoStorage.AddAccessorWithoutSparse(mesh.colors, VgoResourceAccessorDataType.Vector4UInt8, VgoResourceAccessorKind.MeshData);

                    attributes.Add(VertexKey.Color0, accessorIndex);
                }
            }

            // Joints
            if (mesh.boneWeights.Length > 0)
            {
                Vector4Ushort[] joints = mesh.boneWeights.Select(x => new Vector4Ushort((ushort)x.boneIndex0, (ushort)x.boneIndex1, (ushort)x.boneIndex2, (ushort)x.boneIndex3)).ToArray();

                int accessorIndex = vgoStorage.AddAccessorWithoutSparse(joints, VgoResourceAccessorDataType.Vector4UInt16, VgoResourceAccessorKind.MeshData);

                attributes.Add(VertexKey.Joints0, accessorIndex);
            }

            // Weights
            if (mesh.boneWeights.Length > 0)
            {
                Vector4[] weights = mesh.boneWeights.Select(x => new Vector4(x.weight0, x.weight1, x.weight2, x.weight3)).ToArray();

                int accessorIndex = vgoStorage.AddAccessorWithoutSparse(weights, VgoResourceAccessorDataType.Vector4Float, VgoResourceAccessorKind.MeshData);

                attributes.Add(VertexKey.Weights0, accessorIndex);
            }

            return attributes;
        }

        /// <summary>
        /// Sets UV.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="attributes">The primitive attributes.</param>
        /// <param name="texcoordKey">The Texture coord key.</param>
        /// <param name="uvs">An array of UV.</param>
        protected void SetUV(IVgoStorage vgoStorage, VgoMeshPrimitiveAttributes attributes, string texcoordKey, Vector2[] uvs)
        {
            if ((uvs != null) && uvs.Any())
            {
                Vector2[] exportUvs;

                if (vgoStorage.UVCoordinate == VgoUVCoordinate.TopLeft)
                {
                    exportUvs = uvs.Select(x => x.ReverseUV()).ToArray();
                }
                else
                {
                    exportUvs = uvs;
                }

                int accessorIndex = vgoStorage.AddAccessorWithoutSparse(exportUvs, VgoResourceAccessorDataType.Vector2Float, VgoResourceAccessorKind.MeshData);

                attributes.Add(texcoordKey, accessorIndex);
            }
        }

        #endregion

        #region SubMesh (Indices)

        /// <summary>
        /// Add a accessor for sub mesh.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="meshIndices">The mesh indices.</param>
        /// <returns>The index of accessor.</returns>
        protected virtual int AddAccessorForSubMesh(IVgoStorage vgoStorage, int[] meshIndices)
        {
            int accessorIndex = -1;

            var kind = VgoResourceAccessorKind.MeshData;

            int[] _meshIndices;

            if (vgoStorage.GeometryCoordinate == VgoGeometryCoordinate.RightHanded)
            {
                _meshIndices = meshIndices.FlipTriangle();
            }
            else
            {
                _meshIndices = meshIndices;
            }

            if (_Option.EnableSparseForSubMesh && JudgeSparseForSubMesh(meshIndices))
            {
                int[] sparseIndices = Enumerable.Range(0, meshIndices.Length)
                    .Where(i => _meshIndices[i] != 0)
                    .ToArray();

                uint[] sparseValues = sparseIndices
                    .Select(index => (uint)_meshIndices[index])
                    .ToArray();

                accessorIndex = vgoStorage.AddAccessorWithSparse(VgoResourceAccessorSparseType.General, sparseIndices, sparseValues,
                    sparseValueDataType: VgoResourceAccessorDataType.UnsignedInt,
                    accessorDataType: VgoResourceAccessorDataType.UnsignedInt,
                    accessorCount: meshIndices.Length, kind);
            }
            else
            {
                uint[] indices = _meshIndices
                    .Select(x => (uint)x)
                    .ToArray();

                accessorIndex = vgoStorage.AddAccessorWithoutSparse(indices, VgoResourceAccessorDataType.UnsignedInt, kind);
            }

            return accessorIndex;
        }

        /// <summary>
        /// Judge if accessor sparse should be applied.
        /// </summary>
        /// <param name="meshIndices">The mesh indices.</param>
        /// <returns>Returns true if sparse should be applied, false otherwise.</returns>
        /// <remarks>
        /// index is byte(1) or ushort(2) or uint(4)
        /// value is uint(4)
        /// </remarks>
        protected virtual bool JudgeSparseForSubMesh(int[] meshIndices)
        {
            int sparseValuesCount = meshIndices.Count(x => x != 0);

            if (sparseValuesCount > 0)
            {
                int nonSparseDataSize = 4 * meshIndices.Length;

                int sparseIndicesDataTypeByte =
                    (sparseValuesCount <= byte.MaxValue) ? 1 :
                    (sparseValuesCount <= ushort.MaxValue) ? 2 : 4;

                int sparseDataSize = (sparseIndicesDataTypeByte + 4) * sparseValuesCount;

                float compressionRatio = (float)sparseDataSize / (float)nonSparseDataSize;

                if (compressionRatio < _Option.SparseThresholdForSubMesh)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region BlendShape

        /// <summary>
        /// Creat a blend shape attributes.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="mesh">A unity mesh.</param>
        /// <param name="blendShapeIndex">The index of blend shape.</param>
        /// <returns>A vgo blend shape attributes.</returns>
        protected virtual VgoMeshPrimitiveAttributes CreateBlendShapeAttributes(IVgoStorage vgoStorage, Mesh mesh, int blendShapeIndex)
        {
            int frameIndex = mesh.GetBlendShapeFrameCount(blendShapeIndex) - 1;

            Vector3[] positions = new Vector3[mesh.vertexCount];
            Vector3[] normals = new Vector3[mesh.vertexCount];
            Vector3[] tangents = new Vector3[mesh.vertexCount];

            mesh.GetBlendShapeFrameVertices(blendShapeIndex, frameIndex, positions, normals, tangents);

            bool usePosition = positions.Length > 0;
            bool useNormal = usePosition && (normals.Length == positions.Length);
            bool useTangent = usePosition && (tangents.Length == positions.Length) && _Option.ExportTangents;

            int positionAccessorIndex = -1;
            int normalAccessorIndex = -1;
            int tangentAccessorIndex = -1;

            if (usePosition)
            {
                if (vgoStorage.GeometryCoordinate == VgoGeometryCoordinate.RightHanded)
                {
                    positions = positions.Select(x => x.ReverseZ()).ToArray();
                }

                positionAccessorIndex = AddAccessorForBlendShape(vgoStorage, positions);
            }

            if (useNormal)
            {
                if (vgoStorage.GeometryCoordinate == VgoGeometryCoordinate.RightHanded)
                {
                    normals = normals.Select(x => x.ReverseZ()).ToArray();
                }

                normalAccessorIndex = AddAccessorForBlendShape(vgoStorage, normals);
            }

            if (useTangent)
            {
                if (vgoStorage.GeometryCoordinate == VgoGeometryCoordinate.RightHanded)
                {
                    tangents = tangents.Select(x => x.ReverseZ()).ToArray();
                }

                tangentAccessorIndex = AddAccessorForBlendShape(vgoStorage, tangents);
            }

            var attributes = new VgoMeshPrimitiveAttributes
            {
                { VertexKey.Position, positionAccessorIndex },
                { VertexKey.Normal, normalAccessorIndex },
                { VertexKey.Tangent, tangentAccessorIndex },
            };

            return attributes;
        }

        /// <summary>
        /// Add a accessor for morph target.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="vertexes">An array of vertex.</param>
        /// <returns>The index of accessor.</returns>
        protected virtual int AddAccessorForBlendShape(IVgoStorage vgoStorage, Vector3[] vertexes)
        {
            int accessorIndex = -1;

            var kind = VgoResourceAccessorKind.MeshData;

            if (_Option.EnableSparseForMorphTarget && JudgeSparseForBlendShape(vertexes, out VgoResourceAccessorSparseType sparseType, out int sparseCount))
            {
                if (sparseType == VgoResourceAccessorSparseType.General)
                {
                    int[] sparseIndices = Enumerable.Range(0, vertexes.Length)
                        .Where(i => vertexes[i] != Vector3.zero)
                        .ToArray();

                    Vector3[] sparseValues = sparseIndices
                        .Select(index => vertexes[index])
                        .ToArray();

                    //List<int> sparseIndexList = new List<int>(sparseCount);
                    //List<Vector3> sparseValueList = new List<Vector3>(sparseCount);

                    //for (int vertexIndex = 0; vertexIndex < vertexes.Length; vertexIndex++)
                    //{
                    //    if (vertexes[vertexIndex] != Vector3.zero)
                    //    {
                    //        sparseIndexList.Add(vertexIndex);
                    //        sparseValueList.Add(vertexes[vertexIndex]);
                    //    }
                    //}

                    //if ((sparseIndexList.Count != sparseCount) ||
                    //    (sparseValueList.Count != sparseCount))
                    //{
                    //    throw new Exception($"{sparseCount}, {sparseIndexList.Count}, {sparseValueList.Count}");
                    //}

                    accessorIndex = vgoStorage.AddAccessorWithSparse(VgoResourceAccessorSparseType.General, sparseIndices, sparseValues,
                        sparseValueDataType: VgoResourceAccessorDataType.Vector3Float,
                        accessorDataType: VgoResourceAccessorDataType.Vector3Float,
                        accessorCount: vertexes.Length, kind);
                }
                else if (sparseType == VgoResourceAccessorSparseType.Powerful)
                {
                    Span<float> vertexFloatSpan = MemoryMarshal.Cast<Vector3, float>(vertexes.AsSpan());

                    int[] sparseIndices = new int[sparseCount];
                    float[] sparseValues = new float[sparseCount];

                    int sparseIndex = 0;

                    for (int spanIndex = 0; spanIndex < vertexFloatSpan.Length; spanIndex++)
                    {
                        if (vertexFloatSpan[spanIndex] != 0.0f)
                        {
                            sparseIndices[sparseIndex] = spanIndex;
                            sparseValues[sparseIndex] = vertexFloatSpan[spanIndex];
                            sparseIndex++;
                        }
                    }

                    //List<int> sparseIndexList = new List<int>(sparseCount);
                    //List<float> sparseValueList = new List<float>(sparseCount);

                    //for (int spanIndex = 0; spanIndex < vertexFloatSpan.Length; spanIndex++)
                    //{
                    //    if (vertexFloatSpan[spanIndex] != 0.0f)
                    //    {
                    //        sparseIndexList.Add(spanIndex);
                    //        sparseValueList.Add(vertexFloatSpan[spanIndex]);
                    //    }
                    //}

                    //if ((sparseIndexList.Count != sparseCount) ||
                    //    (sparseValueList.Count != sparseCount))
                    //{
                    //    throw new Exception($"{sparseCount}, {sparseIndexList.Count}, {sparseValueList.Count}");
                    //}

                    accessorIndex = vgoStorage.AddAccessorWithSparse(VgoResourceAccessorSparseType.Powerful, sparseIndices, sparseValues,
                        sparseValueDataType: VgoResourceAccessorDataType.Float,
                        accessorDataType: VgoResourceAccessorDataType.Vector3Float,
                        accessorCount: vertexes.Length, kind);
                }
                else
                {
                    throw new Exception();
                }
            }
            else
            {
               accessorIndex = vgoStorage.AddAccessorWithoutSparse(vertexes, VgoResourceAccessorDataType.Vector3Float, kind);
            }

            return accessorIndex;
        }

        /// <summary>
        /// Judge if accessor sparse should be applied.
        /// </summary>
        /// <param name="vertexes">The vertexes.</param>
        /// <returns>Returns true if sparse should be applied, false otherwise.</returns>
        /// <remarks>
        /// index is byte(1) or ushort(2) or uint(4)
        /// value is Vector3(12) [General] or float(4) [Powerful]
        /// </remarks>
        protected virtual bool JudgeSparseForBlendShape(Vector3[] vertexes, out VgoResourceAccessorSparseType sparseType, out int sparseCount)
        {
            sparseType = VgoResourceAccessorSparseType.None;
            sparseCount = 0;

            int nonSparseDataSize = 12 * vertexes.Length;

            int generalSparseCount = 0;
            int generalSparseDataSize = int.MaxValue;

            // General Sparse
            {
                generalSparseCount = vertexes.Count(v => v != Vector3.zero);

                if (generalSparseCount > 0)
                {
                    int sparseIndicesDataTypeByte =
                        (generalSparseCount <= byte.MaxValue) ? 1 :
                        (generalSparseCount <= ushort.MaxValue) ? 2 : 4;

                    generalSparseDataSize = (sparseIndicesDataTypeByte + 12) * generalSparseCount;
                }
            }

            int powerfulSparseCount = 0;
            int powerfulSparseDataSize = int.MaxValue;

            // Powerful Sparse
            {
                Span<float> vertexFloatSpan = MemoryMarshal.Cast<Vector3, float>(vertexes.AsSpan());

                foreach (float vertexFloat in vertexFloatSpan)
                {
                    if (vertexFloat != 0.0f)
                    {
                        powerfulSparseCount++;
                    }
                }

                if (powerfulSparseCount > 0)
                {
                    int sparseIndicesDataTypeByte =
                        (powerfulSparseCount <= byte.MaxValue) ? 1 :
                        (powerfulSparseCount <= ushort.MaxValue) ? 2 : 4;

                    powerfulSparseDataSize = (sparseIndicesDataTypeByte + 4) * powerfulSparseCount;
                }
            }

            if ((generalSparseCount == 0) && (powerfulSparseCount == 0))
            {
                return false;
            }
            else if (
                (generalSparseCount > 0) &&
                (generalSparseDataSize <= nonSparseDataSize) &&
                (generalSparseDataSize <= powerfulSparseDataSize))
            {
                float compressionRatio = (float)generalSparseDataSize / (float)nonSparseDataSize;

                if (compressionRatio < _Option.SparseThresholdForMorphTarget)
                {
                    sparseType = VgoResourceAccessorSparseType.General;
                    sparseCount = generalSparseCount;

                    return true;
                }
            }
            else if (
                (powerfulSparseCount > 0) &&
                (powerfulSparseDataSize <= nonSparseDataSize) &&
                (powerfulSparseDataSize <= generalSparseDataSize))
            {
                float compressionRatio = (float)powerfulSparseDataSize / (float)nonSparseDataSize;

                if (compressionRatio < _Option.SparseThresholdForMorphTarget)
                {
                    sparseType = VgoResourceAccessorSparseType.Powerful;
                    sparseCount = powerfulSparseCount;

                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}
