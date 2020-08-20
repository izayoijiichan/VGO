// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoGeneratorInfo
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// VGO Generator Info
    /// </summary>
    [Serializable]
    [JsonObject("vgo.generatorInfo")]
    public class VgoGeneratorInfo
    {
        #region Fields

        /// <summary>Generator Name</summary>
        [JsonProperty("name", Required = Required.Always)]
        public string name = null;

        /// <summary>Generator Version</summary>
        [JsonProperty("version", Required = Required.Always)]
        public string version = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of VgoGeneratorInfo.
        /// </summary>
        public VgoGeneratorInfo() { }

        /// <summary>
        /// Create a new instance of VgoGeneratorInfo by specifying VgoGeneratorInfo.
        /// </summary>
        /// <param name="meta"></param>
        public VgoGeneratorInfo(VgoGeneratorInfo generatorInfo)
        {
            if (generatorInfo != null)
            {
                name = generatorInfo.name;
                version = generatorInfo.version;
            }
        }

        #endregion
    }
}