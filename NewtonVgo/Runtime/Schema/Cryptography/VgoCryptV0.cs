// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoCryptV0
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;
    using System.Security.Cryptography;

    /// <summary>
    /// VGO Cryptography version 0
    /// </summary>
    [Serializable]
    [JsonObject("crypt.v0")]
    public class VgoCryptV0
    {
        /// <summary></summary>
        /// <remarks>"AES", "Base64"</remarks>
        [JsonProperty("algorithms", Required = Required.Always)]
        public string algorithms;

        /// <summary>The base64 encoded key.</summary>
        [JsonProperty("key", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string key;

        /// <summary>The base64 encoded initialization vector.</summary>
        [JsonProperty("iv", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string iv;

        /// <summary>The mode for operation of the symmetric algorithm.</summary>
        [JsonProperty("cipherMode", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(0)]
        public CipherMode cipherMode;

        /// <summary>The padding mode used in the symmetric algorithm.</summary>
        [JsonProperty("paddingMode", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(0)]
        public PaddingMode paddingMode;
    }
}
