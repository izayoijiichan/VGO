// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : ExportTextureItem
// ----------------------------------------------------------------------
namespace UniVgo
{
    using System.Numerics;
    using VgoGltf;

    /// <summary>
    /// Export Texture Item
    /// </summary>
    public class ExportTextureItem
    {
        /// <summary>The instance ID of texture2D.</summary>
        public int instanceId = -1;

        /// <summary>The name of glTF texture.</summary>
        public string name;

        /// <summary>The name of texture2D.</summary>
        public string texture2dName;

        /// <summary>The texture type.</summary>
        public TextureType textureType = TextureType.Default;

        /// <summary>Image color space.</summary>
        public ColorSpaceType colorSpace = ColorSpaceType.Srgb;

        /// <summary>The name of image.</summary>
        public string imageName;

        /// <summary>The uri of the image.</summary>
        public string uri;

        /// <summary>The image's MIME type.</summary>
        public string mimeType;

        /// <summary></summary>
        public byte[] imageBytes;

        /// <summary>Magnification filter.</summary>
        public GltfTextureMagFilterMode magFilter = GltfTextureMagFilterMode.NONE;

        /// <summary>Minification filter.</summary>
        public GltfTextureMinFilterMode minFilter = GltfTextureMinFilterMode.NONE;

        /// <summary>s wrapping mode.</summary>
        public GltfTextureWrapMode wrapS = GltfTextureWrapMode.NONE;

        /// <summary>t wrapping mode.</summary>
        public GltfTextureWrapMode wrapT = GltfTextureWrapMode.NONE;

        /// <summary></summary>
        public Vector2 offset = Vector2.Zero;

        /// <summary></summary>
        public Vector2 scale = Vector2.One;

        /// <summary></summary>
        public int texCoord = 0;

        /// <summary>The index of gltf.textures.</summary>
        public int textureIndex = -1;

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
