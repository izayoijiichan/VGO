// ----------------------------------------------------------------------
// @Namespace : UniGLTFforUniVgo
// @Class     : KHR_texture_transform
// ----------------------------------------------------------------------
namespace UniGLTFforUniVgo
{
    using Newtonsoft.Json;
    using System;

    [Serializable]
    public class KHR_texture_transform
    {
        [JsonIgnore]
        public static string ExtensionName => "KHR_texture_transform";

        [JsonProperty("offset")]
        public float[] offset = new float[2] { 0.0f, 0.0f };

        [JsonProperty("rotation")]
        public float rotation;

        [JsonProperty("scale")]
        public float[] scale = new float[2] { 1.0f, 1.0f };

        [JsonProperty("texCoord")]
        public int texCoord;
    }
}
