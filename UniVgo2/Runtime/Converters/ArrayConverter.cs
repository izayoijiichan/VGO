// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : ArrayConverter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using UnityEngine;

    /// <summary>
    /// Array Converter
    /// </summary>
    public static class ArrayConverter
    {
        /// <summary>
        /// Convert float[3] or float[4] to Color.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="gamma"></param>
        /// <returns></returns>
        public static Color ToColor(in float[] values, in bool gamma = false)
        {
            if (values == null)
            {
                return default;
            }

            Color color;

            if (values.Length == 3)
            {
                color = new Color(values[0], values[1], values[2]);
            }
            else if (values.Length == 4)
            {
                color = new Color(values[0], values[1], values[2], values[3]);

            }
            else
            {
                color = default;
            }

            if (gamma)
            {
                return color.gamma;
            }
            else
            {
                return color;
            }
        }

        /// <summary>
        /// Convert float[4] to Quaternion.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Quaternion ToQuaternion(in float[] values)
        {
            if (values == null)
            {
                return default;
            }

            if (values.Length == 4)
            {
                return new Quaternion(values[0], values[1], values[2], values[3]);
            }

            return default;
        }

        /// <summary>
        /// Convert float[2] to Vector2.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Vector2 ToVector2(in float[] values)
        {
            if (values == null)
            {
                return default;
            }

            if (values.Length == 2)
            {
                return new Vector2(values[0], values[1]);
            }

            return default;
        }

        /// <summary>
        /// Convert float[3] to Vector3.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Vector3 ToVector3(in float[] values)
        {
            if (values == null)
            {
                return default;
            }

            if (values.Length == 3)
            {
                return new Vector3(values[0], values[1], values[2]);
            }

            return default;
        }

        /// <summary>
        /// Convert float[4] to Vector4.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Vector4 ToVector4(in float[] values)
        {
            if (values == null)
            {
                return default;
            }

            if (values.Length == 4)
            {
                return new Vector4(values[0], values[1], values[2], values[3]);
            }

            return default;
        }

        /// <summary>
        /// Convert float[16] to Matrix4x4.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Matrix4x4 ToMatrix4x4(in float[] values)
        {
            if (values == null)
            {
                return default;
            }

            if (values.Length == 16)
            {
                return new Matrix4x4
                {
                    m00 = values[0],
                    m10 = values[1],
                    m20 = values[2],
                    m30 = values[3],
                    m01 = values[4],
                    m11 = values[5],
                    m21 = values[6],
                    m31 = values[7],
                    m02 = values[8],
                    m12 = values[9],
                    m22 = values[10],
                    m32 = values[11],
                    m03 = values[12],
                    m13 = values[13],
                    m23 = values[14],
                    m33 = values[15],
                };
            }

            return default;
        }
    }
}