using Newtonsoft.Json;
using System;

namespace UniGLTFforUniVgo
{
    /// <summary>
    /// A typed view into a bufferView.
    /// </summary>
    [Serializable]
    [JsonObject("accessor")]
    public class glTFAccessor
    {
        /// <summary>The index of the bufferView.</summary>
        [JsonProperty("bufferView", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        //[JsonSchema(Minimum = 0)]
        public int bufferView = -1;

        /// <summary>The offset relative to the start of the bufferView in bytes.</summary>
        [JsonProperty("byteOffset")]
        //[JsonSchema(Minimum = 0, Dependencies = new string[] { "bufferView" })]
        public int byteOffset;

        /// <summary>The datatype of components in the attribute.</summary>
        [JsonProperty("componentType", Required = Required.Always)]
        //[JsonSchema(Required = true, EnumSerializationType = EnumSerializationType.AsInt)]
        public glComponentType componentType;

        /// <summary>Specifies whether integer data values should be normalized.</summary>
        [JsonProperty("normalized")]
        public bool normalized;

        /// <summary>The number of attributes referenced by this accessor.</summary>
        [JsonProperty("count", Required = Required.Always)]
        //[JsonSchema(Required = true, Minimum = 1)]
        public int count;

        /// <summary>Specifies if the attribute is a scalar, vector, or matrix.</summary>
        [JsonProperty("type", Required = Required.Always)]
        //[JsonSchema(Required = true, EnumValues = new object[] { "SCALAR", "VEC2", "VEC3", "VEC4", "MAT2", "MAT3", "MAT4" }, EnumSerializationType = EnumSerializationType.AsString)]
        public string type;

        /// <summary>Maximum value of each component in this attribute.</summary>
        [JsonProperty("max")]
        //[JsonSchema(MinItems = 1, MaxItems = 16)]
        public float[] max;

        /// <summary>Minimum value of each component in this attribute.</summary>
        [JsonProperty("min")]
        //[JsonSchema(MinItems = 1, MaxItems = 16)]
        public float[] min;

        /// <summary>Sparse storage of attributes that deviate from their initialization value.</summary>
        [JsonProperty("sparse")]
        public glTFAccessorSparse sparse;

        /// <summary></summary>
        [JsonProperty("name")]
        public string name;

        /// <summary></summary>
        [JsonProperty("extensions")]
        public object extensions;

        /// <summary></summary>
        [JsonProperty("extras")]
        public object extras;

        public int TypeCount
        {
            get
            {
                switch (type)
                {
                    case "SCALAR":
                        return 1;
                    case "VEC2":
                        return 2;
                    case "VEC3":
                        return 3;
                    case "VEC4":
                    case "MAT2":
                        return 4;
                    case "MAT3":
                        return 9;
                    case "MAT4":
                        return 16;
                    default:
                        throw new NotImplementedException();
                }
            }
        }
    }
}
