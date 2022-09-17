// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoAssetInfo
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Metadata about the VGO asset.
    /// </summary>
    [Serializable]
    [JsonObject("assetInfo")]
    public class VgoAssetInfo
    {
        /// <summary>Information about the generator for this VGO model.</summary>
        [JsonProperty("generator")]
        public VgoGeneratorInfo? generator;

        /// <summary></summary>
        [JsonProperty("right")]
        public VgoRight? right;

        /// <summary>Dictionary object with extension-specific objects.</summary>
        [JsonProperty("extensions")]
        public VgoExtensions? extensions = null;

        /// <summary>Names of extensions used somewhere in this asset (include layout).</summary>
        [JsonProperty("extensionsUsed")]
        public List<string?>? extensionsUsed;
    }
}
