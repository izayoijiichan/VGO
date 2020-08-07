// ----------------------------------------------------------------------
// @Namespace : UniVgo.Porters
// @Class     : StandardVColorMaterialPorter
// ----------------------------------------------------------------------
namespace UniVgo.Porters
{
    using NewtonGltf;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UniStandardShader;
    using UnityEngine;
    using VgoGltf;

    /// <summary>
    /// StandardVColor Material Importer
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
        /// Create a glTF material.
        /// </summary>
        /// <param name="material">A standard material.</param>
        /// <returns>A glTF material.</returns>
        public override GltfMaterial CreateGltfMaterial(Material material)
        {
            Color color = material.GetColor("_Color");
            Texture mainTex = material.GetTexture("_MainTex");
            float glossiness = material.GetFloat("_Glossiness");
            float metallic = material.GetFloat("_Metallic");

            float roughness = 1.0f - glossiness;

            GltfMaterial gltfMaterial = new GltfMaterial()
            {
                name = material.name,
                alphaMode = GltfAlphaMode.OPAQUE,
                alphaCutoff = -1.0f,
                doubleSided = false,
                pbrMetallicRoughness = new GltfMaterialPbrMetallicRoughness
                {
                    baseColorFactor = color.ToGltfColor4(),
                    baseColorTexture = null,
                    metallicFactor = metallic,
                    roughnessFactor = roughness,
                },
            };

            // Main Texture
            if (mainTex != null)
            {
                gltfMaterial.pbrMetallicRoughness.baseColorTexture = new GltfTextureInfo
                {
                    index = ExportTexture(material, mainTex, Property.MainTex, TextureType.Default, ColorSpaceType.Srgb),
                };
            }

            // Extensions
            //  VGO_materials
            gltfMaterial.extensions = new GltfExtensions(_JsonSerializerSettings)
            {
                { VGO_materials.ExtensionName, new VGO_materials(material.shader.name) },
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

            // Main Texture
            if (gltfMaterial.pbrMetallicRoughness != null)
            {
                if (TryGetTextureAndSetInfo(gltfMaterial.pbrMetallicRoughness.baseColorTexture, out TextureInfo textureInfo))
                {
                    materialInfo.TextureInfoList.TryAdd(textureInfo);
                }
            }
        }

        /// <summary>
        /// Create a standard vcolor material.
        /// </summary>
        /// <param name="materialInfo">A material info.</param>
        /// <param name="shader">A standard vcolor shader.</param>
        /// <returns>A standard vcolor material.</returns>
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

            // Base Color
            if (gltfMaterial.pbrMetallicRoughness != null)
            {
                var pbrMetallicRoughness = gltfMaterial.pbrMetallicRoughness;

                if (pbrMetallicRoughness.baseColorFactor.HasValue)
                {
                    material.SetColor("_Color", pbrMetallicRoughness.baseColorFactor.Value.ToUnityColor().gamma);
                }

                if (pbrMetallicRoughness.baseColorTexture != null)
                {
                    // Main Texture
                    Texture mainTex = AllTexture2dList.GetValueOrDefault(pbrMetallicRoughness.baseColorTexture.index);

                    if (mainTex != null)
                    {
                        material.SetTexture("_MainTex", mainTex);
                    }

                    TextureInfo baseColorTextureInfo = materialInfo.TextureInfoList
                        .Where(ti => ti.textureIndex == gltfMaterial.pbrMetallicRoughness.baseColorTexture.index)
                        .FirstOrDefault();

                    if ((baseColorTextureInfo != null) &&
                        (baseColorTextureInfo.transform != null))
                    {
                        if (baseColorTextureInfo.transform.offset.HasValue)
                        {
                            material.mainTextureOffset = baseColorTextureInfo.transform.offset.Value.ToUnityVector2();
                        }
                        if (baseColorTextureInfo.transform.scale.HasValue)
                        {
                            material.mainTextureScale = baseColorTextureInfo.transform.scale.Value.ToUnityVector2();
                        }
                    }
                }
            }

            // Metallic Gloss Map
            if (gltfMaterial.pbrMetallicRoughness != null)
            {
                var pbrMetallicRoughness = gltfMaterial.pbrMetallicRoughness;

                // Metaric
                float metallic = pbrMetallicRoughness.metallicFactor.SafeValue(0.0f, 1.0f, 0.0f);

                material.SetFloat("_Metallic", metallic);

                // Roughness
                float roughness = pbrMetallicRoughness.roughnessFactor.SafeValue(0.0f, 1.0f, 0.5f);

                float glossiness = 1.0f - roughness;

                material.SetFloat("_Glossiness", glossiness);
            }

            return material;
        }

        #endregion
    }
}
