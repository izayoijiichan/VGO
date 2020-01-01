using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UniGLTFforUniVgo
{
    /// <summary>
    /// A keyframe animation.
    /// </summary>
    [Serializable]
    [JsonObject("animation")]
    public partial class glTFAnimation
    {
        /// <summary></summary>
        [JsonProperty("name")]
        public string name = string.Empty;

        /// <summary></summary>
        [JsonProperty("channels", Required = Required.Always)]
        //[JsonSchema(Required = true, MinItems = 1)]
        public List<glTFAnimationChannel> channels = new List<glTFAnimationChannel>();

        /// <summary></summary>
        [JsonProperty("samplers", Required = Required.Always)]
        //[JsonSchema(Required = true, MinItems = 1)]
        public List<glTFAnimationSampler> samplers = new List<glTFAnimationSampler>();

        /// <summary></summary>
        [JsonProperty("extensions")]
        public object extensions;

        /// <summary></summary>
        [JsonProperty("extras")]
        public object extras;
    }
}
