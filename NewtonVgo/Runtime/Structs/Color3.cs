// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Struct    : Color3
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Color3
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Color3
    {
        /// <summary>Red</summary>
        public float R;

        /// <summary>Green</summary>
        public float G;

        /// <summary>Blue</summary>
        public float B;

        /// <summary>
        /// Create a new instance of Color3 with r, g, b.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public Color3(float r, float g, float b)
        {
            R = r;
            G = g;
            B = b;
        }

        /// <summary>Black</summary>
        public static Color3 Black => new Color3(0.0f, 0.0f, 0.0f);

        /// <summary>White</summary>
        public static Color3 White => new Color3(1.0f, 1.0f, 1.0f);
    }
}
