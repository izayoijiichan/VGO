using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace UniGLTFforUniVgo
{
    /// <summary>
    /// Material PBR Metallic Roughness
    /// </summary>
    [Serializable]
    [JsonObject("material.pbrMetallicRoughness")]
    public class glTFPbrMetallicRoughness
    {
        /// <summary></summary>
        [JsonProperty("baseColorFactor")]
        //[JsonSchema(MinItems = 4, MaxItems = 4)]
        //[ItemJsonSchema(Minimum = 0.0, Maximum = 1.0)]
        public float[] baseColorFactor;

        /// <summary></summary>
        [JsonProperty("baseColorTexture")]
        public glTFMaterialBaseColorTextureInfo baseColorTexture = null;

        /// <summary></summary>
        [JsonProperty("metallicFactor", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        //[JsonSchema(Minimum = 0.0, Maximum = 1.0)]
        public float metallicFactor = -1.0f;

        /// <summary></summary>
        [JsonProperty("roughnessFactor", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        //[JsonSchema(Minimum = 0.0, Maximum = 1.0)]
        public float roughnessFactor = -1.0f;

        /// <summary></summary>
        [JsonProperty("metallicRoughnessTexture")]
        public glTFMaterialMetallicRoughnessTextureInfo metallicRoughnessTexture = null;

        /// <summary></summary>
        [JsonProperty("extensions")]
        public object extensions;

        /// <summary></summary>
        [JsonProperty("extras")]
        public object extras;
    }
}
