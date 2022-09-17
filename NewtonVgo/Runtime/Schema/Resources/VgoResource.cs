// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoResource
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// The resource.
    /// </summary>
    [Serializable]
    [JsonObject("resource")]
    public class VgoResource
    {
        /// <summary>The uri of the resource.</summary>
        [JsonProperty("uri", Required = Required.Always)]
        public string? uri;

        /// <summary>The length of the resource in bytes.</summary>
        [JsonProperty("byteLength", Required = Required.Always)]
        public int byteLength;
    }
}
