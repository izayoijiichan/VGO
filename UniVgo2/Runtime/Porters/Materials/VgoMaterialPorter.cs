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
    using UnityEngine.Rendering;

    /// <summary>
    /// VGO Material Porter
    /// </summary>
    public class VgoMaterialPorter : IMaterialExporter, IMaterialImporter
    {
        #region Fields

        /// <summary>Type of render pipeline.</summary>
        protected RenderPipelineType? _RenderPipelineType;

        #endregion

        #region Properties

        /// <summary>The material porter store.</summary>
        public IMaterialPorterStore MaterialPorterStore { get; set; }

        /// <summary>A delegate of ExportTexture method.</summary>
        /// <remarks>for Export</remarks>
        public ExportTextureDelegate ExportTexture { get; set; }

        /// <summary>The shader store.</summary>
        /// <remarks>for Import</remarks>
        public IShaderStore ShaderStore { get; set; }

        /// <summary>Type of render pipeline.</summary>
        public RenderPipelineType RenderPipelineType
        {
            get
            {
                if (_RenderPipelineType == null)
                {
                    _RenderPipelineType = GetRenderPipelineType();
                }
                return (RenderPipelineType)_RenderPipelineType;
            }
        }

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

            IMaterialPorter materialPorter = MaterialPorterStore.GetPorterOrStandard(material.shader.name, RenderPipelineType);

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

            Shader shader = ShaderStore.GetShaderOrStandard(vgoMaterial, RenderPipelineType);

            IMaterialPorter materialPorter = MaterialPorterStore.GetPorterOrStandard(vgoMaterial, RenderPipelineType);

            materialPorter.AllTexture2dList = texture2dList;

            Material material = materialPorter.CreateMaterialAsset(vgoMaterial, shader);

#if UNITY_EDITOR
            material.hideFlags = HideFlags.DontUnloadUnusedAsset;
#endif

            return material;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Get type of render pipeline.
        /// </summary>
        /// <returns>Type of render pipeline.</returns>
        protected virtual RenderPipelineType GetRenderPipelineType()
        {
            RenderPipelineAsset renderPipelineAsset = GraphicsSettings.renderPipelineAsset;

            if (renderPipelineAsset == null)
            {
                return RenderPipelineType.BRP;
            }
            else if (string.IsNullOrEmpty(renderPipelineAsset.name))
            {
                return RenderPipelineType.BRP;
            }
            else if (
                renderPipelineAsset.name.StartsWith("UniversalRP-") ||
                renderPipelineAsset.name.StartsWith("Universal"))
            {
                return RenderPipelineType.URP;
            }
            else if (
                renderPipelineAsset.name.StartsWith("HDRP") ||
                renderPipelineAsset.name.StartsWith("HDRenderPipeline"))
            {
                return RenderPipelineType.HDRP;
            }

            return RenderPipelineType.BRP;
        }

        #endregion
    }
}
