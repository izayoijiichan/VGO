// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : StandardVColorMaterialPorter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using System.Collections.Generic;
    using UniShader.Shared;
    using UnityEngine;

    /// <summary>
    /// Standard Vertex Color Material Porter
    /// </summary>
    public class StandardVColorMaterialPorter : AbstractMaterialPorterBase
    {
        #region Constructors

        /// <summary>
        /// Create a new instance of StandardVColorMaterialPorter.
        /// </summary>
        public StandardVColorMaterialPorter() : base() { }

        #endregion

        #region Public Methods (Export)

        /// <summary>
        /// Create a vgo material.
        /// </summary>
        /// <param name="material">A standard material.</param>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <returns>A vgo material.</returns>
        public override VgoMaterial CreateVgoMaterial(in Material material, in IVgoStorage vgoStorage)
        {
            VgoMaterial vgoMaterial = new VgoMaterial()
            {
                name = material.name,
                shaderName = material.shader.name,
                renderQueue = material.renderQueue,
                isUnlit = false,
            };

            // Main Color
            ExportProperty(vgoMaterial, material, UniStandardShader.Property.Color, VgoMaterialPropertyType.Color4);

            // Main Texture
            ExportTextureProperty(vgoStorage, vgoMaterial, material, UniStandardShader.Property.MainTex, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);

            // Metallic Gloss
            ExportProperty(vgoMaterial, material, UniStandardShader.Property.Metallic, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, UniStandardShader.Property.Glossiness, VgoMaterialPropertyType.Float);

            return vgoMaterial;
        }

        #endregion

        #region Public Methods (Import)

        /// <summary>
        /// Create a standard vertex color material.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="shader">A standard shader.</param>
        /// <param name="allTextureList">List of all texture.</param>
        /// <returns>A standard vertex color material.</returns>
        public override Material CreateMaterialAsset(in VgoMaterial vgoMaterial, in Shader shader, in List<Texture?> allTextureList)
        {
            if (shader.name == ShaderName.URP_Lit)
            {
                return CreateMaterialAssetAsUrp(vgoMaterial, shader, allTextureList);
            }

            if (vgoMaterial.shaderName != ShaderName.UniGLTF_StandardVColor)
            {
                ThrowHelper.ThrowArgumentException($"vgoMaterial.shaderName: {vgoMaterial.shaderName}");
            }

            if (shader.name != ShaderName.UniGLTF_StandardVColor)
            {
                ThrowHelper.ThrowArgumentException($"shader.name: {shader.name}");
            }

            StandardVColorDefinition vgoMaterialParameter = vgoMaterial.ToStandardVColorDefinition(allTextureList);

            var material = new Material(shader)
            {
                name = vgoMaterial.name
            };

            material.SetSafeColor(UniStandardShader.Property.Color, vgoMaterialParameter.Color);

            if (vgoMaterialParameter.MainTex != null)
            {
                material.SetTexture(UniStandardShader.Property.MainTex, vgoMaterialParameter.MainTex);
            }

            material.SetTextureScale(UniStandardShader.Property.MainTex, vgoMaterialParameter.MainTexScale);

            material.SetTextureOffset(UniStandardShader.Property.MainTex, vgoMaterialParameter.MainTexOffset);

            material.SetSafeFloat(UniStandardShader.Property.Metallic, vgoMaterialParameter.Metallic, minValue: null, maxValue: null);

            material.SetSafeFloat(UniStandardShader.Property.Glossiness, vgoMaterialParameter.Glossiness, minValue: null, maxValue: null);

            return material;
        }

        #endregion

        #region Protected Methods (Import) BRP to URP

        /// <summary>
        /// Create a URP lit material.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="shader">A URP lit shader.</param>
        /// <param name="allTextureList">List of all texture.</param>
        /// <returns>A URP lit material.</returns>
        /// <remarks>
        /// @notice Universal Render Pipeline/Lit shader is not support vertex color.
        /// </remarks>
        protected virtual Material CreateMaterialAssetAsUrp(in VgoMaterial vgoMaterial, Shader shader, in List<Texture?> allTextureList)
        {
            var material = new Material(shader)
            {
                name = vgoMaterial.name
            };

            StandardVColorDefinition standardVColorDefinition = vgoMaterial.ToStandardVColorDefinition(allTextureList);

            UniUrpShader.UrpLitDefinition urpLitDefinition = standardVColorDefinition.ToUrpLitDefinition();

            UniUrpShader.Utils.SetParametersToMaterial(material, urpLitDefinition);

            return material;
        }

        #endregion
    }
}
