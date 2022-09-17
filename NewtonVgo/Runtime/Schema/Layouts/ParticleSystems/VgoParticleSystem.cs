// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoParticleSystem
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using NewtonVgo.Schema.ParticleSystems;
    using System;

    /// <summary>
    /// VGO Particle System
    /// </summary>
    [Serializable]
    [JsonObject("particleSystem")]
    public class VgoParticleSystem
    {
        /// <summary>Access the main Particle System settings.</summary>
        [JsonProperty("main")]
        public VGO_PS_MainModule? main = null;

        /// <summary>Script interface for the EmissionModule of a Particle System.</summary>
        [JsonProperty("emission")]
        public VGO_PS_EmissionModule? emission = null;

        /// <summary>Script interface for the ShapeModule of a Particle System.</summary>
        [JsonProperty("shape")]
        public VGO_PS_ShapeModule? shape = null;

        /// <summary>Script interface for the VelocityOverLifetimeModule of a Particle System.</summary>
        [JsonProperty("velocityOverLifetime")]
        public VGO_PS_VelocityOverLifetimeModule? velocityOverLifetime = null;

        /// <summary>Script interface for the LimitVelocityOverLifetimeModule of a Particle System.</summary>
        [JsonProperty("limitVelocityOverLifetime")]
        public VGO_PS_LimitVelocityOverLifetimeModule? limitVelocityOverLifetime = null;

        /// <summary>Script interface for the InheritVelocityModule of a Particle System.</summary>
        [JsonProperty("inheritVelocity")]
        public VGO_PS_InheritVelocityModule? inheritVelocity = null;

        /// <summary>Script interface for the ForceOverLifetimeModule of a Particle System.</summary>
        [JsonProperty("forceOverLifetime")]
        public VGO_PS_ForceOverLifetimeModule? forceOverLifetime = null;

        /// <summary>Script interface for the ColorOverLifetimeModule of a Particle System.</summary>
        [JsonProperty("colorOverLifetime")]
        public VGO_PS_ColorOverLifetimeModule? colorOverLifetime = null;

        /// <summary>Script interface for the ColorByLifetimeModule of a Particle System.</summary>
        [JsonProperty("colorBySpeed")]
        public VGO_PS_ColorBySpeedModule? colorBySpeed = null;

        /// <summary>Script interface for the SizeOverLifetimeModule of a Particle System.</summary>
        [JsonProperty("sizeOverLifetime")]
        public VGO_PS_SizeOverLifetimeModule? sizeOverLifetime = null;

        /// <summary>Script interface for the SizeBySpeedModule of a Particle System.</summary>
        [JsonProperty("sizeBySpeed")]
        public VGO_PS_SizeBySpeedModule? sizeBySpeed = null;

        /// <summary>Script interface for the RotationOverLifetimeModule of a Particle System.</summary>
        [JsonProperty("rotationOverLifetime")]
        public VGO_PS_RotationOverLifetimeModule? rotationOverLifetime = null;

        /// <summary>Script interface for the RotationBySpeedModule of a Particle System.</summary>
        [JsonProperty("rotationBySpeed")]
        public VGO_PS_RotationBySpeedModule? rotationBySpeed = null;

        /// <summary>Script interface for the ExternalForcesModule of a Particle System.</summary>
        [JsonProperty("externalForces")]
        public VGO_PS_ExternalForcesModule? externalForces = null;

        /// <summary>Script interface for the NoiseModule of a Particle System.</summary>
        [JsonProperty("noise")]
        public VGO_PS_NoiseModule? noise = null;

        /// <summary>Script interface for the CollisionModule of a Particle System.</summary>
        [JsonProperty("collision")]
        public VGO_PS_CollisionModule? collision = null;

        /// <summary>Script interface for the TriggerModule of a Particle System.</summary>
        [JsonProperty("trigger")]
        public VGO_PS_TriggerModule? trigger = null;

        /// <summary>Script interface for the SubEmittersModule of a Particle System.</summary>
        [JsonProperty("subEmitters")]
        public VGO_PS_SubEmittersModule? subEmitters = null;

        /// <summary>Script interface for the TextureSheetAnimationModule of a Particle System.</summary>
        [JsonProperty("textureSheetAnimation")]
        public VGO_PS_TextureSheetAnimationModule? textureSheetAnimation = null;

        /// <summary>Script interface for the LightsModule of a Particle System.</summary>
        [JsonProperty("lights")]
        public VGO_PS_LightsModule? lights = null;

        /// <summary>Script interface for the TrailsModule of a Particle System.</summary>
        [JsonProperty("trails")]
        public VGO_PS_TrailModule? trails = null;

        /// <summary>Script interface for the CustomDataModule of a Particle System.</summary>
        [JsonProperty("customData")]
        public VGO_PS_CustomDataModule? customData = null;

        ///// <summary>Does this system support Procedural Simulation?</summary>
        //[JsonProperty("proceduralSimulationSupported")]
        //public bool proceduralSimulationSupported { get; }

        ///// <summary>Controls whether the Particle System uses an automatically-generated random number to seed the random number generator.</summary>
        //[JsonProperty("useAutoRandomSeed")]
        //public bool useAutoRandomSeed = null;

        ///// <summary>Override the random seed used for the Particle System emission.</summary>
        //[JsonProperty("randomSeed")]
        //public uint randomSeed = null;

        /// <summary></summary>
        [JsonProperty("renderer")]
        public VGO_PS_Renderer? renderer = null;
    }
}
