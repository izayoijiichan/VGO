// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : IMaterialExporter
// ----------------------------------------------------------------------
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using UnityEngine;

    /// <summary>
    /// Material Exporter Interface
    /// </summary>
    public interface IMaterialExporter
    {
        #region Properties

        /// <summary>The material porter store.</summary>
        IMaterialPorterStore MaterialPorterStore { get; set; }

        /// <summary>A delegate of ExportTexture method.</summary>
        /// <remarks>for Export</remarks>
        ExportTextureDelegate ExportTexture { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Create a vgo material.
        /// </summary>
        /// <param name="material">A unity material.</param>
        /// <returns>A vgo material.</returns>
        VgoMaterial CreateVgoMaterial(Material material);

        #endregion
    }
}
