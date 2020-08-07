// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : AccessorTypeInfo
// ----------------------------------------------------------------------
namespace UniVgo
{
    using System;
    using VgoGltf;

    /// <summary>
    /// Accessor Type Info
    /// </summary>
    public class AccessorTypeInfo
    {
        /// <summary></summary>
        public Type SystemType;

        /// <summary></summary>
        public GltfAccessorType AccessorType;

        /// <summary></summary>
        public GltfComponentType ComponentType;

        /// <summary>
        /// Create a new instance of AccessorTypeInfo with systemType and accessorType and componentType.
        /// </summary>
        /// <param name="systemType"></param>
        /// <param name="accessorType"></param>
        /// <param name="componentType"></param>
        public AccessorTypeInfo(Type systemType, GltfAccessorType accessorType, GltfComponentType componentType)
        {
            SystemType = systemType;
            AccessorType = accessorType;
            ComponentType = componentType;
        }
    }
}
