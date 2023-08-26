// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoReadChunk
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    /// <summary>
    /// VGO Read Chunk
    /// </summary>
    public class VgoReadChunk
    {
        #region Fields

        /// <summary>The chunk type ID.</summary>
        public readonly VgoChunkTypeID _TypeId;

        /// <summary>Length of the chunk data.</summary>
        public readonly uint _DataLength;

        /// <summary>The chunk data.</summary>
        public readonly byte[] _ChunkData;

        #endregion

        #region Properties

        /// <summary>The chunk type ID.</summary>
        public VgoChunkTypeID TypeId => _TypeId;

        /// <summary>Length of the chunk data.</summary>
        public uint DataLength => _DataLength;

        /// <summary>The chunk data.</summary>
        public byte[] ChunkData => _ChunkData;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of VgoReadChunk.
        /// </summary>
        /// <param name="chunkTypeId">The chunk type ID.</param>
        /// <param name="chunkData">The chunk data.</param>
        public VgoReadChunk(VgoChunkTypeID chunkTypeId, byte[] chunkData)
        {
            _TypeId = chunkTypeId;

            _DataLength = (uint)chunkData.Length;

            _ChunkData = chunkData;
        }

        /// <summary>
        /// Create a new instance of VgoReadChunk.
        /// </summary>
        /// <param name="chunkTypeId">The chunk type ID.</param>
        /// <param name="chunkDataLength">Length of the chunk data.</param>
        /// <param name="chunkData">The chunk data.</param>
        public VgoReadChunk(VgoChunkTypeID chunkTypeId, uint chunkDataLength, byte[] chunkData)
        {
            _TypeId = chunkTypeId;

            _DataLength = chunkDataLength;

            _ChunkData = chunkData;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return TypeId.ToString();
        }

        #endregion
    }
}
