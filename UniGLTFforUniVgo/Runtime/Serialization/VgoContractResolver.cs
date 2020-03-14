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
                (objectType == typeof(GradientMode)) ||
                (objectType == typeof(MotionVectorGenerationMode)) ||
                (objectType == typeof(PhysicMaterialCombine)) ||
                (objectType == typeof(RigidbodyInterpolation)) ||
                (objectType == typeof(SpriteMaskInteraction)) ||
                (objectType == typeof(WrapMode)) ||
                (objectType == typeof(WeightedMode))
            )
            {
                contract.Converter = new StringEnumConverter(new DefaultNamingStrategy(), allowIntegerValues: false);
            }

            // Rendering
            if ((objectType == typeof(CullMode)) || 
                (objectType == typeof(LightmapBakeType)) ||
                (objectType == typeof(LightProbeUsage)) ||
                (objectType == typeof(LightRenderMode)) ||
                (objectType == typeof(LightShadowResolution)) ||
                (objectType == typeof(LightShadows)) ||
                (objectType == typeof(LightShape)) ||
                (objectType == typeof(LightType)) ||
                (objectType == typeof(ReflectionProbeUsage)) ||
                (objectType == typeof(ShadowCastingMode))
            )
            {
                contract.Converter = new StringEnumConverter(new DefaultNamingStrategy(), allowIntegerValues: false);
            }

            // ParticleSystem
            if ((objectType == typeof(ParticleSystemAnimationMode)) ||
                (objectType == typeof(ParticleSystemAnimationRowMode)) ||
                (objectType == typeof(ParticleSystemAnimationTimeMode)) ||
                (objectType == typeof(ParticleSystemAnimationType)) ||
                (objectType == typeof(ParticleSystemCollisionMode)) ||
                (objectType == typeof(ParticleSystemCollisionQuality)) ||
                (objectType == typeof(ParticleSystemCollisionType)) ||
                (objectType == typeof(ParticleSystemCullingMode)) ||
                (objectType == typeof(ParticleSystemCurveMode)) ||
                (objectType == typeof(ParticleSystemEmitterVelocityMode)) ||
                (objectType == typeof(ParticleSystemGameObjectFilter)) ||
                (objectType == typeof(ParticleSystemGradientMode)) ||
                (objectType == typeof(ParticleSystemInheritVelocityMode)) ||
                (objectType == typeof(ParticleSystemMeshShapeType)) ||
                (objectType == typeof(ParticleSystemNoiseQuality)) ||
                (objectType == typeof(ParticleSystemOverlapAction)) ||
                (objectType == typeof(ParticleSystemRenderMode)) ||
                (objectType == typeof(ParticleSystemRenderSpace)) ||
                (objectType == typeof(ParticleSystemRingBufferMode)) ||
                (objectType == typeof(ParticleSystemScalingMode)) ||
                (objectType == typeof(ParticleSystemShapeMultiModeValue)) ||
                (objectType == typeof(ParticleSystemShapeTextureChannel)) ||
                (objectType == typeof(ParticleSystemShapeType)) ||
                (objectType == typeof(ParticleSystemSimulationSpace)) ||
                (objectType == typeof(ParticleSystemSortMode) ||
                (objectType == typeof(ParticleSystemStopAction)) ||
                (objectType == typeof(ParticleSystemTrailMode)) ||
                (objectType == typeof(ParticleSystemTrailTextureMode))
            )
            )
            {
                contract.Converter = new StringEnumConverter(new DefaultNamingStrategy(), allowIntegerValues: false);
            }

            // Shader Properties (Particle)
            if ((objectType == typeof(ParticleBlendMode)) ||
                (objectType == typeof(ParticleColorMode)) ||
                (objectType == typeof(ParticleFlipBookMode))
            )
            {
                contract.Converter = new StringEnumConverter(new DefaultNamingStrategy(), allowIntegerValues: false);
            }

            // Shader Properties (Skybox)
            if ((objectType == typeof(SkyboxImageType)) ||
                (objectType == typeof(SkyboxLayout)) ||
                (objectType == typeof(SkyboxMapping)) ||
                (objectType == typeof(SkyboxSunDisk))
            )
            {
                contract.Converter = new StringEnumConverter(new DefaultNamingStrategy(), allowIntegerValues: false);
            }

            // Shader Properties (MToon)
            if ((objectType == typeof(MToonCullMode)) ||
                (objectType == typeof(MToonOutlineColorMode)) ||
                (objectType == typeof(MToonOutlineWidthMode)) ||
                (objectType == typeof(MToonRenderMode))
            )
            {
                contract.Converter = new StringEnumConverter(new CamelCaseNamingStrategy(), allowIntegerValues: false);
            }

            return contract;
        }
    }
}