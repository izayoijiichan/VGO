// ----------------------------------------------------------------------
// @Namespace : NewtonVgo.Serialization.JsonConverters
// @Class     : Vector3JsonConverter
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo.Serialization.JsonConverters
{
    using Newtonsoft.Json;
    using System;
    using System.Numerics;

    /// <summary>
    /// System Numerics Vector3 Json Converter
    /// </summary>
    public class Vector3JsonConverter : JsonConverter
    {
        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Vector3);
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The Newtonsoft.Json.JsonReader to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            Vector3 value = default;

            float[]? floatArray = serializer.Deserialize<float[]>(reader);

            if (floatArray != null)
            {
                if (floatArray.Length == 3)
                {
                    value = new Vector3(floatArray[0], floatArray[1], floatArray[2]);
                }
            }

            return value;
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The Newtonsoft.Json.JsonWriter to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value is Vector3 vector3)
            {
                float[] floatArray = new float[] { vector3.X, vector3.Y, vector3.Z };

                serializer.Serialize(writer, floatArray);
            }
        }
    }
}