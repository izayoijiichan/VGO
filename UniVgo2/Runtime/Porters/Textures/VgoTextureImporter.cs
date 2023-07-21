// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : VgoTextureImporter
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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using UnityEngine;
    using UniVgo2.Converters;

    /// <summary>
    /// VGO Texture Importer
    /// </summary>
    public partial class VgoTextureImporter : IVgoTextureImporter
    {
        #region Fields

        /// <summary>The texture converter.</summary>
        protected readonly ITextureConverter _TextureConverter;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of TextureImporter.
        /// </summary>
        public VgoTextureImporter()
        {
            _TextureConverter = new TextureConverter();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Create texture assets.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <returns>List of unity texture2D.</returns>
        public virtual List<Texture2D?> CreateTextureAssets(in IVgoStorage vgoStorage)
        {
            if ((vgoStorage.Layout.textures == null) || (vgoStorage.Layout.textures.Any() == false))
            {
                return new List<Texture2D?>(0);
            }

            var texture2dList = new List<Texture2D?>(vgoStorage.Layout.textures.Count);

            for (int textureIndex = 0; textureIndex < vgoStorage.Layout.textures.Count; textureIndex++)
            {
                VgoTexture? vgoTexture = vgoStorage.Layout.textures[textureIndex];

                if (vgoTexture is null)
                {
                    texture2dList.Add(null);
                }
                else
                {
                    try
                    {
                        Texture2D? texture2d = CreateTexture2D(vgoTexture, vgoStorage);

                        texture2dList.Add(texture2d);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogException(ex);

                        texture2dList.Add(null);
                    }
                }
            }

            return texture2dList;
        }

        /// <summary>
        /// Create texture assets.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>List of unity texture2D.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        public virtual async Awaitable<List<Texture2D?>> CreateTextureAssetsAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken)
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        public virtual async UniTask<List<Texture2D?>> CreateTextureAssetsAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken)
#else
        public virtual async Task<List<Texture2D?>> CreateTextureAssetsAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken)
#endif
        {
            if ((vgoStorage.Layout.textures == null) || (vgoStorage.Layout.textures.Any() == false))
            {
                return new List<Texture2D?>(0);
            }

            var texture2dList = new List<Texture2D?>(vgoStorage.Layout.textures.Count);

            for (int textureIndex = 0; textureIndex < vgoStorage.Layout.textures.Count; textureIndex++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                VgoTexture? vgoTexture = vgoStorage.Layout.textures[textureIndex];

                if (vgoTexture is null)
                {
                    texture2dList.Add(null);
                }
                else
                {
                    try
                    {
                        Texture2D? texture2d = await CreateTexture2DAsync(vgoTexture, vgoStorage, cancellationToken);

                        texture2dList.Add(texture2d);
                    }
                    catch (OperationCanceledException)
                    {
                        throw;
                    }
                    catch (Exception ex)
                    {
                        Debug.LogException(ex);

                        texture2dList.Add(null);
                    }
                }
            }

            return texture2dList;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Create a unity texture 2D.
        /// </summary>
        /// <param name="vgoTexture">A vgo texture.</param>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <returns>A unity texture 2D.</returns>
        protected virtual Texture2D? CreateTexture2D(in VgoTexture vgoTexture, in IVgoStorage vgoStorage)
        {
            if (vgoStorage.ResourceAccessors is null)
            {
                ThrowHelper.ThrowException();

                return null;
            }

            if (vgoTexture.source.IsInRangeOf(vgoStorage.ResourceAccessors) == false)
            {
                Debug.LogError($"{nameof(VgoTexture)}.{nameof(vgoTexture.source)}: {vgoTexture.source}");

                return null;
            }

            if (vgoTexture.dimensionType != TextureDimension.Tex2D)
            {
                Debug.LogError($"{nameof(VgoTexture)}.{nameof(vgoTexture.dimensionType)}: {vgoTexture.dimensionType}");

                return null;
            }

            Texture2D srcTexture2d;

            {
                byte[] imageBytes = vgoStorage.GetAccessorBytes(vgoTexture.source).ToArray();

                srcTexture2d = new Texture2D(width: 2, height: 2, TextureFormat.ARGB32, mipChain: false, linear: vgoTexture.IsLinear)
                {
                    name = vgoTexture.name
                };

                ImageConversion.LoadImage(srcTexture2d, imageBytes);
            }

            Texture2D texture2D = _TextureConverter.GetImportTexture(srcTexture2d, vgoTexture.mapType, vgoTexture.metallicRoughness);

            texture2D.filterMode = (UnityEngine.FilterMode)vgoTexture.filterMode;
            texture2D.wrapMode = (UnityEngine.TextureWrapMode)vgoTexture.wrapMode;
            texture2D.wrapModeU = (UnityEngine.TextureWrapMode)vgoTexture.wrapModeU;
            texture2D.wrapModeV = (UnityEngine.TextureWrapMode)vgoTexture.wrapModeV;

            texture2D.Apply();

            return texture2D;
        }

        /// <summary>
        /// Create a unity texture 2D.
        /// </summary>
        /// <param name="vgoTexture">A vgo texture.</param>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A unity texture 2D.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        protected virtual async Awaitable<Texture2D?> CreateTexture2DAsync(VgoTexture vgoTexture, IVgoStorage vgoStorage, CancellationToken cancellationToken)
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        protected virtual async UniTask<Texture2D?> CreateTexture2DAsync(VgoTexture vgoTexture, IVgoStorage vgoStorage, CancellationToken cancellationToken)
#else
        protected virtual async Task<Texture2D?> CreateTexture2DAsync(VgoTexture vgoTexture, IVgoStorage vgoStorage, CancellationToken cancellationToken)
#endif
        {
            if (vgoStorage.ResourceAccessors is null)
            {
                ThrowHelper.ThrowException();

                return null;
            }

            if (vgoTexture.source.IsInRangeOf(vgoStorage.ResourceAccessors) == false)
            {
                Debug.LogError($"{nameof(VgoTexture)}.{nameof(vgoTexture.source)}: {vgoTexture.source}");

                return null;
            }

            if (vgoTexture.dimensionType != TextureDimension.Tex2D)
            {
                Debug.LogError($"{nameof(VgoTexture)}.{nameof(vgoTexture.dimensionType)}: {vgoTexture.dimensionType}");

                return null;
            }

            cancellationToken.ThrowIfCancellationRequested();

            Texture2D srcTexture2d;

            {
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
                await Awaitable.BackgroundThreadAsync();

                //var sw = new System.Diagnostics.Stopwatch();

                //sw.Start();

                byte[] imageBytes = vgoStorage.GetAccessorBytes(vgoTexture.source).ToArray();

                //sw.Stop();

                //Debug.LogFormat("{0,2}: {1,5}ms", Thread.CurrentThread.ManagedThreadId, sw.ElapsedMilliseconds);

                await Awaitable.MainThreadAsync();
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
                await UniTask.SwitchToThreadPool();

                //var sw = new System.Diagnostics.Stopwatch();

                //sw.Start();

                byte[] imageBytes = vgoStorage.GetAccessorBytes(vgoTexture.source).ToArray();

                //sw.Stop();

                //Debug.LogFormat("{0,2}: {1,5}ms", Thread.CurrentThread.ManagedThreadId, sw.ElapsedMilliseconds);

                await UniTask.SwitchToMainThread();
#else
                //var sw = new System.Diagnostics.Stopwatch();

                //sw.Start();

                byte[] imageBytes = vgoStorage.GetAccessorBytes(vgoTexture.source).ToArray();

                //byte[] imageBytes = await Task.Run(() =>
                //{
                //    //var swt = new System.Diagnostics.Stopwatch();

                //    //swt.Start();

                //    var bytes = vgoStorage.GetAccessorBytes(vgoTexture.source).ToArray();

                //    //swt.Stop();

                //    //Debug.LogFormat("{0,2}: {1,5}ms", Thread.CurrentThread.ManagedThreadId, swt.ElapsedMilliseconds);

                //    return bytes;
                //});

                //sw.Stop();

                //Debug.LogFormat("{0,2}: {1,5}ms", Thread.CurrentThread.ManagedThreadId, sw.ElapsedMilliseconds);
#endif

                srcTexture2d = new Texture2D(width: 2, height: 2, TextureFormat.ARGB32, mipChain: false, linear: vgoTexture.IsLinear)
                {
                    name = vgoTexture.name
                };

                ImageConversion.LoadImage(srcTexture2d, imageBytes);
            }

            cancellationToken.ThrowIfCancellationRequested();

            Texture2D texture2D = _TextureConverter.GetImportTexture(srcTexture2d, vgoTexture.mapType, vgoTexture.metallicRoughness);

            texture2D.filterMode = (UnityEngine.FilterMode)vgoTexture.filterMode;
            texture2D.wrapMode = (UnityEngine.TextureWrapMode)vgoTexture.wrapMode;
            texture2D.wrapModeU = (UnityEngine.TextureWrapMode)vgoTexture.wrapModeU;
            texture2D.wrapModeV = (UnityEngine.TextureWrapMode)vgoTexture.wrapModeV;

            texture2D.Apply();

#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
            //await Awaitable.NextFrameAsync(cancellationToken);

            return texture2D;
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
            return await UniTask.FromResult(texture2D);
#else
            return await Task.FromResult(texture2D);
#endif
        }

        #endregion
    }
}
