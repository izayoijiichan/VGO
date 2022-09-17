// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoNode
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    /// A node in the node hierarchy.
    /// </summary>
    [Serializable]
    [JsonObject("node")]
    public class VgoNode
    {
        /// <summary>The user-defined name of this object.</summary>
        [JsonProperty("name")]
        public string? name;

        /// <summary>Whether GameObject is root.</summary>
        [JsonProperty("isRoot", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(false)]
        public bool isRoot = false;

        /// <summary>Whether GameObject is active.</summary>
        [JsonProperty("isActive", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(true)]
        public bool isActive = false;

        /// <summary>Whether GameObject is static.</summary>
        [JsonProperty("isStatic", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(false)]
        public bool isStatic = false;

        /// <summary>Tag attached to GameObject.</summary>
        [JsonProperty("tag", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue("Untagged")]
        public string? tag = null;

        /// <summary>Index of the layer where GameObject is located.</summary>
        [JsonProperty("layer", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(0)]
        public int layer = 0;

        /// <summary>The animator.</summary>
        [JsonProperty("animator")]
        public VgoAnimator? animator = null;

        /// <summary>The animation.</summary>
        [JsonProperty("animation")]
        public VgoAnimation? animation = null;

        /// <summary>The rigidbody.</summary>
        [JsonProperty("rigidbody")]
        public VgoRigidbody? rigidbody = null;

        /// <summary>The indices of the collider referenced by this node.</summary>
        [JsonProperty("colliders")]
        public List<int>? colliders = null;

        /// <summary>The mesh renderer.</summary>
        /// <remarks>This propety was added in the spec 2.5 version.</remarks>
        [JsonProperty("meshRenderer")]
        public VgoMeshRenderer? meshRenderer = null;

        /// <summary>The index of the mesh referenced by this node.</summary>
        /// <remarks>This propety is used only in spec version between 2.0 and 2.4.</remarks>
        [JsonProperty("mesh", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(-1)]
        public int mesh = -1;

        /// <summary>The index of the skin referenced by this node.</summary>
        [JsonProperty("skin", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(-1)]
        public int skin = -1;

        /// <summary>The indices of the spring bone groups referenced by this node.</summary>
        [JsonProperty("springBoneGroups")]
        public List<int>? springBoneGroups = null;

        /// <summary>The index of the spring bone collider group referenced by this node.</summary>
        [JsonProperty("springBoneColliderGroup", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(-1)]
        public int springBoneColliderGroup = -1;

        /// <summary>The index of the cloth referenced by this node.</summary>
        [JsonProperty("cloth", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(-1)]
        public int cloth = -1;

        /// <summary>The index of the light referenced by this node.</summary>
        [JsonProperty("light", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(-1)]
        public int light = -1;

        /// <summary>The index of the particle referenced by this node.</summary>
        [JsonProperty("particle", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(-1)]
        public int particle = -1;

        /// <summary>The skybox.</summary>
        [JsonProperty("skybox")]
        public VgoSkybox? skybox = null;

        /// <summary>The right.</summary>
        [JsonProperty("right")]
        public VgoRight? right = null;

        /// <summary>The indices of this node's children.</summary>
        [JsonProperty("children")]
        public List<int>? children = null;

        /// <summary>Dictionary object with extension-specific objects.</summary>
        [JsonProperty("extensions")]
        public VgoExtensions? extensions = null;

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return name ?? "{no name}";
        }
    }
}
