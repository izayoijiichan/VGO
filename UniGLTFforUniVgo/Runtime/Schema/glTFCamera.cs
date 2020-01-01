using Newtonsoft.Json;
using System;

namespace UniGLTFforUniVgo
{
    public enum ProjectionType
    {
        Perspective,
        Orthographic
    }

    /// <summary>
    /// Camera
    /// </summary>
    [Serializable]
    [JsonObject("camera")]
    public class glTFCamera
    {
        /// <summary></summary>
        [JsonProperty("orthographic")]
        public glTFCameraOrthographic orthographic;

        /// <summary></summary>
        [JsonProperty("perspective")]
        public glTFCameraPerspective perspective;

        /// <summary></summary>
        [JsonProperty("type", Required = Required.Always)]
        //[JsonSchema(Required = true, EnumSerializationType = EnumSerializationType.AsLowerString)]
        public ProjectionType type;

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
