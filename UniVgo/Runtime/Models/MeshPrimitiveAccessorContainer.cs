// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : MeshPrimitiveAccessorContainer
// ----------------------------------------------------------------------
namespace UniVgo
{
    using System.Collections.Generic;
    using UnityEngine;
    using VgoGltf;

    /// <summary>
    /// Mesh Primitive Accessor Container
    /// </summary>
    /// <remarks>MeshImporter.cs</remarks>
    public class MeshPrimitiveAccessorContainer
    {
        /// <summary></summary>
        /// <remarks>key: accessorIndex</remarks>
        public Dictionary<int, Vector3[]> positions = new Dictionary<int, Vector3[]>();

        /// <summary></summary>
        /// <remarks>key: accessorIndex</remarks>
        public Dictionary<int, Vector3[]> normals = new Dictionary<int, Vector3[]>();

        /// <summary></summary>
        /// <remarks>key: accessorIndex</remarks>
        public Dictionary<int, Vector4[]> tangents = new Dictionary<int, Vector4[]>();

        /// <summary></summary>
        /// <remarks>key: accessorIndex</remarks>
        public Dictionary<int, Vector2[]> uvs = new Dictionary<int, Vector2[]>();

        /// <summary></summary>
        /// <remarks>key: accessorIndex</remarks>
        public Dictionary<int, Color[]> colors = new Dictionary<int, Color[]>();

        /// <summary></summary>
        /// <remarks>key: accessorIndex</remarks>
        public Dictionary<int, Vector4Ushort[]> joints0 = new Dictionary<int, Vector4Ushort[]>();

        /// <summary></summary>
        /// <remarks>key: accessorIndex</remarks>
        public Dictionary<int, Vector4[]> weights0 = new Dictionary<int, Vector4[]>();

        /// <summary></summary>
        /// <remarks>key: accessorIndex</remarks>
        public Dictionary<int, int[]> indices = new Dictionary<int, int[]>();

        /// <summary></summary>
        /// <remarks>key: accessorIndex</remarks>
        public Dictionary<int, Vector3[]> blendShapePositions = new Dictionary<int, Vector3[]>();

        /// <summary></summary>
        /// <remarks>key: accessorIndex</remarks>
        public Dictionary<int, Vector3[]> blendShapeNormals = new Dictionary<int, Vector3[]>();

        /// <summary></summary>
        /// <remarks>key: accessorIndex</remarks>
        public Dictionary<int, Vector3[]> blendShapeTangents = new Dictionary<int, Vector3[]>();
    }
}
