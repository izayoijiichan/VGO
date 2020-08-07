// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : MeshContext
// ----------------------------------------------------------------------
namespace UniVgo
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
        public List<Vector3> positions = new List<Vector3>();

        /// <summary></summary>
        public List<Vector3> normals = new List<Vector3>();

        /// <summary></summary>
        public List<Vector4> tangents = new List<Vector4>();

        /// <summary></summary>
        public List<Vector2> uvs = new List<Vector2>();

        /// <summary></summary>
        public List<Color> colors = new List<Color>();

        /// <summary></summary>
        public List<BoneWeight> boneWeights = new List<BoneWeight>();

        /// <summary></summary>
        public List<List<int>> subMeshes = new List<List<int>>();

        /// <summary></summary>
        public List<int> materialIndices = new List<int>();

        /// <summary></summary>
        public List<BlendShape> blendShapes = new List<BlendShape>();

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
