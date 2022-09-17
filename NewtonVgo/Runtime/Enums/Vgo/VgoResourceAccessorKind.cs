// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Enum      : VgoResourceAccessorKind
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    /// <summary>The kind of the resoure accessor.</summary>
    public enum VgoResourceAccessorKind
    {
        /// <summary></summary>
        None = 0,
        /// <summary>The image data.</summary>
        ImageData,
        /// <summary>The node transform.</summary>
        NodeTransform,
        /// <summary>The mesh data.</summary>
        MeshData,
        /// <summary>The skin data.</summary>
        SkinData,
        /// <summary>The cloth coefficients data.</summary>
        ClothCoefficients,
    };
}
