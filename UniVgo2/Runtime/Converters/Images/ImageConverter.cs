// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Converters
// @Class     : ImageConverter
// ----------------------------------------------------------------------
#nullable enable
#if SIXLABORS_IMAGESHARP_2_OR_NEWER
namespace UniVgo2.Converters
{
    using NewtonVgo;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Formats;
    using SixLabors.ImageSharp.Formats.Jpeg;
    using SixLabors.ImageSharp.Formats.Png;
    using SixLabors.ImageSharp.Formats.Webp;
    using SixLabors.ImageSharp.PixelFormats;
    using SixLabors.ImageSharp.Processing;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using UnityEngine;

    /// <summary>
    /// Image Converter
    /// </summary>
    public class ImageConverter : IImageConverter
    {
        #region Fields

        /// <summary>The JPEG Decoder.</summary>
        protected readonly JpegDecoder _JpegDecoder;

        /// <summary>The PNG Decoder.</summary>
        protected readonly PngDecoder _PngDecoder;

        /// <summary>The WebP Decoder.</summary>
        protected readonly WebpDecoder _WebpDecoder;

        /// <summary>The WebP Encoder.</summary>
        protected readonly WebpEncoder _WebpEncoder;

        /// <summary>The WebP buffer default capacity.</summary>
        protected readonly int _WebpBufferDefaultCapacity = 1024 * 1024;  // 1MB;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of ImageConverter.
        /// </summary>
        public ImageConverter()
        {
            _JpegDecoder = new JpegDecoder();

            _PngDecoder = new PngDecoder();

            _WebpDecoder = new WebpDecoder();

            _WebpEncoder = new WebpEncoder
            {
                FileFormat = WebpFileFormatType.Lossless,
                Quality = 75,
                Method = WebpEncodingMethod.Default,
                UseAlphaCompression = true,
            };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Converts the specified image to a WebP image.
        /// </summary>
        /// <param name="imageBytes">A byte array of image.</param>
        /// <param name="imageType">The image type.</param>
        /// <param name="imageName">The image name.</param>
        /// <param name="flipVertical">Whether to flip the image vertically.</param>
        /// <returns>A byte array of WebP image.</returns>
#if UNITY_2021_2_OR_NEWER
        public byte[] ConvertToWebp(in byte[] imageBytes, in ImageType imageType, in string? imageName = null, in bool flipVertical = false)
#else
        public byte[] ConvertToWebp(in byte[] imageBytes, in ImageType imageType, string? imageName = null, bool flipVertical = false)
#endif
        {
            if (imageType == ImageType.WebP)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(nameof(imageType));
            }

            //var stopwatch = new System.Diagnostics.Stopwatch();

            //stopwatch.Start();

            using Image<Rgba32> image = LoadImage(imageBytes, imageType);

            //stopwatch.Stop();

            //Debug.LogFormat("Thread {0,2}: {1} decode png {2:#,0}ms", Thread.CurrentThread.ManagedThreadId, imageName, stopwatch.ElapsedMilliseconds);

            if (flipVertical)
            {
                image.Mutate(x => x.Flip(FlipMode.Vertical));
            }

            using MemoryStream memoryStream = new(_WebpBufferDefaultCapacity);

            //stopwatch.Restart();

            // @heavy
            _WebpEncoder.Encode(image, memoryStream);

            //stopwatch.Stop();

            //Debug.LogFormat("Thread {0,2}: {1} encode webp {2:#,0}ms", Thread.CurrentThread.ManagedThreadId, imageName, stopwatch.ElapsedMilliseconds);

            // @debug
            //image.SaveAsWebp(Path.Combine(Application.temporaryCachePath, $"{imageName}.webp"), _WebpEncoder);

            image.Dispose();

            byte[] webpBytes = memoryStream.ToArray();

            memoryStream.Dispose();

            return webpBytes;
        }

        /// <summary>
        /// Converts the specified image to a WebP image.
        /// </summary>
        /// <param name="imageBytes">A byte array of image.</param>
        /// <param name="imageType">The image type.</param>
        /// <param name="imageName">The image name.</param>
        /// <param name="flipVertical">Whether to flip the image vertically.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A byte array of WebP image.</returns>
        public async Task<byte[]> ConvertToWebpAsync(
            byte[] imageBytes,
            ImageType imageType,
            string? imageName,
            bool flipVertical,
            CancellationToken cancellationToken)
        {
            if (imageType == ImageType.WebP)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(nameof(imageType));
            }

            //var stopwatch = new System.Diagnostics.Stopwatch();

            //stopwatch.Start();

            using Image<Rgba32> image = LoadImage(imageBytes, imageType);

            //stopwatch.Stop();

            //Debug.LogFormat("Thread {0,2}: {1} decode png {2:#,0}ms", Thread.CurrentThread.ManagedThreadId, imageName, stopwatch.ElapsedMilliseconds);

            if (flipVertical)
            {
                image.Mutate(x => x.Flip(FlipMode.Vertical));
            }

            using MemoryStream memoryStream = new(_WebpBufferDefaultCapacity);

            //stopwatch.Restart();

            // @heavy
            await _WebpEncoder.EncodeAsync(image, memoryStream, cancellationToken);

            //stopwatch.Stop();

            //Debug.LogFormat("Thread {0,2}: {1} encode webp {2:#,0}ms", Thread.CurrentThread.ManagedThreadId, imageName, stopwatch.ElapsedMilliseconds);

            // @debug
            //await image.SaveAsWebpAsync(Path.Combine(Application.temporaryCachePath, $"{imageName}.webp"), _WebpEncoder, cancellationToken);

            image.Dispose();

            byte[] webpBytes = memoryStream.ToArray();

            memoryStream.Dispose();

            return webpBytes;
        }

        /// <summary>
        /// Load the image.
        /// </summary>
        /// <param name="imageBytes">A byte array of image.</param>
        /// <param name="imageType">The image type.</param>
        /// <param name="flipVertical">Whether to flip the image vertically.</param>
        /// <returns>An image info.</returns>
        public virtual ImageInfo? LoadImage(in byte[] imageBytes, in ImageType imageType, in bool flipVertical)
        {
            if (imageBytes.Any() == false)
            {
                return null;
            }

            //var stopwatch = new System.Diagnostics.Stopwatch();

            //stopwatch.Start();

            using Image<Rgba32> image = LoadImage(imageBytes, imageType);

            //stopwatch.Stop();

            //Debug.LogFormat("Thread {0,2}: {1:#,0}ms", Thread.CurrentThread.ManagedThreadId, stopwatch.ElapsedMilliseconds);

            return CreateImageInfo(image, flipVertical);
        }

        /// <summary>
        /// Load the image.
        /// </summary>
        /// <param name="imageBytes">A byte array of image.</param>
        /// <param name="imageType">The image type.</param>
        /// <param name="flipVertical">Whether to flip the image vertically.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>An image info.</returns>
        public virtual async Task<ImageInfo?> LoadImageAsync(
            byte[] imageBytes,
            ImageType imageType,
            bool flipVertical,
            CancellationToken cancellationToken)
        {
            if (imageBytes.Any() == false)
            {
                return await Task.FromResult(default(ImageInfo));
            }

            //var stopwatch = new System.Diagnostics.Stopwatch();

            //stopwatch.Start();

            using Image<Rgba32> image = await LoadImageAsync(imageBytes, imageType, cancellationToken);

            //stopwatch.Stop();

            //Debug.LogFormat("Thread {0,2}: {1:#,0}ms", Thread.CurrentThread.ManagedThreadId, stopwatch.ElapsedMilliseconds);

            cancellationToken.ThrowIfCancellationRequested();

            return CreateImageInfo(image, flipVertical);
        }

        /// <summary>
        /// Save the image data as a WebP image file.
        /// </summary>
        /// <param name="imageBytes">A byte array of image.</param>
        /// <param name="imageType">The image type.</param>
        /// <param name="filePath">Output file path.</param>
        /// <param name="fileName">Output file name without extension.</param>
        public virtual void SaveAsWebp(in byte[] imageBytes, in ImageType imageType, in string filePath, in string fileName)
        {
            if (imageType == ImageType.WebP)
            {
                string fileFullPath = Path.Combine(filePath, fileName);

                File.WriteAllBytes(fileFullPath, imageBytes);

                return;
            }

            //var stopwatch = new System.Diagnostics.Stopwatch();

            //stopwatch.Start();

            using Image<Rgba32> image = LoadImage(imageBytes, imageType);

            //stopwatch.Stop();

            //Debug.LogFormat("Thread {0,2}: {1} decode {2} {3:#,0}ms", Thread.CurrentThread.ManagedThreadId, fileName, imageType, stopwatch.ElapsedMilliseconds);

            // @unnecessary
            //image.Mutate(x => x.Flip(FlipMode.Vertical));

            //stopwatch.Restart();

            string fullPath = Path.Combine(filePath, fileName + ".webp");

            // @heavy
            image.SaveAsWebp(fullPath, _WebpEncoder);

            //stopwatch.Stop();

            //Debug.LogFormat("Thread {0,2}: {1} encode webp {2:#,0}ms", Thread.CurrentThread.ManagedThreadId, fileName, stopwatch.ElapsedMilliseconds);

            image.Dispose();
        }

        /// <summary>
        /// Save the image data as a WebP image file.
        /// </summary>
        /// <param name="imageBytes">A byte array of image.</param>
        /// <param name="imageType">The image type.</param>
        /// <param name="filePath">Output file path.</param>
        /// <param name="fileName">Output file name without extension.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns></returns>
        public virtual async Task SaveAsWebpAsync(
            byte[] imageBytes,
            ImageType imageType,
            string filePath,
            string fileName,
            CancellationToken cancellationToken)
        {
            if (imageType == ImageType.WebP)
            {
                string fileFullPath = Path.Combine(filePath, fileName);

                await File.WriteAllBytesAsync(fileFullPath, imageBytes, cancellationToken);

                return;
            }

            //var stopwatch = new System.Diagnostics.Stopwatch();

            //stopwatch.Start();

            using Image<Rgba32> image = await LoadImageAsync(imageBytes, imageType, cancellationToken);

            //stopwatch.Stop();

            //Debug.LogFormat("Thread {0,2}: {1} decode {2} {3:#,0}ms", Thread.CurrentThread.ManagedThreadId, fileName, imageType, stopwatch.ElapsedMilliseconds);

            // @unnecessary
            //image.Mutate(x => x.Flip(FlipMode.Vertical));

            //stopwatch.Restart();

            string fullPath = Path.Combine(filePath, fileName + ".webp");

            // @heavy
            await image.SaveAsWebpAsync(fullPath, _WebpEncoder, cancellationToken);

            //stopwatch.Stop();

            //Debug.LogFormat("Thread {0,2}: {1} encode webp {2:#,0}ms", Thread.CurrentThread.ManagedThreadId, fileName, stopwatch.ElapsedMilliseconds);

            image.Dispose();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Load the image.
        /// </summary>
        /// <param name="imageBytes">A byte array of image.</param>
        /// <param name="imageType">The image type.</param>
        /// <returns>An image.</returns>
        protected virtual Image<Rgba32> LoadImage(in byte[] imageBytes, in ImageType imageType)
        {
            if (TryGetImageDecoder(imageType, out IImageDecoder imageDecoder) == false)
            {
                ThrowHelper.ThrowNotSupportedException(nameof(imageType));
            }

            return Image.Load<Rgba32>(imageBytes, imageDecoder);

            //return Image.Load<Rgba32>(imageBytes.AsSpan(), imageDecoder);
        }

        /// <summary>
        /// Load the image.
        /// </summary>
        /// <param name="imageBytes">A byte array of image.</param>
        /// <param name="imageType">The image type.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>An image.</returns>
        protected virtual async Task<Image<Rgba32>> LoadImageAsync(byte[] imageBytes, ImageType imageType, CancellationToken cancellationToken)
        {
            if (TryGetImageDecoder(imageType, out IImageDecoder imageDecoder) == false)
            {
                ThrowHelper.ThrowNotSupportedException(nameof(imageType));
            }

            using MemoryStream imageStream = new(imageBytes, index: 0, count: imageBytes.Length, writable: false, publiclyVisible: true);

            return await Image.LoadAsync<Rgba32>(imageStream, imageDecoder, cancellationToken);
        }

        /// <summary>
        /// Get the decoder for the specified image type.
        /// </summary>
        /// <param name="imageType">The image type.</param>
        /// <param name="imageDecoder">The image decoder.</param>
        /// <returns>Returns true if successful, false otherwise.</returns>
        protected virtual bool TryGetImageDecoder(in ImageType imageType, out IImageDecoder imageDecoder)
        {
            if (imageType == ImageType.WebP)
            {
                imageDecoder = _WebpDecoder;

                return true;
            }
            else if (imageType == ImageType.PNG)
            {
                imageDecoder = _PngDecoder;

                return true;
            }
            else if (imageType == ImageType.JPEG)
            {
                imageDecoder = _JpegDecoder;

                return true;
            }
            else
            {
                imageDecoder = _PngDecoder;

                return false;
            }
        }

        /// <summary>
        /// Create an image info.
        /// </summary>
        /// <param name="image">An image.</param>
        /// <param name="flipVertical">Whether to flip the image vertically.</param>
        /// <returns>An image info.</returns>
        protected virtual ImageInfo CreateImageInfo(in Image<Rgba32> image, in bool flipVertical = false)
        {
            int width = image.Width;
            int height = image.Height;

            Color32[] pixels = new Color32[width * height];

            if (flipVertical)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Rgba32 rgba32 = image[x, y];

                        pixels[(height - 1 - y) * width + x] = new Color32(rgba32.R, rgba32.G, rgba32.B, rgba32.A);
                    }
                }
            }
            else
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Rgba32 rgba32 = image[x, y];

                        pixels[y * width + x] = new Color32(rgba32.R, rgba32.G, rgba32.B, rgba32.A);
                    }
                }
            }

            return new ImageInfo(width, height, pixels);
        }

        #endregion
    }
}
#else
namespace UniVgo2.Converters
{
    using NewtonVgo;
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using UnityEngine;
    
    /// <summary>
    /// Image Converter
    /// </summary>
    public class ImageConverter : IImageConverter
    {
        #region Constructors

        /// <summary>
        /// Create a new instance of ImageConverter.
        /// </summary>
        public ImageConverter()
        {
            //Debug.LogFormat("{0} requires SixLabors.ImageSharp", nameof(ImageConverter));
            Debug.LogWarningFormat("{0} requires SixLabors.ImageSharp", nameof(ImageConverter));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Converts the specified image to a WebP image.
        /// </summary>
        /// <param name="imageBytes">A byte array of image.</param>
        /// <param name="imageType">The image type.</param>
        /// <param name="imageName">The image name.</param>
        /// <param name="flipVertical">Whether to flip the image vertically.</param>
        /// <returns>A byte array of WebP image.</returns>
#if UNITY_2021_2_OR_NEWER
        public byte[] ConvertToWebp(in byte[] imageBytes, in ImageType imageType, in string? imageName = null, in bool flipVertical = false)
#else
        public byte[] ConvertToWebp(in byte[] imageBytes, in ImageType imageType, string? imageName = null, bool flipVertical = false)
#endif
        {
            if (imageType == ImageType.WebP)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(nameof(imageType));
            }
            else
            {
                ThrowHelper.ThrowNotSupportedException(string.Format("{0}.{1}()", nameof(ImageConverter), nameof(ConvertToWebp)));
            }

            return Array.Empty<byte>();
        }

        /// <summary>
        /// Converts the specified image to a WebP image.
        /// </summary>
        /// <param name="imageBytes">A byte array of image.</param>
        /// <param name="imageType">The image type.</param>
        /// <param name="imageName">The image name.</param>
        /// <param name="flipVertical">Whether to flip the image vertically.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A byte array of WebP image.</returns>
        public async Task<byte[]> ConvertToWebpAsync(
            byte[] imageBytes,
            ImageType imageType,
            string? imageName,
            bool flipVertical,
            CancellationToken cancellationToken)
        {
            if (imageType == ImageType.WebP)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(nameof(imageType));
            }
            else
            {
                ThrowHelper.ThrowNotSupportedException(string.Format("{0}.{1}()", nameof(ImageConverter), nameof(ConvertToWebpAsync)));
            }

            return await Task.FromResult(Array.Empty<byte>());
        }

        /// <summary>
        /// Load the image.
        /// </summary>
        /// <param name="imageBytes">A byte array of image.</param>
        /// <param name="imageType">The image type.</param>
        /// <param name="flipVertical">Whether to flip the image vertically.</param>
        /// <returns>An image info.</returns>
        public virtual ImageInfo? LoadImage(in byte[] imageBytes, in ImageType imageType, in bool flipVertical)
        {
            if (imageBytes.Any() == false)
            {
                return null;
            }

            //ThrowHelper.ThrowNotSupportedException(string.Format("{0}.{1}()", nameof(ImageConverter), nameof(LoadImage)));

            return null;
        }

        /// <summary>
        /// Load the image.
        /// </summary>
        /// <param name="imageBytes">A byte array of image.</param>
        /// <param name="imageType">The image type.</param>
        /// <param name="flipVertical">Whether to flip the image vertically.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>An image info.</returns>
        public virtual async Task<ImageInfo?> LoadImageAsync(
            byte[] imageBytes,
            ImageType imageType,
            bool flipVertical,
            CancellationToken cancellationToken)
        {
            if (imageBytes.Any() == false)
            {
                return await Task.FromResult(default(ImageInfo));
            }

            //ThrowHelper.ThrowNotSupportedException(string.Format("{0}.{1}()", nameof(ImageConverter), nameof(LoadImageAsync)));

            return await Task.FromResult(default(ImageInfo));
        }

        /// <summary>
        /// Save the image data as a WebP image file.
        /// </summary>
        /// <param name="imageBytes">A byte array of image.</param>
        /// <param name="imageType">The image type.</param>
        /// <param name="filePath">Output file path.</param>
        /// <param name="fileName">Output file name without extension.</param>
        public virtual void SaveAsWebp(in byte[] imageBytes, in ImageType imageType, in string filePath, in string fileName)
        {
            if (imageType == ImageType.WebP)
            {
                string fileFullPath = Path.Combine(filePath, fileName);

                File.WriteAllBytes(fileFullPath, imageBytes);

                return;
            }

            ThrowHelper.ThrowNotSupportedException(string.Format("{0}.{1}()", nameof(ImageConverter), nameof(SaveAsWebp)));
        }

        /// <summary>
        /// Save the image data as a WebP image file.
        /// </summary>
        /// <param name="imageBytes">A byte array of image.</param>
        /// <param name="imageType">The image type.</param>
        /// <param name="filePath">Output file path.</param>
        /// <param name="fileName">Output file name without extension.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns></returns>
        public virtual async Task SaveAsWebpAsync(
            byte[] imageBytes,
            ImageType imageType,
            string filePath,
            string fileName,
            CancellationToken cancellationToken)
        {
            if (imageType == ImageType.WebP)
            {
                string fileFullPath = Path.Combine(filePath, fileName);

#if UNITY_2021_2_OR_NEWER
                await File.WriteAllBytesAsync(fileFullPath, imageBytes, cancellationToken);
#else
                File.WriteAllBytes(fileFullPath, imageBytes);
#endif
                return;
            }

            ThrowHelper.ThrowNotSupportedException(string.Format("{0}.{1}()", nameof(ImageConverter), nameof(SaveAsWebpAsync)));

            await Task.CompletedTask;
        }

#endregion
    }
}
#endif
