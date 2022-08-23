// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : UnlitMaterialPorter
// ----------------------------------------------------------------------
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using UnityEngine;

#if VRMC_VRMSHADERS_0_85_OR_NEWER
    using UniGLTF.UniUnlit;
#elif VRMC_VRMSHADERS_0_72_OR_NEWER
    using UniUnlitUtil = UniGLTF.UniUnlit.Utils;
#else
    using UniUnlitUtil = UniGLTF.UniUnlit.Utils;
#endif

    /// <summary>
    /// Unlit Material Porter
    /// </summary>
    public class UnlitMaterialPorter : AbstractMaterialPorterBase
    {
        #region Constructors

        /// <summary>
        /// Create a new instance of UnlitMaterialPorter.
        /// </summary>
        public UnlitMaterialPorter() : base() { }

        #endregion

        #region Enums

        /// <summary>Blend Mode</summary>
        protected enum BlendMode
        {
            /// <summary>Opaque</summary>
            Opaque,
            /// <summary>Cutout</summary>
            Cutout,
            /// <summary>Fade</summary>
            Fade,
            /// <summary>Transparent</summary>
            Transparent
        }

        #endregion

        #region Public Methods (Export)

        /// <summary>
        /// Create a vgo material.
        /// </summary>
        /// <param name="material">A unlit material.</param>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <returns>vgo material</returns>
        public override VgoMaterial CreateVgoMaterial(Material material, IVgoStorage vgoStorage)
        {
            VgoMaterial vgoMaterial = new VgoMaterial()
            {
                name = material.name,
                shaderName = material.shader.name,
                renderQueue = material.renderQueue,
                isUnlit = true,
            };

            // Properties
            ExportProperty(vgoMaterial, material, UniUnlitUtil.PropNameColor, VgoMaterialPropertyType.Color4);
            ExportProperty(vgoMaterial, material, UniUnlitUtil.PropNameCutoff, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, UniUnlitUtil.PropNameCullMode, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, UniUnlitUtil.PropNameBlendMode, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, UniUnlitUtil.PropNameVColBlendMode, VgoMaterialPropertyType.Int);

            ExportProperty(vgoMaterial, material, UniUnlitUtil.PropNameSrcBlend, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, UniUnlitUtil.PropNameDstBlend, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, UniUnlitUtil.PropNameZWrite, VgoMaterialPropertyType.Int);

            // Textures
            ExportTextureProperty(vgoStorage, vgoMaterial, material, UniUnlitUtil.PropNameMainTex, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);

            // Tags
            ExportTag(vgoMaterial, material, UniUnlitUtil.TagRenderTypeKey);

            // Keywords
            ExportKeyword(vgoMaterial, material, UniUnlitUtil.KeywordAlphaTestOn);
            ExportKeyword(vgoMaterial, material, UniUnlitUtil.KeywordAlphaBlendOn);
            ExportKeyword(vgoMaterial, material, UniUnlitUtil.KeywordVertexColMul);

            return vgoMaterial;
        }

        #endregion

        #region Public Methods (Import)

        ///// <summary>
        ///// Create a unlit material.
        ///// </summary>
        ///// <param name="vgoMaterial">A vgo material.</param>
        ///// <param name="shader">A unlit shader.</param>
        ///// <param name="allTexture2dList">List of all texture 2D.</param>
        ///// <returns>A unlit material.</returns>
        //public override Material CreateMaterialAsset(VgoMaterial vgoMaterial, Shader shader, List<Texture2D> allTexture2dList)
        //{
        //    Material material = base.CreateMaterialAsset(vgoMaterial, shader, allTexture2dList);

        //    if (material.shader.name == ShaderName.UniGLTF_UniUnlit)
        //    {
        //        UniUnlitUtil.ValidateProperties(material, true);

        //        return material;
        //    }

        //    if (vgoMaterial.tagMap.ContainsKey(UniUnlitUtil.TagRenderTypeKey))
        //    {
        //        string renderType = vgoMaterial.tagMap[UniUnlitUtil.TagRenderTypeKey];

        //        switch (renderType)
        //        {
        //            case UniUnlitUtil.TagRenderTypeValueTransparent:
        //                material.SetInt(UniUnlitUtil.PropNameBlendMode, (int)BlendMode.Fade);
        //                material.SetInt(UniUnlitUtil.PropNameSrcBlend, (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        //                material.SetInt(UniUnlitUtil.PropNameDstBlend, (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        //                material.SetInt(UniUnlitUtil.PropNameZWrite, 0);
        //                material.DisableKeyword(UniUnlitUtil.KeywordAlphaTestOn);
        //                material.EnableKeyword(UniUnlitUtil.KeywordAlphaBlendOn);
        //                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
        //                break;

        //            case UniUnlitUtil.TagRenderTypeValueTransparentCutout:
        //                material.SetInt(UniUnlitUtil.PropNameBlendMode, (int)BlendMode.Cutout);
        //                material.SetInt(UniUnlitUtil.PropNameSrcBlend, (int)UnityEngine.Rendering.BlendMode.One);
        //                material.SetInt(UniUnlitUtil.PropNameDstBlend, (int)UnityEngine.Rendering.BlendMode.Zero);
        //                material.SetInt(UniUnlitUtil.PropNameZWrite, 1);
        //                material.EnableKeyword(UniUnlitUtil.KeywordAlphaTestOn);
        //                material.DisableKeyword(UniUnlitUtil.KeywordAlphaBlendOn);
        //                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;
        //                break;

        //            case UniUnlitUtil.TagRenderTypeValueOpaque:
        //            default:
        //                material.SetInt(UniUnlitUtil.PropNameBlendMode, (int)BlendMode.Opaque);
        //                material.SetInt(UniUnlitUtil.PropNameSrcBlend, (int)UnityEngine.Rendering.BlendMode.One);
        //                material.SetInt(UniUnlitUtil.PropNameDstBlend, (int)UnityEngine.Rendering.BlendMode.Zero);
        //                material.SetInt(UniUnlitUtil.PropNameZWrite, 1);
        //                material.DisableKeyword(UniUnlitUtil.KeywordAlphaTestOn);
        //                material.DisableKeyword(UniUnlitUtil.KeywordAlphaBlendOn);
        //                material.renderQueue = -1;
        //                break;
        //        }
        //    }

        //    return material;
        //}

        #endregion
    }
}
