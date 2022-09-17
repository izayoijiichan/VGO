// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoGradientAlphaKey
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// VGO Gradient AlphaKey
    /// </summary>
    [Serializable]
    [JsonObject("gradient.alphaKey")]
    public class VgoGradientAlphaKey
    {
        /// <summary>Alpha channel of key.</summary>
        [JsonProperty("alpha")]
        public float alpha;

        /// <summary>Time of the key (0 - 1).</summary>
        [JsonProperty("time")]
        public float time;
    }
}
