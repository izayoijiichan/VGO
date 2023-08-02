// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Enum      : VgoGeometryCoordinate
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    /// <summary>The type of the geometry coordinates.</summary>
    public enum VgoGeometryCoordinate : byte
    {
        /// <summary></summary>
        None = 0,
        /// <summary>right-handed</summary>
        RightHanded = 1,
        /// <summary>left-handed</summary>
        LeftHanded = 2,
    }
}
