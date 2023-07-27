﻿// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : VgoMaterialMToon0xDefinitionExtensions
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using MToon;
    using NewtonVgo;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Vgo Material MToon 0x Definition Extensions
    /// </summary>
    public static class VgoMaterialMToon0XDefinitionExtensions
    {
        /// <summary>
        /// Convert vgo material to MToon 0x definition.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="allTexture2dList">List of all texture 2D.</param>
        /// <returns>A MToon 0x definition.</returns>
        public static MToonDefinition ToMToon0xDefinition(this VgoMaterial vgoMaterial, in List<Texture2D?> allTexture2dList)
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
                RenderMode = (MToon.RenderMode)vgoMaterial.GetIntOrDefault(MToon.Utils.PropBlendMode),
                CullMode = (MToon.CullMode)vgoMaterial.GetIntOrDefault(MToon.Utils.PropCullMode),
                //RenderQueueOffsetNumber = vrmcMtoon.renderQueueOffsetNumber,
            };

            // Color
            mtoonDefinition.Color = new ColorDefinition
            {
                LitColor = vgoMaterial.GetColorOrDefault(MToon.Utils.PropColor, Color.white).gamma,
                LitMultiplyTexture = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(MToon.Utils.PropMainTex)),
                ShadeColor = vgoMaterial.GetColorOrDefault(MToon.Utils.PropShadeColor, Color.black).gamma,
                ShadeMultiplyTexture = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(MToon.Utils.PropShadeTexture)),
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
                ShadowReceiveMultiplierMultiplyTexture = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(MToon.Utils.PropReceiveShadowTexture)),
                LitAndShadeMixingMultiplierValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropShadingGradeRate, 0.0f, 1.0f, 1.0f),
                LitAndShadeMixingMultiplierMultiplyTexture = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(MToon.Utils.PropShadingGradeTexture)),
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
                NormalTexture = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(MToon.Utils.PropBumpMap)),
                NormalScaleValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropBumpScale, 0.0f, float.MaxValue, 1.0f),
            };

            // Emission
            mtoonDefinition.Emission = new EmissionDefinition
            {
                EmissionColor = vgoMaterial.GetColorOrDefault(MToon.Utils.PropEmissionColor, Color.black).gamma,
                EmissionMultiplyTexture = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(MToon.Utils.PropEmissionMap)),
            };

            // MatCap
            mtoonDefinition.MatCap = new MatCapDefinition
            {
                AdditiveTexture = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(MToon.Utils.PropSphereAdd)),
            };

            // Rim
            mtoonDefinition.Rim = new RimDefinition()
            {
                RimColor = vgoMaterial.GetColorOrDefault(MToon.Utils.PropRimColor, Color.black).gamma,
                RimMultiplyTexture = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(MToon.Utils.PropRimTexture)),
                RimLightingMixValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropRimLightingMix, 0.0f, 1.0f, 0.0f),
                RimFresnelPowerValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropRimFresnelPower, 0.0f, 100.0f, 1.0f),
                RimLiftValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropRimLift, 0.0f, 1.0f, 0.0f),
            };

            // Outline
            mtoonDefinition.Outline = new OutlineDefinition()
            {
                OutlineWidthMode = (MToon.OutlineWidthMode)vgoMaterial.GetIntOrDefault(MToon.Utils.PropOutlineWidthMode),
                OutlineWidthValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropOutlineWidth, 0.0f, 1.0f, 0.5f),
                OutlineWidthMultiplyTexture = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(MToon.Utils.PropOutlineWidthTexture)),
                OutlineScaledMaxDistanceValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropOutlineScaledMaxDistance, 1.0f, 10.0f, 1.0f),
                OutlineColorMode = (MToon.OutlineColorMode)vgoMaterial.GetIntOrDefault(MToon.Utils.PropOutlineColorMode),
                OutlineColor = vgoMaterial.GetColorOrDefault(MToon.Utils.PropOutlineColor, Color.black).gamma,
                OutlineLightingMixValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropOutlineLightingMix, 0.0f, 1.0f, 1.0f),
            };

            // Texture Option
            mtoonDefinition.TextureOption = new TextureUvCoordsDefinition()
            {
                MainTextureLeftBottomOriginScale = vgoMaterial.GetVector2OrDefault(MToon.Utils.PropMainTex, Vector2.one),
                MainTextureLeftBottomOriginOffset = vgoMaterial.GetVector2OrDefault(MToon.Utils.PropMainTex, Vector2.zero),
                UvAnimationMaskTexture = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(MToon.Utils.PropUvAnimMaskTexture)),
                UvAnimationScrollXSpeedValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropUvAnimScrollX, 0.0f, float.MaxValue, 0.0f),
                UvAnimationScrollYSpeedValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropUvAnimScrollY, 0.0f, float.MaxValue, 0.0f),
                UvAnimationRotationSpeedValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropUvAnimRotation, 0.0f, float.MaxValue, 0.0f),
            };

            return mtoonDefinition;
        }
    }
}
