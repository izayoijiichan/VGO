// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : IVgoMeshImporter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// VGO Mesh Importer Interface
    /// </summary>
    public interface IVgoMeshImporter
    {
        #region Public Methods

        /// <summary>
        /// Create a mesh asset.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="meshIndex">The index of vgo mesh.</param>
        /// <param name="unityMaterialList">List of unity material.</param>
        /// <returns>A mesh asset.</returns>
        MeshAsset CreateMeshAsset(IVgoStorage vgoStorage, int meshIndex, IList<Material?>? unityMaterialList = null);

        #endregion
    }
}
