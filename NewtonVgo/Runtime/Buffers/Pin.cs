// ----------------------------------------------------------------------
// @Namespace : NewtonVgo.Buffers
// @Class     : Pin
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo.Buffers
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Pin
    /// </summary>
    public static class Pin
    {
        /// <summary>
        /// Create a Pin for the specified source.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        /// <returns></returns>
        public static Pin<T> Create<T>(ArraySegment<T> src) where T : struct
        {
            return new Pin<T>(src);
        }

        /// <summary>
        /// Create a Pin for the specified source.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        /// <returns></returns>
        public static Pin<T> Create<T>(T[] src) where T : struct
        {
            return new Pin<T>(new ArraySegment<T>(src));
        }
    }

    /// <summary>
    /// Pin
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Pin<T> : IDisposable where T : struct
    {
        /// <summary>
        /// Create a new instance of Pin with src.
        /// </summary>
        /// <param name="src"></param>
        public Pin(ArraySegment<T> src)
        {
            this.src = src;
            pinnedArray = GCHandle.Alloc(src.Array, GCHandleType.Pinned);
        }

        //~Pin()
        //{
        //    Dispose(false);
        //}

        /// <summary></summary>
        protected bool disposedValue = false;

        /// <summary></summary>
        protected ArraySegment<T> src;

        /// <summary></summary>
        protected GCHandle pinnedArray;

        /// <summary></summary>
        public int Length => src.Count * Marshal.SizeOf(typeof(T));

        /// <summary></summary>
        public IntPtr Ptr => new IntPtr(pinnedArray.AddrOfPinnedObject().ToInt64() + src.Offset);

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            // GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposedValue == false)
            {
                if (disposing)
                {
                }

                if (pinnedArray.IsAllocated)
                {
                    pinnedArray.Free();
                }

                disposedValue = true;
            }
        }
    }
}
