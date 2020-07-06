using Newtonsoft.Json;
using System;

namespace UniGLTFforUniVgo
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [JsonObject("material")]
    public class glTFMaterial
    {
        public string name;
        public glTFPbrMetallicRoughness pbrMetallicRoughness = new glTFPbrMetallicRoughness
        {
            baseColorFactor = new float[] { 1.0f, 1.0f, 1.0f, 1.0f },
        };
        public glTFMaterialNormalTextureInfo normalTexture = null;

        public glTFMaterialOcclusionTextureInfo occlusionTexture = null;

        public glTFMaterialEmissiveTextureInfo emissiveTexture = null;

        //[JsonSchema(MinItems = 3, MaxItems = 3)]
        //[ItemJsonSchema(Minimum = 0.0, Maximum = 1.0)]
        public float[] emissiveFactor;

        //[JsonSchema(EnumValues = new object[] { "OPAQUE", "MASK", "BLEND" }, EnumSerializationType = EnumSerializationType.AsUpperString)]
        public string alphaMode;

        //[JsonSchema(Dependencies = new string[] { "alphaMode" }, Minimum = 0.0)]
        public float alphaCutoff = 0.5f;

        public bool doubleSided;

        //[JsonSchema(SkipSchemaComparison = true)]
        [JsonProperty("extensions")]
        public glTFMaterial_extensions extensions;

        /// <summary></summary>
        [JsonProperty("extras")]
        public object extras;

        public glTFTextureInfo[] GetTextures()
        {
            return new glTFTextureInfo[]
            {
                pbrMetallicRoughness?.baseColorTexture,
                pbrMetallicRoughness?.metallicRoughnessTexture,
                normalTexture,
                occlusionTexture,
                emissiveTexture
            };
        }
    }
}
