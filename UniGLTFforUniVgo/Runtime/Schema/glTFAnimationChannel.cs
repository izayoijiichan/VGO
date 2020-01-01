using Newtonsoft.Json;
using System;

namespace UniGLTFforUniVgo
{
    /// <summary>
    /// Targets an animation's sampler at a node's property.
    /// </summary>
    [Serializable]
    [JsonObject("animation.channel")]
    public class glTFAnimationChannel
    {
        /// <summary></summary>
        [JsonProperty("sampler", Required = Required.Always)]
        //[JsonSchema(Required = true, Minimum = 0)]
        public int sampler = -1;

        /// <summary></summary>
        [JsonProperty("target", Required = Required.Always)]
        //[JsonSchema(Required = true)]
        public glTFAnimationTarget target;

        /// <summary></summary>
        [JsonProperty("extensions")]
        public object extensions;

        /// <summary></summary>
        [JsonProperty("extras")]
        public object extras;
    }
}
