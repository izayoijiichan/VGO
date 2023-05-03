// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : MeshContext
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Mesh Context
    /// </summary>
    /// <remarks>MeshImporter.cs</remarks>
    public class MeshContext
    {
        /// <summary></summary>
        private readonly string _Name = string.Empty;

        /// <summary></summary>
        private Vector3[]? _Positions;

        /// <summary></summary>
        private Vector3[]? _Normals;

        /// <summary></summary>
        private Vector4[]? _Tangents;

        /// <summary></summary>
        private Vector2[]? _Uv0s;

        /// <summary></summary>
        private Vector2[]? _Uv1s;

        /// <summary></summary>
        private Vector2[]? _Uv2s;

        /// <summary></summary>
        private Vector2[]? _Uv3s;

        /// <summary></summary>
        private Color[]? _Colors;

        /// <summary></summary>
        private Color32[]? _Color32s;

        /// <summary></summary>
        private BoneWeight[]? _BoneWeights;

        /// <summary></summary>
        private List<int[]>? _SubMeshes;

        /// <summary></summary>
        private List<int>? _MaterialIndices;

        /// <summary></summary>
        private BlendShapesContext? _BlendShapesContext;

        /// <summary></summary>
        public string Name => _Name;

        /// <summary></summary>
        public Vector3[]? Positions { get => _Positions; set => _Positions = value; }

        /// <summary></summary>
        public Vector3[]? Normals { get => _Normals; set => _Normals = value; }

        /// <summary></summary>
        public Vector4[]? Tangents { get => _Tangents; set => _Tangents = value; }

        /// <summary></summary>
        public Vector2[]? UV0s { get => _Uv0s; set => _Uv0s = value; }

        /// <summary></summary>
        public Vector2[]? UV1s { get => _Uv1s; set => _Uv1s = value; }

        /// <summary></summary>
        public Vector2[]? UV2s { get => _Uv2s; set => _Uv2s = value; }

        /// <summary></summary>
        public Vector2[]? UV3s { get => _Uv3s; set => _Uv3s = value; }

        /// <summary></summary>
        public Color[]? Colors { get => _Colors; set => _Colors = value; }

        /// <summary></summary>
        public Color32[]? Color32s { get => _Color32s; set => _Color32s = value; }

        /// <summary></summary>
        public BoneWeight[]? BoneWeights { get => _BoneWeights; set => _BoneWeights = value; }

        /// <summary></summary>
        public List<int[]>? SubMeshes { get => _SubMeshes; set => _SubMeshes = value; }

        /// <summary></summary>
        public List<int>? MaterialIndices { get => _MaterialIndices; set => _MaterialIndices = value; }

        /// <summary></summary>
        public BlendShapesContext? BlendShapesContext { get => _BlendShapesContext; set => _BlendShapesContext = value; }

        /// <summary>
        /// Create a new instance of MeshContext with name.
        /// </summary>
        /// <param name="name"></param>
        public MeshContext(string name)
        {
            _Name = name ?? throw new ArgumentNullException(nameof(name));
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
