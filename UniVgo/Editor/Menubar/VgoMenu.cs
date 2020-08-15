// ----------------------------------------------------------------------
// @Namespace : UniVgo.Editor
// @Class     : VgoMenu
// ----------------------------------------------------------------------
namespace UniVgo.Editor
{
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// VGO Menu
    /// </summary>
    public static class VgoMenu
    {
        #region MenuItem

        /// <summary>
        /// Tools > UniGLTF > Export (GLB)
        /// </summary>
        [MenuItem("Tools/UniVGO/Export (GLB)", priority = 1001)]
        public static void ExportGlbMenu()
        {
            GlbExportProcessor.ExportGlb();
        }

        /// <summary>
        /// Tools > UniVGO > Export (VGO)
        /// </summary>
        [MenuItem("Tools/UniVGO/Export (VGO)", priority = 1002)]
        public static void ExportVgoMenu()
        {
            VgoExportProcessor.ExportVgo();
        }

        /// <summary>
        /// Tools > UniVGO > Version
        /// </summary>
        [MenuItem("Tools/UniVGO/Version", priority = 1031)]
        public static void VersionMenu()
        {
            Debug.Log("UniVGO version: " + VgoVersion.VERSION);
        }

        #endregion
    }
}