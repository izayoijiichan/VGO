// ----------------------------------------------------------------------
// @Namespace : UniGLTFforUniVgo
// @Class     : glTFNode_VGO_PhysicMaterial
// ----------------------------------------------------------------------
namespace UniGLTFforUniVgo
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System;
    using UnityEngine;

    /// <summary>
    /// glTF Node VGO Physic Material
    /// </summary>
    [Serializable]
    [JsonObject("node.vgo.physicMaterial")]
    public class glTFNode_VGO_PhysicMaterial
    {
        /// <summary></summary>
        [JsonProperty("dynamicFriction")]
        public float dynamicFriction;

        /// <summary></summary>
        [JsonProperty("staticFriction")]
        public float staticFriction;

        /// <summary></summary>
        [JsonProperty("bounciness")]
        public float bounciness;

        /// <summary></summary>
        [JsonProperty("frictionCombine", Required = Required.Always)]
        public PhysicMaterialCombine frictionCombine;

        /// <summary></summary>
        [JsonProperty("bounceCombine", Required = Required.Always)]
        public PhysicMaterialCombine bounceCombine;
    }
}