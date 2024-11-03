// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : VgoMaterialStandardParticleDefinitionExtensions
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using NewtonVgo;
    using System.Collections.Generic;
    using UniParticleShader;
    using UnityEngine;
    using UnityEngine.Rendering;

    /// <summary>
    /// Vgo Material Standard Particle Definition Extensions
    /// </summary>
    public static class VgoMaterialStandardParticleDefinitionExtensions
    {
        /// <summary>
        /// Convert vgo material to BRP particle definition.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="allTextureList">List of all texture.</param>
        /// <returns>A BRP particle definition.</returns>
        public static ParticleDefinition ToBrpParticleDefinition(this VgoMaterial vgoMaterial, in List<Texture?> allTextureList)
        {
            if ((vgoMaterial.shaderName != ShaderName.Particles_Standard_Surface) &&
                (vgoMaterial.shaderName != ShaderName.Particles_Standard_Unlit))
            {
                ThrowHelper.ThrowException($"vgoMaterial.shaderName: {vgoMaterial.shaderName}");
            }

            var particleDefinition = new ParticleDefinition
            {
                RenderMode = vgoMaterial.GetEnumOrDefault<UniParticleShader.BlendMode>(Property.BlendMode, UniParticleShader.BlendMode.Opaque),
                ColorMode = vgoMaterial.GetEnumOrDefault<ColorMode>(Property.ColorMode, ColorMode.Multiply),
                FlipBookMode = vgoMaterial.GetEnumOrDefault<FlipBookMode>(Property.FlipbookMode, FlipBookMode.Simple),
                Cull = vgoMaterial.GetEnumOrDefault<UnityEngine.Rendering.CullMode>(Property.Cull, UnityEngine.Rendering.CullMode.Back),
                TwoSided = false,
                Cutoff = vgoMaterial.GetSafeFloat(Property.Cutoff, PropertyRange.Cutoff),
                LightingEnabled = vgoMaterial.GetIntOrDefault(Property.LightingEnabled, 0) == 1,

                // Base
                Color = vgoMaterial.GetColorOrDefault(Property.Color, Color.white).gamma,
                ColorAddSubDiff = vgoMaterial.GetColorOrDefault(Property.ColorAddSubDiff, Color.black).gamma,
                MainTex = null,
                MainTexSt = vgoMaterial.GetVector4OrDefault(Property.MainTexSt, new Vector4(1.0f, 1.0f, 0.0f, 0.0f)),
                MainTexScale = Vector2.one,
                MainTexOffset = Vector2.zero,

                // Metallic Gloss Map
                MetallicGlossMap = null,
                Metallic = vgoMaterial.GetSafeFloat(Property.Metallic, PropertyRange.Metallic),
                Glossiness = vgoMaterial.GetSafeFloat(Property.Glossiness, PropertyRange.Glossiness),

                // Normal Map
                BumpMap = null,
                BumpScale = vgoMaterial.GetSafeFloat(Property.BumpScale, PropertyRange.BumpScale),

                // Emission Map
                EmissionEnabled = vgoMaterial.GetIntOrDefault(Property.EmissionEnabled, 0) == 1,
                EmissionColor = vgoMaterial.GetColorOrDefault(Property.EmissionColor, Color.black).gamma,
                EmissionMap = null,

                // Soft Particles
                SoftParticlesEnabled = vgoMaterial.GetIntOrDefault(Property.SoftParticlesEnabled, 0) == 1,
                SoftParticleFadeParams = vgoMaterial.GetVector4OrDefault(Property.SoftParticleFadeParams, new Vector4(0.0f, 1.0f, 0.0f, 0.0f)),
                SoftParticlesNearFadeDistance = vgoMaterial.GetSafeFloat(Property.SoftParticlesNearFadeDistance, PropertyRange.SoftParticlesNearFadeDistance),
                SoftParticlesFarFadeDistance = vgoMaterial.GetSafeFloat(Property.SoftParticlesFarFadeDistance, PropertyRange.SoftParticlesFarFadeDistance),

                // Camera Fading
                CameraFadingEnabled = vgoMaterial.GetIntOrDefault(Property.CameraFadingEnabled, 0) == 1,
                CameraFadeParams = vgoMaterial.GetVector4OrDefault(Property.CameraFadeParams, new Vector4(1.0f, 2.0f, 0.0f, 0.0f)),
                CameraNearFadeDistance = vgoMaterial.GetSafeFloat(Property.CameraNearFadeDistance, PropertyRange.CameraNearFadeDistance),
                CameraFarFadeDistance = vgoMaterial.GetSafeFloat(Property.CameraFarFadeDistance, PropertyRange.CameraFarFadeDistance),

                // Distortion
                DistortionEnabled = vgoMaterial.GetIntOrDefault(Property.DistortionEnabled, 0) == 1,
                DistortionBlend = vgoMaterial.GetSafeFloat(Property.DistortionBlend, PropertyRange.DistortionBlend),
                DistortionStrengthScaled = vgoMaterial.GetSafeFloat(Property.DistortionStrengthScaled, PropertyRange.DistortionStrengthScaled),

                BlendOp = vgoMaterial.GetEnumOrDefault<BlendOp>(Property.BlendOp, BlendOp.Add),
                SrcBlend = vgoMaterial.GetEnumOrDefault<UnityEngine.Rendering.BlendMode>(Property.SrcBlend, UnityEngine.Rendering.BlendMode.One),
                DstBlend = vgoMaterial.GetEnumOrDefault<UnityEngine.Rendering.BlendMode>(Property.DstBlend, UnityEngine.Rendering.BlendMode.Zero),
                ZWrite = vgoMaterial.GetIntOrDefault(Property.ZWrite, 1) == 1,

                GrabTexture = null,
            };

            particleDefinition.TwoSided = particleDefinition.Cull == UnityEngine.Rendering.CullMode.Off;

            // Textures
            particleDefinition.MainTex
                = allTextureList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.MainTex)) as Texture2D;
            particleDefinition.MetallicGlossMap
                = allTextureList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.MetallicGlossMap)) as Texture2D;
            particleDefinition.BumpMap
                = allTextureList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.BumpMap)) as Texture2D;
            particleDefinition.EmissionMap
                = allTextureList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.EmissionMap)) as Texture2D;
            particleDefinition.GrabTexture
                = allTextureList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.GrabTexture)) as Texture2D;

            particleDefinition.MainTexScale
                = new Vector2(particleDefinition.MainTexSt.x, particleDefinition.MainTexSt.y);
            particleDefinition.MainTexOffset
                = new Vector2(particleDefinition.MainTexSt.z, particleDefinition.MainTexSt.w);

            return particleDefinition;
        }
    }
}
