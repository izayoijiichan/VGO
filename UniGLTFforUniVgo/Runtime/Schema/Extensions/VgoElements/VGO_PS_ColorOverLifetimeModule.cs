// ----------------------------------------------------------------------
// @Namespace : UniGLTFforUniVgo
// @Class     : VGO_PS_ColorOverLifetimeModule
// ----------------------------------------------------------------------
namespace UniGLTFforUniVgo
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// VGO Particle System ColorOverLifetimeModule
    /// </summary>
    [Serializable]
    [JsonObject("vgo.ps.colorOverLifetimeModule")]
    public class VGO_PS_ColorOverLifetimeModule
    {
        /// <summary>Specifies whether the ColorOverLifetimeModule is enabled or disabled.</summary>
        [JsonProperty("enabled")]
        public bool enabled;

        /// <summary>The gradient that controls the particle colors.</summary>
        [JsonProperty("color")]
        public VGO_PS_MinMaxGradient color;
    }
}
