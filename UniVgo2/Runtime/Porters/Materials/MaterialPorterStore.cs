// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : MaterialPorterStore
// ----------------------------------------------------------------------
namespace UniVgo2.Porters
{
    using NewtonVgo;

    /// <summary>
    /// Material Porter Store
    /// </summary>
    public class MaterialPorterStore : IMaterialPorterStore
    {
        #region Fields & Properties

        /// <summary>HDRP Material Porter</summary>
        protected HdrpMaterialPorter _HdrpMaterialPorter;

        /// <summary>HDRP Material Porter</summary>
        protected HdrpMaterialPorter HdrpMaterialPorter
        {
            get
            {
                if (_HdrpMaterialPorter == null)
                {
                    _HdrpMaterialPorter = new HdrpMaterialPorter();
                }
                return _HdrpMaterialPorter;
            }
        }

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

        /// <summary>URP Material Porter</summary>
        protected UrpMaterialPorter _UrpMaterialPorter;

        /// <summary>URP Material Porter</summary>
        protected UrpMaterialPorter UrpMaterialPorter
        {
            get
            {
                if (_UrpMaterialPorter == null)
                {
                    _UrpMaterialPorter = new UrpMaterialPorter();
                }
                return _UrpMaterialPorter;
            }
        }

        #endregion

        #region Public Methods (VgoMaterial)

        /// <summary>
        /// Get a porter or default.
        /// </summary>
        /// <param name="vgoMaterial">The vgo material.</param>
        /// <returns>A material porter instanse.</returns>
        public virtual IMaterialPorter GetPorterOrDefault(VgoMaterial vgoMaterial)
        {
            if (vgoMaterial == null)
            {
                return default;
            }

            if (string.IsNullOrEmpty(vgoMaterial.shaderName) == false)
            {
                IMaterialPorter porter = GetPorterOrDefault(vgoMaterial.shaderName);

                if (porter != default)
                {
                    return porter;
                }
            }

            // @notice
            //if (vgoMaterial.isUnlit)
            //{
            //    return UnlitMaterialPorter;
            //}

            // @notice
            //if (vgoMaterial.HasVertexColor)
            //{
            //    return StandardVColorMaterialPorter;
            //}

            return default;
        }

        /// <summary>
        /// Get a porter or standard.
        /// </summary>
        /// <param name="vgoMaterial">The vgo material.</param>
        /// <param name="renderPipelineType">Type of render pipeline.</param>
        /// <returns>A material porter instanse.</returns>
        public virtual IMaterialPorter GetPorterOrStandard(VgoMaterial vgoMaterial, RenderPipelineType renderPipelineType)
        {
            IMaterialPorter porter = GetPorterOrDefault(vgoMaterial);

            if (porter == default)
            {
                if (renderPipelineType == RenderPipelineType.URP)
                {
                    porter = UrpMaterialPorter;
                }
                else if (renderPipelineType == RenderPipelineType.HDRP)
                {
                    porter = HdrpMaterialPorter;
                }
                else  // BRP
                {
                    if (vgoMaterial.isUnlit)
                    {
                        porter = UnlitMaterialPorter;
                    }
                    else
                    {
                        porter = StandardMaterialPorter;
                    }
                }
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
                case ShaderName.HDRP_Eye:
                case ShaderName.HDRP_Hair:
                case ShaderName.HDRP_Lit:
                    return HdrpMaterialPorter;
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
                case ShaderName.URP_Lit:
                case ShaderName.URP_SimpleLit:
                case ShaderName.URP_Unlit:
                    return UrpMaterialPorter;
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
        /// <param name="renderPipelineType">Type of render pipeline.</param>
        /// <returns>A material porter instanse.</returns>
        public virtual IMaterialPorter GetPorterOrStandard(string shaderName, RenderPipelineType renderPipelineType)
        {
            IMaterialPorter porter = GetPorterOrDefault(shaderName);

            if (porter == default)
            {
                if (renderPipelineType == RenderPipelineType.URP)
                {
                    porter = UrpMaterialPorter;
                }
                else if (renderPipelineType == RenderPipelineType.HDRP)
                {
                    porter = HdrpMaterialPorter;
                }
                else  // BRP
                {
                    porter = StandardMaterialPorter;
                }
            }

            return porter;
        }

        #endregion
    }
}
