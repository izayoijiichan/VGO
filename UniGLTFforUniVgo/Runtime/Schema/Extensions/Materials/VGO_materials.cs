// ----------------------------------------------------------------------
// @Namespace : UniGLTFforUniVgo
// @Class     : VGO_materials
// ----------------------------------------------------------------------
namespace UniGLTFforUniVgo
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// VGO materials
    /// </summary>
    [Serializable]
    [JsonObject("material.extensions.VGO_materials")]
    public class VGO_materials
    {
        /// <summary>Shader Name</summary>
        [JsonProperty("shaderName", Required = Required.Always)]
        public string shaderName = null;

        [JsonIgnore]
        public static string ExtensionName => "VGO_materials";

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public VGO_materials()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shaderName"></param>
        public VGO_materials(string shaderName)
        {
            this.shaderName = shaderName;
        }

        #endregion
    }
}
