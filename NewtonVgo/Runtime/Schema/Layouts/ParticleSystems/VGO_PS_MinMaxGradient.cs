// ----------------------------------------------------------------------
// @Namespace : NewtonVgo.Schema.ParticleSystems
// @Class     : VGO_PS_MinMaxGradient
// ----------------------------------------------------------------------
namespace NewtonVgo.Schema.ParticleSystems
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// VGO Particle System MinMaxGradient
    /// </summary>
    [Serializable]
    [JsonObject("vgo.ps.minMaxGradient")]
    public class VGO_PS_MinMaxGradient
    {
        /// <summary>Set the mode that the Min-Max Gradient uses to evaluate colors.</summary>
        [JsonProperty("mode")]
        public ParticleSystemGradientMode mode;

        /// <summary>Set a constant color.</summary>
        [JsonProperty("color")]
        public Color4 color;

        /// <summary>Set a constant color for the lower bound.</summary>
        [JsonProperty("colorMin")]
        public Color4 colorMin;

        /// <summary> Set a constant color for the upper bound.</summary>
        [JsonProperty("colorMax")]
        public Color4 colorMax;

        /// <summary>Set the gradient.</summary>
        [JsonProperty("gradient")]
        public VgoGradient gradient;

        /// <summary>Set a gradient for the lower bound.</summary>
        [JsonProperty("gradientMin")]
        public VgoGradient gradientMin;

        /// <summary>Set a gradient for the upper bound.</summary>
        [JsonProperty("gradientMax")]
        public VgoGradient gradientMax;
    }
}
