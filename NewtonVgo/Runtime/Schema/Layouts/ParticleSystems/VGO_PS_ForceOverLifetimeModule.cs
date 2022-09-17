// ----------------------------------------------------------------------
// @Namespace : NewtonVgo.Schema.ParticleSystems
// @Class     : VGO_PS_ForceOverLifetimeModule
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo.Schema.ParticleSystems
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// VGO Particle System ForceOverLifetimeModule
    /// </summary>
    [Serializable]
    [JsonObject("vgo.ps.forceOverLifetimeModule")]
    public class VGO_PS_ForceOverLifetimeModule
    {
        /// <summary>Specifies whether the ForceOverLifetimeModule is enabled or disabled.</summary>
        [JsonProperty("enabled")]
        public bool enabled;

        /// <summary>The curve that defines particle forces in the x-axis.</summary>
        [JsonProperty("x")]
        public VGO_PS_MinMaxCurve? x;

        /// <summary>The curve defining particle forces in the y-axis.</summary>
        [JsonProperty("y")]
        public VGO_PS_MinMaxCurve? y;

        /// <summary>The curve defining particle forces in the z-axis.</summary>
        [JsonProperty("z")]
        public VGO_PS_MinMaxCurve? z;

        /// <summary>Defines the x-axis multiplier.</summary>
        [JsonProperty("xMultiplier")]
        public float xMultiplier;

        /// <summary>Defines the y-axis multiplier.</summary>
        [JsonProperty("yMultiplier")]
        public float yMultiplier;

        /// <summary>Defines the z-axis multiplier.</summary>
        [JsonProperty("zMultiplier")]
        public float zMultiplier;

        /// <summary>Specifies whether the modules applies the forces in local or world space.</summary>
        [JsonProperty("space")]
        public ParticleSystemSimulationSpace space;

        /// <summary>When randomly selecting values between two curves or constants, this flag causes the system to choose a new random force on each frame.</summary>
        [JsonProperty("randomized")]
        public bool randomized;
    }
}
