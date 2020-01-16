using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace UniGLTFforUniVgo
{
    /// <summary></summary>
    public enum glTFTextureTypes
    {
        /// <summary></summary>
        BaseColor,
        /// <summary></summary>
        Metallic,
        /// <summary></summary>
        Normal,
        /// <summary></summary>
        Occlusion,
        /// <summary></summary>
        Emissive,
        /// <summary></summary>
        Unknown,
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IglTFTextureinfo
    {
        glTFTextureTypes TextureType { get; }
    }

    /// <summary>
    /// Material Normal Texture Info
    /// </summary>
    [Serializable]
    [JsonObject("textureInfo")]
    public abstract class glTFTextureInfo : IglTFTextureinfo
    {
        /// <summary></summary>
        [JsonProperty("index", Required = Required.Always)]
        //[JsonSchema(Required = true, Minimum = 0)]
        public int index = -1;

        /// <summary></summary>
        [JsonProperty("texCoord", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        //[JsonSchema(Minimum = 0)]
        public int texCoord = -1;

        /// <summary></summary>
        [JsonProperty("extensions")]
        public object extensions;

        /// <summary></summary>
        [JsonProperty("extras")]
        public object extras;

        /// <summary></summary>
        [JsonIgnore]
        public abstract glTFTextureTypes TextureType { get; }
    }
}
