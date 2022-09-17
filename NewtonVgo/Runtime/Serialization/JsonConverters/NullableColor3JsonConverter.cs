// ----------------------------------------------------------------------
// @Namespace : NewtonVgo.Serialization.JsonConverters
// @Class     : NullableColor3JsonConverter
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo.Serialization.JsonConverters
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// Nullable Color3 Json Converter
    /// </summary>
    public class NullableColor3JsonConverter : JsonConverter
    {
        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Color3?);
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
            Color3? value = default;

            float[]? floatArray = serializer.Deserialize<float[]>(reader);

            if (floatArray != null)
            {
                if (floatArray.Length == 3)
                {
                    value = new Color3(floatArray[0], floatArray[1], floatArray[2]);
                }
                else if (floatArray.Length == 4)
                {
                    // @notice
                    value = new Color3(floatArray[0], floatArray[1], floatArray[2]);
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
            Color3? color = (Color3?)value;

            if (color.HasValue)
            {
                float[] floatArray = new float[] { color.Value.R, color.Value.G, color.Value.B };

                serializer.Serialize(writer, floatArray);
            }
        }
    }
}