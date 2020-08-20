// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoCollider
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System;
    using System.ComponentModel;
    using System.Numerics;

    /// <summary>
    /// VGO Collider
    /// </summary>
    [Serializable]
    [JsonObject("node.collider")]
    public class VgoCollider
    {
        /// <summary>Enabled</summary>
        [JsonProperty("enabled", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(true)]
        public bool enabled = false;

        /// <summary>Type</summary>
        [JsonProperty("type", Required = Required.Always)]
        public VgoColliderType type = default;

        /// <summary>Is Trigger</summary>
        [JsonProperty("isTrigger", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(false)]
        public bool isTrigger = false;

        /// <summary>Center</summary>
        [JsonProperty("center")]
        public Vector3? center = null;

        /// <summary>Size</summary>
        [JsonProperty("size")]
        public Vector3? size = null;

        /// <summary>Radius</summary>
        [JsonProperty("radius", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float radius = -1.0f;

        /// <summary>Height</summary>
        [JsonProperty("height", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float height = -1.0f;

        /// <summary>Direction</summary>
        [JsonProperty("direction", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        public int direction = -1;

        /// <summary>Physic Material</summary>
        [JsonProperty("physicMaterial")]
        public VgoPhysicMaterial physicMaterial = null;
    }
}