// ----------------------------------------------------------------------
// @Namespace : UniVgo.Converters
// @Class     : TextureConverter
// ----------------------------------------------------------------------
namespace UniVgo.Converters
{
    using UnityEngine;

    /// <summary>
    /// Texture Converter
    /// </summary>
    public class TextureConverter
    {
        /// <summary>
        /// Get import texture.
        /// </summary>
        /// <param name="source">The source texture.</param>
        /// <param name="textureType">The texture type.</param>
        /// <param name="metallicRoughness">The metallic roughness.</param>
        /// <returns></returns>
        public Texture2D GetImportTexture(Texture2D source, TextureType textureType, float metallicRoughness = -1.0f)
        {
            if (textureType == TextureType.NormalMap)
            {
                return new NormalMapConverter().GetImportTexture(source);
            }
            else if (textureType == TextureType.MetallicRoughnessMap)
            {
                return new MetallicRoughnessMapConverter(metallicRoughness).GetImportTexture(source);
            }
            else if (textureType == TextureType.OcclusionMap)
            {
                return new OcclusionMapConverter().GetImportTexture(source);
            }
            else
            {
                return source;
            }
        }

        /// <summary>
        /// Get export texture.
        /// </summary>
        /// <param name="source">The source texture.</param>
        /// <param name="textureType">The texture type.</param>
        /// <param name="colorSpaceType">The color space type.</param>
        /// <param name="metallicSmoothness">The metallic smoothness.</param>
        /// <returns></returns>
        public Texture2D GetExportTexture(Texture2D source, TextureType textureType, ColorSpaceType colorSpaceType, float metallicSmoothness = -1.0f)
        {
            if (textureType == TextureType.NormalMap)
            {
                return new NormalMapConverter().GetExportTexture(source);
            }
            else if (textureType == TextureType.MetallicRoughnessMap)
            {
                return new MetallicRoughnessMapConverter(metallicSmoothness).GetExportTexture(source);
            }
            else if (textureType == TextureType.OcclusionMap)
            {
                return new OcclusionMapConverter().GetExportTexture(source);
            }
            else
            {
                return CopyTexture2d(source, colorSpaceType);
            }
        }
        /// <summary>
        /// Copy Texture2D.
        /// </summary>
        /// <param name="source">The source texture.</param>
        /// <param name="colorSpaceType">The color space type.</param>
        /// <param name="converter">The converter.</param>
        /// <returns>The copied Texture2D.</returns>
        protected Texture2D CopyTexture2d(Texture2D source, ColorSpaceType colorSpaceType, Material converter = null)
        {
            RenderTextureReadWrite readWrite =
                (colorSpaceType == ColorSpaceType.Linear) ? RenderTextureReadWrite.Linear : RenderTextureReadWrite.sRGB;

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
    }
}
