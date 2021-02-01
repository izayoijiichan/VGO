// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoStorage
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using NewtonVgo.Buffers;
    using NewtonVgo.Security.Cryptography;
    using NewtonVgo.Serialization;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// VGO Storage
    /// </summary>
    public class VgoStorage
    {
        #region Constants

        /// <summary>The spec major version.</summary>
        public const byte SpecMajorVersion = 2;

        /// <summary>The spec minor version.</summary>
        public const byte SpecMinorVersion = 2;

        #endregion

        #region Fields

        /// <summary>The file header.</summary>
        protected VgoHeader Header = default;

        /// <summary>The index map of chunks.</summary>
        protected VgoIndexChunkDataElement[] ChunkIndexMap;

        #endregion

        #region Properties

        /// <summary>The type of the geometry coodinates.</summary>
        public VgoGeometryCoordinate GeometryCoordinate => Header.GeometryCoordinate;

        /// <summary>The type of the UV coodinates.</summary>
        public VgoUVCoordinate UVCoordinate => Header.UVCoordinate;

        /// <summary>The asset info.</summary>
        public VgoAssetInfo AssetInfo { get; set; }

        /// <summary>The layout.</summary>
        public VgoLayout Layout { get; protected set; }

        /// <summary>List of the resource accessor.</summary>
        public List<VgoResourceAccessor> ResourceAccessors { get; protected set; }

        /// <summary>The resource.</summary>
        public IByteBuffer Resource { get; protected set; }

        /// <summary>The directory path.</summary>
        public string DirectoryPath { get; protected set; }

        /// <summary>The timeout seconds of http request.</summary>
        public int HttpTimeoutSeconds { get; set; } = 30;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of VgoStorage with filePath.
        /// </summary>
        /// <param name="vgoFilePath">The file path of the vgo.</param>
        /// <param name="vgkFilePath">The file path of the crypt key.</param>
        /// <remarks>for Import</remarks>
        public VgoStorage(string vgoFilePath, string vgkFilePath = null)
        {
            if (vgoFilePath == null)
            {
                throw new ArgumentNullException(nameof(vgoFilePath));
            }

            FileInfo vgoFileInfo = new FileInfo(vgoFilePath);

            if (vgoFileInfo.Exists == false)
            {
                throw new FileNotFoundException(vgoFilePath);
            }

            DirectoryPath = vgoFileInfo.DirectoryName;

            byte[] allBytes = File.ReadAllBytes(vgoFilePath);

            byte[] cryptKey = null;

            if (vgkFilePath != null)
            {
                FileInfo vgkFileInfo = new FileInfo(vgkFilePath);

                if (vgkFileInfo.Exists == false)
                {
                    throw new FileNotFoundException(vgkFilePath);
                }

                cryptKey = File.ReadAllBytes(vgkFilePath);
            }

            ParseVgo(allBytes, cryptKey);
        }

        /// <summary>
        /// Create a new instance of VgoStorage with allBytes.
        /// </summary>
        /// <param name="allBytes">All bytes of file.</param>
        /// <param name="cryptKey">The crypt key.</param>
        /// <remarks>
        /// for Import
        /// </remarks>
        public VgoStorage(byte[] allBytes, byte[] cryptKey = null)
        {
            ParseVgo(allBytes, cryptKey);
        }

        /// <summary>
        /// Create a new instance of VgoStorage with resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="geometryCoordinate">The type of the geometry coodinates.</param>
        /// <param name="uvCoordinate">The type of the UV coodinates.</param>
        /// <remarks>for Export</remarks>
        public VgoStorage(IByteBuffer resource, VgoGeometryCoordinate geometryCoordinate, VgoUVCoordinate uvCoordinate)
        {
            if (resource == null)
            {
                throw new ArgumentNullException(nameof(resource));
            }

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

        #region Protected Methods (Import)

        /// <summary>
        /// Parse vgo.
        /// </summary>
        /// <param name="allBytes">All bytes of ".vgo" file.</param>
        /// <param name="cryptKey">The crypt key.</param>
        protected virtual void ParseVgo(byte[] allBytes, byte[] cryptKey = null)
        {
            if (allBytes == null)
            {
                throw new ArgumentNullException(nameof(allBytes));
            }

            if (allBytes.Length == 0)
            {
                throw new ArgumentException();
            }

            if (allBytes.Length < 16)
            {
                throw new FormatException();
            }

            ArraySegment<byte> allSegmentBytes = new ArraySegment<byte>(allBytes);

            // Header
            Header = GetHeader(allSegmentBytes);

            if ((Header.IsRequireResourceAccessorExternalCryptKey == 1) && (cryptKey == null))
            {
                throw new Exception("cryptKey is required.");
            }

            // Index chunk
            ChunkIndexMap = GetChunkIndexMap(allSegmentBytes, indexByteOffset: 16);

            // Composer chunk
            ArraySegment<byte> composerChunkDataBytes = ExtractChunkData(VgoChunkTypeID.COMP, ChunkIndexMap, allSegmentBytes);

            VgoComposerChunkData composerChunkData = composerChunkDataBytes
                .ToArray()
                .ConvertToStructure<VgoComposerChunkData>();

            // Asset Info chunk
            try
            {
                AssetInfo = ExtractAssetInfo(composerChunkData, ChunkIndexMap, allSegmentBytes);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Layout chunk
            try
            {
                Layout = ExtractLayout(composerChunkData, ChunkIndexMap, allSegmentBytes);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Resource Accessor chunk
            try
            {
                ResourceAccessors = ExtractResourceAccessor(composerChunkData, ChunkIndexMap, allSegmentBytes, cryptKey);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Resource chunk
            try
            {
                ArraySegment<byte> resourceBytes = ExtractResource(composerChunkData, ChunkIndexMap, allSegmentBytes);

                Resource = new ReadOnlyArraySegmentByteBuffer(resourceBytes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Protected Methods (Import)

        /// <summary>
        /// Get header chunk.
        /// </summary>
        /// <param name="allSegmentBytes">The all segment bytes.</param>
        /// <returns>The header chunk.</returns>
        protected virtual VgoHeader GetHeader(ArraySegment<byte> allSegmentBytes)
        {
            try
            {
                int headerSize = Marshal.SizeOf(typeof(VgoHeader));

                ArraySegment<byte> headerSegment = allSegmentBytes.Slice(offset: 0, count: headerSize);

                VgoHeader header = headerSegment.Array.ConvertToStructure<VgoHeader>();

                if (header.Magic != (uint)VgoChunkTypeID.Vgo)
                {
                    throw new FormatException($"Header.Magic: {header.Magic}");
                }

                if (header.DataLength != 8)
                {
                    throw new FormatException($"Header.DataLength: {header.DataLength}");
                }

                if (header.MajorVersion == 0)
                {
                    throw new FormatException($"Header.MajorVersion: {header.MajorVersion}");
                }

                if ((header.GeometryCoordinate != VgoGeometryCoordinate.RightHanded) &&
                    (header.GeometryCoordinate != VgoGeometryCoordinate.LeftHanded))
                {
                    throw new FormatException($"Header.GeometryCoordinate: {header.GeometryCoordinate}");
                }

                if ((header.UVCoordinate != VgoUVCoordinate.TopLeft) &&
                    (header.UVCoordinate != VgoUVCoordinate.BottomLeft))
                {
                    throw new FormatException($"Header.UVCoordinate: {header.UVCoordinate}");
                }

                return header;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get chunk index map.
        /// </summary>
        /// <param name="allSegmentBytes">The all segment bytes.</param>
        /// <param name="indexByteOffset">The start position of the index chunk.</param>
        /// <returns>The chunk index map.</returns>
        /// <remarks>
        /// Index chunk (8 + 16 * n byte)
        /// </remarks>
        protected virtual VgoIndexChunkDataElement[] GetChunkIndexMap(ArraySegment<byte> allSegmentBytes, int indexByteOffset)
        {
            try
            {
                uint indexChunkTypeId = BitConverter.ToUInt32(allSegmentBytes.Array, indexByteOffset);

                if (indexChunkTypeId != (uint)VgoChunkTypeID.Idx)
                {
                    throw new FormatException($"[IDX] Chunk.ChunkTypeId: {indexChunkTypeId}");
                }

                uint indexChunkDataLength = BitConverter.ToUInt32(allSegmentBytes.Array, indexByteOffset + 4);

                if (indexChunkDataLength == 0)
                {
                    throw new FormatException($"[IDX] Chunk.DataLength: {indexChunkDataLength}");
                }

                int elementSize = Marshal.SizeOf(typeof(VgoIndexChunkDataElement));

                if ((indexChunkDataLength % elementSize) != 0)
                {
                    throw new FormatException($"[IDX] Chunk.DataLength: {indexChunkDataLength}");
                }

                int elementCount = (int)indexChunkDataLength / elementSize;

                VgoIndexChunkDataElement[] chunkIndexMap = new VgoIndexChunkDataElement[elementCount];

                allSegmentBytes
                    .Slice(offset: indexByteOffset + 8, count: elementSize * elementCount)
                    .MarshalCopyTo(chunkIndexMap);

                return chunkIndexMap;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get the index chunk data of the specified chunk type ID from the chunk index map.
        /// </summary>
        /// <param name="chunkTypeId">The chunk type ID.</param>
        /// <param name="chunkIndexMap">The chunk index map.</param>
        /// <returns></returns>
        protected virtual VgoIndexChunkDataElement GetIndexChunkDataElement(VgoChunkTypeID chunkTypeId, VgoIndexChunkDataElement[] chunkIndexMap)
        {
            try
            {
                int count = chunkIndexMap.Where(x => x.ChunkTypeId == chunkTypeId).Count();

                if (count == 0)
                {
                    throw new FormatException($"{chunkTypeId} is not define in IDX chunk.");
                }

                if (count >= 2)
                {
                    throw new FormatException($"{chunkTypeId} is defined more than once in IDX chunk.");
                }

                VgoIndexChunkDataElement chunkInfo = chunkIndexMap.Where(x => x.ChunkTypeId == chunkTypeId).First();

                if (chunkInfo.ByteOffset == 0)
                {
                    throw new FormatException($"[{chunkTypeId}] Chunk.ByteOffset: {chunkInfo.ByteOffset}");
                }

                if (chunkInfo.ByteLength == 0)
                {
                    //throw new FormatException($"[{chunkTypeId}] Chunk.ByteLength: {chunkInfo.ByteLength}");
                }

                //if ((chunkInfo.ByteOffset + chunkInfo.ByteLength) > allBytes.Length)
                //{
                //    throw new FormatException($"[{chunkTypeId}] Chunk.ByteLength: {chunkInfo.ByteLength} is out of the range.");
                //}

                return chunkInfo;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Extract chunk data from asset info chunk.
        /// </summary>
        /// <param name="composerChunkData">The composer chunk data.</param>
        /// <param name="chunkIndexMap">The chunk index map.</param>
        /// <param name="allSegmentBytes">The all segment bytes.</param>
        /// <returns>The chunk data bytes.</returns>
        protected virtual ArraySegment<byte> ExtractChunkData(VgoChunkTypeID chunkTypeId, VgoIndexChunkDataElement[] chunkIndexMap, ArraySegment<byte> allSegmentBytes)
        {
            VgoIndexChunkDataElement chunkIndexInfo = GetIndexChunkDataElement(chunkTypeId, chunkIndexMap);

            uint chunkChunkTypeId = BitConverter.ToUInt32(allSegmentBytes.Array, (int)chunkIndexInfo.ByteOffset);

            if (chunkChunkTypeId != (uint)chunkIndexInfo.ChunkTypeId)
            {
                throw new FormatException($"[{chunkIndexInfo.ChunkTypeId}] Chunk.ChunkTypeId: {chunkChunkTypeId}");
            }

            uint chunkDataLength = BitConverter.ToUInt32(allSegmentBytes.Array, (int)chunkIndexInfo.ByteOffset + 4);

            if (chunkDataLength == 0)
            {
                throw new FormatException($"[{chunkIndexInfo.ChunkTypeId}] Chunk.DataLength: {chunkDataLength}");
            }

            ArraySegment<byte> chunkData = allSegmentBytes.Slice(
                offset: (int)chunkIndexInfo.ByteOffset + 8,
                count: (int)chunkDataLength - chunkIndexInfo.BytePadding
            );

            return chunkData;
        }

        #endregion

        #region Protected Methods (Import) Asset Info chunk

        /// <summary>
        /// Extract asset info from asset info chunk.
        /// </summary>
        /// <param name="composerChunkData">The composer chunk data.</param>
        /// <param name="chunkIndexMap">The chunk index map.</param>
        /// <param name="allSegmentBytes">The all segment bytes.</param>
        /// <returns>The asset info.</returns>
        protected virtual VgoAssetInfo ExtractAssetInfo(VgoComposerChunkData composerChunkData, VgoIndexChunkDataElement[] chunkIndexMap, ArraySegment<byte> allSegmentBytes)
        {
            try
            {
                ArraySegment<byte> assetInfoChunkData = ExtractChunkData(composerChunkData.AssetInfoChunkTypeId, chunkIndexMap, allSegmentBytes);

                VgoAssetInfo vgoAssetInfo;

                if (composerChunkData.AssetInfoChunkTypeId == VgoChunkTypeID.AIPJ)
                {
                    // JSON
                    string jsonString = Encoding.UTF8.GetString(assetInfoChunkData.ToArray());

                    vgoAssetInfo = JsonConvert.DeserializeObject<VgoAssetInfo>(jsonString, new VgoJsonSerializerSettings());
                }
                else if (composerChunkData.AssetInfoChunkTypeId == VgoChunkTypeID.AIPB)
                {
                    // BSON
                    var bsonSerializer = new VgoBsonSerializer();

                    vgoAssetInfo = bsonSerializer.DeserializeObject<VgoAssetInfo>(assetInfoChunkData.ToArray());
                }
                else
                {
                    throw new FormatException($"[COMP] AssetInfoChunkTypeId: {composerChunkData.AssetInfoChunkTypeId}");
                }

                return vgoAssetInfo;
            }
            catch (JsonSerializationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Protected Methods (Import) Layout chunk

        /// <summary>
        /// Extract layout from layout chunk.
        /// </summary>
        /// <param name="composerChunkData">The composer chunk data.</param>
        /// <param name="chunkIndexMap">The chunk index map.</param>
        /// <param name="allSegmentBytes">The all segment bytes.</param>
        /// <returns>The layout.</returns>
        protected virtual VgoLayout ExtractLayout(VgoComposerChunkData composerChunkData, VgoIndexChunkDataElement[] chunkIndexMap, ArraySegment<byte> allSegmentBytes)
        {
            try
            {
                ArraySegment<byte> layoutChunkData = ExtractChunkData(composerChunkData.LayoutChunkTypeId, chunkIndexMap, allSegmentBytes);

                VgoLayout vgoLayout;

                if (composerChunkData.LayoutChunkTypeId == VgoChunkTypeID.LAPJ)
                {
                    // JSON
                    string jsonString = Encoding.UTF8.GetString(layoutChunkData.ToArray());

                    vgoLayout = JsonConvert.DeserializeObject<VgoLayout>(jsonString, new VgoJsonSerializerSettings());
                }
                else if (composerChunkData.LayoutChunkTypeId == VgoChunkTypeID.LAPB)
                {
                    // BSON
                    var bsonSerializer = new VgoBsonSerializer();

                    vgoLayout = bsonSerializer.DeserializeObject<VgoLayout>(layoutChunkData.ToArray());
                }
                else
                {
                    throw new FormatException($"[COMP] LayoutChunkTypeId: {composerChunkData.LayoutChunkTypeId}");
                }

                return vgoLayout;
            }
            catch (JsonSerializationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Protected Methods (Import) Resource Accessor chunk

        /// <summary>
        /// Extract resource accessor from resource accessor chunk.
        /// </summary>
        /// <param name="composerChunkData">The composer chunk data.</param>
        /// <param name="chunkIndexMap">The chunk index map.</param>
        /// <param name="allSegmentBytes">The all segment bytes.</param>
        /// <param name="cryptKey">The crypt key.</param>
        /// <returns>List of the resource accessor.</returns>
        protected virtual List<VgoResourceAccessor> ExtractResourceAccessor(VgoComposerChunkData composerChunkData, VgoIndexChunkDataElement[] chunkIndexMap, ArraySegment<byte> allSegmentBytes, byte[] cryptKey = null)
        {
            VgoChunkTypeID raChunkTypeId = composerChunkData.ResourceAccessorChunkTypeId;

            switch (raChunkTypeId)
            {
                case VgoChunkTypeID.RAPJ:
                case VgoChunkTypeID.RAPB:
                case VgoChunkTypeID.RACJ:
                case VgoChunkTypeID.RACB:
                    break;
                default:
                    throw new FormatException($"[COMP] ResourceAccessorChunkTypeId: {raChunkTypeId}");
            }

            ArraySegment<byte> raChunkData = ExtractChunkData(raChunkTypeId, chunkIndexMap, allSegmentBytes);

            byte[] plainJsonOrBson = null;

            if (raChunkTypeId == VgoChunkTypeID.RAPJ)
            {
                plainJsonOrBson = raChunkData.ToArray();
            }
            else if (raChunkTypeId == VgoChunkTypeID.RAPB)
            {
                plainJsonOrBson = raChunkData.ToArray();
            }
            else if (
                (raChunkTypeId == VgoChunkTypeID.RACJ) ||
                (raChunkTypeId == VgoChunkTypeID.RACB))
            {
                VgoCryptV0 vgoCrypt = GetVgoCrypt(composerChunkData.ResourceAccessorCryptChunkTypeId, chunkIndexMap, allSegmentBytes);

                try
                {
                    byte[] encryptedJsonOrBson = raChunkData.ToArray();

                    if (vgoCrypt.algorithms == VgoCryptographyAlgorithms.AES)
                    {
                        // AES
                        AesCrypter aesCrypter = new AesCrypter
                        {
                            CipherMode = vgoCrypt.cipherMode,
                            PaddingMode = vgoCrypt.paddingMode,
                        };

                        byte[] key;

                        if (cryptKey == null)
                        {
                            if (string.IsNullOrEmpty(vgoCrypt.key))
                            {
                                throw new Exception("crypt key is unknown.");
                            }
                            else
                            {
                                key = Convert.FromBase64String(vgoCrypt.key);
                            }
                        }
                        else
                        {
                            key = cryptKey;
                        }

                        if (key == null)
                        {
                            throw new Exception("crypt key is unknown.");
                        }

                        if (string.IsNullOrEmpty(vgoCrypt.iv))
                        {
                            throw new Exception("iv is unknown.");
                        }

                        byte[] iv = Convert.FromBase64String(vgoCrypt.iv);

                        plainJsonOrBson = aesCrypter.Decrypt(encryptedJsonOrBson, key, iv);
                    }
                    else if (vgoCrypt.algorithms == VgoCryptographyAlgorithms.Base64)
                    {
                        // Base64
                        string base64String = Encoding.UTF8.GetString(encryptedJsonOrBson);

                        plainJsonOrBson = Convert.FromBase64String(base64String);
                    }
                    else
                    {
                        throw new NotSupportedException($"CryptographyAlgorithms: {vgoCrypt.algorithms}");
                    }
                }
                catch (JsonSerializationException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            List<VgoResourceAccessor> vgoResourceAccessors = null;

            try
            {
                if ((raChunkTypeId == VgoChunkTypeID.RAPJ) ||
                    (raChunkTypeId == VgoChunkTypeID.RACJ))
                {
                    // JSON
                    string jsonString = Encoding.UTF8.GetString(plainJsonOrBson);

                    vgoResourceAccessors = JsonConvert.DeserializeObject<List<VgoResourceAccessor>>(jsonString, new VgoJsonSerializerSettings());
                }
                else if (
                    (raChunkTypeId == VgoChunkTypeID.RAPB) ||
                    (raChunkTypeId == VgoChunkTypeID.RACB))
                {
                    // BSON
                    var bsonSerializer = new VgoBsonSerializer();

                    vgoResourceAccessors = bsonSerializer.DeserializeObject<List<VgoResourceAccessor>>(plainJsonOrBson, rootValueAsArray: true);
                }
            }
            catch (JsonSerializationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return vgoResourceAccessors;
        }

        #endregion

        #region Protected Methods (Import) Resource chunk

        /// <summary>
        /// Extract resouce bytes from resouce chunk.
        /// </summary>
        /// <param name="composerChunkData">The composer chunk data.</param>
        /// <param name="chunkIndexMap">The chunk index map.</param>
        /// <param name="allSegmentBytes">The all segment bytes.</param>
        /// <returns>The resouce bytes.</returns>
        protected virtual ArraySegment<byte> ExtractResource(VgoComposerChunkData composerChunkData, VgoIndexChunkDataElement[] chunkIndexMap, ArraySegment<byte> allSegmentBytes)
        {
            VgoChunkTypeID resourceChunkTypeId = composerChunkData.ResourceChunkTypeId;

            VgoResource vgoResource;

            try
            {
                ArraySegment<byte> resouceChunkData = ExtractChunkData(resourceChunkTypeId, chunkIndexMap, allSegmentBytes);

                if (resourceChunkTypeId == VgoChunkTypeID.REPb)
                {
                    return resouceChunkData;
                }
                else if (resourceChunkTypeId == VgoChunkTypeID.REPJ)
                {
                    // JSON
                    string resourceJsonString = Encoding.UTF8.GetString(resouceChunkData.ToArray());

                    vgoResource = JsonConvert.DeserializeObject<VgoResource>(resourceJsonString, new VgoJsonSerializerSettings());
                }
                else if (resourceChunkTypeId == VgoChunkTypeID.REPB)
                {
                    // BSON
                    var bsonSerializer = new VgoBsonSerializer();

                    vgoResource = bsonSerializer.DeserializeObject<VgoResource>(resouceChunkData.ToArray());
                }
                else
                {
                    throw new FormatException($"[COMP] ResourceChunkTypeId: {resourceChunkTypeId}");
                }
            }
            catch (JsonSerializationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (string.IsNullOrEmpty(vgoResource.uri))
            {
                throw new FormatException($"[{resourceChunkTypeId}] uri is null.");
            }

            if (vgoResource.byteLength == 0)
            {
                //throw new FormatException($"[{chunkTypeId}] byteLength: {vgoResource.byteLength}");
            }

            byte[] resourceBytes = GetUriData(vgoResource.uri);

            return new ArraySegment<byte>(resourceBytes);
        }

        #endregion

        #region Protected Methods (Import) Crypt chunk

        /// <summary>
        /// Get the vgo crypt.
        /// </summary>
        /// <param name="cryptChunkTypeId">The crypt chunk type ID.</param>
        /// <param name="chunkIndexMap">The chunk index map.</param>
        /// <param name="allSegmentBytes">The all segment bytes.</param>
        /// <returns>The vgo crypt.</returns>
        protected virtual VgoCryptV0 GetVgoCrypt(VgoChunkTypeID cryptChunkTypeId, VgoIndexChunkDataElement[] chunkIndexMap, ArraySegment<byte> allSegmentBytes)
        {
            try
            {
                ArraySegment<byte> cryptChunkData = ExtractChunkData(cryptChunkTypeId, chunkIndexMap, allSegmentBytes);

                VgoCryptV0 vgoCrypt;

                if (cryptChunkTypeId == VgoChunkTypeID.CRAJ)
                {
                    // JSON
                    string cryptJsonString = Encoding.UTF8.GetString(cryptChunkData.ToArray());

                    vgoCrypt = JsonConvert.DeserializeObject<VgoCryptV0>(cryptJsonString, new VgoJsonSerializerSettings());
                }
                else if (cryptChunkTypeId == VgoChunkTypeID.CRAB)
                {
                    // BSON
                    var bsonSerializer = new VgoBsonSerializer();

                    vgoCrypt = bsonSerializer.DeserializeObject<VgoCryptV0>(cryptChunkData.ToArray());
                }
                else
                {
                    throw new Exception($"cryptChunkTypeId: {cryptChunkTypeId}");
                }

                return vgoCrypt;
            }
            catch (JsonSerializationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Protected Methods (Import) Helper

        /// <summary>
        /// Get URI data.
        /// </summary>
        /// <param name="uri">The uri.</param>
        /// <returns>An byte array of uri data.</returns>
        protected virtual byte[] GetUriData(string uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(uri);
            }

            if (uri.StartsWith("data:"))
            {
                throw new NotSupportedException(uri);
            }
            else if (
                uri.StartsWith("http://") ||
                uri.StartsWith("https://"))
            {
                using (HttpClient httpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(HttpTimeoutSeconds) })
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri))
                using (HttpResponseMessage response = httpClient.SendAsync(request).GetAwaiter().GetResult())
                {
                    if (response.IsSuccessStatusCode)
                    {
                        byte[] byteArray = response.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();

                        return byteArray;
                    }
                    else
                    {
                        throw new Exception(response.StatusCode.ToString());
                    }
                }
            }
            else if (uri.StartsWith("file://"))
            {
                throw new NotSupportedException(uri);
            }
            else
            {
                if (DirectoryPath == null)
                {
                    throw new Exception();
                }

                string binFilePath = Path.Combine(DirectoryPath, uri);

                if (File.Exists(binFilePath) == false)
                {
                    throw new FileNotFoundException(binFilePath);
                }

                byte[] binData = File.ReadAllBytes(binFilePath);

                return binData;
            }
        }

        #endregion

        #region Public Methods (Export)

        /// <summary>
        /// Exports a VGO format file.
        /// </summary>
        /// <param name="filePath">The full path of the file.</param>
        /// <param name="assetInfoTypeId">AIPJ or AIPB</param>
        /// <param name="layoutTypeId">LAPJ or LAPB</param>
        /// <param name="resourceAccessorTypeId">RAPJ or RAPB or RACJ or RACB</param>
        /// <param name="resourceAccessorCryptTypeId">None or CRAJ or CRAB</param>
        /// <param name="resourceAccessorCryptAlgorithm">The resource accessor crypt algorithm.</param>
        /// <param name="resourceAccessorCryptKey">The resource accessor crypt key.</param>
        /// <param name="resourceTypeId">REPb or REPJ or REPB</param>
        /// <param name="resourceUri">The resource URI.</param>
        /// <param name="binFileName">The resource binary file name.</param>
        /// <returns>Returns true if the export was successful, false otherwise.</returns>
        public virtual bool ExportVgoFile(
            string filePath,
            VgoChunkTypeID assetInfoTypeId = VgoChunkTypeID.AIPJ,
            VgoChunkTypeID layoutTypeId = VgoChunkTypeID.LAPJ,
            VgoChunkTypeID resourceAccessorTypeId = VgoChunkTypeID.RAPJ,
            VgoChunkTypeID resourceAccessorCryptTypeId = VgoChunkTypeID.None,
            string resourceAccessorCryptAlgorithm = null,
            byte[] resourceAccessorCryptKey = null,
            VgoChunkTypeID resourceTypeId = VgoChunkTypeID.REPb,
            string resourceUri = null,
            string binFileName = null)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (AssetInfo == null)
            {
                throw new Exception();
            }

            if (Layout == null)
            {
                throw new Exception();
            }

            if (ResourceAccessors == null)
            {
                throw new Exception();
            }

            if (Resource == null)
            {
                throw new Exception();
            }

            FileInfo fileInfo = new FileInfo(filePath);

            // Asset Info chunk
            VgoChunk assetInfoChunk = CreateAssetInfoChunk(assetInfoTypeId);

            // Layout chunk
            VgoChunk layoutChunk = CreateLayoutChunk(layoutTypeId);

            // Resource Accessor chunk
            (VgoChunk resourceAccessorChunk, VgoChunk resourceAccessorCryptChunk) =
                CreateResourceAccessorChunk(
                    resourceAccessorTypeId,
                    resourceAccessorCryptTypeId,
                    resourceAccessorCryptAlgorithm,
                    resourceAccessorCryptKey
                );

            // Resource chunk
            VgoChunk resourceChunk = CreatResourceChunk(resourceTypeId, fileInfo, binFileName, resourceUri);

            // Composer chunk
            VgoChunk composerChunk = CreateComposerChunk(
                assetInfoTypeId,
                layoutTypeId,
                resourceAccessorTypeId,
                resourceAccessorCryptTypeId,
                resourceTypeId
            );

            // Index chunk
            List<VgoChunk> chunkList;

            if (resourceAccessorCryptChunk == null)
            {
                chunkList = new List<VgoChunk>(5)
                {
                    composerChunk,
                    assetInfoChunk,
                    layoutChunk,
                    resourceAccessorChunk,
                    resourceChunk,
                };
            }
            else
            {
                chunkList = new List<VgoChunk>(6)
                {
                    composerChunk,
                    assetInfoChunk,
                    layoutChunk,
                    resourceAccessorChunk,
                    resourceAccessorCryptChunk,
                    resourceChunk,
                };
            }

            VgoChunk indexChunk = CreateIndexChunk(chunkList);

            // Header
            if (resourceAccessorCryptTypeId != VgoChunkTypeID.None)
            {
                Header.ResourceAccessorIsCrypted = 1;

                if (resourceAccessorCryptKey != null)
                {
                    Header.IsRequireResourceAccessorExternalCryptKey = 1;
                }
            }

            // Output (.vgo)
            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write(Header.ConvertToByteArray());
                writer.Write(indexChunk.ConvertToByteArray());
                writer.Write(composerChunk.ConvertToByteArray());
                writer.Write(assetInfoChunk.ConvertToByteArray());
                writer.Write(layoutChunk.ConvertToByteArray());
                writer.Write(resourceAccessorChunk.ConvertToByteArray());
                if (resourceAccessorCryptChunk != null)
                {
                    writer.Write(resourceAccessorCryptChunk.ConvertToByteArray());
                }
                writer.Write(resourceChunk.ConvertToByteArray());

                writer.Flush();
            }

            if (resourceAccessorCryptKey != null)
            {
                string keyFileName = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length) + ".vgk";

                string keyFullPath = Path.Combine(fileInfo.DirectoryName, keyFileName);

                // Output (.vgk)
                using (var stream = new FileStream(keyFullPath, FileMode.Create, FileAccess.Write, FileShare.None))
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(resourceAccessorCryptKey);

                    writer.Flush();
                }
            }

            if (resourceTypeId != VgoChunkTypeID.REPb)
            {
                string binFullPath = Path.Combine(fileInfo.DirectoryName, binFileName);  // @notice binFileName

                // Output (.bin)
                using (var stream = new FileStream(binFullPath, FileMode.Create, FileAccess.Write, FileShare.None))
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(Resource.ToArray());

                    writer.Flush();
                }
            }

            return true;
        }

        #endregion

        #region Protected Methods (Export)

        /// <summary>
        /// Create an asset info chunk.
        /// </summary>
        /// <param name="assetInfoTypeId">The asset info chunk type ID.</param>
        /// <returns>An asset info chunk.</returns>
        public virtual VgoChunk CreateAssetInfoChunk(VgoChunkTypeID assetInfoTypeId)
        {
            IByteBuffer assetInfoChunkData;

            try
            {
                if (assetInfoTypeId == VgoChunkTypeID.AIPJ)
                {
                    // JSON
                    string jsonString = JsonConvert.SerializeObject(AssetInfo, new VgoJsonSerializerSettings());

                    byte[] json = Encoding.UTF8.GetBytes(jsonString);

                    assetInfoChunkData = new ReadOnlyArraySegmentByteBuffer(new ArraySegment<byte>(json));
                }
                else if (assetInfoTypeId == VgoChunkTypeID.AIPB)
                {
                    // BSON
                    var bsonSerializer = new VgoBsonSerializer();

                    byte[] bson = bsonSerializer.SerializeObject(AssetInfo);

                    assetInfoChunkData = new ReadOnlyArraySegmentByteBuffer(new ArraySegment<byte>(bson));
                }
                else
                {
                    throw new Exception($"assetInfoTypeId: {assetInfoTypeId}");
                }
            }
            catch (JsonSerializationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            VgoChunk assetInfoChunk = new VgoChunk(assetInfoTypeId, assetInfoChunkData);

            return assetInfoChunk;
        }

        /// <summary>
        /// Create a layout chunk.
        /// </summary>
        /// <param name="layoutTypeId">The layout chunk type ID.</param>
        /// <returns>A layout chunk.</returns>
        protected virtual VgoChunk CreateLayoutChunk(VgoChunkTypeID layoutTypeId)
        {
            IByteBuffer layoutChunkData;

            try
            {
                if (layoutTypeId == VgoChunkTypeID.LAPJ)
                {
                    // JSON
                    string jsonString = JsonConvert.SerializeObject(Layout, new VgoJsonSerializerSettings());

                    byte[] json = Encoding.UTF8.GetBytes(jsonString);

                    layoutChunkData = new ReadOnlyArraySegmentByteBuffer(new ArraySegment<byte>(json));
                }
                else if (layoutTypeId == VgoChunkTypeID.LAPB)
                {
                    // BSON
                    var bsonSerializer = new VgoBsonSerializer();

                    byte[] bson = bsonSerializer.SerializeObject(Layout);

                    layoutChunkData = new ReadOnlyArraySegmentByteBuffer(new ArraySegment<byte>(bson));
                }
                else
                {
                    throw new Exception($"layoutTypeId: {layoutTypeId}");
                }
            }
            catch (JsonSerializationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            VgoChunk layoutChunk = new VgoChunk(layoutTypeId, layoutChunkData);

            return layoutChunk;
        }

        /// <summary>
        /// Create a resource accessor chunk.
        /// </summary>
        /// <param name="resourceAccessorTypeId">The resource accessor chunk type ID.</param>
        /// <param name="resourceAccessorCryptTypeId">The resource accessor crypt chunk type ID.</param>
        /// <param name="cryptAlgorithm">The crypt algorithm.</param>
        /// <param name="cryptKey">The crypt key.</param>
        /// <returns>A resource accessor chunk and a crypt chunk.</returns>
        protected virtual (VgoChunk, VgoChunk) CreateResourceAccessorChunk(
            VgoChunkTypeID resourceAccessorTypeId,
            VgoChunkTypeID resourceAccessorCryptTypeId,
            string cryptAlgorithm = null,
            byte[] cryptKey = null)
        {
            IByteBuffer resourceAccessorChunkData;
            IByteBuffer resourceAccessorCryptChunkData = null;

            try
            {
                byte[] plainResourceAccessorJsonOrBson;

                if ((resourceAccessorTypeId == VgoChunkTypeID.RAPJ) ||
                    (resourceAccessorTypeId == VgoChunkTypeID.RACJ))
                {
                    // JSON
                    string jsonString = JsonConvert.SerializeObject(ResourceAccessors, new VgoJsonSerializerSettings());

                    plainResourceAccessorJsonOrBson = Encoding.UTF8.GetBytes(jsonString);
                }
                else if (
                    (resourceAccessorTypeId == VgoChunkTypeID.RAPB) ||
                    (resourceAccessorTypeId == VgoChunkTypeID.RACB))
                {
                    // BSON
                    var bsonSerializer = new VgoBsonSerializer();

                    plainResourceAccessorJsonOrBson = bsonSerializer.SerializeObject(ResourceAccessors);
                }
                else
                {
                    throw new Exception($"resourceAccessorTypeId: {resourceAccessorTypeId}");
                }

                if ((resourceAccessorTypeId == VgoChunkTypeID.RAPJ) ||
                    (resourceAccessorTypeId == VgoChunkTypeID.RAPB))
                {
                    resourceAccessorChunkData = new ReadOnlyArraySegmentByteBuffer(new ArraySegment<byte>(plainResourceAccessorJsonOrBson));
                }
                else if (
                    (resourceAccessorTypeId == VgoChunkTypeID.RACJ) ||
                    (resourceAccessorTypeId == VgoChunkTypeID.RACB))
                {
                    byte[] encryptedResourceAccessorJsonOrBson;

                    VgoCryptV0 cryptInfo;

                    if (cryptAlgorithm == VgoCryptographyAlgorithms.AES)
                    {
                        AesCrypter aesCrypter = new AesCrypter();

                        string keyString;
                        string ivString;

                        // JSON or BSON (encrypt)
                        if (cryptKey == null)
                        {
                            encryptedResourceAccessorJsonOrBson = aesCrypter.Encrypt(plainResourceAccessorJsonOrBson, keySize: 128, blockSize: 128, out _, out keyString, out _, out ivString);
                        }
                        else
                        {
                            encryptedResourceAccessorJsonOrBson = aesCrypter.Encrypt(plainResourceAccessorJsonOrBson, cryptKey, blockSize: 128, out _, out ivString);

                            keyString = null;  // @notice Secret
                        }

                        // CRAJ
                        cryptInfo = new VgoCryptV0
                        {
                            algorithms = VgoCryptographyAlgorithms.AES,
                            key = keyString,
                            iv = ivString,
                            cipherMode = aesCrypter.CipherMode,
                            paddingMode = aesCrypter.PaddingMode,
                        };
                    }
                    else if (cryptAlgorithm == VgoCryptographyAlgorithms.Base64)
                    {
                        // JSON or BSON (encrypt)
                        string base64String = Convert.ToBase64String(plainResourceAccessorJsonOrBson);

                        encryptedResourceAccessorJsonOrBson = Encoding.UTF8.GetBytes(base64String);

                        // CRAJ
                        cryptInfo = new VgoCryptV0
                        {
                            algorithms = VgoCryptographyAlgorithms.Base64,
                        };
                    }
                    else
                    {
                        throw new Exception($"resourceAccessorCryptAlgorithm: {cryptAlgorithm}");
                    }

                    byte[] cryptInfoJsonOrBson;

                    if (resourceAccessorCryptTypeId == VgoChunkTypeID.CRAJ)
                    {
                        string cryptInfoJsonString = JsonConvert.SerializeObject(cryptInfo);

                        cryptInfoJsonOrBson = Encoding.UTF8.GetBytes(cryptInfoJsonString);
                    }
                    else if (resourceAccessorCryptTypeId == VgoChunkTypeID.CRAB)
                    {
                        var bsonSerializer = new VgoBsonSerializer();

                        cryptInfoJsonOrBson = bsonSerializer.SerializeObject(cryptInfo);
                    }
                    else
                    {
                        throw new Exception($"resourceAccessorCryptTypeId: {resourceAccessorCryptTypeId}");
                    }

                    resourceAccessorChunkData = new ReadOnlyArraySegmentByteBuffer(new ArraySegment<byte>(encryptedResourceAccessorJsonOrBson));
                    resourceAccessorCryptChunkData = new ReadOnlyArraySegmentByteBuffer(new ArraySegment<byte>(cryptInfoJsonOrBson));
                }
                else
                {
                    throw new Exception($"resourceAccessorTypeId: {resourceAccessorTypeId}");
                }
            }
            catch (JsonSerializationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            VgoChunk resourceAccessorChunk = new VgoChunk(resourceAccessorTypeId, resourceAccessorChunkData);

            VgoChunk resourceAccessorCryptChunk = null;

            if (resourceAccessorCryptChunkData != null)
            {
                resourceAccessorCryptChunk = new VgoChunk(resourceAccessorCryptTypeId, resourceAccessorCryptChunkData);
            }

            return (resourceAccessorChunk, resourceAccessorCryptChunk);
        }

        /// <summary>
        /// Create a resource chunk.
        /// </summary>
        /// <param name="resourceTypeId">The resource chunk type ID.</param>
        /// <param name="fileInfo">The file info.</param>
        /// <param name="binFileName">The resource binary file name.</param>
        /// <param name="resourceUri">The resource URI.</param>
        /// <returns>A resouce chunk.</returns>
        protected virtual VgoChunk CreatResourceChunk(VgoChunkTypeID resourceTypeId, FileInfo fileInfo, string binFileName = null, string resourceUri = null)
        {
            if (binFileName == null)
            {
                binFileName = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length) + ".bin";
            }

            if (resourceUri == null)
            {
                resourceUri = binFileName;
            }

            IByteBuffer resourceChunkData;

            try
            {
                var vgoResource = new VgoResource
                {
                    uri = resourceUri,
                    byteLength = Resource.Length,
                };

                if (resourceTypeId == VgoChunkTypeID.REPb)
                {
                    // binary
                    resourceChunkData = Resource;
                }
                else if (resourceTypeId == VgoChunkTypeID.REPJ)
                {
                    // JSON
                    string resourceJsonString = JsonConvert.SerializeObject(vgoResource, new VgoJsonSerializerSettings());

                    byte[] resourceJson = Encoding.UTF8.GetBytes(resourceJsonString);

                    resourceChunkData = new ReadOnlyArraySegmentByteBuffer(new ArraySegment<byte>(resourceJson));
                }
                else if (resourceTypeId == VgoChunkTypeID.REPB)
                {
                    // BSON
                    var bsonSerializer = new VgoBsonSerializer();

                    byte[] resourceBson = bsonSerializer.SerializeObject(vgoResource);

                    resourceChunkData = new ReadOnlyArraySegmentByteBuffer(new ArraySegment<byte>(resourceBson));
                }
                else
                {
                    throw new Exception($"resourceTypeId: {resourceTypeId}");
                }
            }
            catch (JsonSerializationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            VgoChunk resourceChunk = new VgoChunk(resourceTypeId, resourceChunkData);

            return resourceChunk;
        }

        /// <summary>
        /// Create a composer chunk.
        /// </summary>
        /// <param name="assetInfoTypeId">The asset info chunk type ID.</param>
        /// <param name="layoutTypeId">The layout chunk type ID.</param>
        /// <param name="resourceAccessorTypeId">The resourse accessor chunk type ID.</param>
        /// <param name="resourceAccessorCryptTypeId">The resourse accessor crypt chunk type ID.</param>
        /// <param name="resourceTypeId">The resourse chunk type ID.</param>
        /// <returns>A composer chunk.</returns>
        public virtual VgoChunk CreateComposerChunk(
            VgoChunkTypeID assetInfoTypeId,
            VgoChunkTypeID layoutTypeId,
            VgoChunkTypeID resourceAccessorTypeId,
            VgoChunkTypeID resourceAccessorCryptTypeId,
            VgoChunkTypeID resourceTypeId)
        {
            VgoComposerChunkData composerChunkData = new VgoComposerChunkData
            {
                AssetInfoChunkTypeId = assetInfoTypeId,
                LayoutChunkTypeId = layoutTypeId,
                ResourceAccessorChunkTypeId = resourceAccessorTypeId,
                ResourceAccessorCryptChunkTypeId = resourceAccessorCryptTypeId,
                ResourceCryptChunkTypeId = resourceAccessorCryptTypeId,
                ResourceChunkTypeId = resourceTypeId,
            };

            byte[] compDataBytes = composerChunkData.ConvertToByteArray();

            IByteBuffer composerCunkData = new ReadOnlyArraySegmentByteBuffer(new ArraySegment<byte>(compDataBytes));

            VgoChunk composerChunk = new VgoChunk(VgoChunkTypeID.COMP, composerCunkData);

            return composerChunk;
        }

        /// <summary>
        /// Create a index chunk.
        /// </summary>
        /// <param name="chunkList">List of chunk.</param>
        /// <returns>A index chunk.</returns>
        protected virtual VgoChunk CreateIndexChunk(List<VgoChunk> chunkList)
        {
            VgoIndexChunkDataElement[] indexChunkDataArray = new VgoIndexChunkDataElement[chunkList.Count];

            int headerSize = Marshal.SizeOf(typeof(VgoHeader));

            int indexChunkDataLength = Marshal.SizeOf(typeof(VgoIndexChunkDataElement)) * chunkList.Count;

            int indexChunkSize = 8 + indexChunkDataLength;

            uint byteOffset = (uint)(headerSize + indexChunkSize);

            for (int idx = 0; idx < indexChunkDataArray.Length; idx++)
            {
                VgoChunk curChunk = chunkList[idx];

                indexChunkDataArray[idx] = new VgoIndexChunkDataElement
                {
                    ChunkTypeId = curChunk.TypeId,
                    ByteOffset = byteOffset,
                    ByteLength = curChunk.AllLength,
                    BytePadding = (byte)curChunk.PaddingCount,
                };

                byteOffset += curChunk.AllLength;
            }

            ChunkIndexMap = indexChunkDataArray;

            var indicatorBuffer = new ArraySegmentByteBuffer(indexChunkDataLength);

            indicatorBuffer.Append(new ArraySegment<VgoIndexChunkDataElement>(indexChunkDataArray));

            VgoChunk indexChunk = new VgoChunk(VgoChunkTypeID.Idx, indicatorBuffer);

            return indexChunk;
        }

        #endregion
    }
}
