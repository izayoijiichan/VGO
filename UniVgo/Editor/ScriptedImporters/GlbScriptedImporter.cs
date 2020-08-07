// ----------------------------------------------------------------------
// @Namespace : UniVgo.Editor
// @Class     : GlbScriptedImporter
// ----------------------------------------------------------------------
#if !DISABLE_UNIVGO_GLB_IMPORTER
namespace UniVgo.Editor
{
    using System.IO;
    using UnityEditor.Experimental.AssetImporters;

    /// <summary>
    /// GLB Scripted Importer
    /// </summary>
    [ScriptedImporter(1, "glb")]
    public class GlbScriptedImporter : VgoScriptedImporter
    {
        /// <summary>
        /// Load 3D model.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>A model asset.</returns>
        protected override ModelAsset LoadModel(string filePath)
        {
            byte[] allBytes = File.ReadAllBytes(filePath);

            var importer = new GlbImporter();

            ModelAsset modelAsset = importer.Load(allBytes);

            return modelAsset;
        }

        /// <summary>
        /// Extract 3D model.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>A model asset.</returns>
        protected override ModelAsset ExtractModel(string filePath)
        {
            var importer = new GlbImporter();

            ModelAsset modelAsset = importer.Extract(filePath);

            return modelAsset;
        }
    }
}
#endif