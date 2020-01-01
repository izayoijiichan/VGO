// ----------------------------------------------------------------------
// @Namespace : UniVgo.Editor
// @Class     : VgoMenu
// ----------------------------------------------------------------------
namespace UniVgo.Editor
{
    using System.Threading.Tasks;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// VGO Menu
    /// </summary>
    public static class VgoMenu
    {
        #region MenuItem

        /// <summary>
        /// Tools > UniVGO > Export
        /// </summary>
        [MenuItem("Tools/UniVGO/Export", priority = 1001)]
        public static void ExportMenu()
        {
            VgoExportProcessor.ExportVgo();
        }

        /// <summary>
        /// Tools > UniVGO > Import
        /// </summary>
        /// <returns></returns>
        [MenuItem("Tools/UniVGO/Import", priority = 1002)]
        public static async Task ImportMenu()
        {
            await VgoImportProcessor.ImportVgo();
        }

        /// <summary>
        /// Tools > UniVGO > Version
        /// </summary>
        [MenuItem("Tools/UniVGO/Version", priority = 1013)]
        public static void VersionMenu()
        {
            Debug.Log("UniVGO version: " + VgoVersion.VERSION);
        }

        #endregion
    }
}