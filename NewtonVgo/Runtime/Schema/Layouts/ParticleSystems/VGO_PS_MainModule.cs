// ----------------------------------------------------------------------
// @Namespace : NewtonVgo.Schema.ParticleSystems
// @Class     : VGO_PS_MainModule
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo.Schema.ParticleSystems
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;
    using System.Numerics;

    /// <summary>
    /// VGO Particle System MainModule
    /// </summary>
    [Serializable]
    [JsonObject("vgo.ps.mainModule")]
    public class VGO_PS_MainModule
    {
        /// <summary>The duration of the Particle System in seconds.</summary>
        [JsonProperty("duration")]
        public float duration;

        /// <summary>Specifies whether the Particle System is looping.</summary>
        [JsonProperty("loop")]
        public bool loop;

        /// <summary>If ParticleSystem.MainModule._loop is true, when you enable this property, the Particle System looks like it has already simulated for one loop when first becoming visible.</summary>
        [JsonProperty("prewarm")]
        public bool prewarm;

        /// <summary>Start delay in seconds.</summary>
        [JsonProperty("startDelay")]
        public VGO_PS_MinMaxCurve? startDelay;

        /// <summary>A multiplier for ParticleSystem.MainModule._startDelay in seconds.</summary>
        [JsonProperty("startDelayMultiplier")]
        public float startDelayMultiplier;

        /// <summary>The total lifetime in seconds that each new particle has.</summary>
        [JsonProperty("startLifetime")]
        public VGO_PS_MinMaxCurve? startLifetime;

        /// <summary>A multiplier for ParticleSystem.MainModule._startLifetime.</summary>
        [JsonProperty("startLifetimeMultiplier")]
        public float startLifetimeMultiplier;

        /// <summary>The initial speed of particles when the Particle System first spawns them.</summary>
        [JsonProperty("startSpeed")]
        public VGO_PS_MinMaxCurve? startSpeed;

        /// <summary>A multiplier for ParticleSystem.MainModule._startSpeed.</summary>
        [JsonProperty("startSpeedMultiplier")]
        public float startSpeedMultiplier;

        /// <summary>A flag to enable specifying particle size individually for each axis.</summary>
        [JsonProperty("startSize3D")]
        public bool startSize3D;

        /// <summary>The initial size of particles when the Particle System first spawns them.</summary>
        [JsonProperty("startSize")]
        public VGO_PS_MinMaxCurve? startSize;

        /// <summary>The initial size of particles along the x-axis when the Particle System first spawns them.</summary>
        [JsonProperty("startSizeX")]
        public VGO_PS_MinMaxCurve? startSizeX;

        /// <summary>The initial size of particles along the y-axis when the Particle System first spawns them.</summary>
        [JsonProperty("startSizeY")]
        public VGO_PS_MinMaxCurve? startSizeY;

        /// <summary>The initial size of particles along the z-axis when the Particle System first spawns them.</summary>
        [JsonProperty("startSizeZ")]
        public VGO_PS_MinMaxCurve? startSizeZ;

        /// <summary>A multiplier for the initial size of particles when the Particle System first spawns them.</summary>
        [JsonProperty("startSizeMultiplier", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float startSizeMultiplier = -1.0f;

        /// <summary>A multiplier for ParticleSystem.MainModule._startSizeX.</summary>
        [JsonProperty("startSizeXMultiplier", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float startSizeXMultiplier = -1.0f;

        /// <summary>A multiplier for ParticleSystem.MainModule._startSizeY.</summary>
        [JsonProperty("startSizeYMultiplier", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float startSizeYMultiplier = -1.0f;

        /// <summary>A multiplier for ParticleSystem.MainModule._startSizeZ.</summary>
        [JsonProperty("startSizeZMultiplier", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float startSizeZMultiplier = -1.0f;

        /// <summary>A flag to enable 3D particle rotation.</summary>
        [JsonProperty("startRotation3D")]
        public bool startRotation3D;

        /// <summary>The initial rotation of particles when the Particle System first spawns them.</summary>
        [JsonProperty("startRotation")]
        public VGO_PS_MinMaxCurve? startRotation;

        /// <summary>The initial rotation of particles around the x-axis when emitted.</summary>
        [JsonProperty("startRotationX")]
        public VGO_PS_MinMaxCurve? startRotationX;

        /// <summary>The initial rotation of particles around the y-axis when the Particle System first spawns them.</summary>
        [JsonProperty("startRotationY")]
        public VGO_PS_MinMaxCurve? startRotationY;

        /// <summary>The initial rotation of particles around the z-axis when the Particle System first spawns them.</summary>
        [JsonProperty("startRotationZ")]
        public VGO_PS_MinMaxCurve? startRotationZ;

        /// <summary>A multiplier for ParticleSystem.MainModule._startRotation.</summary>
        [JsonProperty("startRotationMultiplier", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float StartRotationMultiplier = -1.0f;

        /// <summary>The initial rotation multiplier of particles around the x-axis when the Particle System first spawns them.</summary>
        [JsonProperty("startRotationXMultiplier", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float StartRotationXMultiplier = -1.0f;

        /// <summary>The initial rotation multiplier of particles around the y-axis when the Particle System first spawns them..</summary>
        [JsonProperty("startRotationYMultiplier", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float StartRotationYMultiplier = -1.0f;

        /// <summary>The initial rotation multiplier of particles around the z-axis when the Particle System first spawns them.</summary>
        [JsonProperty("startRotationZMultiplier", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float StartRotationZMultiplier = -1.0f;

        /// <summary>Makes some particles spin in the opposite direction.</summary>
        [JsonProperty("flipRotation")]
        public float flipRotation;

        /// <summary>The initial color of particles when the Particle System first spawns them.</summary>
        [JsonProperty("startColor")]
        public VGO_PS_MinMaxGradient? startColor;

        /// <summary>A scale that this Particle System applies to gravity, defined by Physics.gravity.</summary>
        [JsonProperty("gravityModifier")]
        public VGO_PS_MinMaxCurve? gravityModifier;

        /// <summary>Change the gravity multiplier.</summary>
        [JsonProperty("gravityModifierMultiplier")]
        public float gravityModifierMultiplier;

        /// <summary>This selects the space in which to simulate particles. It can be either world or local space.</summary>
        [JsonProperty("simulationSpace")]
        public ParticleSystemSimulationSpace simulationSpace;

        /// <summary>Override the default playback speed of the Particle System.</summary>
        [JsonProperty("simulationSpeed")]
        public float simulationSpeed;

        /// <summary>Simulate particles relative to a custom transform component.</summary>
        [JsonProperty("customSimulationSpace")]
        public VgoTransform? customSimulationSpace;

        /// <summary>When true, use the unscaled delta time to simulate the Particle System. Otherwise, use the scaled delta time.</summary>
        [JsonProperty("useUnscaledTime")]
        public bool useUnscaledTime;

        /// <summary>Control how the Particle System applies its Transform component to the particles it emits.</summary>
        [JsonProperty("scalingMode")]
        public ParticleSystemScalingMode scalingMode;

        /// <summary>If set to true, the Particle System automatically begins to play on startup.</summary>
        [JsonProperty("playOnAwake")]
        public bool playOnAwake;

        /// <summary>Control how the Particle System calculates its velocity, when moving in the world.</summary>
        [JsonProperty("emitterVelocityMode")]
        public ParticleSystemEmitterVelocityMode emitterVelocityMode;

        /// <summary>The maximum number of particles to emit.</summary>
        [JsonProperty("maxParticles")]
        public int maxParticles;

        /// <summary>Select whether to Disable or Destroy the GameObject, or to call the OnParticleSystemStopped script Callback, when the Particle System stops and all particles have died.</summary>
        [JsonProperty("stopAction")]
        public ParticleSystemStopAction stopAction;

        /// <summary>Configure whether the Particle System will still be simulated each frame, when it is offscreen.</summary>
        [JsonProperty("cullingMode")]
        public ParticleSystemCullingMode cullingMode;

        /// <summary>Configure the Particle System to not kill its particles when their lifetimes are exceeded.</summary>
        [JsonProperty("ringBufferMode")]
        public ParticleSystemRingBufferMode ringBufferMode;

        /// <summary>When ParticleSystem.MainModule.ringBufferMode is set to loop, this value defines the proportion of the particle life that loops.</summary>
        [JsonProperty("ringBufferLoopRange")]
        public Vector2? ringBufferLoopRange;
    }
}
