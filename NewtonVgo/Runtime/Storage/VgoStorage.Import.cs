// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoStorage
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// VGO Storage
    /// </summary>
    public partial class VgoStorage : IVgoStorage
    {
        #region Public Methods (Sync)

        /// <summary>
        /// Parse vgo.
        /// </summary>
        /// <param name="vgoFilePath">The file path of the vgo.</param>
        /// <param name="vgkFilePath">The file path of the crypt key.</param>
        public virtual void ParseVgo(in string vgoFilePath, in string? vgkFilePath)
        {
            if (vgoFilePath == null)
            {
#if NET_STANDARD_2_1
                ThrowHelper.ThrowArgumentNullException(nameof(vgoFilePath));
#else
                throw new ArgumentNullException(nameof(vgoFilePath));
#endif
            }

            var vgoFileInfo = new FileInfo(vgoFilePath);

            if (vgoFileInfo.Exists == false)
            {
                ThrowHelper.ThrowFileNotFoundException(vgoFilePath);
            }

            using var vgoStream = new FileStream(vgoFilePath, FileMode.Open, FileAccess.Read);

            if (vgkFilePath == null)
            {
                ParseVgo(vgoStream, vgkStream: null);

                return;
            }

            var vgkFileInfo = new FileInfo(vgkFilePath);

            if (vgkFileInfo.Exists == false)
            {
                ThrowHelper.ThrowFileNotFoundException(vgkFilePath);
            }

            using var vgkStream = new FileStream(vgkFilePath, FileMode.Open, FileAccess.Read);

            ParseVgo(vgoStream, vgkStream);
        }

        /// <summary>
        /// Parse vgo.
        /// </summary>
        /// <param name="vgoBytes">The vgo bytes.</param>
        /// <param name="vgkBytes">The vgk bytes.</param>
        public virtual void ParseVgo(in byte[] vgoBytes, in byte[]? vgkBytes)
        {
            using var vgoStream = new MemoryStream(vgoBytes);

            ParseVgo(vgoStream, vgkBytes);
        }

        /// <summary>
        /// Parse vgo.
        /// </summary>
        /// <param name="vgoStream">The vgo stream.</param>
        /// <param name="vgkBytes">The vgk bytes.</param>
        public virtual void ParseVgo(in Stream vgoStream, in byte[]? vgkBytes)
        {
            using var vgoStreamReader = new VgoStreamReader(vgoStream, vgkBytes);

            ParseVgoInternal(vgoStreamReader, hasVgk: vgkBytes != null);
        }

        /// <summary>
        /// Parse vgo.
        /// </summary>
        /// <param name="vgoStream">The vgo stream.</param>
        /// <param name="vgkStream">The vgk stream.</param>
        public virtual void ParseVgo(in Stream vgoStream, in Stream? vgkStream)
        {
            using var vgoStreamReader = new VgoStreamReader(vgoStream, vgkStream);

            ParseVgoInternal(vgoStreamReader, hasVgk: vgkStream != null);
        }

        #endregion

        #region Public Methods (Async)

        /// <summary>
        /// Parse vgo.
        /// </summary>
        /// <param name="vgoFilePath">The file path of the vgo.</param>
        /// <param name="vgkFilePath">The file path of the crypt key.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns></returns>
        public virtual async Task ParseVgoAsync(string vgoFilePath, string? vgkFilePath, CancellationToken cancellationToken)
        {
            if (vgoFilePath == null)
            {
#if NET_STANDARD_2_1
                ThrowHelper.ThrowArgumentNullException(nameof(vgoFilePath));
#else
                throw new ArgumentNullException(nameof(vgoFilePath));
#endif
            }

            var vgoFileInfo = new FileInfo(vgoFilePath);

            if (vgoFileInfo.Exists == false)
            {
                ThrowHelper.ThrowFileNotFoundException(vgoFilePath);
            }

            using var vgoStream = new FileStream(vgoFilePath, FileMode.Open, FileAccess.Read);

            if (vgkFilePath == null)
            {
                await ParseVgoAsync(vgoStream, vgkStream: null, cancellationToken);

                return;
            }

            var vgkFileInfo = new FileInfo(vgkFilePath);

            if (vgkFileInfo.Exists == false)
            {
                ThrowHelper.ThrowFileNotFoundException(vgkFilePath);
            }

            using var vgkStream = new FileStream(vgkFilePath, FileMode.Open, FileAccess.Read);

            await ParseVgoAsync(vgoStream, vgkStream, cancellationToken);
        }

        /// <summary>
        /// Parse vgo.
        /// </summary>
        /// <param name="vgoBytes">The vgo bytes.</param>
        /// <param name="vgkBytes">The vgk bytes.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns></returns>
        public virtual async Task ParseVgoAsync(byte[] vgoBytes, byte[]? vgkBytes, CancellationToken cancellationToken)
        {
            using var vgoStream = new MemoryStream(vgoBytes);

            await ParseVgoAsync(vgoStream, vgkBytes, cancellationToken);
        }

        /// <summary>
        /// Parse vgo.
        /// </summary>
        /// <param name="vgoStream">The vgo stream.</param>
        /// <param name="vgkBytes">The vgk bytes.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns></returns>
        public virtual async Task ParseVgoAsync(Stream vgoStream, byte[]? vgkBytes, CancellationToken cancellationToken)
        {
            using var vgoStreamReader = new VgoStreamReader(vgoStream, vgkBytes);

            await ParseVgoInternalAsync(vgoStreamReader, hasVgk: vgkBytes != null, cancellationToken);
        }

        /// <summary>
        /// Parse vgo.
        /// </summary>
        /// <param name="vgoStream">The vgo stream.</param>
        /// <param name="vgkStream">The vgk stream.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns></returns>
        public virtual async Task ParseVgoAsync(Stream vgoStream, Stream? vgkStream, CancellationToken cancellationToken)
        {
            using var vgoStreamReader = new VgoStreamReader(vgoStream, vgkStream);

            await ParseVgoInternalAsync(vgoStreamReader, hasVgk: vgkStream != null, cancellationToken);
        }

        #endregion

        #region Protected Methods (Import)

        /// <summary>
        /// Parse vgo.
        /// </summary>
        /// <param name="vgoStreamReader">The vgo stream reader.</param>
        /// <param name="hasVgk">Whether or not you have vgk.</param>
        protected virtual void ParseVgoInternal(in IVgoStreamReader vgoStreamReader, in bool hasVgk)
        {
            // Header
            Header = vgoStreamReader.ReadHeader();

            if ((Header.IsRequireResourceAccessorExternalCryptKey == 1) && (hasVgk == false))
            {
                ThrowHelper.ThrowException("cryptKey is required.");
            }

            // Index chunk
            ChunkIndexMap = vgoStreamReader.ReadIndexChunk();

            // Composer chunk
            _ = vgoStreamReader.ReadComposerChunk();

            // Asset Info chunk
            AssetInfo = vgoStreamReader.ReadAssetInfo();

            // Layout chunk
            Layout = vgoStreamReader.ReadLayout()!;

            // Resource Accessor chunk
            ResourceAccessors = vgoStreamReader.ReadResourceAccessor();

            // Resource chunk
            SegmentResourceData = vgoStreamReader.ReadSegmentResource(ResourceAccessors);
        }

        /// <summary>
        /// Parse vgo.
        /// </summary>
        /// <param name="vgoStreamReader">The vgo stream reader.</param>
        /// <param name="hasVgk">Whether or not you have vgk.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns></returns>
        protected virtual async Task ParseVgoInternalAsync(IVgoStreamReader vgoStreamReader, bool hasVgk, CancellationToken cancellationToken)
        {
            // Header
            Header = vgoStreamReader.ReadHeader();

            if ((Header.IsRequireResourceAccessorExternalCryptKey == 1) && (hasVgk == false))
            {
                ThrowHelper.ThrowException("cryptKey is required.");
            }

            // Index chunk
            ChunkIndexMap = vgoStreamReader.ReadIndexChunk();

            // Composer chunk
            _ = vgoStreamReader.ReadComposerChunk();

            // Asset Info chunk
            AssetInfo = vgoStreamReader.ReadAssetInfo();

            // Layout chunk
            Layout = vgoStreamReader.ReadLayout()!;

            // Resource Accessor chunk
            ResourceAccessors = vgoStreamReader.ReadResourceAccessor();

            // Resource chunk
            SegmentResourceData = await vgoStreamReader.ReadSegmentResourceAsync(ResourceAccessors, cancellationToken);
        }

        #endregion
    }
}
