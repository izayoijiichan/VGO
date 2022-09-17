// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Converters
// @Class     : VgoTransformConverter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Converters
{
    using NewtonVgo;
    using UnityEngine;

    /// <summary>
    /// VGO Transform Converter
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
            transform.localPosition = vgoTransform.position.ToUnityVector3(Vector3.zero, geometryCoordinate);
            transform.localRotation = vgoTransform.rotation.ToUnityQuaternion(Quaternion.identity, geometryCoordinate);
            transform.localScale = vgoTransform.scale.ToUnityVector3(Vector3.one);
        }
    }
}