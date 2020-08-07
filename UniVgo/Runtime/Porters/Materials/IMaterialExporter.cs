// ----------------------------------------------------------------------
// @Namespace : UniVgo.Porters
// @Class     : IMaterialExporter
// ----------------------------------------------------------------------
namespace UniVgo.Porters
{
    using NewtonGltf;
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
        /// Create a glTF material.
        /// </summary>
        /// <param name="material">A unity material.</param>
        /// <returns>A glTF material.</returns>
        GltfMaterial CreateGltfMaterial(Material material);

        #endregion
    }
}
