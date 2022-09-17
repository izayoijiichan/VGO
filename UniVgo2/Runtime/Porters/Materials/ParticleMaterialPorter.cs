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
    using UnityEngine;

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
            ExportProperty(vgoMaterial, material, Property.BlendMode, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, Property.FlipbookMode, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, Property.CullMode, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, Property.SoftParticlesEnabled, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, Property.SoftParticleFadeParams, VgoMaterialPropertyType.Vector4);
            ExportProperty(vgoMaterial, material, Property.CameraFadingEnabled, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, Property.CameraFadeParams, VgoMaterialPropertyType.Vector4);
            ExportProperty(vgoMaterial, material, Property.DistortionEnabled, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, Property.DistortionStrengthScaled, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, Property.DistortionBlend, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, Property.ColorAddSubDiff, VgoMaterialPropertyType.Color4);
            ExportProperty(vgoMaterial, material, Property.MainTexSt, VgoMaterialPropertyType.Vector4);
            ExportProperty(vgoMaterial, material, Property.Color, VgoMaterialPropertyType.Color4);
            ExportProperty(vgoMaterial, material, Property.Cutoff, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, Property.Metallic, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, Property.Glossiness, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, Property.BumpScale, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, Property.LightingEnabled, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, Property.EmissionEnabled, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, Property.EmissionColor, VgoMaterialPropertyType.Color3);

            ExportProperty(vgoMaterial, material, Property.BlendOp, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, Property.SrcBlend, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, Property.DstBlend, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, Property.ZWrite, VgoMaterialPropertyType.Int);

            // Textures
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.GrabTexture);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.MainTex);
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
            ExportKeyword(vgoMaterial, material, Keyword.ColorColorOn);
            ExportKeyword(vgoMaterial, material, Keyword.ColorAddSubDiffOn);

            if (vgoMaterial.intProperties is null)
            {
                vgoMaterial.intProperties = new Dictionary<string, int>();
            }

            // @notice
            vgoMaterial.intProperties.Add("_BlendMode", (int)UniParticleShader.Utils.GetBlendMode(material));
            vgoMaterial.intProperties.Add("_ColorMode", (int)UniParticleShader.Utils.GetColorMode(material));

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
            if (vgoMaterial == null)
            {
                throw new ArgumentNullException(nameof(vgoMaterial));
            }

            if (shader == null)
            {
                throw new ArgumentNullException(nameof(shader));
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
            ParticleDefinition particleDefinition = new ParticleDefinition
            {
                RenderMode = (UniParticleShader.BlendMode)vgoMaterial.GetIntOrDefault("_BlendMode"),
                ColorMode = (UniParticleShader.ColorMode)vgoMaterial.GetIntOrDefault("_ColorMode"),
                FlipBookMode = (UniParticleShader.FlipBookMode)vgoMaterial.GetIntOrDefault(Property.FlipbookMode),
                CullMode = (UnityEngine.Rendering.CullMode)vgoMaterial.GetIntOrDefault(Property.CullMode),
                SoftParticlesEnabled = vgoMaterial.GetIntOrDefault(Property.SoftParticlesEnabled) == 1,
                SoftParticleFadeParams = vgoMaterial.GetVector4OrDefault(Property.SoftParticleFadeParams, new Vector4(1.0f, 1.0f, 0.0f, 0.0f)),
                CameraFadingEnabled = vgoMaterial.GetIntOrDefault(Property.CameraFadingEnabled) == 1,
                CameraFadeParams = vgoMaterial.GetVector4OrDefault(Property.CameraFadeParams, Vector4.zero),
                DistortionEnabled = vgoMaterial.GetIntOrDefault(Property.DistortionEnabled) == 1,
                GrabTexture = null,
                DistortionStrengthScaled = vgoMaterial.GetSafeFloat(Property.DistortionStrengthScaled, 0.0f, 1.0f, 1.0f),
                DistortionBlend = vgoMaterial.GetSafeFloat(Property.DistortionBlend, 0.0f, 1.0f, 0.5f),
                ColorAddSubDiff = vgoMaterial.GetColorOrDefault(Property.ColorAddSubDiff, Color.black).gamma,
                MainTex = null,
                MainTexSt = vgoMaterial.GetVector4OrDefault(Property.MainTexSt, new Vector4(1.0f, 1.0f, 0.0f, 0.0f)),
                Color = vgoMaterial.GetColorOrDefault(Property.Color, Color.white).gamma,
                Cutoff = vgoMaterial.GetSafeFloat(Property.Cutoff, 0.0f, 1.0f, 0.5f),
                MetallicGlossMap = null,
                Metallic = vgoMaterial.GetSafeFloat(Property.Metallic, 0.0f, 1.0f, 0.5f),
                Glossiness = vgoMaterial.GetSafeFloat(Property.Glossiness, 0.0f, 1.0f, 0.5f),
                BumpMap = null,
                BumpScale = vgoMaterial.GetSafeFloat(Property.BumpScale, 0.0f, 1.0f, 1.0f),
                LightingEnabled = vgoMaterial.GetIntOrDefault(Property.LightingEnabled) == 1,
                EmissionEnabled = vgoMaterial.GetIntOrDefault(Property.EmissionEnabled) == 1,
                EmissionColor = vgoMaterial.GetColorOrDefault(Property.EmissionColor, Color.white).gamma,
                EmissionMap = null,
            };

            particleDefinition.GrabTexture = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.GrabTexture));
            particleDefinition.MainTex = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.MainTex));
            particleDefinition.MetallicGlossMap = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.MetallicGlossMap));
            particleDefinition.BumpMap = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.BumpMap));
            particleDefinition.EmissionMap = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.EmissionMap));

            return particleDefinition;
        }

        #endregion
    }
}
