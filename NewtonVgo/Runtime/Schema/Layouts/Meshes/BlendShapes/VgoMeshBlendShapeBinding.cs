// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoBlendShapeBinding
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;

    /// <summary>
    /// A blend shape binding for preset.
    /// </summary>
    [Serializable]
    [JsonObject("mesh.blendshape.binding")]
    public class VgoMeshBlendShapeBinding
    {
        /// <summary>The index of the BlendShape.</summary>
        [JsonProperty("index", Required = Required.Always)]
        public int index;

        /// <summary>The weight for this BlendShape.</summary>
        [JsonProperty("weight", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(0)]
        //[Range(0.0f, 100.0f)]
        public float weight;
    }
}
