// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : BlendShapeConfig
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using NewtonVgo;
    using System.Collections.Generic;

    /// <summary>
    /// BlendShape Config
    /// </summary>
    public class BlendShapeConfig
    {
        /// <summary>The name of this info.</summary>
        private string? _Name;

        /// <summary>The kind of the BlendShape.</summary>
        private VgoBlendShapeKind _Kind;

        /// <summary>List of face parts.</summary>
        private List<BlendShapeFacePart> _FaceParts = new List<BlendShapeFacePart>();

        /// <summary>List of blink.</summary>
        private List<BlendShapeBlink> _Blinks = new List<BlendShapeBlink>();

        /// <summary>Visemes.</summary>
        private List<BlendShapeViseme> _Visemes = new List<BlendShapeViseme>();

        /// <summary>List of preset.</summary>
        private List<VgoMeshBlendShapePreset> _Presets = new List<VgoMeshBlendShapePreset>();

        /// <summary>The name of this info.</summary>
        public string? Name { get => _Name; set => _Name = value; }

        /// <summary>The kind of the BlendShape.</summary>
        public VgoBlendShapeKind Kind { get => _Kind; set => _Kind = value; }

        /// <summary>List of face parts.</summary>
        /// <remarks>
        /// By setting Eyelid and Mouth by the user,
        /// it becomes possible for the application to control such as ignoring changes in the eyelids and mouth when using FaceRig or microphone.
        /// The ones included in blinks and visemes will work without being set here.
        /// </remarks>
        public List<BlendShapeFacePart> FaceParts { get => _FaceParts; set => _FaceParts = value; }

        /// <summary>List of blink.</summary>
        public List<BlendShapeBlink> Blinks { get => _Blinks; set => _Blinks = value; }

        /// <summary>Visemes.</summary>
        public List<BlendShapeViseme> Visemes { get => _Visemes; set => _Visemes = value; }

        /// <summary>List of preset.</summary>
        public List<VgoMeshBlendShapePreset> Presets { get => _Presets; set => _Presets = value; }
    }
}
