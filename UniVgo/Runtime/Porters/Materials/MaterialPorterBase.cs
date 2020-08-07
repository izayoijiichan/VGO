// ----------------------------------------------------------------------
// @Namespace : UniVgo.Porters
// @Class     : MaterialPorterBase
// ----------------------------------------------------------------------
namespace UniVgo.Porters
{
    using NewtonGltf;
    using NewtonGltf.Serialization;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    /// <summary>
    /// Basic Material Porter
    /// </summary>
    public abstract class MaterialPorterBase : IMaterialPorter
    {
        #region Constructors

        /// <summary>
        /// Create a new instance of MaterialPorterBase.
        /// </summary>
        public MaterialPorterBase()
        {
        }

        #endregion

        #region Fields

        /// <summary>The JSON serializer settings.</summary>
        protected VgoJsonSerializerSettings _JsonSerializerSettings = new VgoJsonSerializerSettings();

        #endregion

        #region Properties

        /// <summary>A delegate of ExportTexture method.</summary>
        /// <remarks>for Export</remarks>
        public ExportTextureDelegate ExportTexture { get; set; }

        /// <summary>List of all texture info.</summary>
        /// <remarks>for Import</remarks>
        public List<TextureInfo> AllTextureInfoList { get; set; }

        /// <summary>List of all texture 2D.</summary>
        /// <remarks>for Import</remarks>
        public List<Texture2D> AllTexture2dList { get; set; }

        #endregion

        #region Public Methods (Export)

        /// <summary>
        /// Create a glTF material.
        /// </summary>
        /// <param name="material">A unity material.</param>
        /// <returns>A glTF material.</returns>
        public abstract GltfMaterial CreateGltfMaterial(Material material);

        #endregion

        #region Public Methods (Import)

        /// <summary>
        /// Set material texture info list.
        /// </summary>
        /// <param name="materialInfo">A material info.</param>
        /// <param name="allTextureInfoList">List of all texture info.</param>
        public abstract void SetMaterialTextureInfoList(MaterialInfo materialInfo, List<TextureInfo> allTextureInfoList);

        /// <summary>
        /// Create a unity material.
        /// </summary>
        /// <param name="src">A material info.</param>
        /// <param name="shader">A shader.</param>
        /// <returns>A unity material.</returns>
        public abstract Material CreateMaterialAsset(MaterialInfo materialInfo, Shader shader);

        #endregion

        #region Protected Methods (TextureInfo)

        /// <summary>
        /// Get a texture info and set info.
        /// </summary>
        /// <param name="gltfTextureInfo">A gltf Texture info.</param>
        /// <param name="textureType">The texture type.</param>
        /// <returns>A texture info.</returns>
        /// <remarks>Default</remarks>
        protected TextureInfo GetTextureAndSetInfo(GltfTextureInfo gltfTextureInfo, TextureType textureType = TextureType.Default)
        {
            if (gltfTextureInfo == null)
            {
                return null;
            }

            TextureInfo textureInfo = GetTextureAndSetInfo(gltfTextureInfo.index, textureType);

            if (textureInfo == null)
            {
                return null;
            }

            if (0 <= gltfTextureInfo.texCoord)
            {
                textureInfo.texCoord = gltfTextureInfo.texCoord;
            }
            else
            {
                textureInfo.texCoord = 0;
            }

            textureInfo.transform = GetTextureTransform(gltfTextureInfo);

            return textureInfo;
        }

        /// <summary>
        /// Get a texture info and set info.
        /// </summary>
        /// <param name="textureIndex">The index of the texture.</param>
        /// <param name="textureType">The texture type.</param>
        /// <returns>A texture info.</returns>
        protected TextureInfo GetTextureAndSetInfo(int textureIndex, TextureType textureType = TextureType.Default)
        {
            TextureInfo textureInfo = AllTextureInfoList.Where(ti => ti.textureIndex == textureIndex).FirstOrDefault();

            if (textureInfo == null)
            {
                return null;
            }

            textureInfo.textureType = textureType;

            switch (textureType)
            {
                case TextureType.Default:
                    textureInfo.colorSpace = ColorSpaceType.Srgb;
                    break;
                case TextureType.MetallicRoughnessMap:
                case TextureType.NormalMap:
                case TextureType.OcclusionMap:
                    textureInfo.colorSpace = ColorSpaceType.Linear;
                    break;
                case TextureType.EmissionMap:
                case TextureType.CubeMap:
                default:
                    textureInfo.colorSpace = ColorSpaceType.Srgb;
                    break;
            }

            return textureInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gltfTextureInfo">A gltf Texture info.</param>
        /// <param name="textureType">The texture type.</param>
        /// <param name="textureInfo"></param>
        /// <returns></returns>
        protected bool TryGetTextureAndSetInfo(GltfTextureInfo gltfTextureInfo, TextureType textureType, out TextureInfo textureInfo)
        {
            textureInfo = GetTextureAndSetInfo(gltfTextureInfo, textureType);

            return textureInfo != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gltfTextureInfo">A gltf Texture info.</param>
        /// <param name="textureInfo"></param>
        /// <returns></returns>
        protected bool TryGetTextureAndSetInfo(GltfTextureInfo gltfTextureInfo, out TextureInfo textureInfo)
        {
            return TryGetTextureAndSetInfo(gltfTextureInfo, TextureType.Default, out textureInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="textureIndex">The index of the texture.</param>
        /// <param name="textureType">The texture type.</param>
        /// <param name="textureInfo"></param>
        /// <returns></returns>
        protected bool TryGetTextureAndSetInfo(int textureIndex, TextureType textureType, out TextureInfo textureInfo)
        {
            textureInfo = GetTextureAndSetInfo(textureIndex, textureType);

            return textureInfo != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="textureIndex">The index of the texture.</param>
        /// <param name="textureInfo"></param>
        /// <returns></returns>
        protected bool TryGetTextureAndSetInfo(int textureIndex, out TextureInfo textureInfo)
        {
            return TryGetTextureAndSetInfo(textureIndex, TextureType.Default, out textureInfo);
        }

        /// <summary>
        /// Get a texture transform from gltf.texture.extensions.
        /// </summary>
        /// <param name="gltfTextureInfo">A gltf Texture info.</param>
        /// <returns>A texture transform.</returns>
        protected virtual KHR_texture_transform GetTextureTransform(GltfTextureInfo gltfTextureInfo)
        {
            if (gltfTextureInfo.extensions == null)
            {
                return null;
            }

            gltfTextureInfo.extensions.JsonSerializerSettings = _JsonSerializerSettings;

            try
            {
                if (gltfTextureInfo.extensions.Contains(KHR_texture_transform.ExtensionName))
                {
                    return gltfTextureInfo.extensions.GetValueOrDefault<KHR_texture_transform>(KHR_texture_transform.ExtensionName);
                }
            }
            catch
            {
                return null;
            }

            return null;
        }

        #endregion

        #region Protected Methods (Helper)

        /// <summary>
        /// Set the texture transform to the material.
        /// </summary>
        /// <param name="material">The unity material.</param>
        /// <param name="materialInfo">The material info.</param>
        /// <param name="shaderPropertyName">The shader property name.</param>
        /// <param name="textureIndex">The index of the texture.</param>
        protected virtual void SetMaterialTextureTransform(Material material, MaterialInfo materialInfo, string shaderPropertyName, int textureIndex)
        {
            if (material == null)
            {
                return;
            }

            if (materialInfo == null)
            {
                return;
            }

            if (shaderPropertyName == null)
            {
                return;
            }

            if (textureIndex == -1)
            {
                return;
            }

            TextureInfo textureInfo = materialInfo.TextureInfoList
                .Where(ti => ti.textureIndex == textureIndex)
                .FirstOrDefault();

            if (textureInfo == null)
            {
                return;
            }

            if (textureInfo.transform == null)
            {
                return;
            }

            UnityEngine.Vector2 offset = textureInfo.transform.offset.GetValueOrDefault(System.Numerics.Vector2.Zero).ToUnityVector2();
            UnityEngine.Vector2 scale = textureInfo.transform.scale.GetValueOrDefault(System.Numerics.Vector2.One).ToUnityVector2();

            // @notice
            offset.y = (offset.y + scale.y - 1.0f) * -1.0f;

            material.SetTextureOffset(shaderPropertyName, offset);
            material.SetTextureScale(shaderPropertyName, scale);
        }

        #endregion
    }
}
