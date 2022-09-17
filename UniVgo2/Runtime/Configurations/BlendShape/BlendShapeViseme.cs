// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : BlendShapeViseme
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using NewtonVgo;
    using System;

    /// <summary>
    /// BlendShape Viseme
    /// </summary>
    [Serializable]
    public class BlendShapeViseme
    {
        /// <summary>The type of viseme.</summary>
        public VgoBlendShapeVisemeType type;

        /// <summary>The index of the BlendShape.</summary>
        public int index;
    }
}
