// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoBlendShapePreset
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A blend shape preset.
    /// </summary>
    [Serializable]
    [JsonObject("mesh.blendshape.preset")]
    public class VgoMeshBlendShapePreset
    {
        /// <summary>The name of preset.</summary>
        [JsonProperty("name")]
        public string? name;

        /// <summary>The type of preset.</summary>
        [JsonProperty("type", Required = Required.Always)]
        public VgoBlendShapePresetType type;

        /// <summary>List of binding.</summary>
        [JsonProperty("bindings")]
        public List<VgoMeshBlendShapeBinding> bindings = new List<VgoMeshBlendShapeBinding>();
    }
}
