// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : BlendShapesContext
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using NewtonVgo;
    using System.Collections.Generic;

    /// <summary>
    /// BlendShapes Context
    /// </summary>
    public class BlendShapesContext
    {
        /// <summary>A blend shape configuration.</summary>
        public BlendShapeConfig blendShapeConfig = new BlendShapeConfig();

        /// <summary>List of blend shape context.</summary>
        public List<BlendShapeContext> blendShapeContexts = new List<BlendShapeContext>();
    }
}
