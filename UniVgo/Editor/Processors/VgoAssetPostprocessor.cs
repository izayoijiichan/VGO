// ----------------------------------------------------------------------
// @Namespace : UniVgo.Editor
// @Class     : VgoAssetPostprocessor
// ----------------------------------------------------------------------
#if !VGO_STOP_ASSET_POSTPROCESSOR
namespace UniVgo.Editor
{
    using System;
    using System.IO;
    using UniGLTFforUniVgo;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// VGO Asset Postprocessor
    /// </summary>
    public class VgoAssetPostprocessor : AssetPostprocessor
    {
        /// <summary>
        /// Called after any number of assets have been imported.
        /// </summary>
        /// <param name="importedAssets"></param>
        /// <param name="deletedAssets"></param>
        /// <param name="movedAssets"></param>
        /// <param name="movedFromAssetPaths"></param>
        private static void OnPostprocessAllAssets(
            string[] importedAssets,
            string[] deletedAssets,
            string[] movedAssets,
            string[] movedFromAssetPaths)
        {
            foreach (string path in importedAssets)
            {
                if (UnityPath.FromUnityPath(path).IsStreamingAsset)
                {
                    Debug.LogFormat("Skip StreamingAssets: {0}", path);
                    continue;
                }

                var ext = Path.GetExtension(path).ToLower();

                if (ext == ".vgo")
                {
                    var gltfPath = UnityPath.FromUnityPath(path);
                    var prefabPath = gltfPath.Parent.Child(gltfPath.FileNameWithoutExtension + ".prefab");
                    ImportAsset(UnityPath.FromUnityPath(path).FullPath, ext, prefabPath);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="ext"></param>
        /// <param name="prefabPath"></param>
        public static void ImportAsset(string src, string ext, UnityPath prefabPath)
        {
            if (!prefabPath.IsUnderAssetsFolder)
            {
                Debug.LogWarningFormat("out of asset path: {0}", prefabPath);
                return;
            }

            var context = new VgoImporter();

            context.Parse(src);

            // Extract textures to assets folder
            context.ExtractImages(prefabPath);

            ImportDelayed(src, prefabPath, context);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="prefabPath"></param>
        /// <param name="context"></param>
        static void ImportDelayed(string src, UnityPath prefabPath, ImporterContext context)
        {
            EditorApplication.delayCall += () =>
                {
                    //
                    // After textures imported(To ensure TextureImporter be accessible).
                    //
                    try
                    {
                        context.Load();
                        context.SaveAsAsset(prefabPath);
                        context.EditorDestroyRoot();
                    }
                    catch (UniGLTFNotSupportedException ex)
                    {
                        Debug.LogWarningFormat("{0}: {1}",
                            src,
                            ex.Message
                            );
                        context.EditorDestroyRootAndAssets();
                    }
                    catch (Exception ex)
                    {
                        Debug.LogErrorFormat("import error: {0}", src);
                        Debug.LogErrorFormat("{0}", ex);
                        context.EditorDestroyRootAndAssets();
                    }
                };
        }
    }
}
#endif
