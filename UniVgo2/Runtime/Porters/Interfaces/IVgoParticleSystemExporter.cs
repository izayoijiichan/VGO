// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Interface : IVgoParticleSystemExporter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using UnityEngine;

    /// <summary>
    /// VGO Particle System Exporter Interface
    /// </summary>
    public interface IVgoParticleSystemExporter
    {
        #region Public Methods

        /// <summary>
        /// Create VgoParticleSystem.
        /// </summary>
        /// <param name="particleSystem"></param>
        /// <param name="particleSystemRenderer"></param>
        /// <param name="vgoStorage"></param>
        /// <param name="exportTexture"></param>
        /// <returns></returns>
        VgoParticleSystem Create(in ParticleSystem particleSystem, in ParticleSystemRenderer particleSystemRenderer, in IVgoStorage vgoStorage, in ExportTextureDelegate exportTexture);

        #endregion
    }
}
