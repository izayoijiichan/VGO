// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : UnlitMaterialPorter
// ----------------------------------------------------------------------
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using UnityEngine;

    /// <summary>
    /// Unlit Material Importer
    /// </summary>
    public class UnlitMaterialPorter : MaterialPorterBase
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
        /// <returns>vgo material</returns>
        public override VgoMaterial CreateGltfMaterial(Material material)
        {
            VgoMaterial vgoMaterial = new VgoMaterial()
            {
                name = material.name,
                shaderName = material.shader.name,
                renderQueue = material.renderQueue,
                isUnlit = true,
            };

            // Properties
            ExportProperty(vgoMaterial, material, UniGLTF.UniUnlit.Utils.PropNameColor, VgoMaterialPropertyType.Color4);
            ExportProperty(vgoMaterial, material, UniGLTF.UniUnlit.Utils.PropNameCutoff, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, UniGLTF.UniUnlit.Utils.PropNameCullMode, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, UniGLTF.UniUnlit.Utils.PropNameBlendMode, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, UniGLTF.UniUnlit.Utils.PropNameVColBlendMode, VgoMaterialPropertyType.Int);

            ExportProperty(vgoMaterial, material, UniGLTF.UniUnlit.Utils.PropNameSrcBlend, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, UniGLTF.UniUnlit.Utils.PropNameDstBlend, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, UniGLTF.UniUnlit.Utils.PropNameZWrite, VgoMaterialPropertyType.Int);

            // Textures
            ExportTextureProperty(vgoMaterial, material, UniGLTF.UniUnlit.Utils.PropNameMainTex, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);

            // Tags
            ExportTag(vgoMaterial, material, UniGLTF.UniUnlit.Utils.TagRenderTypeKey);

            // Keywords
            ExportKeyword(vgoMaterial, material, UniGLTF.UniUnlit.Utils.KeywordAlphaTestOn);
            ExportKeyword(vgoMaterial, material, UniGLTF.UniUnlit.Utils.KeywordAlphaBlendOn);
            ExportKeyword(vgoMaterial, material, UniGLTF.UniUnlit.Utils.KeywordVertexColMul);

            return vgoMaterial;
        }

        #endregion

        #region Public Methods (Import)

        ///// <summary>
        ///// Create a unlit material.
        ///// </summary>
        ///// <param name="vgoMaterial">A vgo material.</param>
        ///// <param name="shader">A unlit shader.</param>
        ///// <returns>A unlit material.</returns>
        //public override Material CreateMaterialAsset(VgoMaterial vgoMaterial, Shader shader)
        //{
        //    Material material = base.CreateMaterialAsset(vgoMaterial, shader);

        //    if (material.shader.name == ShaderName.UniGLTF_UniUnlit)
        //    {
        //        UniGLTF.UniUnlit.Utils.ValidateProperties(material, true);

        //        return material;
        //    }

        //    if (vgoMaterial.tagMap.ContainsKey(UniGLTF.UniUnlit.Utils.TagRenderTypeKey))
        //    {
        //        string renderType = vgoMaterial.tagMap[UniGLTF.UniUnlit.Utils.TagRenderTypeKey];

        //        switch (renderType)
        //        {
        //            case UniGLTF.UniUnlit.Utils.TagRenderTypeValueTransparent:
        //                material.SetInt(UniGLTF.UniUnlit.Utils.PropNameBlendMode, (int)BlendMode.Fade);
        //                material.SetInt(UniGLTF.UniUnlit.Utils.PropNameSrcBlend, (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        //                material.SetInt(UniGLTF.UniUnlit.Utils.PropNameDstBlend, (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        //                material.SetInt(UniGLTF.UniUnlit.Utils.PropNameZWrite, 0);
        //                material.DisableKeyword(UniGLTF.UniUnlit.Utils.KeywordAlphaTestOn);
        //                material.EnableKeyword(UniGLTF.UniUnlit.Utils.KeywordAlphaBlendOn);
        //                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
        //                break;

        //            case UniGLTF.UniUnlit.Utils.TagRenderTypeValueTransparentCutout:
        //                material.SetInt(UniGLTF.UniUnlit.Utils.PropNameBlendMode, (int)BlendMode.Cutout);
        //                material.SetInt(UniGLTF.UniUnlit.Utils.PropNameSrcBlend, (int)UnityEngine.Rendering.BlendMode.One);
        //                material.SetInt(UniGLTF.UniUnlit.Utils.PropNameDstBlend, (int)UnityEngine.Rendering.BlendMode.Zero);
        //                material.SetInt(UniGLTF.UniUnlit.Utils.PropNameZWrite, 1);
        //                material.EnableKeyword(UniGLTF.UniUnlit.Utils.KeywordAlphaTestOn);
        //                material.DisableKeyword(UniGLTF.UniUnlit.Utils.KeywordAlphaBlendOn);
        //                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;
        //                break;

        //            case UniGLTF.UniUnlit.Utils.TagRenderTypeValueOpaque:
        //            default:
        //                material.SetInt(UniGLTF.UniUnlit.Utils.PropNameBlendMode, (int)BlendMode.Opaque);
        //                material.SetInt(UniGLTF.UniUnlit.Utils.PropNameSrcBlend, (int)UnityEngine.Rendering.BlendMode.One);
        //                material.SetInt(UniGLTF.UniUnlit.Utils.PropNameDstBlend, (int)UnityEngine.Rendering.BlendMode.Zero);
        //                material.SetInt(UniGLTF.UniUnlit.Utils.PropNameZWrite, 1);
        //                material.DisableKeyword(UniGLTF.UniUnlit.Utils.KeywordAlphaTestOn);
        //                material.DisableKeyword(UniGLTF.UniUnlit.Utils.KeywordAlphaBlendOn);
        //                material.renderQueue = -1;
        //                break;
        //        }
        //    }

        //    return material;
        //}

        #endregion
    }
}
