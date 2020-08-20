// ----------------------------------------------------------------------
// @Namespace : NewtonVgo.Schema.ParticleSystems
// @Class     : VGO_PS_LimitVelocityOverLifetimeModule
// ----------------------------------------------------------------------
namespace NewtonVgo.Schema.ParticleSystems
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;

    /// <summary>
    /// VGO Particle System LimitVelocityOverLifetimeModule
    /// </summary>
    [Serializable]
    [JsonObject("vgo.ps.limitVelocityOverLifetimeModule")]
    public class VGO_PS_LimitVelocityOverLifetimeModule
    {
        /// <summary>Specifies whether the LimitForceOverLifetimeModule is enabled or disabled.</summary>
        [JsonProperty("enabled")]
        public bool enabled;

        /// <summary>Set the velocity limit on each axis separately.</summary>
        [JsonProperty("separateAxes")]
        public bool separateAxes;

        ///// <summary>Maximum velocity curve, when not using one curve per axis.</summary>
        //[JsonProperty("limit")]
        //[NativeName("LimitX")]
        //public VGO_PS_MinMaxCurve limit;

        ///// <summary>Change the limit multiplier.</summary>
        //[JsonProperty("limitMultiplier")]
        //[NativeName("LimitXMultiplier")]
        //public float limitMultiplier;

        /// <summary>Maximum velocity curve for the x-axis.</summary>
        [JsonProperty("limitX")]
        public VGO_PS_MinMaxCurve limitX;

        /// <summary>Maximum velocity curve for the y-axis.</summary>
        [JsonProperty("limitY")]
        public VGO_PS_MinMaxCurve limitY;

        /// <summary>Maximum velocity curve for the z-axis.</summary>
        [JsonProperty("limitZ")]
        public VGO_PS_MinMaxCurve limitZ;

        /// <summary>Change the limit multiplier on the x-axis.</summary>
        [JsonProperty("limitXMultiplier", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float limitXMultiplier = -1.0f;

        /// <summary>Change the limit multiplier on the y-axis.</summary>
        [JsonProperty("limitYMultiplier", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float limitYMultiplier = -1.0f;

        /// <summary>Change the limit multiplier on the z-axis.</summary>
        [JsonProperty("limitZMultiplier", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float limitZMultiplier = -1.0f;

        /// <summary>Specifies if the velocity limits are in local space (rotated with the transform) or world space.</summary>
        [JsonProperty("space")]
        public ParticleSystemSimulationSpace space;

        /// <summary>Controls how much this module dampens particle velocities that exceed the velocity limit.</summary>
        [JsonProperty("dampen")]
        public float dampen;

        /// <summary>Controls the amount of drag that this modules applies to the particle velocities.</summary>
        [JsonProperty("drag")]
        public VGO_PS_MinMaxCurve drag;

        /// <summary>Specifies the drag multiplier.</summary>
        [JsonProperty("dragMultiplier")]
        public float dragMultiplier;

        /// <summary>Adjust the amount of drag this module applies to particles, based on their sizes.</summary>
        [JsonProperty("multiplyDragByParticleSize")]
        public bool multiplyDragByParticleSize;

        /// <summary>Adjust the amount of drag this module applies to particles, based on their speeds.</summary>
        [JsonProperty("multiplyDragByParticleVelocity")]
        public bool multiplyDragByParticleVelocity;
    }
}
