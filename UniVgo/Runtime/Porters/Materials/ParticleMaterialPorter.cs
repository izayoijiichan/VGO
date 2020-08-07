// ----------------------------------------------------------------------
// @Namespace : UniVgo.Porters
// @Class     : ParticleMaterialPorter
// ----------------------------------------------------------------------
namespace UniVgo.Porters
{
    using NewtonGltf;
    using System;
    using System.Collections.Generic;
    using UniParticleShader;
    using UnityEngine;
    using VgoGltf;

    /// <summary>
    /// Particle Material Importer
    /// </summary>
    public class ParticleMaterialPorter : MaterialPorterBase
    {
        #region Constructors

        /// <summary>
        /// Create a new instance of ParticleMaterialPorter.
        /// </summary>
        public ParticleMaterialPorter() : base() { }

        #endregion

        #region Public Methods (Export)

        /// <summary>
        /// Create a glTF material.
        /// </summary>
        /// <param name="material">A particle material.</param>
        /// <returns>A glTF material.</returns>
        public override GltfMaterial CreateGltfMaterial(Material material)
        {
            ParticleDefinition particleDefinition = UniParticleShader.Utils.GetParametersFromMaterial(material);

            var vgoParticle = new VGO_materials_particle()
            {
                renderMode = (ParticleBlendMode)particleDefinition.RenderMode,
                colorMode = (ParticleColorMode)particleDefinition.ColorMode,
                flipBookMode = (ParticleFlipBookMode)particleDefinition.FlipBookMode,
                cullMode = (VgoGltf.CullMode)particleDefinition.CullMode,
                softParticlesEnabled = particleDefinition.SoftParticlesEnabled,
                softParticleFadeParams = particleDefinition.SoftParticleFadeParams.ToNumericsVector4(),
                cameraFadingEnabled = particleDefinition.CameraFadingEnabled,
                cameraFadeParams = particleDefinition.CameraFadeParams.ToNumericsVector4(),
                distortionEnabled = particleDefinition.DistortionEnabled,
                grabTextureIndex = -1,
                distortionStrengthScaled = particleDefinition.DistortionStrengthScaled,
                distortionBlend = particleDefinition.DistortionBlend,
                colorAddSubDiff = particleDefinition.ColorAddSubDiff.linear.ToGltfColor4(),
                mainTexIndex = -1,
                mainTexSt = particleDefinition.MainTexSt.ToNumericsVector4(),
                color = particleDefinition.Color.linear.ToGltfColor4(),
                cutoff = particleDefinition.Cutoff,
                metallicGlossMapIndex = -1,
                metallic = particleDefinition.Metallic,
                glossiness = particleDefinition.Glossiness,
                bumpMapIndex = -1,
                bumpScale = particleDefinition.BumpScale,
                lightingEnabled = particleDefinition.LightingEnabled,
                emissionEnabled = particleDefinition.EmissionEnabled,
                emissionColor = particleDefinition.EmissionColor.linear.ToGltfColor3(),
                emissionMapIndex = -1,
            };

            float smoothness = particleDefinition.Glossiness;

            // Textures
            vgoParticle.grabTextureIndex = ExportTexture(material, particleDefinition.GrabTexture, Property.GrabTexture);
            vgoParticle.mainTexIndex = ExportTexture(material, particleDefinition.MainTex, Property.MainTex);
            vgoParticle.metallicGlossMapIndex = ExportTexture(material, particleDefinition.MetallicGlossMap, Property.MetallicGlossMap, TextureType.MetallicRoughnessMap, ColorSpaceType.Linear, smoothness);
            vgoParticle.bumpMapIndex = ExportTexture(material, particleDefinition.BumpMap, Property.BumpMap, TextureType.NormalMap, ColorSpaceType.Linear);
            vgoParticle.emissionMapIndex = ExportTexture(material, particleDefinition.EmissionMap, Property.EmissionMap, TextureType.EmissionMap, ColorSpaceType.Srgb);

            GltfMaterial gltfMaterial = new GltfMaterial()
            {
                name = material.name,
            };

            // Alpha Mode
            switch (vgoParticle.renderMode)
            {
                case ParticleBlendMode.Opaque:
                    gltfMaterial.alphaMode = GltfAlphaMode.OPAQUE;
                    break;

                case ParticleBlendMode.Cutout:
                    gltfMaterial.alphaMode = GltfAlphaMode.MASK;
                    break;

                case ParticleBlendMode.Fade:
                case ParticleBlendMode.Transparent:
                case ParticleBlendMode.Additive:
                case ParticleBlendMode.Subtractive:
                case ParticleBlendMode.Modulate:
                    gltfMaterial.alphaMode = GltfAlphaMode.BLEND;
                    break;

                default:
                    break;
            }

            // Alpha Cutoff
            if (vgoParticle.renderMode == ParticleBlendMode.Cutout)
            {
                gltfMaterial.alphaCutoff = vgoParticle.cutoff;
            }

            // Double Sided
            switch (vgoParticle.cullMode)
            {
                case VgoGltf.CullMode.Off:
                    gltfMaterial.doubleSided = true;
                    break;

                case VgoGltf.CullMode.Front:
                case VgoGltf.CullMode.Back:
                    gltfMaterial.doubleSided = false;
                    break;

                default:
                    break;
            }

            // Base Color
            if (vgoParticle.color != null)
            {
                if (gltfMaterial.pbrMetallicRoughness == null)
                {
                    gltfMaterial.pbrMetallicRoughness = new GltfMaterialPbrMetallicRoughness();
                }

                gltfMaterial.pbrMetallicRoughness.baseColorFactor = vgoParticle.color;
            }

            // Main Texture
            if (vgoParticle.mainTexIndex != -1)
            {
                if (gltfMaterial.pbrMetallicRoughness == null)
                {
                    gltfMaterial.pbrMetallicRoughness = new GltfMaterialPbrMetallicRoughness();
                }

                gltfMaterial.pbrMetallicRoughness.baseColorTexture = new GltfTextureInfo
                {
                    index = vgoParticle.mainTexIndex,
                };
            }

            // Normal Texture
            if (vgoParticle.bumpMapIndex != -1)
            {
                gltfMaterial.normalTexture = new GltfNormalTextureInfo
                {
                    index = vgoParticle.bumpMapIndex,
                };
            }

            // Emissive
            if (vgoParticle.emissionEnabled)
            {
                gltfMaterial.emissiveFactor = vgoParticle.emissionColor;

                if (vgoParticle.emissionMapIndex != -1)
                {
                    gltfMaterial.emissiveTexture = new GltfTextureInfo
                    {
                        index = vgoParticle.emissionMapIndex,
                    };
                }
            }

            // Extensions
            //  VGO_materials
            //  VGO_materials_particle
            gltfMaterial.extensions = new GltfExtensions(_JsonSerializerSettings)
            {
                { VGO_materials.ExtensionName, new VGO_materials(material.shader.name) },
                { VGO_materials_particle.ExtensionName, vgoParticle },
            };

            return gltfMaterial;
        }

        #endregion

        #region Public Methods (Import)

        /// <summary>
        /// Set material texture info list.
        /// </summary>
        /// <param name="materialInfo">A material info.</param>
        /// <param name="allTextureInfoList">List of all texture info.</param>
        public override void SetMaterialTextureInfoList(MaterialInfo materialInfo, List<TextureInfo> allTextureInfoList)
        {
            AllTextureInfoList = allTextureInfoList;

            GltfMaterial gltfMaterial = materialInfo.gltfMaterial;

            if (gltfMaterial.extensions.Contains(VGO_materials_particle.ExtensionName) == false)
            {
                throw new Exception($"{VGO_materials_particle.ExtensionName} is not found.");
            }

            gltfMaterial.extensions.JsonSerializerSettings = _JsonSerializerSettings;

            VGO_materials_particle vgoParticle = gltfMaterial.extensions.GetValueOrDefault<VGO_materials_particle>(VGO_materials_particle.ExtensionName);

            if (vgoParticle.grabTextureIndex != -1)
            {
                if (TryGetTextureAndSetInfo(vgoParticle.grabTextureIndex, out TextureInfo textureInfo))
                {
                    materialInfo.TextureInfoList.TryAdd(textureInfo);
                }
            }

            // Main Texture
            if (vgoParticle.mainTexIndex != -1)
            {
                if (TryGetTextureAndSetInfo(vgoParticle.mainTexIndex, out TextureInfo textureInfo))
                {
                    materialInfo.TextureInfoList.TryAdd(textureInfo);
                }
            }

            // Metallic Gloss Map
            if (vgoParticle.metallicGlossMapIndex != -1)
            {
                TextureInfo textureInfo = GetTextureAndSetInfo(
                    vgoParticle.metallicGlossMapIndex,
                    TextureType.MetallicRoughnessMap
                );

                if (textureInfo != null)
                {
                    float glossiness = vgoParticle.glossiness.SafeValue(0.0f, 1.0f, 0.5f);
                    float roughness = 1.0f - glossiness;

                    textureInfo.metallicRoughness = roughness;

                    materialInfo.TextureInfoList.TryAdd(textureInfo);
                }
            }

            // Normal Map
            if (vgoParticle.bumpMapIndex != -1)
            {
                if (TryGetTextureAndSetInfo(vgoParticle.bumpMapIndex, TextureType.NormalMap, out TextureInfo textureInfo))
                {
                    textureInfo.normalTextureScale = vgoParticle.bumpScale.SafeValue(0.0f, 1.0f, 1.0f);

                    materialInfo.TextureInfoList.TryAdd(textureInfo);
                }
            }

            // Emission Map
            if (vgoParticle.emissionMapIndex != -1)
            {
                if (TryGetTextureAndSetInfo(vgoParticle.emissionMapIndex, TextureType.EmissionMap, out TextureInfo textureInfo))
                {
                    materialInfo.TextureInfoList.TryAdd(textureInfo);
                }
            }
        }

        /// <summary>
        /// Create a particle material.
        /// </summary>
        /// <param name="materialInfo">A material info.</param>
        /// <param name="shader">A particle shader.</param>
        /// <returns>A particle material.</returns>
        public override Material CreateMaterialAsset(MaterialInfo materialInfo, Shader shader)
        {
            if (materialInfo == null)
            {
                throw new ArgumentNullException(nameof(materialInfo));
            }

            if (shader == null)
            {
                throw new ArgumentNullException(nameof(shader));
            }

            var material = new Material(shader)
            {
                name = materialInfo.name
            };

            ParticleDefinition particleDefinition = CreateParticleDefinition(materialInfo);

            UniParticleShader.Utils.SetParametersToMaterial(material, particleDefinition);

            return material;
        }

        #endregion

        #region Protected Methods (Import)

        /// <summary>
        /// Create a particle definition.
        /// </summary>
        /// <param name="materialInfo">A material info.</param>
        /// <returns>A particle definition.</returns>
        protected virtual ParticleDefinition CreateParticleDefinition(MaterialInfo materialInfo)
        {
            GltfMaterial gltfMaterial = materialInfo.gltfMaterial;

            if (gltfMaterial.extensions.Contains(VGO_materials_particle.ExtensionName) == false)
            {
                throw new Exception($"{VGO_materials_particle.ExtensionName} is not found.");
            }

            gltfMaterial.extensions.JsonSerializerSettings = _JsonSerializerSettings;

            VGO_materials_particle vgoParticle = gltfMaterial.extensions.GetValueOrDefault<VGO_materials_particle>(VGO_materials_particle.ExtensionName);

            ParticleDefinition particleDefinition = new ParticleDefinition
            {
                RenderMode = (UniParticleShader.BlendMode)vgoParticle.renderMode,
                ColorMode = (UniParticleShader.ColorMode)vgoParticle.colorMode,
                FlipBookMode = (UniParticleShader.FlipBookMode)vgoParticle.flipBookMode,
                CullMode = (UnityEngine.Rendering.CullMode)vgoParticle.cullMode,
                SoftParticlesEnabled = vgoParticle.softParticlesEnabled,
                SoftParticleFadeParams = vgoParticle.softParticleFadeParams.GetValueOrDefault(System.Numerics.Vector4.Zero).ToUnityVector4(),
                CameraFadingEnabled = vgoParticle.cameraFadingEnabled,
                CameraFadeParams = vgoParticle.cameraFadeParams.GetValueOrDefault(System.Numerics.Vector4.Zero).ToUnityVector4(),
                DistortionEnabled = vgoParticle.distortionEnabled,
                GrabTexture = null,
                DistortionStrengthScaled = vgoParticle.distortionStrengthScaled.SafeValue(0.0f, 1.0f, 0.5f),
                DistortionBlend = vgoParticle.distortionBlend,
                ColorAddSubDiff = vgoParticle.colorAddSubDiff.GetValueOrDefault(Color4.Black).ToUnityColor().gamma,
                MainTex = null,
                MainTexSt = vgoParticle.mainTexSt.GetValueOrDefault(System.Numerics.Vector4.Zero).ToUnityVector4(),
                Color = vgoParticle.color.GetValueOrDefault(Color4.Black).ToUnityColor().gamma,
                Cutoff = vgoParticle.cutoff.SafeValue(0.0f, 1.0f, 1.0f),
                MetallicGlossMap = null,
                Metallic = vgoParticle.metallic.SafeValue(0.0f, 1.0f, 0.0f),
                Glossiness = vgoParticle.glossiness.SafeValue(0.0f, 1.0f, 0.0f),
                BumpMap = null,
                BumpScale = vgoParticle.bumpScale.SafeValue(0.0f, 1.0f, 1.0f),
                LightingEnabled = vgoParticle.lightingEnabled,
                EmissionEnabled = vgoParticle.emissionEnabled,
                EmissionColor = vgoParticle.emissionColor.GetValueOrDefault(Color3.Black).ToUnityColor().gamma,
                EmissionMap = null,
            };

            particleDefinition.GrabTexture = AllTexture2dList.GetValueOrDefault(vgoParticle.grabTextureIndex);
            particleDefinition.MainTex = AllTexture2dList.GetValueOrDefault(vgoParticle.mainTexIndex);
            particleDefinition.MetallicGlossMap = AllTexture2dList.GetValueOrDefault(vgoParticle.metallicGlossMapIndex);
            particleDefinition.BumpMap = AllTexture2dList.GetValueOrDefault(vgoParticle.bumpMapIndex);
            particleDefinition.EmissionMap = AllTexture2dList.GetValueOrDefault(vgoParticle.emissionMapIndex);

            return particleDefinition;
        }

        #endregion
    }
}
