// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoLayout
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The vgo layout.
    /// </summary>
    [Serializable]
    [JsonObject("vgo")]
    public class VgoLayout
    {
        /// <summary>List of nodes.</summary>
        /// <remarks>The root is included at the first of the node.</remarks>
        [JsonProperty("nodes")]
        public List<VgoNode> nodes = null;

        /// <summary>List of skins.</summary>
        [JsonProperty("skins")]
        public List<VgoSkin> skins = null;

        /// <summary>List of meshes.</summary>
        [JsonProperty("meshes")]
        public List<VgoMesh> meshes = null;

        /// <summary>List of materials.</summary>
        [JsonProperty("materials")]
        public List<VgoMaterial> materials = null;

        /// <summary>List of textures.</summary>
        [JsonProperty("textures")]
        public List<VgoTexture> textures = null;

        /// <summary>List of animation clips.</summary>
        [JsonProperty("animationClips")]
        public List<VgoAnimationClip> animationClips = null;

        /// <summary>List of particles.</summary>
        [JsonProperty("particles")]
        public List<VgoParticleSystem> particles = null;

        /// <summary>The spring bone info.</summary>
        [JsonProperty("springBoneInfo")]
        public VgoSpringBoneInfo springBoneInfo = null;

        /// <summary>Dictionary object with extension-specific objects.</summary>
        [JsonProperty("extensions")]
        public VgoExtensions extensions = null;
    }
}
