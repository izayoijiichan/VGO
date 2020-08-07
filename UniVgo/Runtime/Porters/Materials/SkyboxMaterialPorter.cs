// ----------------------------------------------------------------------
// @Namespace : UniVgo.Porters
// @Class     : SkyboxMaterialPorter
// ----------------------------------------------------------------------
namespace UniVgo.Porters
{
    using NewtonGltf;
    using System;
    using System.Collections.Generic;
    using UniSkyboxShader;
    using UnityEngine;
    using VgoGltf;

    /// <summary>
    /// Skybox Material Importer
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
        /// Create a glTF material.
        /// </summary>
        /// <param name="material">A skybox material.</param>
        /// <returns>A glTF material.</returns>
        public override GltfMaterial CreateGltfMaterial(Material material)
        {
            VGO_materials_skybox vgoSkybox;

            switch (material.shader.name)
            {
                case ShaderName.Skybox_6_Sided:
                    {
                        Skybox6SidedDefinition skyboxDefinition = UniSkyboxShader.Utils.GetParametersFromMaterial<Skybox6SidedDefinition>(material);

                        vgoSkybox = new VGO_materials_skybox()
                        {
                            tint = skyboxDefinition.Tint.linear.ToGltfColor4(),
                            exposure = skyboxDefinition.Exposure,
                            rotation = skyboxDefinition.Rotation,
                            frontTexIndex = ExportTexture(material, skyboxDefinition.FrontTex, Property.FrontTex),
                            backTexIndex = ExportTexture(material, skyboxDefinition.BackTex, Property.BackTex),
                            leftTexIndex = ExportTexture(material, skyboxDefinition.LeftTex, Property.LeftTex),
                            rightTexIndex = ExportTexture(material, skyboxDefinition.RightTex, Property.RightTex),
                            upTexIndex = ExportTexture(material, skyboxDefinition.UpTex, Property.UpTex),
                            downTexIndex = ExportTexture(material, skyboxDefinition.DownTex, Property.DownTex),
                        };
                    }
                    break;

                case ShaderName.Skybox_Cubemap:  // @todo Tex (Cubemap)
                    {
                        SkyboxCubemapDefinition skyboxDefinition = UniSkyboxShader.Utils.GetParametersFromMaterial<SkyboxCubemapDefinition>(material);

                        vgoSkybox = new VGO_materials_skybox()
                        {
                            tint = skyboxDefinition.Tint.linear.ToGltfColor4(),
                            exposure = skyboxDefinition.Exposure,
                            rotation = skyboxDefinition.Rotation,
                            texIndex = ExportTexture(material, skyboxDefinition.Tex, Property.Tex, TextureType.CubeMap),
                        };
                    }
                    break;

                case ShaderName.Skybox_Panoramic:
                    {
                        SkyboxPanoramicDefinition skyboxDefinition = UniSkyboxShader.Utils.GetParametersFromMaterial<SkyboxPanoramicDefinition>(material);

                        // @todo Texture
                        vgoSkybox = new VGO_materials_skybox()
                        {
                            tint = skyboxDefinition.Tint.linear.ToGltfColor4(),
                            exposure = skyboxDefinition.Exposure,
                            rotation = skyboxDefinition.Rotation,
                            mainTexIndex = ExportTexture(material, skyboxDefinition.MainTex, Property.MainTex),
                            mapping = (SkyboxMapping)skyboxDefinition.Mapping,
                            imageType = (SkyboxImageType)skyboxDefinition.ImageType,
                            mirrorOnBack = skyboxDefinition.MirrorOnBack,
                            layout = (SkyboxLayout)skyboxDefinition.Layout,
                        };
                    }
                    break;

                case ShaderName.Skybox_Procedural:
                    {
                        SkyboxProceduralDefinition skyboxDefinition = UniSkyboxShader.Utils.GetParametersFromMaterial<SkyboxProceduralDefinition>(material);

                        vgoSkybox = new VGO_materials_skybox()
                        {
                            sunDisk = (SkyboxSunDisk)skyboxDefinition.SunDisk,
                            sunSize = skyboxDefinition.SunSize,
                            sunSizeConvergence = skyboxDefinition.SunSizeConvergence,
                            atmosphereThickness = skyboxDefinition.AtmosphereThickness,
                            skyTint = skyboxDefinition.SkyTint.linear.ToGltfColor4(),
                            groundColor = skyboxDefinition.GroundColor.linear.ToGltfColor4(),
                            exposure = skyboxDefinition.Exposure,
                        };
                    }
                    break;

                default:
                    throw new NotSupportedException(material.shader.name);
            }

            GltfMaterial gltfMaterial = new GltfMaterial()
            {
                name = material.name,
                pbrMetallicRoughness = new GltfMaterialPbrMetallicRoughness(),
            };

            // Extensions
            //  VGO_materials
            //  VGO_materials_skybox
            gltfMaterial.extensions = new GltfExtensions(_JsonSerializerSettings)
            {
                { VGO_materials.ExtensionName, new VGO_materials(material.shader.name) },
                { VGO_materials_skybox.ExtensionName, vgoSkybox },
            };

            return gltfMaterial;
        }

        #endregion

        #region Public Methods (Import)

        /// <summary>
        /// Set material texture info list.
        /// </summary>
        /// <param name="materialInfo">A material info.</param>
        /// <param name="allTextureInfoList">List of all texture info.</param>
        public override void SetMaterialTextureInfoList(MaterialInfo materialInfo, List<TextureInfo> allTextureInfoList)
        {
            AllTextureInfoList = allTextureInfoList;

            GltfMaterial gltfMaterial = materialInfo.gltfMaterial;

            if (gltfMaterial.extensions.Contains(VGO_materials_skybox.ExtensionName) == false)
            {
                throw new Exception($"{VGO_materials_skybox.ExtensionName} is not found.");
            }

            gltfMaterial.extensions.JsonSerializerSettings = _JsonSerializerSettings;

            VGO_materials_skybox vgoSkybox = gltfMaterial.extensions.GetValueOrDefault<VGO_materials_skybox>(VGO_materials_skybox.ExtensionName);

            if (vgoSkybox.frontTexIndex != -1)
            {
                if (TryGetTextureAndSetInfo(vgoSkybox.frontTexIndex, out TextureInfo textureInfo))
                {
                    materialInfo.TextureInfoList.TryAdd(textureInfo);
                }
            }

            if (vgoSkybox.backTexIndex != -1)
            {
                if (TryGetTextureAndSetInfo(vgoSkybox.backTexIndex, out TextureInfo textureInfo))
                {
                    materialInfo.TextureInfoList.TryAdd(textureInfo);
                }
            }

            if (vgoSkybox.leftTexIndex != -1)
            {
                if (TryGetTextureAndSetInfo(vgoSkybox.leftTexIndex, out TextureInfo textureInfo))
                {
                    materialInfo.TextureInfoList.TryAdd(textureInfo);
                }
            }

            if (vgoSkybox.rightTexIndex != -1)
            {
                if (TryGetTextureAndSetInfo(vgoSkybox.rightTexIndex, out TextureInfo textureInfo))
                {
                    materialInfo.TextureInfoList.TryAdd(textureInfo);
                }
            }

            if (vgoSkybox.upTexIndex != -1)
            {
                if (TryGetTextureAndSetInfo(vgoSkybox.upTexIndex, out TextureInfo textureInfo))
                {
                    materialInfo.TextureInfoList.TryAdd(textureInfo);
                }
            }

            if (vgoSkybox.downTexIndex != -1)
            {
                if (TryGetTextureAndSetInfo(vgoSkybox.downTexIndex, out TextureInfo textureInfo))
                {
                    materialInfo.TextureInfoList.TryAdd(textureInfo);
                }
            }

            if (vgoSkybox.texIndex != -1)
            {
                if (TryGetTextureAndSetInfo(vgoSkybox.texIndex, TextureType.CubeMap, out TextureInfo textureInfo))
                {
                    materialInfo.TextureInfoList.TryAdd(textureInfo);
                }
            }

            if (vgoSkybox.mainTexIndex != -1)
            {
                if (TryGetTextureAndSetInfo(vgoSkybox.mainTexIndex, out TextureInfo textureInfo))
                {
                    materialInfo.TextureInfoList.TryAdd(textureInfo);
                }
            }
        }

        /// <summary>
        /// Create a skybox material.
        /// </summary>
        /// <param name="materialInfo">A material info.</param>
        /// <param name="shader">A skybox shader.</param>
        /// <returns>A skybox material.</returns>
        public override Material CreateMaterialAsset(MaterialInfo materialInfo, Shader shader)
        {
            if (materialInfo == null)
            {
                throw new ArgumentNullException(nameof(materialInfo));
            }

            if (shader == null)
            {
                throw new ArgumentNullException(nameof(shader));
            }

            var material = new Material(shader)
            {
                name = materialInfo.name
            };

            GltfMaterial gltfMaterial = materialInfo.gltfMaterial;

            if (gltfMaterial.extensions.Contains(VGO_materials_skybox.ExtensionName) == false)
            {
                throw new Exception($"{VGO_materials_skybox.ExtensionName} is not found.");
            }

            gltfMaterial.extensions.JsonSerializerSettings = _JsonSerializerSettings;

            VGO_materials_skybox vgoSkybox = gltfMaterial.extensions.GetValueOrDefault<VGO_materials_skybox>(VGO_materials_skybox.ExtensionName);

            switch (shader.name)
            {
                case ShaderName.Skybox_6_Sided:
                    UniSkyboxShader.Utils.SetParametersToMaterial(material, new Skybox6SidedDefinition()
                    {
                        Tint = vgoSkybox.tint.GetValueOrDefault(Color4.White).ToUnityColor().gamma,
                        Exposure = vgoSkybox.exposure.SafeValue(0.0f, 8.0f, 1.0f),
                        Rotation = vgoSkybox.rotation.SafeValue(0, 360, 0),
                        FrontTex = AllTexture2dList.GetValueOrDefault(vgoSkybox.frontTexIndex),
                        BackTex = AllTexture2dList.GetValueOrDefault(vgoSkybox.backTexIndex),
                        LeftTex = AllTexture2dList.GetValueOrDefault(vgoSkybox.leftTexIndex),
                        RightTex = AllTexture2dList.GetValueOrDefault(vgoSkybox.rightTexIndex),
                        UpTex = AllTexture2dList.GetValueOrDefault(vgoSkybox.upTexIndex),
                        DownTex = AllTexture2dList.GetValueOrDefault(vgoSkybox.downTexIndex),
                    });
                    break;

                case ShaderName.Skybox_Cubemap:  // @todo Tex (Cubemap)
                    UniSkyboxShader.Utils.SetParametersToMaterial(material, new SkyboxCubemapDefinition()
                    {
                        Tint = vgoSkybox.tint.GetValueOrDefault(Color4.White).ToUnityColor().gamma,
                        Exposure = vgoSkybox.exposure.SafeValue(0.0f, 8.0f, 1.0f),
                        Rotation = vgoSkybox.rotation.SafeValue(0, 360, 0),
                        //Tex = GetCubemap(UniSkyboxShader.Property.Tex, vgoSkybox.texIndex),
                    });
                    break;

                case ShaderName.Skybox_Panoramic:
                    UniSkyboxShader.Utils.SetParametersToMaterial(material, new SkyboxPanoramicDefinition()
                    {
                        Tint = vgoSkybox.tint.GetValueOrDefault(Color4.White).ToUnityColor().gamma,
                        Exposure = vgoSkybox.exposure.SafeValue(0.0f, 8.0f, 1.0f),
                        Rotation = vgoSkybox.rotation.SafeValue(0, 360, 0),
                        MainTex = AllTexture2dList.GetValueOrDefault(vgoSkybox.mainTexIndex),
                        Mapping = (Mapping)vgoSkybox.mapping,
                        ImageType = (ImageType)vgoSkybox.imageType,
                        MirrorOnBack = vgoSkybox.mirrorOnBack,
                        Layout = (Layout)vgoSkybox.layout,
                    });
                    break;

                case ShaderName.Skybox_Procedural:
                    UniSkyboxShader.Utils.SetParametersToMaterial(material, new SkyboxProceduralDefinition()
                    {
                        SunDisk = (SunDisk)vgoSkybox.sunDisk,
                        SunSize = vgoSkybox.sunSize.SafeValue(0.0f, 1.0f, 0.04f),
                        SunSizeConvergence = vgoSkybox.sunSizeConvergence.SafeValue(1, 10, 5),
                        AtmosphereThickness = vgoSkybox.atmosphereThickness.SafeValue(0.0f, 5.0f, 1.0f),
                        SkyTint = vgoSkybox.skyTint.GetValueOrDefault(Color4.White).ToUnityColor().gamma,
                        GroundColor = vgoSkybox.groundColor.GetValueOrDefault(Color4.White).ToUnityColor().gamma,
                        Exposure = vgoSkybox.exposure.SafeValue(0.0f, 8.0f, 1.3f),
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
