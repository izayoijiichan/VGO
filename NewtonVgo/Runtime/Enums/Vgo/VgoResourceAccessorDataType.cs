// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Enum      : VgoResourceAccessorDataType
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    /// <summary>The data type of the resource accessor.</summary>
    public enum VgoResourceAccessorDataType
    {
        /// <summary></summary>
        None = 0,
        /// <summary>unsigned byte</summary>
        UnsignedByte = 0x10,
        /// <summary>signed byte</summary>
        SignedByte = 0x11,
        /// <summary>unsigned short</summary>
        UnsignedShort = 0x20,
        /// <summary>signed short</summary>
        Short = 0x21,
        /// <summary>unsigned int</summary>
        UnsignedInt = 0x30,
        /// <summary>signed int</summary>
        SignedInt = 0x31,
        /// <summary>single</summary>
        Float = 0x50,
        /// <summary>vector2 (int)</summary>
        Vector2Int32 = 0x1231,
        /// <summary>vector2 (float)</summary>
        Vector2Float = 0x1250,
        /// <summary>vector3 (int)</summary>
        Vector3Int32 = 0x1331,
        /// <summary>vector3 (float)</summary>
        Vector3Float = 0x1350,
        /// <summary>vector4 (byte)</summary>
        Vector4UInt8 = 0x1410,
        /// <summary>vector4 (ushort)</summary>
        Vector4UInt16 = 0x1420,
        /// <summary>vector4 (uint)</summary>
        Vector4UInt32 = 0x1430,
        /// <summary>vector4 (int)</summary>
        Vector4Int32 = 0x1431,
        /// <summary>vector4 (float)</summary>
        Vector4Float = 0x1450,
        /// <summary>matrix4 (float)</summary>
        Matrix4Float = 0x2450,
    };
}
