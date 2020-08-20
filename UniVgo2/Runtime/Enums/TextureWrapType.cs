// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Enum      : TextureWrapType
// ----------------------------------------------------------------------
namespace UniVgo2
{
    /// <summary></summary>
    public enum TextureWrapType
    {
        /// <summary></summary>
        All,
#if UNITY_2017_1_OR_NEWER
        /// <summary></summary>
        U,
        /// <summary></summary>
        V,
        /// <summary></summary>
        W,
#endif
    }
}
