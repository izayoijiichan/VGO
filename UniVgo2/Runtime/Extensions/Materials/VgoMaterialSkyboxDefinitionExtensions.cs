﻿// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : VgoMaterialSkyboxDefinitionExtensions
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using NewtonVgo;
    using System.Collections.Generic;
    using UniSkyboxShader;
    using UnityEngine;

    /// <summary>
    /// Vgo Material Skybox Definition Extensions
    /// </summary>
    public static class VgoMaterialSkyboxDefinitionExtensions
    {
        /// <summary>
        /// Convert vgo material to skybox 6 sided definition.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="allTextureList">List of all texture.</param>
        /// <returns>A skybox 6 sided definition.</returns>
        public static Skybox6SidedDefinition ToSkybox6SidedDefinition(this VgoMaterial vgoMaterial, in List<Texture?> allTextureList)
        {
            if (vgoMaterial.shaderName != ShaderName.Skybox_6_Sided)
            {
                ThrowHelper.ThrowException($"vgoMaterial.shaderName: {vgoMaterial.shaderName}");
            }

            return new Skybox6SidedDefinition()
            {
                Tint = vgoMaterial.GetColorOrDefault(Property.Tint, Color.white).gamma,
                Exposure = vgoMaterial.GetSafeFloat(Property.Exposure, PropertyRange.Exposure),
                Rotation = vgoMaterial.GetSafeInt(Property.Rotation, PropertyRange.Rotation),
                FrontTex = allTextureList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.FrontTex)) as Texture2D,
                BackTex = allTextureList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.BackTex)) as Texture2D,
                LeftTex = allTextureList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.LeftTex)) as Texture2D,
                RightTex = allTextureList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.RightTex)) as Texture2D,
                UpTex = allTextureList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.UpTex)) as Texture2D,
                DownTex = allTextureList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.DownTex)) as Texture2D,
            };
        }

        /// <summary>
        /// Convert vgo material to skybox cubemap definition.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <returns>A skybox cubemap definition.</returns>
        public static SkyboxCubemapDefinition ToSkyboxCubemapDefinition(this VgoMaterial vgoMaterial)
        {
            if (vgoMaterial.shaderName != ShaderName.Skybox_Cubemap)
            {
                ThrowHelper.ThrowException($"vgoMaterial.shaderName: {vgoMaterial.shaderName}");
            }

            return new SkyboxCubemapDefinition
            {
                Tint = vgoMaterial.GetColorOrDefault(Property.Tint, Color.white).gamma,
                Exposure = vgoMaterial.GetSafeFloat(Property.Exposure, PropertyRange.Exposure),
                Rotation = vgoMaterial.GetSafeInt(Property.Rotation, PropertyRange.Rotation),
                //Tex = AllTextureCubeMapList.GetValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.Tex)),
            };
        }

        /// <summary>
        /// Convert vgo material to skybox panoramic definition.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="allTextureList">List of all texture.</param>
        /// <returns>A skybox panoramic definition.</returns>
        public static SkyboxPanoramicDefinition ToSkyboxPanoramicDefinition(this VgoMaterial vgoMaterial, in List<Texture?> allTextureList)
        {
            if (vgoMaterial.shaderName != ShaderName.Skybox_Panoramic)
            {
                ThrowHelper.ThrowException($"vgoMaterial.shaderName: {vgoMaterial.shaderName}");
            }

            return new SkyboxPanoramicDefinition
            {
                Tint = vgoMaterial.GetColorOrDefault(Property.Tint, Color.white).gamma,
                Exposure = vgoMaterial.GetSafeFloat(Property.Exposure, PropertyRange.Exposure),
                Rotation = vgoMaterial.GetSafeInt(Property.Rotation, PropertyRange.Rotation),
                MainTex = allTextureList.GetNullableValueOrDefault(vgoMaterial.GetTextureIndexOrDefault(Property.MainTex)) as Texture2D,
                Mapping = (Mapping)vgoMaterial.GetIntOrDefault(Property.Mapping),
                ImageType = (UniSkyboxShader.ImageType)vgoMaterial.GetIntOrDefault(Property.ImageType),
                MirrorOnBack = vgoMaterial.GetIntOrDefault(Property.MirrorOnBack) == 1,
                Layout = (Layout)vgoMaterial.GetIntOrDefault(Property.Layout),
            };
        }

        /// <summary>
        /// Convert vgo material to skybox procedural definition.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <returns>A skybox procedural definition.</returns>
        public static SkyboxProceduralDefinition ToSkyboxProceduralDefinition(this VgoMaterial vgoMaterial)
        {
            if (vgoMaterial.shaderName != ShaderName.Skybox_Procedural)
            {
                ThrowHelper.ThrowException($"vgoMaterial.shaderName: {vgoMaterial.shaderName}");
            }

            return new SkyboxProceduralDefinition
            {
                SunDisk = (SunDisk)vgoMaterial.GetIntOrDefault(Property.SunDisk),
                SunSize = vgoMaterial.GetSafeFloat(Property.SunSize, PropertyRange.SunSize),
                SunSizeConvergence = vgoMaterial.GetSafeInt(Property.SunSizeConvergence, PropertyRange.SunSizeConvergence),
                AtmosphereThickness = vgoMaterial.GetSafeFloat(Property.AtmosphereThickness, PropertyRange.AtmosphereThickness),
                SkyTint = vgoMaterial.GetColorOrDefault(Property.SkyTint, Color.white).gamma,
                GroundColor = vgoMaterial.GetColorOrDefault(Property.GroundColor, Color.white).gamma,
                Exposure = vgoMaterial.GetSafeFloat(Property.Exposure, PropertyRange.Exposure.minValue, PropertyRange.Exposure.maxValue, 1.3f),
            };
        }
    }
}
