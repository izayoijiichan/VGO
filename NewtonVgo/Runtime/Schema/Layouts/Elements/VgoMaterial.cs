// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoMaterial
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    /// The material appearance of a primitive.
    /// </summary>
    [Serializable]
    [JsonObject("material")]
    public class VgoMaterial
    {
        /// <summary>The user-defined name of this object.</summary>
        [JsonProperty("name")]
        public string? name;

        /// <summary>The shader name.</summary>
        [JsonProperty("shaderName")]
        public string? shaderName;

        /// <summary>The render queue.</summary>
        [JsonProperty("renderQueue", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(-1)]
        public int renderQueue = -1;

        /// <summary>Whether this material is unlit.</summary>
        [JsonProperty("isUnlit", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(false)]
        public bool isUnlit;

        /// <summary></summary>
        public Dictionary<string, int>? intProperties;

        /// <summary></summary>
        public Dictionary<string, float>? floatProperties;

        /// <summary></summary>
        public Dictionary<string, float[]>? colorProperties;

        /// <summary></summary>
        public Dictionary<string, float[]>? vectorProperties;

        /// <summary></summary>
        public Dictionary<string, float[]>? matrixProperties;

        /// <summary></summary>
        public Dictionary<string, float[]>? textureOffsetProperties;

        /// <summary></summary>
        public Dictionary<string, float[]>? textureScaleProperties;

        /// <summary></summary>
        public Dictionary<string, int>? textureIndexProperties;

        /// <summary></summary>
        public Dictionary<string, bool>? keywordMap;

        /// <summary></summary>
        public Dictionary<string, string>? tagMap;

        /// <summary>Dictionary object with extension-specific objects.</summary>
        [JsonProperty("extensions")]
        public VgoExtensions? extensions = null;

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return name ?? "{no name}";
        }
    }
}
