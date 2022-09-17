// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Struct    : Color4
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Color4
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Color4
    {
        /// <summary>Red</summary>
        public float R;

        /// <summary>Green</summary>
        public float G;

        /// <summary>Blue</summary>
        public float B;

        /// <summary>Alpha</summary>
        public float A;

        /// <summary>
        /// Create a new instance of Color4 with r, g, b.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public Color4(float r, float g, float b) : this(r, g, b, 1.0f) { }

        /// <summary>
        /// Create a new instance of Color4 with r, g, b, a.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public Color4(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        /// <summary>Black</summary>
        public static Color4 Black => new Color4(0.0f, 0.0f, 0.0f, 1.0f);

        /// <summary>White</summary>
        public static Color4 White => new Color4(1.0f, 1.0f, 1.0f, 1.0f);
    }
}
