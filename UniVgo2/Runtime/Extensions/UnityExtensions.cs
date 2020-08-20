// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : UnityExtensions
// ----------------------------------------------------------------------
namespace UniVgo2
{
    using UnityEngine;

    /// <summary>
    /// Unity Extensions
    /// </summary>
    public static class UnityExtensions
    {
        public static float[] ToArray3(this UnityEngine.Color c)
        {
            return new float[3] { c.r, c.g, c.b };
        }

        public static float[] ToArray4(this UnityEngine.Color c)
        {
            return new float[4] { c.r, c.g, c.b, c.a };
        }

        public static float[] ToArray(this Quaternion q)
        {
            return new float[] { q.x, q.y, q.z, q.w };
        }

        public static float[] ToArray(this Vector2 v)
        {
            return new float[] { v.x, v.y };
        }

        public static float[] ToArray(this Vector3 v)
        {
            return new float[] { v.x, v.y, v.z };
        }

        public static float[] ToArray(this Vector4 v)
        {
            return new float[] { v.x, v.y, v.z, v.w };
        }

        public static float[] ToArray2(this Vector4 v)
        {
            return new float[] { v.x, v.y };
        }

        public static float[] ToArray3(this Vector4 v)
        {
            return new float[] { v.x, v.y, v.z };
        }

        public static Vector2 ReverseUV(this Vector2 v)
        {
            return new Vector2(v.x, 1.0f - v.y);
        }

        public static Vector3 ReverseZ(this Vector3 v)
        {
            return new Vector3(v.x, v.y, -v.z);
        }

        public static Vector4 ReverseZ(this Vector4 v)
        {
            return new Vector4(v.x, v.y, -v.z, v.w);
        }

        public static Quaternion ReverseZ(this Quaternion q)
        {
            float angle;
            Vector3 axis;
            q.ToAngleAxis(out angle, out axis);
            return Quaternion.AngleAxis(-angle, ReverseZ(axis));
        }

        public static Matrix4x4 ReverseZ(this Matrix4x4 m)
        {
            m.SetTRS(
                m.ExtractTransration().ReverseZ(),
                m.ExtractRotation().ReverseZ(),
                m.ExtractScale());
            return m;
        }

        /// <summary>
        /// Extract the transration from the matrix.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>transration</returns>
        public static Vector3 ExtractTransration(this Matrix4x4 matrix)
        {
            Vector3 transration;
            transration.x = matrix.m03;
            transration.y = matrix.m13;
            transration.z = matrix.m23;
            return transration;
        }

        /// <summary>
        /// Extract the rotation from the matrix.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>rotation</returns>
        /// <remarks>
        /// https://forum.unity.com/threads/how-to-assign-matrix4x4-to-transform.121966/
        /// </remarks>
        public static Quaternion ExtractRotation(this Matrix4x4 matrix)
        {
            Vector3 forward;
            forward.x = matrix.m02;
            forward.y = matrix.m12;
            forward.z = matrix.m22;

            Vector3 upwards;
            upwards.x = matrix.m01;
            upwards.y = matrix.m11;
            upwards.z = matrix.m21;

            return Quaternion.LookRotation(forward, upwards);
        }

        /// <summary>
        /// Extract the scale from the matrix.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>scale</returns>
        public static Vector3 ExtractScale(this Matrix4x4 matrix)
        {
            Vector3 scale;
            scale.x = new Vector4(matrix.m00, matrix.m10, matrix.m20, matrix.m30).magnitude;
            scale.y = new Vector4(matrix.m01, matrix.m11, matrix.m21, matrix.m31).magnitude;
            scale.z = new Vector4(matrix.m02, matrix.m12, matrix.m22, matrix.m32).magnitude;
            return scale;
        }
    }
}
