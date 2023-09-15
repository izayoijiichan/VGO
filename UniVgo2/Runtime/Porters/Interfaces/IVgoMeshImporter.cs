// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Interface : IVgoMeshImporter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Porters
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
    /// VGO Mesh Importer Interface
    /// </summary>
    public interface IVgoMeshImporter
    {
        #region Public Methods

        /// <summary>
        /// Create mesh assets.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="materialList">List of unity material.</param>
        /// <returns>List of mesh asset.</returns>
        List<MeshAsset> CreateMeshAssets(in IVgoStorage vgoStorage, in IList<Material?>? materialList = null);

        /// <summary>
        /// Create mesh assets.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="materialList">List of unity material.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>List of mesh asset.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        Awaitable<List<MeshAsset>> CreateMeshAssetsAsync(IVgoStorage vgoStorage, IList<Material?>? materialList, CancellationToken cancellationToken);
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        UniTask<List<MeshAsset>> CreateMeshAssetsAsync(IVgoStorage vgoStorage, IList<Material?>? materialList, CancellationToken cancellationToken);
#else
        Task<List<MeshAsset>> CreateMeshAssetsAsync(IVgoStorage vgoStorage, IList<Material?>? materialList, CancellationToken cancellationToken);
#endif

        /// <summary>
        /// Create mesh assets.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="materialList">List of unity material.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>List of mesh asset.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        Awaitable<List<MeshAsset>> CreateMeshAssetsParallelAsync(IVgoStorage vgoStorage, IList<Material?>? materialList, CancellationToken cancellationToken);
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        UniTask<List<MeshAsset>> CreateMeshAssetsParallelAsync(IVgoStorage vgoStorage, IList<Material?>? materialList, CancellationToken cancellationToken);
#else
        Task<List<MeshAsset>> CreateMeshAssetsParallelAsync(IVgoStorage vgoStorage, IList<Material?>? materialList, CancellationToken cancellationToken);
#endif

        ///// <summary>
        ///// Create a mesh asset.
        ///// </summary>
        ///// <param name="vgoStorage">A vgo storage.</param>
        ///// <param name="meshIndex">The index of vgo mesh.</param>
        ///// <param name="unityMaterialList">List of unity material.</param>
        ///// <returns>A mesh asset.</returns>
        //MeshAsset CreateMeshAsset(in IVgoStorage vgoStorage, in int meshIndex, in IList<Material?>? unityMaterialList = null);

        ///// <summary>
        ///// Create a mesh asset.
        ///// </summary>
        ///// <param name="vgoStorage">A vgo storage.</param>
        ///// <param name="meshIndex">The index of vgo mesh.</param>
        ///// <param name="unityMaterialList">List of unity material.</param>
        ///// <param name="cancellationToken"></param>
        ///// <returns>A mesh asset.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        //Awaitable<MeshAsset> CreateMeshAssetAsync(IVgoStorage vgoStorage, int meshIndex, IList<Material?>? unityMaterialList, CancellationToken cancellationToken);
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        //UniTask<MeshAsset> CreateMeshAssetAsync(IVgoStorage vgoStorage, int meshIndex, IList<Material?>? unityMaterialList, CancellationToken cancellationToken);
#else
        //Task<MeshAsset> CreateMeshAssetAsync(IVgoStorage vgoStorage, int meshIndex, IList<Material?>? unityMaterialList, CancellationToken cancellationToken);
#endif

        #endregion
    }
}
