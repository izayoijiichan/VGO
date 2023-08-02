// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoTexture
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;

    /// <summary>
    /// A texture.
    /// </summary>
    [Serializable]
    [JsonObject("texture")]
    public class VgoTexture
    {
        /// <summary>The instance ID of texture.</summary>
        [JsonIgnore]
        public int id = -1;

        /// <summary>The user-defined name of this object.</summary>
        [JsonProperty("name")]
        public string? name;

        /// <summary>The index of the accessor that contains the image.</summary>
        [JsonProperty("source", Required = Required.Always)]
        [DefaultValue(-1)]
        public int source = -1;

        /// <summary>Dimensionality type of the texture.</summary>
        [JsonProperty("dimensionType", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(TextureDimension.None)]
        public TextureDimension dimensionType;

        /// <summary>The texture map type.</summary>
        [JsonProperty("mapType", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(VgoTextureMapType.Default)]
        public VgoTextureMapType mapType;

        /// <summary>Image color space.</summary>
        [JsonProperty("colorSpace", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(VgoColorSpaceType.Srgb)]
        public VgoColorSpaceType colorSpace;

        /// <summary>The image's MIME type.</summary>
        [JsonProperty("mimeType")]
        public string? mimeType = null;

        /// <summary>Filtering mode of the texture.</summary>
        [JsonProperty("filterMode", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(FilterMode.Point)]
        public FilterMode filterMode = FilterMode.Point;

        /// <summary>Texture coordinate wrapping mode.</summary>
        [JsonProperty("wrapMode", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(TextureWrapMode.Repeat)]
        public TextureWrapMode wrapMode = TextureWrapMode.Repeat;

        /// <summary>Texture U coordinate wrapping mode.</summary>
        [JsonProperty("wrapModeU", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(TextureWrapMode.Repeat)]
        public TextureWrapMode wrapModeU = TextureWrapMode.Repeat;

        /// <summary>Texture V coordinate wrapping mode.</summary>
        [JsonProperty("wrapModeV", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(TextureWrapMode.Repeat)]
        public TextureWrapMode wrapModeV = TextureWrapMode.Repeat;

        /// <summary>The metallic-roughness of the material.</summary>
        [JsonProperty("metallicRoughness", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(-1.0f)]
        public float metallicRoughness = -1.0f;

        /// <summary>Dictionary object with extension-specific objects.</summary>
        [JsonProperty("extensions")]
        public VgoExtensions? extensions = null;

        /// <summary>Whether the color space is linear.</summary>
        [JsonIgnore]
        public bool IsLinear => (colorSpace == VgoColorSpaceType.Linear);

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
