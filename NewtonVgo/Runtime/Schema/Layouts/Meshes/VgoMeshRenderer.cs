// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoMeshRenderer
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    /// A mesh renderer.
    /// </summary>
    /// <remarks>
    /// A node can contain one renderer.
    /// mesh renderer, skinned mesh renderer, particle renderer.
    /// This item was added in the spec 2.5 version.
    /// </remarks>
    [Serializable]
    [JsonObject("meshRenderer")]
    public class VgoMeshRenderer
    {
        /// <summary>The name of this renderer.</summary>
        [JsonProperty("name")]
        public string? name;

        /// <summary>Enabled</summary>
        [JsonProperty("enabled", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(true)]
        public bool enabled = false;

        /// <summary>The index of the mesh referenced by this node.</summary>
        [JsonProperty("mesh", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(-1)]
        public int mesh = -1;

        /// <summary>The index list of the material to apply to mesh.</summary>
        [JsonProperty("materials")]
        public List<int>? materials;

        /// <summary>The kind of the blend shape.</summary>
        [JsonProperty("blendShapeKind", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(VgoBlendShapeKind.None)]
        public VgoBlendShapeKind blendShapeKind;

        /// <summary>List of the blend shape preset.</summary>
        [JsonProperty("blendShapePesets")]
        public List<VgoMeshBlendShapePreset>? blendShapePesets;

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
