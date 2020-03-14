// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : VgoShaderStore
// ----------------------------------------------------------------------
namespace UniVgo
{
    using UniGLTFforUniVgo;
    using UnityEngine;

    /// <summary>
    /// VGO Shader Store
    /// </summary>
    public class VgoShaderStore : ShaderStore
    {
        #region Fields

        /// <summary>Particles/Standard Surface</summary>
        Shader _ParticlesStandardSurface;

        /// <summary>Particles/Standard Unlit</summary>
        Shader _ParticlesStandardUnlit;

        /// <summary>Skybox/6 Sided</summary>
        Shader _Skybox6Sided;

        /// <summary>Skybox/Cubemap</summary>
        Shader _SkyboxCubemap;

        /// <summary>Skybox/Panoramic</summary>
        Shader _SkyboxPanoramic;

        /// <summary>Skybox/Procedural</summary>
        Shader _SkyboxProcedural;

        /// <summary>VRM/UnlitTexture</summary>
        Shader _VrmUnlitTexture;

        /// <summary>VRM/UnlitTransparent</summary>
        Shader _VrmUnlitTransparent;

        /// <summary>VRM/UnlitCutout</summary>
        Shader _VrmUnlitCutout;

        /// <summary>VRM/UnlitTransparentZWrite</summary>
        Shader _VrmUnlitTransparentZWrite;

        /// <summary>VRM/MToon</summary>
        Shader _VrmMtoon;

        #endregion

        #region Properties

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

        #region Constructors

        /// <summary>
        /// Create a new instance of VgoShaderStore.
        /// </summary>
        /// <param name="_"></param>
        public VgoShaderStore(ImporterContext _) : base(_)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get a shader.
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        public override Shader GetShader(glTFMaterial material)
        {
            if (material == null)
            {
                return Default;
            }

            if (material.extensions == null)
            {
                return Default;
            }

            if ((material.extensions.VGO_materials != null) &&
                (material.extensions.VGO_materials.shaderName != null))
            {
                switch (material.extensions.VGO_materials.shaderName)
                {
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

                    case ShaderName.UniGLTF_StandardVColor:
                        return VColor;

                    case ShaderName.Unlit_Color:
                        return UnlitColor;

                    case ShaderName.Unlit_Texture:
                        return UnlitTexture;

                    case ShaderName.Unlit_Transparent:
                        return UnlitTransparent;

                    case ShaderName.Unlit_Transparent_Cutout:
                        return UnlitCutout;

                    case ShaderName.UniGLTF_UniUnlit:
                        return UniUnlit;

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
                        break;
                }
            }

            if (material.extensions.VGO_materials_particle != null)
            {
                if (material.extensions.KHR_materials_unlit != null)
                {
                    return ParticlesStandardUnlit;
                }
                else
                {
                    return ParticlesStandardSurface;
                }
            }

            if (material.extensions.VGO_materials_skybox != null)
            {
                return SkyboxProcedural;
            }

            if (material.extensions.KHR_materials_unlit != null)
            {
                return UniUnlit;
            }

            if (material.extensions.VRMC_materials_mtoon != null)
            {
                return VrmMtoon;
            }

            return Default;
        }

        #endregion
    }
}
