// ----------------------------------------------------------------------
// @Namespace : UniGLTFforUniVgo
// @Class     : glTF_VRMC_materials_mtoon
// ----------------------------------------------------------------------
namespace UniGLTFforUniVgo
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;

    #region Enums

    /// <summary></summary>
    public enum MToonCullMode
    {
        /// <summary></summary>
        Off = 0,
        /// <summary></summary>
        Front = 1,
        /// <summary></summary>
        Back = 2,
    }

    /// <summary></summary>
    public enum MToonOutlineColorMode
    {
        /// <summary></summary>
        FixedColor = 0,
        /// <summary></summary>
        MixedLighting = 1,
    }

    /// <summary></summary>
    public enum MToonOutlineWidthMode
    {
        /// <summary></summary>
        None = 0,
        /// <summary></summary>
        WorldCoordinates = 1,
        /// <summary></summary>
        ScreenCoordinates = 2,
    }

    /// <summary></summary>
    public enum MToonRenderMode
    {
        /// <summary></summary>
        Opaque = 0,
        /// <summary></summary>
        Cutout = 1,
        /// <summary></summary>
        Transparent = 2,
        /// <summary></summary>
        TransparentWithZWrite = 3,
    }

    #endregion

    /// <summary>
    /// VRM/MToon shader properties
    /// </summary>
    [Serializable]
    public class glTF_VRMC_materials_mtoon
    {
        /// <summary>_MToonVersion</summary>
        [JsonProperty("version")]
        public string version = null;

        /// <summary>_BlendMode</summary>
        [JsonProperty("renderMode")]
        public MToonRenderMode renderMode;

        /// <summary>_CullMode</summary>
        [JsonProperty("cullMode")]
        public MToonCullMode cullMode;

        /// <summary>renderQueue</summary>
        [JsonProperty("renderQueueOffsetNumber", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        public int renderQueueOffsetNumber = -1;

        /// <summary>_Color</summary>
        [JsonProperty("litFactor")]
        public float[] litFactor;

        /// <summary>_MainTex</summary>
        [JsonProperty("litMultiplyTexture", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        public int litMultiplyTexture = -1;

        /// <summary>_ShadeColor</summary>
        [JsonProperty("shadeFactor")]
        public float[] shadeFactor;

        /// <summary>_ShadeTexture</summary>
        [JsonProperty("shadeMultiplyTexture", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        public int shadeMultiplyTexture = -1;

        /// <summary>_Cutoff</summary>
        [JsonProperty("cutoutThresholdFactor", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float cutoutThresholdFactor = -1.0f;

        /// <summary>_ShadeShift</summary>
        [JsonProperty("shadingShiftFactor", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-2.0f)]
        public float shadingShiftFactor = -2.0f;

        /// <summary>_ShadeToony</summary>
        [JsonProperty("shadingToonyFactor", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float shadingToonyFactor = -1.0f;

        /// <summary>_ReceiveShadowRate</summary>
        [JsonProperty("shadowReceiveMultiplierFactor", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float shadowReceiveMultiplierFactor = -1.0f;

        /// <summary>_ReceiveShadowTexture</summary>
        [JsonProperty("shadowReceiveMultiplierMultiplyTexture", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        public int shadowReceiveMultiplierMultiplyTexture = -1;

        /// <summary>_ShadingGradeRate</summary>
        [JsonProperty("litAndShadeMixingMultiplierFactor", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float litAndShadeMixingMultiplierFactor = -1.0f;

        /// <summary>_ShadingGradeTexture</summary>
        [JsonProperty("litAndShadeMixingMultiplierMultiplyTexture", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        public int litAndShadeMixingMultiplierMultiplyTexture = -1;

        /// <summary>_LightColorAttenuation</summary>
        [JsonProperty("lightColorAttenuationFactor", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float lightColorAttenuationFactor = -1.0f;

        /// <summary>_IndirectLightIntensity</summary>
        [JsonProperty("giIntensityFactor", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float giIntensityFactor = -1.0f;

        /// <summary>_BumpMap</summary>
        [JsonProperty("normalTexture", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        public int normalTexture = -1;

        /// <summary>_BumpScale</summary>
        [JsonProperty("normalScaleFactor", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float normalScaleFactor = -1.0f;

        /// <summary>_EmissionColor</summary>
        [JsonProperty("emissionFactor")]
        public float[] emissionFactor;

        /// <summary>_EmissionMap</summary>
        [JsonProperty("emissionMultiplyTexture", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        public int emissionMultiplyTexture = -1;

        /// <summary>_SphereAdd</summary>
        [JsonProperty("additiveTexture", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        public int additiveTexture = -1;

        /// <summary>_RimColor</summary>
        [JsonProperty("rimFactor")]
        public float[] rimFactor;

        /// <summary>_RimTexture</summary>
        [JsonProperty("rimMultiplyTexture", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        public int rimMultiplyTexture = -1;

        /// <summary>_RimLightingMix</summary>
        [JsonProperty("rimLightingMixFactor", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float rimLightingMixFactor = -1.0f;

        /// <summary>_RimFresnelPower</summary>
        [JsonProperty("rimFresnelPowerFactor", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float rimFresnelPowerFactor = -1.0f;

        /// <summary>_RimLift</summary>
        [JsonProperty("rimLiftFactor", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float rimLiftFactor = -1.0f;

        /// <summary>_OutlineWidthMode</summary>
        [JsonProperty("outlineWidthMode")]
        public MToonOutlineWidthMode outlineWidthMode;

        /// <summary>_OutlineWidth</summary>
        [JsonProperty("outlineWidthFactor", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float outlineWidthFactor = -1.0f;

        /// <summary>_OutlineWidthTexture</summary>
        [JsonProperty("outlineWidthMultiplyTexture", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        public int outlineWidthMultiplyTexture = -1;

        /// <summary>_OutlineScaledMaxDistance</summary>
        [JsonProperty("outlineScaledMaxDistanceFactor", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float outlineScaledMaxDistanceFactor = -1.0f;

        /// <summary>_OutlineColorMode</summary>
        [JsonProperty("outlineColorMode")]
        public MToonOutlineColorMode outlineColorMode;

        /// <summary>_OutlineColor</summary>
        [JsonProperty("outlineFactor")]
        public float[] outlineFactor;

        /// <summary>_OutlineLightingMix</summary>
        [JsonProperty("outlineLightingMixFactor", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float outlineLightingMixFactor = -1.0f;

        /// <summary>_MainTex</summary>
        [JsonProperty("mainTextureLeftBottomOriginScale")]
        public float[] mainTextureLeftBottomOriginScale;

        /// <summary>_MainTex</summary>
        [JsonProperty("mainTextureLeftBottomOriginOffset")]
        public float[] mainTextureLeftBottomOriginOffset;

        /// <summary>_UvAnimMaskTexture</summary>
        [JsonProperty("uvAnimationMaskTexture", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        public int uvAnimationMaskTexture = -1;

        /// <summary>_UvAnimScrollX</summary>
        [JsonProperty("uvAnimationScrollXSpeedFactor", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float uvAnimationScrollXSpeedFactor = -1.0f;

        /// <summary>_UvAnimScrollY</summary>
        [JsonProperty("uvAnimationScrollYSpeedFactor", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float uvAnimationScrollYSpeedFactor = -1.0f;

        /// <summary>_UvAnimRotation</summary>
        [JsonProperty("uvAnimationRotationSpeedFactor", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float uvAnimationRotationSpeedFactor = -1.0f;

        [JsonIgnore]
        public static string ExtensionName => "VRMC_materials_mtoon";
    }
}
