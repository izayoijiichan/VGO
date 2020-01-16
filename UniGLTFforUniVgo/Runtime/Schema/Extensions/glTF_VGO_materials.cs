// ----------------------------------------------------------------------
// @Namespace : UniGLTFforUniVgo
// @Class     : glTF_VGO_materials
// ----------------------------------------------------------------------
namespace UniGLTFforUniVgo
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class glTF_VGO_materials
    {
        /// <summary>Shader Name</summary>
        [JsonProperty("shaderName", Required = Required.Always)]
        public string shaderName = null;

        [JsonIgnore]
        public static string ExtensionName => "VGO_materials";
    }
}
