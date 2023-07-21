// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : IMaterialPorter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Porters
{
    using NewtonVgo;
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
        ExportTextureDelegate? ExportTexture { get; set; }

        #endregion

        #region Public Methods (Export)

        /// <summary>
        /// Create a vgo material.
        /// </summary>
        /// <param name="material">A unity material.</param>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <returns>A vgo material.</returns>
        VgoMaterial CreateVgoMaterial(in Material material, in IVgoStorage vgoStorage);

        #endregion

        #region Public Methods (Import)

        /// <summary>
        /// Create a unity material.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="shader">A shader.</param>
        /// <param name="allTexture2dList">List of all texture 2D.</param>
        /// <returns>A unity material.</returns>
        Material CreateMaterialAsset(in VgoMaterial vgoMaterial, in Shader shader, in List<Texture2D?> allTexture2dList);

        #endregion
    }
}
