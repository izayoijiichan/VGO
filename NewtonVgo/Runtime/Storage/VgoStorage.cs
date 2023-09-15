// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoStorage
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using NewtonVgo.Buffers;
    using NewtonVgo.Serialization;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// VGO Storage
    /// </summary>
    public partial class VgoStorage : IVgoStorage
    {
        #region Constants

        /// <summary>The spec major version.</summary>
        public const byte SpecMajorVersion = 2;

        /// <summary>The spec minor version.</summary>
        public const byte SpecMinorVersion = 5;

        #endregion

        #region Fields

        /// <summary>The file header.</summary>
        protected VgoHeader Header = default;

        /// <summary>Vgo BSON Serializer.</summary>
        protected readonly VgoBsonSerializer _VgoBsonSerializer = new VgoBsonSerializer();

        /// <summary>Vgo JSON Serializer settings.</summary>
        protected readonly VgoJsonSerializerSettings _VgoJsonSerializerSettings = new VgoJsonSerializerSettings();

        #endregion

        #region Properties

        /// <summary>The type of the geometry coordinates.</summary>
        public VgoGeometryCoordinate GeometryCoordinate => Header.GeometryCoordinate;

        /// <summary>The type of the UV coordinates.</summary>
        public VgoUVCoordinate UVCoordinate => Header.UVCoordinate;

        /// <summary>The index map of chunks.</summary>
        protected VgoIndexChunkDataElement[]? ChunkIndexMap { get; set; }

        /// <summary>The asset info.</summary>
        public VgoAssetInfo? AssetInfo { get; set; }

        /// <summary>The layout.</summary>
        public VgoLayout Layout { get; protected set; }

        /// <summary>List of the resource accessor.</summary>
        public List<VgoResourceAccessor>? ResourceAccessors { get; protected set; }

        /// <summary>The resource.</summary>
        /// <remarks>for export.</remarks>
        protected IByteBuffer? Resource { get; set; }

        /// <summary>The segmented resource data.</summary>
        /// <remarks>for import.</remarks>
        protected VgoResourceDataCollection? SegmentResourceData { get; set; }

        /// <summary>Whether spec version is 2.4 or lower.</summary>
        public bool IsSpecVersion_2_4_orLower => (Header.MajorVersion == 2) && (Header.MinorVersion <= 4);

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of VgoStorage.
        /// </summary>
        /// <remarks>for Import</remarks>
        public VgoStorage()
        {
            Layout = new VgoLayout();
        }

        /// <summary>
        /// Create a new instance of VgoStorage with resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="geometryCoordinate">The type of the geometry coordinates.</param>
        /// <param name="uvCoordinate">The type of the UV coordinates.</param>
        /// <remarks>for Export</remarks>
        public VgoStorage(IByteBuffer resource, VgoGeometryCoordinate geometryCoordinate, VgoUVCoordinate uvCoordinate)
        {
            ThrowHelper.ThrowExceptionIfArgumentIsNull(nameof(resource), resource);

            Header.Magic = (uint)VgoChunkTypeID.Vgo;
            Header.DataLength = 8;
            Header.MajorVersion = SpecMajorVersion;
            Header.MinorVersion = SpecMinorVersion;
            Header.GeometryCoordinate = geometryCoordinate;
            Header.UVCoordinate = uvCoordinate;

            Layout = new VgoLayout();

            ResourceAccessors = new List<VgoResourceAccessor>();

            Resource = resource;
        }

        #endregion

        #region Protected Methods (JSON or BSON)

        /// <summary>
        /// Serialize a object to JSON or BSON.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="isBson">Specify true if the data is BSON.</param>
        /// <returns>The JSON or BSON.</returns>
        protected virtual byte[] SerializeObject<T>(T? value, bool isBson) where T : class
        {
            if (value == null)
            {
                ThrowHelper.ThrowArgumentNullException(nameof(value));
            }

            try
            {
                if (isBson)
                {
                    byte[] bson = _VgoBsonSerializer.SerializeObject(value);

                    return bson;
                }
                else
                {
                    string jsonString = JsonConvert.SerializeObject(value, _VgoJsonSerializerSettings);

                    byte[] json = Encoding.UTF8.GetBytes(jsonString);

                    return json;
                }
            }
            catch (JsonSerializationException)
            {
                throw;
            }
            catch
            {
                throw;
            }
        }

#if false
        /// <summary>
        /// Deserialize JSON or BSON to a object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonOrBson">The JSON or BSON.</param>
        /// <param name="isBson">Specify true if the data is BSON.</param>
        /// <param name="rootValueAsArray">Specify true if the root value is an array.</param>
        /// <returns>A object.</returns>
        protected virtual T? DeserializeObject<T>(byte[] jsonOrBson, bool isBson, bool rootValueAsArray = false) where T : class
        {
            try
            {
                T? data;

                if (isBson)
                {
                    data = _VgoBsonSerializer.DeserializeObject<T>(jsonOrBson, rootValueAsArray);
                }
                else
                {
                    string jsonString = Encoding.UTF8.GetString(jsonOrBson);

                    data = JsonConvert.DeserializeObject<T>(jsonString, _VgoJsonSerializerSettings);
                }

                return data;
            }
            catch (JsonSerializationException)
            {
                throw;
            }
            catch
            {
                throw;
            }
        }
#endif

        #endregion
    }
}
