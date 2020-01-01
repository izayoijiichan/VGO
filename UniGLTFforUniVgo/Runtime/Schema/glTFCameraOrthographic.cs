using Newtonsoft.Json;
using System;

namespace UniGLTFforUniVgo
{
    /// <summary>
    /// An orthographic camera containing properties to create an orthographic projection matrix.
    /// </summary>
    [Serializable]
    [JsonObject("camera.orthographic")]
    public class glTFCameraOrthographic
    {
        /// <summary></summary>
        [JsonProperty("xmag", Required = Required.Always)]
        //[JsonSchema(Required = true)]
        public float xmag = 0.0f;

        /// <summary></summary>
        [JsonProperty("ymag", Required = Required.Always)]
        //[JsonSchema(Required = true)]
        public float ymag = 0.0f;

        /// <summary></summary>
        [JsonProperty("zfar", Required = Required.Always)]
        //[JsonSchema(Required = true, Minimum = 0.0f, ExclusiveMinimum = true)]
        public float zfar = 0.0f;

        /// <summary></summary>
        [JsonProperty("znear", Required = Required.Always)]
        //[JsonSchema(Required = true, Minimum = 0.0f)]
        public float znear = 0.0f;

        /// <summary></summary>
        [JsonProperty("extensions")]
        public object extensions;

        /// <summary></summary>
        [JsonProperty("extras")]
        public object extras;
    }
}
