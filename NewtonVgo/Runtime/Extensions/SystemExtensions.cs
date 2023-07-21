// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : SystemExtensions
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using System.Collections.Generic;

    /// <summary>
    /// System Extensions
    /// </summary>
    public static class SystemExtensions
    {
        /// <summary>
        /// Get a safe value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="defaultValue"></param>
        public static int SafeValue(this int value, in int min, in int max, in int defaultValue = 0)
        {
            if (value < min)
            {
                return defaultValue;
            }

            if (value > max)
            {
                return defaultValue;
            }

            return value;
        }

        /// <summary>
        /// Get a safe value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="defaultValue"></param>
        public static float SafeValue(this float value, in float min, in float max, in float defaultValue = 0.0f)
        {
            if (value < min)
            {
                return defaultValue;
            }

            if (value > max)
            {
                return defaultValue;
            }

            return value;
        }

        /// <summary>
        /// Whether the index is in the range of the collection.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="collection"></param>
        /// <returns>Returns true if the index is in the range of the collection, false otherwise.</returns>
        public static bool IsInRangeOf<T>(this int index, in ICollection<T> collection)
        {
            if (collection == null)
            {
                return false;
            }

            if (index < 0)
            {
                return false;
            }

            if (index >= collection.Count)
            {
                return false;
            }

            return true;
        }
    }
}
