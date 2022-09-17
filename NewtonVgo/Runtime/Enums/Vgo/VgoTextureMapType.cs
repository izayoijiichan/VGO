// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Enum      : VgoTextureType
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    /// <summary>The map type of the texture.</summary>
    public enum VgoTextureMapType
    {
        /// <summary>Texture map type is unknown.</summary>
        Unknown = -1,
        /// <summary></summary>
        Default = 0,
        /// <summary>The normal map.</summary>
        NormalMap = 1,
        /// <summary>The height map.</summary>
        HeightMap = 2,
        /// <summary>The occlusion map.</summary>
        OcclusionMap = 3,
        /// <summary>The emission map.</summary>
        EmissionMap = 4,
        /// <summary>The metallic-roughness (metallic-smoothness) map.</summary>
        MetallicRoughnessMap = 5,
        /// <summary>The specular-glossiness (specular-smoothness) map.</summary>
        SpecularGlossinessMap = 6,
        /// <summary>The cube map.</summary>
        CubeMap = 7,
    }
}
