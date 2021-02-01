// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoAnimationCurveBinding
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// VGO Animation Curve Binding
    /// </summary>
    [Serializable]
    [JsonObject("animationCurveBinding")]
    public class VgoAnimationCurveBinding
    {
        ///// <summary>The transform path of the object that is animated.</summary>
        //[JsonProperty("path")]
        //public string path;

        /// <summary>The type of the property to be animated.</summary>
        [JsonProperty("type", Required = Required.Always)]
        public string type;

        /// <summary>The name of the property to be animated.</summary>
        [JsonProperty("propertyName", Required = Required.Always)]
        public string propertyName;

        /// <summary>The animation curve.</summary>
        [JsonProperty("animationCurve", Required = Required.Always)]
        public VgoAnimationCurve animationCurve;
    }
}