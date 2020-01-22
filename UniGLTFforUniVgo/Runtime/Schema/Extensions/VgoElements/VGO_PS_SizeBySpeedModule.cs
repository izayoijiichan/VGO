// ----------------------------------------------------------------------
// @Namespace : UniGLTFforUniVgo
// @Class     : VGO_PS_SizeBySpeedModule
// ----------------------------------------------------------------------
namespace UniGLTFforUniVgo
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;

    /// <summary>
    /// VGO Particle System SizeBySpeedModule
    /// </summary>
    [Serializable]
    [JsonObject("vgo.ps.sizeBySpeedModule")]
    public class VGO_PS_SizeBySpeedModule
    {
        /// <summary>Specifies whether the SizeBySpeedModule is enabled or disabled.</summary>
        [JsonProperty("enabled")]
        public bool enabled;

        /// <summary>Set the size by speed on each axis separately.</summary>
        [JsonProperty("separateAxes")]
        public bool separateAxes;

        ///// <summary>Curve to control particle size based on speed.</summary>
        //[JsonProperty("size")]
        //[NativeName("X")]
        //public VGO_PS_MinMaxCurve size;

        ///// <summary>A multiplier for ParticleSystem.SizeBySpeedModule._size.</summary>
        //[JsonProperty("sizeMultiplier")]
        //[NativeName("XMultiplier")]
        //public float sizeMultiplier;

        /// <summary>Size by speed curve for the x-axis.</summary>
        [JsonProperty("x")]
        public VGO_PS_MinMaxCurve x;

        /// <summary>Size by speed curve for the y-axis.</summary>
        [JsonProperty("y")]
        public VGO_PS_MinMaxCurve y;

        /// <summary>Size by speed curve for the z-axis.</summary>
        [JsonProperty("z")]
        public VGO_PS_MinMaxCurve z;

        /// <summary>Size multiplier along the x-axis.</summary>
        [JsonProperty("xMultiplier", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float xMultiplier = -1.0f;

        /// <summary>Size multiplier along the y-axis.</summary>
        [JsonProperty("yMultiplier", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float yMultiplier = -1.0f;

        /// <summary>Size multiplier along the z-axis.</summary>
        [JsonProperty("zMultiplier", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float zMultiplier = -1.0f;

        /// <summary>Set the minimum and maximum speed that this modules applies the size curve between.</summary>
        [JsonProperty("range")]
        //public Vector2 Range;
        public float[] range;
    }
}
