// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : ExportMeshRendererAsset
// ----------------------------------------------------------------------
namespace UniVgo2
{
    using UnityEngine;

    /// <summary>
    /// Export Mesh Renderer Asset
    /// </summary>
    public class ExportMeshRendererAsset
    {
        /// <summary>A renderer.</summary>
        /// <remarks>
        /// This is unique.
        /// MeshRenderer or SkinnedMeshRenderer or ParticleSystemRenderer
        /// </remarks>
        public Renderer Renderer;

        /// <summary>A mesh.</summary>
        public Mesh Mesh;
    }
}