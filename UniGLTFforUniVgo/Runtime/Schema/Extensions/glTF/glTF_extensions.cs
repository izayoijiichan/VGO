// ----------------------------------------------------------------------
// @Namespace : UniGLTFforUniVgo
// @Class     : glTF_extensions
// ----------------------------------------------------------------------
namespace UniGLTFforUniVgo
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// gltf.extensions
    /// </summary>
    [Serializable]
    [JsonObject("glTF.extensions")]
    public class glTF_extensions
    {
        /// <summary>VGO</summary>
        [JsonProperty("VGO")]
        public glTF_VGO VGO = null;
    }
}
