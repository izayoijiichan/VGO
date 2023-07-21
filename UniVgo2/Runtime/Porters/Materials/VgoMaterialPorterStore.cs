// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : VgoMaterialPorterStore
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Porters
{
    using NewtonVgo;

    /// <summary>
    /// VGO Material Porter Store
    /// </summary>
    public class VgoMaterialPorterStore : IMaterialPorterStore
    {
        #region Fields & Properties

        /// <summary>HDRP Material Porter</summary>
        protected HdrpMaterialPorter? _HdrpMaterialPorter;

        /// <summary>HDRP Material Porter</summary>
        protected HdrpMaterialPorter HdrpMaterialPorter
            => _HdrpMaterialPorter ??= new HdrpMaterialPorter();

        /// <summary>lilToon Material Porter</summary>
        protected LilToonMaterialPorter? _LilToonMaterialPorter;

        /// <summary>lilToon Material Porter</summary>
        protected LilToonMaterialPorter LilToonMaterialPorter
            => _LilToonMaterialPorter ??= new LilToonMaterialPorter();

        /// <summary>MToon Material Porter</summary>
        protected MtoonMaterialPorter? _MtoonMaterialPorter;

        /// <summary>MToon Material Porter</summary>
        protected MtoonMaterialPorter MtoonMaterialPorter
            => _MtoonMaterialPorter ??= new MtoonMaterialPorter();

        /// <summary>MToon 1.0 Material Porter</summary>
        protected Mtoon10MaterialPorter? _Mtoon10MaterialPorter;

        /// <summary>MToon 1.0 Material Porter</summary>
        protected Mtoon10MaterialPorter Mtoon10MaterialPorter
            => _Mtoon10MaterialPorter ??= new Mtoon10MaterialPorter();

        /// <summary>Particle Material Porter</summary>
        protected ParticleMaterialPorter? _ParticleMaterialPorter;

        /// <summary>Particle Material Porter</summary>
        protected ParticleMaterialPorter ParticleMaterialPorter
            => _ParticleMaterialPorter ??= new ParticleMaterialPorter();

        /// <summary>Skybox Material Porter</summary>
        protected SkyboxMaterialPorter? _SkyboxMaterialPorter;

        /// <summary>Skybox Material Porter</summary>
        protected SkyboxMaterialPorter SkyboxMaterialPorter
            => _SkyboxMaterialPorter ??= new SkyboxMaterialPorter();

        /// <summary>Standard Material Porter</summary>
        protected StandardMaterialPorter? _StandardMaterialPorter;

        /// <summary>Standard Material Porter</summary>
        protected StandardMaterialPorter StandardMaterialPorter
            => _StandardMaterialPorter ??= new StandardMaterialPorter();

        /// <summary>Standard VColor Material Porter</summary>
        protected StandardVColorMaterialPorter? _StandardVColorMaterialPorter;

        /// <summary>Standard VColor Material Porter</summary>
        protected StandardVColorMaterialPorter StandardVColorMaterialPorter
            => _StandardVColorMaterialPorter ??= new StandardVColorMaterialPorter();

        /// <summary>Unlit Material Porter</summary>
        protected UnlitMaterialPorter? _UnlitMaterialPorter;

        /// <summary>Unlit Material Porter</summary>
        protected UnlitMaterialPorter UnlitMaterialPorter
            => _UnlitMaterialPorter ??= new UnlitMaterialPorter();

        /// <summary>URP Material Porter</summary>
        protected UrpMaterialPorter? _UrpMaterialPorter;

        /// <summary>URP Material Porter</summary>
        protected UrpMaterialPorter UrpMaterialPorter
            => _UrpMaterialPorter ??= new UrpMaterialPorter();

        /// <summary>URP Particle Material Porter</summary>
        protected UrpParticleMaterialPorter? _UrpParticleMaterialPorter;

        /// <summary>URP Particle Material Porter</summary>
        protected UrpParticleMaterialPorter UrpParticleMaterialPorter
            => _UrpParticleMaterialPorter ??= new UrpParticleMaterialPorter();

        #endregion

        #region Public Methods (VgoMaterial)

        /// <summary>
        /// Get a porter or default.
        /// </summary>
        /// <param name="vgoMaterial">The vgo material.</param>
        /// <returns>A material porter instanse.</returns>
        public virtual IMaterialPorter? GetPorterOrDefault(in VgoMaterial vgoMaterial)
        {
            if (vgoMaterial == null)
            {
                return default;
            }

            if ((vgoMaterial.shaderName != null) &&
                (string.IsNullOrEmpty(vgoMaterial.shaderName) == false))
            {
                IMaterialPorter? porter = GetPorterOrDefault(vgoMaterial.shaderName);

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
        public virtual IMaterialPorter GetPorterOrStandard(in VgoMaterial vgoMaterial, in RenderPipelineType renderPipelineType)
        {
            IMaterialPorter? porter = GetPorterOrDefault(vgoMaterial);

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
        public virtual IMaterialPorter? GetPorterOrDefault(in string shaderName)
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

                case ShaderName.URP_Lit:
                case ShaderName.URP_SimpleLit:
                case ShaderName.URP_Unlit:
                    return UrpMaterialPorter;

                case ShaderName.URP_Particles_Lit:
                case ShaderName.URP_Particles_Unlit:
                    return UrpParticleMaterialPorter;

                case ShaderName.HDRP_Eye:
                case ShaderName.HDRP_Hair:
                case ShaderName.HDRP_Lit:
                    return HdrpMaterialPorter;

                case ShaderName.Lil_LilToon:
                case ShaderName.Lil_LilToonOutline:
                case ShaderName.Lil_LilToonOutlineOnly:
                case ShaderName.Lil_LilToonOutlineOnlyCutout:
                case ShaderName.Lil_LilToonOutlineOnlyTransparent:
                case ShaderName.Lil_LilToonCutout:
                case ShaderName.Lil_LilToonCutoutOutline:
                case ShaderName.Lil_LilToonTransparent:
                case ShaderName.Lil_LilToonTransparentOutline:
                case ShaderName.Lil_LilToonOnePassTransparent:
                case ShaderName.Lil_LilToonOnePassTransparentOutline:
                case ShaderName.Lil_LilToonTwoPassTransparent:
                case ShaderName.Lil_LilToonTwoPassTransparentOutline:
                case ShaderName.Lil_LilToonOverlay:
                case ShaderName.Lil_LilToonOverlayOnePass:
                case ShaderName.Lil_LilToonRefraction:
                case ShaderName.Lil_LilToonRefractionBlur:
                case ShaderName.Lil_LilToonFur:
                case ShaderName.Lil_LilToonFurCutout:
                case ShaderName.Lil_LilToonFurTwoPass:
                case ShaderName.Lil_LilToonFurOnly:
                case ShaderName.Lil_LilToonFurOnlyCutout:
                case ShaderName.Lil_LilToonFurOnlyTwoPass:
                case ShaderName.Lil_LilToonGem:
                case ShaderName.Lil_LilToonTessellation:
                case ShaderName.Lil_LilToonTessellationOutline:
                case ShaderName.Lil_LilToonTessellationCutout:
                case ShaderName.Lil_LilToonTessellationCutoutOutline:
                case ShaderName.Lil_LilToonTessellationTransparent:
                case ShaderName.Lil_LilToonTessellationTransparentOutline:
                case ShaderName.Lil_LilToonTessellationOnePassTransparent:
                case ShaderName.Lil_LilToonTessellationOnePassTransparentOutline:
                case ShaderName.Lil_LilToonTessellationTwoPassTransparent:
                case ShaderName.Lil_LilToonTessellationTwoPassTransparentOutline:
                case ShaderName.Lil_LilToonLite:
                case ShaderName.Lil_LilToonLiteOutline:
                case ShaderName.Lil_LilToonLiteCutout:
                case ShaderName.Lil_LilToonLiteCutoutOutline:
                case ShaderName.Lil_LilToonLiteTransparent:
                case ShaderName.Lil_LilToonLiteTransparentOutline:
                case ShaderName.Lil_LilToonLiteOnePassTransparent:
                case ShaderName.Lil_LilToonLiteOnePassTransparentOutline:
                case ShaderName.Lil_LilToonLiteTwoPassTransparent:
                case ShaderName.Lil_LilToonLiteTwoPassTransparentOutline:
                case ShaderName.Lil_LilToonLiteOverlay:
                case ShaderName.Lil_LilToonLiteOverlayOnePass:
                case ShaderName.Lil_LilToonMulti:
                case ShaderName.Lil_LilToonMultiOutline:
                case ShaderName.Lil_LilToonMultiRefraction:
                case ShaderName.Lil_LilToonMultiFur:
                case ShaderName.Lil_LilToonMultiGem:
                case ShaderName.Lil_LilToonFakeShadow:
                case ShaderName.Lil_LilToonOtherBaker:
                case ShaderName.Lil_LilToonPassDummy:
                case ShaderName.Lil_LilToonPassOpaque:
                case ShaderName.Lil_LilToonPassCutout:
                case ShaderName.Lil_LilToonPassTransparent:
                case ShaderName.Lil_LilToonPassTessOpaque:
                case ShaderName.Lil_LilToonPassTessCutout:
                case ShaderName.Lil_LilToonPassTessTransparent:
                case ShaderName.Lil_LilToonPassLiteOpaque:
                case ShaderName.Lil_LilToonPassLiteCutout:
                case ShaderName.Lil_LilToonPassLiteTransparent:
                    return LilToonMaterialPorter;

                case ShaderName.VRM_MToon:
                    return MtoonMaterialPorter;

                case ShaderName.VRM_MToon10:
                    return Mtoon10MaterialPorter;

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
        public virtual IMaterialPorter GetPorterOrStandard(in string shaderName, in RenderPipelineType renderPipelineType)
        {
            IMaterialPorter? porter = GetPorterOrDefault(shaderName);

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

        /// <summary>
        /// Get a porter or standard.
        /// </summary>
        /// <param name="shaderName">The shader name.</param>
        /// <param name="vgoMaterial">The vgo material.</param>
        /// <param name="renderPipelineType">Type of render pipeline.</param>
        /// <returns>A material porter instanse.</returns>
        public virtual IMaterialPorter GetPorterOrStandard(in string shaderName, in VgoMaterial vgoMaterial, in RenderPipelineType renderPipelineType)
        {
            IMaterialPorter? porter = GetPorterOrDefault(shaderName);

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
            else
            {
                if (renderPipelineType == RenderPipelineType.URP)
                {
                    switch (vgoMaterial.shaderName)
                    {
                        case ShaderName.Particles_Standard_Surface:
                        case ShaderName.Particles_Standard_Unlit:
                            porter = ParticleMaterialPorter;
                            break;
                        case ShaderName.VRM_MToon:
                            porter = MtoonMaterialPorter;
                            break;
                        default:
                            break;
                    }
                }

                // @test
                //if (vgoMaterial.shaderName == ShaderName.VRM_MToon)
                //{
                //    porter = MtoonMaterialPorter;
                //}
            }

            return porter;
        }

        #endregion
    }
}
