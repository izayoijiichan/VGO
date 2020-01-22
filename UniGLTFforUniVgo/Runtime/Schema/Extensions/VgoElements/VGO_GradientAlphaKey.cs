// ----------------------------------------------------------------------
// @Namespace : UniGLTFforUniVgo
// @Class     : VGO_GradientAlphaKey
// ----------------------------------------------------------------------
namespace UniGLTFforUniVgo
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// VGO Gradient AlphaKey
    /// </summary>
    [Serializable]
    [JsonObject("vgo.gradient.alphaKey")]
    public class VGO_GradientAlphaKey
    {
        /// <summary>Alpha channel of key.</summary>
        [JsonProperty("alpha")]
        public float alpha;

        /// <summary>Time of the key (0 - 1).</summary>
        [JsonProperty("time")]
        public float time;
    }
}
