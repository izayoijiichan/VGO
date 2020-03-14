// ----------------------------------------------------------------------
// @Namespace : UniSkybox
// @Class     : Utils
// ----------------------------------------------------------------------
namespace UniSkybox
{
    using System;
    using UnityEngine;

    public static partial class Utils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="material"></param>
        /// <param name="parameters"></param>
        public static void SetParametersToMaterial<T>(Material material, T parameters)
        {
            Type type = typeof(T);

            if (type == Skybox6SidedDefinitionType)
            {
                SetSkybox6SidedParametersToMaterial(material, parameters as Skybox6SidedDefinition);
            }
            else if (type == SkyboxCubemapDefinitionType)
            {
                SetSkyboxCubemapParametersToMaterial(material, parameters as SkyboxCubemapDefinition);
            }
            else if (type == SkyboxPanoramicDefinitionType)
            {
                SetSkyboxPanoramicParametersToMaterial(material, parameters as SkyboxPanoramicDefinition);
            }
            else if (type == SkyboxProceduralDefinitionType)
            {
                SetSkyboxProceduralParametersToMaterial(material, parameters as SkyboxProceduralDefinition);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="parameters"></param>
        private static void SetSkybox6SidedParametersToMaterial(Material material, Skybox6SidedDefinition parameters)
        {
            SetColor(material, PropTint, parameters.Tint);
            SetFloat(material, PropExposure, parameters.Exposure);
            SetInt(material, PropRotation, parameters.Rotation);
            SetTexture(material, PropFrontTex, parameters.FrontTex);
            SetTexture(material, PropBackTex, parameters.BackTex);
            SetTexture(material, PropLeftTex, parameters.LeftTex);
            SetTexture(material, PropRightTex, parameters.RightTex);
            SetTexture(material, PropUpTex, parameters.UpTex);
            SetTexture(material, PropDownTex, parameters.DownTex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="parameters"></param>
        private static void SetSkyboxCubemapParametersToMaterial(Material material, SkyboxCubemapDefinition parameters)
        {
            SetColor(material, PropTint, parameters.Tint);
            SetFloat(material, PropExposure, parameters.Exposure);
            SetInt(material, PropRotation, parameters.Rotation);
            SetCubemap(material, PropTex, parameters.Tex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="parameters"></param>
        private static void SetSkyboxPanoramicParametersToMaterial(Material material, SkyboxPanoramicDefinition parameters)
        {
            SetColor(material, PropTint, parameters.Tint);
            SetFloat(material, PropExposure, parameters.Exposure);
            SetInt(material, PropRotation, parameters.Rotation);
            SetTexture(material, PropMainTex, parameters.MainTex);
            SetInt(material, PropMapping, (int)parameters.Mapping);
            SetKeyword(material, KeyMapping6FramesLayout, parameters.Mapping == Mapping.SixFramesLayout);
            SetInt(material, PropImageType, (int)parameters.ImageType);
            SetBool(material, PropMirrorOnBack, parameters.MirrorOnBack);
            SetInt(material, PropLayout, (int)parameters.Layout);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="parameters"></param>
        private static void SetSkyboxProceduralParametersToMaterial(Material material, SkyboxProceduralDefinition parameters)
        {
            SetSunDisk(material, parameters.SunDisk);
            SetFloat(material, PropSunSize, parameters.SunSize);
            SetInt(material, PropSunSizeConvergence, parameters.SunSizeConvergence);
            SetFloat(material, PropAtmosphereThickness, parameters.AtmosphereThickness);
            SetColor(material, PropSkyTint, parameters.SkyTint);
            SetColor(material, PropGroundColor, parameters.GroundColor);
            SetFloat(material, PropExposure, parameters.Exposure);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="propertyName"></param>
        /// <param name="val"></param>
        private static void SetBool(Material material, string propertyName, bool val)
        {
            if (material.HasProperty(propertyName))
            {
                material.SetInt(propertyName, (val == true) ? 1 : 0);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="propertyName"></param>
        /// <param name="val"></param>
        private static void SetInt(Material material, string propertyName, int val)
        {
            if (material.HasProperty(propertyName))
            {
                material.SetInt(propertyName, val);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="propertyName"></param>
        /// <param name="val"></param>
        private static void SetFloat(Material material, string propertyName, float val)
        {
            if (material.HasProperty(propertyName))
            {
                material.SetFloat(propertyName, val);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="propertyName"></param>
        /// <param name="color"></param>
        private static void SetColor(Material material, string propertyName, Color color)
        {
            if (material.HasProperty(propertyName))
            {
                material.SetColor(propertyName, color);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="propertyName"></param>
        /// <param name="texture"></param>
        private static void SetTexture(Material material, string propertyName, Texture2D texture)
        {
            if (material.HasProperty(propertyName))
            {
                material.SetTexture(propertyName, texture);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="propertyName"></param>
        /// <param name="cubemap"></param>
        private static void SetCubemap(Material material, string propertyName, Cubemap cubemap)
        {
            if (material.HasProperty(propertyName))
            {
                material.SetTexture(propertyName, cubemap);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="keyword"></param>
        /// <param name="required"></param>
        private static void SetKeyword(Material material, string keyword, bool required)
        {
            if (required)
            {
                material.EnableKeyword(keyword);
            }
            else
            {
                material.DisableKeyword(keyword);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="sunDisk"></param>
        public static void SetSunDisk(Material material, SunDisk sunDisk)
        {
            SetInt(material, PropSunDisk, (int)sunDisk);

            switch (sunDisk)
            {
                case SunDisk.None:
                    SetKeyword(material, KeySundiskNone, true);
                    SetKeyword(material, KeySundiskSimple, false);
                    SetKeyword(material, KeySundiskHighQuality, false);
                    break;
                case SunDisk.Simple:
                    SetKeyword(material, KeySundiskNone, false);
                    SetKeyword(material, KeySundiskSimple, true);
                    SetKeyword(material, KeySundiskHighQuality, false);
                    break;
                case SunDisk.HighQuality:
                    SetKeyword(material, KeySundiskNone, false);
                    SetKeyword(material, KeySundiskSimple, false);
                    SetKeyword(material, KeySundiskHighQuality, true);
                    break;
                default:
                    SetKeyword(material, KeySundiskNone, true);
                    SetKeyword(material, KeySundiskSimple, false);
                    SetKeyword(material, KeySundiskHighQuality, false);
                    break;
            }
        }
    }
}