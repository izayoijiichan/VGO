// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : ShaderStore
// ----------------------------------------------------------------------
namespace UniVgo
{
    using UnityEngine;

    /// <summary>
    /// Shader Store
    /// </summary>
    public class ShaderStore : IShaderStore
    {
        #region Fields

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

                default:
                    return default;
            }
        }

        /// <summary>
        /// Get a shader or default.
        /// </summary>
        /// <param name="materialInfo">The material info.</param>
        /// <returns>A shader instanse or default.</returns>
        public virtual Shader GetShaderOrDefault(MaterialInfo materialInfo)
        {
            if (materialInfo == null)
            {
                return default;
            }

            if (string.IsNullOrEmpty(materialInfo.shaderName) == false)
            {
                Shader shader = GetShaderOrDefault(materialInfo.shaderName);

                if (shader != default)
                {
                    return shader;
                }
            }

            if (materialInfo.shaderGroup == ShaderGroup.Standard)
            {
                return Standard;
            }

            if (materialInfo.shaderGroup == ShaderGroup.Particles)
            {
                if (materialInfo.lightingType == MaterialLightingType.Unlit)
                {
                    return ParticlesStandardUnlit;
                }
                else
                {
                    return ParticlesStandardSurface;
                }
            }

            if (materialInfo.shaderGroup == ShaderGroup.Skybox)
            {
                switch (materialInfo.shaderName)
                {
                    case ShaderName.Skybox_6_Sided:
                        return Skybox6Sided;

                    case ShaderName.Skybox_Cubemap:
                        return SkyboxCubemap;

                    case ShaderName.Skybox_Panoramic:
                        return SkyboxPanoramic;

                    case ShaderName.Skybox_Procedural:
                        return SkyboxProcedural;

                    default:
                        return SkyboxProcedural;
                }
            }

            if (materialInfo.shaderGroup == ShaderGroup.Unlit)
            {
                switch (materialInfo.shaderName)
                {
                    case ShaderName.Unlit_Color:
                        return UnlitColor;

                    case ShaderName.Unlit_Texture:
                        return UnlitTexture;

                    case ShaderName.Unlit_Transparent:
                        return UnlitTransparent;

                    case ShaderName.Unlit_Transparent_Cutout:
                        return UnlitTransparentCutout;

                    default:
                        return UnlitColor;
                }
            }

            if (materialInfo.shaderGroup == ShaderGroup.UniGltf)
            {
                if (materialInfo.lightingType == MaterialLightingType.Unlit)
                {
                    return UniGLTFUniUnlit;
                }
                else if (materialInfo.lightingType == MaterialLightingType.Light)
                {
                    return UniGLTFStandardVColor;
                }
                else
                {
                    return UniGLTFStandardVColor;
                }
            }

            if (materialInfo.shaderGroup == ShaderGroup.VRM)
            {
                //if (materialInfo.lightingType == MaterialLightingType.Unlit)
                //{
                //    switch (materialInfo.shaderName)
                //    {
                //        case ShaderName.VRM_UnlitTexture:
                //            //return VrmUnlitTexture;
                //            return UnlitTexture;
                //        case ShaderName.VRM_UnlitTransparent:
                //            //return VrmUnlitTransparent;
                //            return UnlitTransparent;
                //        case ShaderName.VRM_UnlitCutout:
                //            //return VrmUnlitCutout;
                //            return UnlitTransparentCutout;
                //        case ShaderName.VRM_UnlitTransparentZWrite:
                //            //return VrmUnlitTransparentZWrite;
                //            return UnlitTransparentCutout;
                //        default:
                //            //return VrmUnlitTexture;
                //            return UnlitTexture;
                //    }
                //}
                //else
                {
                    return VrmMtoon;
                }
            }

            // @notice
            if (materialInfo.lightingType == MaterialLightingType.Unlit)
            {
                return UniGLTFUniUnlit;
            }

            // @notice
            if (materialInfo.HasVertexColor)
            {
                return UniGLTFStandardVColor;
            }

            return default;
        }

        /// <summary>
        /// Get a shader or standard.
        /// </summary>
        /// <param name="materialInfo">The material info.</param>
        /// <returns>A shader instanse.</returns>
        public virtual Shader GetShaderOrStandard(MaterialInfo materialInfo)
        {
            Shader shader = GetShaderOrDefault(materialInfo);

            if (shader == default)
            {
                shader = Standard;
            }

            return shader;
        }

        #endregion
    }
}
