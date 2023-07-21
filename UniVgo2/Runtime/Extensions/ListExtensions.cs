// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : ListExtensions
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// List Extensions
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Searches for the specified object and returns the zero-based index of the first occurrence within the entire List&lt;T&gt;.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="item">The object to locate in the List&lt;T&gt;. The value can be null for reference types.</param>
        /// <returns>The zero-based index of the first occurrence of item within the entire List&lt;T&gt;, if found; otherwise, -1.</returns>
        public static int IndexOf<T>(this IReadOnlyList<T> self, in T item)
        {
            if (item == null)
            {
                return -1;
            }

            for (int index = 0; index < self.Count; index++)
            {
                if (Equals(self[index], item))
                {
                    return index;
                }
            }

            return -1;
        }

        /// <summary>
        /// Searches for the specified object and returns the zero-based index of the first occurrence within the entire List&lt;T&gt;.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="predicate"></param>
        /// <returns>The zero-based index of the first occurrence of item within the entire List&lt;T&gt;, if found; otherwise, -1.</returns>
        public static int IndexOf<T>(this IReadOnlyList<T> self, in Func<T, bool> predicate)
        {
            for (int index = 0; index < self.Count; index++)
            {
                if (predicate(self[index]))
                {
                    return index;
                }
            }

            return -1;
        }

        /// <summary>
        /// Searches for the specified object and returns the zero-based index of the first occurrence within the entire List&lt;T&gt;.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="predicate"></param>
        /// <returns>The zero-based index of the first occurrence of item within the entire List&lt;T&gt;, if found; otherwise, -1.</returns>
        public static int IndexOf<T>(this IList<T> self, in Func<T, bool> predicate)
        {
            for (int index = 0; index < self.Count; index++)
            {
                if (predicate(self[index]))
                {
                    return index;
                }
            }

            return -1;
        }
    }
}
