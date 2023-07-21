﻿// ----------------------------------------------------------------------
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
        Texture2D GetImportTexture(in Texture2D source, in VgoTextureMapType textureMapType, in float metallicRoughness = -1.0f);

        /// <summary>
        /// Get export texture.
        /// </summary>
        /// <param name="source">The source texture.</param>
        /// <param name="textureMapType">The texture map type.</param>
        /// <param name="colorSpaceType">The color space type.</param>
        /// <param name="metallicSmoothness">The metallic smoothness.</param>
        /// <returns></returns>
        Texture2D GetExportTexture(in Texture2D source, in VgoTextureMapType textureMapType, in VgoColorSpaceType colorSpaceType, in float metallicSmoothness = -1.0f);

        #endregion
    }
}
