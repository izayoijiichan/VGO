// ----------------------------------------------------------------------
// @Namespace : UniGLTFforUniVgo
// @Class     : glTF_VGO_Right
// ----------------------------------------------------------------------
namespace UniGLTFforUniVgo
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;

    /// <summary>
    /// glTF VGO Right
    /// </summary>
    [Serializable]
    [JsonObject("vgo.right")]
    public class glTF_VGO_Right
    {
        #region Fields

        /// <summary>Title</summary>
        [JsonProperty("title", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue("")]
        public string title = null;

        /// <summary>Author</summary>
        [JsonProperty("author", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue("")]
        public string author = null;

        /// <summary>Organization</summary>
        [JsonProperty("organization", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue("")]
        public string organization = null;

        /// <summary>Created Date</summary>
        [JsonProperty("createdDate", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue("")]
        public string createdDate = null;

        /// <summary>Updated Date</summary>
        [JsonProperty("updatedDate", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue("")]
        public string updatedDate = null;

        /// <summary>Version</summary>
        [JsonProperty("version", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue("")]
        public string version = null;

        /// <summary>Distribution URL</summary>
        [JsonProperty("distributionUrl", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue("")]
        public string distributionUrl = null;

        /// <summary>License URL</summary>
        [JsonProperty("licenseUrl", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue("")]
        public string licenseUrl = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of glTF_VGO_Right.
        /// </summary>
        public glTF_VGO_Right() { }

        /// <summary>
        /// Create a new instance of glTF_VGO_Right by specifying glTF_VGO_Right.
        /// </summary>
        public glTF_VGO_Right(glTF_VGO_Right right)
        {
            if (right != null)
            {
                title = right.title;
                author = right.author;
                organization = right.organization;
                createdDate = right.createdDate;
                updatedDate = right.updatedDate;
                version = right.version;
                distributionUrl = right.distributionUrl;
                licenseUrl = right.licenseUrl;
            }
        }

        #endregion
    }
}