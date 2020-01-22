// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : VgoTransformConverter
// ----------------------------------------------------------------------
namespace UniVgo
{
    using UniGLTFforUniVgo;
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
                position = transform.localPosition.ReverseZ().ToArray(),
                rotation = transform.localRotation.ReverseZ().ToArray(),
                scale = transform.localScale.ToArray(),
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

            transform.localPosition = ArrayConverter.ToVector3(vgoTransform.position, reverseZ: true);
            transform.localRotation = ArrayConverter.ToQuaternion(vgoTransform.rotation, reverseZ: true);
            transform.localScale = ArrayConverter.ToVector3(vgoTransform.scale);
        }
    }
}