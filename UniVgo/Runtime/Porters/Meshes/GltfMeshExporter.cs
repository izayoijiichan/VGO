// ----------------------------------------------------------------------
// @Namespace : UniVgo.Porters
// @Class     : GltfMeshExporter
// ----------------------------------------------------------------------
namespace UniVgo.Porters
{
    using NewtonGltf;
    using NewtonGltf.Serialization;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using VgoGltf;

    /// <summary>
    /// glTF Mesh Exporter
    /// </summary>
    public class GltfMeshExporter
    {
        #region Fields

        /// <summary>The JSON selializer settings.</summary>
        protected readonly VgoJsonSerializerSettings _JsonSerializerSettings = new VgoJsonSerializerSettings();

        /// <summary>Whether export tangents.</summary>
        /// <remarks>setting</remarks>
        protected bool _ExportTangents = false;

        /// <summary>Whether remove vertex color.</summary>
        /// <remarks>setting</remarks>
        protected bool _RemoveVertexColor = false;

        /// <summary>Whether enable accessor sparse for sub mesh.</summary>
        /// <remarks>setting</remarks>
        protected bool _EnableSparseForSubMesh = true;

        /// <summary>The threshold to apply sparse for sub mesh.</summary>
        /// <remarks>setting</remarks>
        protected float _SparseThresholdForSubMesh = 0.8f;

        /// <summary>Whether enable accessor sparse for morph target.</summary>
        /// <remarks>setting</remarks>
        protected bool _EnableSparseForMorphTarget = true;

        /// <summary>The threshold to apply sparse for morph target.</summary>
        /// <remarks>setting</remarks>
        protected float _SparseThresholdForMorphTarget = 0.8f;

        #endregion

        #region Properties

        /// <summary>The glTF storage adapter.</summary>
        public GltfStorageAdapter GltfStorageAdapter { get; set; }

        /// <summary>List of unity mesh.</summary>
        public List<MeshAsset> UnityMeshList { get; protected set; }

        /// <summary>List of unity material.</summary>
        public List<Material> UnityMaterialList { get; protected set; }

        /// <summary>The index of buffer.</summary>
        public int BufferIndex { get; protected set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Export meshes.
        /// </summary>
        /// <param name="unityMeshList">List of unity mesh.</param>
        /// <param name="unityMaterialList">List of unity material.</param>
        /// <param name="bufferIndex">The index of buffer.</param>
        public virtual void ExportMeshes(List<MeshAsset> unityMeshList, List<Material> unityMaterialList, int bufferIndex = 0)
        {
            if (GltfStorageAdapter == null)
            {
                throw new Exception();
            }

            UnityMaterialList = unityMaterialList ?? throw new ArgumentNullException();
            UnityMeshList = unityMeshList ?? throw new ArgumentNullException();
            BufferIndex = bufferIndex;

            for (int meshIndex = 0; meshIndex < UnityMeshList.Count; meshIndex++)
            {
                GltfMesh gltfMesh = CreateGltfMesh(UnityMeshList[meshIndex]);

                GltfStorageAdapter.Gltf.meshes.Add(gltfMesh);
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Create a glTF mesh.
        /// </summary>
        /// <param name="meshAsset">The mesh asset.</param>
        /// <returns>A glTF mesh.</returns>
        protected virtual GltfMesh CreateGltfMesh(MeshAsset meshAsset)
        {
            Mesh mesh = meshAsset.Mesh;
            Renderer renderer = meshAsset.Renderer;
            Material[] materials = renderer.sharedMaterials;

            GltfMesh gltfMesh = new GltfMesh(mesh.name);

            // Attribetes
            GltfMeshPrimitiveAttributes primitiveAttributes = CreatePrimitiveAttributes(mesh);

            // SubMeshes
            for (int subMeshIndex = 0; subMeshIndex < mesh.subMeshCount; subMeshIndex++)
            {
                int[] meshIndices = mesh.GetIndices(subMeshIndex);

                int indicesAccessorIndex = AddAccessorForSubMesh(meshIndices);

                int materialIndex = UnityMaterialList.IndexOf(materials[subMeshIndex]);

                GltfMeshPrimitive gltfMeshPrimitive = new GltfMeshPrimitive
                {
                    attributes = primitiveAttributes,
                    indices = indicesAccessorIndex,
                    mode = GltfMeshPrimitiveMode.TRIANGLES,
                    material = materialIndex,
                };

                gltfMesh.primitives.Add(gltfMeshPrimitive);
            }

            // primitives[].targets
            if (mesh.blendShapeCount > 0)
            {
                for (int shapeIndex = 0; shapeIndex < mesh.blendShapeCount; shapeIndex++)
                {
                    GltfMeshPrimitiveAttributes morphTarget = CreateMorphTarget(mesh, shapeIndex);

                    string targetName = mesh.GetBlendShapeName(shapeIndex);

                    //
                    // all primitive has same blendShape
                    //
                    for (int primitiveIndex = 0; primitiveIndex < gltfMesh.primitives.Count; primitiveIndex++)
                    {
                        if (gltfMesh.primitives[primitiveIndex].targets == null)
                        {
                            gltfMesh.primitives[primitiveIndex].targets = new List<GltfMeshPrimitiveAttributes>();
                        }

                        if (gltfMesh.primitives[primitiveIndex].targetNames == null)
                        {
                            gltfMesh.primitives[primitiveIndex].targetNames = new List<string>();
                        }

                        gltfMesh.primitives[primitiveIndex].targets.Add(morphTarget);
                        gltfMesh.primitives[primitiveIndex].targetNames.Add(targetName);
                    }
                }
            }

            // primitives[].extras
            for (int primitiveIndex = 0; primitiveIndex < gltfMesh.primitives.Count; primitiveIndex++)
            {
                SetExtras(gltfMesh.primitives[primitiveIndex]);
            }

            return gltfMesh;
        }

        #endregion

        #region Attributes

        /// <summary>
        /// Create a glTF mesh primitive attributes.
        /// </summary>
        /// <param name="mesh">A unity mesh.</param>
        /// <returns>A glTF mesh primitive attributes.</returns>
        protected virtual GltfMeshPrimitiveAttributes CreatePrimitiveAttributes(Mesh mesh)
        {
            var attributes = new GltfMeshPrimitiveAttributes();

            // Positions
            if (mesh.vertices.Length > 0)
            {
                Vector3[] positions = mesh.vertices.Select(x => x.ReverseZ()).ToArray();

                CalculateVector3MinAndMax(positions, out float[] min, out float[] max);

                int accessorIndex = GltfStorageAdapter.AddAccessorWithoutSparse(BufferIndex, positions, GltfBufferViewTarget.ARRAY_BUFFER, min, max);

                attributes.Add(VertexKey.Position, accessorIndex);
            }

            // Normals
            if (mesh.normals.Length > 0)
            {
                Vector3[] normals = mesh.normals.Select(x => x.normalized.ReverseZ()).ToArray();

                int accessorIndex = GltfStorageAdapter.AddAccessorWithoutSparse(BufferIndex, normals, GltfBufferViewTarget.ARRAY_BUFFER);

                attributes.Add(VertexKey.Normal, accessorIndex);
            }

            // Tangents
            if (_ExportTangents)
            {
                if (mesh.tangents.Length > 0)
                {
                    Vector4[] tangents = mesh.tangents.Select(x => x.ReverseZ()).ToArray();

                    int accessorIndex = GltfStorageAdapter.AddAccessorWithoutSparse(BufferIndex, tangents, GltfBufferViewTarget.ARRAY_BUFFER);

                    attributes.Add(VertexKey.Tangent, accessorIndex);
                }
            }

            // UVs
            if (mesh.uv.Length > 0)
            {
                Vector2[] uvs = mesh.uv.Select(x => x.ReverseUV()).ToArray();

                int accessorIndex = GltfStorageAdapter.AddAccessorWithoutSparse(BufferIndex, uvs, GltfBufferViewTarget.ARRAY_BUFFER);

                attributes.Add(VertexKey.TexCoord0, accessorIndex);
            }

            // Colors
            if (_RemoveVertexColor == false)
            {
                if (mesh.colors.Length > 0)
                {
                    int accessorIndex = GltfStorageAdapter.AddAccessorWithoutSparse(BufferIndex, mesh.colors, GltfBufferViewTarget.ARRAY_BUFFER);

                    attributes.Add(VertexKey.Color0, accessorIndex);
                }
            }

            // Joints
            if (mesh.boneWeights.Length > 0)
            {
                Vector4Ushort[] joints = mesh.boneWeights.Select(x => new Vector4Ushort((ushort)x.boneIndex0, (ushort)x.boneIndex1, (ushort)x.boneIndex2, (ushort)x.boneIndex3)).ToArray();

                int accessorIndex = GltfStorageAdapter.AddAccessorWithoutSparse(BufferIndex, joints, GltfBufferViewTarget.ARRAY_BUFFER);

                attributes.Add(VertexKey.Joints0, accessorIndex);
            }

            // Weights
            if (mesh.boneWeights.Length > 0)
            {
                Vector4[] weights = mesh.boneWeights.Select(x => new Vector4(x.weight0, x.weight1, x.weight2, x.weight3)).ToArray();

                int accessorIndex = GltfStorageAdapter.AddAccessorWithoutSparse(BufferIndex, weights, GltfBufferViewTarget.ARRAY_BUFFER);

                attributes.Add(VertexKey.Weights0, accessorIndex);
            }

            return attributes;
        }

        #endregion

        #region SubMesh (Indices)

        /// <summary>
        /// Add a accessor for sub mesh.
        /// </summary>
        /// <param name="meshIndices">The mesh indices.</param>
        /// <returns>The index of accessor.</returns>
        protected virtual int AddAccessorForSubMesh(int[] meshIndices)
        {
            int accessorIndex = -1;

            var target = GltfBufferViewTarget.ELEMENT_ARRAY_BUFFER;

            if (_EnableSparseForSubMesh && JudgeSparseForSubMesh(meshIndices))
            {
                int[] flipedMeshIndices = meshIndices.FlipTriangle();

                int[] sparseIndices = Enumerable.Range(0, meshIndices.Length)
                    .Where(i => flipedMeshIndices[i] != 0)
                    .ToArray();

                uint[] sparseValues = sparseIndices
                    .Select(index => (uint)flipedMeshIndices[index])
                    .ToArray();

                accessorIndex = GltfStorageAdapter.AddAccessorWithSparse(
                    BufferIndex, target, accessorCount: meshIndices.Length,
                    sparseIndices, sparseValues);
            }
            else
            {
                uint[] indices = meshIndices
                    .FlipTriangle()
                    .Select(x => (uint)x)
                    .ToArray();

                accessorIndex = GltfStorageAdapter.AddAccessorWithoutSparse(BufferIndex, indices, target);
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

                if (compressionRatio < _SparseThresholdForSubMesh)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region MorphTarget (BlendShape)

        /// <summary>
        /// Creat a glTF mopth target.
        /// </summary>
        /// <param name="mesh">A unity mesh.</param>
        /// <param name="blendShapeIndex">The index of blend shape.</param>
        /// <returns>A glTF morph target.</returns>
        protected virtual GltfMeshPrimitiveAttributes CreateMorphTarget(Mesh mesh, int blendShapeIndex)
        {
            int frameIndex = mesh.GetBlendShapeFrameCount(blendShapeIndex) - 1;

            Vector3[] positions = new Vector3[mesh.vertexCount];
            Vector3[] normals = new Vector3[mesh.vertexCount];
            Vector3[] tangents = new Vector3[mesh.vertexCount];

            mesh.GetBlendShapeFrameVertices(blendShapeIndex, frameIndex, positions, normals, tangents);

            bool usePosition = positions.Length > 0;
            bool useNormal = usePosition && (normals.Length == positions.Length);
            bool useTangent = usePosition && (tangents.Length == positions.Length) && _ExportTangents;

            int positionAccessorIndex = -1;
            int normalAccessorIndex = -1;
            int tangentAccessorIndex = -1;

            if (usePosition)
            {
                positions = positions.Select(x => x.ReverseZ()).ToArray();

                positionAccessorIndex = AddAccessorForMorphTarget(positions, setMinMax: true);
            }

            if (useNormal)
            {
                normals = normals.Select(x => x.ReverseZ()).ToArray();

                normalAccessorIndex = AddAccessorForMorphTarget(normals);
            }

            if (useTangent)
            {
                tangents = tangents.Select(x => x.ReverseZ()).ToArray();

                tangentAccessorIndex = AddAccessorForMorphTarget(tangents);
            }

            var morphTarget = new GltfMeshPrimitiveAttributes
            {
                { VertexKey.Position, positionAccessorIndex },
                { VertexKey.Normal, normalAccessorIndex },
                { VertexKey.Tangent, tangentAccessorIndex },
            };

            return morphTarget;
        }

        /// <summary>
        /// Add a accessor for morph target.
        /// </summary>
        /// <param name="vertexes">An array of vertex.</param>
        /// <param name="setMinMax">Whether set min and max.</param>
        /// <returns>The index of accessor.</returns>
        protected virtual int AddAccessorForMorphTarget(Vector3[] vertexes, bool setMinMax = false)
        {
            int accessorIndex = -1;

            var target = GltfBufferViewTarget.ARRAY_BUFFER;

            if (_EnableSparseForMorphTarget && JudgeSparseForMorphTarget(vertexes))
            {
                int[] sparseIndices = Enumerable.Range(0, vertexes.Length)
                    .Where(i => vertexes[i] != Vector3.zero)
                    .ToArray();

                Vector3[] sparseValues = sparseIndices
                    .Select(index => vertexes[index])
                    .ToArray();

                float[] min = null;
                float[] max = null;

                if (setMinMax)
                {
                    CalculateVector3MinAndMax(sparseValues, out min, out max);
                }

                accessorIndex = GltfStorageAdapter.AddAccessorWithSparse(
                    BufferIndex, target, accessorCount: vertexes.Length,
                    sparseIndices, sparseValues, min, max);
            }
            else
            {
                float[] min = null;
                float[] max = null;

                if (setMinMax)
                {
                    CalculateVector3MinAndMax(vertexes, out min, out max);
                }

                accessorIndex = GltfStorageAdapter.AddAccessorWithoutSparse(BufferIndex, vertexes, target, min, max);
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
        /// value is Vector3(12)
        /// </remarks>
        protected virtual bool JudgeSparseForMorphTarget(Vector3[] vertexes)
        {
            int sparseValuesCount = vertexes.Count(v => v != Vector3.zero);

            if (sparseValuesCount > 0)
            {
                int nonSparseDataSize = 12 * vertexes.Length;

                int sparseIndicesDataTypeByte =
                    (sparseValuesCount <= byte.MaxValue) ? 1 :
                    (sparseValuesCount <= ushort.MaxValue) ? 2 : 4;

                int sparseDataSize = (sparseIndicesDataTypeByte + 12) * sparseValuesCount;

                float compressionRatio = (float)sparseDataSize / (float)nonSparseDataSize;

                if (compressionRatio < _SparseThresholdForMorphTarget)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Caluculate vector3 min and max.
        /// </summary>
        /// <param name="values">An array of values.</param>
        /// <param name="min">An array of minimum vector3 value.</param>
        /// <param name="max">An array of maximum vector3 value.</param>
        protected virtual void CalculateVector3MinAndMax(Vector3[] values, out float[] min, out float[] max)
        {
            min = values.Aggregate(values[0], (a, b) => new Vector3(Mathf.Min(a.x, b.x), Math.Min(a.y, b.y), Mathf.Min(a.z, b.z))).ToArray();
            max = values.Aggregate(values[0], (a, b) => new Vector3(Mathf.Max(a.x, b.x), Math.Max(a.y, b.y), Mathf.Max(a.z, b.z))).ToArray();
        }

        /// <summary>
        /// Set gltf.mesh.primitive.extras.
        /// </summary>
        /// <param name="gltfMeshPrimitive">A glTF mesh primitive.</param>
        protected virtual void SetExtras(GltfMeshPrimitive gltfMeshPrimitive)
        {
            if (gltfMeshPrimitive.targetNames.Count > 0)
            {
                gltfMeshPrimitive.extras = new GltfExtras(_JsonSerializerSettings)
                {
                    { "targetNames", gltfMeshPrimitive.targetNames }
                };
            }
        }

        #endregion
    }
}
