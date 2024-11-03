// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Converters
// @Class     : TextureConverterBase
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Converters
{
    using NewtonVgo;
    using UnityEngine;

    /// <summary>
    /// Texture Converter Base
    /// </summary>
    public abstract class TextureConverterBase
    {
        #region Protected Methods

        /// <summary>
        /// Copy Texture.
        /// </summary>
        /// <param name="source">The source texture.</param>
        /// <param name="colorSpaceType">The color space type.</param>
        /// <param name="converter">The converter.</param>
        /// <returns>The copied Texture.</returns>
        protected virtual Texture CopyTexture(in Texture source, in VgoColorSpaceType colorSpaceType, in Material? converter = null)
        {
            if (source is Texture2D texture2D)
            {
                return CopyTexture2D(texture2D, colorSpaceType, converter);
            }
            else if (source is Texture2DArray texture2DArray)
            {
                return CopyTexture2DArray(texture2DArray, colorSpaceType, converter);
            }
            else if (source is Texture3D texture3D)
            {
                return CopyTexture3D(texture3D, colorSpaceType, converter);
            }
            else
            {
#if NET_STANDARD_2_1
                ThrowHelper.ThrowNotSupportedException($"{source.dimension}, {source.name}");
                return default;
#else
                throw new NotSupportedException($"{source.dimension}, {source.name}");
#endif
            }
        }

        /// <summary>
        /// Copy Texture2D.
        /// </summary>
        /// <param name="source">The source texture.</param>
        /// <param name="colorSpaceType">The color space type.</param>
        /// <param name="converter">The converter.</param>
        /// <returns>The copied Texture2D.</returns>
        protected virtual Texture2D CopyTexture2D(in Texture2D source, in VgoColorSpaceType colorSpaceType, in Material? converter = null)
        {
            RenderTextureReadWrite readWrite = (colorSpaceType == VgoColorSpaceType.Linear)
                ? RenderTextureReadWrite.Linear
                : RenderTextureReadWrite.sRGB;

            var renderTexture = new RenderTexture(source.width, source.height, depth: 0, RenderTextureFormat.ARGB32, readWrite);

            using (var scope = new ColorSpaceScope(readWrite))
            {
                if (converter == null)
                {
                    Graphics.Blit(source, renderTexture);
                }
                else
                {
                    Graphics.Blit(source, renderTexture, converter);
                }
            }

            var dest = new Texture2D(source.width, source.height, TextureFormat.ARGB32, mipChain: false, linear: (readWrite == RenderTextureReadWrite.Linear));

            dest.ReadPixels(new Rect(x: 0, y: 0, source.width, source.height), destX: 0, destY: 0, recalculateMipMaps: false);

            dest.name = source.name;
            dest.anisoLevel = source.anisoLevel;
            dest.filterMode = source.filterMode;
            dest.mipMapBias = source.mipMapBias;
            dest.wrapMode = source.wrapMode;
            dest.wrapModeU = source.wrapModeU;
            dest.wrapModeV = source.wrapModeV;
            dest.wrapModeW = source.wrapModeW;

            dest.Apply();

            RenderTexture.active = null;

            if (Application.isEditor)
            {
                UnityEngine.Object.DestroyImmediate(renderTexture);
            }
            else
            {
                UnityEngine.Object.Destroy(renderTexture);
            }

            return dest;
        }

        /// <summary>
        /// Copy Texture2DArray.
        /// </summary>
        /// <param name="source">The source texture.</param>
        /// <param name="colorSpaceType">The color space type.</param>
        /// <param name="converter">The converter.</param>
        /// <returns>The copied Texture2DArray.</returns>
        protected virtual Texture2DArray CopyTexture2DArray(in Texture2DArray source, in VgoColorSpaceType colorSpaceType, in Material? converter = null)
        {
            RenderTextureReadWrite readWrite = (colorSpaceType == VgoColorSpaceType.Linear)
                ? RenderTextureReadWrite.Linear
                : RenderTextureReadWrite.sRGB;

            var renderTexture = new RenderTexture(source.width, source.height, depth: source.depth, RenderTextureFormat.ARGB32, readWrite);

            using (var scope = new ColorSpaceScope(readWrite))
            {
                if (converter == null)
                {
                    Graphics.Blit(source, renderTexture);
                }
                else
                {
                    Graphics.Blit(source, renderTexture, converter);
                }
            }

            var dest = new Texture2DArray(source.width, source.height, source.depth, TextureFormat.ARGB32, mipChain: false);

            for (int arrayElement = 0; arrayElement < source.depth; arrayElement++)
            {
                Color32[] pixels = source.GetPixels32(arrayElement);

                dest.SetPixels32(pixels, arrayElement);
            }

            dest.name = source.name;
            dest.anisoLevel = source.anisoLevel;
            dest.filterMode = source.filterMode;
            dest.mipMapBias = source.mipMapBias;
            dest.wrapMode = source.wrapMode;
            dest.wrapModeU = source.wrapModeU;
            dest.wrapModeV = source.wrapModeV;
            dest.wrapModeW = source.wrapModeW;

            dest.Apply();

            RenderTexture.active = null;

            if (Application.isEditor)
            {
                UnityEngine.Object.DestroyImmediate(renderTexture);
            }
            else
            {
                UnityEngine.Object.Destroy(renderTexture);
            }

            return dest;
        }

        /// <summary>
        /// Copy Texture3D.
        /// </summary>
        /// <param name="source">The source texture.</param>
        /// <param name="colorSpaceType">The color space type.</param>
        /// <param name="converter">The converter.</param>
        /// <returns>The copied Texture3D.</returns>
        protected virtual Texture3D CopyTexture3D(in Texture3D source, in VgoColorSpaceType colorSpaceType, in Material? converter = null)
        {
            RenderTextureReadWrite readWrite = (colorSpaceType == VgoColorSpaceType.Linear)
                ? RenderTextureReadWrite.Linear
                : RenderTextureReadWrite.sRGB;

            var renderTexture = new RenderTexture(source.width, source.height, depth: source.depth, RenderTextureFormat.ARGB32, readWrite);

            using (var scope = new ColorSpaceScope(readWrite))
            {
                if (converter == null)
                {
                    Graphics.Blit(source, renderTexture);
                }
                else
                {
                    Graphics.Blit(source, renderTexture, converter);
                }
            }

            var dest = new Texture3D(source.width, source.height, source.depth, TextureFormat.ARGB32, mipChain: false);

            Color32[] pixels = source.GetPixels32();

            dest.SetPixels32(pixels);

            dest.name = source.name;
            dest.anisoLevel = source.anisoLevel;
            dest.filterMode = source.filterMode;
            dest.mipMapBias = source.mipMapBias;
            dest.wrapMode = source.wrapMode;
            dest.wrapModeU = source.wrapModeU;
            dest.wrapModeV = source.wrapModeV;
            dest.wrapModeW = source.wrapModeW;

            dest.Apply();

            RenderTexture.active = null;

            if (Application.isEditor)
            {
                UnityEngine.Object.DestroyImmediate(renderTexture);
            }
            else
            {
                UnityEngine.Object.Destroy(renderTexture);
            }

            return dest;
        }

        #endregion
    }
}
