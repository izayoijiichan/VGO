// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Editor
// @Class     : VgoGeneratorPopupWindowContent
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Editor
{
    using NewtonVgo;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// VGO Generator Popup Window Content
    /// </summary>
    public class VgoGeneratorPopupWindowContent : PopupWindowContent
    {
        /// <summary>The radio button style.</summary>
        private readonly GUIStyle _RadioButtonStyte = new GUIStyle(EditorStyles.radioButton);

        /// <summary>A label text of vgo export.</summary>
        private readonly string _VgoExportLabelText = "VGO Export";

        /// <summary>A label text of vgo spec.</summary>
        private readonly string _VgoSpecLabelText = "VGO Spec";

        /// <summary>A label text of geometry coordinate.</summary>
        private readonly string _GeometryCoordinateLabelText = "Geometry Coordinates";

        /// <summary>An array of geometry coordinate radio button text.</summary>
        private readonly string[] _GeometryCoordinateRadioTexts = new string[] { " Left Hand", " Right Hand" };

        /// <summary>A label text of UV coordinate.</summary>
        private readonly string _UVCoordinateLabelText = "UV Coordinates";

        /// <summary>An array of UV coordinate radio button text.</summary>
        private readonly string[] _UVCoordinateRadioTexts = new string[] { " Bottom Left", " Top Left" };

        /// <summary>A label text of JSON or BSON.</summary>
        private readonly string _JsonOrBsonLabelText = "JSON or BSON";

        /// <summary>An array of JSON or BSON radio button text.</summary>
        private readonly string[] _JsonOrBsonRadioTexts = new string[] { " JSON", " BSON" };

        /// <summary>A label text of crypt algorithms.</summary>
        private readonly string _CryptAlgorithmsLabelText = "Crypt Algorithms";

        /// <summary>An array of crypt algorithms radio button text.</summary>
        private readonly string[] _CryptAlgorithmsRadioTexts = new string[] { " None", " AES", " Base64" };

        ///// <summary>A label text of crypt key.</summary>
        //private readonly string _CryptKeyLabelText = "Crypt Key";

        ///// <summary>A label text of resource type.</summary>
        //private readonly string _ResourceTypeLabelText = "Resource Type";

        ///// <summary>An array of resource type radio button text.</summary>
        //private readonly string[] _ResourceTypeRadioTexts = new string[] { " Combine", " Separate" };

        /// <summary>A content of export button.</summary>
        private readonly GUIContent _ExportButtonContent = new GUIContent("Export");

        /// <summary>An array of export button layout option.</summary>
        private readonly GUILayoutOption[] _ExportButtonLayoutOptions = new GUILayoutOption[]
        {
            GUILayout.Width(200),
            GUILayout.Height(22),
        };

        /// <summary>Size of this window.</summary>
        private readonly Vector2 _WindowSize = new Vector2(400, 180);

        /// <summary>The target GameObject.</summary>
        private readonly GameObject _TargetGameObject;

        /// <summary>The selected index of the geometry coordinate.</summary>
        private int _GeometryCoordinateIndex = 0;

        /// <summary>The selected index of the UV coordinate.</summary>
        private int _UVCoordinateIndex = 0;

        /// <summary>The selected index of the JSON or BSON.</summary>
        private int _JsonOrBsonIndex = 0;

        /// <summary>The selected index of the crypt algorithms.</summary>
        private int _CryptAlgorithmsIndex = 0;

        ///// <summary>The crypt key.</summary>
        //private string _CryptKey = string.Empty;

        ///// <summary>The selected index of the resource type.</summary>
        //private int _ResourceTypeIndex = 0;

        /// <summary>
        /// Create a new instance of VgoGeneratorWindowContent.
        /// </summary>
        /// <param name="targetGameObject"></param>
        public VgoGeneratorPopupWindowContent(GameObject targetGameObject)
        {
            _TargetGameObject = targetGameObject;
        }

        /// <summary>
        /// The size of the popup window.
        /// </summary>
        /// <returns>The size of the popup window.</returns>
        public override Vector2 GetWindowSize()
        {
            return _WindowSize;
        }

        /// <summary>
        /// Callback for drawing GUI controls for the popup window.
        /// </summary>
        /// <param name="rect">The rectangle to draw the GUI inside.</param>
        public override void OnGUI(Rect rect)
        {
            EditorGUI.indentLevel++;

            // Toolbar
            using (new EditorGUILayout.HorizontalScope(EditorStyles.toolbar))
            {
                EditorGUILayout.LabelField(_VgoExportLabelText, EditorStyles.boldLabel);
            }

            EditorGUILayout.Space();

            // VGO Spec
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUI.BeginDisabledGroup(true);

                EditorGUILayout.PrefixLabel(_VgoSpecLabelText);

                EditorGUILayout.TextField(Vgo.SpecVersion);

                EditorGUI.EndDisabledGroup();

                EditorGUILayout.EndHorizontal();
            }

            // Geometry Coordinate
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.PrefixLabel(_GeometryCoordinateLabelText);

                _GeometryCoordinateIndex = GUILayout.SelectionGrid(_GeometryCoordinateIndex, _GeometryCoordinateRadioTexts, xCount: 2, _RadioButtonStyte);

                EditorGUILayout.EndHorizontal();
            }

            // UV Coordinate
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.PrefixLabel(_UVCoordinateLabelText);

                _UVCoordinateIndex = GUILayout.SelectionGrid(_UVCoordinateIndex, _UVCoordinateRadioTexts, xCount: 2, _RadioButtonStyte);

                EditorGUILayout.EndHorizontal();
            }

            // JSON or BSON
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.PrefixLabel(_JsonOrBsonLabelText);

                _JsonOrBsonIndex = GUILayout.SelectionGrid(_JsonOrBsonIndex, _JsonOrBsonRadioTexts, xCount: 2, _RadioButtonStyte);

                EditorGUILayout.EndHorizontal();
            }

            // Crypt Algorithms
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.PrefixLabel(_CryptAlgorithmsLabelText);

                _CryptAlgorithmsIndex = GUILayout.SelectionGrid(_CryptAlgorithmsIndex, _CryptAlgorithmsRadioTexts, xCount: 4, _RadioButtonStyte);

                EditorGUILayout.EndHorizontal();
            }

            // Crypt Key (AES)
            //{
            //    EditorGUILayout.BeginHorizontal();

            //    EditorGUILayout.PrefixLabel(_CryptKeyLabelText);

            //    GUI.enabled = _CryptAlgorithmsIndex == 1;

            //    _CryptKey = EditorGUILayout.TextField(_CryptKey);

            //    GUI.enabled = true;

            //    EditorGUILayout.EndHorizontal();
            //}

            // Resource Type
            //{
            //    EditorGUILayout.BeginHorizontal();

            //    EditorGUILayout.PrefixLabel(_ResourceTypeLabelText);

            //    _ResourceTypeIndex = GUILayout.SelectionGrid(_ResourceTypeIndex, _ResourceTypeRadioTexts, xCount: 2, _RadioButtonStyte);

            //    EditorGUILayout.EndHorizontal();
            //}

            EditorGUI.indentLevel--;

            EditorGUILayout.Space();

            // Export Button
            {
                EditorGUILayout.BeginHorizontal();

                GUILayout.FlexibleSpace();

                if (GUILayout.Button(_ExportButtonContent, GUI.skin.button, _ExportButtonLayoutOptions))
                {
                    ExportVgo();

                    //GUILayout.FlexibleSpace();

                    //EditorGUILayout.EndHorizontal();

                    //GUIUtility.ExitGUI();
                }

                GUILayout.FlexibleSpace();

                EditorGUILayout.EndHorizontal();
            }
        }

        /// <summary>
        /// Export VGO.
        /// </summary>
        private void ExportVgo()
        {
            VgoGeometryCoordinate geometryCoordinate = _GeometryCoordinateIndex == 0
                ? VgoGeometryCoordinate.LeftHanded
                : VgoGeometryCoordinate.RightHanded;

            VgoUVCoordinate uvCoordinate = _UVCoordinateIndex == 0
                ? VgoUVCoordinate.BottomLeft
                : VgoUVCoordinate.TopLeft;

            bool isBson = _JsonOrBsonIndex == 1;

            string? cryptographyAlgorithms
                = _CryptAlgorithmsIndex == 0 ? null
                : _CryptAlgorithmsIndex == 1 ? VgoCryptographyAlgorithms.AES
                : _CryptAlgorithmsIndex == 2 ? VgoCryptographyAlgorithms.Base64
                : null;

            VgoExportProcessor.ExportVgo(_TargetGameObject, geometryCoordinate, uvCoordinate, ImageType.PNG, isBson, cryptographyAlgorithms);
        }
    }
}
