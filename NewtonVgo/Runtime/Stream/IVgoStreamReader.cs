// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Interface : IVgoStreamReader
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// VGO Stream Reader Interface
    /// </summary>
    public interface IVgoStreamReader : IDisposable
    {
        #region Methods

        ///// <summary>
        ///// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ///// </summary>
        //void Dispose();

        /// <summary>
        /// Read header chunk.
        /// </summary>
        /// <returns>The header chunk.</returns>
        VgoHeader ReadHeader();

        /// <summary>
        /// Read index chunk data.
        /// </summary>
        /// <returns>The index chunk.</returns>
        VgoIndexChunkDataElement[] ReadIndexChunk();

        /// <summary>
        /// Read composer chunk.
        /// </summary>
        /// <returns>The composer chunk data.</returns>
        VgoComposerChunkData ReadComposerChunk();

        /// <summary>
        /// Read asset info chunk.
        /// </summary>
        /// <returns>The vgo asset info.</returns>
        VgoAssetInfo? ReadAssetInfo();

        /// <summary>
        /// Read layout chunk.
        /// </summary>
        /// <returns>The vgo layout.</returns>
        VgoLayout? ReadLayout();

        /// <summary>
        /// Read resource accessor chunk.
        /// </summary>
        /// <returns>List of resource accessor.</returns>
        List<VgoResourceAccessor>? ReadResourceAccessor();

        /// <summary>
        /// Read resource chunk.
        /// </summary>
        /// <returns>The resource chunk data.</returns>
        byte[] ReadResource();

        /// <summary>
        /// Read resource chunk.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>The resource chunk data.</returns>
        Task<byte[]> ReadResourceAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Read resource chunk.
        /// </summary>
        /// <param name="resourceAccessors">List of resource accessor.</param>
        /// <returns>The resource chunk data.</returns>
        VgoResourceDataCollection? ReadSegmentResource(List<VgoResourceAccessor>? resourceAccessors = null);

        /// <summary>
        /// Read resource chunk.
        /// </summary>
        /// <param name="resourceAccessors">List of resource accessor.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>The resource chunk data.</returns>
        Task<VgoResourceDataCollection?> ReadSegmentResourceAsync(List<VgoResourceAccessor>? resourceAccessors, CancellationToken cancellationToken);

        #endregion
    }
}
