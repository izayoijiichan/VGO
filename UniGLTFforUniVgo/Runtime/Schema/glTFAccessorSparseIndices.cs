using Newtonsoft.Json;
using System;

namespace UniGLTFforUniVgo
{
    /// <summary>
    /// Indices of those attributes that deviate from their initialization value.
    /// </summary>
    [Serializable]
    [JsonObject("accessor.sparse.indices")]
    public class glTFAccessorSparseIndices
    {
        /// <summary>The index of the bufferView with sparse indices. Referenced bufferView can't have ARRAY_BUFFER or ELEMENT_ARRAY_BUFFER target.</summary>
        [JsonProperty("bufferView", Required = Required.Always)]
        //[JsonSchema(Required = true, Minimum = 0)]
        public int bufferView = -1;

        /// <summary>The offset relative to the start of the bufferView in bytes. Must be aligned.</summary>
        [JsonProperty("byteOffset")]
        //[JsonSchema(Minimum = 0)]
        public int byteOffset;

        /// <summary>The indices data type.</summary>
        [JsonProperty("componentType", Required = Required.Always)]
        //[JsonSchema(Required = true, EnumValues = new object[] { 5121, 5123, 5125 })]
        public glComponentType componentType;

        /// <summary></summary>
        [JsonProperty("extensions")]
        public object extensions;

        /// <summary></summary>
        [JsonProperty("extras")]
        public object extras;
    }
}
