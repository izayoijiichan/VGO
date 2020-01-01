using Newtonsoft.Json;
using System;

namespace UniGLTFforUniVgo
{
    /// <summary>
    /// Combines input and output accessors with an interpolation algorithm to define a keyframe graph (but not its target).
    /// </summary>
    [Serializable]
    [JsonObject("animation.sampler")]
    public class glTFAnimationSampler
    {
        /// <summary></summary>
        [JsonProperty("input", Required = Required.Always)]
        //[JsonSchema(Required = true, Minimum = 0)]
        public int input = -1;

        /// <summary></summary>
        [JsonProperty("interpolation")]
        //[JsonSchema(EnumValues = new object[] { "LINEAR", "STEP", "CUBICSPLINE" }, EnumSerializationType = EnumSerializationType.AsString)]
        public string interpolation;

        /// <summary></summary>
        [JsonProperty("output", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonSchema(Required = true, Minimum = 0)]
        public int output = -1;

        /// <summary></summary>
        [JsonProperty("extensions")]
        public object extensions;

        /// <summary></summary>
        [JsonProperty("extras")]
        public object extras;
    }
}
