// ----------------------------------------------------------------------
// @Namespace : UniVgo.Editor
// @Class     : GlbScriptedImporterEditor
// ----------------------------------------------------------------------
#if !DISABLE_UNIVGO_GLB_IMPORTER
namespace UniVgo.Editor
{
    using UnityEditor;

    /// <summary>
    /// Glb Scripted Importer Editor
    /// </summary>
    [CustomEditor(typeof(GlbScriptedImporter))]
    public class GlbScriptedImporterEditor : VgoScriptedImporterEditor
    {
    }
}
#endif