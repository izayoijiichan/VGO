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
        ParticleSystem AddComponent(GameObject go, in VgoParticleSystem vgoParticleSystem, in VgoGeometryCoordinate geometryCoordinate, in IList<Material?>? materialList, in IList<Texture2D?>? texture2dList);

        /// <summary>
        /// Set particleSystemRenderer field value.
        /// </summary>
        /// <param name="particleSystemRenderer"></param>
        /// <param name="vgoRenderer"></param>
        /// <param name="geometryCoordinate"></param>
        /// <param name="materialList"></param>
        void SetComponentValue(ParticleSystemRenderer particleSystemRenderer, in VGO_PS_Renderer vgoRenderer, in VgoGeometryCoordinate geometryCoordinate, in IList<Material?>? materialList);

        #endregion
    }
}
