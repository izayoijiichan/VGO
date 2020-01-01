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
    [JsonObject("node.extensions")]
    [Serializable]
    public partial class glTFNode_extensions : ExtensionsBase<glTFNode_extensions>
    {
        /// <summary>VGO</summary>
        [JsonProperty("VGO_nodes")]
        public glTFNode_VGO VGO_nodes = null;
    }
}
