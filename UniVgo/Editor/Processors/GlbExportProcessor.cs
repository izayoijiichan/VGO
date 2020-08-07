// ----------------------------------------------------------------------
// @Namespace : UniVgo.Editor
// @Class     : GlbExportProcessor
// ----------------------------------------------------------------------
namespace UniVgo.Editor
{
    using UnityEditor;
    using UnityEngine;
    using VgoGltf;

    /// <summary>
    /// GLB Export Processor
    /// </summary>
    public static class GlbExportProcessor
    {
        /// <summary>
        /// Export GLB.
        /// </summary>
        public static void ExportGlb()
        {
            EditorApplication.isPlaying = false;

            try
            {
                string errorMessage;

                if (!Validate(out errorMessage))
                {
                    Debug.LogAssertion(errorMessage);

                    EditorUtility.DisplayDialog("Error", errorMessage, "OK");

                    return;
                }

                GameObject root = Selection.activeObject as GameObject;

                string path = EditorUtility.SaveFilePanel(title: "Save File Dialog", directory: "", defaultName: root.name + ".glb", extension: "glb");

                if (string.IsNullOrEmpty(path))
                {
                    return;
                }

                GltfStorage gltfStorage;

                using (var exporter = new GlbExporter())
                {
                    gltfStorage = exporter.CreateGltfStorage(root);
                }

                gltfStorage.ExportGlbFile(path);

                Debug.LogFormat("Export GLB file.\nGameObject: {0}, output: {1}", root.name, path);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Perform verification.
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns>Returns true if validation is successful, false otherwise.</returns>
        private static bool Validate(out string errorMessage)
        {
            GameObject[] selectedGameObjects = Selection.gameObjects;

            if (selectedGameObjects.Length == 0)
            {
                errorMessage = "Select a GameObject.";
                return false;
            }

            if (2 <= selectedGameObjects.Length)
            {
                errorMessage = "Please select only one GameObject.";
                return false;
            }

            errorMessage = string.Empty;

            return true;
        }
    }
}