// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : VgoMeshImporter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Porters
{
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
    //
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
    using Cysharp.Threading.Tasks;
#else
    //System.Threading.Tasks;
#endif

    using NewtonVgo;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
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
        public VgoMeshImporter(in MeshImporterOption option)
        {
            _Option = option;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Create mesh assets.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="materialList">List of unity material.</param>
        /// <returns>List of mesh asset.</returns>
        public virtual List<MeshAsset> CreateMeshAssets(in IVgoStorage vgoStorage, in IList<Material?>? materialList = null)
        {
            if ((vgoStorage.Layout.meshes == null) || (vgoStorage.Layout.meshes.Any() == false))
            {
                return new List<MeshAsset>(0);
            }

            var meshAssetList = new List<MeshAsset>(vgoStorage.Layout.meshes.Count);

            for (int meshIndex = 0; meshIndex < vgoStorage.Layout.meshes.Count; meshIndex++)
            {
                MeshAsset meshAsset = CreateMeshAsset(vgoStorage, meshIndex, materialList);

                if (meshAssetList.Where(x => x?.Mesh.name == meshAsset.Mesh.name).Any())
                {
                    meshAsset.Mesh.name = string.Format($"mesh_{meshIndex}");
                }

                meshAssetList.Add(meshAsset);
            }

            return meshAssetList;
        }

        /// <summary>
        /// Create mesh assets.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="materialList">List of unity material.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>List of mesh asset.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        public virtual async Awaitable<List<MeshAsset>> CreateMeshAssetsAsync(IVgoStorage vgoStorage, IList<Material?>? materialList, CancellationToken cancellationToken)
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        public virtual async UniTask<List<MeshAsset>> CreateMeshAssetsAsync(IVgoStorage vgoStorage, IList<Material?>? materialList, CancellationToken cancellationToken)
#else
        public virtual async Task<List<MeshAsset>> CreateMeshAssetsAsync(IVgoStorage vgoStorage, IList<Material?>? materialList, CancellationToken cancellationToken)
#endif
        {
            if ((vgoStorage.Layout.meshes == null) || (vgoStorage.Layout.meshes.Any() == false))
            {
                return new List<MeshAsset>(0);
            }

            var meshAssetList = new List<MeshAsset>(vgoStorage.Layout.meshes.Count);

            for (int meshIndex = 0; meshIndex < vgoStorage.Layout.meshes.Count; meshIndex++)
            {
                MeshAsset meshAsset = await CreateMeshAssetAsync(vgoStorage, meshIndex, materialList, cancellationToken);

                if (meshAssetList.Where(x => x?.Mesh.name == meshAsset.Mesh.name).Any())
                {
                    meshAsset.Mesh.name = string.Format($"mesh_{meshIndex}");
                }

                meshAssetList.Add(meshAsset);
            }

            return meshAssetList;
        }

        /// <summary>
        /// Create mesh assets.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="materialList">List of unity material.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>List of mesh asset.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        public virtual async Awaitable<List<MeshAsset>> CreateMeshAssetsParallelAsync(IVgoStorage vgoStorage, IList<Material?>? materialList, CancellationToken cancellationToken)
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        public virtual async UniTask<List<MeshAsset>> CreateMeshAssetsParallelAsync(IVgoStorage vgoStorage, IList<Material?>? materialList, CancellationToken cancellationToken)
#else
        public virtual async Task<List<MeshAsset>> CreateMeshAssetsParallelAsync(IVgoStorage vgoStorage, IList<Material?>? materialList, CancellationToken cancellationToken)
#endif
        {
            if ((vgoStorage.Layout.meshes == null) || (vgoStorage.Layout.meshes.Any() == false))
            {
                return new List<MeshAsset>(0);
            }

            var meshAssetList = new List<MeshAsset>(vgoStorage.Layout.meshes.Count);

#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
            await Awaitable.BackgroundThreadAsync();
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
            await UniTask.SwitchToThreadPool();
#endif
            //var sw = new System.Diagnostics.Stopwatch();

            //sw.Start();

            List<MeshContext> meshContextList = await CreateMeshContextListParallelAsync(vgoStorage, cancellationToken);

            //sw.Stop();

            //Debug.LogFormat("{0,2}: {1,5}ms", Thread.CurrentThread.ManagedThreadId, sw.ElapsedMilliseconds);

#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
            await Awaitable.MainThreadAsync();
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
            await UniTask.SwitchToMainThread();
#endif

            for (int meshIndex = 0; meshIndex < vgoStorage.Layout.meshes.Count; meshIndex++)
            {
                MeshAsset meshAsset = CreateMeshAsset(vgoStorage, meshIndex, meshContextList, materialList);

                if (meshAssetList.Where(x => x?.Mesh.name == meshAsset.Mesh.name).Any())
                {
                    meshAsset.Mesh.name = string.Format($"mesh_{meshIndex}");
                }

                meshAssetList.Add(meshAsset);
            }

#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
            //await Awaitable.NextFrameAsync(cancellationToken);

            return meshAssetList;
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
            return await UniTask.FromResult(meshAssetList);
#else
            return await Task.FromResult(meshAssetList);
#endif
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Create a mesh asset.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="meshIndex">The index of vgo mesh.</param>
        /// <param name="unityMaterialList">List of unity material.</param>
        /// <returns>A mesh asset.</returns>
        protected virtual MeshAsset CreateMeshAsset(in IVgoStorage vgoStorage, in int meshIndex, in IList<Material?>? unityMaterialList = null)
        {
            MeshContext meshContext = ReadMesh(vgoStorage, meshIndex);

            Mesh mesh = BuildMesh(meshContext);

            MeshAsset meshAsset = new MeshAsset(mesh);

            if (vgoStorage.IsSpecVersion_2_4_orLower)
            {
                if (unityMaterialList == null)
                {
                    ThrowHelper.ThrowArgumentNullException(nameof(unityMaterialList));

                    return meshAsset;
                }

                var materialList = unityMaterialList;

                meshAsset.Materials = meshContext.MaterialIndices.Select(x => materialList[x]).ToArray();
            }

            if ((meshContext.BlendShapesContext != null) &&
                (meshContext.BlendShapesContext.BlendShapeConfig != null))
            {
                meshAsset.BlendShapeConfig = meshContext.BlendShapesContext.BlendShapeConfig;
            }

            return meshAsset;
        }

        /// <summary>
        /// Create a mesh asset.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="meshIndex">The index of vgo mesh.</param>
        /// <param name="meshContextList">List of mesh context.</param>
        /// <param name="unityMaterialList">List of unity material.</param>
        /// <returns>A mesh asset.</returns>
        protected virtual MeshAsset CreateMeshAsset(in IVgoStorage vgoStorage, in int meshIndex, in IList<MeshContext> meshContextList, in IList<Material?>? unityMaterialList = null)
        {
            if (meshIndex.IsInRangeOf(meshContextList) == false)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(nameof(meshIndex), meshIndex, min: 0, max: meshContextList.Count);
            }

            MeshContext meshContext = meshContextList[meshIndex];

            Mesh mesh = BuildMesh(meshContext);

            MeshAsset meshAsset = new MeshAsset(mesh);

            if (vgoStorage.IsSpecVersion_2_4_orLower)
            {
                if (unityMaterialList == null)
                {
                    ThrowHelper.ThrowArgumentNullException(nameof(unityMaterialList));

                    return meshAsset;
                }

                var materialList = unityMaterialList;

                meshAsset.Materials = meshContext.MaterialIndices.Select(x => materialList[x]).ToArray();
            }

            if ((meshContext.BlendShapesContext != null) &&
                (meshContext.BlendShapesContext.BlendShapeConfig != null))
            {
                meshAsset.BlendShapeConfig = meshContext.BlendShapesContext.BlendShapeConfig;
            }

            return meshAsset;
        }

        /// <summary>
        /// Create a mesh asset.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="meshIndex">The index of vgo mesh.</param>
        /// <param name="unityMaterialList">List of unity material.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A mesh asset.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        protected virtual async Awaitable<MeshAsset> CreateMeshAssetAsync(IVgoStorage vgoStorage, int meshIndex, IList<Material?>? unityMaterialList, CancellationToken cancellationToken)
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        protected virtual async UniTask<MeshAsset> CreateMeshAssetAsync(IVgoStorage vgoStorage, int meshIndex, IList<Material?>? unityMaterialList, CancellationToken cancellationToken)
#else
        protected virtual async Task<MeshAsset> CreateMeshAssetAsync(IVgoStorage vgoStorage, int meshIndex, IList<Material?>? unityMaterialList, CancellationToken cancellationToken)
#endif
        {
            cancellationToken.ThrowIfCancellationRequested();

#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
            await Awaitable.BackgroundThreadAsync();

            //var sw = new System.Diagnostics.Stopwatch();

            //sw.Start();

            MeshContext meshContext = ReadMesh(vgoStorage, meshIndex);

            //sw.Stop();

            //Debug.LogFormat("{0,2}: {1,5}ms", Thread.CurrentThread.ManagedThreadId, sw.ElapsedMilliseconds);

            await Awaitable.MainThreadAsync();
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
            await UniTask.SwitchToThreadPool();

            //var sw = new System.Diagnostics.Stopwatch();

            //sw.Start();

            MeshContext meshContext = ReadMesh(vgoStorage, meshIndex);

            //sw.Stop();

            //Debug.LogFormat("{0,2}: {1,5}ms", Thread.CurrentThread.ManagedThreadId, sw.ElapsedMilliseconds);

            await UniTask.SwitchToMainThread();
#else
            //var sw = new System.Diagnostics.Stopwatch();

            //sw.Start();

            MeshContext meshContext = ReadMesh(vgoStorage, meshIndex);

            //MeshContext meshContext = await Task.Run(() =>
            //{
            //    //var swt = new System.Diagnostics.Stopwatch();

            //    //swt.Start();

            //    MeshContext context = ReadMesh(vgoStorage, meshIndex);

            //    //swt.Stop();

            //    //Debug.LogFormat("{0,2}: {1,5}ms", Thread.CurrentThread.ManagedThreadId, swt.ElapsedMilliseconds);

            //    return context;
            //});

            //sw.Stop();

            //Debug.LogFormat("{0,2}: {1,5}ms", Thread.CurrentThread.ManagedThreadId, sw.ElapsedMilliseconds);
#endif

            Mesh mesh = BuildMesh(meshContext);

            MeshAsset meshAsset = new MeshAsset(mesh);

            if (vgoStorage.IsSpecVersion_2_4_orLower)
            {
                if (unityMaterialList == null)
                {
                    ThrowHelper.ThrowArgumentNullException(nameof(unityMaterialList));

                    return meshAsset;
                }

                meshAsset.Materials = meshContext.MaterialIndices.Select(x => unityMaterialList[x]).ToArray();
            }

            if ((meshContext.BlendShapesContext != null) &&
                (meshContext.BlendShapesContext.BlendShapeConfig != null))
            {
                meshAsset.BlendShapeConfig = meshContext.BlendShapesContext.BlendShapeConfig;
            }

#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
            //await Awaitable.NextFrameAsync(cancellationToken);

            return meshAsset;
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
            return await UniTask.FromResult(meshAsset);
#else
            return await Task.FromResult(meshAsset);
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>List of mesh context.</returns>
        protected virtual async Task<List<MeshContext>> CreateMeshContextListParallelAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken)
        {
            if ((vgoStorage.Layout.meshes == null) || (vgoStorage.Layout.meshes.Any() == false))
            {
                return new List<MeshContext>(0);
            }

            var meshContextDictionary = new ConcurrentDictionary<int, MeshContext>();

            var createMeshContextTasks = new List<Task>(vgoStorage.Layout.meshes.Count);

            for (int meshIndex = 0; meshIndex < vgoStorage.Layout.meshes.Count; meshIndex++)
            {
                int index = meshIndex;  // @important

                Task createMeshContextTask = Task.Run(() =>
                {
                    MeshContext meshContext = ReadMesh(vgoStorage, index);

                    meshContextDictionary.TryAdd(index, meshContext);
                });

                createMeshContextTasks.Add(createMeshContextTask);
            }

            await Task.WhenAll(createMeshContextTasks);

            return meshContextDictionary.Values.ToList();
        }

        #endregion

        #region ReadMesh

        /// <summary>
        /// Read a mesh.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="meshIndex">The index of vgo mesh.</param>
        /// <returns>A mesh context.</returns>
        protected virtual MeshContext ReadMesh(in IVgoStorage vgoStorage, in int meshIndex)
        {
            if (vgoStorage.Layout.meshes == null)
            {
#if NET_STANDARD_2_1
                ThrowHelper.ThrowException();
#else
                throw new Exception();
#endif
            }

            VgoMesh? vgoMesh = vgoStorage.Layout.meshes[meshIndex];

            if (vgoMesh is null)
            {
#if NET_STANDARD_2_1
                ThrowHelper.ThrowException();
#else
                throw new Exception();
#endif
            }

            string meshName = ((vgoMesh.name is null) || (vgoMesh.name == string.Empty))
                ? string.Format($"mesh_{meshIndex}")
                : vgoMesh.name;

            var meshContext = new MeshContext(meshName);

            if (vgoMesh.attributes == null)
            {
#if NET_STANDARD_2_1
                ThrowHelper.ThrowException();
#else
                throw new Exception();
#endif
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

                    if (vgoMesh.blendShapePresets != null)
                    {
                        blendShapeConfig.Presets = vgoMesh.blendShapePresets;
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
        protected virtual void SetPrimitiveAttributes(in IVgoStorage vgoStorage, MeshContext meshContext, in VgoMeshPrimitiveAttributes attributes, out int positionsCount)
        {
            positionsCount = 0;

            // Positions
            if (attributes.POSITION != -1)
            {
                Vector3[] positions = vgoStorage.GetResourceDataAsArray<Vector3>(attributes.POSITION);

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
                Vector3[] normals = vgoStorage.GetResourceDataAsArray<Vector3>(attributes.NORMAL);

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
                        tangents = vgoStorage.GetResourceDataAsArray<Vector4>(attributes.TANGENT);
                    }
                    else
                    {
                        ThrowHelper.ThrowNotImplementedException($"unknown tangentAccessor.type: {tangentAccessor.dataType}");

                        return;
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
                    Color32[] colors = vgoStorage.GetResourceDataAsArray<Color32>(attributes.COLOR_0);

                    meshContext.Color32s = colors;
                }
                else if (colorAccessor.dataType == VgoResourceAccessorDataType.Vector4Float)
                {
                    // @notice Vector4(float) = Color
                    Color[] colors = vgoStorage.GetResourceDataAsArray<Color>(attributes.COLOR_0);

                    meshContext.Colors = colors;
                }
                else
                {
                    ThrowHelper.ThrowNotImplementedException($"unknown colorAccessor.dataType: {colorAccessor.dataType}");
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
                    ReadOnlySpan<Vector4Ubyte> vec4byteSpan = vgoStorage.GetResourceDataAsSpan<Vector4Ubyte>(attributes.JOINTS_0);

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
                    joints = vgoStorage.GetResourceDataAsArray<Vector4Ushort>(attributes.JOINTS_0);
                }
                else
                {
                    ThrowHelper.ThrowNotImplementedException($"unknown jointsAccessor.dataType: {jointsAccessor.dataType}");

                    return;
                }

                // Weights
                Vector4[] weights = vgoStorage.GetResourceDataAsArray<Vector4>(attributes.WEIGHTS_0);

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
        /// <param name="texcoord">The accessor index of texture coordinate.</param>
        /// <param name="positionsCount">The positions count.</param>
        /// <returns>An array of UV.</returns>
        protected virtual Vector2[]? ReadUV(in IVgoStorage vgoStorage, in int texcoord, in int positionsCount)
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
                Vector2[] resourceUvs = vgoStorage.GetResourceDataAsArray<Vector2>(texcoord);

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
        protected virtual List<int[]>? CreateSubMeshes(in IVgoStorage vgoStorage, in List<int>? vgoSubMeshes, in int positionsLength)
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
        protected virtual int[] GetSubMeshIndices(in IVgoStorage vgoStorage, in int accessorIndex)
        {
            VgoResourceAccessor accessor = vgoStorage.GetAccessor(accessorIndex);

            int[] indices;

            switch (accessor.dataType)
            {
                case VgoResourceAccessorDataType.UnsignedByte:
                    //indices = vgoStorage.GetAccessorArrayData<byte>(accessorIndex).Select(x => (int)x);
                    {
                        ReadOnlySpan<byte> indexSpan = vgoStorage.GetResourceDataAsSpan<byte>(accessorIndex);

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
                        ReadOnlySpan<ushort> indexSpan = vgoStorage.GetResourceDataAsSpan<ushort>(accessorIndex);

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
                        ReadOnlySpan<uint> indexSpan = vgoStorage.GetResourceDataAsSpan<uint>(accessorIndex);

                        indices = new int[indexSpan.Length];

                        for (int idx = 0; idx < indexSpan.Length; idx++)
                        {
                            indices[idx] = (int)indexSpan[idx];
                        }
                    }
                    break;

                default:
                    ThrowHelper.ThrowNotSupportedException($"accessor.dataType: {accessor.dataType}");

                    return Array.Empty<int>();
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
        protected virtual BlendShapesContext? CreateBlendShapes(in IVgoStorage vgoStorage, in List<VgoMeshBlendShape>? vgoMeshBlendShapes)
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
                    Vector3[] positions = vgoStorage.GetResourceDataAsArray<Vector3>(attributes.POSITION);

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
                    Vector3[] normals = vgoStorage.GetResourceDataAsArray<Vector3>(attributes.NORMAL);

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
                    Vector3[] tangents = vgoStorage.GetResourceDataAsArray<Vector3>(attributes.TANGENT);

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
                        Index = shapeIndex,
                        Type = vgoBlendShape.facePartsType,
                    };

                    context.BlendShapeConfig.FaceParts.Add(facePart);
                }

                if (vgoBlendShape.blinkType != VgoBlendShapeBlinkType.None)
                {
                    var blink = new BlendShapeBlink
                    {
                        Index = shapeIndex,
                        Type = vgoBlendShape.blinkType,
                    };

                    context.BlendShapeConfig.Blinks.Add(blink);
                }

                if (vgoBlendShape.visemeType != VgoBlendShapeVisemeType.None)
                {
                    var viseme = new BlendShapeViseme
                    {
                        Index = shapeIndex,
                        Type = vgoBlendShape.visemeType,
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
                        emptyVertices ??= new Vector3[mesh.vertexCount];

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
        protected virtual BoneWeight ConvertJointAndWeightToBoneWeight(in Vector4Ushort joint, in Vector4 weight)
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
