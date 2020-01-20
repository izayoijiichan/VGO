// ----------------------------------------------------------------------
// @Namespace : UniGLTFforUniVgo
// @Class     : glTFMaterial_extensions
// ----------------------------------------------------------------------
namespace UniGLTFforUniVgo
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// glTF Material extensions
    /// </summary>
    [Serializable]
    [JsonObject("material.extensions")]
    public class glTFMaterial_extensions
    {
        /// <summary></summary>
        [JsonProperty("VGO_materials")]
        public VGO_materials VGO_materials = null;

        /// <summary></summary>
        [JsonProperty("KHR_materials_unlit")]
        public KHR_materials_unlit KHR_materials_unlit = null;

        /// <summary></summary>
        [JsonProperty("VRMC_materials_mtoon")]
        public VRMC_materials_mtoon VRMC_materials_mtoon = null;
    }
}
