// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : BlendShapesContext
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using System.Collections.Generic;

    /// <summary>
    /// BlendShapes Context
    /// </summary>
    public class BlendShapesContext
    {
        /// <summary>A blend shape configuration.</summary>
        private readonly BlendShapeConfig _BlendShapeConfig = new BlendShapeConfig();

        /// <summary>List of blend shape context.</summary>
        private List<BlendShapeContext> _BlendShapeContexts = new List<BlendShapeContext>();

        /// <summary>A blend shape configuration.</summary>
        public BlendShapeConfig BlendShapeConfig => _BlendShapeConfig;

        /// <summary>List of blend shape context.</summary>
        public List<BlendShapeContext> BlendShapeContexts { get => _BlendShapeContexts; set => _BlendShapeContexts = value; }
    }
}
