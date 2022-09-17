// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Struct    : Vector4Byte
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Vector4 (byte)
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vector4Ubyte
    {
        /// <summary></summary>
        public byte X;

        /// <summary></summary>
        public byte Y;

        /// <summary></summary>
        public byte Z;

        /// <summary></summary>
        public byte W;

        /// <summary>
        /// Create a new instance of Vector4Ubyte with x and y and z and w.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        public Vector4Ubyte(byte x, byte y, byte z, byte w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
    }
}
