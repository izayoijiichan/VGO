// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : ResourceAccessorDataTypeExtensions
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    using System;

    /// <summary>
    /// ResourceAccessorDataType Extensions
    /// </summary>
    public static class ResourceAccessorDataTypeExtensions
    {
        /// <summary>
        /// Get byte size of this dataType.
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns>The byte size.</returns>
        public static int ByteSize(this VgoResourceAccessorDataType dataType)
        {
            switch (dataType)
            {
                case VgoResourceAccessorDataType.UnsignedByte:
                case VgoResourceAccessorDataType.SignedByte:
                    return 1;
                case VgoResourceAccessorDataType.UnsignedShort:
                case VgoResourceAccessorDataType.Short:
                    return 2;
                case VgoResourceAccessorDataType.UnsignedInt:
                case VgoResourceAccessorDataType.SignedInt:
                case VgoResourceAccessorDataType.Float:
                    return 4;
                case VgoResourceAccessorDataType.Vector2Int32:
                case VgoResourceAccessorDataType.Vector2Float:
                    return 8;
                case VgoResourceAccessorDataType.Vector3Int32:
                case VgoResourceAccessorDataType.Vector3Float:
                    return 12;
                case VgoResourceAccessorDataType.Vector4UInt8:
                    return 4;
                case VgoResourceAccessorDataType.Vector4UInt16:
                    return 8;
                case VgoResourceAccessorDataType.Vector4UInt32:
                case VgoResourceAccessorDataType.Vector4Int32:
                case VgoResourceAccessorDataType.Vector4Float:
                    return 16;
                case VgoResourceAccessorDataType.Matrix4Float:
                    return 64;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
