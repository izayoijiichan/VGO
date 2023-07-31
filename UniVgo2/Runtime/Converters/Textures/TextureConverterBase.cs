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
        /// Copy Texture2D.
        /// </summary>
        /// <param name="source">The source texture.</param>
        /// <param name="colorSpaceType">The color space type.</param>
        /// <param name="converter">The converter.</param>
        /// <returns>The copied Texture2D.</returns>
        protected virtual Texture2D CopyTexture2d(in Texture2D source, in VgoColorSpaceType colorSpaceType, in Material? converter = null)
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

            Texture2D dest = new Texture2D(source.width, source.height, TextureFormat.ARGB32, mipChain: false, linear: (readWrite == RenderTextureReadWrite.Linear));

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

        #endregion
    }
}
