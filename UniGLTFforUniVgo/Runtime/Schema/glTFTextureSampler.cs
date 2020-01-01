using Newtonsoft.Json;
using System;

namespace UniGLTFforUniVgo
{
    /// <summary>
    /// Texture sampler properties for filtering and wrapping modes.
    /// </summary>
    [Serializable]
    [JsonObject("sampler")]
    public class glTFTextureSampler
    {
        /// <summary></summary>
        [JsonProperty("magFilter")]
        //[JsonSchema(EnumSerializationType = EnumSerializationType.AsInt,
        //    EnumExcludes = new object[] {
        //        glFilter.NONE,
        //        glFilter.NEAREST_MIPMAP_NEAREST,
        //        glFilter.LINEAR_MIPMAP_NEAREST,
        //        glFilter.NEAREST_MIPMAP_LINEAR,
        //        glFilter.LINEAR_MIPMAP_LINEAR,
        //    })]
        public glFilter magFilter = glFilter.NEAREST;

        /// <summary></summary>
        [JsonProperty("minFilter")]
        //[JsonSchema(EnumSerializationType = EnumSerializationType.AsInt,
        //    EnumExcludes = new object[] { glFilter.NONE })]
        public glFilter minFilter = glFilter.NEAREST;

        /// <summary></summary>
        [JsonProperty("wrapS")]
        //[JsonSchema(EnumSerializationType = EnumSerializationType.AsInt,
        //    EnumExcludes = new object[] { glWrap.NONE })]
        public glWrap wrapS = glWrap.REPEAT;

        /// <summary></summary>
        [JsonProperty("wrapT")]
        //[JsonSchema(EnumSerializationType = EnumSerializationType.AsInt,
        //    EnumExcludes = new object[] { glWrap.NONE })]
        public glWrap wrapT = glWrap.REPEAT;

        /// <summary></summary>
        [JsonProperty("name")]
        public string name;

        /// <summary></summary>
        [JsonProperty("extensions")]
        public object extensions;

        /// <summary></summary>
        [JsonProperty("extras")]
        public object extras;
    }
}
