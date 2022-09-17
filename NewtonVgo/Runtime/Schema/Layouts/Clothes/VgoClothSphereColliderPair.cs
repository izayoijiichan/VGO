// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoClothSphereColliderPair
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;

    /// <summary>
    /// VGO Cloth Sphere Collider Pair
    /// </summary>
    [Serializable]
    public class VgoClothSphereColliderPair
    {
        /// <summary>The index of the first SphereCollider.</summary>
        [JsonProperty("first", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(-1)]
        public int first = -1;

        /// <summary>The index of the second SphereCollider.</summary>
        [JsonProperty("second", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(-1)]
        public int second = -1;
    }
}
