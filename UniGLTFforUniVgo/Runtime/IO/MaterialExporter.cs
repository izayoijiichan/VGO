using System;
using System.Collections.Generic;
using UniGLTF.UniUnlit;
using UnityEngine;
using UnityEngine.Rendering;


namespace UniGLTFforUniVgo
{
    public enum glTFBlendMode
    {
        OPAQUE,
        MASK,
        BLEND
    }

    public interface IMaterialExporter
    {
        glTFMaterial ExportMaterial(Material m, TextureExportManager textureManager);
    }

    public class MaterialExporter : IMaterialExporter
    {
        public virtual glTFMaterial ExportMaterial(Material m, TextureExportManager textureManager)
        {
            var material = CreateMaterial(m);

            // common params
            material.name = m.name;
            Export_Color(m, textureManager, material);
            Export_Metallic(m, textureManager, material);
            Export_Normal(m, textureManager, material);
            Export_Occlusion(m, textureManager, material);
            Export_Emission(m, textureManager, material);

            if ((material.pbrMetallicRoughness != null) &&
                (material.pbrMetallicRoughness.baseColorFactor == null) &&
                (material.pbrMetallicRoughness.baseColorTexture == null) &&
                (material.pbrMetallicRoughness.metallicFactor == -1.0f) &&
                (material.pbrMetallicRoughness.roughnessFactor == -1.0f) &&
                (material.pbrMetallicRoughness.baseColorFactor == null) &&
                (material.pbrMetallicRoughness.extensions == null) &&
                (material.pbrMetallicRoughness.extras == null))
            {
                material.pbrMetallicRoughness = null;
            }

            return material;
        }

        static void Export_Color(Material m, TextureExportManager textureManager, glTFMaterial material)
        {
            if (m.HasProperty("_Color"))
            {
                material.pbrMetallicRoughness.baseColorFactor = m.color.linear.ToArray();
            }

            if (m.HasProperty("_MainTex"))
            {
                var index = textureManager.CopyAndGetIndex(m.GetTexture("_MainTex"), RenderTextureReadWrite.sRGB);
                if (index != -1)
                {
                    material.pbrMetallicRoughness.baseColorTexture = new glTFMaterialBaseColorTextureInfo()
                    {
                        index = index,
                    };
                }
            }
        }

        static void Export_Metallic(Material m, TextureExportManager textureManager, glTFMaterial material)
        {
            int index = -1;
            if (m.HasProperty("_MetallicGlossMap"))
            {
                float smoothness = 0.0f;
                if (m.HasProperty("_GlossMapScale"))
                {
                    smoothness = m.GetFloat("_GlossMapScale");
                }

                // Bake smoothness values into a texture.
                var converter = new MetallicRoughnessConverter(smoothness);
                index = textureManager.ConvertAndGetIndex(m.GetTexture("_MetallicGlossMap"), converter);
                if (index != -1)
                {
                    material.pbrMetallicRoughness.metallicRoughnessTexture =
                        new glTFMaterialMetallicRoughnessTextureInfo()
                        {
                            index = index,
                        };
                }
            }

            if (index != -1)
            {
                material.pbrMetallicRoughness.metallicFactor = 1.0f;
                // Set 1.0f as hard-coded. See: https://github.com/dwango/UniVRM/issues/212.
                material.pbrMetallicRoughness.roughnessFactor = 1.0f;
            }
            else
            {
                if (m.HasProperty("_Metallic"))
                {
                    material.pbrMetallicRoughness.metallicFactor = m.GetFloat("_Metallic");
                }

                if (m.HasProperty("_Glossiness"))
                {
                    material.pbrMetallicRoughness.roughnessFactor = 1.0f - m.GetFloat("_Glossiness");
                }
            }
        }

        static void Export_Normal(Material m, TextureExportManager textureManager, glTFMaterial material)
        {
            if (m.HasProperty("_BumpMap"))
            {
                var index = textureManager.ConvertAndGetIndex(m.GetTexture("_BumpMap"), new NormalConverter());
                if (index != -1)
                {
                    material.normalTexture = new glTFMaterialNormalTextureInfo()
                    {
                        index = index,
                    };
                }

                if (index != -1 && m.HasProperty("_BumpScale"))
                {
                    material.normalTexture.scale = m.GetFloat("_BumpScale");
                }
            }
        }

        static void Export_Occlusion(Material m, TextureExportManager textureManager, glTFMaterial material)
        {
            if (m.HasProperty("_OcclusionMap"))
            {
                var index = textureManager.ConvertAndGetIndex(m.GetTexture("_OcclusionMap"), new OcclusionConverter());
                if (index != -1)
                {
                    material.occlusionTexture = new glTFMaterialOcclusionTextureInfo()
                    {
                        index = index,
                    };
                }

                if (index != -1 && m.HasProperty("_OcclusionStrength"))
                {
                    material.occlusionTexture.strength = m.GetFloat("_OcclusionStrength");
                }
            }
        }

        static void Export_Emission(Material m, TextureExportManager textureManager, glTFMaterial material)
        {
            if (m.IsKeywordEnabled("_EMISSION") == false)
                return;

            if (m.HasProperty("_EmissionColor"))
            {
                var color = m.GetColor("_EmissionColor");
                if (color.maxColorComponent > 1)
                {
                    color /= color.maxColorComponent;
                }
                material.emissiveFactor = new float[] { color.r, color.g, color.b };
            }

            if (m.HasProperty("_EmissionMap"))
            {
                var index = textureManager.CopyAndGetIndex(m.GetTexture("_EmissionMap"), RenderTextureReadWrite.sRGB);
                if (index != -1)
                {
                    material.emissiveTexture = new glTFMaterialEmissiveTextureInfo()
                    {
                        index = index,
                    };
                }
            }
        }

        protected virtual glTFMaterial CreateMaterial(Material m)
        {
            switch (m.shader.name)
            {
                case "Unlit/Color":
                    return Export_UnlitColor(m);

                case "Unlit/Texture":
                    return Export_UnlitTexture(m);

                case "Unlit/Transparent":
                    return Export_UnlitTransparent(m);

                case "Unlit/Transparent Cutout":
                    return Export_UnlitCutout(m);

                case "UniGLTF/UniUnlit":
                    return Export_UniUnlit(m);

                default:
                    return Export_Standard(m);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        static glTFMaterial CreateUnlitMaterialDefault(Material m)
        {
            return new glTFMaterial
            {
                pbrMetallicRoughness = new glTFPbrMetallicRoughness
                {
                    //baseColorFactor = new float[] { 1.0f, 1.0f, 1.0f, 1.0f },
                    //roughnessFactor = 0.9f,
                    //metallicFactor = 0.0f,
                },
                extensions = new glTFMaterial_extensions
                {
                    KHR_materials_unlit = new glTF_KHR_materials_unlit(),
                    VGO_materials = new glTF_VGO_materials()
                    {
                        shaderName = m.shader.name,
                    }
                },
            };
        }

        static glTFMaterial Export_UnlitColor(Material m)
        {
            var material = CreateUnlitMaterialDefault(m);
            material.alphaMode = glTFBlendMode.OPAQUE.ToString();
            return material;
        }

        static glTFMaterial Export_UnlitTexture(Material m)
        {
            var material = CreateUnlitMaterialDefault(m);
            material.alphaMode = glTFBlendMode.OPAQUE.ToString();
            return material;
        }

        static glTFMaterial Export_UnlitTransparent(Material m)
        {
            var material = CreateUnlitMaterialDefault(m);
            material.alphaMode = glTFBlendMode.BLEND.ToString();
            return material;
        }

        static glTFMaterial Export_UnlitCutout(Material m)
        {
            var material = CreateUnlitMaterialDefault(m);
            material.alphaMode = glTFBlendMode.MASK.ToString();
            material.alphaCutoff = m.GetFloat("_Cutoff");
            return material;
        }

        private glTFMaterial Export_UniUnlit(Material m)
        {
            var material = CreateUnlitMaterialDefault(m);

            var renderMode = UniGLTF.UniUnlit.Utils.GetRenderMode(m);
            if (renderMode == UniUnlitRenderMode.Opaque)
            {
                material.alphaMode = glTFBlendMode.OPAQUE.ToString();
            }
            else if (renderMode == UniUnlitRenderMode.Transparent)
            {
                material.alphaMode = glTFBlendMode.BLEND.ToString();
            }
            else if (renderMode == UniUnlitRenderMode.Cutout)
            {
                material.alphaMode = glTFBlendMode.MASK.ToString();
            }
            else
            {
                material.alphaMode = glTFBlendMode.OPAQUE.ToString();
            }

            var cullMode = UniGLTF.UniUnlit.Utils.GetCullMode(m);
            if (cullMode == UniUnlitCullMode.Off)
            {
                material.doubleSided = true;
            }
            else
            {
                material.doubleSided = false;
            }

            return material;
        }

        static glTFMaterial Export_Standard(Material m)
        {
            var material = new glTFMaterial
            {
                pbrMetallicRoughness = new glTFPbrMetallicRoughness(),
                extensions = new glTFMaterial_extensions
                {
                    VGO_materials = new glTF_VGO_materials()
                    {
                        shaderName = m.shader.name,
                    }
                },
            };

            switch (m.GetTag("RenderType", true))
            {
                case "Transparent":
                    material.alphaMode = glTFBlendMode.BLEND.ToString();
                    break;

                case "TransparentCutout":
                    material.alphaMode = glTFBlendMode.MASK.ToString();
                    material.alphaCutoff = m.GetFloat("_Cutoff");
                    break;

                default:
                    material.alphaMode = glTFBlendMode.OPAQUE.ToString();
                    break;
            }

            return material;
        }
    }
}
