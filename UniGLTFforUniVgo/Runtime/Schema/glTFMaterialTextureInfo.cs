using Newtonsoft.Json;
using System;

namespace UniGLTFforUniVgo
{
    /// <summary>
    /// Material Base Color Texture Info
    /// </summary>
    [Serializable]
    [JsonObject("material.baseColorTextureInfo")]
    public class glTFMaterialBaseColorTextureInfo : glTFTextureInfo
    {
        /// <summary></summary>
        public override glTFTextureTypes TextureType { get => glTFTextureTypes.BaseColor; }
    }

    /// <summary>
    /// Material Emissive Texture Info
    /// </summary>
    [Serializable]
    [JsonObject("material.emissiveTextureInfo")]
    public class glTFMaterialEmissiveTextureInfo : glTFTextureInfo
    {
        /// <summary></summary>
        public override glTFTextureTypes TextureType { get => glTFTextureTypes.Emissive; }
    }

    /// <summary>
    /// Material Metalic Roughness Texture Info
    /// </summary>
    [Serializable]
    [JsonObject("material.metalicRoughnessTextureInfo")]
    public class glTFMaterialMetallicRoughnessTextureInfo : glTFTextureInfo
    {
        /// <summary></summary>
        public override glTFTextureTypes TextureType { get => glTFTextureTypes.Metallic; }
    }

    /// <summary>
    /// Material Normal Texture Info
    /// </summary>
    [Serializable]
    [JsonObject("material.normalTextureInfo")]
    public class glTFMaterialNormalTextureInfo : glTFTextureInfo
    {
        /// <summary></summary>
        [JsonProperty("scale")]
        public float scale = 1.0f;

        /// <summary></summary>
        public override glTFTextureTypes TextureType { get => glTFTextureTypes.Normal; }
    }

    /// <summary>
    /// Material Occlusion Texture Info
    /// </summary>
    [Serializable]
    [JsonObject("material.occlusionTextureInfo")]
    public class glTFMaterialOcclusionTextureInfo : glTFTextureInfo
    {
        /// <summary></summary>
        [JsonProperty("strength")]
        //[JsonSchema(Minimum = 0.0, Maximum = 1.0)]
        public float strength = 1.0f;

        /// <summary></summary>
        public override glTFTextureTypes TextureType { get => glTFTextureTypes.Occlusion; }
    }
}
