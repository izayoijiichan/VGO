using Newtonsoft.Json;
using System;
using System.IO;

namespace UniGLTFforUniVgo
{
    /// <summary>
    /// Image data used to create a texture.
    /// Image can be referenced by URI or `bufferView` index.
    /// `mimeType` is required in the latter case.
    /// </summary>
    [Serializable]
    [JsonObject("image")]
    public class glTFImage
    {
        /// <summary></summary>
        [JsonProperty("name")]
        public string name;

        /// <summary></summary>
        [JsonProperty("uri")]
        public string uri;

        /// <summary></summary>
        [JsonProperty("mimeType", Required = Required.Always)]
        //[JsonSchema(EnumValues = new object[] { "image/jpeg", "image/png" }, EnumSerializationType =EnumSerializationType.AsString)]
        public string mimeType;

        /// <summary></summary>
        [JsonProperty("bufferView", Required = Required.Always)]
        //[JsonSchema(Dependencies = new string[] { "mimeType" }, Minimum = 0)]
        public int bufferView = 0;

        /// <summary></summary>
        [JsonProperty("extensions")]
        public object extensions;

        /// <summary></summary>
        [JsonProperty("extras")]
        public object extras;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetExt()
        {
            switch (mimeType)
            {
                case "image/png":
                    return ".png";

                case "image/jpeg":
                    return ".jpg";

                default:
                    if (uri.StartsWith("data:image/jpeg;"))
                    {
                        return ".jpg";
                    }
                    else if (uri.StartsWith("data:image/png;"))
                    {
                        return ".png";
                    }
                    else
                    {
                        return Path.GetExtension(uri).ToLower();
                    }
            }
        }
    }
}
