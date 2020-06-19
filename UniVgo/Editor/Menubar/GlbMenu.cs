// ----------------------------------------------------------------------
// @Namespace : UniVgo.Editor
// @Class     : GlbMenu
// ----------------------------------------------------------------------
namespace UniVgo.Editor
{
    using System.Threading.Tasks;
    using UniGLTFforUniVgo;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// GLB Menu
    /// </summary>
    public static class GlbMenu
    {
        #region MenuItem

        /// <summary>
        /// Tools > UniGLTF > Export
        /// </summary>
        [MenuItem("Tools/UniGLTF/Export", priority = 1001)]
        public static void ExportMenu()
        {
            GlbExportProcessor.ExportGlb();
        }

        /// <summary>
        /// Tools > UniGLTF > Import
        /// </summary>
        /// <returns></returns>
        [MenuItem("Tools/UniGLTF/Import", priority = 1002)]
        public static void ImportMenu()
        {
            GlbImportProcessor.ImportGlb();
        }

        /// <summary>
        /// Tools > UniGLTF > Version
        /// </summary>
        [MenuItem("Tools/UniGLTF/Version", priority = 1013)]
        public static void VersionMenu()
        {
            Debug.Log("UniGLTF version: " + UniGLTFVersion.VERSION);
        }

        #endregion
    }
}