using Newtonsoft.Json;
using System;

namespace UniGLTFforUniVgo
{
    /// <summary>
    /// Array of size `accessor.sparse.count` times number of components storing the displaced accessor attributes pointed by `accessor.sparse.indices`.
    /// </summary>
    [Serializable]
    [JsonObject("accessor.sparse.values")]
    public class glTFAccessorSparseValues
    {
        /// <summary>The index of the bufferView with sparse values. Referenced bufferView can't have ARRAY_BUFFER or ELEMENT_ARRAY_BUFFER target.</summary>
        [JsonProperty("bufferView", Required = Required.Always)]
        //[JsonSchema(Required = true, Minimum = 0)]
        public int bufferView = -1;

        /// <summary>The offset relative to the start of the bufferView in bytes. Must be aligned.</summary>
        [JsonProperty("byteOffset")]
        //[JsonSchema(Minimum = 0)]
        public int byteOffset;

        /// <summary></summary>
        [JsonProperty("extensions")]
        public object extensions;

        /// <summary></summary>
        [JsonProperty("extras")]
        public object extras;
    }
}
