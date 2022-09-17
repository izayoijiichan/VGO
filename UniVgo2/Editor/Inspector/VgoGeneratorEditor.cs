// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Editor
// @Class     : VgoGeneratorEditor
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Editor
{
    using NewtonVgo;
    using NewtonVgo.Security.Cryptography;
    using System;
    using UnityEditor;
    using UnityEngine;
    using UniVgo2;

    /// <summary>
    /// VGO Meta Editor
    /// </summary>
    [CustomEditor(typeof(VgoGenerator))]
    public class VgoGeneratorEditor : ScriptEditorBase
    {
        /// <summary></summary>
        private VgoGenerator? _Target;

        /// <summary>GeneratorInfo Property</summary>
        private SerializedProperty? _GeneratorInfoProperty;

        /// <summary>The selected index of the geometry coodinate.</summary>
        private int _GeometryCoordinateIndex = 0;

        /// <summary>The selected index of the UV coodinate.</summary>
        private int _UVCoordinateIndex = 0;

        /// <summary>The selected index of the JSON or BSON.</summary>
        private int _JsonOrBsonIndex = 0;

        /// <summary>The selected index of the crypt algorithms.</summary>
        private int _CryptAlgorithmsIndex = 0;

        ///// <summary></summary>
        //private string _CryptKey = string.Empty;

        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        protected override void OnEnable()
        {
            base.OnEnable();

            if (target is VgoGenerator _Target)
            {
                //
            }
            else
            {
                throw new InvalidCastException();
            }

            _GeneratorInfoProperty = serializedObject.FindProperty("GeneratorInfo");

            if (_Target.GeneratorInfo == null)
            {
                _Target.GeneratorInfo = new VgoGeneratorInfo();
            }

            if (string.IsNullOrEmpty(_Target.GeneratorInfo.name))
            {
                _Target.GeneratorInfo.name = Vgo.Generator;
            }

            if (string.IsNullOrEmpty(_Target.GeneratorInfo.version))
            {
                _Target.GeneratorInfo.version = VgoVersion.VERSION;
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
            if (_GeneratorInfoProperty != null)
            {
                //EditorGUILayout.LabelField(_GeneratorInfoProperty.name, EditorStyles.boldLabel);
                EditorGUI.BeginDisabledGroup(true);
                SetPropertyFields(_GeneratorInfoProperty, new string[]
                {
                "name",
                "version",
                });
                EditorGUI.EndDisabledGroup();
            }

            EditorGUILayout.Space();

            GUIStyle radioButtonStyte = new GUIStyle(EditorStyles.radioButton);

            // Geometry Coordinate
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Geometry Coordinates");
            _GeometryCoordinateIndex = GUILayout.SelectionGrid(_GeometryCoordinateIndex, new string[] { " Left Hand", " Right Hand" }, xCount: 2, radioButtonStyte);
            EditorGUILayout.EndHorizontal();

            // UV Coordinate
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("UV Coordinates");
            _UVCoordinateIndex = GUILayout.SelectionGrid(_UVCoordinateIndex, new string[] { " Bottom Left", " Top Left" }, xCount: 2, radioButtonStyte);
            EditorGUILayout.EndHorizontal();

            // JSON or BSON
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("JSON or BSON");
            _JsonOrBsonIndex = GUILayout.SelectionGrid(_JsonOrBsonIndex, new string[] { " JSON", " BSON" }, xCount: 2, radioButtonStyte);
            EditorGUILayout.EndHorizontal();

            // Crypt Algorithms
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Crypt Algorithms");
            _CryptAlgorithmsIndex = GUILayout.SelectionGrid(_CryptAlgorithmsIndex, new string[] { " None", " AES", " Base64" }, xCount: 4, radioButtonStyte);
            EditorGUILayout.EndHorizontal();

            //// Crypt Key
            //EditorGUILayout.BeginHorizontal();
            //EditorGUILayout.PrefixLabel("Crypt Key");
            //GUI.enabled = _CryptAlgorithmsIndex == 1;
            //_CryptKey = EditorGUILayout.TextField(_CryptKey);
            //GUI.enabled = true;
            //EditorGUILayout.EndHorizontal();

            // Export Button
            if (GUILayout.Button("Export VGO"))
            {
                VgoGeometryCoordinate geometryCoordinate = _GeometryCoordinateIndex == 0 ? VgoGeometryCoordinate.LeftHanded : VgoGeometryCoordinate.RightHanded;
                VgoUVCoordinate uvCoordinate = _UVCoordinateIndex == 0 ? VgoUVCoordinate.BottomLeft : VgoUVCoordinate.TopLeft;

                bool isBson = _JsonOrBsonIndex == 1;

                string? cryptAlgorithms;

                if (_CryptAlgorithmsIndex == 1)
                {
                    cryptAlgorithms = VgoCryptographyAlgorithms.AES;
                }
                else if (_CryptAlgorithmsIndex == 2)
                {
                    cryptAlgorithms = VgoCryptographyAlgorithms.Base64;
                }
                else
                {
                    cryptAlgorithms = null;
                }

                VgoExportProcessor.ExportVgo(geometryCoordinate, uvCoordinate, isBson, cryptAlgorithms);
                
                //VgoExportProcessor.ExportVgo(geometryCoordinate, uvCoordinate, isBson, cryptAlgorithms, Convert.FromBase64String(_CryptKey));

                //AesCrypter aesCrypter = new AesCrypter();
                //byte[] cryptKey = aesCrypter.GenerateRandomKey(256);
                //VgoExportProcessor.ExportVgo(geometryCoordinate, uvCoordinate, isBson, cryptAlgorithms, cryptKey);

                GUIUtility.ExitGUI();
            }
        }
    }
}
