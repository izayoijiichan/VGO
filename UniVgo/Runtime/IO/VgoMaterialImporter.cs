// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : VgoMaterialImporter
// ----------------------------------------------------------------------
namespace UniVgo
{
    using MToon;
    using System.Collections.Generic;
    using UniGLTFforUniVgo;
    using UnityEngine;
    using UnityEngine.Rendering;
    using UniStandardParticle;

    /// <summary>
    /// VGO Material Importer
    /// </summary>
    public class VgoMaterialImporter : MaterialImporter
    {
        #region Constructors

        /// <summary>
        /// Create a new instance of VgoMaterialImporter with ImporterContext.
        /// </summary>
        /// <param name="context"></param>
        public VgoMaterialImporter(ImporterContext context) : base(new VgoShaderStore(context), context)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Create a material.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="src"></param>
        /// <param name="hasVertexColor"></param>
        /// <returns></returns>
        public override Material CreateMaterial(int i, glTFMaterial src, bool hasVertexColor)
        {
            if (src.extensions != null)
            {
                if (src.extensions.VGO_materials_particle != null)
                {
                    return CreateParticleMaterial(i, src);
                }
                if (src.extensions.KHR_materials_unlit != null)
                {
                    return CreateUnlitMaterial(i, src, hasVertexColor);
                }
                if (src.extensions.VRMC_materials_mtoon != null)
                {
                    return CreateMtoonMaterial(i, src);
                }
            }

            return base.CreateMaterial(i, src, hasVertexColor);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Create a Particle material.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="src"></param>
        /// <returns></returns>
        protected virtual Material CreateParticleMaterial(int i, glTFMaterial src)
        {
            var shader = m_shaderStore.GetShader(src);

            Material material = new Material(shader);

            material.name = CreateMaterialName(i, src);

#if UNITY_EDITOR
            material.hideFlags = HideFlags.DontUnloadUnusedAsset;
#endif

            ParticleDefinition particleDefinition = CreateParticleDefinition(src.extensions.VGO_materials_particle);

            UniStandardParticle.Utils.SetParticleParametersToMaterial(material, particleDefinition);

            return material;
        }

        /// <summary>
        /// Create a Particle definition.
        /// </summary>
        /// <param name="vgoParticle"></param>
        /// <returns></returns>
        protected virtual ParticleDefinition CreateParticleDefinition(VGO_materials_particle vgoParticle)
        {
            ParticleDefinition particleDefinition = new ParticleDefinition()
            {
                RenderMode = (UniStandardParticle.BlendMode)vgoParticle.renderMode,
                ColorMode = (UniStandardParticle.ColorMode)vgoParticle.colorMode,
                FlipBookMode = (UniStandardParticle.FlipBookMode)vgoParticle.flipBookMode,
                CullMode = vgoParticle.cullMode,
                SoftParticlesEnabled = vgoParticle.softParticlesEnabled,
                SoftParticleFadeParams = ArrayConverter.ToVector4(vgoParticle.softParticleFadeParams),
                CameraFadingEnabled = vgoParticle.cameraFadingEnabled,
                CameraFadeParams = ArrayConverter.ToVector4(vgoParticle.cameraFadeParams),
                DistortionEnabled = vgoParticle.distortionEnabled,
                GrabTexture = GetTexture(UniStandardParticle.Utils.PropGrabTexture, vgoParticle.grabTextureIndex),
                DistortionStrengthScaled = vgoParticle.distortionStrengthScaled,
                DistortionBlend = vgoParticle.distortionBlend,
                ColorAddSubDiff = ArrayConverter.ToColor(vgoParticle.colorAddSubDiff, gamma: true),
                MainTex = GetTexture(UniStandardParticle.Utils.PropMainTex, vgoParticle.mainTexIndex),
                MainTexSt = ArrayConverter.ToVector4(vgoParticle.mainTexSt),
                Color = ArrayConverter.ToColor(vgoParticle.color, gamma: true),
                Cutoff = vgoParticle.cutoff,
                MetallicGlossMap = GetTexture(UniStandardParticle.Utils.PropMetallicGlossMap, vgoParticle.metallicGlossMapIndex),
                Metallic = vgoParticle.metallic,
                Glossiness = vgoParticle.glossiness,
                BumpMap = GetTexture(UniStandardParticle.Utils.PropBumpMap, vgoParticle.bumpMapIndex),
                BumpScale = vgoParticle.bumpScale,
                LightingEnabled = vgoParticle.lightingEnabled,
                EmissionEnabled = vgoParticle.emissionEnabled,
                EmissionColor = ArrayConverter.ToColor(vgoParticle.emissionColor, gamma: true),
                EmissionMap = GetTexture(UniStandardParticle.Utils.PropEmissionMap, vgoParticle.emissionMapIndex),
            };

            return particleDefinition;
        }

        /// <summary>
        /// Create a Unlit material.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="src"></param>
        /// <param name="hasVertexColor"></param>
        /// <returns></returns>
        protected virtual Material CreateUnlitMaterial(int i, glTFMaterial src, bool hasVertexColor)
        {
            var shader = m_shaderStore.GetShader(src);

            var material = new Material(shader);

            material.name = CreateMaterialName(i, src);

#if UNITY_EDITOR
            material.hideFlags = HideFlags.DontUnloadUnusedAsset;
#endif
            // renderMode
            switch (src.alphaMode)
            {
                case MaterialAlphaMode.OPAQUE:
                    UniGLTF.UniUnlit.Utils.SetRenderMode(material, UniGLTF.UniUnlit.UniUnlitRenderMode.Opaque);
                    break;

                case MaterialAlphaMode.BLEND:
                    UniGLTF.UniUnlit.Utils.SetRenderMode(material, UniGLTF.UniUnlit.UniUnlitRenderMode.Transparent);
                    break;

                case MaterialAlphaMode.MASK:
                    UniGLTF.UniUnlit.Utils.SetRenderMode(material, UniGLTF.UniUnlit.UniUnlitRenderMode.Cutout);
                    break;

                default:
                    UniGLTF.UniUnlit.Utils.SetRenderMode(material, UniGLTF.UniUnlit.UniUnlitRenderMode.Opaque);
                    break;
            }

            // culling
            if (src.doubleSided)
            {
                UniGLTF.UniUnlit.Utils.SetCullMode(material, UniGLTF.UniUnlit.UniUnlitCullMode.Off);
            }
            else
            {
                UniGLTF.UniUnlit.Utils.SetCullMode(material, UniGLTF.UniUnlit.UniUnlitCullMode.Back);
            }

            // VColor
            if (hasVertexColor)
            {
                UniGLTF.UniUnlit.Utils.SetVColBlendMode(material, UniGLTF.UniUnlit.UniUnlitVertexColorBlendOp.Multiply);
            }
            else
            {
                UniGLTF.UniUnlit.Utils.SetVColBlendMode(material, UniGLTF.UniUnlit.UniUnlitVertexColorBlendOp.None);
            }

            if (src.pbrMetallicRoughness != null)
            {
                // color
                if (src.pbrMetallicRoughness.baseColorFactor != null)
                {
                    material.color = ArrayConverter.ToColor(src.pbrMetallicRoughness.baseColorFactor, gamma: true);
                }

                // texture
                if (src.pbrMetallicRoughness.baseColorTexture != null)
                {
                    var texture = Context.GetTexture(src.pbrMetallicRoughness.baseColorTexture.index);

                    if (texture != null)
                    {
                        material.mainTexture = texture.Texture;
                    }
                }
            }

            if (material.shader.name == ShaderName.UniGLTF_UniUnlit)
            {
                UniGLTF.UniUnlit.Utils.ValidateProperties(material, true);

                return material;
            }

            switch (src.alphaMode)
            {
                case MaterialAlphaMode.BLEND:
                    material.SetInt(UniGLTF.UniUnlit.Utils.PropNameBlendMode, (int)BlendMode.Fade);
                    material.SetInt(UniGLTF.UniUnlit.Utils.PropNameSrcBlend, (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt(UniGLTF.UniUnlit.Utils.PropNameDstBlend, (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt(UniGLTF.UniUnlit.Utils.PropNameZWrite, 0);
                    material.DisableKeyword(UniGLTF.UniUnlit.Utils.KeywordAlphaTestOn);
                    material.EnableKeyword(UniGLTF.UniUnlit.Utils.KeywordAlphaBlendOn);
                    material.renderQueue = (int)RenderQueue.Transparent;
                    break;

                case MaterialAlphaMode.MASK:
                    material.SetFloat(UniGLTF.UniUnlit.Utils.PropNameCutoff, src.alphaCutoff);
                    material.SetInt(UniGLTF.UniUnlit.Utils.PropNameBlendMode, (int)BlendMode.Cutout);
                    material.SetInt(UniGLTF.UniUnlit.Utils.PropNameSrcBlend, (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt(UniGLTF.UniUnlit.Utils.PropNameDstBlend, (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt(UniGLTF.UniUnlit.Utils.PropNameZWrite, 1);
                    material.EnableKeyword(UniGLTF.UniUnlit.Utils.KeywordAlphaTestOn);
                    material.DisableKeyword(UniGLTF.UniUnlit.Utils.KeywordAlphaBlendOn);
                    material.renderQueue = (int)RenderQueue.AlphaTest;
                    break;

                case MaterialAlphaMode.OPAQUE:
                default:
                    material.SetInt(UniGLTF.UniUnlit.Utils.PropNameBlendMode, (int)BlendMode.Opaque);
                    material.SetInt(UniGLTF.UniUnlit.Utils.PropNameSrcBlend, (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt(UniGLTF.UniUnlit.Utils.PropNameDstBlend, (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt(UniGLTF.UniUnlit.Utils.PropNameZWrite, 1);
                    material.DisableKeyword(UniGLTF.UniUnlit.Utils.KeywordAlphaTestOn);
                    material.DisableKeyword(UniGLTF.UniUnlit.Utils.KeywordAlphaBlendOn);
                    material.renderQueue = -1;
                    break;
            }

            return material;
        }

        #endregion

        /// <summary>
        /// Create a MToon material.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="src"></param>
        /// <returns></returns>
        protected virtual Material CreateMtoonMaterial(int i, glTFMaterial src)
        {
            Shader shader = Shader.Find(MToon.Utils.ShaderName);

            Material material = new Material(shader);

            material.name = CreateMaterialName(i, src);

#if UNITY_EDITOR
            material.hideFlags = HideFlags.DontUnloadUnusedAsset;
#endif

            MToonDefinition mtoonDefinition = CreateMtoonDefinition(src.extensions.VRMC_materials_mtoon);

            MToon.Utils.SetMToonParametersToMaterial(material, mtoonDefinition);

            return material;
        }

        /// <summary>
        /// Create a MToon definition.
        /// </summary>
        /// <param name="mtoon"></param>
        /// <returns></returns>
        protected virtual MToonDefinition CreateMtoonDefinition(VRMC_materials_mtoon mtoon)
        {
            MToonDefinition mtoonDefinition = new MToonDefinition();

            // Meta
            mtoonDefinition.Meta = new MetaDefinition()
            {
                VersionNumber = MToon.Utils.VersionNumber,
                Implementation = MToon.Utils.Implementation,
            };

            // Rendering
            mtoonDefinition.Rendering = new RenderingDefinition()
            {
                RenderMode = (MToon.RenderMode)mtoon.renderMode,
                CullMode = (MToon.CullMode)mtoon.cullMode,
                RenderQueueOffsetNumber = mtoon.renderQueueOffsetNumber,
            };

            // Color
            mtoonDefinition.Color = new ColorDefinition()
            {
                LitColor = ArrayConverter.ToColor(mtoon.litFactor, gamma: true),
                LitMultiplyTexture = GetTexture(MToon.Utils.PropMainTex, mtoon.litMultiplyTexture),
                ShadeColor = ArrayConverter.ToColor(mtoon.shadeFactor, gamma: true),
                ShadeMultiplyTexture = GetTexture(MToon.Utils.PropShadeTexture, mtoon.shadeMultiplyTexture),
                CutoutThresholdValue = mtoon.cutoutThresholdFactor,
            };

            // Lighting
            mtoonDefinition.Lighting = new LightingDefinition();
            mtoonDefinition.Lighting.LitAndShadeMixing = new LitAndShadeMixingDefinition()
            {
                ShadingShiftValue = mtoon.shadingShiftFactor,
                ShadingToonyValue = mtoon.shadingToonyFactor,
                ShadowReceiveMultiplierValue = mtoon.shadowReceiveMultiplierFactor,
                ShadowReceiveMultiplierMultiplyTexture = GetTexture(MToon.Utils.PropReceiveShadowTexture, mtoon.shadowReceiveMultiplierMultiplyTexture),
                LitAndShadeMixingMultiplierValue = mtoon.litAndShadeMixingMultiplierFactor,
                LitAndShadeMixingMultiplierMultiplyTexture = GetTexture(MToon.Utils.PropShadingGradeTexture, mtoon.litAndShadeMixingMultiplierMultiplyTexture),
            };
            mtoonDefinition.Lighting.LightingInfluence = new LightingInfluenceDefinition()
            {
                LightColorAttenuationValue = mtoon.lightColorAttenuationFactor,
                GiIntensityValue = mtoon.giIntensityFactor,
            };
            mtoonDefinition.Lighting.Normal = new NormalDefinition()
            {
                NormalTexture = GetTexture(MToon.Utils.PropBumpMap, mtoon.normalTexture),
                NormalScaleValue = mtoon.normalScaleFactor,
            };

            // Emission
            mtoonDefinition.Emission = new EmissionDefinition()
            {
                EmissionColor = ArrayConverter.ToColor(mtoon.emissionFactor, gamma: true),
                EmissionMultiplyTexture = GetTexture(MToon.Utils.PropEmissionMap, mtoon.emissionMultiplyTexture),
            };

            // MatCap
            mtoonDefinition.MatCap = new MatCapDefinition()
            {
                AdditiveTexture = GetTexture(MToon.Utils.PropSphereAdd, mtoon.additiveTexture),
            };

            // Rim
            mtoonDefinition.Rim = new RimDefinition()
            {
                RimColor = ArrayConverter.ToColor(mtoon.rimFactor, gamma: true),
                RimMultiplyTexture = GetTexture(MToon.Utils.PropRimTexture, mtoon.rimMultiplyTexture),
                RimLightingMixValue = mtoon.rimLightingMixFactor,
                RimFresnelPowerValue = mtoon.rimFresnelPowerFactor,
                RimLiftValue = mtoon.rimLiftFactor,
            };

            // Outline
            mtoonDefinition.Outline = new OutlineDefinition()
            {
                OutlineWidthMode = (MToon.OutlineWidthMode)mtoon.outlineWidthMode,
                OutlineWidthValue = mtoon.outlineWidthFactor,
                OutlineWidthMultiplyTexture = GetTexture(MToon.Utils.PropOutlineWidthTexture, mtoon.outlineWidthMultiplyTexture),
                OutlineScaledMaxDistanceValue = mtoon.outlineScaledMaxDistanceFactor,
                OutlineColorMode = (MToon.OutlineColorMode)mtoon.outlineColorMode,
                OutlineColor = ArrayConverter.ToColor(mtoon.outlineFactor, gamma: true),
                OutlineLightingMixValue = mtoon.outlineLightingMixFactor,
            };

            // Texture Option
            mtoonDefinition.TextureOption = new TextureUvCoordsDefinition()
            {
                MainTextureLeftBottomOriginScale = ArrayConverter.ToVector2(mtoon.mainTextureLeftBottomOriginScale),
                MainTextureLeftBottomOriginOffset = ArrayConverter.ToVector2(mtoon.mainTextureLeftBottomOriginOffset),
                UvAnimationMaskTexture = GetTexture(MToon.Utils.PropUvAnimMaskTexture, mtoon.uvAnimationMaskTexture),
                UvAnimationScrollXSpeedValue = mtoon.uvAnimationScrollXSpeedFactor,
                UvAnimationScrollYSpeedValue = mtoon.uvAnimationScrollYSpeedFactor,
                UvAnimationRotationSpeedValue = mtoon.uvAnimationRotationSpeedFactor,
            };

            return mtoonDefinition;
        }

        /// <summary>
        /// Create material name.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="src"></param>
        /// <returns></returns>
        protected virtual string CreateMaterialName(int i, glTFMaterial src)
        {
            if ((src == null) || string.IsNullOrEmpty(src.name))
            {
                return string.Format("material_{0:00}", i);
            }
            return src.name;
        }

        /// <summary>
        /// Get the texture.
        /// </summary>
        /// <param name="shaderPropertyName"></param>
        /// <param name="textureIndex"></param>
        /// <returns></returns>
        protected virtual Texture2D GetTexture(string shaderPropertyName, int textureIndex)
        {
            TextureItem textureItem = Context.GetTexture(textureIndex);

            if (textureItem == null)
            {
                return null;
            }

            Texture2D texture2D = textureItem.ConvertTexture(shaderPropertyName);

            if (texture2D == null)
            {
                return textureItem.Texture;
            }

            return texture2D;
        }
    }
}
