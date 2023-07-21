// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : BlendShapeViseme
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using NewtonVgo;
    using System;
    using UnityEngine;

    /// <summary>
    /// BlendShape Viseme
    /// </summary>
    [Serializable]
    public class BlendShapeViseme
    {
        /// <summary>The type of viseme.</summary>
        [SerializeField]
        private VgoBlendShapeVisemeType type;

        /// <summary>The type of viseme.</summary>
        public VgoBlendShapeVisemeType Type
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
