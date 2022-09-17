// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Converters
// @Class     : VgoColliderConverter
// ----------------------------------------------------------------------
#nullable enable
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
            if (collider is BoxCollider boxCollider)
            {
                return new VgoCollider()
                {
                    type = VgoColliderType.Box,
                    enabled = boxCollider.enabled,
                    isTrigger = boxCollider.isTrigger,
                    center = boxCollider.center.ToNullableNumericsVector3(Vector3.zero, geometryCoordinate),
                    size = boxCollider.size.ToNullableNumericsVector3(Vector3.one, geometryCoordinate),
                    physicMaterial = VgoPhysicMaterialConverter.CreateOrDefaultFrom(collider.sharedMaterial),
                };
            }
            else if (collider is CapsuleCollider capsuleCollider)
            {
                return new VgoCollider()
                {
                    type = VgoColliderType.Capsule,
                    enabled = capsuleCollider.enabled,
                    isTrigger = capsuleCollider.isTrigger,
                    center = capsuleCollider.center.ToNullableNumericsVector3(Vector3.zero, geometryCoordinate),
                    radius = capsuleCollider.radius,
                    height = capsuleCollider.height,
                    direction = capsuleCollider.direction,
                    physicMaterial = VgoPhysicMaterialConverter.CreateOrDefaultFrom(collider.sharedMaterial),
                };
            }
            else if (collider is SphereCollider sphereCollider)
            {
                return new VgoCollider()
                {
                    type = VgoColliderType.Sphere,
                    enabled = sphereCollider.enabled,
                    isTrigger = sphereCollider.isTrigger,
                    center = sphereCollider.center.ToNullableNumericsVector3(Vector3.zero, geometryCoordinate),
                    radius = sphereCollider.radius,
                    physicMaterial = VgoPhysicMaterialConverter.CreateOrDefaultFrom(collider.sharedMaterial),
                };
            }
            else
            {
                throw new NotSupportedException();
            }
        }
        /// <summary>
        /// Create VgoCollider from Collider.
        /// </summary>
        /// <param name="collider"></param>
        /// <param name="geometryCoordinate"></param>
        /// <returns></returns>
        public static VgoCollider? CreateOrDefaultFrom(Collider? collider, VgoGeometryCoordinate geometryCoordinate)
        {
            if (collider == null)
            {
                return default;
            }

            if (collider is BoxCollider ||
                collider is CapsuleCollider ||
                collider is SphereCollider)
            {
                return CreateFrom(collider, geometryCoordinate);
            }
            else
            {
                return default;
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

            if (collider is BoxCollider boxCollider)
            {
                if (vgoCollider.type == VgoColliderType.Box)
                {
                    boxCollider.enabled = vgoCollider.enabled;
                    boxCollider.isTrigger = vgoCollider.isTrigger;
                    boxCollider.center = vgoCollider.center.ToUnityVector3(Vector3.zero, geometryCoordinate);
                    boxCollider.size = vgoCollider.size.ToUnityVector3(Vector3.one, geometryCoordinate);
                    if (vgoCollider.physicMaterial != null)
                    {
                        boxCollider.sharedMaterial = VgoPhysicMaterialConverter.ToPhysicMaterial(vgoCollider.physicMaterial);
                    }
                }
            }
            else if (collider is CapsuleCollider capsuleCollider)
            {
                if (vgoCollider.type == VgoColliderType.Capsule)
                {
                    capsuleCollider.enabled = vgoCollider.enabled;
                    capsuleCollider.isTrigger = vgoCollider.isTrigger;
                    capsuleCollider.center = vgoCollider.center.ToUnityVector3(Vector3.zero, geometryCoordinate);
                    capsuleCollider.radius = vgoCollider.radius;
                    capsuleCollider.height = vgoCollider.height;
                    capsuleCollider.direction = vgoCollider.direction;
                    if (vgoCollider.physicMaterial != null)
                    {
                        capsuleCollider.sharedMaterial = VgoPhysicMaterialConverter.ToPhysicMaterial(vgoCollider.physicMaterial);
                    }
                }
            }
            else if (collider is SphereCollider sphereCollider)
            {
                if (vgoCollider.type == VgoColliderType.Sphere)
                {
                    sphereCollider.enabled = vgoCollider.enabled;
                    sphereCollider.isTrigger = vgoCollider.isTrigger;
                    sphereCollider.center = vgoCollider.center.ToUnityVector3(Vector3.zero, geometryCoordinate);
                    sphereCollider.radius = vgoCollider.radius;
                    if (vgoCollider.physicMaterial != null)
                    {
                        sphereCollider.sharedMaterial = VgoPhysicMaterialConverter.ToPhysicMaterial(vgoCollider.physicMaterial);
                    }
                }
            }
        }
    }
}