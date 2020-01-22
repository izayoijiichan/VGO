// ----------------------------------------------------------------------
// @Namespace : UniStandardParticle
// @Class     : 
// ----------------------------------------------------------------------
namespace UniStandardParticle
{
    /// <summary></summary>
    public enum BlendMode
    {
        /// <summary></summary>
        Opaque = 0,
        /// <summary></summary>
        Cutout = 1,
        /// <summary></summary>
        Fade = 2,
        /// <summary></summary>
        Transparent = 3,
        /// <summary></summary>
        Additive = 4,
        /// <summary></summary>
        Subtractive = 5,
        /// <summary></summary>
        Modulate = 6,
    }

    /// <summary></summary>
    public enum ColorMode
    {
        /// <summary></summary>
        Multiply = 0,
        /// <summary></summary>
        Additive = 1,
        /// <summary></summary>
        Subtractive = 2,
        /// <summary></summary>
        Overlay = 3,
        /// <summary></summary>
        Color = 4,
        /// <summary></summary>
        Difference = 5,
    }

    /// <summary></summary>
    public enum FlipBookMode
    {
        /// <summary></summary>
        Simple = 0,
        /// <summary></summary>
        Blended = 1,
    }
}