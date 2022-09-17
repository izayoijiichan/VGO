// ----------------------------------------------------------------------
// @Namespace : NewtonVgo.Serialization.JsonConverters
// @Class     : QuaternionJsonConverter
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo.Serialization.JsonConverters
{
    using Newtonsoft.Json;
    using System;
    using System.Numerics;

    /// <summary>
    /// System Numerics Quaternion Json Converter
    /// </summary>
    public class QuaternionJsonConverter : JsonConverter
    {
        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Quaternion);
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
            Quaternion value = default;

            float[]? floatArray = serializer.Deserialize<float[]>(reader);

            if (floatArray != null)
            {
                if (floatArray.Length == 4)
                {
                    value = new Quaternion(floatArray[0], floatArray[1], floatArray[2], floatArray[3]);
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
            if (value is Quaternion quaternion)
            {
                float[] floatArray = new float[] { quaternion.X, quaternion.Y, quaternion.Z, quaternion.W };

                serializer.Serialize(writer, floatArray);
            }
        }
    }
}