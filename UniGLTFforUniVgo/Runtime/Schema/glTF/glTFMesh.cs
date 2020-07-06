using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace UniGLTFforUniVgo
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [JsonObject("mesh")]
    public class glTFMesh
    {
        /// <summary></summary>
        [JsonProperty("name")]
        public string name;

        /// <summary></summary>
        [JsonProperty("primitives", Required = Required.Always)]
        //[JsonSchema(Required = true, MinItems = 1)]
        public List<glTFPrimitives> primitives = new List<glTFPrimitives>();

        /// <summary></summary>
        [JsonProperty("weights")]
        //[JsonSchema(MinItems = 1)]
        public float[] weights;

        /// <summary></summary>
        [JsonProperty("extensions")]
        public object extensions;

        /// <summary></summary>
        [JsonProperty("extras")]
        public glTFMesh_extras extras;

        public glTFMesh()
        {
        }

        public glTFMesh(string name)
        {
            this.name = name;
        }
    }
}
