// ----------------------------------------------------------------------
// @Namespace : NewtonVgo.Buffers
// @Class     : IByteBuffer
// ----------------------------------------------------------------------
namespace NewtonVgo.Buffers
{
    using System;

    /// <summary>
    /// ByteBuffer Interface
    /// </summary>
    public interface IByteBuffer
    {
        #region Properties

        /// <summary>Gets or sets the number of bytes allocated for this buffer.</summary>
        int Capacity { get; }

        /// <summary>Gets or sets the current length of the buffer in bytes.</summary>
        int Length { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appends data to the buffer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="stride"></param>
        /// <returns>Returns the size of the appended data.</returns>
        int Append<T>(ArraySegment<T> data, int stride = 0) where T : struct;

        /// <summary>
        /// Get the used byte data in the buffer.
        /// </summary>
        /// <returns>Returns the used byte data.</returns>
        ArraySegment<byte> GetBytes();

        /// <summary>
        /// Writes the buffer data to a byte array.
        /// </summary>
        /// <returns>A new byte array.</returns>
        byte[] ToArray();

        #endregion
    }
}
