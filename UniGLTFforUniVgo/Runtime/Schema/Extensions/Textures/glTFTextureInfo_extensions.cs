// ----------------------------------------------------------------------
// @Namespace : UniGLTFforUniVgo
// @Class     : glTFTextureInfo_extensions
// ----------------------------------------------------------------------
namespace UniGLTFforUniVgo
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// glTF TextureInfo extensions
    /// </summary>
    [Serializable]
    [JsonObject("textureinfo.extensions")]
    public class glTFTextureInfo_extensions
    {
        /// <summary></summary>
        [JsonProperty("KHR_texture_transform")]
        public KHR_texture_transform KHR_texture_transform = null;
    }
}
