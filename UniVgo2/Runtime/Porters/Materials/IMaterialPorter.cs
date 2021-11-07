// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : IMaterialPorter
// ----------------------------------------------------------------------
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
        ExportTextureDelegate ExportTexture { get; set; }

        /// <summary>List of all texture 2D.</summary>
        /// <remarks>for Import</remarks>
        List<Texture2D> AllTexture2dList { get; set; }

        #endregion

        #region Public Methods (Export)

        /// <summary>
        /// Create a vgo material.
        /// </summary>
        /// <param name="material">A unity material.</param>
        /// <returns>A vgo material.</returns>
        VgoMaterial CreateVgoMaterial(Material material);

        #endregion

        #region Public Methods (Import)

        /// <summary>
        /// Create a unity material.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="shader">A shader.</param>
        /// <returns>A unity material.</returns>
        Material CreateMaterialAsset(VgoMaterial vgoMaterial, Shader shader);

        #endregion
    }
}
