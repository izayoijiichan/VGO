// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : MtoonMaterialPorter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Porters
{
#if UNIVGO_ENABLE_MTOON_0_0
    using MToon;
#endif
    using NewtonVgo;
    using System.Collections.Generic;
    using UnityEngine;
#if UNIVGO_ENABLE_MTOON_1_0
#if VRMC_UNIVRM1_0_125_OR_NEWER
    using VRM10.MToon10;
#else
    using VRMShaders.VRM10.MToon10.Runtime;
#endif
#endif

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
        public override VgoMaterial CreateVgoMaterial(in Material material, in IVgoStorage vgoStorage)
        {
#if UNIVGO_ENABLE_MTOON_0_0
            //MToonDefinition definition = MToon.Utils.GetMToonParametersFromMaterial(material);

            var vgoMaterial = new VgoMaterial()
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

            // Tags
            ExportTag(vgoMaterial, material, MToon.Utils.TagRenderTypeKey);

            // Keywords
            ExportKeywords(vgoMaterial, material);

            return vgoMaterial;
#else
#if NET_STANDARD_2_1
            ThrowHelper.ThrowNotSupportedException(material.shader.name);
            return default;
#else
            throw new NotSupportedException(material.shader.name);
#endif
#endif
        }

        #endregion

        #region Public Methods (Import)

        /// <summary>
        /// Create a MToon material.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="shader">A MToon shader.</param>
        /// <param name="allTextureList">List of all texture.</param>
        /// <returns>A MToon material.</returns>
        public override Material CreateMaterialAsset(in VgoMaterial vgoMaterial, in Shader shader, in List<Texture?> allTextureList)
        {
            if (vgoMaterial.shaderName != ShaderName.VRM_MToon)
            {
                ThrowHelper.ThrowArgumentException($"vgoMaterial.shaderName: {vgoMaterial.shaderName}");
            }

#if UNIVGO_ENABLE_MTOON_0_0 && UNIVGO_ENABLE_MTOON_1_0
            if ((shader.name == ShaderName.VRM_MToon10) || (shader.name == ShaderName.VRM_URP_MToon10))
            {
                return CreateMaterialAssetAsMtoon10(vgoMaterial, shader, allTextureList);
            }
#endif

            if (shader.name != ShaderName.VRM_MToon)
            {
                ThrowHelper.ThrowArgumentException($"shader.name: {shader.name}");
            }

#if UNIVGO_ENABLE_MTOON_0_0
            var material = new Material(shader)
            {
                name = vgoMaterial.name
            };

            if (vgoMaterial.renderQueue >= 0)
            {
                material.renderQueue = vgoMaterial.renderQueue;
            }

            MToonDefinition mtoonParameter = vgoMaterial.ToMToon0xDefinition(allTextureList);

            MToon.Utils.SetMToonParametersToMaterial(material, mtoonParameter);

            return material;
#else
#if NET_STANDARD_2_1
            ThrowHelper.ThrowNotSupportedException(vgoMaterial.shaderName);
            return default;
#else
            throw new NotSupportedException(vgoMaterial.shaderName);
#endif
#endif
        }

        #endregion

#if UNIVGO_ENABLE_MTOON_0_0 && UNIVGO_ENABLE_MTOON_1_0
        #region Protected Methods (Import)

        /// <summary>
        /// Create a MToon material as 1.0.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="shader">A MToon 1.0 shader.</param>
        /// <param name="allTextureList">List of all texture.</param>
        /// <returns>A MToon 1.0 material.</returns>
        /// <remarks>
        /// Migrate from MToon 0.x setting to MToon 1.0 material.
        /// </remarks>
        protected virtual Material CreateMaterialAssetAsMtoon10(in VgoMaterial vgoMaterial, Shader shader, in List<Texture?> allTextureList)
        {
            var material = new Material(shader)
            {
                name = vgoMaterial.name
            };

            MToonDefinition mtoon0x = vgoMaterial.ToMToon0xDefinition(allTextureList);

            MToon10Definition mtoon10 = mtoon0x.ToMToon10Definition(destructiveMigration: true);

            mtoon10.BaseColorTextureScale = vgoMaterial.GetTextureScaleOrDefault(MToon.Utils.PropMainTex, Vector2.one);
            mtoon10.BaseColorTextureOffset = vgoMaterial.GetTextureOffsetOrDefault(MToon.Utils.PropMainTex, Vector2.zero);

            mtoon10.UnitySrcBlend = vgoMaterial.GetEnumOrDefault<UnityEngine.Rendering.BlendMode>(MToon.Utils.PropSrcBlend, UnityEngine.Rendering.BlendMode.One);
            mtoon10.UnityDstBlend = vgoMaterial.GetEnumOrDefault<UnityEngine.Rendering.BlendMode>(MToon.Utils.PropDstBlend, UnityEngine.Rendering.BlendMode.Zero);
            mtoon10.UnityZWrite = vgoMaterial.GetIntOrDefault(MToon.Utils.PropZWrite, (int)UnityZWriteMode.On) == 1;
            mtoon10.UnityAlphaToMask = vgoMaterial.GetIntOrDefault(MToon.Utils.PropAlphaToMask, (int)UnityAlphaToMaskMode.Off) == 1;

            // Rendering
            {
                SetSafeValue(material, MToon10Prop.AlphaMode, (int)mtoon10.AlphaMode);
                SetSafeValue(material, MToon10Prop.TransparentWithZWrite, (int)mtoon10.TransparentWithZWrite);
                SetSafeValue(material, MToon10Prop.AlphaCutoff, mtoon10.AlphaCutoff);
                SetSafeValue(material, MToon10Prop.RenderQueueOffsetNumber, mtoon10.RenderQueueOffsetNumber);
                SetSafeValue(material, MToon10Prop.DoubleSided, (int)mtoon10.DoubleSided);
            }

            // Unity ShaderPass Mode
            {
                SetSafeValue(material, MToon10Prop.UnityCullMode, (int)mtoon10.UnityCullMode);
                SetSafeValue(material, MToon10Prop.UnitySrcBlend, (int)mtoon10.UnitySrcBlend);
                SetSafeValue(material, MToon10Prop.UnityDstBlend, (int)mtoon10.UnityDstBlend);
                SetSafeValue(material, MToon10Prop.UnityZWrite, mtoon10.UnityZWrite ? 1 : 0);
                SetSafeValue(material, MToon10Prop.UnityAlphaToMask, mtoon10.UnityAlphaToMask ? 1 : 0);
            }

            // Color
            {
                // Base (Main)
                SetColor(material, MToon10Prop.BaseColorFactor, mtoon10.BaseColorFactor);
                SetTexture(material, MToon10Prop.BaseColorTexture, mtoon10.BaseColorTexture);
                SetTextureScale(material, MToon10Prop.BaseColorTexture, mtoon10.BaseColorTextureScale);
                SetTextureOffset(material, MToon10Prop.BaseColorTexture, mtoon10.BaseColorTextureOffset);

                // Shade
                SetColor(material, MToon10Prop.ShadeColorFactor, mtoon10.ShadeColorFactor);
                SetTexture(material, MToon10Prop.ShadeColorTexture, mtoon10.ShadeColorTexture, vgoMaterial, MToon.Utils.PropShadeTexture);

                // Normal (Bump)
                SetTexture(material, MToon10Prop.NormalTexture, mtoon10.NormalTexture, vgoMaterial, MToon.Utils.PropBumpMap);
                SetSafeValue(material, MToon10Prop.NormalTextureScale, mtoon10.NormalTextureScale);

                // Shading Shift
                SetSafeValue(material, MToon10Prop.ShadingShiftFactor, mtoon10.ShadingShiftFactor);

                SetTexture(material, MToon10Prop.ShadingShiftTexture, mtoon10.ShadingShiftTexture, vgoMaterial, string.Empty);
                SetSafeValue(material, MToon10Prop.ShadingShiftTextureScale, mtoon10.ShadingShiftTextureScale);

                // Shading Toony
                SetSafeValue(material, MToon10Prop.ShadingToonyFactor, mtoon10.ShadingToonyFactor);
            }

            // Global Illumination
            {
                SetSafeValue(material, MToon10Prop.GiEqualizationFactor, mtoon10.GiEqualizationFactor);
            }

            // Emission
            {
                SetColor(material, MToon10Prop.EmissiveFactor, mtoon10.EmissiveFactor);
                SetTexture(material, MToon10Prop.EmissiveTexture, mtoon10.EmissiveTexture, vgoMaterial, MToon.Utils.PropEmissionMap);
            }

            // Rim Lighting
            {
                SetTexture(material, MToon10Prop.RimMultiplyTexture, mtoon10.RimMultiplyTexture, vgoMaterial, MToon.Utils.PropRimTexture);
                SetSafeValue(material, MToon10Prop.RimLightingMixFactor, mtoon10.RimLightingMixFactor);

                // Mat Cap (Sphere)
#if VRMC_VRMSHADERS_0_104_OR_NEWER
                SetColor(material, MToon10Prop.MatcapColorFactor, mtoon10.MatcapColorFactor);
#endif
                SetTexture(material, MToon10Prop.MatcapTexture, mtoon10.MatcapTexture, vgoMaterial, MToon.Utils.PropSphereAdd);

                // Parametric Rim
                SetColor(material, MToon10Prop.ParametricRimColorFactor, mtoon10.ParametricRimColorFactor);
                SetSafeValue(material, MToon10Prop.ParametricRimFresnelPowerFactor, mtoon10.ParametricRimFresnelPowerFactor);
                SetSafeValue(material, MToon10Prop.ParametricRimLiftFactor, mtoon10.ParametricRimLiftFactor);
            }

            // Outline
            {
                SetSafeValue(material, MToon10Prop.OutlineWidthMode, (int)mtoon10.OutlineWidthMode);
                SetSafeValue(material, MToon10Prop.OutlineWidthFactor, mtoon10.OutlineWidthFactor);
                SetTexture(material, MToon10Prop.OutlineWidthMultiplyTexture, mtoon10.OutlineWidthMultiplyTexture, vgoMaterial, MToon.Utils.PropOutlineWidthTexture);
                SetColor(material, MToon10Prop.OutlineColorFactor, mtoon10.OutlineColorFactor);
                SetSafeValue(material, MToon10Prop.OutlineLightingMixFactor, mtoon10.OutlineLightingMixFactor);
            }

            // UV Animation
            {
                SetTexture(material, MToon10Prop.UvAnimationMaskTexture, mtoon10.UvAnimationMaskTexture, vgoMaterial, MToon.Utils.PropUvAnimMaskTexture);
                SetSafeValue(material, MToon10Prop.UvAnimationScrollXSpeedFactor, mtoon10.UvAnimationScrollXSpeedFactor);
                SetSafeValue(material, MToon10Prop.UvAnimationScrollYSpeedFactor, mtoon10.UvAnimationScrollYSpeedFactor);
                SetSafeValue(material, MToon10Prop.UvAnimationRotationSpeedFactor, mtoon10.UvAnimationRotationSpeedFactor);
            }

            // for Editor
            {
                SetSafeValue(material, MToon10Prop.EditorEditMode, mtoon10.EditorEditMode);
            }

            var mtoonValidator = new MToonValidator(material);

            mtoonValidator.Validate();

            return material;
        }

        #endregion

        #region Protected Methods (Import)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material">A material.</param>
        /// <param name="property">A material property.</param>
        /// <param name="value"></param>
        protected virtual void SetSafeValue(Material material, in MToon10Prop property, int value)
        {
            string propertyName = property.ToUnityShaderLabName();

            material.SetSafeInt(propertyName, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        protected virtual void SetSafeValue(Material material, in MToon10Prop property, float value)
        {
            string propertyName = property.ToUnityShaderLabName();

            material.SetSafeFloat(propertyName, value, minValue: null, maxValue: null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material">A material.</param>
        /// <param name="property">A material property.</param>
        /// <param name="color"></param>
        protected virtual void SetColor(Material material, in MToon10Prop property, Color color)
        {
            string propertyName = property.ToUnityShaderLabName();

            material.SetSafeColor(propertyName, color);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material">A material.</param>
        /// <param name="property">A material property.</param>
        /// <param name="texture"></param>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="mtoon0xPropertyName"></param>
        protected virtual void SetTexture(Material material, in MToon10Prop property, Texture2D? texture, in VgoMaterial vgoMaterial, in string mtoon0xPropertyName)
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

            if (string.IsNullOrEmpty(mtoon0xPropertyName) == false)
            {
                Vector2? textureOffset = vgoMaterial.GetTextureOffsetOrNull(mtoon0xPropertyName);

                if (textureOffset.HasValue)
                {
                    material.SetTextureOffset(propertyName, textureOffset.Value);
                }

                Vector2? textureScale = vgoMaterial.GetTextureScaleOrNull(mtoon0xPropertyName);

                if (textureScale.HasValue)
                {
                    material.SetTextureScale(propertyName, textureScale.Value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material">A material.</param>
        /// <param name="property">A material property.</param>
        /// <param name="texture"></param>
        protected virtual void SetTexture(Material material, in MToon10Prop property, Texture2D? texture)
        {
            string propertyName = property.ToUnityShaderLabName();

            if (material.HasProperty(propertyName) == false)
            {
                return;
            }

            material.SetTexture(propertyName, texture);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material">A material.</param>
        /// <param name="property">A material property.</param>
        /// <param name="textureScale"></param>
        protected virtual void SetTextureScale(Material material, in MToon10Prop property, Vector2 textureScale)
        {
            string propertyName = property.ToUnityShaderLabName();

            if (material.HasProperty(propertyName) == false)
            {
                return;
            }

            material.SetTextureScale(propertyName, textureScale);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material">A material.</param>
        /// <param name="property">A material property.</param>
        /// <param name="textureOffset"></param>
        protected virtual void SetTextureOffset(Material material, in MToon10Prop property, Vector2 textureOffset)
        {
            string propertyName = property.ToUnityShaderLabName();

            if (material.HasProperty(propertyName) == false)
            {
                return;
            }

            material.SetTextureOffset(propertyName, textureOffset);
        }

        #endregion
#endif
    }
}
