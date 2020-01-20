// ----------------------------------------------------------------------
// @Namespace : UniGLTFforUniVgo
// @Class     : glTF_VGO
// ----------------------------------------------------------------------
namespace UniGLTFforUniVgo
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// glTF VGO
    /// </summary>
    [Serializable]
    [JsonObject("vgo")]
    public class glTF_VGO
    {
        /// <summary>VGO Meta</summary>
        [JsonProperty("meta", Required = Required.Default)]
        public glTF_VGO_Meta meta = null;

        /// <summary>VGO Right</summary>
        [JsonProperty("right", Required = Required.Default)]
        public glTF_VGO_Right right = null;

        /// <summary></summary>
        [JsonIgnore]
        public static string ExtensionName => "VGO";
    }
}