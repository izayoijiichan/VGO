// ----------------------------------------------------------------------
// @Namespace : NewtonVgo.Buffers
// @Class     : ArraySegmentByteBuffer
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo.Buffers
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Array Segment Byte Buffer
    /// </summary>
    public class ArraySegmentByteBuffer : IByteBuffer
    {
        #region Fields

        /// <summary>The bytes in the buffer.</summary>
        protected byte[] _Bytes;

        /// <summary>The default byte size to extend when the buffer runs out of size.</summary>
        protected readonly int _DefaultExtendSize = 10 * 1024;

        #endregion

        #region Properties

        /// <summary>Gets or sets the number of bytes allocated for this buffer.</summary>
        public int Capacity { get; private set; }

        /// <summary>Gets or sets the current length of the buffer in bytes.</summary>
        public int Length { get; private set; }

        /// <summary>Gets the maximum capacity of this instance.</summary>
        public int MaxCapacity { get; } = 2 * 1024 * 1024;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of ArraySegmentByteBuffer with capacity.
        /// </summary>
        /// <param name="capacity">Specifies the initial size of the buffer.</param>
        public ArraySegmentByteBuffer(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }

            _Bytes = new byte[capacity];

            Length = 0;

            Capacity = capacity;
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
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (data.Count == 0)
            {
                throw new ArgumentException(nameof(data));
            }

            int perDataSize = Marshal.SizeOf(typeof(T));

            if (stride == 0)
            {
                stride = perDataSize;
            }
            else if (stride < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(stride));
            }
            else if (stride < perDataSize)
            {
                throw new InvalidOperationException();
            }

            int dataCount = data.Count;

            int dataSize = stride * dataCount;

            ExtendBufferIfNecessary(dataSize);

            if (stride == perDataSize)
            {
                using (Pin<T> pin = Pin.Create(data))
                {
                    Marshal.Copy(pin.Ptr, _Bytes, Length, dataSize);
                }

                Length += dataSize;

                return dataSize;
            }
            else
            {
                int start = Length;

                for (int index = 0; index < dataCount; index++)
                {
                    ArraySegment<T> src = new ArraySegment<T>(data.Array, data.Offset + index, perDataSize);

                    using (Pin<T> pin = Pin.Create(src))
                    {
                        Marshal.Copy(pin.Ptr, _Bytes, start + index * stride, perDataSize);
                    }
                }

                Length += dataSize;

                return dataSize;
            }
        }

        /// <summary>
        /// Get the used byte data in the buffer.
        /// </summary>
        /// <returns>Returns the used byte data.</returns>
        public ArraySegment<byte> GetBytes()
        {
            //if (m_bytes == null)
            //{
            //    return new ArraySegment<byte>();
            //}
            return new ArraySegment<byte>(_Bytes, 0, Length);
        }

        /// <summary>
        /// Writes the buffer data to a byte array.
        /// </summary>
        /// <returns>A new byte array.</returns>
        public byte[] ToArray()
        {
            byte[] destinationArray = new byte[Length];

            Array.Copy(sourceArray: _Bytes, destinationArray, Length);

            return destinationArray;
        }

        #endregion 

        #region Private Methods

        /// <summary>
        /// Extend the buffer if necessary.
        /// </summary>
        /// <param name="dataSizeToWrite">The data size to be written.</param>
        private void ExtendBufferIfNecessary(int dataSizeToWrite)
        {
            if (Length + dataSizeToWrite > Capacity)
            {
                int extendSize = (dataSizeToWrite > _DefaultExtendSize) ? dataSizeToWrite : _DefaultExtendSize;

                ExtendBuffer(extendSize);
            }
        }

        /// <summary>
        /// Extend the buffer.
        /// </summary>
        /// <param name="extendSize">The size to extend.</param>
        private void ExtendBuffer(int extendSize)
        {
            int newCapacity = Length + extendSize;

            byte[] newBuffer = new byte[newCapacity];

            Buffer.BlockCopy(_Bytes, 0, newBuffer, 0, Length);

            _Bytes = newBuffer;

            Capacity = newCapacity;
        }

        #endregion 
    }
}