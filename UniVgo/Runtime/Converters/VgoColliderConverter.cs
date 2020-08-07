// ----------------------------------------------------------------------
// @Namespace : UniVgo.Converters
// @Class     : VgoColliderConverter
// ----------------------------------------------------------------------
namespace UniVgo.Converters
{
    using NewtonGltf;
    using System;
    using UnityEngine;

    /// <summary>
    /// VGO Collider Converter
    /// </summary>
    public class VgoColliderConverter
    {
        /// <summary>
        /// Create glTF_VGO_Collider from Collider.
        /// </summary>
        /// <param name="collider"></param>
        /// <returns></returns>
        public static VGO_Collider CreateFrom(Collider collider)
        {
            if (collider == null)
            {
                return null;
            }

            Type type = collider.GetType();

            if (type == typeof(BoxCollider))
            {
                var boxCollider = collider as BoxCollider;

                return new VGO_Collider()
                {
                    type = ColliderType.Box,
                    enabled = boxCollider.enabled,
                    isTrigger = boxCollider.isTrigger,
                    center = boxCollider.center.ReverseZ().ToNumericsVector3(),
                    size = boxCollider.size.ToNumericsVector3(),
                    physicMaterial = VgoPhysicMaterialConverter.CreateFrom(collider.sharedMaterial),
                };
            }
            else if (type == typeof(CapsuleCollider))
            {
                var capsuleCollider = collider as CapsuleCollider;

                return new VGO_Collider()
                {
                    type = ColliderType.Capsule,
                    enabled = capsuleCollider.enabled,
                    isTrigger = capsuleCollider.isTrigger,
                    center = capsuleCollider.center.ReverseZ().ToNumericsVector3(),
                    radius = capsuleCollider.radius,
                    height = capsuleCollider.height,
                    direction = capsuleCollider.direction,
                    physicMaterial = VgoPhysicMaterialConverter.CreateFrom(collider.sharedMaterial),
                };
            }
            else if (type == typeof(SphereCollider))
            {
                var sphereCollider = collider as SphereCollider;

                return new VGO_Collider()
                {
                    type = ColliderType.Sphere,
                    enabled = sphereCollider.enabled,
                    isTrigger = sphereCollider.isTrigger,
                    center = sphereCollider.center.ReverseZ().ToNumericsVector3(),
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
        public static void SetComponentValue(Collider collider, VGO_Collider vgoCollider)
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

                if (vgoCollider.type == ColliderType.Box)
                {
                    boxCollider.enabled = vgoCollider.enabled;
                    boxCollider.isTrigger = vgoCollider.isTrigger;
                    boxCollider.center = vgoCollider.center.GetValueOrDefault(System.Numerics.Vector3.Zero).ToUnityVector3().ReverseZ();
                    boxCollider.size = vgoCollider.size.GetValueOrDefault(System.Numerics.Vector3.One).ToUnityVector3();
                    boxCollider.sharedMaterial = VgoPhysicMaterialConverter.ToPhysicMaterial(vgoCollider.physicMaterial);
                }
            }
            else if (type == typeof(CapsuleCollider))
            {
                var capsuleCollider = collider as CapsuleCollider;

                if (vgoCollider.type == ColliderType.Capsule)
                {
                    capsuleCollider.enabled = vgoCollider.enabled;
                    capsuleCollider.isTrigger = vgoCollider.isTrigger;
                    capsuleCollider.center = vgoCollider.center.GetValueOrDefault(System.Numerics.Vector3.Zero).ToUnityVector3().ReverseZ();
                    capsuleCollider.radius = vgoCollider.radius;
                    capsuleCollider.height = vgoCollider.height;
                    capsuleCollider.direction = vgoCollider.direction;
                    capsuleCollider.sharedMaterial = VgoPhysicMaterialConverter.ToPhysicMaterial(vgoCollider.physicMaterial);
                }
            }
            else if (type == typeof(SphereCollider))
            {
                var sphereCollider = collider as SphereCollider;

                if (vgoCollider.type == ColliderType.Sphere)
                {
                    sphereCollider.enabled = vgoCollider.enabled;
                    sphereCollider.isTrigger = vgoCollider.isTrigger;
                    sphereCollider.center = vgoCollider.center.GetValueOrDefault(System.Numerics.Vector3.Zero).ToUnityVector3().ReverseZ();
                    sphereCollider.radius = vgoCollider.radius;
                    sphereCollider.sharedMaterial = VgoPhysicMaterialConverter.ToPhysicMaterial(vgoCollider.physicMaterial);
                }
            }
        }
    }
}