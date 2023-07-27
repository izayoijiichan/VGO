// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : VgoMaterialUrpParticleDefinitionExtensions
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using NewtonVgo;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Rendering;
    using UniUrpParticleShader;

    /// <summary>
    /// Vgo Material URP Particle Definition Extensions
    /// </summary>
    public static class VgoMaterialUrpParticleDefinitionExtensions
    {
        /// <summary>
        /// Convert vgo material to URP particle definition.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="allTexture2dList">List of all texture 2D.</param>
        /// <returns>A URP particle definition.</returns>
        public static UrpParticleDefinition ToUrpParticleDefinition(this VgoMaterial vgoMaterial, in List<Texture2D?> allTexture2dList)
        {
            if ((vgoMaterial.shaderName != ShaderName.URP_Particles_Lit) &&
                (vgoMaterial.shaderName != ShaderName.URP_Particles_Unlit))
            {
                ThrowHelper.ThrowException($"vgoMaterial.shaderName: {vgoMaterial.shaderName}");
            }

            var urpParticleDefinition = new UrpParticleDefinition
            {
                Surface = vgoMaterial.GetEnumOrDefault<SurfaceType>(Property.Surface, SurfaceType.Opaque),
                Blend = vgoMaterial.GetEnumOrDefault<UniUrpParticleShader.BlendMode>(Property.Blend, UniUrpParticleShader.BlendMode.Alpha),
                BlendModePreserveSpecular = vgoMaterial.GetIntOrDefault(Property.BlendModePreserveSpecular, 0) == 1,
                ColorMode = vgoMaterial.GetEnumOrDefault<ColorMode>(Property.ColorMode, ColorMode.Multiply),
                Cull = vgoMaterial.GetEnumOrDefault<UnityEngine.Rendering.CullMode>(Property.Cull, UnityEngine.Rendering.CullMode.Back),
                AlphaClip = vgoMaterial.GetIntOrDefault(Property.AlphaClip, 0) == 1,
                AlphaToMask = vgoMaterial.GetIntOrDefault(Property.AlphaToMask, 0) == 1,
                Cutoff = vgoMaterial.GetSafeFloat(Property.Cutoff, PropertyRange.Cutoff),
                ReceiveShadows = vgoMaterial.GetIntOrDefault(Property.AlphaClip, 0) == 1,

                // Base
                BaseColor = vgoMaterial.GetColorOrDefault(Property.BaseColor, Color.white).gamma,
                BaseColorAddSubDiff = vgoMaterial.GetColorOrDefault(Property.BaseColorAddSubDiff, new Color(0.0f, 0.0f, 0.0f, 0.0f)).gamma,
                BaseMap = null,
                BaseMapScale = vgoMaterial.GetTextureScaleOrDefault(Property.BaseMap, Vector2.one),
                BaseMapOffset = vgoMaterial.GetTextureOffsetOrDefault(Property.BaseMap, Vector2.zero),

                // Metallic Gloss Map
                MetallicGlossMap = null,
                Metallic = vgoMaterial.GetSafeFloat(Property.Metallic, PropertyRange.Metallic),
                Smoothness = vgoMaterial.GetSafeFloat(Property.Smoothness, PropertyRange.Smoothness),

                // Normal Map
                BumpMap = null,
                BumpScale = vgoMaterial.GetSafeFloat(Property.BumpScale, PropertyRange.BumpScale),

                // Emission Map
                //EmissionEnabled = vgoMaterial.GetIntOrDefault(Property.EmissionEnabled, 0) == 1,
                EmissionColor = vgoMaterial.GetColorOrDefault(Property.BaseColor, new Color(0.0f, 0.0f, 0.0f)).gamma,
                EmissionMap = null,

                FlipbookBlending = vgoMaterial.GetIntOrDefault(Property.FlipbookBlending, 0) == 1,

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
                DistortionStrength = vgoMaterial.GetSafeFloat(Property.DistortionStrength, PropertyRange.DistortionStrength),
                DistortionStrengthScaled = vgoMaterial.GetSafeFloat(Property.DistortionStrengthScaled, PropertyRange.DistortionStrengthScaled),

                QueueOffset = vgoMaterial.GetSafeInt(Property.QueueOffset, PropertyRange.QueueOffset),
                BlendOp = vgoMaterial.GetEnumOrDefault<BlendOp>(Property.BlendOp, BlendOp.Add),
                SrcBlend = vgoMaterial.GetEnumOrDefault<UnityEngine.Rendering.BlendMode>(Property.SrcBlend, UnityEngine.Rendering.BlendMode.One),
                DstBlend = vgoMaterial.GetEnumOrDefault<UnityEngine.Rendering.BlendMode>(Property.DstBlend, UnityEngine.Rendering.BlendMode.Zero),
                ZWrite = vgoMaterial.GetIntOrDefault(Property.ZWrite, 1) == 1,

                // Obsolete
                //Mode,
                //FlipBookMode,
                //Color,
                //Glossiness,
                //MainTex,
                //MainTexSt,
            };

            // Textures
            urpParticleDefinition.BaseMap
                = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.BaseMap));
            urpParticleDefinition.MetallicGlossMap
                = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.MetallicGlossMap));
            urpParticleDefinition.BumpMap
                = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.BumpMap));
            urpParticleDefinition.EmissionMap
                = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.EmissionMap));

            return urpParticleDefinition;
        }
    }
}
