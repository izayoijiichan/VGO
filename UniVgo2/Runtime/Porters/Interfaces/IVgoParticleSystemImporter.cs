// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : IVgoParticleSystemImporter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using NewtonVgo.Schema.ParticleSystems;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// VGO Particle System Importer Interface
    /// </summary>
    public interface IVgoParticleSystemImporter
    {
        #region Public Methods

        /// <summary>
        /// Adds a ParticleSystem component to the game object.
        /// </summary>
        /// <param name="go"></param>
        /// <param name="vgoParticleSystem"></param>
        /// <param name="geometryCoordinate"></param>
        /// <param name="materialList"></param>
        /// <param name="texture2dList"></param>
        /// <returns>Returns ParticleSystem component.</returns>
        ParticleSystem AddComponent(GameObject go, VgoParticleSystem vgoParticleSystem, VgoGeometryCoordinate geometryCoordinate, IList<Material?>? materialList, IList<Texture2D?>? texture2dList);

        /// <summary>
        /// Set particleSystemRenderer field value.
        /// </summary>
        /// <param name="particleSystemRenderer"></param>
        /// <param name="vgoRenderer"></param>
        /// <param name="geometryCoordinate"></param>
        /// <param name="materialList"></param>
        void SetComponentValue(ParticleSystemRenderer particleSystemRenderer, VGO_PS_Renderer vgoRenderer, VgoGeometryCoordinate geometryCoordinate, IList<Material?>? materialList);

        #endregion
    }
}
