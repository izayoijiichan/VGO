// ----------------------------------------------------------------------
// @Namespace : UniStandardParticle
// @Class     : ParticleDefinition
// ----------------------------------------------------------------------
namespace UniStandardParticle
{
    using UnityEngine;
    using UnityEngine.Rendering;

    /// <summary>
    /// 
    /// </summary>
    public class ParticleDefinition
    {
        /// <summary></summary>
        public BlendMode RenderMode { get; set; }

        /// <summary></summary>
        public ColorMode ColorMode { get; set; }

        /// <summary></summary>
        public FlipBookMode FlipBookMode { get; set; }

        /// <summary></summary>
        public CullMode CullMode { get; set; }

        /// <summary></summary>
        public bool TwoSided { get; set; }

        /// <summary></summary>
        public bool SoftParticlesEnabled { get; set; }

        /// <summary></summary>
        public Vector4 SoftParticleFadeParams { get; set; }

        /// <summary></summary>
        public bool CameraFadingEnabled { get; set; }

        /// <summary></summary>
        public Vector4 CameraFadeParams { get; set; }

        /// <summary></summary>
        public bool DistortionEnabled { get; set; }

        /// <summary></summary>
        public Texture2D GrabTexture { get; set; }

        /// <summary></summary>
        public float DistortionStrengthScaled { get; set; }

        /// <summary></summary>
        public float DistortionBlend { get; set; }

        /// <summary></summary>
        public Color ColorAddSubDiff { get; set; }

        /// <summary></summary>
        public Texture2D MainTex { get; set; }

        /// <summary></summary>
        public Vector4 MainTexSt { get; set; }

        /// <summary></summary>
        public Color Color { get; set; }

        /// <summary></summary>
        public float Cutoff { get; set; }

        /// <summary></summary>
        public Texture2D MetallicGlossMap { get; set; }

        /// <summary></summary>
        public float Metallic { get; set; }

        /// <summary></summary>
        public float Glossiness { get; set; }

        /// <summary></summary>
        public Texture2D BumpMap { get; set; }

        /// <summary></summary>
        public float BumpScale { get; set; }

        /// <summary></summary>
        public bool LightingEnabled { get; set; }

        /// <summary></summary>
        public bool EmissionEnabled { get; set; }

        /// <summary></summary>
        public Color EmissionColor { get; set; }

        /// <summary></summary>
        public Texture2D EmissionMap { get; set; }
    }
}