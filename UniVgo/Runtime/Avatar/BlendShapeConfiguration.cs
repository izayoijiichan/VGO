// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : BlendShapeConfiguration
// ----------------------------------------------------------------------
namespace UniVgo
{
    using NewtonGltf;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using VgoGltf;

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
        public List<VGO_BlendShapeFacePart> faceParts = new List<VGO_BlendShapeFacePart>();

        /// <summary>List of blink.</summary>
        public List<VGO_BlendShapeBlink> blinks = new List<VGO_BlendShapeBlink>();

        /// <summary>Visemes.</summary>
        public List<VGO_BlendShapeViseme> visemes = new List<VGO_BlendShapeViseme>();

        /// <summary>List of preset.</summary>
        public List<BlendShapePreset> presets = new List<BlendShapePreset>();
    }
}
