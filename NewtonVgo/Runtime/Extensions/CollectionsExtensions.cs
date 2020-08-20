// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : CollectionsExtensions
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    using System.Collections.Generic;

    /// <summary>
    /// Collections Extensions
    /// </summary>
    public static class CollectionsExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="pair"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Deconstruct<T, U>(this KeyValuePair<T, U> pair, out T key, out U value)
        {
            key = pair.Key;
            value = pair.Value;
        }

        /// <summary>
        /// Gets the value associated with the specified index or default value.
        /// </summary>
        /// <typeparam name="T">Type of the item.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="index">The index of the list.</param>
        /// <returns></returns>
        public static T GetValueOrDefault<T>(this IList<T> list, int index)
        {
            if (index < 0)
            {
                return default;
            }
            if (index >= list.Count)
            {
                return default;
            }

            return list[index];
        }

        /// <summary>
        /// Attempts to get the value associated with the specified index.
        /// </summary>
        /// <typeparam name="T">Type of the item.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="index">The index of the list.</param>
        /// <param name="item"></param>
        /// <returns>true if the list contains a item with the specified index; otherwise, false.</returns>
        public static bool TryGetValue<T>(this IList<T> list, int index, out T item)
        {
            item = default;

            if (index < 0)
            {
                return false;
            }
            if (index >= list.Count)
            {
                return false;
            }

            item = list[index];

            return true;
        }
    }
}
