// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoChunk
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using NewtonVgo.Buffers;
    using System;

    /// <summary>
    /// VGO Chunk
    /// </summary>
    public class VgoChunk
    {
        #region Constructors

        /// <summary>
        /// Create a new instance of VgoChunk with chunkTypeId and chunkData.
        /// </summary>
        /// <param name="chunkTypeId"></param>
        /// <param name="chunkData"></param>
        public VgoChunk(VgoChunkTypeID chunkTypeId, IByteBuffer chunkData)
        {
            PaddingCount = CalculatePaddingCount(chunkData.Length, quotient: 2);

            TypeId = chunkTypeId;
            DataLength = (uint)(chunkData.Length + PaddingCount);
            ChunkData = chunkData;
        }

        #endregion

        #region Fields

        /// <summary>The chunk type ID.</summary>
        public readonly VgoChunkTypeID TypeId;

        /// <summary>Length of the chunk data.</summary>
        public readonly uint DataLength = 0;

        /// <summary>The chunk data.</summary>
        public readonly IByteBuffer? ChunkData = default;

        #endregion

        #region Properties

        /// <summary>Total length of this chunk.</summary>
        public uint AllLength => 8 + DataLength;

        /// <summary>Number of bytes of padding for chunk data.</summary>
        /// <remarks>0 or 1</remarks>
        public int PaddingCount { get; protected set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Convert chunk to a byte array.
        /// </summary>
        /// <returns>An array of byte.</returns>
        public byte[] ConvertToByteArray()
        {
            byte[] chunkBytes = new byte[8 + DataLength];

            byte[] typeIdBytes = BitConverter.GetBytes((uint)TypeId);
            byte[] dataLengthBytes = BitConverter.GetBytes(DataLength);

            // @notice Array.Copy(sourceArray, sourceIndex, destinationArray, destinationIndex length);
            Array.Copy(typeIdBytes, 0, chunkBytes, 0, 4);
            Array.Copy(dataLengthBytes, 0, chunkBytes, 4, 4);

            if (ChunkData != null)
            {
                Array.Copy(ChunkData.ToArray(), 0, chunkBytes, 8, ChunkData.Length);

                if (PaddingCount == 1)
                {
                    int paddingStartIndex = 8 + ChunkData.Length;

                    if ((TypeId == VgoChunkTypeID.AIPJ) ||
                        (TypeId == VgoChunkTypeID.LAPJ) ||
                        (TypeId == VgoChunkTypeID.RAPJ) ||
                        (TypeId == VgoChunkTypeID.REPJ))
                    {
                        chunkBytes[paddingStartIndex] = (byte)0x20;  // space
                    }
                    else
                    {
                        chunkBytes[paddingStartIndex] = (byte)0x00;
                    }
                }
            }

            return chunkBytes;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Calculate the number of paddings.
        /// </summary>
        /// <param name="targetCount"></param>
        /// <param name="quotient"></param>
        /// <returns>The number of paddings.</returns>
        /// <remarks>
        /// When the quotient is 4, the padding is 0, 1, 2, 3.
        /// </remarks>
        protected int CalculatePaddingCount(int targetCount, int quotient)
        {
            int remainder = targetCount % quotient;

            int paddingCount = (remainder == 0) ? 0 : quotient - remainder;

            return paddingCount;
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
