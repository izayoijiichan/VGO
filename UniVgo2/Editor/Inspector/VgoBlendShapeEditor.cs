﻿// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Editor
// @Class     : VgoBlendShapeEditor
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Editor
{
    using NewtonVgo;
    using System;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;
    using UniVgo2;

    /// <summary>
    /// VGO Blend Shape Editor
    /// </summary>
    [CustomEditor(typeof(VgoBlendShape))]
    public class VgoBlendShapeEditor : ScriptEditorBase
    {
        /// <summary></summary>
        private VgoBlendShape? _Target;

        /// <summary>Blend Shape Configuration Serialized Property</summary>
        private SerializedProperty? _BlendShapeConfigurationSerializedProperty;

        /// <summary>Blend Shape Configuration</summary>
        private BlendShapeConfiguration? _BlendShapeConfiguration;

        /// <summary>An array of Blend Shape Index.</summary>
        private int[] _BlendShapeIndices = new int[0];

        /// <summary>An array of Blend Shape Name.</summary>
        private string[] _BlendShapeNames = new string[0];

        /// <summary>Whether face parts block is open.</summary>
        private bool _FacePartsBlockIsOpen = false;

        /// <summary>Whether blinks block is open.</summary>
        private bool _BlinksBlockIsOpen = false;

        /// <summary>Whether visemes block is open.</summary>
        private bool _VisemesBlockIsOpen = false;

        /// <summary>Whether presets block is open.</summary>
        private bool _PresetsBlockIsOpen = false;

        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        protected override void OnEnable()
        {
            base.OnEnable();

            if (target is VgoBlendShape vgoBlendShapeComponent)
            {
                //
            }
            else
            {
                throw new InvalidCastException();
            }

            if (vgoBlendShapeComponent == null)
            {
                return;
            }

            _Target = vgoBlendShapeComponent;

            if (vgoBlendShapeComponent.TryGetComponent<SkinnedMeshRenderer>(out var skinnedMeshRenderer))
            {
                int blendShapeCount = skinnedMeshRenderer.sharedMesh.blendShapeCount;

                if (blendShapeCount > 0)
                {
                    _BlendShapeIndices = new int[blendShapeCount];
                    _BlendShapeNames = new string[blendShapeCount];

                    for (int index = 0; index < blendShapeCount; index++)
                    {
                        _BlendShapeIndices[index] = index;
                        _BlendShapeNames[index] = skinnedMeshRenderer.sharedMesh.GetBlendShapeName(index);
                    }
                }
            }

            SerializedProperty? blendShapeConfigurationSerializedProperty = serializedObject.FindProperty(nameof(VgoBlendShape.BlendShapeConfiguration));

            if (blendShapeConfigurationSerializedProperty == null)
            {
                return;
            }

            _BlendShapeConfigurationSerializedProperty = blendShapeConfigurationSerializedProperty;

            if (blendShapeConfigurationSerializedProperty.objectReferenceValue is BlendShapeConfiguration blendShapeConfiguration)
            {
                //
            }
            else
            {
                return;
            }

            if (blendShapeConfiguration == null)
            {
                return;
            }

            _BlendShapeConfiguration = blendShapeConfiguration;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (_Target == null)
            {
                return;
            }

            if (_BlendShapeConfigurationSerializedProperty == null)
            {
                return;
            }

            if (_BlendShapeConfigurationSerializedProperty.objectReferenceValue is BlendShapeConfiguration blendShapeConfiguration)
            {
                //
            }
            else
            {
                return;
            }

            _BlendShapeConfiguration = blendShapeConfiguration;

            if (_BlendShapeConfiguration == null)
            {
                return;
            }

            DrawKind();

            DrawFaceParts();

            DrawBlinks();

            DrawVisemes();

            DrawPresets();
        }

        /// <summary>
        /// Draw kinds in the inspector.
        /// </summary>
        private void DrawKind()
        {
            if (_BlendShapeConfiguration == null)
            {
                return;
            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PrefixLabel("Kind");

            _BlendShapeConfiguration.Kind = (VgoBlendShapeKind)EditorGUILayout.EnumPopup(_BlendShapeConfiguration.Kind);

            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// Draw face parts in the inspector.
        /// </summary>
        private void DrawFaceParts()
        {
            if (_BlendShapeConfiguration == null)
            {
                return;
            }

            List<BlendShapeFacePart> confFaceParts = _BlendShapeConfiguration.FaceParts;

            int listCount = confFaceParts.Count;

            {
                EditorGUILayout.BeginHorizontal();

                _FacePartsBlockIsOpen = EditorGUILayout.Foldout(_FacePartsBlockIsOpen, "Face Parts");

                listCount = EditorGUILayout.IntField(string.Empty, listCount, GUILayout.Width(50));

                EditorGUILayout.EndHorizontal();
            }

            if (confFaceParts.Count != listCount)
            {
                AdjustListCount(confFaceParts, listCount);
            }

            if (confFaceParts.Count == 0)
            {
                return;
            }

            if (_FacePartsBlockIsOpen == false)
            {
                return;
            }

            EditorGUI.indentLevel++;

            Color defaultBackgroundColor = GUI.backgroundColor;

            var boxStyle = new GUIStyle("Box");

            using (new EditorGUILayout.VerticalScope(boxStyle, GUILayout.ExpandWidth(true)))
            {
                for (int index = 0; index < confFaceParts.Count; index++)
                {
                    BlendShapeFacePart facePart = confFaceParts[index];

                    using (new EditorGUILayout.VerticalScope(boxStyle, GUILayout.ExpandWidth(true)))
                    {
                        facePart.Index = EditorGUILayout.IntPopup("Index", facePart.Index, _BlendShapeNames, _BlendShapeIndices);

                        facePart.Type = (VgoBlendShapeFacePartsType)EditorGUILayout.EnumPopup("Type", facePart.Type);
                    }
                }

                GUI.backgroundColor = defaultBackgroundColor;
            }

            EditorGUI.indentLevel--;
        }

        /// <summary>
        /// Draw blinks in the inspector.
        /// </summary>
        private void DrawBlinks()
        {
            if (_BlendShapeConfiguration == null)
            {
                return;
            }

            List<BlendShapeBlink> confBlinks = _BlendShapeConfiguration.Blinks;

            int listCount = confBlinks.Count;

            {
                EditorGUILayout.BeginHorizontal();

                _BlinksBlockIsOpen = EditorGUILayout.Foldout(_BlinksBlockIsOpen, "Blinks");

                listCount = EditorGUILayout.IntField(string.Empty, listCount, GUILayout.Width(50));

                EditorGUILayout.EndHorizontal();
            }

            if (confBlinks.Count != listCount)
            {
                AdjustListCount(confBlinks, listCount);
            }

            if (confBlinks.Count == 0)
            {
                return;
            }

            if (_BlinksBlockIsOpen == false)
            {
                return;
            }

            EditorGUI.indentLevel++;

            var boxStyle = new GUIStyle("Box");

            using (new EditorGUILayout.VerticalScope(boxStyle, GUILayout.ExpandWidth(true)))
            {
                for (int index = 0; index < confBlinks.Count; index++)
                {
                    BlendShapeBlink blink = confBlinks[index];

                    using (new EditorGUILayout.VerticalScope(boxStyle, GUILayout.ExpandWidth(true)))
                    {
                        blink.Type = (VgoBlendShapeBlinkType)EditorGUILayout.EnumPopup("Type", blink.Type);

                        blink.Index = EditorGUILayout.IntPopup("Index", blink.Index, _BlendShapeNames, _BlendShapeIndices);
                    }
                }
            }

            EditorGUI.indentLevel--;
        }

        /// <summary>
        /// Draw visemes in the inspector.
        /// </summary>
        private void DrawVisemes()
        {
            if (_BlendShapeConfiguration == null)
            {
                return;
            }

            List<BlendShapeViseme> confVisemes = _BlendShapeConfiguration.Visemes;

            int listCount = confVisemes.Count;

            {
                EditorGUILayout.BeginHorizontal();

                _VisemesBlockIsOpen = EditorGUILayout.Foldout(_VisemesBlockIsOpen, "Visemes");

                listCount = EditorGUILayout.IntField(string.Empty, listCount, GUILayout.Width(50));

                EditorGUILayout.EndHorizontal();
            }

            if (confVisemes.Count != listCount)
            {
                AdjustListCount(confVisemes, listCount);
            }

            if (confVisemes.Count == 0)
            {
                return;
            }

            if (_VisemesBlockIsOpen == false)
            {
                return;
            }

            EditorGUI.indentLevel++;

            var boxStyle = new GUIStyle("Box");

            using (new EditorGUILayout.VerticalScope(boxStyle, GUILayout.ExpandWidth(true)))
            {
                for (int index = 0; index < confVisemes.Count; index++)
                {
                    BlendShapeViseme viseme = confVisemes[index];

                    using (new EditorGUILayout.VerticalScope(boxStyle, GUILayout.ExpandWidth(true)))
                    {
                        viseme.Type = (VgoBlendShapeVisemeType)EditorGUILayout.EnumPopup("Type", viseme.Type);

                        viseme.Index = EditorGUILayout.IntPopup("Index", viseme.Index, _BlendShapeNames, _BlendShapeIndices);
                    }
                }
            }

            EditorGUI.indentLevel--;
        }

        /// <summary>
        /// Draw presets in the inspector.
        /// </summary>
        private void DrawPresets()
        {
            if (_BlendShapeConfiguration == null)
            {
                return;
            }

            List<VgoMeshBlendShapePreset> confPresets = _BlendShapeConfiguration.Presets;

            int listCount = confPresets.Count;

            {
                EditorGUILayout.BeginHorizontal();

                _PresetsBlockIsOpen = EditorGUILayout.Foldout(_PresetsBlockIsOpen, "Presets");

                listCount = EditorGUILayout.IntField(string.Empty, listCount, GUILayout.Width(50));

                EditorGUILayout.EndHorizontal();
            }

            if (confPresets.Count != listCount)
            {
                AdjustListCount(confPresets, listCount);
            }

            if (confPresets.Count == 0)
            {
                return;
            }

            if (_PresetsBlockIsOpen == false)
            {
                return;
            }

            EditorGUI.indentLevel++;

            var boxStyle = new GUIStyle("Box");

            using (new EditorGUILayout.VerticalScope(boxStyle, GUILayout.ExpandWidth(true)))
            {
                for (int index = 0; index < confPresets.Count; index++)
                {
                    VgoMeshBlendShapePreset preset = confPresets[index];

                    using (new EditorGUILayout.VerticalScope(boxStyle, GUILayout.ExpandWidth(true)))
                    {
                        preset.name = EditorGUILayout.DelayedTextField("Name", preset.name);

                        preset.type = (VgoBlendShapePresetType)EditorGUILayout.EnumPopup("Type", preset.type);

                        DrawPresetBindings(preset.bindings);
                    }
                }
            }

            EditorGUI.indentLevel--;
        }

        /// <summary>
        /// Draw preset bindings in the inspector.
        /// </summary>
        /// <param name="bindings"></param>
        private void DrawPresetBindings(List<VgoMeshBlendShapeBinding> bindings)
        {
            int listCount = bindings.Count;

            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField("Bindings");

                listCount = EditorGUILayout.IntField(string.Empty, listCount, GUILayout.Width(50));

                EditorGUILayout.EndHorizontal();
            }

            if (bindings.Count != listCount)
            {
                AdjustListCount(bindings, listCount);
            }

            if (bindings.Count == 0)
            {
                return;
            }

            EditorGUI.indentLevel++;

            //var boxStyle = new GUIStyle("Box");

            //using (new EditorGUILayout.VerticalScope(boxStyle, GUILayout.ExpandWidth(true)))
            {
                for (int index = 0; index < listCount; index++)
                {
                    VgoMeshBlendShapeBinding binding = bindings[index];

                    binding.index = EditorGUILayout.IntPopup("Index", binding.index, _BlendShapeNames, _BlendShapeIndices);

                    binding.weight = EditorGUILayout.Slider("Weight", binding.weight, 0.0f, 100.0f);
                }
            }

            EditorGUI.indentLevel--;
        }

        /// <summary>
        /// Adjust the number of elements in the list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="num"></param>
        private void AdjustListCount<T>(IList<T> list, int num) where T : class, new()
        {
            int listCount = list.Count;

            if (listCount < num)
            {
                int addCount = num - listCount;

                for (int index = 0; index < addCount; index++)
                {
                    list.Add(new T());
                }
            }
            else if (listCount > num)
            {
                for (int index = listCount - 1; index > num - 1; index--)
                {
                    list.RemoveAt(index);
                }
            }
        }
    }
}
