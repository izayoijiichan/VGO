// ----------------------------------------------------------------------
// @Namespace : UniGLTFforUniVgo
// @Class     : VGO_PS_MinMaxGradient
// ----------------------------------------------------------------------
namespace UniGLTFforUniVgo
{
    using Newtonsoft.Json;
    using System;
    using UnityEngine;

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
        //public Color color;
        public float[] color;

        /// <summary>Set a constant color for the lower bound.</summary>
        [JsonProperty("colorMin")]
        //public Color colorMin;
        public float[] colorMin;

        /// <summary> Set a constant color for the upper bound.</summary>
        [JsonProperty("colorMax")]
        //public Color colorMax;
        public float[] colorMax;

        /// <summary>Set the gradient.</summary>
        [JsonProperty("gradient")]
        public VGO_Gradient gradient;

        /// <summary>Set a gradient for the lower bound.</summary>
        [JsonProperty("gradientMin")]
        public VGO_Gradient gradientMin;

        /// <summary>Set a gradient for the upper bound.</summary>
        [JsonProperty("gradientMax")]
        public VGO_Gradient gradientMax;
    }
}
