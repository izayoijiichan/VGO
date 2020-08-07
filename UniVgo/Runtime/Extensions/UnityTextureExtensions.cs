// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : UnityTextureExtensions
// ----------------------------------------------------------------------
namespace UniVgo
{
    using System;
    using UnityEngine;
    using VgoGltf;

    /// <summary>
    /// Unity Texture Extensions
    /// </summary>
    public static class UnityTextureExtensions
    {
        /// <summary>
        /// Convert UnityEngine.FilterMode to GltfTextureMagFilterMode.
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static GltfTextureMagFilterMode ToGltfMagMode(this FilterMode mode)
        {
            switch (mode)
            {
                case FilterMode.Bilinear:
                case FilterMode.Trilinear:
                    return GltfTextureMagFilterMode.LINEAR;

                case FilterMode.Point:
                    return GltfTextureMagFilterMode.NEAREST;

                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Convert UnityEngine.FilterMode to GltfTextureMinFilterMode.
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static GltfTextureMinFilterMode ToGltfMinMode(this FilterMode mode)
        {
            switch (mode)
            {
                case FilterMode.Bilinear:
                case FilterMode.Trilinear:
                    return GltfTextureMinFilterMode.LINEAR;

                case FilterMode.Point:
                    return GltfTextureMinFilterMode.NEAREST;

                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Convert UnityEngine.TextureWrapMode to GltfTextureWrapMode.
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static GltfTextureWrapMode ToGltfMode(this TextureWrapMode mode)
        {
            switch (mode)
            {
                case TextureWrapMode.Clamp:
                    return GltfTextureWrapMode.CLAMP_TO_EDGE;

                case TextureWrapMode.Repeat:
                    return GltfTextureWrapMode.REPEAT;

                case TextureWrapMode.Mirror:
                    return GltfTextureWrapMode.MIRRORED_REPEAT;

                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Convert GltfTextureMagFilterMode to UnityEngine.FilterMode.
        /// </summary>
        /// <param name="filterMode"></param>
        /// <returns></returns>
        public static FilterMode ToUnityMode(this GltfTextureMagFilterMode filterMode)
        {
            switch (filterMode)
            {
                case GltfTextureMagFilterMode.NEAREST:
                    return FilterMode.Point;

                case GltfTextureMagFilterMode.NONE:
                case GltfTextureMagFilterMode.LINEAR:
                    return FilterMode.Bilinear;

                default:
                    throw new NotImplementedException();

            }
        }

        /// <summary>
        /// Convert GltfTextureMinFilterMode to UnityEngine.FilterMode.
        /// </summary>
        /// <param name="filterMode"></param>
        /// <returns></returns>
        public static FilterMode ToUnityMode(this GltfTextureMinFilterMode filterMode)
        {
            switch (filterMode)
            {
                case GltfTextureMinFilterMode.NEAREST:
                case GltfTextureMinFilterMode.NEAREST_MIPMAP_LINEAR:
                case GltfTextureMinFilterMode.NEAREST_MIPMAP_NEAREST:
                    return FilterMode.Point;

                case GltfTextureMinFilterMode.NONE:
                case GltfTextureMinFilterMode.LINEAR:
                case GltfTextureMinFilterMode.LINEAR_MIPMAP_NEAREST:
                    return FilterMode.Bilinear;

                case GltfTextureMinFilterMode.LINEAR_MIPMAP_LINEAR:
                    return FilterMode.Trilinear;

                default:
                    throw new NotImplementedException();

            }
        }

        /// <summary>
        /// Convert GltfTextureWrapMode to UnityEngine.TextureWrapMode.
        /// </summary>
        /// <param name="wrapMode"></param>
        /// <returns></returns>
        public static TextureWrapMode ToUnityMode(this GltfTextureWrapMode wrapMode)
        {
            switch (wrapMode)
            {
                case GltfTextureWrapMode.NONE:
                    return TextureWrapMode.Repeat;

                case GltfTextureWrapMode.CLAMP_TO_EDGE:
                    return TextureWrapMode.Clamp;

                case GltfTextureWrapMode.REPEAT:
                    return TextureWrapMode.Repeat;

                case GltfTextureWrapMode.MIRRORED_REPEAT:
                    return TextureWrapMode.Mirror;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
