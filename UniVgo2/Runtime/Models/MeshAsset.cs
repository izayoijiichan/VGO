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
        /// <summary>A mesh.</summary>
        public Mesh Mesh;

        /// <summary>An array of materials.</summary>
        /// <remarks>
        /// for Import.
        /// </remarks>
        public Material[] Materials;

        /// <summary>A renderer.</summary>
        /// <remarks>
        /// for Export.
        /// </remarks>
        public Renderer Renderer;

        /// <summary>A blend shape configuration.</summary>
        public BlendShapeConfiguration BlendShapeConfiguration;
    }
}
