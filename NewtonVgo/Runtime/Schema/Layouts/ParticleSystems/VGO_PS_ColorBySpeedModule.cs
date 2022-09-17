// ----------------------------------------------------------------------
// @Namespace : NewtonVgo.Schema.ParticleSystems
// @Class     : VGO_PS_ColorBySpeedModule
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo.Schema.ParticleSystems
{
    using Newtonsoft.Json;
    using System;
    using System.Numerics;

    /// <summary>
    /// VGO Particle System ColorBySpeedModule
    /// </summary>
    [Serializable]
    [JsonObject("vgo.ps.colorBySpeedModule")]
    public class VGO_PS_ColorBySpeedModule
    {
        /// <summary>Specifies whether the ColorBySpeedModule is enabled or disabled.</summary>
        [JsonProperty("enabled")]
        public bool enabled;

        /// <summary>The gradient that controls the particle colors.</summary>
        [JsonProperty("color")]
        public VGO_PS_MinMaxGradient? color;

        /// <summary>Apply the color gradient between these minimum and maximum speeds.</summary>
        [JsonProperty("range")]
        public Vector2? range;
    }
}
