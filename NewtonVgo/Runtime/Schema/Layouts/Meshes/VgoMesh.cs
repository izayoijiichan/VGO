// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoMesh
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    /// A set of primitives to be rendered.
    /// </summary>
    /// <remarks>
    /// A node can contain one mesh.
    /// </remarks>
    [Serializable]
    [JsonObject("mesh")]
    public class VgoMesh
    {
        /// <summary>The name of this mesh.</summary>
        [JsonProperty("name")]
        public string name;

        /// <summary>A dictionary mapping attributes.</summary>
        [JsonProperty("attributes", Required = Required.Always)]
        public VgoMeshPrimitiveAttributes attributes;

        /// <summary>The index of the accessor that contains the sub-mesh indices.</summary>
        [JsonProperty("subMeshes")]
        public List<int> subMeshes;

        /// <summary>The index list of the material to apply to this primitive when rendering.</summary>
        /// <remarks>This property is used only in spec version between 2.0 and 2.4.</remarks>
        [JsonProperty("materials")]
        public List<int> materials;

        /// <summary>The kind of the blend shape.</summary>
        /// <remarks>This property is used only in spec version between 2.0 and 2.4.</remarks>
        [JsonProperty("blendShapeKind", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(VgoBlendShapeKind.None)]
        public VgoBlendShapeKind blendShapeKind;

        /// <summary>List of the blend shape.</summary>
        [JsonProperty("blendShapes")]
        public List<VgoMeshBlendShape> blendShapes;

        /// <summary>List of the blend shape preset.</summary>
        /// <remarks>This property is used only in spec version between 2.0 and 2.4.</remarks>
        [JsonProperty("blendShapePesets")]
        public List<VgoMeshBlendShapePreset> blendShapePesets;

        /// <summary>Whether this primitive has a vertex color.</summary>
        [JsonIgnore]
        public bool HasVertexColor => (attributes != null) && (attributes.COLOR_0 != -1);

        /// <summary>
        /// Create a new instance of VgoMesh.
        /// </summary>
        public VgoMesh()
        {
        }

        /// <summary>
        /// Create a new instance of VgoMesh with name.
        /// </summary>
        /// <param name="name"></param>
        public VgoMesh(string name)
        {
            this.name = name;
        }

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
