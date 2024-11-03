// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : VgoMaterialUniGltfUnlitDefinitionExtensions
// ----------------------------------------------------------------------
#nullable enable
#if UNIVGO_ENABLE_UNIGLTF_UNIUNLIT
namespace UniVgo2
{
    using NewtonVgo;
    using System.Collections.Generic;
    using UniGLTF.UniUnlit;
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
    /// Vgo Material UniGLTF Unlit Definition Extensions
    /// </summary>
    public static class VgoMaterialUniGltfUnlitDefinitionExtensions
    {
        /// <summary>
        /// Convert vgo material to UniGLTF unlit definition.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="allTextureList">List of all texture.</param>
        /// <returns>A UniGLTF unlit definition.</returns>
        public static UniGltfUnlitDefinition ToUniGltfUnlitDefinition(this VgoMaterial vgoMaterial, in List<Texture?> allTextureList)
        {
            if (vgoMaterial.shaderName != ShaderName.UniGLTF_UniUnlit)
            {
                ThrowHelper.ThrowException($"vgoMaterial.shaderName: {vgoMaterial.shaderName}");
            }

            var uniGltfUnlitDefinition = new UniGltfUnlitDefinition
            {
                Color = vgoMaterial.GetColorOrDefault(UniUnlitUtil.PropNameColor, Color.white),
                MainTex = null,
                MainTexScale = vgoMaterial.GetTextureScaleOrDefault(UniUnlitUtil.PropNameMainTex, Vector2.one),
                MainTexOffset = vgoMaterial.GetTextureOffsetOrDefault(UniUnlitUtil.PropNameMainTex, Vector2.zero),
                Cutoff = vgoMaterial.GetSafeFloat(UniUnlitUtil.PropNameCutoff, UniStandardShader.PropertyRange.Cutoff),
                CullMode = vgoMaterial.GetEnumOrDefault<UniUnlitCullMode>(UniUnlitUtil.PropNameCullMode, UniUnlitCullMode.Back),
                BlendMode = vgoMaterial.GetEnumOrDefault<UniUnlitRenderMode>(UniUnlitUtil.PropNameBlendMode, UniUnlitRenderMode.Opaque),
                VColBlendMode = vgoMaterial.GetEnumOrDefault<UniUnlitVertexColorBlendOp>(UniUnlitUtil.PropNameVColBlendMode, UniUnlitVertexColorBlendOp.None),
                SrcBlend = vgoMaterial.GetEnumOrDefault<UnityEngine.Rendering.BlendMode>(UniUnlitUtil.PropNameSrcBlend, UnityEngine.Rendering.BlendMode.One),
                DstBlend = vgoMaterial.GetEnumOrDefault<UnityEngine.Rendering.BlendMode>(UniUnlitUtil.PropNameDstBlend, UnityEngine.Rendering.BlendMode.Zero),
                ZWrite = vgoMaterial.GetIntOrDefault(UniUnlitUtil.PropNameZWrite, 1) == 1,
            };

            int mainTextureIndex = vgoMaterial.GetTextureIndexOrDefault(UniUnlitUtil.PropNameMainTex);

            uniGltfUnlitDefinition.MainTex = allTextureList.GetNullableValueOrDefault(mainTextureIndex) as Texture2D;

            return uniGltfUnlitDefinition;
        }
    }
}
#endif