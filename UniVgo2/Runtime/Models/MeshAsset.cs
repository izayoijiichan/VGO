// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : MeshAsset
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using UnityEngine;

    /// <summary>
    /// Mesh Asset
    /// </summary>
    public class MeshAsset
    {
        /// <summary>A mesh.</summary>
        /// <remarks>This is unique, if spec version is 2.5 or higher.</remarks>
        public Mesh Mesh;

        /// <summary>An array of materials.</summary>
        /// <remarks>
        /// for Import.
        /// This field is used only in spec version between 2.0 and 2.4.
        /// </remarks>
        public Material?[]? Materials;

        /// <summary>A renderer.</summary>
        /// <remarks>
        /// for Export.
        /// This field is used only in spec version between 2.0 and 2.4.
        /// </remarks>
        public Renderer? Renderer;

        /// <summary>A blend shape configuration.</summary>
        public BlendShapeConfig? BlendShapeConfig;

        /// <summary>
        /// Create a new instance of MeshAsset with mesh.
        /// </summary>
        /// <param name="mesh"></param>
        public MeshAsset(Mesh mesh)
        {
            Mesh = mesh;
        }
    }
}
