// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoHumanAvatar
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// VGO Human Avatar
    /// </summary>
    [Serializable]
    [JsonObject("humanAvatar")]
    public class VgoHumanAvatar
    {
        /// <summary>The name of this human avatar.</summary>
        [JsonProperty("name")]
        public string? name;

        /// <summary>List of the human bone.</summary>
        [JsonProperty("humanBones", Required = Required.Always)]
        public List<VgoHumanBone?> humanBones = new List<VgoHumanBone?>();

        ///// <summary>List of the node index of the skeleton bone.</summary>
        //[JsonProperty("skeletonBones")]
        //public List<int> skeletonBones = new List<int>();
    }
}