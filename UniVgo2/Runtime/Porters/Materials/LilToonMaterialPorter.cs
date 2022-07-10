// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : LilToonMaterialPorter
// ----------------------------------------------------------------------
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using System;
    using LilToonShader;
    using LilToonShader.Extensions;
    using UnityEngine;

    /// <summary>
    /// lilToon Material Porter
    /// </summary>
    public class LilToonMaterialPorter : MaterialPorterBase
    {
        #region Constructors

        /// <summary>
        /// Create a new instance of LilToonMaterialPorter.
        /// </summary>
        public LilToonMaterialPorter() : base() { }

        #endregion

        #region Public Methods (Export)

        /// <summary>
        /// Create a vgo material.
        /// </summary>
        /// <param name="material">A URP material.</param>
        /// <returns>A vgo material.</returns>
        public override VgoMaterial CreateVgoMaterial(Material material)
        {
            switch (material.shader.name)
            {
                // Opaque
                case ShaderName.Lil_LilToon:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Normal, LilRenderingMode.Opaque);
                case ShaderName.Lil_LilToonOutline:
                case ShaderName.Lil_LilToonOutlineOnly:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Normal, LilRenderingMode.Opaque, isOutline: true);

                // Cutout
                case ShaderName.Lil_LilToonCutout:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Normal, LilRenderingMode.Cutout);
                case ShaderName.Lil_LilToonOutlineOnlyCutout:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Normal, LilRenderingMode.Cutout, isOutline: true);

                // Transparent
                case ShaderName.Lil_LilToonTransparent:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Normal, LilRenderingMode.Transparent, LilTransparentMode.Normal);
                case ShaderName.Lil_LilToonTransparentOutline:
                case ShaderName.Lil_LilToonOutlineOnlyTransparent:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Normal, LilRenderingMode.Transparent, LilTransparentMode.Normal, isOutline: true);
                case ShaderName.Lil_LilToonOnePassTransparent:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Normal, LilRenderingMode.Transparent, LilTransparentMode.OnePass);
                case ShaderName.Lil_LilToonOnePassTransparentOutline:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Normal, LilRenderingMode.Transparent, LilTransparentMode.OnePass, isOutline: true);
                case ShaderName.Lil_LilToonTwoPassTransparent:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Normal, LilRenderingMode.Transparent, LilTransparentMode.TwoPass);
                case ShaderName.Lil_LilToonTwoPassTransparentOutline:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Normal, LilRenderingMode.Transparent, LilTransparentMode.TwoPass, isOutline: true);

                // Overlay
                case ShaderName.Lil_LilToonOverlay:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Normal, LilRenderingMode.Transparent, LilTransparentMode.Normal, isOverlay: true);
                case ShaderName.Lil_LilToonOverlayOnePass:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Normal, LilRenderingMode.Transparent, LilTransparentMode.OnePass, isOverlay: true);

                // Refraction
                case ShaderName.Lil_LilToonRefraction:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Normal, LilRenderingMode.Refraction);
                case ShaderName.Lil_LilToonRefractionBlur:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Normal, LilRenderingMode.RefractionBlur);

                // Fur
                case ShaderName.Lil_LilToonFur:
                case ShaderName.Lil_LilToonFurOnly:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Normal, LilRenderingMode.Fur);
                case ShaderName.Lil_LilToonFurCutout:
                case ShaderName.Lil_LilToonFurOnlyCutout:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Normal, LilRenderingMode.FurCutout);
                case ShaderName.Lil_LilToonFurTwoPass:
                case ShaderName.Lil_LilToonFurOnlyTwoPass:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Normal, LilRenderingMode.FurTwoPass, LilTransparentMode.TwoPass);

                // Gem
                case ShaderName.Lil_LilToonGem:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Normal, LilRenderingMode.Gem);

                // Tessellation
                case ShaderName.Lil_LilToonTessellation:
                    return CreateVgoMaterialFromLilToon(material, isTessellation: true, renderingMode: LilRenderingMode.Opaque);
                case ShaderName.Lil_LilToonTessellationOutline:
                    return CreateVgoMaterialFromLilToon(material, isTessellation: true, renderingMode: LilRenderingMode.Opaque, isOutline: true);
                case ShaderName.Lil_LilToonTessellationCutout:
                    return CreateVgoMaterialFromLilToon(material, isTessellation: true, renderingMode: LilRenderingMode.Cutout);
                case ShaderName.Lil_LilToonTessellationCutoutOutline:
                    return CreateVgoMaterialFromLilToon(material, isTessellation: true, renderingMode: LilRenderingMode.Cutout, isOutline: true);
                case ShaderName.Lil_LilToonTessellationTransparent:
                    return CreateVgoMaterialFromLilToon(material, isTessellation: true, renderingMode: LilRenderingMode.Transparent, transparentMode: LilTransparentMode.Normal);
                case ShaderName.Lil_LilToonTessellationTransparentOutline:
                    return CreateVgoMaterialFromLilToon(material, isTessellation: true, renderingMode: LilRenderingMode.Transparent, transparentMode: LilTransparentMode.Normal, isOutline: true);
                case ShaderName.Lil_LilToonTessellationOnePassTransparent:
                    return CreateVgoMaterialFromLilToon(material, isTessellation: true, renderingMode: LilRenderingMode.Transparent, transparentMode: LilTransparentMode.OnePass);
                case ShaderName.Lil_LilToonTessellationOnePassTransparentOutline:
                    return CreateVgoMaterialFromLilToon(material, isTessellation: true, renderingMode: LilRenderingMode.Transparent, transparentMode: LilTransparentMode.OnePass, isOutline: true);
                case ShaderName.Lil_LilToonTessellationTwoPassTransparent:
                    return CreateVgoMaterialFromLilToon(material, isTessellation: true, renderingMode: LilRenderingMode.Transparent, transparentMode: LilTransparentMode.TwoPass);
                case ShaderName.Lil_LilToonTessellationTwoPassTransparentOutline:
                    return CreateVgoMaterialFromLilToon(material, isTessellation: true, renderingMode: LilRenderingMode.Transparent, transparentMode: LilTransparentMode.TwoPass, isOutline: true);

                // Lite
                case ShaderName.Lil_LilToonLite:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Lite, LilRenderingMode.Opaque);
                case ShaderName.Lil_LilToonLiteOutline:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Lite, LilRenderingMode.Opaque, isOutline: true);
                case ShaderName.Lil_LilToonLiteCutout:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Lite, LilRenderingMode.Cutout);
                case ShaderName.Lil_LilToonLiteCutoutOutline:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Lite, LilRenderingMode.Cutout, isOutline: true);
                case ShaderName.Lil_LilToonLiteTransparent:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Lite, LilRenderingMode.Transparent, LilTransparentMode.Normal);
                case ShaderName.Lil_LilToonLiteTransparentOutline:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Lite, LilRenderingMode.Transparent, LilTransparentMode.Normal, isOutline: true);
                case ShaderName.Lil_LilToonLiteOnePassTransparent:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Lite, LilRenderingMode.Transparent, LilTransparentMode.OnePass);
                case ShaderName.Lil_LilToonLiteOnePassTransparentOutline:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Lite, LilRenderingMode.Transparent, LilTransparentMode.OnePass, isOutline: true);
                case ShaderName.Lil_LilToonLiteTwoPassTransparent:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Lite, LilRenderingMode.Transparent, LilTransparentMode.TwoPass);
                case ShaderName.Lil_LilToonLiteTwoPassTransparentOutline:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Lite, LilRenderingMode.Transparent, LilTransparentMode.TwoPass, isOutline: true);
                case ShaderName.Lil_LilToonLiteOverlay:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Lite, LilRenderingMode.Transparent, LilTransparentMode.Normal, isOverlay: true);
                case ShaderName.Lil_LilToonLiteOverlayOnePass:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Lite, LilRenderingMode.Transparent, LilTransparentMode.OnePass, isOverlay: true);

                // Multi
                case ShaderName.Lil_LilToonMulti:
                case ShaderName.Lil_LilToonMultiOutline:
                case ShaderName.Lil_LilToonMultiRefraction:
                case ShaderName.Lil_LilToonMultiFur:
                case ShaderName.Lil_LilToonMultiGem:
                    return CreateVgoMaterialFromLilToonMulti(material);

                // FakeShadow
                case ShaderName.Lil_LilToonFakeShadow:
                    return CreateVgoMaterialFromLilToonFakeShadow(material);

                // Pass
                case ShaderName.Lil_LilToonPassOpaque:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Normal, LilRenderingMode.Opaque);
                case ShaderName.Lil_LilToonPassCutout:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Normal, LilRenderingMode.Cutout);
                case ShaderName.Lil_LilToonPassTransparent:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Normal, LilRenderingMode.Transparent);

                case ShaderName.Lil_LilToonPassTessOpaque:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Normal, LilRenderingMode.Opaque, isTessellation: true);
                case ShaderName.Lil_LilToonPassTessCutout:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Normal, LilRenderingMode.Cutout, isTessellation: true);
                case ShaderName.Lil_LilToonPassTessTransparent:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Normal, LilRenderingMode.Transparent, isTessellation: true);

                case ShaderName.Lil_LilToonPassLiteOpaque:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Lite, LilRenderingMode.Opaque);
                case ShaderName.Lil_LilToonPassLiteCutout:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Lite, LilRenderingMode.Cutout);
                case ShaderName.Lil_LilToonPassLiteTransparent:
                    return CreateVgoMaterialFromLilToon(material, LilShaderType.Lite, LilRenderingMode.Transparent);

                case ShaderName.Lil_LilToonPassDummy:
                case ShaderName.Lil_LilToonOtherBaker:
                default:
                    throw new NotSupportedException(material.shader.name);
            }
        }

        /// <summary>
        /// Create a vgo material from a lilToon material.
        /// </summary>
        /// <param name="material">A lilToon material.</param>
        /// <param name="shaderType"></param>
        /// <param name="renderingMode"></param>
        /// <param name="transparentMode"></param>
        /// <param name="isOverlay"></param>
        /// <param name="isOutline"></param>
        /// <param name="isTessellation"></param>
        /// <returns>A vgo material.</returns>
        protected virtual VgoMaterial CreateVgoMaterialFromLilToon(
            Material material,
            LilShaderType shaderType = LilShaderType.Normal,
            LilRenderingMode renderingMode = LilRenderingMode.Opaque,
            LilTransparentMode transparentMode = LilTransparentMode.Normal,
            bool isOverlay = false,
            bool isOutline = false,
            bool isTessellation = false)
        {
            var vgoMaterial = new VgoMaterial()
            {
                name = material.name,
                shaderName = material.shader.name,
                renderQueue = material.renderQueue,
                isUnlit = material.GetSafeBool(LilToonShader.PropertyName.AsUnlit),
            };

            ExportProperties(vgoMaterial, material);

            ExportLilTextureProperties(vgoMaterial, material, shaderType, renderingMode, isOutline: isOutline);

            ExportKeywords(vgoMaterial, material);

            return vgoMaterial;
        }

        /// <summary>
        /// Create a vgo material from a lilToon FakeShadow material.
        /// </summary>
        /// <param name="material">A lilToon FakeShadow material.</param>
        /// <returns>A vgo material.</returns>
        protected virtual VgoMaterial CreateVgoMaterialFromLilToonFakeShadow(Material material)
        {
            var vgoMaterial = new VgoMaterial()
            {
                name = material.name,
                shaderName = material.shader.name,
                renderQueue = material.renderQueue,
                isUnlit = false,
            };

            ExportProperties(vgoMaterial, material);

            // Main
            base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.MainTex, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);

            // Save
            //ExportTextureProperty(vgoMaterial, material, Property.BaseMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
            //ExportTextureProperty(vgoMaterial, material, Property.BaseColorMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);

            ExportKeywords(vgoMaterial, material);

            return vgoMaterial;
        }

        /// <summary>
        /// Create a vgo material from a lilToon Multi material.
        /// </summary>
        /// <param name="material">A lilToon Multi material.</param>
        /// <returns>A vgo material.</returns>
        protected virtual VgoMaterial CreateVgoMaterialFromLilToonMulti(Material material)
        {
            // @notice Multi.TransparentMode is RenderingMode
            LilRenderingMode renderingMode = material.GetSafeEnum<LilRenderingMode>(LilToonShader.PropertyName.TransparentMode);

            bool useOutline = material.GetSafeBool(LilToonShader.PropertyName.UseOutline);

            bool asOverlay = material.GetSafeBool(LilToonShader.PropertyName.AsOverlay);

            var vgoMaterial = CreateVgoMaterialFromLilToon(material, LilShaderType.Normal, renderingMode, isOverlay: asOverlay, isOutline: useOutline);

            return vgoMaterial;
        }

        /// <summary>
        /// Export lilToon texture type properties.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="material">A lilToon material.</param>
        /// <param name="shaderType"></param>
        /// <param name="renderingMode"></param>
        /// <param name="isOutline"></param>
        protected virtual void ExportLilTextureProperties(
            VgoMaterial vgoMaterial,
            Material material,
            LilShaderType shaderType = LilShaderType.Normal,
            LilRenderingMode renderingMode = LilRenderingMode.Opaque,
            bool isOutline = false)
        {
            int lilToonVersin = material.GetLilToonVersion();

            // Base
            if (shaderType == LilShaderType.Lite)
            {
                base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.TriMask, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
            }

            // Main
            base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.MainTex, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);

            if (shaderType == LilShaderType.Normal)
            {
                // Main 1st
                if (material.GetSafeFloat(LilToonShader.PropertyName.MainGradationStrength) != 0.0f)
                {
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.MainGradationTex, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                }
                base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.MainColorAdjustMask, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);

                // Main 2nd
                if (material.GetSafeBool(LilToonShader.PropertyName.UseMain2ndTex))
                {
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.Main2ndTex, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.Main2ndBlendMask, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.Main2ndDissolveMask, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.Main2ndDissolveNoiseMask, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                }

                // Main 3rd
                if (material.GetSafeBool(LilToonShader.PropertyName.UseMain3rdTex))
                {
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.Main3rdTex, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.Main3rdBlendMask, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.Main3rdDissolveMask, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.Main3rdDissolveNoiseMask, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                }
            }

            // Alpha Mask
            if (shaderType == LilShaderType.Normal)
            {
                if (material.GetSafeEnum<LilAlphaMaskMode>(LilToonShader.PropertyName.AlphaMaskMode) != LilAlphaMaskMode.None)
                {
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.AlphaMask, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                }
            }

            // NormalMap
            if (shaderType == LilShaderType.Normal)
            {
                // NormalMap 1st
                if (material.GetSafeBool(LilToonShader.PropertyName.UseBumpMap))
                {
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.BumpMap, VgoTextureMapType.NormalMap, VgoColorSpaceType.Linear);
                }

                // NormalMap 2nd
                if (material.GetSafeBool(LilToonShader.PropertyName.UseBump2ndMap))
                {
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.Bump2ndMap, VgoTextureMapType.NormalMap, VgoColorSpaceType.Linear);
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.Bump2ndScaleMask, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                }
            }

            // Anisotropy
            if (shaderType == LilShaderType.Normal)
            {
                if (material.GetSafeBool(LilToonShader.PropertyName.UseAnisotropy))
                {
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.AnisotropyTangentMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.AnisotropyScaleMask, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.AnisotropyShiftNoiseMask, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                }
            }

            // Backlight
            if (shaderType == LilShaderType.Normal)
            {
                if (renderingMode != LilRenderingMode.Gem)
                {
                    if (material.GetSafeBool(LilToonShader.PropertyName.UseBacklight))
                    {
                        base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.BacklightColorTex, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                    }
                }
            }

            // Shadow
            if (material.GetSafeBool(LilToonShader.PropertyName.UseShadow))
            {
                if (shaderType == LilShaderType.Normal)
                {
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.ShadowStrengthMask, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.ShadowBorderMask, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.ShadowBlurMask, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                }

                base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.ShadowColorTex, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.Shadow2ndColorTex, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);

                if (shaderType == LilShaderType.Normal)
                {
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.Shadow3rdColorTex, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                }
            }

            // Reflection
            if (shaderType == LilShaderType.Normal)
            {
                if (material.GetSafeBool(LilToonShader.PropertyName.UseReflection))
                {
                    float smoothness = -1.0f;

                    if (material.HasProperty(LilToonShader.PropertyName.Smoothness))
                    {
                        smoothness = material.GetFloat(LilToonShader.PropertyName.Smoothness);
                    }
                    else
                    {
                        Debug.LogWarning($"{material.shader.name} does not have {LilToonShader.PropertyName.Smoothness} property.");
                    }

                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.SmoothnessTex, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.MetallicGlossMap, VgoTextureMapType.MetallicRoughnessMap, VgoColorSpaceType.Linear, smoothness);
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.ReflectionColorTex, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.ReflectionCubeTex, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                }
            }

            // MatCap 1st
            if (material.GetSafeBool(LilToonShader.PropertyName.UseMatCap))
            {
                base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.MatCapTex, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);

                if (shaderType == LilShaderType.Normal)
                {
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.MatCapBlendMask, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.MatCapBumpMap, VgoTextureMapType.NormalMap, VgoColorSpaceType.Linear);
                }
            }

            // MatCap 2nd
            if (shaderType == LilShaderType.Normal)
            {
                if (material.GetSafeBool(LilToonShader.PropertyName.UseMatCap2nd))
                {
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.MatCap2ndTex, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.MatCap2ndBlendMask, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.MatCap2ndBumpMap, VgoTextureMapType.NormalMap, VgoColorSpaceType.Linear);
                }
            }

            // Rim
            if (material.GetSafeBool(LilToonShader.PropertyName.UseRim))
            {
                if (shaderType == LilShaderType.Normal)
                {
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.RimColorTex, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                }
            }

            // Glitter
            if (shaderType == LilShaderType.Normal)
            {
                if (material.GetSafeBool(LilToonShader.PropertyName.UseGlitter))
                {
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.GlitterColorTex, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);

                    if (lilToonVersin >= 26)  // v1.3.0
                    {
                        base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.GlitterShapeTex, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                    }
                }
            }

            // Emission 1st
            if (material.GetSafeBool(LilToonShader.PropertyName.UseEmission))
            {
                base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.EmissionMap, VgoTextureMapType.EmissionMap, VgoColorSpaceType.Srgb);

                if (shaderType == LilShaderType.Normal)
                {
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.EmissionBlendMask, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.EmissionGradTex, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                }
            }

            // Emission 2nd
            if (shaderType == LilShaderType.Normal)
            {
                if (material.GetSafeBool(LilToonShader.PropertyName.UseEmission2nd))
                {
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.Emission2ndMap, VgoTextureMapType.EmissionMap, VgoColorSpaceType.Srgb);
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.Emission2ndBlendMask, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.Emission2ndGradTex, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                }
            }

            // Parallax
            if (shaderType == LilShaderType.Normal)
            {
                if (material.GetSafeBool(LilToonShader.PropertyName.UseParallax))
                {
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.ParallaxMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                }
            }

            // Audio Link
            if (shaderType == LilShaderType.Normal)
            {
                if (material.GetSafeBool(LilToonShader.PropertyName.UseAudioLink))
                {
                    if (material.GetSafeEnum<LilAudioLinkUVMode>(LilToonShader.PropertyName.AudioLinkUVMode) == LilAudioLinkUVMode.Mask)
                    {
                        base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.AudioLinkMask, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                    }
                    if (material.GetSafeBool(LilToonShader.PropertyName.AudioLinkAsLocal))
                    {
                        base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.AudioLinkLocalMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                    }
                }
            }

            // Dissolve
            if (shaderType == LilShaderType.Normal)
            {
                base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.DissolveMask, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.DissolveNoiseMask, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
            }

            // Fur
            if ((renderingMode == LilRenderingMode.Fur) ||
                (renderingMode == LilRenderingMode.FurCutout) ||
                (renderingMode == LilRenderingMode.FurTwoPass))
            {
                base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.FurNoiseMask, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.FurMask, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.FurLengthMask, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.FurVectorTex, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
            }

            // Outline
            if (isOutline)
            {
                base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.OutlineTex, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.OutlineWidthMask, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);

                if (shaderType == LilShaderType.Normal)
                {
                    base.ExportTextureProperty(vgoMaterial, material, LilToonShader.PropertyName.OutlineVectorTex, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                }
            }

            // Save
            //ExportTextureProperty(vgoMaterial, material, Property.BaseMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
            //ExportTextureProperty(vgoMaterial, material, Property.BaseColorMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
        }

        #endregion

        #region Public Methods (Import)

        /// <summary>
        /// Create a lilToon material.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="shader">A lilToon shader.</param>
        /// <returns>A lilToon material.</returns>
        public override Material CreateMaterialAsset(VgoMaterial vgoMaterial, Shader shader)
        {
            if ((shader.name.Contains("lilToon") == false) &&
                (shader.name.StartsWith("Hidden/ltspass") == false))
            {
                throw new NotSupportedException(vgoMaterial.shaderName);
            }

            Material material = base.CreateMaterialAsset(vgoMaterial, shader);

            return material;
        }

        #endregion
    }
}
