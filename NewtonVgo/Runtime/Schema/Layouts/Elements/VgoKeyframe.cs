// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoKeyframe
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;

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

        /// <summary>Sets the incoming tangent for this key. The incoming tangent affects the slope of the curve from the previous key to this key.</summary>
        [JsonProperty("inTangent")]
        public float inTangent;

        /// <summary>Sets the outgoing tangent for this key. The outgoing tangent affects the slope of the curve from this key to the next key.</summary>
        [JsonProperty("outTangent")]
        public float outTangent;

        /// <summary>Sets the incoming weight for this key. The incoming weight affects the slope of the curve from the previous key to this key.</summary>
        [JsonProperty("inWeight")]
        public float inWeight;

        /// <summary>Sets the outgoing weight for this key. The outgoing weight affects the slope of the curve from this key to the next key.</summary>
        [JsonProperty("outWeight")]
        public float outWeight;

        /// <summary>Weighted mode for the keyframe.</summary>
        [JsonProperty("weightedMode")]
        public WeightedMode weightedMode;
    }
}
