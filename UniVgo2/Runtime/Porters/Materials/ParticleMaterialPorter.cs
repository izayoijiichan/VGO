// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : ParticleMaterialPorter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using System;
    using System.Collections.Generic;
    using UniParticleShader;
    using UniShader.Shared;
    using UnityEngine;
    using UnityEngine.Rendering;

    /// <summary>
    /// Particle Material Porter
    /// </summary>
    public class ParticleMaterialPorter : AbstractMaterialPorterBase
    {
        #region Constructors

        /// <summary>
        /// Create a new instance of ParticleMaterialPorter.
        /// </summary>
        public ParticleMaterialPorter() : base() { }

        #endregion

        #region Public Methods (Export)

        /// <summary>
        /// Create a vgo material.
        /// </summary>
        /// <param name="material">A particle material.</param>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <returns>A vgo material.</returns>
        public override VgoMaterial CreateVgoMaterial(Material material, IVgoStorage vgoStorage)
        {
            //ParticleDefinition particleDefinition = UniParticleShader.Utils.GetParametersFromMaterial(material);

            var vgoMaterial = new VgoMaterial()
            {
                name = material.name,
                shaderName = material.shader.name,
                isUnlit = material.shader.name == ShaderName.Particles_Standard_Unlit,
            };

            float smoothness = material.HasProperty(Property.Glossiness) ? material.GetFloat(Property.Glossiness) : 0.5f;

            // Properties

            if (vgoMaterial.intProperties is null)
            {
                vgoMaterial.intProperties = new Dictionary<string, int>();
            }

            ExportProperty(vgoMaterial, material, Property.BlendMode, VgoMaterialPropertyType.Int);

            // @notice for old UniVGO
            vgoMaterial.intProperties.Add("_BlendMode", (int)UniParticleShader.Utils.GetBlendMode(material));

            // @notice for Unity 2020 builtin shader
            vgoMaterial.intProperties.Add(Property.ColorMode, (int)UniParticleShader.Utils.GetColorMode(material));

            //ExportProperty(vgoMaterial, material, Property.ColorMode, VgoMaterialPropertyType.Int);

            ExportProperty(vgoMaterial, material, Property.FlipbookMode, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, Property.Cull, VgoMaterialPropertyType.Int);

            ExportProperty(vgoMaterial, material, Property.LightingEnabled, VgoMaterialPropertyType.Int);

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
            ExportProperty(vgoMaterial, material, Property.DistortionStrengthScaled, VgoMaterialPropertyType.Float);

            ExportProperty(vgoMaterial, material, Property.Color, VgoMaterialPropertyType.Color4);
            ExportProperty(vgoMaterial, material, Property.ColorAddSubDiff, VgoMaterialPropertyType.Color4);
            ExportProperty(vgoMaterial, material, Property.Cutoff, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, Property.MainTexSt, VgoMaterialPropertyType.Vector4);

            ExportProperty(vgoMaterial, material, Property.Metallic, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, Property.Glossiness, VgoMaterialPropertyType.Float);

            ExportProperty(vgoMaterial, material, Property.BumpScale, VgoMaterialPropertyType.Float);

            ExportProperty(vgoMaterial, material, Property.EmissionEnabled, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, Property.EmissionColor, VgoMaterialPropertyType.Color3);

            ExportProperty(vgoMaterial, material, VgoMaterialPropertyType.Int, Property.BlendOp);
            ExportProperty(vgoMaterial, material, VgoMaterialPropertyType.Int, Property.SrcBlend);
            ExportProperty(vgoMaterial, material, VgoMaterialPropertyType.Int, Property.DstBlend);
            ExportProperty(vgoMaterial, material, VgoMaterialPropertyType.Int, Property.ZWrite);

            // Textures
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.MainTex);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.MetallicGlossMap, VgoTextureMapType.MetallicRoughnessMap, VgoColorSpaceType.Linear, smoothness);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.BumpMap, VgoTextureMapType.NormalMap, VgoColorSpaceType.Linear);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.EmissionMap, VgoTextureMapType.EmissionMap, VgoColorSpaceType.Srgb);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.GrabTexture);

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
        /// Create a particle material.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="shader">A particle shader.</param>
        /// <param name="allTexture2dList">List of all texture 2D.</param>
        /// <returns>A particle material.</returns>
        public override Material CreateMaterialAsset(VgoMaterial vgoMaterial, Shader shader, List<Texture2D?> allTexture2dList)
        {
            if ((vgoMaterial.shaderName != UniVgo2.ShaderName.Particles_Standard_Surface) &&
                (vgoMaterial.shaderName != UniVgo2.ShaderName.Particles_Standard_Unlit))
            {
                ThrowHelper.ThrowArgumentException($"vgoMaterial.shaderName: {vgoMaterial.shaderName}");
            }

            if ((shader.name == UniVgo2.ShaderName.URP_Particles_Lit) ||
                (shader.name == UniVgo2.ShaderName.URP_Particles_Unlit))
            {
                return CreateMaterialAssetAsUrp(vgoMaterial, shader, allTexture2dList);
            }

            if ((shader.name != UniVgo2.ShaderName.Particles_Standard_Surface) &&
                (shader.name != UniVgo2.ShaderName.Particles_Standard_Unlit))
            {
                ThrowHelper.ThrowArgumentException($"shader.name: {shader.name}");
            }

            var material = new Material(shader)
            {
                name = vgoMaterial.name
            };

            ParticleDefinition particleDefinition = CreateParticleDefinition(vgoMaterial, allTexture2dList);

            UniParticleShader.Utils.SetParametersToMaterial(material, particleDefinition);

            return material;
        }

        #endregion

        #region Protected Methods (Import)

        /// <summary>
        /// Create a particle definition.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="allTexture2dList">List of all texture 2D.</param>
        /// <returns>A particle definition.</returns>
        protected virtual ParticleDefinition CreateParticleDefinition(VgoMaterial vgoMaterial, List<Texture2D?> allTexture2dList)
        {
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
            particleDefinition.MainTex = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.MainTex));
            particleDefinition.MetallicGlossMap = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.MetallicGlossMap));
            particleDefinition.BumpMap = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.BumpMap));
            particleDefinition.EmissionMap = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.EmissionMap));
            particleDefinition.GrabTexture = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.GrabTexture));

            particleDefinition.MainTexScale = new Vector2(particleDefinition.MainTexSt.x, particleDefinition.MainTexSt.y);
            particleDefinition.MainTexOffset = new Vector2(particleDefinition.MainTexSt.z, particleDefinition.MainTexSt.w);

            return particleDefinition;
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
        public virtual Material CreateMaterialAssetAsUrp(VgoMaterial vgoMaterial, Shader shader, List<Texture2D?> allTexture2dList)
        {
            var material = new Material(shader)
            {
                name = vgoMaterial.name
            };

            if (vgoMaterial.renderQueue >= 0)
            {
                material.renderQueue = vgoMaterial.renderQueue;
            }

            ParticleDefinition brpParticleDefinition = CreateParticleDefinition(vgoMaterial, allTexture2dList);

            UniUrpParticleShader.UrpParticleDefinition urpParticleDefinition = ConvertDefinitionBrpToUrp(brpParticleDefinition);

            UniUrpParticleShader.Utils.SetParametersToMaterial(material, urpParticleDefinition);

            // @notice
            if (brpParticleDefinition.EmissionEnabled)
            {
                material.SetSafeKeyword(Keyword.Emission, true);
            }

            return material;
        }

        #endregion

        #region Protected Methods (Import)

        /// <summary>
        /// Convert BRP particle definition to URP particle definition.
        /// </summary>
        /// <param name="brpParticleDefinition">A BRP particle definition.</param>
        /// <returns>A URP particle definition.</returns>
        protected virtual UniUrpParticleShader.UrpParticleDefinition ConvertDefinitionBrpToUrp(ParticleDefinition brpParticleDefinition)
        {
            var urpParticleDefinition = new UniUrpParticleShader.UrpParticleDefinition
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

                FlipbookBlending = brpParticleDefinition.FlipBookMode == FlipBookMode.Blended,

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

        #endregion
    }
}
