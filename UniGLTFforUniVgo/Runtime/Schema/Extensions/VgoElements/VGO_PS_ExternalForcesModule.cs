// ----------------------------------------------------------------------
// @Namespace : UniGLTFforUniVgo
// @Class     : VGO_PS_ExternalForcesModule
// ----------------------------------------------------------------------
namespace UniGLTFforUniVgo
{
    using Newtonsoft.Json;
    using System;
    using UnityEngine;

    /// <summary>
    /// VGO Particle System ExternalForcesModule
    /// </summary>
    [Serializable]
    [JsonObject("vgo.ps.externalForcesModule")]
    public class VGO_PS_ExternalForcesModule
    {
        /// <summary>Specifies whether the ExternalForcesModule is enabled or disabled.</summary>
        [JsonProperty("enabled")]
        public bool enabled;

        /// <summary>Multiplies the magnitude of applied external forces.</summary>
        [JsonProperty("multiplierCurve")]
        public VGO_PS_MinMaxCurve multiplierCurve;

        /// <summary>Multiplies the magnitude of external forces affecting the particles.</summary>
        [JsonProperty("multiplier")]
        public float multiplier;

        /// <summary>Apply all Force Fields belonging to a matching Layer to this Particle System.</summary>
        [JsonProperty("influenceFilter")]
        public ParticleSystemGameObjectFilter influenceFilter;

        /// <summary>Particle System Force Field Components with a matching Layer affect this Particle System.</summary>
        [JsonProperty("influenceMask")]
        //public LayerMask influenceMask;
        public int influenceMask;

        ///// <summary>The number of Force Fields explicitly provided to the influencers list.</summary>
        //[JsonProperty("influenceCount")]
        //public int influenceCount { get; }
    }
}
