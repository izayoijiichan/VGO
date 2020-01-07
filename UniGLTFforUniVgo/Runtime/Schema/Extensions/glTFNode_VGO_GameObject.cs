// ----------------------------------------------------------------------
// @Namespace : UniGLTFforUniVgo
// @Class     : glTFNode_VGO_GameObject
// ----------------------------------------------------------------------
namespace UniGLTFforUniVgo
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System;
    using System.ComponentModel;
    using UnityEngine;

    /// <summary>
    /// glTF Node VGO GameObject
    /// </summary>
    [Serializable]
    [JsonObject("node.vgo.gameobject")]
    public class glTFNode_VGO_GameObject
    {
        /// <summary>Is Active</summary>
        [JsonProperty("isActive", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(true)]
        public bool isActive = false;

        /// <summary>Is Static</summary>
        [JsonProperty("isStatic", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(false)]
        public bool isStatic = false;

        /// <summary>Tag</summary>
        [JsonProperty("tag", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue("Untagged")]
        public string tag = null;

        /// <summary>Layer</summary>
        [JsonProperty("layer", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Range(0, 31)]
        [DefaultValue(0)]
        public int layer = 0;
    }
}