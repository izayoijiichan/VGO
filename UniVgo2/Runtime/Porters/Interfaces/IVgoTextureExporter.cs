// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Interface : IVgoTextureExporter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using UnityEngine;

    #region Delegates

    /// <summary>The delegate to ExportTexture method.</summary>
    public delegate int ExportTextureDelegate(IVgoStorage vgoStorage, Texture srcTexture, in VgoTextureMapType textureMapType = VgoTextureMapType.Default, in VgoColorSpaceType colorSpaceType = VgoColorSpaceType.Srgb, in float metallicSmoothness = -1.0f);

    #endregion

    /// <summary>
    /// VGO Texture Exporter Interface
    /// </summary>
    public interface IVgoTextureExporter
    {
        #region Properties

        /// <summary>A delegate of ExportTexture method.</summary>
        ExportTextureDelegate ExportTextureDelegate { get; }

        /// <summary>The texture type.</summary>
        ImageType TextureType { get; set; }

        #endregion
    }
}
