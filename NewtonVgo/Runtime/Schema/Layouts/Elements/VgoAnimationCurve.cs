// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoAnimationCurve
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// VGO AnimationCurve
    /// </summary>
    [Serializable]
    [JsonObject("animationCurve")]
    public class VgoAnimationCurve
    {
        /// <summary>All keys defined in the animation curve.</summary>
        [JsonProperty("keys")]
        public VgoKeyframe[] keys = null;

        /// <summary>The behaviour of the animation before the first keyframe.</summary>
        [JsonProperty("preWrapMode")]
        public WrapMode preWrapMode;

        /// <summary>The behaviour of the animation after the last keyframe.</summary>
        [JsonProperty("postWrapMode")]
        public WrapMode postWrapMode;
    }
}
