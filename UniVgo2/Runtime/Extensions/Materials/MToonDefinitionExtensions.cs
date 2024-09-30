// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : MToonDefinitionExtensions
// ----------------------------------------------------------------------
#nullable enable
#if UNIVGO_ENABLE_MTOON_0_0 && UNIVGO_ENABLE_MTOON_1_0
namespace UniVgo2
{
#if UNIVGO_ENABLE_MTOON_0_0
    using MToon;
#endif
    using NewtonVgo;
    using UnityEngine;
#if UNIVGO_ENABLE_MTOON_1_0
#if VRMC_UNIVRM1_0_125_OR_NEWER
    using VRM10.MToon10;
#else
    using VRMShaders.VRM10.MToon10.Runtime;
#endif
#endif

    /// <summary>
    /// MToon 0.x Definition Extensions
    /// </summary>
    public static class MToonDefinitionExtensions
    {
        /// <summary>
        /// Convert MToon 0.x definition to MToon 1.0 definition.
        /// </summary>
        /// <param name="mtoon0x">A MToon 0.x definition.</param>
        /// <param name="destructiveMigration">Enable destructive migration.</param>
        /// <returns>A MToon 1.0 definition.</returns>
        public static MToon10Definition ToMToon10Definition(this MToonDefinition mtoon0x, bool destructiveMigration)
        {
            MToon10AlphaMode alphaMode;
            MToon10TransparentWithZWriteMode zWriteMode;

            switch (mtoon0x.Rendering.RenderMode)
            {
                case MToon.RenderMode.Opaque:
                    alphaMode = MToon10AlphaMode.Opaque;
                    zWriteMode = MToon10TransparentWithZWriteMode.On;
                    break;
                case MToon.RenderMode.Cutout:
                    alphaMode = MToon10AlphaMode.Cutout;
                    zWriteMode = MToon10TransparentWithZWriteMode.On;
                    break;
                case MToon.RenderMode.Transparent:
                    alphaMode = MToon10AlphaMode.Transparent;
                    zWriteMode = MToon10TransparentWithZWriteMode.Off;
                    break;
                case MToon.RenderMode.TransparentWithZWrite:
                    alphaMode = MToon10AlphaMode.Transparent;
                    zWriteMode = MToon10TransparentWithZWriteMode.On;
                    break;
                default:
                    alphaMode = MToon10AlphaMode.Opaque;
                    zWriteMode = MToon10TransparentWithZWriteMode.On;
                    break;
            }

            MToon10DoubleSidedMode doubleSidedMode = (mtoon0x.Rendering.CullMode == MToon.CullMode.Off)
                ? MToon10DoubleSidedMode.On
                : MToon10DoubleSidedMode.Off;

            float giEqualization = MToon10Migrator.MigrateToGiEqualization(mtoon0x.Lighting.LightingInfluence.GiIntensityValue);

            float shadingShift = MToon10Migrator.MigrateToShadingShift(
                mtoon0x.Lighting.LitAndShadeMixing.ShadingToonyValue,
                mtoon0x.Lighting.LitAndShadeMixing.ShadingShiftValue
            );

            float shadingToony = MToon10Migrator.MigrateToShadingToony(
                mtoon0x.Lighting.LitAndShadeMixing.ShadingToonyValue,
                mtoon0x.Lighting.LitAndShadeMixing.ShadingShiftValue
            );
 
            var mtoon10 = new MToon10Definition
            {
                // Rendering
                AlphaMode = alphaMode,
                TransparentWithZWrite = zWriteMode,
                AlphaCutoff = mtoon0x.Color.CutoutThresholdValue.SafeValue(0.0f, 1.0f, 0.5f),
                RenderQueueOffsetNumber = mtoon0x.Rendering.RenderQueueOffsetNumber,
                DoubleSided = doubleSidedMode,

                // Color
                BaseColorFactor = mtoon0x.Color.LitColor,
                BaseColorTexture = mtoon0x.Color.LitMultiplyTexture,
                BaseColorTextureScale = Vector2.one,
                BaseColorTextureOffset = Vector2.zero,
                ShadeColorFactor = mtoon0x.Color.ShadeColor,
                ShadeColorTexture = mtoon0x.Color.ShadeMultiplyTexture,
                NormalTexture = mtoon0x.Lighting.Normal.NormalTexture,
                NormalTextureScale = mtoon0x.Lighting.Normal.NormalScaleValue.SafeValue(0.0f, float.MaxValue, 1.0f),
                ShadingShiftFactor = shadingShift,
                ShadingShiftTexture = null,
                ShadingShiftTextureScale = 1.0f,
                ShadingToonyFactor = shadingToony,

                // Global Illumination
                GiEqualizationFactor = giEqualization,

                // Emission
                EmissiveFactor = mtoon0x.Emission.EmissionColor,
                EmissiveTexture = mtoon0x.Emission.EmissionMultiplyTexture,

                // Rim Lighting
                MatcapColorFactor = Color.black,
                MatcapTexture = mtoon0x.MatCap.AdditiveTexture,
                ParametricRimColorFactor = mtoon0x.Rim.RimColor,
                ParametricRimFresnelPowerFactor = mtoon0x.Rim.RimFresnelPowerValue.SafeValue(0.0f, 100.0f, 5.0f),
                ParametricRimLiftFactor = mtoon0x.Rim.RimLiftValue.SafeValue(0.0f, 1.0f),
                RimMultiplyTexture = mtoon0x.Rim.RimMultiplyTexture,
                RimLightingMixFactor = mtoon0x.Rim.RimLightingMixValue.SafeValue(0.0f, 1.0f, 1.0f),

                // Outline
                OutlineWidthMode = mtoon0x.Outline.OutlineWidthMode switch
                {
                    OutlineWidthMode.None => MToon10OutlineMode.None,
                    OutlineWidthMode.WorldCoordinates => MToon10OutlineMode.World,
                    OutlineWidthMode.ScreenCoordinates => MToon10OutlineMode.Screen,
                    _ => MToon10OutlineMode.None,
                },
                OutlineWidthFactor = mtoon0x.Outline.OutlineWidthValue.SafeValue(0.0f, 0.05f),
                OutlineWidthMultiplyTexture = mtoon0x.Outline.OutlineWidthMultiplyTexture,
                OutlineColorFactor = mtoon0x.Outline.OutlineColor,
                OutlineLightingMixFactor = mtoon0x.Outline.OutlineLightingMixValue.SafeValue(0.0f, 1.0f, 1.0f),

                // UV Animation
                UvAnimationMaskTexture = mtoon0x.TextureOption.UvAnimationMaskTexture,
                UvAnimationScrollXSpeedFactor = mtoon0x.TextureOption.UvAnimationScrollXSpeedValue.SafeValue(0.0f, float.MaxValue, 0.0f),
                UvAnimationScrollYSpeedFactor = mtoon0x.TextureOption.UvAnimationScrollYSpeedValue.SafeValue(0.0f, float.MaxValue, 0.0f),
                UvAnimationRotationSpeedFactor = mtoon0x.TextureOption.UvAnimationRotationSpeedValue.SafeValue(0.0f, float.MaxValue, 0.0f),

                UnityCullMode = (UnityEngine.Rendering.CullMode)mtoon0x.Rendering.CullMode,
                UnitySrcBlend = default,
                UnityDstBlend = default,
                UnityZWrite = default,
                UnityAlphaToMask = mtoon0x.Rendering.RenderMode == MToon.RenderMode.Cutout,

                EditorEditMode = 1,
            };

            if (destructiveMigration)
            {
                if ((mtoon0x.Color.LitMultiplyTexture != null) && (mtoon0x.Color.ShadeMultiplyTexture == null))
                {
                    mtoon10.ShadeColorTexture = mtoon0x.Color.LitMultiplyTexture;
                }
            }

            mtoon10.EditorEditMode = (mtoon0x.Rendering.RenderMode == MToon.RenderMode.TransparentWithZWrite) ? 1 : 0;

            // Light Color Attenuation (Obsolete)
            // mtoon0x.Lighting.LightingInfluence.LightColorAttenuationValue

            // Shadow Receive Multiplier (Obsolete)
            // mtoon0x.Lighting.LitAndShadeMixing.ShadowReceiveMultiplierValue
            // mtoon0x.Lighting.LitAndShadeMixing.ShadowReceiveMultiplierMultiplyTexture

            // Lit and Shade Mixing Multiplier (Obsolete)
            // mtoon0x.Lighting.LitAndShadeMixing.LitAndShadeMixingMultiplierValue
            // mtoon0x.Lighting.LitAndShadeMixing.LitAndShadeMixingMultiplierMultiplyTexture

            // Outline Scaled Max Distance (Obsolete)
            // mtoon0x.Outline.OutlineScaledMaxDistanceValue

            return mtoon10;
        }
    }
}
#endif