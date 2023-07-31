// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : Mtoon10MaterialPorter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using System.Collections.Generic;
    using UnityEngine;
    using VRMShaders.VRM10.MToon10.Runtime;

    /// <summary>
    /// MToon 1.0 Material Porter
    /// </summary>
    public class Mtoon10MaterialPorter : AbstractMaterialPorterBase
    {
        #region Constructors

        /// <summary>
        /// Create a new instance of Mtoon10MaterialPorter.
        /// </summary>
        public Mtoon10MaterialPorter() : base() { }

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
            var mtoonContext = new MToon10Context(material);

            mtoonContext.Validate();

            var vgoMaterial = new VgoMaterial()
            {
                name = material.name,
                shaderName = material.shader.name,
                renderQueue = material.renderQueue,
                isUnlit = false,
            };

            // Rendering
            {
                ExportProperty(vgoMaterial, material, MToon10Prop.AlphaMode, VgoMaterialPropertyType.Int);
                ExportProperty(vgoMaterial, material, MToon10Prop.TransparentWithZWrite, VgoMaterialPropertyType.Int);
                ExportProperty(vgoMaterial, material, MToon10Prop.AlphaCutoff, VgoMaterialPropertyType.Float);
                ExportProperty(vgoMaterial, material, MToon10Prop.RenderQueueOffsetNumber, VgoMaterialPropertyType.Int);
                ExportProperty(vgoMaterial, material, MToon10Prop.DoubleSided, VgoMaterialPropertyType.Int);
            }

            // Color
            {
                // Base (Main)
                ExportProperty(vgoMaterial, material, MToon10Prop.BaseColorFactor, VgoMaterialPropertyType.Color4);
                ExportTextureProperty(vgoStorage, vgoMaterial, material, MToon10Prop.BaseColorTexture);

                // Shade
                ExportProperty(vgoMaterial, material, MToon10Prop.ShadeColorFactor, VgoMaterialPropertyType.Color4);
                ExportTextureProperty(vgoStorage, vgoMaterial, material, MToon10Prop.ShadeColorTexture);

                // Normal (Bump)
                ExportTextureProperty(vgoStorage, vgoMaterial, material, MToon10Prop.NormalTexture, VgoTextureMapType.NormalMap, VgoColorSpaceType.Linear);
                ExportProperty(vgoMaterial, material, MToon10Prop.NormalTextureScale, VgoMaterialPropertyType.Float);

                // Shading Shift
                ExportProperty(vgoMaterial, material, MToon10Prop.ShadingShiftFactor, VgoMaterialPropertyType.Float);
                ExportTextureProperty(vgoStorage, vgoMaterial, material, MToon10Prop.ShadingShiftTexture);
                ExportProperty(vgoMaterial, material, MToon10Prop.ShadingShiftTextureScale, VgoMaterialPropertyType.Float);

                // Shading Toony
                ExportProperty(vgoMaterial, material, MToon10Prop.ShadingToonyFactor, VgoMaterialPropertyType.Float);
            }

            // Global Illumination
            {
                ExportProperty(vgoMaterial, material, MToon10Prop.GiEqualizationFactor, VgoMaterialPropertyType.Float);
            }

            // Emission
            {
                ExportProperty(vgoMaterial, material, MToon10Prop.EmissiveFactor, VgoMaterialPropertyType.Color3);
                ExportTextureProperty(vgoStorage, vgoMaterial, material, MToon10Prop.EmissiveTexture, VgoTextureMapType.EmissionMap, VgoColorSpaceType.Srgb);
            }

            // Rim Lighting
            {
                // Mat Cap
#if VRMC_VRMSHADERS_0_104_OR_NEWER
                ExportProperty(vgoMaterial, material, MToon10Prop.MatcapColorFactor, VgoMaterialPropertyType.Color4);
#endif
                ExportTextureProperty(vgoStorage, vgoMaterial, material, MToon10Prop.MatcapTexture);

                // Parametric Rim
                ExportProperty(vgoMaterial, material, MToon10Prop.ParametricRimColorFactor, VgoMaterialPropertyType.Color4);
                ExportProperty(vgoMaterial, material, MToon10Prop.ParametricRimFresnelPowerFactor, VgoMaterialPropertyType.Float);
                ExportProperty(vgoMaterial, material, MToon10Prop.ParametricRimLiftFactor, VgoMaterialPropertyType.Float);

                ExportTextureProperty(vgoStorage, vgoMaterial, material, MToon10Prop.RimMultiplyTexture);
                ExportProperty(vgoMaterial, material, MToon10Prop.RimLightingMixFactor, VgoMaterialPropertyType.Float);
            }

            // Outline
            {
                ExportProperty(vgoMaterial, material, MToon10Prop.OutlineWidthMode, VgoMaterialPropertyType.Int);
                ExportProperty(vgoMaterial, material, MToon10Prop.OutlineWidthFactor, VgoMaterialPropertyType.Float);
                ExportTextureProperty(vgoStorage, vgoMaterial, material, MToon10Prop.OutlineWidthMultiplyTexture);
                ExportProperty(vgoMaterial, material, MToon10Prop.OutlineColorFactor, VgoMaterialPropertyType.Color4);
                ExportProperty(vgoMaterial, material, MToon10Prop.OutlineLightingMixFactor, VgoMaterialPropertyType.Float);
            }

            // UV Animation
            {
                ExportTextureProperty(vgoStorage, vgoMaterial, material, MToon10Prop.UvAnimationMaskTexture);
                ExportProperty(vgoMaterial, material, VgoMaterialPropertyType.Float, MToon10Prop.UvAnimationScrollXSpeedFactor);
                ExportProperty(vgoMaterial, material, VgoMaterialPropertyType.Float, MToon10Prop.UvAnimationScrollYSpeedFactor);
                ExportProperty(vgoMaterial, material, VgoMaterialPropertyType.Float, MToon10Prop.UvAnimationRotationSpeedFactor);
            }

            // Unity ShaderPass Mode
            {
                ExportProperty(vgoMaterial, material, VgoMaterialPropertyType.Int, MToon10Prop.UnityCullMode);
                ExportProperty(vgoMaterial, material, VgoMaterialPropertyType.Int, MToon10Prop.UnitySrcBlend);
                ExportProperty(vgoMaterial, material, VgoMaterialPropertyType.Int, MToon10Prop.UnityDstBlend);
                ExportProperty(vgoMaterial, material, VgoMaterialPropertyType.Int, MToon10Prop.UnityZWrite);
                ExportProperty(vgoMaterial, material, VgoMaterialPropertyType.Int, MToon10Prop.UnityAlphaToMask);
            }

            // etc
            {
                //ExportProperty(vgoMaterial, material, MToon10Prop.DebugMode, VgoMaterialPropertyType.Float);
            }

            // for Editor
            {
                //ExportProperty(vgoMaterial, material, MToon10Prop.EditorEditMode, VgoMaterialPropertyType.Float);
            }

            // Tags
            ExportTag(vgoMaterial, material, UnityRenderTag.Key);

            // Keywords
            ExportKeywords(vgoMaterial, material);

            return vgoMaterial;
        }

        #endregion

        #region Protected Methods (Export)

        /// <summary>
        /// Export a property.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="material">A unity material.</param>
        /// <param name="property">A MToon 1.0 property.</param>
        /// <param name="type">The type of property.</param>
        /// <returns></returns>
        protected virtual bool ExportProperty(in VgoMaterial vgoMaterial, in Material material, in MToon10Prop property, in VgoMaterialPropertyType type)
        {
            return ExportProperty(vgoMaterial, material, property.ToUnityShaderLabName(), type);
        }

        /// <summary>
        /// Export a property.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="material">A unity material.</param>
        /// <param name="type">The type of property.</param>
        /// <param name="property">A MToon 1.0 property.</param>
        /// <returns></returns>
        protected virtual bool ExportProperty(in VgoMaterial vgoMaterial, in Material material, in VgoMaterialPropertyType type, in MToon10Prop property)
        {
            return ExportProperty(vgoMaterial, material, property, type);
        }

        /// <summary>
        /// Export a texture type property.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="material">A unity material.</param>
        /// <param name="property">A MToon 1.0 property.</param>
        /// <param name="textureMapType">Type of the texture map.</param>
        /// <param name="colorSpace">The color space.</param>
        /// <param name="metallicSmoothness">The metallic smoothness.</param>
        /// <returns></returns>
        protected virtual bool ExportTextureProperty(
            IVgoStorage vgoStorage,
            VgoMaterial vgoMaterial,
            in Material material,
            in MToon10Prop property,
            in VgoTextureMapType textureMapType = VgoTextureMapType.Default,
            in VgoColorSpaceType colorSpace = VgoColorSpaceType.Srgb,
            in float metallicSmoothness = -1.0f)
        {
            return ExportTextureProperty(vgoStorage, vgoMaterial, material, property.ToUnityShaderLabName(), textureMapType, colorSpace, metallicSmoothness);
        }

        #endregion

        #region Public Methods (Import)

        /// <summary>
        /// Create a MToon 1.0 material.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="shader">A MToon 1.0 shader.</param>
        /// <param name="allTexture2dList">List of all texture 2D.</param>
        /// <returns>A MToon 1.0 material.</returns>
        public override Material CreateMaterialAsset(in VgoMaterial vgoMaterial, in Shader shader, in List<Texture2D?> allTexture2dList)
        {
            if (vgoMaterial.shaderName != ShaderName.VRM_MToon10)
            {
                ThrowHelper.ThrowArgumentException($"vgoMaterial.shaderName: {vgoMaterial.shaderName}");
            }

            if (shader.name == ShaderName.VRM_URP_MToon10)
            {
                return CreateMaterialAssetAsUrp(vgoMaterial, shader, allTexture2dList);
            }

            if (shader.name != ShaderName.VRM_MToon10)
            {
                ThrowHelper.ThrowArgumentException($"shader.name: {shader.name}");
            }

            return CreateMaterialAssetInternal(vgoMaterial, shader, allTexture2dList);
        }

        #endregion

        #region Protected Methods (Import)

        /// <summary>
        /// Create a MToon 1.0 material.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="shader">A MToon 1.0 shader.</param>
        /// <param name="allTexture2dList">List of all texture 2D.</param>
        /// <returns>A MToon 1.0 material.</returns>
        protected virtual Material CreateMaterialAssetAsUrp(in VgoMaterial vgoMaterial, in Shader shader, in List<Texture2D?> allTexture2dList)
        {
            return CreateMaterialAssetInternal(vgoMaterial, shader, allTexture2dList);
        }

        /// <summary>
        /// Create a MToon 1.0 material.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="shader">A MToon 1.0 shader.</param>
        /// <param name="allTexture2dList">List of all texture 2D.</param>
        /// <returns>A MToon 1.0 material.</returns>
        protected virtual Material CreateMaterialAssetInternal(in VgoMaterial vgoMaterial, in Shader shader, in List<Texture2D?> allTexture2dList)
        {
            MToon10Definition mtoon10 = vgoMaterial.ToMToon10Definition(allTexture2dList);

            var material = new Material(shader)
            {
                name = vgoMaterial.name
            };

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
                SetTexture(material, vgoMaterial, allTexture2dList, MToon10Prop.BaseColorTexture);

                // Shade
                SetColor(material, MToon10Prop.ShadeColorFactor, mtoon10.ShadeColorFactor);
                SetTexture(material, vgoMaterial, allTexture2dList, MToon10Prop.ShadeColorTexture);

                // Normal (Bump)
                SetTexture(material, vgoMaterial, allTexture2dList, MToon10Prop.NormalTexture);
                SetSafeValue(material, MToon10Prop.NormalTextureScale, mtoon10.NormalTextureScale);

                // Shading Shift
                SetSafeValue(material, MToon10Prop.ShadingShiftFactor, mtoon10.ShadingShiftFactor);

                SetTexture(material, vgoMaterial, allTexture2dList, MToon10Prop.ShadingShiftTexture);
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
                SetTexture(material, vgoMaterial, allTexture2dList, MToon10Prop.EmissiveTexture);
                SetColor(material, MToon10Prop.EmissiveFactor, mtoon10.EmissiveFactor);
            }

            // Rim Lighting
            {
                SetTexture(material, vgoMaterial, allTexture2dList, MToon10Prop.RimMultiplyTexture);
                SetSafeValue(material, MToon10Prop.RimLightingMixFactor, mtoon10.RimLightingMixFactor);

                // Mat Cap
#if VRMC_VRMSHADERS_0_104_OR_NEWER
                SetColor(material, MToon10Prop.MatcapColorFactor, mtoon10.MatcapColorFactor);
#endif
                SetTexture(material, vgoMaterial, allTexture2dList, MToon10Prop.MatcapTexture);

                // Parametric Rim
                SetColor(material, MToon10Prop.ParametricRimColorFactor, mtoon10.ParametricRimColorFactor);
                SetSafeValue(material, MToon10Prop.ParametricRimFresnelPowerFactor, mtoon10.ParametricRimFresnelPowerFactor);
                SetSafeValue(material, MToon10Prop.ParametricRimLiftFactor, mtoon10.ParametricRimLiftFactor);
            }

            // Outline
            {
                SetSafeValue(material, MToon10Prop.OutlineWidthMode, (int)mtoon10.OutlineWidthMode);
                SetSafeValue(material, MToon10Prop.OutlineWidthFactor, mtoon10.OutlineWidthFactor);
                SetTexture(material, vgoMaterial, allTexture2dList, MToon10Prop.OutlineWidthMultiplyTexture);
                SetColor(material, MToon10Prop.OutlineColorFactor, mtoon10.OutlineColorFactor);
                SetSafeValue(material, MToon10Prop.OutlineLightingMixFactor, mtoon10.OutlineLightingMixFactor);
            }

            // UV Animation
            {
                SetTexture(material, vgoMaterial, allTexture2dList, MToon10Prop.UvAnimationMaskTexture);
                SetSafeValue(material, MToon10Prop.UvAnimationScrollXSpeedFactor, mtoon10.UvAnimationScrollXSpeedFactor);
                SetSafeValue(material, MToon10Prop.UvAnimationScrollYSpeedFactor, mtoon10.UvAnimationScrollYSpeedFactor);
                SetSafeValue(material, MToon10Prop.UvAnimationRotationSpeedFactor, mtoon10.UvAnimationRotationSpeedFactor);
            }

            // for Editor
            {
                //int editorEditMode =
                //    (mtoon10.AlphaMode == MToon10AlphaMode.Transparent) &&
                //    (mtoon10.TransparentWithZWrite == MToon10TransparentWithZWriteMode.On) ? 1 : 0;

                SetSafeValue(material, MToon10Prop.EditorEditMode, mtoon10.EditorEditMode);
            }

            var mtoonValidator = new MToonValidator(material);

            mtoonValidator.Validate();

            return material;
        }

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
        /// <param name="material">A material.</param>
        /// <param name="property">A material property.</param>
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
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="allTexture2dList">List of all texture 2D.</param>
        /// <param name="property">A material property.</param>
        protected virtual void SetTexture(Material material, in VgoMaterial vgoMaterial, in List<Texture2D?> allTexture2dList, in MToon10Prop property)
        {
            string propertyName = property.ToUnityShaderLabName();

            if (material.HasProperty(propertyName) == false)
            {
                return;
            }

            int textureIndex = vgoMaterial.GetTextureIndexOrDefault(propertyName);

            Texture2D? texture2d = allTexture2dList.GetNullableValueOrDefault(textureIndex);

            if (texture2d != null)
            {
                material.SetTexture(propertyName, texture2d);
            }

            Vector2? textureOffset = vgoMaterial.GetTextureOffsetOrNull(propertyName);

            if (textureOffset.HasValue)
            {
                material.SetTextureOffset(propertyName, textureOffset.Value);
            }

            Vector2? textureScale = vgoMaterial.GetTextureScaleOrNull(propertyName);

            if (textureScale.HasValue)
            {
                material.SetTextureScale(propertyName, textureScale.Value);
            }
        }

        #endregion
    }
}
