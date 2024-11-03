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
        /// <param name="allTextureList">List of all texture.</param>
        /// <returns>A standard definition.</returns>
        public static StandardDefinition ToStandardDefinition(this VgoMaterial vgoMaterial, in List<Texture?> allTextureList)
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
                = allTextureList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.MainTex)) as Texture2D;
            standardDefinition.MetallicGlossMap
                = allTextureList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.MetallicGlossMap)) as Texture2D;
            standardDefinition.BumpMap
                = allTextureList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.BumpMap)) as Texture2D;
            standardDefinition.ParallaxMap
                = allTextureList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.ParallaxMap)) as Texture2D;
            standardDefinition.OcclusionMap
                = allTextureList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.OcclusionMap)) as Texture2D;
            standardDefinition.EmissionMap
                = allTextureList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.EmissionMap)) as Texture2D;
            standardDefinition.DetailMask
                = allTextureList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.DetailMask)) as Texture2D;
            standardDefinition.DetailAlbedoMap
                = allTextureList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.DetailAlbedoMap)) as Texture2D;
            standardDefinition.DetailNormalMap
                = allTextureList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.DetailNormalMap)) as Texture2D;

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
