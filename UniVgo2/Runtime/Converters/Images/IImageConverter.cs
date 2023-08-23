// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Converters
// @Interfase : IImageConverter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Converters
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Image Converter Interface
    /// </summary>
    public interface IImageConverter
    {
        #region Methods

        /// <summary>
        /// Converts the specified image to a WebP image.
        /// </summary>
        /// <param name="imageBytes">A byte array of image.</param>
        /// <param name="imageType">The image type.</param>
        /// <param name="imageName">The image name.</param>
        /// <param name="flipVertical">Whether to flip the image vertically.</param>
        /// <returns>A byte array of WebP image.</returns>
#if UNITY_2021_2_OR_NEWER
        byte[] ConvertToWebp(in byte[] imageBytes, in ImageType imageType, in string? imageName = null, in bool flipVertical = false);
#else
        byte[] ConvertToWebp(in byte[] imageBytes, in ImageType imageType, string? imageName = null, bool flipVertical = false);
#endif

        /// <summary>
        /// Converts the specified image to a WebP image.
        /// </summary>
        /// <param name="imageBytes">A byte array of image.</param>
        /// <param name="imageType">The image type.</param>
        /// <param name="imageName">The image name.</param>
        /// <param name="flipVertical">Whether to flip the image vertically.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A byte array of WebP image.</returns>
        Task<byte[]> ConvertToWebpAsync(
            byte[] imageBytes,
            ImageType imageType,
            string? imageName,
            bool flipVertical,
            CancellationToken cancellationToken);

        /// <summary>
        /// Load the image.
        /// </summary>
        /// <param name="imageBytes">A byte array of image.</param>
        /// <param name="imageType">The image type.</param>
        /// <param name="flipVertical">Whether to flip the image vertically.</param>
        /// <returns>An image info.</returns>
        ImageInfo? LoadImage(in byte[] imageBytes, in ImageType imageType, in bool flipVertical);

        /// <summary>
        /// Load the image.
        /// </summary>
        /// <param name="imageBytes">A byte array of image.</param>
        /// <param name="imageType">The image type.</param>
        /// <param name="flipVertical">Whether to flip the image vertically.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>An image info.</returns>
        Task<ImageInfo?> LoadImageAsync(
            byte[] imageBytes,
            ImageType imageType,
            bool flipVertical,
            CancellationToken cancellationToken);

        /// <summary>
        /// Save the image data as a WebP image file.
        /// </summary>
        /// <param name="imageBytes">A byte array of image.</param>
        /// <param name="imageType">The image type.</param>
        /// <param name="filePath">Output file path.</param>
        /// <param name="fileName">Output file name without extension.</param>
        void SaveAsWebp(in byte[] imageBytes, in ImageType imageType, in string filePath, in string fileName);

        /// <summary>
        /// Save the image data as a WebP image file.
        /// </summary>
        /// <param name="imageBytes">A byte array of image.</param>
        /// <param name="imageType">The image type.</param>
        /// <param name="filePath">Output file path.</param>
        /// <param name="fileName">Output file name without extension.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns></returns>
        Task SaveAsWebpAsync(
            byte[] imageBytes,
            ImageType imageType,
            string filePath,
            string fileName,
            CancellationToken cancellationToken);

        #endregion
    }
}
