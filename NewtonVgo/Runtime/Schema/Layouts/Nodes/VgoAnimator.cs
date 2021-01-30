// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoAnimator
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;

    /// <summary>
    /// VGO Animator
    /// </summary>
    [Serializable]
    [JsonObject("node.animator")]
    public class VgoAnimator
    {
        /// <summary>The name of the object.</summary>
        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string name;

        /// <summary>Enabled</summary>
        [JsonProperty("enabled", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(true)]
        public bool enabled = false;

        ///// <summary>The runtime representation of AnimatorController that controls the Animator.</summary>
        //[JsonProperty("controller")]
        //public VgoRuntimeAnimatorController controller;

        /// <summary>The current human avatar.</summary>
        [JsonProperty("humanAvatar")]
        public VgoHumanAvatar humanAvatar = null;

        /// <summary>Should root motion be applied?</summary>
        [JsonProperty("applyRootMotion", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(false)]
        public bool applyRootMotion;

        /// <summary>Specifies the update mode of the Animator.</summary>
        [JsonProperty("updateMode", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(AnimatorUpdateMode.Normal)]
        public AnimatorUpdateMode updateMode;

        /// <summary>Controls culling of this Animator component.</summary>
        [JsonProperty("cullingMode", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(AnimatorCullingMode.AlwaysAnimate)]
        public AnimatorCullingMode cullingMode;
    }
}