// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : ExportMeshRendererAsset
// ----------------------------------------------------------------------
#nullable enable
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

        /// <summary>
        /// Create a new instance of ExportMeshRendererAsset with renderer and mesh.
        /// </summary>
        /// <param name="renderer"></param>
        /// <param name="mesh"></param>
        public ExportMeshRendererAsset(Renderer renderer, Mesh mesh)
        {
            Renderer = renderer;

            Mesh = mesh;
        }
    }
}