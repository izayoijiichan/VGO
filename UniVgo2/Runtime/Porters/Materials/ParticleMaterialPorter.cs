// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : ParticleMaterialPorter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using System.Collections.Generic;
    using UniParticleShader;
    using UniShader.Shared;
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
        public override VgoMaterial CreateVgoMaterial(in Material material, in IVgoStorage vgoStorage)
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

            vgoMaterial.intProperties ??= new Dictionary<string, int>();

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
        public override Material CreateMaterialAsset(in VgoMaterial vgoMaterial, in Shader shader, in List<Texture2D?> allTexture2dList)
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

            ParticleDefinition particleDefinition = vgoMaterial.ToBrpParticleDefinition(allTexture2dList);

            UniParticleShader.Utils.SetParametersToMaterial(material, particleDefinition);

            return material;
        }

        #endregion

        #region Protected Methods (Import)

        /// <summary>
        /// Create a URP Particle material.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="shader">A URP Particle shader.</param>
        /// <param name="allTexture2dList">List of all texture 2D.</param>
        /// <returns>A URP Particle material.</returns>
        protected virtual Material CreateMaterialAssetAsUrp(in VgoMaterial vgoMaterial, Shader shader, in List<Texture2D?> allTexture2dList)
        {
            var material = new Material(shader)
            {
                name = vgoMaterial.name
            };

            if (vgoMaterial.renderQueue >= 0)
            {
                material.renderQueue = vgoMaterial.renderQueue;
            }

            ParticleDefinition brpParticleDefinition = vgoMaterial.ToBrpParticleDefinition(allTexture2dList);

            UniUrpParticleShader.UrpParticleDefinition urpParticleDefinition = brpParticleDefinition.ToUrpParticleDefinition();

            UniUrpParticleShader.Utils.SetParametersToMaterial(material, urpParticleDefinition);

            // @notice
            if (brpParticleDefinition.EmissionEnabled)
            {
                material.SetSafeKeyword(Keyword.Emission, true);
            }

            return material;
        }

        #endregion
    }
}
