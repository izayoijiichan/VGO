// ----------------------------------------------------------------------
// @Namespace : UniVgo.Porters
// @Class     : VgoMaterialExporter
// ----------------------------------------------------------------------
namespace UniVgo.Porters
{
    using NewtonGltf;
    using System;
    using UnityEngine;

    /// <summary>
    /// VGO Material Exporter
    /// </summary>
    public class VgoMaterialExporter : IMaterialExporter
    {
        #region Properties

        /// <summary>The material porter store.</summary>
        public IMaterialPorterStore MaterialPorterStore { get; set; }

        /// <summary>A delegate of ExportTexture method.</summary>
        /// <remarks>for Export</remarks>
        public ExportTextureDelegate ExportTexture { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Create a glTF material.
        /// </summary>
        /// <param name="material">A unity material.</param>
        /// <returns>A glTF material.</returns>
        public GltfMaterial CreateGltfMaterial(Material material)
        {
            if (MaterialPorterStore == null)
            {
                throw new Exception();
            }

            if (ExportTexture == null)
            {
                throw new Exception();
            }

            IMaterialPorter materialPorter = MaterialPorterStore.GetPorterOrStandard(material.shader.name);

            materialPorter.ExportTexture = ExportTexture;

            GltfMaterial gltfMaterial = materialPorter.CreateGltfMaterial(material);

            return gltfMaterial;
        }

        #endregion
    }
}
