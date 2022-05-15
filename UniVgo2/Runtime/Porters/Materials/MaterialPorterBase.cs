// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : MaterialPorterBase
// ----------------------------------------------------------------------
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Rendering;

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

        #region Properties

        /// <summary>A delegate of ExportTexture method.</summary>
        /// <remarks>for Export</remarks>
        public ExportTextureDelegate ExportTexture { get; set; }

        /// <summary>List of all texture 2D.</summary>
        /// <remarks>for Import</remarks>
        public List<Texture2D> AllTexture2dList { get; set; }

        #endregion

        #region Public Methods (Export)

        /// <summary>
        /// Create a vgo material.
        /// </summary>
        /// <param name="material">A unity material.</param>
        /// <returns>A vgo material.</returns>
        public abstract VgoMaterial CreateVgoMaterial(Material material);

        #endregion

        #region Protected Methods (Export)

        /// <summary>
        /// Export the properties.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="material">A unity material.</param>
        /// <param name="excludeColor">Whether to exclude properties of type color.</param>
        /// <param name="excludeVector">Whether to exclude properties of type vector.</param>
        /// <returns></returns>
        protected virtual bool ExportProperties(VgoMaterial vgoMaterial, Material material, bool excludeColor = false, bool excludeVector = false)
        {
            bool isSuccess = true;

            int propertyCount = material.shader.GetPropertyCount();

            for (int propertyIndex = 0; propertyIndex < propertyCount; propertyIndex++)
            {
                if (ExportProperty(vgoMaterial, material, propertyIndex, excludeColor, excludeVector) == false)
                {
                    isSuccess = false;
                }
            }

            return isSuccess;
        }

        /// <summary>
        /// Export a property.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="material">A unity material.</param>
        /// <param name="propertyIndex">Index of the property.</param>
        /// <param name="excludeColor">Whether to exclude property of type color.</param>
        /// <param name="excludeVector">Whether to exclude property of type vector.</param>
        /// <returns></returns>
        protected virtual bool ExportProperty(VgoMaterial vgoMaterial, Material material, int propertyIndex, bool excludeColor = false, bool excludeVector = false)
        {
            string propertyName = material.shader.GetPropertyName(propertyIndex);

            ShaderPropertyType propertyType = material.shader.GetPropertyType(propertyIndex);

            switch (propertyType)
            {
                case ShaderPropertyType.Color:
                    if (excludeColor == false)
                    {
                        if (vgoMaterial.colorProperties == null)
                        {
                            vgoMaterial.colorProperties = new Dictionary<string, float[]>();
                        }
                        vgoMaterial.colorProperties.Add(propertyName, material.GetColor(propertyName).linear.ToArray4());
                    }
                    break;

                case ShaderPropertyType.Vector:
                    if (excludeVector == false)
                    {
                        if (vgoMaterial.vectorProperties == null)
                        {
                            vgoMaterial.vectorProperties = new Dictionary<string, float[]>();
                        }
                        vgoMaterial.vectorProperties.Add(propertyName, material.GetVector(propertyName).ToArray());
                    }
                    break;

                case ShaderPropertyType.Float:
                case ShaderPropertyType.Range:
                    if (vgoMaterial.floatProperties == null)
                    {
                        vgoMaterial.floatProperties = new Dictionary<string, float>();
                    }
                    vgoMaterial.floatProperties.Add(propertyName, material.GetFloat(propertyName));
                    break;

                case ShaderPropertyType.Texture:
                    //throw new NotSupportedException($"{nameof(propertyName)}: {propertyName}, {nameof(ShaderPropertyType)}: {propertyType} ");
                    return true;

#if UNITY_2020_1_OR_NEWER
                case ShaderPropertyType.Int:
                    if (vgoMaterial.intProperties == null)
                    {
                        vgoMaterial.intProperties = new Dictionary<string, int>();
                    }
                    vgoMaterial.intProperties.Add(propertyName, material.GetInt(propertyName));
                    break;
#endif

                default:
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Export a property.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="material">A unity material.</param>
        /// <param name="propertyName">A property name.</param>
        /// <param name="type">The type of property.</param>
        /// <returns></returns>
        protected virtual bool ExportProperty(VgoMaterial vgoMaterial, Material material, string propertyName, VgoMaterialPropertyType type)
        {
            if (material.HasProperty(propertyName) == false)
            {
                return false;
            }

            switch (type)
            {
                case VgoMaterialPropertyType.Int:
                    if (vgoMaterial.intProperties == null)
                    {
                        vgoMaterial.intProperties = new Dictionary<string, int>();
                    }
                    vgoMaterial.intProperties.Add(propertyName, material.GetInt(propertyName));
                    break;

                case VgoMaterialPropertyType.Float:
                    if (vgoMaterial.floatProperties == null)
                    {
                        vgoMaterial.floatProperties = new Dictionary<string, float>();
                    }
                    vgoMaterial.floatProperties.Add(propertyName, material.GetFloat(propertyName));
                    break;

                case VgoMaterialPropertyType.Color3:
                    if (vgoMaterial.colorProperties == null)
                    {
                        vgoMaterial.colorProperties = new Dictionary<string, float[]>();
                    }
                    vgoMaterial.colorProperties.Add(propertyName, material.GetColor(propertyName).linear.ToArray3());
                    break;

                case VgoMaterialPropertyType.Color4:
                    if (vgoMaterial.colorProperties == null)
                    {
                        vgoMaterial.colorProperties = new Dictionary<string, float[]>();
                    }
                    vgoMaterial.colorProperties.Add(propertyName, material.GetColor(propertyName).linear.ToArray4());
                    break;

                case VgoMaterialPropertyType.Vector2:
                    if (vgoMaterial.vectorProperties == null)
                    {
                        vgoMaterial.vectorProperties = new Dictionary<string, float[]>();
                    }
                    vgoMaterial.vectorProperties.Add(propertyName, material.GetVector(propertyName).ToArray2());
                    break;

                case VgoMaterialPropertyType.Vector3:
                    if (vgoMaterial.vectorProperties == null)
                    {
                        vgoMaterial.vectorProperties = new Dictionary<string, float[]>();
                    }
                    vgoMaterial.vectorProperties.Add(propertyName, material.GetVector(propertyName).ToArray3());
                    break;

                case VgoMaterialPropertyType.Vector4:
                    if (vgoMaterial.vectorProperties == null)
                    {
                        vgoMaterial.vectorProperties = new Dictionary<string, float[]>();
                    }
                    vgoMaterial.vectorProperties.Add(propertyName, material.GetVector(propertyName).ToArray());
                    break;

                case VgoMaterialPropertyType.Texture:
                    throw new Exception();

                case VgoMaterialPropertyType.TextureOffset:
                    if (vgoMaterial.textureOffsetProperties == null)
                    {
                        vgoMaterial.textureOffsetProperties = new Dictionary<string, float[]>();
                    }
                    vgoMaterial.textureOffsetProperties.Add(propertyName, material.GetTextureOffset(propertyName).ToArray());
                    break;

                case VgoMaterialPropertyType.TextureScale:
                    if (vgoMaterial.textureScaleProperties == null)
                    {
                        vgoMaterial.textureScaleProperties = new Dictionary<string, float[]>();
                    }
                    vgoMaterial.textureScaleProperties.Add(propertyName, material.GetTextureScale(propertyName).ToArray());
                    break;

                default:
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Export keywords.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="material">A unity material.</param>
        /// <returns></returns>
        protected virtual bool ExportKeywords(VgoMaterial vgoMaterial, Material material)
        {
            bool isSuccess = true;

            string[] keywords = material.shaderKeywords;

            foreach (string keyword in keywords)
            {
                if (ExportKeyword(vgoMaterial, material, keyword) == false)
                {
                    isSuccess = false;
                }
            }

            return isSuccess;
        }

        /// <summary>
        /// Export keyword.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="material">A unity material.</param>
        /// <param name="keyword">keyword.</param>
        /// <returns></returns>
        protected virtual bool ExportKeyword(VgoMaterial vgoMaterial, Material material, string keyword)
        {
            if (vgoMaterial.keywordMap == null)
            {
                vgoMaterial.keywordMap = new Dictionary<string, bool>();
            }

            if (material.IsKeywordEnabled(keyword))
            {
                vgoMaterial.keywordMap.Add(keyword, true);
            }
            else
            {
                vgoMaterial.keywordMap.Add(keyword, false);
            }

            return true;
        }

        /// <summary>
        /// Export tag.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="material">A unity material.</param>
        /// <param name="tagName">A tag name.</param>
        /// <param name="searchFallbacks">
        /// If searchFallbacks is true then this function will look for tag in all subshaders and all fallbacks.
        /// If seachFallbacks is false then only the currently used subshader will be queried for the tag.
        /// </param>
        /// <param name="defaultValue">If the material's shader does not define the tag, defaultValue is returned.</param>
        /// <returns></returns>
        protected virtual bool ExportTag(VgoMaterial vgoMaterial, Material material, string tagName, bool searchFallbacks = false, string defaultValue = null)
        {
            string tagValue = material.GetTag(tagName, searchFallbacks, defaultValue);

            if (vgoMaterial.tagMap == null)
            {
                vgoMaterial.tagMap = new Dictionary<string, string>();
            }

            vgoMaterial.tagMap.Add(tagName, tagValue);

            return true;
        }

        /// <summary>
        /// Export a texture type property.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="material">A unity material.</param>
        /// <param name="propertyName">A property name.</param>
        /// <param name="textureMapType">Type of the texture map.</param>
        /// <param name="colorSpace">The color space.</param>
        /// <param name="metallicSmoothness">The metallic smoothness.</param>
        /// <returns></returns>
        public virtual bool ExportTextureProperty(VgoMaterial vgoMaterial, Material material, string propertyName, VgoTextureMapType textureMapType = VgoTextureMapType.Default, VgoColorSpaceType colorSpace = VgoColorSpaceType.Srgb, float metallicSmoothness = -1.0f)
        {
            if (material.HasProperty(propertyName) == false)
            {
                return false;
            }

#if UNITY_2020_1_OR_NEWER
            if (material.HasTexture(propertyName) == false)
            {
                return false;
            }
#endif

            Texture texture = material.GetTexture(propertyName);

            if (texture == null)
            {
                return false;
            }

            int textureIndex = ExportTexture(material, texture, textureMapType, colorSpace, metallicSmoothness);

            if (textureIndex == -1)
            {
                return false;
            }

            if (vgoMaterial.textureIndexProperties == null)
            {
                vgoMaterial.textureIndexProperties = new Dictionary<string, int>();
            }

            vgoMaterial.textureIndexProperties.Add(propertyName, textureIndex);

            ExportTextureOffset(vgoMaterial, material, propertyName);
            ExportTextureScale(vgoMaterial, material, propertyName);

            return true;
        }

        /// <summary>
        /// Export a texture offset.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="material">A unity material.</param>
        /// <param name="propertyName">A property name.</param>
        /// <returns></returns>
        public virtual bool ExportTextureOffset(VgoMaterial vgoMaterial, Material material, string propertyName)
        {
            if (material.HasProperty(propertyName) == false)
            {
                return false;
            }

            Vector2 textureOffset = material.GetTextureOffset(propertyName);

            if (textureOffset != Vector2.zero)
            {
                if (vgoMaterial.textureOffsetProperties == null)
                {
                    vgoMaterial.textureOffsetProperties = new Dictionary<string, float[]>();
                }
                vgoMaterial.textureOffsetProperties.Add(propertyName, textureOffset.ToArray());
            }

            return true;
        }

        /// <summary>
        /// Export a texture scale.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="material">A unity material.</param>
        /// <param name="propertyName">A property name.</param>
        /// <returns></returns>
        public virtual bool ExportTextureScale(VgoMaterial vgoMaterial, Material material, string propertyName)
        {
            if (material.HasProperty(propertyName) == false)
            {
                return false;
            }

            Vector2 textureScale = material.GetTextureScale(propertyName);

            if (textureScale != Vector2.one)
            {
                if (vgoMaterial.textureScaleProperties == null)
                {
                    vgoMaterial.textureScaleProperties = new Dictionary<string, float[]>();
                }
                vgoMaterial.textureScaleProperties.Add(propertyName, textureScale.ToArray());
            }

            return true;
        }

        #endregion

        #region Public Methods (Import)

        ///// <summary>
        ///// Set material texture info list.
        ///// </summary>
        ///// <param name="materialInfo">A material info.</param>
        ///// <param name="allTextureInfoList">List of all texture info.</param>
        //public abstract void SetMaterialTextureInfoList(MaterialInfo materialInfo, List<TextureInfo> allTextureInfoList);

        /// <summary>
        /// Create a unity material.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="shader">A shader.</param>
        /// <returns>A unity material.</returns>
        public virtual Material CreateMaterialAsset(VgoMaterial vgoMaterial, Shader shader)
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

            if (vgoMaterial.tagMap != null)
            {
                foreach ((string tagName, string value) in vgoMaterial.tagMap)
                {
                    if (string.IsNullOrEmpty(value) == false)
                    {
                        try
                        {
                            material.SetOverrideTag(tagName, value);
                        }
                        catch
                        {
                            //
                        }
                    }
                }
            }

            if (vgoMaterial.keywordMap != null)
            {
                foreach ((string propertyName, bool value) in vgoMaterial.keywordMap)
                {
                    try
                    {
                        if (value == true)
                        {
                            material.EnableKeyword(propertyName);
                        }
                        else
                        {
                            material.DisableKeyword(propertyName);
                        }
                    }
                    catch
                    {
                        //
                    }
                }
            }

            if (vgoMaterial.intProperties != null)
            {
                foreach ((string propertyName, int value) in vgoMaterial.intProperties)
                {
                    if (material.HasProperty(propertyName))
                    {
                        try
                        {
                            material.SetInt(propertyName, value);
                        }
                        catch
                        {
                            //
                        }
                    }
                }
            }

            if (vgoMaterial.floatProperties != null)
            {
                foreach ((string propertyName, float value) in vgoMaterial.floatProperties)
                {
                    if (material.HasProperty(propertyName))
                    {
                        try
                        {
                            material.SetFloat(propertyName, value);
                        }
                        catch
                        {
                            //
                        }
                    }
                }
            }

            if (vgoMaterial.colorProperties != null)
            {
                foreach ((string propertyName, float[] value) in vgoMaterial.colorProperties)
                {
                    if (material.HasProperty(propertyName))
                    {
                        if (value == null)
                        {
                            continue;
                        }

                        try
                        {
                            if ((value.Length == 3) ||
                                (value.Length == 4))
                            {
                                material.SetColor(propertyName, ArrayConverter.ToColor(value, gamma: true));
                            }
                            else
                            {
                                //
                            }
                        }
                        catch
                        {
                            //
                        }
                    }
                }
            }

            if (vgoMaterial.vectorProperties != null)
            {
                foreach ((string propertyName, float[] value) in vgoMaterial.vectorProperties)
                {
                    if (material.HasProperty(propertyName))
                    {
                        if (value == null)
                        {
                            continue;
                        }

                        try
                        {
                            if (value.Length == 2)
                            {
                                material.SetVector(propertyName, ArrayConverter.ToVector2(value));
                            }
                            else if (value.Length == 3)
                            {
                                material.SetVector(propertyName, ArrayConverter.ToVector3(value));
                            }
                            else if (value.Length == 4)
                            {
                                material.SetVector(propertyName, ArrayConverter.ToVector4(value));
                            }
                            else
                            {
                                //
                            }
                        }
                        catch
                        {
                            //
                        }
                    }
                }
            }

            if (vgoMaterial.textureIndexProperties != null)
            {
                foreach ((string propertyName, int textureIndex) in vgoMaterial.textureIndexProperties)
                {
                    if (material.HasProperty(propertyName))
                    {
                        try
                        {
                            //if (AllTexture2dList.TryGetValue(textureIndex, out Texture2D texture))
                            //{
                            //    material.SetTexture(propertyName, texture);
                            //}

                            if (textureIndex.IsInRangeOf(AllTexture2dList))
                            {
                                material.SetTexture(propertyName, AllTexture2dList[textureIndex]);
                            }
                        }
                        catch
                        {
                            //
                        }
                    }
                }
            }

            if (vgoMaterial.textureOffsetProperties != null)
            {
                foreach ((string propertyName, float[] value) in vgoMaterial.textureOffsetProperties)
                {
                    if (material.HasProperty(propertyName))
                    {
                        if (value == null)
                        {
                            continue;
                        }

                        try
                        {
                            if (value.Length == 2)
                            {
                                material.SetTextureOffset(propertyName, ArrayConverter.ToVector2(value));
                            }
                            else
                            {
                                //
                            }
                        }
                        catch
                        {
                            //
                        }
                    }
                }
            }

            if (vgoMaterial.textureScaleProperties != null)
            {
                foreach ((string propertyName, float[] value) in vgoMaterial.textureScaleProperties)
                {
                    if (material.HasProperty(propertyName))
                    {
                        if (value == null)
                        {
                            continue;
                        }

                        try
                        {
                            if (value.Length == 2)
                            {
                                material.SetTextureScale(propertyName, ArrayConverter.ToVector2(value));
                            }
                            else
                            {
                                //
                            }
                        }
                        catch
                        {
                            //
                        }
                    }
                }
            }

            if (vgoMaterial.matrixProperties != null)
            {
                foreach ((string propertyName, float[] value) in vgoMaterial.matrixProperties)
                {
                    if (material.HasProperty(propertyName))
                    {
                        if (value == null)
                        {
                            continue;
                        }

                        try
                        {
                            if (value.Length == 16)
                            {
                                material.SetMatrix(propertyName, ArrayConverter.ToMatrix4x4(value));
                            }
                            else
                            {
                                //
                            }
                        }
                        catch
                        {
                            //
                        }
                    }
                }
            }

            return material;
        }

        /// <summary>
        /// Gets a list of property names.
        /// </summary>
        /// <param name="material">A unity material.</param>
        /// <returns>List of the property name.</returns>
        protected List<string> GetPropertyNameList(Material material)
        {
            int propertyCount = material.shader.GetPropertyCount();

            var propertyNameList = new List<string>(propertyCount);

            for (int propertyIndex = 0; propertyIndex < propertyCount; propertyIndex++)
            {
                propertyNameList.Add(material.shader.GetPropertyName(propertyIndex));
            }

            return propertyNameList;
        }

        #endregion
    }
}
