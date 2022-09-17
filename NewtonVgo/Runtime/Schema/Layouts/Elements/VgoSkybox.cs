// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoSkybox
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;

    /// <summary>
    /// VGO Skybox
    /// </summary>
    [Serializable]
    [JsonObject("vgo.skybox")]
    public class VgoSkybox
    {
        /// <summary>Whether the skybox is enable.</summary>
        [JsonProperty("enabled", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(true)]
        public bool enabled = false;

        /// <summary>The index of the material.</summary>
        [JsonProperty("materialIndex", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        public int materialIndex = -1;
    }
}