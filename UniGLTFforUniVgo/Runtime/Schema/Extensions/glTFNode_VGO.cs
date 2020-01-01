// ----------------------------------------------------------------------
// @Namespace : UniGLTFforUniVgo
// @Class     : glTFNode_VGO
// ----------------------------------------------------------------------
namespace UniGLTFforUniVgo
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Node extensions VGO
    /// </summary>
    [Serializable]
    [JsonObject("nodes.extensions.VGO_nodes")]
    public class glTFNode_VGO
    {
        /// <summary>Colliders</summary>
        [JsonProperty("colliders")]
        public List<glTFNode_VGO_Collider> colliders = null;

        /// <summary>Rigidbody</summary>
        [JsonProperty("rigidbody")]
        public glTFNode_VGO_Rigidbody rigidbody = null;

        /// <summary>Right</summary>
        [JsonProperty("right")]
        public glTF_VGO_Right right = null;

        /// <summary></summary>
        [JsonIgnore]
        public static string ExtensionName => "VGO_nodes";
    }
}