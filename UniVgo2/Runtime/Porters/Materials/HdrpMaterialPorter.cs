// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : HdrpMaterialPorter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using System;
    using System.Collections.Generic;
    using UniHdrpShader;
    using UnityEngine;

    /// <summary>
    /// HDRP Material Porter
    /// </summary>
    public class HdrpMaterialPorter : AbstractMaterialPorterBase
    {
        #region Constructors

        /// <summary>
        /// Create a new instance of HdrpMaterialPorter.
        /// </summary>
        public HdrpMaterialPorter() : base() { }

        #endregion

        #region Public Methods (Export)

        /// <summary>
        /// Create a vgo material.
        /// </summary>
        /// <param name="material">A HDRP material.</param>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <returns>A vgo material.</returns>
        public override VgoMaterial CreateVgoMaterial(in Material material, in IVgoStorage vgoStorage)
        {
            switch (material.shader.name)
            {
                case ShaderName.HDRP_Eye:
                    return CreateVgoMaterialFromHdrpEye(material, vgoStorage);

                case ShaderName.HDRP_Hair:
                    return CreateVgoMaterialFromHdrpHair(material, vgoStorage);

                case ShaderName.HDRP_Lit:
                    return CreateVgoMaterialFromHdrpLit(material, vgoStorage);

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
        /// Create a vgo material from a HDRP/Eye material.
        /// </summary>
        /// <param name="material">A HDRP/Eye material.</param>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <returns>A vgo material.</returns>
        protected VgoMaterial CreateVgoMaterialFromHdrpEye(in Material material, in IVgoStorage vgoStorage)
        {
            //HdrpEyeDefinition definition = UniHdrpShader.Utils.GetParametersFromMaterial<HdrpEyeDefinition>(material);

            VgoMaterial vgoMaterial = new VgoMaterial()
            {
                name = material.name,
                shaderName = material.shader.name,
                renderQueue = material.renderQueue,
                isUnlit = false,
            };

            ExportProperties(vgoMaterial, material);

            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.Texture2D_5F873FC1, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.Texture2D_B9F5688C, VgoTextureMapType.NormalMap, VgoColorSpaceType.Linear);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.Texture2D_D8BF6575, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.Texture2D_4DB28C10, VgoTextureMapType.NormalMap, VgoColorSpaceType.Linear);

            ExportKeywords(vgoMaterial, material);

            return vgoMaterial;
        }

        /// <summary>
        /// Create a vgo material from a HDRP/Hair material.
        /// </summary>
        /// <param name="material">A HDRP/Hair material.</param>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <returns>A vgo material.</returns>
        protected VgoMaterial CreateVgoMaterialFromHdrpHair(in Material material, in IVgoStorage vgoStorage)
        {
            //HdrpHairDefinition definition = UniHdrpShader.Utils.GetParametersFromMaterial<HdrpHairDefinition>(material);

            VgoMaterial vgoMaterial = new VgoMaterial()
            {
                name = material.name,
                shaderName = material.shader.name,
                renderQueue = material.renderQueue,
                isUnlit = false,
            };

            ExportProperties(vgoMaterial, material);

            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.BaseColorMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.NormalMap, VgoTextureMapType.NormalMap, VgoColorSpaceType.Linear);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.MaskMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.SmoothnessMask, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);

            ExportKeywords(vgoMaterial, material);

            return vgoMaterial;
        }

        /// <summary>
        /// Create a vgo material from a HDRP/Lit material.
        /// </summary>
        /// <param name="material">A HDRP/Lit material.</param>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <returns>A vgo material.</returns>
        protected VgoMaterial CreateVgoMaterialFromHdrpLit(in Material material, in IVgoStorage vgoStorage)
        {
            //HdrpLitDefinition definition = UniHdrpShader.Utils.GetParametersFromMaterial<HdrpLitDefinition>(material);

            VgoMaterial vgoMaterial = new VgoMaterial()
            {
                name = material.name,
                shaderName = material.shader.name,
                renderQueue = material.renderQueue,
                isUnlit = false,
            };

            ExportProperties(vgoMaterial, material);

            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.BaseColorMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.MaskMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.NormalMap, VgoTextureMapType.NormalMap, VgoColorSpaceType.Linear);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.NormalMapOS, VgoTextureMapType.NormalMap, VgoColorSpaceType.Linear);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.HeightMap, VgoTextureMapType.HeightMap, VgoColorSpaceType.Linear);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.DetailMap, VgoTextureMapType.Default, VgoColorSpaceType.Linear);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.TangentMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.TangentMapOS, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.AnisotropyMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.SubsurfaceMaskMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.ThicknessMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.IridescenceThicknessMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.IridescenceMaskMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.CoatMaskMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.SpecularColorMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.EmissiveColorMap, VgoTextureMapType.EmissionMap, VgoColorSpaceType.Srgb);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.DistortionVectorMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.TransmittanceColorMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.MainTex, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);

            ExportKeywords(vgoMaterial, material);

            return vgoMaterial;
        }

        #endregion

        #region Public Methods (Import)

        /// <summary>
        /// Create a HDRP material.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="shader">A HDRP shader.</param>
        /// <param name="allTextureList">List of all texture.</param>
        /// <returns>A HDRP material.</returns>
        public override Material CreateMaterialAsset(in VgoMaterial vgoMaterial, in Shader shader, in List<Texture?> allTextureList)
        {
            switch (vgoMaterial.shaderName)
            {
                case ShaderName.HDRP_Eye:
                    break;
                case ShaderName.HDRP_Hair:
                    break;
                case ShaderName.HDRP_Lit:
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
                if (vgoMaterial.intProperties.TryGetValue(Property.SurfaceType, out int intSurfaceType))
                {
                    surfaceType = (SurfaceType)intSurfaceType;
                }
                if (vgoMaterial.intProperties.TryGetValue(Property.BlendMode, out int intBlendMode))
                {
                    blendMode = (BlendMode)intBlendMode;
                }
            }

            if (vgoMaterial.floatProperties != null)
            {
                if (vgoMaterial.floatProperties.TryGetValue(Property.SurfaceType, out float floatSurfaceType))
                {
                    surfaceType = (SurfaceType)Convert.ToInt32(floatSurfaceType);
                }

                if (vgoMaterial.floatProperties.TryGetValue(Property.BlendMode, out float floatBlendMode))
                {
                    blendMode = (BlendMode)Convert.ToInt32(floatBlendMode);
                }
            }

            if (surfaceType.HasValue)
            {
                if (blendMode.HasValue)
                {
                    //UniHdrpShader.Utils.SetSurfaceType(material, surfaceType.Value, blendMode.Value);
                }
                else
                {
                    //UniHdrpShader.Utils.SetSurfaceType(material, surfaceType.Value);
                }
            }

            return material;
        }

        #endregion
    }
}
