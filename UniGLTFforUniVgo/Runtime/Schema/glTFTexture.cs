using Newtonsoft.Json;
using System;

namespace UniGLTFforUniVgo
{
    /// <summary>
    /// A texture and its sampler.
    /// </summary>
    [Serializable]
    [JsonObject("texture")]
    public class glTFTexture
    {
        /// <summary></summary>
        [JsonProperty("sampler")]
        //[JsonSchema(Minimum = 0)]
        public int sampler;

        /// <summary></summary>
        [JsonProperty("source")]
        //[JsonSchema(Minimum = 0)]
        public int source;

        /// <summary></summary>
        [JsonProperty("name")]
        public string name;

        /// <summary></summary>
        [JsonProperty("extensions")]
        public object extensions;

        /// <summary></summary>
        [JsonProperty("extras")]
        public object extras;
    }
}
