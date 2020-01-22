// ----------------------------------------------------------------------
// @Namespace : UniGLTFforUniVgo
// @Class     : VGO_Transform
// ----------------------------------------------------------------------
namespace UniGLTFforUniVgo
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// glTF Node Transform
    /// </summary>
    [Serializable]
    [JsonObject("node.vgo.transform")]
    public class VGO_Transform
    {
        /// <summary>Position of the transform relative to the parent transform.</summary>
        [JsonProperty("position")]
        public float[] position = null;

        /// <summary>The rotation of the transform relative to the transform rotation of the parent.</summary>
        [JsonProperty("rotation")]
        public float[] rotation = null;

        /// <summary>The scale of the transform relative to the GameObjects parent.</summary>
        [JsonProperty("scale")]
        public float[] scale = null;
    }
}
