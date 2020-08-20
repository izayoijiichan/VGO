// ----------------------------------------------------------------------
// @Namespace : NewtonVgo.Schema.ParticleSystems
// @Class     : VGO_PS_MinMaxCurve
// ----------------------------------------------------------------------
namespace NewtonVgo.Schema.ParticleSystems
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;

    /// <summary>
    /// VGO Particle System MinMaxCurve
    /// </summary>
    [Serializable]
    [JsonObject("vgo.ps.minMaxCurve")]
    public class VGO_PS_MinMaxCurve
    {
        /// <summary>Set the mode that the min-max curve uses to evaluate values.</summary>
        [JsonProperty("mode")]
        public ParticleSystemCurveMode mode;

        /// <summary>Set the constant value.</summary>
        [JsonProperty("constant", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float constant = -1.0f;

        /// <summary>Set a constant for the lower bound.</summary>
        [JsonProperty("constantMin", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float constantMin = -1.0f;

        /// <summary>Set a constant for the upper bound.</summary>
        [JsonProperty("constantMax", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float constantMax = -1.0f;

        /// <summary>Set a multiplier to apply to the curves.</summary>
        [JsonProperty("curveMultiplier", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float curveMultiplier = -1.0f;

        /// <summary>Set the curve.</summary>
        [JsonProperty("curve")]
        public VgoAnimationCurve curve;

        /// <summary>Set a curve for the lower bound.</summary>
        [JsonProperty("curveMin")]
        public VgoAnimationCurve curveMin;

        /// <summary>Set a curve for the upper bound.</summary>
        [JsonProperty("curveMax")]
        public VgoAnimationCurve curveMax;
    }
}
