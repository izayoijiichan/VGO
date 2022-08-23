// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : StandardMaterialPorter
// ----------------------------------------------------------------------
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using System.Collections.Generic;
    using UniStandardShader;
    using UnityEngine;

    /// <summary>
    /// Standard Material Porter
    /// </summary>
    public class StandardMaterialPorter : AbstractMaterialPorterBase
    {
        #region Constructors

        /// <summary>
        /// Create a new instance of StandardMaterialPorter.
        /// </summary>
        public StandardMaterialPorter() : base() { }

        #endregion

        #region Public Methods (Export)

        /// <summary>
        /// Create a vgo material.
        /// </summary>
        /// <param name="material">A standard material.</param>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <returns>A vgo material.</returns>
        public override VgoMaterial CreateVgoMaterial(Material material, IVgoStorage vgoStorage)
        {
            //StandardDefinition definition = UniStandardShader.Utils.GetParametersFromMaterial(material);

            VgoMaterial vgoMaterial = new VgoMaterial()
            {
                name = material.name,
                shaderName = material.shader.name,
                renderQueue = material.renderQueue,
                isUnlit = false,
            };

            // Mode
            ExportProperty(vgoMaterial, material, Property.Mode, VgoMaterialPropertyType.Int);

            // Main Color
            ExportProperty(vgoMaterial, material, Property.Color, VgoMaterialPropertyType.Color4);

            // Main Texture
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.MainTex, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);

            // Cutoff
            ExportProperty(vgoMaterial, material, Property.Cutoff, VgoMaterialPropertyType.Float);

            // Metallic Gloss Map
            float smoothness = material.HasProperty(Property.Glossiness) ? material.GetFloat(Property.Glossiness) : 0.0f;
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.MetallicGlossMap, VgoTextureMapType.MetallicRoughnessMap, VgoColorSpaceType.Linear, smoothness);
            ExportProperty(vgoMaterial, material, Property.Metallic, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, Property.Glossiness, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, Property.GlossMapScale, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, Property.SmoothnessTextureChannel, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, Property.SpecularHighlights, VgoMaterialPropertyType.Int);
            ExportProperty(vgoMaterial, material, Property.GlossyReflections, VgoMaterialPropertyType.Int);

            // Normal Map
            ExportProperty(vgoMaterial, material, Property.BumpScale, VgoMaterialPropertyType.Float);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.BumpMap, VgoTextureMapType.NormalMap, VgoColorSpaceType.Linear);

            // Parallax Map @notice
            ExportProperty(vgoMaterial, material, Property.Parallax, VgoMaterialPropertyType.Float);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.ParallaxMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);

            // Occlusion Map
            ExportProperty(vgoMaterial, material, Property.OcclusionStrength, VgoMaterialPropertyType.Float);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.OcclusionMap, VgoTextureMapType.OcclusionMap, VgoColorSpaceType.Linear);

            // Emission Map
            ExportProperty(vgoMaterial, material, Property.EmissionColor, VgoMaterialPropertyType.Color3);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.EmissionMap, VgoTextureMapType.EmissionMap, VgoColorSpaceType.Srgb);

            // Detail @notice
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.DetailMask, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.DetailAlbedoMap, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);
            ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.DetailNormalMap, VgoTextureMapType.NormalMap, VgoColorSpaceType.Linear);
            ExportProperty(vgoMaterial, material, Property.DetailNormalMapScale, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, Property.UVSec, VgoMaterialPropertyType.Int);

            //ExportProperty(vgoMaterial, material, Property.SrcBlend, MaterialPropertyType.Int);
            //ExportProperty(vgoMaterial, material, Property.DstBlend, MaterialPropertyType.Int);
            //ExportProperty(vgoMaterial, material, Property.ZWrite, MaterialPropertyType.Int);

            // Keywords
            //ExportKeyword(vgoMaterial, material, Keyword.AlphaTestOn);
            //ExportKeyword(vgoMaterial, material, Keyword.AlphaBlendOn);
            //ExportKeyword(vgoMaterial, material, Keyword.AlphaPreMultiplyOn);
            ExportKeyword(vgoMaterial, material, Keyword.NormalMap);
            ExportKeyword(vgoMaterial, material, Keyword.ParallaxMap);
            ExportKeyword(vgoMaterial, material, Keyword.Emission);
            ExportKeyword(vgoMaterial, material, Keyword.DetailMulx2);
            ExportKeyword(vgoMaterial, material, Keyword.MetallicGlossMap);
            //ExportKeyword(vgoMaterial, material, Keyword.SpecGlossMap);

            return vgoMaterial;
        }

        #endregion

        #region Public Methods (Import)

        /// <summary>
        /// Create a standard material.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="shader">A standard shader.</param>
        /// <param name="allTexture2dList">List of all texture 2D.</param>
        /// <returns>A standard material.</returns>
        public override Material CreateMaterialAsset(VgoMaterial vgoMaterial, Shader shader, List<Texture2D> allTexture2dList)
        {
            Material material = base.CreateMaterialAsset(vgoMaterial, shader, allTexture2dList);

            if (vgoMaterial.intProperties.TryGetValue(Property.Mode, out int mode))
            {
                UniStandardShader.Utils.SetMode(material, (AlphaMode)mode);
            }

            return material;
        }

        #endregion
    }
}
