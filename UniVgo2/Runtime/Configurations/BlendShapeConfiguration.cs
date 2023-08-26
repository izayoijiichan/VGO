// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : BlendShapeConfiguration
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using NewtonVgo;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// BlendShape Configuration
    /// </summary>
    [CreateAssetMenu(menuName = "Vgo/Blend Shape Configuration")]
    [Serializable]
    public class BlendShapeConfiguration : ScriptableObject
    {
        ///// <summary>The name of this configuration.</summary>
        //public string name;

        /// <summary>The name of this configuration.</summary>
        public string Name
        {
            get => name;
            set => name = value;
        }

        /// <summary>The kind of the BlendShape.</summary>
        /// <remarks>For VgoScriptedImporter it is set to public.</remarks>
        [SerializeField]
        public VgoBlendShapeKind kind;
        //private VgoBlendShapeKind kind;

        /// <summary>The kind of the BlendShape.</summary>
        public VgoBlendShapeKind Kind
        {
            get => kind;
            set => kind = value;
        }

        /// <summary>List of face parts.</summary>
        /// <remarks>For VgoScriptedImporter it is set to public.</remarks>
        [SerializeField]
        public List<BlendShapeFacePart> faceParts = new List<BlendShapeFacePart>();
        //private List<BlendShapeFacePart> faceParts = new List<BlendShapeFacePart>();

        /// <summary>List of face parts.</summary>
        /// <remarks>
        /// By setting Eyelid and Mouth by the user,
        /// it becomes possible for the application to control such as ignoring changes in the eyelids and mouth when using FaceRig or microphone.
        /// The ones included in blinks and visemes will work without being set here.
        /// </remarks>
        public List<BlendShapeFacePart> FaceParts
        {
            get => faceParts;
            set => faceParts = value;
        }

        /// <summary>List of blink.</summary>
        /// <remarks>For VgoScriptedImporter it is set to public.</remarks>
        [SerializeField]
        public List<BlendShapeBlink> blinks = new List<BlendShapeBlink>();
        //private List<BlendShapeBlink> blinks = new List<BlendShapeBlink>();

        /// <summary>List of blink.</summary>
        public List<BlendShapeBlink> Blinks
        {
            get => blinks;
            set => blinks = value;
        }

        /// <summary>Visemes.</summary>
        /// <remarks>For VgoScriptedImporter it is set to public.</remarks>
        [SerializeField]
        public List<BlendShapeViseme> visemes = new List<BlendShapeViseme>();
        //private List<BlendShapeViseme> visemes = new List<BlendShapeViseme>();

        /// <summary>Visemes.</summary>
        public List<BlendShapeViseme> Visemes
        {
            get => visemes;
            set => visemes = value;
        }

        /// <summary>List of preset.</summary>
        /// <remarks>For VgoScriptedImporter it is set to public.</remarks>
        [SerializeField]
        public List<VgoMeshBlendShapePreset> presets = new List<VgoMeshBlendShapePreset>();
        //private List<VgoMeshBlendShapePreset> presets = new List<VgoMeshBlendShapePreset>();

        /// <summary>List of preset.</summary>
        public List<VgoMeshBlendShapePreset> Presets
        {
            get => presets;
            set => presets = value;
        }
    }
}
