// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Struct    : Matrix2
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Matrix2
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Matrix2
    {
        /// <summary></summary>
        public float M11;

        /// <summary></summary>
        public float M12;

        /// <summary></summary>
        public float M21;

        /// <summary></summary>
        public float M22;

        /// <summary>
        /// Create a new instance of Matrix2.
        /// </summary>
        /// <param name="m11"></param>
        /// <param name="m12"></param>
        /// <param name="m21"></param>
        /// <param name="m22"></param>
        public Matrix2(float m11, float m12, float m21, float m22)
        {
            M11 = m11;
            M12 = m12;
            M21 = m21;
            M22 = m22;
        }

        /// <summary></summary>
        public static Matrix2 Identity { get; } = new Matrix2(1, 0, 0, 1);

        /// <summary></summary>
        public static Matrix2 Zero { get; } = new Matrix2(0, 0, 0, 0);
    }
}
