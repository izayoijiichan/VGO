// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : UnityGameObjectExtensions
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    /// <summary>
    /// UnityEngine.GameObject Extensions
    /// </summary>
    public static class UnityGameObjectExtensions
    {
        #region Methods (GetChild)

        /// <summary>
        /// Get the children.
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static IEnumerable<GameObject> GetChildren(this GameObject self)
        {
            foreach (Transform child in self.transform)
            {
                yield return child.gameObject;
            }
        }

        #endregion

        #region Methods (GetComponent)

        /// <summary>
        /// Gets the component of the specified type, if it exists.
        /// </summary>
        /// <param name="go"></param>
        /// <param name="type">The type of the component to retrieve.</param>
        /// <param name="component">The output argument that will contain the component or null.</param>
        /// <returns>Returns true if the component is found, false otherwise.</returns>
        public static bool TryGetComponentEx(this GameObject go, in Type type, out Component component)
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
        public static bool TryGetComponentEx<T>(this GameObject go, out T component) where T : Component
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
        public static bool TryGetComponentsEx<T>(this GameObject go, out T[] components) where T : Component
        {
            components = go.GetComponents<T>();

            return components.Any();
        }

        /// <summary>
        /// Gets references to all components of type T on the same GameObject as the component specified, and any child of the GameObject, if it exists.
        /// </summary>
        /// <typeparam name="T">The type of the component to retrieve.</typeparam>
        /// <param name="go"></param>
        /// <param name="includeInactive">Whether to include inactive child GameObjects in the search.</param>
        /// <param name="components"></param>
        /// <returns>Returns true if the components is found, false otherwise.</returns>
        public static bool TryGetComponentsInChildren<T>(this GameObject go, in bool includeInactive, out List<T> components) where T : Component
        {
            components = new List<T>(0);

            go.GetComponentsInChildren(includeInactive, components);

            return components.Any();
        }

        /// <summary>
        /// Gets the first component of the specified type, if it exists.
        /// </summary>
        /// <typeparam name="T">The type of the component to retrieve.</typeparam>
        /// <param name="go"></param>
        /// <param name="includeInactive">Whether to include inactive child GameObjects in the search.</param>
        /// <param name="component">The output argument that will contain the component or null.</param>
        /// <returns>Returns true if the components is found, false otherwise.</returns>
        /// <remarks>breadth-first search</remarks>
        public static bool TryGetFirstComponentInChildren<T>(this GameObject go, in bool includeInactive, out T component) where T : Component
        {
#pragma warning disable CS8625
            component = default;
#pragma warning restore CS8625

            if (includeInactive == false)
            {
                if (go.activeSelf == false)
                {
                    return false;
                }
            }

            if (go.TryGetComponentEx(out component))
            {
                return true;
            }

            return TryGetFirstComponentInChildrenRecursive(go, includeInactive, out component);
        }

        /// <summary>
        /// Gets the first component of the specified type, if it exists.
        /// </summary>
        /// <typeparam name="T">The type of the component to retrieve.</typeparam>
        /// <param name="go"></param>
        /// <param name="includeInactive">Whether to include inactive child GameObjects in the search.</param>
        /// <param name="component">The output argument that will contain the component or null.</param>
        /// <returns>Returns true if the components is found, false otherwise.</returns>
        /// <remarks>breadth-first search</remarks>
        private static bool TryGetFirstComponentInChildrenRecursive<T>(this GameObject go, in bool includeInactive, out T component) where T : Component
        {
#pragma warning disable CS8625
            component = default;
#pragma warning restore CS8625

            if (includeInactive == false)
            {
                if (go.activeSelf == false)
                {
                    return false;
                }
            }

            GameObject[] children = go.GetChildren().ToArray();

            if (includeInactive)
            {
                // Sibling
                for (int index = 0; index < children.Length; index++)
                {
                    GameObject child = children[index];

                    if (child.TryGetComponentEx(out component))
                    {
                        return true;
                    }
                }

                // Child
                for (int index = 0; index < children.Length; index++)
                {
                    GameObject child = children[index];

                    if (TryGetFirstComponentInChildrenRecursive(child, includeInactive, out component))
                    {
                        return true;
                    }
                }
            }
            else
            {
                // Sibling
                for (int index = 0; index < children.Length; index++)
                {
                    GameObject child = children[index];

                    if (child.activeSelf == false)
                    {
                        continue;
                    }

                    if (child.TryGetComponentEx(out component))
                    {
                        return true;
                    }
                }

                // Child
                for (int index = 0; index < children.Length; index++)
                {
                    GameObject child = children[index];

                    if (child.activeSelf == false)
                    {
                        continue;
                    }

                    if (TryGetFirstComponentInChildrenRecursive(child, includeInactive, out component))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion
    }
}
