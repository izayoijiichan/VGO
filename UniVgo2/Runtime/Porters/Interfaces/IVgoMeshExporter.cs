// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : IVgoMeshExporter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// VGO Mesh Exporter Interface
    /// </summary>
    public interface IVgoMeshExporter
    {
        #region Public Methods

        /// <summary>
        /// Export meshes.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="unityMeshAssetList">List of unity mesh asset.</param>
        /// <param name="unityMaterialList">List of unity material.</param>
        void ExportMeshes(IVgoStorage vgoStorage, in IList<MeshAsset> unityMeshAssetList, in IList<Material>? unityMaterialList = null);

        #endregion
    }
}
