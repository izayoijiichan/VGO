// ----------------------------------------------------------------------
// @Namespace : UniVgo.Porters
// @Class     : MaterialPorterStore
// ----------------------------------------------------------------------
namespace UniVgo.Porters
{
    /// <summary>
    /// Material Porter Store
    /// </summary>
    public class MaterialPorterStore : IMaterialPorterStore
    {
        #region Fields & Properties

        /// <summary>MToon Material Porter</summary>
        protected MtoonMaterialPorter _MtoonMaterialPorter;

        /// <summary>MToon Material Porter</summary>
        protected MtoonMaterialPorter MtoonMaterialPorter
        {
            get
            {
                if (_MtoonMaterialPorter == null)
                {
                    _MtoonMaterialPorter = new MtoonMaterialPorter();
                }
                return _MtoonMaterialPorter;
            }
        }

        /// <summary>Particle Material Porter</summary>
        protected ParticleMaterialPorter _ParticleMaterialPorter;

        /// <summary>Particle Material Porter</summary>
        protected ParticleMaterialPorter ParticleMaterialPorter
        {
            get
            {
                if (_ParticleMaterialPorter == null)
                {
                    _ParticleMaterialPorter = new ParticleMaterialPorter();
                }
                return _ParticleMaterialPorter;
            }
        }

        /// <summary>Skybox Material Porter</summary>
        protected SkyboxMaterialPorter _SkyboxMaterialPorter;

        /// <summary>Skybox Material Porter</summary>
        protected SkyboxMaterialPorter SkyboxMaterialPorter
        {
            get
            {
                if (_SkyboxMaterialPorter == null)
                {
                    _SkyboxMaterialPorter = new SkyboxMaterialPorter();
                }
                return _SkyboxMaterialPorter;
            }
        }

        /// <summary>Standard Material Porter</summary>
        protected StandardMaterialPorter _StandardMaterialPorter;

        /// <summary>Standard Material Porter</summary>
        protected StandardMaterialPorter StandardMaterialPorter
        {
            get
            {
                if (_StandardMaterialPorter == null)
                {
                    _StandardMaterialPorter = new StandardMaterialPorter();
                }
                return _StandardMaterialPorter;
            }
        }

        /// <summary>Standard VColor Material Porter</summary>
        protected StandardVColorMaterialPorter _StandardVColorMaterialPorter;

        /// <summary>Standard VColor Material Porter</summary>
        protected StandardVColorMaterialPorter StandardVColorMaterialPorter
        {
            get
            {
                if (_StandardVColorMaterialPorter == null)
                {
                    _StandardVColorMaterialPorter = new StandardVColorMaterialPorter();
                }
                return _StandardVColorMaterialPorter;
            }
        }

        /// <summary>Unlit Material Porter</summary>
        protected UnlitMaterialPorter _UnlitMaterialPorter;

        /// <summary>Unlit Material Porter</summary>
        protected UnlitMaterialPorter UnlitMaterialPorter
        {
            get
            {
                if (_UnlitMaterialPorter == null)
                {
                    _UnlitMaterialPorter = new UnlitMaterialPorter();
                }
                return _UnlitMaterialPorter;
            }
        }

        #endregion

        #region Public Methods (MaterialInfo)

        /// <summary>
        /// Get a porter or default.
        /// </summary>
        /// <param name="materialInfo">The material info.</param>
        /// <returns>A material porter instanse.</returns>
        public virtual IMaterialPorter GetPorterOrDefault(MaterialInfo materialInfo)
        {
            if (materialInfo == null)
            {
                return default;
            }

            if (string.IsNullOrEmpty(materialInfo.shaderName) == false)
            {
                IMaterialPorter porter = GetPorterOrDefault(materialInfo.shaderName);

                if (porter != default)
                {
                    return porter;
                }
            }

            switch (materialInfo.shaderGroup)
            {
                case ShaderGroup.Standard:
                    return StandardMaterialPorter;
                case ShaderGroup.Particles:
                    return ParticleMaterialPorter;
                case ShaderGroup.Skybox:
                    return SkyboxMaterialPorter;
                case ShaderGroup.Unlit:
                    return UnlitMaterialPorter;
                case ShaderGroup.UniGltf:
                    if (materialInfo.lightingType == MaterialLightingType.Unlit)
                    {
                        return UnlitMaterialPorter;
                    }
                    else if (materialInfo.lightingType == MaterialLightingType.Light)
                    {
                        return StandardVColorMaterialPorter;
                    }
                    else
                    {
                        return StandardVColorMaterialPorter;
                    }
                case ShaderGroup.VRM:
                    //if (materialInfo.lightingType == MaterialLightingType.Unlit)
                    //{
                    //    return UnlitMaterialPorter;
                    //}
                    //else
                    {
                        return MtoonMaterialPorter;
                    }
                default:
                    break;
            }

            // @notice
            if (materialInfo.lightingType == MaterialLightingType.Unlit)
            {
                return UnlitMaterialPorter;
            }

            // @notice
            if (materialInfo.HasVertexColor)
            {
                return StandardVColorMaterialPorter;
            }

            return default;
        }

        /// <summary>
        /// Get a porter or standard.
        /// </summary>
        /// <param name="materialInfo">The material info.</param>
        /// <returns>A material porter instanse.</returns>
        public virtual IMaterialPorter GetPorterOrStandard(MaterialInfo materialInfo)
        {
            IMaterialPorter porter = GetPorterOrDefault(materialInfo);

            if (porter == default)
            {
                porter = StandardMaterialPorter;
            }

            return porter;
        }

        #endregion

        #region Public Methods (ShaderName)

        /// <summary>
        /// Get a porter or default.
        /// </summary>
        /// <param name="shaderName">The shader name.</param>
        /// <returns>A material porter instanse.</returns>
        public virtual IMaterialPorter GetPorterOrDefault(string shaderName)
        {
            if (string.IsNullOrEmpty(shaderName))
            {
                return default;
            }

            switch (shaderName)
            {
                case ShaderName.Standard:
                    return StandardMaterialPorter;
                case ShaderName.Particles_Standard_Surface:
                case ShaderName.Particles_Standard_Unlit:
                    return ParticleMaterialPorter;
                case ShaderName.Skybox_6_Sided:
                case ShaderName.Skybox_Cubemap:
                case ShaderName.Skybox_Panoramic:
                case ShaderName.Skybox_Procedural:
                    return SkyboxMaterialPorter;
                case ShaderName.Unlit_Color:
                case ShaderName.Unlit_Texture:
                case ShaderName.Unlit_Transparent:
                case ShaderName.Unlit_Transparent_Cutout:
                case ShaderName.UniGLTF_UniUnlit:
                    return UnlitMaterialPorter;
                case ShaderName.UniGLTF_StandardVColor:
                    return StandardVColorMaterialPorter;
                case ShaderName.VRM_UnlitTexture:
                case ShaderName.VRM_UnlitTransparent:
                case ShaderName.VRM_UnlitCutout:
                case ShaderName.VRM_UnlitTransparentZWrite:
                    return UnlitMaterialPorter;
                case ShaderName.VRM_MToon:
                    return MtoonMaterialPorter;
                default:
                    return default;
            }
        }

        /// <summary>
        /// Get a porter or standard.
        /// </summary>
        /// <param name="shaderName">The shader name.</param>
        /// <returns>A material porter instanse.</returns>
        public virtual IMaterialPorter GetPorterOrStandard(string shaderName)
        {
            IMaterialPorter porter = GetPorterOrDefault(shaderName);

            if (porter == default)
            {
                porter = StandardMaterialPorter;
            }

            return porter;
        }

        #endregion
    }
}
