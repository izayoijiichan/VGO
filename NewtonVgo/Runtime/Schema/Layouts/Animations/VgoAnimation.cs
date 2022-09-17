// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoAnimation
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;

    /// <summary>
    /// VGO Animation
    /// </summary>
    [Serializable]
    [JsonObject("animation")]
    public class VgoAnimation
    {
        /// <summary>The name of the object.</summary>
        [JsonProperty("name")]
        public string? name;

        /// <summary>Whether this component is enabled.</summary>
        [JsonProperty("enabled", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(true)]
        public bool enabled = false;

        /// <summary>The index of default animation clip.</summary>
        [JsonProperty("clipIndex", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(-1)]
        public int clipIndex = -1;

        /// <summary>AABB of this Animation animation component in local space.</summary>
        [JsonProperty("localBounds")]
        public Bounds? localBounds;

        /// <summary>Should the default animation clip (the Animation.clip property) automatically start playing on startup?</summary>
        [JsonProperty("playAutomatically")]
        public bool playAutomatically;

        /// <summary>When turned on, animations will be executed in the physics loop.</summary>
        /// <remarks>This is only useful in conjunction with kinematic rigidbodies.</remarks>
        [JsonProperty("animatePhysics", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(false)]
        public bool animatePhysics = false;

        /// <summary>Controls culling of this Animation component.</summary>
        [JsonProperty("cullingType", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(AnimationCullingType.AlwaysAnimate)]
        public AnimationCullingType cullingType = AnimationCullingType.AlwaysAnimate;

        /// <summary>The default wrap mode used in the animation state.</summary>
        [JsonProperty("wrapMode", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(WrapMode.Default)]
        public WrapMode wrapMode = WrapMode.Default;
    }
}