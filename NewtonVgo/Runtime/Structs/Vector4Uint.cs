// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Struct    : Vector4Uint
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Vector4 (uint)
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vector4Uint
    {
        /// <summary></summary>
        public uint X;

        /// <summary></summary>
        public uint Y;

        /// <summary></summary>
        public uint Z;

        /// <summary></summary>
        public uint W;

        /// <summary>
        /// Create a new instance of Vector4Uint with x and y and z and w.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        public Vector4Uint(uint x, uint y, uint z, uint w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
    }
}
