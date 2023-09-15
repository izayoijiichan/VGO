// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Interface : IVgoMaterialExporter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using UnityEngine;

    /// <summary>
    /// VGO Material Exporter Interface
    /// </summary>
    public interface IVgoMaterialExporter
    {
        #region Properties

        /// <summary>The material porter store.</summary>
        IMaterialPorterStore? MaterialPorterStore { get; set; }

        /// <summary>A delegate of ExportTexture method.</summary>
        /// <remarks>for Export</remarks>
        ExportTextureDelegate? ExportTexture { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Create a vgo material.
        /// </summary>
        /// <param name="material">A unity material.</param>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <returns>A vgo material.</returns>
        VgoMaterial CreateVgoMaterial(in Material material, in IVgoStorage vgoStorage);

        #endregion
    }
}
