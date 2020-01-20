using Newtonsoft.Json;
using System;

namespace UniGLTFforUniVgo
{
    /// <summary>
    /// A buffer points to binary geometry, animation, or skins.
    /// </summary>
    [Serializable]
    [JsonObject("buffer")]
    public partial class glTFBuffer
    {
        /// <summary>The uri of the buffer.</summary>
        [JsonProperty("uri")]
        public string uri;

        /// <summary>The length of the buffer in bytes.</summary>
        [JsonProperty("byteLength", Required = Required.Always)]
        //[JsonSchema(Required = true, Minimum = 1)]
        public int byteLength;

        /// <summary></summary>
        [JsonProperty("name")]
        public string name;

        /// <summary></summary>
        [JsonProperty("extensions")]
        public object extensions;

        /// <summary></summary>
        [JsonProperty("extras")]
        public object extras;

        /// <summary></summary>
        [JsonIgnore]
        IBytesBuffer Storage;
    }
}
