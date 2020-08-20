// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : BlendShapeConfiguration
// ----------------------------------------------------------------------
namespace UniVgo2
{
    using NewtonVgo;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// BlendShape Configuration
    /// </summary>
    [CreateAssetMenu(menuName = "VGO/BlendShapeConfiguration")]
    [Serializable]
    public class BlendShapeConfiguration : ScriptableObject
    {
        ///// <summary>The name of this info.</summary>
        //public string name;

        /// <summary>The kind of the BlendShape.</summary>
        public VgoBlendShapeKind kind;

        /// <summary>List of face parts.</summary>
        /// <remarks>
        /// By setting Eyelid and Mouth by the user,
        /// it becomes possible for the application to control such as ignoring changes in the eyelids and mouth when using FaceRig or microphone.
        /// The ones included in blinks and visemes will work without being set here.
        /// </remarks>
        public List<BlendShapeFacePart> faceParts = new List<BlendShapeFacePart>();

        /// <summary>List of blink.</summary>
        public List<BlendShapeBlink> blinks = new List<BlendShapeBlink>();

        /// <summary>Visemes.</summary>
        public List<BlendShapeViseme> visemes = new List<BlendShapeViseme>();

        /// <summary>List of preset.</summary>
        public List<VgoMeshBlendShapePreset> presets = new List<VgoMeshBlendShapePreset>();
    }
}
