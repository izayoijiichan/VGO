// ----------------------------------------------------------------------
// @Namespace : UniVgo.Porters
// @Class     : VgoMaterialImporter
// ----------------------------------------------------------------------
namespace UniVgo.Porters
{
    using NewtonGltf;
    using NewtonGltf.Serialization;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// VGO Material Importer
    /// </summary>
    public class VgoMaterialImporter : IMaterialImporter
    {
        #region Fields

        /// <summary>JSON selializer settings.</summary>
        protected readonly VgoJsonSerializerSettings _JsonSerializerSettings = new VgoJsonSerializerSettings();

        #endregion

        #region Properties

        /// <summary>The material porter store.</summary>
        public IMaterialPorterStore MaterialPorterStore { get; set; }

        /// <summary>The shader store.</summary>
        public IShaderStore ShaderStore { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Create a material info.
        /// </summary>
        /// <param name="materialIndex">The index of gltf.material.</param>
        /// <param name="gltfMaterial">A gltf.material.</param>
        /// <param name="gltfMeshList">List of gltf.material.</param>
        /// <param name="textureInfoList">List of texture info.</param>
        /// <returns>A material info.</returns>

        public virtual MaterialInfo CreateMaterialInfo(int materialIndex, GltfMaterial gltfMaterial, List<GltfMesh> gltfMeshList, List<TextureInfo> textureInfoList)
        {
            if (MaterialPorterStore == null)
            {
                throw new Exception();
            }

            if (ShaderStore == null)
            {
                throw new Exception();
            }

            string vgoMaterialShaderName = GetShaderName(gltfMaterial);

            ShaderGroup shaderGroup;
            MaterialLightingType lightingType;

            if (vgoMaterialShaderName == null)
            {
                shaderGroup = GetShaderGroup(gltfMaterial);
                lightingType = GetMaterialLightingType(gltfMaterial);
            }
            else
            {
                ShaderGroup shaderGroup1 = GetShaderGroup(vgoMaterialShaderName);
                ShaderGroup shaderGroup2 = GetShaderGroup(gltfMaterial);

                if (shaderGroup1 == shaderGroup2)
                {
                    shaderGroup = shaderGroup1;
                }
                else if (shaderGroup2 == ShaderGroup.Unknown)
                {
                    shaderGroup = shaderGroup1;
                }
                else if (shaderGroup1 == ShaderGroup.Unknown)
                {
                    shaderGroup = shaderGroup2;
                }
                else
                {
                    shaderGroup = shaderGroup1;  // @notice
                }

                MaterialLightingType lightingType1 = GetMaterialLightingType(vgoMaterialShaderName);
                MaterialLightingType lightingType2 = GetMaterialLightingType(gltfMaterial);

                if (lightingType1 == lightingType2)
                {
                    lightingType = lightingType1;
                }
                else if (lightingType2 == MaterialLightingType.Unknown)
                {
                    lightingType = lightingType1;
                }
                else if (lightingType1 == MaterialLightingType.Unknown)
                {
                    lightingType = lightingType2;
                }
                else
                {
                    lightingType = lightingType1;  // @notice
                }
            }

            bool hasVertexColor = HasVertexColor(materialIndex, gltfMeshList);

            MaterialInfo materialInfo = new MaterialInfo(materialIndex, gltfMaterial)
            {
                name = gltfMaterial.name,
                shaderName = vgoMaterialShaderName,
                shaderGroup = shaderGroup,
                lightingType = lightingType,
                HasVertexColor = hasVertexColor,
            };

            IMaterialPorter materialPorter = MaterialPorterStore.GetPorterOrStandard(materialInfo);

            materialPorter.SetMaterialTextureInfoList(materialInfo, textureInfoList);

            return materialInfo;
        }

        /// <summary>
        /// Create a material.
        /// </summary>
        /// <param name="materialInfo">A material info.</param>
        /// <param name="texture2dList">List of Texture2D.</param>
        /// <returns>A unity material.</returns>
        public virtual Material CreateMaterialAsset(MaterialInfo materialInfo, List<Texture2D> texture2dList)
        {
            if (MaterialPorterStore == null)
            {
                throw new Exception();
            }

            if (ShaderStore == null)
            {
                throw new Exception();
            }

            Shader shader = ShaderStore.GetShaderOrStandard(materialInfo);

            IMaterialPorter materialPorter = MaterialPorterStore.GetPorterOrStandard(materialInfo);

            materialPorter.AllTexture2dList = texture2dList;

            Material material = materialPorter.CreateMaterialAsset(materialInfo, shader);

#if UNITY_EDITOR
            material.hideFlags = HideFlags.DontUnloadUnusedAsset;
#endif

            return material;
        }

        #endregion

        #region Protected Methods (Helpers)

        /// <summary>
        /// Get the shader name.
        /// </summary>
        /// <param name="gltfMaterial">A gltf.material.</param>
        /// <returns>The shader name.</returns>
        protected virtual string GetShaderName(GltfMaterial gltfMaterial)
        {
            if (gltfMaterial.extensions == null)
            {
                return null;
            }

            if (gltfMaterial.extensions.Contains(VGO_materials.ExtensionName) == false)
            {
                return null;
            }

            VGO_materials vgoMaterials = gltfMaterial.extensions.GetValueOrDefault<VGO_materials>(VGO_materials.ExtensionName);

            if (vgoMaterials == null)
            {
                return null;
            }

            if (string.IsNullOrEmpty(vgoMaterials.shaderName))
            {
                return null;
            }

            return vgoMaterials.shaderName;
        }

        /// <summary>
        /// Get the shader group by the gltf.material.
        /// </summary>
        /// <param name="gltfMaterial">A gltf.material.</param>
        /// <returns>The shader group.</returns>
        protected virtual ShaderGroup GetShaderGroup(GltfMaterial gltfMaterial)
        {
            if (gltfMaterial.extensions == null)
            {
                return ShaderGroup.Unknown;
            }

            if (gltfMaterial.extensions.Contains(VGO_materials_particle.ExtensionName))
            {
                return ShaderGroup.Particles;
            }

            if (gltfMaterial.extensions.Contains(VGO_materials_skybox.ExtensionName))
            {
                return ShaderGroup.Skybox;
            }

            if (gltfMaterial.extensions.Contains(VRMC_materials_mtoon.ExtensionName))
            {
                return ShaderGroup.VRM;
            }

            return ShaderGroup.Unknown;
        }

        /// <summary>
        /// Get the shader group by the shader name.
        /// </summary>
        /// <param name="shaderName">The shader name.</param>
        /// <returns>The shader group.</returns>
        protected virtual ShaderGroup GetShaderGroup(string shaderName)
        {
            ShaderGroup shaderGroup;

            switch (shaderName)
            {
                case ShaderName.Standard:
                    shaderGroup = ShaderGroup.Standard;
                    break;
                case ShaderName.Particles_Standard_Surface:
                case ShaderName.Particles_Standard_Unlit:
                    shaderGroup = ShaderGroup.Particles;
                    break;
                case ShaderName.Skybox_6_Sided:
                case ShaderName.Skybox_Cubemap:
                case ShaderName.Skybox_Panoramic:
                case ShaderName.Skybox_Procedural:
                    shaderGroup = ShaderGroup.Skybox;
                    break;
                case ShaderName.Unlit_Color:
                case ShaderName.Unlit_Texture:
                case ShaderName.Unlit_Transparent:
                case ShaderName.Unlit_Transparent_Cutout:
                    shaderGroup = ShaderGroup.Unlit;
                    break;
                case ShaderName.UniGLTF_StandardVColor:
                case ShaderName.UniGLTF_UniUnlit:
                    shaderGroup = ShaderGroup.UniGltf;
                    break;
                case ShaderName.VRM_MToon:
                case ShaderName.VRM_UnlitTexture:
                case ShaderName.VRM_UnlitTransparent:
                case ShaderName.VRM_UnlitCutout:
                case ShaderName.VRM_UnlitTransparentZWrite:
                    shaderGroup = ShaderGroup.VRM;
                    break;
                default:
                    shaderGroup = ShaderGroup.Unknown;
                    break;
            }

            return shaderGroup;
        }

        /// <summary>
        /// Get the material lighting type by the gltf.material.
        /// </summary>
        /// <param name="gltfMaterial">A gltf.material.</param>
        /// <returns>The material lighting type.</returns>
        protected virtual MaterialLightingType GetMaterialLightingType(GltfMaterial gltfMaterial)
        {
            if (gltfMaterial.extensions == null)
            {
                return MaterialLightingType.Unknown;
            }

            if (gltfMaterial.extensions.Contains(KHR_materials_unlit.ExtensionName))
            {
                return MaterialLightingType.Unlit;
            }

            if (gltfMaterial.extensions.Contains(VGO_materials_skybox.ExtensionName))
            {
                return MaterialLightingType.Light;
            }

            return MaterialLightingType.Unknown;
        }

        /// <summary>
        /// Get the material lighting type by the gltf.material.
        /// </summary>
        /// <param name="shaderName">The shader name.</param>
        /// <returns>The material lighting type.</returns>
        protected virtual MaterialLightingType GetMaterialLightingType(string shaderName)
        {
            switch (shaderName)
            {
                case ShaderName.Particles_Standard_Unlit:
                case ShaderName.Unlit_Color:
                case ShaderName.Unlit_Texture:
                case ShaderName.Unlit_Transparent:
                case ShaderName.Unlit_Transparent_Cutout:
                case ShaderName.UniGLTF_UniUnlit:
                case ShaderName.VRM_UnlitTexture:
                case ShaderName.VRM_UnlitTransparent:
                case ShaderName.VRM_UnlitCutout:
                case ShaderName.VRM_UnlitTransparentZWrite:
                    return MaterialLightingType.Unlit;

                case ShaderName.Standard:
                case ShaderName.Particles_Standard_Surface:
                case ShaderName.Skybox_6_Sided:
                case ShaderName.Skybox_Cubemap:
                case ShaderName.Skybox_Panoramic:
                case ShaderName.Skybox_Procedural:
                case ShaderName.UniGLTF_StandardVColor:
                case ShaderName.VRM_MToon:
                    return MaterialLightingType.Light;

                default:
                    return MaterialLightingType.Unknown;
            }
        }

        /// <summary>
        /// Whether the material has vertex color.
        /// </summary>
        /// <param name="materialIndex">The index of gltf.material.</param>
        /// <param name="gltfMeshes">gltf.meshes.</param>
        /// <returns>Returns true if material has vertex color, false otherwise.</returns>
        protected bool HasVertexColor(int materialIndex, List<GltfMesh> gltfMeshes)
        {
            foreach (GltfMesh gltfMesh in gltfMeshes)
            {
                foreach (GltfMeshPrimitive meshPrimitive in gltfMesh.primitives)
                {
                    if (meshPrimitive.material == materialIndex)
                    {
                        return meshPrimitive.HasVertexColor;
                    }
                }
            }
            return false;
        }

        #endregion
    }
}
