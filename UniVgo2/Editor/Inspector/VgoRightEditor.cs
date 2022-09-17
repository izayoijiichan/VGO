// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Editor
// @Class     : VgoRightEditor
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Editor
{
    using UnityEditor;
    using UniVgo2;

    /// <summary>
    /// VGO Right Editor
    /// </summary>
    [CustomEditor(typeof(VgoRight))]
    public class VgoRightEditor : ScriptEditorBase
    {
        /// <summary>Right Property</summary>
        private SerializedProperty? _RightProperty;

        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        protected override void OnEnable()
        {
            base.OnEnable();

            _RightProperty = serializedObject.FindProperty("Right");
        }

        /// <summary>
        /// 
        /// </summary>
        public override void OnInspectorGUI()
        {
            // Script
            //base.OnInspectorGUI();


            serializedObject.Update();

            // Right
            if (_RightProperty != null)
            {
                //EditorGUILayout.LabelField(_RightProperty.name, EditorStyles.boldLabel);
                SetPropertyFields(_RightProperty, new string[]
                {
                "title",
                "author",
                "organization",
                "createdDate",
                "updatedDate",
                "version",
                "distributionUrl",
                "licenseUrl",
                });
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
