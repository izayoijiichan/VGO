// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : VgoMaterialPorter
// ----------------------------------------------------------------------
#nullable enable
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
    public class VgoMaterialPorter : IVgoMaterialExporter, IVgoMaterialImporter
    {
        #region Fields

        /// <summary>Type of render pipeline.</summary>
        protected RenderPipelineType? _RenderPipelineType;

        #endregion

        #region Properties

        /// <summary>The material porter store.</summary>
        public IMaterialPorterStore? MaterialPorterStore { get; set; }

        /// <summary>A delegate of ExportTexture method.</summary>
        /// <remarks>for Export</remarks>
        public ExportTextureDelegate? ExportTexture { get; set; }

        /// <summary>The shader store.</summary>
        /// <remarks>for Import</remarks>
        public IShaderStore? ShaderStore { get; set; }

        /// <summary>Type of render pipeline.</summary>
        public RenderPipelineType RenderPipelineType
        {
            get
            {
                _RenderPipelineType ??= GetRenderPipelineType();

                return (RenderPipelineType)_RenderPipelineType;
            }
        }

        #endregion

        #region Public Methods (for Export)

        /// <summary>
        /// Create a vgo material.
        /// </summary>
        /// <param name="material">A unity material.</param>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <returns>A vgo material.</returns>
        public VgoMaterial CreateVgoMaterial(in Material material, in IVgoStorage vgoStorage)
        {
            if (MaterialPorterStore == null)
            {
#if NET_STANDARD_2_1
                ThrowHelper.ThrowException();
#else
                throw new Exception();
#endif
            }

            if (ExportTexture == null)
            {
                ThrowHelper.ThrowException();
            }

            IMaterialPorter materialPorter = MaterialPorterStore.GetPorterOrStandard(material.shader.name, RenderPipelineType);

            materialPorter.ExportTexture = ExportTexture;

            VgoMaterial vgoMaterial = materialPorter.CreateVgoMaterial(material, vgoStorage);

            return vgoMaterial;
        }

        #endregion

        #region Public Methods (for Import)

        /// <summary>
        /// Create a material.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="textureList">List of Texture.</param>
        /// <returns>A unity material.</returns>
        public virtual Material CreateMaterialAsset(in VgoMaterial vgoMaterial, in List<Texture?> textureList)
        {
            if (MaterialPorterStore == null)
            {
#if NET_STANDARD_2_1
                ThrowHelper.ThrowException();
#else
                throw new Exception();
#endif
            }

            if (ShaderStore == null)
            {
#if NET_STANDARD_2_1
                ThrowHelper.ThrowException();
#else
                throw new Exception();
#endif
            }

            Shader shader = ShaderStore.GetShaderOrStandard(vgoMaterial, RenderPipelineType);

            //IMaterialPorter materialPorter = MaterialPorterStore.GetPorterOrStandard(vgoMaterial, RenderPipelineType);
            //IMaterialPorter materialPorter = MaterialPorterStore.GetPorterOrStandard(shader.name, RenderPipelineType);
            IMaterialPorter materialPorter = MaterialPorterStore.GetPorterOrStandard(shader.name, vgoMaterial, RenderPipelineType);

            Material material = materialPorter.CreateMaterialAsset(vgoMaterial, shader, textureList);

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
#if UNITY_6000_0_OR_NEWER
            RenderPipelineAsset renderPipelineAsset = GraphicsSettings.defaultRenderPipeline;
#else
            RenderPipelineAsset renderPipelineAsset = GraphicsSettings.renderPipelineAsset;
#endif

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
