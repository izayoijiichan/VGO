// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : MeshAsset
// ----------------------------------------------------------------------
namespace UniVgo2
{
    using UnityEngine;

    /// <summary>
    /// Mesh Asset
    /// </summary>
    public class MeshAsset
    {
        /// <summary></summary>
        public Mesh Mesh;

        /// <summary></summary>
        public Material[] Materials;

        /// <summary></summary>
        /// <remarks>MeshRenderer or SkinnedMeshRenderer</remarks>
        public Renderer Renderer;

        /// <summary></summary>
        public BlendShapeConfiguration BlendShapeConfiguration;
    }
}
