// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : CollectionsExtensions
// ----------------------------------------------------------------------
namespace UniVgo
{
    using System.Collections.Generic;

    /// <summary>
    /// Collections Extensions
    /// </summary>
    public static class CollectionsExtensions
    {
        /// <summary>
        /// Attempts to add the specified item to the collection.
        /// </summary>
        /// <typeparam name="T">Type of the item.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="item">The item to add.</param>
        /// <returns>true if the item was added to the collection successfully; otherwise, false.</returns>
        public static bool TryAdd<T>(this ICollection<T> collection, T item) where T : class
        {
            if (item == null)
            {
                return false;
            }

            if (collection.Contains(item))
            {
                return false;
            }

            collection.Add(item);

            return true;
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
