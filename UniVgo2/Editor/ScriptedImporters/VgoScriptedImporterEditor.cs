// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Editor
// @Class     : VgoScriptedImporterEditor
// ----------------------------------------------------------------------
namespace UniVgo2.Editor
{
    using System.Linq;
    using UnityEditor;
#if UNITY_2020_2_OR_NEWER
    using UnityEditor.AssetImporters;
#else
    using UnityEditor.Experimental.AssetImporters;
#endif
    using UnityEngine;

    /// <summary>
    /// VGO Scripted Importer Editor
    /// </summary>
    [CustomEditor(typeof(VgoScriptedImporter))]
    public class VgoScriptedImporterEditor : ScriptedImporterEditor
    {
        /// <summary></summary>
        protected bool _isOpen = true;

        /// <summary>
        /// Override this method to create your own Inpsector GUI for a ScriptedImporter.
        /// </summary>
        public override void OnInspectorGUI()
        {
            var importer = target as VgoScriptedImporter;

            EditorGUILayout.LabelField("Extract settings");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Avatar");
            GUI.enabled = (importer.GetExternalUnityObjects<Avatar>().Any() == false);
            if (GUILayout.Button("Extract"))
            {
                importer.ExtractAvatars();
                GUIUtility.ExitGUI();
            }
            GUI.enabled = !GUI.enabled;
            if (GUILayout.Button("Clear"))
            {
                importer.ClearExtarnalObjects<Avatar>();
                GUIUtility.ExitGUI();
            }
            GUI.enabled = true;
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("BlendShapes");
            GUI.enabled = (importer.GetExternalUnityObjects<BlendShapeConfiguration>().Any() == false);
            if (GUILayout.Button("Extract"))
            {
                importer.ExtractBlendShapes();
                GUIUtility.ExitGUI();
            }
            GUI.enabled = !GUI.enabled;
            if (GUILayout.Button("Clear"))
            {
                importer.ClearExtarnalObjects<BlendShapeConfiguration>();
                GUIUtility.ExitGUI();
            }
            GUI.enabled = true;
            EditorGUILayout.EndHorizontal();

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

            DrawRemapGUI<Avatar>("Avatar Remap", importer);
            DrawRemapGUI<BlendShapeConfiguration>("Blendshape Remap", importer);
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
        protected virtual void DrawRemapGUI<T>(string title, VgoScriptedImporter importer) where T : UnityEngine.Object
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
