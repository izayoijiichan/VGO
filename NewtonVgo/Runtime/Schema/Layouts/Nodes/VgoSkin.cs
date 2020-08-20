// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoSkin
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Joints and matrices defining a skin.
    /// </summary>
    [Serializable]
    [JsonObject("skin")]
    public class VgoSkin
    {
        /// <summary>The index of the accessor containing the floating-point 4x4 inverse-bind matrices.</summary>
        /// <remarks>The default is that each matrix is a 4x4 identity matrix, which implies that inverse-bind matrices were pre-applied.</remarks>
        [JsonProperty("inverseBindMatrices", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(-1)]
        public int inverseBindMatrices = -1;

        /// <summary>The index of the node used as a skeleton root.</summary>
        /// <remarks>The node must be the closest common root of the joints hierarchy or a direct or indirect parent node of the closest common root.</remarks>
        [JsonProperty("skeleton", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(-1)]
        public int skeleton = -1;

        /// <summary>Indices of skeleton nodes, used as joints in this skin.</summary>
        /// <remarks>The array length must be the same as the `count` property of the `inverseBindMatrices` accessor (when defined).</remarks>
        [JsonProperty("joints", Required = Required.Always)]
        public int[] joints;
    }
}
