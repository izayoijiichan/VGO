// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoCloth
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Numerics;

    /// <summary>
    /// VGO Cloth
    /// </summary>
    [Serializable]
    public class VgoCloth
    {
        /// <summary>The name of the object.</summary>
        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string name;

        /// <summary>Whether this component is enabled.</summary>
        [JsonProperty("enabled", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(true)]
        public bool enabled = false;

        /// <summary>Stretching stiffness of the cloth.</summary>
        [JsonProperty("stretchingStiffness", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(0.0f)]
        //[Range(0.0, 1.0)]
        public float stretchingStiffness;

        /// <summary>Bending stiffness of the cloth.</summary>
        [JsonProperty("bendingStiffness", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(0.0f)]
        //[Range(0.0, 1.0)]
        public float bendingStiffness;

        /// <summary>Use Tether Anchors.</summary>
        [JsonProperty("useTethers", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(true)]
        public bool useTethers;

        /// <summary>Should gravity affect the cloth simulation?</summary>
        [JsonProperty("useGravity", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(true)]
        public bool useGravity;

        /// <summary>Damp cloth motion.</summary>
        [JsonProperty("damping", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(0.0f)]
        //[Range(0.0, 1.0)]
        public float damping;

        /// <summary>A constant, external acceleration applied to the cloth.</summary>
        [JsonProperty("externalAcceleration")]
        public Vector3? externalAcceleration;

        /// <summary>A random, external acceleration applied to the cloth.</summary>
        [JsonProperty("randomAcceleration")]
        public Vector3? randomAcceleration;

        /// <summary>How much world-space movement of the character will affect cloth vertices.</summary>
        [JsonProperty("worldVelocityScale", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(0.0f)]
        public float worldVelocityScale;

        /// <summary>How much world-space acceleration of the character will affect cloth vertices.</summary>
        [JsonProperty("worldAccelerationScale", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(0.0f)]
        public float worldAccelerationScale;

        /// <summary>The friction of the cloth when colliding with the character.</summary>
        [JsonProperty("friction", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(0.0f)]
        //[Range(0.0, 1.0)]
        public float friction;

        /// <summary>How much to increase mass of colliding particles.</summary>
        [JsonProperty("collisionMassScale", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(0.0f)]
        //[Range(0.0, float.MaxValue)]
        public float collisionMassScale;

        /// <summary>Enable continuous collision to improve collision stability.</summary>
        [JsonProperty("enableContinuousCollision", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(true)]
        public bool enableContinuousCollision;

        /// <summary>Add one virtual particle per triangle to improve collision stability.</summary>
        [JsonProperty("useVirtualParticles", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(0.0f)]
        public float useVirtualParticles;

        /// <summary>Number of cloth solver iterations per second.</summary>
        [JsonProperty("clothSolverFrequency", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(1.0f)]
        //[Range(1.0, float.MaxValue)]
        public float clothSolverFrequency;

        /// <summary>Sets the stiffness frequency parameter.</summary>
        [JsonProperty("stiffnessFrequency", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(0.0f)]
        public float stiffnessFrequency;

        /// <summary>Minimum distance at which two cloth particles repel each other.</summary>
        [JsonProperty("selfCollisionDistance", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(0.0f)]
        public float selfCollisionDistance;

        /// <summary>Self-collision stiffness defines how strong the separating impulse should be for colliding particles.</summary>
        [JsonProperty("selfCollisionStiffness", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(0.0f)]
        public float selfCollisionStiffness;

        /// <summary>Cloth's sleep threshold.</summary>
        [JsonProperty("sleepThreshold", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(0.0f)]
        //[Range(0.0, float.MaxValue)]
        public float sleepThreshold;

        /// <summary>An array of ClothSphereColliderPairs which this Cloth instance should collide with.</summary>
        [JsonProperty("sphereColliders")]
        public List<VgoClothSphereColliderPair> sphereColliders;

        /// <summary>An array of CapsuleColliders which this Cloth instance should collide with.</summary>
        [JsonProperty("capsuleColliders")]
        public List<int> capsuleColliders;

        /// <summary>The resource accessor index of the cloth skinning coefficients used to set up how the cloth interacts with the skinned mesh.</summary>
        [JsonProperty("coefficients", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(-1)]
        public int coefficients = -1;

        ///// <summary>The cloth skinning coefficients used to set up how the cloth interacts with the skinned mesh.</summary>
        //[JsonProperty("coefficients")]
        //public List<VgoClothSkinningCoefficient> coefficients;

        ///// <summary>The current normals of the cloth object.</summary>
        //[JsonProperty("normals")]
        //public List<Vector3> normals;

        ///// <summary>The current vertex positions of the cloth object.</summary>
        //[JsonProperty("vertices")]
        //public List<Vector3> vertices;
    }
}