// ----------------------------------------------------------------------
// @Namespace : UniVgo.Editor
// @Class     : GlbScriptedImporterEditor
// ----------------------------------------------------------------------
#if !DISABLE_UNIVGO_GLB_IMPORTER
namespace UniVgo.Editor
{
    using System.Linq;
    using UnityEditor;
    using UnityEditor.Experimental.AssetImporters;
    using UnityEngine;

    /// <summary>
    /// Glb Scripted Importer Editor
    /// </summary>
    [CustomEditor(typeof(GlbScriptedImporter))]
    public class GlbScriptedImporterEditor : ScriptedImporterEditor
    {
        /// <summary></summary>
        protected bool _isOpen = true;

        /// <summary>
        /// Override this method to create your own Inpsector GUI for a ScriptedImporter.
        /// </summary>
        public override void OnInspectorGUI()
        {
            var importer = target as GlbScriptedImporter;

            EditorGUILayout.LabelField("Extract settings");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Materials and Textures");
            GUI.enabled =
                (importer.GetExternalUnityObjects<Texture2D>().Any() == false) ||
                (importer.GetExternalUnityObjects<Material>().Any() == false);
            if (GUILayout.Button("Extract"))
            {
                importer.ExtractTexturesAndMaterials();
                GUIUtility.ExitGUI();
            }
            GUI.enabled = !GUI.enabled;
            if (GUILayout.Button("Clear"))
            {
                importer.ClearExtarnalObjects<Material>();
                importer.ClearExtarnalObjects<Texture2D>();
                GUIUtility.ExitGUI();
            }
            GUI.enabled = true;
            EditorGUILayout.EndHorizontal();

            DrawRemapGUI<Material>("Material Remap", importer);
            DrawRemapGUI<Texture2D>("Texture Remap", importer);

            EditorGUILayout.Space();

            base.OnInspectorGUI();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="title"></param>
        /// <param name="importer"></param>
        protected virtual void DrawRemapGUI<T>(string title, GlbScriptedImporter importer) where T : UnityEngine.Object
        {
            EditorGUILayout.Foldout(_isOpen, title);
            EditorGUI.indentLevel++;
            var objects = importer.GetExternalObjectMap().Where(x => x.Key.type == typeof(T));
            foreach (var obj in objects)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel(obj.Key.name);
                var asset = EditorGUILayout.ObjectField(obj.Value, obj.Key.type, true) as T;
                if (asset != obj.Value)
                {
                    importer.SetExternalUnityObject(obj.Key, asset);
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUI.indentLevel--;
        }
    }
}
#endif