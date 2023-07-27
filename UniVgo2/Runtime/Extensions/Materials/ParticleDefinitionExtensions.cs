// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : ParticleDefinitionExtensions
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using UniUrpParticleShader;
    using UniParticleShader;

    /// <summary>
    /// Particle Definition Extensions
    /// </summary>
    public static class ParticleDefinitionExtensions
    {
        /// <summary>
        /// Convert BRP particle definition to URP particle definition.
        /// </summary>
        /// <param name="brpParticleDefinition">A BRP particle definition.</param>
        /// <returns>A URP particle definition.</returns>
        public static UrpParticleDefinition ToUrpParticleDefinition(this ParticleDefinition brpParticleDefinition)
        {
            var urpParticleDefinition = new UrpParticleDefinition
            {
                Surface = default,
                Blend = default,
                ColorMode = (UniUrpParticleShader.ColorMode)brpParticleDefinition.ColorMode,
                Cull = brpParticleDefinition.Cull,
                AlphaClip = brpParticleDefinition.RenderMode == UniParticleShader.BlendMode.Cutout,
                Cutoff = brpParticleDefinition.Cutoff,
                ReceiveShadows = brpParticleDefinition.LightingEnabled,

                // Base
                BaseColor = brpParticleDefinition.Color,
                BaseColorAddSubDiff = brpParticleDefinition.ColorAddSubDiff,
                BaseMap = brpParticleDefinition.MainTex,
                BaseMapScale = brpParticleDefinition.MainTexScale,
                BaseMapOffset = brpParticleDefinition.MainTexOffset,

                // Metallic Gloss Map
                MetallicGlossMap = brpParticleDefinition.MetallicGlossMap,
                Metallic = brpParticleDefinition.Metallic,
                Smoothness = 1.0f - brpParticleDefinition.Glossiness,

                // Normal Map
                BumpScale = brpParticleDefinition.BumpScale,
                BumpMap = brpParticleDefinition.BumpMap,

                // Emission Map
                //EmissionEnabled = brpParticleDefinition.EmissionEnabled,
                EmissionColor = brpParticleDefinition.EmissionColor,
                EmissionMap = brpParticleDefinition.EmissionMap,

                FlipbookBlending = brpParticleDefinition.FlipBookMode == UniParticleShader.FlipBookMode.Blended,

                // Soft Particles
                SoftParticlesEnabled = brpParticleDefinition.SoftParticlesEnabled,
                SoftParticleFadeParams = brpParticleDefinition.SoftParticleFadeParams,
                SoftParticlesNearFadeDistance = brpParticleDefinition.SoftParticlesNearFadeDistance,
                SoftParticlesFarFadeDistance = brpParticleDefinition.SoftParticlesFarFadeDistance,

                // Camera Fading
                CameraFadingEnabled = brpParticleDefinition.CameraFadingEnabled,
                CameraFadeParams = brpParticleDefinition.CameraFadeParams,
                CameraNearFadeDistance = brpParticleDefinition.CameraNearFadeDistance,
                CameraFarFadeDistance = brpParticleDefinition.CameraFarFadeDistance,

                // Distortion
                DistortionEnabled = brpParticleDefinition.DistortionEnabled,
                DistortionBlend = brpParticleDefinition.DistortionBlend,
                DistortionStrength = UniUrpParticleShader.PropertyRange.DistortionStrength.defaultValue,
                DistortionStrengthScaled = brpParticleDefinition.DistortionStrengthScaled,

                BlendOp = brpParticleDefinition.BlendOp,
                SrcBlend = brpParticleDefinition.SrcBlend,
                DstBlend = brpParticleDefinition.DstBlend,
                ZWrite = brpParticleDefinition.ZWrite,

                //Mode = materialProxy.Mode,
                //FlipBookMode = materialProxy.FlipBookMode,
                //Color = materialProxy.Color,
                //Glossiness = materialProxy.Glossiness,

                //MainTex = materialProxy.MainTex,
                //MainTexSt = materialProxy.MainTexSt,
            };

            urpParticleDefinition.Surface = brpParticleDefinition.RenderMode switch
            {
                UniParticleShader.BlendMode.Opaque => UniUrpParticleShader.SurfaceType.Opaque,
                UniParticleShader.BlendMode.Cutout => UniUrpParticleShader.SurfaceType.Opaque,
                UniParticleShader.BlendMode.Fade => UniUrpParticleShader.SurfaceType.Transparent,
                UniParticleShader.BlendMode.Transparent => UniUrpParticleShader.SurfaceType.Transparent,
                UniParticleShader.BlendMode.Additive => UniUrpParticleShader.SurfaceType.Transparent,
                UniParticleShader.BlendMode.Subtractive => UniUrpParticleShader.SurfaceType.Transparent,
                UniParticleShader.BlendMode.Modulate => UniUrpParticleShader.SurfaceType.Transparent,
                _ => UniUrpParticleShader.SurfaceType.Opaque,
            };

            urpParticleDefinition.Blend = brpParticleDefinition.RenderMode switch
            {
                UniParticleShader.BlendMode.Opaque => UniUrpParticleShader.BlendMode.Alpha,   // @todo
                UniParticleShader.BlendMode.Cutout => UniUrpParticleShader.BlendMode.Alpha,
                UniParticleShader.BlendMode.Fade => UniUrpParticleShader.BlendMode.Additive,  // @notice
                UniParticleShader.BlendMode.Transparent => UniUrpParticleShader.BlendMode.Premultiply,
                UniParticleShader.BlendMode.Additive => UniUrpParticleShader.BlendMode.Additive,
                UniParticleShader.BlendMode.Subtractive => UniUrpParticleShader.BlendMode.Additive, // @notice
                UniParticleShader.BlendMode.Modulate => UniUrpParticleShader.BlendMode.Additive,    // @notice
                _ => UniUrpParticleShader.BlendMode.Alpha,
            };

            urpParticleDefinition.BlendModePreserveSpecular = false;  // @todo

            urpParticleDefinition.AlphaToMask = false;  // @todo

            return urpParticleDefinition;
        }
    }
}
