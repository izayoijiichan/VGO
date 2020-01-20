using Newtonsoft.Json;
using System;

namespace UniGLTFforUniVgo
{
    /// <summary>
    /// A perspective camera containing properties to create a perspective projection matrix.
    /// </summary>
    [Serializable]
    [JsonObject("camera.perspective")]
    public class glTFCameraPerspective
    {
        /// <summary></summary>
        [JsonProperty("aspectRatio")]
        //[JsonSchema(Minimum = 0.0f, ExclusiveMinimum = true)]
        public float aspectRatio = 0.0f;

        /// <summary></summary>
        [JsonProperty("yfov", Required = Required.Always)]
        //[JsonSchema(Required = true, Minimum = 0.0f, ExclusiveMinimum = true)]
        public float yfov = 0.0f;

        /// <summary></summary>
        [JsonProperty("zfar")]
        //[JsonSchema(Minimum = 0.0f, ExclusiveMinimum = true)]
        public float zfar = 0.0f;

        /// <summary></summary>
        [JsonProperty("znear", Required = Required.Always)]
        //[JsonSchema(Required = true, Minimum = 0.0f, ExclusiveMinimum = true)]
        public float znear = 0.0f;

        /// <summary></summary>
        [JsonProperty("extensions")]
        public object extensions;

        /// <summary></summary>
        [JsonProperty("extras")]
        public object extras;
    }
}
