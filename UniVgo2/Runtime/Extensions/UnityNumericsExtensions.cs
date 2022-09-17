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

        public static System.Numerics.Vector2 ToNumericsVector2(this UnityEngine.Vector2 unityVector)
        {
            return new System.Numerics.Vector2(
                unityVector.x,
                unityVector.y
            );
        }

        public static System.Numerics.Vector3 ToNumericsVector3(this UnityEngine.Vector3 unityVector)
        {
            return new System.Numerics.Vector3(
                unityVector.x,
                unityVector.y,
                unityVector.z
            );
        }

        public static System.Numerics.Vector3 ToNumericsVector3(this UnityEngine.Vector3 unityVector, NewtonVgo.VgoGeometryCoordinate geometryCoordinate)
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

        public static System.Numerics.Vector4 ToNumericsVector4(this UnityEngine.Vector4 unityVector)
        {
            return new System.Numerics.Vector4(unityVector.x, unityVector.y, unityVector.z, unityVector.w);
        }

        public static System.Numerics.Vector4 ToNumericsVector4(this UnityEngine.Vector4 unityVector, NewtonVgo.VgoGeometryCoordinate geometryCoordinate)
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

        public static System.Numerics.Quaternion ToNumericsQuaternion(this UnityEngine.Quaternion unityQuaternion)
        {
            return new System.Numerics.Quaternion(
                unityQuaternion.x,
                unityQuaternion.y,
                unityQuaternion.z,
                unityQuaternion.w
            );
        }

        public static System.Numerics.Quaternion ToNumericsQuaternion(this UnityEngine.Quaternion unityQuaternion, NewtonVgo.VgoGeometryCoordinate geometryCoordinate)
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

        public static System.Numerics.Matrix4x4 ToNumericsMatrix(this UnityEngine.Matrix4x4 unityMatrix)
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

        public static System.Numerics.Vector3? ToNullableNumericsVector3(this UnityEngine.Vector3 unityVector, UnityEngine.Vector3 defaultValue)
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

        public static System.Numerics.Vector3? ToNullableNumericsVector3(this UnityEngine.Vector3 unityVector, UnityEngine.Vector3 defaultValue, NewtonVgo.VgoGeometryCoordinate geometryCoordinate)
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

        public static System.Numerics.Quaternion? ToNullableNumericsQuaternion(this UnityEngine.Quaternion unityQuaternion, UnityEngine.Quaternion defaultValue, NewtonVgo.VgoGeometryCoordinate geometryCoordinate)
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

        public static UnityEngine.Vector2 ToUnityVector2(this System.Numerics.Vector2 numericsVector)
        {
            return new UnityEngine.Vector2(
                numericsVector.X,
                numericsVector.Y
            );
        }

        public static UnityEngine.Vector3 ToUnityVector3(this System.Numerics.Vector3 numericsVector)
        {
            return new UnityEngine.Vector3(
                numericsVector.X,
                numericsVector.Y,
                numericsVector.Z
            );
        }

        public static UnityEngine.Vector3 ToUnityVector3(this System.Numerics.Vector3 numericsVector, NewtonVgo.VgoGeometryCoordinate geometryCoordinate)
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

        public static UnityEngine.Vector3 ToUnityVector3(this System.Numerics.Vector3? numericsVector, UnityEngine.Vector3 defaultValue)
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

        public static UnityEngine.Vector3 ToUnityVector3(this System.Numerics.Vector3? numericsVector, UnityEngine.Vector3 defaultValue, NewtonVgo.VgoGeometryCoordinate geometryCoordinate)
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

        public static UnityEngine.Vector4 ToUnityVector4(this System.Numerics.Vector4 numericsVector)
        {
            return new UnityEngine.Vector4(
                numericsVector.X,
                numericsVector.Y,
                numericsVector.Z,
                numericsVector.W
            );
        }

        public static UnityEngine.Vector4 ToUnityVector4(this System.Numerics.Vector4 numericsVector, NewtonVgo.VgoGeometryCoordinate geometryCoordinate)
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

        public static UnityEngine.Quaternion ToUnityQuaternion(this System.Numerics.Quaternion numericsQuaternion)
        {
            return new UnityEngine.Quaternion(
                numericsQuaternion.X,
                numericsQuaternion.Y,
                numericsQuaternion.Z,
                numericsQuaternion.W
            );
        }

        public static UnityEngine.Quaternion ToUnityQuaternion(this System.Numerics.Quaternion numericsQuaternion, NewtonVgo.VgoGeometryCoordinate geometryCoordinate)
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

        public static UnityEngine.Quaternion ToUnityQuaternion(this System.Numerics.Quaternion? numericsQuaternion, UnityEngine.Quaternion defaultValue, NewtonVgo.VgoGeometryCoordinate geometryCoordinate)
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

        public static UnityEngine.Matrix4x4 ToUnityMatrix(this System.Numerics.Matrix4x4 numericsMatrix)
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
