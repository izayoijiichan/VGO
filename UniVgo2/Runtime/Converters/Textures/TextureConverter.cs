// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Converters
// @Class     : TextureConverter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Converters
{
    using NewtonVgo;
    using UnityEngine;

    /// <summary>
    /// Texture Converter
    /// </summary>
    public class TextureConverter : TextureConverterBase, ITextureConverter
    {
        #region Fields

        /// <summary>Normal Map Converter</summary>
        private readonly NormalMapConverter _NormalMapConverter = new NormalMapConverter();

        /// <summary>ORM Map Converter</summary>
        private readonly OrmMapConverter _OrmMapConverter = new OrmMapConverter();

        #endregion

        #region Public Methods

        /// <summary>
        /// Get import texture.
        /// </summary>
        /// <param name="source">The source texture.</param>
        /// <param name="textureMapType">The texture map type.</param>
        /// <param name="metallicRoughness">The metallic roughness.</param>
        /// <returns></returns>
#if UNITY_2021_2_OR_NEWER
        public virtual Texture GetImportTexture(in Texture source, in VgoTextureMapType textureMapType, in float metallicRoughness = -1.0f)
#else
        public virtual Texture GetImportTexture(in Texture source, in VgoTextureMapType textureMapType, float metallicRoughness = -1.0f)
#endif
        {
            if (textureMapType == VgoTextureMapType.NormalMap)
            {
                //return _NormalMapConverter.GetImportTexture(source);

                // @notice no conversion.
                // Unity's normal map is same with glTF's.
                return CopyTexture(source, VgoColorSpaceType.Linear, converter: null);
            }
            else if (
                (textureMapType == VgoTextureMapType.OcclusionMap) ||
                (textureMapType == VgoTextureMapType.MetallicRoughnessMap))
            {
                return _OrmMapConverter.GetImportTexture(source, VgoColorSpaceType.Linear, metallicRoughness);
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
        /// <param name="textureMapType">The texture map type.</param>
        /// <param name="colorSpaceType">The color space type.</param>
        /// <param name="metallicSmoothness">The metallic smoothness.</param>
        /// <returns></returns>
#if UNITY_2021_2_OR_NEWER
        public virtual Texture GetExportTexture(in Texture source, in VgoTextureMapType textureMapType, in VgoColorSpaceType colorSpaceType, in float metallicSmoothness = -1.0f)
#else
        public virtual Texture GetExportTexture(in Texture source, in VgoTextureMapType textureMapType, in VgoColorSpaceType colorSpaceType, float metallicSmoothness = -1.0f)
#endif
        {
            if (textureMapType == VgoTextureMapType.NormalMap)
            {
                return _NormalMapConverter.GetExportTexture(source, colorSpaceType);
            }
            else if (
                (textureMapType == VgoTextureMapType.OcclusionMap) ||
                (textureMapType == VgoTextureMapType.MetallicRoughnessMap))
            {
                return _OrmMapConverter.GetExportTexture(source, colorSpaceType, metallicSmoothness);
            }
            else
            {
                return CopyTexture(source, colorSpaceType);
            }
        }

        #endregion
    }
}
