// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoSkybox
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;

    /// <summary>
    /// VGO Skybox
    /// </summary>
    [Serializable]
    [JsonObject("node.skybox")]
    public class VgoSkybox
    {
        /// <summary>Material Index</summary>
        [JsonProperty("materialIndex", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        public int materialIndex = -1;
    }
}