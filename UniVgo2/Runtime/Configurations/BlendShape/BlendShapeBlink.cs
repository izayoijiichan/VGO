// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : BlendShapeBlink
// ----------------------------------------------------------------------
namespace UniVgo2
{
    using NewtonVgo;
    using System;

    /// <summary>
    /// BlendShape Blink
    /// </summary>
    [Serializable]
    public class BlendShapeBlink
    {
        /// <summary>The type of blink.</summary>
        public VgoBlendShapeBlinkType type;

        /// <summary>The index of the BlendShape.</summary>
        public int index;
    }
}
