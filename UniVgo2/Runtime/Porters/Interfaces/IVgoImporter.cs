// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Interface : IVgoImporter
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
    using System.IO;
    using System.Threading;
    using UnityEngine;

    /// <summary>
    /// VGO Importer Interface
    /// </summary>
    public interface IVgoImporter
    {
        #region Methods (Sync)

        /// <summary>
        /// Load a 3D model from the specified file.
        /// </summary>
        /// <param name="vgoFilePath">The file path of the vgo.</param>
        /// <param name="vgkFilePath">The file path of the crypt key.</param>
        /// <returns>A vgo model asset.</returns>
        VgoModelAsset Load(in string vgoFilePath, in string? vgkFilePath = null);

        /// <summary>
        /// Load a 3D model from the specified bytes.
        /// </summary>
        /// <param name="vgoBytes">The vgo bytes.</param>
        /// <param name="vgkBytes">The vgk bytes.</param>
        /// <returns>A vgo model asset.</returns>
        VgoModelAsset Load(in byte[] vgoBytes, in byte[]? vgkBytes = null);

        /// <summary>
        /// Load a 3D model from the specified bytes.
        /// </summary>
        /// <param name="vgoStream">The vgo stream.</param>
        /// <param name="vgkStream">The vgk stream.</param>
        /// <returns>A vgo model asset.</returns>
        VgoModelAsset Load(in Stream vgoStream, in Stream? vgkStream = null);

        /// <summary>
        /// Load a 3D model from the specified vgo storage.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <returns>A vgo model asset.</returns>
        VgoModelAsset Load(in IVgoStorage vgoStorage);

        #endregion

#if UNITY_WEBGL
        //
#else
        #region Methods (Async)

        /// <summary>
        /// Load a 3D model from the specified file.
        /// </summary>
        /// <param name="vgoFilePath">The file path of the vgo.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A vgo model asset.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        Awaitable<VgoModelAsset> LoadAsync(string vgoFilePath, CancellationToken cancellationToken);
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        UniTask<VgoModelAsset> LoadAsync(string vgoFilePath, CancellationToken cancellationToken);
#else
        Task<VgoModelAsset> LoadAsync(string vgoFilePath, CancellationToken cancellationToken);
#endif

        /// <summary>
        /// Load a 3D model from the specified file.
        /// </summary>
        /// <param name="vgoFilePath">The file path of the vgo.</param>
        /// <param name="vgkFilePath">The file path of the crypt key.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A vgo model asset.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        Awaitable<VgoModelAsset> LoadAsync(string vgoFilePath, string vgkFilePath, CancellationToken cancellationToken);
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        UniTask<VgoModelAsset> LoadAsync(string vgoFilePath, string vgkFilePath, CancellationToken cancellationToken);
#else
        Task<VgoModelAsset> LoadAsync(string vgoFilePath, string vgkFilePath, CancellationToken cancellationToken);
#endif

        /// <summary>
        /// Load a 3D model from the specified bytes.
        /// </summary>
        /// <param name="vgoBytes">The vgo bytes.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A vgo model asset.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        Awaitable<VgoModelAsset> LoadAsync(byte[] vgoBytes, CancellationToken cancellationToken);
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        UniTask<VgoModelAsset> LoadAsync(byte[] vgoBytes, CancellationToken cancellationToken);
#else
        Task<VgoModelAsset> LoadAsync(byte[] vgoBytes, CancellationToken cancellationToken);
#endif

        /// <summary>
        /// Load a 3D model from the specified bytes.
        /// </summary>
        /// <param name="vgoBytes">The vgo bytes.</param>
        /// <param name="vgkBytes">The vgk bytes.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A vgo model asset.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        Awaitable<VgoModelAsset> LoadAsync(byte[] vgoBytes, byte[] vgkBytes, CancellationToken cancellationToken);
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        UniTask<VgoModelAsset> LoadAsync(byte[] vgoBytes, byte[] vgkBytes, CancellationToken cancellationToken);
#else
        Task<VgoModelAsset> LoadAsync(byte[] vgoBytes, byte[] vgkBytes, CancellationToken cancellationToken);
#endif

        /// <summary>
        /// Load a 3D model from the specified stream.
        /// </summary>
        /// <param name="vgoStream">The vgo stream.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A vgo model asset.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        Awaitable<VgoModelAsset> LoadAsync(Stream vgoStream, CancellationToken cancellationToken);
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        UniTask<VgoModelAsset> LoadAsync(Stream vgoStream, CancellationToken cancellationToken);
#else
        Task<VgoModelAsset> LoadAsync(Stream vgoStream, CancellationToken cancellationToken);
#endif

        /// <summary>
        /// Load a 3D model from the specified stream.
        /// </summary>
        /// <param name="vgoStream">The vgo stream.</param>
        /// <param name="vgkStream">The vgk stream.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A vgo model asset.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        Awaitable<VgoModelAsset> LoadAsync(Stream vgoStream, Stream vgkStream, CancellationToken cancellationToken);
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        UniTask<VgoModelAsset> LoadAsync(Stream vgoStream, Stream vgkStream, CancellationToken cancellationToken);
#else
        Task<VgoModelAsset> LoadAsync(Stream vgoStream, Stream vgkStream, CancellationToken cancellationToken);
#endif

        /// <summary>
        /// Load a 3D model from the specified vgo storage.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A vgo model asset.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        Awaitable<VgoModelAsset> LoadAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken);
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        UniTask<VgoModelAsset> LoadAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken);
#else
        Task<VgoModelAsset> LoadAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken);
#endif

        #endregion
#endif  // UNITY_WEBGL

        #region Methods (Extract)

        /// <summary>
        /// Extract a 3D model asset from the specified file.
        /// </summary>
        /// <param name="vgoFilePath">The file path of the vgo.</param>
        /// <param name="vgkFilePath">The file path of the vgk.</param>
        /// <returns>A vgo model asset.</returns>
        /// <remarks>for ScriptedImporter</remarks>
        VgoModelAsset Extract(in string vgoFilePath, in string? vgkFilePath = null);

        /// <summary>
        /// Extract a 3D model asset from the specified file.
        /// </summary>
        /// <param name="vgoFilePath">The file path of the vgo.</param>
        /// <param name="vgkFilePath">The file path of the vgk.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A vgo model asset.</returns>
        /// <remarks>for ScriptedImporter</remarks>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        Awaitable<VgoModelAsset> ExtractAsync(string vgoFilePath, string? vgkFilePath, CancellationToken cancellationToken);
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        UniTask<VgoModelAsset> ExtractAsync(string vgoFilePath, string? vgkFilePath, CancellationToken cancellationToken);
#else
        Task<VgoModelAsset> ExtractAsync(string vgoFilePath, string? vgkFilePath, CancellationToken cancellationToken);
#endif

        #endregion
    }
}
