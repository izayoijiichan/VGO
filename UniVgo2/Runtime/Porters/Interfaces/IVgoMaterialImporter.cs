// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : IVgoMaterialImporter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// VGO Material Importer Interface
    /// </summary>
    public interface IVgoMaterialImporter
    {
        #region Properties

        /// <summary>The material porter store.</summary>
        IMaterialPorterStore? MaterialPorterStore { get; set; }

        /// <summary>The shader store.</summary>
        IShaderStore? ShaderStore { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Create a material.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="texture2dList">List of Texture2D.</param>
        /// <returns>A unity material.</returns>
        Material CreateMaterialAsset(in VgoMaterial vgoMaterial, in List<Texture2D?> texture2dList);

        #endregion
    }
}
