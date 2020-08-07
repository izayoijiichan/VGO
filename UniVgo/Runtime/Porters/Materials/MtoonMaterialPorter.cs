// ----------------------------------------------------------------------
// @Namespace : UniVgo.Porters
// @Class     : MtoonMaterialPorter
// ----------------------------------------------------------------------
namespace UniVgo.Porters
{
    using MToon;
    using NewtonGltf;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using VgoGltf;

    /// <summary>
    /// MToon Material Importer
    /// </summary>
    public class MtoonMaterialPorter : MaterialPorterBase
    {
        #region Constructors

        /// <summary>
        /// Create a new instance of MtoonMaterialPorter.
        /// </summary>
        public MtoonMaterialPorter() : base() { }

        #endregion

        #region Public Methods (Export)

        /// <summary>
        /// Create a glTF material.
        /// </summary>
        /// <param name="material">A MToon material.</param>
        /// <returns>A glTF material.</returns>
        public override GltfMaterial CreateGltfMaterial(Material material)
        {
            MToonDefinition definition = MToon.Utils.GetMToonParametersFromMaterial(material);

            var vrmcMtoon = new VRMC_materials_mtoon()
            {
                // Meta
                version = definition.Meta.VersionNumber.ToString(),

                // Rendering
                renderMode = (MToonRenderMode)definition.Rendering.RenderMode,
                cullMode = (MToonCullMode)definition.Rendering.CullMode,
                renderQueueOffsetNumber = definition.Rendering.RenderQueueOffsetNumber,

                // Color
                litFactor = definition.Color.LitColor.linear.ToGltfColor4(),
                litMultiplyTexture = -1,
                shadeFactor = definition.Color.ShadeColor.linear.ToGltfColor4(),
                shadeMultiplyTexture = -1,
                cutoutThresholdFactor = definition.Color.CutoutThresholdValue,

                // Lighting
                shadingShiftFactor = definition.Lighting.LitAndShadeMixing.ShadingShiftValue,
                shadingToonyFactor = definition.Lighting.LitAndShadeMixing.ShadingToonyValue,
                shadowReceiveMultiplierFactor = definition.Lighting.LitAndShadeMixing.ShadowReceiveMultiplierValue,
                shadowReceiveMultiplierMultiplyTexture = -1,
                litAndShadeMixingMultiplierFactor = definition.Lighting.LitAndShadeMixing.LitAndShadeMixingMultiplierValue,
                litAndShadeMixingMultiplierMultiplyTexture = -1,
                lightColorAttenuationFactor = definition.Lighting.LightingInfluence.LightColorAttenuationValue,
                giIntensityFactor = definition.Lighting.LightingInfluence.GiIntensityValue,
                normalTexture = -1,
                normalScaleFactor = definition.Lighting.Normal.NormalScaleValue,

                // Emission
                emissionFactor = definition.Emission.EmissionColor.linear.ToGltfColor3(),
                emissionMultiplyTexture = -1,

                // MatCap
                additiveTexture = -1,

                // Rim
                rimFactor = definition.Rim.RimColor.linear.ToGltfColor4(),
                rimMultiplyTexture = -1,
                rimLightingMixFactor = definition.Rim.RimLightingMixValue,
                rimFresnelPowerFactor = definition.Rim.RimFresnelPowerValue,
                rimLiftFactor = definition.Rim.RimLiftValue,

                // Outline
                outlineWidthMode = (MToonOutlineWidthMode)definition.Outline.OutlineWidthMode,
                outlineWidthFactor = definition.Outline.OutlineWidthValue,
                outlineWidthMultiplyTexture = -1,
                outlineScaledMaxDistanceFactor = definition.Outline.OutlineScaledMaxDistanceValue,
                outlineColorMode = (MToonOutlineColorMode)definition.Outline.OutlineColorMode,
                outlineFactor = definition.Outline.OutlineColor.linear.ToGltfColor4(),
                outlineLightingMixFactor = definition.Outline.OutlineLightingMixValue,

                // TextureOption
                mainTextureLeftBottomOriginScale = definition.TextureOption.MainTextureLeftBottomOriginScale.ToNumericsVector2(),
                mainTextureLeftBottomOriginOffset = definition.TextureOption.MainTextureLeftBottomOriginOffset.ToNumericsVector2(),
                uvAnimationMaskTexture = -1,
                uvAnimationScrollXSpeedFactor = definition.TextureOption.UvAnimationScrollXSpeedValue,
                uvAnimationScrollYSpeedFactor = definition.TextureOption.UvAnimationScrollYSpeedValue,
                uvAnimationRotationSpeedFactor = definition.TextureOption.UvAnimationRotationSpeedValue,
            };

            // Textures
            vrmcMtoon.litMultiplyTexture = ExportTexture(material, definition.Color.LitMultiplyTexture, Utils.PropMainTex);
            vrmcMtoon.shadeMultiplyTexture = ExportTexture(material, definition.Color.ShadeMultiplyTexture, Utils.PropShadeTexture);
            vrmcMtoon.shadowReceiveMultiplierMultiplyTexture = ExportTexture(material, definition.Lighting.LitAndShadeMixing.ShadowReceiveMultiplierMultiplyTexture, Utils.PropReceiveShadowTexture);
            vrmcMtoon.litAndShadeMixingMultiplierMultiplyTexture = ExportTexture(material, definition.Lighting.LitAndShadeMixing.LitAndShadeMixingMultiplierMultiplyTexture, Utils.PropShadingGradeTexture);
            vrmcMtoon.normalTexture = ExportTexture(material, definition.Lighting.Normal.NormalTexture, Utils.PropBumpMap, TextureType.NormalMap, ColorSpaceType.Linear);
            vrmcMtoon.emissionMultiplyTexture = ExportTexture(material, definition.Emission.EmissionMultiplyTexture, Utils.PropEmissionMap, TextureType.EmissionMap, ColorSpaceType.Srgb);
            vrmcMtoon.additiveTexture = ExportTexture(material, definition.MatCap.AdditiveTexture, Utils.PropSphereAdd);
            vrmcMtoon.rimMultiplyTexture = ExportTexture(material, definition.Rim.RimMultiplyTexture, Utils.PropRimTexture);
            vrmcMtoon.outlineWidthMultiplyTexture = ExportTexture(material, definition.Outline.OutlineWidthMultiplyTexture, Utils.PropOutlineWidthTexture);
            vrmcMtoon.uvAnimationMaskTexture = ExportTexture(material, definition.TextureOption.UvAnimationMaskTexture, Utils.PropUvAnimMaskTexture);

            GltfMaterial gltfMaterial = new GltfMaterial()
            {
                name = material.name,
            };

            // Alpha Mode
            switch (definition.Rendering.RenderMode)
            {
                case MToon.RenderMode.Opaque:
                    gltfMaterial.alphaMode = GltfAlphaMode.OPAQUE;
                    break;

                case MToon.RenderMode.Cutout:
                    gltfMaterial.alphaMode = GltfAlphaMode.MASK;
                    break;

                case MToon.RenderMode.Transparent:
                case MToon.RenderMode.TransparentWithZWrite:
                    gltfMaterial.alphaMode = GltfAlphaMode.BLEND;
                    break;

                default:
                    break;
            }

            // Alpha Cutoff
            if (definition.Rendering.RenderMode == MToon.RenderMode.Cutout)
            {
                gltfMaterial.alphaCutoff = definition.Color.CutoutThresholdValue;
            }

            // Double Sided
            switch (definition.Rendering.CullMode)
            {
                case MToon.CullMode.Off:
                    gltfMaterial.doubleSided = true;
                    break;
                case MToon.CullMode.Front:
                case MToon.CullMode.Back:
                    gltfMaterial.doubleSided = false;
                    break;
                default:
                    break;
            }

            // Base Color
            if (vrmcMtoon.litFactor != null)
            {
                if (gltfMaterial.pbrMetallicRoughness == null)
                {
                    gltfMaterial.pbrMetallicRoughness = new GltfMaterialPbrMetallicRoughness();
                }

                gltfMaterial.pbrMetallicRoughness.baseColorFactor = vrmcMtoon.litFactor;
            }

            // Main Texture
            if (vrmcMtoon.litMultiplyTexture != -1)
            {
                if (gltfMaterial.pbrMetallicRoughness == null)
                {
                    gltfMaterial.pbrMetallicRoughness = new GltfMaterialPbrMetallicRoughness();
                }

                gltfMaterial.pbrMetallicRoughness.baseColorTexture = new GltfTextureInfo
                {
                    index = vrmcMtoon.litMultiplyTexture,
                };
            }

            // Normal Map
            if (vrmcMtoon.normalTexture != -1)
            {
                gltfMaterial.normalTexture = new GltfNormalTextureInfo
                {
                    index = vrmcMtoon.normalTexture,
                    scale = vrmcMtoon.normalScaleFactor,
                };
            }

            // Emission Map
            if (vrmcMtoon.emissionMultiplyTexture != -1)
            {
                gltfMaterial.emissiveTexture = new GltfTextureInfo
                {
                    index = vrmcMtoon.emissionMultiplyTexture,
                };
            }
            if (vrmcMtoon.emissionFactor != null)
            {
                gltfMaterial.emissiveFactor = vrmcMtoon.emissionFactor;
            }

            // Extensions
            //  VGO_materials
            //  VRMC_materials_mtoon
            gltfMaterial.extensions = new GltfExtensions(_JsonSerializerSettings)
            {
                { VGO_materials.ExtensionName, new VGO_materials(material.shader.name) },
                { VRMC_materials_mtoon.ExtensionName, vrmcMtoon },
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

            if (gltfMaterial.extensions.Contains(VRMC_materials_mtoon.ExtensionName) == false)
            {
                throw new Exception($"{VRMC_materials_mtoon.ExtensionName} is not found.");
            }

            gltfMaterial.extensions.JsonSerializerSettings = _JsonSerializerSettings;

            VRMC_materials_mtoon vrmcMtoon = gltfMaterial.extensions.GetValueOrDefault<VRMC_materials_mtoon>(VRMC_materials_mtoon.ExtensionName);

            // Main Texture
            if (vrmcMtoon.litMultiplyTexture != -1)
            {
                if (TryGetTextureAndSetInfo(vrmcMtoon.litMultiplyTexture, out TextureInfo textureInfo))
                {
                    materialInfo.TextureInfoList.TryAdd(textureInfo);
                }
            }

            if (vrmcMtoon.shadeMultiplyTexture != -1)
            {
                if (TryGetTextureAndSetInfo(vrmcMtoon.shadeMultiplyTexture, out TextureInfo textureInfo))
                {
                    materialInfo.TextureInfoList.TryAdd(textureInfo);
                }
            }

            if (vrmcMtoon.shadowReceiveMultiplierMultiplyTexture != -1)
            {
                if (TryGetTextureAndSetInfo(vrmcMtoon.shadowReceiveMultiplierMultiplyTexture, out TextureInfo textureInfo))
                {
                    materialInfo.TextureInfoList.TryAdd(textureInfo);
                }
            }

            if (vrmcMtoon.litAndShadeMixingMultiplierMultiplyTexture != -1)
            {
                if (TryGetTextureAndSetInfo(vrmcMtoon.litAndShadeMixingMultiplierMultiplyTexture, out TextureInfo textureInfo))
                {
                    materialInfo.TextureInfoList.TryAdd(textureInfo);
                }
            }

            // Normal Map
            if (vrmcMtoon.normalTexture != -1)
            {
                if (TryGetTextureAndSetInfo(vrmcMtoon.normalTexture, TextureType.NormalMap, out TextureInfo textureInfo))
                {
                    textureInfo.normalTextureScale = vrmcMtoon.normalScaleFactor.SafeValue(0.0f, 1.0f, 1.0f);

                    materialInfo.TextureInfoList.TryAdd(textureInfo);
                }
            }

            // Emission Map
            if (vrmcMtoon.emissionMultiplyTexture != -1)
            {
                if (TryGetTextureAndSetInfo(vrmcMtoon.emissionMultiplyTexture, TextureType.EmissionMap, out TextureInfo textureInfo))
                {
                    materialInfo.TextureInfoList.TryAdd(textureInfo);
                }
            }

            if (vrmcMtoon.additiveTexture != -1)
            {
                if (TryGetTextureAndSetInfo(vrmcMtoon.additiveTexture, out TextureInfo textureInfo))
                {
                    materialInfo.TextureInfoList.TryAdd(textureInfo);
                }
            }

            if (vrmcMtoon.rimMultiplyTexture != -1)
            {
                if (TryGetTextureAndSetInfo(vrmcMtoon.rimMultiplyTexture, out TextureInfo textureInfo))
                {
                    materialInfo.TextureInfoList.TryAdd(textureInfo);
                }
            }

            if (vrmcMtoon.outlineWidthMultiplyTexture != -1)
            {
                if (TryGetTextureAndSetInfo(vrmcMtoon.outlineWidthMultiplyTexture, out TextureInfo textureInfo))
                {
                    materialInfo.TextureInfoList.TryAdd(textureInfo);
                }
            }

            if (vrmcMtoon.uvAnimationMaskTexture != -1)
            {
                if (TryGetTextureAndSetInfo(vrmcMtoon.uvAnimationMaskTexture, out TextureInfo textureInfo))
                {
                    materialInfo.TextureInfoList.TryAdd(textureInfo);
                }
            }
        }

        /// <summary>
        /// Create a MToon material.
        /// </summary>
        /// <param name="materialInfo">A material info.</param>
        /// <param name="shader">A MToon shader.</param>
        /// <returns>A MToon material.</returns>
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

            MToonDefinition mtoonDefinition = CreateMToonDefinition(materialInfo);

            MToon.Utils.SetMToonParametersToMaterial(material, mtoonDefinition);

            //SetMaterialTextureTransforms(material, materialInfo);

            return material;
        }

        #endregion

        #region Protected Methods (Import)

        /// <summary>
        /// Create a MToon definition.
        /// </summary>
        /// <param name="materialInfo">A material info.</param>
        /// <returns>A MToon definition.</returns>
        protected virtual MToonDefinition CreateMToonDefinition(MaterialInfo materialInfo)
        {
            GltfMaterial gltfMaterial = materialInfo.gltfMaterial;

            if (gltfMaterial.extensions.Contains(VRMC_materials_mtoon.ExtensionName) == false)
            {
                throw new Exception($"{VRMC_materials_mtoon.ExtensionName} is not found.");
            }

            gltfMaterial.extensions.JsonSerializerSettings = _JsonSerializerSettings;

            VRMC_materials_mtoon vrmcMtoon = gltfMaterial.extensions.GetValueOrDefault<VRMC_materials_mtoon>(VRMC_materials_mtoon.ExtensionName);

            MToonDefinition mtoonDefinition = new MToonDefinition();

            // Meta
            mtoonDefinition.Meta = new MetaDefinition
            {
                VersionNumber = MToon.Utils.VersionNumber,
                Implementation = MToon.Utils.Implementation,
            };

            // Rendering
            mtoonDefinition.Rendering = new RenderingDefinition
            {
                RenderMode = (MToon.RenderMode)vrmcMtoon.renderMode,
                CullMode = (MToon.CullMode)vrmcMtoon.cullMode,
                RenderQueueOffsetNumber = vrmcMtoon.renderQueueOffsetNumber,
            };

            // Color
            mtoonDefinition.Color = new ColorDefinition
            {
                LitColor = vrmcMtoon.litFactor.GetValueOrDefault(Color4.White).ToUnityColor().gamma,
                LitMultiplyTexture = AllTexture2dList.GetValueOrDefault(vrmcMtoon.litMultiplyTexture),
                ShadeColor = vrmcMtoon.shadeFactor.GetValueOrDefault(Color4.Black).ToUnityColor().gamma,
                ShadeMultiplyTexture = AllTexture2dList.GetValueOrDefault(vrmcMtoon.shadeMultiplyTexture),
                CutoutThresholdValue = vrmcMtoon.cutoutThresholdFactor.SafeValue(0.0f, 1.0f, 0.5f),
            };

            // Lighting
            mtoonDefinition.Lighting = new LightingDefinition();
            mtoonDefinition.Lighting.LitAndShadeMixing = new LitAndShadeMixingDefinition
            {
                ShadingShiftValue = vrmcMtoon.shadingShiftFactor.SafeValue(-1.0f, 1.0f, 0.0f),
                ShadingToonyValue = vrmcMtoon.shadingToonyFactor.SafeValue(0.0f, 1.0f, 0.9f),
                ShadowReceiveMultiplierValue = vrmcMtoon.shadowReceiveMultiplierFactor.SafeValue(0.0f, 1.0f, 1.0f),
                ShadowReceiveMultiplierMultiplyTexture = AllTexture2dList.GetValueOrDefault(vrmcMtoon.shadowReceiveMultiplierMultiplyTexture),
                LitAndShadeMixingMultiplierValue = vrmcMtoon.litAndShadeMixingMultiplierFactor.SafeValue(0.0f, 1.0f, 1.0f),
                LitAndShadeMixingMultiplierMultiplyTexture = AllTexture2dList.GetValueOrDefault(vrmcMtoon.litAndShadeMixingMultiplierMultiplyTexture),
            };
            mtoonDefinition.Lighting.LightingInfluence = new LightingInfluenceDefinition
            {
                LightColorAttenuationValue = vrmcMtoon.lightColorAttenuationFactor.SafeValue(0.0f, 1.0f, 0.0f),
                GiIntensityValue = vrmcMtoon.giIntensityFactor.SafeValue(0.0f, 1.0f, 0.1f),
            };
            mtoonDefinition.Lighting.Normal = new NormalDefinition
            {
                NormalTexture = AllTexture2dList.GetValueOrDefault(vrmcMtoon.normalTexture),
                NormalScaleValue = vrmcMtoon.normalScaleFactor.SafeValue(0.0f, 1.0f, 1.0f),
            };

            // Emission
            mtoonDefinition.Emission = new EmissionDefinition
            {
                EmissionColor = vrmcMtoon.emissionFactor.GetValueOrDefault(Color3.Black).ToUnityColor().gamma,
                EmissionMultiplyTexture = AllTexture2dList.GetValueOrDefault(vrmcMtoon.emissionMultiplyTexture),
            };

            // MatCap
            mtoonDefinition.MatCap = new MatCapDefinition
            {
                AdditiveTexture = AllTexture2dList.GetValueOrDefault(vrmcMtoon.additiveTexture),
            };

            // Rim
            mtoonDefinition.Rim = new RimDefinition()
            {
                RimColor = vrmcMtoon.rimFactor.GetValueOrDefault(Color4.Black).ToUnityColor().gamma,
                RimMultiplyTexture = AllTexture2dList.GetValueOrDefault(vrmcMtoon.rimMultiplyTexture),
                RimLightingMixValue = vrmcMtoon.rimLightingMixFactor.SafeValue(0.0f, 1.0f, 0.0f),
                RimFresnelPowerValue = vrmcMtoon.rimFresnelPowerFactor.SafeValue(0.0f, 100.0f, 1.0f),
                RimLiftValue = vrmcMtoon.rimLiftFactor.SafeValue(0.0f, 1.0f, 0.0f),
            };

            // Outline
            mtoonDefinition.Outline = new OutlineDefinition()
            {
                OutlineWidthMode = (MToon.OutlineWidthMode)vrmcMtoon.outlineWidthMode,
                OutlineWidthValue = vrmcMtoon.outlineWidthFactor.SafeValue(0.0f, 1.0f, 0.5f),
                OutlineWidthMultiplyTexture = AllTexture2dList.GetValueOrDefault(vrmcMtoon.outlineWidthMultiplyTexture),
                OutlineScaledMaxDistanceValue = vrmcMtoon.outlineScaledMaxDistanceFactor.SafeValue(1.0f, 10.0f, 1.0f),
                OutlineColorMode = (MToon.OutlineColorMode)vrmcMtoon.outlineColorMode,
                OutlineColor = vrmcMtoon.outlineFactor.GetValueOrDefault(Color4.Black).ToUnityColor().gamma,
                OutlineLightingMixValue = vrmcMtoon.outlineLightingMixFactor.SafeValue(0.0f, 1.0f, 1.0f),
            };

            // Texture Option
            mtoonDefinition.TextureOption = new TextureUvCoordsDefinition()
            {
                MainTextureLeftBottomOriginScale = vrmcMtoon.mainTextureLeftBottomOriginScale.GetValueOrDefault(System.Numerics.Vector2.One).ToUnityVector2(),
                MainTextureLeftBottomOriginOffset = vrmcMtoon.mainTextureLeftBottomOriginOffset.GetValueOrDefault(System.Numerics.Vector2.Zero).ToUnityVector2(),
                UvAnimationMaskTexture = AllTexture2dList.GetValueOrDefault(vrmcMtoon.uvAnimationMaskTexture),
                UvAnimationScrollXSpeedValue = vrmcMtoon.uvAnimationScrollXSpeedFactor.SafeValue(0.0f, float.MaxValue, 0.0f),
                UvAnimationScrollYSpeedValue = vrmcMtoon.uvAnimationScrollYSpeedFactor.SafeValue(0.0f, float.MaxValue, 0.0f),
                UvAnimationRotationSpeedValue = vrmcMtoon.uvAnimationRotationSpeedFactor.SafeValue(0.0f, float.MaxValue, 0.0f),
            };

            return mtoonDefinition;
        }

        #endregion
    }
}
