// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : VgoMaterialExtensions
// ----------------------------------------------------------------------
namespace UniVgo2
{
    using NewtonVgo;
    using UnityEngine;

    /// <summary>
    /// VgoMaterial Extensions
    /// </summary>
    public static class VgoMaterialExtensions
    {
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
    }
}
