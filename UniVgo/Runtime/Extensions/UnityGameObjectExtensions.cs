// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : UnityGameObjectExtensions
// ----------------------------------------------------------------------
namespace UniVgo
{
    using NewtonGltf;
    using System;
    using System.Linq;
    using UnityEngine;
    using UniVgo.Converters;

    /// <summary>
    /// UnityEngine.GameObject Extensions
    /// </summary>
    public static class UnityGameObjectExtensions
    {
        #region AddComponent

        /// <summary>
        /// Adds a VgoMeta component to the game object.
        /// </summary>
        /// <typeparam name="T">The type of the component to add.</typeparam>
        /// <param name="go"></param>
        /// <param name="gltfMeta"></param>
        /// <returns>Returns VgoMeta component.</returns>
        public static VgoMeta AddComponent<T>(this GameObject go, Gltf_VGO_Meta gltfMeta)
            where T : VgoMeta
        {
            VgoMeta vgoMeta = go.GetComponent<VgoMeta>();

            if (vgoMeta == null)
            {
                vgoMeta = go.AddComponent<VgoMeta>();
            }

            if (gltfMeta == null)
            {
                vgoMeta.Meta = null;
            }
            else
            {
                vgoMeta.Meta = new Gltf_VGO_Meta(gltfMeta);
            }

            return vgoMeta;
        }

        /// <summary>
        /// Adds a VgoRight component to the game object.
        /// </summary>
        /// <typeparam name="T">The type of the component to add.</typeparam>
        /// <param name="go"></param>
        /// <param name="gltfRight"></param>
        /// <returns>Returns VgoRight component.</returns>
        public static VgoRight AddComponent<T>(this GameObject go, Gltf_VGO_Right gltfRight)
            where T : VgoRight
        {
            VgoRight vgoRight = go.GetComponent<VgoRight>();

            if (vgoRight == null)
            {
                vgoRight = go.AddComponent<VgoRight>();
            }

            if (gltfRight == null)
            {
                vgoRight.Right = null;
            }
            else
            {
                vgoRight.Right = new Gltf_VGO_Right(gltfRight);
            }

            return vgoRight;
        }

        /// <summary>
        /// Adds a Collider component to the game object.
        /// </summary>
        /// <typeparam name="T">The type of the component to add.</typeparam>
        /// <param name="go"></param>
        /// <param name="vgoCollider"></param>
        /// <returns>Returns Collider component if successful, null otherwise.</returns>
        public static Collider AddComponent<T>(this GameObject go, VGO_Collider vgoCollider)
            where T : Collider
        {
            if (vgoCollider == null)
            {
                return null;
            }

            Collider collider;

            switch (vgoCollider.type)
            {
                case ColliderType.Box:
                    collider = go.AddComponent<BoxCollider>();
                    break;
                case ColliderType.Capsule:
                    collider = go.AddComponent<CapsuleCollider>();
                    break;
                case ColliderType.Sphere:
                    collider = go.AddComponent<SphereCollider>();
                    break;
                default:
                    collider = null;
                    break;
            }

            if (collider != null)
            {
                VgoColliderConverter.SetComponentValue(collider, vgoCollider);
            }

            return collider;
        }

        /// <summary>
        /// Adds a Rigidbody component to the game object.
        /// </summary>
        /// <typeparam name="T">The type of the component to add.</typeparam>
        /// <param name="go"></param>
        /// <param name="vgoRigidbody"></param>
        /// <returns>Returns Rigidbody component.</returns>
        public static Rigidbody AddComponent<T>(this GameObject go, VGO_Rigidbody vgoRigidbody)
            where T : Rigidbody
        {
            Rigidbody rigidbody = go.GetComponent<Rigidbody>();

            if (rigidbody == null)
            {
                rigidbody = go.AddComponent<Rigidbody>();
            }

            if (vgoRigidbody != null)
            {
                VgoRigidbodyConverter.SetComponentValue(rigidbody, vgoRigidbody);
            }

            return rigidbody;
        }

        /// <summary>
        /// Adds a Light component to the game object.
        /// </summary>
        /// <typeparam name="T">The type of the component to add.</typeparam>
        /// <param name="go"></param>
        /// <param name="vgoLight"></param>
        /// <returns>Returns Light component.</returns>
        /// <remarks>Light is sealed class.</remarks>
        public static Light AddComponent<T>(this GameObject go, VGO_Light vgoLight)
            //where T : Light
        {
            if (typeof(T) != typeof(Light))
            {
                return null;
            }

            Light light = go.GetComponent<Light>();

            if (light == null)
            {
                light = go.AddComponent<Light>();
            }

            if (vgoLight != null)
            {
                VgoLightConverter.SetComponentValue(light, vgoLight);
            }

            return light;
        }

        #endregion

        #region GetComponent

        /// <summary>
        /// Gets the component of the specified type, if it exists.
        /// </summary>
        /// <param name="go"></param>
        /// <param name="type">The type of the component to retrieve.</param>
        /// <param name="component">The output argument that will contain the component or null.</param>
        /// <returns>Returns true if the component is found, false otherwise.</returns>
        public static bool TryGetComponentEx(this GameObject go, Type type, out Component component)
        {
#if UNITY_2019_2_OR_NEWER
            return go.TryGetComponent(type, out component);
#else
            component = go.GetComponent(type);

            return component != null;
#endif
        }

        /// <summary>
        /// Gets the component of the specified type, if it exists.
        /// </summary>
        /// <typeparam name="T">The type of the component to retrieve.</typeparam>
        /// <param name="go"></param>
        /// <param name="component">The output argument that will contain the component or null.</param>
        /// <returns>Returns true if the component is found, false otherwise.</returns>
        public static bool TryGetComponentEx<T>(this GameObject go, out T component)
        {
#if UNITY_2019_2_OR_NEWER
            return go.TryGetComponent(out component);
#else
            component = go.GetComponent<T>();

            return component != null;
#endif
        }

        /// <summary>
        /// Gets the components of the specified type, if it exists.
        /// </summary>
        /// <typeparam name="T">The type of the component to retrieve.</typeparam>
        /// <param name="go"></param>
        /// <param name="components">The output argument that will contain the components or null.</param>
        /// <returns>Returns true if the components is found, false otherwise.</returns>
        public static bool TryGetComponentsEx<T>(this GameObject go, out T[] components)
        {
            components = go.GetComponents<T>();

            return components.Any();
        }

        #endregion
    }
}
