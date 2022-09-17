// ----------------------------------------------------------------------
// @Namespace : NewtonVgo.Schema.ParticleSystems
// @Class     : VGO_PS_TrailModule
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo.Schema.ParticleSystems
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// VGO Particle System TrailModule
    /// </summary>
    [Serializable]
    [JsonObject("vgo.ps.trailModule")]
    public class VGO_PS_TrailModule
    {
        /// <summary>Specifies whether the TrailModule is enabled or disabled.</summary>
        [JsonProperty("enabled")]
        public bool enabled;

        /// <summary>Choose how the system generates the particle trails.</summary>
        [JsonProperty("mode")]
        public ParticleSystemTrailMode mode;

        /// <summary>Choose what proportion of particles receive a trail.</summary>
        [JsonProperty("ratio")]
        public float ratio;

        /// <summary>The curve describing the trail lifetime, throughout the lifetime of the particle.</summary>
        [JsonProperty("lifetime")]
        public VGO_PS_MinMaxCurve? lifetime;

        /// <summary>A multiplier for ParticleSystem.TrailModule._lifetime.</summary>
        [JsonProperty("lifetimeMultiplier")]
        public float lifetimeMultiplier;

        /// <summary>Set the minimum distance each trail can travel before the system adds a new vertex to it.</summary>
        [JsonProperty("minVertexDistance")]
        public float minVertexDistance;

        /// <summary>Drop new trail points in world space, regardless of Particle System Simulation Space.</summary>
        [JsonProperty("worldSpace")]
        public bool worldSpace;

        /// <summary>Specifies whether trails disappear immediately when their owning particle dies.</summary>
        [JsonProperty("dieWithParticles")]
        public bool dieWithParticles;

        /// <summary>Select how many lines to create through the Particle System.</summary>
        [JsonProperty("ribbonCount")]
        public int ribbonCount;

        /// <summary>Specifies whether, if you use this system as a sub-emitter, ribbons connect particles from each parent particle independently.</summary>
        [JsonProperty("splitSubEmitterRibbons")]
        public bool splitSubEmitterRibbons;

        /// <summary>Adds an extra position to each ribbon, connecting it to the location of the Transform Component.</summary>
        [JsonProperty("attachRibbonsToTransform")]
        public bool attachRibbonsToTransform;

        /// <summary>Choose whether the U coordinate of the trail Texture is tiled or stretched.</summary>
        [JsonProperty("textureMode")]
        public ParticleSystemTrailTextureMode textureMode;

        /// <summary>Set whether the particle size acts as a multiplier on top of the trail width.</summary>
        [JsonProperty("sizeAffectsWidth")]
        public bool sizeAffectsWidth;

        /// <summary>Set whether the particle size acts as a multiplier on top of the trail lifetime.</summary>
        [JsonProperty("sizeAffectsLifetime")]
        public bool sizeAffectsLifetime;

        /// <summary>Toggle whether the trail inherits the particle color as its starting color.</summary>
        [JsonProperty("useMeinheritParticleColorshColors")]
        public bool inheritParticleColor;

        /// <summary>The gradient that controls the trail colors during the lifetime of the attached particle.</summary>
        [JsonProperty("colorOverLifetime")]
        public VGO_PS_MinMaxGradient? colorOverLifetime;

        /// <summary>The curve describing the width of each trail point.</summary>
        [JsonProperty("widthOverTrail")]
        public VGO_PS_MinMaxCurve? widthOverTrail;

        /// <summary>A multiplier for ParticleSystem.TrailModule._widthOverTrail.</summary>
        [JsonProperty("widthOverTrailMultiplier")]
        public float widthOverTrailMultiplier;

        /// <summary>The gradient that controls the trail colors over the length of the trail.</summary>
        [JsonProperty("colorOverTrail")]
        public VGO_PS_MinMaxGradient? colorOverTrail;

        /// <summary>Configures the trails to generate Normals and Tangents.</summary>
        [JsonProperty("generateLightingData")]
        public bool generateLightingData;

        /// <summary>Apply a shadow bias to prevent self-shadowing layouts.</summary>
        [JsonProperty("shadowBias")]
        public float shadowBias;
    }
}
