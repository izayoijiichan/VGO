// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Editor
// @Class     : VgoMenu
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Editor
{
    using NewtonVgo;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// VGO Menu
    /// </summary>
    public static class VgoMenu
    {
        #region MenuItem

        ///// <summary>
        ///// Tools > UniVGO > Export (Left)
        ///// </summary>
        //[MenuItem("Tools/UniVGO/Export (Left)", priority = 1013)]
        //public static void ExportVgoMenuLeft()
        //{
        //    VgoExportProcessor.ExportVgo(VgoGeometryCoordinate.LeftHanded, VgoUVCoordinate.BottomLeft);
        //}

        ///// <summary>
        ///// Tools > UniVGO > Export (Right)
        ///// </summary>
        //[MenuItem("Tools/UniVGO/Export (Right)", priority = 1014)]
        //public static void ExportVgoMenuRight()
        //{
        //    VgoExportProcessor.ExportVgo(VgoGeometryCoordinate.RightHanded, VgoUVCoordinate.TopLeft);
        //}

        /// <summary>
        /// Tools > UniVGO > Version 2
        /// </summary>
        [MenuItem("Tools/UniVGO/Version 2", priority = 1032)]
        public static void VersionMenu()
        {
            Debug.Log($"UniVGO version: {VgoVersion.VERSION}");
        }

        #endregion
    }
}