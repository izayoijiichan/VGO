// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : VgoImporterOption
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    /// <summary>
    /// VGO Importer Option
    /// </summary>
    public class VgoImporterOption
    {
        /// <summary>Mesh importer option.</summary>
        public MeshImporterOption MeshImporterOption { get; set; } = new MeshImporterOption();

        /// <summary>Whether show mesh renderer.</summary>
        public bool ShowMesh { get; set; } = true;

        /// <summary>Whether update skinned mesh renderer when off screen.</summary>
        public bool UpdateWhenOffscreen { get; set; } = false;
    }
}
