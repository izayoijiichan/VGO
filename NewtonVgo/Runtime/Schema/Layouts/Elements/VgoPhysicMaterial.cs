// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoPhysicMaterial
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System;

    /// <summary>
    /// VGO Physic Material
    /// </summary>
    [Serializable]
    [JsonObject("physicMaterial")]
    public class VgoPhysicMaterial
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