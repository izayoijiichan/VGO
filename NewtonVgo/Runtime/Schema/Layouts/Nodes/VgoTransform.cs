// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoTransform
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;
    using System.Numerics;

    /// <summary>
    /// VGO Transform
    /// </summary>
    [Serializable]
    [JsonObject("node.transform")]
    public class VgoTransform
    {
        /// <summary>Position of the transform relative to the parent transform.</summary>
        [JsonProperty("position")]
        public Vector3? position = null;

        /// <summary>The rotation of the transform relative to the transform rotation of the parent.</summary>
        [JsonProperty("rotation")]
        public Quaternion? rotation = null;

        /// <summary>The scale of the transform relative to the GameObjects parent.</summary>
        [JsonProperty("scale")]
        public Vector3? scale = null;
    }
}
