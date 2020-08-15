// ----------------------------------------------------------------------
// @Namespace : UniVgo.Porters
// @Class     : GltfMeshImporter
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
    /// glTF Mesh Importer
    /// </summary>
    public class GltfMeshImporter
    {
        #region Fields

        /// <summary>The JSON serializer settings.</summary>
        protected readonly VgoJsonSerializerSettings _JsonSerializerSettings = new VgoJsonSerializerSettings();

        /// <summary>Whether import tangents.</summary>
        /// <remarks>setting</remarks>
        protected bool _ImportTangents = false;

        #endregion

        #region Properties

        /// <summary>The glTF storage adapter.</summary>
        public GltfStorageAdapter GltfStorageAdapter { get; set; }

        /// <summary>List of unity material.</summary>
        public List<Material> UnityMaterialList { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Create a mesh asset.
        /// </summary>
        /// <param name="meshIndex">The index of glTF mesh.</param>
        /// <returns>A mesh asset.</returns>
        public virtual MeshAsset CreateMeshAsset(int meshIndex)
        {
            if (GltfStorageAdapter == null)
            {
                throw new Exception();
            }

            if (UnityMaterialList == null)
            {
                throw new Exception();
            }

            MeshContext meshContext = ReadMesh(meshIndex);

            Mesh mesh = BuildMesh(meshContext);

            MeshAsset meshAsset = new MeshAsset
            {
                Mesh = mesh,
                Materials = meshContext.materialIndices.Select(x => UnityMaterialList[x]).ToArray()
            };

            return meshAsset;
        }

        #endregion

        #region ReadMesh

        /// <summary>
        /// Read a mesh.
        /// </summary>
        /// <param name="meshIndex">The index of glTF mesh.</param>
        /// <returns>A mesh context.</returns>
        protected virtual MeshContext ReadMesh(int meshIndex)
        {
            GltfMesh gltfMesh = GltfStorageAdapter.Gltf.meshes[meshIndex];

            List<string> targetNames = GetTargetNames(gltfMesh.extras);

            bool isSharedMorphTarget = targetNames.Count > 0;

            MeshContext meshContext;

            if (isSharedMorphTarget)
            {
                meshContext = ImportMeshSharingAsMorphTarget(gltfMesh, targetNames);
            }
            else if (gltfMesh.AllPrimitivesHasSameAttributes())
            {
                meshContext = ImportMeshAsSharingVertexBuffer(gltfMesh);
            }
            else
            {
                meshContext = ImportMeshAsIndependentVertexBuffer(gltfMesh);
            }

            if (string.IsNullOrEmpty(meshContext.name))
            {
                meshContext.name = string.Format($"mesh_{meshIndex}");
            }

            return meshContext;
        }

        #endregion

        #region ReadMesh

        /// <summary>
        /// Import a mesh as sharing morph target.
        /// </summary>
        /// <param name="gltfMesh">The gltf mesh.</param>
        /// <param name="targetNames"></param>
        /// <returns>A mesh context.</returns>
        /// <remarks>
        /// mesh is sharing morph targets.
        /// </remarks>
        protected virtual MeshContext ImportMeshSharingAsMorphTarget(GltfMesh gltfMesh, List<string> targetNames)
        {
            var meshContext = new MeshContext(gltfMesh.name);

            // BlendShapes
            {
                for (int i = 1; i < gltfMesh.primitives.Count; ++i)
                {
                    if (gltfMesh.primitives[i].targets == null)
                    {
                        throw new Exception();
                    }
                    if (gltfMesh.primitives[i].targets.Count != targetNames.Count)
                    {
                        throw new FormatException(string.Format("different targets length: {0} with targetNames length.", gltfMesh.primitives[i]));
                    }
                }

                meshContext.blendShapes = targetNames
                    .Select((x, i) => new BlendShape(name: !string.IsNullOrEmpty(targetNames[i]) ? targetNames[i] : i.ToString()))
                    .ToList();
            }

            MeshPrimitiveAccessorContainer primitiveAccessorContainer = ChacheMeshPrimitiveAccessors(gltfMesh);

            foreach (GltfMeshPrimitive primitive in gltfMesh.primitives)
            {
                int indexOffset = meshContext.positions.Count;

                // Attributes
                AddAttributes(meshContext, primitive.attributes, primitiveAccessorContainer, out int positionsCount);

                // BlendShapes
                SetOrCreateBlendShapes(primitive, primitiveAccessorContainer, meshContext.blendShapes);

                // SubMeshes
                {
                    int[] indices = (primitive.indices != -1)
                        ? primitiveAccessorContainer.indices[primitive.indices]
                        : new int[positionsCount] // without index array
                        ;

                    for (int i = 0; i < indices.Length; ++i)
                    {
                        indices[i] += indexOffset;
                    }

                    meshContext.subMeshes.Add(indices.ToList());
                }

                // Material
                meshContext.materialIndices.Add(primitive.material);
            }

            return meshContext;
        }

        /// <summary>
        /// Import mesh as independent vertex buffer.
        /// </summary>
        /// <param name="gltfMesh">The gltf Mesh.</param>
        /// <returns>A mesh context.</returns>
        /// <remarks>
        /// multiple submMesh is not sharing a VertexBuffer.
        /// each subMesh use a independent VertexBuffer.
        /// </remarks>
        protected virtual MeshContext ImportMeshAsIndependentVertexBuffer(GltfMesh gltfMesh)
        {
            //Debug.LogWarning("_ImportMeshIndependentVertexBuffer");

            // MorphTargets
            {
                var targets = gltfMesh.primitives.First().targets;

                if (targets != null)
                {
                    for (int i = 1; i < gltfMesh.primitives.Count; ++i)
                    {
                        if (gltfMesh.primitives[i].targets.SequenceEqual(targets) == false)
                        {
                            throw new NotImplementedException(string.Format("different targets: {0} with {1}", gltfMesh.primitives[i], targets));
                        }
                    }
                }
            }

            var meshContext = new MeshContext(gltfMesh.name);

            MeshPrimitiveAccessorContainer primitiveAccessorContainer = ChacheMeshPrimitiveAccessors(gltfMesh);

            foreach (GltfMeshPrimitive primitive in gltfMesh.primitives)
            {
                int indexOffset = meshContext.positions.Count;

                // Attributes
                AddAttributes(meshContext, primitive.attributes, primitiveAccessorContainer, out int positionsCount);

                // BlendShapes
                meshContext.blendShapes = SetOrCreateBlendShapes(primitive, primitiveAccessorContainer);

                // SubMeshes
                {
                    int[] indices = (primitive.indices != -1)
                        ? primitiveAccessorContainer.indices[primitive.indices]
                        : new int[positionsCount] // without index array
                        ;

                    for (int i = 0; i < indices.Length; ++i)
                    {
                        indices[i] += indexOffset;
                    }

                    meshContext.subMeshes.Add(indices.ToList());
                }

                // Material
                meshContext.materialIndices.Add(primitive.material);
            }

            return meshContext;
        }

        /// <summary>
        /// Import mesh as sharing vertex buffer.
        /// </summary>
        /// <param name="gltfMesh">The gltf Mesh.</param>
        /// <returns>A mesh context.</returns>
        /// <remarks>
        /// multiple submesh sharing same VertexBuffer
        /// </remarks>
        protected virtual MeshContext ImportMeshAsSharingVertexBuffer(GltfMesh gltfMesh)
        {
            var meshContext = new MeshContext(gltfMesh.name);

            MeshPrimitiveAccessorContainer primitiveAccessorContainer = ChacheMeshPrimitiveAccessors(gltfMesh);

            // First only
            {
                GltfMeshPrimitive primitive = gltfMesh.primitives.First();

                // Attributes
                AddAttributes(meshContext, primitive.attributes, primitiveAccessorContainer, out _);

                // BlendShapes
                meshContext.blendShapes = SetOrCreateBlendShapes(primitive, primitiveAccessorContainer);
            }

            foreach (GltfMeshPrimitive primitive in gltfMesh.primitives)
            {
                // SubMeshes
                if (primitive.indices == -1)
                {
                    meshContext.subMeshes.Add(Enumerable.Range(0, meshContext.positions.Count).ToList());
                }
                else
                {
                    meshContext.subMeshes.Add(primitiveAccessorContainer.indices[primitive.indices].ToList());
                }

                // Material
                meshContext.materialIndices.Add(primitive.material);
            }

            return meshContext;
        }

        #endregion

        #region ReadMesh Cache Accessors

        /// <summary>
        /// Chache mesh primitive accessors data.
        /// </summary>
        /// <param name="gltfMesh">The glTF mesh.</param>
        /// <returns>A mesh primitive accessor container.</returns>
        protected virtual MeshPrimitiveAccessorContainer ChacheMeshPrimitiveAccessors(GltfMesh gltfMesh)
        {
            MeshPrimitiveAccessorContainer accessorContainer = new MeshPrimitiveAccessorContainer();

            for (int primitiveIndex = 0; primitiveIndex < gltfMesh.primitives.Count; primitiveIndex++)
            {
                GltfMeshPrimitive primitive = gltfMesh.primitives[primitiveIndex];

                GltfMeshPrimitiveAttributes attributes = primitive.attributes;

                // Positions
                if (attributes.POSITION != -1)
                {
                    if (accessorContainer.positions.ContainsKey(attributes.POSITION) == false)
                    {
                        accessorContainer.positions.Add(attributes.POSITION, GltfStorageAdapter.GetAccessorArrayData<Vector3>(attributes.POSITION));
                    }
                }

                // Normals
                if (attributes.NORMAL != -1)
                {
                    if (accessorContainer.normals.ContainsKey(attributes.NORMAL) == false)
                    {
                        accessorContainer.normals.Add(attributes.NORMAL, GltfStorageAdapter.GetAccessorArrayData<Vector3>(attributes.NORMAL));
                    }
                }

                // Tangents
                if (_ImportTangents)
                {
                    if (attributes.TANGENT != -1)
                    {
                        if (accessorContainer.tangents.ContainsKey(attributes.TANGENT) == false)
                        {
                            accessorContainer.tangents.Add(attributes.TANGENT, GltfStorageAdapter.GetAccessorArrayData<Vector4>(attributes.TANGENT));
                        }
                    }
                }

                // UVs
                if (attributes.TEXCOORD_0 != -1)
                {
                    if (accessorContainer.uvs.ContainsKey(attributes.TEXCOORD_0) == false)
                    {
                        GltfAccessor texcoordAccessor = GltfStorageAdapter.GetAccessor(attributes.TEXCOORD_0);

                        if (texcoordAccessor.type != GltfAccessorType.VEC2)
                        {
                            throw new FormatException(string.Format("texcoordAccessor.type: {0}", texcoordAccessor.type));
                        }

                        if (texcoordAccessor.componentType == GltfComponentType.FLOAT)
                        {
                            accessorContainer.uvs.Add(attributes.TEXCOORD_0, GltfStorageAdapter.GetAccessorArrayData<Vector2>(attributes.TEXCOORD_0));
                        }
                        else if (
                            (texcoordAccessor.componentType == GltfComponentType.UNSIGNED_BYTE) ||
                            (texcoordAccessor.componentType == GltfComponentType.UNSIGNED_SHORT))
                        {
                            if (texcoordAccessor.normalized)
                            {
                                throw new NotImplementedException(string.Format("texcoordAccessor.componentType: {0}", texcoordAccessor.componentType));
                            }
                            else
                            {
                                throw new FormatException(string.Format("texcoordAccessor.normalized: {0}", texcoordAccessor.normalized));
                            }
                        }
                        else
                        {
                            throw new FormatException(string.Format("texcoordAccessor.componentType: {0}", texcoordAccessor.componentType));
                        }
                    }
                }

                // Colors
                if (attributes.COLOR_0 != -1)
                {
                    if (accessorContainer.colors.ContainsKey(attributes.COLOR_0) == false)
                    {
                        GltfAccessor colorAccessor = GltfStorageAdapter.GetAccessor(attributes.COLOR_0);

                        if ((colorAccessor.type != GltfAccessorType.VEC3) &&
                            (colorAccessor.type != GltfAccessorType.VEC4))
                        {
                            throw new FormatException(string.Format("colorAccessor.type: {0}", colorAccessor.type));
                        }

                        Color[] colors = null;

                        if (colorAccessor.componentType == GltfComponentType.FLOAT)
                        {
                            if (colorAccessor.type == GltfAccessorType.VEC4)
                            {
                                // @notice Vector4(float) = Color4(float) = Color
                                colors = GltfStorageAdapter.GetAccessorArrayData<Color>(attributes.COLOR_0);
                            }
                            else if (colorAccessor.type == GltfAccessorType.VEC3)
                            {
                                // @notice Vector3(float) = Color3(float)
                                Color3[] color3array = GltfStorageAdapter.GetAccessorArrayData<Color3>(attributes.COLOR_0);

                                colors = new Color[color3array.Length];

                                for (int i = 0; i < color3array.Length; i++)
                                {
                                    colors[i] = color3array[i].ToUnityColor();
                                }
                            }
                        }
                        else if (
                            (colorAccessor.componentType == GltfComponentType.UNSIGNED_BYTE) ||
                            (colorAccessor.componentType == GltfComponentType.UNSIGNED_SHORT))
                        {
                            if (colorAccessor.normalized)
                            {
                                throw new NotImplementedException(string.Format("colorAccessor.componentType: {0}", colorAccessor.componentType));
                            }
                            else
                            {
                                throw new FormatException(string.Format("colorAccessor.normalized: {0}", colorAccessor.normalized));
                            }
                        }
                        else
                        {
                            throw new FormatException(string.Format("colorAccessor.componentType: {0}", colorAccessor.componentType));
                        }

                        accessorContainer.colors.Add(attributes.COLOR_0, colors);
                    }
                }

                // Joints
                if (attributes.JOINTS_0 != -1)
                {
                    if (accessorContainer.joints0.ContainsKey(attributes.JOINTS_0) == false)
                    {
                        GltfAccessor jointsAccessor = GltfStorageAdapter.GetAccessor(attributes.JOINTS_0);

                        if (jointsAccessor.type != GltfAccessorType.VEC4)
                        {
                            throw new FormatException(string.Format("jointsAccessor.type: {0}", jointsAccessor.type));
                        }

                        Vector4Ushort[] joints;

                        if (jointsAccessor.componentType == GltfComponentType.UNSIGNED_BYTE)
                        {
                            Vector4Ubyte[] vec4ubyteArray = GltfStorageAdapter.GetAccessorArrayData<Vector4Ubyte>(attributes.JOINTS_0);

                            joints = new Vector4Ushort[vec4ubyteArray.Length];

                            for (int i = 0; i < vec4ubyteArray.Length; i++)
                            {
                                joints[i] = new Vector4Ushort(
                                    vec4ubyteArray[i].X,
                                    vec4ubyteArray[i].Y,
                                    vec4ubyteArray[i].Z,
                                    vec4ubyteArray[i].W);
                            }
                        }
                        else if (jointsAccessor.componentType == GltfComponentType.UNSIGNED_SHORT)
                        {
                            joints = GltfStorageAdapter.GetAccessorArrayData<Vector4Ushort>(attributes.JOINTS_0);
                        }
                        else if (jointsAccessor.componentType == GltfComponentType.UNSIGNED_INT)
                        {
                            Vector4Uint[] vec4uintArray = GltfStorageAdapter.GetAccessorArrayData<Vector4Uint>(attributes.JOINTS_0);

                            joints = new Vector4Ushort[vec4uintArray.Length];

                            for (int i = 0; i < vec4uintArray.Length; i++)
                            {
                                joints[i] = new Vector4Ushort(
                                    (ushort)vec4uintArray[i].X,
                                    (ushort)vec4uintArray[i].Y,
                                    (ushort)vec4uintArray[i].Z,
                                    (ushort)vec4uintArray[i].W);
                            }
                        }
                        else
                        {
                            throw new FormatException(string.Format("jointsAccessor.componentType: {0}", jointsAccessor.componentType));
                        }

                        accessorContainer.joints0.Add(attributes.JOINTS_0, joints);
                    }
                }

                // Weights
                if (attributes.WEIGHTS_0 != -1)
                {
                    if (accessorContainer.weights0.ContainsKey(attributes.WEIGHTS_0) == false)
                    {
                        GltfAccessor weightsAccessor = GltfStorageAdapter.GetAccessor(attributes.WEIGHTS_0);

                        if (weightsAccessor.type != GltfAccessorType.VEC4)
                        {
                            throw new FormatException(string.Format("weightsAccessor.type: {0}", weightsAccessor.type));
                        }

                        Vector4[] weights;

                        if (weightsAccessor.componentType == GltfComponentType.FLOAT)
                        {
                            weights = GltfStorageAdapter.GetAccessorArrayData<Vector4>(attributes.WEIGHTS_0);
                        }
                        else if (
                            (weightsAccessor.componentType == GltfComponentType.UNSIGNED_BYTE) ||
                            (weightsAccessor.componentType == GltfComponentType.UNSIGNED_SHORT))
                        {
                            if (weightsAccessor.normalized)
                            {
                                throw new NotImplementedException(string.Format("weightsAccessor.componentType: {0}", weightsAccessor.componentType));
                            }
                            else
                            {
                                throw new FormatException(string.Format("weightsAccessor.normalized: {0}", weightsAccessor.normalized));
                            }
                        }
                        else
                        {
                            throw new FormatException(string.Format("weightsAccessor.componentType: {0}", weightsAccessor.componentType));
                        }

                        accessorContainer.weights0.Add(attributes.WEIGHTS_0, weights);
                    }
                }

                // Indices (SubMeshes)
                if (primitive.indices != -1)
                {
                    if (accessorContainer.indices.ContainsKey(primitive.indices) == false)
                    {
                        int[] indices = GetIndices(primitive.indices).ToArray().FlipTriangle();

                        accessorContainer.indices.Add(primitive.indices, indices);
                    }
                }

                // BlendShapes
                if (primitive.targets != null)
                {
                    for (int targetIndex = 0; targetIndex < primitive.targets.Count; targetIndex++)
                    {
                        GltfMeshPrimitiveAttributes morphTarget = primitive.targets[targetIndex];

                        if (morphTarget.POSITION != -1)
                        {
                            if (accessorContainer.blendShapePositions.ContainsKey(morphTarget.POSITION) == false)
                            {
                                accessorContainer.blendShapePositions.Add(morphTarget.POSITION, GltfStorageAdapter.GetAccessorArrayData<Vector3>(morphTarget.POSITION));
                            }
                        }

                        if (morphTarget.NORMAL != -1)
                        {
                            if (accessorContainer.blendShapeNormals.ContainsKey(morphTarget.NORMAL) == false)
                            {
                                accessorContainer.blendShapeNormals.Add(morphTarget.NORMAL, GltfStorageAdapter.GetAccessorArrayData<Vector3>(morphTarget.NORMAL));
                            }
                        }

                        if (morphTarget.TANGENT != -1)
                        {
                            if (accessorContainer.blendShapeTangents.ContainsKey(morphTarget.TANGENT) == false)
                            {
                                accessorContainer.blendShapeTangents.Add(morphTarget.TANGENT, GltfStorageAdapter.GetAccessorArrayData<Vector3>(morphTarget.TANGENT));
                            }
                        }
                    }
                }
            }

            return accessorContainer;
        }

        #endregion

        #region ReadMesh SubMesh

        /// <summary>
        /// Get submesh indices.
        /// </summary>
        /// <param name="accessorIndex">The index of accessor.</param>
        /// <returns>An enumerable indices.</returns>
        protected virtual IEnumerable<int> GetIndices(int accessorIndex)
        {
            GltfAccessor accessor = GltfStorageAdapter.GetAccessor(accessorIndex);

            if (accessor.type != GltfAccessorType.SCALAR)
            {
                throw new NotSupportedException($"accessor.type: {accessor.type}");
            }

            IEnumerable<int> indices;

            switch (accessor.componentType)
            {
                case GltfComponentType.UNSIGNED_BYTE:
                    indices = GltfStorageAdapter.GetAccessorArrayData<byte>(accessorIndex).Select(x => (int)x);
                    break;

                case GltfComponentType.UNSIGNED_SHORT:
                    indices = GltfStorageAdapter.GetAccessorArrayData<ushort>(accessorIndex).Select(x => (int)x);
                    break;

                case GltfComponentType.UNSIGNED_INT:
                    indices = GltfStorageAdapter.GetAccessorArrayData<uint>(accessorIndex).Select(x => (int)x);
                    break;

                default:
                    throw new NotSupportedException($"accessor.componentType: {accessor.componentType}");
            }

            return indices;
        }

        #endregion

        #region ReadMesh Attributes

        /// <summary>
        /// Add attributes.
        /// </summary>
        /// <param name="meshContext">The mesh context.</param>
        /// <param name="attributes">The mesh primitive attributes.</param>
        /// <param name="accessorContainer">The mesh primitive accessor container.</param>
        /// <param name="positionsCount">The positions count.</param>
        /// <remarks>Always cached in the Container unless the accessorIndex equals -1.</remarks>
        protected virtual void AddAttributes(MeshContext meshContext, GltfMeshPrimitiveAttributes attributes, MeshPrimitiveAccessorContainer accessorContainer, out int positionsCount)
        {
            positionsCount = 0;

            // Positions
            if (attributes.POSITION != -1)
            {
                Vector3[] positions = accessorContainer.positions[attributes.POSITION];

                for (int idx = 0; idx < positions.Length; idx++)
                {
                    meshContext.positions.Add(positions[idx].ReverseZ());
                }

                positionsCount = positions.Length;
            }

            // Normals
            if (attributes.NORMAL != -1)
            {
                Vector3[] normals = accessorContainer.normals[attributes.NORMAL];

                for (int idx = 0; idx < normals.Length; idx++)
                {
                    meshContext.normals.Add(normals[idx].ReverseZ());
                }
            }

            // Tangents
            if (_ImportTangents)
            {
                if (attributes.TANGENT != -1)
                {
                    Vector4[] tangents = accessorContainer.tangents[attributes.TANGENT];

                    for (int idx = 0; idx < tangents.Length; idx++)
                    {
                        meshContext.tangents.Add(tangents[idx].ReverseZ());
                    }
                }
            }

            // UVs
            if (attributes.TEXCOORD_0 != -1)
            {
                Vector2[] uvs = accessorContainer.uvs[attributes.TEXCOORD_0];

                for (int idx = 0; idx < uvs.Length; idx++)
                {
                    meshContext.uvs.Add(uvs[idx].ReverseUV());
                }
            }
            else
            {
                meshContext.uvs.AddRange(new Vector2[positionsCount]);
            }

            // Colors
            if (attributes.COLOR_0 != -1)
            {
                meshContext.colors.AddRange(accessorContainer.colors[attributes.COLOR_0]);
            }

            // Joints & Weights -> BoneWeights
            if ((attributes.JOINTS_0 != -1) && (attributes.WEIGHTS_0 != -1))
            {
                Vector4Ushort[] joints0 = accessorContainer.joints0[attributes.JOINTS_0];
                Vector4[] weights0 = accessorContainer.weights0[attributes.WEIGHTS_0];

                if (joints0.Length == weights0.Length)
                {
                    for (int idx = 0; idx < joints0.Length; idx++)
                    {
                        BoneWeight boneWeight = ConvertJointAndWeightToBoneWeight(joints0[idx], weights0[idx]);

                        meshContext.boneWeights.Add(boneWeight);
                    }
                }
            }
        }

        #endregion

        #region ReadMesh BlendShapes

        /// <summary>
        /// Set or create blend shapes.
        /// </summary>
        /// <param name="primitive">The mesh primitive.</param>
        /// <param name="accessorContainer">The mesh primitive accessor container.</param>
        /// <param name="blendShapes">List of blend shape.</param>
        /// <returns>List of blend shape.</returns>
        /// <remarks>Always cached in the Container unless the accessorIndex equals -1.</remarks>
        protected virtual List<BlendShape> SetOrCreateBlendShapes(GltfMeshPrimitive primitive, MeshPrimitiveAccessorContainer accessorContainer, List<BlendShape> blendShapes = null)
        {
            List<GltfMeshPrimitiveAttributes> morphTargets = primitive.targets;

            if ((morphTargets == null) || (morphTargets.Any() == false))
            {
                return null;
            }

            if (blendShapes == null)
            {
                if (primitive.extras.Contains("targetNames") == false)
                {
                    throw new Exception();
                }

                List<string> targetNames = GetTargetNames(primitive.extras);

                blendShapes = morphTargets
                    .Select((x, i) => new BlendShape(name: !string.IsNullOrEmpty(targetNames[i]) ? targetNames[i] : i.ToString()))
                    .ToList();
            }

            for (int targetIndex = 0; targetIndex < morphTargets.Count; targetIndex++)
            {
                GltfMeshPrimitiveAttributes morphTarget = primitive.targets[targetIndex];

                if (morphTarget.POSITION != -1)
                {
                    Vector3[] positions = accessorContainer.blendShapePositions[morphTarget.POSITION];

                    for (int idx = 0; idx < positions.Length; idx++)
                    {
                        blendShapes[targetIndex].Positions.Add(positions[idx].ReverseZ());
                    }
                }
                if (morphTarget.NORMAL != -1)
                {
                    Vector3[] normals = accessorContainer.blendShapeNormals[morphTarget.NORMAL];

                    for (int idx = 0; idx < normals.Length; idx++)
                    {
                        blendShapes[targetIndex].Normals.Add(normals[idx].ReverseZ());
                    }
                }
                if (morphTarget.TANGENT != -1)
                {
                    Vector3[] tangents = accessorContainer.blendShapeTangents[morphTarget.TANGENT];

                    for (int idx = 0; idx < tangents.Length; idx++)
                    {
                        blendShapes[targetIndex].Tangents.Add(tangents[idx].ReverseZ());
                    }
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
            if (meshContext.positions.Count > UInt16.MaxValue)
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
            if ((meshContext.uvs != null) && meshContext.uvs.Any())
            {
                mesh.SetUVs(channel: 0, meshContext.uvs);
            }

            // Colors
            if ((meshContext.colors != null) && meshContext.colors.Any())
            {
                mesh.SetColors(meshContext.colors);
            }

            // BoneWeights
            if ((meshContext.boneWeights != null) && meshContext.boneWeights.Any())
            {
                mesh.boneWeights = meshContext.boneWeights.ToArray();
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
                    if (blendShape.Positions.Count > 0)
                    {
                        if (blendShape.Positions.Count == mesh.vertexCount)
                        {
                            Vector3[] deltaPositions = blendShape.Positions.ToArray();

                            Vector3[] deltaNormals = null;
                            if ((meshContext.normals != null) &&
                                (meshContext.normals.Count == mesh.vertexCount) &&
                                (blendShape.Normals.Count() == blendShape.Positions.Count()))
                            {
                                deltaNormals = blendShape.Normals.ToArray();
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

                        //Debug.LogFormat($"empty blendshape: {mesh.name}.{blendShape.name}");

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

        /// <summary>
        /// Get target names.
        /// </summary>
        /// <param name="extras">The extras.</param>
        /// <returns>List of the target name.</returns>
        /// <remarks>
        /// gltf.meshes[].extras
        /// gltf.meshes[].primitives[].extras
        /// </remarks>
        protected virtual List<string> GetTargetNames(GltfExtras extras)
        {
            if (extras != null)
            {
                extras.JsonSerializerSettings = _JsonSerializerSettings;

                if (extras.TryGetValue("targetNames", out List<string> targetNames))
                {
                    return targetNames;
                }
            }

            return new List<string>();
        }

        #endregion
    }
}
