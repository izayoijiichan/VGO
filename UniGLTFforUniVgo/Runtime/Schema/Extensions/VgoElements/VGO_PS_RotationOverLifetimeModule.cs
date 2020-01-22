// ----------------------------------------------------------------------
// @Namespace : UniGLTFforUniVgo
// @Class     : VGO_PS_RotationOverLifetimeModule
// ----------------------------------------------------------------------
namespace UniGLTFforUniVgo
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;

    /// <summary>
    /// VGO Particle System RotationOverLifetimeModule
    /// </summary>
    [Serializable]
    [JsonObject("vgo.ps.rotationOverLifetimeModule")]
    public class VGO_PS_RotationOverLifetimeModule
    {
        /// <summary>Specifies whether the RotationOverLifetimeModule is enabled or disabled.</summary>
        [JsonProperty("enabled")]
        public bool enabled;

        /// <summary>Set the rotation over lifetime on each axis separately.</summary>
        [JsonProperty("separateAxes")]
        public bool separateAxes;

        /// <summary>Rotation over lifetime curve for the x-axis.</summary>
        [JsonProperty("x")]
        public VGO_PS_MinMaxCurve x;

        /// <summary>Rotation over lifetime curve for the y-axis.</summary>
        [JsonProperty("y")]
        public VGO_PS_MinMaxCurve y;

        /// <summary>Rotation over lifetime curve for the z-axis.</summary>
        [JsonProperty("z")]
        public VGO_PS_MinMaxCurve z;

        /// <summary>Rotation multiplier along the x-axis.</summary>
        [JsonProperty("xMultiplier", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float xMultiplier = -1.0f;

        /// <summary>Rotation multiplier along the y-axis.</summary>
        [JsonProperty("yMultiplier", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float yMultiplier = -1.0f;

        /// <summary>Rotation multiplier along the z-axis.</summary>
        [JsonProperty("zMultiplier", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float zMultiplier = -1.0f;
    }
}
