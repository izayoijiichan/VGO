// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoHumanBone
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// VGO Human Bone
    /// </summary>
    [Serializable]
    [JsonObject("humanBone")]
    public class VgoHumanBone
    {
        /// <summary>The human body bone.</summary>
        [JsonProperty("humanBodyBone", Required = Required.Always)]
        public HumanBodyBones humanBodyBone;

        /// <summary>The index of the node.</summary>
        [JsonProperty("nodeIndex", Required = Required.Always)]
        public int nodeIndex;
    }
}