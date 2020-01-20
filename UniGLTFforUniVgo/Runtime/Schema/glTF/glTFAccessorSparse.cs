using Newtonsoft.Json;
using System;

namespace UniGLTFforUniVgo
{
    /// <summary>
    /// Sparse storage of attributes that deviate from their initialization value.
    /// </summary>
    [Serializable]
    [JsonObject("accessor.sparse")]
    public class glTFAccessorSparse
    {
        /// <summary>Number of entries stored in the sparse array.</summary>
        [JsonProperty("count", Required = Required.Always)]
        //[JsonSchema(Required = true, Minimum = 1)]
        public int count;

        /// <summary>Index array of size `count` that points to those accessor attributes that deviate from their initialization value. Indices must strictly increase.</summary>
        [JsonProperty("indices", Required = Required.Always)]
        //[JsonSchema(Required = true)]
        public glTFAccessorSparseIndices indices;

        /// <summary>Array of size `count` times number of components, storing the displaced accessor attributes pointed by `indices`. Substituted values must have the same `componentType` and number of components as the base accessor.</summary>
        [JsonProperty("values", Required = Required.Always)]
        //[JsonSchema(Required = true)]
        public glTFAccessorSparseValues values;

        /// <summary></summary>
        [JsonProperty("extensions")]
        public object extensions;

        /// <summary></summary>
        [JsonProperty("extras")]
        public object extras;
    }
}
