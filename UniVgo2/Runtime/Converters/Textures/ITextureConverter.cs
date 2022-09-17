// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Converters
// @Class     : ITextureConverter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Converters
{
    using NewtonVgo;
    using UnityEngine;

    /// <summary>
    /// Texture Converter Interface
    /// </summary>
    public interface ITextureConverter
    {
        #region Public Methods

        /// <summary>
        /// Get import texture.
        /// </summary>
        /// <param name="source">The source texture.</param>
        /// <param name="textureMapType">The texture map type.</param>
        /// <param name="metallicRoughness">The metallic roughness.</param>
        /// <returns></returns>
        Texture2D GetImportTexture(Texture2D source, VgoTextureMapType textureMapType, float metallicRoughness = -1.0f);

        /// <summary>
        /// Get export texture.
        /// </summary>
        /// <param name="source">The source texture.</param>
        /// <param name="textureMapType">The texture map type.</param>
        /// <param name="colorSpaceType">The color space type.</param>
        /// <param name="metallicSmoothness">The metallic smoothness.</param>
        /// <returns></returns>
        Texture2D GetExportTexture(Texture2D source, VgoTextureMapType textureMapType, VgoColorSpaceType colorSpaceType, float metallicSmoothness = -1.0f);

        #endregion
    }
}
