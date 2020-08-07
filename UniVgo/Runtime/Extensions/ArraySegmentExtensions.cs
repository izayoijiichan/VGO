// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : ArraySegmentExtensions
// ----------------------------------------------------------------------
namespace UniVgo
{
    using System;
    using System.Runtime.InteropServices;
    using VgoGltf.Buffers;

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
        public static ArraySegment<T> Slice<T>(this ArraySegment<T> self, int offset, int count)
        {
            return new ArraySegment<T>(self.Array, self.Offset + offset, count);
        }

        /// <summary>
        /// Writes the segment data to a byte array.
        /// </summary>
        /// <param name="self"></param>
        /// <returns>A new byte array.</returns>
        public static byte[] ToArray(this ArraySegment<byte> self)
        {
            byte[] destinationArray = new byte[self.Count];

            Array.Copy(self.Array, self.Offset, destinationArray, 0, self.Count);

            return destinationArray;
        }
    }
}
