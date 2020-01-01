using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace UniGLTFforUniVgo
{
    /// <summary>
    /// Joints and matrices defining a skin.
    /// </summary>
    [Serializable]
    [JsonObject("skin")]
    public class glTFSkin
    {
        /// <summary></summary>
        [JsonProperty("inverseBindMatrices", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        //[JsonSchema(Minimum = 0, ExplicitIgnorableValue = -1)]
        public int inverseBindMatrices = -1;

        /// <summary></summary>
        [JsonProperty("skeleton", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        //[JsonSchema(Minimum = 0, ExplicitIgnorableValue = -1)]
        public int skeleton = -1;

        /// <summary></summary>
        [JsonProperty("joints")]
        //[JsonSchema(Required = true, MinItems = 1)]
        //[ItemJsonSchema(Minimum = 0)]
        public int[] joints;

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
