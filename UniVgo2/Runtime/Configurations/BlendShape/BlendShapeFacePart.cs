// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : BlendShapeFacePart
// ----------------------------------------------------------------------
namespace UniVgo2
{
    using NewtonVgo;
    using System;

    /// <summary>
    /// VGO BlendShape Face Part
    /// </summary>
    [Serializable]
    public class BlendShapeFacePart
    {
        /// <summary>The index of the BlendShape.</summary>
        public int index;

        /// <summary>The type of the face parts.</summary>
        public VgoBlendShapeFacePartsType type;
    }
}
