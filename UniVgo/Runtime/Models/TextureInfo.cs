// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : TextureInfo
// ----------------------------------------------------------------------
namespace UniVgo
{
    using NewtonGltf;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Texture Info
    /// </summary>
    public class TextureInfo
    {
        /// <summary>The index of gltf.textures.</summary>
        public int textureIndex = -1;

        /// <summary>The texture name.</summary>
        public string name;

        /// <summary>The image.</summary>
        public GltfImage source;

        /// <summary>The gltf.sampler.</summary>
        public GltfTextureSampler sampler;

        /// <summary></summary>
        public int texCoord = 0;

        /// <summary></summary>
        public KHR_texture_transform transform = null;

        /// <summary>The texture type.</summary>
        public TextureType textureType = TextureType.Default;

        /// <summary>Image color space.</summary>
        public ColorSpaceType colorSpace = ColorSpaceType.Srgb;

        /// <summary>Normal map scale.</summary>
        public float normalTextureScale = 0.0f;

        /// <summary>Occlusion map strength.</summary>
        public float occlusionStrength = 0.0f;

        /// <summary>The metallic-roughness of the material.</summary>
        public float metallicRoughness = 0.0f;

        /// <summary>The metallic-smoothness of the material.</summary>
        public float MetallicSmoothness => (metallicRoughness == -1.0f) ? -1.0f : (1.0f - metallicRoughness);

        /// <summary>The metallic-glossiness of the material.</summary>
        public float MetallicGlossiness => MetallicSmoothness;

        /// <summary></summary>
        public bool IsLinear => (colorSpace == ColorSpaceType.Linear);

        /// <summary>List of MaterialInfo.</summary>
        public List<MaterialInfo> MaterialInfoList { get; } = new List<MaterialInfo>();

        /// <summary>
        /// Create a new instance of TextureInfo with textureIndex name and source and sampler.
        /// </summary>
        /// <param name="textureIndex"></param>
        /// <param name="name"></param>
        /// <param name="source"></param>
        /// <param name="sampler"></param>
        public TextureInfo(int textureIndex, string name, GltfImage source, GltfTextureSampler sampler)
        {
            this.textureIndex = textureIndex;
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.source = source ?? throw new ArgumentNullException(nameof(source));
            this.sampler = sampler; //?? throw new ArgumentNullException(nameof(sampler));
        }

        /// <summary>
        /// Create a new instance of TextureInfo with textureIndex name and source and sampler and textureType and colorSpace.
        /// </summary>
        /// <param name="textureIndex"></param>
        /// <param name="name"></param>
        /// <param name="source"></param>
        /// <param name="sampler"></param>
        /// <param name="textureType"></param>
        /// <param name="colorSpace"></param>
        public TextureInfo(int textureIndex, string name, GltfImage source, GltfTextureSampler sampler, TextureType textureType, ColorSpaceType colorSpace)
            : this(textureIndex, name, source, sampler)
        {
            this.textureType = textureType;
            this.colorSpace = colorSpace;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return name ?? "{no name}";
        }
    }
}
