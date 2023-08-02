// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoRigidbody
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System;
    using System.ComponentModel;

    /// <summary>
    /// VGO Rigidbody
    /// </summary>
    [Serializable]
    [JsonObject("node.rigidbody")]
    public class VgoRigidbody
    {
        /// <summary>Mass</summary>
        [JsonProperty("mass")]
        public float mass = 1.0f;

        /// <summary>Drag</summary>
        [JsonProperty("drag", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(0.0f)]
        public float drag = 0.0f;

        /// <summary>Angular Drag</summary>
        [JsonProperty("angularDrag", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(0.0f)]
        public float angularDrag = 0.0f;

        /// <summary>Use Gravity</summary>
        [JsonProperty("useGravity", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(true)]
        public bool useGravity;

        /// <summary>Is Kinematic</summary>
        [JsonProperty("isKinematic", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(false)]
        public bool isKinematic;

        /// <summary>Interpolation</summary>
        [JsonProperty("interpolation", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(RigidbodyInterpolation.None)]
        public RigidbodyInterpolation interpolation;

        /// <summary>Collision Detection Mode</summary>
        [JsonProperty("collisionDetectionMode", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(CollisionDetectionMode.Discrete)]
        public CollisionDetectionMode collisionDetectionMode;

        /// <summary>Constraints</summary>
        [JsonProperty("constraints")]
        [DefaultValue(0)]
        public RigidbodyConstraints constraints = 0;
    }
}