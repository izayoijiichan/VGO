// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Editor
// @Class     : ScriptEditorBase
// ----------------------------------------------------------------------
namespace UniVgo2.Editor
{
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Script Editor Base
    /// </summary>
    public abstract class ScriptEditorBase : Editor
    {
        /// <summary>Script Property</summary>
        protected SerializedProperty _ScriptProperty;

        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        protected virtual void OnEnable()
        {
            _ScriptProperty = serializedObject.FindProperty("m_Script");
        }

        /// <summary>
        /// 
        /// </summary>
        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(_ScriptProperty);
            EditorGUI.EndDisabledGroup();
        }

        /// <summary>
        /// Set property field.
        /// </summary>
        /// <param name="serializedProperty"></param>
        /// <param name="fieldName"></param>
        protected virtual void SetPropertyField(SerializedProperty serializedProperty, string fieldName)
        {
            var property = serializedProperty.FindPropertyRelative(fieldName);

            if (property == null)
            {
                Debug.LogErrorFormat("SerializedProperty not found. fieldName: {0}", fieldName);
            }
            else
            {
                EditorGUILayout.PropertyField(property);
            }
        }

        /// <summary>
        /// Set property fields.
        /// </summary>
        /// <param name="serializedProperty"></param>
        /// <param name="fieldNames"></param>
        protected virtual void SetPropertyFields(SerializedProperty serializedProperty, IEnumerable<string> fieldNames)
        {
            foreach (string fieldName in fieldNames)
            {
                SetPropertyField(serializedProperty, fieldName);
            }
        }
    }
}
