// ----------------------------------------------------------------------
// @Namespace : UniVgo.Editor
// @Class     : GlbImportProcessor
// ----------------------------------------------------------------------
namespace UniVgo.Editor
{
    using System.IO;
    using UniGLTFforUniVgo;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// GLB Import Processor
    /// </summary>
    public static class GlbImportProcessor
    {
        /// <summary>
        /// Import GLB.
        /// </summary>
        /// <returns></returns>
        public static void ImportGlb()
        {
            string path = EditorUtility.OpenFilePanel(title: "Open File Dialog", directory: "", extension: "glb");

            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            if (Application.isPlaying)
            {
                //
                // load into scene
                //
                var context = new ImporterContext();

                context.Load(path);

                context.ShowMeshes();

                Selection.activeGameObject = context.Root;
            }
            else
            {
                //
                // save as asset
                //
                if (path.StartsWithUnityAssetPath())
                {
                    Debug.LogWarningFormat("disallow import from folder under the Assets");
                    return;
                }

                string assetPath = EditorUtility.SaveFilePanel(title: "Save prefab", directory: "Assets", defaultName: Path.GetFileNameWithoutExtension(path), extension: "prefab");

                if (string.IsNullOrEmpty(path))
                {
                    return;
                }

                // import as asset
                GlbAssetPostprocessor.ImportAsset(src: path, prefabPath: UnityPath.FromFullpath(assetPath));
            }
        }
    }
}