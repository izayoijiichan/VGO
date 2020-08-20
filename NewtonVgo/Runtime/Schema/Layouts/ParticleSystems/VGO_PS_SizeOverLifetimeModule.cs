// ----------------------------------------------------------------------
// @Namespace : NewtonVgo.Schema.ParticleSystems
// @Class     : VGO_PS_SizeOverLifetimeModule
// ----------------------------------------------------------------------
namespace NewtonVgo.Schema.ParticleSystems
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;

    /// <summary>
    /// VGO Particle System SizeOverLifetimeModule
    /// </summary>
    [Serializable]
    [JsonObject("vgo.ps.sizeOverLifetimeModule")]
    public class VGO_PS_SizeOverLifetimeModule
    {
        /// <summary>Specifies whether the SizeOverLifetimeModule is enabled or disabled.</summary>
        [JsonProperty("enabled")]
        public bool enabled;

        /// <summary>Set the size over lifetime on each axis separately.</summary>
        [JsonProperty("separateAxes")]
        public bool separateAxes;

        ///// <summary>Curve to control particle size based on lifetime.</summary>
        //[JsonProperty("size")]
        //[NativeName("X")]
        //public VGO_PS_MinMaxCurve size;

        ///// <summary>A multiplier for ParticleSystem.SizeOverLifetimeModule._size.</summary>
        //[JsonProperty("sizeMultiplier")]
        //[NativeName("XMultiplier")]
        //public float sizeMultiplier;

        /// <summary>Size over lifetime curve for the x-axis.</summary>
        [JsonProperty("x")]
        public VGO_PS_MinMaxCurve x;

        /// <summary>Size over lifetime curve for the y-axis.</summary>
        [JsonProperty("y")]
        public VGO_PS_MinMaxCurve y;

        /// <summary>Size over lifetime curve for the z-axis.</summary>
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
    }
}
