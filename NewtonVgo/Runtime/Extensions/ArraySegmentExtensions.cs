// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : ArraySegmentExtensions
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using NewtonVgo.Buffers;
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// ArraySegment Extensions
    /// </summary>
    public static class ArraySegmentExtensions
    {
        /// <summary>
        /// Copies data from unmanaged memory into a managed array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        /// <returns>The copied byte size.</returns>
        public static int MarshalCopyTo<T>(this ArraySegment<byte> src, T[] dst) where T : struct
        {
            if (src.Array is null)
            {
                ThrowHelper.ThrowArgumentException(nameof(src));

                return 0;
            }

            if (src.Count == 0)
            {
                return 0;
            }

            int copySize = Marshal.SizeOf(typeof(T)) * dst.Length;

            using (Pin<T> pin = Pin.Create(dst))
            {
                Marshal.Copy(src.Array, src.Offset, pin.Ptr, copySize);
            }

            return copySize;
        }

        /// <summary>
        /// Forms a slice out of the current segment starting at a specified index for a specified length.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="offset">The index at which to begin the slice.</param>
        /// <param name="count">The number of elements to include in the slice.</param>
        /// <returns></returns>
        public static ArraySegment<T> Slice<T>(this ArraySegment<T> self, in int offset, in int count)
        {
            if (self.Array is null)
            {
                ThrowHelper.ThrowArgumentException(nameof(self));

                return self;
            }

            if (self.Count == 0)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(nameof(self));
            }

            return new ArraySegment<T>(self.Array, self.Offset + offset, count);
        }

        /// <summary>
        /// Writes the segment data to a byte array.
        /// </summary>
        /// <param name="self"></param>
        /// <returns>A new byte array.</returns>
        public static byte[] ToArray(this ArraySegment<byte> self)
        {
            if (self.Array is null)
            {
                return Array.Empty<byte>();
            }

            if (self.Count == 0)
            {
                return Array.Empty<byte>();
            }

            byte[] destinationArray = new byte[self.Count];

            Array.Copy(self.Array, self.Offset, destinationArray, 0, self.Count);

            return destinationArray;
        }
    }
}
