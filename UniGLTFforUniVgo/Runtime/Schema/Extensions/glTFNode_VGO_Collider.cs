// ----------------------------------------------------------------------
// @Namespace : UniGLTFforUniVgo
// @Class     : glTFNode_VGO_Collider
// ----------------------------------------------------------------------
namespace UniGLTFforUniVgo
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System;
    using System.ComponentModel;

    /// <summary>Collider Type</summary>
    public enum ColliderType
    {
        /// <summary>Box Collier</summary>
        Box = 0,
        /// <summary>Capsule Collier</summary>
        Capsule = 1,
        /// <summary>Sphere Collier</summary>
        Sphere = 2,
    }

    /// <summary>
    /// glTF Node VGO Collider
    /// </summary>
    [Serializable]
    [JsonObject("node.vgo.collider")]
    public class glTFNode_VGO_Collider
    {
        /// <summary>Enabled</summary>
        [JsonProperty("enabled", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(true)]
        public bool enabled = false;

        /// <summary>Type</summary>
        [JsonProperty("type", Required = Required.Always)]
        public ColliderType type = default;

        /// <summary>Is Trigger</summary>
        [JsonProperty("isTrigger")]
        public bool isTrigger = false;

        /// <summary>Center</summary>
        [JsonProperty("center")]
        public float[] center = new float[] { 0.0f, 0.0f, 0.0f };

        /// <summary>Size</summary>
        [JsonProperty("size")]
        public float[] size = null;

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
        public glTFNode_VGO_PhysicMaterial physicMaterial = null;
    }
}