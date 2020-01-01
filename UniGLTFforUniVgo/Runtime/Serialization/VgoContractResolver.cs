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
                (objectType == typeof(PhysicMaterialCombine)) ||
                (objectType == typeof(RigidbodyInterpolation)))
            {
                contract.Converter = new StringEnumConverter(new DefaultNamingStrategy(), allowIntegerValues: false);
            }

            return contract;
        }
    }
}