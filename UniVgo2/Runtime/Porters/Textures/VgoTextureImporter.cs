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
        /// <returns>List of unity texture.</returns>
        public virtual List<Texture?> CreateTextureAssets(in IVgoStorage vgoStorage)
        {
            if ((vgoStorage.Layout.textures == null) || (vgoStorage.Layout.textures.Any() == false))
            {
                return new List<Texture?>(0);
            }

            var textureList = new List<Texture?>(vgoStorage.Layout.textures.Count);

            for (int textureIndex = 0; textureIndex < vgoStorage.Layout.textures.Count; textureIndex++)
            {
                VgoTexture? vgoTexture = vgoStorage.Layout.textures[textureIndex];

                if (vgoTexture is null)
                {
                    textureList.Add(null);

                    continue;
                }

                try
                {
                    Texture? texture = CreateTexture(vgoTexture, vgoStorage);

                    textureList.Add(texture);
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);

                    textureList.Add(null);
                }
            }

            return textureList;
        }

        /// <summary>
        /// Create texture assets.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>List of unity texture.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        public virtual async Awaitable<List<Texture?>> CreateTextureAssetsAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken)
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        public virtual async UniTask<List<Texture?>> CreateTextureAssetsAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken)
#else
        public virtual async Task<List<Texture?>> CreateTextureAssetsAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken)
#endif
        {
            if ((vgoStorage.Layout.textures == null) || (vgoStorage.Layout.textures.Any() == false))
            {
                return new List<Texture?>(0);
            }

            var textureList = new List<Texture?>(vgoStorage.Layout.textures.Count);

            for (int textureIndex = 0; textureIndex < vgoStorage.Layout.textures.Count; textureIndex++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                VgoTexture? vgoTexture = vgoStorage.Layout.textures[textureIndex];

                if (vgoTexture is null)
                {
                    textureList.Add(null);

                    continue;
                }

                try
                {
                    Texture? texture = await CreateTextureAsync(vgoTexture, vgoStorage, cancellationToken);

                    textureList.Add(texture);
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);

                    textureList.Add(null);
                }
            }

            return textureList;
        }

        /// <summary>
        /// Create texture assets.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>List of unity texture.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        public virtual async Awaitable<List<Texture?>> CreateTextureAssetsParallelAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken)
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        public virtual async UniTask<List<Texture?>> CreateTextureAssetsParallelAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken)
#else
        public virtual async Task<List<Texture?>> CreateTextureAssetsParallelAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken)
#endif
        {
            if ((vgoStorage.Layout.textures == null) || (vgoStorage.Layout.textures.Any() == false))
            {
                return new List<Texture?>(0);
            }

            List<ImageInfo?> imageInfoListForWebp = await CreateImageInfoListForWebpParallelAsync(vgoStorage, cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();

            var textureList = new List<Texture?>(vgoStorage.Layout.textures.Count);

            for (int textureIndex = 0; textureIndex < vgoStorage.Layout.textures.Count; textureIndex++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                VgoTexture? vgoTexture = vgoStorage.Layout.textures[textureIndex];

                if (vgoTexture is null)
                {
                    textureList.Add(null);

                    continue;
                }

                try
                {
                    if (vgoTexture.mimeType == MimeType.Image_WebP)
                    {
                        if (imageInfoListForWebp.TryGetValue(textureIndex, out ImageInfo? imageInfo) == false ||
                            imageInfo == null)
                        {
                            textureList.Add(null);

                            continue;
                        }

                        Texture? texture = CreateTextureInternal1(vgoTexture, imageInfo);

                        textureList.Add(texture);
                    }
                    else
                    {
                        Texture? texture = await CreateTextureAsync(vgoTexture, vgoStorage, cancellationToken);

                        textureList.Add(texture);
                    }
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);

                    textureList.Add(null);
                }
            }

            return textureList;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Create a unity texture.
        /// </summary>
        /// <param name="vgoTexture">A vgo texture.</param>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <returns>A unity texture.</returns>
        protected virtual Texture? CreateTexture(in VgoTexture vgoTexture, in IVgoStorage vgoStorage)
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

            //if (vgoTexture.dimensionType != TextureDimension.Tex2D)
            //{
            //    Debug.LogError($"{nameof(VgoTexture)}.{nameof(vgoTexture.dimensionType)}: {vgoTexture.dimensionType}");

            //    return null;
            //}

            byte[] imageBytes = vgoStorage.GetResourceDataAsByteArray(vgoTexture.source);

            if (imageBytes.Any() == false)
            {
                return null;
            }

            if (vgoTexture.mimeType == MimeType.Image_WebP)
            {
                // @heavy
                ImageInfo? imageInfo = ImageConverter.LoadImage(imageBytes, ImageType.WebP, _WebpFlipVertical);

                if (imageInfo is null)
                {
                    return null;
                }

                return CreateTextureInternal1(vgoTexture, imageInfo);
            }
            else
            {
                if (vgoTexture.dimensionType == TextureDimension.Tex2D)
                {
                    var srcTexture2d = new Texture2D(width: 2, height: 2, TextureFormat.ARGB32, mipChain: false, linear: vgoTexture.IsLinear)
                    {
                        name = vgoTexture.name
                    };

                    ImageConversion.LoadImage(srcTexture2d, imageBytes);

                    return CreateTextureInternal2(vgoTexture, srcTexture2d);
                }
                else if (vgoTexture.dimensionType == TextureDimension.Tex2DArray)
                {
                    //var srcTexture2dArray = new Texture2DArray(vgoTexture.width, vgoTexture.height, vgoTexture.depth, TextureFormat.ARGB32, mipChain: false, linear: vgoTexture.IsLinear)
                    //{
                    //    name = vgoTexture.name
                    //};

                    //ImageConversion.LoadImage(srcTexture2dArray, imageBytes);

                    //return CreateTextureInternal2(vgoTexture, srcTexture2dArray);
                }
                else if (vgoTexture.dimensionType == TextureDimension.Tex3D)
                {
                    //var srcTexture3d = new Texture3D(vgoTexture.width, vgoTexture.height, vgoTexture.depth, TextureFormat.ARGB32, mipChain: false)
                    //{
                    //    name = vgoTexture.name
                    //};

                    //ImageConversion.LoadImage(srcTexture3d, imageBytes);

                    //return CreateTextureInternal2(vgoTexture, srcTexture3d);
                }

                return null;
            }
        }

        /// <summary>
        /// Create a unity texture.
        /// </summary>
        /// <param name="vgoTexture">A vgo texture.</param>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A unity texture.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        protected virtual async Awaitable<Texture?> CreateTextureAsync(VgoTexture vgoTexture, IVgoStorage vgoStorage, CancellationToken cancellationToken)
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        protected virtual async UniTask<Texture?> CreateTextureAsync(VgoTexture vgoTexture, IVgoStorage vgoStorage, CancellationToken cancellationToken)
#else
        protected virtual async Task<Texture?> CreateTextureAsync(VgoTexture vgoTexture, IVgoStorage vgoStorage, CancellationToken cancellationToken)
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

                return CreateTextureInternal1(vgoTexture, imageInfo);
            }
            else
            {
                // @notice sync
                Texture? texture = CreateTexture(vgoTexture, vgoStorage);

#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
                //await Awaitable.NextFrameAsync(cancellationToken);

                return texture;
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
                return await UniTask.FromResult(texture);
#else
                return await Task.FromResult(texture);
#endif
            }
        }

        /// <summary>
        /// Create a unity texture.
        /// </summary>
        /// <param name="vgoTexture">A vgo texture.</param>
        /// <param name="imageInfo">An image info.</param>
        /// <returns>A unity texture.</returns>
        protected virtual Texture? CreateTextureInternal1(in VgoTexture vgoTexture, in ImageInfo imageInfo)
        {
            if (vgoTexture.dimensionType == TextureDimension.Tex2D)
            {
                var srcTexture2d = new Texture2D(imageInfo.Width, imageInfo.Height, TextureFormat.ARGB32, mipChain: false, linear: vgoTexture.IsLinear)
                {
                    name = vgoTexture.name
                };

                srcTexture2d.SetPixels32(imageInfo.Pixels);

                srcTexture2d.Apply();

                return CreateTextureInternal2(vgoTexture, srcTexture2d);
            }
            else if (vgoTexture.dimensionType == TextureDimension.Tex2DArray)
            {
                //var srcTexture2dArray = new Texture2DArray(imageInfo.Width, imageInfo.Height, vgoTexture.depth, TextureFormat.ARGB32, mipChain: false, linear: vgoTexture.IsLinear)
                //{
                //    name = vgoTexture.name
                //};

                //for (int elementIndex = 0; elementIndex < vgoTexture.depth; elementIndex++)
                //{
                //    //srcTexture2dArray.SetPixels32(imageInfo.Pixels, elementIndex);

                //    srcTexture2dArray.Apply();
                //}

                //return CreateTextureInternal2(vgoTexture, srcTexture2dArray);
            }
            else if (vgoTexture.dimensionType == TextureDimension.Tex3D)
            {
                //var srcTexture3d = new Texture3D(imageInfo.Width, imageInfo.Height, vgoTexture.depth, TextureFormat.ARGB32, mipChain: false)
                //{
                //    name = vgoTexture.name
                //};

                //srcTexture3d.SetPixels32(imageInfo.Pixels);

                //srcTexture3d.Apply();

                //return CreateTextureInternal2(vgoTexture, srcTexture3d);
            }

            return null;
        }

        /// <summary>
        /// Create a unity texture.
        /// </summary>
        /// <param name="vgoTexture">A vgo texture.</param>
        /// <param name="srcTexture"></param>
        /// <returns>A unity texture.</returns>
        protected virtual Texture? CreateTextureInternal2(in VgoTexture vgoTexture, in Texture srcTexture)
        {
            if (srcTexture is Texture2D)
            {
                // OK
            }
            else if (srcTexture is Texture2DArray)
            {
                // OK
            }
            else if (srcTexture is Texture3D)
            {
                // OK
            }
            else
            {
                return null;
            }

            Texture texture = _TextureConverter.GetImportTexture(srcTexture, vgoTexture.mapType, vgoTexture.metallicRoughness);

            texture.filterMode = (UnityEngine.FilterMode)vgoTexture.filterMode;
            texture.wrapMode = (UnityEngine.TextureWrapMode)vgoTexture.wrapMode;
            texture.wrapModeU = (UnityEngine.TextureWrapMode)vgoTexture.wrapModeU;
            texture.wrapModeV = (UnityEngine.TextureWrapMode)vgoTexture.wrapModeV;

            if (texture is Texture2D texture2D)
            {
                texture2D.Apply();

                return texture2D;
            }
            else if (texture is Texture2DArray texture2DArray)
            {
                texture2DArray.Apply();

                return texture2DArray;
            }
            else if (texture is Texture3D texture3D)
            {
                texture3D.Apply();

                return texture3D;
            }

            return null;
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

            byte[] imageBytes = vgoStorage.GetResourceDataAsByteArray(vgoTexture.source);

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
                int index = textureIndex;  // @important

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
