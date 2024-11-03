// ----------------------------------------------------------------------
// @Namespace : NewtonVgo.Schema.ParticleSystems
// @Class     : VGO_PS_CustomDataModule
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo.Schema.ParticleSystems
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// VGO Particle System CustomDataModule
    /// </summary>
    [Serializable]
    [JsonObject("vgo.ps.customDataModule")]
    public class VGO_PS_CustomDataModule
    {
        /// <summary>Specifies whether the CustomDataModule is enabled or disabled.</summary>
        [JsonProperty("enabled")]
        public bool enabled;

        /// <summary>Array of the custom data.</summary>
        [JsonProperty("customData")]
        public VGO_PS_CustomData[]? customData;
    }

    /// <summary>
    /// VGO Particle System CustomData
    /// </summary>
    [Serializable]
    [JsonObject("vgo.ps.customData")]
    public class VGO_PS_CustomData
    {
        /// <summary>The stream of data.</summary>
        [JsonProperty("stream")]
        public ParticleSystemCustomData stream;

        /// <summary>The mode of data.</summary>
        [JsonProperty("mode")]
        public ParticleSystemCustomDataMode mode;

        /// <summary>Array of the vector component.</summary>
        [JsonProperty("vector")]
        public VGO_PS_MinMaxCurve[]? vector;

        ///// <summary>The color gradient.</summary>
        [JsonProperty("color")]
        public VGO_PS_MinMaxGradient? color;
    }
}
