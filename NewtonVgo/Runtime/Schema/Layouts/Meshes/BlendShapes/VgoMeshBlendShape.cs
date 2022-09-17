// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoMeshBlendShape
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;

    /// <summary>
    /// A mesh blend shape.
    /// </summary>
    [Serializable]
    [JsonObject("mesh.blendShape")]
    public class VgoMeshBlendShape
    {
        /// <summary>The name of the blend shape.</summary>
        [JsonProperty("name", Required = Required.Always)]
        public string? name;

        /// <summary>A dictionary mapping attributes.</summary>
        /// <remarks>Supported only `POSITION`, `NORMAL`, `TANGENT`.</remarks>
        [JsonProperty("attributes", Required = Required.Always)]
        public VgoMeshPrimitiveAttributes? attributes;

        ///// <summary></summary>
        //[JsonProperty("weight", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[DefaultValue(0.0f)]
        //public float weight;

        /// <summary>The type of face parts.</summary>
        [JsonProperty("facePartsType", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(VgoBlendShapeFacePartsType.None)]
        public VgoBlendShapeFacePartsType facePartsType;

        /// <summary>The type of blink.</summary>
        [JsonProperty("blinkType", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(VgoBlendShapeBlinkType.None)]
        public VgoBlendShapeBlinkType blinkType;

        /// <summary>The type of viseme.</summary>
        /// <remarks>None value is -1.</remarks>
        [JsonProperty("visemeType", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(VgoBlendShapeVisemeType.None)]
        public VgoBlendShapeVisemeType visemeType = VgoBlendShapeVisemeType.None;
    }
}
