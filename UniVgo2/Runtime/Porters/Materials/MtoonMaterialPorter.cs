// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : MtoonMaterialPorter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Porters
{
    using MToon;
    using NewtonVgo;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using VRMShaders.VRM10.MToon10.Runtime;

    /// <summary>
    /// MToon Material Porter
    /// </summary>
    public class MtoonMaterialPorter : AbstractMaterialPorterBase
    {
        #region Constructors

        /// <summary>
        /// Create a new instance of MtoonMaterialPorter.
        /// </summary>
        public MtoonMaterialPorter() : base() { }

        #endregion

        #region Public Methods (Export)

        /// <summary>
        /// Create a vgo material.
        /// </summary>
        /// <param name="material">A MToon material.</param>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <returns>A vgo material.</returns>
        public override VgoMaterial CreateVgoMaterial(Material material, IVgoStorage vgoStorage)
        {
            //MToonDefinition definition = MToon.Utils.GetMToonParametersFromMaterial(material);

            VgoMaterial vgoMaterial = new VgoMaterial()
            {
                name = material.name,
                shaderName = material.shader.name,
                renderQueue = material.renderQueue,
                isUnlit = false,
            };

            // Meta
            ExportProperty(vgoMaterial, material, MToon.Utils.PropVersion, VgoMaterialPropertyType.Int);

            // Rendering
            ExportProperty(vgoMaterial, material, MToon.Utils.PropBlendMode, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, MToon.Utils.PropCullMode, VgoMaterialPropertyType.Int);
            //renderQueueOffsetNumber = definition.Rendering.RenderQueueOffsetNumber,

            // Color
            ExportProperty(vgoMaterial, material, MToon.Utils.PropColor, VgoMaterialPropertyType.Color4);
            ExportProperty(vgoMaterial, material, MToon.Utils.PropShadeColor, VgoMaterialPropertyType.Color4);
            ExportProperty(vgoMaterial, material, MToon.Utils.PropCutoff, VgoMaterialPropertyType.Float);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, MToon.Utils.PropMainTex);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, MToon.Utils.PropShadeTexture);

            // LitAndShadeMixing
            ExportProperty(vgoMaterial, material, MToon.Utils.PropShadeShift, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, MToon.Utils.PropShadeToony, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, MToon.Utils.PropReceiveShadowRate, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, MToon.Utils.PropShadingGradeRate, VgoMaterialPropertyType.Float);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, MToon.Utils.PropReceiveShadowTexture);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, MToon.Utils.PropShadingGradeTexture);

            // LightingInfluence
            ExportProperty(vgoMaterial, material, MToon.Utils.PropLightColorAttenuation, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, MToon.Utils.PropIndirectLightIntensity, VgoMaterialPropertyType.Float);

            // Normal
            ExportProperty(vgoMaterial, material, MToon.Utils.PropBumpScale, VgoMaterialPropertyType.Float);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, MToon.Utils.PropBumpMap, VgoTextureMapType.NormalMap, VgoColorSpaceType.Linear);

            // Emission
            ExportProperty(vgoMaterial, material, MToon.Utils.PropEmissionColor, VgoMaterialPropertyType.Color3);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, MToon.Utils.PropEmissionMap, VgoTextureMapType.EmissionMap, VgoColorSpaceType.Srgb);

            // MatCap
            ExportTextureProperty(vgoStorage, vgoMaterial, material, MToon.Utils.PropSphereAdd);

            // Rim
            ExportProperty(vgoMaterial, material, MToon.Utils.PropRimColor, VgoMaterialPropertyType.Color4);
            ExportProperty(vgoMaterial, material, MToon.Utils.PropRimLightingMix, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, MToon.Utils.PropRimFresnelPower, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, MToon.Utils.PropRimLift, VgoMaterialPropertyType.Float);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, MToon.Utils.PropRimTexture);

            // Outline
            ExportProperty(vgoMaterial, material, MToon.Utils.PropOutlineWidthMode, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, MToon.Utils.PropOutlineWidth, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, MToon.Utils.PropOutlineScaledMaxDistance, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, MToon.Utils.PropOutlineColorMode, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, MToon.Utils.PropOutlineColor, VgoMaterialPropertyType.Color4);
            ExportProperty(vgoMaterial, material, MToon.Utils.PropOutlineLightingMix, VgoMaterialPropertyType.Float);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, MToon.Utils.PropOutlineWidthTexture);

            // TextureOption
            //ExportTextureOffset(vgoMaterial, material, MToon.Utils.PropMainTex);
            //ExportTextureScale(vgoMaterial, material, MToon.Utils.PropMainTex);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, MToon.Utils.PropUvAnimMaskTexture);
            ExportProperty(vgoMaterial, material, MToon.Utils.PropUvAnimScrollX, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, MToon.Utils.PropUvAnimScrollY, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, MToon.Utils.PropUvAnimRotation, VgoMaterialPropertyType.Float);

            return vgoMaterial;
        }

        #endregion

        #region Public Methods (Import)

        /// <summary>
        /// Create a MToon material.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="shader">A MToon shader.</param>
        /// <param name="allTexture2dList">List of all texture 2D.</param>
        /// <returns>A MToon material.</returns>
        public override Material CreateMaterialAsset(VgoMaterial vgoMaterial, Shader shader, List<Texture2D?> allTexture2dList)
        {
            if (vgoMaterial.shaderName != ShaderName.VRM_MToon)
            {
                ThrowHelper.ThrowArgumentException($"vgoMaterial.shaderName: {vgoMaterial.shaderName}");
            }

            if (shader.name == ShaderName.VRM_MToon10)
            {
                return CreateMaterialAssetAsMtoon10(vgoMaterial, shader, allTexture2dList);
            }

            if (shader.name != ShaderName.VRM_MToon)
            {
                ThrowHelper.ThrowArgumentException($"shader.name: {shader.name}");
            }

            var material = new Material(shader)
            {
                name = vgoMaterial.name
            };

            if (vgoMaterial.renderQueue >= 0)
            {
                material.renderQueue = vgoMaterial.renderQueue;
            }

            MToonDefinition mtoonDefinition = CreateMToonDefinition(vgoMaterial, allTexture2dList);

            MToon.Utils.SetMToonParametersToMaterial(material, mtoonDefinition);

            return material;
        }

        #endregion

        #region Protected Methods (Import)

        /// <summary>
        /// Create a MToon definition.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="allTexture2dList">List of all texture 2D.</param>
        /// <returns>A MToon definition.</returns>
        protected virtual MToonDefinition CreateMToonDefinition(VgoMaterial vgoMaterial, List<Texture2D?> allTexture2dList)
        {
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

        #endregion

        #region Protected Methods (Import)

        /// <summary>
        /// Create a MToon material as 1.0.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="shader">A MToon 1.0 shader.</param>
        /// <param name="allTexture2dList">List of all texture 2D.</param>
        /// <returns>A MToon 1.0 material.</returns>
        /// <remarks>
        /// Migrate from MToon 0.x setting to MToon 1.0 material.
        /// </remarks>
        protected virtual Material CreateMaterialAssetAsMtoon10(VgoMaterial vgoMaterial, Shader shader, List<Texture2D?> allTexture2dList)
        {
            var material = new Material(shader)
            {
                name = vgoMaterial.name
            };

            MToonDefinition mtoon0x = CreateMToonDefinition(vgoMaterial, allTexture2dList);

            // Rendering
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

                SetSafeValue(material, vgoMaterial, MToon10Prop.AlphaMode, (int)alphaMode);
                SetSafeValue(material, vgoMaterial, MToon10Prop.TransparentWithZWrite, (int)zWriteMode);
                SetSafeValue(material, vgoMaterial, MToon10Prop.AlphaCutoff, mtoon0x.Color.CutoutThresholdValue, 0.0f, 1.0f, 0.5f);

                //SetSafeValue(material, vgoMaterial, MToon10Prop.RenderQueueOffsetNumber, mtoon0x.Rendering.RenderQueueOffsetNumber);

                MToon10DoubleSidedMode doubleSidedMode = (mtoon0x.Rendering.CullMode == MToon.CullMode.Off)
                    ? MToon10DoubleSidedMode.On
                    : MToon10DoubleSidedMode.Off;

                SetSafeValue(material, vgoMaterial, MToon10Prop.DoubleSided, (int)doubleSidedMode);
            }

            // Unity ShaderPass Mode
            {
                //SetSafeValue(material, vgoMaterial, MToon10Prop.UnitySrcBlend, vgoMaterial.GetIntOrDefault(MToon.Utils.PropSrcBlend, (int)UnityEngine.Rendering.BlendMode.One));
                //SetSafeValue(material, vgoMaterial, MToon10Prop.UnityDstBlend, vgoMaterial.GetIntOrDefault(MToon.Utils.PropDstBlend, (int)UnityEngine.Rendering.BlendMode.Zero));
                //SetSafeValue(material, vgoMaterial, MToon10Prop.UnityZWrite, vgoMaterial.GetIntOrDefault(MToon.Utils.PropZWrite, (int)UnityZWriteMode.On));
                //SetSafeValue(material, vgoMaterial, MToon10Prop.UnityAlphaToMask, vgoMaterial.GetIntOrDefault(MToon.Utils.PropAlphaToMask, (int)UnityAlphaToMaskMode.Off));

                SetUnityShaderPassSettingsAsMtoon10(material, mtoon0x.Rendering.RenderMode, mtoon0x.Rendering.RenderQueueOffsetNumber);

                SetSafeValue(material, vgoMaterial, MToon10Prop.UnityCullMode, (int)mtoon0x.Rendering.CullMode);
            }

            // Keywords
            {
                material.SetKeyword(MToon10NormalMapKeyword.On, mtoon0x.Lighting.Normal.NormalTexture != null);

                material.SetKeyword(MToon10EmissiveMapKeyword.On, mtoon0x.Emission.EmissionMultiplyTexture != null);

                material.SetKeyword(MToon10RimMapKeyword.On, (mtoon0x.Rim.RimMultiplyTexture != null) || (mtoon0x.MatCap.AdditiveTexture != null));

                material.SetKeyword(MToon10ParameterMapKeyword.On,
                    (mtoon0x.Lighting.LitAndShadeMixing.ShadowReceiveMultiplierMultiplyTexture != null) ||
                    (mtoon0x.Outline.OutlineWidthMultiplyTexture != null) ||
                    (mtoon0x.TextureOption.UvAnimationMaskTexture != null));
            }

            // Color
            {
                // Base (Main)
                SetColor(material, vgoMaterial, MToon10Prop.BaseColorFactor, mtoon0x.Color.LitColor);
                SetTexture(material, vgoMaterial, MToon10Prop.BaseColorTexture, mtoon0x.Color.LitMultiplyTexture);

                // Shade
                SetColor(material, vgoMaterial, MToon10Prop.ShadeColorFactor, mtoon0x.Color.ShadeColor);

                if ((mtoon0x.Color.LitMultiplyTexture != null) && (mtoon0x.Color.ShadeMultiplyTexture == null))
                {
                    // @notice Destructive Migration
                    SetTexture(material, vgoMaterial, MToon10Prop.ShadeColorTexture, mtoon0x.Color.LitMultiplyTexture);
                }
                else
                {
                    SetTexture(material, vgoMaterial, MToon10Prop.ShadeColorTexture, mtoon0x.Color.ShadeMultiplyTexture);
                }

                // Normal (Bump)
                SetTexture(material, vgoMaterial, MToon10Prop.NormalTexture, mtoon0x.Lighting.Normal.NormalTexture);
                SetSafeValue(material, vgoMaterial, MToon10Prop.NormalTextureScale, mtoon0x.Lighting.Normal.NormalScaleValue, 0.0f, float.MaxValue, 1.0f);

                // Shading Shift
                float shadingShift = MToon10Migrator.MigrateToShadingShift(mtoon0x.Lighting.LitAndShadeMixing.ShadingToonyValue, mtoon0x.Lighting.LitAndShadeMixing.ShadingShiftValue);

                SetSafeValue(material, vgoMaterial, MToon10Prop.ShadingShiftFactor, shadingShift, - 1.0f, 1.0f, -0.05f);

                SetTexture(material, vgoMaterial, MToon10Prop.ShadingShiftTexture, null);  // @notice
                SetSafeValue(material, vgoMaterial, MToon10Prop.ShadingShiftTextureScale, 1.0f, 0.0f, float.MaxValue, 1.0f);  // @notice

                // Shading Toony
                float shadingToony = MToon10Migrator.MigrateToShadingToony(mtoon0x.Lighting.LitAndShadeMixing.ShadingToonyValue, mtoon0x.Lighting.LitAndShadeMixing.ShadingShiftValue);

                SetSafeValue(material, vgoMaterial, MToon10Prop.ShadingToonyFactor, shadingToony);

                // Shadow Receive Multiplier (Obsolete)
                // mtoon0x.Lighting.LitAndShadeMixing.ShadowReceiveMultiplierValue
                // mtoon0x.Lighting.LitAndShadeMixing.ShadowReceiveMultiplierMultiplyTexture

                // Lit and Shade Mixing Multiplier (Obsolete)
                // mtoon0x.Lighting.LitAndShadeMixing.LitAndShadeMixingMultiplierValue
                // mtoon0x.Lighting.LitAndShadeMixing.LitAndShadeMixingMultiplierMultiplyTexture
            }

            // Global Illumination
            {
                float giEqualization = MToon10Migrator.MigrateToGiEqualization(mtoon0x.Lighting.LightingInfluence.GiIntensityValue);

                SetSafeValue(material, vgoMaterial, MToon10Prop.GiEqualizationFactor, giEqualization, 0.0f, 1.0f, 0.9f);

                // Light Color Attenuation (Obsolete)
                // mtoon0x.Lighting.LightingInfluence.LightColorAttenuationValue
            }

            // Emission
            {
                SetColor(material, vgoMaterial, MToon10Prop.EmissiveFactor, mtoon0x.Emission.EmissionColor);
                SetTexture(material, vgoMaterial, MToon10Prop.EmissiveTexture, mtoon0x.Emission.EmissionMultiplyTexture);
            }

            // Rim Lighting
            {
                SetTexture(material, vgoMaterial, MToon10Prop.RimMultiplyTexture, mtoon0x.Rim.RimMultiplyTexture);
                SetSafeValue(material, vgoMaterial, MToon10Prop.RimLightingMixFactor, mtoon0x.Rim.RimLightingMixValue, 0.0f, 1.0f, 1.0f);

                // Mat Cap (Sphere)
#if VRMC_VRMSHADERS_0_104_OR_NEWER
                SetColor(material, vgoMaterial, MToon10Prop.MatcapColorFactor, Color.black);
#endif
                SetTexture(material, vgoMaterial, MToon10Prop.MatcapTexture, mtoon0x.MatCap.AdditiveTexture);

                // Parametric Rim
                SetColor(material, vgoMaterial, MToon10Prop.ParametricRimColorFactor, mtoon0x.Rim.RimColor);
                SetSafeValue(material, vgoMaterial, MToon10Prop.ParametricRimFresnelPowerFactor, mtoon0x.Rim.RimFresnelPowerValue, 0.0f, 100.0f, 5.0f);
                SetSafeValue(material, vgoMaterial, MToon10Prop.ParametricRimLiftFactor, mtoon0x.Rim.RimLiftValue, 0.0f, 1.0f, 0.0f);
            }

            // Outline
            {
                MToon10OutlineMode outlineWidthMode;

                switch (mtoon0x.Outline.OutlineWidthMode)
                {
                    case OutlineWidthMode.WorldCoordinates:
                        outlineWidthMode = MToon10OutlineMode.World;
                        material.SetKeyword(MToon10OutlineModeKeyword.World, true);
                        material.SetKeyword(MToon10OutlineModeKeyword.Screen, false);
                        break;
                    case OutlineWidthMode.ScreenCoordinates:
                        outlineWidthMode = MToon10OutlineMode.Screen;
                        material.SetKeyword(MToon10OutlineModeKeyword.World, false);
                        material.SetKeyword(MToon10OutlineModeKeyword.Screen, true);
                        break;
                    case OutlineWidthMode.None:
                    default:
                        outlineWidthMode = MToon10OutlineMode.None;
                        material.SetKeyword(MToon10OutlineModeKeyword.World, false);
                        material.SetKeyword(MToon10OutlineModeKeyword.Screen, false);
                        break;
                }

                SetSafeValue(material, vgoMaterial, MToon10Prop.OutlineWidthMode, (int)outlineWidthMode);
                SetSafeValue(material, vgoMaterial, MToon10Prop.OutlineWidthFactor, mtoon0x.Outline.OutlineWidthValue, 0.0f, 0.05f, 0.0f);
                SetTexture(material, vgoMaterial, MToon10Prop.OutlineWidthMultiplyTexture, mtoon0x.Outline.OutlineWidthMultiplyTexture);
                SetColor(material, vgoMaterial, MToon10Prop.OutlineColorFactor, mtoon0x.Outline.OutlineColor);
                SetSafeValue(material, vgoMaterial, MToon10Prop.OutlineLightingMixFactor, mtoon0x.Outline.OutlineLightingMixValue, 0.0f, 1.0f, 1.0f);

                // Outline Scaled Max Distance (Obsolete)
                // mtoon0x.Outline.OutlineScaledMaxDistanceValue
            }

            // UV Animation
            {
                SetTexture(material, vgoMaterial, MToon10Prop.UvAnimationMaskTexture, mtoon0x.TextureOption.UvAnimationMaskTexture);
                SetSafeValue(material, vgoMaterial, MToon10Prop.UvAnimationScrollXSpeedFactor, mtoon0x.TextureOption.UvAnimationScrollXSpeedValue, 0.0f, float.MaxValue, 0.0f);
                SetSafeValue(material, vgoMaterial, MToon10Prop.UvAnimationScrollYSpeedFactor, mtoon0x.TextureOption.UvAnimationScrollYSpeedValue, 0.0f, float.MaxValue, 0.0f);
                SetSafeValue(material, vgoMaterial, MToon10Prop.UvAnimationRotationSpeedFactor, mtoon0x.TextureOption.UvAnimationRotationSpeedValue, 0.0f, float.MaxValue, 0.0f);
            }

            // for Editor
            {
                //int editorEditMode = (mtoon0x.Rendering.RenderMode == MToon.RenderMode.TransparentWithZWrite) ? 1 : 0;

                SetSafeValue(material, vgoMaterial, MToon10Prop.EditorEditMode, 1);
            }

            return material;
        }

        #endregion

        #region Protected Methods (Import)

        /// <summary>
        /// Sets unity shader pass settings.
        /// </summary>
        /// <param name="material">A MToon 1.0 material.</param>
        /// <param name="renderMode">A MToon 0.0 render mode.</param>
        /// <param name="renderQueueOffsetNumber">A MToon 0.0 render queue offset number.</param>
        protected virtual void SetUnityShaderPassSettingsAsMtoon10(Material material, in MToon.RenderMode renderMode, in int renderQueueOffsetNumber)
        {
            int renderQueueOffset = 0;

            switch (renderMode)
            {
                case MToon.RenderMode.Opaque:
                    material.SetOverrideTag(UnityRenderTag.Key, UnityRenderTag.OpaqueValue);

                    material.SetInt(MToon10Prop.UnitySrcBlend, (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt(MToon10Prop.UnityDstBlend, (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt(MToon10Prop.UnityZWrite, (int)UnityZWriteMode.On);
                    material.SetInt(MToon10Prop.UnityAlphaToMask, (int)UnityAlphaToMaskMode.Off);

                    material.SetKeyword(UnityAlphaModeKeyword.AlphaTest, false);
                    material.SetKeyword(UnityAlphaModeKeyword.AlphaBlend, false);
                    material.SetKeyword(UnityAlphaModeKeyword.AlphaPremultiply, false);

                    renderQueueOffset = 0;

                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;
                    break;
                case MToon.RenderMode.Cutout:
                    material.SetOverrideTag(UnityRenderTag.Key, UnityRenderTag.TransparentCutoutValue);

                    material.SetInt(MToon10Prop.UnitySrcBlend, (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt(MToon10Prop.UnityDstBlend, (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt(MToon10Prop.UnityZWrite, (int)UnityZWriteMode.On);
                    material.SetInt(MToon10Prop.UnityAlphaToMask, (int)UnityAlphaToMaskMode.On);

                    material.SetKeyword(UnityAlphaModeKeyword.AlphaTest, true);
                    material.SetKeyword(UnityAlphaModeKeyword.AlphaBlend, false);
                    material.SetKeyword(UnityAlphaModeKeyword.AlphaPremultiply, false);

                    renderQueueOffset = 0;

                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;
                    break;
                case MToon.RenderMode.Transparent:
                    material.SetOverrideTag(UnityRenderTag.Key, UnityRenderTag.TransparentValue);

                    material.SetInt(MToon10Prop.UnitySrcBlend, (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt(MToon10Prop.UnityDstBlend, (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt(MToon10Prop.UnityZWrite, (int)UnityZWriteMode.Off);
                    material.SetInt(MToon10Prop.UnityAlphaToMask, (int)UnityAlphaToMaskMode.Off);

                    material.SetKeyword(UnityAlphaModeKeyword.AlphaTest, false);
                    material.SetKeyword(UnityAlphaModeKeyword.AlphaBlend, true);
                    material.SetKeyword(UnityAlphaModeKeyword.AlphaPremultiply, false);

                    renderQueueOffset = Mathf.Clamp(renderQueueOffsetNumber, -9, 0);

                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent + renderQueueOffset;
                    break;
                case MToon.RenderMode.TransparentWithZWrite:
                    material.SetOverrideTag(UnityRenderTag.Key, UnityRenderTag.TransparentValue);

                    material.SetInt(MToon10Prop.UnitySrcBlend, (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt(MToon10Prop.UnityDstBlend, (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt(MToon10Prop.UnityZWrite, (int)UnityZWriteMode.On);
                    material.SetInt(MToon10Prop.UnityAlphaToMask, (int)UnityAlphaToMaskMode.Off);

                    material.SetKeyword(UnityAlphaModeKeyword.AlphaTest, false);
                    material.SetKeyword(UnityAlphaModeKeyword.AlphaBlend, true);
                    material.SetKeyword(UnityAlphaModeKeyword.AlphaPremultiply, false);

                    renderQueueOffset = Mathf.Clamp(renderQueueOffsetNumber, 0, +9);

                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.GeometryLast + 1 + renderQueueOffset; // Transparent First + N
                    break;
                default:
                    break;
            }

            material.SetInt(MToon10Prop.RenderQueueOffsetNumber, renderQueueOffset);
        }

        protected virtual void SetSafeValue(Material material, VgoMaterial vgoMaterial, MToon10Prop property, int value)
        {
            string propertyName = property.ToUnityShaderLabName();

            material.SetSafeInt(propertyName, value);
        }

        protected virtual void SetSafeValue(Material material, VgoMaterial vgoMaterial, MToon10Prop property, float value)
        {
            string propertyName = property.ToUnityShaderLabName();

            material.SetSafeFloat(propertyName, value);
        }

        protected virtual void SetSafeValue(Material material, VgoMaterial vgoMaterial, MToon10Prop property, float value, float min, float max, float defaultValue = default)
        {
            string propertyName = property.ToUnityShaderLabName();

            material.SetSafeFloat(propertyName, value, min, max, defaultValue);
        }

        protected virtual void SetColor(Material material, VgoMaterial vgoMaterial, MToon10Prop property, Color color)
        {
            string propertyName = property.ToUnityShaderLabName();

            material.SetSafeColor(propertyName, color);
        }

        protected virtual void SetTexture(Material material, VgoMaterial vgoMaterial, MToon10Prop property, Texture2D? texture, Vector2? offset = null, Vector2? scale = null)
        {
            string propertyName = property.ToUnityShaderLabName();

            if (material.HasProperty(propertyName) == false)
            {
                return;
            }

            if (texture != null)
            {
                material.SetTexture(propertyName, texture);
            }

            if (offset.HasValue)
            {
                material.SetTextureOffset(propertyName, offset.Value);
            }

            if (scale.HasValue)
            {
                material.SetTextureScale(propertyName, scale.Value);
            }
        }

        #endregion
    }
}
