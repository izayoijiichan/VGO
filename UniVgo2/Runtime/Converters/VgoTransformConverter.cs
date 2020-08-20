// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Converters
// @Class     : VgoTransformConverter
// ----------------------------------------------------------------------
namespace UniVgo2.Converters
{
    using NewtonVgo;
    using UnityEngine;

    /// <summary>
    /// VGO Gradient Converter
    /// </summary>
    public class VgoTransformConverter
    {
        /// <summary>
        /// Create VgoTransform from Transform.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="geometryCoordinate"></param>
        /// <returns></returns>
        public static VgoTransform CreateFrom(Transform transform, VgoGeometryCoordinate geometryCoordinate)
        {
            if (transform == null)
            {
                return default;
            }

            VgoTransform vgoTransform = new VgoTransform()
            {
                position = transform.localPosition.ToNullableNumericsVector3(Vector3.zero, geometryCoordinate),
                rotation = transform.localRotation.ToNullableNumericsQuaternion(Quaternion.identity, geometryCoordinate),
                scale = transform.localScale.ToNullableNumericsVector3(Vector3.one),
            };

            return vgoTransform;
        }

        /// <summary>
        /// Create Transform from VgoTransform.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="vgoTransform"></param>
        /// <param name="geometryCoordinate"></param>
        /// <returns></returns>
        public static void SetComponentValue(Transform transform, VgoTransform vgoTransform, VgoGeometryCoordinate geometryCoordinate)
        {
            if (transform == null)
            {
                return;  // @notice
            }

            if (vgoTransform == null)
            {
                return;
            }

            transform.localPosition = vgoTransform.position.ToUnityVector3(Vector3.zero, geometryCoordinate);
            transform.localRotation = vgoTransform.rotation.ToUnityQuaternion(Quaternion.identity, geometryCoordinate);
            transform.localScale = vgoTransform.scale.ToUnityVector3(Vector3.one);
        }
    }
}