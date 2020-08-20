// ----------------------------------------------------------------------
// @Namespace : NewtonVgo.Schema.ParticleSystems
// @Class     : VGO_PS_VelocityOverLifetimeModule
// ----------------------------------------------------------------------
namespace NewtonVgo.Schema.ParticleSystems
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// VGO Particle System VelocityOverLifetimeModule
    /// </summary>
    [Serializable]
    [JsonObject("vgo.ps.velocityOverLifetimeModule")]
    public class VGO_PS_VelocityOverLifetimeModule
    {
        /// <summary>Specifies whether the VelocityOverLifetimeModule is enabled or disabled.</summary>
        [JsonProperty("enabled")]
        public bool enabled;

        /// <summary>Curve to control particle speed based on lifetime, on the x-axis.</summary>
        [JsonProperty("x")]
        public VGO_PS_MinMaxCurve x;

        /// <summary>Curve to control particle speed based on lifetime, on the y-axis.</summary>
        [JsonProperty("y")]
        public VGO_PS_MinMaxCurve y;

        /// <summary>Curve to control particle speed based on lifetime, on the z-axis.</summary>
        [JsonProperty("z")]
        public VGO_PS_MinMaxCurve z;

        /// <summary>A multiplier for ParticleSystem.VelocityOverLifetimeModule._x</summary>
        [JsonProperty("xMultiplier")]
        public float xMultiplier;

        /// <summary>A multiplier for ParticleSystem.VelocityOverLifetimeModule._y.</summary>
        [JsonProperty("yMultiplier")]
        public float yMultiplier;

        /// <summary>A multiplier for ParticleSystem.VelocityOverLifetimeModule._z.</summary>
        [JsonProperty("zMultiplier")]
        public float zMultiplier;

        /// <summary>Specifies if the velocities are in local space (rotated with the transform) or world space.</summary>
        [JsonProperty("space")]
        public ParticleSystemSimulationSpace space;

        /// <summary>Curve to control particle speed based on lifetime, around the x-axis.</summary>
        [JsonProperty("orbitalX")]
        public VGO_PS_MinMaxCurve orbitalX;

        /// <summary>Curve to control particle speed based on lifetime, around the y-axis.</summary>
        [JsonProperty("orbitalY")]
        public VGO_PS_MinMaxCurve orbitalY;

        /// <summary>Curve to control particle speed based on lifetime, around the z-axis.</summary>
        [JsonProperty("orbitalZ")]
        public VGO_PS_MinMaxCurve orbitalZ;

        /// <summary>Speed multiplier along the x-axis.</summary>
        [JsonProperty("orbitalXMultiplier")]
        public float orbitalXMultiplier;

        /// <summary>Speed multiplier along the y-axis.</summary>
        [JsonProperty("orbitalYMultiplier")]
        public float orbitalYMultiplier;

        /// <summary>Speed multiplier along the z-axis.</summary>
        [JsonProperty("orbitalZMultiplier")]
        public float orbitalZMultiplier;

        /// <summary>Specify a custom center of rotation for the orbital and radial velocities.</summary>
        [JsonProperty("orbitalOffsetX")]
        public VGO_PS_MinMaxCurve orbitalOffsetX;

        /// <summary>Specify a custom center of rotation for the orbital and radial velocities.</summary>
        [JsonProperty("orbitalOffsetY")]
        public VGO_PS_MinMaxCurve orbitalOffsetY;

        /// <summary>Specify a custom center of rotation for the orbital and radial velocities.</summary>
        [JsonProperty("orbitalOffsetZ")]
        public VGO_PS_MinMaxCurve orbitalOffsetZ;

        /// <summary>A multiplier for _orbitalOffsetX.</summary>
        [JsonProperty("orbitalOffsetXMultiplier")]
        public float orbitalOffsetXMultiplier;

        /// <summary>A multiplier for _orbitalOffsetY.</summary>
        [JsonProperty("orbitalOffsetYMultiplier")]
        public float orbitalOffsetYMultiplier;

        /// <summary>A multiplier for _orbitalOffsetY.</summary>
        [JsonProperty("orbitalOffsetZMultiplier")]
        public float orbitalOffsetZMultiplier;

        /// <summary>Curve to control particle speed based on lifetime, away from a center position.</summary>
        [JsonProperty("radial")]
        public VGO_PS_MinMaxCurve radial;

        /// <summary>A multiplier for ParticleSystem.VelocityOverLifetimeModule._radial.</summary>
        [JsonProperty("radialMultiplier")]
        public float radialMultiplier;

        /// <summary>Curve to control particle speed based on lifetime, without affecting the direction of the particles.</summary>
        [JsonProperty("speedModifier")]
        public VGO_PS_MinMaxCurve speedModifier;

        /// <summary>A multiplier for ParticleSystem.VelocityOverLifetimeModule._speedModifier.</summary>
        [JsonProperty("speedModifierMultiplier")]
        public float speedModifierMultiplier;
    }
}
