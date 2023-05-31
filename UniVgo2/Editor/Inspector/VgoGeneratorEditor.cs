// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Editor
// @Class     : VgoGeneratorEditor
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Editor
{
    using NewtonVgo;
    using System;
    using UnityEditor;
    using UnityEngine;
    using UniVgo2;

    /// <summary>
    /// VGO Generator Editor
    /// </summary>
    [CustomEditor(typeof(VgoGenerator))]
    public class VgoGeneratorEditor : ScriptEditorBase
    {
        /// <summary>A inspector's target.</summary>
        private VgoGenerator? _Target;

        /// <summary>A generator info property.</summary>
        private SerializedProperty? _GeneratorInfoProperty;

        /// <summary>An array of generator info property name.</summary>
        private readonly string[] _GeneratorInfoPropertyNames = new string[]
        {
            "name",
            "version",
        };

        /// <summary>A content of export button.</summary>
        private readonly GUIContent _ExportButtonContent = new GUIContent("Export VGO");

        /// <summary>An array of export button layout option.</summary>
        private readonly GUILayoutOption[] _ExportButtonLayoutOptions = new GUILayoutOption[]
        {
            GUILayout.Width(200),
            GUILayout.Height(22),
        };

        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        protected override void OnEnable()
        {
            base.OnEnable();

            if (target is VgoGenerator vgoGenerator)
            {
                //
            }
            else
            {
                throw new InvalidCastException();
            }

            _Target = vgoGenerator;

            _GeneratorInfoProperty = serializedObject.FindProperty(nameof(VgoGenerator.GeneratorInfo));

            if (vgoGenerator.GeneratorInfo == null)
            {
                vgoGenerator.GeneratorInfo = new VgoGeneratorInfo();
            }

            if (string.IsNullOrEmpty(vgoGenerator.GeneratorInfo.name))
            {
                vgoGenerator.GeneratorInfo.name = Vgo.Generator;
            }

            if (string.IsNullOrEmpty(vgoGenerator.GeneratorInfo.version))
            {
                vgoGenerator.GeneratorInfo.version = VgoVersion.VERSION;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void OnInspectorGUI()
        {
            // Script
            //base.OnInspectorGUI();

            serializedObject.Update();

            // Generator Info
            if (_GeneratorInfoProperty != null)
            {
                EditorGUI.BeginDisabledGroup(true);

                SetPropertyFields(_GeneratorInfoProperty, _GeneratorInfoPropertyNames);

                EditorGUI.EndDisabledGroup();
            }

            EditorGUILayout.Space();

            // Export Button
            {
                EditorGUILayout.BeginHorizontal();

                GUILayout.FlexibleSpace();

                if (GUILayout.Button(_ExportButtonContent, GUI.skin.button, _ExportButtonLayoutOptions))
                {
                    if (_Target != null)
                    {
                        PopupWindow.Show(new Rect(), new VgoGeneratorPopupWindowContent(_Target.gameObject));
                    }
                }

                GUILayout.FlexibleSpace();

                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
