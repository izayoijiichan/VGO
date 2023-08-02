// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : StandardDefinitionExtensions
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using UniStandardShader;
    using UniUrpShader;
    using UnityEngine;

    /// <summary>
    /// Standard Definition Extensions
    /// </summary>
    public static class StandardDefinitionExtensions
    {
        /// <summary>
        /// Convert BRP standard definition to URP lit definition.
        /// </summary>
        /// <param name="brpStandardDefinition">A BRP standard definition.</param>
        /// <returns>A URP lit definition.</returns>
        public static UrpLitDefinition ToUrpLitDefinition(this StandardDefinition brpStandardDefinition)
        {
            return new UrpLitDefinition
            {
                WorkflowMode = WorkflowMode.Metallic,
                Surface = brpStandardDefinition.Mode switch
                {
                    AlphaBlendMode.Opaque => SurfaceType.Opaque,
                    AlphaBlendMode.Cutout => SurfaceType.Opaque,
                    AlphaBlendMode.Fade => SurfaceType.Transparent,
                    AlphaBlendMode.Transparent => SurfaceType.Transparent,
                    _ => SurfaceType.Opaque,
                },
                Blend = UniUrpShader.BlendMode.Alpha,
                SrcBlend = brpStandardDefinition.SrcBlend,
                DstBlend = brpStandardDefinition.DstBlend,
                SrcBlendAlpha = UnityEngine.Rendering.BlendMode.One,
                DstBlendAlpha = UnityEngine.Rendering.BlendMode.Zero,
                ZWrite = brpStandardDefinition.ZWrite,

                Cull = UniUrpShader.CullMode.Back,  // @notice
                AlphaClip = brpStandardDefinition.Mode == AlphaBlendMode.Cutout,
                Cutoff = brpStandardDefinition.Cutoff,

                ReceiveShadows = true,

                BaseColor = brpStandardDefinition.Color,

                // Base Map
                BaseMap = brpStandardDefinition.MainTex,
                BaseMapScale = brpStandardDefinition.MainTexScale,
                BaseMapOffset = brpStandardDefinition.MainTexOffset,

                // Metallic Gloss Map
                Metallic = brpStandardDefinition.Metallic,
                MetallicGlossMap = brpStandardDefinition.MetallicGlossMap,

                Smoothness = UniUrpShader.PropertyRange.Smoothness.defaultValue,  // @notice
                SmoothnessTextureChannel = (UniUrpShader.SmoothnessTextureChannel)brpStandardDefinition.SmoothnessTextureChannel,

                // Specular Gloss Map
                SpecColor = new Color(0.2f, 0.2f, 0.2f),
                SpecGlossMap = null,
                SpecularHighlights = brpStandardDefinition.SpecularHighlights,

                EnvironmentReflections = true,

                // Bump Map (Normal Map)
                BumpMap = brpStandardDefinition.BumpMap,
                BumpScale = brpStandardDefinition.BumpScale,

                // Parallax Map (Height Map)
                ParallaxMap = brpStandardDefinition.ParallaxMap,
                Parallax = brpStandardDefinition.Parallax,

                // Occlusion Map
                OcclusionStrength = brpStandardDefinition.OcclusionStrength,
                OcclusionMap = brpStandardDefinition.OcclusionMap,

                // Emission Map
                EmissionColor = brpStandardDefinition.EmissionColor,
                EmissionMap = brpStandardDefinition.EmissionMap,

                // Detail Inputs
                DetailMask = brpStandardDefinition.DetailMask,

                DetailAlbedoMap = brpStandardDefinition.DetailAlbedoMap,
                DetailAlbedoMapScale = UniUrpShader.PropertyRange.DetailAlbedoMapScale.defaultValue,
                DetailAlbedoMapScale2 = brpStandardDefinition.DetailAlbedoMapScale,
                DetailAlbedoMapOffset = brpStandardDefinition.DetailAlbedoMapOffset,

                DetailNormalMapScale = brpStandardDefinition.DetailNormalMapScale,
                DetailNormalMap = brpStandardDefinition.DetailNormalMap,

                // Advanced Options
                ClearCoatMask = false,
                ClearCoatSmoothness = false,

                // Editmode Properties
                QueueOffset = default,

                // Obsolete Properties
                //Color = brpStandardDefinition.Color,
                //MainTex = brpStandardDefinition.MainTex,

                //GlossMapScale = brpStandardDefinition.GlossMapScale,
                //Glossiness = brpStandardDefinition.Glossiness,
                //GlossyReflections = brpStandardDefinition.GlossyReflections,

                //UnityLightmaps = brpStandardDefinition.UnityLightmaps,
                //UnityLightmapsInd = brpStandardDefinition.UnityLightmapsInd,
                //UnityShadowMasks = brpStandardDefinition.UnityShadowMasks,
            };
        }
    }
}
