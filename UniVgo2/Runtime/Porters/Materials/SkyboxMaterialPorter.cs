// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : SkyboxMaterialPorter
// ----------------------------------------------------------------------
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using System;
    using UniSkyboxShader;
    using UnityEngine;

    /// <summary>
    /// Skybox Material Porter
    /// </summary>
    public class SkyboxMaterialPorter : MaterialPorterBase
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
        /// <returns>A vgo material.</returns>
        public override VgoMaterial CreateVgoMaterial(Material material)
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
                        ExportTextureProperty(vgoMaterial, material, Property.FrontTex);
                        ExportTextureProperty(vgoMaterial, material, Property.BackTex);
                        ExportTextureProperty(vgoMaterial, material, Property.LeftTex);
                        ExportTextureProperty(vgoMaterial, material, Property.RightTex);
                        ExportTextureProperty(vgoMaterial, material, Property.UpTex);
                        ExportTextureProperty(vgoMaterial, material, Property.DownTex);
                    }
                    break;

                case ShaderName.Skybox_Cubemap:  // @todo Tex (Cubemap)
                    //SkyboxCubemapDefinition skyboxDefinition = UniSkyboxShader.Utils.GetParametersFromMaterial<SkyboxCubemapDefinition>(material);
                    {
                        ExportProperty(vgoMaterial, material, Property.Tint, VgoMaterialPropertyType.Color4);
                        ExportProperty(vgoMaterial, material, Property.Exposure, VgoMaterialPropertyType.Float);
                        ExportProperty(vgoMaterial, material, Property.Rotation, VgoMaterialPropertyType.Int);
                        ExportTextureProperty(vgoMaterial, material, Property.Tex, VgoTextureMapType.CubeMap);
                    }
                    break;

                case ShaderName.Skybox_Panoramic:
                    //SkyboxPanoramicDefinition skyboxDefinition = UniSkyboxShader.Utils.GetParametersFromMaterial<SkyboxPanoramicDefinition>(material);
                    {
                        ExportProperty(vgoMaterial, material, Property.Tint, VgoMaterialPropertyType.Color4);
                        ExportProperty(vgoMaterial, material, Property.Exposure, VgoMaterialPropertyType.Float);
                        ExportProperty(vgoMaterial, material, Property.Rotation, VgoMaterialPropertyType.Int);
                        ExportTextureProperty(vgoMaterial, material, Property.MainTex);
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
                    throw new NotSupportedException(material.shader.name);
            }

            return vgoMaterial;
        }

        #endregion

        #region Public Methods (Import)

        /// <summary>
        /// Create a skybox material.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="shader">A skybox shader.</param>
        /// <returns>A skybox material.</returns>
        public override Material CreateMaterialAsset(VgoMaterial vgoMaterial, Shader shader)
        {
            if (vgoMaterial == null)
            {
                throw new ArgumentNullException(nameof(vgoMaterial));
            }

            if (shader == null)
            {
                throw new ArgumentNullException(nameof(shader));
            }

            var material = new Material(shader)
            {
                name = vgoMaterial.name
            };

            if (vgoMaterial.renderQueue >= 0)
            {
                material.renderQueue = vgoMaterial.renderQueue;
            }

            switch (shader.name)
            {
                case ShaderName.Skybox_6_Sided:
                    UniSkyboxShader.Utils.SetParametersToMaterial(material, new Skybox6SidedDefinition()
                    {
                        Tint = vgoMaterial.GetColorOrDefault(Property.Tint, Color.white).gamma,
                        Exposure = vgoMaterial.GetSafeFloat(Property.Exposure, 0.0f, 8.0f, 1.0f),
                        Rotation = vgoMaterial.GetSafeInt(Property.Rotation, 0, 360, 0),
                        FrontTex = AllTexture2dList.GetValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.FrontTex)),
                        BackTex = AllTexture2dList.GetValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.BackTex)),
                        LeftTex = AllTexture2dList.GetValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.LeftTex)),
                        RightTex = AllTexture2dList.GetValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.RightTex)),
                        UpTex = AllTexture2dList.GetValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.UpTex)),
                        DownTex = AllTexture2dList.GetValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.DownTex)),
                    });
                    break;

                case ShaderName.Skybox_Cubemap:  // @todo Tex (Cubemap)
                    UniSkyboxShader.Utils.SetParametersToMaterial(material, new SkyboxCubemapDefinition()
                    {
                        Tint = vgoMaterial.GetColorOrDefault(Property.Tint, Color.white).gamma,
                        Exposure = vgoMaterial.GetSafeFloat(Property.Exposure, 0.0f, 8.0f, 1.0f),
                        Rotation = vgoMaterial.GetSafeInt(Property.Rotation, 0, 360, 0),
                        //Tex = AllTextureCubeMapList.GetValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.Tex)),
                    });
                    break;

                case ShaderName.Skybox_Panoramic:
                    UniSkyboxShader.Utils.SetParametersToMaterial(material, new SkyboxPanoramicDefinition()
                    {
                        Tint = vgoMaterial.GetColorOrDefault(Property.Tint, Color.white).gamma,
                        Exposure = vgoMaterial.GetSafeFloat(Property.Exposure, 0.0f, 8.0f, 1.0f),
                        Rotation = vgoMaterial.GetSafeInt(Property.Rotation, 0, 360, 0),
                        MainTex = AllTexture2dList.GetValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.MainTex)),
                        Mapping = (Mapping)vgoMaterial.GetIntOrDefault(Property.Mapping),
                        ImageType = (ImageType)vgoMaterial.GetIntOrDefault(Property.ImageType),
                        MirrorOnBack = vgoMaterial.GetIntOrDefault(Property.MirrorOnBack) == 1,
                        Layout = (Layout)vgoMaterial.GetIntOrDefault(Property.Layout),
                    });
                    break;

                case ShaderName.Skybox_Procedural:
                    UniSkyboxShader.Utils.SetParametersToMaterial(material, new SkyboxProceduralDefinition()
                    {
                        SunDisk = (SunDisk)vgoMaterial.GetIntOrDefault(Property.SunDisk),
                        SunSize = vgoMaterial.GetSafeFloat(Property.SunSize, 0.0f, 1.0f, 0.04f),
                        SunSizeConvergence = vgoMaterial.GetSafeInt(Property.SunSizeConvergence, 1, 10, 5),
                        AtmosphereThickness = vgoMaterial.GetSafeFloat(Property.AtmosphereThickness, 0.0f, 5.0f, 1.0f),
                        SkyTint = vgoMaterial.GetColorOrDefault(Property.SkyTint, Color.white).gamma,
                        GroundColor = vgoMaterial.GetColorOrDefault(Property.GroundColor, Color.white).gamma,
                        Exposure = vgoMaterial.GetSafeFloat(Property.Exposure, 0.0f, 8.0f, 1.3f),
                    });
                    break;

                default:
                    break;
            }

            return material;
        }

        #endregion
    }
}
