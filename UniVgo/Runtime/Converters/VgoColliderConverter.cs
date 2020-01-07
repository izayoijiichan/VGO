// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : VgoColliderConverter
// ----------------------------------------------------------------------
namespace UniVgo
{
    using System;
    using UniGLTFforUniVgo;
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
        public static glTFNode_VGO_Collider CreateFrom(Collider collider)
        {
            if (collider == null)
            {
                return null;
            }

            Type type = collider.GetType();

            if (type == typeof(BoxCollider))
            {
                var boxCollider = collider as BoxCollider;

                return new glTFNode_VGO_Collider()
                {
                    type = ColliderType.Box,
                    enabled = boxCollider.enabled,
                    isTrigger = boxCollider.isTrigger,
                    center = boxCollider.center.ReverseZ().ToArray(),
                    size = boxCollider.size.ToArray(),
                    physicMaterial = VgoPhysicMaterialConverter.CreateFrom(collider.sharedMaterial),
                };
            }
            else if (type == typeof(CapsuleCollider))
            {
                var capsuleCollider = collider as CapsuleCollider;

                return new glTFNode_VGO_Collider()
                {
                    type = ColliderType.Capsule,
                    enabled = capsuleCollider.enabled,
                    isTrigger = capsuleCollider.isTrigger,
                    center = capsuleCollider.center.ReverseZ().ToArray(),
                    radius = capsuleCollider.radius,
                    height = capsuleCollider.height,
                    direction = capsuleCollider.direction,
                    physicMaterial = VgoPhysicMaterialConverter.CreateFrom(collider.sharedMaterial),
                };
            }
            else if (type == typeof(SphereCollider))
            {
                var sphereCollider = collider as SphereCollider;

                return new glTFNode_VGO_Collider()
                {
                    type = ColliderType.Sphere,
                    enabled = sphereCollider.enabled,
                    isTrigger = sphereCollider.isTrigger,
                    center = sphereCollider.center.ReverseZ().ToArray(),
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
        public static void SetComponentValue(Collider collider, glTFNode_VGO_Collider vgoCollider)
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
                    boxCollider.center = ConvertToVector3(vgoCollider.center, reverseZ: true);
                    boxCollider.size = ConvertToVector3(vgoCollider.size);
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
                    capsuleCollider.center = ConvertToVector3(vgoCollider.center, reverseZ: true);
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
                    sphereCollider.center = ConvertToVector3(vgoCollider.center, reverseZ: true);
                    sphereCollider.radius = vgoCollider.radius;
                    sphereCollider.sharedMaterial = VgoPhysicMaterialConverter.ToPhysicMaterial(vgoCollider.physicMaterial);
                }
            }
        }

        /// <summary>
        /// Convert float[3] to Vector3.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="reverseZ"></param>
        /// <returns></returns>
        private static Vector3 ConvertToVector3(float[] values, bool reverseZ = false)
        {
            if (values == null)
            {
                return default;
            }

            if (values.Length == 3)
            {
                Vector3 vecter3 = new Vector3(values[0], values[1], values[2]);

                if (reverseZ)
                {
                    return vecter3.ReverseZ();
                }
                else
                {
                    return vecter3;
                }
            }

            return default;
        }
    }
}