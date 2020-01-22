// ----------------------------------------------------------------------
// @Namespace : UniGLTFforUniVgo
// @Class     : VGO_GradientColorKey
// ----------------------------------------------------------------------
namespace UniGLTFforUniVgo
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// VGO Gradient ColorKey
    /// </summary>
    [Serializable]
    [JsonObject("vgo.gradient.colorKey")]
    public class VGO_GradientColorKey
    {
        /// <summary>Color of key</summary>
        [JsonProperty("color")]
        public float[] color;

        /// <summary>Time of the key (0 - 1).</summary>
        [JsonProperty("time")]
        public float time;
    }
}
