// ----------------------------------------------------------------------
// @Namespace : UniGLTFforUniVgo
// @Class     : VGO_PS_InheritVelocityModule
// ----------------------------------------------------------------------
namespace UniGLTFforUniVgo
{
    using Newtonsoft.Json;
    using System;
    using UnityEngine;

    /// <summary>
    /// VGO Particle System InheritVelocityModule
    /// </summary>
    [Serializable]
    [JsonObject("vgo.ps.inheritVelocityModule")]
    public class VGO_PS_InheritVelocityModule
    {
        /// <summary>Specifies whether the InheritVelocityModule is enabled or disabled.</summary>
        [JsonProperty("enabled")]
        public bool enabled;

        /// <summary>Specifies how to apply emitter velocity to particles.</summary>
        [JsonProperty("mode")]
        public ParticleSystemInheritVelocityMode mode;

        /// <summary>Curve to define how much of the emitter velocity the system applies during the lifetime of a particle.</summary>
        [JsonProperty("curve")]
        public VGO_PS_MinMaxCurve curve;

        /// <summary>Change the curve multiplier.</summary>
        [JsonProperty("curveMultiplier")]
        public float curveMultiplier;
    }
}
