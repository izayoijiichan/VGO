// ----------------------------------------------------------------------
// @Namespace : UniVgo.Porters
// @Class     : UnlitMaterialPorter
// ----------------------------------------------------------------------
namespace UniVgo.Porters
{
    using NewtonGltf;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using VgoGltf;

    /// <summary>
    /// Unlit Material Importer
    /// </summary>
    public class UnlitMaterialPorter : MaterialPorterBase
    {
        #region Constructors

        /// <summary>
        /// Create a new instance of UnlitMaterialPorter.
        /// </summary>
        public UnlitMaterialPorter() : base() { }

        #endregion

        #region Enums

        /// <summary>Blend Mode</summary>
        protected enum BlendMode
        {
            /// <summary>Opaque</summary>
            Opaque,
            /// <summary>Cutout</summary>
            Cutout,
            /// <summary>Fade</summary>
            Fade,
            /// <summary>Transparent</summary>
            Transparent
        }

        #endregion

        #region Public Methods (Export)

        /// <summary>
        /// Create a glTF material.
        /// </summary>
        /// <param name="material">A unlit material.</param>
        /// <returns>glTF material</returns>
        public override GltfMaterial CreateGltfMaterial(Material material)
        {
            GltfMaterial gltfMaterial = new GltfMaterial()
            {
                name = material.name,
                alphaMode = GltfAlphaMode.OPAQUE,
                alphaCutoff = -1.0f,
                doubleSided = false,
            };

            UniGLTF.UniUnlit.UniUnlitRenderMode renderMode = UniGLTF.UniUnlit.Utils.GetRenderMode(material);

            // Alpha Mode
            switch (renderMode)
            {
                case UniGLTF.UniUnlit.UniUnlitRenderMode.Opaque:
                    gltfMaterial.alphaMode = GltfAlphaMode.OPAQUE;
                    break;
                case UniGLTF.UniUnlit.UniUnlitRenderMode.Cutout:
                    gltfMaterial.alphaMode = GltfAlphaMode.MASK;
                    break;
                case UniGLTF.UniUnlit.UniUnlitRenderMode.Transparent:
                    gltfMaterial.alphaMode = GltfAlphaMode.BLEND;
                    break;
                default:
                    break;
            }

            // Alpha Cutoff
            if (renderMode == UniGLTF.UniUnlit.UniUnlitRenderMode.Cutout)
            {
                if (material.HasProperty(UniGLTF.UniUnlit.Utils.PropNameCutoff))
                {
                    gltfMaterial.alphaCutoff = material.GetFloat(UniGLTF.UniUnlit.Utils.PropNameCutoff);
                }
            }

            // Double Sided
            if (material.HasProperty(UniGLTF.UniUnlit.Utils.PropNameCullMode))
            {
                UniGLTF.UniUnlit.UniUnlitCullMode cullMode = UniGLTF.UniUnlit.Utils.GetCullMode(material);

                switch (cullMode)
                {
                    case UniGLTF.UniUnlit.UniUnlitCullMode.Off:
                        gltfMaterial.doubleSided = true;
                        break;
                    case UniGLTF.UniUnlit.UniUnlitCullMode.Back:
                        gltfMaterial.doubleSided = false;
                        break;
                    default:
                        break;
                }
            }

            // Base Color
            if (material.HasProperty(UniGLTF.UniUnlit.Utils.PropNameColor))
            {
                if (gltfMaterial.pbrMetallicRoughness == null)
                {
                    gltfMaterial.pbrMetallicRoughness = new GltfMaterialPbrMetallicRoughness();
                }

                gltfMaterial.pbrMetallicRoughness.baseColorFactor = material.GetColor(UniGLTF.UniUnlit.Utils.PropNameColor).linear.ToGltfColor4();
            }

            // Main Texture
            if (material.HasProperty(UniGLTF.UniUnlit.Utils.PropNameMainTex))
            {
                Texture mainTexture = material.GetTexture(UniGLTF.UniUnlit.Utils.PropNameMainTex);

                if (mainTexture != null)
                {
                    if (gltfMaterial.pbrMetallicRoughness == null)
                    {
                        gltfMaterial.pbrMetallicRoughness = new GltfMaterialPbrMetallicRoughness();
                    }

                    // @todo GetOrCreateTexture(material, mainTexture, UniGLTF.UniUnlit.Utils.PropNameMainTex, ColorSpaceType.Srgb, TextureType.Default)
                    gltfMaterial.pbrMetallicRoughness.baseColorTexture = new GltfTextureInfo
                    {
                        index = ExportTexture(material, mainTexture, UniGLTF.UniUnlit.Utils.PropNameMainTex, TextureType.Default, ColorSpaceType.Srgb)
                    };
                }
            }

            if (material.HasProperty(UniGLTF.UniUnlit.Utils.PropNameVColBlendMode))
            {
                //material.GetInt(UniGLTF.UniUnlit.Utils.PropNameVColBlendMode);
            }

            // Extensions
            //  VGO_materials
            //  KHR_materials_unlit
            gltfMaterial.extensions = new GltfExtensions(_JsonSerializerSettings)
            {
                { VGO_materials.ExtensionName, new VGO_materials(material.shader.name) },
                { KHR_materials_unlit.ExtensionName, new KHR_materials_unlit() },
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

            if (gltfMaterial.extensions.Contains(KHR_materials_unlit.ExtensionName) == false)
            {
                Debug.LogWarning($"{KHR_materials_unlit.ExtensionName} is not found.");
            }

            // Base Color Texture
            if (gltfMaterial.pbrMetallicRoughness != null)
            {
                if (TryGetTextureAndSetInfo(gltfMaterial.pbrMetallicRoughness.baseColorTexture, out TextureInfo textureInfo))
                {
                    materialInfo.TextureInfoList.TryAdd(textureInfo);
                }
            }
        }

        /// <summary>
        /// Create a unlit material.
        /// </summary>
        /// <param name="materialInfo">A material info.</param>
        /// <param name="shader">A unlit shader.</param>
        /// <returns>A unlit material.</returns>
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

            // renderMode
            switch (gltfMaterial.alphaMode)
            {
                case GltfAlphaMode.OPAQUE:
                    UniGLTF.UniUnlit.Utils.SetRenderMode(material, UniGLTF.UniUnlit.UniUnlitRenderMode.Opaque);
                    break;
                case GltfAlphaMode.BLEND:
                    UniGLTF.UniUnlit.Utils.SetRenderMode(material, UniGLTF.UniUnlit.UniUnlitRenderMode.Transparent);
                    break;
                case GltfAlphaMode.MASK:
                    UniGLTF.UniUnlit.Utils.SetRenderMode(material, UniGLTF.UniUnlit.UniUnlitRenderMode.Cutout);
                    material.SetFloat(UniGLTF.UniUnlit.Utils.PropNameCutoff, gltfMaterial.alphaCutoff.SafeValue(0.0f, 1.0f, 0.5f));
                    break;
                case GltfAlphaMode.BLEND_ZWRITE:  // @todo VRM_UnlitTransparentZWrite
                    UniGLTF.UniUnlit.Utils.SetRenderMode(material, UniGLTF.UniUnlit.UniUnlitRenderMode.Transparent);
                    material.SetFloat(UniGLTF.UniUnlit.Utils.PropNameCutoff, gltfMaterial.alphaCutoff.SafeValue(0.0f, 1.0f, 0.5f));
                    break;
                default:
                    UniGLTF.UniUnlit.Utils.SetRenderMode(material, UniGLTF.UniUnlit.UniUnlitRenderMode.Opaque);
                    break;
            }

            // culling
            if (gltfMaterial.doubleSided)
            {
                UniGLTF.UniUnlit.Utils.SetCullMode(material, UniGLTF.UniUnlit.UniUnlitCullMode.Off);
            }
            else
            {
                UniGLTF.UniUnlit.Utils.SetCullMode(material, UniGLTF.UniUnlit.UniUnlitCullMode.Back);
            }

            // VColor
            if (materialInfo.HasVertexColor)
            {
                UniGLTF.UniUnlit.Utils.SetVColBlendMode(material, UniGLTF.UniUnlit.UniUnlitVertexColorBlendOp.Multiply);
            }
            else
            {
                UniGLTF.UniUnlit.Utils.SetVColBlendMode(material, UniGLTF.UniUnlit.UniUnlitVertexColorBlendOp.None);
            }

            // BaseColor
            if (gltfMaterial.pbrMetallicRoughness != null)
            {
                if (gltfMaterial.pbrMetallicRoughness.baseColorFactor.HasValue)
                {
                    material.color = gltfMaterial.pbrMetallicRoughness.baseColorFactor.Value.ToUnityColor();
                }
            }

            // Main Texture
            if (gltfMaterial.pbrMetallicRoughness?.baseColorTexture != null)
            {
                material.mainTexture = AllTexture2dList.GetValueOrDefault(gltfMaterial.pbrMetallicRoughness.baseColorTexture.index);

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

            if (material.shader.name == ShaderName.UniGLTF_UniUnlit)
            {
                UniGLTF.UniUnlit.Utils.ValidateProperties(material, true);

                return material;
            }

            switch (gltfMaterial.alphaMode)
            {
                case GltfAlphaMode.BLEND:
                    material.SetInt(UniGLTF.UniUnlit.Utils.PropNameBlendMode, (int)BlendMode.Fade);
                    material.SetInt(UniGLTF.UniUnlit.Utils.PropNameSrcBlend, (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt(UniGLTF.UniUnlit.Utils.PropNameDstBlend, (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt(UniGLTF.UniUnlit.Utils.PropNameZWrite, 0);
                    material.DisableKeyword(UniGLTF.UniUnlit.Utils.KeywordAlphaTestOn);
                    material.EnableKeyword(UniGLTF.UniUnlit.Utils.KeywordAlphaBlendOn);
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                    break;

                case GltfAlphaMode.MASK:
                    material.SetInt(UniGLTF.UniUnlit.Utils.PropNameBlendMode, (int)BlendMode.Cutout);
                    material.SetInt(UniGLTF.UniUnlit.Utils.PropNameSrcBlend, (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt(UniGLTF.UniUnlit.Utils.PropNameDstBlend, (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt(UniGLTF.UniUnlit.Utils.PropNameZWrite, 1);
                    material.EnableKeyword(UniGLTF.UniUnlit.Utils.KeywordAlphaTestOn);
                    material.DisableKeyword(UniGLTF.UniUnlit.Utils.KeywordAlphaBlendOn);
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;
                    break;

                case GltfAlphaMode.OPAQUE:
                default:
                    material.SetInt(UniGLTF.UniUnlit.Utils.PropNameBlendMode, (int)BlendMode.Opaque);
                    material.SetInt(UniGLTF.UniUnlit.Utils.PropNameSrcBlend, (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt(UniGLTF.UniUnlit.Utils.PropNameDstBlend, (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt(UniGLTF.UniUnlit.Utils.PropNameZWrite, 1);
                    material.DisableKeyword(UniGLTF.UniUnlit.Utils.KeywordAlphaTestOn);
                    material.DisableKeyword(UniGLTF.UniUnlit.Utils.KeywordAlphaBlendOn);
                    material.renderQueue = -1;
                    break;
            }

            return material;
        }

        #endregion
    }
}
