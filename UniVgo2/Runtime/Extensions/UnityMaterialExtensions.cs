// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : UnityMaterialExtensions
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using LilToonShader;
    using System;
    using UnityEngine;

    /// <summary>
    /// UnityEngine.Material Extensions
    /// </summary>
    public static class UnityMaterialExtensions
    {
        #region Methods (Get)

        /// <summary>
        /// Gets bool value.
        /// </summary>
        /// <param name="material">A material.</param>
        /// <param name="propertyNameId">A material property name ID.</param>
        /// <returns></returns>
        public static bool GetSafeBool(this Material material, int propertyNameId)
        {
#if UNITY_2021_2_OR_NEWER
            if (material.HasInt(propertyNameId))
            {
                return material.GetInt(propertyNameId) == 1;
            }
            else if (material.HasProperty(propertyNameId))
            {
                Debug.LogError($"{material.name} {propertyNameId} property type is not int.");

                return default;
            }
#else
            if (material.HasProperty(propertyNameId))
            {
                return material.GetInt(propertyNameId) == 1;
            }
#endif
            else
            {
                Debug.LogError($"{material.name} don't have {propertyNameId} property.");

                return false;
            }
        }

        /// <summary>
        /// Gets bool value.
        /// </summary>
        /// <param name="material">A material.</param>
        /// <param name="propertyName">A material property name.</param>
        /// <returns></returns>
        public static bool GetSafeBool(this Material material, string propertyName)
        {
#if UNITY_2021_2_OR_NEWER
            if (material.HasInt(propertyName))
            {
                return material.GetInt(propertyName) == 1;
            }
            else if (material.HasProperty(propertyName))
            {
                Debug.LogError($"{material.name} {propertyName} property type is not int.");

                return default;
            }
#else
            if (material.HasProperty(propertyName))
            {
                return material.GetInt(propertyName) == 1;
            }
#endif
            else
            {
                Debug.LogError($"{material.name} don't have {propertyName} property.");

                return false;
            }
        }

        /// <summary>
        /// Gets color value.
        /// </summary>
        /// <param name="material">A material.</param>
        /// <param name="propertyNameId">A material property name ID.</param>
        /// <returns></returns>
        public static Color GetSafeColor(this Material material, int propertyNameId)
        {
#if UNITY_2021_2_OR_NEWER
            if (material.HasColor(propertyNameId))
            {
                return material.GetColor(propertyNameId);
            }
            else if (material.HasProperty(propertyNameId))
            {
                Debug.LogError($"{material.name} {propertyNameId} property type is not Color.");

                return default;
            }
#else
            if (material.HasProperty(propertyNameId))
            {
                return material.GetColor(propertyNameId);
            }
#endif
            else
            {
                Debug.LogError($"{material.name} don't have {propertyNameId} property.");

                return default;
            }
        }

        /// <summary>
        /// Gets color value.
        /// </summary>
        /// <param name="material">A material.</param>
        /// <param name="propertyNameId">A material property name.</param>
        /// <returns></returns>
        public static Color GetSafeColor(this Material material, string propertyName)
        {
#if UNITY_2021_2_OR_NEWER
            if (material.HasColor(propertyName))
            {
                return material.GetColor(propertyName);
            }
            else if (material.HasProperty(propertyName))
            {
                Debug.LogError($"{material.name} {propertyName} property type is not Color.");

                return default;
            }
#else
            if (material.HasProperty(propertyName))
            {
                return material.GetColor(propertyName);
            }
#endif
            else
            {
                Debug.LogError($"{material.name} don't have {propertyName} property.");

                return default;
            }
        }

        /// <summary>
        /// Gets enum value.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="material">A material.</param>
        /// <param name="propertyNameId">A material property name ID.</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TEnum GetSafeEnum<TEnum>(this Material material, int propertyNameId, TEnum? defaultValue = null) where TEnum : struct
        {
            int propertyValue = material.GetSafeInt(propertyNameId);

            if (Enum.TryParse(propertyValue.ToString(), out TEnum result))
            {
                return result;
            }
            else if (defaultValue.HasValue)
            {
                return defaultValue.Value;
            }
            else
            {
                return default;
            }
        }

        /// <summary>
        /// Gets enum value.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="material">A material.</param>
        /// <param name="propertyName">A material property name.</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TEnum GetSafeEnum<TEnum>(this Material material, string propertyName, TEnum? defaultValue = null) where TEnum : struct
        {
            int propertyValue = material.GetSafeInt(propertyName);

            if (Enum.TryParse(propertyValue.ToString(), out TEnum result))
            {
                return result;
            }
            else if (defaultValue.HasValue)
            {
                return defaultValue.Value;
            }
            else
            {
                return default;
            }
        }

        /// <summary>
        /// Gets float value.
        /// </summary>
        /// <param name="material">A material.</param>
        /// <param name="propertyNameId">A material property name ID.</param>
        /// <returns></returns>
        public static float GetSafeFloat(this Material material, int propertyNameId)
        {
#if UNITY_2021_2_OR_NEWER
            if (material.HasFloat(propertyNameId))
            {
                return material.GetFloat(propertyNameId);
            }
            else if (material.HasProperty(propertyNameId))
            {
                Debug.LogError($"{material.name} {propertyNameId} property type is not float.");

                return default;
            }
#else
            if (material.HasProperty(propertyNameId))
            {
                return material.GetFloat(propertyNameId);
            }
#endif
            else
            {
                Debug.LogError($"{material.name} don't have {propertyNameId} property.");

                return default;
            }
        }

        /// <summary>
        /// Gets float value.
        /// </summary>
        /// <param name="material">A material.</param>
        /// <param name="propertyName">A material property name.</param>
        /// <returns></returns>
        public static float GetSafeFloat(this Material material, string propertyName)
        {
#if UNITY_2021_2_OR_NEWER
            if (material.HasFloat(propertyName))
            {
                return material.GetFloat(propertyName);
            }
            else if (material.HasProperty(propertyName))
            {
                Debug.LogError($"{material.name} {propertyName} property type is not float.");

                return default;
            }
#else
            if (material.HasProperty(propertyName))
            {
                return material.GetFloat(propertyName);
            }
#endif
            else
            {
                Debug.LogError($"{material.name} don't have {propertyName} property.");

                return default;
            }
        }

        /// <summary>
        /// Gets int value.
        /// </summary>
        /// <param name="material">A material.</param>
        /// <param name="propertyNameId">A material property name ID.</param>
        /// <returns></returns>
        public static int GetSafeInt(this Material material, int propertyNameId)
        {
            //return material.GetSafeInteger(propertyNameId);

#if UNITY_2021_2_OR_NEWER
            if (material.HasInt(propertyNameId))
            {
                return material.GetInt(propertyNameId);
            }
            else if (material.HasProperty(propertyNameId))
            {
                Debug.LogError($"{material.name} {propertyNameId} property type is not int.");

                return default;
            }
#else
            if (material.HasProperty(propertyNameId))
            {
                return material.GetInt(propertyNameId);
            }
#endif
            else
            {
                Debug.LogError($"{material.name} don't have {propertyNameId} property.");

                return default;
            }
        }

        /// <summary>
        /// Gets int value.
        /// </summary>
        /// <param name="material">A material.</param>
        /// <param name="propertyName">A material property name.</param>
        /// <returns></returns>
        public static int GetSafeInt(this Material material, string propertyName)
        {
            //return material.GetSafeInteger(propertyName);

#if UNITY_2021_2_OR_NEWER
            if (material.HasInt(propertyName))
            {
                return material.GetInt(propertyName);
            }
            else if (material.HasProperty(propertyName))
            {
                Debug.LogError($"{material.name} {propertyName} property type is not int.");

                return default;
            }
#else
            if (material.HasProperty(propertyName))
            {
                return material.GetInt(propertyName);
            }
#endif
            else
            {
                Debug.LogError($"{material.name} don't have {propertyName} property.");

                return default;
            }
        }

        /// <summary>
        /// Gets int value.
        /// </summary>
        /// <param name="material">A material.</param>
        /// <param name="propertyNameId">A material property name ID.</param>
        /// <returns></returns>
        public static int GetSafeInteger(this Material material, int propertyNameId)
        {
#if UNITY_2021_2_OR_NEWER
            if (material.HasInteger(propertyNameId))
            {
                return material.GetInteger(propertyNameId);
            }
            else if (material.HasProperty(propertyNameId))
            {
                Debug.LogError($"{material.name} {propertyNameId} property type is not integer.");

                return default;
            }
            else
            {
                Debug.LogError($"{material.name} don't have {propertyNameId} property.");

                return default;
            }
#else
            return GetSafeInt(material, propertyNameId);
#endif
        }

        /// <summary>
        /// Gets int value.
        /// </summary>
        /// <param name="material">A material.</param>
        /// <param name="propertyName">A material property name.</param>
        /// <returns></returns>
        public static int GetSafeInteger(this Material material, string propertyName)
        {
#if UNITY_2021_2_OR_NEWER
            if (material.HasInteger(propertyName))
            {
                return material.GetInteger(propertyName);
            }
            else if (material.HasProperty(propertyName))
            {
                Debug.LogError($"{material.name} {propertyName} property type is not integer.");

                return default;
            }
            else
            {
                Debug.LogError($"{material.name} don't have {propertyName} property.");

                return default;
            }
#else
            return GetSafeInt(material, propertyName);
#endif
        }

        /// <summary>
        /// Gets the Texture.
        /// </summary>
        /// <param name="material">A material.</param>
        /// <param name="propertyNameId">A material property name ID.</param>
        /// <returns></returns>
        public static Texture2D? GetSafeTexture(this Material material, int propertyNameId)
        {
#if UNITY_2021_2_OR_NEWER
            if (material.HasTexture(propertyNameId))
            {
                Texture? texture = material.GetTexture(propertyNameId);

                if (texture == null)
                {
                    return default;
                }

                return texture as Texture2D;
            }
            else if (material.HasProperty(propertyNameId))
            {
                Debug.LogError($"{material.name} {propertyNameId} property type is not Texture.");

                return default;
            }
#else
            if (material.HasProperty(propertyNameId))
            {
                return (Texture2D)material.GetTexture(propertyNameId);
            }
#endif
            else
            {
                Debug.LogError($"{material.name} don't have {propertyNameId} property.");

                return default;
            }
        }

        /// <summary>
        /// Gets the Texture.
        /// </summary>
        /// <param name="material">A material.</param>
        /// <param name="propertyName">A material property name.</param>
        /// <returns></returns>
        public static Texture2D? GetSafeTexture(this Material material, string propertyName)
        {
#if UNITY_2021_2_OR_NEWER
            if (material.HasTexture(propertyName))
            {
                Texture? texture = material.GetTexture(propertyName);

                if (texture == null)
                {
                    return default;
                }

                return texture as Texture2D;
            }
            else if (material.HasProperty(propertyName))
            {
                Debug.LogError($"{material.name} {propertyName} property type is not Texture.");

                return default;
            }
#else
            if (material.HasProperty(propertyName))
            {
                return (Texture2D)material.GetTexture(propertyName);
            }
#endif
            else
            {
                Debug.LogError($"{material.name} don't have {propertyName} property.");

                return default;
            }
        }

        /// <summary>
        /// Gets Vector3 value.
        /// </summary>
        /// <param name="material">A material.</param>
        /// <param name="propertyNameId">A material property name ID.</param>
        /// <returns></returns>
        public static Vector3 GetSafeVector3(this Material material, int propertyNameId)
        {
            return material.GetSafeVector4(propertyNameId);
        }

        /// <summary>
        /// Gets Vector3 value.
        /// </summary>
        /// <param name="material">A material.</param>
        /// <param name="propertyName">A material property name.</param>
        /// <returns></returns>
        public static Vector3 GetSafeVector3(this Material material, string propertyName)
        {
            return material.GetSafeVector4(propertyName);
        }

        /// <summary>
        /// Gets Vector4 value.
        /// </summary>
        /// <param name="material">A material.</param>
        /// <param name="propertyNameId">A material property name ID.</param>
        /// <returns></returns>
        public static Vector4 GetSafeVector4(this Material material, int propertyNameId)
        {
#if UNITY_2021_2_OR_NEWER
            if (material.HasVector(propertyNameId))
            {
                return material.GetVector(propertyNameId);
            }
            else if (material.HasProperty(propertyNameId))
            {
                Debug.LogError($"{material.name} {propertyNameId} property type is not Vector.");

                return default;
            }
#else
            if (material.HasProperty(propertyNameId))
            {
                return material.GetVector(propertyNameId);
            }
#endif
            else
            {
                Debug.LogError($"{material.name} don't have {propertyNameId} property.");

                return default;
            }
        }

        /// <summary>
        /// Gets Vector4 value.
        /// </summary>
        /// <param name="material">A material.</param>
        /// <param name="propertyName">A material property name.</param>
        /// <returns></returns>
        public static Vector4 GetSafeVector4(this Material material, string propertyName)
        {
#if UNITY_2021_2_OR_NEWER
            if (material.HasVector(propertyName))
            {
                return material.GetVector(propertyName);
            }
            else if (material.HasProperty(propertyName))
            {
                Debug.LogError($"{material.name} {propertyName} property type is not Vector.");

                return default;
            }
#else
            if (material.HasProperty(propertyName))
            {
                return material.GetVector(propertyName);
            }
#endif
            else
            {
                Debug.LogError($"{material.name} don't have {propertyName} property.");

                return default;
            }
        }

        #endregion

        #region Methods (Set)

        /// <summary>
        /// Sets bool value to property.
        /// </summary>
        /// <param name="material">A lilToon material.</param>
        /// <param name="propertyNameId">A material property name ID.</param>
        /// <param name="value"></param>
        /// <returns>Whether it could be set.</returns>
        public static bool SetSafeBool(this Material material, int propertyNameId, bool value)
        {
#if UNITY_2021_2_OR_NEWER
            if (material.HasInt(propertyNameId))
            {
                material.SetInt(propertyNameId, (value == true) ? 1 : 0);

                return true;
            }
            else if (material.HasProperty(propertyNameId))
            {
                Debug.LogError($"{material.name} {propertyNameId} property type is not int.");

                return default;
            }
#else
            if (material.HasProperty(propertyNameId))
            {
                material.SetInt(propertyNameId, (value == true) ? 1 : 0);

                return true;
            }
#endif
            else
            {
                Debug.LogWarning($"{material.name} don't have {propertyNameId} property.");

                return false;
            }
        }

        /// <summary>
        /// Sets bool value to property.
        /// </summary>
        /// <param name="material">A lilToon material.</param>
        /// <param name="propertyName">A material property name.</param>
        /// <param name="value"></param>
        /// <returns>Whether it could be set.</returns>
        public static bool SetSafeBool(this Material material, string propertyName, bool value)
        {
#if UNITY_2021_2_OR_NEWER
            if (material.HasInt(propertyName))
            {
                material.SetInt(propertyName, (value == true) ? 1 : 0);

                return true;
            }
            else if (material.HasProperty(propertyName))
            {
                Debug.LogError($"{material.name} {propertyName} property type is not int.");

                return default;
            }
#else
            if (material.HasProperty(propertyName))
            {
                material.SetInt(propertyName, (value == true) ? 1 : 0);

                return true;
            }
#endif
            else
            {
                Debug.LogWarning($"{material.name} don't have {propertyName} property.");

                return false;
            }
        }

        /// <summary>
        /// Sets int value.
        /// </summary>
        /// <param name="material">A lilToon material.</param>
        /// <param name="propertyNameId">A material property name ID.</param>
        /// <param name="value"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns>Whether it could be set.</returns>
        public static bool SetSafeInt(this Material material, int propertyNameId, int value, int? minValue = null, int? maxValue = null, int defaultValue = default)
        {
            //return material.SetSafeInteger(propertyName, value, minValue, maxValue, defaultValue);

#if UNITY_2021_2_OR_NEWER
            if (material.HasInt(propertyNameId))
            {
                int setValue = value;

                if (minValue.HasValue && value < minValue)
                {
                    setValue = defaultValue;
                }
                else if (maxValue.HasValue && value > maxValue)
                {
                    setValue = defaultValue;
                }

                material.SetInt(propertyNameId, setValue);

                return true;
            }
            else if (material.HasProperty(propertyNameId))
            {
                Debug.LogError($"{material.name} {propertyNameId} property type is not int.");

                return default;
            }
#else
            if (material.HasProperty(propertyNameId))
            {
                int setValue = value;

                if (minValue.HasValue && value < minValue)
                {
                    setValue = defaultValue;
                }
                else if (maxValue.HasValue && value > maxValue)
                {
                    setValue = defaultValue;
                }

                material.SetInt(propertyNameId, setValue);

                return true;
            }
#endif
            else
            {
                Debug.LogWarning($"{material.name} don't have {propertyNameId} property.");

                return false;
            }
        }

        /// <summary>
        /// Sets int value.
        /// </summary>
        /// <param name="material">A lilToon material.</param>
        /// <param name="propertyName">A material property name.</param>
        /// <param name="value"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns>Whether it could be set.</returns>
        public static bool SetSafeInt(this Material material, string propertyName, int value, int? minValue = null, int? maxValue = null, int defaultValue = default)
        {
            //return material.SetSafeInteger(propertyName, value, minValue, maxValue, defaultValue);

#if UNITY_2021_2_OR_NEWER
            if (material.HasInt(propertyName))
            {
                int setValue = value;

                if (minValue.HasValue && value < minValue)
                {
                    setValue = defaultValue;
                }
                else if (maxValue.HasValue && value > maxValue)
                {
                    setValue = defaultValue;
                }

                material.SetInt(propertyName, setValue);

                return true;
            }
            else if (material.HasProperty(propertyName))
            {
                Debug.LogError($"{material.name} {propertyName} property type is not int.");

                return default;
            }
#else
            if (material.HasProperty(propertyName))
            {
                int setValue = value;

                if (minValue.HasValue && value < minValue)
                {
                    setValue = defaultValue;
                }
                else if (maxValue.HasValue && value > maxValue)
                {
                    setValue = defaultValue;
                }

                material.SetInt(propertyName, setValue);

                return true;
            }
#endif
            else
            {
                Debug.LogWarning($"{material.name} don't have {propertyName} property.");

                return false;
            }
        }

        /// <summary>
        /// Sets int value.
        /// </summary>
        /// <param name="material">A lilToon material.</param>
        /// <param name="propertyNameId">A material property name ID.</param>
        /// <param name="value"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns>Whether it could be set.</returns>
        public static bool SetSafeInteger(this Material material, int propertyNameId, int value, int? minValue = null, int? maxValue = null, int defaultValue = default)
        {
#if UNITY_2021_2_OR_NEWER
            if (material.HasInteger(propertyNameId))
            {
                int setValue = value;

                if (minValue.HasValue && value < minValue)
                {
                    setValue = defaultValue;
                }
                else if (maxValue.HasValue && value > maxValue)
                {
                    setValue = defaultValue;
                }

                material.SetInteger(propertyNameId, setValue);

                return true;
            }
            else if (material.HasProperty(propertyNameId))
            {
                Debug.LogError($"{material.name} {propertyNameId} property type is not integer.");

                return default;
            }
            else
            {
                Debug.LogWarning($"{material.name} don't have {propertyNameId} property.");

                return false;
            }
#else
            return SetSafeInt(material, propertyNameId, value, minValue, maxValue, defaultValue);
#endif
        }

        /// <summary>
        /// Sets int value.
        /// </summary>
        /// <param name="material">A lilToon material.</param>
        /// <param name="propertyName">A material property name.</param>
        /// <param name="value"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns>Whether it could be set.</returns>
        public static bool SetSafeInteger(this Material material, string propertyName, int value, int? minValue = null, int? maxValue = null, int defaultValue = default)
        {
#if UNITY_2021_2_OR_NEWER
            if (material.HasInteger(propertyName))
            {
                int setValue = value;

                if (minValue.HasValue && value < minValue)
                {
                    setValue = defaultValue;
                }
                else if (maxValue.HasValue && value > maxValue)
                {
                    setValue = defaultValue;
                }

                material.SetInteger(propertyName, setValue);

                return true;
            }
            else if (material.HasProperty(propertyName))
            {
                Debug.LogError($"{material.name} {propertyName} property type is not integer.");

                return default;
            }
            else
            {
                Debug.LogWarning($"{material.name} don't have {propertyName} property.");

                return false;
            }
#else
            return SetSafeInt(material, propertyName, value, minValue, maxValue, defaultValue);
#endif
        }

        /// <summary>
        /// Sets float value.
        /// </summary>
        /// <param name="material">A lilToon material.</param>
        /// <param name="propertyNameId">A material property name ID.</param>
        /// <param name="value"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns>Whether it could be set.</returns>
        public static bool SetSafeFloat(this Material material, int propertyNameId, float value, float? minValue = null, float? maxValue = null, float defaultValue = default)
        {
#if UNITY_2021_2_OR_NEWER
            if (material.HasFloat(propertyNameId))
            {
                float setValue = value;

                if (minValue.HasValue && value < minValue)
                {
                    setValue = defaultValue;
                }
                else if (maxValue.HasValue && value > maxValue)
                {
                    setValue = defaultValue;
                }

                material.SetFloat(propertyNameId, setValue);

                return true;
            }
            else if (material.HasProperty(propertyNameId))
            {
                Debug.LogError($"{material.name} {propertyNameId} property type is not float.");

                return default;
            }
#else
            if (material.HasProperty(propertyNameId))
            {
                float setValue = value;

                if (minValue.HasValue && value < minValue)
                {
                    setValue = defaultValue;
                }
                else if (maxValue.HasValue && value > maxValue)
                {
                    setValue = defaultValue;
                }

                material.SetFloat(propertyNameId, setValue);

                return true;
            }
#endif
            else
            {
                Debug.LogWarning($"{material.name} don't have {propertyNameId} property.");

                return false;
            }
        }

        /// <summary>
        /// Sets float value.
        /// </summary>
        /// <param name="material">A lilToon material.</param>
        /// <param name="propertyName">A material property name.</param>
        /// <param name="value"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns>Whether it could be set.</returns>
        public static bool SetSafeFloat(this Material material, string propertyName, float value, float? minValue = null, float? maxValue = null, float defaultValue = default)
        {
#if UNITY_2021_2_OR_NEWER
            if (material.HasFloat(propertyName))
            {
                float setValue = value;

                if (minValue.HasValue && value < minValue)
                {
                    setValue = defaultValue;
                }
                else if (maxValue.HasValue && value > maxValue)
                {
                    setValue = defaultValue;
                }

                material.SetFloat(propertyName, setValue);

                return true;
            }
            else if (material.HasProperty(propertyName))
            {
                Debug.LogError($"{material.name} {propertyName} property type is not float.");

                return default;
            }
#else
            if (material.HasProperty(propertyName))
            {
                float setValue = value;

                if (minValue.HasValue && value < minValue)
                {
                    setValue = defaultValue;
                }
                else if (maxValue.HasValue && value > maxValue)
                {
                    setValue = defaultValue;
                }

                material.SetFloat(propertyName, setValue);

                return true;
            }
#endif
            else
            {
                Debug.LogWarning($"{material.name} don't have {propertyName} property.");

                return false;
            }
        }

        /// <summary>
        /// Sets color value.
        /// </summary>
        /// <param name="material">A lilToon material.</param>
        /// <param name="propertyNameId">A material property name ID.</param>
        /// <param name="color"></param>
        /// <returns>Whether it could be set.</returns>
        public static bool SetSafeColor(this Material material, int propertyNameId, Color color)
        {
#if UNITY_2021_2_OR_NEWER
            if (material.HasColor(propertyNameId))
            {
                material.SetColor(propertyNameId, color);

                return true;
            }
            else if (material.HasProperty(propertyNameId))
            {
                Debug.LogError($"{material.name} {propertyNameId} property type is not Color.");

                return default;
            }
#else
            if (material.HasProperty(propertyNameId))
            {
                material.SetColor(propertyNameId, color);

                return true;
            }
#endif
            else
            {
                Debug.LogWarning($"{material.name} don't have {propertyNameId} property.");

                return false;
            }
        }

        /// <summary>
        /// Sets color value.
        /// </summary>
        /// <param name="material">A lilToon material.</param>
        /// <param name="propertyName">A material property name.</param>
        /// <param name="color"></param>
        /// <returns>Whether it could be set.</returns>
        public static bool SetSafeColor(this Material material, string propertyName, Color color)
        {
#if UNITY_2021_2_OR_NEWER
            if (material.HasColor(propertyName))
            {
                material.SetColor(propertyName, color);

                return true;
            }
            else if (material.HasProperty(propertyName))
            {
                Debug.LogError($"{material.name} {propertyName} property type is not Color.");

                return default;
            }
#else
            if (material.HasProperty(propertyName))
            {
                material.SetColor(propertyName, color);

                return true;
            }
#endif
            else
            {
                Debug.LogWarning($"{material.name} don't have {propertyName} property.");

                return false;
            }
        }

        /// <summary>
        /// Sets the Texture.
        /// </summary>
        /// <param name="material">A lilToon material.</param>
        /// <param name="propertyNameId">A material property name ID.</param>
        /// <param name="texture"></param>
        /// <returns>Whether it could be set.</returns>
        public static bool SetSafeTexture(this Material material, int propertyNameId, Texture2D texture)
        {
#if UNITY_2021_2_OR_NEWER
            if (material.HasTexture(propertyNameId))
            {
                material.SetTexture(propertyNameId, texture);

                return true;
            }
            else if (material.HasProperty(propertyNameId))
            {
                Debug.LogError($"{material.name} {propertyNameId} property type is not Texture.");

                return default;
            }
#else
            if (material.HasProperty(propertyNameId))
            {
                material.SetTexture(propertyNameId, texture);

                return true;
            }
#endif
            else
            {
                Debug.LogWarning($"{material.name} don't have {propertyNameId} property.");

                return false;
            }
        }

        /// <summary>
        /// Sets the Texture.
        /// </summary>
        /// <param name="material">A lilToon material.</param>
        /// <param name="propertyName">A material property name.</param>
        /// <param name="texture"></param>
        /// <returns>Whether it could be set.</returns>
        public static bool SetSafeTexture(this Material material, string propertyName, Texture2D texture)
        {
#if UNITY_2021_2_OR_NEWER
            if (material.HasTexture(propertyName))
            {
                material.SetTexture(propertyName, texture);

                return true;
            }
            else if (material.HasProperty(propertyName))
            {
                Debug.LogError($"{material.name} {propertyName} property type is not Texture.");

                return default;
            }
#else
            if (material.HasProperty(propertyName))
            {
                material.SetTexture(propertyName, texture);

                return true;
            }
#endif
            else
            {
                Debug.LogWarning($"{material.name} don't have {propertyName} property.");

                return false;
            }
        }

        /// <summary>
        /// Sets the Vector value.
        /// </summary>
        /// <param name="material">A lilToon material.</param>
        /// <param name="propertyNameId">A material property name ID.</param>
        /// <param name="vector"></param>
        /// <returns>Whether it could be set.</returns>
        public static bool SetSafeVector(this Material material, int propertyNameId, Vector4 vector)
        {
#if UNITY_2021_2_OR_NEWER
            if (material.HasVector(propertyNameId))
            {
                material.SetVector(propertyNameId, vector);

                return true;
            }
            else if (material.HasProperty(propertyNameId))
            {
                Debug.LogError($"{material.name} {propertyNameId} property type is not Vector.");

                return default;
            }
#else
            if (material.HasProperty(propertyNameId))
            {
                material.SetVector(propertyNameId, vector);

                return true;
            }
#endif
            else
            {
                Debug.LogWarning($"{material.name} don't have {propertyNameId} property.");

                return false;
            }
        }

        /// <summary>
        /// Sets the Vector value.
        /// </summary>
        /// <param name="material">A lilToon material.</param>
        /// <param name="propertyName">A material property name.</param>
        /// <param name="vector"></param>
        /// <returns>Whether it could be set.</returns>
        public static bool SetSafeVector(this Material material, string propertyName, Vector4 vector)
        {
#if UNITY_2021_2_OR_NEWER
            if (material.HasVector(propertyName))
            {
                material.SetVector(propertyName, vector);

                return true;
            }
            else if (material.HasProperty(propertyName))
            {
                Debug.LogError($"{material.name} {propertyName} property type is not Vector.");

                return default;
            }
#else
            if (material.HasProperty(propertyName))
            {
                material.SetVector(propertyName, vector);

                return true;
            }
#endif
            else
            {
                Debug.LogWarning($"{material.name} don't have {propertyName} property.");

                return false;
            }
        }

        /// <summary>
        /// Sets the keyword.
        /// </summary>
        /// <param name="material">A material.</param>
        /// <param name="keyword">A material keyword.</param>
        /// <param name="enable"></param>
        public static void SetKeyword(this Material material, string keyword, bool enable)
        {
            if (enable)
            {
                material.EnableKeyword(keyword);
            }
            else
            {
                material.DisableKeyword(keyword);
            }
        }

        #endregion
    }
}
