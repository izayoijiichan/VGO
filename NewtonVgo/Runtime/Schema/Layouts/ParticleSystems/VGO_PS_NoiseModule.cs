// ----------------------------------------------------------------------
// @Namespace : NewtonVgo.Schema.ParticleSystems
// @Class     : VGO_PS_NoiseModule
// ----------------------------------------------------------------------
namespace NewtonVgo.Schema.ParticleSystems
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;

    /// <summary>
    /// VGO Particle System NoiseModule
    /// </summary>
    [Serializable]
    [JsonObject("vgo.ps.noiseModule")]
    public class VGO_PS_NoiseModule
    {
        /// <summary>Specifies whether the the NoiseModule is enabled or disabled.</summary>
        [JsonProperty("enabled")]
        public bool enabled;

        /// <summary>Control the noise separately for each axis.</summary>
        [JsonProperty("separateAxes")]
        public bool separateAxes;

        ///// <summary>How strong the overall noise effect is.</summary>
        //[JsonProperty("strength")]
        //[NativeName("StrengthX")]
        //public VGO_PS_MinMaxCurve strength;

        ///// <summary>Strength multiplier.</summary>
        //[JsonProperty("strengthMultiplier")]
        //[NativeName("StrengthXMultiplier")]
        //public float strengthMultiplier;

        /// <summary>Define the strength of the effect on the x-axis, when using the ParticleSystem.NoiseModule.separateAxes option.</summary>
        [JsonProperty("strengthX")]
        public VGO_PS_MinMaxCurve strengthX;

        /// <summary>Define the strength of the effect on the y-axis, when using the ParticleSystem.NoiseModule.separateAxes option.</summary>
        [JsonProperty("strengthY")]
        public VGO_PS_MinMaxCurve strengthY;

        /// <summary>Define the strength of the effect on the z-axis, when using the ParticleSystem.NoiseModule.separateAxes option.</summary>
        [JsonProperty("strengthZ")]
        public VGO_PS_MinMaxCurve strengthZ;

        /// <summary>x-axis strength multiplier.</summary>
        [JsonProperty("strengthXMultiplier", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float strengthXMultiplier = -1.0f;

        /// <summary>y-axis strength multiplier.</summary>
        [JsonProperty("strengthYMultiplier", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float strengthYMultiplier = -1.0f;

        /// <summary>z-axis strength multiplier.</summary>
        [JsonProperty("strengthZMultiplier", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float strengthZMultiplier = -1.0f;

        /// <summary>Low values create soft, smooth noise, and high values create rapidly changing noise.</summary>
        [JsonProperty("frequency")]
        public float frequency;

        /// <summary>Scroll the noise map over the Particle System.</summary>
        [JsonProperty("scrollSpeed")]
        public VGO_PS_MinMaxCurve scrollSpeed;

        /// <summary>Scroll speed multiplier.</summary>
        [JsonProperty("scrollSpeedMultiplier")]
        public float scrollSpeedMultiplier;

        /// <summary>Higher frequency noise reduces the strength by a proportional amount, if enabled.</summary>
        [JsonProperty("damping")]
        public bool damping;

        /// <summary>Layers of noise that combine to produce final noise.</summary>
        [JsonProperty("octaveCount")]
        public int octaveCount;

        /// <summary>When combining each octave, scale the intensity by this amount.</summary>
        [JsonProperty("octaveMultiplier")]
        public float octaveMultiplier;

        /// <summary>When combining each octave, zoom in by this amount.</summary>
        [JsonProperty("octaveScale")]
        public float octaveScale;

        /// <summary>Generate 1D, 2D or 3D noise.</summary>
        [JsonProperty("quality")]
        public ParticleSystemNoiseQuality quality;

        /// <summary>Enable remapping of the final noise values, allowing for noise values to be translated into different values.</summary>
        [JsonProperty("remapEnabled")]
        public bool remapEnabled;

        ///// <summary>Define how the noise values are remapped.</summary>
        //[JsonProperty("remap")]
        //[NativeName("RemapX")]
        //public VGO_PS_MinMaxCurve remap;

        ///// <summary>Remap multiplier.</summary>
        //[JsonProperty("remapMultiplier")]
        //[NativeName("RemapXMultiplier")]
        //public float remapMultiplier;

        /// <summary>Define how the noise values are remapped on the x-axis, when using the ParticleSystem.NoiseModule.separateAxes option.</summary>
        [JsonProperty("remapX")]
        public VGO_PS_MinMaxCurve remapX;

        /// <summary>Define how the noise values are remapped on the y-axis, when using the ParticleSystem.NoiseModule.separateAxes option.</summary>
        [JsonProperty("remapY")]
        public VGO_PS_MinMaxCurve remapY;

        /// <summary>Define how the noise values are remapped on the z-axis, when using the ParticleSystem.NoiseModule.separateAxes option.</summary>
        [JsonProperty("remapZ")]
        public VGO_PS_MinMaxCurve remapZ;

        /// <summary>x-axis remap multiplier.</summary>
        [JsonProperty("remapXMultiplier", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float remapXMultiplier = -1.0f;

        /// <summary>y-axis remap multiplier.</summary>
        [JsonProperty("remapYMultiplier", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float remapYMultiplier = -1.0f;

        /// <summary>z-axis remap multiplier.</summary>
        [JsonProperty("remapZMultiplier", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float remapZMultiplier = -1.0f;

        /// <summary>How much the noise affects the particle positions.</summary>
        [JsonProperty("positionAmount")]
        public VGO_PS_MinMaxCurve positionAmount;

        /// <summary>How much the noise affects the particle rotation, in degrees per second.</summary>
        [JsonProperty("rotationAmount")]
        public VGO_PS_MinMaxCurve rotationAmount;

        /// <summary>How much the noise affects the particle sizes, applied as a multiplier on the size of each particle.</summary>
        [JsonProperty("sizeAmount")]
        public VGO_PS_MinMaxCurve sizeAmount;
    }
}
