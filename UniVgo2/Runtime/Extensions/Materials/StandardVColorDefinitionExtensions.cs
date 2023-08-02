// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : StandardVColorDefinitionExtensions
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using UniStandardShader;
    using UniUrpShader;
    using UnityEngine;

    /// <summary>
    /// Standard Vertex Color Definition
    /// </summary>
    public class StandardVColorDefinition
    {
        /// <summary>Color</summary>
        public Color Color { get; set; }

        /// <summary>Main Texture</summary>
        public Texture2D? MainTex { get; set; }

        /// <summary>Main Texture Scale</summary>
        public Vector2 MainTexScale { get; set; }

        /// <summary>Main Texture Offset</summary>
        public Vector2 MainTexOffset { get; set; }

        /// <summary>Metallic</summary>
        public float Metallic { get; set; }

        /// <summary>Glossiness</summary>
        public float Glossiness { get; set; }
    }

    /// <summary>
    /// Standard Vertex Color Definition Extensions
    /// </summary>
    public static class StandardVColorDefinitionExtensions
    {
        /// <summary>
        /// Convert BRP standard vertex color definition to URP lit definition.
        /// </summary>
        /// <param name="standardVColorDefinition">A BRP standard vertex color definition.</param>
        /// <returns>A URP lit definition.</returns>
        public static UrpLitDefinition ToUrpLitDefinition(this StandardVColorDefinition standardVColorDefinition)
        {
            var urpLitDefinition = new UniUrpShader.UrpLitDefinition
            {
                WorkflowMode = UniUrpShader.WorkflowMode.Metallic,
                Surface = UniUrpShader.SurfaceType.Opaque,

                Blend = UniUrpShader.BlendMode.Alpha,
                SrcBlend = UnityEngine.Rendering.BlendMode.One,
                DstBlend = UnityEngine.Rendering.BlendMode.Zero,
                SrcBlendAlpha = UnityEngine.Rendering.BlendMode.One,
                DstBlendAlpha = UnityEngine.Rendering.BlendMode.Zero,
                ZWrite = true,

                Cull = UniUrpShader.CullMode.Back,
                AlphaClip = false,
                Cutoff = UniUrpShader.PropertyRange.Cutoff.defaultValue,

                ReceiveShadows = true,

                BaseColor = standardVColorDefinition.Color,

                // Base Map
                BaseMap = standardVColorDefinition.MainTex,
                BaseMapScale = standardVColorDefinition.MainTexScale,
                BaseMapOffset = standardVColorDefinition.MainTexOffset,

                // Metallic Gloss Map
                Metallic = standardVColorDefinition.Metallic,
                MetallicGlossMap = null,

                Smoothness = 1.0f - standardVColorDefinition.Glossiness,
                SmoothnessTextureChannel = UniUrpShader.SmoothnessTextureChannel.MetallicAlpha,

                // Specular Gloss Map
                SpecColor = new Color(0.2f, 0.2f, 0.2f),
                SpecGlossMap = null,
                SpecularHighlights = false,

                EnvironmentReflections = true,

                // Bump Map (Normal Map)
                BumpMap = null,
                BumpScale = UniUrpShader.PropertyRange.BumpScale.defaultValue,

                // Parallax Map (Height Map)
                ParallaxMap = null,
                Parallax = UniUrpShader.PropertyRange.Parallax.defaultValue,

                // Occlusion Map
                OcclusionStrength = UniUrpShader.PropertyRange.OcclusionStrength.defaultValue,
                OcclusionMap = null,

                // Emission Map
                EmissionColor = new Color(0.0f, 0.0f, 0.0f),
                EmissionMap = null,

                // Detail Inputs
                DetailMask = null,

                DetailAlbedoMap = null,
                DetailAlbedoMapScale = UniUrpShader.PropertyRange.DetailAlbedoMapScale.defaultValue,
                DetailAlbedoMapScale2 = Vector2.one,
                DetailAlbedoMapOffset = Vector2.zero,

                DetailNormalMapScale = UniUrpShader.PropertyRange.DetailNormalMapScale.defaultValue,
                DetailNormalMap = null,

                // Advanced Options
                ClearCoatMask = false,
                ClearCoatSmoothness = false,

                // Editmode Properties
                QueueOffset = default,

                // Obsolete Properties
                //Color = vgoMaterialParameter.Color,
                //MainTex = vgoMaterialParameter.MainTex,

                //GlossMapScale = default,
                //Glossiness = vgoMaterialParameter.Glossiness,
                //GlossyReflections = default,

                //UnityLightmaps = default,
                //UnityLightmapsInd = default,
                //UnityShadowMasks = default,
            };

            return urpLitDefinition;
        }
    }
}
