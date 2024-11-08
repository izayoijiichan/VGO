﻿// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : VgoMaterialMToon0xDefinitionExtensions
// ----------------------------------------------------------------------
#nullable enable
#if UNIVGO_ENABLE_MTOON_0_0
namespace UniVgo2
{
    using MToon;
    using NewtonVgo;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Vgo Material MToon 0x Definition Extensions
    /// </summary>
    public static class VgoMaterialMToon0xDefinitionExtensions
    {
        /// <summary>
        /// Convert vgo material to MToon 0x definition.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="allTextureList">List of all texture.</param>
        /// <returns>A MToon 0x definition.</returns>
        public static MToonDefinition ToMToon0xDefinition(this VgoMaterial vgoMaterial, in List<Texture?> allTextureList)
        {
            if (vgoMaterial.shaderName != ShaderName.VRM_MToon)
            {
                ThrowHelper.ThrowException($"vgoMaterial.shaderName: {vgoMaterial.shaderName}");
            }

            var mtoonDefinition = new MToonDefinition();

            // Meta
            mtoonDefinition.Meta = new MetaDefinition
            {
                VersionNumber = MToon.Utils.VersionNumber,
                Implementation = MToon.Utils.Implementation,
            };

            // Rendering
            mtoonDefinition.Rendering = new RenderingDefinition
            {
                RenderMode = vgoMaterial.GetEnumOrDefault<MToon.RenderMode>(MToon.Utils.PropBlendMode, MToon.RenderMode.Opaque),
                CullMode = vgoMaterial.GetEnumOrDefault<MToon.CullMode>(MToon.Utils.PropCullMode, MToon.CullMode.Back),
                //RenderQueueOffsetNumber = vrmcMtoon.renderQueueOffsetNumber,
            };

            // Color
            mtoonDefinition.Color = new ColorDefinition
            {
                LitColor = vgoMaterial.GetColorOrDefault(MToon.Utils.PropColor, Color.white).gamma,
                LitMultiplyTexture = allTextureList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(MToon.Utils.PropMainTex)) as Texture2D,
                ShadeColor = vgoMaterial.GetColorOrDefault(MToon.Utils.PropShadeColor, Color.black).gamma,
                ShadeMultiplyTexture = allTextureList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(MToon.Utils.PropShadeTexture)) as Texture2D,
                CutoutThresholdValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropCutoff, 0.0f, 1.0f, 0.5f),
            };

            // Lighting
            mtoonDefinition.Lighting = new LightingDefinition();

            // LitAndShadeMixing
            mtoonDefinition.Lighting.LitAndShadeMixing = new LitAndShadeMixingDefinition
            {
                ShadingShiftValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropShadeShift, -1.0f, 1.0f, 0.0f),
                ShadingToonyValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropShadeToony, 0.0f, 1.0f, 0.9f),
                ShadowReceiveMultiplierValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropReceiveShadowRate, 0.0f, 1.0f, 1.0f),
                ShadowReceiveMultiplierMultiplyTexture = allTextureList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(MToon.Utils.PropReceiveShadowTexture)) as Texture2D,
                LitAndShadeMixingMultiplierValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropShadingGradeRate, 0.0f, 1.0f, 1.0f),
                LitAndShadeMixingMultiplierMultiplyTexture = allTextureList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(MToon.Utils.PropShadingGradeTexture)) as Texture2D,
            };

            // LightingInfluence
            mtoonDefinition.Lighting.LightingInfluence = new LightingInfluenceDefinition
            {
                LightColorAttenuationValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropLightColorAttenuation, 0.0f, 1.0f, 0.0f),
                GiIntensityValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropIndirectLightIntensity, 0.0f, 1.0f, 0.1f),
            };

            // Normal
            mtoonDefinition.Lighting.Normal = new NormalDefinition
            {
                NormalTexture = allTextureList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(MToon.Utils.PropBumpMap)) as Texture2D,
                NormalScaleValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropBumpScale, 0.0f, float.MaxValue, 1.0f),
            };

            // Emission
            mtoonDefinition.Emission = new EmissionDefinition
            {
                EmissionColor = vgoMaterial.GetColorOrDefault(MToon.Utils.PropEmissionColor, Color.black).gamma,
                EmissionMultiplyTexture = allTextureList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(MToon.Utils.PropEmissionMap)) as Texture2D,
            };

            // MatCap
            mtoonDefinition.MatCap = new MatCapDefinition
            {
                AdditiveTexture = allTextureList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(MToon.Utils.PropSphereAdd)) as Texture2D,
            };

            // Rim
            mtoonDefinition.Rim = new RimDefinition()
            {
                RimColor = vgoMaterial.GetColorOrDefault(MToon.Utils.PropRimColor, Color.black).gamma,
                RimMultiplyTexture = allTextureList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(MToon.Utils.PropRimTexture)) as Texture2D,
                RimLightingMixValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropRimLightingMix, 0.0f, 1.0f, 0.0f),
                RimFresnelPowerValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropRimFresnelPower, 0.0f, 100.0f, 1.0f),
                RimLiftValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropRimLift, 0.0f, 1.0f, 0.0f),
            };

            // Outline
            mtoonDefinition.Outline = new OutlineDefinition()
            {
                OutlineWidthMode = vgoMaterial.GetEnumOrDefault<MToon.OutlineWidthMode>(MToon.Utils.PropOutlineWidthMode, MToon.OutlineWidthMode.None),
                OutlineWidthValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropOutlineWidth, 0.0f, 1.0f, 0.5f),
                OutlineWidthMultiplyTexture = allTextureList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(MToon.Utils.PropOutlineWidthTexture)) as Texture2D,
                OutlineScaledMaxDistanceValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropOutlineScaledMaxDistance, 1.0f, 10.0f, 1.0f),
                OutlineColorMode = vgoMaterial.GetEnumOrDefault<MToon.OutlineColorMode>(MToon.Utils.PropOutlineColorMode, MToon.OutlineColorMode.FixedColor),
                OutlineColor = vgoMaterial.GetColorOrDefault(MToon.Utils.PropOutlineColor, Color.black).gamma,
                OutlineLightingMixValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropOutlineLightingMix, 0.0f, 1.0f, 1.0f),
            };

            // Texture Option
            mtoonDefinition.TextureOption = new TextureUvCoordsDefinition()
            {
                MainTextureLeftBottomOriginScale = vgoMaterial.GetVector2OrDefault(MToon.Utils.PropMainTex, Vector2.one),
                MainTextureLeftBottomOriginOffset = vgoMaterial.GetVector2OrDefault(MToon.Utils.PropMainTex, Vector2.zero),
                UvAnimationMaskTexture = allTextureList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(MToon.Utils.PropUvAnimMaskTexture)) as Texture2D,
                UvAnimationScrollXSpeedValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropUvAnimScrollX, 0.0f, float.MaxValue, 0.0f),
                UvAnimationScrollYSpeedValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropUvAnimScrollY, 0.0f, float.MaxValue, 0.0f),
                UvAnimationRotationSpeedValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropUvAnimRotation, 0.0f, float.MaxValue, 0.0f),
            };

            return mtoonDefinition;
        }
    }
}
#endif