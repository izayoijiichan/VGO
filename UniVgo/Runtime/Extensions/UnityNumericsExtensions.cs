﻿// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : UnityNumericsExtensions
// ----------------------------------------------------------------------
namespace UniVgo
{
    /// <summary>
    /// Unity Numerics Extensions
    /// </summary>
    public static class UnityNumericsExtensions
    {
        #region UnityEngine -> System.Numerics

        public static System.Numerics.Vector2 ToNumericsVector2(this UnityEngine.Vector2 v)
        {
            return new System.Numerics.Vector2(v.x, v.y);
        }

        public static System.Numerics.Vector3 ToNumericsVector3(this UnityEngine.Vector3 v)
        {
            return new System.Numerics.Vector3(v.x, v.y, v.z);
        }

        public static System.Numerics.Vector4 ToNumericsVector4(this UnityEngine.Vector4 v)
        {
            return new System.Numerics.Vector4(v.x, v.y, v.z, v.w);
        }

        public static System.Numerics.Quaternion ToNumericsVector4(this UnityEngine.Quaternion q)
        {
            return new System.Numerics.Quaternion(q.x, q.y, q.z, q.w);
        }

        public static System.Numerics.Matrix4x4 ToNumericsMatrix(this UnityEngine.Matrix4x4 m)
        {
            return new System.Numerics.Matrix4x4()
            {
                M11 = m.m00,
                M12 = m.m10,
                M13 = m.m20,
                M14 = m.m30,
                M21 = m.m01,
                M22 = m.m11,
                M23 = m.m21,
                M24 = m.m31,
                M31 = m.m02,
                M32 = m.m12,
                M33 = m.m22,
                M34 = m.m32,
                M41 = m.m03,
                M42 = m.m13,
                M43 = m.m23,
                M44 = m.m33,
            };
            //return new System.Numerics.Matrix4x4()
            //{
            //    M11 = m.m00,
            //    M12 = m.m01,
            //    M13 = m.m02,
            //    M14 = m.m03,
            //    M21 = m.m10,
            //    M22 = m.m11,
            //    M23 = m.m12,
            //    M24 = m.m13,
            //    M31 = m.m20,
            //    M32 = m.m21,
            //    M33 = m.m22,
            //    M34 = m.m23,
            //    M41 = m.m30,
            //    M42 = m.m31,
            //    M43 = m.m32,
            //    M44 = m.m33,
            //};
        }

        public static VgoGltf.Color3 ToGltfColor3(this UnityEngine.Color c)
        {
            return new VgoGltf.Color3(c.r, c.g, c.b);
        }

        public static VgoGltf.Color4 ToGltfColor4(this UnityEngine.Color c)
        {
            return new VgoGltf.Color4(c.r, c.g, c.b, c.a);
        }

        #endregion

        #region System.Numerics -> UnityEngine

        public static UnityEngine.Vector2 ToUnityVector2(this System.Numerics.Vector2 v)
        {
            return new UnityEngine.Vector2(v.X, v.Y);
        }

        public static UnityEngine.Vector3 ToUnityVector3(this System.Numerics.Vector3 v)
        {
            return new UnityEngine.Vector3(v.X, v.Y, v.Z);
        }

        public static UnityEngine.Vector4 ToUnityVector4(this System.Numerics.Vector4 v)
        {
            return new UnityEngine.Vector4(v.X, v.Y, v.Z, v.W);
        }

        public static UnityEngine.Quaternion ToUnityQuaternion(this System.Numerics.Quaternion q)
        {
            return new UnityEngine.Quaternion(q.X, q.Y, q.Z, q.W);
        }

        public static UnityEngine.Matrix4x4 ToUnityMatrix(this System.Numerics.Matrix4x4 m)
        {
            return new UnityEngine.Matrix4x4()
            {
                m00 = m.M11,
                m01 = m.M21,
                m02 = m.M31,
                m03 = m.M41,
                m10 = m.M12,
                m11 = m.M22,
                m12 = m.M32,
                m13 = m.M42,
                m20 = m.M13,
                m21 = m.M23,
                m22 = m.M33,
                m23 = m.M43,
                m30 = m.M14,
                m31 = m.M24,
                m32 = m.M34,
                m33 = m.M44,
            };
            //return new UnityEngine.Matrix4x4()
            //{
            //    m00 = m.M11,
            //    m01 = m.M12,
            //    m02 = m.M13,
            //    m03 = m.M14,
            //    m10 = m.M21,
            //    m11 = m.M22,
            //    m12 = m.M23,
            //    m13 = m.M24,
            //    m20 = m.M31,
            //    m21 = m.M32,
            //    m22 = m.M33,
            //    m23 = m.M34,
            //    m30 = m.M41,
            //    m31 = m.M42,
            //    m32 = m.M43,
            //    m33 = m.M44,
            //};
        }

        public static UnityEngine.Color ToUnityColor(this VgoGltf.Color3 c)
        {
            return new UnityEngine.Color(c.R, c.G, c.B, 1.0f);
        }

        public static UnityEngine.Color ToUnityColor(this VgoGltf.Color4 c)
        {
            return new UnityEngine.Color(c.R, c.G, c.B, c.A);
        }

        #endregion
    }
}
