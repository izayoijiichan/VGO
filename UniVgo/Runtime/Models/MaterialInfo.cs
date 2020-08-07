// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : MaterialInfo
// ----------------------------------------------------------------------
namespace UniVgo
{
    using NewtonGltf;
    using System.Collections.Generic;

    /// <summary>
    /// Material Info
    /// </summary>
    public class MaterialInfo
    {
        /// <summary></summary>
        public int materialIndex = -1;

        /// <summary></summary>
        public GltfMaterial gltfMaterial;
        
        /// <summary>The material name.</summary>
        public string name;

        /// <summary>The shader name.</summary>
        public string shaderName;

        /// <summary>The shader group.</summary>
        public ShaderGroup shaderGroup = ShaderGroup.Unknown;

        /// <summary>The lighting type.</summary>
        public MaterialLightingType lightingType = MaterialLightingType.Unknown;

        /// <summary></summary>
        public bool HasVertexColor { get; set; } = false;

        /// <summary></summary>
        public List<TextureInfo> TextureInfoList { get; } = new List<TextureInfo>();

        /// <summary>
        /// Create a new instance of MaterialInfo with materialIndex and gltfMaterial.
        /// </summary>
        /// <param name="materialIndex"></param>
        /// <param name="gltfMaterial"></param>
        public MaterialInfo(int materialIndex, GltfMaterial gltfMaterial)
        {
            this.materialIndex = materialIndex;
            this.gltfMaterial = gltfMaterial;
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
