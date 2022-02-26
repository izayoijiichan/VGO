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
        #region Fields

        /// <summary>Standard</summary>
        protected Shader _Standard;

        /// <summary>HDRP/Eye</summary>
        protected Shader _HDRPEye;

        /// <summary>HDRP/Hair</summary>
        protected Shader _HDRPHair;

        /// <summary>HDRP/Lit</summary>
        protected Shader _HDRPLit;

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

        /// <summary>Universal Render Pipeline/Lit</summary>
        protected Shader _UrpLit;

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

        #region Properties

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

                case ShaderName.HDRP_Eye:
                    return HDRPEye;

                case ShaderName.HDRP_Hair:
                    return HDRPHair;

                case ShaderName.HDRP_Lit:
                    return HDRPLit;

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

                case ShaderName.URP_Lit:
                    return UrpLit;

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
            if (vgoMaterial.isUnlit)
            {
                return UniGLTFUniUnlit;
            }

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
        /// <returns>A shader instanse.</returns>
        public virtual Shader GetShaderOrStandard(VgoMaterial vgoMaterial)
        {
            Shader shader = GetShaderOrDefault(vgoMaterial);

            if (shader == default)
            {
                shader = Standard;
            }

            return shader;
        }

        #endregion
    }
}
