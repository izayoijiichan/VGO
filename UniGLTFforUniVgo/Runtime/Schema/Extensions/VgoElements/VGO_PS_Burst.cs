// ----------------------------------------------------------------------
// @Namespace : UniGLTFforUniVgo
// @Class     : VGO_PS_Burst
// ----------------------------------------------------------------------
namespace UniGLTFforUniVgo
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;

    /// <summary>
    /// VGO Particle System Burst
    /// </summary>
    [Serializable]
    [JsonObject("vgo.ps.burst")]
    public class VGO_PS_Burst
    {
        /// <summary>The time that each burst occurs.</summary>
        [JsonProperty("time")]
        public float time;

        /// <summary>Specify the number of particles to emit.</summary>
        [JsonProperty("count")]
        public VGO_PS_MinMaxCurve count;

        ///// <summary>The minimum number of particles to emit.</summary>
        //[JsonProperty("minCount", DefaultValueHandling = DefaultValueHandling.Ignore)]
        //[DefaultValue(-1)]
        //public short minCount; = -1;

        ///// <summary>The maximum number of particles to emit.</summary>
        //[JsonProperty("maxCount", DefaultValueHandling = DefaultValueHandling.Ignore)]
        //[DefaultValue(-1)]
        //public short maxCount; = -1;

        /// <summary>Specifies how many times the system should play the burst. Set this to 0 to make it play indefinitely.</summary>
        [JsonProperty("cycleCount")]
        public int cycleCount;

        /// <summary>How often to repeat the burst, in seconds.</summary>
        [JsonProperty("repeatInterval")]
        public float repeatInterval;

        /// <summary>The probability that the system triggers a burst.</summary>
        [JsonProperty("probability")]
        //[Range(0.0f,1.0f)]
        public float probability;
    }
}
