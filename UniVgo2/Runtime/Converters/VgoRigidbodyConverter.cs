// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Converters
// @Class     : VgoRigidbodyConverter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Converters
{
    using NewtonVgo;
    using UnityEngine;

    /// <summary>
    /// VGO Rigidbody Converter
    /// </summary>
    public class VgoRigidbodyConverter
    {
        /// <summary>
        /// Create VgoRigidbody from Rigidbody.
        /// </summary>
        /// <param name="rigidbody"></param>
        /// <returns></returns>
        public static VgoRigidbody CreateFrom(in Rigidbody rigidbody)
        {
            return new VgoRigidbody()
            {
                mass = rigidbody.mass,
#if UNITY_6000_0_OR_NEWER
                drag = rigidbody.linearDamping,
                angularDrag = rigidbody.angularDamping,
#else
                drag = rigidbody.drag,
                angularDrag = rigidbody.angularDrag,
#endif
                useGravity = rigidbody.useGravity,
                isKinematic = rigidbody.isKinematic,
                interpolation = (NewtonVgo.RigidbodyInterpolation)rigidbody.interpolation,
                collisionDetectionMode = (NewtonVgo.CollisionDetectionMode)rigidbody.collisionDetectionMode,
                constraints = (NewtonVgo.RigidbodyConstraints)rigidbody.constraints,
            };
        }

        /// <summary>
        /// Set Rigidbody parameter.
        /// </summary>
        /// <param name="rigidbody"></param>
        /// <param name="vgoRigidbody"></param>
        public static void SetComponentValue(Rigidbody rigidbody, in VgoRigidbody vgoRigidbody)
        {
            rigidbody.mass = vgoRigidbody.mass;
#if UNITY_6000_0_OR_NEWER
            rigidbody.linearDamping = vgoRigidbody.drag;
            rigidbody.angularDamping = vgoRigidbody.angularDrag;
#else
            rigidbody.drag = vgoRigidbody.drag;
            rigidbody.angularDrag = vgoRigidbody.angularDrag;
#endif
            rigidbody.useGravity = vgoRigidbody.useGravity;
            rigidbody.isKinematic = vgoRigidbody.isKinematic;
            rigidbody.interpolation = (UnityEngine.RigidbodyInterpolation)vgoRigidbody.interpolation;
            rigidbody.collisionDetectionMode = (UnityEngine.CollisionDetectionMode)vgoRigidbody.collisionDetectionMode;
            rigidbody.constraints = (UnityEngine.RigidbodyConstraints)vgoRigidbody.constraints;
        }
    }
}