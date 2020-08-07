// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : AccessorTypeInfoCollection
// ----------------------------------------------------------------------
namespace UniVgo
{
    using System;
    using System.Collections.ObjectModel;
    using VgoGltf;

    /// <summary>
    /// AccessorTypeInfo Collection
    /// </summary>
    public class AccessorTypeInfoCollection : KeyedCollection<Type, AccessorTypeInfo>
    {
        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="type">The key of the item to get.</param>
        /// <param name="item">When this method returns true, contains the value associated with the specified key; otherwise, the default value for the type of the value parameter.</param>
        /// <returns>true if the collection contains an element with the specified key; otherwise, false.</returns>
        public bool TryGetValue(Type type, out AccessorTypeInfo item)
        {
            if (Contains(type))
            {
                item = this[type];

                return true;
            }
            else
            {
                item = default;

                return false;
            }
        }
        /// <summary>
        /// Check if the specified type and accessorType and componentType matches.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="accessorType"></param>
        /// <param name="componentType"></param>
        /// <returns></returns>
        public bool IsMatch(Type type, GltfAccessorType accessorType, GltfComponentType componentType)
        {
            if (Contains(type))
            {
                AccessorTypeInfo item = this[type];

                if ((accessorType == item.AccessorType) &&
                    (componentType == item.ComponentType))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the type of an item contained in the collection.
        /// </summary>
        /// <param name="item">The item in the collection whose type is to be retrieved.</param>
        /// <returns>The type of the specified item in the collection.</returns>
        protected override Type GetKeyForItem(AccessorTypeInfo item)
        {
            return item.SystemType;
        }
    }
}
