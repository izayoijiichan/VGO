// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoAnimationClip
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    /// VGO Animation Clip
    /// </summary>
    [Serializable]
    [JsonObject("animationClip")]
    public class VgoAnimationClip
    {
        /// <summary>The name of the object.</summary>
        [JsonProperty("name", Required = Required.Always)]
        public string name;

        ///// <summary>Frame rate at which keyframes are sampled. (Read Only)</summary>
        //[JsonProperty("frameRate")]
        //public float frameRate;

        /// <summary>Set to true if the AnimationClip will be used with the Legacy Animation component (instead of the Animator).</summary>
        [JsonProperty("legacy", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(true)]
        public bool legacy = true;

        /// <summary>AABB of this Animation Clip in local space of Animation component that it is attached too.</summary>
        [JsonProperty("localBounds")]
        public Bounds? localBounds;

        /// <summary>The default wrap mode used in the animation state.</summary>
        [JsonProperty("wrapMode", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(WrapMode.Default)]
        public WrapMode wrapMode = WrapMode.Default;

        /// <summary>Defines how a curve is attached to this animation clip.</summary>
        [JsonProperty("curveBindings")]
        public List<VgoAnimationCurveBinding> curveBindings = new List<VgoAnimationCurveBinding>();
    }
}