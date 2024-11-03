// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : AbstractMaterialPorterBase
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Rendering;

    /// <summary>
    /// Abstract Basic Material Porter
    /// </summary>
    public abstract class AbstractMaterialPorterBase : IMaterialPorter
    {
        #region Constructors

        /// <summary>
        /// Create a new instance of AbstractMaterialPorterBase.
        /// </summary>
        public AbstractMaterialPorterBase()
        {
        }

        #endregion

        #region Properties

        /// <summary>A delegate of ExportTexture method.</summary>
        /// <remarks>for Export</remarks>
        public ExportTextureDelegate? ExportTexture { get; set; }

        #endregion

        #region Public Methods (Export)

        /// <summary>
        /// Create a vgo material.
        /// </summary>
        /// <param name="material">A unity material.</param>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <returns>A vgo material.</returns>
        public abstract VgoMaterial CreateVgoMaterial(in Material material, in IVgoStorage vgoStorage);

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
        protected virtual bool ExportProperty(VgoMaterial vgoMaterial, Material material, in int propertyIndex, bool excludeColor = false, bool excludeVector = false)
        {
            string propertyName = material.shader.GetPropertyName(propertyIndex);

            ShaderPropertyType propertyType = material.shader.GetPropertyType(propertyIndex);

            switch (propertyType)
            {
                case ShaderPropertyType.Color:
                    if (excludeColor == false)
                    {
                        vgoMaterial.colorProperties ??= new Dictionary<string, float[]>();

                        if (vgoMaterial.colorProperties.ContainsKey(propertyName))
                        {
                            Debug.LogFormat($"vgoMaterial.colorProperties {propertyName}");
                        }
                        else
                        {
                            vgoMaterial.colorProperties.Add(propertyName, material.GetColor(propertyName).linear.ToArray4());
                        }
                    }
                    break;

                case ShaderPropertyType.Vector:
                    if (excludeVector == false)
                    {
                        vgoMaterial.vectorProperties ??= new Dictionary<string, float[]>();

                        if (vgoMaterial.vectorProperties.ContainsKey(propertyName))
                        {
                            Debug.LogFormat($"vgoMaterial.vectorProperties {propertyName}");
                        }
                        else
                        {
                            vgoMaterial.vectorProperties.Add(propertyName, material.GetVector(propertyName).ToArray());
                        }
                    }
                    break;

                case ShaderPropertyType.Float:
                case ShaderPropertyType.Range:
                    {
                        vgoMaterial.floatProperties ??= new Dictionary<string, float>();

                        if (vgoMaterial.floatProperties.ContainsKey(propertyName))
                        {
                            Debug.LogFormat($"vgoMaterial.floatProperties {propertyName}");
                        }
                        else
                        {
                            vgoMaterial.floatProperties.Add(propertyName, material.GetFloat(propertyName));
                        }
                    }
                    break;

                case ShaderPropertyType.Texture:
                    //ThrowHelper.ThrowNotSupportedException($"{nameof(propertyName)}: {propertyName}, {nameof(ShaderPropertyType)}: {propertyType} ");
                    return true;

#if UNITY_2021_1_OR_NEWER
                case ShaderPropertyType.Int:
                    {
                        vgoMaterial.intProperties ??= new Dictionary<string, int>();

                        if (vgoMaterial.intProperties.ContainsKey(propertyName))
                        {
                            Debug.LogFormat($"vgoMaterial.intProperties {propertyName}");
                        }
                        else
                        {
                            vgoMaterial.intProperties.Add(propertyName, material.GetInt(propertyName));
                        }
                    }
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
        protected virtual bool ExportProperty(VgoMaterial vgoMaterial, Material material, in string propertyName, in VgoMaterialPropertyType type)
        {
            if (material.HasProperty(propertyName) == false)
            {
                return false;
            }

            switch (type)
            {
                case VgoMaterialPropertyType.Int:
                    {
                        vgoMaterial.intProperties ??= new Dictionary<string, int>();

                        if (vgoMaterial.intProperties.ContainsKey(propertyName))
                        {
                            Debug.LogFormat($"vgoMaterial.intProperties {propertyName}");
                        }
                        else
                        {
                            vgoMaterial.intProperties.Add(propertyName, material.GetInt(propertyName));
                        }
                    }
                    break;

                case VgoMaterialPropertyType.Float:
                    {
                        vgoMaterial.floatProperties ??= new Dictionary<string, float>();

                        if (vgoMaterial.floatProperties.ContainsKey(propertyName))
                        {
                            Debug.LogFormat($"vgoMaterial.floatProperties {propertyName}");
                        }
                        else
                        {
                            vgoMaterial.floatProperties.Add(propertyName, material.GetFloat(propertyName));
                        }
                    }
                    break;

                case VgoMaterialPropertyType.Color3:
                    {
                        vgoMaterial.colorProperties ??= new Dictionary<string, float[]>();

                        if (vgoMaterial.colorProperties.ContainsKey(propertyName))
                        {
                            Debug.LogFormat($"vgoMaterial.colorProperties {propertyName}");
                        }
                        else
                        {
                            vgoMaterial.colorProperties.Add(propertyName, material.GetColor(propertyName).linear.ToArray3());
                        }
                    }
                    break;

                case VgoMaterialPropertyType.Color4:
                    {
                        vgoMaterial.colorProperties ??= new Dictionary<string, float[]>();

                        if (vgoMaterial.colorProperties.ContainsKey(propertyName))
                        {
                            Debug.LogFormat($"vgoMaterial.colorProperties {propertyName}");
                        }
                        else
                        {
                            vgoMaterial.colorProperties.Add(propertyName, material.GetColor(propertyName).linear.ToArray4());
                        }
                    }
                    break;

                case VgoMaterialPropertyType.Vector2:
                    {
                        vgoMaterial.vectorProperties ??= new Dictionary<string, float[]>();

                        if (vgoMaterial.vectorProperties.ContainsKey(propertyName))
                        {
                            Debug.LogFormat($"vgoMaterial.vectorProperties {propertyName}");
                        }
                        else
                        {
                            vgoMaterial.vectorProperties.Add(propertyName, material.GetVector(propertyName).ToArray2());
                        }
                    }
                    break;

                case VgoMaterialPropertyType.Vector3:
                    {
                        vgoMaterial.vectorProperties ??= new Dictionary<string, float[]>();

                        if (vgoMaterial.vectorProperties.ContainsKey(propertyName))
                        {
                            Debug.LogFormat($"vgoMaterial.vectorProperties {propertyName}");
                        }
                        else
                        {
                            vgoMaterial.vectorProperties.Add(propertyName, material.GetVector(propertyName).ToArray3());
                        }
                    }
                    break;

                case VgoMaterialPropertyType.Vector4:
                    {
                        vgoMaterial.vectorProperties ??= new Dictionary<string, float[]>();

                        if (vgoMaterial.vectorProperties.ContainsKey(propertyName))
                        {
                            Debug.LogFormat($"vgoMaterial.vectorProperties {propertyName}");
                        }
                        else
                        {
                            vgoMaterial.vectorProperties.Add(propertyName, material.GetVector(propertyName).ToArray());
                        }
                    }
                    break;

                case VgoMaterialPropertyType.Texture:
                    ThrowHelper.ThrowException();
                    break;

                case VgoMaterialPropertyType.TextureOffset:
                    {
                        vgoMaterial.textureOffsetProperties ??= new Dictionary<string, float[]>();

                        if (vgoMaterial.textureOffsetProperties.ContainsKey(propertyName))
                        {
                            Debug.LogFormat($"vgoMaterial.textureOffsetProperties {propertyName}");
                        }
                        else
                        {
                            vgoMaterial.textureOffsetProperties.Add(propertyName, material.GetTextureOffset(propertyName).ToArray());
                        }
                    }
                    break;

                case VgoMaterialPropertyType.TextureScale:
                    {
                        vgoMaterial.textureScaleProperties ??= new Dictionary<string, float[]>();

                        if (vgoMaterial.textureScaleProperties.ContainsKey(propertyName))
                        {
                            Debug.LogFormat($"vgoMaterial.textureScaleProperties {propertyName}");
                        }
                        else
                        {
                            vgoMaterial.textureScaleProperties.Add(propertyName, material.GetTextureScale(propertyName).ToArray());
                        }
                    }
                    break;

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
        /// <param name="type">The type of property.</param>
        /// <param name="propertyName">A property name.</param>
        /// <returns></returns>
        protected virtual bool ExportProperty(VgoMaterial vgoMaterial, Material material, in VgoMaterialPropertyType type, in string propertyName)
        {
            return ExportProperty(vgoMaterial, material, propertyName, type);
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
        protected virtual bool ExportKeyword(VgoMaterial vgoMaterial, Material material, in string keyword)
        {
            vgoMaterial.keywordMap ??= new Dictionary<string, bool>();

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
        /// If searchFallbacks is false then only the currently used subshader will be queried for the tag.
        /// </param>
        /// <param name="defaultValue">If the material's shader does not define the tag, defaultValue is returned.</param>
        /// <returns></returns>
        protected virtual bool ExportTag(VgoMaterial vgoMaterial, Material material, in string tagName, in bool searchFallbacks = false, in string? defaultValue = null)
        {
            string tagValue = material.GetTag(tagName, searchFallbacks, defaultValue);

            vgoMaterial.tagMap ??= new Dictionary<string, string>();

            if (vgoMaterial.tagMap.ContainsKey(tagName))
            {
                Debug.LogFormat($"vgoMaterial.tagMap {tagName}");
            }
            else
            {
                vgoMaterial.tagMap.Add(tagName, tagValue);
            }

            return true;
        }

        /// <summary>
        /// Export a texture type property.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="material">A unity material.</param>
        /// <param name="propertyName">A property name.</param>
        /// <param name="textureMapType">Type of the texture map.</param>
        /// <param name="colorSpace">The color space.</param>
        /// <param name="metallicSmoothness">The metallic smoothness.</param>
        /// <returns></returns>
        protected virtual bool ExportTextureProperty(
            IVgoStorage vgoStorage,
            VgoMaterial vgoMaterial,
            Material material,
            in string propertyName,
            in VgoTextureMapType textureMapType = VgoTextureMapType.Default,
            in VgoColorSpaceType colorSpace = VgoColorSpaceType.Srgb,
            in float metallicSmoothness = -1.0f)
        {
            if (material.HasProperty(propertyName) == false)
            {
                return false;
            }

#if UNITY_2021_1_OR_NEWER
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

            if (ExportTexture == null)
            {
                return false;
            }

            int textureIndex = ExportTexture(vgoStorage, texture, textureMapType, colorSpace, metallicSmoothness);

            if (textureIndex == -1)
            {
                return false;
            }

            vgoMaterial.textureIndexProperties ??= new Dictionary<string, int>();

            if (vgoMaterial.textureIndexProperties.ContainsKey(propertyName))
            {
                Debug.LogFormat($"vgoMaterial.textureIndexProperties {propertyName}");

                return false;
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
        protected virtual bool ExportTextureOffset(VgoMaterial vgoMaterial, Material material, in string propertyName)
        {
            if (material.HasProperty(propertyName) == false)
            {
                return false;
            }

            Vector2 textureOffset = material.GetTextureOffset(propertyName);

            if (textureOffset != Vector2.zero)
            {
                vgoMaterial.textureOffsetProperties ??= new Dictionary<string, float[]>();

                if (vgoMaterial.textureOffsetProperties.ContainsKey(propertyName))
                {
                    Debug.LogFormat($"vgoMaterial.textureOffsetProperties {propertyName}");

                    return false;
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
        protected virtual bool ExportTextureScale(VgoMaterial vgoMaterial, Material material, in string propertyName)
        {
            if (material.HasProperty(propertyName) == false)
            {
                return false;
            }

            Vector2 textureScale = material.GetTextureScale(propertyName);

            if (textureScale != Vector2.one)
            {
                vgoMaterial.textureScaleProperties ??= new Dictionary<string, float[]>();

                if (vgoMaterial.textureScaleProperties.ContainsKey(propertyName))
                {
                    Debug.LogFormat($"vgoMaterial.textureScaleProperties {propertyName}");

                    return false;
                }

                vgoMaterial.textureScaleProperties.Add(propertyName, textureScale.ToArray());
            }

            return true;
        }

        #endregion

        #region Public Methods (Import)

        /// <summary>
        /// Create a unity material.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="shader">A shader.</param>
        /// <param name="allTextureList">List of all texture.</param>
        /// <returns>A unity material.</returns>
        public virtual Material CreateMaterialAsset(in VgoMaterial vgoMaterial, in Shader shader, in List<Texture?> allTextureList)
        {
            var material = new Material(shader)
            {
                name = vgoMaterial.name
            };

            if (vgoMaterial.renderQueue >= 0)
            {
                material.renderQueue = vgoMaterial.renderQueue;
            }

            ImportTagMap(material, vgoMaterial);

            ImportKeywords(material, vgoMaterial);

            ImportIntProperties(material, vgoMaterial);

            ImportFloatProperties(material, vgoMaterial);

            ImportMatrixProperties(material, vgoMaterial);

            ImportVectorProperties(material, vgoMaterial);

            ImportColorProperties(material, vgoMaterial);

            ImportTextureProperties(material, vgoMaterial, allTextureList);

            return material;
        }

        /// <summary>
        /// Import keywords.
        /// </summary>
        /// <param name="material">A unity material.</param>
        /// <param name="vgoMaterial">A vgo material.</param>
        protected virtual void ImportKeywords(Material material, in VgoMaterial vgoMaterial)
        {
            if (vgoMaterial.keywordMap == null)
            {
                return;
            }

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

        /// <summary>
        /// Import tag map.
        /// </summary>
        /// <param name="material">A unity material.</param>
        /// <param name="vgoMaterial">A vgo material.</param>
        protected virtual void ImportTagMap(Material material, in VgoMaterial vgoMaterial)
        {
            if (vgoMaterial.tagMap == null)
            {
                return;
            }

            foreach ((string tagName, string value) in vgoMaterial.tagMap)
            {
                if (string.IsNullOrEmpty(tagName))
                {
                    continue;
                }

                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }

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

        /// <summary>
        /// Import color properties.
        /// </summary>
        /// <param name="material">A unity material.</param>
        /// <param name="vgoMaterial">A vgo material.</param>
        protected virtual void ImportColorProperties(Material material, in VgoMaterial vgoMaterial)
        {
            if (vgoMaterial.colorProperties == null)
            {
                return;
            }

            foreach ((string propertyName, float[] value) in vgoMaterial.colorProperties)
            {
                if (value == null)
                {
                    continue;
                }

                if (material.HasProperty(propertyName)  == false)
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

        /// <summary>
        /// Import float properties.
        /// </summary>
        /// <param name="material">A unity material.</param>
        /// <param name="vgoMaterial">A vgo material.</param>
        protected virtual void ImportFloatProperties(Material material, in VgoMaterial vgoMaterial)
        {
            if (vgoMaterial.floatProperties == null)
            {
                return;
            }

            foreach ((string propertyName, float value) in vgoMaterial.floatProperties)
            {

                if (material.HasProperty(propertyName) == false)
                {
                    continue;
                }

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

        /// <summary>
        /// Import int properties.
        /// </summary>
        /// <param name="material">A unity material.</param>
        /// <param name="vgoMaterial">A vgo material.</param>
        protected virtual void ImportIntProperties(Material material, in VgoMaterial vgoMaterial)
        {
            if (vgoMaterial.intProperties == null)
            {
                return;
            }

            foreach ((string propertyName, int value) in vgoMaterial.intProperties)
            {
                if (material.HasProperty(propertyName) == false)
                {
                    continue;
                }

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

        /// <summary>
        /// Import matrix properties.
        /// </summary>
        /// <param name="material">A unity material.</param>
        /// <param name="vgoMaterial">A vgo material.</param>
        protected virtual void ImportMatrixProperties(Material material, in VgoMaterial vgoMaterial)
        {
            if (vgoMaterial.matrixProperties == null)
            {
                return;
            }

            foreach ((string propertyName, float[] value) in vgoMaterial.matrixProperties)
            {
                if (value == null)
                {
                    continue;
                }

                if (material.HasProperty(propertyName) == false)
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

        /// <summary>
        /// Import vector properties.
        /// </summary>
        /// <param name="material">A unity material.</param>
        /// <param name="vgoMaterial">A vgo material.</param>
        protected virtual void ImportVectorProperties(Material material, in VgoMaterial vgoMaterial)
        {
            if (vgoMaterial.vectorProperties == null)
            {
                return;
            }

            foreach ((string propertyName, float[] value) in vgoMaterial.vectorProperties)
            {
                if (value == null)
                {
                    continue;
                }

                if (material.HasProperty(propertyName) == false)
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

        /// <summary>
        /// Import texture properties.
        /// </summary>
        /// <param name="material">A unity material.</param>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="allTextureList">List of all texture.</param>
        protected virtual void ImportTextureProperties(Material material, in VgoMaterial vgoMaterial, in List<Texture?> allTextureList)
        {
            if (vgoMaterial.textureIndexProperties != null)
            {
                foreach ((string propertyName, int textureIndex) in vgoMaterial.textureIndexProperties)
                {
                    if (material.HasProperty(propertyName) == false)
                    {
                        continue;
                    }

                    try
                    {
                        //if (allTextureList.TryGetValue(textureIndex, out Texture texture))
                        //{
                        //    material.SetTexture(propertyName, texture);
                        //}

                        if (textureIndex.IsInRangeOf(allTextureList))
                        {
                            material.SetTexture(propertyName, allTextureList[textureIndex]);
                        }
                    }
                    catch
                    {
                        //
                    }
                }
            }

            if (vgoMaterial.textureOffsetProperties != null)
            {
                foreach ((string propertyName, float[] value) in vgoMaterial.textureOffsetProperties)
                {
                    if (value == null)
                    {
                        continue;
                    }

                    if (material.HasProperty(propertyName) == false)
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

            if (vgoMaterial.textureScaleProperties != null)
            {
                foreach ((string propertyName, float[] value) in vgoMaterial.textureScaleProperties)
                {
                    if (value == null)
                    {
                        continue;
                    }

                    if (material.HasProperty(propertyName) == false)
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

        /// <summary>
        /// Gets a list of property names.
        /// </summary>
        /// <param name="material">A unity material.</param>
        /// <returns>List of the property name.</returns>
        protected List<string> GetPropertyNameList(in Material material)
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
