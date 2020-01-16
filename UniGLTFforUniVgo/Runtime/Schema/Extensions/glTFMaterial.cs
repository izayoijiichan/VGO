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
    public partial class glTFMaterial_extensions : ExtensionsBase<glTFMaterial_extensions>
    {
        /// <summary></summary>
        [JsonProperty("KHR_materials_unlit")]
        public glTF_KHR_materials_unlit KHR_materials_unlit;

        /// <summary></summary>
        [JsonProperty("VGO_materials")]
        public glTF_VGO_materials VGO_materials;

        /// <summary></summary>
        [JsonProperty("VRMC_materials_mtoon")]
        public glTF_VRMC_materials_mtoon VRMC_materials_mtoon;
    }
}
