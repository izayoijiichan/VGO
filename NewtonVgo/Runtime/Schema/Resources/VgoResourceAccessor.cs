// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoResourceAccessor
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;

    /// <summary>
    /// An accessor of the resource.
    /// </summary>
    [Serializable]
    [JsonObject("resourceAccessor")]
    public class VgoResourceAccessor
    {
        /// <summary>The kind of the resource accessor.</summary>
        [JsonProperty("kind", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(VgoResourceAccessorKind.None)]
        public VgoResourceAccessorKind kind;

        /// <summary>The offset relative to the start of the resource in bytes.</summary>
        [JsonProperty("byteOffset", Required = Required.Always)]
        [DefaultValue(0)]
        public int byteOffset;

        /// <summary>The total byte length of this attribute.</summary>
        [JsonProperty("byteLength", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(0)]
        public int byteLength;

        /// <summary>The stride, in bytes.</summary>
        [JsonProperty("byteStride", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(0)]
        public int byteStride;

        /// <summary>The data type of this attribute.</summary>
        [JsonProperty("dataType", Required = Required.Always)]
        public VgoResourceAccessorDataType dataType;

        /// <summary>The number of attributes referenced by this accessor.</summary>
        /// <remarks>The number of attributes referenced by this accessor, not to be confused with the number of bytes or number of components.</remarks>
        [JsonProperty("count", Required = Required.Always)]
        public int count;

        // -- for sparse

        /// <summary>The type of the sparse.</summary>
        [JsonProperty("sparseType", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(VgoResourceAccessorSparseType.None)]
        public VgoResourceAccessorSparseType sparseType;

        /// <summary>Number of entries stored in the sparse array.</summary>
        /// <remarks>The number of attributes encoded in this sparse accessor.</remarks>
        [JsonProperty("sparseCount", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(0)]
        public int sparseCount;

        /// <summary>The indices data type.</summary>
        /// <remarks>Valid values correspond to UNSIGNED_BYTE, UNSIGNED_SHORT, UNSIGNED_INT.</remarks>
        [JsonProperty("sparseIndexDataType", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(VgoResourceAccessorDataType.None)]
        public VgoResourceAccessorDataType sparseIndexDataType;

        /// <summary>The values data type.</summary>
        [JsonProperty("sparseValueDataType", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(VgoResourceAccessorDataType.None)]
        public VgoResourceAccessorDataType sparseValueDataType;

        /// <summary>The relative offset for this accessor of sparse value.</summary>
        [JsonProperty("sparseValueByteOffset", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(0)]
        public int sparseValueOffset;

        // -- for sparse
    }
}
