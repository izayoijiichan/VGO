// ----------------------------------------------------------------------
// @Namespace : UniGLTFforUniVgo
// @Class     : VgoContractResolver
// ----------------------------------------------------------------------
namespace UniGLTFforUniVgo
{
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;
    using System;
    using UnityEngine;
    using UnityEngine.Rendering;

    /// <summary>
    /// VGO Contract Resolver
    /// </summary>
    public class VgoContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// Determines which contract type is created for the given type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>A Newtonsoft.Json.Serialization.JsonContract for the given type.</returns>
        protected override JsonContract CreateContract(Type objectType)
        {
            JsonContract contract = base.CreateContract(objectType);

            if ((objectType == typeof(ColliderType)) ||
                (objectType == typeof(CollisionDetectionMode)) ||
                (objectType == typeof(LightmapBakeType)) ||
                (objectType == typeof(LightRenderMode)) ||
                (objectType == typeof(LightShadowResolution)) ||
                (objectType == typeof(LightShadows)) ||
                (objectType == typeof(LightShape)) ||
                (objectType == typeof(LightType)) ||
                (objectType == typeof(PhysicMaterialCombine)) ||
                (objectType == typeof(RigidbodyInterpolation)))
            {
                contract.Converter = new StringEnumConverter(new DefaultNamingStrategy(), allowIntegerValues: false);
            }

            if ((objectType == typeof(MToonCullMode)) ||
                (objectType == typeof(MToonOutlineColorMode)) ||
                (objectType == typeof(MToonOutlineWidthMode)) ||
                (objectType == typeof(MToonRenderMode)))
            {
                contract.Converter = new StringEnumConverter(new CamelCaseNamingStrategy(), allowIntegerValues: false);
            }

            return contract;
        }
    }
}