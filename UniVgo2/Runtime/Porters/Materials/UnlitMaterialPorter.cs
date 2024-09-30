// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : UnlitMaterialPorter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using System.Collections.Generic;
#if UNIVGO_ENABLE_UNIGLTF_UNIUNLIT
    using UniGLTF.UniUnlit;
#endif
    using UniShader.Shared;
    using UnityEngine;

#if VRMC_GLTF_0_125_OR_NEWER
    //
#elif VRMC_VRMSHADERS_0_104_OR_NEWER
    //
#elif VRMC_VRMSHADERS_0_85_OR_NEWER
    //
#elif VRMC_VRMSHADERS_0_79_OR_NEWER
    using UniUnlitUtil = UniGLTF.UniUnlit.Utils;
#elif VRMC_VRMSHADERS_0_72_OR_NEWER
    using UniUnlitUtil = UniGLTF.UniUnlit.Utils;
#else
    //
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

        #region Public Methods (Export)

        /// <summary>
        /// Create a vgo material.
        /// </summary>
        /// <param name="material">A unlit material.</param>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <returns>vgo material</returns>
        public override VgoMaterial CreateVgoMaterial(in Material material, in IVgoStorage vgoStorage)
        {
#if UNIVGO_ENABLE_UNIGLTF_UNIUNLIT
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
            if (material.shader.name == ShaderName.UniGLTF_UniUnlit)
            {
                ExportKeyword(vgoMaterial, material, UniUnlitUtil.KeywordAlphaTestOn);
                ExportKeyword(vgoMaterial, material, UniUnlitUtil.KeywordAlphaBlendOn);
                ExportKeyword(vgoMaterial, material, UniUnlitUtil.KeywordVertexColMul);
            }

            return vgoMaterial;
#else
#if NET_STANDARD_2_1
            ThrowHelper.ThrowNotSupportedException(material.shader.name);
            return default;
#else
            throw new NotSupportedException(material.shader.name);
#endif
#endif
        }

        #endregion

        #region Public Methods (Import)

        /// <summary>
        /// Create a unlit material.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="shader">A unlit shader.</param>
        /// <param name="allTexture2dList">List of all texture 2D.</param>
        /// <returns>A unlit material.</returns>
        public override Material CreateMaterialAsset(in VgoMaterial vgoMaterial, in Shader shader, in List<Texture2D?> allTexture2dList)
        {
#if UNIVGO_ENABLE_UNIGLTF_UNIUNLIT
            if (shader.name == ShaderName.URP_Unlit)
            {
                return CreateMaterialAssetAsUrp(vgoMaterial, shader, allTexture2dList);
            }

            if (vgoMaterial.shaderName == ShaderName.UniGLTF_UniUnlit)
            {
                UniGltfUnlitDefinition vgoMaterialParameter = vgoMaterial.ToUniGltfUnlitDefinition(allTexture2dList);

                var material = new Material(shader)
                {
                    name = vgoMaterial.name
                };

                material.SetSafeColor(UniUnlitUtil.PropNameColor, vgoMaterialParameter.Color);

                if (vgoMaterialParameter.MainTex != null)
                {
                    material.SetTexture(UniUnlitUtil.PropNameMainTex, vgoMaterialParameter.MainTex);
                }

                material.SetSafeFloat(UniUnlitUtil.PropNameCutoff, vgoMaterialParameter.Cutoff);

                material.SetSafeInt(UniUnlitUtil.PropNameCullMode, (int)vgoMaterialParameter.CullMode);

                material.SetSafeInt(UniUnlitUtil.PropNameBlendMode, (int)vgoMaterialParameter.BlendMode);

                material.SetSafeInt(UniUnlitUtil.PropNameVColBlendMode, (int)vgoMaterialParameter.VColBlendMode);

                material.SetSafeInt(UniUnlitUtil.PropNameSrcBlend, (int)vgoMaterialParameter.SrcBlend);

                material.SetSafeInt(UniUnlitUtil.PropNameDstBlend, (int)vgoMaterialParameter.DstBlend);

                material.SetSafeInt(UniUnlitUtil.PropNameZWrite, vgoMaterialParameter.ZWrite ? 1 : 0);

                UniUnlitUtil.ValidateProperties(material, true);

                return material;
            }

            return base.CreateMaterialAsset(vgoMaterial, shader, allTexture2dList);
#else
#if NET_STANDARD_2_1
            ThrowHelper.ThrowNotSupportedException(vgoMaterial.shaderName ?? string.Empty);
            return default;
#else
            throw new NotSupportedException(vgoMaterial.shaderName ?? string.Empty);
#endif
#endif
        }

        #endregion

#if UNIVGO_ENABLE_UNIGLTF_UNIUNLIT
        #region Protected Methods (Import) BRP to URP

        /// <summary>
        /// Create a URP unlit material.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="shader">A URP unlit shader.</param>
        /// <param name="allTexture2dList">List of all texture 2D.</param>
        /// <returns>A URP unlit material.</returns>
        protected virtual Material CreateMaterialAssetAsUrp(in VgoMaterial vgoMaterial, Shader shader, in List<Texture2D?> allTexture2dList)
        {
#if ENABLE_UNITY_URP_SHADER
            var material = new Material(shader)
            {
                name = vgoMaterial.name
            };

            UniUrpShader.UrpUnlitDefinition urpUnlitMaterialParameter = vgoMaterial.ToUrpUnlitDefinition(allTexture2dList);

            UniUrpShader.Utils.SetParametersToMaterial(material, urpUnlitMaterialParameter);

            // @notice
            if (vgoMaterial.shaderName == ShaderName.VRM_UnlitTransparentZWrite)
            {
                material.SetInt(UniUrpShader.Property.ZWrite, 1);
            }

            return material;
#else
#if NET_STANDARD_2_1
            ThrowHelper.ThrowNotSupportedException(vgoMaterial.shaderName ?? string.Empty);
            return default;
#else
            throw new NotSupportedException(vgoMaterial.shaderName ?? string.Empty);
#endif
#endif
        }

        #endregion
#endif
    }
}
