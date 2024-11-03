// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : VgoMaterialStandardVColorDefinitionExtensions
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using NewtonVgo;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Vgo Material Standard Vertex Color Definition Extensions
    /// </summary>
    public static class VgoMaterialStandardVColorDefinitionExtensions
    {
        /// <summary>
        /// Convert vgo material to standard vertex color definition.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="allTextureList">List of all texture.</param>
        /// <returns>A standard vertex color definition.</returns>
        public static StandardVColorDefinition ToStandardVColorDefinition(this VgoMaterial vgoMaterial, in List<Texture?> allTextureList)
        {
            if (vgoMaterial.shaderName != ShaderName.UniGLTF_StandardVColor)
            {
                ThrowHelper.ThrowException($"vgoMaterial.shaderName: {vgoMaterial.shaderName}");
            }

            var standardVColorDefinition = new StandardVColorDefinition
            {
                Color = vgoMaterial.GetColorOrDefault(UniStandardShader.Property.Color, Color.white),
                MainTex = null,
                MainTexScale = vgoMaterial.GetTextureScaleOrDefault(UniStandardShader.Property.MainTex, Vector2.one),
                MainTexOffset = vgoMaterial.GetTextureOffsetOrDefault(UniStandardShader.Property.MainTex, Vector2.zero),
                Metallic = vgoMaterial.GetSafeFloat(UniStandardShader.Property.Metallic, UniStandardShader.PropertyRange.Metallic),
                Glossiness = vgoMaterial.GetSafeFloat(UniStandardShader.Property.Glossiness, UniStandardShader.PropertyRange.Glossiness),
            };

            int mainTextureIndex = vgoMaterial.GetTextureIndexOrDefault(UniStandardShader.Property.MainTex);

            standardVColorDefinition.MainTex = allTextureList.GetNullableValueOrDefault(mainTextureIndex) as Texture2D;

            return standardVColorDefinition;
        }
    }
}
