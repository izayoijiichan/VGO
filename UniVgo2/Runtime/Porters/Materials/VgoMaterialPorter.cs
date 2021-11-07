// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : VgoMaterialPorter
// ----------------------------------------------------------------------
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// VGO Material Porter
    /// </summary>
    public class VgoMaterialPorter : IMaterialExporter, IMaterialImporter
    {
        #region Properties

        /// <summary>The material porter store.</summary>
        public IMaterialPorterStore MaterialPorterStore { get; set; }

        /// <summary>A delegate of ExportTexture method.</summary>
        /// <remarks>for Export</remarks>
        public ExportTextureDelegate ExportTexture { get; set; }

        /// <summary>The shader store.</summary>
        /// <remarks>for Import</remarks>
        public IShaderStore ShaderStore { get; set; }

        #endregion

        #region Public Methods (for Export)

        /// <summary>
        /// Create a vgo material.
        /// </summary>
        /// <param name="material">A unity material.</param>
        /// <returns>A vgo material.</returns>
        public VgoMaterial CreateVgoMaterial(Material material)
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

            VgoMaterial vgoMaterial = materialPorter.CreateVgoMaterial(material);

            return vgoMaterial;
        }

        #endregion

        #region Public Methods (for Import)

        /// <summary>
        /// Create a material.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="texture2dList">List of Texture2D.</param>
        /// <returns>A unity material.</returns>
        public virtual Material CreateMaterialAsset(VgoMaterial vgoMaterial, List<Texture2D> texture2dList)
        {
            if (MaterialPorterStore == null)
            {
                throw new Exception();
            }

            if (ShaderStore == null)
            {
                throw new Exception();
            }

            Shader shader = ShaderStore.GetShaderOrStandard(vgoMaterial);

            IMaterialPorter materialPorter = MaterialPorterStore.GetPorterOrStandard(vgoMaterial);

            materialPorter.AllTexture2dList = texture2dList;

            Material material = materialPorter.CreateMaterialAsset(vgoMaterial, shader);

#if UNITY_EDITOR
            material.hideFlags = HideFlags.DontUnloadUnusedAsset;
#endif

            return material;
        }

        #endregion
    }
}
