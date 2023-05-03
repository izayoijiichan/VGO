// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : ParticleSystemAsset
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using UnityEngine;

    /// <summary>
    /// Particle System Asset
    /// </summary>
    public class ParticleSystemAsset
    {
        /// <summary>Particle System</summary>
        private readonly ParticleSystem _ParticleSystem;

        /// <summary>Particle System Renderer</summary>
        private readonly ParticleSystemRenderer _ParticleSystemRenderer;

        /// <summary>Particle System</summary>
        public ParticleSystem ParticleSystem => _ParticleSystem;

        /// <summary>Particle System Renderer</summary>
        public ParticleSystemRenderer ParticleSystemRenderer => _ParticleSystemRenderer;

        /// <summary>
        /// Create a new instance of ParticleSystemAsset with particleSystem and particleSystemRenderer.
        /// </summary>
        /// <param name="particleSystem"></param>
        /// <param name="particleSystemRenderer"></param>
        public ParticleSystemAsset(ParticleSystem particleSystem, ParticleSystemRenderer particleSystemRenderer)
        {
            _ParticleSystem = particleSystem;

            _ParticleSystemRenderer = particleSystemRenderer;
        }
    }
}
