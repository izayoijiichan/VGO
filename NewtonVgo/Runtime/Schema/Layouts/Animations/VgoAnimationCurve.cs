// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoAnimationCurve
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;

    /// <summary>
    /// VGO Animation Curve
    /// </summary>
    [Serializable]
    [JsonObject("animationCurve")]
    public class VgoAnimationCurve
    {
        /// <summary>All keys defined in the animation curve.</summary>
        [JsonProperty("keys")]
        public VgoKeyframe[] keys = null;

        /// <summary>The behaviour of the animation before the first keyframe.</summary>
        [JsonProperty("preWrapMode", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(WrapMode.Default)]
        public WrapMode preWrapMode = WrapMode.Default;

        /// <summary>The behaviour of the animation after the last keyframe.</summary>
        [JsonProperty("postWrapMode", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(WrapMode.Default)]
        public WrapMode postWrapMode = WrapMode.Default;
    }
}
