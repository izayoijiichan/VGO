// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Enum      : TextureType
// ----------------------------------------------------------------------
namespace UniVgo
{
    /// <summary></summary>
    public enum TextureType
    {
        /// <summary></summary>
        Default = 0,
        /// <summary>The normal map texture.</summary>
        NormalMap,
        /// <summary>The height map texture.</summary>
        HeightMap,
        /// <summary>The occlusion map texture.</summary>
        OcclusionMap,
        /// <summary>The emission map texture.</summary>
        EmissionMap,
        /// <summary>The metallic-roughness (metallic-smoothness) map texture.</summary>
        MetallicRoughnessMap,
        /// <summary>The specular-glossiness (specular-smoothness) map texture.</summary>
        SpecularGlossinessMap,
        /// <summary>The cube map texture.</summary>
        CubeMap,
        /// <summary></summary>
        Unknown,
    }
}
