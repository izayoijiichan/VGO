// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoSpringBoneInfo
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// VGO Spring Bone Info
    /// </summary>
    [Serializable]
    public class VgoSpringBoneInfo
    {
        /// <summary>List of spring bone groups.</summary>
        [JsonProperty("springBoneGroups")]
        public List<VgoSpringBoneGroup?>? springBoneGroups = null;

        /// <summary>List of spring bone collider groups.</summary>
        [JsonProperty("colliderGroups")]
        public List<VgoSpringBoneColliderGroup?>? colliderGroups = null;
    }
}