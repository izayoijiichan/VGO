// ----------------------------------------------------------------------
// @Namespace : UniVgo.Porters
// @Class     : StandardMaterialPorter
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
    /// Standard Material Importer
    /// </summary>
    public class StandardMaterialPorter : MaterialPorterBase
    {
        #region Constructors

        /// <summary>
        /// Create a new instance of StandardMaterialPorter.
        /// </summary>
        public StandardMaterialPorter() : base() { }

        #endregion

        #region Public Methods (Export)

        /// <summary>
        /// Create a glTF material.
        /// </summary>
        /// <param name="material">A standard material.</param>
        /// <returns>A glTF material.</returns>
        public override GltfMaterial CreateGltfMaterial(Material material)
        {
            StandardDefinition definition = UniStandardShader.Utils.GetParametersFromMaterial(material);

            float smoothness = definition.Glossiness;
            float roughness = 1.0f - smoothness;

            GltfMaterial gltfMaterial = new GltfMaterial()
            {
                name = material.name,
                alphaMode = (GltfAlphaMode)definition.Mode,
                alphaCutoff = -1.0f,
                doubleSided = false,
                pbrMetallicRoughness = new GltfMaterialPbrMetallicRoughness
                {
                    baseColorFactor = definition.Color.linear.ToGltfColor4(),
                    baseColorTexture = null,
                    metallicFactor = definition.Metallic,
                    roughnessFactor = roughness,
                    metallicRoughnessTexture = null,  
                },
                normalTexture = null,
                emissiveFactor = definition.EmissionColor.linear.ToGltfColor3(),
            };

            // Alpha Cutoff
            switch (definition.Mode)
            {
                case AlphaMode.Opaque:
                    break;
                case AlphaMode.Mask:
                    gltfMaterial.alphaCutoff = definition.Cutoff;
                    break;
                case AlphaMode.Blend:
                    break;
            }

            // Main Texture
            if (definition.MainTex != null)
            {
                gltfMaterial.pbrMetallicRoughness.baseColorTexture = new GltfTextureInfo
                {
                    index = ExportTexture(material, definition.MainTex, Property.MainTex, TextureType.Default, ColorSpaceType.Srgb),
                };
            }

            // Metallic Gloss Map
            if (definition.MetallicGlossMap != null)
            {
                gltfMaterial.pbrMetallicRoughness.metallicRoughnessTexture = new GltfTextureInfo
                {
                    index = ExportTexture(material, definition.MetallicGlossMap, Property.MetallicGlossMap, TextureType.MetallicRoughnessMap, ColorSpaceType.Linear, smoothness),
                };
            }

            // Normal Map
            if (definition.BumpMap != null)
            {
                gltfMaterial.normalTexture = new GltfNormalTextureInfo
                {
                    index = ExportTexture(material, definition.BumpMap, Property.BumpMap, TextureType.NormalMap, ColorSpaceType.Linear),
                    scale = definition.BumpScale,
                };
            }

            // Occlusion Map
            if (definition.OcclusionMap != null)
            {
                gltfMaterial.occlusionTexture = new GltfOcclusionTextureInfo
                {
                    index = ExportTexture(material, definition.OcclusionMap, Property.OcclusionMap, TextureType.OcclusionMap, ColorSpaceType.Linear),
                    strength = definition.OcclusionStrength,
                };
            }

            // Emission Map
            if (definition.EmissionMap != null)
            {
                gltfMaterial.emissiveTexture = new GltfTextureInfo
                {
                    index = ExportTexture(material, definition.EmissionMap, Property.EmissionMap, TextureType.EmissionMap, ColorSpaceType.Srgb),
                };
            }

            //switch (material.GetTag(Tag.RenderType, true))
            //{
            //    case RenderTypeValue.Opaque:
            //        gltfMaterial.alphaMode = GltfAlphaMode.OPAQUE;
            //        break;
            //    case RenderTypeValue.Transparent:
            //        gltfMaterial.alphaMode = GltfAlphaMode.BLEND;
            //        break;
            //    case RenderTypeValue.TransparentCutout:
            //        gltfMaterial.alphaMode = GltfAlphaMode.MASK;
            //        break;
            //}

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

            // Base Color
            if (gltfMaterial.pbrMetallicRoughness != null)
            {
                if (TryGetTextureAndSetInfo(gltfMaterial.pbrMetallicRoughness.baseColorTexture, out TextureInfo textureInfo))
                {
                    materialInfo.TextureInfoList.TryAdd(textureInfo);
                }
            }

            // Metallic Gloss Map
            if (gltfMaterial.pbrMetallicRoughness != null)
            {
                TextureInfo textureInfo = GetTextureAndSetInfo(
                    gltfMaterial.pbrMetallicRoughness.metallicRoughnessTexture,
                    TextureType.MetallicRoughnessMap
                );

                if (textureInfo != null)
                {
                    textureInfo.metallicRoughness = gltfMaterial.pbrMetallicRoughness.roughnessFactor.SafeValue(0.0f, 1.0f, 0.5f);

                    materialInfo.TextureInfoList.TryAdd(textureInfo);
                }
            }

            // Normal Map
            if (gltfMaterial.normalTexture != null)
            {
                if (TryGetTextureAndSetInfo(gltfMaterial.normalTexture, TextureType.NormalMap, out TextureInfo textureInfo))
                {
                    textureInfo.normalTextureScale = gltfMaterial.normalTexture.scale.SafeValue(0.0f, 1.0f, 1.0f);

                    materialInfo.TextureInfoList.TryAdd(textureInfo);
                }
            }

            // Occlusion Map
            if (gltfMaterial.occlusionTexture != null)
            {
                if (TryGetTextureAndSetInfo(gltfMaterial.occlusionTexture, TextureType.OcclusionMap, out TextureInfo textureInfo))
                {
                    textureInfo.occlusionStrength = gltfMaterial.occlusionTexture.strength.SafeValue(0.0f, 1.0f, 1.0f);

                    materialInfo.TextureInfoList.TryAdd(textureInfo);
                }
            }

            // Emission Map
            if (gltfMaterial.emissiveTexture != null)
            {
                if (TryGetTextureAndSetInfo(gltfMaterial.emissiveTexture, TextureType.EmissionMap, out TextureInfo textureInfo))
                {
                    materialInfo.TextureInfoList.TryAdd(textureInfo);
                }
            }
        }

        /// <summary>
        /// Create a standard material.
        /// </summary>
        /// <param name="materialInfo">A material info.</param>
        /// <param name="shader">A standard shader.</param>
        /// <returns>A standard material.</returns>
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

            StandardDefinition standardDefinition = CreateStandardDefinition(materialInfo);

            UniStandardShader.Utils.SetParametersToMaterial(material, standardDefinition);

            SetMaterialTextureTransforms(material, materialInfo);

            return material;
        }

        #endregion

        #region Protected Methods (Import)

        /// <summary>
        /// Create a standard definition.
        /// </summary>
        /// <param name="materialInfo">A material info.</param>
        /// <returns>A standard definition.</returns>
        protected virtual StandardDefinition CreateStandardDefinition(MaterialInfo materialInfo)
        {
            GltfMaterial gltfMaterial = materialInfo.gltfMaterial;

            StandardDefinition definition = new StandardDefinition
            {
                Mode = AlphaMode.Opaque,
                Cutoff = gltfMaterial.alphaCutoff.SafeValue(0.0f, 1.0f, 0.5f),
                Color = Color.white,
                MainTex = null,
                Glossiness = 0.5f,
                GlossMapScale = 1.0f,
                SmoothnessTextureChannel = SmoothnessTextureChannel.MetallicAlpha,
                Metallic = 0.0f,
                MetallicGlossMap = null,
                SpecularHighlights = false,
                GlossyReflections = false,
                BumpScale = 1.0f,
                BumpMap = null,
                Parallax = 0.02f,
                ParallaxMap = null,
                OcclusionStrength = 1.0f,
                OcclusionMap = null,
                EmissionColor = Color.white,
                EmissionMap = null,
                DetailMask = null,
                DetailAlbedoMap = null,
                DetailNormalMapScale = 1.0f,
                DetailNormalMap = null,
                UVSec = UV.UV0,
            };

            // Mode
            switch (gltfMaterial.alphaMode)
            {
                case GltfAlphaMode.OPAQUE:
                    definition.Mode = AlphaMode.Opaque;
                    break;
                case GltfAlphaMode.BLEND:
                    definition.Mode = AlphaMode.Blend;
                    break;
                case GltfAlphaMode.MASK:
                    definition.Mode = AlphaMode.Mask;
                    break;
                default:
                    definition.Mode = AlphaMode.Opaque;
                    break;
            }

            // Base Color
            if (gltfMaterial.pbrMetallicRoughness != null)
            {
                var pbrMetallicRoughness = gltfMaterial.pbrMetallicRoughness;

                if (pbrMetallicRoughness.baseColorFactor.HasValue)
                {
                    definition.Color = pbrMetallicRoughness.baseColorFactor.Value.ToUnityColor().gamma;
                }

                if (pbrMetallicRoughness.baseColorTexture != null)
                {
                    definition.MainTex = AllTexture2dList.GetValueOrDefault(pbrMetallicRoughness.baseColorTexture.index);
                }
            }

            // Metallic Gloss Map
            if (gltfMaterial.pbrMetallicRoughness != null)
            {
                var pbrMetallicRoughness = gltfMaterial.pbrMetallicRoughness;

                // MetallicRoughnessTexture
                if (pbrMetallicRoughness.metallicRoughnessTexture != null)
                {
                    definition.MetallicGlossMap = AllTexture2dList.GetValueOrDefault(pbrMetallicRoughness.metallicRoughnessTexture.index);

                    //definition.GlossMapScale
                }

                // Metaric
                definition.Metallic = pbrMetallicRoughness.metallicFactor.SafeValue(0.0f, 1.0f, 0.0f);

                // Roughness
                float roughness = pbrMetallicRoughness.roughnessFactor.SafeValue(0.0f, 1.0f, 0.5f);

                // Glossiness
                definition.Glossiness = 1.0f - roughness;
            }

            // Normal Map
            if (gltfMaterial.normalTexture != null)
            {
                definition.BumpMap = AllTexture2dList.GetValueOrDefault(gltfMaterial.normalTexture.index);

                definition.BumpScale = gltfMaterial.normalTexture.scale.SafeValue(0.0f, 1.0f, 1.0f);
            }

            // Occlusion Map
            if (gltfMaterial.occlusionTexture != null)
            {
                definition.OcclusionMap = AllTexture2dList.GetValueOrDefault(gltfMaterial.occlusionTexture.index);

                definition.OcclusionStrength = gltfMaterial.occlusionTexture.strength.SafeValue(0.0f, 1.0f, 1.0f);
            }

            // Emission
            if (gltfMaterial.emissiveFactor.HasValue)
            {
                definition.EmissionColor = gltfMaterial.emissiveFactor.Value.ToUnityColor().gamma;
            }
            if (gltfMaterial.emissiveTexture != null)
            {
                definition.EmissionMap = AllTexture2dList.GetValueOrDefault(gltfMaterial.emissiveTexture.index);
            }

            return definition;
        }

        /// <summary>
        /// Set the texture transforms to the material.
        /// </summary>
        /// <param name="material">A standard material.</param>
        /// <param name="materialInfo">A material info.</param>
        protected virtual void SetMaterialTextureTransforms(Material material, MaterialInfo materialInfo)
        {
            GltfMaterial gltfMaterial = materialInfo.gltfMaterial;

            // Main Texture (Offset, Scale)
            if (gltfMaterial.pbrMetallicRoughness?.baseColorTexture != null)
            {
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

            // Metallic Gloss Map (Offset, Scale)
            if (gltfMaterial.pbrMetallicRoughness?.metallicRoughnessTexture != null)
            {
                SetMaterialTextureTransform(material, materialInfo, Property.MetallicGlossMap, gltfMaterial.pbrMetallicRoughness.metallicRoughnessTexture.index);
            }

            // Mormal Map (Offset, Scale)
            if (gltfMaterial.normalTexture != null)
            {
                SetMaterialTextureTransform(material, materialInfo, Property.BumpMap, gltfMaterial.normalTexture.index);
            }

            // Emission Map (Offset, Scale)
            if (gltfMaterial.emissiveTexture != null)
            {
                SetMaterialTextureTransform(material, materialInfo, Property.EmissionMap, gltfMaterial.emissiveTexture.index);
            }
        }

        #endregion
    }
}
