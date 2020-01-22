// ----------------------------------------------------------------------
// @Namespace : UniGLTFforUniVgo
// @Class     : VGO_PS_MinMaxCurve
// ----------------------------------------------------------------------
namespace UniGLTFforUniVgo
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;
    using UnityEngine;

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
        public VGO_AnimationCurve curve;

        /// <summary>Set a curve for the lower bound.</summary>
        [JsonProperty("curveMin")]
        public VGO_AnimationCurve curveMin;

        /// <summary>Set a curve for the upper bound.</summary>
        [JsonProperty("curveMax")]
        public VGO_AnimationCurve curveMax;
    }
}
