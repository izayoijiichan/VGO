using Newtonsoft.Json;
using System;

namespace UniGLTFforUniVgo
{
    /// <summary>
    /// Scene
    /// </summary>
    [Serializable]
    [JsonObject("scene")]
    public class glTFScene
    {
        /// <summary></summary>
        [JsonProperty("nodes")]
        //[JsonSchema(MinItems = 1)]
        //[ItemJsonSchema(Minimum = 0)]
        public int[] nodes;

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
