// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : VgoMaterialUrpUnlitDefinitionExtensions
// ----------------------------------------------------------------------
#nullable enable
#if UNIVGO_ENABLE_UNIGLTF_UNIUNLIT
namespace UniVgo2
{
    using NewtonVgo;
    using System.Collections.Generic;
    using UniGLTF.UniUnlit;
    using UnityEngine;
    using UniUrpShader;

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
    /// Vgo Material URP Unlit Definition Extensions
    /// </summary>
    public static class VgoMaterialUrpUnlitDefinitionExtensions
    {
        /// <summary>
        /// Convert vgo material to URP unlit definition.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="allTexture2dList">List of all texture 2D.</param>
        /// <returns>A URP unlit definition.</returns>
        /// <remarks>
        /// ShaderName.UniGLTF_UniUnlit
        /// ShaderName.Unlit_Color
        /// ShaderName.Unlit_Texture
        /// ShaderName.Unlit_Transparent
        /// ShaderName.Unlit_Transparent_Cutout
        /// ShaderName.VRM_UnlitTexture
        /// ShaderName.VRM_UnlitCutout
        /// ShaderName.VRM_UnlitTransparent
        /// ShaderName.VRM_UnlitTransparentZWrite
        /// </remarks>
        public static UrpUnlitDefinition ToUrpUnlitDefinition(this VgoMaterial vgoMaterial, in List<Texture2D?> allTexture2dList)
        {
            if (vgoMaterial.shaderName == ShaderName.UniGLTF_UniUnlit)
            {
                UniGltfUnlitDefinition brpUnlitDefinition = vgoMaterial.ToUniGltfUnlitDefinition(allTexture2dList);

                UrpUnlitDefinition urpUnlitDefinition = brpUnlitDefinition.ToUrpUnlitDefinition();

                return urpUnlitDefinition;
            }
            else
            {
                var urpUnlitDefinition = new UrpUnlitDefinition
                {
                    Surface = vgoMaterial.shaderName switch
                    {
                        ShaderName.Unlit_Color => UniUrpShader.SurfaceType.Opaque,
                        ShaderName.Unlit_Texture => UniUrpShader.SurfaceType.Opaque,
                        ShaderName.Unlit_Transparent => UniUrpShader.SurfaceType.Transparent,
                        ShaderName.Unlit_Transparent_Cutout => UniUrpShader.SurfaceType.Opaque,
                        ShaderName.VRM_UnlitTexture => UniUrpShader.SurfaceType.Opaque,
                        ShaderName.VRM_UnlitCutout => UniUrpShader.SurfaceType.Opaque,
                        ShaderName.VRM_UnlitTransparent => UniUrpShader.SurfaceType.Transparent,
                        ShaderName.VRM_UnlitTransparentZWrite => UniUrpShader.SurfaceType.Transparent,
                        _ => UniUrpShader.SurfaceType.Opaque,
                    },

                    Blend = vgoMaterial.shaderName switch
                    {
                        ShaderName.Unlit_Transparent => UniUrpShader.BlendMode.Alpha,  // @notice
                        ShaderName.VRM_UnlitTransparent => UniUrpShader.BlendMode.Alpha,  // @notice
                        ShaderName.VRM_UnlitTransparentZWrite => UniUrpShader.BlendMode.Alpha,  // @notice
                        _ => UniUrpShader.BlendMode.Alpha,
                    },

                    BlendOp = UnityEngine.Rendering.BlendOp.Add,  // @notice

                    SrcBlend = vgoMaterial.GetEnumOrDefault<UnityEngine.Rendering.BlendMode>(UniUrpShader.Property.SrcBlend, UnityEngine.Rendering.BlendMode.One),
                    DstBlend = vgoMaterial.GetEnumOrDefault<UnityEngine.Rendering.BlendMode>(UniUrpShader.Property.DstBlend, UnityEngine.Rendering.BlendMode.Zero),
                    ZWrite = vgoMaterial.GetIntOrDefault(UniUrpShader.Property.ZWrite, 1) == 1,

                    Cull = vgoMaterial.GetEnumOrDefault<UniUrpShader.CullMode>(UniUrpShader.Property.Cull, UniUrpShader.CullMode.Back),

                    AlphaClip = vgoMaterial.shaderName switch
                    {
                        ShaderName.Unlit_Transparent_Cutout => true,
                        ShaderName.VRM_UnlitCutout => true,
                        _ => false,
                    },

                    Cutoff = vgoMaterial.GetSafeFloat(UniUrpShader.Property.Cutoff, UniStandardShader.PropertyRange.Cutoff),

                    BaseColor = vgoMaterial.GetColorOrDefault(UniUnlitUtil.PropNameColor, Color.white),

                    BaseMap = null,
                    BaseMapScale = Vector2.one,
                    BaseMapOffset = Vector2.zero,

                    QueueOffset = 0,

                    // Obsolete Properties
                    //Color = default,
                    //MainTex = default,
                    //SampleGI = default,
                };

                int mainTextureIndex = vgoMaterial.GetTextureIndexOrDefault(UniUnlitUtil.PropNameMainTex);

                urpUnlitDefinition.BaseMap = allTexture2dList.GetNullableValueOrDefault(mainTextureIndex);
                urpUnlitDefinition.BaseMapScale = vgoMaterial.GetTextureScaleOrDefault(UniUnlitUtil.PropNameMainTex, Vector2.one);
                urpUnlitDefinition.BaseMapOffset = vgoMaterial.GetTextureOffsetOrDefault(UniUnlitUtil.PropNameMainTex, Vector2.zero);

                return urpUnlitDefinition;
            }
        }
    }
}
#endif