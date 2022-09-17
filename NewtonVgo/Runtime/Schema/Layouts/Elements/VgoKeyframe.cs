// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoKeyframe
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;

    /// <summary>
    /// VGO Keyframe
    /// </summary>
    [Serializable]
    [JsonObject("keyframe")]
    public class VgoKeyframe
    {
        /// <summary>The time of the keyframe.</summary>
        [JsonProperty("time")]
        public float time;

        /// <summary>The value of the curve at keyframe.</summary>
        [JsonProperty("value")]
        public float value;

        /// <summary>The incoming tangent for this key.</summary>
        /// <remarks>The incoming tangent affects the slope of the curve from the previous key to this key.</remarks>
        [JsonProperty("inTangent")]
        public float inTangent;

        /// <summary>The outgoing tangent for this key.</summary>
        /// <remarks>The outgoing tangent affects the slope of the curve from this key to the next key.</remarks>
        [JsonProperty("outTangent")]
        public float outTangent;

        /// <summary>The incoming weight for this key.</summary>
        /// <remarks>The incoming weight affects the slope of the curve from the previous key to this key.</remarks>
        [JsonProperty("inWeight")]
        public float inWeight;

        /// <summary>The outgoing weight for this key.</summary>
        /// <remarks>The outgoing weight affects the slope of the curve from this key to the next key.</remarks>
        [JsonProperty("outWeight")]
        public float outWeight;

        /// <summary>Weighted mode for the keyframe.</summary>
        [JsonProperty("weightedMode", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(WeightedMode.None)]
        public WeightedMode weightedMode = WeightedMode.None;
    }
}
