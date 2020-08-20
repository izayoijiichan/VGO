// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : MtoonMaterialPorter
// ----------------------------------------------------------------------
namespace UniVgo2.Porters
{
    using MToon;
    using NewtonVgo;
    using System;
    using UnityEngine;

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
        /// <returns>A vgo material.</returns>
        public override VgoMaterial CreateGltfMaterial(Material material)
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
            ExportTextureProperty(vgoMaterial, material, MToon.Utils.PropMainTex);
            ExportTextureProperty(vgoMaterial, material, MToon.Utils.PropShadeTexture);

            // LitAndShadeMixing
            ExportProperty(vgoMaterial, material, MToon.Utils.PropShadeShift, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, MToon.Utils.PropShadeToony, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, MToon.Utils.PropReceiveShadowRate, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, MToon.Utils.PropShadingGradeRate, VgoMaterialPropertyType.Float);
            ExportTextureProperty(vgoMaterial, material, MToon.Utils.PropReceiveShadowTexture);
            ExportTextureProperty(vgoMaterial, material, MToon.Utils.PropShadingGradeTexture);

            // LightingInfluence
            ExportProperty(vgoMaterial, material, MToon.Utils.PropLightColorAttenuation, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, MToon.Utils.PropIndirectLightIntensity, VgoMaterialPropertyType.Float);

            // Normal
            ExportProperty(vgoMaterial, material, MToon.Utils.PropBumpScale, VgoMaterialPropertyType.Float);
            ExportTextureProperty(vgoMaterial, material, MToon.Utils.PropBumpMap, VgoTextureMapType.NormalMap, VgoColorSpaceType.Linear);

            // Emission
            ExportProperty(vgoMaterial, material, MToon.Utils.PropEmissionColor, VgoMaterialPropertyType.Color3);
            ExportTextureProperty(vgoMaterial, material, MToon.Utils.PropEmissionMap, VgoTextureMapType.EmissionMap, VgoColorSpaceType.Srgb);

            // MatCap
            ExportTextureProperty(vgoMaterial, material, MToon.Utils.PropSphereAdd);

            // Rim
            ExportProperty(vgoMaterial, material, MToon.Utils.PropRimColor, VgoMaterialPropertyType.Color4);
            ExportProperty(vgoMaterial, material, MToon.Utils.PropRimLightingMix, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, MToon.Utils.PropRimFresnelPower, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, MToon.Utils.PropRimLift, VgoMaterialPropertyType.Float);
            ExportTextureProperty(vgoMaterial, material, MToon.Utils.PropRimTexture);

            // Outline
            ExportProperty(vgoMaterial, material, MToon.Utils.PropOutlineWidthMode, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, MToon.Utils.PropOutlineWidth, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, MToon.Utils.PropOutlineScaledMaxDistance, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, MToon.Utils.PropOutlineColorMode, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, MToon.Utils.PropOutlineColor, VgoMaterialPropertyType.Color4);
            ExportProperty(vgoMaterial, material, MToon.Utils.PropOutlineLightingMix, VgoMaterialPropertyType.Float);
            ExportTextureProperty(vgoMaterial, material, MToon.Utils.PropOutlineWidthTexture);

            // TextureOption
            //ExportTextureOffset(vgoMaterial, material, MToon.Utils.PropMainTex);
            //ExportTextureScale(vgoMaterial, material, MToon.Utils.PropMainTex);
            ExportTextureProperty(vgoMaterial, material, MToon.Utils.PropUvAnimMaskTexture);
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
        /// <returns>A MToon material.</returns>
        public override Material CreateMaterialAsset(VgoMaterial vgoMaterial, Shader shader)
        {
            //Material material = base.CreateMaterialAsset(vgoMaterial, shader);

            if (vgoMaterial == null)
            {
                throw new ArgumentNullException(nameof(vgoMaterial));
            }

            if (shader == null)
            {
                throw new ArgumentNullException(nameof(shader));
            }

            var material = new Material(shader)
            {
                name = vgoMaterial.name
            };

            if (vgoMaterial.renderQueue >= 0)
            {
                material.renderQueue = vgoMaterial.renderQueue;
            }

            MToonDefinition mtoonDefinition = CreateMToonDefinition(vgoMaterial);

            MToon.Utils.SetMToonParametersToMaterial(material, mtoonDefinition);

            return material;
        }

        /// <summary>
        /// Create a MToon definition.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <returns>A MToon definition.</returns>
        protected virtual MToonDefinition CreateMToonDefinition(VgoMaterial vgoMaterial)
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
                LitMultiplyTexture = AllTexture2dList.GetValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(MToon.Utils.PropMainTex)),
                ShadeColor = vgoMaterial.GetColorOrDefault(MToon.Utils.PropShadeColor, Color.black).gamma,
                ShadeMultiplyTexture = AllTexture2dList.GetValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(MToon.Utils.PropShadeTexture)),
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
                ShadowReceiveMultiplierMultiplyTexture = AllTexture2dList.GetValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(MToon.Utils.PropReceiveShadowTexture)),
                LitAndShadeMixingMultiplierValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropShadingGradeRate, 0.0f, 1.0f, 1.0f),
                LitAndShadeMixingMultiplierMultiplyTexture = AllTexture2dList.GetValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(MToon.Utils.PropShadingGradeTexture)),
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
                NormalTexture = AllTexture2dList.GetValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(MToon.Utils.PropBumpMap)),
                NormalScaleValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropBumpScale, 0.0f, 1.0f, 1.0f),
            };

            // Emission
            mtoonDefinition.Emission = new EmissionDefinition
            {
                EmissionColor = vgoMaterial.GetColorOrDefault(MToon.Utils.PropEmissionColor, Color.black).gamma,
                EmissionMultiplyTexture = AllTexture2dList.GetValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(MToon.Utils.PropEmissionMap)),
            };

            // MatCap
            mtoonDefinition.MatCap = new MatCapDefinition
            {
                AdditiveTexture = AllTexture2dList.GetValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(MToon.Utils.PropSphereAdd)),
            };

            // Rim
            mtoonDefinition.Rim = new RimDefinition()
            {
                RimColor = vgoMaterial.GetColorOrDefault(MToon.Utils.PropRimColor, Color.black).gamma,
                RimMultiplyTexture = AllTexture2dList.GetValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(MToon.Utils.PropRimTexture)),
                RimLightingMixValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropRimLightingMix, 0.0f, 1.0f, 0.0f),
                RimFresnelPowerValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropRimFresnelPower, 0.0f, 100.0f, 1.0f),
                RimLiftValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropRimLift, 0.0f, 1.0f, 0.0f),
            };

            // Outline
            mtoonDefinition.Outline = new OutlineDefinition()
            {
                OutlineWidthMode = (MToon.OutlineWidthMode)vgoMaterial.GetIntOrDefault(MToon.Utils.PropOutlineWidthMode),
                OutlineWidthValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropOutlineWidth, 0.0f, 1.0f, 0.5f),
                OutlineWidthMultiplyTexture = AllTexture2dList.GetValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(MToon.Utils.PropOutlineWidthTexture)),
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
                UvAnimationMaskTexture = AllTexture2dList.GetValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(MToon.Utils.PropUvAnimMaskTexture)),
                UvAnimationScrollXSpeedValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropUvAnimScrollX, 0.0f, float.MaxValue, 0.0f),
                UvAnimationScrollYSpeedValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropUvAnimScrollY, 0.0f, float.MaxValue, 0.0f),
                UvAnimationRotationSpeedValue = vgoMaterial.GetSafeFloat(MToon.Utils.PropUvAnimRotation, 0.0f, float.MaxValue, 0.0f),
            };

            return mtoonDefinition;
        }

        #endregion
    }
}
