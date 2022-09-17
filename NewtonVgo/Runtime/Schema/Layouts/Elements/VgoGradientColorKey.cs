// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoGradientColorKey
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// VGO Gradient ColorKey
    /// </summary>
    [Serializable]
    [JsonObject("gradient.colorKey")]
    public class VgoGradientColorKey
    {
        /// <summary>Color of key</summary>
        [JsonProperty("color")]
        public Color4 color;

        /// <summary>Time of the key (0 - 1).</summary>
        [JsonProperty("time")]
        public float time;
    }
}
