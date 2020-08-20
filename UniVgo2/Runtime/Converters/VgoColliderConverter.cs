// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Converters
// @Class     : VgoColliderConverter
// ----------------------------------------------------------------------
namespace UniVgo2.Converters
{
    using NewtonVgo;
    using System;
    using UnityEngine;

    /// <summary>
    /// VGO Collider Converter
    /// </summary>
    public class VgoColliderConverter
    {
        /// <summary>
        /// Create VgoCollider from Collider.
        /// </summary>
        /// <param name="collider"></param>
        /// <param name="geometryCoordinate"></param>
        /// <returns></returns>
        public static VgoCollider CreateFrom(Collider collider, VgoGeometryCoordinate geometryCoordinate)
        {
            if (collider == null)
            {
                return null;
            }

            Type type = collider.GetType();

            if (type == typeof(BoxCollider))
            {
                var boxCollider = collider as BoxCollider;

                return new VgoCollider()
                {
                    type = VgoColliderType.Box,
                    enabled = boxCollider.enabled,
                    isTrigger = boxCollider.isTrigger,
                    center = boxCollider.center.ToNullableNumericsVector3(Vector3.zero, geometryCoordinate),
                    size = boxCollider.size.ToNullableNumericsVector3(Vector3.one, geometryCoordinate),
                    physicMaterial = VgoPhysicMaterialConverter.CreateFrom(collider.sharedMaterial),
                };
            }
            else if (type == typeof(CapsuleCollider))
            {
                var capsuleCollider = collider as CapsuleCollider;

                return new VgoCollider()
                {
                    type = VgoColliderType.Capsule,
                    enabled = capsuleCollider.enabled,
                    isTrigger = capsuleCollider.isTrigger,
                    center = capsuleCollider.center.ToNullableNumericsVector3(Vector3.zero, geometryCoordinate),
                    radius = capsuleCollider.radius,
                    height = capsuleCollider.height,
                    direction = capsuleCollider.direction,
                    physicMaterial = VgoPhysicMaterialConverter.CreateFrom(collider.sharedMaterial),
                };
            }
            else if (type == typeof(SphereCollider))
            {
                var sphereCollider = collider as SphereCollider;

                return new VgoCollider()
                {
                    type = VgoColliderType.Sphere,
                    enabled = sphereCollider.enabled,
                    isTrigger = sphereCollider.isTrigger,
                    center = sphereCollider.center.ToNullableNumericsVector3(Vector3.zero, geometryCoordinate),
                    radius = sphereCollider.radius,
                    physicMaterial = VgoPhysicMaterialConverter.CreateFrom(collider.sharedMaterial),
                };
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Set Collider field value.
        /// </summary>
        /// <param name="collider"></param>
        /// <param name="vgoCollider"></param>
        /// <param name="geometryCoordinate"></param>
        public static void SetComponentValue(Collider collider, VgoCollider vgoCollider, VgoGeometryCoordinate geometryCoordinate)
        {
            if (collider == null)
            {
                return;
            }

            if (vgoCollider == null)
            {
                return;
            }

            Type type = collider.GetType();

            if (type == typeof(BoxCollider))
            {
                var boxCollider = collider as BoxCollider;

                if (vgoCollider.type == VgoColliderType.Box)
                {
                    boxCollider.enabled = vgoCollider.enabled;
                    boxCollider.isTrigger = vgoCollider.isTrigger;
                    boxCollider.center = vgoCollider.center.ToUnityVector3(Vector3.zero, geometryCoordinate);
                    boxCollider.size = vgoCollider.size.ToUnityVector3(Vector3.one, geometryCoordinate);
                    boxCollider.sharedMaterial = VgoPhysicMaterialConverter.ToPhysicMaterial(vgoCollider.physicMaterial);
                }
            }
            else if (type == typeof(CapsuleCollider))
            {
                var capsuleCollider = collider as CapsuleCollider;

                if (vgoCollider.type == VgoColliderType.Capsule)
                {
                    capsuleCollider.enabled = vgoCollider.enabled;
                    capsuleCollider.isTrigger = vgoCollider.isTrigger;
                    capsuleCollider.center = vgoCollider.center.ToUnityVector3(Vector3.zero, geometryCoordinate);
                    capsuleCollider.radius = vgoCollider.radius;
                    capsuleCollider.height = vgoCollider.height;
                    capsuleCollider.direction = vgoCollider.direction;
                    capsuleCollider.sharedMaterial = VgoPhysicMaterialConverter.ToPhysicMaterial(vgoCollider.physicMaterial);
                }
            }
            else if (type == typeof(SphereCollider))
            {
                var sphereCollider = collider as SphereCollider;

                if (vgoCollider.type == VgoColliderType.Sphere)
                {
                    sphereCollider.enabled = vgoCollider.enabled;
                    sphereCollider.isTrigger = vgoCollider.isTrigger;
                    sphereCollider.center = vgoCollider.center.ToUnityVector3(Vector3.zero, geometryCoordinate);
                    sphereCollider.radius = vgoCollider.radius;
                    sphereCollider.sharedMaterial = VgoPhysicMaterialConverter.ToPhysicMaterial(vgoCollider.physicMaterial);
                }
            }
        }
    }
}