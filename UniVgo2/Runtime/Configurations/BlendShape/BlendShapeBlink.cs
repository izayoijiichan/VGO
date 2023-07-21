// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : BlendShapeBlink
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using NewtonVgo;
    using System;
    using UnityEngine;

    /// <summary>
    /// BlendShape Blink
    /// </summary>
    [Serializable]
    public class BlendShapeBlink
    {
        /// <summary>The type of blink.</summary>
        [SerializeField]
        private VgoBlendShapeBlinkType type;

        /// <summary>The type of blink.</summary>
        public VgoBlendShapeBlinkType Type
        {
            get => type;
            set => type = value;
        }

        /// <summary>The index of the BlendShape.</summary>
        [SerializeField]
        private int index;

        /// <summary>The index of the BlendShape.</summary>
        public int Index
        {
            get => index;
            set => index = value;
        }
    }
}
