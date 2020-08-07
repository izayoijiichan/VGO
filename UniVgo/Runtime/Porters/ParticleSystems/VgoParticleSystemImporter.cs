// ----------------------------------------------------------------------
// @Namespace : UniVgo.Porters
// @Class     : VgoParticleSystemImporter
// ----------------------------------------------------------------------
namespace UniVgo.Porters
{
    using NewtonGltf;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UniVgo.Converters;
    using VgoGltf;
    using static UnityEngine.ParticleSystem;

    /// <summary>
    /// VGO Particle System Importer
    /// </summary>
    public class VgoParticleSystemImporter
    {
        #region Public Methods

        /// <summary>
        /// Adds a ParticleSystem component to the game object.
        /// </summary>
        /// <param name="go"></param>
        /// <param name="vgoParticleSystem"></param>
        /// <param name="materialList"></param>
        /// <param name="texture2dList"></param>
        /// <returns>Returns ParticleSystem component.</returns>
        public virtual ParticleSystem AddComponent(GameObject go, VGO_ParticleSystem vgoParticleSystem, IList<Material> materialList, IList<Texture2D> texture2dList)
        {
            ParticleSystem particleSystem = go.GetComponent<ParticleSystem>();

            if (particleSystem == null)
            {
                particleSystem = go.AddComponent<ParticleSystem>();
            }

            if (vgoParticleSystem != null)
            {
                SetModuleValue(particleSystem, vgoParticleSystem.main);
                SetModuleValue(particleSystem, vgoParticleSystem.emission);
                SetModuleValue(particleSystem, vgoParticleSystem.shape, texture2dList);
                SetModuleValue(particleSystem, vgoParticleSystem.velocityOverLifetime);
                SetModuleValue(particleSystem, vgoParticleSystem.limitVelocityOverLifetime);
                SetModuleValue(particleSystem, vgoParticleSystem.inheritVelocity);
                SetModuleValue(particleSystem, vgoParticleSystem.forceOverLifetime);
                SetModuleValue(particleSystem, vgoParticleSystem.colorOverLifetime);
                SetModuleValue(particleSystem, vgoParticleSystem.colorBySpeed);
                SetModuleValue(particleSystem, vgoParticleSystem.sizeOverLifetime);
                SetModuleValue(particleSystem, vgoParticleSystem.sizeBySpeed);
                SetModuleValue(particleSystem, vgoParticleSystem.rotationOverLifetime);
                SetModuleValue(particleSystem, vgoParticleSystem.rotationBySpeed);
                SetModuleValue(particleSystem, vgoParticleSystem.externalForces);
                SetModuleValue(particleSystem, vgoParticleSystem.noise);
                //SetModuleValue(particleSystem, vgoParticleSystem.Collision);
                //SetModuleValue(particleSystem, vgoParticleSystem.Trigger);
                //SetModuleValue(particleSystem, vgoParticleSystem.SubEmitters);
                //SetModuleValue(particleSystem, vgoParticleSystem.TextureSheetAnimation);
                SetModuleValue(particleSystem, vgoParticleSystem.lights);
                SetModuleValue(particleSystem, vgoParticleSystem.trails);
                //SetModuleValue(particleSystem, vgoParticleSystem.CustomData);
            }

            ParticleSystemRenderer particleSystemRenderer = go.GetComponent<ParticleSystemRenderer>();

            if (particleSystemRenderer == null)
            {
                particleSystemRenderer = go.AddComponent<ParticleSystemRenderer>();
            }

            if (particleSystemRenderer != null)
            {
                SetComponentValue(particleSystemRenderer, vgoParticleSystem.renderer, materialList);
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
        protected virtual void SetModuleValue(ParticleSystem particleSystem, VGO_PS_MainModule vgoModule)
        {
            if (vgoModule == null)
            {
                return;
            }

            MainModule module = particleSystem.main;

            // @notice
            if (Application.isPlaying == false)
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
            VgoTransformConverter.SetComponentValue(module.customSimulationSpace, vgoModule.customSimulationSpace);

            module.useUnscaledTime = vgoModule.useUnscaledTime;
            module.scalingMode = (UnityEngine.ParticleSystemScalingMode)vgoModule.scalingMode;
            module.playOnAwake = vgoModule.playOnAwake;
            module.emitterVelocityMode = (UnityEngine.ParticleSystemEmitterVelocityMode)vgoModule.emitterVelocityMode;
            module.maxParticles = vgoModule.maxParticles;
            module.stopAction = (UnityEngine.ParticleSystemStopAction)vgoModule.stopAction;
            module.cullingMode = (UnityEngine.ParticleSystemCullingMode)vgoModule.cullingMode;
            module.ringBufferMode = (UnityEngine.ParticleSystemRingBufferMode)vgoModule.ringBufferMode;

            if (vgoModule.ringBufferMode == VgoGltf.ParticleSystemRingBufferMode.LoopUntilReplaced)
            {
                module.ringBufferLoopRange = vgoModule.ringBufferLoopRange.GetValueOrDefault(System.Numerics.Vector2.Zero).ToUnityVector2();
            }
        }

        /// <summary>
        /// Set ParticleSystem emission field value.
        /// </summary>
        /// <param name="particleSystem"></param>
        /// <param name="vgoModule"></param>
        protected virtual void SetModuleValue(ParticleSystem particleSystem, VGO_PS_EmissionModule vgoModule)
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
        /// <param name="texture2dList"></param>
        protected virtual void SetModuleValue(ParticleSystem particleSystem, VGO_PS_ShapeModule vgoModule, IList<Texture2D> texture2dList)
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
            if ((texture2dList != null) && (-1 < vgoModule.textureIndex) && (vgoModule.textureIndex < texture2dList.Count))
            {
                module.texture = texture2dList[vgoModule.textureIndex];
            }
            module.textureClipChannel = (UnityEngine.ParticleSystemShapeTextureChannel)vgoModule.textureClipChannel;
            module.textureClipThreshold = vgoModule.textureClipThreshold;
            module.textureColorAffectsParticles = vgoModule.textureColorAffectsParticles;
            module.textureAlphaAffectsParticles = vgoModule.textureAlphaAffectsParticles;
            module.textureBilinearFiltering = vgoModule.textureBilinearFiltering;
            module.textureUVChannel = vgoModule.textureUVChannel;
            module.position = vgoModule.position.GetValueOrDefault(System.Numerics.Vector3.Zero).ToUnityVector3().ReverseZ();
            module.rotation = vgoModule.rotation.GetValueOrDefault(System.Numerics.Vector3.Zero).ToUnityVector3().ReverseZ();
            module.scale = vgoModule.scale.GetValueOrDefault(System.Numerics.Vector3.One).ToUnityVector3();
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
        protected virtual void SetModuleValue(ParticleSystem particleSystem, VGO_PS_VelocityOverLifetimeModule vgoModule)
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
        protected virtual void SetModuleValue(ParticleSystem particleSystem, VGO_PS_LimitVelocityOverLifetimeModule vgoModule)
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
        /// Set ParticleSystem inhelitVelocity field value.
        /// </summary>
        /// <param name="particleSystem"></param>
        /// <param name="vgoForceOverLifetimeModule"></param>
        protected virtual void SetModuleValue(ParticleSystem particleSystem, VGO_PS_InheritVelocityModule vgoModule)
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
        protected virtual void SetModuleValue(ParticleSystem particleSystem, VGO_PS_ForceOverLifetimeModule vgoModule)
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
        protected virtual void SetModuleValue(ParticleSystem particleSystem, VGO_PS_ColorOverLifetimeModule vgoModule)
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
        protected virtual void SetModuleValue(ParticleSystem particleSystem, VGO_PS_ColorBySpeedModule vgoModule)
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
        protected virtual void SetModuleValue(ParticleSystem particleSystem, VGO_PS_SizeOverLifetimeModule vgoModule)
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
        protected virtual void SetModuleValue(ParticleSystem particleSystem, VGO_PS_SizeBySpeedModule vgoModule)
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
        protected virtual void SetModuleValue(ParticleSystem particleSystem, VGO_PS_RotationOverLifetimeModule vgoModule)
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
        protected virtual void SetModuleValue(ParticleSystem particleSystem, VGO_PS_RotationBySpeedModule vgoModule)
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
        protected virtual void SetModuleValue(ParticleSystem particleSystem, VGO_PS_ExternalForcesModule vgoModule)
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
        protected virtual void SetModuleValue(ParticleSystem particleSystem, VGO_PS_NoiseModule vgoModule)
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
        protected virtual void SetModuleValue(ParticleSystem particleSystem, VGO_PS_CollisionModule vgoModule)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Set ParticleSystem triggerModule field value.
        /// </summary>
        /// <param name="particleSystem"></param>
        /// <param name="vgoModule"></param>
        protected virtual void SetModuleValue(ParticleSystem particleSystem, VGO_PS_TriggerModule vgoModule)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Set ParticleSystem subEmittersModule field value.
        /// </summary>
        /// <param name="particleSystem"></param>
        /// <param name="vgoModule"></param>
        protected virtual void SetModuleValue(ParticleSystem particleSystem, VGO_PS_SubEmittersModule vgoModule)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Set ParticleSystem textureSheetAnimationModule field value.
        /// </summary>
        /// <param name="particleSystem"></param>
        /// <param name="vgoModule"></param>
        protected virtual void SetModuleValue(ParticleSystem particleSystem, VGO_PS_TextureSheetAnimationModule vgoModule)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Set ParticleSystem lightsModule field value.
        /// </summary>
        /// <param name="particleSystem"></param>
        /// <param name="vgoModule"></param>
        protected virtual void SetModuleValue(ParticleSystem particleSystem, VGO_PS_LightsModule vgoModule)
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
                Light goLight = particleSystem.gameObject.GetComponent<Light>();

                if (goLight == null)
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
        protected virtual void SetModuleValue(ParticleSystem particleSystem, VGO_PS_TrailModule vgoModule)
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
        protected virtual void SetModuleValue(ParticleSystem particleSystem, VGO_PS_CustomDataModule vgoModule)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Protected Methods (Renderer)

        /// <summary>
        /// Set particleSystemRenderer field value.
        /// </summary>
        /// <param name="particleSystemRenderer"></param>
        /// <param name="vgoRenderer"></param>
        /// <param name="materialList"></param>
        public virtual void SetComponentValue(ParticleSystemRenderer particleSystemRenderer, VGO_PS_Renderer vgoRenderer, IList<Material> materialList)
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
            if ((materialList != null) && (-1 < vgoRenderer.sharedMaterial) && (vgoRenderer.sharedMaterial < materialList.Count))
            {
                particleSystemRenderer.sharedMaterial = materialList[vgoRenderer.sharedMaterial];
            }

            if ((materialList != null) && (-1 < vgoRenderer.trailMaterialIndex) && (vgoRenderer.trailMaterialIndex < materialList.Count))
            {
                particleSystemRenderer.trailMaterial = materialList[vgoRenderer.trailMaterialIndex];
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
            VgoTransformConverter.SetComponentValue(particleSystemRenderer.probeAnchor, vgoRenderer.probeAnchor);

            if (particleSystemRenderer.sharedMaterial != null)
            {
                //SetVertexStream(particleSystemRenderer, particleSystemRenderer.sharedMaterial);
            }
        }

        /// <summary>
        /// Set vertex stream.
        /// </summary>
        /// <param name="particleSystemRenderer"></param>
        /// <param name="material"></param>
        protected virtual void SetVertexStream(ParticleSystemRenderer particleSystemRenderer, Material material)
        {
            bool useLighting = (material.GetFloat(UniParticleShader.Property.LightingEnabled) > 0.0f);
            bool useFlipbookBlending = (material.GetFloat(UniParticleShader.Property.FlipbookMode) > 0.0f);
            bool useTangents = useLighting && material.GetTexture(UniParticleShader.Property.BumpMap);

            //bool useGPUInstancing = ShaderUtil.HasProceduralInstancing(material.shader);
            bool useGPUInstancing = particleSystemRenderer.enableGPUInstancing;

            List<ParticleSystemVertexStream> streams = new List<ParticleSystemVertexStream>();

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