// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : UrpMaterialPorter
// ----------------------------------------------------------------------
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using System;
    using UniUrpShader;
    using UnityEngine;

    /// <summary>
    /// URP Material Porter
    /// </summary>
    public class UrpMaterialPorter : MaterialPorterBase
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
        /// <returns>A vgo material.</returns>
        public override VgoMaterial CreateVgoMaterial(Material material)
        {
            switch (material.shader.name)
            {
                case ShaderName.URP_Lit:
                    return CreateVgoMaterialFromUrpLit(material);
                default:
                    throw new NotSupportedException(material.shader.name);
            }
        }

        /// <summary>
        /// Create a vgo material from a URP/Lit material.
        /// </summary>
        /// <param name="material">A URP/Lit material.</param>
        /// <returns>A vgo material.</returns>
        protected VgoMaterial CreateVgoMaterialFromUrpLit(Material material)
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

            ExportTextureProperty(vgoMaterial, material, Property.BaseMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
            ExportTextureProperty(vgoMaterial, material, Property.BumpMap, VgoTextureMapType.NormalMap, VgoColorSpaceType.Linear);
            ExportTextureProperty(vgoMaterial, material, Property.ParallaxMap, VgoTextureMapType.HeightMap, VgoColorSpaceType.Linear);
            ExportTextureProperty(vgoMaterial, material, Property.EmissionMap, VgoTextureMapType.EmissionMap, VgoColorSpaceType.Srgb);

            // @notice Metallic Occlusion Map (Occlusion -> Metallic)
            ExportTextureProperty(vgoMaterial, material, Property.OcclusionMap, VgoTextureMapType.OcclusionMap, VgoColorSpaceType.Linear);
            ExportTextureProperty(vgoMaterial, material, Property.MetallicGlossMap, VgoTextureMapType.MetallicRoughnessMap, VgoColorSpaceType.Linear, smoothness);
            ExportTextureProperty(vgoMaterial, material, Property.SpecGlossMap, VgoTextureMapType.MetallicRoughnessMap, VgoColorSpaceType.Linear, smoothness);

            ExportTextureProperty(vgoMaterial, material, Property.DetailMask, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
            ExportTextureProperty(vgoMaterial, material, Property.DetailAlbedoMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
            ExportTextureProperty(vgoMaterial, material, Property.DetailNormalMap, VgoTextureMapType.NormalMap, VgoColorSpaceType.Linear);

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
        /// <returns>A URP material.</returns>
        public override Material CreateMaterialAsset(VgoMaterial vgoMaterial, Shader shader)
        {
            switch (vgoMaterial.shaderName)
            {
                case ShaderName.URP_Lit:
                    break;
                default:
                    throw new NotSupportedException(vgoMaterial.shaderName);
            }

            Material material = base.CreateMaterialAsset(vgoMaterial, shader);

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
