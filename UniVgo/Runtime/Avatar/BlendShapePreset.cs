// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : BlendShapePreset
// ----------------------------------------------------------------------
namespace UniVgo
{
    using System;
    using System.Collections.Generic;
    using VgoGltf;

    /// <summary>
    /// BlendShape Preset
    /// </summary>
    [Serializable]
    public class BlendShapePreset
    {
        /// <summary>The name of preset.</summary>
        public string name;

        /// <summary>The type of preset.</summary>
        public VgoBlendShapePresetType type;

        /// <summary>List of binding.</summary>
        public List<BlendShapeBinding> bindings = new List<BlendShapeBinding>();
    }
}
