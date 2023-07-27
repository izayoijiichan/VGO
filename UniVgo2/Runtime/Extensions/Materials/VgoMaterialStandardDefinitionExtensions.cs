// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : VgoMaterialStandardDefinitionExtensions
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using NewtonVgo;
    using System.Collections.Generic;
    using UniStandardShader;
    using UnityEngine;

    /// <summary>
    /// Vgo Material Standard Definition Extensions
    /// </summary>
    public static class VgoMaterialStandardDefinitionExtensions
    {
        /// <summary>
        /// Convert vgo material to standard definition.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="allTexture2dList">List of all texture 2D.</param>
        /// <returns>A standard definition.</returns>
        public static StandardDefinition ToStandardDefinition(this VgoMaterial vgoMaterial, in List<Texture2D?> allTexture2dList)
        {
            // @notice Accept any shader.
            //if (vgoMaterial.shaderName != UniStandardShader.ShaderName.Standard)
            //{
            //    ThrowHelper.ThrowException($"vgoMaterial.shaderName: {vgoMaterial.shaderName}");
            //}

            var standardDefinition = new StandardDefinition
            {
                Mode = vgoMaterial.GetEnumOrDefault<AlphaBlendMode>(Property.Mode, AlphaBlendMode.Opaque),

                Cutoff = vgoMaterial.GetSafeFloat(Property.Cutoff, PropertyRange.Cutoff),

                // Base
                Color = vgoMaterial.GetColorOrDefault(Property.Color, Color.white).gamma,
                MainTex = null,
                MainTexScale = Vector2.one,
                MainTexOffset = Vector2.zero,

                // Metallic Gloss Map
                Glossiness = vgoMaterial.GetSafeFloat(Property.Glossiness, PropertyRange.Glossiness),
                GlossMapScale = vgoMaterial.GetSafeFloat(Property.GlossMapScale, PropertyRange.GlossMapScale),
                SmoothnessTextureChannel = vgoMaterial.GetEnumOrDefault<SmoothnessTextureChannel>(Property.SmoothnessTextureChannel, SmoothnessTextureChannel.SpecularMetallicAlpha),
                Metallic = vgoMaterial.GetSafeFloat(Property.Metallic, PropertyRange.Metallic),
                MetallicGlossMap = null,
                SpecularHighlights = vgoMaterial.GetIntOrDefault(Property.SpecularHighlights, 0) == 1,
                GlossyReflections = vgoMaterial.GetIntOrDefault(Property.GlossyReflections, 0) == 1,

                // Bump Map (Normal Map)
                BumpScale = vgoMaterial.GetSafeFloat(Property.BumpScale, PropertyRange.BumpScale),
                BumpMap = null,

                // Parallax Map (Height Map)
                Parallax = vgoMaterial.GetSafeFloat(Property.Parallax, PropertyRange.Parallax),
                ParallaxMap = null,

                // Occlusion Map
                OcclusionStrength = vgoMaterial.GetSafeFloat(Property.OcclusionStrength, PropertyRange.OcclusionStrength),
                OcclusionMap = null,

                // Emission Map
                EmissionColor = vgoMaterial.GetColorOrDefault(Property.EmissionColor, Color.black).gamma,
                EmissionMap = null,

                // Detail Inputs
                DetailMask = null,
                DetailAlbedoMap = null,
                DetailAlbedoMapScale = Vector2.one,
                DetailAlbedoMapOffset = Vector2.zero,
                DetailNormalMapScale = vgoMaterial.GetSafeFloat(Property.DetailNormalMapScale, PropertyRange.DetailNormalMapScale),
                DetailNormalMap = null,

                UVSec = vgoMaterial.GetEnumOrDefault<UV>(Property.UVSec, UV.UV0),

                SrcBlend = vgoMaterial.GetEnumOrDefault<UnityEngine.Rendering.BlendMode>(Property.SrcBlend, UnityEngine.Rendering.BlendMode.One),
                DstBlend = vgoMaterial.GetEnumOrDefault<UnityEngine.Rendering.BlendMode>(Property.DstBlend, UnityEngine.Rendering.BlendMode.Zero),
                ZWrite = vgoMaterial.GetIntOrDefault(Property.ZWrite, 1) == 1,
            };

            // Textures
            standardDefinition.MainTex
                = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.MainTex));
            standardDefinition.MetallicGlossMap
                = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.MetallicGlossMap));
            standardDefinition.BumpMap
                = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.BumpMap));
            standardDefinition.ParallaxMap
                = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.ParallaxMap));
            standardDefinition.OcclusionMap
                = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.OcclusionMap));
            standardDefinition.EmissionMap
                = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.EmissionMap));
            standardDefinition.DetailMask
                = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.DetailMask));
            standardDefinition.DetailAlbedoMap
                = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.DetailAlbedoMap));
            standardDefinition.DetailNormalMap
                = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.DetailNormalMap));

            standardDefinition.MainTexScale
                = vgoMaterial.GetTextureScaleOrDefault(Property.MainTex, Vector2.one);
            standardDefinition.MainTexOffset
                = vgoMaterial.GetTextureOffsetOrDefault(Property.MainTex, Vector2.zero);

            standardDefinition.DetailAlbedoMapScale
                = vgoMaterial.GetTextureScaleOrDefault(Property.DetailAlbedoMap, Vector2.one);
            standardDefinition.DetailAlbedoMapOffset
                = vgoMaterial.GetTextureOffsetOrDefault(Property.DetailAlbedoMap, Vector2.zero);

            return standardDefinition;
        }
    }
}
