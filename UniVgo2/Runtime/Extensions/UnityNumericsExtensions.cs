// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : UnityNumericsExtensions
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    /// <summary>
    /// Unity Numerics Extensions
    /// </summary>
    public static class UnityNumericsExtensions
    {
        #region UnityEngine -> System.Numerics

        /// <summary>
        /// Convert UnityEngine.Vector2 to System.Numerics.Vector2.
        /// </summary>
        /// <param name="unityVector"></param>
        /// <returns></returns>
        public static System.Numerics.Vector2 ToNumericsVector2(this in UnityEngine.Vector2 unityVector)
        {
            return new System.Numerics.Vector2(
                unityVector.x,
                unityVector.y
            );
        }

        /// <summary>
        /// Convert UnityEngine.Vector3 to System.Numerics.Vector3.
        /// </summary>
        /// <param name="unityVector"></param>
        /// <returns></returns>
        public static System.Numerics.Vector3 ToNumericsVector3(this in UnityEngine.Vector3 unityVector)
        {
            return new System.Numerics.Vector3(
                unityVector.x,
                unityVector.y,
                unityVector.z
            );
        }

        /// <summary>
        /// Convert UnityEngine.Vector3 to System.Numerics.Vector3.
        /// </summary>
        /// <param name="unityVector"></param>
        /// <param name="geometryCoordinate"></param>
        /// <returns></returns>
        public static System.Numerics.Vector3 ToNumericsVector3(this in UnityEngine.Vector3 unityVector, in NewtonVgo.VgoGeometryCoordinate geometryCoordinate)
        {
            if (geometryCoordinate == NewtonVgo.VgoGeometryCoordinate.RightHanded)
            {
                return unityVector.ReverseZ().ToNumericsVector3();
            }
            else
            {
                return unityVector.ToNumericsVector3();
            }
        }

        /// <summary>
        /// Convert UnityEngine.Vector4 to System.Numerics.Vector4.
        /// </summary>
        /// <param name="unityVector"></param>
        /// <returns></returns>
        public static System.Numerics.Vector4 ToNumericsVector4(this in UnityEngine.Vector4 unityVector)
        {
            return new System.Numerics.Vector4(unityVector.x, unityVector.y, unityVector.z, unityVector.w);
        }

        /// <summary>
        /// Convert UnityEngine.Vector4 to System.Numerics.Vector4.
        /// </summary>
        /// <param name="unityVector"></param>
        /// <param name="geometryCoordinate"></param>
        /// <returns></returns>
        public static System.Numerics.Vector4 ToNumericsVector4(this in UnityEngine.Vector4 unityVector, in NewtonVgo.VgoGeometryCoordinate geometryCoordinate)
        {
            if (geometryCoordinate == NewtonVgo.VgoGeometryCoordinate.RightHanded)
            {
                return unityVector.ReverseZ().ToNumericsVector4();
            }
            else
            {
                return unityVector.ToNumericsVector4();
            }
        }

        /// <summary>
        /// Convert UnityEngine.Quaternion to System.Numerics.Quaternion.
        /// </summary>
        /// <param name="unityQuaternion"></param>
        /// <returns></returns>
        public static System.Numerics.Quaternion ToNumericsQuaternion(this in UnityEngine.Quaternion unityQuaternion)
        {
            return new System.Numerics.Quaternion(
                unityQuaternion.x,
                unityQuaternion.y,
                unityQuaternion.z,
                unityQuaternion.w
            );
        }

        /// <summary>
        /// Convert UnityEngine.Quaternion to System.Numerics.Quaternion.
        /// </summary>
        /// <param name="unityQuaternion"></param>
        /// <param name="geometryCoordinate"></param>
        /// <returns></returns>
        public static System.Numerics.Quaternion ToNumericsQuaternion(this in UnityEngine.Quaternion unityQuaternion, in NewtonVgo.VgoGeometryCoordinate geometryCoordinate)
        {
            if (geometryCoordinate == NewtonVgo.VgoGeometryCoordinate.RightHanded)
            {
                return unityQuaternion.ReverseZ().ToNumericsQuaternion();
            }
            else
            {
                return unityQuaternion.ToNumericsQuaternion();
            }
        }

        /// <summary>
        /// Convert UnityEngine.Matrix4x4 to System.Numerics.Matrix4x4.
        /// </summary>
        /// <param name="unityMatrix"></param>
        /// <returns></returns>
        public static System.Numerics.Matrix4x4 ToNumericsMatrix(this in UnityEngine.Matrix4x4 unityMatrix)
        {
            return new System.Numerics.Matrix4x4()
            {
                M11 = unityMatrix.m00,
                M12 = unityMatrix.m10,
                M13 = unityMatrix.m20,
                M14 = unityMatrix.m30,
                M21 = unityMatrix.m01,
                M22 = unityMatrix.m11,
                M23 = unityMatrix.m21,
                M24 = unityMatrix.m31,
                M31 = unityMatrix.m02,
                M32 = unityMatrix.m12,
                M33 = unityMatrix.m22,
                M34 = unityMatrix.m32,
                M41 = unityMatrix.m03,
                M42 = unityMatrix.m13,
                M43 = unityMatrix.m23,
                M44 = unityMatrix.m33,
            };
        }

        /// <summary>
        /// Convert UnityEngine.Vector3 to System.Numerics.Vector3.
        /// </summary>
        /// <param name="unityVector"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static System.Numerics.Vector3? ToNullableNumericsVector3(this in UnityEngine.Vector3 unityVector, in UnityEngine.Vector3 defaultValue)
        {
            if (unityVector == defaultValue)
            {
                return null;
            }
            else
            {
                return unityVector.ToNumericsVector3();
            }
        }

        /// <summary>
        /// Convert UnityEngine.Vector3 to System.Numerics.Vector3.
        /// </summary>
        /// <param name="unityVector"></param>
        /// <param name="defaultValue"></param>
        /// <param name="geometryCoordinate"></param>
        /// <returns></returns>
        public static System.Numerics.Vector3? ToNullableNumericsVector3(this in UnityEngine.Vector3 unityVector, in UnityEngine.Vector3 defaultValue, in NewtonVgo.VgoGeometryCoordinate geometryCoordinate)
        {
            if (unityVector == defaultValue)
            {
                return null;
            }
            else
            {
                return unityVector.ToNumericsVector3(geometryCoordinate);
            }
        }

        /// <summary>
        /// Convert UnityEngine.Quaternion to System.Numerics.Quaternion.
        /// </summary>
        /// <param name="unityQuaternion"></param>
        /// <param name="defaultValue"></param>
        /// <param name="geometryCoordinate"></param>
        /// <returns></returns>
        public static System.Numerics.Quaternion? ToNullableNumericsQuaternion(this in UnityEngine.Quaternion unityQuaternion, in UnityEngine.Quaternion defaultValue, in NewtonVgo.VgoGeometryCoordinate geometryCoordinate)
        {
            if (unityQuaternion == defaultValue)
            {
                return null;
            }
            else
            {
                return unityQuaternion.ToNumericsQuaternion(geometryCoordinate);
            }
        }

        #endregion

        #region System.Numerics -> UnityEngine

        /// <summary>
        /// Convert System.Numerics.Vector2 to UnityEngine.Vector2.
        /// </summary>
        /// <param name="numericsVector"></param>
        /// <returns></returns>
        public static UnityEngine.Vector2 ToUnityVector2(this in System.Numerics.Vector2 numericsVector)
        {
            return new UnityEngine.Vector2(
                numericsVector.X,
                numericsVector.Y
            );
        }

        /// <summary>
        /// Convert System.Numerics.Vector3 to UnityEngine.Vector3.
        /// </summary>
        /// <param name="numericsVector"></param>
        /// <returns></returns>
        public static UnityEngine.Vector3 ToUnityVector3(this in System.Numerics.Vector3 numericsVector)
        {
            return new UnityEngine.Vector3(
                numericsVector.X,
                numericsVector.Y,
                numericsVector.Z
            );
        }

        /// <summary>
        /// Convert System.Numerics.Vector3 to UnityEngine.Vector3.
        /// </summary>
        /// <param name="numericsVector"></param>
        /// <param name="geometryCoordinate"></param>
        /// <returns></returns>
        public static UnityEngine.Vector3 ToUnityVector3(this in System.Numerics.Vector3 numericsVector, in NewtonVgo.VgoGeometryCoordinate geometryCoordinate)
        {
            if (geometryCoordinate == NewtonVgo.VgoGeometryCoordinate.RightHanded)
            {
                return numericsVector.ToUnityVector3().ReverseZ();
            }
            else
            {
                return numericsVector.ToUnityVector3();
            }
        }

        /// <summary>
        /// Convert System.Numerics.Vector3 to UnityEngine.Vector3.
        /// </summary>
        /// <param name="numericsVector"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static UnityEngine.Vector3 ToUnityVector3(this in System.Numerics.Vector3? numericsVector, in UnityEngine.Vector3 defaultValue)
        {
            if (numericsVector.HasValue)
            {
                return numericsVector.Value.ToUnityVector3();
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Convert System.Numerics.Vector3 to UnityEngine.Vector3.
        /// </summary>
        /// <param name="numericsVector"></param>
        /// <param name="defaultValue"></param>
        /// <param name="geometryCoordinate"></param>
        /// <returns></returns>
        public static UnityEngine.Vector3 ToUnityVector3(this in System.Numerics.Vector3? numericsVector, in UnityEngine.Vector3 defaultValue, in NewtonVgo.VgoGeometryCoordinate geometryCoordinate)
        {
            if (numericsVector.HasValue)
            {
                return numericsVector.Value.ToUnityVector3(geometryCoordinate);
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Convert System.Numerics.Vector4 to UnityEngine.Vector4.
        /// </summary>
        /// <param name="numericsVector"></param>
        /// <returns></returns>
        public static UnityEngine.Vector4 ToUnityVector4(this in System.Numerics.Vector4 numericsVector)
        {
            return new UnityEngine.Vector4(
                numericsVector.X,
                numericsVector.Y,
                numericsVector.Z,
                numericsVector.W
            );
        }

        /// <summary>
        /// Convert System.Numerics.Vector4 to UnityEngine.Vector4.
        /// </summary>
        /// <param name="numericsVector"></param>
        /// <param name="geometryCoordinate"></param>
        /// <returns></returns>
        public static UnityEngine.Vector4 ToUnityVector4(this in System.Numerics.Vector4 numericsVector, in NewtonVgo.VgoGeometryCoordinate geometryCoordinate)
        {
            if (geometryCoordinate == NewtonVgo.VgoGeometryCoordinate.RightHanded)
            {
                return numericsVector.ToUnityVector4().ReverseZ();
            }
            else
            {
                return numericsVector.ToUnityVector4();
            }
        }

        /// <summary>
        /// Convert System.Numerics.Quaternion to UnityEngine.Quaternion.
        /// </summary>
        /// <param name="numericsQuaternion"></param>
        /// <returns></returns>
        public static UnityEngine.Quaternion ToUnityQuaternion(this in System.Numerics.Quaternion numericsQuaternion)
        {
            return new UnityEngine.Quaternion(
                numericsQuaternion.X,
                numericsQuaternion.Y,
                numericsQuaternion.Z,
                numericsQuaternion.W
            );
        }

        /// <summary>
        /// Convert System.Numerics.Quaternion to UnityEngine.Quaternion.
        /// </summary>
        /// <param name="numericsQuaternion"></param>
        /// <param name="geometryCoordinate"></param>
        /// <returns></returns>
        public static UnityEngine.Quaternion ToUnityQuaternion(this in System.Numerics.Quaternion numericsQuaternion, in NewtonVgo.VgoGeometryCoordinate geometryCoordinate)
        {
            if (geometryCoordinate == NewtonVgo.VgoGeometryCoordinate.RightHanded)
            {
                return numericsQuaternion.ToUnityQuaternion().ReverseZ();
            }
            else
            {
                return numericsQuaternion.ToUnityQuaternion();
            }
        }

        /// <summary>
        /// Convert System.Numerics.Quaternion to UnityEngine.Quaternion.
        /// </summary>
        /// <param name="numericsQuaternion"></param>
        /// <param name="defaultValue"></param>
        /// <param name="geometryCoordinate"></param>
        /// <returns></returns>
        public static UnityEngine.Quaternion ToUnityQuaternion(this in System.Numerics.Quaternion? numericsQuaternion, in UnityEngine.Quaternion defaultValue, in NewtonVgo.VgoGeometryCoordinate geometryCoordinate)
        {
            if (numericsQuaternion.HasValue)
            {
                return numericsQuaternion.Value.ToUnityQuaternion(geometryCoordinate);
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Convert System.Numerics.Matrix4x4 to UnityEngine.Matrix4x4.
        /// </summary>
        /// <param name="numericsMatrix"></param>
        /// <returns></returns>
        public static UnityEngine.Matrix4x4 ToUnityMatrix(this in System.Numerics.Matrix4x4 numericsMatrix)
        {
            return new UnityEngine.Matrix4x4()
            {
                m00 = numericsMatrix.M11,
                m01 = numericsMatrix.M21,
                m02 = numericsMatrix.M31,
                m03 = numericsMatrix.M41,
                m10 = numericsMatrix.M12,
                m11 = numericsMatrix.M22,
                m12 = numericsMatrix.M32,
                m13 = numericsMatrix.M42,
                m20 = numericsMatrix.M13,
                m21 = numericsMatrix.M23,
                m22 = numericsMatrix.M33,
                m23 = numericsMatrix.M43,
                m30 = numericsMatrix.M14,
                m31 = numericsMatrix.M24,
                m32 = numericsMatrix.M34,
                m33 = numericsMatrix.M44,
            };
        }

        #endregion
    }
}
