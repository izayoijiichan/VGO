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
        public string name;

        /// <summary></summary>
        public Vector3[]? positions;

        /// <summary></summary>
        public Vector3[]? normals;

        /// <summary></summary>
        public Vector4[]? tangents;

        /// <summary></summary>
        public Vector2[]? uv0s;

        /// <summary></summary>
        public Vector2[]? uv1s;

        /// <summary></summary>
        public Vector2[]? uv2s;

        /// <summary></summary>
        public Vector2[]? uv3s;

        /// <summary></summary>
        public Color[]? colors;

        /// <summary></summary>
        public Color32[]? color32s;

        /// <summary></summary>
        public BoneWeight[]? boneWeights;

        /// <summary></summary>
        public List<int[]>? subMeshes;

        /// <summary></summary>
        public List<int>? materialIndices;

        /// <summary></summary>
        public List<BlendShapeContext>? blendShapes;

        /// <summary>
        /// Create a new instance of MeshContext with name.
        /// </summary>
        /// <param name="name"></param>
        public MeshContext(string name)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
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
