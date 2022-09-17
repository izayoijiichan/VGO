// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : IVgoParticleSystemExporter
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
        VgoParticleSystem Create(ParticleSystem particleSystem, ParticleSystemRenderer particleSystemRenderer, IVgoStorage vgoStorage, ExportTextureDelegate exportTexture);

        #endregion
    }
}
