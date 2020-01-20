// ----------------------------------------------------------------------
// @Namespace : UniGLTFforUniVgo
// @Class     : KHR_materials_unlit
// ----------------------------------------------------------------------
namespace UniGLTFforUniVgo
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// KHR materials unlit
    /// </summary>
    [Serializable]
    [JsonObject("material.extensions.KHR_materials_unlit")]
    public class KHR_materials_unlit
    {
        [JsonIgnore]
        public static string ExtensionName => "KHR_materials_unlit";
    }
}
