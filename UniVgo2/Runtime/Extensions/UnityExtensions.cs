// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : UnityExtensions
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using UnityEngine;

    /// <summary>
    /// Unity Extensions
    /// </summary>
    public static class UnityExtensions
    {
        /// <summary>
        /// Convert Color to float[3].
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static float[] ToArray3(this in UnityEngine.Color c)
        {
            return new float[3] { c.r, c.g, c.b };
        }

        /// <summary>
        /// Convert Color to float[4].
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static float[] ToArray4(this in UnityEngine.Color c)
        {
            return new float[4] { c.r, c.g, c.b, c.a };
        }

        /// <summary>
        /// Convert Quaternion to float[4].
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public static float[] ToArray(this in Quaternion q)
        {
            return new float[] { q.x, q.y, q.z, q.w };
        }

        /// <summary>
        /// Convert Vector2 to float[2].
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static float[] ToArray(this in Vector2 v)
        {
            return new float[] { v.x, v.y };
        }

        /// <summary>
        /// Convert Vector3 to float[3].
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static float[] ToArray(this in Vector3 v)
        {
            return new float[] { v.x, v.y, v.z };
        }

        /// <summary>
        /// Convert Vector4 to float[4].
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static float[] ToArray(this in Vector4 v)
        {
            return new float[] { v.x, v.y, v.z, v.w };
        }

        /// <summary>
        /// Convert Vector4 to float[2].
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static float[] ToArray2(this in Vector4 v)
        {
            return new float[] { v.x, v.y };
        }

        /// <summary>
        /// Convert Vector4 to float[3].
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static float[] ToArray3(this in Vector4 v)
        {
            return new float[] { v.x, v.y, v.z };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector2 ReverseUV(this in Vector2 v)
        {
            return new Vector2(v.x, 1.0f - v.y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3 ReverseZ(this in Vector3 v)
        {
            return new Vector3(v.x, v.y, -v.z);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector4 ReverseZ(this in Vector4 v)
        {
            return new Vector4(v.x, v.y, -v.z, v.w);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public static Quaternion ReverseZ(this in Quaternion q)
        {
            q.ToAngleAxis(out float angle, out Vector3 axis);

            return Quaternion.AngleAxis(-angle, ReverseZ(axis));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Matrix4x4 ReverseZ(this in Matrix4x4 m)
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
        public static Vector3 ExtractTransration(this in Matrix4x4 matrix)
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
        public static Quaternion ExtractRotation(this in Matrix4x4 matrix)
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
        public static Vector3 ExtractScale(this in Matrix4x4 matrix)
        {
            Vector3 scale;

            scale.x = new Vector4(matrix.m00, matrix.m10, matrix.m20, matrix.m30).magnitude;
            scale.y = new Vector4(matrix.m01, matrix.m11, matrix.m21, matrix.m31).magnitude;
            scale.z = new Vector4(matrix.m02, matrix.m12, matrix.m22, matrix.m32).magnitude;

            return scale;
        }
    }
}
