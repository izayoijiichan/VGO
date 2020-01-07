// ----------------------------------------------------------------------
// @Namespace : UniVgo.Editor
// @Class     : VgoMetaEditor
// ----------------------------------------------------------------------
namespace UniVgo.Editor
{
    using UnityEditor;
    using UnityEngine;
    using UniVgo;

    /// <summary>
    /// VGO Meta Editor
    /// </summary>
    [CustomEditor(typeof(VgoMeta))]
    public class VgoMetaEditor : ScriptEditorBase
    {
        /// <summary></summary>
        private VgoMeta _Target;

        /// <summary>Meta Property</summary>
        private SerializedProperty _MetaProperty;

        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        protected override void OnEnable()
        {
            base.OnEnable();

            _Target = target as VgoMeta;

            _MetaProperty = serializedObject.FindProperty("Meta");

            if (string.IsNullOrEmpty(_Target.Meta.generatorName))
            {
                _Target.Meta.generatorName = Vgo.Generator;
            }

            if (string.IsNullOrEmpty(_Target.Meta.generatorVersion))
            {
                _Target.Meta.generatorVersion = VgoVersion.VERSION;
            }

            if (string.IsNullOrEmpty(_Target.Meta.specVersion))
            {
                _Target.Meta.specVersion = Vgo.SpecVersion;
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

            // Meta
            //EditorGUILayout.LabelField(_MetaProperty.name, EditorStyles.boldLabel);
            EditorGUI.BeginDisabledGroup(true);
            SetPropertyFields(_MetaProperty, new string[]
            { 
                "generatorName",
                "generatorVersion",
                "specVersion",
            });
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.Space();
            
            // Export Button
            if (GUILayout.Button("Export VGO"))
            {
#if UNITY_EDITOR_WIN
                VgoExportProcessor.ExportVgo();
#endif
            }
        }
    }
}
