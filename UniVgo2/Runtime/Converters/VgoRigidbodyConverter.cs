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
                drag = rigidbody.drag,
                angularDrag = rigidbody.angularDrag,
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
            rigidbody.drag = vgoRigidbody.drag;
            rigidbody.angularDrag = vgoRigidbody.angularDrag;
            rigidbody.useGravity = vgoRigidbody.useGravity;
            rigidbody.isKinematic = vgoRigidbody.isKinematic;
            rigidbody.interpolation = (UnityEngine.RigidbodyInterpolation)vgoRigidbody.interpolation;
            rigidbody.collisionDetectionMode = (UnityEngine.CollisionDetectionMode)vgoRigidbody.collisionDetectionMode;
            rigidbody.constraints = (UnityEngine.RigidbodyConstraints)vgoRigidbody.constraints;
        }
    }
}