// ----------------------------------------------------------------------
// @Namespace : NewtonVgo.Serialization.JsonConverters
// @Class     : NullableColor4JsonConverter
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo.Serialization.JsonConverters
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// Nullable Color4 Json Converter
    /// </summary>
    public class NullableColor4JsonConverter : JsonConverter
    {
        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Color4?);
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
            Color4? value = default;

            float[]? floatArray = serializer.Deserialize<float[]>(reader);

            if (floatArray != null)
            {
                if (floatArray.Length == 4)
                {
                    value = new Color4(floatArray[0], floatArray[1], floatArray[2], floatArray[3]);
                }
                else if (floatArray.Length == 3)
                {
                    // @notice
                    value = new Color4(floatArray[0], floatArray[1], floatArray[2], 1.0f);
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
            Color4? color = (Color4?)value;

            if (color.HasValue)
            {
                float[] floatArray = new float[] { color.Value.R, color.Value.G, color.Value.B, color.Value.A };

                serializer.Serialize(writer, floatArray);
            }
        }
    }
}