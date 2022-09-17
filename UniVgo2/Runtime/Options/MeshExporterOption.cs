// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : MeshExporterOption
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    /// <summary>
    /// Mesh Exporter Option
    /// </summary>
    public class MeshExporterOption
    {
        /// <summary>Whether export tangents.</summary>
        public bool ExportTangents { get; set; } = false;

        /// <summary>Whether remove vertex color.</summary>
        public bool RemoveVertexColor { get; set; } = false;

        /// <summary>Whether enable accessor sparse for sub mesh.</summary>
        public bool EnableSparseForSubMesh { get; set; } = true;

        /// <summary>The threshold to apply sparse for sub mesh.</summary>
        public float SparseThresholdForSubMesh { get; set; } = 0.8f;

        /// <summary>Whether enable accessor sparse for morph target.</summary>
        public bool EnableSparseForMorphTarget { get; set; } = true;

        /// <summary>The threshold to apply sparse for morph target.</summary>
        public float SparseThresholdForMorphTarget { get; set; } = 0.8f;
    }
}
