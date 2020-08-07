// ----------------------------------------------------------------------
// @Namespace : UniVgo.Editor
// @Class     : GltfScriptedImporter
// ----------------------------------------------------------------------
#if !DISABLE_UNIVGO_GLTF_IMPORTER
namespace UniVgo.Editor
{
    using UnityEditor.Experimental.AssetImporters;

    /// <summary>
    /// glTF Scripted Importer
    /// </summary>
    [ScriptedImporter(1, "gltf")]
    public class GltfScriptedImporter : GlbScriptedImporter
    {
    }
}
#endif