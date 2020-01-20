// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : VgoRigidbodyConverter
// ----------------------------------------------------------------------
namespace UniVgo
{
    using UniGLTFforUniVgo;
    using UnityEngine;

    /// <summary>
    /// VGO Rigidbody Converter
    /// </summary>
    public class VgoRigidbodyConverter
    {
        /// <summary>
        /// Create glTF_VGO_Rigidbody from Rigidbody.
        /// </summary>
        /// <param name="rigidbody"></param>
        /// <returns></returns>
        public static VGO_Rigidbody CreateFrom(Rigidbody rigidbody)
        {
            if (rigidbody == null)
            {
                return null;
            }

            return new VGO_Rigidbody()
            {
                mass = rigidbody.mass,
                drag = rigidbody.drag,
                angularDrag = rigidbody.angularDrag,
                useGravity = rigidbody.useGravity,
                isKinematic = rigidbody.isKinematic,
                interpolation = rigidbody.interpolation,
                collisionDetectionMode = rigidbody.collisionDetectionMode,
                constraints = rigidbody.constraints,
            };
        }

        /// <summary>
        /// Set Rigidbody parameter.
        /// </summary>
        /// <param name="rigidbody"></param>
        /// <param name="vgoRigidbody"></param>
        public static void SetComponentValue(Rigidbody rigidbody, VGO_Rigidbody vgoRigidbody)
        {
            if (rigidbody == null)
            {
                return;
            }

            if (vgoRigidbody == null)
            {
                return;
            }

            rigidbody.mass = vgoRigidbody.mass;
            rigidbody.drag = vgoRigidbody.drag;
            rigidbody.angularDrag = vgoRigidbody.angularDrag;
            rigidbody.useGravity = vgoRigidbody.useGravity;
            rigidbody.isKinematic = vgoRigidbody.isKinematic;
            rigidbody.interpolation = vgoRigidbody.interpolation;
            rigidbody.collisionDetectionMode = vgoRigidbody.collisionDetectionMode;
            rigidbody.constraints = vgoRigidbody.constraints;
        }
    }
}