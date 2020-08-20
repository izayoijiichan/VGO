// ----------------------------------------------------------------------
// @Namespace : NewtonVgo.Serialization.JsonConverters
// @Class     : VgoExtensionsJsonConverter
// ----------------------------------------------------------------------
namespace NewtonVgo.Serialization.JsonConverters
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// VgoExtensions Json Converter
    /// </summary>
    public class VgoExtensionsJsonConverter : JsonConverter
    {
        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(VgoExtensions);
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
            Dictionary<string, JRaw> dictionary = serializer.Deserialize<Dictionary<string, JRaw>>(reader);

            if (dictionary == null)
            {
                return null;
            }

            VgoExtensions extensions = new VgoExtensions();

            extensions.SetConverterDictionary(dictionary);

            return extensions;
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The Newtonsoft.Json.JsonWriter to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                return;
            }

            VgoExtensions extensions = value as VgoExtensions;

            Dictionary<string, JRaw> dictionary = extensions.GetConverterDictionary();

            if (dictionary == null)
            {
                return;
            }

            serializer.Serialize(writer, dictionary);
        }
    }
}