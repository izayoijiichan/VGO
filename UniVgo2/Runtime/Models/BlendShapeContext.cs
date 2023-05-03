// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : BlendShapeContext
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using UnityEngine;

    /// <summary>
    /// Blend Shape Context
    /// </summary>
    public class BlendShapeContext
    {
        /// <summary></summary>
        private readonly string _Name = string.Empty;

        /// <summary></summary>
        private Vector3[]? _Positions;

        /// <summary></summary>
        private Vector3[]? _Normals;

        /// <summary></summary>
        private Vector3[]? _Tangents;

        /// <summary></summary>
        public string Name => _Name;

        /// <summary></summary>
        public Vector3[]? Positions { get => _Positions; set => _Positions = value; }

        /// <summary></summary>
        public Vector3[]? Normals { get => _Normals; set => _Normals = value; }

        /// <summary></summary>
        public Vector3[]? Tangents { get => _Tangents; set => _Tangents = value; }

        /// <summary>
        /// Create a new instance of BlendShape with name.
        /// </summary>
        /// <param name="name"></param>
        public BlendShapeContext(string name)
        {
            _Name = name;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return _Name ?? "{no name}";
        }
    }
}
