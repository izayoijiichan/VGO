// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : VgoMaterialMToon10DefinitionExtensions
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using NewtonVgo;
    using System.Collections.Generic;
    using UnityEngine;
    using VRMShaders.VRM10.MToon10.Runtime;

    /// <summary>
    /// Vgo Material MToon 1.0 Definition Extensions
    /// </summary>
    public static class VgoMaterialMToon10DefinitionExtensions
    {
        /// <summary>
        /// Convert vgo material to MToon 1.0 definition.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="allTexture2dList">List of all texture 2D.</param>
        /// <returns>A MToon 1.0 definition.</returns>
        public static MToon10Definition ToMToon10Definition(this VgoMaterial vgoMaterial, in List<Texture2D?> allTexture2dList)
        {
            if ((vgoMaterial.shaderName != ShaderName.VRM_MToon10) &&
                (vgoMaterial.shaderName != ShaderName.VRM_URP_MToon10))
            {
                ThrowHelper.ThrowException($"vgoMaterial.shaderName: {vgoMaterial.shaderName}");
            }

            var mtoon10 = new MToon10Definition
            {
                // Rendering
                AlphaMode = vgoMaterial.GetEnumOrDefault<MToon10AlphaMode>(MToon10Prop.AlphaMode, MToon10AlphaMode.Opaque),
                TransparentWithZWrite = vgoMaterial.GetEnumOrDefault<MToon10TransparentWithZWriteMode>(MToon10Prop.TransparentWithZWrite, MToon10TransparentWithZWriteMode.Off),
                AlphaCutoff = vgoMaterial.GetSafeFloat(MToon10Prop.AlphaCutoff, 0.0f, 1.0f, 0.5f),
                RenderQueueOffsetNumber = vgoMaterial.GetIntOrDefault(MToon10Prop.RenderQueueOffsetNumber, 0),
                DoubleSided = vgoMaterial.GetEnumOrDefault<MToon10DoubleSidedMode>(MToon10Prop.DoubleSided, MToon10DoubleSidedMode.Off),

                // Color
                BaseColorFactor = vgoMaterial.GetColorOrDefault(MToon10Prop.BaseColorFactor, Color.white).gamma,
                BaseColorTexture = vgoMaterial.GetTextureOrDefault(MToon10Prop.BaseColorTexture, allTexture2dList),
                BaseColorTextureScale = vgoMaterial.GetTextureScaleOrDefault(MToon10Prop.BaseColorTexture, Vector2.one),
                BaseColorTextureOffset = vgoMaterial.GetTextureOffsetOrDefault(MToon10Prop.BaseColorTexture, Vector2.zero),
                ShadeColorFactor = vgoMaterial.GetColorOrDefault(MToon10Prop.ShadeColorFactor, Color.white).gamma,
                ShadeColorTexture = vgoMaterial.GetTextureOrDefault(MToon10Prop.ShadeColorTexture, allTexture2dList),
                NormalTexture = vgoMaterial.GetTextureOrDefault(MToon10Prop.NormalTexture, allTexture2dList),
                NormalTextureScale = vgoMaterial.GetSafeFloat(MToon10Prop.NormalTextureScale, 0.0f, float.MaxValue, 1.0f),
                ShadingShiftFactor = vgoMaterial.GetSafeFloat(MToon10Prop.ShadingShiftFactor, -1.0f, 1.0f, -0.05f),
                ShadingShiftTexture = vgoMaterial.GetTextureOrDefault(MToon10Prop.ShadingShiftTexture, allTexture2dList),
                ShadingShiftTextureScale = vgoMaterial.GetSafeFloat(MToon10Prop.ShadingShiftTextureScale, 0.0f, float.MaxValue, 1.0f),
                ShadingToonyFactor = vgoMaterial.GetSafeFloat(MToon10Prop.ShadingToonyFactor, 0.0f, 1.0f, 0.95f),

                // Global Illumination
                GiEqualizationFactor = vgoMaterial.GetSafeFloat(MToon10Prop.GiEqualizationFactor, 0.0f, 1.0f, 0.9f),

                // Emission
                EmissiveFactor = vgoMaterial.GetColorOrDefault(MToon10Prop.EmissiveFactor, Color.black).gamma,
                EmissiveTexture = vgoMaterial.GetTextureOrDefault(MToon10Prop.EmissiveTexture, allTexture2dList),

                // Rim Lighting
#if VRMC_VRMSHADERS_0_104_OR_NEWER
                MatcapColorFactor = vgoMaterial.GetColorOrDefault(MToon10Prop.MatcapColorFactor, Color.black).gamma,
#endif
                MatcapTexture = vgoMaterial.GetTextureOrDefault(MToon10Prop.MatcapTexture, allTexture2dList),
                ParametricRimColorFactor = vgoMaterial.GetColorOrDefault(MToon10Prop.ParametricRimColorFactor, Color.black).gamma,
                ParametricRimFresnelPowerFactor = vgoMaterial.GetSafeFloat(MToon10Prop.ParametricRimFresnelPowerFactor, 0.0f, 100.0f, 5.0f),
                ParametricRimLiftFactor = vgoMaterial.GetSafeFloat(MToon10Prop.ParametricRimLiftFactor, 0.0f, 1.0f),
                RimMultiplyTexture = vgoMaterial.GetTextureOrDefault(MToon10Prop.RimMultiplyTexture, allTexture2dList),
                RimLightingMixFactor = vgoMaterial.GetSafeFloat(MToon10Prop.RimLightingMixFactor, 0.0f, 1.0f, 1.0f),

                // Outline
                OutlineWidthMode = vgoMaterial.GetEnumOrDefault<MToon10OutlineMode>(MToon10Prop.OutlineWidthMode, MToon10OutlineMode.None),
                OutlineWidthFactor = vgoMaterial.GetSafeFloat(MToon10Prop.OutlineWidthFactor, 0.0f, 0.05f),
                OutlineWidthMultiplyTexture = vgoMaterial.GetTextureOrDefault(MToon10Prop.OutlineWidthMultiplyTexture, allTexture2dList),
                OutlineColorFactor = vgoMaterial.GetColorOrDefault(MToon10Prop.OutlineColorFactor, Color.black).gamma,
                OutlineLightingMixFactor = vgoMaterial.GetSafeFloat(MToon10Prop.OutlineLightingMixFactor, 0.0f, 1.0f, 1.0f),

                // UV Animation
                UvAnimationMaskTexture = vgoMaterial.GetTextureOrDefault(MToon10Prop.UvAnimationMaskTexture, allTexture2dList),
                UvAnimationScrollXSpeedFactor = vgoMaterial.GetSafeFloat(MToon10Prop.UvAnimationScrollXSpeedFactor, 0.0f, float.MaxValue, 0.0f),
                UvAnimationScrollYSpeedFactor = vgoMaterial.GetSafeFloat(MToon10Prop.UvAnimationScrollYSpeedFactor, 0.0f, float.MaxValue, 0.0f),
                UvAnimationRotationSpeedFactor = vgoMaterial.GetSafeFloat(MToon10Prop.UvAnimationRotationSpeedFactor, 0.0f, float.MaxValue, 0.0f),

                UnityCullMode = vgoMaterial.GetEnumOrDefault<UnityEngine.Rendering.CullMode>(MToon10Prop.UnityCullMode, UnityEngine.Rendering.CullMode.Back),
                UnitySrcBlend = vgoMaterial.GetEnumOrDefault<UnityEngine.Rendering.BlendMode>(MToon10Prop.UnitySrcBlend, UnityEngine.Rendering.BlendMode.One),
                UnityDstBlend = vgoMaterial.GetEnumOrDefault<UnityEngine.Rendering.BlendMode>(MToon10Prop.UnityDstBlend, UnityEngine.Rendering.BlendMode.Zero),
                UnityZWrite = vgoMaterial.GetIntOrDefault(MToon10Prop.UnityZWrite, 1) == 1,
                UnityAlphaToMask = vgoMaterial.GetIntOrDefault(MToon10Prop.UnityAlphaToMask, 1) == 1,

                EditorEditMode = vgoMaterial.GetIntOrDefault(MToon10Prop.EditorEditMode, 1),
            };

            return mtoon10;
        }

        #region Private Methods

        /// <summary>
        /// Gets enum value.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="self">A vgo material.</param>
        /// <param name="property">A material property.</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private static TEnum GetEnumOrDefault<TEnum>(this VgoMaterial self, in MToon10Prop property, TEnum? defaultValue = null) where TEnum : struct
        {
            string propertyName = property.ToUnityShaderLabName();

            return self.GetEnumOrDefault(propertyName, defaultValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self">A vgo material.</param>
        /// <param name="property">A material property.</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private static int GetIntOrDefault(this VgoMaterial self, in MToon10Prop property, int defaultValue = 0)
        {
            string propertyName = property.ToUnityShaderLabName();

            return self.GetIntOrDefault(propertyName, defaultValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self">A vgo material.</param>
        /// <param name="property">A material property.</param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private static float GetSafeFloat(this VgoMaterial self, in MToon10Prop property, in float min, in float max, float defaultValue = 0.0f)
        {
            string propertyName = property.ToUnityShaderLabName();

            return self.GetSafeFloat(propertyName, min, max, defaultValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self">A vgo material.</param>
        /// <param name="property">A material property.</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private static Color GetColorOrDefault(this VgoMaterial self, in MToon10Prop property, Color defaultValue)
        {
            string propertyName = property.ToUnityShaderLabName();

            return self.GetColorOrDefault(propertyName, defaultValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self">A vgo material.</param>
        /// <param name="property">A material property.</param>
        /// <param name="allTexture2dList">List of all texture 2D.</param>
        /// <returns></returns>
        private static Texture2D? GetTextureOrDefault(this VgoMaterial self, in MToon10Prop property, in List<Texture2D?> allTexture2dList)
        {
            string propertyName = property.ToUnityShaderLabName();

            Texture2D? texture = null;

            int textureIndex = self.GetTextureIndexOrDefault(propertyName);

            if (textureIndex >= 0)
            {
                texture = allTexture2dList.GetNullableValueOrDefault(textureIndex);
            }

            return texture;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self">A vgo material.</param>
        /// <param name="property">A material property.</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private static Vector2 GetTextureScaleOrDefault(this VgoMaterial self, in MToon10Prop property, Vector2 defaultValue)
        {
            string propertyName = property.ToUnityShaderLabName();

            return self.GetTextureScaleOrDefault(propertyName, defaultValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self">A vgo material.</param>
        /// <param name="property">A material property.</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private static Vector2 GetTextureOffsetOrDefault(this VgoMaterial self, in MToon10Prop property, Vector2 defaultValue)
        {
            string propertyName = property.ToUnityShaderLabName();

            return self.GetTextureOffsetOrDefault(propertyName, defaultValue);
        }

        #endregion
    }
}
