// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : BlendShapeContext
// ----------------------------------------------------------------------
namespace UniVgo2
{
    using UnityEngine;

    /// <summary>
    /// Blend Shape Context
    /// </summary>
    public class BlendShapeContext
    {
        /// <summary></summary>
        public string name;

        /// <summary></summary>
        public Vector3[] positions;

        /// <summary></summary>
        public Vector3[] normals;

        /// <summary></summary>
        public Vector3[] tangents;

        /// <summary>
        /// Create a new instance of BlendShape with name.
        /// </summary>
        /// <param name="name"></param>
        public BlendShapeContext(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return name ?? "{no name}";
        }
    }
}
