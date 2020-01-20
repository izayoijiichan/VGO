// ----------------------------------------------------------------------
// @Namespace : UniGLTFforUniVgo
// @Class     : glTFNode_extensions
// ----------------------------------------------------------------------
namespace UniGLTFforUniVgo
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// node.extensions
    /// </summary>
    [Serializable]
    [JsonObject("node.extensions")]
    public class glTFNode_extensions
    {
        /// <summary>VGO</summary>
        [JsonProperty("VGO_nodes")]
        public VGO_nodes VGO_nodes = null;
    }
}
