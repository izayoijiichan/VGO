// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : VgoMaterialExtensions
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using NewtonVgo;
    using System;
    using UniShader.Shared;
    using UnityEngine;

    /// <summary>
    /// VgoMaterial Extensions
    /// </summary>
    public static class VgoMaterialExtensions
    {
        /// <summary>
        /// Gets enum value.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="self">A vgo material.</param>
        /// <param name="propertyName">A material property name.</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TEnum GetEnumOrDefault<TEnum>(this VgoMaterial self, string propertyName, TEnum? defaultValue = null) where TEnum : struct
        {
            int? propertyValue = null;

            if (self.intProperties != null)
            {
                if (self.intProperties.ContainsKey(propertyName))
                {
                    propertyValue = self.intProperties[propertyName];
                }
            }

            if (propertyValue == null)
            {
                if (self.floatProperties != null)
                {
                    if (self.floatProperties.ContainsKey(propertyName))
                    {
                        float floatValue = self.floatProperties[propertyName];

                        if (int.TryParse(floatValue.ToString(), out int intValue))
                        {
                            propertyValue = intValue;
                        }
                    }
                }
            }

            if (propertyValue == null)
            {
                return default;
            }

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
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="propertyName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetIntOrDefault(this VgoMaterial self, string propertyName, int defaultValue = 0)
        {
            if (self.intProperties == null)
            {
                return defaultValue;
            }

            if (self.intProperties.ContainsKey(propertyName) == false)
            {
                return defaultValue;
            }

            return self.intProperties[propertyName];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="propertyName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static float GetFloatOrDefault(this VgoMaterial self, string propertyName, float defaultValue = 0.0f)
        {
            if (self.floatProperties == null)
            {
                return defaultValue;
            }

            if (self.floatProperties.ContainsKey(propertyName) == false)
            {
                return defaultValue;
            }

            return self.floatProperties[propertyName];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetSafeInt(this VgoMaterial self, string propertyName, int min, int max, int defaultValue = 0)
        {
            if (self.intProperties == null)
            {
                return defaultValue;
            }

            if (self.intProperties.ContainsKey(propertyName) == false)
            {
                return defaultValue;
            }

            int value = self.intProperties[propertyName];

            if (value < min)
            {
                return defaultValue;
            }

            if (value > max)
            {
                return defaultValue;
            }

            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public static int GetSafeInt(this VgoMaterial self, string propertyName, IntRangeDefault range)
        {
            return GetSafeInt(self, propertyName, range.minValue, range.maxValue, range.defaultValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="propertyName"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static float GetSafeFloat(this VgoMaterial self, string propertyName, float min, float max, float defaultValue = 0.0f)
        {
            if (self.floatProperties == null)
            {
                return defaultValue;
            }

            if (self.floatProperties.ContainsKey(propertyName) == false)
            {
                return defaultValue;
            }

            float value = self.floatProperties[propertyName];

            if (value < min)
            {
                return defaultValue;
            }

            if (value > max)
            {
                return defaultValue;
            }

            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="propertyName"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public static float GetSafeFloat(this VgoMaterial self, string propertyName, FloatRangeDefault range)
        {
            return GetSafeFloat(self, propertyName, range.minValue, range.maxValue, range.defaultValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="propertyName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Color GetColorOrDefault(this VgoMaterial self, string propertyName, Color defaultValue)
        {
            if (self.colorProperties == null)
            {
                return defaultValue;
            }

            if (self.colorProperties.ContainsKey(propertyName) == false)
            {
                return defaultValue;
            }

            float[] value = self.colorProperties[propertyName];

            if (value == null)
            {
                return defaultValue;
            }

            if (value.Length == 3)
            {
                return new Color(value[0], value[1], value[2]);
            }

            if (value.Length == 4)
            {
                return new Color(value[0], value[1], value[2], value[3]);
            }

            return defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="propertyName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Vector2 GetVector2OrDefault(this VgoMaterial self, string propertyName, Vector2 defaultValue)
        {
            if (self.vectorProperties == null)
            {
                return defaultValue;
            }

            if (self.vectorProperties.ContainsKey(propertyName) == false)
            {
                return defaultValue;
            }

            float[] value = self.vectorProperties[propertyName];

            if (value == null)
            {
                return defaultValue;
            }

            if (value.Length == 2)
            {
                return new Vector2(value[0], value[1]);
            }

            return defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="propertyName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Vector3 GetVector3OrDefault(this VgoMaterial self, string propertyName, Vector3 defaultValue)
        {
            if (self.vectorProperties == null)
            {
                return defaultValue;
            }

            if (self.vectorProperties.ContainsKey(propertyName) == false)
            {
                return defaultValue;
            }

            float[] value = self.vectorProperties[propertyName];

            if (value == null)
            {
                return defaultValue;
            }

            if (value.Length == 3)
            {
                return new Vector3(value[0], value[1], value[2]);
            }

            return defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="propertyName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Vector4 GetVector4OrDefault(this VgoMaterial self, string propertyName, Vector4 defaultValue)
        {
            if (self.vectorProperties == null)
            {
                return defaultValue;
            }

            if (self.vectorProperties.ContainsKey(propertyName) == false)
            {
                return defaultValue;
            }

            float[] value = self.vectorProperties[propertyName];

            if (value == null)
            {
                return defaultValue;
            }

            if (value.Length == 4)
            {
                return new Vector4(value[0], value[1], value[2], value[3]);
            }

            return defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static int GetTextureIndexOrDefault(this VgoMaterial self, string propertyName)
        {
            if (self.textureIndexProperties == null)
            {
                return -1;
            }

            if (self.textureIndexProperties.ContainsKey(propertyName) == false)
            {
                return -1;
            }

            return self.textureIndexProperties[propertyName];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="propertyName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Vector2 GetTextureOffsetOrDefault(this VgoMaterial self, string propertyName, Vector2 defaultValue = default)
        {
            Vector2? offsetVector = self.GetTextureOffsetOrNull(propertyName);

            if (offsetVector.HasValue)
            {
                return offsetVector.Value;
            }

            return defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static Vector2? GetTextureOffsetOrNull(this VgoMaterial self, string propertyName)
        {
            if (self.textureOffsetProperties == null)
            {
                return null;
            }

            if (self.textureOffsetProperties.ContainsKey(propertyName) == false)
            {
                return null;
            }

            float[]? offsetArray = self.textureOffsetProperties[propertyName];

            if (offsetArray == null)
            {
                return null;
            }

            if (offsetArray.Length != 2)
            {
                return null;
            }

            Vector2 offsetVector = ArrayConverter.ToVector2(offsetArray);

            return offsetVector;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="propertyName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Vector2 GetTextureScaleOrDefault(this VgoMaterial self, string propertyName, Vector2 defaultValue = default)
        {
            Vector2? scaleVector = self.GetTextureScaleOrNull(propertyName);

            if (scaleVector.HasValue)
            {
                return scaleVector.Value;
            }

            return defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static Vector2? GetTextureScaleOrNull(this VgoMaterial self, string propertyName)
        {
            if (self.textureScaleProperties == null)
            {
                return null;
            }

            if (self.textureScaleProperties.ContainsKey(propertyName) == false)
            {
                return null;
            }

            float[]? scaleArray = self.textureScaleProperties[propertyName];

            if (scaleArray == null)
            {
                return null;
            }

            if (scaleArray.Length != 2)
            {
                return null;
            }

            Vector2 scaleVector = ArrayConverter.ToVector2(scaleArray);

            return scaleVector;
        }
    }
}
