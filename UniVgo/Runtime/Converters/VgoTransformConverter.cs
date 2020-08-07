// ----------------------------------------------------------------------
// @Namespace : UniVgo.Converters
// @Class     : VgoTransformConverter
// ----------------------------------------------------------------------
namespace UniVgo.Converters
{
    using NewtonGltf;
    using UnityEngine;

    /// <summary>
    /// VGO Gradient Converter
    /// </summary>
    public class VgoTransformConverter
    {
        /// <summary>
        /// Create VGO_Transform from Transform.
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static VGO_Transform CreateFrom(Transform transform)
        {
            if (transform == null)
            {
                return default;
            }

            var vgoTransform = new VGO_Transform()
            {
                position = transform.localPosition.ReverseZ().ToNumericsVector3(),
                rotation = transform.localRotation.ReverseZ().ToNumericsVector4(),
                scale = transform.localScale.ToNumericsVector3(),
            };

            return vgoTransform;
        }

        /// <summary>
        /// Create Transform from VGO_Transform.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="vgoTransform"></param>
        /// <returns></returns>
        public static void SetComponentValue(Transform transform, VGO_Transform vgoTransform)
        {
            if (transform == null)
            {
                return;  // @notice
            }

            if (vgoTransform == null)
            {
                return;
            }

            transform.localPosition = vgoTransform.position.GetValueOrDefault(System.Numerics.Vector3.Zero).ToUnityVector3().ReverseZ();
            transform.localRotation = vgoTransform.rotation.HasValue ? vgoTransform.rotation.Value.ToUnityQuaternion().ReverseZ() : Quaternion.identity;
            transform.localScale = vgoTransform.scale.GetValueOrDefault(System.Numerics.Vector3.One).ToUnityVector3();
        }
    }
}