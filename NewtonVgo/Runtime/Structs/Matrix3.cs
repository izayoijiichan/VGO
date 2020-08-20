// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Struct    : Matrix3
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Matrix3
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Matrix3
    {
        /// <summary></summary>
        public float M11;

        /// <summary></summary>
        public float M12;

        /// <summary></summary>
        public float M13;

        /// <summary></summary>
        public float M21;

        /// <summary></summary>
        public float M22;

        /// <summary></summary>
        public float M23;

        /// <summary></summary>
        public float M31;

        /// <summary></summary>
        public float M32;

        /// <summary></summary>
        public float M33;

        /// <summary>
        /// Create a new instance of Matrix3.
        /// </summary>
        /// <param name="m11"></param>
        /// <param name="m12"></param>
        /// <param name="m13"></param>
        /// <param name="m21"></param>
        /// <param name="m22"></param>
        /// <param name="m23"></param>
        /// <param name="m31"></param>
        /// <param name="m32"></param>
        /// <param name="m33"></param>
        public Matrix3(float m11, float m12, float m13, float m21, float m22, float m23, float m31, float m32, float m33)
        {
            M11 = m11;
            M12 = m12;
            M13 = m13;
            M21 = m21;
            M22 = m22;
            M23 = m23;
            M31 = m31;
            M32 = m32;
            M33 = m33;
        }

        /// <summary></summary>
        public static Matrix3 Identity { get; } = new Matrix3(1, 0, 0, 0, 1, 0, 0, 0, 1);

        /// <summary></summary>
        public static Matrix3 Zero { get; } = new Matrix3(0, 0, 0, 0, 0, 0, 0, 0, 0);
    }
}
