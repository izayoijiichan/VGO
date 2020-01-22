// ----------------------------------------------------------------------
// @Namespace : UniGLTFforUniVgo
// @Class     : VGO_PS_LightsModule
// ----------------------------------------------------------------------
namespace UniGLTFforUniVgo
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// VGO Particle System LightsModule
    /// </summary>
    [Serializable]
    [JsonObject("vgo.ps.lightsModule")]
    public class VGO_PS_LightsModule
    {
        /// <summary>Specifies whether the LightsModule is enabled or disabled.</summary>
        [JsonProperty("enabled")]
        public bool enabled;

        /// <summary>Choose what proportion of particles receive a dynamic light.</summary>
        [JsonProperty("ratio")]
        public float ratio;

        /// <summary>Randomly assign Lights to new particles based on ParticleSystem.LightsModule.ratio.</summary>
        [JsonProperty("useRandomDistribution")]
        public bool useRandomDistribution;

        /// <summary>Select what Light Prefab you want to base your particle lights on.</summary>
        [JsonProperty("light")]
        public VGO_Light light;

        /// <summary>Toggle whether the particle lights multiply their color by the particle color.</summary>
        [JsonProperty("useParticleColor")]
        public bool useParticleColor;

        /// <summary>Toggle whether the system multiplies the particle size by the light range to determine the final light range.</summary>
        [JsonProperty("sizeAffectsRange")]
        public bool sizeAffectsRange;

        /// <summary>Toggle whether the system multiplies the particle alpha by the light intensity when it computes the final light intensity.</summary>
        [JsonProperty("alphaAffectsIntensity")]
        public bool alphaAffectsIntensity;

        /// <summary>Define a curve to apply custom range scaling to particle Lights.</summary>
        [JsonProperty("range")]
        public VGO_PS_MinMaxCurve range;

        /// <summary>Range multiplier.</summary>
        [JsonProperty("rangeMultiplier")]
        public float rangeMultiplier;

        /// <summary>Define a curve to apply custom intensity scaling to particle Lights.</summary>
        [JsonProperty("intensity")]
        public VGO_PS_MinMaxCurve intensity;

        /// <summary>Intensity multiplier.</summary>
        [JsonProperty("intensityMultiplier")]
        public float intensityMultiplier;

        /// <summary>Set a limit on how many Lights this Module can create.</summary>
        [JsonProperty("maxLights")]
        public int maxLights;
    }
}
