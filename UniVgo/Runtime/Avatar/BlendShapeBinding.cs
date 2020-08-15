// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : BlendShapeBinding
// ----------------------------------------------------------------------
namespace UniVgo
{
    using System;
    using UnityEngine;

    /// <summary>
    /// BlendShape Binding
    /// </summary>
    [Serializable]
    public class BlendShapeBinding
    {
        /// <summary>The index of the BlendShape.</summary>
        public int index;

        /// <summary>The weight for this BlendShape.</summary>
        [Range(0.0f, 100.0f)]
        public float weight;
    }
}
