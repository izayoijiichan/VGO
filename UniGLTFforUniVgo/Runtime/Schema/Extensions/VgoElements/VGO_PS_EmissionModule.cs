// ----------------------------------------------------------------------
// @Namespace : UniGLTFforUniVgo
// @Class     : VGO_PS_EmissionModule
// ----------------------------------------------------------------------
namespace UniGLTFforUniVgo
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// VGO Particle System EmissionModule
    /// </summary>
    [Serializable]
    [JsonObject("vgo.ps.emissionModule")]
    public class VGO_PS_EmissionModule
    {
        /// <summary>Specifies whether the EmissionModule is enabled or disabled.</summary>
        [JsonProperty("enabled")]
        public bool enabled;

        /// <summary>The rate at which the emitter spawns new particles over time.</summary>
        [JsonProperty("rateOverTime")]
        public VGO_PS_MinMaxCurve rateOverTime;

        /// <summary>Change the rate over time multiplier.</summary>
        [JsonProperty("rateOverTimeMultiplier")]
        public float rateOverTimeMultiplier;

        /// <summary>The rate at which the emitter spawns new particles over distance.</summary>
        [JsonProperty("rateOverDistance")]
        public VGO_PS_MinMaxCurve rateOverDistance;

        /// <summary>Change the rate over distance multiplier.</summary>
        [JsonProperty("rateOverDistanceMultiplier")]
        public float rateOverDistanceMultiplier;

        ///// <summary>The current number of bursts.</summary>
        //[JsonProperty("burstCount")]
        //public int burstCount;

        /// <summary></summary>
        [JsonProperty("bursts")]
        public VGO_PS_Burst[] bursts;
    }
}
