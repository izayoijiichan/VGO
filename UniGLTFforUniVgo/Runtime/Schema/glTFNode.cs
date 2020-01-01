using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace UniGLTFforUniVgo
{
    /// <summary>
    /// A node in the node hierarchy.
    /// </summary>
    [Serializable]
    [JsonObject("node")]
    public class glTFNode
    {
        /// <summary></summary>
        [JsonProperty("name")]
        public string name = null;

        /// <summary></summary>
        [JsonProperty("camera", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        //[JsonSchema(Minimum = 0, ExplicitIgnorableValue = -1)]
        public int camera = -1;

        /// <summary></summary>
        [JsonProperty("children")]
        //[JsonSchema(MinItems = 1)]
        //[ItemJsonSchema(Minimum = 0)]
        public int[] children = null;

        /// <summary></summary>
        [JsonProperty("skin", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        //[JsonSchema(Dependencies = new string[] { "mesh" }, Minimum = 0, ExplicitIgnorableValue = -1)]
        public int skin = -1;

        /// <summary></summary>
        [JsonProperty("matrix")]
        //[JsonSchema(MinItems = 16, MaxItems = 16)]
        public float[] matrix = null;

        /// <summary></summary>
        [JsonProperty("mesh", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        //[JsonSchema(Minimum = 0, ExplicitIgnorableValue = -1)]
        public int mesh = -1;

        /// <summary></summary>
        [JsonProperty("translation")]
        //[JsonSchema(MinItems = 3, MaxItems = 3)]
        public float[] translation = null;

        /// <summary></summary>
        [JsonProperty("rotation")]
        //[JsonSchema(MinItems = 4, MaxItems = 4)]
        //[ItemJsonSchema(Minimum = -1.0, Maximum = 1.0)]
        public float[] rotation = null;

        /// <summary></summary>
        [JsonProperty("scale")]
        //[JsonSchema(MinItems = 3, MaxItems = 3)]
        public float[] scale = null;

        /// <summary></summary>
        [JsonProperty("weights")]
        //[JsonSchema(Dependencies = new string[] { "mesh" }, MinItems = 1)]
        public float[] weights = null;

        /// <summary></summary>
        [JsonProperty("extensions")]
        public glTFNode_extensions extensions = null;

        /// <summary></summary>
        [JsonProperty("extras")]
        //public glTFNode_extra extras;
        public object extras = null;
    }
}
