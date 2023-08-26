// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoStreamReader
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using NewtonVgo.Security.Cryptography;
    using NewtonVgo.Serialization;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// VGO Stream Reader
    /// </summary>
    public class VgoStreamReader : IVgoStreamReader
    {
        #region Fields

        /// <summary>The vgo stream.</summary>
        protected Stream _VgoStream;

        /// <summary>The vgk bytes.</summary>
        protected byte[] _VgkBytes;

        /// <summary>The vgo stream lock object.</summary>
        protected readonly object _VgoStreamLockObject = new object();

        /// <summary>The vgo binary reader.</summary>
        protected BinaryReader _VgoBinaryReader;

        /// <summary>Vgo BSON Serializer.</summary>
        protected readonly VgoBsonSerializer _VgoBsonSerializer = new VgoBsonSerializer();

        /// <summary>Vgo JSON Serializer settings.</summary>
        protected readonly VgoJsonSerializerSettings _VgoJsonSerializerSettings = new VgoJsonSerializerSettings();

        /// <summary>The file header.</summary>
        protected VgoHeader? _Header = null;

        /// <summary>The index map of chunks.</summary>
        protected VgoIndexChunkDataElement[]? _ChunkIndexMap = null;

        /// <summary>The composer chunk data.</summary>
        protected VgoComposerChunkData? _ComposerChunkData = null;

        /// <summary></summary>
        protected bool _Disposed;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of VgoStreamReader with vgo stream.
        /// </summary>
        /// <param name="vgoStream">The vgo stream.</param>
        public VgoStreamReader(in Stream vgoStream)
        {
            _VgoStream = vgoStream;

            _VgkBytes = Array.Empty<byte>();

            _VgoBinaryReader = new BinaryReader(_VgoStream);
        }

        /// <summary>
        /// Create a new instance of VgoStreamReader with vgo stream and vgk stream.
        /// </summary>
        /// <param name="vgoStream">The vgo stream.</param>
        /// <param name="vgkStream">The vgk stream.</param>
        public VgoStreamReader(in Stream vgoStream, in Stream? vgkStream)
        {
            _VgoStream = vgoStream;

            if (vgkStream == null)
            {
                _VgkBytes = Array.Empty<byte>();
            }
            else
            {
                if (vgkStream.Length != 32)
                {
                    ThrowHelper.ThrowArgumentOutOfRangeException(nameof(vgkStream), (int)vgkStream.Length, min: 32, max: 32);
                }

                byte[] vgkBytes = vgkStream.ReadBytes(32);

                if (vgkBytes.Length != 32)
                {
                    ThrowHelper.ThrowIOException();
                }

                _VgkBytes = vgkBytes;
            }

            _VgoBinaryReader = new BinaryReader(_VgoStream);
        }

        /// <summary>
        /// Create a new instance of VgoStreamReader with vgo stream and vgk bytes.
        /// </summary>
        /// <param name="vgoStream">The vgo stream.</param>
        /// <param name="vgkBytes">The vgk bytes.</param>
        public VgoStreamReader(in Stream vgoStream, in byte[]? vgkBytes)
        {
            _VgoStream = vgoStream;

            if (vgkBytes == null)
            {
                _VgkBytes = Array.Empty<byte>();
            }
            else
            {
                if (vgkBytes.Length != 32)
                {
                    ThrowHelper.ThrowArgumentOutOfRangeException(nameof(vgkBytes), vgkBytes.Length, min: 32, max: 32);
                }

                _VgkBytes = vgkBytes;
            }

            _VgoBinaryReader = new BinaryReader(_VgoStream);
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
            Dispose(disposing: true);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (_Disposed)
            {
                return;
            }

            if (disposing)
            {
                _VgoBinaryReader.Dispose();

                //_VgoStream.Dispose();
            }

            _Disposed = true;
        }

        #endregion

        #region Protected Methods (Common chunk)

        /// <summary>
        /// Read chunk.
        /// </summary>
        /// <param name="chunkTypeId">The chunk type ID.</param>
        /// <returns>The vgo read chunk.</returns>
        /// <remarks>This method is not available for header and index chunks.</remarks>
        protected virtual VgoReadChunk ReadChunk(in VgoChunkTypeID chunkTypeId)
        {
            if (_ChunkIndexMap == null)
            {
                _ = ReadIndexChunk();

                if (_ChunkIndexMap == null)
                {
#if NET_STANDARD_2_1
                    ThrowHelper.ThrowFormatException();
#else
                    throw new FormatException();
#endif
                }
            }

            VgoIndexChunkDataElement chunkInfo = GetIndexChunkDataElement(chunkTypeId, _ChunkIndexMap);

            return ReadChunk(chunkTypeId, chunkInfo.ByteOffset, chunkInfo.BytePadding);
        }

        /// <summary>
        /// Read chunk.
        /// </summary>
        /// <param name="chunkTypeId">The chunk type ID.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>The vgo read chunk.</returns>
        /// <remarks>This method is not available for header and index chunks.</remarks>
        protected virtual async Task<VgoReadChunk> ReadChunkAsync(VgoChunkTypeID chunkTypeId, CancellationToken cancellationToken)
        {
            if (_ChunkIndexMap == null)
            {
                _ = ReadIndexChunk();

                if (_ChunkIndexMap == null)
                {
#if NET_STANDARD_2_1
                    ThrowHelper.ThrowFormatException();
#else
                    throw new FormatException();
#endif
                }
            }

            VgoIndexChunkDataElement chunkInfo = GetIndexChunkDataElement(chunkTypeId, _ChunkIndexMap);

            return await ReadChunkAsync(chunkTypeId, chunkInfo.ByteOffset, chunkInfo.BytePadding, cancellationToken);
        }

        /// <summary>
        /// Read chunk.
        /// </summary>
        /// <param name="chunkTypeId">The chunk type ID.</param>
        /// <param name="byteOffset">The byte offset.</param>
        /// <param name="bytePadding">The byte padding.</param>
        /// <returns>The vgo read chunk.</returns>
        protected virtual VgoReadChunk ReadChunk(in VgoChunkTypeID chunkTypeId, in uint byteOffset, in byte bytePadding)
        {
            uint typeId;
            uint chunkDataLength;
            byte[] chunkData;

            lock (_VgoStreamLockObject)
            {
                _VgoStream.Seek(byteOffset, SeekOrigin.Begin);

                typeId = _VgoBinaryReader.ReadUInt32();

                if (typeId != (uint)chunkTypeId)
                {
                    ThrowHelper.ThrowFormatException($"[{chunkTypeId}] Chunk.TypeId: {chunkTypeId}");
                }

                chunkDataLength = _VgoBinaryReader.ReadUInt32();

                if (chunkDataLength == 0)
                {
                    ThrowHelper.ThrowFormatException($"[{chunkTypeId}] Chunk.DataLength: {chunkDataLength}");
                }

                int effectiveDataLength = (int)chunkDataLength - bytePadding;

                chunkData = _VgoBinaryReader.ReadBytes(effectiveDataLength);

                if (chunkData.Length != effectiveDataLength)
                {
                    ThrowHelper.ThrowIOException($"[{chunkTypeId}] readSize: {effectiveDataLength}");
                }
            }

            return new VgoReadChunk(chunkTypeId, chunkDataLength, chunkData);
        }

        /// <summary>
        /// Read chunk.
        /// </summary>
        /// <param name="chunkTypeId">The chunk type ID.</param>
        /// <param name="byteOffset">The byte offset.</param>
        /// <param name="bytePadding">The byte padding.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>The vgo read chunk.</returns>
        protected virtual async Task<VgoReadChunk> ReadChunkAsync(
            VgoChunkTypeID chunkTypeId, uint byteOffset, byte bytePadding, CancellationToken cancellationToken)
        {
            uint typeId;
            uint chunkDataLength;
            byte[] chunkData;

            //lock (_VgoStreamLockObject)
            {
                _VgoStream.Seek(byteOffset, SeekOrigin.Begin);

                typeId = _VgoBinaryReader.ReadUInt32();

                if (typeId != (uint)chunkTypeId)
                {
                    ThrowHelper.ThrowFormatException($"[{chunkTypeId}] Chunk.TypeId: {chunkTypeId}");
                }

                chunkDataLength = _VgoBinaryReader.ReadUInt32();

                if (chunkDataLength == 0)
                {
                    ThrowHelper.ThrowFormatException($"[{chunkTypeId}] Chunk.DataLength: {chunkDataLength}");
                }

                int effectiveDataLength = (int)chunkDataLength - bytePadding;

                chunkData = await _VgoStream.ReadBytesAsync(effectiveDataLength, cancellationToken);

                if (chunkData.Length != effectiveDataLength)
                {
                    ThrowHelper.ThrowIOException($"[{chunkTypeId}] readSize: {effectiveDataLength}");
                }
            }

            return new VgoReadChunk(chunkTypeId, chunkDataLength, chunkData);
        }

        #endregion

        #region Public Methods (Header chunk)

        /// <summary>
        /// Read header chunk.
        /// </summary>
        /// <returns>The header chunk.</returns>
        public virtual VgoHeader ReadHeader()
        {
            if (_Header.HasValue)
            {
                return _Header.Value;
            }

            try
            {
                int headerSize = Marshal.SizeOf(typeof(VgoHeader));

                byte[] headerBytes;

                lock (_VgoStreamLockObject)
                {
                    _VgoStream.Seek(0, SeekOrigin.Begin);

                    headerBytes = _VgoStream.ReadBytes(headerSize);
                }

                if (headerBytes.Length != 16)
                {
                    ThrowHelper.ThrowIOException($"[Header] readSize: {headerBytes.Length}");
                }

                VgoHeader header = headerBytes.ConvertToStructure<VgoHeader>();

                if (header.Magic != (uint)VgoChunkTypeID.Vgo)
                {
                    ThrowHelper.ThrowFormatException($"[Header] Magic: {header.Magic}");
                }

                if (header.DataLength != 8)
                {
                    ThrowHelper.ThrowFormatException($"[Header] DataLength: {header.DataLength}");
                }

                if (header.MajorVersion == 0)
                {
                    ThrowHelper.ThrowFormatException($"[Header] MajorVersion: {header.MajorVersion}");
                }

                if (header.GeometryCoordinate != VgoGeometryCoordinate.RightHanded &&
                    header.GeometryCoordinate != VgoGeometryCoordinate.LeftHanded)
                {
                    ThrowHelper.ThrowFormatException($"[Header] GeometryCoordinate: {header.GeometryCoordinate}");
                }

                if (header.UVCoordinate != VgoUVCoordinate.TopLeft &&
                    header.UVCoordinate != VgoUVCoordinate.BottomLeft)
                {
                    ThrowHelper.ThrowFormatException($"[Header] UVCoordinate: {header.UVCoordinate}");
                }

                _Header = header;

                return header;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Public Methods (Index chunk)

        /// <summary>
        /// Read index chunk data.
        /// </summary>
        /// <returns>The index chunk.</returns>
        public virtual VgoIndexChunkDataElement[] ReadIndexChunk()
        {
            if (_ChunkIndexMap != null)
            {
                return _ChunkIndexMap;
            }

            if (_Header == null)
            {
                _ = ReadHeader();
            }

            try
            {
                VgoReadChunk indexChunk = ReadChunk(VgoChunkTypeID.Idx, byteOffset: 16, bytePadding: 0);

                int elementSize = Marshal.SizeOf(typeof(VgoIndexChunkDataElement));

                if ((indexChunk.DataLength % elementSize) != 0)
                {
                    ThrowHelper.ThrowFormatException($"[IDX] Chunk.DataLength: {indexChunk.DataLength}");
                }

                Span<VgoIndexChunkDataElement> elementSpan = MemoryMarshal.Cast<byte, VgoIndexChunkDataElement>(indexChunk.ChunkData);

                _ChunkIndexMap = elementSpan.ToArray();

                return _ChunkIndexMap;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Protected Methods (Index chunk)

        /// <summary>
        /// Get the index chunk data of the specified chunk type ID from the chunk index map.
        /// </summary>
        /// <param name="chunkTypeId">The chunk type ID.</param>
        /// <param name="chunkIndexMap">The chunk index map.</param>
        /// <returns>An index chunk data element.</returns>
        protected virtual VgoIndexChunkDataElement GetIndexChunkDataElement(VgoChunkTypeID chunkTypeId, VgoIndexChunkDataElement[] chunkIndexMap)
        {
            int count = chunkIndexMap.Count(x => x.ChunkTypeId == chunkTypeId);

            if (count == 0)
            {
                ThrowHelper.ThrowFormatException($"{chunkTypeId} is not define in IDX chunk.");
            }

            if (count >= 2)
            {
                ThrowHelper.ThrowFormatException($"{chunkTypeId} is defined more than once in IDX chunk.");
            }

            VgoIndexChunkDataElement chunkInfo = chunkIndexMap.First(x => x.ChunkTypeId == chunkTypeId);

            if (chunkInfo.ByteOffset == 0)
            {
                ThrowHelper.ThrowFormatException($"[{chunkTypeId}] Chunk.ByteOffset: {chunkInfo.ByteOffset}");
            }

            if (chunkInfo.ByteLength == 0)
            {
                //ThrowHelper.ThrowFormatException($"[{chunkTypeId}] Chunk.ByteLength: {chunkInfo.ByteLength}");
            }

            if ((chunkInfo.ByteOffset + chunkInfo.ByteLength) > _VgoStream.Length)
            {
                ThrowHelper.ThrowFormatException($"[{chunkTypeId}] Chunk.ByteLength: {chunkInfo.ByteLength} is out of the range.");
            }

            return chunkInfo;
        }

        #endregion

        #region Public Methods (Composer chunk)

        /// <summary>
        /// Read composer chunk.
        /// </summary>
        /// <returns>The composer chunk data.</returns>
        public virtual VgoComposerChunkData ReadComposerChunk()
        {
            if (_ComposerChunkData.HasValue)
            {
                return _ComposerChunkData.Value;
            }

            try
            {
                VgoReadChunk composerChunk = ReadChunk(VgoChunkTypeID.COMP);

                VgoComposerChunkData composerChunkData = composerChunk.ChunkData.ConvertToStructure<VgoComposerChunkData>();

                _ComposerChunkData = composerChunkData;

                return composerChunkData;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Public Methods (Asset Info chunk)

        /// <summary>
        /// Read asset info chunk.
        /// </summary>
        /// <returns>The vgo asset info.</returns>
        public virtual VgoAssetInfo? ReadAssetInfo()
        {
            if (_ComposerChunkData == null)
            {
                _ = ReadComposerChunk();

                if (_ComposerChunkData == null)
                {
                    ThrowHelper.ThrowFormatException();

                    return null;
                }
            }

            try
            {
                VgoChunkTypeID chunkTypeId = _ComposerChunkData.Value.AssetInfoChunkTypeId;

                VgoReadChunk assetInfoChunk = ReadChunk(chunkTypeId);

                VgoAssetInfo? assetInfo = null;

                if (chunkTypeId is VgoChunkTypeID.AIPJ)
                {
                    // JSON
                    assetInfo = DeserializeObject<VgoAssetInfo>(assetInfoChunk.ChunkData, isBson: false);
                }
                else if (chunkTypeId is VgoChunkTypeID.AIPB)
                {
                    // BSON
                    assetInfo = DeserializeObject<VgoAssetInfo>(assetInfoChunk.ChunkData, isBson: true);
                }
                else
                {
                    ThrowHelper.ThrowFormatException($"[COMP] AssetInfoChunkTypeId: {chunkTypeId}");
                }

                return assetInfo;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Public Methods (Layout chunk)

        /// <summary>
        /// Read layout chunk.
        /// </summary>
        /// <returns>The vgo layout.</returns>
        public virtual VgoLayout? ReadLayout()
        {
            if (_ComposerChunkData == null)
            {
                _ = ReadComposerChunk();

                if (_ComposerChunkData == null)
                {
                    ThrowHelper.ThrowFormatException();

                    return null;
                }
            }

            try
            {
                VgoChunkTypeID chunkTypeId = _ComposerChunkData.Value.LayoutChunkTypeId;

                VgoReadChunk layoutChunk = ReadChunk(chunkTypeId);

                VgoLayout? layout = null;

                if (chunkTypeId is VgoChunkTypeID.LAPJ)
                {
                    // JSON
                    layout = DeserializeObject<VgoLayout>(layoutChunk.ChunkData, isBson: false);
                }
                else if (chunkTypeId is VgoChunkTypeID.LAPB)
                {
                    // BSON
                    layout = DeserializeObject<VgoLayout>(layoutChunk.ChunkData, isBson: true);
                }
                else
                {
                    ThrowHelper.ThrowFormatException($"[COMP] LayoutChunkTypeId: {chunkTypeId}");
                }

                return layout;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Public Methods (Resource Accessor chunk)

        /// <summary>
        /// Read resource accessor chunk.
        /// </summary>
        /// <returns>List of resource accessor.</returns>
        public virtual List<VgoResourceAccessor>? ReadResourceAccessor()
        {
            if (_ComposerChunkData == null)
            {
                _ = ReadComposerChunk();

                if (_ComposerChunkData == null)
                {
                    ThrowHelper.ThrowFormatException();

                    return null;
                }
            }

            try
            {
                VgoChunkTypeID raChunkTypeId = _ComposerChunkData.Value.ResourceAccessorChunkTypeId;

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

                VgoReadChunk resourceAccessorChunk = ReadChunk(raChunkTypeId);

                byte[]? plainJsonOrBson;

                if (raChunkTypeId is VgoChunkTypeID.RAPJ)
                {
                    plainJsonOrBson = resourceAccessorChunk.ChunkData;
                }
                else if (raChunkTypeId is VgoChunkTypeID.RAPB)
                {
                    plainJsonOrBson = resourceAccessorChunk.ChunkData;
                }
                else if (
                    (raChunkTypeId == VgoChunkTypeID.RACJ) ||
                    (raChunkTypeId == VgoChunkTypeID.RACB))
                {
                    VgoCryptV0? vgoCrypt = ReadCryptChunk();

                    if (vgoCrypt is null)
                    {
                        ThrowHelper.ThrowFormatException($" ResourceAccessorChunkTypeId: {raChunkTypeId}");

                        return null;
                    }

                    try
                    {
                        byte[] encryptedJsonOrBson = resourceAccessorChunk.ChunkData;

                        if (vgoCrypt.algorithms == VgoCryptographyAlgorithms.AES)
                        {
                            // AES
                            var aesCrypter = new AesCrypter()
                            {
                                CipherMode = vgoCrypt.cipherMode,
                                PaddingMode = vgoCrypt.paddingMode,
                            };

                            byte[] key;

                            if (_VgkBytes is null || _VgkBytes.Any() == false)
                            {
                                if (string.IsNullOrEmpty(vgoCrypt.key))
                                {
                                    ThrowHelper.ThrowException("crypt key is unknown.");

                                    return default;
                                }
                                else
                                {
                                    key = Convert.FromBase64String(vgoCrypt.key);
                                }
                            }
                            else
                            {
                                key = _VgkBytes;
                            }

                            if (key is null || key.Any() == false)
                            {
                                ThrowHelper.ThrowException("crypt key is unknown.");

                                return default;
                            }

                            if (string.IsNullOrEmpty(vgoCrypt.iv))
                            {
                                ThrowHelper.ThrowException("iv is unknown.");

                                return default;
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
                            ThrowHelper.ThrowNotSupportedException($"CryptographyAlgorithms: {vgoCrypt.algorithms}");

                            return default;
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
                    ThrowHelper.ThrowFormatException($"[COMP] ResourceAccessorChunkTypeId: {raChunkTypeId}");

                    return default;
                }

                List<VgoResourceAccessor>? vgoResourceAccessors = null;
                
                try
                {
                    if (plainJsonOrBson is null)
                    {
                        ThrowHelper.ThrowException("plainJsonOrBson is null.");

                        return default;
                    }

                    if (raChunkTypeId == VgoChunkTypeID.RAPJ ||
                        raChunkTypeId == VgoChunkTypeID.RACJ)
                    {
                        // JSON
                        vgoResourceAccessors = DeserializeObject<List<VgoResourceAccessor>>(plainJsonOrBson, isBson: false);
                    }
                    else if (
                        raChunkTypeId == VgoChunkTypeID.RAPB ||
                        raChunkTypeId == VgoChunkTypeID.RACB)
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
            catch
            {
                throw;
            }
        }

        #endregion

        #region Public Methods (Resource chunk)

        /// <summary>
        /// Read resource chunk.
        /// </summary>
        /// <returns>The resource chunk data.</returns>
        public virtual byte[] ReadResource()
        {
            if (_ComposerChunkData == null)
            {
                _ = ReadComposerChunk();

                if (_ComposerChunkData == null)
                {
                    ThrowHelper.ThrowFormatException();

                    return Array.Empty<byte>();
                }
            }

            try
            {
                VgoChunkTypeID resourceChunkTypeId = _ComposerChunkData.Value.ResourceChunkTypeId;

                switch (resourceChunkTypeId)
                {
                    case VgoChunkTypeID.REPb:
                    case VgoChunkTypeID.REPJ:
                    case VgoChunkTypeID.REPB:
                        break;
                    default:
                        throw new FormatException($"[COMP] ResourceChunkTypeId: {resourceChunkTypeId}");
                }

                VgoReadChunk resourceChunk = ReadChunk(resourceChunkTypeId);

                if (resourceChunkTypeId is VgoChunkTypeID.REPb)
                {
                    return resourceChunk.ChunkData;
                }

                VgoResource? vgoResource;

                if (resourceChunkTypeId == VgoChunkTypeID.REPJ)
                {
                    // JSON
                    vgoResource = DeserializeObject<VgoResource>(resourceChunk.ChunkData, isBson: false);
                }
                else if (resourceChunkTypeId == VgoChunkTypeID.REPB)
                {
                    // BSON
                    vgoResource = DeserializeObject<VgoResource>(resourceChunk.ChunkData, isBson: true);
                }
                else
                {
                    ThrowHelper.ThrowFormatException($"[COMP] ResourceChunkTypeId: {resourceChunkTypeId}");

                    return Array.Empty<byte>();
                }

                if (vgoResource is null)
                {
                    ThrowHelper.ThrowFormatException($"[{resourceChunkTypeId}] resource is null.");

                    return Array.Empty<byte>();
                }

                if (vgoResource.uri is null || vgoResource.uri == string.Empty)
                {
                    ThrowHelper.ThrowFormatException($"[{resourceChunkTypeId}] uri is null or empty.");

                    return Array.Empty<byte>();
                }

                if (vgoResource.byteLength == 0)
                {
                    //ThrowHelper.ThrowFormatException($"[{chunkTypeId}] byteLength: {vgoResource.byteLength}");
                }

                if (_VgoStream is FileStream fileStream)
                {
                    var fileInfo = new FileInfo(fileStream.Name);

                    return GetUriData(vgoResource.uri, directory: fileInfo.Directory.FullName);
                }
                else
                {
                    return GetUriData(vgoResource.uri);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Read resource chunk.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>The resource chunk data.</returns>
        public virtual async Task<byte[]> ReadResourceAsync(CancellationToken cancellationToken)
        {
            if (_ComposerChunkData == null)
            {
                _ = ReadComposerChunk();

                if (_ComposerChunkData == null)
                {
                    ThrowHelper.ThrowFormatException();

                    return Array.Empty<byte>();
                }
            }

            try
            {
                VgoChunkTypeID resourceChunkTypeId = _ComposerChunkData.Value.ResourceChunkTypeId;

                switch (resourceChunkTypeId)
                {
                    case VgoChunkTypeID.REPb:
                    case VgoChunkTypeID.REPJ:
                    case VgoChunkTypeID.REPB:
                        break;
                    default:
                        throw new FormatException($"[COMP] ResourceChunkTypeId: {resourceChunkTypeId}");
                }

                VgoReadChunk resourceChunk = await ReadChunkAsync(resourceChunkTypeId, cancellationToken);

                if (resourceChunkTypeId is VgoChunkTypeID.REPb)
                {
                    return resourceChunk.ChunkData;
                }

                VgoResource? vgoResource;

                if (resourceChunkTypeId == VgoChunkTypeID.REPJ)
                {
                    // JSON
                    vgoResource = DeserializeObject<VgoResource>(resourceChunk.ChunkData, isBson: false);
                }
                else if (resourceChunkTypeId == VgoChunkTypeID.REPB)
                {
                    // BSON
                    vgoResource = DeserializeObject<VgoResource>(resourceChunk.ChunkData, isBson: true);
                }
                else
                {
                    ThrowHelper.ThrowFormatException($"[COMP] ResourceChunkTypeId: {resourceChunkTypeId}");

                    return Array.Empty<byte>();
                }

                if (vgoResource is null)
                {
                    ThrowHelper.ThrowFormatException($"[{resourceChunkTypeId}] resource is null.");

                    return Array.Empty<byte>();
                }

                if (vgoResource.uri is null || vgoResource.uri == string.Empty)
                {
                    ThrowHelper.ThrowFormatException($"[{resourceChunkTypeId}] uri is null or empty.");

                    return Array.Empty<byte>();
                }

                if (vgoResource.byteLength == 0)
                {
                    //ThrowHelper.ThrowFormatException($"[{chunkTypeId}] byteLength: {vgoResource.byteLength}");
                }

                if (_VgoStream is FileStream fileStream)
                {
                    var fileInfo = new FileInfo(fileStream.Name);

                    return await GetUriDataAsync(vgoResource.uri, directory: fileInfo.Directory.FullName);
                }
                else
                {
                    return await GetUriDataAsync(vgoResource.uri);
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Protected Methods (Crypt chunk)

        /// <summary>
        /// Read crypt chunk.
        /// </summary>
        /// <returns>The crypt chunk data.</returns>
        protected virtual VgoCryptV0? ReadCryptChunk()
        {
            if (_ComposerChunkData == null)
            {
                _ = ReadComposerChunk();

                if (_ComposerChunkData == null)
                {
                    ThrowHelper.ThrowFormatException();

                    return null;
                }
            }

            try
            {
                VgoChunkTypeID chunkTypeId = _ComposerChunkData.Value.ResourceAccessorCryptChunkTypeId;

                VgoReadChunk cryptChunk = ReadChunk(chunkTypeId);

                VgoCryptV0? crypt = null;

                if (chunkTypeId is VgoChunkTypeID.CRAJ)
                {
                    // JSON
                    crypt = DeserializeObject<VgoCryptV0>(cryptChunk.ChunkData, isBson: false);
                }
                else if (chunkTypeId is VgoChunkTypeID.CRAB)
                {
                    // BSON
                    crypt = DeserializeObject<VgoCryptV0>(cryptChunk.ChunkData, isBson: true);
                }
                else
                {
                    ThrowHelper.ThrowFormatException($"[COMP] ResourceAccessorCryptChunkTypeId: {chunkTypeId}");
                }

                return crypt;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Protected Methods (JSON or BSON)

        /// <summary>
        /// Deserialize JSON or BSON to a object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonOrBson">The JSON or BSON.</param>
        /// <param name="isBson">Specify true if the data is BSON.</param>
        /// <param name="rootValueAsArray">Specify true if the root value is an array.</param>
        /// <returns>A object.</returns>
        protected virtual T? DeserializeObject<T>(in byte[] jsonOrBson, in bool isBson, bool rootValueAsArray = false) where T : class
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

        #endregion

        #region Protected Methods (Helper)

        /// <summary>
        /// Get URI data.
        /// </summary>
        /// <param name="uri">The uri.</param>
        /// <param name="directory">The directory.</param>
        /// <returns>An byte array of uri data.</returns>
        protected virtual byte[] GetUriData(in string uri, string? directory = null)
        {
            ThrowHelper.ThrowExceptionIfArgumentIsNull(nameof(uri), uri);

            if (uri.StartsWith("data:"))
            {
                ThrowHelper.ThrowNotSupportedException(uri);

                return Array.Empty<byte>();
            }
            else if (
                uri.StartsWith("http://") ||
                uri.StartsWith("https://"))
            {
                using var httpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(30) };

                using var request = new HttpRequestMessage(HttpMethod.Get, uri);

                using var response = httpClient.SendAsync(request).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    byte[] byteArray = response.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();

                    return byteArray;
                }
                else
                {
                    ThrowHelper.ThrowHttpRequestException(response.StatusCode.ToString());

                    return Array.Empty<byte>();
                }
            }
            else if (uri.StartsWith("file://"))
            {
                ThrowHelper.ThrowNotSupportedException(uri);

                return Array.Empty<byte>();
            }
            else
            {
                if (directory == null)
                {
                    ThrowHelper.ThrowException();

                    return Array.Empty<byte>();
                }

                if (Directory.Exists(directory) == false)
                {
                    ThrowHelper.ThrowDirectoryNotFoundException(directory);
                }

                string binFilePath = Path.Combine(directory, uri);

                if (File.Exists(binFilePath) == false)
                {
                    ThrowHelper.ThrowFileNotFoundException(binFilePath);
                }

                //using var binFileStream = new FileStream(binFilePath, FileMode.Open, FileAccess.Read);

                byte[] binData = File.ReadAllBytes(binFilePath);

                return binData;
            }
        }

        /// <summary>
        /// Get URI data.
        /// </summary>
        /// <param name="uri">The uri.</param>
        /// <param name="directory">The directory.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>An byte array of uri data.</returns>
        protected virtual async Task<byte[]> GetUriDataAsync(string uri, string? directory = null, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowExceptionIfArgumentIsNull(nameof(uri), uri);

            cancellationToken.ThrowIfCancellationRequested();

            if (uri.StartsWith("data:"))
            {
                ThrowHelper.ThrowNotSupportedException(uri);

                return Array.Empty<byte>();
            }
            else if (
                uri.StartsWith("http://") ||
                uri.StartsWith("https://"))
            {
                using var httpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(30) };

                using var request = new HttpRequestMessage(HttpMethod.Get, uri);

                using var response = await httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    byte[] byteArray = await response.Content.ReadAsByteArrayAsync();

                    return byteArray;
                }
                else
                {
                    ThrowHelper.ThrowHttpRequestException(response.StatusCode.ToString());

                    return Array.Empty<byte>();
                }
            }
            else if (uri.StartsWith("file://"))
            {
                ThrowHelper.ThrowNotSupportedException(uri);

                return Array.Empty<byte>();
            }
            else
            {
                if (directory == null)
                {
                    ThrowHelper.ThrowException();

                    return Array.Empty<byte>();
                }

                if (Directory.Exists(directory) == false)
                {
                    ThrowHelper.ThrowDirectoryNotFoundException(directory);
                }

                string binFilePath = Path.Combine(directory, uri);

                if (File.Exists(binFilePath) == false)
                {
                    ThrowHelper.ThrowFileNotFoundException(binFilePath);
                }

                //using var binFileStream = new FileStream(binFilePath, FileMode.Open, FileAccess.Read);

#if UNITY_2021_2_OR_NEWER
                byte[] binData = await File.ReadAllBytesAsync(binFilePath, cancellationToken);
#else
                byte[] binData = File.ReadAllBytes(binFilePath);
#endif
                return binData;
            }
        }

        #endregion
    }
}
