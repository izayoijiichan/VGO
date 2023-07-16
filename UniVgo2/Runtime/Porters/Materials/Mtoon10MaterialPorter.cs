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
        public override VgoMaterial CreateVgoMaterial(Material material, IVgoStorage vgoStorage)
        {
            var mtoonContext = new MToon10Context(material);

            mtoonContext.Validate();

            VgoMaterial vgoMaterial = new VgoMaterial()
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
        protected virtual bool ExportProperty(VgoMaterial vgoMaterial, Material material, MToon10Prop property, VgoMaterialPropertyType type)
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
        protected virtual bool ExportProperty(VgoMaterial vgoMaterial, Material material, VgoMaterialPropertyType type, MToon10Prop property)
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
        protected virtual bool ExportTextureProperty(IVgoStorage vgoStorage, VgoMaterial vgoMaterial, Material material, MToon10Prop property, VgoTextureMapType textureMapType = VgoTextureMapType.Default, VgoColorSpaceType colorSpace = VgoColorSpaceType.Srgb, float metallicSmoothness = -1.0f)
        {
            return ExportTextureProperty(vgoStorage, vgoMaterial, material, property.ToUnityShaderLabName(), textureMapType, colorSpace, metallicSmoothness);
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
            if (vgoMaterial.shaderName != ShaderName.VRM_MToon10)
            {
                ThrowHelper.ThrowArgumentException($"vgoMaterial.shaderName: {vgoMaterial.shaderName}");
            }

            if (shader.name != ShaderName.VRM_MToon10)
            {
                ThrowHelper.ThrowArgumentException($"shader.name: {shader.name}");
            }

            //Material material = base.CreateMaterialAsset(vgoMaterial, shader, allTexture2dList);

            var material = new Material(shader)
            {
                name = vgoMaterial.name
            };

            // Rendering
            {
                SetSafeValue(material, vgoMaterial, MToon10Prop.AlphaMode, 0, 2, (int)MToon10AlphaMode.Opaque);
                SetSafeValue(material, vgoMaterial, MToon10Prop.TransparentWithZWrite, 0, 1, (int)MToon10TransparentWithZWriteMode.Off);
                SetSafeValue(material, vgoMaterial, MToon10Prop.AlphaCutoff, 0.0f, 1.0f, 0.5f);
                //SetSafeValue(material, vgoMaterial, MToon10Prop.RenderQueueOffsetNumber, 0);
                SetSafeValue(material, vgoMaterial, MToon10Prop.DoubleSided, 0, 1, (int)MToon10DoubleSidedMode.Off);
            }

            // Unity ShaderPass Mode
            {
                //SetSafeValue(material, vgoMaterial, MToon10Prop.UnityCullMode, 0, 2, (int)UnityCullMode.Back);
                //SetSafeValue(material, vgoMaterial, MToon10Prop.UnitySrcBlend, (int)UnityEngine.Rendering.BlendMode.One);
                //SetSafeValue(material, vgoMaterial, MToon10Prop.UnityDstBlend, (int)UnityEngine.Rendering.BlendMode.Zero);
                //SetSafeValue(material, vgoMaterial, MToon10Prop.UnityZWrite, (int)UnityZWriteMode.On);
                //SetSafeValue(material, vgoMaterial, MToon10Prop.UnityAlphaToMask, 0, 1, (int)UnityAlphaToMaskMode.Off);

                MToon10AlphaMode alphaMode = (MToon10AlphaMode)vgoMaterial.GetIntOrDefault(MToon10Prop.AlphaMode.ToUnityShaderLabName());

                MToon10TransparentWithZWriteMode zWriteMode = (MToon10TransparentWithZWriteMode)vgoMaterial.GetIntOrDefault(MToon10Prop.TransparentWithZWrite.ToUnityShaderLabName());

                MToon10DoubleSidedMode doubleSidedMode = (MToon10DoubleSidedMode)vgoMaterial.GetIntOrDefault(MToon10Prop.DoubleSided.ToUnityShaderLabName());

                int renderQueueOffsetNumber = vgoMaterial.GetIntOrDefault(MToon10Prop.RenderQueueOffsetNumber.ToUnityShaderLabName());

                SetUnityShaderPassSettings(material, alphaMode, zWriteMode, renderQueueOffsetNumber);

                UnityCullMode cullMode = (doubleSidedMode == MToon10DoubleSidedMode.On) ? UnityCullMode.Off : UnityCullMode.Back;

                material.SetInt(MToon10Prop.UnityCullMode, (int)cullMode);
            }

            // Keywords (Initialize)
            {
                material.SetKeyword(MToon10NormalMapKeyword.On, false);
                material.SetKeyword(MToon10EmissiveMapKeyword.On, false);
                material.SetKeyword(MToon10RimMapKeyword.On, false);
                material.SetKeyword(MToon10ParameterMapKeyword.On, false);
            }

            // Color
            {
                // Base (Main)
                SetColor(material, vgoMaterial, MToon10Prop.BaseColorFactor, Color.white);
                SetTexture(material, vgoMaterial, allTexture2dList, MToon10Prop.BaseColorTexture);

                // Shade
                SetColor(material, vgoMaterial, MToon10Prop.ShadeColorFactor, Color.white);
                SetTexture(material, vgoMaterial, allTexture2dList, MToon10Prop.ShadeColorTexture);

                // Normal (Bump)
                SetTexture(material, vgoMaterial, allTexture2dList, MToon10Prop.NormalTexture);
                SetSafeValue(material, vgoMaterial, MToon10Prop.NormalTextureScale, 0.0f, float.MaxValue, 1.0f);

                // Shading Shift
                SetSafeValue(material, vgoMaterial, MToon10Prop.ShadingShiftFactor, -1.0f, 1.0f, -0.05f);
                SetTexture(material, vgoMaterial, allTexture2dList, MToon10Prop.ShadingShiftTexture);
                SetSafeValue(material, vgoMaterial, MToon10Prop.ShadingShiftTextureScale, 0.0f, float.MaxValue, 1.0f);

                // Shading Toony
                SetSafeValue(material, vgoMaterial, MToon10Prop.ShadingToonyFactor, 0.0f, 1.0f, 0.95f);
            }

            // Global Illumination
            {
                SetSafeValue(material, vgoMaterial, MToon10Prop.GiEqualizationFactor, 0.0f, 1.0f, 0.9f);
            }

            // Emission
            {
                SetTexture(material, vgoMaterial, allTexture2dList, MToon10Prop.EmissiveTexture);
                SetColor(material, vgoMaterial, MToon10Prop.EmissiveFactor, Color.black);
            }

            // Rim Lighting
            {
                SetTexture(material, vgoMaterial, allTexture2dList, MToon10Prop.RimMultiplyTexture);
                SetSafeValue(material, vgoMaterial, MToon10Prop.RimLightingMixFactor, 0.0f, 1.0f, 1.0f);

                // Mat Cap
#if VRMC_VRMSHADERS_0_104_OR_NEWER
                SetColor(material, vgoMaterial, MToon10Prop.MatcapColorFactor, Color.black);
#endif
                SetTexture(material, vgoMaterial, allTexture2dList, MToon10Prop.MatcapTexture);

                // Parametric Rim
                SetColor(material, vgoMaterial, MToon10Prop.ParametricRimColorFactor, Color.black);
                SetSafeValue(material, vgoMaterial, MToon10Prop.ParametricRimFresnelPowerFactor, 0.0f, 100.0f, 5.0f);
                SetSafeValue(material, vgoMaterial, MToon10Prop.ParametricRimLiftFactor, 0.0f, 1.0f, 0.0f);
            }

            // Outline
            {
                MToon10OutlineMode outlineWidthMode = (MToon10OutlineMode)vgoMaterial.GetIntOrDefault(MToon10Prop.OutlineWidthMode.ToUnityShaderLabName());
 
                switch (outlineWidthMode)
                {
                    case MToon10OutlineMode.World:
                        material.SetKeyword(MToon10OutlineModeKeyword.World, true);
                        material.SetKeyword(MToon10OutlineModeKeyword.Screen, false);
                        break;
                    case MToon10OutlineMode.Screen:
                        material.SetKeyword(MToon10OutlineModeKeyword.World, false);
                        material.SetKeyword(MToon10OutlineModeKeyword.Screen, true);
                        break;
                    case MToon10OutlineMode.None:
                    default:
                        material.SetKeyword(MToon10OutlineModeKeyword.World, false);
                        material.SetKeyword(MToon10OutlineModeKeyword.Screen, false);
                        break;
                }

                SetSafeValue(material, vgoMaterial, MToon10Prop.OutlineWidthMode, 0, 2, (int)MToon10OutlineMode.None);
                SetSafeValue(material, vgoMaterial, MToon10Prop.OutlineWidthFactor, 0.0f, 0.05f, 0.0f);
                SetTexture(material, vgoMaterial, allTexture2dList, MToon10Prop.OutlineWidthMultiplyTexture);
                SetColor(material, vgoMaterial, MToon10Prop.OutlineColorFactor, Color.black);
                SetSafeValue(material, vgoMaterial, MToon10Prop.OutlineLightingMixFactor, 0.0f, 1.0f, 1.0f);
            }

            // UV Animation
            {
                SetTexture(material, vgoMaterial, allTexture2dList, MToon10Prop.UvAnimationMaskTexture);
                SetSafeValue(material, vgoMaterial, MToon10Prop.UvAnimationScrollXSpeedFactor, 0.0f, float.MaxValue, 0.0f);
                SetSafeValue(material, vgoMaterial, MToon10Prop.UvAnimationScrollYSpeedFactor, 0.0f, float.MaxValue, 0.0f);
                SetSafeValue(material, vgoMaterial, MToon10Prop.UvAnimationRotationSpeedFactor, 0.0f, float.MaxValue, 0.0f);
            }

            // for Editor
            {
                //int editorEditMode = 
                //    (alphaMode == MToon10AlphaMode.Transparent) && 
                //    (zWriteMode == MToon10TransparentWithZWriteMode.On) ? 1 : 0;

                SetSafeValue(material, vgoMaterial, MToon10Prop.EditorEditMode, 1);
            }

            return material;
        }

        #endregion

        #region Protected Methods (Import)

        /// <summary>
        /// Sets unity shader pass settings.
        /// </summary>
        /// <param name="material">A MToon material.</param>
        /// <param name="alphaMode">An alpha mode.</param>
        /// <param name="zWriteMode">A z-write mode.</param>
        /// <param name="renderQueueOffsetNumber">A render queue offset number.</param>
        protected virtual void SetUnityShaderPassSettings(Material material, in MToon10AlphaMode alphaMode, in MToon10TransparentWithZWriteMode zWriteMode, in int renderQueueOffsetNumber)
        {
            int renderQueueOffset = 0;

            switch (alphaMode)
            {
                case MToon10AlphaMode.Opaque:
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
                case MToon10AlphaMode.Cutout:
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
                case MToon10AlphaMode.Transparent:
                    material.SetOverrideTag(UnityRenderTag.Key, UnityRenderTag.TransparentValue);

                    material.SetInt(MToon10Prop.UnitySrcBlend, (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt(MToon10Prop.UnityDstBlend, (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt(MToon10Prop.UnityAlphaToMask, (int)UnityAlphaToMaskMode.Off);

                    material.SetKeyword(UnityAlphaModeKeyword.AlphaTest, false);
                    material.SetKeyword(UnityAlphaModeKeyword.AlphaBlend, true);
                    material.SetKeyword(UnityAlphaModeKeyword.AlphaPremultiply, false);

                    if (zWriteMode == MToon10TransparentWithZWriteMode.On)
                    {
                        material.SetInt(MToon10Prop.UnityZWrite, (int)UnityZWriteMode.On);

                        renderQueueOffset = Mathf.Clamp(renderQueueOffsetNumber, 0, +9);

                        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.GeometryLast + 1 + renderQueueOffset; // Transparent First + N
                    }
                    else
                    {
                        material.SetInt(MToon10Prop.UnityZWrite, (int)UnityZWriteMode.Off);

                        renderQueueOffset = Mathf.Clamp(renderQueueOffsetNumber, -9, 0);

                        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent + renderQueueOffset;
                    }
                    break;
                default:
                    //ThrowHelper.ThrowArgumentOutOfRangeException(nameof(alphaMode), alphaMode);
                    break;
            }

            material.SetInt(MToon10Prop.RenderQueueOffsetNumber, renderQueueOffset);
        }

        protected virtual void SetSafeValue(Material material, VgoMaterial vgoMaterial, MToon10Prop property, int defaultValue = default)
        {
            string propertyName = property.ToUnityShaderLabName();

            int value = vgoMaterial.GetIntOrDefault(propertyName, defaultValue);

            material.SetSafeInt(propertyName, value);
        }

        protected virtual void SetSafeValue(Material material, VgoMaterial vgoMaterial, MToon10Prop property, int min, int max, int defaultValue = default)
        {
            string propertyName = property.ToUnityShaderLabName();

            int value = vgoMaterial.GetSafeInt(propertyName, min, max, defaultValue);

            material.SetSafeInt(propertyName, value);
        }

        protected virtual void SetSafeValue(Material material, VgoMaterial vgoMaterial, MToon10Prop property, float defaultValue = default)
        {
            string propertyName = property.ToUnityShaderLabName();

            float value = vgoMaterial.GetFloatOrDefault(propertyName, defaultValue);

            material.SetSafeFloat(propertyName, value, minValue: null, maxValue: null);
        }

        protected virtual void SetSafeValue(Material material, VgoMaterial vgoMaterial, MToon10Prop property, float min, float max, float defaultValue = default)
        {
            string propertyName = property.ToUnityShaderLabName();

            float value = vgoMaterial.GetSafeFloat(propertyName, min, max, defaultValue);

            material.SetSafeFloat(propertyName, value, minValue: null, maxValue: null);
        }

        protected virtual void SetColor(Material material, VgoMaterial vgoMaterial, MToon10Prop property, Color defaultValue = default)
        {
            string propertyName = property.ToUnityShaderLabName();

            Color color = vgoMaterial.GetColorOrDefault(propertyName, defaultValue).gamma;

            material.SetSafeColor(propertyName, color);
        }

        protected virtual void SetTexture(Material material, VgoMaterial vgoMaterial, List<Texture2D?> allTexture2dList, MToon10Prop property)
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
                if (property == MToon10Prop.NormalTexture)
                {
                    material.SetKeyword(MToon10NormalMapKeyword.On, true);
                }
                else if (property == MToon10Prop.EmissiveTexture)
                {
                    material.SetKeyword(MToon10EmissiveMapKeyword.On, true);
                }
                else if (
                    (property == MToon10Prop.MatcapTexture) ||
                    (property == MToon10Prop.RimMultiplyTexture))
                {
                    material.SetKeyword(MToon10RimMapKeyword.On, true);
                }
                else if (
                    (property == MToon10Prop.ShadingShiftTexture) ||
                    (property == MToon10Prop.OutlineWidthMultiplyTexture) ||
                    (property == MToon10Prop.UvAnimationMaskTexture))
                {
                    material.SetKeyword(MToon10ParameterMapKeyword.On, true);
                }

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
