﻿// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : UrpMaterialPorter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using System;
    using UniUrpShader;
    using UnityEngine;
    using System.Collections.Generic;

    /// <summary>
    /// URP Material Porter
    /// </summary>
    public class UrpMaterialPorter : AbstractMaterialPorterBase
    {
        #region Constructors

        /// <summary>
        /// Create a new instance of UrpMaterialPorter.
        /// </summary>
        public UrpMaterialPorter() : base() { }

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
                case ShaderName.URP_Lit:
                    return CreateVgoMaterialFromUrpLit(material, vgoStorage);
                case ShaderName.URP_SimpleLit:
                    return CreateVgoMaterialFromUrpSimpleLit(material, vgoStorage);
                case ShaderName.URP_Unlit:
                    return CreateVgoMaterialFromUrpUnlit(material, vgoStorage);
                default:
#if NET_STANDARD_2_1
                    ThrowHelper.ThrowNotSupportedException(material.shader.name);
                    return default;
#else
                    throw new NotSupportedException(material.shader.name);
#endif
            }
        }

        /// <summary>
        /// Create a vgo material from a URP/Lit material.
        /// </summary>
        /// <param name="material">A URP/Lit material.</param>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <returns>A vgo material.</returns>
        protected VgoMaterial CreateVgoMaterialFromUrpLit(in Material material, in IVgoStorage vgoStorage)
        {
            //UrpLitDefinition definition = UniUrpShader.Utils.GetParametersFromMaterial<UrpLitDefinition>(material);

            var vgoMaterial = new VgoMaterial()
            {
                name = material.name,
                shaderName = material.shader.name,
                renderQueue = material.renderQueue,
                isUnlit = false,
            };

            ExportProperties(vgoMaterial, material);

            float smoothness = -1.0f;

            if (material.HasProperty(Property.Smoothness))
            {
                smoothness = material.GetFloat(Property.Smoothness);
            }
            else
            {
                Debug.LogWarning($"{material.shader.name} does not have {Property.Smoothness} property.");
            }

            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.BaseMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.BumpMap, VgoTextureMapType.NormalMap, VgoColorSpaceType.Linear);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.ParallaxMap, VgoTextureMapType.HeightMap, VgoColorSpaceType.Linear);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.EmissionMap, VgoTextureMapType.EmissionMap, VgoColorSpaceType.Srgb);

            // @notice Metallic Occlusion Map (Occlusion -> Metallic)
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.OcclusionMap, VgoTextureMapType.OcclusionMap, VgoColorSpaceType.Linear);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.MetallicGlossMap, VgoTextureMapType.MetallicRoughnessMap, VgoColorSpaceType.Linear, smoothness);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.SpecGlossMap, VgoTextureMapType.SpecularGlossinessMap, VgoColorSpaceType.Linear, smoothness);

            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.DetailMask, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.DetailAlbedoMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.DetailNormalMap, VgoTextureMapType.NormalMap, VgoColorSpaceType.Linear);

            // Tags
            ExportTag(vgoMaterial, material, Tag.RenderType);
            ExportTag(vgoMaterial, material, Tag.RenderPipeline);
            ExportTag(vgoMaterial, material, Tag.UniversalMaterialType);
            ExportTag(vgoMaterial, material, Tag.IgnoreProjector);

            ExportKeywords(vgoMaterial, material);

            return vgoMaterial;
        }

        /// <summary>
        /// Create a vgo material from a URP/Simple Lit material.
        /// </summary>
        /// <param name="material">A URP/Simple Lit material.</param>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <returns>A vgo material.</returns>
        protected VgoMaterial CreateVgoMaterialFromUrpSimpleLit(in Material material, in IVgoStorage vgoStorage)
        {
            //UrpSimpleLitDefinition definition = UniUrpShader.Utils.GetParametersFromMaterial<UrpSimpleLitDefinition>(material);

            var vgoMaterial = new VgoMaterial()
            {
                name = material.name,
                shaderName = material.shader.name,
                renderQueue = material.renderQueue,
                isUnlit = false,
            };

            ExportProperties(vgoMaterial, material);

            float smoothness = -1.0f;

            if (material.HasProperty(Property.Smoothness))
            {
                smoothness = material.GetFloat(Property.Smoothness);
            }
            //else if (material.HasProperty(Property.SmoothnessSource))
            //{
            //    smoothness = material.GetFloat(Property.SmoothnessSource);
            //}
            else
            {
                Debug.LogWarning($"{material.shader.name} does not have {Property.Smoothness} property.");
            }

            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.BaseMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.BumpMap, VgoTextureMapType.NormalMap, VgoColorSpaceType.Linear);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.EmissionMap, VgoTextureMapType.EmissionMap, VgoColorSpaceType.Srgb);

            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.SpecGlossMap, VgoTextureMapType.SpecularGlossinessMap, VgoColorSpaceType.Linear, smoothness);

            // Tags
            ExportTag(vgoMaterial, material, Tag.RenderType);
            ExportTag(vgoMaterial, material, Tag.RenderPipeline);
            ExportTag(vgoMaterial, material, Tag.UniversalMaterialType);
            ExportTag(vgoMaterial, material, Tag.IgnoreProjector);

            ExportKeywords(vgoMaterial, material);

            return vgoMaterial;
        }

        /// <summary>
        /// Create a vgo material from a URP/Unlit material.
        /// </summary>
        /// <param name="material">A URP/Unlit material.</param>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <returns>A vgo material.</returns>
        protected VgoMaterial CreateVgoMaterialFromUrpUnlit(in Material material, in IVgoStorage vgoStorage)
        {
            //UrpUnlitDefinition definition = UniUrpShader.Utils.GetParametersFromMaterial<UrpUnlitDefinition>(material);

            var vgoMaterial = new VgoMaterial()
            {
                name = material.name,
                shaderName = material.shader.name,
                renderQueue = material.renderQueue,
                isUnlit = true,
            };

            ExportProperties(vgoMaterial, material);

            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.BaseMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);

            // Tags
            ExportTag(vgoMaterial, material, Tag.RenderType);
            ExportTag(vgoMaterial, material, Tag.RenderPipeline);
            ExportTag(vgoMaterial, material, Tag.IgnoreProjector);

            ExportKeywords(vgoMaterial, material);

            return vgoMaterial;
        }

        #endregion

        #region Public Methods (Import)

        /// <summary>
        /// Create a URP material.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="shader">A URP shader.</param>
        /// <param name="allTextureList">List of all texture.</param>
        /// <returns>A URP material.</returns>
        public override Material CreateMaterialAsset(in VgoMaterial vgoMaterial, in Shader shader, in List<Texture?> allTextureList)
        {
            switch (vgoMaterial.shaderName)
            {
                case ShaderName.URP_Lit:
                    break;
                case ShaderName.URP_SimpleLit:
                    break;
                case ShaderName.URP_Unlit:
                    break;
                default:
#if NET_STANDARD_2_1
                    ThrowHelper.ThrowNotSupportedException(vgoMaterial?.shaderName ?? string.Empty);
                    break;
#else
                    throw new NotSupportedException(vgoMaterial.shaderName ?? string.Empty);
#endif

            }

            Material material = base.CreateMaterialAsset(vgoMaterial, shader, allTextureList);

            SurfaceType? surfaceType = null;
            BlendMode? blendMode = null;

            if (vgoMaterial.intProperties != null)
            {
                if (vgoMaterial.intProperties.TryGetValue(Property.Surface, out int intSurfaceType))
                {
                    surfaceType = (SurfaceType)intSurfaceType;
                }
                if (vgoMaterial.intProperties.TryGetValue(Property.Blend, out int intBlendMode))
                {
                    blendMode = (BlendMode)intBlendMode;
                }
            }

            if (vgoMaterial.floatProperties != null)
            {
                if (vgoMaterial.floatProperties.TryGetValue(Property.Surface, out float floatSurfaceType))
                {
                    surfaceType = (SurfaceType)Convert.ToInt32(floatSurfaceType);
                }

                if (vgoMaterial.floatProperties.TryGetValue(Property.Blend, out float floatBlendMode))
                {
                    blendMode = (BlendMode)Convert.ToInt32(floatBlendMode);
                }
            }

            if (surfaceType.HasValue)
            {
                if (blendMode.HasValue)
                {
                    //UniUrpShader.Utils.SetSurfaceType(material, surfaceType.Value, blendMode.Value);
                }
                else
                {
                    //UniUrpShader.Utils.SetSurfaceType(material, surfaceType.Value);
                }
            }

            return material;
        }

        #endregion
    }
}
