// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : ShaderName
// ----------------------------------------------------------------------
namespace UniVgo2
{
    /// <summary>
    /// Shader Name
    /// </summary>
    public class ShaderName
    {
        #region BRP (Built-in Render Pipeline)

        /// <summary>Standard</summary>
        public const string Standard = "Standard";

        /// <summary>Particles/Standard Surface</summary>
        public const string Particles_Standard_Surface = "Particles/Standard Surface";

        /// <summary>Particles/Standard Unlit</summary>
        public const string Particles_Standard_Unlit = "Particles/Standard Unlit";

        /// <summary>Skybox/6 Sided</summary>
        public const string Skybox_6_Sided = "Skybox/6 Sided";

        /// <summary>Skybox/Cubemap</summary>
        public const string Skybox_Cubemap = "Skybox/Cubemap";

        /// <summary>Skybox/Panoramic</summary>
        public const string Skybox_Panoramic = "Skybox/Panoramic";

        /// <summary>Skybox/Procedural</summary>
        public const string Skybox_Procedural = "Skybox/Procedural";

        /// <summary>UniGLTF/StandardVColor</summary>
        public const string UniGLTF_StandardVColor = "UniGLTF/StandardVColor";

        /// <summary>Unlit/Color</summary>
        public const string Unlit_Color = "Unlit/Color";

        /// <summary>Unlit/Texture</summary>
        public const string Unlit_Texture = "Unlit/Texture";

        /// <summary>Unlit/Transparent</summary>
        public const string Unlit_Transparent = "Unlit/Transparent";

        /// <summary>Unlit/Transparent Cutout</summary>
        public const string Unlit_Transparent_Cutout = "Unlit/Transparent Cutout";

        /// <summary>UniGLTF/UniUnlit</summary>
        public const string UniGLTF_UniUnlit = "UniGLTF/UniUnlit";

        /// <summary>VRM/UnlitTexture</summary>
        public const string VRM_UnlitTexture = "VRM/UnlitTexture";

        /// <summary>VRM/UnlitTransparent</summary>
        public const string VRM_UnlitTransparent = "VRM/UnlitTransparent";

        /// <summary>VRM/UnlitCutout</summary>
        public const string VRM_UnlitCutout = "VRM/UnlitCutout";

        /// <summary>VRM/UnlitTransparentZWrite</summary>
        public const string VRM_UnlitTransparentZWrite = "VRM/UnlitTransparentZWrite";

        /// <summary>VRM/MToon</summary>
        public const string VRM_MToon = "VRM/MToon";

        #endregion

        #region URP (Universal Render Pipeline)

        /// <summary>Universal Render Pipeline/Lit</summary>
        public const string URP_Lit = "Universal Render Pipeline/Lit";

        /// <summary>Universal Render Pipeline/Simple Lit</summary>
        public const string URP_SimpleLit = "Universal Render Pipeline/Simple Lit";

        /// <summary>Universal Render Pipeline/Unlit</summary>
        public const string URP_Unlit = "Universal Render Pipeline/Unlit";

        #endregion

        #region HDRP (High Definition Render Pipeline)

        /// <summary>HDRP/Eye</summary>
        public const string HDRP_Eye = "HDRP/Eye";

        /// <summary>HDRP/Hair</summary>
        public const string HDRP_Hair = "HDRP/Hair";

        /// <summary>HDRP/Lit</summary>
        public const string HDRP_Lit = "HDRP/Lit";

        #endregion

        #region lilToon

        /// <summary>lilToon</summary>
        /// <remarks>lts.shader</remarks>
        public const string Lil_LilToon = "lilToon";

        /// <summary>lilToon Outline</summary>
        /// <remarks>lts_o.shader</remarks>
        public const string Lil_LilToonOutline = "Hidden/lilToonOutline";

        /// <summary>lilToon Outline only</summary>
        /// <remarks>lts_oo.shader</remarks>
        public const string Lil_LilToonOutlineOnly = "_lil/[Optional] lilToonOutlineOnly";

        /// <summary>lilToon Outline only Cutout</summary>
        /// <remarks>lts_cutout_oo.shader</remarks>
        public const string Lil_LilToonOutlineOnlyCutout = "_lil/[Optional] lilToonOutlineOnlyCutout";

        /// <summary>lilToon Outline only Transparent</summary>
        /// <remarks>lts_trans_oo.shader</remarks>
        public const string Lil_LilToonOutlineOnlyTransparent = "_lil/[Optional] lilToonOutlineOnlyTransparent";

        /// <summary>lilToon Cutout</summary>
        /// <remarks>lts_coutout.shader</remarks>
        public const string Lil_LilToonCutout = "Hidden/lilToonCutout";

        /// <summary>lilToon Cutout Outline</summary>
        /// <remarks>lts_cutout_o.shader</remarks>
        public const string Lil_LilToonCutoutOutline = "Hidden/lilToonCutoutOutline";

        /// <summary>lilToon Transparent</summary>
        /// <remarks>lts_trans.shader</remarks>
        public const string Lil_LilToonTransparent = "Hidden/lilToonTransparent";

        /// <summary>lilToon Transparent Outline</summary>
        /// <remarks>lts_trans_o.shader</remarks>
        public const string Lil_LilToonTransparentOutline = "Hidden/lilToonTransparentOutline";

        /// <summary>lilToon OnePass Transparent</summary>
        /// <remarks>lts_onetrans.shader</remarks>
        public const string Lil_LilToonOnePassTransparent = "Hidden/lilToonOnePassTransparent";

        /// <summary>lilToon OnePass Transparent Outline</summary>
        /// <remarks>lts_onetrans_o.shader</remarks>
        public const string Lil_LilToonOnePassTransparentOutline = "Hidden/lilToonOnePassTransparentOutline";

        /// <summary>lilToon TwoPass Transparent</summary>
        /// <remarks>lts_twotrans.shader</remarks>
        public const string Lil_LilToonTwoPassTransparent = "Hidden/lilToonTwoPassTransparent";

        /// <summary>lilToon TwoPass Transparent Outline</summary>
        /// <remarks>lts_twotrans_o.shader</remarks>
        public const string Lil_LilToonTwoPassTransparentOutline = "Hidden/lilToonTwoPassTransparentOutline";

        /// <summary>lilToon Overlay</summary>
        /// <remarks>lts_overlay.shader</remarks>
        public const string Lil_LilToonOverlay = "_lil/[Optional] lilToonOverlay";

        /// <summary>lilToon Overlay OnePass</summary>
        /// <remarks>lts_overlay_one.shader</remarks>
        public const string Lil_LilToonOverlayOnePass = "_lil/[Optional] lilToonOverlayOnePass";

        /// <summary>lilToon Refraction</summary>
        /// <remarks>lts_ref.shader</remarks>
        public const string Lil_LilToonRefraction = "Hidden/lilToonRefraction";

        /// <summary>lilToon Refraction Blur</summary>
        /// <remarks>lts_ref_blur.shader</remarks>
        public const string Lil_LilToonRefractionBlur = "Hidden/lilToonRefractionBlur";

        /// <summary>lilToon Fur</summary>
        /// <remarks>lts_fur.shader</remarks>
        public const string Lil_LilToonFur = "Hidden/lilToonFur";

        /// <summary>lilToon Fur Cutout</summary>
        /// <remarks>lts_fur_cutout.shader</remarks>
        public const string Lil_LilToonFurCutout = "Hidden/lilToonFurCutout";

        /// <summary>lilToon Fur TwoPass</summary>
        /// <remarks>lts_fur_two.shader</remarks>
        public const string Lil_LilToonFurTwoPass = "Hidden/lilToonFurTwoPass";

        /// <summary>lilToon Fur only</summary>
        /// <remarks>lts_furonly.shader</remarks>
        public const string Lil_LilToonFurOnly = "_lil/[Optional] lilToonFurOnly";

        /// <summary>lilToon Fur only Cutout</summary>
        /// <remarks>lts_furonly_cutout.shader</remarks>
        public const string Lil_LilToonFurOnlyCutout = "_lil/[Optional] lilToonFurOnlyCutout";

        /// <summary>lilToon Fur only TwoPass</summary>
        /// <remarks>lts_furonly_two.shader</remarks>
        public const string Lil_LilToonFurOnlyTwoPass = "_lil/[Optional] lilToonFurOnlyTwoPass";

        /// <summary>lilToon Gem</summary>
        /// <remarks>lts_gem.shader</remarks>
        public const string Lil_LilToonGem = "Hidden/lilToonGem";

        /// <summary>lilToon Tessellation</summary>
        /// <remarks>lts_tess.shader</remarks>
        public const string Lil_LilToonTessellation = "Hidden/lilToonTessellation";

        /// <summary>lilToon Tessellation Outline</summary>
        /// <remarks>lts_tess_o.shader</remarks>
        public const string Lil_LilToonTessellationOutline = "Hidden/lilToonTessellationOutline";

        /// <summary>lilToon Tessellation Cutout</summary>
        /// <remarks>lts_tess_cutout.shader</remarks>
        public const string Lil_LilToonTessellationCutout = "Hidden/lilToonTessellationCutout";

        /// <summary>lilToon Tessellation Cutout Outline</summary>
        /// <remarks>lts_tess_cutout_o.shader</remarks>
        public const string Lil_LilToonTessellationCutoutOutline = "Hidden/lilToonTessellationCutoutOutline";

        /// <summary>lilToon Tessellation Transparent</summary>
        /// <remarks>lts_tess_trans.shader</remarks>
        public const string Lil_LilToonTessellationTransparent = "Hidden/lilToonTessellationTransparent";

        /// <summary>lilToon Tessellation Transparent Outline</summary>
        /// <remarks>lts_tess_trans_o.shader</remarks>
        public const string Lil_LilToonTessellationTransparentOutline = "Hidden/lilToonTessellationTransparentOutline";

        /// <summary>lilToon Tessellation OnePass Transparent</summary>
        /// <remarks>lts_tess_onetrans.shader</remarks>
        public const string Lil_LilToonTessellationOnePassTransparent = "Hidden/lilToonTessellationOnePassTransparent";

        /// <summary>lilToon Tessellation OnePass Transparent Outline</summary>
        /// <remarks>lts_tess_onetrans_o.shader</remarks>
        public const string Lil_LilToonTessellationOnePassTransparentOutline = "Hidden/lilToonTessellationOnePassTransparentOutline";

        /// <summary>lilToon Tessellation TwoPass Transparent</summary>
        /// <remarks>lts_tess_twotrans.shader</remarks>
        public const string Lil_LilToonTessellationTwoPassTransparent = "Hidden/lilToonTessellationTwoPassTransparent";

        /// <summary>lilToon Tessellation TwoPass Transparent Outline</summary>
        /// <remarks>lts_tess_twotrans_o.shader</remarks>
        public const string Lil_LilToonTessellationTwoPassTransparentOutline = "Hidden/lilToonTessellationTwoPassTransparentOutline";

        /// <summary>lilToon Lite</summary>
        /// <remarks>ltsl.shader</remarks>
        public const string Lil_LilToonLite = "Hidden/lilToonLite";

        /// <summary>lilToon Lite Outline</summary>
        /// <remarks>ltsl_o.shader</remarks>
        public const string Lil_LilToonLiteOutline = "Hidden/lilToonLiteOutline";

        /// <summary>lilToon Lite Cutout</summary>
        /// <remarks>ltsl_cutout.shader</remarks>
        public const string Lil_LilToonLiteCutout = "Hidden/lilToonLiteCutout";

        /// <summary>lilToon Lite Cutout Outline</summary>
        /// <remarks>ltsl_cutout_o.shader</remarks>
        public const string Lil_LilToonLiteCutoutOutline = "Hidden/lilToonLiteCutoutOutline";

        /// <summary>lilToon Lite Transparent</summary>
        /// <remarks>ltsl_trans.shader</remarks>
        public const string Lil_LilToonLiteTransparent = "Hidden/lilToonLiteTransparent";

        /// <summary>lilToon Lite Transparent Outline</summary>
        /// <remarks>ltsl_trans_o.shader</remarks>
        public const string Lil_LilToonLiteTransparentOutline = "Hidden/lilToonLiteTransparentOutline";

        /// <summary>lilToon Lite OnePass Transparent</summary>
        /// <remarks>ltsl_onetrans.shader</remarks>
        public const string Lil_LilToonLiteOnePassTransparent = "Hidden/lilToonLiteOnePassTransparent";

        /// <summary>lilToon Lite OnePass Transparent Outline</summary>
        /// <remarks>ltsl_onetrans_o.shader</remarks>
        public const string Lil_LilToonLiteOnePassTransparentOutline = "Hidden/lilToonLiteOnePassTransparentOutline";

        /// <summary>lilToon Lite TwoPass Transparent</summary>
        /// <remarks>ltsl_twotrans.shader</remarks>
        public const string Lil_LilToonLiteTwoPassTransparent = "Hidden/lilToonLiteTwoPassTransparent";

        /// <summary>lilToon Lite TwoPass Transparent Outline</summary>
        /// <remarks>ltsl_twotrans_o.shader</remarks>
        public const string Lil_LilToonLiteTwoPassTransparentOutline = "Hidden/lilToonLiteTwoPassTransparentOutline";

        /// <summary>lilToon Lite Overlay</summary>
        /// <remarks>ltsl_overlay.shader</remarks>
        public const string Lil_LilToonLiteOverlay = "_lil/[Optional] lilToonLiteOverlay";

        /// <summary>lilToon Lite Overlay OnePass</summary>
        /// <remarks>ltsl_overlay_one.shader</remarks>
        public const string Lil_LilToonLiteOverlayOnePass = "_lil/[Optional] lilToonLiteOverlayOnePass";

        /// <summary>lilToon Multi</summary>
        /// <remarks>ltsmulti.shader</remarks>
        public const string Lil_LilToonMulti = "_lil/lilToonMulti";

        /// <summary>lilToon Multi Outline</summary>
        /// <remarks>ltsmulti_o.shader</remarks>
        public const string Lil_LilToonMultiOutline = "Hidden/lilToonMultiOutline";

        /// <summary>lilToon Multi Refraction</summary>
        /// <remarks>ltsmulti_ref.shader</remarks>
        public const string Lil_LilToonMultiRefraction = "Hidden/lilToonMultiRefraction";

        /// <summary>lilToon Multi Fur</summary>
        /// <remarks>ltsmulti_fur.shader</remarks>
        public const string Lil_LilToonMultiFur = "Hidden/lilToonMultiFur";

        /// <summary>lilToon Multi Gem</summary>
        /// <remarks>ltsmulti_gem.shader</remarks>
        public const string Lil_LilToonMultiGem = "Hidden/lilToonMultiGem";

        /// <summary>lilToon FakeShadow</summary>
        /// <remarks>lts_fakeshadow.shader</remarks>
        public const string Lil_LilToonFakeShadow = "_lil/[Optional] lilToonFakeShadow";

        /// <summary>lilToon Other Baker</summary>
        /// <remarks>ltspass_baker.shader</remarks>
        public const string Lil_LilToonOtherBaker = "Hidden/ltsother_baker";

        /// <summary>lilToon Pass Dummy</summary>
        /// <remarks>ltspass_dummy.shader</remarks>
        public const string Lil_LilToonPassDummy = "Hidden/ltspass_dummy";

        /// <summary>lilToon Pass Opaque</summary>
        /// <remarks>ltspass_opaque.shader</remarks>
        public const string Lil_LilToonPassOpaque = "Hidden/ltspass_opaque";

        /// <summary>lilToon Pass Cutout</summary>
        /// <remarks>ltspass_cutout.shader</remarks>
        public const string Lil_LilToonPassCutout = "Hidden/ltspass_cutout";

        /// <summary>lilToon Pass Transparent</summary>
        /// <remarks>ltspass_transparent.shader</remarks>
        public const string Lil_LilToonPassTransparent = "Hidden/ltspass_transparent";

        /// <summary>lilToon Pass Tessellation Opaque</summary>
        /// <remarks>ltspass_tess_opaque.shader</remarks>
        public const string Lil_LilToonPassTessOpaque = "Hidden/ltspass_tess_opaque";

        /// <summary>lilToon Pass Tessellation Cutout</summary>
        /// <remarks>ltspass_tess_cutout.shader</remarks>
        public const string Lil_LilToonPassTessCutout = "Hidden/ltspass_tess_cutout";

        /// <summary>lilToon Pass Tessellation Transparent</summary>
        /// <remarks>ltspass_tess_transparent.shader</remarks>
        public const string Lil_LilToonPassTessTransparent = "Hidden/ltspass_tess_transparent";

        /// <summary>lilToon Pass Lite Opaque</summary>
        /// <remarks>ltspass_lite_opaque.shader</remarks>
        public const string Lil_LilToonPassLiteOpaque = "Hidden/ltspass_lite_opaque";

        /// <summary>lilToon Pass Lite Cutout</summary>
        /// <remarks>ltspass_lite_cutout.shader</remarks>
        public const string Lil_LilToonPassLiteCutout = "Hidden/ltspass_lite_cutout";

        /// <summary>lilToon Pass Lite Transparent</summary>
        /// <remarks>ltspass_lite_transparent.shader</remarks>
        public const string Lil_LilToonPassLiteTransparent = "Hidden/ltspass_lite_transparent";

        #endregion
    }
}
