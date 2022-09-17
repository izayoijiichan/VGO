// ----------------------------------------------------------------------
// @Namespace : NewtonVgo.Serialization
// @Class     : VgoContractResolver
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo.Serialization
{
    using Newtonsoft.Json.Serialization;
    using NewtonVgo.Serialization.JsonConverters;
    using System;

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

            if (objectType == typeof(VgoMeshPrimitiveAttributes))
            {
                contract.Converter = new VgoMeshPrimitiveAttributesJsonConverter();
            }

            if (objectType == typeof(VgoExtensions))
            {
                contract.Converter = new VgoExtensionsJsonConverter();
            }

            return contract;
        }
    }
}