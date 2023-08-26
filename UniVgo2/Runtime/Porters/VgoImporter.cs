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
    using System.IO;
    using System.Threading;
    using UnityEngine;
    using UniVgo2.Porters;

    /// <summary>
    /// VGO Importer
    /// </summary>
    public partial class VgoImporter
    {
        #region Fields

        /// <summary>The vgo importer option.</summary>
        protected readonly VgoImporterOption _Option;

        /// <summary>The material importer.</summary>
        protected readonly IVgoMaterialImporter _MaterialImporter;

        /// <summary>The mesh importer.</summary>
        protected readonly IVgoMeshImporter _MeshImporter;

        /// <summary>The particle system importer.</summary>
        protected readonly IVgoParticleSystemImporter _ParticleSystemImporter;

        /// <summary>The texture importer.</summary>
        protected readonly IVgoTextureImporter _TextureImporter;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of VgoImporter.
        /// </summary>
        public VgoImporter() : this(new VgoImporterOption()) { }

        /// <summary>
        /// Create a new instance of VgoImporter with option.
        /// </summary>
        /// <param name="option">The vgo importer option.</param>
        public VgoImporter(in VgoImporterOption option)
        {
            _Option = option;

            _MaterialImporter = new VgoMaterialPorter()
            {
                MaterialPorterStore = new VgoMaterialPorterStore(),
                ShaderStore = new ShaderStore(),
            };

            _MeshImporter = new VgoMeshImporter(option.MeshImporterOption);

            _ParticleSystemImporter = new VgoParticleSystemImporter();

            _TextureImporter = new VgoTextureImporter();
        }

        #endregion

        #region Public Methods (Sync)

        /// <summary>
        /// Load a 3D model from the specified file.
        /// </summary>
        /// <param name="vgoFilePath">The file path of the vgo.</param>
        /// <param name="vgkFilePath">The file path of the crypt key.</param>
        /// <returns>A vgo model asset.</returns>
        public virtual VgoModelAsset Load(in string vgoFilePath, in string? vgkFilePath = null)
        {
            var vgoStorage = new VgoStorage();

            vgoStorage.ParseVgo(vgoFilePath, vgkFilePath);

            return LoadInternal(vgoStorage);
        }

        /// <summary>
        /// Load a 3D model from the specified bytes.
        /// </summary>
        /// <param name="vgoBytes">The vgo bytes.</param>
        /// <param name="vgkBytes">The vgk bytes.</param>
        /// <returns>A vgo model asset.</returns>
        public virtual VgoModelAsset Load(in byte[] vgoBytes, in byte[]? vgkBytes = null)
        {
            var vgoStorage = new VgoStorage();

            vgoStorage.ParseVgo(vgoBytes, vgkBytes);

            return LoadInternal(vgoStorage);
        }

        /// <summary>
        /// Load a 3D model from the specified bytes.
        /// </summary>
        /// <param name="vgoStream">The vgo stream.</param>
        /// <param name="vgkStream">The vgk stream.</param>
        /// <returns>A vgo model asset.</returns>
        public virtual VgoModelAsset Load(in Stream vgoStream, in Stream? vgkStream = null)
        {
            var vgoStorage = new VgoStorage();

            vgoStorage.ParseVgo(vgoStream, vgkStream);

            return LoadInternal(vgoStorage);
        }

        /// <summary>
        /// Load a 3D model from the specified vgo storage.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <returns>A vgo model asset.</returns>
        public virtual VgoModelAsset Load(in IVgoStorage vgoStorage)
        {
            return LoadInternal(vgoStorage);
        }

        #endregion

#if UNITY_WEBGL
        //
#else
        #region Public Methods (Async)

        /// <summary>
        /// Load a 3D model from the specified file.
        /// </summary>
        /// <param name="vgoFilePath">The file path of the vgo.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A vgo model asset.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        public virtual async Awaitable<VgoModelAsset> LoadAsync(string vgoFilePath, CancellationToken cancellationToken)
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        public virtual async UniTask<VgoModelAsset> LoadAsync(string vgoFilePath, CancellationToken cancellationToken)
#else
        public virtual async Task<VgoModelAsset> LoadAsync(string vgoFilePath, CancellationToken cancellationToken)
#endif
        {
            var vgoStorage = new VgoStorage();

#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
            await Awaitable.BackgroundThreadAsync();
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
            await UniTask.SwitchToThreadPool();
#endif
            await vgoStorage.ParseVgoAsync(vgoFilePath, vgkFilePath: null, cancellationToken);

#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
            await Awaitable.MainThreadAsync();
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
            await UniTask.SwitchToMainThread();
#endif
            return await LoadInternalAsync(vgoStorage, cancellationToken);
        }

        /// <summary>
        /// Load a 3D model from the specified file.
        /// </summary>
        /// <param name="vgoFilePath">The file path of the vgo.</param>
        /// <param name="vgkFilePath">The file path of the crypt key.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A vgo model asset.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        public virtual async Awaitable<VgoModelAsset> LoadAsync(string vgoFilePath, string vgkFilePath, CancellationToken cancellationToken)
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        public virtual async UniTask<VgoModelAsset> LoadAsync(string vgoFilePath, string vgkFilePath, CancellationToken cancellationToken)
#else
        public virtual async Task<VgoModelAsset> LoadAsync(string vgoFilePath, string vgkFilePath, CancellationToken cancellationToken)
#endif
        {
            var vgoStorage = new VgoStorage();

#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
            await Awaitable.BackgroundThreadAsync();
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
            await UniTask.SwitchToThreadPool();
#endif
            await vgoStorage.ParseVgoAsync(vgoFilePath, vgkFilePath, cancellationToken);

#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
            await Awaitable.MainThreadAsync();
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
            await UniTask.SwitchToMainThread();
#endif
            return await LoadInternalAsync(vgoStorage, cancellationToken);
        }

        /// <summary>
        /// Load a 3D model from the specified bytes.
        /// </summary>
        /// <param name="vgoBytes">The vgo bytes.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A vgo model asset.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        public virtual async Awaitable<VgoModelAsset> LoadAsync(byte[] vgoBytes, CancellationToken cancellationToken)
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        public virtual async UniTask<VgoModelAsset> LoadAsync(byte[] vgoBytes, CancellationToken cancellationToken)
#else
        public virtual async Task<VgoModelAsset> LoadAsync(byte[] vgoBytes, CancellationToken cancellationToken)
#endif
        {
            var vgoStorage = new VgoStorage();

#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
            await Awaitable.BackgroundThreadAsync();
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
            await UniTask.SwitchToThreadPool();
#endif
            await vgoStorage.ParseVgoAsync(vgoBytes, vgkBytes: null, cancellationToken);

#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
            await Awaitable.MainThreadAsync();
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
            await UniTask.SwitchToMainThread();
#endif
            return await LoadInternalAsync(vgoStorage, cancellationToken);
        }

        /// <summary>
        /// Load a 3D model from the specified bytes.
        /// </summary>
        /// <param name="vgoBytes">The vgo bytes.</param>
        /// <param name="vgkBytes">The vgk bytes.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A vgo model asset.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        public virtual async Awaitable<VgoModelAsset> LoadAsync(byte[] vgoBytes, byte[] vgkBytes, CancellationToken cancellationToken)
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        public virtual async UniTask<VgoModelAsset> LoadAsync(byte[] vgoBytes, byte[] vgkBytes, CancellationToken cancellationToken)
#else
        public virtual async Task<VgoModelAsset> LoadAsync(byte[] vgoBytes, byte[] vgkBytes, CancellationToken cancellationToken)
#endif
        {
            var vgoStorage = new VgoStorage();

#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
            await Awaitable.BackgroundThreadAsync();
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
            await UniTask.SwitchToThreadPool();
#endif
            await vgoStorage.ParseVgoAsync(vgoBytes, vgkBytes, cancellationToken);

#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
            await Awaitable.MainThreadAsync();
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
            await UniTask.SwitchToMainThread();
#endif
            return await LoadInternalAsync(vgoStorage, cancellationToken);
        }

        /// <summary>
        /// Load a 3D model from the specified stream.
        /// </summary>
        /// <param name="vgoStream">The vgo stream.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A vgo model asset.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        public virtual async Awaitable<VgoModelAsset> LoadAsync(Stream vgoStream, CancellationToken cancellationToken)
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        public virtual async UniTask<VgoModelAsset> LoadAsync(Stream vgoStream, CancellationToken cancellationToken)
#else
        public virtual async Task<VgoModelAsset> LoadAsync(Stream vgoStream, CancellationToken cancellationToken)
#endif
        {
            var vgoStorage = new VgoStorage();

#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
            await Awaitable.BackgroundThreadAsync();
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
            await UniTask.SwitchToThreadPool();
#endif
            await vgoStorage.ParseVgoAsync(vgoStream, vgkStream: null, cancellationToken);

#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
            await Awaitable.MainThreadAsync();
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
            await UniTask.SwitchToMainThread();
#endif
            return await LoadInternalAsync(vgoStorage, cancellationToken);
        }

        /// <summary>
        /// Load a 3D model from the specified stream.
        /// </summary>
        /// <param name="vgoStream">The vgo stream.</param>
        /// <param name="vgkStream">The vgk stream.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A vgo model asset.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        public virtual async Awaitable<VgoModelAsset> LoadAsync(Stream vgoStream, Stream vgkStream, CancellationToken cancellationToken)
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        public virtual async UniTask<VgoModelAsset> LoadAsync(Stream vgoStream, Stream vgkStream, CancellationToken cancellationToken)
#else
        public virtual async Task<VgoModelAsset> LoadAsync(Stream vgoStream, Stream vgkStream, CancellationToken cancellationToken)
#endif
        {
            var vgoStorage = new VgoStorage();

#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
            await Awaitable.BackgroundThreadAsync();
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
            await UniTask.SwitchToThreadPool();
#endif
            await vgoStorage.ParseVgoAsync(vgoStream, vgkStream, cancellationToken);

#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
            await Awaitable.MainThreadAsync();
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
            await UniTask.SwitchToMainThread();
#endif
            return await LoadInternalAsync(vgoStorage, cancellationToken);
        }

        /// <summary>
        /// Load a 3D model from the specified vgo storage.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A vgo model asset.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        public virtual async Awaitable<VgoModelAsset> LoadAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken)
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        public virtual async UniTask<VgoModelAsset> LoadAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken)
#else
        public virtual async Task<VgoModelAsset> LoadAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken)
#endif
        {
            return await LoadInternalAsync(vgoStorage, cancellationToken);
        }

        #endregion
#endif  // UNITY_WEBGL

        #region Public Methods (Extract)

        /// <summary>
        /// Extract a 3D model asset from the specified file.
        /// </summary>
        /// <param name="vgoFilePath">The file path of the vgo.</param>
        /// <param name="vgkFilePath">The file path of the vgk.</param>
        /// <returns>A vgo model asset.</returns>
        /// <remarks>for ScriptedImporter</remarks>
        public virtual VgoModelAsset Extract(in string vgoFilePath, in string? vgkFilePath = null)
        {
            var vgoStorage = new VgoStorage();

            vgoStorage.ParseVgo(vgoFilePath, vgkFilePath);

            return ExtractInternal(vgoStorage);
        }

        /// <summary>
        /// Extract a 3D model asset from the specified file.
        /// </summary>
        /// <param name="vgoFilePath">The file path of the vgo.</param>
        /// <param name="vgkFilePath">The file path of the vgk.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A vgo model asset.</returns>
        /// <remarks>for ScriptedImporter</remarks>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        public virtual async Awaitable<VgoModelAsset> ExtractAsync(string vgoFilePath, string? vgkFilePath, CancellationToken cancellationToken)
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        public virtual async UniTask<VgoModelAsset> ExtractAsync(string vgoFilePath, string? vgkFilePath, CancellationToken cancellationToken)
#else
        public virtual async Task<VgoModelAsset> ExtractAsync(string vgoFilePath, string? vgkFilePath, CancellationToken cancellationToken)
#endif
        {
            var vgoStorage = new VgoStorage();

#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
            await Awaitable.BackgroundThreadAsync();
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
            await UniTask.SwitchToThreadPool();
#endif
            await vgoStorage.ParseVgoAsync(vgoFilePath, vgkFilePath, cancellationToken);

#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
            await Awaitable.MainThreadAsync();
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
            await UniTask.SwitchToMainThread();
#endif
            return await ExtractInternalAsync(vgoStorage, cancellationToken);
        }

        #endregion

        #region Public Methods (Helper)

        /// <summary>
        /// Reflect VGO skybox to Camera skybox.
        /// </summary>
        /// <param name="camera">A scene main camera.</param>
        /// <param name="vgoModelAsset">A vgo model asset.</param>
        public virtual void ReflectSkybox(Camera camera, VgoModelAsset vgoModelAsset)
        {
            vgoModelAsset.ReflectSkybox(camera);
        }

        #endregion
    }
}
