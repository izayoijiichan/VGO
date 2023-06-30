// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoStorage
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using NewtonVgo.Buffers;
    using NewtonVgo.Security.Cryptography;
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
    public partial class VgoStorage : IVgoStorage
    {
        #region Protected Methods (Import)

        /// <summary>
        /// Parse vgo.
        /// </summary>
        /// <param name="vgoBytes">The vgo bytes.</param>
        /// <param name="vgkBytes">The vgk bytes.</param>
        protected virtual void ParseVgo(byte[] vgoBytes, byte[]? vgkBytes, out VgoLayout layout)
        {
            if (vgoBytes == null)
            {
                throw new ArgumentNullException(nameof(vgoBytes));
            }

            if (vgoBytes.Any() == false)
            {
                throw new ArgumentOutOfRangeException(nameof(vgoBytes));
            }

            if (vgoBytes.Length < 16)
            {
                throw new ArgumentOutOfRangeException(nameof(vgoBytes));
            }

            ArraySegment<byte> allSegmentBytes = new ArraySegment<byte>(vgoBytes);

            // Header
            Header = GetHeader(allSegmentBytes);

            if ((Header.IsRequireResourceAccessorExternalCryptKey == 1) && (vgkBytes == null))
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
            catch
            {
                throw;
            }

            // Layout chunk
            try
            {
                layout = ExtractLayout(composerChunkData, ChunkIndexMap, allSegmentBytes);
            }
            catch
            {
                throw;
            }

            // Resource Accessor chunk
            try
            {
                ResourceAccessors = ExtractResourceAccessor(composerChunkData, ChunkIndexMap, allSegmentBytes, vgkBytes);
            }
            catch
            {
                throw;
            }

            // Resource chunk
            try
            {
                ArraySegment<byte> resourceBytes = ExtractResource(composerChunkData, ChunkIndexMap, allSegmentBytes);

                Resource = new ReadOnlyArraySegmentByteBuffer(resourceBytes);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Protected Methods (Import) Header chunk

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
            catch
            {
                throw;
            }
        }

        #endregion

        #region Protected Methods (Import) Index chunk

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
        /// <param name="chunkTypeId">The chunk type ID.</param>
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
        protected virtual VgoAssetInfo? ExtractAssetInfo(VgoComposerChunkData composerChunkData, VgoIndexChunkDataElement[] chunkIndexMap, ArraySegment<byte> allSegmentBytes)
        {
            try
            {
                ArraySegment<byte> assetInfoChunkData = ExtractChunkData(composerChunkData.AssetInfoChunkTypeId, chunkIndexMap, allSegmentBytes);

                VgoAssetInfo? vgoAssetInfo;

                if (composerChunkData.AssetInfoChunkTypeId == VgoChunkTypeID.AIPJ)
                {
                    // JSON
                    vgoAssetInfo = DeserializeObject<VgoAssetInfo>(assetInfoChunkData.ToArray(), isBson: false);
                }
                else if (composerChunkData.AssetInfoChunkTypeId == VgoChunkTypeID.AIPB)
                {
                    // BSON
                    vgoAssetInfo = DeserializeObject<VgoAssetInfo>(assetInfoChunkData.ToArray(), isBson: true);
                }
                else
                {
                    throw new FormatException($"[COMP] AssetInfoChunkTypeId: {composerChunkData.AssetInfoChunkTypeId}");
                }

                return vgoAssetInfo;
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

                VgoLayout? vgoLayout;

                if (composerChunkData.LayoutChunkTypeId == VgoChunkTypeID.LAPJ)
                {
                    // JSON
                    vgoLayout = DeserializeObject<VgoLayout>(layoutChunkData.ToArray(), isBson: false);
                }
                else if (composerChunkData.LayoutChunkTypeId == VgoChunkTypeID.LAPB)
                {
                    // BSON
                    vgoLayout = DeserializeObject<VgoLayout>(layoutChunkData.ToArray(), isBson: true);
                }
                else
                {
                    throw new FormatException($"[COMP] LayoutChunkTypeId: {composerChunkData.LayoutChunkTypeId}");
                }

                if (vgoLayout is null)
                {
                    throw new FormatException();
                }

                return vgoLayout;
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
        protected virtual List<VgoResourceAccessor>? ExtractResourceAccessor(VgoComposerChunkData composerChunkData, VgoIndexChunkDataElement[] chunkIndexMap, ArraySegment<byte> allSegmentBytes, byte[]? cryptKey = null)
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

            byte[]? plainJsonOrBson;

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
                VgoCryptV0? vgoCrypt = GetVgoCrypt(composerChunkData.ResourceAccessorCryptChunkTypeId, chunkIndexMap, allSegmentBytes);

                if (vgoCrypt is null)
                {
                    throw new FormatException($" ResourceAccessorChunkTypeId: {raChunkTypeId}");
                }

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
                catch (JsonSerializationException)
                {
                    throw;
                }
                catch
                {
                    throw;
                }
            }
            else
            {
                throw new FormatException($"[COMP] ResourceAccessorChunkTypeId: {raChunkTypeId}");
            }

            List<VgoResourceAccessor>? vgoResourceAccessors = null;

            try
            {
                if ((raChunkTypeId == VgoChunkTypeID.RAPJ) ||
                    (raChunkTypeId == VgoChunkTypeID.RACJ))
                {
                    // JSON
                    vgoResourceAccessors = DeserializeObject<List<VgoResourceAccessor>>(plainJsonOrBson, isBson: false);
                }
                else if (
                    (raChunkTypeId == VgoChunkTypeID.RAPB) ||
                    (raChunkTypeId == VgoChunkTypeID.RACB))
                {
                    // BSON
                    vgoResourceAccessors = DeserializeObject<List<VgoResourceAccessor>>(plainJsonOrBson, isBson: true, rootValueAsArray: true);
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

            VgoResource? vgoResource;

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
                    vgoResource = DeserializeObject<VgoResource>(resouceChunkData.ToArray(), isBson: false);
                }
                else if (resourceChunkTypeId == VgoChunkTypeID.REPB)
                {
                    // BSON
                    vgoResource = DeserializeObject<VgoResource>(resouceChunkData.ToArray(), isBson: true);
                }
                else
                {
                    throw new FormatException($"[COMP] ResourceChunkTypeId: {resourceChunkTypeId}");
                }

                if (vgoResource is null)
                {
                    throw new FormatException($"[{resourceChunkTypeId}] resource is null.");
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

            if (vgoResource.uri is null || vgoResource.uri == string.Empty)
            {
                throw new FormatException($"[{resourceChunkTypeId}] uri is null or empty.");
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
        protected virtual VgoCryptV0? GetVgoCrypt(VgoChunkTypeID cryptChunkTypeId, VgoIndexChunkDataElement[] chunkIndexMap, ArraySegment<byte> allSegmentBytes)
        {
            try
            {
                ArraySegment<byte> cryptChunkData = ExtractChunkData(cryptChunkTypeId, chunkIndexMap, allSegmentBytes);

                VgoCryptV0? vgoCrypt;

                if (cryptChunkTypeId == VgoChunkTypeID.CRAJ)
                {
                    // JSON
                    vgoCrypt = DeserializeObject<VgoCryptV0>(cryptChunkData.ToArray(), isBson: false);
                }
                else if (cryptChunkTypeId == VgoChunkTypeID.CRAB)
                {
                    // BSON
                    vgoCrypt = DeserializeObject<VgoCryptV0>(cryptChunkData.ToArray(), isBson: true);
                }
                else
                {
                    throw new Exception($"{nameof(cryptChunkTypeId)}: {cryptChunkTypeId}");
                }

                return vgoCrypt;
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
    }
}
