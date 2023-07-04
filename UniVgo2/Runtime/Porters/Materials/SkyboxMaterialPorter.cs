// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : SkyboxMaterialPorter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using System;
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
        public override VgoMaterial CreateVgoMaterial(Material material, IVgoStorage vgoStorage)
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

            return vgoMaterial;
        }

        #endregion

        #region Public Methods (Import)

        /// <summary>
        /// Create a skybox material.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="shader">A skybox shader.</param>
        /// <param name="allTexture2dList">List of all texture 2D.</param>
        /// <returns>A skybox material.</returns>
        public override Material CreateMaterialAsset(VgoMaterial vgoMaterial, Shader shader, List<Texture2D?> allTexture2dList)
        {
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
                        FrontTex = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.FrontTex)),
                        BackTex = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.BackTex)),
                        LeftTex = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.LeftTex)),
                        RightTex = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.RightTex)),
                        UpTex = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.UpTex)),
                        DownTex = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.DownTex)),
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
                        MainTex = allTexture2dList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.MainTex)),
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
