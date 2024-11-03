// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : SkyboxMaterialPorter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using System.Collections.Generic;
    using UniSkyboxShader;
    using UnityEngine;

    /// <summary>
    /// Skybox Material Porter
    /// </summary>
    public class SkyboxMaterialPorter : AbstractMaterialPorterBase
    {
        #region Constructors

        /// <summary>
        /// Create a new instance of SkyboxMaterialPorter.
        /// </summary>
        public SkyboxMaterialPorter() : base() { }

        #endregion

        #region Public Methods (Export)

        /// <summary>
        /// Create a vgo material.
        /// </summary>
        /// <param name="material">A skybox material.</param>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <returns>A vgo material.</returns>
        public override VgoMaterial CreateVgoMaterial(in Material material, in IVgoStorage vgoStorage)
        {
            var vgoMaterial = new VgoMaterial
            {
                name = material.name,
                shaderName = material.shader.name,
                renderQueue = material.renderQueue,
                isUnlit = false,
            };

            switch (material.shader.name)
            {
                case ShaderName.Skybox_6_Sided:
                    //Skybox6SidedDefinition skyboxDefinition = UniSkyboxShader.Utils.GetParametersFromMaterial<Skybox6SidedDefinition>(material);
                    {
                        ExportProperty(vgoMaterial, material, Property.Tint, VgoMaterialPropertyType.Color4);
                        ExportProperty(vgoMaterial, material, Property.Exposure, VgoMaterialPropertyType.Float);
                        ExportProperty(vgoMaterial, material, Property.Rotation, VgoMaterialPropertyType.Int);
                        ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.FrontTex);
                        ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.BackTex);
                        ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.LeftTex);
                        ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.RightTex);
                        ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.UpTex);
                        ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.DownTex);
                    }
                    break;

                case ShaderName.Skybox_Cubemap:  // @todo Tex (Cubemap)
                    //SkyboxCubemapDefinition skyboxDefinition = UniSkyboxShader.Utils.GetParametersFromMaterial<SkyboxCubemapDefinition>(material);
                    {
                        ExportProperty(vgoMaterial, material, Property.Tint, VgoMaterialPropertyType.Color4);
                        ExportProperty(vgoMaterial, material, Property.Exposure, VgoMaterialPropertyType.Float);
                        ExportProperty(vgoMaterial, material, Property.Rotation, VgoMaterialPropertyType.Int);
                        ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.Tex, VgoTextureMapType.CubeMap);
                    }
                    break;

                case ShaderName.Skybox_Panoramic:
                    //SkyboxPanoramicDefinition skyboxDefinition = UniSkyboxShader.Utils.GetParametersFromMaterial<SkyboxPanoramicDefinition>(material);
                    {
                        ExportProperty(vgoMaterial, material, Property.Tint, VgoMaterialPropertyType.Color4);
                        ExportProperty(vgoMaterial, material, Property.Exposure, VgoMaterialPropertyType.Float);
                        ExportProperty(vgoMaterial, material, Property.Rotation, VgoMaterialPropertyType.Int);
                        ExportTextureProperty(vgoStorage, vgoMaterial, material, Property.MainTex);
                        ExportProperty(vgoMaterial, material, Property.Mapping, VgoMaterialPropertyType.Int);
                        ExportProperty(vgoMaterial, material, Property.ImageType, VgoMaterialPropertyType.Int);
                        ExportProperty(vgoMaterial, material, Property.MirrorOnBack, VgoMaterialPropertyType.Int);
                        ExportProperty(vgoMaterial, material, Property.Layout, VgoMaterialPropertyType.Int);
                    }
                    break;

                case ShaderName.Skybox_Procedural:
                    //SkyboxProceduralDefinition skyboxDefinition = UniSkyboxShader.Utils.GetParametersFromMaterial<SkyboxProceduralDefinition>(material);
                    {
                        ExportProperty(vgoMaterial, material, Property.SunDisk, VgoMaterialPropertyType.Int);
                        ExportProperty(vgoMaterial, material, Property.SunSize, VgoMaterialPropertyType.Float);
                        ExportProperty(vgoMaterial, material, Property.SunSizeConvergence, VgoMaterialPropertyType.Int);
                        ExportProperty(vgoMaterial, material, Property.AtmosphereThickness, VgoMaterialPropertyType.Float);
                        ExportProperty(vgoMaterial, material, Property.SkyTint, VgoMaterialPropertyType.Color4);
                        ExportProperty(vgoMaterial, material, Property.GroundColor, VgoMaterialPropertyType.Color4);
                        ExportProperty(vgoMaterial, material, Property.Exposure, VgoMaterialPropertyType.Float);
                    }
                    break;

                default:
                    ThrowHelper.ThrowNotSupportedException(material.shader.name);
                    return vgoMaterial;
            }

            // Tags
            //ExportTag(vgoMaterial, material, Tag.Queue);
            //ExportTag(vgoMaterial, material, Tag.RenderType);
            //ExportTag(vgoMaterial, material, Tag.PreviewType);

            // Keywords
            ExportKeywords(vgoMaterial, material);

            return vgoMaterial;
        }

        #endregion

        #region Public Methods (Import)

        /// <summary>
        /// Create a skybox material.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="shader">A skybox shader.</param>
        /// <param name="allTextureList">List of all texture.</param>
        /// <returns>A skybox material.</returns>
        public override Material CreateMaterialAsset(in VgoMaterial vgoMaterial, in Shader shader, in List<Texture?> allTextureList)
        {
            var material = new Material(shader)
            {
                name = vgoMaterial.name
            };

            if (vgoMaterial.renderQueue >= 0)
            {
                material.renderQueue = vgoMaterial.renderQueue;
            }

            ImportKeywords(material, vgoMaterial);

            switch (shader.name)
            {
                case ShaderName.Skybox_6_Sided:
                    {
                        var skyboxParameter = vgoMaterial.ToSkybox6SidedDefinition(allTextureList);

                        UniSkyboxShader.Utils.SetParametersToMaterial(material, skyboxParameter);
                    }
                    break;

                case ShaderName.Skybox_Cubemap:  // @todo Tex (Cubemap)
                    {
                        var skyboxParameter = vgoMaterial.ToSkyboxCubemapDefinition();

                        UniSkyboxShader.Utils.SetParametersToMaterial(material, skyboxParameter);
                    }
                    break;

                case ShaderName.Skybox_Panoramic:
                    {
                        var skyboxParameter = vgoMaterial.ToSkyboxPanoramicDefinition(allTextureList);

                        UniSkyboxShader.Utils.SetParametersToMaterial(material, skyboxParameter);
                    }
                    break;

                case ShaderName.Skybox_Procedural:
                    {
                        var skyboxParameter = vgoMaterial.ToSkyboxProceduralDefinition();

                        UniSkyboxShader.Utils.SetParametersToMaterial(material, skyboxParameter);
                    }
                    break;

                default:
                    break;
            }

            return material;
        }

        #endregion
    }
}
