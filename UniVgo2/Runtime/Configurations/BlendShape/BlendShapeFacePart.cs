// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : BlendShapeFacePart
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using NewtonVgo;
    using System;
    using UnityEngine;

    /// <summary>
    /// VGO BlendShape Face Part
    /// </summary>
    [Serializable]
    public class BlendShapeFacePart
    {
        /// <summary>The index of the BlendShape.</summary>
        [SerializeField]
        private int index;

        /// <summary>The index of the BlendShape.</summary>
        public int Index
        {
            get => index;
            set => index = value;
        }

        /// <summary>The type of the face parts.</summary>
        [SerializeField]
        private VgoBlendShapeFacePartsType type;

        /// <summary>The type of the face parts.</summary>
        public VgoBlendShapeFacePartsType Type
        {
            get => type;
            set => type = value;
        }
    }
}
