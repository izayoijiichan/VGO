// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Struct    : Vector4Ushort
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Vector4 (ushort)
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vector4Ushort
    {
        /// <summary></summary>
        public ushort X;

        /// <summary></summary>
        public ushort Y;

        /// <summary></summary>
        public ushort Z;

        /// <summary></summary>
        public ushort W;

        /// <summary>
        /// Create a new instance of Vector4Ushort with x and y and z and w.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        public Vector4Ushort(ushort x, ushort y, ushort z, ushort w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
    }
}
