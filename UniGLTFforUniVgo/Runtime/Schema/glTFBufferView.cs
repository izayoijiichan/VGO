using Newtonsoft.Json;
using System;

namespace UniGLTFforUniVgo
{
    /// <summary>
    /// A view into a buffer generally representing a subset of the buffer.
    /// </summary>
    [Serializable]
    [JsonObject("bufferView")]
    public class glTFBufferView
    {
        /// <summary>The index of the buffer.</summary>
        [JsonProperty("buffer", Required = Required.Always)]
        //[JsonSchema(Required = true, Minimum = 0)]
        public int buffer;

        /// <summary>The offset into the buffer in bytes.</summary>
        [JsonProperty("byteOffset")]
        //[JsonSchema(Minimum = 0)]
        public int byteOffset;

        /// <summary>The total byte length of the buffer view.</summary>
        [JsonProperty("byteLength", Required = Required.Always)]
        //[JsonSchema(Required = true, Minimum = 1)]
        public int byteLength;

        /// <summary>The stride, in bytes.</summary>
        [JsonProperty("byteStride")]
        //[JsonSchema(Minimum = 4, Maximum = 252, MultipleOf = 4)]
        public int byteStride;

        /// <summary>The target that the GPU buffer should be bound to.</summary>
        [JsonProperty("target")]
        //[JsonSchema(EnumSerializationType = EnumSerializationType.AsInt, EnumExcludes = new object[] { glBufferTarget.NONE })]
        public glBufferTarget target;

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
