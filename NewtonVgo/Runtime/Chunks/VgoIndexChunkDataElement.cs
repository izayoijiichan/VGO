// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoIndexChunkDataElement
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>The index chunk data element.</summary>
    /// <remarks>16-byte</remarks>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct VgoIndexChunkDataElement
    {
        #region Fields

        /// <summary>Chunk type ID.</summary>
        /// <remarks>4-byte</remarks>
        public VgoChunkTypeID ChunkTypeId;

        /// <summary>Start position of chunk.</summary>
        public uint ByteOffset;

        /// <summary>Total chunk length. (Including padding)</summary>
        public uint ByteLength;

        /// <summary>Number of bytes of padding for chunk data.</summary>
        public byte BytePadding;

        /// <summary></summary>
        public byte Reserved2;

        /// <summary></summary>
        public byte Reserved3;

        /// <summary></summary>
        public byte Reserved4;

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ChunkTypeId.ToString();
        }

        #endregion
    }
}
