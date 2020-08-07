// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : GltfStorage
// ----------------------------------------------------------------------
namespace UniVgo
{
    using NewtonGltf;
    using NewtonGltf.Serialization;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using VgoGltf;
    using VgoGltf.Buffers;

    /// <summary>
    /// glTF Storage
    /// </summary>
    public class GltfStorage
    {
        #region Fields

        /// <summary>The glTF header.</summary>
        protected GltfHeader Header = default;

        /// <summary>The glTF chunk0</summary>
        protected GltfChunk Chunk0 = null;

        /// <summary>The glTF chunk1</summary>
        protected GltfChunk Chunk1 = null;

        /// <summary>The JSON string.</summary>
        protected string JsonString = null;

        #endregion

        #region Properties

        /// <summary>The glTF.</summary>
        public Gltf Gltf { get; protected set; }

        /// <summary>The buffer data.</summary>
        public List<IByteBuffer> BufferData { get; protected set; }

        /// <summary>The directory path.</summary>
        /// <remarks>for .gltf</remarks>
        public string DirectoryPath { get; protected set; }

        #endregion

        #region Constants

        /// <summary>Magic</summary>
        /// <remarks>"glTF"</remarks>
        public const uint GltfMagic = 0x46546C67;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of GltfStorage with filePath.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <remarks>for Import</remarks>
        public GltfStorage(string filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            FileInfo fileInfo = new FileInfo(filePath);

            if (fileInfo.Exists == false)
            {
                throw new FileNotFoundException(filePath);
            }

            byte[] allBytes = File.ReadAllBytes(filePath);

            if (fileInfo.Extension == ".gltf")
            {
                DirectoryPath = fileInfo.DirectoryName;

                ParseGltf(allBytes);
            }
            else
            {
                ParseGlb(allBytes);
            }
        }

        /// <summary>
        /// Create a new instance of GltfStorage with allBytes.
        /// </summary>
        /// <param name="allBytes">All bytes of file.</param>
        /// <remarks>
        /// for Import
        /// @notice Not available for ".gltf".
        /// </remarks>
        public GltfStorage(byte[] allBytes)
        {
            ParseGlb(allBytes);
        }

        /// <summary>
        /// Create a new instance of GltfStorage with gltf.
        /// </summary>
        /// <param name="gltf">The Gltf object.</param>
        /// <remarks>for Export</remarks>
        public GltfStorage(Gltf gltf)
        {
            Gltf = gltf ?? throw new ArgumentNullException(nameof(gltf));

            BufferData = new List<IByteBuffer>();
        }

        #endregion

        #region Protected Methods (Import)

        /// <summary>
        /// Parse glb.
        /// </summary>
        /// <param name="allBytes">All bytes of ".glb" file.</param>
        protected virtual void ParseGlb(byte[] allBytes)
        {
            if (allBytes == null)
            {
                throw new ArgumentNullException(nameof(allBytes));
            }

            if (allBytes.Length == 0)
            {
                throw new ArgumentException();
            }

            if (allBytes.Length < 12)
            {
                throw new FormatException();
            }

            ArraySegment<byte> segmentBytes = new ArraySegment<byte>(allBytes);

            int pos = 0;

            // Header (12-byte)
            try
            {
                Header.Magic = BitConverter.ToUInt32(allBytes, pos);

                pos += 4;

                Header.Version = BitConverter.ToUInt32(allBytes, pos);

                pos += 4;

                Header.Length = BitConverter.ToUInt32(allBytes, pos);

                pos += 4;

                if (Header.Magic != GltfMagic)
                {
                    throw new FormatException("Header: magic");
                }

                if (Header.Version != 2)
                {
                    throw new FormatException("Header: version");
                }

                if (Header.Length != (uint)allBytes.Length)
                {
                    throw new FormatException("Header: length");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Chunk0 (JSON)
            try
            {
                uint uiChunkLength = BitConverter.ToUInt32(allBytes, pos);

                pos += 4;

                if (uiChunkLength <= 0)
                {
                    throw new FormatException("Chunk0: chunkLength");
                }

                uint uiChunkType = BitConverter.ToUInt32(allBytes, pos);

                pos += 4;

                if (uiChunkType != (uint)GltfChunkType.Json)
                {
                    throw new FormatException("Chunk0: chunkType");
                }

                ArraySegment<byte> chunkData = segmentBytes.Slice(pos, (int)uiChunkLength);

                pos += (int)uiChunkLength;

                Chunk0 = new GltfChunk(GltfChunkType.Json, new ReadOnlyArraySegmentByteBuffer(chunkData));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //Chunk1
            try
            {
                if (pos == allBytes.Length)
                {
                    Chunk1 = null;
                }
                else
                {
                    uint uiChunkLength = BitConverter.ToUInt32(allBytes, pos);

                    pos += 4;

                    if (uiChunkLength <= 0)
                    {
                        throw new FormatException("Chunk1: chunkLength");
                    }

                    uint uiChunkType = BitConverter.ToUInt32(allBytes, pos);

                    pos += 4;

                    if ((uiChunkType != (uint)GltfChunkType.Json) &&
                        (uiChunkType != (uint)GltfChunkType.Bin))
                    {
                        throw new FormatException("Chunk1: chunkType");
                    }

                    ArraySegment<byte> chunkData = segmentBytes.Slice(pos, (int)uiChunkLength);

                    pos += (int)uiChunkLength;

                    if (pos != allBytes.Length)
                    {
                        throw new FormatException("Chunk1: chunkData");
                    }

                    if (uiChunkType == (uint)GltfChunkType.Bin)
                    {
                        // GLB (Binary Buffer)
                        Chunk1 = new GltfChunk(GltfChunkType.Bin, new ReadOnlyArraySegmentByteBuffer(chunkData));
                    }
                    else
                    {
                        throw new FormatException("Chunk1: chunkType");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // JSON -> glTF
            try
            {
                string jsonString = Encoding.UTF8.GetString(Chunk0.ChunkData.ToArray(), 0, Chunk0.ChunkData.Length).TrimEnd();
#if UNITY_EDITOR
                JsonString = jsonString;
#endif
                var gltfJsonConverter = new GltfJsonConverter();

                Gltf = gltfJsonConverter.ConvertJsonToGltf(jsonString);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Check glTF
            {
                if (Gltf.asset.version != "2.0")
                {
                    throw new FormatException($"glTF.asset.version: {Gltf.asset.version}");
                }

                if (Gltf.buffers.Count != 1)
                {
                    throw new FormatException($"glTF.buffers.count: {Gltf.buffers.Count}");
                }

                if (Gltf.buffers[0].byteLength != Chunk1.ChunkData.Length)
                {
                    // @notice Data in Chunk1 may not include padding.
                    //throw new FormatException($"glTF.buffers[0].dataSize: {Gltf.buffers[0].byteLength}");
                }
            }

            // Buffer
            BufferData = new List<IByteBuffer>
            {
                Chunk1.ChunkData
            };
        }

        /// <summary>
        /// Parse gltf.
        /// </summary>
        /// <param name="allBytes">All bytes of ".gltf" file.</param>
        protected virtual void ParseGltf(byte[] allBytes)
        {
            if (allBytes == null)
            {
                throw new ArgumentNullException(nameof(allBytes));
            }

            if (DirectoryPath == null)
            {
                throw new Exception();
            }

            // JSON -> glTF
            try
            {
                string jsonString = Encoding.UTF8.GetString(allBytes, 0, allBytes.Length);
#if UNITY_EDITOR
                JsonString = jsonString;
#endif
                var gltfJsonConverter = new GltfJsonConverter();

                Gltf = gltfJsonConverter.ConvertJsonToGltf(jsonString);

                Chunk0 = new GltfChunk(GltfChunkType.Json, new ReadOnlyArraySegmentByteBuffer(new ArraySegment<byte>(allBytes)));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Check glTF
            {
                if (Gltf.asset.version != "2.0")
                {
                    throw new FormatException($"glTF.asset.version: {Gltf.asset.version}");
                }

                for (int bufferIndex = 0; bufferIndex < Gltf.buffers.Count; bufferIndex++)
                {
                    if (string.IsNullOrEmpty(Gltf.buffers[bufferIndex].uri))
                    {
                        throw new FormatException($"glTF.buffers[{bufferIndex}].uri: null");
                    }
                }

                for (int imageIndex = 0; imageIndex < Gltf.images.Count; imageIndex++)
                {
                    if (Gltf.images[imageIndex].bufferView != -1)
                    {
                        throw new FormatException($"glTF.images[{imageIndex}].bufferView: {Gltf.images[imageIndex].bufferView}");
                    }

                    if (string.IsNullOrEmpty(Gltf.images[imageIndex].uri))
                    {
                        throw new FormatException($"glTF.images[{imageIndex}].uri: null");
                    }
                }
            }

            BufferData = new List<IByteBuffer>();

            for (int bufferIndex = 0; bufferIndex < Gltf.buffers.Count; bufferIndex++)
            {
                GltfBuffer gltfBuffer = Gltf.buffers[bufferIndex];

                byte[] binData = GetUriData(gltfBuffer.uri);

                IByteBuffer buffer = new ReadOnlyArraySegmentByteBuffer(new ArraySegment<byte>(binData));

                BufferData.Add(buffer);
            }
        }

        #endregion

        #region Public Methods (Import)

        /// <summary>
        /// Get URI data.
        /// </summary>
        /// <param name="uri">The uri.</param>
        /// <returns>An byte array of uri data.</returns>
        public virtual byte[] GetUriData(string uri)
        {
            return GetUriDataAsync(uri).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Get URI data.
        /// </summary>
        /// <param name="uri">The uri.</param>
        /// <returns></returns>
        public virtual async Task<byte[]> GetUriDataAsync(string uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(uri);
            }

            if (uri.StartsWith("data:"))
            {
                Regex regex = new Regex("^data:[a-z-]+/[a-z-]+;base64,");

                Match match = regex.Match(uri);

                if (match.Success == false)
                {
                    throw new FormatException($"uri: {uri}");
                }

                string mediaType = uri.Substring("data:".Length, uri.IndexOf(";base64") - "data:".Length);

                string base64String = uri.Substring(match.Length);

                byte[] base64Bytes = Convert.FromBase64String(base64String);

                if ((mediaType == "image/jpeg") ||
                    (mediaType == "image/png") ||
                    (mediaType == "application/octet-stream"))  // .bin
                {
                    return base64Bytes;
                }
                else
                {
                    //throw new NotSupportedException(mediaType);
                    return base64Bytes;
                }

            }
            else if (
                uri.StartsWith("http://") ||
                uri.StartsWith("https://") ||
                uri.StartsWith("file://"))
            {
                using (HttpClient httpClient = new HttpClient())
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri))
                using (HttpResponseMessage response = await httpClient.SendAsync(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        byte[] byteArray = await response.Content.ReadAsByteArrayAsync();

                        return byteArray;
                    }
                    else
                    {
                        throw new Exception(response.StatusCode.ToString());
                    }
                }
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
        /// Exports a GLB format file.
        /// </summary>
        /// <param name="filePath">The full path of the file.</param>
        /// <returns>Returns true if the export was successful, false otherwise.</returns>
        public virtual bool ExportGlbFile(string filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (Chunk0 == null)
            {
                if (Gltf == null)
                {
                    throw new Exception();
                }
                Chunk0 = CreateChunk0(Gltf);
            }

            if (Chunk1 == null)
            {
                if (BufferData == null)
                {
                    throw new Exception();
                }
                if (BufferData.Count != 1)
                {
                    throw new Exception();
                }
                Chunk1 = new GltfChunk(GltfChunkType.Bin, BufferData[0]);
            }

            Header.Magic = GltfMagic;
            Header.Version = 2;
            Header.Length = 12 + Chunk0.AllLength + Chunk1.AllLength;

            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 8192, useAsync: true))
            using (var writer = new BinaryWriter(stream))
            {
                // Header (12-byte)
                try
                {
                    writer.Write(Header.Magic);
                    writer.Write(Header.Version);
                    writer.Write(Header.Length);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                // Chunk0 (JSON)
                try
                {
                    byte[] paddingedData = Chunk0.GetPaddingedData();

                    // ChunkLength
                    writer.Write((uint)paddingedData.Length);

                    // ChunkType
                    writer.Write((uint)Chunk0.ChunkType);

                    // ChunkData
                    stream.Write(paddingedData, 0, paddingedData.Length);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                // Chunk1 (Binary Buffer)
                try
                {
                    byte[] paddingedData = Chunk1.GetPaddingedData();

                    // ChunkLength
                    writer.Write((uint)paddingedData.Length);

                    // ChunkType
                    writer.Write((uint)Chunk1.ChunkType);

                    // ChunkData
                    stream.Write(paddingedData, 0, paddingedData.Length);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                stream.Flush();
            }

            return true;
        }

        #endregion

        #region Protected Methods (Export)

        /// <summary>
        /// Create chunk0 from gltf.
        /// </summary>
        /// <param name="gltf">The Gltf object.</param>
        /// <returns>The chunk0 object.</returns>
        protected virtual GltfChunk CreateChunk0(Gltf gltf)
        {
            try
            {
                var gltfJsonConverter = new GltfJsonConverter();

                // glTF -> JSON
                string jsonString = gltfJsonConverter.ConvertGltfToJsonString(gltf);

#if UNITY_EDITOR
                JsonString = jsonString;
#endif

                byte[] json = Encoding.UTF8.GetBytes(jsonString);

                IByteBuffer chunkData = new ReadOnlyArraySegmentByteBuffer(new ArraySegment<byte>(json));

                GltfChunk chunk0 = new GltfChunk(GltfChunkType.Json, chunkData);

                return chunk0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
