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
    //
#endif

    using NewtonVgo;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using UnityEngine;
    using UniVgo2.Converters;

    /// <summary>
    /// VGO Texture Importer
    /// </summary>
    public partial class VgoTextureImporter : IVgoTextureImporter
    {
        #region Fields

        /// <summary>The image converter.</summary>
        protected IImageConverter? _ImageConverter;

        /// <summary>The texture converter.</summary>
        protected readonly ITextureConverter _TextureConverter;

        /// <summary>Whether to flip the WebP pixels vertically.</summary>
        protected readonly bool _WebpFlipVertical = true;

        #endregion

        #region Properties

        /// <summary>The image converter.</summary>
        protected IImageConverter ImageConverter => _ImageConverter ??= new ImageConverter();

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

                    continue;
                }

                try
                {
                    Texture2D? texture2d = CreateTexture2d(vgoTexture, vgoStorage);

                    texture2dList.Add(texture2d);
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);

                    texture2dList.Add(null);
                }
            }

            return texture2dList;
        }

        /// <summary>
        /// Create texture assets.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
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

                    continue;
                }

                try
                {
                    Texture2D? texture2d = await CreateTexture2dAsync(vgoTexture, vgoStorage, cancellationToken);

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

            return texture2dList;
        }

        /// <summary>
        /// Create texture assets.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>List of unity texture2D.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        public virtual async Awaitable<List<Texture2D?>> CreateTextureAssetsParallelAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken)
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        public virtual async UniTask<List<Texture2D?>> CreateTextureAssetsParallelAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken)
#else
        public virtual async Task<List<Texture2D?>> CreateTextureAssetsParallelAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken)
#endif
        {
            if ((vgoStorage.Layout.textures == null) || (vgoStorage.Layout.textures.Any() == false))
            {
                return new List<Texture2D?>(0);
            }

            List<ImageInfo?> imageInfoListForWebp = await CreateImageInfoListForWebpParallelAsync(vgoStorage, cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();

            var texture2dList = new List<Texture2D?>(vgoStorage.Layout.textures.Count);

            for (int textureIndex = 0; textureIndex < vgoStorage.Layout.textures.Count; textureIndex++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                VgoTexture? vgoTexture = vgoStorage.Layout.textures[textureIndex];

                if (vgoTexture is null)
                {
                    texture2dList.Add(null);

                    continue;
                }

                try
                {
                    if (vgoTexture.mimeType == MimeType.Image_WebP)
                    {
                        if (imageInfoListForWebp.TryGetValue(textureIndex, out ImageInfo? imageInfo) == false ||
                            imageInfo == null)
                        {
                            texture2dList.Add(null);

                            continue;
                        }

                        Texture2D? texture2D = CreateTexture2dInternal1(vgoTexture, imageInfo);

                        texture2dList.Add(texture2D);
                    }
                    else
                    {
                        Texture2D? texture2d = await CreateTexture2dAsync(vgoTexture, vgoStorage, cancellationToken);

                        texture2dList.Add(texture2d);
                    }
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
        protected virtual Texture2D? CreateTexture2d(in VgoTexture vgoTexture, in IVgoStorage vgoStorage)
        {
            if (vgoStorage.ResourceAccessors is null)
            {
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

            byte[] imageBytes = vgoStorage.GetAccessorBytes(vgoTexture.source).ToArray();

            if (vgoTexture.mimeType == MimeType.Image_WebP)
            {
                // @heavy
                ImageInfo? imageInfo = ImageConverter.LoadImage(imageBytes, ImageType.WebP, _WebpFlipVertical);

                if (imageInfo is null)
                {
                    return null;
                }

                return CreateTexture2dInternal1(vgoTexture, imageInfo);
            }
            else
            {
                var srcTexture2d = new Texture2D(width: 2, height: 2, TextureFormat.ARGB32, mipChain: false, linear: vgoTexture.IsLinear)
                {
                    name = vgoTexture.name
                };

                ImageConversion.LoadImage(srcTexture2d, imageBytes);

                return CreateTexture2dInternal2(vgoTexture, srcTexture2d);
            }
        }

        /// <summary>
        /// Create a unity texture 2D.
        /// </summary>
        /// <param name="vgoTexture">A vgo texture.</param>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A unity texture 2D.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        protected virtual async Awaitable<Texture2D?> CreateTexture2dAsync(VgoTexture vgoTexture, IVgoStorage vgoStorage, CancellationToken cancellationToken)
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        protected virtual async UniTask<Texture2D?> CreateTexture2dAsync(VgoTexture vgoTexture, IVgoStorage vgoStorage, CancellationToken cancellationToken)
#else
        protected virtual async Task<Texture2D?> CreateTexture2dAsync(VgoTexture vgoTexture, IVgoStorage vgoStorage, CancellationToken cancellationToken)
#endif
        {
            if (vgoTexture.mimeType == MimeType.Image_WebP)
            {
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
                await Awaitable.BackgroundThreadAsync();
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
                await UniTask.SwitchToThreadPool();
#endif
                // @heavy
                ImageInfo? imageInfo = await CreateImageInfoAsync(vgoTexture, vgoStorage, ImageType.WebP, _WebpFlipVertical, cancellationToken);

#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
                await Awaitable.MainThreadAsync();
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
                await UniTask.SwitchToMainThread();
#endif
                if (imageInfo is null)
                {
                    return null;
                }

                return CreateTexture2dInternal1(vgoTexture, imageInfo);
            }
            else
            {
                // @notice sync
                Texture2D? texture2D = CreateTexture2d(vgoTexture, vgoStorage);

#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
                //await Awaitable.NextFrameAsync(cancellationToken);

                return texture2D;
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
                return await UniTask.FromResult(texture2D);
#else
                return await Task.FromResult(texture2D);
#endif
            }
        }

        /// <summary>
        /// Create a unity texture 2D.
        /// </summary>
        /// <param name="vgoTexture">A vgo texture.</param>
        /// <param name="imageInfo">An image info.</param>
        /// <returns>A unity texture 2D.</returns>
        protected virtual Texture2D? CreateTexture2dInternal1(in VgoTexture vgoTexture, in ImageInfo imageInfo)
        {
            var srcTexture2d = new Texture2D(imageInfo.Width, imageInfo.Height, TextureFormat.ARGB32, mipChain: false, linear: vgoTexture.IsLinear)
            {
                name = vgoTexture.name
            };

            srcTexture2d.SetPixels32(imageInfo.Pixels);

            srcTexture2d.Apply();

            return CreateTexture2dInternal2(vgoTexture, srcTexture2d);
        }

        /// <summary>
        /// Create a unity texture 2D.
        /// </summary>
        /// <param name="vgoTexture">A vgo texture.</param>
        /// <param name="srcTexture2d"></param>
        /// <returns>A unity texture 2D.</returns>
        protected virtual Texture2D? CreateTexture2dInternal2(in VgoTexture vgoTexture, in Texture2D srcTexture2d)
        {
            Texture2D texture2D = _TextureConverter.GetImportTexture(srcTexture2d, vgoTexture.mapType, vgoTexture.metallicRoughness);

            texture2D.filterMode = (UnityEngine.FilterMode)vgoTexture.filterMode;
            texture2D.wrapMode = (UnityEngine.TextureWrapMode)vgoTexture.wrapMode;
            texture2D.wrapModeU = (UnityEngine.TextureWrapMode)vgoTexture.wrapModeU;
            texture2D.wrapModeV = (UnityEngine.TextureWrapMode)vgoTexture.wrapModeV;

            texture2D.Apply();

            return texture2D;
        }

        #endregion

        #region Protected Methods (Image Info)

        /// <summary>
        /// Create an image info.
        /// </summary>
        /// <param name="vgoTexture">A vgo texture.</param>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="imageType">An image type.</param>
        /// <param name="flipVertical">Whether to flip the image vertically.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>An image info.</returns>
        protected virtual async Task<ImageInfo?> CreateImageInfoAsync(
            VgoTexture vgoTexture,
            IVgoStorage vgoStorage,
            ImageType imageType,
            bool flipVertical,
            CancellationToken cancellationToken)
        {
            if (vgoStorage.ResourceAccessors is null)
            {
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

            byte[] imageBytes = vgoStorage.GetAccessorBytes(vgoTexture.source).ToArray();

            if (imageBytes.Any() == false)
            {
                return null;
            }

            try
            {
                return await ImageConverter.LoadImageAsync(imageBytes, imageType, flipVertical, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                return null;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);

                return null;
            }
        }

        /// <summary>
        /// Create a list of image info for WebP.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>List of image info.</returns>
        protected virtual async Task<List<ImageInfo?>> CreateImageInfoListForWebpParallelAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken)
        {
            if ((vgoStorage.Layout.textures == null) || (vgoStorage.Layout.textures.Any() == false))
            {
                return new List<ImageInfo?>(0);
            }

            var imageInfoDictionary = new ConcurrentDictionary<int, ImageInfo?>();

            var createImageInfoTasks = new List<Task>(vgoStorage.Layout.textures.Count);

            // @notice for parallel task
            if (_ImageConverter is null && vgoStorage.Layout.textures.Any(t => t?.mimeType == MimeType.Image_WebP))
            {
                _ImageConverter = new ImageConverter();
            }

            for (int textureIndex = 0; textureIndex < vgoStorage.Layout.textures.Count; textureIndex++)
            {
                int index = textureIndex;  // @impotant

                Task createImageInfoTask = Task.Run(async () =>
                {
                    VgoTexture? vgoTexture = vgoStorage.Layout.textures[index];

                    if (vgoTexture is null)
                    {
                        imageInfoDictionary.TryAdd(index, null);
                    }
                    else if (vgoTexture.mimeType == MimeType.Image_WebP)
                    {
                        // @heavy
                        ImageInfo? imageInfo = await CreateImageInfoAsync(vgoTexture, vgoStorage, ImageType.WebP, _WebpFlipVertical, cancellationToken);

                        imageInfoDictionary.TryAdd(index, imageInfo);
                    }
                    else
                    {
                        imageInfoDictionary.TryAdd(index, null);
                    }
                });

                createImageInfoTasks.Add(createImageInfoTask);
            }

            await Task.WhenAll(createImageInfoTasks);

            return imageInfoDictionary.Values.ToList();
        }

        #endregion
    }
}
