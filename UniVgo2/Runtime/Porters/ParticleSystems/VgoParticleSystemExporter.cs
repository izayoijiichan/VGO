// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : VgoParticleSystemExporter
// ----------------------------------------------------------------------
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using NewtonVgo.Schema.ParticleSystems;
    using System;
    using System.Linq;
    using UnityEngine;
    using UniVgo2.Converters;
    using static UnityEngine.ParticleSystem;

    /// <summary>
    /// VGO Particle System Exporter
    /// </summary>
    public class VgoParticleSystemExporter : IVgoParticleSystemExporter
    {
        #region Public Methods

        /// <summary>
        /// Create VgoParticleSystem.
        /// </summary>
        /// <param name="particleSystem"></param>
        /// <param name="particleSystemRenderer"></param>
        /// <param name="vgoStorage"></param>
        /// <param name="exportTexture"></param>
        /// <returns></returns>
        public virtual VgoParticleSystem Create(ParticleSystem particleSystem, ParticleSystemRenderer particleSystemRenderer, IVgoStorage vgoStorage, ExportTextureDelegate exportTexture)
        {
            if (particleSystem == null)
            {
                return null;
            }

            var vgoParticleSystem = new VgoParticleSystem()
            {
                main = CreateVgoModule(particleSystem.main, vgoStorage.GeometryCoordinate),
                emission = CreateVgoModule(particleSystem.emission),
                shape = CreateVgoModule(particleSystem.shape, vgoStorage, exportTexture),
                velocityOverLifetime = CreateVgoModule(particleSystem.velocityOverLifetime),
                limitVelocityOverLifetime = CreateVgoModule(particleSystem.limitVelocityOverLifetime),
                inheritVelocity = CreateVgoModule(particleSystem.inheritVelocity),
                forceOverLifetime = CreateVgoModule(particleSystem.forceOverLifetime),
                colorOverLifetime = CreateVgoModule(particleSystem.colorOverLifetime),
                colorBySpeed = CreateVgoModule(particleSystem.colorBySpeed),
                sizeOverLifetime = CreateVgoModule(particleSystem.sizeOverLifetime),
                sizeBySpeed = CreateVgoModule(particleSystem.sizeBySpeed),
                rotationOverLifetime = CreateVgoModule(particleSystem.rotationOverLifetime),
                rotationBySpeed = CreateVgoModule(particleSystem.rotationBySpeed),
                externalForces = CreateVgoModule(particleSystem.externalForces),
                noise = CreateVgoModule(particleSystem.noise),
                //Collision = CreateVgoModule(particleSystem.collision),
                //Trigger = CreateVgoModule(particleSystem.trigger),
                //SubEmitters = CreateVgoModule(particleSystem.subEmitters),
                //TextureSheetAnimation = CreateVgoModule(particleSystem.textureSheetAnimation),
                lights = CreateVgoModule(particleSystem.lights),
                trails = CreateVgoModule(particleSystem.trails),
                //CustomData = CreateVgoModule(particleSystem.customData),
                renderer = CreateVgoPsRenderer(particleSystemRenderer, vgoStorage.GeometryCoordinate, vgoStorage.Layout),
            };

            return vgoParticleSystem;
        }

        #endregion

        #region Protected Methods (Module)

        /// <summary>
        /// Create VGO_PS_MainModule from MainModule.
        /// </summary>
        /// <param name="module"></param>
        /// <param name="geometryCoordinate"></param>
        /// <returns></returns>
        protected virtual VGO_PS_MainModule CreateVgoModule(MainModule module, VgoGeometryCoordinate geometryCoordinate)
        {
            var vgoModule = new VGO_PS_MainModule()
            {
                duration = module.duration,
                loop = module.loop,
                prewarm = module.prewarm,
                startDelay = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.startDelay),
                startDelayMultiplier = module.startDelayMultiplier,
                startLifetime = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.startLifetime),
                startLifetimeMultiplier = module.startLifetimeMultiplier,
                startSpeed = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.startSpeed),
                startSpeedMultiplier = module.startSpeedMultiplier,
                startSize3D = module.startSize3D,
                startRotation3D = module.startRotation3D,
                flipRotation = module.flipRotation,
                startColor = VgoParticleSystemMinMaxGradientConverter.CreateFrom(module.startColor),
                gravityModifier = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.gravityModifier),
                gravityModifierMultiplier = module.gravityModifierMultiplier,
                simulationSpace = (NewtonVgo.ParticleSystemSimulationSpace)module.simulationSpace,
                simulationSpeed = module.simulationSpeed,
                customSimulationSpace = VgoTransformConverter.CreateFrom(module.customSimulationSpace, geometryCoordinate),
                useUnscaledTime = module.useUnscaledTime,
                scalingMode = (NewtonVgo.ParticleSystemScalingMode)module.scalingMode,
                playOnAwake = module.playOnAwake,
                emitterVelocityMode = (NewtonVgo.ParticleSystemEmitterVelocityMode)module.emitterVelocityMode,
                maxParticles = module.maxParticles,
                stopAction = (NewtonVgo.ParticleSystemStopAction)module.stopAction,
                cullingMode = (NewtonVgo.ParticleSystemCullingMode)module.cullingMode,
                ringBufferMode = (NewtonVgo.ParticleSystemRingBufferMode)module.ringBufferMode,
            };

            if (module.startSize3D)
            {
                vgoModule.startSizeX = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.startSizeX);
                vgoModule.startSizeY = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.startSizeY);
                vgoModule.startSizeZ = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.startSizeZ);
                vgoModule.startSizeXMultiplier = module.startSizeXMultiplier;
                vgoModule.startSizeYMultiplier = module.startSizeYMultiplier;
                vgoModule.startSizeZMultiplier = module.startSizeZMultiplier;
            }
            else
            {
                vgoModule.startSize = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.startSize);
                vgoModule.startSizeMultiplier = module.startSizeMultiplier;
            }

            if (module.startRotation3D)
            {
                vgoModule.startRotationX = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.startRotationX);
                vgoModule.startRotationY = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.startRotationY);
                vgoModule.startRotationZ = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.startRotationZ);
                vgoModule.StartRotationXMultiplier = module.startRotationXMultiplier;
                vgoModule.StartRotationYMultiplier = module.startRotationYMultiplier;
                vgoModule.StartRotationZMultiplier = module.startRotationZMultiplier;
            }
            else
            {
                vgoModule.startRotation = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.startRotation);
                vgoModule.StartRotationMultiplier = module.startRotationMultiplier;
            }

            if (module.ringBufferMode == UnityEngine.ParticleSystemRingBufferMode.LoopUntilReplaced)
            {
                vgoModule.ringBufferLoopRange = module.ringBufferLoopRange.ToNumericsVector2();
            }

            return vgoModule;
        }

        /// <summary>
        /// Create VGO_PS_EmissionModule from EmissionModule.
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        protected virtual VGO_PS_EmissionModule CreateVgoModule(EmissionModule module)
        {
            var vgoModule = new VGO_PS_EmissionModule()
            {
                enabled = module.enabled,
                rateOverTime = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.rateOverTime),
                rateOverTimeMultiplier = module.rateOverTimeMultiplier,
                rateOverDistance = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.rateOverDistance),
                rateOverDistanceMultiplier = module.rateOverDistanceMultiplier,
                //BurstCount = module.burstCount,
            };

            if (module.burstCount > 0)
            {
                Burst[] bursts = new Burst[module.burstCount];

                module.GetBursts(bursts);

                if ((bursts != null) && bursts.Any())
                {
                    vgoModule.bursts = new VGO_PS_Burst[bursts.Length];

                    for (int idx = 0; idx < bursts.Length; idx++)
                    {
                        vgoModule.bursts[idx] = VgoParticleSystemBurstConverter.CreateFrom(bursts[idx]);
                    }
                }
            }

            return vgoModule;
        }

        /// <summary>
        /// Create VGO_PS_ShapeModule from ShapeModule.
        /// </summary>
        /// <param name="module"></param>
        /// <param name="vgoStorage"></param>
        /// <param name="exportTexture"></param>
        /// <returns></returns>
        protected virtual VGO_PS_ShapeModule CreateVgoModule(ShapeModule module, IVgoStorage vgoStorage, ExportTextureDelegate exportTexture)
        {
            var vgoShapeModule = new VGO_PS_ShapeModule()
            {
                enabled = module.enabled,
                shapeType = (NewtonVgo.ParticleSystemShapeType)module.shapeType,
                angle = module.angle,
                radius = module.radius,
                donutRadius = module.donutRadius,
                radiusMode = (NewtonVgo.ParticleSystemShapeMultiModeValue)module.radiusMode,
                radiusSpread = module.radiusSpread,
                radiusSpeed = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.radiusSpeed),
                radiusSpeedMultiplier = module.radiusSpeedMultiplier,
                radiusThickness = module.radiusThickness,
                boxThickness = module.boxThickness.ToNumericsVector3(),
                arc = module.arc,
                arcMode = (NewtonVgo.ParticleSystemShapeMultiModeValue)module.arcMode,
                arcSpread = module.arcSpread,
                arcSpeed = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.arcSpeed),
                arcSpeedMultiplier = module.arcSpeedMultiplier,
                length = module.length,
                meshShapeType = (NewtonVgo.ParticleSystemMeshShapeType)module.meshShapeType,
                meshSpawnMode = (NewtonVgo.ParticleSystemShapeMultiModeValue)module.meshSpawnMode,
                meshSpawnSpread = module.meshSpawnSpread,
                meshSpawnSpeed = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.meshSpawnSpread),
                meshSpawnSpeedMultiplier = module.meshSpawnSpeedMultiplier,
                //mesh
                //meshRenderer
                //skinnedMeshRenderer
                useMeshMaterialIndex = module.useMeshMaterialIndex,
                meshMaterialIndex = module.meshMaterialIndex,
                useMeshColors = module.useMeshColors,
                //sprite
                //spriteRenderer
                normalOffset = module.normalOffset,
                textureIndex = -1,
                textureClipChannel = (NewtonVgo.ParticleSystemShapeTextureChannel)module.textureClipChannel,
                textureClipThreshold = module.textureClipThreshold,
                textureColorAffectsParticles = module.textureColorAffectsParticles,
                textureAlphaAffectsParticles = module.textureAlphaAffectsParticles,
                textureBilinearFiltering = module.textureBilinearFiltering,
                textureUVChannel = module.textureUVChannel,
                position = module.position.ToNullableNumericsVector3(Vector3.zero, vgoStorage.GeometryCoordinate),
                rotation = module.rotation.ToNullableNumericsVector3(Vector3.zero, vgoStorage.GeometryCoordinate),
                scale = module.scale.ToNullableNumericsVector3(Vector3.one),
                alignToDirection = module.alignToDirection,
                randomPositionAmount = module.randomPositionAmount,
                sphericalDirectionAmount = module.sphericalDirectionAmount,
                randomDirectionAmount = module.randomDirectionAmount,
            };

            if (module.texture != null)
            {
                vgoShapeModule.textureIndex = exportTexture(vgoStorage, material: null, module.texture);
            }

            return vgoShapeModule;
        }

        /// <summary>
        /// Create VGO_PS_VelocityOverLifetimeModule from VelocityOverLifetimeModule.
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        protected virtual VGO_PS_VelocityOverLifetimeModule CreateVgoModule(VelocityOverLifetimeModule module)
        {
            return new VGO_PS_VelocityOverLifetimeModule()
            {
                enabled = module.enabled,
                x = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.x),
                y = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.y),
                z = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.z),
                xMultiplier = module.xMultiplier,
                yMultiplier = module.yMultiplier,
                zMultiplier = module.zMultiplier,
                space = (NewtonVgo.ParticleSystemSimulationSpace)module.space,
                orbitalX = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.orbitalX),
                orbitalY = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.orbitalY),
                orbitalZ = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.orbitalZ),
                orbitalXMultiplier = module.orbitalXMultiplier,
                orbitalYMultiplier = module.orbitalYMultiplier,
                orbitalZMultiplier = module.orbitalZMultiplier,
                orbitalOffsetX = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.orbitalOffsetX),
                orbitalOffsetY = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.orbitalOffsetY),
                orbitalOffsetZ = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.orbitalOffsetZ),
                orbitalOffsetXMultiplier = module.orbitalOffsetXMultiplier,
                orbitalOffsetYMultiplier = module.orbitalOffsetYMultiplier,
                orbitalOffsetZMultiplier = module.orbitalOffsetZMultiplier,
                radial = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.radial),
                radialMultiplier = module.radialMultiplier,
                speedModifier = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.speedModifier),
                speedModifierMultiplier = module.speedModifierMultiplier,
            };
        }

        /// <summary>
        /// Create VGO_PS_VelocityOverLifetimeModule from VelocityOverLifetimeModule.
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        protected virtual VGO_PS_LimitVelocityOverLifetimeModule CreateVgoModule(LimitVelocityOverLifetimeModule module)
        {
            var vgoModule = new VGO_PS_LimitVelocityOverLifetimeModule()
            {
                enabled = module.enabled,
                separateAxes = module.separateAxes,
                space = (NewtonVgo.ParticleSystemSimulationSpace)module.space,
                dampen = module.dampen,
                drag = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.drag),
                dragMultiplier = module.dragMultiplier,
                multiplyDragByParticleSize = module.multiplyDragByParticleSize,
                multiplyDragByParticleVelocity = module.multiplyDragByParticleVelocity,
            };

            if (module.separateAxes)
            {
                vgoModule.limitX = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.limitX);
                vgoModule.limitY = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.limitY);
                vgoModule.limitZ = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.limitZ);
                vgoModule.limitXMultiplier = module.limitXMultiplier;
                vgoModule.limitYMultiplier = module.limitYMultiplier;
                vgoModule.limitZMultiplier = module.limitZMultiplier;
            }
            else
            {
                vgoModule.limitX = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.limit);
                vgoModule.limitXMultiplier = module.limitMultiplier;
            }

            return vgoModule;
        }

        /// <summary>
        /// Create VGO_PS_InheritVelocityModule from InheritVelocityModule.
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        protected virtual VGO_PS_InheritVelocityModule CreateVgoModule(InheritVelocityModule module)
        {
            return new VGO_PS_InheritVelocityModule()
            {
                enabled = module.enabled,
                mode = (NewtonVgo.ParticleSystemInheritVelocityMode)module.mode,
                curve = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.curve),
                curveMultiplier = module.curveMultiplier,
            };
        }

        /// <summary>
        /// Create VGO_PS_ForceOverLifetimeModule from ForceOverLifetimeModule.
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        protected virtual VGO_PS_ForceOverLifetimeModule CreateVgoModule(ForceOverLifetimeModule module)
        {
            return new VGO_PS_ForceOverLifetimeModule()
            {
                enabled = module.enabled,
                x = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.x),
                y = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.y),
                z = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.z),
                xMultiplier = module.xMultiplier,
                yMultiplier = module.yMultiplier,
                zMultiplier = module.zMultiplier,
                space = (NewtonVgo.ParticleSystemSimulationSpace)module.space,
                randomized = module.randomized,
            };
        }

        /// <summary>
        /// Create VGO_PS_ColorOverLifetimeModule from ColorOverLifetimeModule.
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        protected virtual VGO_PS_ColorOverLifetimeModule CreateVgoModule(ColorOverLifetimeModule module)
        {
            return new VGO_PS_ColorOverLifetimeModule()
            {
                enabled = module.enabled,
                color = VgoParticleSystemMinMaxGradientConverter.CreateFrom(module.color),
            };
        }

        /// <summary>
        /// Create VGO_PS_ColorBySpeedModule from ColorBySpeedModule.
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        protected virtual VGO_PS_ColorBySpeedModule CreateVgoModule(ColorBySpeedModule module)
        {
            return new VGO_PS_ColorBySpeedModule()
            {
                enabled = module.enabled,
                color = VgoParticleSystemMinMaxGradientConverter.CreateFrom(module.color),
                range = module.range.ToNumericsVector2(),
            };
        }

        /// <summary>
        /// Create VGO_PS_SizeOverLifetimeModule from SizeOverLifetimeModule.
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        protected virtual VGO_PS_SizeOverLifetimeModule CreateVgoModule(SizeOverLifetimeModule module)
        {
            if (module.separateAxes)
            {
                return new VGO_PS_SizeOverLifetimeModule()
                {
                    enabled = module.enabled,
                    separateAxes = module.separateAxes,
                    x = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.x),
                    y = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.y),
                    z = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.z),
                    xMultiplier = module.xMultiplier,
                    yMultiplier = module.yMultiplier,
                    zMultiplier = module.zMultiplier,
                };
            }
            else
            {
                return new VGO_PS_SizeOverLifetimeModule()
                {
                    enabled = module.enabled,
                    separateAxes = module.separateAxes,
                    x = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.size),
                    xMultiplier = module.sizeMultiplier,
                };
            }
        }

        /// <summary>
        /// Create VGO_PS_SizeBySpeedModule from SizeBySpeedModule.
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        protected virtual VGO_PS_SizeBySpeedModule CreateVgoModule(SizeBySpeedModule module)
        {
            if (module.separateAxes)
            {
                return new VGO_PS_SizeBySpeedModule()
                {
                    enabled = module.enabled,
                    separateAxes = module.separateAxes,
                    x = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.x),
                    y = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.y),
                    z = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.z),
                    xMultiplier = module.xMultiplier,
                    yMultiplier = module.yMultiplier,
                    zMultiplier = module.zMultiplier,
                    range = module.range.ToNumericsVector2(),
                };
            }
            else
            {
                return new VGO_PS_SizeBySpeedModule()
                {
                    enabled = module.enabled,
                    separateAxes = module.separateAxes,
                    x = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.size),
                    xMultiplier = module.sizeMultiplier,
                    range = module.range.ToNumericsVector2(),
                };
            }
        }

        /// <summary>
        /// Create VGO_PS_RotationOverLifetimeModule from RotationOverLifetimeModule.
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        protected virtual VGO_PS_RotationOverLifetimeModule CreateVgoModule(RotationOverLifetimeModule module)
        {
            if (module.separateAxes)
            {
                return new VGO_PS_RotationOverLifetimeModule()
                {
                    enabled = module.enabled,
                    separateAxes = module.separateAxes,
                    x = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.x),
                    y = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.y),
                    z = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.z),
                    xMultiplier = module.xMultiplier,
                    yMultiplier = module.yMultiplier,
                    zMultiplier = module.zMultiplier,
                };
            }
            else
            {
                return new VGO_PS_RotationOverLifetimeModule()
                {
                    enabled = module.enabled,
                    separateAxes = module.separateAxes,
                    x = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.x),
                    xMultiplier = module.xMultiplier,
                };
            }
        }

        /// <summary>
        /// Create VGO_PS_RotationBySpeedModule from RotationBySpeedModule.
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        protected virtual VGO_PS_RotationBySpeedModule CreateVgoModule(RotationBySpeedModule module)
        {
            if (module.separateAxes)
            {
                return new VGO_PS_RotationBySpeedModule()
                {
                    enabled = module.enabled,
                    separateAxes = module.separateAxes,
                    x = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.x),
                    y = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.y),
                    z = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.z),
                    xMultiplier = module.xMultiplier,
                    yMultiplier = module.yMultiplier,
                    zMultiplier = module.zMultiplier,
                    range = module.range.ToNumericsVector2(),
                };
            }
            else
            {
                return new VGO_PS_RotationBySpeedModule()
                {
                    enabled = module.enabled,
                    separateAxes = module.separateAxes,
                    x = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.x),
                    xMultiplier = module.xMultiplier,
                    range = module.range.ToNumericsVector2(),
                };
            }
        }

        /// <summary>
        /// Create VGO_PS_ExternalForcesModule from ExternalForcesModule.
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        protected virtual VGO_PS_ExternalForcesModule CreateVgoModule(ExternalForcesModule module)
        {
            return new VGO_PS_ExternalForcesModule()
            {
                enabled = module.enabled,
                multiplierCurve = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.multiplierCurve),
                multiplier = module.multiplier,
                influenceFilter = (NewtonVgo.ParticleSystemGameObjectFilter)module.influenceFilter,
                influenceMask = module.influenceMask.value,
            };
        }

        /// <summary>
        /// Create VGO_PS_NoiseModule from NoiseModule.
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        protected virtual VGO_PS_NoiseModule CreateVgoModule(NoiseModule module)
        {
            var vgoModule = new VGO_PS_NoiseModule()
            {
                enabled = module.enabled,
                separateAxes = module.separateAxes,
                frequency = module.frequency,
                scrollSpeed = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.scrollSpeed),
                scrollSpeedMultiplier = module.scrollSpeedMultiplier,
                damping = module.damping,
                octaveCount = module.octaveCount,
                octaveMultiplier = module.octaveMultiplier,
                octaveScale = module.octaveScale,
                quality = (NewtonVgo.ParticleSystemNoiseQuality)module.quality,
                remapEnabled = module.remapEnabled,
                positionAmount = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.positionAmount),
                rotationAmount = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.rotationAmount),
                sizeAmount = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.sizeAmount),
            };

            if (module.separateAxes)
            {
                vgoModule.strengthX = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.strengthX);
                vgoModule.strengthY = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.strengthY);
                vgoModule.strengthZ = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.strengthZ);
                vgoModule.strengthXMultiplier = module.strengthXMultiplier;
                vgoModule.strengthYMultiplier = module.strengthYMultiplier;
                vgoModule.strengthZMultiplier = module.strengthZMultiplier;

                if (module.remapEnabled)
                {
                    vgoModule.remapX = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.remapX);
                    vgoModule.remapY = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.remapY);
                    vgoModule.remapZ = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.remapZ);
                    vgoModule.remapXMultiplier = module.remapXMultiplier;
                    vgoModule.remapYMultiplier = module.remapYMultiplier;
                    vgoModule.remapZMultiplier = module.remapZMultiplier;
                }
            }
            else
            {
                vgoModule.strengthX = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.strengthX);
                vgoModule.strengthXMultiplier = module.strengthXMultiplier;

                if (module.remapEnabled)
                {
                    vgoModule.remapX = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.remapX);
                    vgoModule.remapXMultiplier = module.remapXMultiplier;
                }
            }

            return vgoModule;
        }

        /// <summary>
        /// Create VGO_PS_LightsModule from LightsModule.
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        protected virtual VGO_PS_LightsModule CreateVgoModule(LightsModule module)
        {
            return new VGO_PS_LightsModule()
            {
                enabled = module.enabled,
                ratio = module.ratio,
                useRandomDistribution = module.useRandomDistribution,
                light = VgoLightConverter.CreateFrom(module.light),
                useParticleColor = module.useParticleColor,
                sizeAffectsRange = module.sizeAffectsRange,
                alphaAffectsIntensity = module.alphaAffectsIntensity,
                range = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.range),
                rangeMultiplier = module.rangeMultiplier,
                intensity = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.intensity),
                intensityMultiplier = module.intensityMultiplier,
                maxLights = module.maxLights,
            };
        }

        /// <summary>
        /// Create VGO_PS_CollisionModule from CollisionModule.
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        protected virtual VGO_PS_CollisionModule CreateVgoModule(CollisionModule module)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create VGO_PS_TriggerModule from TriggerModule.
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        protected virtual VGO_PS_TriggerModule CreateVgoModule(TriggerModule module)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create VGO_PS_SubEmittersModule from SubEmittersModule.
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        protected virtual VGO_PS_SubEmittersModule CreateVgoModule(SubEmittersModule module)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create VGO_PS_TextureSheetAnimationModule from TextureSheetAnimationModule.
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        protected virtual VGO_PS_TextureSheetAnimationModule CreateVgoModule(TextureSheetAnimationModule module)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create VGO_PS_TrailModule from TrailModule.
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        protected virtual VGO_PS_TrailModule CreateVgoModule(TrailModule module)
        {
            return new VGO_PS_TrailModule()
            {
                enabled = module.enabled,
                mode = (NewtonVgo.ParticleSystemTrailMode)module.mode,
                ratio = module.ratio,
                lifetime = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.lifetime),
                lifetimeMultiplier = module.lifetimeMultiplier,
                minVertexDistance = module.minVertexDistance,
                worldSpace = module.worldSpace,
                dieWithParticles = module.dieWithParticles,
                ribbonCount = module.ribbonCount,
                splitSubEmitterRibbons = module.splitSubEmitterRibbons,
                attachRibbonsToTransform = module.attachRibbonsToTransform,
                textureMode = (NewtonVgo.ParticleSystemTrailTextureMode)module.textureMode,
                sizeAffectsWidth = module.sizeAffectsWidth,
                sizeAffectsLifetime = module.sizeAffectsLifetime,
                inheritParticleColor = module.inheritParticleColor,
                colorOverLifetime = VgoParticleSystemMinMaxGradientConverter.CreateFrom(module.colorOverLifetime),
                widthOverTrail = VgoParticleSystemMinMaxCurveConverter.CreateFrom(module.widthOverTrail),
                widthOverTrailMultiplier = module.widthOverTrailMultiplier,
                colorOverTrail = VgoParticleSystemMinMaxGradientConverter.CreateFrom(module.colorOverTrail),
                generateLightingData = module.generateLightingData,
                shadowBias = module.shadowBias,
            };
        }

        /// <summary>
        /// Create VGO_PS_CustomDataModule from CustomDataModule.
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        protected virtual VGO_PS_CustomDataModule CreateVgoModule(CustomDataModule module)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create VGO_PS_Renderer from ParticleSystemRenderer.
        /// </summary>
        /// <param name="particleSystemRenderer"></param>
        /// <param name="geometryCoordinate"></param>
        /// <param name="vgoLayout"></param>
        /// <returns></returns>
        protected virtual VGO_PS_Renderer CreateVgoPsRenderer(ParticleSystemRenderer particleSystemRenderer, VgoGeometryCoordinate geometryCoordinate, VgoLayout vgoLayout)
        {
            return new VGO_PS_Renderer()
            {
                enabled = particleSystemRenderer.enabled,
                renderMode = (NewtonVgo.ParticleSystemRenderMode)particleSystemRenderer.renderMode,
                cameraVelocityScale = particleSystemRenderer.cameraVelocityScale,
                velocityScale = particleSystemRenderer.velocityScale,
                lengthScale = particleSystemRenderer.lengthScale,
                normalDirection = particleSystemRenderer.normalDirection,
                sharedMaterial = GetMaterialIndex(particleSystemRenderer.sharedMaterial, vgoLayout),
                trailMaterialIndex = GetMaterialIndex(particleSystemRenderer.trailMaterial, vgoLayout),
                sortMode = (NewtonVgo.ParticleSystemSortMode)particleSystemRenderer.sortMode,
                sortingFudge = particleSystemRenderer.sortingFudge,
                minParticleSize = particleSystemRenderer.minParticleSize,
                maxParticleSize = particleSystemRenderer.maxParticleSize,
                alignment = (NewtonVgo.ParticleSystemRenderSpace)particleSystemRenderer.alignment,
                flip = particleSystemRenderer.flip.ToNumericsVector3(),
                allowRoll = particleSystemRenderer.allowRoll,
                pivot = particleSystemRenderer.pivot.ToNumericsVector3(),
                maskInteraction = (NewtonVgo.SpriteMaskInteraction)particleSystemRenderer.maskInteraction,
                enableGPUInstancing = particleSystemRenderer.enableGPUInstancing,
                shadowCastingMode = (ShadowCastingMode)particleSystemRenderer.shadowCastingMode,
                receiveShadows = particleSystemRenderer.receiveShadows,
                shadowBias = particleSystemRenderer.shadowBias,
                motionVectorGenerationMode = (NewtonVgo.MotionVectorGenerationMode)particleSystemRenderer.motionVectorGenerationMode,
                forceRenderingOff = particleSystemRenderer.forceRenderingOff,
                rendererPriority = particleSystemRenderer.rendererPriority,
                renderingLayerMask = particleSystemRenderer.renderingLayerMask,
                sortingLayerID = particleSystemRenderer.sortingLayerID,
                sortingOrder = particleSystemRenderer.sortingOrder,
                lightProbeUsage = (LightProbeUsage)particleSystemRenderer.lightProbeUsage,
                reflectionProbeUsage = (ReflectionProbeUsage)particleSystemRenderer.reflectionProbeUsage,
                probeAnchor = VgoTransformConverter.CreateFrom(particleSystemRenderer.probeAnchor, geometryCoordinate),
            };
        }

        #endregion

        #region Protected Methods (Utility)

        /// <summary>
        /// Get a material index.
        /// </summary>
        /// <param name="material"></param>
        /// <param name="vgoLayout"></param>
        /// <returns></returns>
        protected virtual int GetMaterialIndex(Material material, VgoLayout vgoLayout)
        {
            if (material == null)
            {
                return -1;
            }

            if (vgoLayout.materials == null)
            {
                return -1;
            }

            var vgoMaterial = vgoLayout.materials.FirstOrDefault(m => m.name.Equals(material.name));

            if (vgoMaterial == null)
            {
                return -1;
            }

            return vgoLayout.materials.IndexOf(vgoMaterial);
        }

        #endregion
    }
}