using System;
using System.Collections.Generic;

namespace UniGLTFforUniVgo
{
    /// <summary>
    /// https://github.com/KhronosGroup/glTF/issues/1036
    /// </summary>
    [Serializable]
    public partial class glTFPrimitives_extras : ExtraBase<glTFPrimitives_extras>
    {
        //[JsonSchema(Required = true, MinItems = 1)]
        public List<string> targetNames = null;
    }
}
