// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : IVgoMeshImporter
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
    /// VGO Texture Importer Interface
    /// </summary>
    public interface IVgoTextureImporter
    {
        #region Public Methods

        /// <summary>
        /// Create texture assets.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <returns>List of unity texture2D.</returns>
        List<Texture2D?> CreateTextureAssets(in IVgoStorage vgoStorage);

        /// <summary>
        /// Create texture assets.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>List of unity texture2D.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        Awaitable<List<Texture2D?>> CreateTextureAssetsAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken);
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        UniTask<List<Texture2D?>> CreateTextureAssetsAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken);
#else
        Task<List<Texture2D?>> CreateTextureAssetsAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken);
#endif

        /// <summary>
        /// Create texture assets.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>List of unity texture2D.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        Awaitable<List<Texture2D?>> CreateTextureAssetsParallelAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken);
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        UniTask<List<Texture2D?>> CreateTextureAssetsParallelAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken);
#else
        Task<List<Texture2D?>> CreateTextureAssetsParallelAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken);
#endif

        #endregion
    }
}
