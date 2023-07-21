// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : UnityGameObjectExtensions
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using System;
    using System.Linq;
    using UnityEngine;

    /// <summary>
    /// UnityEngine.GameObject Extensions
    /// </summary>
    public static class UnityGameObjectExtensions
    {
        #region GetComponent

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

        #endregion
    }
}
