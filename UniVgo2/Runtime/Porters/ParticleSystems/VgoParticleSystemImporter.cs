// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : VgoParticleSystemImporter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using NewtonVgo.Schema.ParticleSystems;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UniVgo2.Converters;
    using static UnityEngine.ParticleSystem;

    /// <summary>
    /// VGO Particle System Importer
    /// </summary>
    public class VgoParticleSystemImporter : IVgoParticleSystemImporter
    {
        #region Public Methods

        /// <summary>
        /// Adds a ParticleSystem component to the game object.
        /// </summary>
        /// <param name="go"></param>
        /// <param name="vgoParticleSystem"></param>
        /// <param name="geometryCoordinate"></param>
        /// <param name="materialList"></param>
        /// <param name="textureList"></param>
        /// <param name="forceSetRuntimeDuration"></param>
        /// <returns>Returns ParticleSystem component.</returns>
        public virtual ParticleSystem AddComponent(
            GameObject go,
            in VgoParticleSystem vgoParticleSystem,
            in VgoGeometryCoordinate geometryCoordinate,
            in IList<Material?>? materialList,
            in IList<Texture?>? textureList,
            in bool forceSetRuntimeDuration)
        {
            if (go.TryGetComponentEx<ParticleSystem>(out var particleSystem) == false)
            {
                particleSystem = go.AddComponent<ParticleSystem>();
            }

            if (vgoParticleSystem == null)
            {
                return particleSystem;
            }

            if (vgoParticleSystem.main != null)
            {
                SetModuleValue(particleSystem, vgoParticleSystem.main, geometryCoordinate, forceSetRuntimeDuration);
            }
            if (vgoParticleSystem.emission != null)
            {
                SetModuleValue(particleSystem, vgoParticleSystem.emission);
            }
            if (vgoParticleSystem.shape != null)
            {
                SetModuleValue(particleSystem, vgoParticleSystem.shape, textureList, geometryCoordinate);
            }
            if (vgoParticleSystem.velocityOverLifetime != null)
            {
                SetModuleValue(particleSystem, vgoParticleSystem.velocityOverLifetime);
            }
            if (vgoParticleSystem.limitVelocityOverLifetime != null)
            {
                SetModuleValue(particleSystem, vgoParticleSystem.limitVelocityOverLifetime);
            }
            if (vgoParticleSystem.inheritVelocity != null)
            {
                SetModuleValue(particleSystem, vgoParticleSystem.inheritVelocity);
            }
            if (vgoParticleSystem.forceOverLifetime != null)
            {
                SetModuleValue(particleSystem, vgoParticleSystem.forceOverLifetime);
            }
            if (vgoParticleSystem.colorOverLifetime != null)
            {
                SetModuleValue(particleSystem, vgoParticleSystem.colorOverLifetime);
            }
            if (vgoParticleSystem.colorBySpeed != null)
            {
                SetModuleValue(particleSystem, vgoParticleSystem.colorBySpeed);
            }
            if (vgoParticleSystem.sizeOverLifetime != null)
            {
                SetModuleValue(particleSystem, vgoParticleSystem.sizeOverLifetime);
            }
            if (vgoParticleSystem.sizeBySpeed != null)
            {
                SetModuleValue(particleSystem, vgoParticleSystem.sizeBySpeed);
            }
            if (vgoParticleSystem.rotationOverLifetime != null)
            {
                SetModuleValue(particleSystem, vgoParticleSystem.rotationOverLifetime);
            }
            if (vgoParticleSystem.rotationBySpeed != null)
            {
                SetModuleValue(particleSystem, vgoParticleSystem.rotationBySpeed);
            }
            if (vgoParticleSystem.externalForces != null)
            {
                SetModuleValue(particleSystem, vgoParticleSystem.externalForces);
            }
            if (vgoParticleSystem.noise != null)
            {
                SetModuleValue(particleSystem, vgoParticleSystem.noise);
            }
            //SetModuleValue(particleSystem, vgoParticleSystem.Collision);
            //SetModuleValue(particleSystem, vgoParticleSystem.Trigger);
            //SetModuleValue(particleSystem, vgoParticleSystem.SubEmitters);
            if (vgoParticleSystem.textureSheetAnimation != null)
            {
                SetModuleValue(particleSystem, vgoParticleSystem.textureSheetAnimation);
            }
            if (vgoParticleSystem.lights != null)
            {
                SetModuleValue(particleSystem, vgoParticleSystem.lights);
            }
            if (vgoParticleSystem.trails != null)
            {
                SetModuleValue(particleSystem, vgoParticleSystem.trails);
            }
            if (vgoParticleSystem.customData != null)
            {
                SetModuleValue(particleSystem, vgoParticleSystem.customData);
            }

            if (vgoParticleSystem.renderer != null)
            {
                if (go.TryGetComponentEx<ParticleSystemRenderer>(out var particleSystemRenderer) == false)
                {
                    particleSystemRenderer = go.AddComponent<ParticleSystemRenderer>();
                }

                if (particleSystemRenderer != null)
                {
                    SetComponentValue(particleSystemRenderer, vgoParticleSystem.renderer, geometryCoordinate, materialList);
                }
            }

            return particleSystem;
        }

        #endregion

        #region Protected Methods (Module)

        /// <summary>
        /// Set ParticleSystem main field value.
        /// </summary>
        /// <param name="particleSystem"></param>
        /// <param name="vgoModule"></param>
        /// <param name="geometryCoordinate"></param>
        /// <param name="forceSetRuntimeDuration"></param>
        protected virtual void SetModuleValue(ParticleSystem particleSystem, in VGO_PS_MainModule vgoModule, in VgoGeometryCoordinate geometryCoordinate, in bool forceSetRuntimeDuration)
        {
            if (vgoModule == null)
            {
                return;
            }

            MainModule module = particleSystem.main;

            if (Application.isPlaying)
            {
                if (module.duration != vgoModule.duration)
                {
                    if (forceSetRuntimeDuration)
                    {
                        try
                        {
                            module.duration = vgoModule.duration;
                        }
                        catch (Exception ex)
                        {
                            Debug.LogException(ex);
                        }
                    }
                }
            }
            else
            {
                module.duration = vgoModule.duration;
            }

            module.loop = vgoModule.loop;
            module.prewarm = vgoModule.prewarm;
            module.startDelay = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.startDelay);
            module.startDelayMultiplier = vgoModule.startDelayMultiplier;
            module.startLifetime = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.startLifetime);
            module.startLifetimeMultiplier = vgoModule.startLifetimeMultiplier;
            module.startSpeed = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.startSpeed);
            module.startSpeedMultiplier = vgoModule.startSpeedMultiplier;

            module.startSize3D = vgoModule.startSize3D;

            if (vgoModule.startSize3D)
            {
                module.startSizeX = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.startSizeX);
                module.startSizeY = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.startSizeY);
                module.startSizeZ = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.startSizeZ);
                module.startSizeXMultiplier = vgoModule.startSizeXMultiplier;
                module.startSizeYMultiplier = vgoModule.startSizeYMultiplier;
                module.startSizeZMultiplier = vgoModule.startSizeZMultiplier;
            }
            else
            {
                module.startSize = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.startSize);
                module.startSizeMultiplier = vgoModule.startSizeMultiplier;
            }

            module.startRotation3D = vgoModule.startRotation3D;

            if (vgoModule.startRotation3D)
            {
                module.startRotationX = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.startRotationX);
                module.startRotationY = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.startRotationY);
                module.startRotationZ = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.startRotationZ);
                module.startRotationXMultiplier = vgoModule.StartRotationXMultiplier;
                module.startRotationYMultiplier = vgoModule.StartRotationYMultiplier;
                module.startRotationZMultiplier = vgoModule.StartRotationZMultiplier;
            }
            else
            {
                module.startRotation = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.startRotation);
                module.startRotationMultiplier = vgoModule.StartRotationMultiplier;
            }

            module.flipRotation = vgoModule.flipRotation;
            module.startColor = VgoParticleSystemMinMaxGradientConverter.CreateMinMaxGradient(vgoModule.startColor);
            module.gravityModifier = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.gravityModifier);
            module.gravityModifierMultiplier = vgoModule.gravityModifierMultiplier;

            module.simulationSpace = (UnityEngine.ParticleSystemSimulationSpace)vgoModule.simulationSpace;
            module.simulationSpeed = vgoModule.simulationSpeed;

            if (vgoModule.customSimulationSpace != null)
            {
                VgoTransformConverter.SetComponentValue(module.customSimulationSpace, vgoModule.customSimulationSpace, geometryCoordinate);
            }

            module.useUnscaledTime = vgoModule.useUnscaledTime;
            module.scalingMode = (UnityEngine.ParticleSystemScalingMode)vgoModule.scalingMode;
            module.playOnAwake = vgoModule.playOnAwake;
            module.emitterVelocityMode = (UnityEngine.ParticleSystemEmitterVelocityMode)vgoModule.emitterVelocityMode;
            module.maxParticles = vgoModule.maxParticles;
            module.stopAction = (UnityEngine.ParticleSystemStopAction)vgoModule.stopAction;
            module.cullingMode = (UnityEngine.ParticleSystemCullingMode)vgoModule.cullingMode;
            module.ringBufferMode = (UnityEngine.ParticleSystemRingBufferMode)vgoModule.ringBufferMode;

            if (vgoModule.ringBufferMode == NewtonVgo.ParticleSystemRingBufferMode.LoopUntilReplaced)
            {
                module.ringBufferLoopRange = vgoModule.ringBufferLoopRange.GetValueOrDefault(System.Numerics.Vector2.Zero).ToUnityVector2();
            }
        }

        /// <summary>
        /// Set ParticleSystem emission field value.
        /// </summary>
        /// <param name="particleSystem"></param>
        /// <param name="vgoModule"></param>
        protected virtual void SetModuleValue(ParticleSystem particleSystem, in VGO_PS_EmissionModule vgoModule)
        {
            if (vgoModule == null)
            {
                return;
            }

            EmissionModule module = particleSystem.emission;

            module.enabled = vgoModule.enabled;
            module.rateOverTime = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.rateOverTime);
            module.rateOverTimeMultiplier = vgoModule.rateOverTimeMultiplier;
            module.rateOverDistance = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.rateOverDistance);
            module.rateOverDistanceMultiplier = vgoModule.rateOverDistanceMultiplier;
            //module.burstCount = vgoModule.BurstCount;

            if ((vgoModule.bursts != null) && vgoModule.bursts.Any())
            {
                module.burstCount = vgoModule.bursts.Length;

                for (int idx = 0; idx < vgoModule.bursts.Length; idx++)
                {
                    module.SetBurst(idx, VgoParticleSystemBurstConverter.CreateBurst(vgoModule.bursts[idx]));
                }
            }
        }

        /// <summary>
        /// Set ParticleSystem shape field value.
        /// </summary>
        /// <param name="particleSystem"></param>
        /// <param name="vgoModule"></param>
        /// <param name="textureList"></param>
        /// <param name="geometryCoordinate"></param>
        protected virtual void SetModuleValue(ParticleSystem particleSystem, in VGO_PS_ShapeModule vgoModule, in IList<Texture?>? textureList, in VgoGeometryCoordinate geometryCoordinate)
        {
            if (vgoModule == null)
            {
                return;
            }

            ShapeModule module = particleSystem.shape;

            module.enabled = vgoModule.enabled;
            module.shapeType = (UnityEngine.ParticleSystemShapeType)vgoModule.shapeType;
            module.angle = vgoModule.angle;
            module.radius = vgoModule.radius;
            module.donutRadius = vgoModule.donutRadius;
            module.radiusMode = (UnityEngine.ParticleSystemShapeMultiModeValue)vgoModule.radiusMode;
            module.radiusSpread = vgoModule.radiusSpread;
            module.radiusSpeed = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.radiusSpeed);
            module.radiusSpeedMultiplier = vgoModule.radiusSpeedMultiplier;
            module.radiusThickness = vgoModule.radiusThickness;
            module.boxThickness = vgoModule.boxThickness.GetValueOrDefault(System.Numerics.Vector3.Zero).ToUnityVector3();
            module.arc = vgoModule.arc;
            module.arcMode = (UnityEngine.ParticleSystemShapeMultiModeValue)vgoModule.arcMode;
            module.arcSpread = vgoModule.arcSpread;
            module.arcSpeed = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.arcSpeed);
            module.arcSpeedMultiplier = vgoModule.arcSpeedMultiplier;
            module.length = vgoModule.length;
            module.meshShapeType = (UnityEngine.ParticleSystemMeshShapeType)vgoModule.meshShapeType;
            module.meshSpawnMode = (UnityEngine.ParticleSystemShapeMultiModeValue)vgoModule.meshSpawnMode;
            module.meshSpawnSpread = vgoModule.meshSpawnSpread;
            module.meshSpawnSpeed = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.meshSpawnSpeed);
            module.meshSpawnSpeedMultiplier = vgoModule.meshSpawnSpeedMultiplier;
            //module.mesh;
            //module.meshRenderer;
            //module.skinnedMeshRenderer;
            module.useMeshMaterialIndex = vgoModule.useMeshMaterialIndex;
            module.meshMaterialIndex = vgoModule.meshMaterialIndex;
            module.useMeshColors = vgoModule.useMeshColors;
            //module.sprite;
            //module.spriteRenderer;
            module.normalOffset = vgoModule.normalOffset;
            if ((textureList != null) && (-1 < vgoModule.textureIndex) && (vgoModule.textureIndex < textureList.Count))
            {
                Texture? texture = textureList[vgoModule.textureIndex];

                if (texture is Texture2D texture2D)
                {
                    module.texture = texture2D;
                }
            }
            module.textureClipChannel = (UnityEngine.ParticleSystemShapeTextureChannel)vgoModule.textureClipChannel;
            module.textureClipThreshold = vgoModule.textureClipThreshold;
            module.textureColorAffectsParticles = vgoModule.textureColorAffectsParticles;
            module.textureAlphaAffectsParticles = vgoModule.textureAlphaAffectsParticles;
            module.textureBilinearFiltering = vgoModule.textureBilinearFiltering;
            module.textureUVChannel = vgoModule.textureUVChannel;
            module.position = vgoModule.position.ToUnityVector3(Vector3.zero, geometryCoordinate);
            module.rotation = vgoModule.rotation.ToUnityVector3(Vector3.zero, geometryCoordinate);
            module.scale = vgoModule.scale.ToUnityVector3(Vector3.one);
            module.alignToDirection = vgoModule.alignToDirection;
            module.randomPositionAmount = vgoModule.randomPositionAmount;
            module.sphericalDirectionAmount = vgoModule.sphericalDirectionAmount;
            module.randomDirectionAmount = vgoModule.randomDirectionAmount;
        }

        /// <summary>
        /// Set ParticleSystem velocityOverLifetime field value.
        /// </summary>
        /// <param name="particleSystem"></param>
        /// <param name="vgoModule"></param>
        protected virtual void SetModuleValue(ParticleSystem particleSystem, in VGO_PS_VelocityOverLifetimeModule vgoModule)
        {
            if (vgoModule == null)
            {
                return;
            }

            VelocityOverLifetimeModule module = particleSystem.velocityOverLifetime;

            module.enabled = vgoModule.enabled;

            module.xMultiplier = vgoModule.xMultiplier;
            module.yMultiplier = vgoModule.yMultiplier;
            module.zMultiplier = vgoModule.zMultiplier;
            module.x = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.x);
            module.y = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.y);
            module.z = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.z);

            module.space = (UnityEngine.ParticleSystemSimulationSpace)vgoModule.space;

            module.orbitalX = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.orbitalX);
            module.orbitalY = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.orbitalY);
            module.orbitalZ = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.orbitalZ);
            module.orbitalXMultiplier = vgoModule.orbitalXMultiplier;
            module.orbitalYMultiplier = vgoModule.orbitalYMultiplier;
            module.orbitalZMultiplier = vgoModule.orbitalZMultiplier;

            module.orbitalOffsetX = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.orbitalOffsetX);
            module.orbitalOffsetY = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.orbitalOffsetY);
            module.orbitalOffsetZ = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.orbitalOffsetZ);
            module.orbitalOffsetXMultiplier = vgoModule.orbitalOffsetXMultiplier;
            module.orbitalOffsetYMultiplier = vgoModule.orbitalOffsetYMultiplier;
            module.orbitalOffsetZMultiplier = vgoModule.orbitalOffsetZMultiplier;

            module.radial = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.radial);
            module.radialMultiplier = vgoModule.radialMultiplier;

            module.speedModifier = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.speedModifier);
            module.speedModifierMultiplier = vgoModule.speedModifierMultiplier;
        }

        /// <summary>
        /// Set ParticleSystem limitVelocityOverLifetime field value.
        /// </summary>
        /// <param name="particleSystem"></param>
        /// <param name="vgoModule"></param>
        protected virtual void SetModuleValue(ParticleSystem particleSystem, in VGO_PS_LimitVelocityOverLifetimeModule vgoModule)
        {
            if (vgoModule == null)
            {
                return;
            }

            LimitVelocityOverLifetimeModule module = particleSystem.limitVelocityOverLifetime;

            module.enabled = vgoModule.enabled;
            module.separateAxes = vgoModule.separateAxes;

            if (vgoModule.separateAxes)
            {
                module.limitX = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.limitX);
                module.limitY = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.limitY);
                module.limitZ = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.limitZ);
                module.limitXMultiplier = vgoModule.limitXMultiplier;
                module.limitYMultiplier = vgoModule.limitYMultiplier;
                module.limitZMultiplier = vgoModule.limitZMultiplier;
            }
            else
            {
                module.limit = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.limitX);
                module.limitMultiplier = vgoModule.limitXMultiplier;
            }

            module.space = (UnityEngine.ParticleSystemSimulationSpace)vgoModule.space;
            module.dampen = vgoModule.dampen;
            module.drag = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.drag);
            module.dragMultiplier = vgoModule.dragMultiplier;
            module.multiplyDragByParticleSize = vgoModule.multiplyDragByParticleSize;
            module.multiplyDragByParticleVelocity = vgoModule.multiplyDragByParticleVelocity;
        }

        /// <summary>
        /// Set ParticleSystem inheritVelocity field value.
        /// </summary>
        /// <param name="particleSystem"></param>
        /// <param name="vgoModule"></param>
        protected virtual void SetModuleValue(ParticleSystem particleSystem, in VGO_PS_InheritVelocityModule vgoModule)
        {
            if (vgoModule == null)
            {
                return;
            }

            InheritVelocityModule module = particleSystem.inheritVelocity;

            module.enabled = vgoModule.enabled;
            module.mode = (UnityEngine.ParticleSystemInheritVelocityMode)vgoModule.mode;
            module.curve = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.curve);
            module.curveMultiplier = vgoModule.curveMultiplier;
        }

        /// <summary>
        /// Set ParticleSystem forceOverLifetime field value.
        /// </summary>
        /// <param name="particleSystem"></param>
        /// <param name="vgoModule"></param>
        protected virtual void SetModuleValue(ParticleSystem particleSystem, in VGO_PS_ForceOverLifetimeModule vgoModule)
        {
            if (vgoModule == null)
            {
                return;
            }

            ForceOverLifetimeModule module = particleSystem.forceOverLifetime;

            module.enabled = vgoModule.enabled;
            module.x = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.x);
            module.y = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.y);
            module.z = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.z);
            module.xMultiplier = vgoModule.xMultiplier;
            module.yMultiplier = vgoModule.yMultiplier;
            module.zMultiplier = vgoModule.zMultiplier;
            module.space = (UnityEngine.ParticleSystemSimulationSpace)vgoModule.space;
            module.randomized = vgoModule.randomized;
        }

        /// <summary>
        /// Set ParticleSystem colorOverLifetime field value.
        /// </summary>
        /// <param name="particleSystem"></param>
        /// <param name="vgoModule"></param>
        protected virtual void SetModuleValue(ParticleSystem particleSystem, in VGO_PS_ColorOverLifetimeModule vgoModule)
        {
            if (vgoModule == null)
            {
                return;
            }

            ColorOverLifetimeModule module = particleSystem.colorOverLifetime;

            module.enabled = vgoModule.enabled;
            module.color = VgoParticleSystemMinMaxGradientConverter.CreateMinMaxGradient(vgoModule.color);
        }

        /// <summary>
        /// Set ParticleSystem colorOverLifetime field value.
        /// </summary>
        /// <param name="particleSystem"></param>
        /// <param name="vgoModule"></param>
        protected virtual void SetModuleValue(ParticleSystem particleSystem, in VGO_PS_ColorBySpeedModule vgoModule)
        {
            if (vgoModule == null)
            {
                return;
            }

            ColorBySpeedModule module = particleSystem.colorBySpeed;

            module.enabled = vgoModule.enabled;
            module.color = VgoParticleSystemMinMaxGradientConverter.CreateMinMaxGradient(vgoModule.color);
            module.range = vgoModule.range.GetValueOrDefault(System.Numerics.Vector2.Zero).ToUnityVector2();
        }

        /// <summary>
        /// Set ParticleSystem sizeOverLifetime field value.
        /// </summary>
        /// <param name="particleSystem"></param>
        /// <param name="vgoModule"></param>
        protected virtual void SetModuleValue(ParticleSystem particleSystem, in VGO_PS_SizeOverLifetimeModule vgoModule)
        {
            if (vgoModule == null)
            {
                return;
            }

            SizeOverLifetimeModule module = particleSystem.sizeOverLifetime;

            module.enabled = vgoModule.enabled;
            module.separateAxes = vgoModule.separateAxes;

            if (vgoModule.separateAxes)
            {
                module.x = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.x);
                module.y = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.y);
                module.z = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.z);
                module.xMultiplier = vgoModule.xMultiplier;
                module.yMultiplier = vgoModule.yMultiplier;
                module.zMultiplier = vgoModule.zMultiplier;
            }
            else
            {
                module.size = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.x);
                module.sizeMultiplier = vgoModule.xMultiplier;
            }
        }

        /// <summary>
        /// Set ParticleSystem sizeBySpeed field value.
        /// </summary>
        /// <param name="particleSystem"></param>
        /// <param name="vgoModule"></param>
        protected virtual void SetModuleValue(ParticleSystem particleSystem, in VGO_PS_SizeBySpeedModule vgoModule)
        {
            if (vgoModule == null)
            {
                return;
            }

            SizeBySpeedModule module = particleSystem.sizeBySpeed;

            module.enabled = vgoModule.enabled;
            module.separateAxes = vgoModule.separateAxes;

            if (vgoModule.separateAxes)
            {
                module.x = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.x);
                module.y = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.y);
                module.z = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.z);
                module.xMultiplier = vgoModule.xMultiplier;
                module.yMultiplier = vgoModule.yMultiplier;
                module.zMultiplier = vgoModule.zMultiplier;
            }
            else
            {
                module.size = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.x);
                module.sizeMultiplier = vgoModule.xMultiplier;
            }

            module.range = vgoModule.range.GetValueOrDefault(System.Numerics.Vector2.Zero).ToUnityVector2();
        }

        /// <summary>
        /// Set ParticleSystem rotationOverLifetime field value.
        /// </summary>
        /// <param name="particleSystem"></param>
        /// <param name="vgoModule"></param>
        protected virtual void SetModuleValue(ParticleSystem particleSystem, in VGO_PS_RotationOverLifetimeModule vgoModule)
        {
            if (vgoModule == null)
            {
                return;
            }

            RotationOverLifetimeModule module = particleSystem.rotationOverLifetime;

            module.enabled = vgoModule.enabled;
            module.separateAxes = vgoModule.separateAxes;

            if (vgoModule.separateAxes)
            {
                module.x = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.x);
                module.y = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.y);
                module.z = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.z);
                module.xMultiplier = vgoModule.xMultiplier;
                module.yMultiplier = vgoModule.yMultiplier;
                module.zMultiplier = vgoModule.zMultiplier;
            }
            else
            {
                module.x = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.x);
                module.xMultiplier = vgoModule.xMultiplier;
            }
        }

        /// <summary>
        /// Set ParticleSystem rotationBySpeed field value.
        /// </summary>
        /// <param name="particleSystem"></param>
        /// <param name="vgoModule"></param>
        protected virtual void SetModuleValue(ParticleSystem particleSystem, in VGO_PS_RotationBySpeedModule vgoModule)
        {
            if (vgoModule == null)
            {
                return;
            }

            RotationBySpeedModule module = particleSystem.rotationBySpeed;

            module.enabled = vgoModule.enabled;
            module.separateAxes = vgoModule.separateAxes;

            if (vgoModule.separateAxes)
            {
                module.x = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.x);
                module.y = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.y);
                module.z = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.z);
                module.xMultiplier = vgoModule.xMultiplier;
                module.yMultiplier = vgoModule.yMultiplier;
                module.zMultiplier = vgoModule.zMultiplier;
            }
            else
            {
                module.x = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.x);
                module.xMultiplier = vgoModule.xMultiplier;
            }

            module.range = vgoModule.range.GetValueOrDefault(System.Numerics.Vector2.Zero).ToUnityVector2();
        }

        /// <summary>
        /// Set ParticleSystem externalForcesModule field value.
        /// </summary>
        /// <param name="particleSystem"></param>
        /// <param name="vgoModule"></param>
        protected virtual void SetModuleValue(ParticleSystem particleSystem, in VGO_PS_ExternalForcesModule vgoModule)
        {
            if (vgoModule == null)
            {
                return;
            }

            ExternalForcesModule module = particleSystem.externalForces;

            module.enabled = vgoModule.enabled;
            module.multiplierCurve = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.multiplierCurve);
            module.multiplier = vgoModule.multiplier;
            module.influenceFilter = (UnityEngine.ParticleSystemGameObjectFilter)vgoModule.influenceFilter;
            module.influenceMask = new LayerMask() { value = vgoModule.influenceMask };
        }

        /// <summary>
        /// Set ParticleSystem noiseModule field value.
        /// </summary>
        /// <param name="particleSystem"></param>
        /// <param name="vgoModule"></param>
        protected virtual void SetModuleValue(ParticleSystem particleSystem, in VGO_PS_NoiseModule vgoModule)
        {
            if (vgoModule == null)
            {
                return;
            }

            NoiseModule module = particleSystem.noise;

            module.enabled = vgoModule.enabled;
            module.separateAxes = vgoModule.separateAxes;

            if (vgoModule.separateAxes)
            {
                module.strengthX = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.strengthX);
                module.strengthY = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.strengthY);
                module.strengthZ = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.strengthZ);
                module.strengthXMultiplier = vgoModule.strengthXMultiplier;
                module.strengthYMultiplier = vgoModule.strengthYMultiplier;
                module.strengthZMultiplier = vgoModule.strengthZMultiplier;
            }
            else
            {
                module.strength = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.strengthX);
                module.strengthMultiplier = vgoModule.strengthXMultiplier;
            }

            module.frequency = vgoModule.frequency;
            module.scrollSpeed = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.scrollSpeed);
            module.scrollSpeedMultiplier = vgoModule.scrollSpeedMultiplier;
            module.damping = vgoModule.damping;
            module.octaveCount = vgoModule.octaveCount;
            module.octaveMultiplier = vgoModule.octaveMultiplier;
            module.octaveScale = vgoModule.octaveScale;
            module.quality = (UnityEngine.ParticleSystemNoiseQuality)vgoModule.quality;
            module.remapEnabled = vgoModule.remapEnabled;

            if (vgoModule.remapEnabled)
            {
                if (vgoModule.separateAxes)
                {
                    module.remapX = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.remapX);
                    module.remapY = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.remapY);
                    module.remapZ = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.remapZ);
                    module.remapXMultiplier = vgoModule.remapXMultiplier;
                    module.remapYMultiplier = vgoModule.remapYMultiplier;
                    module.remapZMultiplier = vgoModule.remapZMultiplier;
                }
                else
                {
                    module.remap = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.remapX);
                    module.remapMultiplier = vgoModule.remapXMultiplier;
                }
            }

            module.positionAmount = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.positionAmount);
            module.rotationAmount = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.rotationAmount);
            module.sizeAmount = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.sizeAmount);
        }

        /// <summary>
        /// Set ParticleSystem collisionModule field value.
        /// </summary>
        /// <param name="particleSystem"></param>
        /// <param name="vgoModule"></param>
        protected virtual void SetModuleValue(ParticleSystem particleSystem, in VGO_PS_CollisionModule vgoModule)
        {
            ThrowHelper.ThrowNotImplementedException();
        }

        /// <summary>
        /// Set ParticleSystem triggerModule field value.
        /// </summary>
        /// <param name="particleSystem"></param>
        /// <param name="vgoModule"></param>
        protected virtual void SetModuleValue(ParticleSystem particleSystem, in VGO_PS_TriggerModule vgoModule)
        {
            ThrowHelper.ThrowNotImplementedException();
        }

        /// <summary>
        /// Set ParticleSystem subEmittersModule field value.
        /// </summary>
        /// <param name="particleSystem"></param>
        /// <param name="vgoModule"></param>
        protected virtual void SetModuleValue(ParticleSystem particleSystem, in VGO_PS_SubEmittersModule vgoModule)
        {
            ThrowHelper.ThrowNotImplementedException();
        }

        /// <summary>
        /// Set ParticleSystem textureSheetAnimationModule field value.
        /// </summary>
        /// <param name="particleSystem"></param>
        /// <param name="vgoModule"></param>
        protected virtual void SetModuleValue(ParticleSystem particleSystem, in VGO_PS_TextureSheetAnimationModule vgoModule)
        {
            if (vgoModule == null)
            {
                return;
            }

            TextureSheetAnimationModule module = particleSystem.textureSheetAnimation;

            module.enabled = vgoModule.enabled;

            module.mode = (UnityEngine.ParticleSystemAnimationMode)vgoModule.mode;

            module.numTilesX = vgoModule.numTilesX;
            module.numTilesY = vgoModule.numTilesY;

            module.animation = (UnityEngine.ParticleSystemAnimationType)vgoModule.animation;

            module.rowMode = (UnityEngine.ParticleSystemAnimationRowMode)vgoModule.rowMode;
            module.rowIndex = vgoModule.rowIndex;

            module.timeMode = (UnityEngine.ParticleSystemAnimationTimeMode)vgoModule.timeMode;

            if (vgoModule.timeMode == NewtonVgo.ParticleSystemAnimationTimeMode.Lifetime)
            {
                module.frameOverTime = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.frameOverTime);
                module.frameOverTimeMultiplier = vgoModule.frameOverTimeMultiplier;
            }
            else if (vgoModule.timeMode == NewtonVgo.ParticleSystemAnimationTimeMode.Speed)
            {
                module.speedRange = vgoModule.speedRange.GetValueOrDefault().ToUnityVector2();
            }
            else if (vgoModule.timeMode == NewtonVgo.ParticleSystemAnimationTimeMode.FPS)
            {
                module.fps = vgoModule.fps;
            }

            module.startFrame = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.startFrame);
            module.startFrameMultiplier = vgoModule.startFrameMultiplier;

            module.cycleCount = vgoModule.cycleCount;

            module.uvChannelMask = (UnityEngine.Rendering.UVChannelFlags)vgoModule.uvChannelMask;
        }

        /// <summary>
        /// Set ParticleSystem lightsModule field value.
        /// </summary>
        /// <param name="particleSystem"></param>
        /// <param name="vgoModule"></param>
        protected virtual void SetModuleValue(ParticleSystem particleSystem, in VGO_PS_LightsModule vgoModule)
        {
            if (vgoModule == null)
            {
                return;
            }

            LightsModule module = particleSystem.lights;

            module.enabled = vgoModule.enabled;
            module.ratio = vgoModule.ratio;
            module.useRandomDistribution = vgoModule.useRandomDistribution;
            module.useParticleColor = vgoModule.useParticleColor;
            module.sizeAffectsRange = vgoModule.sizeAffectsRange;
            module.alphaAffectsIntensity = vgoModule.alphaAffectsIntensity;
            module.range = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.range);
            module.rangeMultiplier = vgoModule.rangeMultiplier;
            module.intensity = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.intensity);
            module.intensityMultiplier = vgoModule.intensityMultiplier;
            module.maxLights = vgoModule.maxLights;

            if (vgoModule.light != null)
            {
                // @notice
                if (particleSystem.gameObject.TryGetComponent<Light>(out var goLight) == false)
                {
                    goLight = particleSystem.gameObject.AddComponent<Light>();
                }

                VgoLightConverter.SetComponentValue(goLight, vgoModule.light);

                goLight.enabled = false;

                module.light = goLight;

                if (Application.isEditor)
                {
                    // @todo
                }
            }
        }

        /// <summary>
        /// Set ParticleSystem noiseModule field value.
        /// </summary>
        /// <param name="particleSystem"></param>
        /// <param name="vgoModule"></param>
        protected virtual void SetModuleValue(ParticleSystem particleSystem, in VGO_PS_TrailModule vgoModule)
        {
            if (vgoModule == null)
            {
                return;
            }

            TrailModule module = particleSystem.trails;

            module.enabled = vgoModule.enabled;
            module.mode = (UnityEngine.ParticleSystemTrailMode)vgoModule.mode;

            // PerParticle
            module.ratio = vgoModule.ratio;
            module.lifetime = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.lifetime);
            module.lifetimeMultiplier = vgoModule.lifetimeMultiplier;
            module.minVertexDistance = vgoModule.minVertexDistance;
            module.worldSpace = vgoModule.worldSpace;
            module.dieWithParticles = vgoModule.dieWithParticles;

            // Ribbon
            module.ribbonCount = vgoModule.ribbonCount;
            module.splitSubEmitterRibbons = vgoModule.splitSubEmitterRibbons;
            module.attachRibbonsToTransform = vgoModule.attachRibbonsToTransform;

            module.textureMode = (UnityEngine.ParticleSystemTrailTextureMode)vgoModule.textureMode;
            module.sizeAffectsWidth = vgoModule.sizeAffectsWidth;
            module.sizeAffectsLifetime = vgoModule.sizeAffectsLifetime;
            module.inheritParticleColor = vgoModule.inheritParticleColor;
            module.colorOverLifetime = VgoParticleSystemMinMaxGradientConverter.CreateMinMaxGradient(vgoModule.colorOverLifetime);
            module.widthOverTrail = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoModule.widthOverTrail);
            module.widthOverTrailMultiplier = vgoModule.widthOverTrailMultiplier;
            module.colorOverTrail = VgoParticleSystemMinMaxGradientConverter.CreateMinMaxGradient(vgoModule.colorOverTrail);
            module.generateLightingData = vgoModule.generateLightingData;
            module.shadowBias = vgoModule.shadowBias;
        }

        /// <summary>
        /// Set ParticleSystem customDataModule field value.
        /// </summary>
        /// <param name="particleSystem"></param>
        /// <param name="vgoModule"></param>
        protected virtual void SetModuleValue(ParticleSystem particleSystem, in VGO_PS_CustomDataModule vgoModule)
        {
            if (vgoModule == null)
            {
                return;
            }

            CustomDataModule module = particleSystem.customData;

            module.enabled = vgoModule.enabled;

            if (vgoModule.customData == null || vgoModule.customData.Length == 0)
            {
                return;
            }

            for (int customDataIndex = 0; customDataIndex < vgoModule.customData.Length; customDataIndex++)
            {
                VGO_PS_CustomData vgoCustomData = vgoModule.customData[customDataIndex];

                UnityEngine.ParticleSystemCustomData stream;

                if (vgoCustomData.stream == NewtonVgo.ParticleSystemCustomData.Custom1)
                {
                    stream = UnityEngine.ParticleSystemCustomData.Custom1;
                }
                else if (vgoCustomData.stream == NewtonVgo.ParticleSystemCustomData.Custom2)
                {
                    stream = UnityEngine.ParticleSystemCustomData.Custom2;
                }
                else
                {
                    continue;
                }

                module.SetMode(stream, (UnityEngine.ParticleSystemCustomDataMode)vgoCustomData.mode);

                if (vgoCustomData.mode == NewtonVgo.ParticleSystemCustomDataMode.Disabled)
                {
                    module.SetVectorComponentCount(stream, 0);

                    continue;
                }

                if (vgoCustomData.mode == NewtonVgo.ParticleSystemCustomDataMode.Vector)
                {
                    if (vgoCustomData.vector == null || vgoCustomData.vector.Length == 0)
                    {
                        module.SetVectorComponentCount(stream, 0);

                        continue;
                    }

                    module.SetVectorComponentCount(stream, vgoCustomData.vector.Length);

                    // X, Y, Z, W
                    for (int componentIndex = 0; componentIndex < vgoCustomData.vector.Length; componentIndex++)
                    {
                        VGO_PS_MinMaxCurve? vgoComponentCurve = vgoCustomData.vector[componentIndex];

                        MinMaxCurve componentCurve = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoComponentCurve);

                        module.SetVector(stream, componentIndex, componentCurve);
                    }
                }
                else
                {
                    module.SetVectorComponentCount(stream, 0);
                }

                if (vgoCustomData.mode == NewtonVgo.ParticleSystemCustomDataMode.Color)
                {
                    MinMaxGradient colorGradient = VgoParticleSystemMinMaxGradientConverter.CreateMinMaxGradient(vgoCustomData.color);

                    module.SetColor(stream, colorGradient);
                }
            }
        }

        #endregion

        #region Public Methods (Renderer)

        /// <summary>
        /// Set particleSystemRenderer field value.
        /// </summary>
        /// <param name="particleSystemRenderer"></param>
        /// <param name="vgoRenderer"></param>
        /// <param name="geometryCoordinate"></param>
        /// <param name="materialList"></param>
        public virtual void SetComponentValue(
            ParticleSystemRenderer particleSystemRenderer, 
            in VGO_PS_Renderer vgoRenderer,
            in VgoGeometryCoordinate geometryCoordinate, 
            in IList<Material?>? materialList)
        {
            if (vgoRenderer == null)
            {
                return;
            }

            particleSystemRenderer.enabled = vgoRenderer.enabled;
            particleSystemRenderer.renderMode = (UnityEngine.ParticleSystemRenderMode)vgoRenderer.renderMode;

            // Billboard
            particleSystemRenderer.cameraVelocityScale = vgoRenderer.cameraVelocityScale;
            particleSystemRenderer.velocityScale = vgoRenderer.velocityScale;
            particleSystemRenderer.lengthScale = vgoRenderer.lengthScale;
            particleSystemRenderer.normalDirection = vgoRenderer.normalDirection;

            // Material
            if ((materialList != null) && vgoRenderer.sharedMaterial.IsInRangeOf(materialList))
            {
                if (materialList[vgoRenderer.sharedMaterial] != null)
                {
                    particleSystemRenderer.sharedMaterial = materialList[vgoRenderer.sharedMaterial];
                }
            }

            if ((materialList != null) && vgoRenderer.trailMaterialIndex.IsInRangeOf(materialList))
            {
                if (materialList[vgoRenderer.trailMaterialIndex] != null)
                {
                    particleSystemRenderer.trailMaterial = materialList[vgoRenderer.trailMaterialIndex];
                }
            }

            particleSystemRenderer.sortMode = (UnityEngine.ParticleSystemSortMode)vgoRenderer.sortMode;
            particleSystemRenderer.sortingFudge = vgoRenderer.sortingFudge;
            particleSystemRenderer.minParticleSize = vgoRenderer.minParticleSize;
            particleSystemRenderer.maxParticleSize = vgoRenderer.maxParticleSize;
            particleSystemRenderer.alignment = (UnityEngine.ParticleSystemRenderSpace)vgoRenderer.alignment;
            particleSystemRenderer.flip = vgoRenderer.flip.GetValueOrDefault(System.Numerics.Vector3.Zero).ToUnityVector3();
            particleSystemRenderer.allowRoll = vgoRenderer.allowRoll;
            particleSystemRenderer.pivot = vgoRenderer.pivot.GetValueOrDefault(System.Numerics.Vector3.Zero).ToUnityVector3();

            particleSystemRenderer.maskInteraction = (UnityEngine.SpriteMaskInteraction)vgoRenderer.maskInteraction;
            particleSystemRenderer.enableGPUInstancing = vgoRenderer.enableGPUInstancing;

            // Shadow
            particleSystemRenderer.shadowCastingMode = (UnityEngine.Rendering.ShadowCastingMode)vgoRenderer.shadowCastingMode;
            particleSystemRenderer.receiveShadows = vgoRenderer.receiveShadows;
            particleSystemRenderer.shadowBias = vgoRenderer.shadowBias;

            particleSystemRenderer.motionVectorGenerationMode = (UnityEngine.MotionVectorGenerationMode)vgoRenderer.motionVectorGenerationMode;
            particleSystemRenderer.forceRenderingOff = vgoRenderer.forceRenderingOff;
            particleSystemRenderer.rendererPriority = vgoRenderer.rendererPriority;
            particleSystemRenderer.renderingLayerMask = vgoRenderer.renderingLayerMask;
            particleSystemRenderer.sortingLayerID = vgoRenderer.sortingLayerID;
            particleSystemRenderer.sortingOrder = vgoRenderer.sortingOrder;
            particleSystemRenderer.lightProbeUsage = (UnityEngine.Rendering.LightProbeUsage)vgoRenderer.lightProbeUsage;
            particleSystemRenderer.reflectionProbeUsage = (UnityEngine.Rendering.ReflectionProbeUsage)vgoRenderer.reflectionProbeUsage;

            // @notice
            if (vgoRenderer.probeAnchor != null)
            {
                VgoTransformConverter.SetComponentValue(particleSystemRenderer.probeAnchor, vgoRenderer.probeAnchor, geometryCoordinate);
            }

            if (particleSystemRenderer.sharedMaterial != null)
            {
                //SetVertexStream(particleSystemRenderer, particleSystemRenderer.sharedMaterial);
            }
        }

        #endregion

        #region Protected Methods (Renderer)

        /// <summary>
        /// Set vertex stream.
        /// </summary>
        /// <param name="particleSystemRenderer"></param>
        /// <param name="material"></param>
        protected virtual void SetVertexStream(ParticleSystemRenderer particleSystemRenderer, in Material material)
        {
            bool useLighting = (material.GetFloat(UniParticleShader.Property.LightingEnabled) > 0.0f);
            bool useFlipbookBlending = (material.GetFloat(UniParticleShader.Property.FlipbookMode) > 0.0f);
            bool useTangents = useLighting && material.GetTexture(UniParticleShader.Property.BumpMap);

            //bool useGPUInstancing = ShaderUtil.HasProceduralInstancing(material.shader);
            bool useGPUInstancing = particleSystemRenderer.enableGPUInstancing;

            var streams = new List<ParticleSystemVertexStream>();

            streams.Add(ParticleSystemVertexStream.Position);

            if (useLighting)
            {
                streams.Add(ParticleSystemVertexStream.Normal);
            }

            streams.Add(ParticleSystemVertexStream.Color);
            streams.Add(ParticleSystemVertexStream.UV);

            if (useTangents)
            {
                streams.Add(ParticleSystemVertexStream.Tangent);
            }

            if (useFlipbookBlending)
            {
                streams.Add(ParticleSystemVertexStream.UV2);
                streams.Add(ParticleSystemVertexStream.AnimBlend);
            }

            List<ParticleSystemVertexStream> instancedStreams = new List<ParticleSystemVertexStream>(streams);

            if (useGPUInstancing)
            {
                instancedStreams.Add(ParticleSystemVertexStream.AnimFrame);
            }

            // Set the streams on all systems using this material
            //if (useGPUInstancing &&
            //    particleSystemRenderer.renderMode == ParticleSystemRenderMode.Mesh &&
            //    particleSystemRenderer.supportsMeshInstancing)
            if (useGPUInstancing && (particleSystemRenderer.renderMode == UnityEngine.ParticleSystemRenderMode.Mesh))
            {
                particleSystemRenderer.SetActiveVertexStreams(instancedStreams);
            }
            else
            {
                particleSystemRenderer.SetActiveVertexStreams(streams);
            }
        }

        #endregion
    }
}