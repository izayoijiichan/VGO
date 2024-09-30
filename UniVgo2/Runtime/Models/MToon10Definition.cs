// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : MToon10Definition
// ----------------------------------------------------------------------
#nullable enable
#if UNIVGO_ENABLE_MTOON_1_0
namespace UniVgo2
{
    using UnityEngine;
    using UnityEngine.Rendering;
#if UNIVGO_ENABLE_MTOON_1_0
#if VRMC_UNIVRM1_0_125_OR_NEWER
    using VRM10.MToon10;
#else
    using VRMShaders.VRM10.MToon10.Runtime;
#endif
#endif

    /// <summary>
    /// MToon 1.0 Definition
    /// </summary>
    /// <remarks>
    /// It is common for BRP and URP.
    /// </remarks>
    public class MToon10Definition
    {
        /// <summary>Alpha Mode</summary>
        public MToon10AlphaMode AlphaMode { get; set; }

        /// <summary>Transparent with ZWrite</summary>
        public MToon10TransparentWithZWriteMode TransparentWithZWrite { get; set; }

        /// <summary>Alpha Cutoff</summary>
        public float AlphaCutoff { get; set; }

        /// <summary>Render Queue Offset Number</summary>
        public int RenderQueueOffsetNumber { get; set; }

        /// <summary>Double Sided</summary>
        public MToon10DoubleSidedMode DoubleSided { get; set; }

        /// <summary>Base Color Factor</summary>
        public Color BaseColorFactor { get; set; }

        /// <summary>Base Color Texture</summary>
        public Texture2D? BaseColorTexture { get; set; }

        /// <summary>Base Color Texture Scale</summary>
        public Vector2 BaseColorTextureScale { get; set; }

        /// <summary>Base Color Texture Offset</summary>
        public Vector2 BaseColorTextureOffset { get; set; }

        /// <summary>Shade Color Factor</summary>
        public Color ShadeColorFactor { get; set; }

        /// <summary>Shade Color Texture</summary>
        public Texture2D? ShadeColorTexture { get; set; }

        /// <summary>Normal Texture</summary>
        public Texture2D? NormalTexture { get; set; }

        /// <summary>Normal Texture Scale</summary>
        public float NormalTextureScale { get; set; }

        /// <summary>Shading Shift Factor</summary>
        public float ShadingShiftFactor { get; set; }

        /// <summary>Shading Shift Texture</summary>
        public Texture2D? ShadingShiftTexture { get; set; }

        /// <summary>Shading Shift Texture Scale</summary>
        public float ShadingShiftTextureScale { get; set; }

        /// <summary>Shading Toony Factor</summary>
        public float ShadingToonyFactor { get; set; }

        /// <summary>Global Illumination Equalization Factor</summary>
        public float GiEqualizationFactor { get; set; }

        /// <summary>Emissive Factor</summary>
        public Color EmissiveFactor { get; set; }

        /// <summary>Emissive Texture</summary>
        public Texture2D? EmissiveTexture { get; set; }

        /// <summary>Matcap Color Factor</summary>
        public Color MatcapColorFactor { get; set; }

        /// <summary>Matcap Texture</summary>
        public Texture2D? MatcapTexture { get; set; }

        /// <summary>Parametric Rim Color Factor</summary>
        public Color ParametricRimColorFactor { get; set; }

        /// <summary>Parametric Rim Fresnel Power Factor</summary>
        public float ParametricRimFresnelPowerFactor { get; set; }

        /// <summary>Parametric Rim Lift Factor</summary>
        public float ParametricRimLiftFactor { get; set; }

        /// <summary>Rim Multiply Texture</summary>
        public Texture2D? RimMultiplyTexture { get; set; }

        /// <summary>Rim Lighting Mix Factor</summary>
        public float RimLightingMixFactor { get; set; }

        /// <summary>Outline Width Mode</summary>
        public MToon10OutlineMode OutlineWidthMode { get; set; }

        /// <summary>Outline Width Factor</summary>
        public float OutlineWidthFactor { get; set; }

        /// <summary>Outline Width Multiply Texture</summary>
        public Texture2D? OutlineWidthMultiplyTexture { get; set; }

        /// <summary>Outline Color Factor</summary>
        public Color OutlineColorFactor { get; set; }

        /// <summary>Outline Lighting Mix Factor</summary>
        public float OutlineLightingMixFactor { get; set; }

        /// <summary>UV Animation Mask Texture</summary>
        public Texture2D? UvAnimationMaskTexture { get; set; }

        /// <summary>UV Animation Scroll X Speed Factor</summary>
        public float UvAnimationScrollXSpeedFactor { get; set; }

        /// <summary>UV Animation Scroll Y Speed Factor</summary>
        public float UvAnimationScrollYSpeedFactor { get; set; }

        /// <summary>UV Animation Rotation Speed Factor</summary>
        public float UvAnimationRotationSpeedFactor { get; set; }

        /// <summary>Unity Cull Mode</summary>
        public CullMode UnityCullMode { get; set; }

        /// <summary>Unity Src Blend</summary>
        public BlendMode UnitySrcBlend { get; set; }

        /// <summary>Unity Dst Blend</summary>
        public BlendMode UnityDstBlend { get; set; }

        /// <summary>Unity ZWrite</summary>
        public bool UnityZWrite { get; set; }

        /// <summary>Unity Alpha to Mask</summary>
        public bool UnityAlphaToMask { get; set; }

        /// <summary>Editor Edit Mode</summary>
        public int EditorEditMode { get; set; }
    }
}
#endif