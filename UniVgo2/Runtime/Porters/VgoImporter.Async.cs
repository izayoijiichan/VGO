// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : VgoImporter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
    //
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
    using Cysharp.Threading.Tasks;
#else
    using System.Threading.Tasks;
#endif

    using NewtonVgo;
    using System.Collections.Generic;
    using System.Threading;
    using UnityEngine;

    /// <summary>
    /// VGO Importer
    /// </summary>
    public partial class VgoImporter
    {
#if UNITY_WEBGL
        //
#else
        #region Public Methods

        /// <summary>
        /// Load a 3D model from the specified file.
        /// </summary>
        /// <param name="vgoFilePath">The file path of the vgo.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A vgo model asset.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        public virtual async Awaitable<VgoModelAsset> LoadAsync(string vgoFilePath, CancellationToken cancellationToken)
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        public virtual async UniTask<VgoModelAsset> LoadAsync(string vgoFilePath, CancellationToken cancellationToken)
#else
        public virtual async Task<VgoModelAsset> LoadAsync(string vgoFilePath, CancellationToken cancellationToken)
#endif
        {
            var vgoStorage = new VgoStorage(vgoFilePath);

            return await LoadAsync(vgoStorage, cancellationToken);
        }

        /// <summary>
        /// Load a 3D model from the specified file.
        /// </summary>
        /// <param name="vgoFilePath">The file path of the vgo.</param>
        /// <param name="vgkFilePath">The file path of the crypt key.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A vgo model asset.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        public virtual async Awaitable<VgoModelAsset> LoadAsync(string vgoFilePath, string vgkFilePath, CancellationToken cancellationToken)
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        public virtual async UniTask<VgoModelAsset> LoadAsync(string vgoFilePath, string vgkFilePath, CancellationToken cancellationToken)
#else
        public virtual async Task<VgoModelAsset> LoadAsync(string vgoFilePath, string vgkFilePath, CancellationToken cancellationToken)
#endif
        {
            var vgoStorage = new VgoStorage(vgoFilePath, vgkFilePath);

            return await LoadAsync(vgoStorage, cancellationToken);
        }

        /// <summary>
        /// Load a 3D model from the specified bytes.
        /// </summary>
        /// <param name="vgoBytes">The vgo bytes.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A vgo model asset.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        public virtual async Awaitable<VgoModelAsset> LoadAsync(byte[] vgoBytes, CancellationToken cancellationToken)
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        public virtual async UniTask<VgoModelAsset> LoadAsync(byte[] vgoBytes, CancellationToken cancellationToken)
#else
        public virtual async Task<VgoModelAsset> LoadAsync(byte[] vgoBytes, CancellationToken cancellationToken)
#endif
        {
            var vgoStorage = new VgoStorage(vgoBytes);

            return await LoadAsync(vgoStorage, cancellationToken);
        }

        /// <summary>
        /// Load a 3D model from the specified bytes.
        /// </summary>
        /// <param name="vgoBytes">The vgo bytes.</param>
        /// <param name="vgkBytes">The vgk bytes.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A vgo model asset.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        public virtual async Awaitable<VgoModelAsset> LoadAsync(byte[] vgoBytes, byte[] vgkBytes, CancellationToken cancellationToken)
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        public virtual async UniTask<VgoModelAsset> LoadAsync(byte[] vgoBytes, byte[] vgkBytes, CancellationToken cancellationToken)
#else
        public virtual async Task<VgoModelAsset> LoadAsync(byte[] vgoBytes, byte[] vgkBytes, CancellationToken cancellationToken)
#endif
        {
            var vgoStorage = new VgoStorage(vgoBytes, vgkBytes);

            return await LoadAsync(vgoStorage, cancellationToken);
        }

        /// <summary>
        /// Load a 3D model from the specified vgo storage.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A vgo model asset.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        public virtual async Awaitable<VgoModelAsset> LoadAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken)
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        public virtual async UniTask<VgoModelAsset> LoadAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken)
#else
        public virtual async Task<VgoModelAsset> LoadAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken)
#endif
        {
            var vgoModelAsset = new VgoModelAsset();

            vgoModelAsset.Layout = vgoStorage.Layout;

            // UnityEngine.Texture2D
            //vgoModelAsset.Texture2dList = _TextureImporter.CreateTextureAssets(vgoStorage);
            vgoModelAsset.Texture2dList = await _TextureImporter.CreateTextureAssetsAsync(vgoStorage, cancellationToken);

            // UnityEngine.Material
            vgoModelAsset.MaterialList = CreateMaterialAssets(vgoStorage, vgoModelAsset.Texture2dList);

            // UnityEngine.Mesh
            if (vgoStorage.IsSpecVersion_2_4_orLower)
            {
                //vgoModelAsset.MeshAssetList = _MeshImporter.CreateMeshAssets(vgoStorage, modelAsset.MaterialList);
                vgoModelAsset.MeshAssetList = await _MeshImporter.CreateMeshAssetsAsync(vgoStorage, vgoModelAsset.MaterialList, cancellationToken);
                //vgoModelAsset.MeshAssetList = await _MeshImporter.CreateMeshAssetsParallelAsync(vgoStorage, modelAsset.MaterialList, cancellationToken);
            }
            else
            {
                //vgoModelAsset.MeshAssetList = _MeshImporter.CreateMeshAssets(vgoStorage);
                vgoModelAsset.MeshAssetList = await _MeshImporter.CreateMeshAssetsAsync(vgoStorage, null, cancellationToken);
                //vgoModelAsset.MeshAssetList = await _MeshImporter.CreateMeshAssetsParallelAsync(vgoStorage, null, cancellationToken);
            }

            // UnityEngine.AnimationClip
            vgoModelAsset.AnimationClipList = CreateAnimationClipAssets(vgoStorage.Layout, vgoStorage.GeometryCoordinate);

            // UnityEngine.VgoSpringBoneColliderGroup
            vgoModelAsset.SpringBoneColliderGroupArray = CreateSpringBoneColliderGroupArray(vgoStorage.Layout);

            // UnityEngine.GameObejct
            List<Transform> nodes = CreateNodes(vgoStorage);

            CreateNodeHierarchy(nodes, vgoStorage.Layout);

            vgoModelAsset.Root = nodes[0].gameObject;

            if (vgoStorage.GeometryCoordinate == VgoGeometryCoordinate.RightHanded)
            {
                FixCoordinate(nodes);
            }

            SetupNodes(nodes, vgoStorage, vgoModelAsset);

            SetupAssetInfo(vgoStorage, vgoModelAsset);

#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
            //await Awaitable.NextFrameAsync(cancellationToken);

            return vgoModelAsset;
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
            return await UniTask.FromResult(vgoModelAsset);
#else
            return await Task.FromResult(vgoModelAsset);
#endif
        }

        #endregion
#endif  // UNITY_WEBGL

        #region Public Methods

        ///// <summary>
        ///// Extract a 3D model asset from the specified file.
        ///// </summary>
        ///// <param name="vgoFilePath">The file path of the vgo.</param>
        ///// <param name="vgkFilePath">The file path of the vgk.</param>
        ///// <param name="cancellationToken"></param>
        ///// <returns>A vgo model asset.</returns>
        ///// <remarks>for ScriptedImporter</remarks>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        //public virtual async Awaitable<VgoModelAsset> ExtractAsync(string vgoFilePath, string? vgkFilePath, CancellationToken cancellationToken)
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        //public virtual async UniTask<VgoModelAsset> ExtractAsync(string vgoFilePath, string? vgkFilePath, CancellationToken cancellationToken)
#else
        //public virtual async Task<VgoModelAsset> ExtractAsync(string vgoFilePath, string? vgkFilePath, CancellationToken cancellationToken)
#endif
        //{
        //    var vgoStorage = new VgoStorage(vgoFilePath, vgkFilePath);

        //    var vgoModelAsset = new VgoModelAsset();

        //    vgoModelAsset.Layout = vgoStorage.Layout;

        //    // UnityEngine.Texture2D
        //    vgoModelAsset.Texture2dList = await _TextureImporter.CreateTextureAssetsAsync(vgoStorage, cancellationToken);

        //    // UnityEngine.Material
        //    vgoModelAsset.MaterialList = CreateMaterialAssets(vgoStorage, modelAsset.Texture2dList);

        //    return vgoModelAsset;
        //}

        #endregion
    }
}
