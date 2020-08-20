// ----------------------------------------------------------------------
// @Namespace : NewtonVgo.Serialization.JsonConverters
// @Class     : Matrix4x4JsonConverter
// ----------------------------------------------------------------------
namespace NewtonVgo.Serialization.JsonConverters
{
    using Newtonsoft.Json;
    using System;
    using System.Numerics;

    /// <summary>
    /// System Numerics Matrix4x4 Json Converter
    /// </summary>
    public class Matrix4x4JsonConverter : JsonConverter
    {
        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Matrix4x4);
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The Newtonsoft.Json.JsonReader to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Matrix4x4 value = default;

            float[] floatArray = serializer.Deserialize<float[]>(reader);

            if (floatArray != null)
            {
                if (floatArray.Length == 16)
                {
                    value = new Matrix4x4(
                        floatArray[0],
                        floatArray[1],
                        floatArray[2],
                        floatArray[3],
                        floatArray[4],
                        floatArray[5],
                        floatArray[6],
                        floatArray[7],
                        floatArray[8],
                        floatArray[9],
                        floatArray[10],
                        floatArray[11],
                        floatArray[12],
                        floatArray[13],
                        floatArray[14],
                        floatArray[15]
                    );
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
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Matrix4x4 m = (Matrix4x4)value;

            float[] floatArray = new float[]
            {
                m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44
            };

            serializer.Serialize(writer, floatArray);
        }
    }
}