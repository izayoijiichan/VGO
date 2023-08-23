// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Editor
// @Class     : VgoMenu
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Editor
{
    using System.Text;
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
            StringBuilder sb = new StringBuilder(128);

            sb.AppendLine($"UniVGO version: {VgoVersion.VERSION}");

#if UNITY_2023_1_OR_NEWER
            sb.Append("UNITY_2023_1_OR_NEWER");
#elif UNITY_2022_3_OR_NEWER
            sb.Append("UNITY_2022_3_OR_NEWER");
#elif UNITY_2022_2_OR_NEWER
            sb.Append("UNITY_2022_2_OR_NEWER");
#elif UNITY_2022_1_OR_NEWER
            sb.Append("UNITY_2022_1_OR_NEWER");
#elif UNITY_2021_3_OR_NEWER
            sb.Append("UNITY_2021_3_OR_NEWER");
#elif UNITY_2021_2_OR_NEWER
            sb.Append("UNITY_2021_2_OR_NEWER");
#elif UNITY_2021_1_OR_NEWER
            sb.Append("UNITY_2021_1_OR_NEWER");
#elif UNITY_2020_3_OR_NEWER
            sb.Append("UNITY_2020_3_OR_NEWER");
#endif

#if UNIVGO_USE_UNITY_AWAITABLE
            sb.Append(", ").Append("UNIVGO_USE_UNITY_AWAITABLE");
#endif

#if UNITY_NUGET_NEWTONSOFT_JSON_1_0
            sb.Append(", ").Append("UNITY_NUGET_NEWTONSOFT_JSON_1_0");
#elif UNITY_NUGET_NEWTONSOFT_JSON_2_0_OR_NEWER
            sb.Append(", ").Append("UNITY_NUGET_NEWTONSOFT_JSON_2_0_OR_NEWER");
#elif UNITY_NUGET_NEWTONSOFT_JSON_3_0_OR_NEWER
            sb.Append(", ").Append("UNITY_NUGET_NEWTONSOFT_JSON_3_0_OR_NEWER");
#elif UNITY_NUGET_NEWTONSOFT_JSON_3_1_0
            sb.Append(", ").Append("UNITY_NUGET_NEWTONSOFT_JSON_3_1_0");
#elif UNITY_NUGET_NEWTONSOFT_JSON_3_2_OR_NEWER
            sb.Append(", ").Append("UNITY_NUGET_NEWTONSOFT_JSON_3_2_OR_NEWER");
#endif

#if IZAYOI_LILTOON_SHADER_UTILITY_1_0_OR_NEWER
            sb.Append(", ").Append("IZAYOI_LILTOON_SHADER_UTILITY_1_0_OR_NEWER");
#elif IZAYOI_LILTOON_SHADER_UTILITY_1_4
            sb.Append(", ").Append("IZAYOI_LILTOON_SHADER_UTILITY_1_4");
#endif

#if IZAYOI_UNISHADERS_1_0_OR_NEWER
            sb.Append(", ").Append("IZAYOI_UNISHADERS_1_0_OR_NEWER");
#elif IZAYOI_UNISHADERS_1_1
            sb.Append(", ").Append("IZAYOI_UNISHADERS_1_1");
#elif IZAYOI_UNISHADERS_1_2
            sb.Append(", ").Append("IZAYOI_UNISHADERS_1_2");
#elif IZAYOI_UNISHADERS_1_3
            sb.Append(", ").Append("IZAYOI_UNISHADERS_1_3");
#elif IZAYOI_UNISHADERS_1_4
            sb.Append(", ").Append("IZAYOI_UNISHADERS_1_4");
#elif IZAYOI_UNISHADERS_1_5_OR_NEWER
            sb.Append(", ").Append("IZAYOI_UNISHADERS_1_5_OR_NEWER");
#elif IZAYOI_UNISHADERS_1_6_OR_NEWER
            sb.Append(", ").Append("IZAYOI_UNISHADERS_1_6_OR_NEWER");
#endif

#if IZAYOI_VGOSPRINGBONE_1_0
            sb.Append(", ").Append("IZAYOI_VGOSPRINGBONE_1_0");
#elif IZAYOI_VGOSPRINGBONE_1_1_OR_NEWER
            sb.Append(", ").Append("IZAYOI_VGOSPRINGBONE_1_1_OR_NEWER");
#endif

#if VRMC_VRMSHADERS_0_79_OR_NEWER
            sb.Append(", ").Append("VRMC_VRMSHADERS_0_79_OR_NEWER");
#elif VRMC_VRMSHADERS_0_85_OR_NEWER
            sb.Append(", ").Append("VRMC_VRMSHADERS_0_85_OR_NEWER");
#elif VRMC_VRMSHADERS_0_104_OR_NEWER
            sb.Append(", ").Append("VRMC_VRMSHADERS_0_104_OR_NEWER");
#endif

#if LILTOON_1_2_12_OR_OLDER
            sb.Append(", ").Append("LILTOON_1_2_12_OR_OLDER");
#elif LILTOON_1_3_0_OR_NEWER
            sb.Append(", ").Append("LILTOON_1_3_0_OR_NEWER");
#elif LILTOON_1_4_0_OR_NEWER
            sb.Append(", ").Append("LILTOON_1_4_0_OR_NEWER");
#endif

#if CYSHARP_UNITASK_2_OR_NEWER
            sb.Append(", ").Append("CYSHARP_UNITASK_2_OR_NEWER");
#endif

#if UNIVGO_USE_UNITASK
            sb.Append(", ").Append("UNIVGO_USE_UNITASK");
#endif

#if SIXLABORS_IMAGESHARP_1_OR_NEWER
            sb.Append(", ").Append("SIXLABORS_IMAGESHARP_1_OR_NEWER");
#elif SIXLABORS_IMAGESHARP_2_OR_NEWER
            sb.Append(", ").Append("SIXLABORS_IMAGESHARP_2_OR_NEWER");
#elif SIXLABORS_IMAGESHARP_3_OR_NEWER
            sb.Append(", ").Append("SIXLABORS_IMAGESHARP_3_OR_NEWER");
#endif

#if UNIVGO_EXPORT_WEBP_TEXTURE_ENABLE
            sb.Append(", ").Append("UNIVGO_EXPORT_WEBP_TEXTURE_ENABLE");
#endif

#if VGO_FILE_EXTENSION_2
            sb.Append(", ").Append("VGO_FILE_EXTENSION_2");
#endif

#if VGO_2_DISABLE_SCRIPTED_IMPORTER
            sb.Append(", ").Append("VGO_2_DISABLE_SCRIPTED_IMPORTER");
#endif

#if UNIVGO_SCRIPTED_IMPORTER_FORCE_PNG
            sb.Append(", ").Append("UNIVGO_SCRIPTED_IMPORTER_FORCE_PNG");
#endif

#if UNITY_2021_2_OR_NEWER

#if ORG_NUGET_SYSTEM_BUFFERS_4_0_0
            sb.Append(", ").Append("ORG_NUGET_SYSTEM_BUFFERS_4_0_0");
#elif ORG_NUGET_SYSTEM_BUFFERS_4_3_0
            sb.Append(", ").Append("ORG_NUGET_SYSTEM_BUFFERS_4_3_0");
#elif ORG_NUGET_SYSTEM_BUFFERS_4_4_0
            sb.Append(", ").Append("ORG_NUGET_SYSTEM_BUFFERS_4_4_0");
#elif ORG_NUGET_SYSTEM_BUFFERS_4_5_0
            sb.Append(", ").Append("ORG_NUGET_SYSTEM_BUFFERS_4_5_0");
#elif ORG_NUGET_SYSTEM_BUFFERS_4_5_1
            sb.Append(", ").Append("ORG_NUGET_SYSTEM_BUFFERS_4_5_1");
#endif

#if ORG_NUGET_SYSTEM_MEMORY_4_5_0
            sb.Append(", ").Append("ORG_NUGET_SYSTEM_MEMORY_4_5_0");
#elif ORG_NUGET_SYSTEM_MEMORY_4_5_1
            sb.Append(", ").Append("ORG_NUGET_SYSTEM_MEMORY_4_5_1");
#elif ORG_NUGET_SYSTEM_MEMORY_4_5_2
            sb.Append(", ").Append("ORG_NUGET_SYSTEM_MEMORY_4_5_2");
#elif ORG_NUGET_SYSTEM_MEMORY_4_5_3
            sb.Append(", ").Append("ORG_NUGET_SYSTEM_MEMORY_4_5_3");
#elif ORG_NUGET_SYSTEM_MEMORY_4_5_4
            sb.Append(", ").Append("ORG_NUGET_SYSTEM_MEMORY_4_5_4");
#elif ORG_NUGET_SYSTEM_MEMORY_4_5_5
            sb.Append(", ").Append("ORG_NUGET_SYSTEM_MEMORY_4_5_5");
#endif

#if ORG_NUGET_SYSTEM_NUMERICS_VECTORS_4_3_0
            sb.Append(", ").Append("ORG_NUGET_SYSTEM_NUMERICS_VECTORS_4_3_0");
#elif ORG_NUGET_SYSTEM_NUMERICS_VECTORS_4_4_0
            sb.Append(", ").Append("ORG_NUGET_SYSTEM_NUMERICS_VECTORS_4_4_0");
#elif ORG_NUGET_SYSTEM_NUMERICS_VECTORS_4_5_0
            sb.Append(", ").Append("ORG_NUGET_SYSTEM_NUMERICS_VECTORS_4_5_0");
#endif

#if ORG_NUGET_SYSTEM_RUNTIME_COMPILERSERVICES_UNSAFE_4_3_0
            sb.Append(", ").Append("ORG_NUGET_SYSTEM_RUNTIME_COMPILERSERVICES_UNSAFE_4_3_0");
#elif ORG_NUGET_SYSTEM_RUNTIME_COMPILERSERVICES_UNSAFE_4_4_0
            sb.Append(", ").Append("ORG_NUGET_SYSTEM_RUNTIME_COMPILERSERVICES_UNSAFE_4_4_0");
#elif ORG_NUGET_SYSTEM_RUNTIME_COMPILERSERVICES_UNSAFE_4_5_0_OR_NEWER
            sb.Append(", ").Append("ORG_NUGET_SYSTEM_RUNTIME_COMPILERSERVICES_UNSAFE_4_5_0_OR_NEWER");
#elif ORG_NUGET_SYSTEM_RUNTIME_COMPILERSERVICES_UNSAFE_4_6_0
            sb.Append(", ").Append("ORG_NUGET_SYSTEM_RUNTIME_COMPILERSERVICES_UNSAFE_4_6_0");
#elif ORG_NUGET_SYSTEM_RUNTIME_COMPILERSERVICES_UNSAFE_4_7_0_OR_NEWER
            sb.Append(", ").Append("ORG_NUGET_SYSTEM_RUNTIME_COMPILERSERVICES_UNSAFE_4_7_0_OR_NEWER");
#elif ORG_NUGET_SYSTEM_RUNTIME_COMPILERSERVICES_UNSAFE_5_0_0
            sb.Append(", ").Append("ORG_NUGET_SYSTEM_RUNTIME_COMPILERSERVICES_UNSAFE_5_0_0");
#elif ORG_NUGET_SYSTEM_RUNTIME_COMPILERSERVICES_UNSAFE_6_0_0
            sb.Append(", ").Append("ORG_NUGET_SYSTEM_RUNTIME_COMPILERSERVICES_UNSAFE_6_0_0");
#endif
            Debug.Log(sb.ToString());

#else  // UNITY_2020_3_OR_LOWER

            Debug.Log(sb.ToString());

#if ORG_NUGET_SYSTEM_BUFFERS_4_0_0
            Debug.LogWarning("ORG_NUGET_SYSTEM_BUFFERS_4_0_0");
#elif ORG_NUGET_SYSTEM_BUFFERS_4_3_0
            Debug.LogWarning("ORG_NUGET_SYSTEM_BUFFERS_4_3_0");
#elif ORG_NUGET_SYSTEM_BUFFERS_4_4_0
            Debug.Log("ORG_NUGET_SYSTEM_BUFFERS_4_4_0");
#elif ORG_NUGET_SYSTEM_BUFFERS_4_5_0
            Debug.Log("ORG_NUGET_SYSTEM_BUFFERS_4_5_0");
#elif ORG_NUGET_SYSTEM_BUFFERS_4_5_1
            Debug.Log("ORG_NUGET_SYSTEM_BUFFERS_4_5_1");
#endif

#if ORG_NUGET_SYSTEM_MEMORY_4_5_0
            Debug.Log("ORG_NUGET_SYSTEM_MEMORY_4_5_0");
#elif ORG_NUGET_SYSTEM_MEMORY_4_5_1
            Debug.Log("ORG_NUGET_SYSTEM_MEMORY_4_5_1");
#elif ORG_NUGET_SYSTEM_MEMORY_4_5_2
            Debug.Log("ORG_NUGET_SYSTEM_MEMORY_4_5_2");
#elif ORG_NUGET_SYSTEM_MEMORY_4_5_3
            Debug.Log("ORG_NUGET_SYSTEM_MEMORY_4_5_3");
#elif ORG_NUGET_SYSTEM_MEMORY_4_5_4
            Debug.LogWarning("ORG_NUGET_SYSTEM_MEMORY_4_5_4");
#elif ORG_NUGET_SYSTEM_MEMORY_4_5_5
            Debug.LogWarning("ORG_NUGET_SYSTEM_MEMORY_4_5_5");
#endif

#if ORG_NUGET_SYSTEM_NUMERICS_VECTORS_4_3_0
            Debug.LogWarning("ORG_NUGET_SYSTEM_NUMERICS_VECTORS_4_3_0");
#elif ORG_NUGET_SYSTEM_NUMERICS_VECTORS_4_4_0
            Debug.Log("ORG_NUGET_SYSTEM_NUMERICS_VECTORS_4_4_0");
#elif ORG_NUGET_SYSTEM_NUMERICS_VECTORS_4_5_0
            Debug.LogWarning("ORG_NUGET_SYSTEM_NUMERICS_VECTORS_4_5_0");
#endif

#if ORG_NUGET_SYSTEM_RUNTIME_COMPILERSERVICES_UNSAFE_4_3_0
            Debug.LogWarning("ORG_NUGET_SYSTEM_RUNTIME_COMPILERSERVICES_UNSAFE_4_3_0");
#elif ORG_NUGET_SYSTEM_RUNTIME_COMPILERSERVICES_UNSAFE_4_4_0
            Debug.Log("ORG_NUGET_SYSTEM_RUNTIME_COMPILERSERVICES_UNSAFE_4_4_0");
#elif ORG_NUGET_SYSTEM_RUNTIME_COMPILERSERVICES_UNSAFE_4_5_0_OR_NEWER
            Debug.LogWarning("ORG_NUGET_SYSTEM_RUNTIME_COMPILERSERVICES_UNSAFE_4_5_0_OR_NEWER");
#elif ORG_NUGET_SYSTEM_RUNTIME_COMPILERSERVICES_UNSAFE_4_6_0
            Debug.LogWarning("ORG_NUGET_SYSTEM_RUNTIME_COMPILERSERVICES_UNSAFE_4_6_0");
#elif ORG_NUGET_SYSTEM_RUNTIME_COMPILERSERVICES_UNSAFE_4_7_0_OR_NEWER
            Debug.LogWarning("ORG_NUGET_SYSTEM_RUNTIME_COMPILERSERVICES_UNSAFE_4_7_0_OR_NEWER");
#elif ORG_NUGET_SYSTEM_RUNTIME_COMPILERSERVICES_UNSAFE_5_0_0
            Debug.LogWarning("ORG_NUGET_SYSTEM_RUNTIME_COMPILERSERVICES_UNSAFE_5_0_0");
#elif ORG_NUGET_SYSTEM_RUNTIME_COMPILERSERVICES_UNSAFE_6_0_0
            Debug.LogWarning("ORG_NUGET_SYSTEM_RUNTIME_COMPILERSERVICES_UNSAFE_6_0_0");
#endif

#endif
        }

        #endregion
    }
}