﻿// ----------------------------------------------------------------------
// @Namespace : NewtonVgo.Buffers
// @Class     : ReadOnlyArraySegmentByteBuffer
// ----------------------------------------------------------------------
#nullable enable
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
        public int Length => _Bytes.Count;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of ReadOnlyArraySegmentByteBuffer with bytes.
        /// </summary>
        /// <param name="bytes">The data to store in the buffer.</param>
        public ReadOnlyArraySegmentByteBuffer(byte[] bytes)
        {
            _Bytes = new ArraySegment<byte>(bytes);
        }

        /// <summary>
        /// Create a new instance of ReadOnlyArraySegmentByteBuffer with bytes.
        /// </summary>
        /// <param name="bytes">The data to store in the buffer.</param>
        public ReadOnlyArraySegmentByteBuffer(ArraySegment<byte> bytes)
        {
            _Bytes = bytes;
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
            ThrowHelper.ThrowNotImplementedException();

            return default;
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
            if (_Bytes.Array is null)
            {
                return Array.Empty<byte>();
            }

            if (_Bytes.Count == 0)
            {
                return Array.Empty<byte>();
            }

            byte[] destinationArray = new byte[_Bytes.Count];

            Array.Copy(sourceArray: _Bytes.Array, _Bytes.Offset, destinationArray, 0, _Bytes.Count);

            return destinationArray;
        }

        #endregion
    }
}