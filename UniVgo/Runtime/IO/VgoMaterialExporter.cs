// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : VgoMaterialExporter
// ----------------------------------------------------------------------
namespace UniVgo
{
    using MToon;
    using UniGLTFforUniVgo;
    using UnityEngine;

    /// <summary>
    /// VGO Material Exporter
    /// </summary>
    public class VgoMaterialExporter : MaterialExporter
    {
        #region Public Methods

        /// <summary>
        /// Export the material.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="textureManager"></param>
        /// <returns></returns>
        public override glTFMaterial ExportMaterial(Material m, TextureExportManager textureManager)
        {
            if (m.shader.name == ShaderName.VRM_MToon)
            {
                return CreateVrmMtoonMaterial(m, textureManager);
            }

            return base.ExportMaterial(m, textureManager);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Create a material.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        protected override glTFMaterial CreateMaterial(Material m)
        {
            switch (m.shader.name)
            {
                case ShaderName.VRM_UnlitTexture:
                    return CreateVrmUnlitMaterial(m, MToon.RenderMode.Opaque);

                case ShaderName.VRM_UnlitTransparent:
                    return CreateVrmUnlitMaterial(m, MToon.RenderMode.Transparent);

                case ShaderName.VRM_UnlitCutout:
                    return CreateVrmUnlitMaterial(m, MToon.RenderMode.Cutout);

                case ShaderName.VRM_UnlitTransparentZWrite:
                    return CreateVrmUnlitMaterial(m, MToon.RenderMode.TransparentWithZWrite);

                default:
                    return base.CreateMaterial(m);
            }
        }

        /// <summary>
        /// Create a VRM default material.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="renderMode"></param>
        /// <returns></returns>
        protected virtual glTFMaterial CreateVrmDefaultMaterial(Material m, MToon.RenderMode renderMode)
        {
            var material = new glTFMaterial();

            material.name = m.name;

            // Alpha Mode
            switch (renderMode)
            {
                case MToon.RenderMode.Opaque:
                    material.alphaMode = glTFBlendMode.OPAQUE.ToString();
                    break;

                case MToon.RenderMode.Cutout:
                    material.alphaMode = glTFBlendMode.MASK.ToString();
                    break;

                case MToon.RenderMode.Transparent:
                case MToon.RenderMode.TransparentWithZWrite:
                    material.alphaMode = glTFBlendMode.BLEND.ToString();
                    break;

                default:
                    break;
            }

            // Alpha Cutoff
            if (renderMode == MToon.RenderMode.Cutout)
            {
                material.alphaCutoff = m.GetFloat(MToon.Utils.PropCutoff);
            }

            return material;
        }

        /// <summary>
        /// Create a VRM Unlit material.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="renderMode"></param>
        /// <returns></returns>
        protected virtual glTFMaterial CreateVrmUnlitMaterial(Material m, MToon.RenderMode renderMode)
        {
            var material = CreateVrmDefaultMaterial(m, renderMode);

            // PBR Metallic Roughness
            material.pbrMetallicRoughness = new glTFPbrMetallicRoughness();

            // extensions
            material.extensions = new glTFMaterial_extensions()
            {
                KHR_materials_unlit = new glTF_KHR_materials_unlit(),
                VGO_materials = new glTF_VGO_materials()
                {
                    shaderName = m.shader.name,
                }
            };

            return material;
        }

        /// <summary>
        /// Create a VRM MToon material.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="textureManager"></param>
        /// <returns></returns>
        protected virtual glTFMaterial CreateVrmMtoonMaterial(Material m, TextureExportManager textureManager)
        {
            MToonDefinition mtoonDefinition = MToon.Utils.GetMToonParametersFromMaterial(m);

            var mtoon = new glTF_VRMC_materials_mtoon()
            {
                // Meta
                version = mtoonDefinition.Meta.VersionNumber.ToString(),

                // Rendering
                renderMode = (MToonRenderMode)mtoonDefinition.Rendering.RenderMode,
                cullMode = (MToonCullMode)mtoonDefinition.Rendering.CullMode,
                renderQueueOffsetNumber = mtoonDefinition.Rendering.RenderQueueOffsetNumber,

                // Color
                litFactor = mtoonDefinition.Color.LitColor.linear.ToArray(),
                litMultiplyTexture = -1,
                shadeFactor = mtoonDefinition.Color.ShadeColor.linear.ToArray(),
                shadeMultiplyTexture = -1,
                cutoutThresholdFactor = mtoonDefinition.Color.CutoutThresholdValue,

                // Lighting
                shadingShiftFactor = mtoonDefinition.Lighting.LitAndShadeMixing.ShadingShiftValue,
                shadingToonyFactor = mtoonDefinition.Lighting.LitAndShadeMixing.ShadingToonyValue,
                shadowReceiveMultiplierFactor = mtoonDefinition.Lighting.LitAndShadeMixing.ShadowReceiveMultiplierValue,
                shadowReceiveMultiplierMultiplyTexture = -1,
                litAndShadeMixingMultiplierFactor = mtoonDefinition.Lighting.LitAndShadeMixing.LitAndShadeMixingMultiplierValue,
                litAndShadeMixingMultiplierMultiplyTexture = -1,
                lightColorAttenuationFactor = mtoonDefinition.Lighting.LightingInfluence.LightColorAttenuationValue,
                giIntensityFactor = mtoonDefinition.Lighting.LightingInfluence.GiIntensityValue,
                normalTexture = -1,
                normalScaleFactor = mtoonDefinition.Lighting.Normal.NormalScaleValue,

                // Emission
                emissionFactor = mtoonDefinition.Emission.EmissionColor.linear.ToArray(),
                emissionMultiplyTexture = -1,

                // MatCap
                additiveTexture = -1,

                // Rim
                rimFactor = mtoonDefinition.Rim.RimColor.linear.ToArray(),
                rimMultiplyTexture = -1,
                rimLightingMixFactor = mtoonDefinition.Rim.RimLightingMixValue,
                rimFresnelPowerFactor = mtoonDefinition.Rim.RimFresnelPowerValue,
                rimLiftFactor = mtoonDefinition.Rim.RimLiftValue,

                // Outline
                outlineWidthMode = (MToonOutlineWidthMode)mtoonDefinition.Outline.OutlineWidthMode,
                outlineWidthFactor = mtoonDefinition.Outline.OutlineWidthValue,
                outlineWidthMultiplyTexture = -1,
                outlineScaledMaxDistanceFactor = mtoonDefinition.Outline.OutlineScaledMaxDistanceValue,
                outlineColorMode = (MToonOutlineColorMode)mtoonDefinition.Outline.OutlineColorMode,
                outlineFactor = mtoonDefinition.Outline.OutlineColor.linear.ToArray(),
                outlineLightingMixFactor = mtoonDefinition.Outline.OutlineLightingMixValue,

                // TextureOption
                mainTextureLeftBottomOriginScale = new float[]
                {
                    mtoonDefinition.TextureOption.MainTextureLeftBottomOriginScale.x,
                    mtoonDefinition.TextureOption.MainTextureLeftBottomOriginScale.y
                },
                mainTextureLeftBottomOriginOffset = new float[]
                {
                    mtoonDefinition.TextureOption.MainTextureLeftBottomOriginOffset.x,
                    mtoonDefinition.TextureOption.MainTextureLeftBottomOriginOffset.y
                },
                uvAnimationMaskTexture = -1,
                uvAnimationScrollXSpeedFactor = mtoonDefinition.TextureOption.UvAnimationScrollXSpeedValue,
                uvAnimationScrollYSpeedFactor = mtoonDefinition.TextureOption.UvAnimationScrollYSpeedValue,
                uvAnimationRotationSpeedFactor = mtoonDefinition.TextureOption.UvAnimationRotationSpeedValue,
            };

            // Textures
            mtoon.litMultiplyTexture = textureManager.CopyAndGetIndex(mtoonDefinition.Color.LitMultiplyTexture, RenderTextureReadWrite.sRGB);
            mtoon.shadeMultiplyTexture = textureManager.CopyAndGetIndex(mtoonDefinition.Color.ShadeMultiplyTexture, RenderTextureReadWrite.sRGB);
            mtoon.shadowReceiveMultiplierMultiplyTexture = textureManager.CopyAndGetIndex(mtoonDefinition.Lighting.LitAndShadeMixing.ShadowReceiveMultiplierMultiplyTexture, RenderTextureReadWrite.sRGB);
            mtoon.litAndShadeMixingMultiplierMultiplyTexture = textureManager.CopyAndGetIndex(mtoonDefinition.Lighting.LitAndShadeMixing.LitAndShadeMixingMultiplierMultiplyTexture, RenderTextureReadWrite.sRGB);
            mtoon.normalTexture = textureManager.ConvertAndGetIndex(mtoonDefinition.Lighting.Normal.NormalTexture, new NormalConverter());
            mtoon.emissionMultiplyTexture = textureManager.CopyAndGetIndex(mtoonDefinition.Emission.EmissionMultiplyTexture, RenderTextureReadWrite.sRGB);
            mtoon.additiveTexture = textureManager.CopyAndGetIndex(mtoonDefinition.MatCap.AdditiveTexture, RenderTextureReadWrite.sRGB);
            mtoon.rimMultiplyTexture = textureManager.CopyAndGetIndex(mtoonDefinition.Rim.RimMultiplyTexture, RenderTextureReadWrite.sRGB);
            mtoon.outlineWidthMultiplyTexture = textureManager.CopyAndGetIndex(mtoonDefinition.Outline.OutlineWidthMultiplyTexture, RenderTextureReadWrite.sRGB);
            mtoon.uvAnimationMaskTexture = textureManager.CopyAndGetIndex(mtoonDefinition.TextureOption.UvAnimationMaskTexture, RenderTextureReadWrite.sRGB);

            var material = CreateVrmDefaultMaterial(m, mtoonDefinition.Rendering.RenderMode);

            // Double Sided
            switch (mtoon.cullMode)
            {
                case MToonCullMode.Off:
                    material.doubleSided = true;
                    break;

                case MToonCullMode.Front:
                case MToonCullMode.Back:
                    material.doubleSided = false;
                    break;

                default:
                    break;
            }

            // PBR Metallic Roughness
            {
                if (mtoon.litFactor != null)
                {
                    if (material.pbrMetallicRoughness == null)
                    {
                        material.pbrMetallicRoughness = new glTFPbrMetallicRoughness();
                    }

                    material.pbrMetallicRoughness.baseColorFactor = mtoon.litFactor;
                }

                if (mtoon.litMultiplyTexture != -1)
                {
                    if (material.pbrMetallicRoughness == null)
                    {
                        material.pbrMetallicRoughness = new glTFPbrMetallicRoughness();
                    }

                    material.pbrMetallicRoughness.baseColorTexture = new glTFMaterialBaseColorTextureInfo()
                    {
                        index = mtoon.litMultiplyTexture,
                    };

                    //material.pbrMetallicRoughness.metallicFactor = 1.0f;
                    //material.pbrMetallicRoughness.roughnessFactor = 1.0f;
                }
            }

            // Normal Texture
            if (mtoon.normalTexture != -1)
            {
                material.normalTexture = new glTFMaterialNormalTextureInfo()
                {
                    index = mtoon.normalTexture,
                    scale = mtoon.normalScaleFactor,
                };
            }

            // Emissive
            {
                material.emissiveFactor = mtoon.emissionFactor;

                if (mtoon.emissionMultiplyTexture != -1)
                {
                    material.emissiveTexture = new glTFMaterialEmissiveTextureInfo()
                    {
                        index = mtoon.emissionMultiplyTexture,
                    };
                }
            }

            // Extensions
            material.extensions = new glTFMaterial_extensions()
            {
                VRMC_materials_mtoon = mtoon,
                VGO_materials = new glTF_VGO_materials()
                {
                    shaderName = m.shader.name,
                }
            };

            return material;
        }

        #endregion
    }
}
