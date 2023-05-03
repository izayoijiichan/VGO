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
        private readonly Mesh _Mesh;

        /// <summary>An array of materials.</summary>
        private Material?[]? _Materials;

        /// <summary>A renderer.</summary>
        private Renderer? _Renderer;

        /// <summary>A blend shape configuration.</summary>
        private BlendShapeConfig? _BlendShapeConfig;

        /// <summary>A mesh.</summary>
        /// <remarks>This is unique, if spec version is 2.5 or higher.</remarks>
        public Mesh Mesh => _Mesh;

        /// <summary>An array of materials.</summary>
        /// <remarks>
        /// for Import.
        /// This field is used only in spec version between 2.0 and 2.4.
        /// </remarks>
        public Material?[]? Materials { get => _Materials; set => _Materials = value; }

        /// <summary>A renderer.</summary>
        /// <remarks>
        /// for Export.
        /// This field is used only in spec version between 2.0 and 2.4.
        /// </remarks>
        public Renderer? Renderer { get => _Renderer; set => _Renderer = value; }

        /// <summary>A blend shape configuration.</summary>
        public BlendShapeConfig? BlendShapeConfig { get => _BlendShapeConfig; set => _BlendShapeConfig = value; }

        /// <summary>
        /// Create a new instance of MeshAsset with mesh.
        /// </summary>
        /// <param name="mesh"></param>
        public MeshAsset(Mesh mesh)
        {
            _Mesh = mesh;
        }
    }
}
