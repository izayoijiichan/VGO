// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : ArrayConverter
// ----------------------------------------------------------------------
namespace UniVgo
{
    using UniGLTFforUniVgo;
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
        public static Color ToColor(float[] values, bool gamma = false)
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
        /// <param name="reverseZ"></param>
        /// <returns></returns>
        public static Quaternion ToQuaternion(float[] values, bool reverseZ = false)
        {
            if (values == null)
            {
                return default;
            }

            if (values.Length == 4)
            {
                Quaternion quaternion = new Quaternion(values[0], values[1], values[2], values[3]);

                if (reverseZ)
                {
                    return quaternion.ReverseZ();
                }
                else
                {
                    return quaternion;
                }
            }

            return default;
        }

        /// <summary>
        /// Convert float[2] to Vector2.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Vector2 ToVector2(float[] values)
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
        /// <param name="reverseZ"></param>
        /// <returns></returns>
        public static Vector3 ToVector3(float[] values, bool reverseZ = false)
        {
            if (values == null)
            {
                return default;
            }

            if (values.Length == 3)
            {
                Vector3 vecter3 = new Vector3(values[0], values[1], values[2]);

                if (reverseZ)
                {
                    return vecter3.ReverseZ();
                }
                else
                {
                    return vecter3;
                }
            }

            return default;
        }

        /// <summary>
        /// Convert float[4] to Vector4.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="reverseZ"></param>
        /// <returns></returns>
        public static Vector4 ToVector4(float[] values, bool reverseZ = false)
        {
            if (values == null)
            {
                return default;
            }

            if (values.Length == 4)
            {
                Vector4 vecter4 = new Vector4(values[0], values[1], values[2], values[3]);

                if (reverseZ)
                {
                    return vecter4.ReverseZ();
                }
                else
                {
                    return vecter4;
                }
            }

            return default;
        }
    }
}