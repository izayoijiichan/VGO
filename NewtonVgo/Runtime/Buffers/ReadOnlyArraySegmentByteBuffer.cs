// ----------------------------------------------------------------------
// @Namespace : NewtonVgo.Buffers
// @Class     : ReadOnlyArraySegmentByteBuffer
// ----------------------------------------------------------------------
namespace NewtonVgo.Buffers
{
    using System;

    /// <summary>
    /// Read only Array Segment Byte Buffer
    /// </summary>
    public class ReadOnlyArraySegmentByteBuffer : IByteBuffer
    {
        #region Fields

        /// <summary>The bytes in the buffer.</summary>
        private readonly ArraySegment<byte> _Bytes;

        #endregion

        #region Properties

        /// <summary>Gets or sets the number of bytes allocated for this buffer.</summary>
        public int Capacity => _Bytes.Count;

        /// <summary>Gets or sets the current length of the buffer in bytes.</summary>
        public int Length { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of ReadOnlyArraySegmentByteBuffer with bytes.
        /// </summary>
        /// <param name="bytes">The data to store in the buffer.</param>
        public ReadOnlyArraySegmentByteBuffer(ArraySegment<byte> bytes)
        {
            _Bytes = bytes;

            Length = bytes.Count;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appends data to the buffer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="stride"></param>
        /// <returns>Returns the size of the appended data.</returns>
        public int Append<T>(ArraySegment<T> data, int stride = 0) where T : struct
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the used byte data in the buffer.
        /// </summary>
        /// <returns>Returns the used byte data.</returns>
        public ArraySegment<byte> GetBytes()
        {
            return _Bytes;
        }

        /// <summary>
        /// Writes the buffer data to a byte array.
        /// </summary>
        /// <returns>A new byte array.</returns>
        public byte[] ToArray()
        {
            byte[] destinationArray = new byte[_Bytes.Count];

            Array.Copy(sourceArray: _Bytes.Array, _Bytes.Offset, destinationArray, 0, _Bytes.Count);

            return destinationArray;
        }

        #endregion
    }
}