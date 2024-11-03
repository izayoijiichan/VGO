// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : NovaMaterialPorter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Porters
{
#if IZAYOI_NOVA_SHADER_UTILITY_2_0_OR_NEWER
    using Izayoi.NovaShader;
    using Izayoi.NovaShader.Extensions;
    using NovaShader = Izayoi.NovaShader;
#endif
    using NewtonVgo;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// NOVA Shader Material Porter
    /// </summary>
    /// <remarks>
    /// The following particle system features are not yet implemented, so it is not complete.
    /// - VgoParticleSystemExporter
    /// - VgoParticleSystemImporter
    /// </remarks>
    public class NovaMaterialPorter : AbstractMaterialPorterBase
    {
        #region Constructors

        /// <summary>
        /// Create a new instance of NovaMaterialPorter.
        /// </summary>
        public NovaMaterialPorter() : base() { }

        #endregion

        #region Public Methods (Export)

        /// <summary>
        /// Create a vgo material.
        /// </summary>
        /// <param name="material">A URP material.</param>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <returns>A vgo material.</returns>
        public override VgoMaterial CreateVgoMaterial(in Material material, in IVgoStorage vgoStorage)
        {
            switch (material.shader.name)
            {
#if IZAYOI_NOVA_SHADER_UTILITY_2_0_OR_NEWER
                // Opaque
                case ShaderName.Nova_Particles_UberLit:
                case ShaderName.Nova_UIParticles_UberLit:
                    return CreateVgoMaterialFromNova(material, vgoStorage, NovaPropertyEntityType.UberLit);
                case ShaderName.Nova_Particles_UberUnlit:
                case ShaderName.Nova_UIParticles_UberUnlit:
                    return CreateVgoMaterialFromNova(material, vgoStorage, NovaPropertyEntityType.UberUnlit);

                // Cutout
                case ShaderName.Nova_Particles_Distortion:
                    return CreateVgoMaterialFromNova(material, vgoStorage, NovaPropertyEntityType.Distortion);
#endif
                default:
#if NET_STANDARD_2_1
                    ThrowHelper.ThrowNotSupportedException(material.shader.name);
                    return default;
#else
                    throw new NotSupportedException(material.shader.name);
#endif
            }
        }

        #endregion

#if IZAYOI_NOVA_SHADER_UTILITY_2_0_OR_NEWER

        #region Protected Methods (Export)

        /// <summary>
        /// Create a vgo material from a NOVA material.
        /// </summary>
        /// <param name="material">A NOVA material.</param>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="propertyEntityType"></param>
        /// <returns>A vgo material.</returns>
        protected virtual VgoMaterial CreateVgoMaterialFromNova(
            in Material material,
            in IVgoStorage vgoStorage,
            in NovaPropertyEntityType propertyEntityType)
        {
            var vgoMaterial = new VgoMaterial()
            {
                name = material.name,
                shaderName = material.shader.name,
                renderQueue = material.renderQueue,
                isUnlit = propertyEntityType == NovaPropertyEntityType.UberUnlit,
            };

            ExportProperties(vgoMaterial, material);

            ExportNovaTextureProperties(vgoStorage, vgoMaterial, material, propertyEntityType);

            // Tags
            ExportTag(vgoMaterial, material, Tag.RenderType);
            ExportTag(vgoMaterial, material, Tag.RenderPipeline);

            // Keywords
            ExportKeywords(vgoMaterial, material);

            return vgoMaterial;
        }

        /// <summary>
        /// Export NOVA texture type properties.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="material">A NOVA material.</param>
        /// <param name="propertyEntityType"></param>
        protected virtual void ExportNovaTextureProperties(
            IVgoStorage vgoStorage,
            VgoMaterial vgoMaterial,
            in Material material,
            in NovaPropertyEntityType propertyEntityType)
        {
            // Base Map
            if (propertyEntityType == NovaPropertyEntityType.Distortion)
            {
                base.ExportTextureProperty(vgoStorage, vgoMaterial, material, NovaShader.PropertyName.BaseMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
            }
            else
            {
                NovaBaseMapMode baseMapMode = material.GetSafeEnum<NovaBaseMapMode>(NovaShader.PropertyName.BaseMapMode);

                if (baseMapMode == NovaBaseMapMode.SingleTexture)
                {
                    base.ExportTextureProperty(vgoStorage, vgoMaterial, material, NovaShader.PropertyName.BaseMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                }
                else if (baseMapMode == NovaBaseMapMode.FlipBook)
                {
                    base.ExportTextureProperty(vgoStorage, vgoMaterial, material, NovaShader.PropertyName.BaseMap2DArray, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                }
                else if (baseMapMode == NovaBaseMapMode.FlipBookBlending)
                {
                    base.ExportTextureProperty(vgoStorage, vgoMaterial, material, NovaShader.PropertyName.BaseMap3D, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                }
            }

            // Surface Maps
            if (propertyEntityType == NovaPropertyEntityType.UberLit)
            {
                float metallic = material.GetSafeFloat(NovaShader.PropertyNameID.Metallic, -1.0f);
                float smoothness = material.GetSafeFloat(NovaShader.PropertyNameID.Smoothness, -1.0f);

                // Normal Map
                base.ExportTextureProperty(vgoStorage, vgoMaterial, material, NovaShader.PropertyName.NormalMap, VgoTextureMapType.NormalMap, VgoColorSpaceType.Linear);
                base.ExportTextureProperty(vgoStorage, vgoMaterial, material, NovaShader.PropertyName.NormalMap2DArray, VgoTextureMapType.NormalMap, VgoColorSpaceType.Linear);
                base.ExportTextureProperty(vgoStorage, vgoMaterial, material, NovaShader.PropertyName.NormalMap3D, VgoTextureMapType.NormalMap, VgoColorSpaceType.Linear);

                // Specular Map
                base.ExportTextureProperty(vgoStorage, vgoMaterial, material, NovaShader.PropertyName.SpecularMap, VgoTextureMapType.SpecularGlossinessMap, VgoColorSpaceType.Linear);
                base.ExportTextureProperty(vgoStorage, vgoMaterial, material, NovaShader.PropertyName.SpecularMap2DArray, VgoTextureMapType.SpecularGlossinessMap, VgoColorSpaceType.Linear);
                base.ExportTextureProperty(vgoStorage, vgoMaterial, material, NovaShader.PropertyName.SpecularMap3D, VgoTextureMapType.SpecularGlossinessMap, VgoColorSpaceType.Linear);

                // Metallic Map
                base.ExportTextureProperty(vgoStorage, vgoMaterial, material, NovaShader.PropertyName.MetallicMap, VgoTextureMapType.SpecularGlossinessMap, VgoColorSpaceType.Linear, metallic);
                base.ExportTextureProperty(vgoStorage, vgoMaterial, material, NovaShader.PropertyName.MetallicMap2DArray, VgoTextureMapType.SpecularGlossinessMap, VgoColorSpaceType.Linear, metallic);
                base.ExportTextureProperty(vgoStorage, vgoMaterial, material, NovaShader.PropertyName.MetallicMap3D, VgoTextureMapType.SpecularGlossinessMap, VgoColorSpaceType.Linear, metallic);

                // Smoothness Map
                base.ExportTextureProperty(vgoStorage, vgoMaterial, material, NovaShader.PropertyName.SmoothnessMap, VgoTextureMapType.SpecularGlossinessMap, VgoColorSpaceType.Linear, smoothness);
                base.ExportTextureProperty(vgoStorage, vgoMaterial, material, NovaShader.PropertyName.SmoothnessMap2DArray, VgoTextureMapType.SpecularGlossinessMap, VgoColorSpaceType.Linear, smoothness);
                base.ExportTextureProperty(vgoStorage, vgoMaterial, material, NovaShader.PropertyName.SmoothnessMap3D, VgoTextureMapType.SpecularGlossinessMap, VgoColorSpaceType.Linear, smoothness);
            }

            // Tint
            if (propertyEntityType != NovaPropertyEntityType.Distortion)
            {
                NovaTintAreaMode tintAreaMode = material.GetSafeEnum<NovaTintAreaMode>(NovaShader.PropertyName.TintAreaMode);

                if (tintAreaMode != NovaTintAreaMode.None)
                {
                    NovaTintColorMode tintColorMode = material.GetSafeEnum<NovaTintColorMode>(NovaShader.PropertyName.TintColorMode);

                    // Tint Map
                    if (tintColorMode == NovaTintColorMode.Texture2D)
                    {
                        base.ExportTextureProperty(vgoStorage, vgoMaterial, material, NovaShader.PropertyName.TintMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                    }
                    else if (tintColorMode == NovaTintColorMode.Texture3D)
                    {
                        base.ExportTextureProperty(vgoStorage, vgoMaterial, material, NovaShader.PropertyName.TintMap3D, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                    }
                }
            }

            // Flow Map
            {
                base.ExportTextureProperty(vgoStorage, vgoMaterial, material, NovaShader.PropertyName.FlowMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
            }

            // Parallax Map
            if (propertyEntityType != NovaPropertyEntityType.Distortion)
            {
                NovaParallaxMapMode parallaxMapMode = material.GetSafeEnum<NovaParallaxMapMode>(NovaShader.PropertyName.ParallaxMapMode);

                if (parallaxMapMode == NovaParallaxMapMode.SingleTexture)
                {
                    base.ExportTextureProperty(vgoStorage, vgoMaterial, material, NovaShader.PropertyName.ParallaxMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                }
                else if (parallaxMapMode == NovaParallaxMapMode.FlipBook)
                {
                    base.ExportTextureProperty(vgoStorage, vgoMaterial, material, NovaShader.PropertyName.ParallaxMap2DArray, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                }
                else if (parallaxMapMode == NovaParallaxMapMode.FlipBookBlending)
                {
                    base.ExportTextureProperty(vgoStorage, vgoMaterial, material, NovaShader.PropertyName.ParallaxMap3D, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                }
            }

            // Color Correction
            if (propertyEntityType != NovaPropertyEntityType.Distortion)
            {
                NovaColorCorrectionMode colorCorrectionMode = material.GetSafeEnum<NovaColorCorrectionMode>(NovaShader.PropertyName.ColorCorrectionMode);

                // Gradient Map
                if (colorCorrectionMode == NovaColorCorrectionMode.GradientMap)
                {
                    base.ExportTextureProperty(vgoStorage, vgoMaterial, material, NovaShader.PropertyName.GradientMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                }
            }

            // Alpha Transition
            {
                NovaAlphaTransitionMode alphaTransitionMode = material.GetSafeEnum<NovaAlphaTransitionMode>(NovaShader.PropertyName.AlphaTransitionMode);

                if (alphaTransitionMode != NovaAlphaTransitionMode.None)
                {
                    // Alpha Transition Map
                    if (propertyEntityType == NovaPropertyEntityType.Distortion)
                    {
                        base.ExportTextureProperty(vgoStorage, vgoMaterial, material, NovaShader.PropertyName.AlphaTransitionMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                    }
                    else
                    {
                        NovaAlphaTransitionMapMode alphaTransitionMapMode = material.GetSafeEnum<NovaAlphaTransitionMapMode>(NovaShader.PropertyName.AlphaTransitionMapMode);

                        if (alphaTransitionMapMode == NovaAlphaTransitionMapMode.SingleTexture)
                        {
                            base.ExportTextureProperty(vgoStorage, vgoMaterial, material, NovaShader.PropertyName.AlphaTransitionMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                        }
                        else if (alphaTransitionMapMode == NovaAlphaTransitionMapMode.FlipBook)
                        {
                            base.ExportTextureProperty(vgoStorage, vgoMaterial, material, NovaShader.PropertyName.AlphaTransitionMap2DArray, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                        }
                        else if (alphaTransitionMapMode == NovaAlphaTransitionMapMode.FlipBookBlending)
                        {
                            base.ExportTextureProperty(vgoStorage, vgoMaterial, material, NovaShader.PropertyName.AlphaTransitionMap3D, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                        }
                    }
                }
            }

            // Emission
            if (propertyEntityType != NovaPropertyEntityType.Distortion)
            {
                NovaEmissionAreaType emissionAreaType = material.GetSafeEnum<NovaEmissionAreaType>(NovaShader.PropertyName.EmissionAreaType);

                // Emission Map
                if (emissionAreaType == NovaEmissionAreaType.ByTexture)
                {
                    NovaEmissionMapMode emissionMapMode = material.GetSafeEnum<NovaEmissionMapMode>(NovaShader.PropertyName.EmissionMapMode);

                    if (emissionMapMode == NovaEmissionMapMode.SingleTexture)
                    {
                        base.ExportTextureProperty(vgoStorage, vgoMaterial, material, NovaShader.PropertyName.EmissionMap, VgoTextureMapType.EmissionMap, VgoColorSpaceType.Srgb);
                    }
                    else if (emissionMapMode == NovaEmissionMapMode.FlipBook)
                    {
                        base.ExportTextureProperty(vgoStorage, vgoMaterial, material, NovaShader.PropertyName.EmissionMap2DArray, VgoTextureMapType.EmissionMap, VgoColorSpaceType.Srgb);
                    }
                    else if (emissionMapMode == NovaEmissionMapMode.FlipBookBlending)
                    {
                        base.ExportTextureProperty(vgoStorage, vgoMaterial, material, NovaShader.PropertyName.EmissionMap3D, VgoTextureMapType.EmissionMap, VgoColorSpaceType.Srgb);
                    }
                }

                // Emission Color Ramp
                if (emissionAreaType != NovaEmissionAreaType.None)
                {
                    NovaEmissionColorType emissionColorType = material.GetSafeEnum<NovaEmissionColorType>(NovaShader.PropertyName.EmissionColorType);

                    if (emissionColorType == NovaEmissionColorType.GradientMap)
                    {
                        base.ExportTextureProperty(vgoStorage, vgoMaterial, material, NovaShader.PropertyName.EmissionColorRamp, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                    }
                }
            }

            // Vertex Deformation Map
            if (propertyEntityType != NovaPropertyEntityType.Distortion)
            {
                if (material.GetSafeBool(NovaShader.PropertyName.VertexDeformationEnabled))
                {
                    base.ExportTextureProperty(vgoStorage, vgoMaterial, material, NovaShader.PropertyName.VertexDeformationMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
                }
            }
        }

        #endregion
#endif

        #region Public Methods (Import)

        /// <summary>
        /// Create a lilToon material.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="shader">A lilToon shader.</param>
        /// <param name="allTextureList">List of all texture.</param>
        /// <returns>A lilToon material.</returns>
        public override Material CreateMaterialAsset(in VgoMaterial vgoMaterial, in Shader shader, in List<Texture?> allTextureList)
        {
#if IZAYOI_NOVA_SHADER_UTILITY_2_0_OR_NEWER
            if (shader.name.StartsWith("Nova/Particles/") == false)
            {
#if NET_STANDARD_2_1
                ThrowHelper.ThrowNotSupportedException(vgoMaterial?.shaderName ?? string.Empty);
#else
                throw new NotSupportedException(vgoMaterial.shaderName ?? string.Empty);
#endif
            }

            Material material = base.CreateMaterialAsset(vgoMaterial, shader, allTextureList);

            return material;
#else
#if NET_STANDARD_2_1
            ThrowHelper.ThrowNotSupportedException(vgoMaterial.shaderName ?? string.Empty);
            return default;
#else
            throw new NotSupportedException(vgoMaterial.shaderName ?? string.Empty);
#endif
#endif
        }

        #endregion
    }
}
