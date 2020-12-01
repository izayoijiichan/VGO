// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoSpringBoneCollider
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;
    using System.Numerics;

    /// <summary>
    /// VGO Spring Bone Collider
    /// </summary>
    [Serializable]
    public class VgoSpringBoneCollider
    {
        /// <summary>The type of the spring bone collider.</summary>
        [JsonProperty("colliderType", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(VgoSpringBoneColliderType.Sphere)]
        public VgoSpringBoneColliderType colliderType;

        /// <summary>The offset position from the game object.</summary>
        [JsonProperty("offset")]
        public Vector3 offset;

        /// <summary>The radius of the collider.</summary>
        [JsonProperty("radius")]
        public float? radius;
    }
}