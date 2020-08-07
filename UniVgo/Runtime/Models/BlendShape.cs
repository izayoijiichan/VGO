// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : BlendShape
// ----------------------------------------------------------------------
namespace UniVgo
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Blend Shape
    /// </summary>
    public class BlendShape
    {
        /// <summary></summary>
        public string name;

        /// <summary></summary>
        public List<Vector3> Positions = new List<Vector3>();

        /// <summary></summary>
        public List<Vector3> Normals = new List<Vector3>();

        /// <summary></summary>
        public List<Vector3> Tangents = new List<Vector3>();

        /// <summary>
        /// Create a new instance of BlendShape with name.
        /// </summary>
        /// <param name="name"></param>
        public BlendShape(string name)
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
