// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : StandardVColorMaterialPorter
// ----------------------------------------------------------------------
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using UnityEngine;

    /// <summary>
    /// StandardVColor Material Porter
    /// </summary>
    public class StandardVColorMaterialPorter : MaterialPorterBase
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
        /// <returns>A vgo material.</returns>
        public override VgoMaterial CreateVgoMaterial(Material material)
        {
            VgoMaterial vgoMaterial = new VgoMaterial()
            {
                name = material.name,
                shaderName = material.shader.name,
                isUnlit = false,
            };

            // Main Color
            ExportProperty(vgoMaterial, material, UniStandardShader.Property.Color, VgoMaterialPropertyType.Color4);

            // Main Texture
            ExportTextureProperty(vgoMaterial, material, UniStandardShader.Property.MainTex, VgoTextureMapType.Default, VgoColorSpaceType.Srgb);

            // Metallic Gloss
            ExportProperty(vgoMaterial, material, UniStandardShader.Property.Metallic, VgoMaterialPropertyType.Float);
            ExportProperty(vgoMaterial, material, UniStandardShader.Property.Glossiness, VgoMaterialPropertyType.Float);

            return vgoMaterial;
        }

        #endregion
    }
}
