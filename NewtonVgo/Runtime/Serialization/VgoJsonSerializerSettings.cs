// ----------------------------------------------------------------------
// @Namespace : NewtonVgo.Serialization
// @Class     : VgoJsonSerializerSettings
// ----------------------------------------------------------------------
namespace NewtonVgo.Serialization
{
    using Newtonsoft.Json;
    using NewtonVgo.Serialization.JsonConverters;

    /// <summary>
    /// VGO JSON Serializer Settings
    /// </summary>
    public class VgoJsonSerializerSettings : JsonSerializerSettings
    {
        #region Constructors

        /// <summary>
        /// Create a new instance of VgoJsonSerializerSettings.
        /// </summary>
        public VgoJsonSerializerSettings() : base()
        {
            ContractResolver = new VgoContractResolver();

            //Converters = new List<JsonConverter>();

            Converters.Add(new Color3JsonConverter());
            Converters.Add(new Color4JsonConverter());
            Converters.Add(new Matrix4x4JsonConverter());
            Converters.Add(new QuaternionJsonConverter());
            Converters.Add(new Vector2JsonConverter());
            Converters.Add(new Vector3JsonConverter());
            Converters.Add(new Vector4JsonConverter());

            Converters.Add(new NullableColor3JsonConverter());
            Converters.Add(new NullableColor4JsonConverter());
            Converters.Add(new NullableMatrix4x4JsonConverter());
            Converters.Add(new NullableQuaternionJsonConverter());
            Converters.Add(new NullableVector2JsonConverter());
            Converters.Add(new NullableVector3JsonConverter());
            Converters.Add(new NullableVector4JsonConverter());

            NullValueHandling = NullValueHandling.Ignore;
        }

        #endregion
    }
}
