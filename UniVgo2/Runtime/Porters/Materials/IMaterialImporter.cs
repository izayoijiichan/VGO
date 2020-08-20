// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : IMaterialImporter
// ----------------------------------------------------------------------
namespace UniVgo2.Porters
{
    using NewtonVgo;
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
        /// Create a material.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="texture2dList">List of Texture2D.</param>
        /// <returns>A unity material.</returns>
        Material CreateMaterialAsset(VgoMaterial vgoMaterial, List<Texture2D> texture2dList);

        #endregion
    }
}
