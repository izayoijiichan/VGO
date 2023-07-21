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
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// VGO Storage
    /// </summary>
    public partial class VgoStorage : IVgoStorage
    {
        #region Public Methods (Export)

        /// <summary>
        /// Exports a VGO format file.
        /// </summary>
        /// <param name="filePath">The full path of the file.</param>
        /// <param name="exportSetting">A vgo export setting.</param>
        /// <returns>Returns true if the export was successful, false otherwise.</returns>
        public virtual bool ExportVgoFile(in string filePath, in VgoExportSetting exportSetting)
        {
            ThrowHelper.ThrowExceptionIfArgumentIsNull(nameof(filePath), filePath);

            if (AssetInfo == null)
            {
                ThrowHelper.ThrowException();

                return false;
            }

            if (Layout == null)
            {
                ThrowHelper.ThrowException();

                return false;
            }

            if (ResourceAccessors == null)
            {
                ThrowHelper.ThrowException();

                return false;
            }

            if (Resource == null)
            {
                ThrowHelper.ThrowException();

                return false;
            }

            if (exportSetting.Validate(out IReadOnlyList<string> errorMessages) == false)
            {
                ThrowHelper.ThrowException(string.Join("\n", errorMessages));

                return false;
            }

            FileInfo fileInfo = new FileInfo(filePath);

            // Asset Info chunk
            VgoChunk assetInfoChunk = CreateAssetInfoChunk(exportSetting.AssetInfoTypeId);

            // Layout chunk
            VgoChunk layoutChunk = CreateLayoutChunk(exportSetting.LayoutTypeId);

            // Resource Accessor chunk
            (VgoChunk resourceAccessorChunk, VgoChunk? resourceAccessorCryptChunk) =
                CreateResourceAccessorChunk(
                    exportSetting.ResourceAccessorTypeId,
                    exportSetting.ResourceAccessorCryptTypeId,
                    exportSetting.ResourceAccessorCryptAlgorithm,
                    exportSetting.ResourceAccessorCryptKey
                );

            // Resource chunk
            VgoChunk resourceChunk = CreateResourceChunk(exportSetting.ResourceTypeId, fileInfo, exportSetting.BinFileName, exportSetting.ResourceUri);

            // Composer chunk
            VgoChunk composerChunk = CreateComposerChunk(
                exportSetting.AssetInfoTypeId,
                exportSetting.LayoutTypeId,
                exportSetting.ResourceAccessorTypeId,
                exportSetting.ResourceAccessorCryptTypeId,
                exportSetting.ResourceTypeId
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
            if (exportSetting.ResourceAccessorCryptTypeId != VgoChunkTypeID.None)
            {
                Header.ResourceAccessorIsCrypted = 1;

                if (exportSetting.ResourceAccessorCryptKey != null)
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

            if (exportSetting.ResourceAccessorCryptKey != null)
            {
                string keyFileName = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length) + ".vgk";

                string keyFullPath = Path.Combine(fileInfo.DirectoryName!, keyFileName);

                // Output (.vgk)
                using (var stream = new FileStream(keyFullPath, FileMode.Create, FileAccess.Write, FileShare.None))
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(exportSetting.ResourceAccessorCryptKey);

                    writer.Flush();
                }
            }

            if (exportSetting.ResourceTypeId != VgoChunkTypeID.REPb)
            {
                if (exportSetting.BinFileName is null)
                {
                    ThrowHelper.ThrowArgumentException(nameof(exportSetting), "exportSetting.BinFileName is null.");

                    return false;
                }

                string binFullPath = Path.Combine(fileInfo.DirectoryName!, exportSetting.BinFileName);  // @notice binFileName

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

        #region Protected Methods (Export) Asset Info chunk

        /// <summary>
        /// Create an asset info chunk.
        /// </summary>
        /// <param name="assetInfoTypeId">The asset info chunk type ID.</param>
        /// <returns>An asset info chunk.</returns>
        public virtual VgoChunk CreateAssetInfoChunk(in VgoChunkTypeID assetInfoTypeId)
        {
            IByteBuffer assetInfoChunkData;

            try
            {
                if (assetInfoTypeId == VgoChunkTypeID.AIPJ)
                {
                    // JSON
                    byte[] json = SerializeObject(AssetInfo, isBson: false);

                    assetInfoChunkData = new ReadOnlyArraySegmentByteBuffer(new ArraySegment<byte>(json));
                }
                else if (assetInfoTypeId == VgoChunkTypeID.AIPB)
                {
                    // BSON
                    byte[] bson = SerializeObject(AssetInfo, isBson: true);

                    assetInfoChunkData = new ReadOnlyArraySegmentByteBuffer(new ArraySegment<byte>(bson));
                }
                else
                {
#if NET_STANDARD_2_1
                    ThrowHelper.ThrowException($"{nameof(assetInfoTypeId)}: {assetInfoTypeId}");

                    return default;
#else
                    throw new Exception($"{nameof(assetInfoTypeId)}: {assetInfoTypeId}");
#endif
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

            VgoChunk assetInfoChunk = new VgoChunk(assetInfoTypeId, assetInfoChunkData);

            return assetInfoChunk;
        }

        #endregion

        #region Protected Methods (Export) Layout chunk

        /// <summary>
        /// Create a layout chunk.
        /// </summary>
        /// <param name="layoutTypeId">The layout chunk type ID.</param>
        /// <returns>A layout chunk.</returns>
        protected virtual VgoChunk CreateLayoutChunk(in VgoChunkTypeID layoutTypeId)
        {
            IByteBuffer layoutChunkData;

            try
            {
                if (layoutTypeId == VgoChunkTypeID.LAPJ)
                {
                    // JSON
                    byte[] json = SerializeObject(Layout, isBson: false);

                    layoutChunkData = new ReadOnlyArraySegmentByteBuffer(new ArraySegment<byte>(json));
                }
                else if (layoutTypeId == VgoChunkTypeID.LAPB)
                {
                    // BSON
                    byte[] bson = SerializeObject(Layout, isBson: true);

                    layoutChunkData = new ReadOnlyArraySegmentByteBuffer(new ArraySegment<byte>(bson));
                }
                else
                {
#if NET_STANDARD_2_1
                    ThrowHelper.ThrowException($"{nameof(layoutTypeId)}: {layoutTypeId}");

                    return default;
#else
                    throw new Exception($"{nameof(layoutTypeId)}: {layoutTypeId}");
#endif
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

            VgoChunk layoutChunk = new VgoChunk(layoutTypeId, layoutChunkData);

            return layoutChunk;
        }

        #endregion

        #region Protected Methods (Export) Resource Accessor chunk

        /// <summary>
        /// Create a resource accessor chunk.
        /// </summary>
        /// <param name="resourceAccessorTypeId">The resource accessor chunk type ID.</param>
        /// <param name="resourceAccessorCryptTypeId">The resource accessor crypt chunk type ID.</param>
        /// <param name="cryptAlgorithm">The crypt algorithm.</param>
        /// <param name="cryptKey">The crypt key.</param>
        /// <returns>A resource accessor chunk and a crypt chunk.</returns>
        protected virtual (VgoChunk, VgoChunk?) CreateResourceAccessorChunk(
            in VgoChunkTypeID resourceAccessorTypeId,
            in VgoChunkTypeID resourceAccessorCryptTypeId,
            in string? cryptAlgorithm = null,
            in byte[]? cryptKey = null)
        {
            IByteBuffer resourceAccessorChunkData;
            IByteBuffer? resourceAccessorCryptChunkData = null;

            try
            {
                byte[] plainResourceAccessorJsonOrBson;

                if ((resourceAccessorTypeId == VgoChunkTypeID.RAPJ) ||
                    (resourceAccessorTypeId == VgoChunkTypeID.RACJ))
                {
                    // JSON
                    plainResourceAccessorJsonOrBson = SerializeObject(ResourceAccessors, isBson: false);
                }
                else if (
                    (resourceAccessorTypeId == VgoChunkTypeID.RAPB) ||
                    (resourceAccessorTypeId == VgoChunkTypeID.RACB))
                {
                    // BSON
                    plainResourceAccessorJsonOrBson = SerializeObject(ResourceAccessors, isBson: true);
                }
                else
                {
#if NET_STANDARD_2_1
                    ThrowHelper.ThrowException($"{nameof(resourceAccessorTypeId)}: {resourceAccessorTypeId}");

                    return (default, default);
#else
                    throw new Exception($"{nameof(resourceAccessorTypeId)}: {resourceAccessorTypeId}");
#endif
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

                        string? keyString;
                        string? ivString;

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
#if NET_STANDARD_2_1
                        ThrowHelper.ThrowException($"resourceAccessorCryptAlgorithm: {cryptAlgorithm}");

                        return (default, default);
#else
                        throw new Exception($"resourceAccessorCryptAlgorithm: {cryptAlgorithm}");
#endif
                    }

                    byte[] cryptInfoJsonOrBson;

                    if (resourceAccessorCryptTypeId == VgoChunkTypeID.CRAJ)
                    {
                        cryptInfoJsonOrBson = SerializeObject(cryptInfo, isBson: false);
                    }
                    else if (resourceAccessorCryptTypeId == VgoChunkTypeID.CRAB)
                    {
                        cryptInfoJsonOrBson = SerializeObject(cryptInfo, isBson: true);
                    }
                    else
                    {
#if NET_STANDARD_2_1
                        ThrowHelper.ThrowException($"{nameof(resourceAccessorCryptTypeId)}: {resourceAccessorCryptTypeId}");

                        return (default, default);
#else
                        throw new Exception($"{nameof(resourceAccessorCryptTypeId)}: {resourceAccessorCryptTypeId}");
#endif
                    }

                    resourceAccessorChunkData = new ReadOnlyArraySegmentByteBuffer(new ArraySegment<byte>(encryptedResourceAccessorJsonOrBson));
                    resourceAccessorCryptChunkData = new ReadOnlyArraySegmentByteBuffer(new ArraySegment<byte>(cryptInfoJsonOrBson));
                }
                else
                {
#if NET_STANDARD_2_1
                    ThrowHelper.ThrowException($"{nameof(resourceAccessorTypeId)}: {resourceAccessorTypeId}");

                    return (default, default);
#else
                    throw new Exception($"{nameof(resourceAccessorTypeId)}: {resourceAccessorTypeId}");
#endif
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

            VgoChunk resourceAccessorChunk = new VgoChunk(resourceAccessorTypeId, resourceAccessorChunkData);

            VgoChunk? resourceAccessorCryptChunk = null;

            if (resourceAccessorCryptChunkData != null)
            {
                resourceAccessorCryptChunk = new VgoChunk(resourceAccessorCryptTypeId, resourceAccessorCryptChunkData);
            }

            return (resourceAccessorChunk, resourceAccessorCryptChunk);
        }

        #endregion

        #region Protected Methods (Export) Resource chunk

        /// <summary>
        /// Create a resource chunk.
        /// </summary>
        /// <param name="resourceTypeId">The resource chunk type ID.</param>
        /// <param name="fileInfo">The file info.</param>
        /// <param name="binFileName">The resource binary file name.</param>
        /// <param name="resourceUri">The resource URI.</param>
        /// <returns>A resouce chunk.</returns>
        protected virtual VgoChunk CreateResourceChunk(in VgoChunkTypeID resourceTypeId, in FileInfo fileInfo, string? binFileName = null, string? resourceUri = null)
        {
            binFileName ??= fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length) + ".bin";

            resourceUri ??= binFileName;

            if (Resource == null)
            {
#if NET_STANDARD_2_1
                ThrowHelper.ThrowException();
#else
                throw new Exception();
#endif
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
                    byte[] resourceJson = SerializeObject(vgoResource, isBson: false);

                    resourceChunkData = new ReadOnlyArraySegmentByteBuffer(new ArraySegment<byte>(resourceJson));
                }
                else if (resourceTypeId == VgoChunkTypeID.REPB)
                {
                    // BSON
                    byte[] resourceBson = SerializeObject(vgoResource, isBson: true);

                    resourceChunkData = new ReadOnlyArraySegmentByteBuffer(new ArraySegment<byte>(resourceBson));
                }
                else
                {
#if NET_STANDARD_2_1
                    ThrowHelper.ThrowException($"{nameof(resourceTypeId)}: {resourceTypeId}");

                    return default;
#else
                    throw new Exception($"{nameof(resourceTypeId)}: {resourceTypeId}");
#endif
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

            VgoChunk resourceChunk = new VgoChunk(resourceTypeId, resourceChunkData);

            return resourceChunk;
        }

        #endregion

        #region Protected Methods (Export) Composer chunk

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
            in VgoChunkTypeID assetInfoTypeId,
            in VgoChunkTypeID layoutTypeId,
            in VgoChunkTypeID resourceAccessorTypeId,
            in VgoChunkTypeID resourceAccessorCryptTypeId,
            in VgoChunkTypeID resourceTypeId)
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

        #endregion

        #region Protected Methods (Export) Index chunk

        /// <summary>
        /// Create a index chunk.
        /// </summary>
        /// <param name="chunkList">List of chunk.</param>
        /// <returns>A index chunk.</returns>
        protected virtual VgoChunk CreateIndexChunk(in List<VgoChunk> chunkList)
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
