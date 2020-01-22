// ----------------------------------------------------------------------
// @Namespace : UniStandardParticle
// @Class     : Utils
// ----------------------------------------------------------------------
namespace UniStandardParticle
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class Utils
    {
        /// <summary></summary>
        public const string ShaderNameStandardSurface = "Particles/Standard Surface";

        /// <summary></summary>
        public const string ShaderNameStandardUnlit = "Particles/Standard Unlit";

        #region Properties

        /// <summary></summary>
        public const string PropBlendMode = "_Mode";

        /// <summary></summary>
        public const string PropCullMode = "_Cull";

        /// <summary></summary>
        public const string PropFlipbookMode = "_FlipbookMode";

        /// <summary></summary>
        public const string PropSoftParticlesEnabled = "_SoftParticlesEnabled";

        /// <summary></summary>
        public const string PropSoftParticleFadeParams = "_SoftParticleFadeParams";

        /// <summary></summary>
        public const string PropSoftParticleNearFadeDistance = "_SoftParticlesNearFadeDistance";

        /// <summary></summary>
        public const string PropSoftParticleFarFadeDistance = "_SoftParticlesFarFadeDistance";

        /// <summary></summary>
        public const string PropCameraFadingEnabled = "_CameraFadingEnabled";

        /// <summary></summary>
        public const string PropCameraFadeParams = "_CameraFadeParams";

        /// <summary></summary>
        public const string PropCameraNearFadeDistance = "_CameraNearFadeDistance";

        /// <summary></summary>
        public const string PropCameraFarFadeDistance = "_CameraFarFadeDistance";

        /// <summary></summary>
        public const string PropDistortionEnabled = "_DistortionEnabled";

        /// <summary></summary>
        public const string PropGrabTexture = "_GrabTexture";

        /// <summary></summary>
        public const string PropDistortionStrengthScaled = "_DistortionStrengthScaled";

        /// <summary></summary>
        public const string PropDistortionBlend = "_DistortionBlend";

        /// <summary></summary>
        public const string PropColorAddSubDiff = "_ColorAddSubDiff";

        /// <summary>Albedo</summary>
        public const string PropMainTex = "_MainTex";

        /// <summary></summary>
        public const string PropMainTexSt = "_MainTex_ST";

        /// <summary></summary>
        public const string PropColor = "_Color";

        /// <summary></summary>
        public const string PropCutoff = "_Cutoff";

        /// <summary></summary>
        public const string PropMetallicGlossMap = "_MetallicGlossMap";

        /// <summary></summary>
        public const string PropMetallic = "_Metallic";

        /// <summary></summary>
        public const string PropGlossiness = "_Glossiness";

        /// <summary></summary>
        public const string PropBumpMap = "_BumpMap";

        /// <summary></summary>
        public const string PropBumpScale = "_BumpScale";

        /// <summary></summary>
        public const string PropLightingEnabled = "_LightingEnabled";

        /// <summary></summary>
        public const string PropEmissionEnabled = "_EmissionEnabled";

        /// <summary></summary>
        public const string PropEmissionColor = "_EmissionColor";

        /// <summary></summary>
        public const string PropEmissionMap = "_EmissionMap";

        /// <summary></summary>
        public const string PropBlendOp = "_BlendOp";

        /// <summary></summary>
        public const string PropSrcBlend = "_SrcBlend";

        /// <summary></summary>
        public const string PropDstBlend = "_DstBlend";

        /// <summary></summary>
        public const string PropZWrite = "_ZWrite";

        #endregion

        #region Keywords

        /// <summary></summary>
        public const string KeyFlipBookBlending = "_FLIPBOOK_BLENDING";

        /// <summary></summary>
        public const string KeyTwoSided = "_TWO_SIDED";

        /// <summary></summary>
        public const string KeyFadingOn = "_FADING_ON";

        /// <summary></summary>
        public const string KeyDistortionOn = "_DISTORTION_ON";

        /// <summary></summary>
        public const string KeyRequireUv2 = "_REQUIRE_UV2";

        /// <summary></summary>
        public const string KeyEffectBump = "EFFECT_BUMP";

        /// <summary></summary>
        public const string KeyNormalMap = "_NORMALMAP";

        /// <summary></summary>
        public const string KeyMetallicGlossMap = "_METALLICGLOSSMAP";

        /// <summary></summary>
        public const string KeyEmission = "_EMISSION";

        /// <summary></summary>
        public const string KeyAlphaTestOn = "_ALPHATEST_ON";

        /// <summary></summary>
        public const string KeyAlphaBlendOn = "_ALPHABLEND_ON";

        /// <summary></summary>
        public const string KeyAlphaPremultiplyOn = "_ALPHAPREMULTIPLY_ON";

        /// <summary></summary>
        public const string KeyAlphaOverlayOn = "_ALPHAOVERLAY_ON";

        /// <summary></summary>
        public const string KeyAlphaModulateOn = "_ALPHAMODULATE_ON";

        /// <summary></summary>
        public const string KeyColorOverlayOn = "_COLOROVERLAY_ON";

        /// <summary></summary>
        public const string KeyColorColorOn = "_COLORCOLOR_ON";

        /// <summary></summary>
        public const string KeyColorAddSubDiffOn = "_COLORADDSUBDIFF_ON";

        #endregion

        #region Tags

        /// <summary></summary>
        public const string TagRenderTypeKey = "RenderType";

        /// <summary></summary>
        public const string TagRenderTypeValueOpaque = "Opaque";

        /// <summary></summary>
        public const string TagRenderTypeValueTransparentCutout = "TransparentCutout";

        /// <summary></summary>
        public const string TagRenderTypeValueCutout = "Cutout";

        /// <summary></summary>
        public const string TagRenderTypeValueTransparent = "Transparent";

        /// <summary></summary>
        public const string TagColorModeKey = "ColorMode";

        /// <summary></summary>
        public const string TagColorModeValueMultiply = "Multiply";

        /// <summary></summary>
        public const string TagColorModeValueAdditive = "Additive";

        /// <summary></summary>
        public const string TagColorModeValueSubtractive = "Subtractive";

        /// <summary></summary>
        public const string TagColorModeValueOverlay = "Overlay";

        /// <summary></summary>
        public const string TagColorModeValueColor = "Color";

        /// <summary></summary>
        public const string TagColorModeValueDifference = "Difference";

        /// <summary></summary>
        public const string TagFlipBookModeKey = "FlipBookMode";

        /// <summary></summary>
        public const string TagFlipBookModeValueSimple = "Simple";

        /// <summary></summary>
        public const string TagFlipBookModeValueBlended = "Blended";

        #endregion

        /// <summary></summary>
        public const int DisabledIntValue = 0;

        /// <summary></summary>
        public const int EnabledIntValue = 1;
    }
}