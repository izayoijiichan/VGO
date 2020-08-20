// ----------------------------------------------------------------------
// @Namespace : NewtonVgo.Schema.ParticleSystems
// @Class     : VGO_PS_TextureSheetAnimationModule
// ----------------------------------------------------------------------
namespace NewtonVgo.Schema.ParticleSystems
{
    using Newtonsoft.Json;
    using System;
    using System.Numerics;

    /// <summary>
    /// VGO Particle System TextureSheetAnimationModule
    /// </summary>
    [Serializable]
    [JsonObject("vgo.ps.textureSheetAnimationModule")]
    public class VGO_PS_TextureSheetAnimationModule
    {
        /// <summary>Specifies whether the TextureSheetAnimationModule is enabled or disabled.</summary>
        [JsonProperty("enabled")]
        public bool enabled;

        /// <summary>Select whether the animated Texture information comes from a grid of frames on a single Texture, or from a list of Sprite objects.</summary>
        [JsonProperty("mode")]
        public ParticleSystemAnimationMode mode;

        /// <summary>Defines the tiling of the Texture in the x-axis.</summary>
        [JsonProperty("numTilesX")]
        public int numTilesX;

        /// <summary>Defines the tiling of the texture in the y-axis.</summary>
        [JsonProperty("numTilesY")]
        public int numTilesY;

        ///// <summary></summary>
        //[JsonProperty("sprites")]
        //public VGO_Sprite[] sprites;

        ///// <summary>The total number of sprites.</summary>
        //[JsonProperty("spriteCount")]
        //public int spriteCount { get; }

        /// <summary>Specifies the animation type.</summary>
        [JsonProperty("animation")]
        public ParticleSystemAnimationType animation;

        /// <summary>Select how particles choose which row of a Texture Sheet Animation to use.</summary>
        [JsonProperty("rowMode")]
        public ParticleSystemAnimationRowMode rowMode;

        /// <summary>
        /// Select whether the system bases the playback on mapping a curve to the lifetime of each particle,
        /// by using the particle speeds, or if playback simply uses a constant frames per second.
        /// </summary>
        [JsonProperty("timeMode")]
        public ParticleSystemAnimationTimeMode timeMode;

        /// <summary>
        /// Explicitly select which row of the Texture sheet to use.
        /// The system uses this property when ParticleSystem.TextureSheetAnimationModule.rowMode is set to Custom.
        /// </summary>
        [JsonProperty("rowIndex")]
        public int rowIndex;

        /// <summary>A curve to control which frame of the Texture sheet animation to play.</summary>
        [JsonProperty("frameOverTime")]
        public VGO_PS_MinMaxCurve frameOverTime;

        /// <summary>The frame over time mutiplier.</summary>
        [JsonProperty("frameOverTimeMultiplier")]
        public float frameOverTimeMultiplier;

        /// <summary>Define a random starting frame for the Texture sheet animation.</summary>
        [JsonProperty("startFrame")]
        public VGO_PS_MinMaxCurve startFrame;

        /// <summary>The starting frame multiplier.</summary>
        [JsonProperty("startFrameMultiplier")]
        public float startFrameMultiplier;

        /// <summary>Specify how particle speeds are mapped to the animation frames.</summary>
        [JsonProperty("speedRange")]
        //public Vector2 speedRange;
        public Vector2? speedRange;

        /// <summary>Control how quickly the animation plays.</summary>
        [JsonProperty("fps")]
        public float fps;

        /// <summary>Specifies how many times the animation loops during the lifetime of the particle.</summary>
        [JsonProperty("cycleCount")]
        public int cycleCount;

        /// <summary>Choose which UV channels receive Texture animation.</summary>
        [JsonProperty("uvChannelMask")]
        public UVChannelFlags uvChannelMask;
    }
}
