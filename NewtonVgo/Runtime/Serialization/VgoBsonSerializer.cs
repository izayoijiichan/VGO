// ----------------------------------------------------------------------
// @Namespace : NewtonVgo.Serialization
// @Class     : VgoBsonSerializer
// ----------------------------------------------------------------------
#pragma warning disable 618 
namespace NewtonVgo.Serialization
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Bson;
    using NewtonVgo.Serialization.JsonConverters;
    using System;
    using System.IO;

    /// <summary>
    /// BSON Serializer
    /// </summary>
    public class VgoBsonSerializer
    {
        #region Fields

        /// <summary>The serializer.</summary>
        protected JsonSerializer _Serializer;

        #endregion

        #region Properties

        /// <summary>The serializer.</summary>
        protected JsonSerializer Serializer
        {
            get
            {
                if (_Serializer == null)
                {
                    _Serializer = CreateBsonSerializer();
                }
                return _Serializer;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Create a new serializer for BSON.
        /// </summary>
        /// <returns></returns>
        protected virtual JsonSerializer CreateBsonSerializer()
        {
            JsonSerializer serializer = new JsonSerializer
            {
                ContractResolver = new VgoContractResolver(),
            };

            serializer.Converters.Add(new Color3JsonConverter());
            serializer.Converters.Add(new Color4JsonConverter());
            serializer.Converters.Add(new Matrix4x4JsonConverter());
            serializer.Converters.Add(new QuaternionJsonConverter());
            serializer.Converters.Add(new Vector2JsonConverter());
            serializer.Converters.Add(new Vector3JsonConverter());
            serializer.Converters.Add(new Vector4JsonConverter());

            serializer.Converters.Add(new NullableColor3JsonConverter());
            serializer.Converters.Add(new NullableColor4JsonConverter());
            serializer.Converters.Add(new NullableMatrix4x4JsonConverter());
            serializer.Converters.Add(new NullableQuaternionJsonConverter());
            serializer.Converters.Add(new NullableVector2JsonConverter());
            serializer.Converters.Add(new NullableVector3JsonConverter());
            serializer.Converters.Add(new NullableVector4JsonConverter());

            return serializer;
        }

        /// <summary>
        /// Serialize a object to BSON.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns>The BSON.</returns>
        public byte[] SerializeObject<T>(T value)
        {
            try
            {
                using (MemoryStream meworySterem = new MemoryStream())
                using (BsonWriter bsonWriter = new BsonWriter(meworySterem))
                {
                    Serializer.Serialize(bsonWriter, value);

                    return meworySterem.ToArray();
                }
            }
            catch (JsonSerializationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Deserialize BSON to a object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bson">The BSON.</param>
        /// <param name="rootValueAsArray">Specify true if the root value is an array.</param>
        /// <returns>A object.</returns>
        public T DeserializeObject<T>(byte[] bson, bool rootValueAsArray = false)
        {
            try
            {
                using (MemoryStream memoryStream = new MemoryStream(bson))
                using (BsonReader bsonReader = new BsonReader(memoryStream))
                {
                    bsonReader.ReadRootValueAsArray = rootValueAsArray;

                    return Serializer.Deserialize<T>(bsonReader);
                }
            }
            catch (JsonSerializationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
#pragma warning restore 618