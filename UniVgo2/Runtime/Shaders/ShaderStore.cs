// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : ShaderStore
// ----------------------------------------------------------------------
namespace UniVgo2
{
    using NewtonVgo;
    using UnityEngine;

    /// <summary>
    /// Shader Store
    /// </summary>
    public class ShaderStore : IShaderStore
    {
        #region Fields (BRP)

        /// <summary>Standard</summary>
        protected Shader _Standard;

        /// <summary>Particles/Standard Surface</summary>
        protected Shader _ParticlesStandardSurface;

        /// <summary>Particles/Standard Unlit</summary>
        protected Shader _ParticlesStandardUnlit;

        /// <summary>Skybox/6 Sided</summary>
        protected Shader _Skybox6Sided;

        /// <summary>Skybox/Cubemap</summary>
        protected Shader _SkyboxCubemap;

        /// <summary>Skybox/Panoramic</summary>
        protected Shader _SkyboxPanoramic;

        /// <summary>Skybox/Procedural</summary>
        protected Shader _SkyboxProcedural;

        /// <summary>Unlit/Texture</summary>
        protected Shader _UnlitTexture;

        /// <summary>Unlit/Color</summary>
        protected Shader _UnlitColor;

        /// <summary>Unlit/Transparent</summary>
        protected Shader _UnlitTransparent;

        /// <summary>Unlit/Transparent Cutout</summary>
        protected Shader _UnlitTransparentCutout;

        /// <summary>UniGLTF/UniUnlit</summary>
        protected Shader _UniGLTFUniUnlit;

        /// <summary>UniGLTF/StandardVColor</summary>
        protected Shader _UniGLTFStandardVColor;

        /// <summary>VRM/UnlitTexture</summary>
        protected Shader _VrmUnlitTexture;

        /// <summary>VRM/UnlitTransparent</summary>
        protected Shader _VrmUnlitTransparent;

        /// <summary>VRM/UnlitCutout</summary>
        protected Shader _VrmUnlitCutout;

        /// <summary>VRM/UnlitTransparentZWrite</summary>
        protected Shader _VrmUnlitTransparentZWrite;

        /// <summary>VRM/MToon</summary>
        protected Shader _VrmMtoon;

        #endregion

        #region Fields (URP)

        /// <summary>Universal Render Pipeline/Lit</summary>
        protected Shader _UrpLit;

        /// <summary>Universal Render Pipeline/Simple Lit</summary>
        protected Shader _UrpSimpleLit;

        /// <summary>Universal Render Pipeline/Unlit</summary>
        protected Shader _UrpUnlit;

        #endregion

        #region Fields (HDRP)

        /// <summary>HDRP/Eye</summary>
        protected Shader _HDRPEye;

        /// <summary>HDRP/Hair</summary>
        protected Shader _HDRPHair;

        /// <summary>HDRP/Lit</summary>
        protected Shader _HDRPLit;

        #endregion

        #region Fields (lilToon)

        /// <summary>lilToon</summary>
        /// <remarks>lilToon</remarks>
        protected Shader _Lil_LilToon;

        /// <summary>lilToon Outline</summary>
        /// <remarks>Hidden/lilToonOutline</remarks>
        protected Shader _Lil_LilToonOutline;

        /// <summary>lilToon Outline only</summary>
        /// <remarks>_lil/[Optional] lilToonOutlineOnly</remarks>
        protected Shader _Lil_LilToonOutlineOnly;

        /// <summary>lilToon Outline only Cutout</summary>
        /// <remarks>_lil/[Optional] lilToonOutlineOnlyCutout</remarks>
        protected Shader _Lil_LilToonOutlineOnlyCutout;

        /// <summary>lilToon Outline only Transparent</summary>
        /// <remarks>_lil/[Optional] lilToonOutlineOnlyTransparent</remarks>
        protected Shader _Lil_LilToonOutlineOnlyTransparent;

        /// <summary>lilToon Cutout</summary>
        /// <remarks>Hidden/lilToonCutout</remarks>
        protected Shader _Lil_LilToonCutout;

        /// <summary>lilToon Cutout Outline</summary>
        /// <remarks>Hidden/lilToonCutoutOutline</remarks>
        protected Shader _Lil_LilToonCutoutOutline;

        /// <summary>lilToon Transparent</summary>
        /// <remarks>Hidden/lilToonTransparent</remarks>
        protected Shader _Lil_LilToonTransparent;

        /// <summary>lilToon Transparent Outline</summary>
        /// <remarks>Hidden/lilToonTransparentOutline</remarks>
        protected Shader _Lil_LilToonTransparentOutline;

        /// <summary>lilToon OnePass Transparent</summary>
        /// <remarks>Hidden/lilToonOnePassTransparent</remarks>
        protected Shader _Lil_LilToonOnePassTransparent;

        /// <summary>lilToon OnePass Transparent Outline</summary>
        /// <remarks>Hidden/lilToonOnePassTransparentOutline</remarks>
        protected Shader _Lil_LilToonOnePassTransparentOutline;

        /// <summary>lilToon TwoPass Transparent</summary>
        /// <remarks>Hidden/lilToonTwoPassTransparent</remarks>
        protected Shader _Lil_LilToonTwoPassTransparent;

        /// <summary>lilToon TwoPass Transparent Outline</summary>
        /// <remarks>Hidden/lilToonTwoPassTransparentOutline</remarks>
        protected Shader _Lil_LilToonTwoPassTransparentOutline;

        /// <summary>lilToon Overlay</summary>
        /// <remarks>_lil/[Optional] lilToonOverlay</remarks>
        protected Shader _Lil_LilToonOverlay;

        /// <summary>lilToon Overlay OnePass</summary>
        /// <remarks>_lil/[Optional] lilToonOverlayOnePass</remarks>
        protected Shader _Lil_LilToonOverlayOnePass;

        /// <summary>lilToon Refraction</summary>
        /// <remarks>Hidden/lilToonRefraction</remarks>
        protected Shader _Lil_LilToonRefraction;

        /// <summary>lilToon Refraction Blur</summary>
        /// <remarks>Hidden/lilToonRefractionBlur</remarks>
        protected Shader _Lil_LilToonRefractionBlur;

        /// <summary>lilToon Fur</summary>
        /// <remarks>Hidden/lilToonFur</remarks>
        protected Shader _Lil_LilToonFur;

        /// <summary>lilToon Fur Cutout</summary>
        /// <remarks>Hidden/lilToonFurCutout</remarks>
        protected Shader _Lil_LilToonFurCutout;

        /// <summary>lilToon Fur TwoPass</summary>
        /// <remarks>Hidden/lilToonFurTwoPass</remarks>
        protected Shader _Lil_LilToonFurTwoPass;

        /// <summary>lilToon Fur only</summary>
        /// <remarks>_lil/[Optional] lilToonFurOnly</remarks>
        protected Shader _Lil_LilToonFurOnly;

        /// <summary>lilToon Fur only Cutout</summary>
        /// <remarks>_lil/[Optional] lilToonFurOnlyCutout</remarks>
        protected Shader _Lil_LilToonFurOnlyCutout;

        /// <summary>lilToon Fur only TwoPass</summary>
        /// <remarks>_lil/[Optional] lilToonFurOnlyTwoPass</remarks>
        protected Shader _Lil_LilToonFurOnlyTwoPass;

        /// <summary>lilToon Gem</summary>
        /// <remarks>Hidden/lilToonGem</remarks>
        protected Shader _Lil_LilToonGem;

        /// <summary>lilToon Tessellation</summary>
        /// <remarks>Hidden/lilToonTessellation</remarks>
        protected Shader _Lil_LilToonTessellation;

        /// <summary>lilToon Tessellation Outline</summary>
        /// <remarks>Hidden/lilToonTessellationOutline</remarks>
        protected Shader _Lil_LilToonTessellationOutline;

        /// <summary>lilToon Tessellation Cutout</summary>
        /// <remarks>Hidden/lilToonTessellationCutout</remarks>
        protected Shader _Lil_LilToonTessellationCutout;

        /// <summary>lilToon Tessellation Cutout Outline</summary>
        /// <remarks>Hidden/lilToonTessellationCutoutOutline</remarks>
        protected Shader _Lil_LilToonTessellationCutoutOutline;

        /// <summary>lilToon Tessellation Transparent</summary>
        /// <remarks>Hidden/lilToonTessellationTransparent</remarks>
        protected Shader _Lil_LilToonTessellationTransparent;

        /// <summary>lilToon Tessellation Transparent Outline</summary>
        /// <remarks>Hidden/lilToonTessellationTransparentOutline</remarks>
        protected Shader _Lil_LilToonTessellationTransparentOutline;

        /// <summary>lilToon Tessellation OnePass Transparent</summary>
        /// <remarks>Hidden/lilToonTessellationOnePassTransparent</remarks>
        protected Shader _Lil_LilToonTessellationOnePassTransparent;

        /// <summary>lilToon Tessellation OnePass Transparent Outline</summary>
        /// <remarks>Hidden/lilToonTessellationOnePassTransparentOutline</remarks>
        protected Shader _Lil_LilToonTessellationOnePassTransparentOutline;

        /// <summary>lilToon Tessellation TwoPass Transparent</summary>
        /// <remarks>Hidden/lilToonTessellationTwoPassTransparent</remarks>
        protected Shader _Lil_LilToonTessellationTwoPassTransparent;

        /// <summary>lilToon Tessellation TwoPass Transparent Outline</summary>
        /// <remarks>Hidden/lilToonTessellationTwoPassTransparentOutline</remarks>
        protected Shader _Lil_LilToonTessellationTwoPassTransparentOutline;

        /// <summary>lilToon Lite</summary>
        /// <remarks>Hidden/lilToonLite</remarks>
        protected Shader _Lil_LilToonLite;

        /// <summary>lilToon Lite Outline</summary>
        /// <remarks>Hidden/lilToonLiteOutline</remarks>
        protected Shader _Lil_LilToonLiteOutline;

        /// <summary>lilToon Lite Cutout</summary>
        /// <remarks>Hidden/lilToonLiteCutout</remarks>
        protected Shader _Lil_LilToonLiteCutout;

        /// <summary>lilToon Lite Cutout Outline</summary>
        /// <remarks>Hidden/lilToonLiteCutoutOutline</remarks>
        protected Shader _Lil_LilToonLiteCutoutOutline;

        /// <summary>lilToon Lite Transparent</summary>
        /// <remarks>Hidden/lilToonLiteTransparent</remarks>
        protected Shader _Lil_LilToonLiteTransparent;

        /// <summary>lilToon Lite Transparent Outline</summary>
        /// <remarks>Hidden/lilToonLiteTransparentOutline</remarks>
        protected Shader _Lil_LilToonLiteTransparentOutline;

        /// <summary>lilToon Lite OnePass Transparent</summary>
        /// <remarks>Hidden/lilToonLiteOnePassTransparent</remarks>
        protected Shader _Lil_LilToonLiteOnePassTransparent;

        /// <summary>lilToon Lite OnePass Transparent Outline</summary>
        /// <remarks>Hidden/lilToonLiteOnePassTransparentOutline</remarks>
        protected Shader _Lil_LilToonLiteOnePassTransparentOutline;

        /// <summary>lilToon Lite TwoPass Transparent</summary>
        /// <remarks>Hidden/lilToonLiteTransparent</remarks>
        protected Shader _Lil_LilToonLiteTwoPassTransparent;

        /// <summary>lilToon Lite TwoPass Transparent Outline</summary>
        /// <remarks>Hidden/lilToonLiteTwoPassTransparentOutline</remarks>
        protected Shader _Lil_LilToonLiteTwoPassTransparentOutline;

        /// <summary>lilToon Lite Overlay</summary>
        /// <remarks>_lil/[Optional] lilToonLiteOverlay</remarks>
        protected Shader _Lil_LilToonLiteOverlay;

        /// <summary>lilToon Lite Overlay OnePass</summary>
        /// <remarks>_lil/[Optional] lilToonLiteOverlayOnePass</remarks>
        protected Shader _Lil_LilToonLiteOverlayOnePass;

        /// <summary>lilToon Multi</summary>
        /// <remarks>_lil/lilToonMulti</remarks>
        protected Shader _Lil_LilToonMulti;

        /// <summary>lilToon Multi Outline</summary>
        /// <remarks>Hidden/lilToonMultiOutline</remarks>
        protected Shader _Lil_LilToonMultiOutline;

        /// <summary>lilToon Multi Refraction</summary>
        /// <remarks>Hidden/lilToonMultiRefraction</remarks>
        protected Shader _Lil_LilToonMultiRefraction;

        /// <summary>lilToon Multi Fur</summary>
        /// <remarks>Hidden/lilToonMultiFur</remarks>
        protected Shader _Lil_LilToonMultiFur;

        /// <summary>lilToon Multi Gem</summary>
        /// <remarks>Hidden/lilToonMultiGem</remarks>
        protected Shader _Lil_LilToonMultiGem;

        /// <summary>lilToon FakeShadow</summary>
        /// <remarks>_lil/[Optional] lilToonFakeShadow</remarks>
        protected Shader _Lil_LilToonFakeShadow;

        /// <summary>lilToon Other Baker</summary>
        /// <remarks>Hidden/ltsother_baker</remarks>
        protected Shader _Lil_LilToonOtherBaker;

        /// <summary>lilToon Pass Dummy</summary>
        /// <remarks>Hidden/ltspass_dummy</remarks>
        protected Shader _Lil_LilToonPassDummy;

        /// <summary>lilToon Pass Opaque</summary>
        /// <remarks>Hidden/ltspass_opaque</remarks>
        protected Shader _Lil_LilToonPassOpaque;

        /// <summary>lilToon Pass Cutout</summary>
        /// <remarks>Hidden/ltspass_cutout</remarks>
        protected Shader _Lil_LilToonPassCutout;

        /// <summary>lilToon Pass Transparent</summary>
        /// <remarks>Hidden/ltspass_transparent</remarks>
        protected Shader _Lil_LilToonPassTransparent;

        /// <summary>lilToon Pass Tessellation Opaque</summary>
        /// <remarks>Hidden/ltspass_tess_opaque</remarks>
        protected Shader _Lil_LilToonPassTessOpaque;

        /// <summary>lilToon Pass Tessellation Cutout</summary>
        /// <remarks>Hidden/ltspass_tess_cutout</remarks>
        protected Shader _Lil_LilToonPassTessCutout;

        /// <summary>lilToon Pass Tessellation Transparent</summary>
        /// <remarks>Hidden/ltspass_tess_transparent</remarks>
        protected Shader _Lil_LilToonPassTessTransparent;

        /// <summary>lilToon Pass Lite Opaque</summary>
        /// <remarks>Hidden/ltspass_lite_opaque</remarks>
        protected Shader _Lil_LilToonPassLiteOpaque;

        /// <summary>lilToon Pass Lite Cutout</summary>
        /// <remarks>Hidden/ltspass_lite_cutout</remarks>
        protected Shader _Lil_LilToonPassLiteCutout;

        /// <summary>lilToon Pass Lite Transparent</summary>
        /// <remarks>Hidden/ltspass_lite_transparent</remarks>
        protected Shader _Lil_LilToonPassLiteTransparent;

        #endregion

        #region Properties (BRP)

        /// <summary>Standard</summary>
        protected Shader Standard
        {
            get
            {
                if (_Standard == null)
                {
                    _Standard = Shader.Find(ShaderName.Standard);
                }
                return _Standard;
            }
        }
        /// <summary>Particles/Standard Surface</summary>
        protected Shader ParticlesStandardSurface
        {
            get
            {
                if (_ParticlesStandardSurface == null)
                {
                    _ParticlesStandardSurface = Shader.Find(ShaderName.Particles_Standard_Surface);
                }
                return _ParticlesStandardSurface;
            }
        }

        /// <summary>Particles/Standard Unlit</summary>
        protected Shader ParticlesStandardUnlit
        {
            get
            {
                if (_ParticlesStandardUnlit == null)
                {
                    _ParticlesStandardUnlit = Shader.Find(ShaderName.Particles_Standard_Unlit);
                }
                return _ParticlesStandardUnlit;
            }
        }

        /// <summary>Skybox/6 Sided</summary>
        protected Shader Skybox6Sided
        {
            get
            {
                if (_Skybox6Sided == null)
                {
                    _Skybox6Sided = Shader.Find(ShaderName.Skybox_6_Sided);
                }
                return _Skybox6Sided;
            }
        }

        /// <summary>Skybox/Cubemap</summary>
        protected Shader SkyboxCubemap
        {
            get
            {
                if (_SkyboxCubemap == null)
                {
                    _SkyboxCubemap = Shader.Find(ShaderName.Skybox_Cubemap);
                }
                return _SkyboxCubemap;
            }
        }

        /// <summary>Skybox/Panoramic</summary>
        protected Shader SkyboxPanoramic
        {
            get
            {
                if (_SkyboxPanoramic == null)
                {
                    _SkyboxPanoramic = Shader.Find(ShaderName.Skybox_Panoramic);
                }
                return _SkyboxPanoramic;
            }
        }

        /// <summary>Skybox/Procedural</summary>
        protected Shader SkyboxProcedural
        {
            get
            {
                if (_SkyboxProcedural == null)
                {
                    _SkyboxProcedural = Shader.Find(ShaderName.Skybox_Procedural);
                }
                return _SkyboxProcedural;
            }
        }

        /// <summary>Unlit/Texture</summary>
        protected Shader UnlitTexture
        {
            get
            {
                if (_UnlitTexture == null)
                {
                    _UnlitTexture = Shader.Find(ShaderName.Unlit_Texture);
                }
                return _UnlitTexture;
            }
        }

        /// <summary>Unlit/Color</summary>
        protected Shader UnlitColor
        {
            get
            {
                if (_UnlitColor == null)
                {
                    _UnlitColor = Shader.Find(ShaderName.Unlit_Color);
                }
                return _UnlitColor;
            }
        }

        /// <summary>Unlit/Transparent</summary>
        protected Shader UnlitTransparent
        {
            get
            {
                if (_UnlitTransparent == null)
                {
                    _UnlitTransparent = Shader.Find(ShaderName.Unlit_Transparent);
                }
                return _UnlitTransparent;
            }
        }

        /// <summary>Unlit/Transparent Cutout</summary>
        protected Shader UnlitTransparentCutout
        {
            get
            {
                if (_UnlitTransparentCutout == null)
                {
                    _UnlitTransparentCutout = Shader.Find(ShaderName.Unlit_Transparent_Cutout);
                }
                return _UnlitTransparentCutout;
            }
        }

        /// <summary>UniGLTF/UniUnlit</summary>
        protected Shader UniGLTFUniUnlit
        {
            get
            {
                if (_UniGLTFUniUnlit == null)
                {
                    _UniGLTFUniUnlit = Shader.Find(ShaderName.UniGLTF_UniUnlit);
                }
                return _UniGLTFUniUnlit;
            }
        }

        /// <summary>UniGLTF/StandardVColor</summary>
        protected Shader UniGLTFStandardVColor
        {
            get
            {
                if (_UniGLTFStandardVColor == null)
                {
                    _UniGLTFStandardVColor = Shader.Find(ShaderName.UniGLTF_StandardVColor);
                }
                return _UniGLTFStandardVColor;
            }
        }

        /// <summary>VRM/UnlitTexture</summary>
        protected Shader VrmUnlitTexture
        {
            get
            {
                if (_VrmUnlitTexture == null)
                {
                    _VrmUnlitTexture = Shader.Find(ShaderName.VRM_UnlitTexture);
                }
                return _VrmUnlitTexture;
            }
        }

        /// <summary>VRM/UnlitTransparent</summary>
        protected Shader VrmUnlitTransparent
        {
            get
            {
                if (_VrmUnlitTransparent == null)
                {
                    _VrmUnlitTransparent = Shader.Find(ShaderName.VRM_UnlitTransparent);
                }
                return _VrmUnlitTransparent;
            }
        }

        /// <summary>VRM/UnlitCutout</summary>
        protected Shader VrmUnlitCutout
        {
            get
            {
                if (_VrmUnlitCutout == null)
                {
                    _VrmUnlitCutout = Shader.Find(ShaderName.VRM_UnlitCutout);
                }
                return _VrmUnlitCutout;
            }
        }

        /// <summary>VRM/UnlitTransparentZWrite</summary>
        protected Shader VrmUnlitTransparentZWrite
        {
            get
            {
                if (_VrmUnlitTransparentZWrite == null)
                {
                    _VrmUnlitTransparentZWrite = Shader.Find(ShaderName.VRM_UnlitTransparentZWrite);
                }
                return _VrmUnlitTransparentZWrite;
            }
        }

        /// <summary>VRM/MToon</summary>
        protected Shader VrmMtoon
        {
            get
            {
                if (_VrmMtoon == null)
                {
                    _VrmMtoon = Shader.Find(ShaderName.VRM_MToon);
                }
                return _VrmMtoon;
            }
        }

        #endregion

        #region Properties (URP)

        /// <summary>Universal Render Pipeline/Lit</summary>
        protected Shader UrpLit
        {
            get
            {
                if (_UrpLit == null)
                {
                    _UrpLit = Shader.Find(ShaderName.URP_Lit);
                }
                return _UrpLit;
            }
        }

        /// <summary>Universal Render Pipeline/Simple Lit</summary>
        protected Shader UrpSimpleLit
        {
            get
            {
                if (_UrpSimpleLit == null)
                {
                    _UrpSimpleLit = Shader.Find(ShaderName.URP_SimpleLit);
                }
                return _UrpSimpleLit;
            }
        }

        /// <summary>Universal Render Pipeline/Unlit</summary>
        protected Shader UrpUnlit
        {
            get
            {
                if (_UrpUnlit == null)
                {
                    _UrpUnlit = Shader.Find(ShaderName.URP_Unlit);
                }
                return _UrpUnlit;
            }
        }

        #endregion

        #region Properties (HDRP)

        /// <summary>HDRP/Eye</summary>
        protected Shader HDRPEye
        {
            get
            {
                if (_HDRPEye == null)
                {
                    _HDRPEye = Shader.Find(ShaderName.HDRP_Eye);
                }
                return _HDRPEye;
            }
        }

        /// <summary>HDRP/Hair</summary>
        protected Shader HDRPHair
        {
            get
            {
                if (_HDRPHair == null)
                {
                    _HDRPHair = Shader.Find(ShaderName.HDRP_Hair);
                }
                return _HDRPHair;
            }
        }

        /// <summary>HDRP/Lit</summary>
        protected Shader HDRPLit
        {
            get
            {
                if (_HDRPLit == null)
                {
                    _HDRPLit = Shader.Find(ShaderName.HDRP_Lit);
                }
                return _HDRPLit;
            }
        }

        #endregion

        #region Properties (lilToon)

        /// <summary>lilToon</summary>
        /// <remarks>lilToon</remarks>
        protected Shader Lil_LilToon
        {
            get
            {
                if (_Lil_LilToon == null)
                {
                    _Lil_LilToon = Shader.Find(ShaderName.Lil_LilToon);
                }
                return _Lil_LilToon;
            }
        }

        /// <summary>lilToon Outline</summary>
        /// <remarks>Hidden/lilToonOutline</remarks>
        protected Shader Lil_LilToonOutline
        {
            get
            {
                if (_Lil_LilToonOutline == null)
                {
                    _Lil_LilToonOutline = Shader.Find(ShaderName.Lil_LilToonOutline);
                }
                return _Lil_LilToonOutline;
            }
        }

        /// <summary>lilToon Outline only</summary>
        /// <remarks>_lil/[Optional] lilToonOutlineOnly</remarks>
        protected Shader Lil_LilToonOutlineOnly
        {
            get
            {
                if (_Lil_LilToonOutlineOnly == null)
                {
                    _Lil_LilToonOutlineOnly = Shader.Find(ShaderName.Lil_LilToonOutlineOnly);
                }
                return _Lil_LilToonOutlineOnly;
            }
        }

        /// <summary>lilToon Outline only Cutout</summary>
        /// <remarks>_lil/[Optional] lilToonOutlineOnlyCutout</remarks>
        protected Shader Lil_LilToonOutlineOnlyCutout
        {
            get
            {
                if (_Lil_LilToonOutlineOnlyCutout == null)
                {
                    _Lil_LilToonOutlineOnlyCutout = Shader.Find(ShaderName.Lil_LilToonOutlineOnlyCutout);
                }
                return _Lil_LilToonOutlineOnlyCutout;
            }
        }

        /// <summary>lilToon Outline only Transparent</summary>
        /// <remarks>_lil/[Optional] lilToonOutlineOnlyTransparent</remarks>
        protected Shader Lil_LilToonOutlineOnlyTransparent
        {
            get
            {
                if (_Lil_LilToonOutlineOnlyTransparent == null)
                {
                    _Lil_LilToonOutlineOnlyTransparent = Shader.Find(ShaderName.Lil_LilToonOutlineOnlyTransparent);
                }
                return _Lil_LilToonOutlineOnlyTransparent;
            }
        }

        /// <summary>lilToon Cutout</summary>
        /// <remarks>Hidden/lilToonCutout</remarks>
        protected Shader Lil_LilToonCutout
        {
            get
            {
                if (_Lil_LilToonCutout == null)
                {
                    _Lil_LilToonCutout = Shader.Find(ShaderName.Lil_LilToonCutout);
                }
                return _Lil_LilToonCutout;
            }
        }

        /// <summary>lilToon Cutout Outline</summary>
        /// <remarks>Hidden/lilToonCutoutOutline</remarks>
        protected Shader Lil_LilToonCutoutOutline
        {
            get
            {
                if (_Lil_LilToonCutoutOutline == null)
                {
                    _Lil_LilToonCutoutOutline = Shader.Find(ShaderName.Lil_LilToonCutoutOutline);
                }
                return _Lil_LilToonCutoutOutline;
            }
        }

        /// <summary>lilToon Transparent</summary>
        /// <remarks>Hidden/lilToonTransparent</remarks>
        protected Shader Lil_LilToonTransparent
        {
            get
            {
                if (_Lil_LilToonTransparent == null)
                {
                    _Lil_LilToonTransparent = Shader.Find(ShaderName.Lil_LilToonTransparent);
                }
                return _Lil_LilToonTransparent;
            }
        }

        /// <summary>lilToon Transparent Outline</summary>
        /// <remarks>Hidden/lilToonTransparentOutline</remarks>
        protected Shader Lil_LilToonTransparentOutline
        {
            get
            {
                if (_Lil_LilToonTransparentOutline == null)
                {
                    _Lil_LilToonTransparentOutline = Shader.Find(ShaderName.Lil_LilToonTransparentOutline);
                }
                return _Lil_LilToonTransparentOutline;
            }
        }

        /// <summary>lilToon OnePass Transparent</summary>
        /// <remarks>Hidden/lilToonOnePassTransparent</remarks>
        protected Shader Lil_LilToonOnePassTransparent
        {
            get
            {
                if (_Lil_LilToonOnePassTransparent == null)
                {
                    _Lil_LilToonOnePassTransparent = Shader.Find(ShaderName.Lil_LilToonOnePassTransparent);
                }
                return _Lil_LilToonOnePassTransparent;
            }
        }

        /// <summary>lilToon OnePass Transparent Outline</summary>
        /// <remarks>Hidden/lilToonOnePassTransparentOutline</remarks>
        protected Shader Lil_LilToonOnePassTransparentOutline
        {
            get
            {
                if (_Lil_LilToonOnePassTransparentOutline == null)
                {
                    _Lil_LilToonOnePassTransparentOutline = Shader.Find(ShaderName.Lil_LilToonOnePassTransparentOutline);
                }
                return _Lil_LilToonOnePassTransparentOutline;
            }
        }

        /// <summary>lilToon TwoPass Transparent</summary>
        /// <remarks>Hidden/lilToonTwoPassTransparent</remarks>
        protected Shader Lil_LilToonTwoPassTransparent
        {
            get
            {
                if (_Lil_LilToonTwoPassTransparent == null)
                {
                    _Lil_LilToonTwoPassTransparent = Shader.Find(ShaderName.Lil_LilToonTwoPassTransparent);
                }
                return _Lil_LilToonTwoPassTransparent;
            }
        }

        /// <summary>lilToon TwoPass Transparent Outline</summary>
        /// <remarks>Hidden/lilToonTwoPassTransparentOutline</remarks>
        protected Shader Lil_LilToonTwoPassTransparentOutline
        {
            get
            {
                if (_Lil_LilToonTwoPassTransparentOutline == null)
                {
                    _Lil_LilToonTwoPassTransparentOutline = Shader.Find(ShaderName.Lil_LilToonTwoPassTransparentOutline);
                }
                return _Lil_LilToonTwoPassTransparentOutline;
            }
        }

        /// <summary>lilToon Overlay</summary>
        /// <remarks>_lil/[Optional] lilToonOverlay</remarks>
        protected Shader Lil_LilToonOverlay
        {
            get
            {
                if (_Lil_LilToonOverlay == null)
                {
                    _Lil_LilToonOverlay = Shader.Find(ShaderName.Lil_LilToonOverlay);
                }
                return _Lil_LilToonOverlay;
            }
        }

        /// <summary>lilToon Overlay OnePass</summary>
        /// <remarks>_lil/[Optional] lilToonOverlayOnePass</remarks>
        protected Shader Lil_LilToonOverlayOnePass
        {
            get
            {
                if (_Lil_LilToonOverlayOnePass == null)
                {
                    _Lil_LilToonOverlayOnePass = Shader.Find(ShaderName.Lil_LilToonOverlayOnePass);
                }
                return _Lil_LilToonOverlayOnePass;
            }
        }

        /// <summary>lilToon Refraction</summary>
        /// <remarks>Hidden/lilToonRefraction</remarks>
        protected Shader Lil_LilToonRefraction
        {
            get
            {
                if (_Lil_LilToonRefraction == null)
                {
                    _Lil_LilToonRefraction = Shader.Find(ShaderName.Lil_LilToonRefraction);
                }
                return _Lil_LilToonRefraction;
            }
        }

        /// <summary>lilToon Refraction Blur</summary>
        /// <remarks>Hidden/lilToonRefractionBlur</remarks>
        protected Shader Lil_LilToonRefractionBlur
        {
            get
            {
                if (_Lil_LilToonRefractionBlur == null)
                {
                    _Lil_LilToonRefractionBlur = Shader.Find(ShaderName.Lil_LilToonRefractionBlur);
                }
                return _Lil_LilToonRefractionBlur;
            }
        }

        /// <summary>lilToon Fur</summary>
        /// <remarks>Hidden/lilToonFur</remarks>
        protected Shader Lil_LilToonFur
        {
            get
            {
                if (_Lil_LilToonFur == null)
                {
                    _Lil_LilToonFur = Shader.Find(ShaderName.Lil_LilToonFur);
                }
                return _Lil_LilToonFur;
            }
        }

        /// <summary>lilToon Fur Cutout</summary>
        /// <remarks>Hidden/lilToonFurCutout</remarks>
        protected Shader Lil_LilToonFurCutout
        {
            get
            {
                if (_Lil_LilToonFurCutout == null)
                {
                    _Lil_LilToonFurCutout = Shader.Find(ShaderName.Lil_LilToonFurCutout);
                }
                return _Lil_LilToonFurCutout;
            }
        }

        /// <summary>lilToon Fur TwoPass</summary>
        /// <remarks>Hidden/lilToonFurTwoPass</remarks>
        protected Shader Lil_LilToonFurTwoPass
        {
            get
            {
                if (_Lil_LilToonFurTwoPass == null)
                {
                    _Lil_LilToonFurTwoPass = Shader.Find(ShaderName.Lil_LilToonFurTwoPass);
                }
                return _Lil_LilToonFurTwoPass;
            }
        }

        /// <summary>lilToon Fur only</summary>
        /// <remarks>_lil/[Optional] lilToonFurOnly</remarks>
        protected Shader Lil_LilToonFurOnly
        {
            get
            {
                if (_Lil_LilToonFurOnly == null)
                {
                    _Lil_LilToonFurOnly = Shader.Find(ShaderName.Lil_LilToonFurOnly);
                }
                return _Lil_LilToonFurOnly;
            }
        }

        /// <summary>lilToon Fur only Cutout</summary>
        /// <remarks>_lil/[Optional] lilToonFurOnlyCutout</remarks>
        protected Shader Lil_LilToonFurOnlyCutout
        {
            get
            {
                if (_Lil_LilToonFurOnlyCutout == null)
                {
                    _Lil_LilToonFurOnlyCutout = Shader.Find(ShaderName.Lil_LilToonFurOnlyCutout);
                }
                return _Lil_LilToonFurOnlyCutout;
            }
        }

        /// <summary>lilToon Fur only TwoPass</summary>
        /// <remarks>_lil/[Optional] lilToonFurOnlyTwoPass</remarks>
        protected Shader Lil_LilToonFurOnlyTwoPass
        {
            get
            {
                if (_Lil_LilToonFurOnlyTwoPass == null)
                {
                    _Lil_LilToonFurOnlyTwoPass = Shader.Find(ShaderName.Lil_LilToonFurOnlyTwoPass);
                }
                return _Lil_LilToonFurOnlyTwoPass;
            }
        }

        /// <summary>lilToon Gem</summary>
        /// <remarks>Hidden/lilToonGem</remarks>
        protected Shader Lil_LilToonGem
        {
            get
            {
                if (_Lil_LilToonGem == null)
                {
                    _Lil_LilToonGem = Shader.Find(ShaderName.Lil_LilToonGem);
                }
                return _Lil_LilToonGem;
            }
        }

        /// <summary>lilToon Tessellation</summary>
        /// <remarks>Hidden/lilToonTessellation</remarks>
        protected Shader Lil_LilToonTessellation
        {
            get
            {
                if (_Lil_LilToonTessellation == null)
                {
                    _Lil_LilToonTessellation = Shader.Find(ShaderName.Lil_LilToonTessellation);
                }
                return _Lil_LilToonTessellation;
            }
        }

        /// <summary>lilToon Tessellation Outline</summary>
        /// <remarks>Hidden/lilToonTessellationOutline</remarks>
        protected Shader Lil_LilToonTessellationOutline
        {
            get
            {
                if (_Lil_LilToonTessellationOutline == null)
                {
                    _Lil_LilToonTessellationOutline = Shader.Find(ShaderName.Lil_LilToonTessellationOutline);
                }
                return _Lil_LilToonTessellationOutline;
            }
        }

        /// <summary>lilToon Tessellation Cutout</summary>
        /// <remarks>Hidden/lilToonTessellationCutout</remarks>
        protected Shader Lil_LilToonTessellationCutout
        {
            get
            {
                if (_Lil_LilToonTessellationCutout == null)
                {
                    _Lil_LilToonTessellationCutout = Shader.Find(ShaderName.Lil_LilToonTessellationCutout);
                }
                return _Lil_LilToonTessellationCutout;
            }
        }

        /// <summary>lilToon Tessellation Cutout Outline</summary>
        /// <remarks>Hidden/lilToonTessellationCutoutOutline</remarks>
        protected Shader Lil_LilToonTessellationCutoutOutline
        {
            get
            {
                if (_Lil_LilToonTessellationCutoutOutline == null)
                {
                    _Lil_LilToonTessellationCutoutOutline = Shader.Find(ShaderName.Lil_LilToonTessellationCutoutOutline);
                }
                return _Lil_LilToonTessellationCutoutOutline;
            }
        }

        /// <summary>lilToon Tessellation Transparent</summary>
        /// <remarks>Hidden/lilToonTessellationTransparent</remarks>
        protected Shader Lil_LilToonTessellationTransparent
        {
            get
            {
                if (_Lil_LilToonTessellationTransparent == null)
                {
                    _Lil_LilToonTessellationTransparent = Shader.Find(ShaderName.Lil_LilToonTessellationTransparent);
                }
                return _Lil_LilToonTessellationTransparent;
            }
        }

        /// <summary>lilToon Tessellation Transparent Outline</summary>
        /// <remarks>Hidden/lilToonTessellationTransparentOutline</remarks>
        protected Shader Lil_LilToonTessellationTransparentOutline
        {
            get
            {
                if (_Lil_LilToonTessellationTransparentOutline == null)
                {
                    _Lil_LilToonTessellationTransparentOutline = Shader.Find(ShaderName.Lil_LilToonTessellationTransparentOutline);
                }
                return _Lil_LilToonTessellationTransparentOutline;
            }
        }

        /// <summary>lilToon Tessellation OnePass Transparent</summary>
        /// <remarks>Hidden/lilToonTessellationOnePassTransparent</remarks>
        protected Shader Lil_LilToonTessellationOnePassTransparent
        {
            get
            {
                if (_Lil_LilToonTessellationOnePassTransparent == null)
                {
                    _Lil_LilToonTessellationOnePassTransparent = Shader.Find(ShaderName.Lil_LilToonTessellationOnePassTransparent);
                }
                return _Lil_LilToonTessellationOnePassTransparent;
            }
        }

        /// <summary>lilToon Tessellation OnePass Transparent Outline</summary>
        /// <remarks>Hidden/lilToonTessellationOnePassTransparentOutline</remarks>
        protected Shader Lil_LilToonTessellationOnePassTransparentOutline
        {
            get
            {
                if (_Lil_LilToonTessellationOnePassTransparentOutline == null)
                {
                    _Lil_LilToonTessellationOnePassTransparentOutline = Shader.Find(ShaderName.Lil_LilToonTessellationOnePassTransparentOutline);
                }
                return _Lil_LilToonTessellationOnePassTransparentOutline;
            }
        }

        /// <summary>lilToon Tessellation TwoPass Transparent</summary>
        /// <remarks>Hidden/lilToonTessellationTwoPassTransparent</remarks>
        protected Shader Lil_LilToonTessellationTwoPassTransparent
        {
            get
            {
                if (_Lil_LilToonTessellationTwoPassTransparent == null)
                {
                    _Lil_LilToonTessellationTwoPassTransparent = Shader.Find(ShaderName.Lil_LilToonTessellationTwoPassTransparent);
                }
                return _Lil_LilToonTessellationTwoPassTransparent;
            }
        }

        /// <summary>lilToon Tessellation TwoPass Transparent Outline</summary>
        /// <remarks>Hidden/lilToonTessellationTwoPassTransparentOutline</remarks>
        protected Shader Lil_LilToonTessellationTwoPassTransparentOutline
        {
            get
            {
                if (_Lil_LilToonTessellationTwoPassTransparentOutline == null)
                {
                    _Lil_LilToonTessellationTwoPassTransparentOutline = Shader.Find(ShaderName.Lil_LilToonTessellationTwoPassTransparentOutline);
                }
                return _Lil_LilToonTessellationTwoPassTransparentOutline;
            }
        }

        /// <summary>lilToon Lite</summary>
        /// <remarks>Hidden/lilToonLite</remarks>
        protected Shader Lil_LilToonLite
        {
            get
            {
                if (_Lil_LilToonLite == null)
                {
                    _Lil_LilToonLite = Shader.Find(ShaderName.Lil_LilToonLite);
                }
                return _Lil_LilToonLite;
            }
        }

        /// <summary>lilToon Lite Outline</summary>
        /// <remarks>Hidden/lilToonLiteOutline</remarks>
        protected Shader Lil_LilToonLiteOutline
        {
            get
            {
                if (_Lil_LilToonLiteOutline == null)
                {
                    _Lil_LilToonLiteOutline = Shader.Find(ShaderName.Lil_LilToonLiteOutline);
                }
                return _Lil_LilToonLiteOutline;
            }
        }

        /// <summary>lilToon Lite Cutout</summary>
        /// <remarks>Hidden/lilToonLiteCutout</remarks>
        protected Shader Lil_LilToonLiteCutout
        {
            get
            {
                if (_Lil_LilToonLiteCutout == null)
                {
                    _Lil_LilToonLiteCutout = Shader.Find(ShaderName.Lil_LilToonLiteCutout);
                }
                return _Lil_LilToonLiteCutout;
            }
        }

        /// <summary>lilToon Lite Cutout Outline</summary>
        /// <remarks>Hidden/lilToonLiteCutoutOutline</remarks>
        protected Shader Lil_LilToonLiteCutoutOutline
        {
            get
            {
                if (_Lil_LilToonLiteCutoutOutline == null)
                {
                    _Lil_LilToonLiteCutoutOutline = Shader.Find(ShaderName.Lil_LilToonLiteCutoutOutline);
                }
                return _Lil_LilToonLiteCutoutOutline;
            }
        }

        /// <summary>lilToon Lite Transparent</summary>
        /// <remarks>Hidden/lilToonLiteTransparent</remarks>
        protected Shader Lil_LilToonLiteTransparent
        {
            get
            {
                if (_Lil_LilToonLiteTransparent == null)
                {
                    _Lil_LilToonLiteTransparent = Shader.Find(ShaderName.Lil_LilToonLiteTransparent);
                }
                return _Lil_LilToonLiteTransparent;
            }
        }

        /// <summary>lilToon Lite Transparent Outline</summary>
        /// <remarks>Hidden/lilToonLiteTransparentOutline</remarks>
        protected Shader Lil_LilToonLiteTransparentOutline
        {
            get
            {
                if (_Lil_LilToonLiteTransparentOutline == null)
                {
                    _Lil_LilToonLiteTransparentOutline = Shader.Find(ShaderName.Lil_LilToonLiteTransparentOutline);
                }
                return _Lil_LilToonLiteTransparentOutline;
            }
        }

        /// <summary>lilToon Lite OnePass Transparent</summary>
        /// <remarks>Hidden/lilToonLiteOnePassTransparent</remarks>
        protected Shader Lil_LilToonLiteOnePassTransparent
        {
            get
            {
                if (_Lil_LilToonLiteOnePassTransparent == null)
                {
                    _Lil_LilToonLiteOnePassTransparent = Shader.Find(ShaderName.Lil_LilToonLiteOnePassTransparent);
                }
                return _Lil_LilToonLiteOnePassTransparent;
            }
        }

        /// <summary>lilToon Lite OnePass Transparent Outline</summary>
        /// <remarks>Hidden/lilToonLiteOnePassTransparentOutline</remarks>
        protected Shader Lil_LilToonLiteOnePassTransparentOutline
        {
            get
            {
                if (_Lil_LilToonLiteOnePassTransparentOutline == null)
                {
                    _Lil_LilToonLiteOnePassTransparentOutline = Shader.Find(ShaderName.Lil_LilToonLiteOnePassTransparentOutline);
                }
                return _Lil_LilToonLiteOnePassTransparentOutline;
            }
        }

        /// <summary>lilToon Lite TwoPass Transparent</summary>
        /// <remarks>Hidden/lilToonLiteTransparent</remarks>
        protected Shader Lil_LilToonLiteTwoPassTransparent
        {
            get
            {
                if (_Lil_LilToonLiteTwoPassTransparent == null)
                {
                    _Lil_LilToonLiteTwoPassTransparent = Shader.Find(ShaderName.Lil_LilToonLiteTwoPassTransparent);
                }
                return _Lil_LilToonLiteTwoPassTransparent;
            }
        }

        /// <summary>lilToon Lite TwoPass Transparent Outline</summary>
        /// <remarks>Hidden/lilToonLiteTwoPassTransparentOutline</remarks>
        protected Shader Lil_LilToonLiteTwoPassTransparentOutline
        {
            get
            {
                if (_Lil_LilToonLiteTwoPassTransparentOutline == null)
                {
                    _Lil_LilToonLiteTwoPassTransparentOutline = Shader.Find(ShaderName.Lil_LilToonLiteTwoPassTransparentOutline);
                }
                return _Lil_LilToonLiteTwoPassTransparentOutline;
            }
        }

        /// <summary>lilToon Lite Overlay</summary>
        /// <remarks>_lil/[Optional] lilToonLiteOverlay</remarks>
        protected Shader Lil_LilToonLiteOverlay
        {
            get
            {
                if (_Lil_LilToonLiteOverlay == null)
                {
                    _Lil_LilToonLiteOverlay = Shader.Find(ShaderName.Lil_LilToonLiteOverlay);
                }
                return _Lil_LilToonLiteOverlay;
            }
        }

        /// <summary>lilToon Lite Overlay OnePass</summary>
        /// <remarks>_lil/[Optional] lilToonLiteOverlayOnePass</remarks>
        protected Shader Lil_LilToonLiteOverlayOnePass
        {
            get
            {
                if (_Lil_LilToonLiteOverlayOnePass == null)
                {
                    _Lil_LilToonLiteOverlayOnePass = Shader.Find(ShaderName.Lil_LilToonLiteOverlayOnePass);
                }
                return _Lil_LilToonLiteOverlayOnePass;
            }
        }

        /// <summary>lilToon Multi</summary>
        /// <remarks>_lil/lilToonMulti</remarks>
        protected Shader Lil_LilToonMulti
        {
            get
            {
                if (_Lil_LilToonMulti == null)
                {
                    _Lil_LilToonMulti = Shader.Find(ShaderName.Lil_LilToonMulti);
                }
                return _Lil_LilToonMulti;
            }
        }

        /// <summary>lilToon Multi Outline</summary>
        /// <remarks>Hidden/lilToonMultiOutline</remarks>
        protected Shader Lil_LilToonMultiOutline
        {
            get
            {
                if (_Lil_LilToonMultiOutline == null)
                {
                    _Lil_LilToonMultiOutline = Shader.Find(ShaderName.Lil_LilToonMultiOutline);
                }
                return _Lil_LilToonMultiOutline;
            }
        }

        /// <summary>lilToon Multi Refraction</summary>
        /// <remarks>Hidden/lilToonMultiRefraction</remarks>
        protected Shader Lil_LilToonMultiRefraction
        {
            get
            {
                if (_Lil_LilToonMultiRefraction == null)
                {
                    _Lil_LilToonMultiRefraction = Shader.Find(ShaderName.Lil_LilToonMultiRefraction);
                }
                return _Lil_LilToonMultiRefraction;
            }
        }

        /// <summary>lilToon Multi Fur</summary>
        /// <remarks>Hidden/lilToonMultiFur</remarks>
        protected Shader Lil_LilToonMultiFur
        {
            get
            {
                if (_Lil_LilToonMultiFur == null)
                {
                    _Lil_LilToonMultiFur = Shader.Find(ShaderName.Lil_LilToonMultiFur);
                }
                return _Lil_LilToonMultiFur;
            }
        }

        /// <summary>lilToon Multi Gem</summary>
        /// <remarks>Hidden/lilToonMultiGem</remarks>
        protected Shader Lil_LilToonMultiGem
        {
            get
            {
                if (_Lil_LilToonMultiGem == null)
                {
                    _Lil_LilToonMultiGem = Shader.Find(ShaderName.Lil_LilToonMultiGem);
                }
                return _Lil_LilToonMultiGem;
            }
        }

        /// <summary>lilToon FakeShadow</summary>
        /// <remarks>_lil/[Optional] lilToonFakeShadow</remarks>
        protected Shader Lil_LilToonFakeShadow
        {
            get
            {
                if (_Lil_LilToonFakeShadow == null)
                {
                    _Lil_LilToonFakeShadow = Shader.Find(ShaderName.Lil_LilToonFakeShadow);
                }
                return _Lil_LilToonFakeShadow;
            }
        }

        /// <summary>lilToon Other Baker</summary>
        /// <remarks>Hidden/ltsother_baker</remarks>
        protected Shader Lil_LilToonOtherBaker
        {
            get
            {
                if (_Lil_LilToonOtherBaker == null)
                {
                    _Lil_LilToonOtherBaker = Shader.Find(ShaderName.Lil_LilToonOtherBaker);
                }
                return _Lil_LilToonOtherBaker;
            }
        }

        /// <summary>lilToon Pass Dummy</summary>
        /// <remarks>Hidden/ltspass_dummy</remarks>
        protected Shader Lil_LilToonPassDummy
        {
            get
            {
                if (_Lil_LilToonPassDummy == null)
                {
                    _Lil_LilToonPassDummy = Shader.Find(ShaderName.Lil_LilToonPassDummy);
                }
                return _Lil_LilToonPassDummy;
            }
        }

        /// <summary>lilToon Pass Opaque</summary>
        /// <remarks>Hidden/ltspass_opaque</remarks>
        protected Shader Lil_LilToonPassOpaque
        {
            get
            {
                if (_Lil_LilToonPassOpaque == null)
                {
                    _Lil_LilToonPassOpaque = Shader.Find(ShaderName.Lil_LilToonPassOpaque);
                }
                return _Lil_LilToonPassOpaque;
            }
        }

        /// <summary>lilToon Pass Cutout</summary>
        /// <remarks>Hidden/ltspass_cutout</remarks>
        protected Shader Lil_LilToonPassCutout
        {
            get
            {
                if (_Lil_LilToonPassCutout == null)
                {
                    _Lil_LilToonPassCutout = Shader.Find(ShaderName.Lil_LilToonPassCutout);
                }
                return _Lil_LilToonPassCutout;
            }
        }

        /// <summary>lilToon Pass Transparent</summary>
        /// <remarks>Hidden/ltspass_transparent</remarks>
        protected Shader Lil_LilToonPassTransparent
        {
            get
            {
                if (_Lil_LilToonPassTransparent == null)
                {
                    _Lil_LilToonPassTransparent = Shader.Find(ShaderName.Lil_LilToonPassTransparent);
                }
                return _Lil_LilToonPassTransparent;
            }
        }

        /// <summary>lilToon Pass Tessellation Opaque</summary>
        /// <remarks>Hidden/ltspass_tess_opaque</remarks>
        protected Shader Lil_LilToonPassTessOpaque
        {
            get
            {
                if (_Lil_LilToonPassTessOpaque == null)
                {
                    _Lil_LilToonPassTessOpaque = Shader.Find(ShaderName.Lil_LilToonPassTessOpaque);
                }
                return _Lil_LilToonPassTessOpaque;
            }
        }

        /// <summary>lilToon Pass Tessellation Cutout</summary>
        /// <remarks>Hidden/ltspass_tess_cutout</remarks>
        protected Shader Lil_LilToonPassTessCutout
        {
            get
            {
                if (_Lil_LilToonPassTessCutout == null)
                {
                    _Lil_LilToonPassTessCutout = Shader.Find(ShaderName.Lil_LilToonPassTessCutout);
                }
                return _Lil_LilToonPassTessCutout;
            }
        }

        /// <summary>lilToon Pass Tessellation Transparent</summary>
        /// <remarks>Hidden/ltspass_tess_transparent</remarks>
        protected Shader Lil_LilToonPassTessTransparent
        {
            get
            {
                if (_Lil_LilToonPassTessTransparent == null)
                {
                    _Lil_LilToonPassTessTransparent = Shader.Find(ShaderName.Lil_LilToonPassTessTransparent);
                }
                return _Lil_LilToonPassTessTransparent;
            }
        }

        /// <summary>lilToon Pass Lite Opaque</summary>
        /// <remarks>Hidden/ltspass_lite_opaque</remarks>
        protected Shader Lil_LilToonPassLiteOpaque
        {
            get
            {
                if (_Lil_LilToonPassLiteOpaque == null)
                {
                    _Lil_LilToonPassLiteOpaque = Shader.Find(ShaderName.Lil_LilToonPassLiteOpaque);
                }
                return _Lil_LilToonPassLiteOpaque;
            }
        }

        /// <summary>lilToon Pass Lite Cutout</summary>
        /// <remarks>Hidden/ltspass_lite_cutout</remarks>
        protected Shader Lil_LilToonPassLiteCutout
        {
            get
            {
                if (_Lil_LilToonPassLiteCutout == null)
                {
                    _Lil_LilToonPassLiteCutout = Shader.Find(ShaderName.Lil_LilToonPassLiteCutout);
                }
                return _Lil_LilToonPassLiteCutout;
            }
        }

        /// <summary>lilToon Pass Lite Transparent</summary>
        /// <remarks>Hidden/ltspass_lite_transparent</remarks>
        protected Shader Lil_LilToonPassLiteTransparent
        {
            get
            {
                if (_Lil_LilToonPassLiteTransparent == null)
                {
                    _Lil_LilToonPassLiteTransparent = Shader.Find(ShaderName.Lil_LilToonPassLiteTransparent);
                }
                return _Lil_LilToonPassLiteTransparent;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get a shader or default.
        /// </summary>
        /// <param name="shaderName">The shader name.</param>
        /// <returns>A shader instanse or default.</returns>
        public virtual Shader GetShaderOrDefault(string shaderName)
        {
            if (string.IsNullOrEmpty(shaderName))
            {
                return default;
            }

            switch (shaderName)
            {
                case ShaderName.Standard:
                    return Standard;

                case ShaderName.Particles_Standard_Surface:
                    return ParticlesStandardSurface;

                case ShaderName.Particles_Standard_Unlit:
                    return ParticlesStandardUnlit;

                case ShaderName.Skybox_6_Sided:
                    return Skybox6Sided;

                case ShaderName.Skybox_Cubemap:
                    return SkyboxCubemap;

                case ShaderName.Skybox_Panoramic:
                    return SkyboxPanoramic;

                case ShaderName.Skybox_Procedural:
                    return SkyboxProcedural;

                case ShaderName.Unlit_Color:
                    return UnlitColor;

                case ShaderName.Unlit_Texture:
                    return UnlitTexture;

                case ShaderName.Unlit_Transparent:
                    return UnlitTransparent;

                case ShaderName.Unlit_Transparent_Cutout:
                    return UnlitTransparentCutout;

                case ShaderName.UniGLTF_UniUnlit:
                    return UniGLTFUniUnlit;

                case ShaderName.UniGLTF_StandardVColor:
                    return UniGLTFStandardVColor;

                case ShaderName.VRM_UnlitTexture:
                    return VrmUnlitTexture;

                case ShaderName.VRM_UnlitTransparent:
                    return VrmUnlitTransparent;

                case ShaderName.VRM_UnlitCutout:
                    return VrmUnlitCutout;

                case ShaderName.VRM_UnlitTransparentZWrite:
                    return VrmUnlitTransparentZWrite;

                case ShaderName.VRM_MToon:
                    return VrmMtoon;

                case ShaderName.URP_Lit:
                    return UrpLit;

                case ShaderName.URP_SimpleLit:
                    return UrpSimpleLit;

                case ShaderName.URP_Unlit:
                    return UrpUnlit;

                case ShaderName.HDRP_Eye:
                    return HDRPEye;

                case ShaderName.HDRP_Hair:
                    return HDRPHair;

                case ShaderName.HDRP_Lit:
                    return HDRPLit;

                case ShaderName.Lil_LilToon:
                    return Lil_LilToon;

                case ShaderName.Lil_LilToonOutline:
                    return Lil_LilToonOutline;

                case ShaderName.Lil_LilToonOutlineOnly:
                    return Lil_LilToonOutlineOnly;

                case ShaderName.Lil_LilToonOutlineOnlyCutout:
                    return Lil_LilToonOutlineOnlyCutout;

                case ShaderName.Lil_LilToonOutlineOnlyTransparent:
                    return Lil_LilToonOutlineOnlyTransparent;

                case ShaderName.Lil_LilToonCutout:
                    return Lil_LilToonCutout;

                case ShaderName.Lil_LilToonCutoutOutline:
                    return Lil_LilToonCutoutOutline;

                case ShaderName.Lil_LilToonTransparent:
                    return Lil_LilToonTransparent;

                case ShaderName.Lil_LilToonTransparentOutline:
                    return Lil_LilToonTransparentOutline;

                case ShaderName.Lil_LilToonOnePassTransparent:
                    return Lil_LilToonOnePassTransparent;

                case ShaderName.Lil_LilToonOnePassTransparentOutline:
                    return Lil_LilToonOnePassTransparentOutline;

                case ShaderName.Lil_LilToonTwoPassTransparent:
                    return Lil_LilToonTwoPassTransparent;

                case ShaderName.Lil_LilToonTwoPassTransparentOutline:
                    return Lil_LilToonTwoPassTransparentOutline;

                case ShaderName.Lil_LilToonOverlay:
                    return Lil_LilToonOverlay;

                case ShaderName.Lil_LilToonOverlayOnePass:
                    return Lil_LilToonOverlayOnePass;

                case ShaderName.Lil_LilToonRefraction:
                    return Lil_LilToonRefraction;

                case ShaderName.Lil_LilToonRefractionBlur:
                    return Lil_LilToonRefractionBlur;

                case ShaderName.Lil_LilToonTessellation:
                    return Lil_LilToonTessellation;

                case ShaderName.Lil_LilToonTessellationOutline:
                    return Lil_LilToonTessellationOutline;

                case ShaderName.Lil_LilToonTessellationCutout:
                    return Lil_LilToonTessellationCutout;

                case ShaderName.Lil_LilToonTessellationCutoutOutline:
                    return Lil_LilToonTessellationCutoutOutline;

                case ShaderName.Lil_LilToonTessellationTransparent:
                    return Lil_LilToonTessellationTransparent;

                case ShaderName.Lil_LilToonTessellationTransparentOutline:
                    return Lil_LilToonTessellationTransparentOutline;

                case ShaderName.Lil_LilToonTessellationOnePassTransparent:
                    return Lil_LilToonTessellationOnePassTransparent;

                case ShaderName.Lil_LilToonTessellationOnePassTransparentOutline:
                    return Lil_LilToonTessellationOnePassTransparentOutline;

                case ShaderName.Lil_LilToonTessellationTwoPassTransparent:
                    return Lil_LilToonTessellationTwoPassTransparent;

                case ShaderName.Lil_LilToonTessellationTwoPassTransparentOutline:
                    return Lil_LilToonTessellationTwoPassTransparentOutline;

                case ShaderName.Lil_LilToonFur:
                    return Lil_LilToonFur;

                case ShaderName.Lil_LilToonFurCutout:
                    return Lil_LilToonFurCutout;

                case ShaderName.Lil_LilToonFurTwoPass:
                    return Lil_LilToonFurTwoPass;

                case ShaderName.Lil_LilToonFurOnly:
                    return Lil_LilToonFurOnly;

                case ShaderName.Lil_LilToonFurOnlyCutout:
                    return Lil_LilToonFurOnlyCutout;

                case ShaderName.Lil_LilToonFurOnlyTwoPass:
                    return Lil_LilToonFurOnlyTwoPass;

                case ShaderName.Lil_LilToonGem:
                    return Lil_LilToonGem;

                case ShaderName.Lil_LilToonFakeShadow:
                    return Lil_LilToonFakeShadow;

                case ShaderName.Lil_LilToonLite:
                    return Lil_LilToonLite;

                case ShaderName.Lil_LilToonLiteOutline:
                    return Lil_LilToonLiteOutline;

                case ShaderName.Lil_LilToonLiteCutout:
                    return Lil_LilToonLiteCutout;

                case ShaderName.Lil_LilToonLiteCutoutOutline:
                    return Lil_LilToonLiteCutoutOutline;

                case ShaderName.Lil_LilToonLiteTransparent:
                    return Lil_LilToonLiteTransparent;

                case ShaderName.Lil_LilToonLiteTransparentOutline:
                    return Lil_LilToonLiteTransparentOutline;

                case ShaderName.Lil_LilToonLiteOnePassTransparent:
                    return Lil_LilToonLiteOnePassTransparent;

                case ShaderName.Lil_LilToonLiteOnePassTransparentOutline:
                    return Lil_LilToonLiteOnePassTransparentOutline;

                case ShaderName.Lil_LilToonLiteTwoPassTransparent:
                    return Lil_LilToonLiteTwoPassTransparent;

                case ShaderName.Lil_LilToonLiteTwoPassTransparentOutline:
                    return Lil_LilToonLiteTwoPassTransparentOutline;

                case ShaderName.Lil_LilToonLiteOverlay:
                    return Lil_LilToonLiteOverlay;

                case ShaderName.Lil_LilToonLiteOverlayOnePass:
                    return Lil_LilToonLiteOverlayOnePass;

                case ShaderName.Lil_LilToonMulti:
                    return Lil_LilToonMulti;

                case ShaderName.Lil_LilToonMultiOutline:
                    return Lil_LilToonMultiOutline;

                case ShaderName.Lil_LilToonMultiRefraction:
                    return Lil_LilToonMultiRefraction;

                case ShaderName.Lil_LilToonMultiFur:
                    return Lil_LilToonMultiFur;

                case ShaderName.Lil_LilToonMultiGem:
                    return Lil_LilToonMultiGem;

                case ShaderName.Lil_LilToonOtherBaker:
                    return Lil_LilToonOtherBaker;

                case ShaderName.Lil_LilToonPassDummy:
                    return Lil_LilToonPassDummy;

                case ShaderName.Lil_LilToonPassOpaque:
                    return Lil_LilToonPassOpaque;

                case ShaderName.Lil_LilToonPassCutout:
                    return Lil_LilToonPassCutout;

                case ShaderName.Lil_LilToonPassTransparent:
                    return Lil_LilToonPassTransparent;

                case ShaderName.Lil_LilToonPassTessOpaque:
                    return Lil_LilToonPassTessOpaque;

                case ShaderName.Lil_LilToonPassTessCutout:
                    return Lil_LilToonPassTessCutout;

                case ShaderName.Lil_LilToonPassTessTransparent:
                    return Lil_LilToonPassTessTransparent;

                case ShaderName.Lil_LilToonPassLiteOpaque:
                    return Lil_LilToonPassLiteOpaque;

                case ShaderName.Lil_LilToonPassLiteCutout:
                    return Lil_LilToonPassLiteCutout;

                case ShaderName.Lil_LilToonPassLiteTransparent:
                    return Lil_LilToonPassLiteTransparent;

                default:
                    return default;
            }
        }

        /// <summary>
        /// Get a shader or default.
        /// </summary>
        /// <param name="vgoMaterial">The vgo material.</param>
        /// <returns>A shader instanse or default.</returns>
        public virtual Shader GetShaderOrDefault(VgoMaterial vgoMaterial)
        {
            if (vgoMaterial == null)
            {
                return default;
            }

            if (string.IsNullOrEmpty(vgoMaterial.shaderName) == false)
            {
                Shader shader = GetShaderOrDefault(vgoMaterial.shaderName);

                if (shader != default)
                {
                    return shader;
                }
            }

            //// @notice
            //if (vgoMaterial.isUnlit)
            //{
            //    return UniGLTFUniUnlit;
            //}

            //// @notice
            //if (vgoMaterial.HasVertexColor)
            //{
            //    return UniGLTFStandardVColor;
            //}

            return default;
        }

        /// <summary>
        /// Get a shader or standard.
        /// </summary>
        /// <param name="vgoMaterial">The vgo material.</param>
        /// <param name="renderPipelineType">Type of render pipeline.</param>
        /// <returns>A shader instanse.</returns>
        public virtual Shader GetShaderOrStandard(VgoMaterial vgoMaterial, RenderPipelineType renderPipelineType)
        {
            Shader shader = GetShaderOrDefault(vgoMaterial);

            if (shader == default)
            {
                if (renderPipelineType == RenderPipelineType.URP)
                {
                    if (vgoMaterial.isUnlit)
                    {
                        shader = UrpUnlit;
                    }
                    else
                    {
                        shader = UrpLit;
                    }
                }
                else if (renderPipelineType == RenderPipelineType.HDRP)
                {
                    if (vgoMaterial.isUnlit)
                    {
                        // @notice
                        shader = HDRPLit;
                    }
                    else
                    {
                        shader = HDRPLit;
                    }
                }
                else  // BRP
                {
                    if (vgoMaterial.isUnlit)
                    {
                        // @notice
                        shader = UniGLTFUniUnlit;
                    }
                    else
                    {
                        shader = Standard;
                    }
                }
            }

            return shader;
        }

        #endregion
    }
}
