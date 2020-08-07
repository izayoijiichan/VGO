// ----------------------------------------------------------------------
// @Namespace : UniVgo.Porters
// @Class     : IMaterialImporter
// ----------------------------------------------------------------------
namespace UniVgo.Porters
{
    using NewtonGltf;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Material Importer Interface
    /// </summary>
    public interface IMaterialImporter
    {
        #region Properties

        /// <summary>The material porter store.</summary>
        IMaterialPorterStore MaterialPorterStore { get; set; }

        /// <summary>The shader store.</summary>
        IShaderStore ShaderStore { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Create a material info.
        /// </summary>
        /// <param name="materialIndex">The index of gltf.material.</param>
        /// <param name="gltfMaterial">A gltf.material.</param>
        /// <param name="gltfMeshList">List of gltf.material.</param>
        /// <param name="textureInfoList">List of texture info.</param>
        /// <returns>A material info.</returns>
        MaterialInfo CreateMaterialInfo(int materialIndex, GltfMaterial gltfMaterial, List<GltfMesh> gltfMeshList, List<TextureInfo> textureInfoList);

        /// <summary>
        /// Create a material.
        /// </summary>
        /// <param name="materialInfo">A material info.</param>
        /// <param name="texture2dList">List of Texture2D.</param>
        /// <returns>A unity material.</returns>
        Material CreateMaterialAsset(MaterialInfo materialInfo, List<Texture2D> texture2dList);

        #endregion
    }
}
