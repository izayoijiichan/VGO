// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : UrpParticleMaterialPorter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using System.Collections.Generic;
    using UniUrpParticleShader;
    using UnityEngine;
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
                renderQueue = material.renderQueue,
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
            //ExportKeyword(vgoMaterial, material, Keyword.AlphaTestOn);
            //ExportKeyword(vgoMaterial, material, Keyword.AlphaBlendOn);
            //ExportKeyword(vgoMaterial, material, Keyword.AlphaPremultiplyOn);
            //ExportKeyword(vgoMaterial, material, Keyword.AlphaOverlayOn);
            //ExportKeyword(vgoMaterial, material, Keyword.AlphaModulateOn);
            //ExportKeyword(vgoMaterial, material, Keyword.ColorOverlayOn);
            //ExportKeyword(vgoMaterial, material, Keyword.ColorColorOn);
            //ExportKeyword(vgoMaterial, material, Keyword.ColorAddSubDiffOn);
            ExportKeywords(vgoMaterial, material);

            return vgoMaterial;
        }

        #endregion

        #region Public Methods (Import)

        /// <summary>
        /// Create a URP Particle material.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="shader">A URP Particle shader.</param>
        /// <param name="allTextureList">List of all texture.</param>
        /// <returns>A URP Particle material.</returns>
        public override Material CreateMaterialAsset(in VgoMaterial vgoMaterial, in Shader shader, in List<Texture?> allTextureList)
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
                    throw new System.NotSupportedException(vgoMaterial.shaderName ?? string.Empty);
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

            UrpParticleDefinition definition = vgoMaterial.ToUrpParticleDefinition(allTextureList);

            UniUrpParticleShader.Utils.SetParametersToMaterial(material, definition);

            // @notice
            material.SetSafeKeyword(Keyword.Emission, definition.EmissionColor != Color.black);

            return material;
        }

        #endregion
    }
}
