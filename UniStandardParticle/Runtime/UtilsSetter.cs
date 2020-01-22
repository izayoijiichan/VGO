// ----------------------------------------------------------------------
// @Namespace : UniStandardParticle
// @Class     : Utils
// ----------------------------------------------------------------------
namespace UniStandardParticle
{
    using UnityEngine;

    public static partial class Utils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="parameters"></param>
        public static void SetParticleParametersToMaterial(Material material, ParticleDefinition parameters)
        {
            material.SetInt(PropBlendMode, (int)parameters.RenderMode);

            //SetColorMode(material, parameters.ColorMode);

            SetInt(material, PropFlipbookMode, (int)parameters.FlipBookMode);
            SetInt(material, PropCullMode, (int)parameters.CullMode);

            SetKeyword(material, KeyRequireUv2, parameters.FlipBookMode == FlipBookMode.Blended);
            SetKeyword(material, KeyTwoSided, parameters.CullMode == UnityEngine.Rendering.CullMode.Off);
            SetKeyword(material, KeyFadingOn, parameters.CameraFadingEnabled);
            SetKeyword(material, KeyDistortionOn, parameters.DistortionEnabled);
            SetKeyword(material, KeyEffectBump, parameters.DistortionEnabled);
            SetKeyword(material, KeyNormalMap, (parameters.BumpMap != null));
            SetKeyword(material, KeyMetallicGlossMap, (parameters.MetallicGlossMap != null));
            SetKeyword(material, KeyEmission, parameters.EmissionEnabled);

            SetBool(material, PropSoftParticlesEnabled, parameters.SoftParticlesEnabled);
            SetVector(material, PropSoftParticleFadeParams, parameters.SoftParticleFadeParams);
            //SetFloat(material, PropSoftParticleNearFadeDistance, parameters.SoftParticleFadeParams.x);
            //SetFloat(material, PropSoftParticleFarFadeDistance, parameters.SoftParticleFadeParams.y);

            SetBool(material, PropCameraFadingEnabled, parameters.CameraFadingEnabled);
            SetVector(material, PropCameraFadeParams, parameters.CameraFadeParams);
            //SetFloat(material, PropCameraNearFadeDistance, parameters.CameraFadeParams.x);
            //SetFloat(material, PropCameraFarFadeDistance, parameters.CameraFadeParams.y);

            SetBool(material, PropDistortionEnabled, parameters.DistortionEnabled);
            SetTexture(material, PropGrabTexture, parameters.GrabTexture);
            SetFloat(material, PropDistortionStrengthScaled, parameters.DistortionStrengthScaled);
            SetFloat(material, PropDistortionBlend, parameters.DistortionBlend);
            SetColor(material, PropColorAddSubDiff, parameters.ColorAddSubDiff);

            SetTexture(material, PropMainTex, parameters.MainTex);
            SetVector(material, PropMainTexSt, parameters.MainTexSt);
            //material.SetTextureScale(PropMainTex, new Vector2(parameters.MainTexSt[0], parameters.MainTexSt[1]));
            //material.SetTextureOffset(PropMainTex, new Vector2(parameters.MainTexSt[2], parameters.MainTexSt[3]));
            SetColor(material, PropColor, parameters.Color);
            SetFloat(material, PropCutoff, parameters.Cutoff);

            SetTexture(material, PropMetallicGlossMap, parameters.MetallicGlossMap);
            SetFloat(material, PropMetallic, parameters.Metallic);
            SetFloat(material, PropGlossiness, parameters.Glossiness);

            SetTexture(material, PropBumpMap, parameters.BumpMap);
            SetFloat(material, PropBumpScale, parameters.BumpScale);

            SetBool(material, PropLightingEnabled, parameters.LightingEnabled);
            SetBool(material, PropEmissionEnabled, parameters.EmissionEnabled);
            SetColor(material, PropEmissionColor, parameters.EmissionColor);
            SetTexture(material, PropEmissionMap, parameters.EmissionMap);

            material.SetShaderPassEnabled("Always", parameters.DistortionEnabled);

            //material.mainTexture = parameters.MainTex;
            //material.color = parameters.Color;
            //material.mainTextureScale = new Vector2(parameters.MainTexSt[0], parameters.MainTexSt[1]);
            //material.mainTextureOffset = new Vector2(parameters.MainTexSt[2], parameters.MainTexSt[3]);

            SetBlendMode(material, parameters.RenderMode);
            SetColorMode(material, parameters.ColorMode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="blendMode"></param>
        public static void SetBlendMode(Material material, BlendMode blendMode)
        {
            //material.SetInt(PropBlendMode, (int)blendMode);

            switch (blendMode)
            {
                case BlendMode.Opaque:
                    material.SetOverrideTag(TagRenderTypeKey, TagRenderTypeValueOpaque);
                    material.SetInt(PropBlendOp, (int)UnityEngine.Rendering.BlendOp.Add);
                    material.SetInt(PropSrcBlend, (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt(PropDstBlend, (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt(PropZWrite, 1);
                    SetKeyword(material, KeyAlphaTestOn, false);
                    SetKeyword(material, KeyAlphaBlendOn, false);
                    SetKeyword(material, KeyAlphaPremultiplyOn, false);
                    SetKeyword(material, KeyAlphaModulateOn, false);
                    material.renderQueue = -1;
                    break;
                case BlendMode.Cutout:
                    material.SetOverrideTag(TagRenderTypeKey, TagRenderTypeValueTransparentCutout);
                    material.SetInt(PropBlendOp, (int)UnityEngine.Rendering.BlendOp.Add);
                    material.SetInt(PropSrcBlend, (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt(PropDstBlend, (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt(PropZWrite, 1);
                    SetKeyword(material, KeyAlphaTestOn, true);
                    SetKeyword(material, KeyAlphaBlendOn, false);
                    SetKeyword(material, KeyAlphaPremultiplyOn, false);
                    SetKeyword(material, KeyAlphaModulateOn, false);
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;
                    break;
                case BlendMode.Fade:
                    material.SetOverrideTag(TagRenderTypeKey, TagRenderTypeValueTransparent);
                    material.SetInt(PropBlendOp, (int)UnityEngine.Rendering.BlendOp.Add);
                    material.SetInt(PropSrcBlend, (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt(PropDstBlend, (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt(PropZWrite, 0);
                    SetKeyword(material, KeyAlphaTestOn, false);
                    SetKeyword(material, KeyAlphaBlendOn, true);
                    SetKeyword(material, KeyAlphaPremultiplyOn, false);
                    SetKeyword(material, KeyAlphaModulateOn, false);
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                    break;
                case BlendMode.Transparent:
                    material.SetOverrideTag(TagRenderTypeKey, TagRenderTypeValueTransparent);
                    material.SetInt(PropBlendOp, (int)UnityEngine.Rendering.BlendOp.Add);
                    material.SetInt(PropSrcBlend, (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt(PropDstBlend, (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt(PropZWrite, 0);
                    SetKeyword(material, KeyAlphaTestOn, false);
                    SetKeyword(material, KeyAlphaBlendOn, false);
                    SetKeyword(material, KeyAlphaPremultiplyOn, true);
                    SetKeyword(material, KeyAlphaModulateOn, false);
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                    break;
                case BlendMode.Additive:
                    material.SetOverrideTag(TagRenderTypeKey, TagRenderTypeValueTransparent);
                    material.SetInt(PropBlendOp, (int)UnityEngine.Rendering.BlendOp.Add);
                    material.SetInt(PropSrcBlend, (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt(PropDstBlend, (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt(PropZWrite, 0);
                    SetKeyword(material, KeyAlphaTestOn, false);
                    SetKeyword(material, KeyAlphaBlendOn, true);
                    SetKeyword(material, KeyAlphaPremultiplyOn, false);
                    SetKeyword(material, KeyAlphaModulateOn, false);
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                    break;
                case BlendMode.Subtractive:
                    material.SetOverrideTag(TagRenderTypeKey, TagRenderTypeValueTransparent);
                    material.SetInt(PropBlendOp, (int)UnityEngine.Rendering.BlendOp.ReverseSubtract);
                    material.SetInt(PropSrcBlend, (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt(PropDstBlend, (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt(PropZWrite, 0);
                    SetKeyword(material, KeyAlphaTestOn, false);
                    SetKeyword(material, KeyAlphaBlendOn, true);
                    SetKeyword(material, KeyAlphaPremultiplyOn, false);
                    SetKeyword(material, KeyAlphaModulateOn, false);
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                    break;
                case BlendMode.Modulate:
                    material.SetOverrideTag(TagRenderTypeKey, TagRenderTypeValueTransparent);
                    material.SetInt(PropBlendOp, (int)UnityEngine.Rendering.BlendOp.Add);
                    material.SetInt(PropSrcBlend, (int)UnityEngine.Rendering.BlendMode.DstColor);
                    material.SetInt(PropDstBlend, (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt(PropZWrite, 0);
                    SetKeyword(material, KeyAlphaTestOn, false);
                    SetKeyword(material, KeyAlphaBlendOn, false);
                    SetKeyword(material, KeyAlphaPremultiplyOn, false);
                    SetKeyword(material, KeyAlphaModulateOn, true);
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="colorMode"></param>
        public static void SetColorMode(Material material, ColorMode colorMode)
        {
            switch (colorMode)
            {
                case ColorMode.Multiply:
                    SetKeyword(material, KeyColorOverlayOn, false);
                    SetKeyword(material, KeyColorAddSubDiffOn, false);
                    SetKeyword(material, KeyColorAddSubDiffOn, false);
                    break;
                case ColorMode.Overlay:
                    SetKeyword(material, KeyColorOverlayOn, true);
                    SetKeyword(material, KeyColorAddSubDiffOn, false);
                    SetKeyword(material, KeyColorAddSubDiffOn, false);
                    break;
                case ColorMode.Color:
                    SetKeyword(material, KeyColorOverlayOn, false);
                    SetKeyword(material, KeyColorAddSubDiffOn, true);
                    SetKeyword(material, KeyColorAddSubDiffOn, false);
                    break;
                case ColorMode.Additive:
                case ColorMode.Subtractive:
                case ColorMode.Difference:
                    SetKeyword(material, KeyColorOverlayOn, false);
                    SetKeyword(material, KeyColorAddSubDiffOn, false);
                    SetKeyword(material, KeyColorAddSubDiffOn, true);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="propertyName"></param>
        /// <param name="val"></param>
        private static void SetBool(Material material, string propertyName, bool val)
        {
            if (material.HasProperty(propertyName))
            {
                material.SetInt(propertyName, (val == true) ? 1 : 0);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="propertyName"></param>
        /// <param name="val"></param>
        private static void SetInt(Material material, string propertyName, int val)
        {
            if (material.HasProperty(propertyName))
            {
                material.SetInt(propertyName, val);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="propertyName"></param>
        /// <param name="val"></param>
        private static void SetFloat(Material material, string propertyName, float val)
        {
            if (material.HasProperty(propertyName))
            {
                material.SetFloat(propertyName, val);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="propertyName"></param>
        /// <param name="val"></param>
        private static void SetVector(Material material, string propertyName, Vector4 val)
        {
            if (material.HasProperty(propertyName))
            {
                material.SetVector(propertyName, val);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="propertyName"></param>
        /// <param name="color"></param>
        private static void SetColor(Material material, string propertyName, Color color)
        {
            if (material.HasProperty(propertyName))
            {
                material.SetColor(propertyName, color);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="propertyName"></param>
        /// <param name="texture"></param>
        private static void SetTexture(Material material, string propertyName, Texture2D texture)
        {
            if (material.HasProperty(propertyName))
            {
                material.SetTexture(propertyName, texture);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="keyword"></param>
        /// <param name="required"></param>
        private static void SetKeyword(Material material, string keyword, bool required)
        {
            if (required)
            {
                material.EnableKeyword(keyword);
            }
            else
            {
                material.DisableKeyword(keyword);
            }
        }
    }
}