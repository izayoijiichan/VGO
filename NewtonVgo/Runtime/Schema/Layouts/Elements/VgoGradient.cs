// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoGradient
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// VGO Gradient
    /// </summary>
    [Serializable]
    [JsonObject("gradient")]
    public class VgoGradient
    {
        /// <summary>All color keys defined in the gradient.</summary>
        [JsonProperty("colorKeys")]
        public VgoGradientColorKey[]? colorKeys = null;

        /// <summary>All alpha keys defined in the gradient.</summary>
        [JsonProperty("alphaKeys")]
        public VgoGradientAlphaKey[]? alphaKeys = null;

        /// <summary>Control how the gradient is evaluated.</summary>
        [JsonProperty("mode")]
        public GradientMode mode;
    }
}
