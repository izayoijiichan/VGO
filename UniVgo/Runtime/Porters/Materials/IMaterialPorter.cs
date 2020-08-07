// ----------------------------------------------------------------------
// @Namespace : UniVgo.Porters
// @Class     : IMaterialPorter
// ----------------------------------------------------------------------
namespace UniVgo.Porters
{
    using NewtonGltf;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Material Porter Interface
    /// </summary>
    public interface IMaterialPorter
    {
        #region Properties

        /// <summary>A delegate of ExportTexture method.</summary>
        /// <remarks>for Export</remarks>
        ExportTextureDelegate ExportTexture { get; set; }

        /// <summary>List of all texture info.</summary>
        /// <remarks>for Import</remarks>
        List<TextureInfo> AllTextureInfoList { get; set; }

        /// <summary>List of all texture 2D.</summary>
        /// <remarks>for Import</remarks>
        List<Texture2D> AllTexture2dList { get; set; }

        #endregion

        #region Public Methods (Export)

        /// <summary>
        /// Create a glTF material.
        /// </summary>
        /// <param name="material">A unity material.</param>
        /// <returns>A glTF material.</returns>
        GltfMaterial CreateGltfMaterial(Material material);

        #endregion

        #region Public Methods (Import)

        /// <summary>
        /// Set material texture info list.
        /// </summary>
        /// <param name="materialInfo">A material info.</param>
        /// <param name="allTextureInfoList">List of all texture info.</param>
        void SetMaterialTextureInfoList(MaterialInfo materialInfo, List<TextureInfo> allTextureInfoList);

        /// <summary>
        /// Create a unity material.
        /// </summary>
        /// <param name="src">A material info.</param>
        /// <param name="shader">A shader.</param>
        /// <returns>A unity material.</returns>
        Material CreateMaterialAsset(MaterialInfo materialInfo, Shader shader);

        #endregion
    }
}
