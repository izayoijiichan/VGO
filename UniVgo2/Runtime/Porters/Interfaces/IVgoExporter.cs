// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Interface : IVgoExporter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using UnityEngine;

    /// <summary>
    /// VGO Exporter Interface
    /// </summary>
    public interface IVgoExporter
    {
        #region Methods

        /// <summary>
        /// Create a vgo storage.
        /// </summary>
        /// <param name="root">The GameObject of root.</param>
        /// <param name="geometryCoordinate"></param>
        /// <param name="uvCoordinate"></param>
        /// <param name="textureType"></param>
        /// <returns>A vgo storage.</returns>
        IVgoStorage CreateVgoStorage(
            in GameObject root,
            in VgoGeometryCoordinate geometryCoordinate = VgoGeometryCoordinate.RightHanded,
            in VgoUVCoordinate uvCoordinate = VgoUVCoordinate.TopLeft,
            in ImageType textureType = ImageType.PNG);

        #endregion
    }
}
