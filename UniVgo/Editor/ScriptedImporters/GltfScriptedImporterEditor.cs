// ----------------------------------------------------------------------
// @Namespace : UniVgo.Editor
// @Class     : GltfScriptedImporterEditor
// ----------------------------------------------------------------------
#if !DISABLE_UNIVGO_GLTF_IMPORTER
namespace UniVgo.Editor
{
    using UnityEditor;

    /// <summary>
    /// glTF Scripted Importer Editor
    /// </summary>
    [CustomEditor(typeof(GltfScriptedImporter))]
    public class GltfScriptedImporterEditor : VgoScriptedImporterEditor
    {
    }
}
#endif