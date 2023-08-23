// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : ImageInfo
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using UnityEngine;

    /// <summary>
    /// Image Info
    /// </summary>
    public class ImageInfo
    {
        #region Fields

        /// <summary>Image width.</summary>
        private readonly int _Width;

        /// <summary>Image height.</summary>
        private readonly int _Height;

        /// <summary>An array of pixel.</summary>
        private readonly Color32[] _Pixels;

        #endregion

        #region Properties

        /// <summary>Image width.</summary>
        public int Width => _Width;

        /// <summary>Image height.</summary>
        public int Height => _Height;

        /// <summary>An array of pixel.</summary>
        public Color32[] Pixels => _Pixels;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of ImageInfo.
        /// </summary>
        /// <param name="width">The image width.</param>
        /// <param name="height">The image height.</param>
        /// <param name="pixels">An array of pixel.</param>
        public ImageInfo(int width, int height, Color32[] pixels)
        {
            _Width = width;

            _Height = height;

            _Pixels = pixels;
        }

        #endregion
    }
}
