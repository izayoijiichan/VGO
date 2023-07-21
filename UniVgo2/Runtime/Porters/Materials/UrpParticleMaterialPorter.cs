// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : UrpParticleMaterialPorter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using System;
    using System.Collections.Generic;
    using UniUrpParticleShader;
    using UnityEngine;
    using UnityEngine.Rendering;
    using UniShader.Shared;

    /// <summary>
    /// URP Particle Material Porter
    /// </summary>
    public class UrpParticleMaterialPorter : AbstractMaterialPorterBase
    {
        #region Constructors

        /// <summary>
        /// Create a new instance of UrpParticleMaterialPorter.
        /// </summary>
        public UrpParticleMaterialPorter() : base() { }

        #endregion

        #region Public Methods (Export)

        /// <summary>
        /// Create a vgo material.
        /// </summary>
        /// <param name="material">A HDRP material.</param>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <returns>A vgo material.</returns>
        public override VgoMaterial CreateVgoMaterial(in Material material, in IVgoStorage vgoStorage)
        {
            var vgoMaterial = new VgoMaterial()
            {
                name = material.name,
                shaderName = material.shader.name,
                isUnlit = material.shader.name == ShaderName.URP_Particles_Unlit,
            };

            float smoothness = material.HasProperty(Property.Smoothness) ? material.GetFloat(Property.Smoothness) : 0.5f;

            // Properties

            ExportProperty(vgoMaterial, material, Property.Surface, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, Property.Blend, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, Property.BlendModePreserveSpecular, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, Property.ColorMode, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, Property.Cull, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, Property.AlphaClip, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, Property.AlphaToMask, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, Property.Cutoff, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, Property.ReceiveShadows, VgoMaterialPropertyType.Int);

            ExportProperty(vgoMaterial, material, Property.BaseColor, VgoMaterialPropertyType.Color4);
            ExportProperty(vgoMaterial, material, Property.BaseColorAddSubDiff, VgoMaterialPropertyType.Color4);

            ExportProperty(vgoMaterial, material, Property.Metallic, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, Property.Smoothness, VgoMaterialPropertyType.Float);

            ExportProperty(vgoMaterial, material, Property.BumpScale, VgoMaterialPropertyType.Float);

            //ExportProperty(vgoMaterial, material, Property.EmissionEnabled, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, Property.EmissionColor, VgoMaterialPropertyType.Color3);

            ExportProperty(vgoMaterial, material, Property.FlipbookBlending, VgoMaterialPropertyType.Int);

            ExportProperty(vgoMaterial, material, Property.SoftParticlesEnabled, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, Property.SoftParticleFadeParams, VgoMaterialPropertyType.Vector4);
            ExportProperty(vgoMaterial, material, Property.SoftParticlesNearFadeDistance, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, Property.SoftParticlesFarFadeDistance, VgoMaterialPropertyType.Float);

            ExportProperty(vgoMaterial, material, Property.CameraFadingEnabled, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, Property.CameraFadeParams, VgoMaterialPropertyType.Vector4);
            ExportProperty(vgoMaterial, material, Property.CameraNearFadeDistance, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, Property.CameraFarFadeDistance, VgoMaterialPropertyType.Float);

            ExportProperty(vgoMaterial, material, Property.DistortionEnabled, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, Property.DistortionBlend, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, Property.DistortionStrength, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, Property.DistortionStrengthScaled, VgoMaterialPropertyType.Float);

            ExportProperty(vgoMaterial, material, VgoMaterialPropertyType.Int, Property.QueueOffset);
            ExportProperty(vgoMaterial, material, VgoMaterialPropertyType.Int, Property.BlendOp);
            ExportProperty(vgoMaterial, material, VgoMaterialPropertyType.Int, Property.SrcBlend);
            ExportProperty(vgoMaterial, material, VgoMaterialPropertyType.Int, Property.DstBlend);
            ExportProperty(vgoMaterial, material, VgoMaterialPropertyType.Int, Property.ZWrite);

            // Textures
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.BaseMap);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.MetallicGlossMap, VgoTextureMapType.MetallicRoughnessMap, VgoColorSpaceType.Linear, smoothness);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.BumpMap, VgoTextureMapType.NormalMap, VgoColorSpaceType.Linear);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.EmissionMap, VgoTextureMapType.EmissionMap, VgoColorSpaceType.Srgb);

            // Tags
            ExportTag(vgoMaterial, material, Tag.RenderType);

            // Keywords
            ExportKeyword(vgoMaterial, material, Keyword.AlphaTestOn);
            ExportKeyword(vgoMaterial, material, Keyword.AlphaBlendOn);
            ExportKeyword(vgoMaterial, material, Keyword.AlphaPremultiplyOn);
            ExportKeyword(vgoMaterial, material, Keyword.AlphaOverlayOn);
            ExportKeyword(vgoMaterial, material, Keyword.AlphaModulateOn);
            ExportKeyword(vgoMaterial, material, Keyword.ColorOverlayOn);
            ExportKeyword(vgoMaterial, material, Keyword.ColorColorOn);
            ExportKeyword(vgoMaterial, material, Keyword.ColorAddSubDiffOn);

            return vgoMaterial;
        }

        #endregion

        #region Public Methods (Import)

        /// <summary>
        /// Create a URP Particle material.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="shader">A URP Particle shader.</param>
        /// <param name="allTexture2dList">List of all texture 2D.</param>
        /// <returns>A URP Particle material.</returns>
        public override Material CreateMaterialAsset(in VgoMaterial vgoMaterial, in Shader shader, in List<Texture2D?> allTexture2dList)
        {
            switch (vgoMaterial.shaderName)
            {
                case ShaderName.URP_Particles_Lit:
                case ShaderName.URP_Particles_Unlit:
                    break;
                default:
#if NET_STANDARD_2_1
                    ThrowHelper.ThrowNotSupportedException(vgoMaterial?.shaderName ?? string.Empty);
                    break;
#else
                    throw new NotSupportedException(vgoMaterial.shaderName ?? string.Empty);
#endif
            }

            var material = new Material(shader)
            {
                name = vgoMaterial.name
            };

            if (vgoMaterial.renderQueue >= 0)
            {
                material.renderQueue = vgoMaterial.renderQueue;
            }

            UrpParticleDefinition definition = CreateUrpParticleDefinition(vgoMaterial, allTexture2dList);

            UniUrpParticleShader.Utils.SetParametersToMaterial(material, definition);

            // @notice
            material.SetSafeKeyword(Keyword.Emission, definition.EmissionColor != Color.black);

            return material;
        }

        #endregion

        #region Protected Methods (Import)

        /// <summary>
        /// Create a URP particle definition.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="allTexture2dList">List of all texture 2D.</param>
        /// <returns>A URP particle definition.</returns>
        protected virtual UrpParticleDefinition CreateUrpParticleDefinition(in VgoMaterial vgoMaterial, in List<Texture2D?> allTexture2dList)
        {
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
            urpParticleDefinition.BaseMap = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.BaseMap));
            urpParticleDefinition.MetallicGlossMap = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.MetallicGlossMap));
            urpParticleDefinition.BumpMap = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.BumpMap));
            urpParticleDefinition.EmissionMap = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.EmissionMap));

            return urpParticleDefinition;
        }

        #endregion
    }
}
