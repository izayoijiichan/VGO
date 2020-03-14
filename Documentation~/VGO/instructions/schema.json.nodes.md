# VGO nodes

## JSON schema of glTF

### glTF.nodes.[*].extensions

|definition name|description|type|
|:---|:---|:---|
|VGO_nodes|Node VGO information|VGO_nodes|

### VGO_nodes

|definition name|description|type|
|:---|:---|:---|
|gameObject|GameObject information|VGO_GameObject|
|colliders|Collider information|VGO_Collider[]|
|rigidbody|Rigid body information|VGO_Rigidbody|
|light|Light information|VGO_Light|
|particleSystem|Particle System information|VGO_ParticleSystem|
|right|VGO rights information|VGO_Right|
|skybox|VGO skybox information|VGO_Skybox|

### VGO_GameObject

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|isActive|Whether the GameObject is active.|bool|true / false|true|
|isStatic|Whether the GameObject is static.|bool|true / false|false|
|tag|Tag attached to GameGbject.|string||Untagged|
|layer|The layer on which the GameObject is located.|int|[0, 31]|0|

### VGO_Collider

|definition name|description|type|setting value|default value|Box|Capsule|Sphere|
|:---|:---|:---:|:---:|:---:|:---:|:---:|:---:|
|enabled|Whether the collider is enable.|bool|true / false|true|*|*|*|
|type|The type of collider.|string|Box / Capsule / Sphere||*|*|*|
|isTrigger|Whether the collider is a trigger.|bool|true / false||*|*|*|
|center|The center of the collider.（Unit is m）|float[3]|x, y, z||*|*|*|
|size|The total size of the collider.（Unit is m）|float[3]|x, y, z||*|-|-|
|radius|The radius of the collider.|float|[0, infinity]||-|*|*|
|height|The height of the collider.|float|[0, infinity]||-|*|-|
|direction|The direction of the collider.|int|0:X / 1:Y / 2:Z||-|*|-|
|physicMaterial|The physic material of this collider.|VGO_PhysicMaterial|||*|*|*|

### VGO_PhysicMaterial

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|dynamicFriction|Friction against a moving object.|float|[0.0, 1.0]||
|staticFriction|Friction used for objects that are stationary on a surface.|float|[0.0, 1.0]||
|bounciness|How elastic is the surface.|float|[0.0, 1.0]||
|frictionCombine|The type of friction handling between colliding objects.|string|Average / Minimum / Maximum / Multiply|Average|
|bounceCombine|Processing type for bounce between colliding objects.|string|Average / Minimum / Maximum / Multiply|Average|

### VGO_Rigidbody

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|mass|The mass of the object. (Unit is kg)|float|[0.0000001, 1000000000]||
|drag|The amount of air resistance that affects an object when it is moved by force.|float|[0.0, infinity]||
|angularDrag|The amount of air resistance that affects the object when rotating by torque.|float|[0.0, infinity]||
|useGravity|Whether the object is affected by gravity.|bool|true / false||
|isKinematic|Whether physics affects the rigid body.|bool|true / false||
|interpolation|The type of completion.|string|None / Interpolate / Extrapolate|None|
|collisionDetectionMode|Collision detection mode.|string|Discrete / Continuous / ContinuousDynamic / ContinuousSpeculative|Discrete|
|constraints|The flags that restricts the movement of the rigid body.|int|FreesePositionX(2) \| FreesePositionY(4) \| FreesePositionZ(8) \| FreeseRotationX(16) \| FreeseRotationY(32) \| FreeseRotationZ(64)|0|

### VGO_Light

|definition name|description|type|setting value|default value|Spot|Directional|Point|Rectangle|Disc|
|:---|:---|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|
|enabled|Whether the light is enable.|bool|true / false|true|*|*|*|*|*|
|type|The type of the light.|string|Spot / Directional / Point / Rectangle / Disc|Spot|*|*|*|*|*|
|shape|This property describes the shape of the spot light.|string|Cone / Pyramid / Box|Cone|*|-|-|-|-|
|range|The range of the light.|float|[0, infinity]||*|-|*|*|*|
|spotAngle|The angle of the light's spotlight cone in degrees.|float|[0, infinity]||*|-|-|-|-|
|areaSize|The size of the area light.|float[2]|x, y||-|-|-|*|-|
|areaRadius|The radius of the area light|float|[0, infinity]||-|-|-|-|*|
|color|The color of the light.|float[4]|r, g, b, a||*|*|*|*|*|
|lightmapBakeType|This property describes what part of a light's contribution can be baked.|string|Baked / Realtime / Mixed||*|*|*|*|*|
|intensity|The Intensity of a light is multiplied with the Light color.|float|[0, infinity]||*|*|*|*|*|
|bounceIntensity|The multiplier that defines the strength of the bounce lighting.|float|[0, infinity]||*|*|*|*|*|
|shadows|How this light casts shadows.|string|None / Hard / Soft|None|*|*|*|*|*|
|shadowRadius|Controls the amount of artificial softening applied to the edges of shadows cast by the Point or Spot light.|float|[0, infinity]||*|-|*|-|-|
|shadowAngle|Controls the amount of artificial softening applied to the edges of shadows cast by directional lights.|float|[0, infinity]||-|*|-|-|-|
|shadowStrength|Strength of light's shadows.|float|[0, infinity]||-|*|*|-|-|
|shadowResolution|The resolution of the shadow map.|string|FromQualitySettings / Low / Medium / High / VeryHigh|FromQualitySettings|-|*|*|-|-|
|shadowBias|Shadow mapping constant bias.|float|[0, infinity]||-|*|*|-|-|
|shadowNormalBias|Shadow mapping normal-based bias.|float|[0, infinity]||-|*|*|-|-|
|shadowNearPlane|Near plane value to use for shadow frustums.|float|[0, infinity]||-|*|*|-|-|
|renderMode|How to render the light.|string|Auto / ForcePixel / ForceVertex|Auto|*|*|*|*|*|
|cullingMask|This is used to light certain objects in the Scene selectively.|int|[-1, infinity]|-1 (Everything)|*|*|*|*|*|

Cookie, Flare, Halo are not supported.

### VGO_ParticleSystem

|definition name|description|type|remarks|
|:---|:---|:---|:---:|
|main||VGO_PS_MainModule||
|emission||VGO_PS_EmissionModule||
|shape||VGO_PS_ShapeModule||
|velocityOverLifetime||VGO_PS_VelocityOverLifetimeModule||
|limitVelocityOverLifetime||VGO_PS_LimitVelocityOverLifetimeModule||
|inheritVelocity||VGO_PS_InheritVelocityModule||
|forceOverLifetime||VGO_PS_ForceOverLifetimeModule||
|colorOverLifetime||VGO_PS_ColorOverLifetimeModule||
|colorBySpeed||VGO_PS_ColorBySpeedModule||
|sizeOverLifetime||VGO_PS_SizeOverLifetimeModule||
|sizeBySpeed||VGO_PS_SizeBySpeedModule||
|rotationOverLifetime||VGO_PS_RotationOverLifetimeModule||
|rotationBySpeed||VGO_PS_RotationBySpeedModule||
|externalForces||VGO_PS_ExternalForcesModule||
|noise||VGO_PS_NoiseModule||
|collision||VGO_PS_CollisionModule|unimplemented|
|trigger||VGO_PS_TriggerModule|unimplemented|
|subEmitters||VGO_PS_SubEmittersModule|unimplemented|
|textureSheetAnimation||VGO_PS_TextureSheetAnimationModule|unimplemented|
|lights||VGO_PS_LightsModule||
|trails||VGO_PS_TrailModule||
|customData||VGO_PS_CustomDataModule|unimplemented|
|renderer||VGO_PS_Renderer||

### VGO_PS_MainModule

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|duration||float|[0.05, infinity]||
|loop||float|[0, 1]||
|prewarm||bool|true / false||
|startDelay||VGO_PS_MinMaxCurve|||
|startDelayMultiplier||float|[0, infinity]||
|startLifetime||VGO_PS_MinMaxCurve|||
|startLifetimeMultiplier||float|[0, infinity]||
|startSpeed||VGO_PS_MinMaxCurve|||
|startSpeedMultiplier||float|[0, infinity]||
|startSize3D||bool|true / false||
|startSize||VGO_PS_MinMaxCurve|||
|startSizeX||VGO_PS_MinMaxCurve|||
|startSizeY||VGO_PS_MinMaxCurve|||
|startSizeZ||VGO_PS_MinMaxCurve|||
|startSizeMultiplier||float|[0, infinity]||
|startSizeXMultiplier||float|[0, infinity]||
|startSizeYMultiplier||float|[0, infinity]||
|startSizeZMultiplier||float|[0, infinity]||
|startRotation3D||bool|true / false||
|startRotation||VGO_PS_MinMaxCurve|||
|startRotationX||VGO_PS_MinMaxCurve|||
|startRotationY||VGO_PS_MinMaxCurve|||
|startRotationZ||VGO_PS_MinMaxCurve|||
|startRotationMultiplier||float|[0, infinity]||
|startRotationXMultiplier||float|[0, infinity]||
|startRotationYMultiplier||float|[0, infinity]||
|startRotationZMultiplier||float|[0, infinity]||
|flipRotation||float|[0, 1]||
|startColor||VGO_PS_MinMaxGradient|||
|gravityModifier||VGO_PS_MinMaxCurve|||
|gravityModifierMultiplier||float|[0, infinity]||
|simulationSpace||string|Local / World / Custom|Local|
|simulationSpeed||float|[0, infinity]||
|customSimulationSpace||VGO_Transform|||
|useUnscaledTime||bool|true / false||
|scalingMode||string|Hierarchy / Local / Shape|Hierarchy|
|playOnAwake||bool|true / false||
|emitterVelocityMode||string|Transform / Rigidbody|Transform|
|maxParticles||int|[0, infinity]||
|stopAction||string|None / Disable / Destroy / Callback|None|
|cullingMode||string|Automatic / PauseAndCatchup / Pause / AlwaysSimulate|Automatic|
|ringBufferMode||string|Disabled / PauseUntilReplaced / LoopUntilReplaced|Disabled|
|ringBufferLoopRange||float[2]|x, y||

### VGO_PS_EmissionModule

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|enabled||bool|true / false||
|rateOverTime||VGO_PS_MinMaxCurve|||
|rateOverTimeMultiplier||float|[0, infinity]||
|rateOverDistance||VGO_PS_MinMaxCurve|||
|rateOverDistanceMultiplier||float|[0, infinity]||
|bursts||VGO_PS_Burst[]|||

### VGO_PS_Burst

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|time||float|[0, infinity]||
|count||VGO_PS_MinMaxCurve|||
|cycleCount||int|[0, infinity]||
|repeatInterval||float|[0.01, infinity]||
|probability||float|[0, 1]||

### VGO_PS_ShapeModule

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|enabled||bool|true / false||
|shapeType||string|Sphere / SphereShell / Hemisphere / HemisphereShell / Cone / Box / Mesh / ConeVolume / ConeVolumeShell / Circle / CircleEdge / SingleSidedEdge / MeshRenderer / SkinnedMeshRenderer / BoxShell / BoxEdge / Donut / Rectangle / Sprite / SpriteRenderer|Sphere|
|angle||float|[0, 90]||
|radius||float|[0.0001, infinity]||
|donutRadius||float|[0.0001, infinity]||
|radiusMode||string|Random / Loop / PingPong / BurstSpread|Random|
|radiusSpread||float|[0, 1]||
|radiusSpeed||VGO_PS_MinMaxCurve|||
|radiusSpeedMultiplier||float|[0, infinity]||
|radiusThickness||float|[0, 1]||
|boxThickness||float[3]|x, y, z||
|arc||float|[0, 360]||
|arcMode||string|Random / Loop / PingPong / BurstSpread|Random|
|arcSpread||float|||
|arcSpeed||VGO_PS_MinMaxCurve|||
|arcSpeedMultiplier||float|[0, infinity]||
|length||float|||
|meshShapeType||string|Vertex / Edge / Triangle|Vertex|
|meshSpawnMode||string|Random / Loop / PingPong / BurstSpread|Random|
|meshSpawnSpread||float|||
|meshSpawnSpeed||VGO_PS_MinMaxCurve|||
|meshSpawnSpeedMultiplier||float|[0, infinity]||
|useMeshMaterialIndex||bool|true / false||
|meshMaterialIndex||int|||
|useMeshColors||bool|true / false||
|normalOffset||float|||
|textureIndex||int|||
|textureClipChannel||string|Red / Green / Blue / Alpha|Red|
|textureClipThreshold||float|||
|textureColorAffectsParticles||bool|true / false||
|textureAlphaAffectsParticles||bool|true / false||
|textureBilinearFiltering||bool|true / false||
|textureUVChannel||int|||
|position||float[3]|x, y, z||
|rotation||float[4]|x, y, z, w||
|scale||float[3]|x, y, z||
|alignToDirection||bool|true / false||
|randomDirectionAmount||float|[0, 1]||
|sphericalDirectionAmount||float|[0, 1]||
|randomPositionAmount||float|[0, 1]||

### VGO_PS_VelocityOverLifetimeModule

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|enabled||bool|true / false||
|x||VGO_PS_MinMaxCurve|||
|y||VGO_PS_MinMaxCurve|||
|z||VGO_PS_MinMaxCurve|||
|xMultiplier||float|[0, infinity]||
|yMultiplier||float|[0, infinity]||
|zMultiplier||float|[0, infinity]||
|space||string|Local / World / Custom|Local|
|orbitalX||VGO_PS_MinMaxCurve|||
|orbitalY||VGO_PS_MinMaxCurve|||
|orbitalZ||VGO_PS_MinMaxCurve|||
|orbitalXMultiplier||float|[0, infinity]||
|orbitalYMultiplier||float|[0, infinity]||
|orbitalZMultiplier||float|[0, infinity]||
|orbitalOffsetX||VGO_PS_MinMaxCurve|||
|orbitalOffsetY||VGO_PS_MinMaxCurve|||
|orbitalOffsetZ||VGO_PS_MinMaxCurve|||
|orbitalOffsetXMultiplier||float|[0, infinity]||
|orbitalOffsetYMultiplier||float|[0, infinity]||
|orbitalOffsetZMultiplier||float|[0, infinity]||
|radial||VGO_PS_MinMaxCurve|||
|radialMultiplier||float|[0, infinity]||
|speedModifier||VGO_PS_MinMaxCurve|||
|speedModifierMultiplier||float|[0, infinity]||

### VGO_PS_LimitVelocityOverLifetimeModule

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|enabled||bool|true / false||
|separateAxes||bool|true / false||
|limitX||VGO_PS_MinMaxCurve|||
|limitY||VGO_PS_MinMaxCurve|||
|limitZ||VGO_PS_MinMaxCurve|||
|limitXMultiplier||float|[0, infinity]||
|limitYMultiplier||float|[0, infinity]||
|limitZMultiplier||float|[0, infinity]||
|space||string|Local / World / Custom|Local|
|dampen||float|[0, 1]||
|drag||VGO_PS_MinMaxCurve|||
|dragMultiplier||float|[0, infinity]||
|multiplyDragByParticleSize||bool|true / false||
|multiplyDragByParticleVelocity||bool|true / false||

### VGO_PS_InheritVelocityModule

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|enabled||bool|true / false||
|mode||string|Initial / Current|Initial|
|curve||VGO_PS_MinMaxCurve|||
|curveMultiplier||float|[0, infinity]||

### VGO_PS_ForceOverLifetimeModule

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|enabled||bool|true / false||
|x||VGO_PS_MinMaxCurve|||
|y||VGO_PS_MinMaxCurve|||
|z||VGO_PS_MinMaxCurve|||
|xMultiplier||float|[0, infinity]||
|yMultiplier||float|[0, infinity]||
|zMultiplier||float|[0, infinity]||
|space||string|Local / World / Custom|Local|
|randomized||bool|true / false||

### VGO_PS_ColorOverLifetimeModule

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|enabled||bool|true / false||
|color||VGO_PS_MinMaxGradient|||

### VGO_PS_ColorBySpeedModule

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|enabled||bool|true / false||
|color||VGO_PS_MinMaxGradient|||
|range||float[]|x, y||

### VGO_PS_SizeOverLifetimeModule

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|enabled||bool|true / false||
|separateAxes||bool|true / false||
|x||VGO_PS_MinMaxCurve|||
|y||VGO_PS_MinMaxCurve|||
|z||VGO_PS_MinMaxCurve|||
|xMultiplier||float|[0, infinity]||
|yMultiplier||float|[0, infinity]||
|zMultiplier||float|[0, infinity]||

### VGO_PS_SizeBySpeedModule

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|enabled||bool|true / false||
|separateAxes||bool|true / false||
|x||VGO_PS_MinMaxCurve|||
|y||VGO_PS_MinMaxCurve|||
|z||VGO_PS_MinMaxCurve|||
|xMultiplier||float|[0, infinity]||
|yMultiplier||float|[0, infinity]||
|zMultiplier||float|[0, infinity]||
|range||float[]|x, y||

### VGO_PS_RotationOverLifetimeModule

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|enabled||bool|true / false||
|separateAxes||bool|true / false||
|x||VGO_PS_MinMaxCurve|||
|y||VGO_PS_MinMaxCurve|||
|z||VGO_PS_MinMaxCurve|||
|xMultiplier||float|[0, infinity]||
|yMultiplier||float|[0, infinity]||
|zMultiplier||float|[0, infinity]||

### VGO_PS_RotationBySpeedModule

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|enabled||bool|true / false||
|separateAxes||bool|true / false||
|x||VGO_PS_MinMaxCurve|||
|y||VGO_PS_MinMaxCurve|||
|z||VGO_PS_MinMaxCurve|||
|xMultiplier||float|[0, infinity]||
|yMultiplier||float|[0, infinity]||
|zMultiplier||float|[0, infinity]||
|range||float[]|x, y||

### VGO_PS_ExternalForcesModule

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|enabled||bool|true / false||
|multiplierCurve||VGO_PS_MinMaxCurve|||
|Multiplier||float|[0, infinity]||
|influenceFilter||string|LayerMask / List / LayerMaskAndList|LayerMask|
|influenceMask||int|||

### VGO_PS_NoiseModule

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|enabled||bool|true / false||
|separateAxes||bool|true / false||
|strengthX||VGO_PS_MinMaxCurve|||
|strengthY||VGO_PS_MinMaxCurve|||
|strengthZ||VGO_PS_MinMaxCurve|||
|strengthXMultiplier||float|[0, infinity]||
|strengthYMultiplier||float|[0, infinity]||
|strengthZMultiplier||float|[0, infinity]||
|frequency||float|[0.0001, infinity]||
|scrollSpeed||VGO_PS_MinMaxCurve|||
|scrollSpeedMultiplier||float|[0, infinity]||
|damping||bool|true / false||
|octaveCount||int|[1, 4]||
|octaveMultiplier||float|[0, infinity]||
|octaveScale||float|||
|quality||string|Low / Medium / High|Low|
|remapEnabled||bool|true / false||
|remapX||VGO_PS_MinMaxCurve|||
|remapY||VGO_PS_MinMaxCurve|||
|remapZ||VGO_PS_MinMaxCurve|||
|remapXMultiplier||float|[0, infinity]||
|remapYMultiplier||float|[0, infinity]||
|remapZMultiplier||float|[0, infinity]||
|positionAmount||VGO_PS_MinMaxCurve|||
|rotationAmount||VGO_PS_MinMaxCurve|||
|sizeAmount||VGO_PS_MinMaxCurve|||

### VGO_PS_CollisionModule

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|

unimplemented

### VGO_PS_TriggerModule

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|

unimplemented

### VGO_PS_SubEmittersModule

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|

unimplemented

### VGO_PS_TextureSheetAnimationModule

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|

unimplemented

### VGO_PS_LightsModule

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|enabled||bool|true / false||
|ratio||float|[0, 1]||
|useRandomDistribution||bool|true / false||
|light||VGO_Light|||
|useParticleColor||bool|true / false||
|sizeAffectsRange||bool|true / false||
|alphaAffectsIntensity||bool|true / false||
|range||VGO_PS_MinMaxCurve|||
|rangeMultiplier||float|[0, infinity]||
|intensity||VGO_PS_MinMaxCurve|||
|intensityMultiplier||float|[0, infinity]||
|maxLights||int|[0, infinity]||

### VGO_PS_TrailModule

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|enabled||bool|true / false||
|mode||string|PerParticle / Ribbon|PerParticle|
|ratio||float|[0, 1]||
|lifetime||VGO_PS_MinMaxCurve|||
|lifetimeMultiplier||float|[0, infinity]||
|minVertexDistance||float|[0, infinity]||
|worldSpace||bool|true / false||
|dieWithParticles||bool|true / false||
|ribbonCount||int|[1, infinity]||
|splitSubEmitterRibbons||bool|true / false||
|attachRibbonsToTransform||bool|true / false||
|textureMode||string|Stretch / Tile / DistributePerSegment / RepeatPerSegment|Stretch|
|sizeAffectsWidth||bool|true / false||
|sizeAffectsLifetime||bool|true / false||
|useMeinheritParticleColorshColors||bool|true / false||
|colorOverLifetime||VGO_PS_MinMaxCurve|||
|widthOverTrail||VGO_PS_MinMaxCurve|||
|widthOverTrailMultiplier||float|[0, infinity]||
|colorOverTrail||VGO_PS_MinMaxGradient|||
|generateLightingData||bool|true / false||
|shadowBias||float|[0, infinity]||

### VGO_PS_CustomDataModule

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|

unimplemented

### VGO_PS_Renderer

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|enabled||bool|true / false||
|renderMode||string|Billboard / Stretch / HorizontalBillboard / VerticalBillboard / Mesh / None|Billboard|
|cameraVelocityScale||float|||
|velocityScale||float|||
|lengthScale||float|||
|normalDirection||float|[0, 1]||
|sharedMaterialIndex||int|||
|trailMaterialIndex||int|||
|sortMode||string|None / Distance / OldestInFront / YoungestInFront|None|
|sortingFudge||float|||
|minParticleSize||float|[0, infinity]||
|maxParticleSize||float|[0, infinity]||
|alignment||string|View / World / Local / Facing / Velocity|View|
|flip||float[]|x, y, z||
|allowRoll||bool|true / false||
|pivot||float[]|x, y, z||
|maskInteraction||string|None / VisibleInsideMask/ VisibleOutsideMask|None|
|enableGPUInstancing||bool|true / false||
|shadowCastingMode||string|Off / On / TwoSided / ShadowsOnly|Off|
|receiveShadows||bool|true / false||
|shadowBias||float|||
|motionVectorGenerationMode||string|Camera / Object / ForceNoMotion|Camera|
|forceRenderingOff||bool|true / false||
|rendererPriority||int|||
|renderingLayerMask||int|||
|sortingLayerID||int|||
|sortingOrder||int|||
|lightProbeUsage||string|Off / BlendProbes / UseProxyVolume / CustomProvided|Off|
|reflectionProbeUsage||string|Off / BlendProbes / BlendProbesAndSkybox / Simple|Off|
|probeAnchor||VGO_Transform|||

### VGO_PS_MinMaxCurve

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|mode||string|Constant / Curve / TwoCurves / TwoConstants|Constant|
|constant||float|||
|constantMin||float|||
|constantMax||float|||
|curveMultiplier||float|[0, infinity]||
|curve||VGO_AnimationCurve|||
|curveMin||VGO_AnimationCurve|||
|curveMax||VGO_AnimationCurve|||

### VGO_AnimationCurve

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|keys||VGO_Keyframe[]|||
|preWrapMode||string|Once / Loop / PingPong / Default / ClampForever|Once|
|postWrapMode||string|Once / Loop / PingPong / Default / ClampForever|Once|

### VGO_Keyframe

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|time||float|||
|value||float|||
|inTangent||float|||
|outTangent||float|||
|inWeight||float|||
|outWeight||float|||
|weightedMode||string|None / In / Out / Both|None|

### VGO_PS_MinMaxGradient

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|mode||string|Color / Gradient / TwoColors / TwoGradients / RandomColor|Color|
|color||float[]|r, g, b, a||
|colorMin||float[]|r, g, b, a||
|colorMax||float[]|r, g, b, a||
|gradient||VGO_Gradient|||
|gradientMin||VGO_Gradient|||
|gradientMax||VGO_Gradient|||

### VGO_Gradient

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|colorKeys||VGO_GradientColorKey[]|||
|alphaKeys||VGO_GradientAlphaKey[]|||
|mode||string|Blend / Fixed|Blend|

### VGO_GradientColorKey

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|color||float[]|r, g, b, a||
|time||float|[0,1]||

### VGO_GradientAlphaKey

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|alpha||float|||
|time||float|[0,1]||

### VGO_Skybox

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|materialIndex||int|||

___
## Example of glTF JSON structure

### glTF.nodes.[*].extensions
```json
JSON{
    "nodes": [
        {
            "name": "Capsule1",
            "translation": [ 1, 1, 1 ],
            "rotation": [ 0, 0, 0, 1 ],
            "scale": [ 0.5, 0.5, 0.5 ],
            "mesh": 0,
            "extensions": {
                "VGO_nodes": {
                    "gameObject": {
                        "isActive": false,
                        "isStatic": true,
                        "tag": "Player",
                        "layer": 2
                    },
                    "colliders": [
                        {
                            "enabled": false,
                            "type": "Capsule",
                            "isTrigger": false,
                            "center": [ 0, 0, 0 ],
                            "radius": 0.5,
                            "height": 2,
                            "direction": 1,
                            "physicMaterial":{
                                "dynamicFriction":0.6,
                                "staticFriction":0.6,
                                "bounciness":0.0,
                                "frictionCombine":"Average",
                                "bounceCombine":"Multiply"
                            }
                        }
                    ],
                    "rigidbody": {
                        "mass": 10,
                        "drag": 0,
                        "angularDrag": 0.05,
                        "useGravity": true,
                        "isKinematic": false,
                        "interpolation": "None",
                        "collisionDetectionMode": "Discrete",
                        "constraints": 36
                    },
                    "light":{
                        "enabled":true,
                        "type":"Point",
                        "shape":"",
                        "range":1.0,
                        "spotAngle":0.0,
                        "areaSize":[ 0.0, 0.0 ],
                        "areaRadius":0.0,
                        "color":[ 0.122,0.404,0.637,1.0 ],
                        "lightmapBakeType":"Realtime",
                        "intensity":1.0,
                        "bounceIntensity":1.0,
                        "shadows":"Soft",
                        "shadowRadius":1.0,
                        "shadowAngle":0.0,
                        "shadowStrength":1.0,
                        "shadowResolution":"Low",
                        "shadowBias":0.004,
                        "shadowNormalBias":0.26,
                        "shadowNearPlane":0.1,
                        "renderMode":"Auto",
                        "cullingMask":-1
                    },
                    "particleSystem":{
                        "main":{
                            "duration":5.0,
                            "loop":true,
                            "prewarm":false,
                            "startDelay":{
                                "mode":"Constant",
                                "constant":0.0
                            },
                            "startDelayMultiplier":0.0,
                            "startLifetime":{
                                "mode":"Curve",
                                "curveMultiplier":5.0,
                                "curve":{
                                    "keys":[
                                        {
                                            "time":0.0,
                                            "value":0.510416031,
                                            "inTangent":0.0,
                                            "outTangent":0.0,
                                            "inWeight":0.333333343,
                                            "outWeight":0.333333343,
                                            "weightedMode":"None"
                                        },
                                        {
                                            "time":1.0,
                                            "value":1.0,
                                            "inTangent":0.0,
                                            "outTangent":0.0,
                                            "inWeight":0.333333343,
                                            "outWeight":0.333333343,
                                            "weightedMode":"None"
                                        }
                                    ],
                                    "preWrapMode":"ClampForever",
                                    "postWrapMode":"ClampForever"
                                }
                            },
                            "startLifetimeMultiplier":5.0,
                            "startSpeed":{
                                "mode":"Constant",
                                "constant":1.0
                            },
                            "startSpeedMultiplier":1.0,
                            "startSize3D":false,
                            "startSize":{
                                "mode":"Constant",
                                "constant":0.08
                            },
                            "startSizeMultiplier":0.08,
                            "startRotation3D":true,
                            "startRotationX":{
                                "mode":"Constant",
                                "constant":0.0174532924
                            },
                            "startRotationY":{
                                "mode":"Constant",
                                "constant":0.0349065848
                            },
                            "startRotationZ":{
                                "mode":"Constant",
                                "constant":0.0523598753
                            },
                            "startRotationXMultiplier":0.0174532924,
                            "startRotationYMultiplier":0.0349065848,
                            "startRotationZMultiplier":0.0523598753,
                            "flipRotation":0.0,
                            "startColor":{
                                "mode":"Color",
                                "color":[
                                    0.743349731,0.534801245,0.0394535959,1.0
                                ]
                            },
                            "gravityModifier":{
                                "mode":"Constant",
                                "constant":0.0
                            },
                            "gravityModifierMultiplier":0.0,
                            "simulationSpace":"Local",
                            "simulationSpeed":1.0,
                            "useUnscaledTime":false,
                            "scalingMode":"Local",
                            "playOnAwake":true,
                            "emitterVelocityMode":"Rigidbody",
                            "maxParticles":1000,
                            "stopAction":"None",
                            "cullingMode":"Automatic",
                            "ringBufferMode":"Disabled"
                        },
                        "emission":{
                            "enabled":true,
                            "rateOverTime":{
                                "mode":"Constant",
                                "constant":10.0
                            },
                            "rateOverTimeMultiplier":10.0,
                            "rateOverDistance":{
                                "mode":"Constant",
                                "constant":0.0
                            },
                            "rateOverDistanceMultiplier":0.0,
                            "bursts":[
                                {
                                    "time":0.0,
                                    "count":{
                                        "mode":"Constant",
                                        "constant":3.0
                                    },
                                    "cycleCount":2,
                                    "repeatInterval":0.01,
                                    "probability":1.0
                                },
                                {
                                    "time":1.0,
                                    "count":{
                                        "mode":"TwoConstants",
                                        "constantMin":1.0,
                                        "constantMax":2.0
                                    },
                                    "cycleCount":10,
                                    "repeatInterval":1.0,
                                    "probability":0.5
                                },
                                {
                                    "time":2.0,
                                    "count":{
                                        "mode":"Curve",
                                        "curveMultiplier":30.0,
                                        "curve":{
                                            "keys":[
                                                {
                                                    "time":0.0,
                                                    "value":0.364583969,
                                                    "inTangent":0.0,
                                                    "outTangent":0.0,
                                                    "inWeight":0.0,
                                                    "outWeight":0.0,
                                                    "weightedMode":"None"
                                                },
                                                {
                                                    "time":0.9888916,
                                                    "value":0.552083969,
                                                    "inTangent":0.0,
                                                    "outTangent":0.0,
                                                    "inWeight":0.0,
                                                    "outWeight":0.0,
                                                    "weightedMode":"None"
                                                }
                                            ],
                                            "preWrapMode":"ClampForever",
                                            "postWrapMode":"ClampForever"
                                        }
                                    },
                                    "cycleCount":5,
                                    "repeatInterval":5.0,
                                    "probability":1.0
                                }
                            ]
                        },
                        "shape":{
                            "enabled":true,
                            "shapeType":"Cone",
                            "angle":25.0,
                            "radius":1.0,
                            "donutRadius":0.2,
                            "radiusMode":"Random",
                            "radiusSpread":0.0,
                            "radiusSpeed":{
                                "mode":"Constant",
                                "constant":1.0
                            },
                            "radiusSpeedMultiplier":1.0,
                            "radiusThickness":1.0,
                            "boxThickness":[ 0.0,0.0,0.0 ],
                            "arc":360.0,
                            "arcMode":"Random",
                            "arcSpread":0.0,
                            "arcSpeed":{
                                "mode":"Constant",
                                "constant":1.0
                            },
                            "arcSpeedMultiplier":1.0,
                            "length":5.0,
                            "meshShapeType":"Edge",
                            "meshSpawnMode":"Random",
                            "meshSpawnSpread":0.0,
                            "meshSpawnSpeed":{
                                "mode":"Constant",
                                "constant":0.0
                            },
                            "meshSpawnSpeedMultiplier":1.0,
                            "useMeshMaterialIndex":false,
                            "meshMaterialIndex":0,
                            "useMeshColors":true,
                            "normalOffset":0.0,
                            "textureClipChannel":"Alpha",
                            "textureClipThreshold":0.0,
                            "textureColorAffectsParticles":true,
                            "textureAlphaAffectsParticles":true,
                            "textureBilinearFiltering":false,
                            "textureUVChannel":0,
                            "position":[ 0.0,0.0,0.0 ],
                            "rotation":[ 0.0,0.0,0.0 ],
                            "scale":[ 1.0,1.0,1.0 ],
                            "alignToDirection":false,
                            "randomDirectionAmount":0.0,
                            "sphericalDirectionAmount":0.0,
                            "randomPositionAmount":0.0
                        },
                        "velocityOverLifetime":{
                            "enabled":true,
                            "x":{
                                "mode":"Constant",
                                "constant":0.0
                            },
                            "y":{
                                "mode":"Constant",
                                "constant":0.0
                            },
                            "z":{
                                "mode":"Constant",
                                "constant":0.0
                            },
                            "xMultiplier":0.0,
                            "yMultiplier":0.0,
                            "zMultiplier":0.0,
                            "space":"Local",
                            "orbitalX":{
                                "mode":"Constant",
                                "constant":0.0
                            },
                            "orbitalY":{
                                "mode":"Constant",
                                "constant":0.0
                            },
                            "orbitalZ":{
                                "mode":"Constant",
                                "constant":0.0
                            },
                            "orbitalXMultiplier":0.0,
                            "orbitalYMultiplier":0.0,
                            "orbitalZMultiplier":0.0,
                            "orbitalOffsetX":{
                                "mode":"Constant",
                                "constant":0.0
                            },
                            "orbitalOffsetY":{
                                "mode":"Constant",
                                "constant":0.0
                            },
                            "orbitalOffsetZ":{
                                "mode":"Constant",
                                "constant":0.0
                            },
                            "orbitalOffsetXMultiplier":0.0,
                            "orbitalOffsetYMultiplier":0.0,
                            "orbitalOffsetZMultiplier":0.0,
                            "radial":{
                                "mode":"Constant",
                                "constant":0.0
                            },
                            "radialMultiplier":0.0,
                            "speedModifier":{
                                "mode":"Constant",
                                "constant":1.2
                            },
                            "speedModifierMultiplier":1.2
                        },
                        "limitVelocityOverLifetime":{
                            "enabled":true,
                            "separateAxes":false,
                            "limitX":{
                                "mode":"Constant",
                                "constant":1.0
                            },
                            "limitXMultiplier":1.0,
                            "space":"Local",
                            "dampen":0.0,
                            "drag":{
                                "mode":"Constant",
                                "constant":0.0
                            },
                            "dragMultiplier":0.0,
                            "multiplyDragByParticleSize":true,
                            "multiplyDragByParticleVelocity":true
                        },
                        "inheritVelocity":{
                            "enabled":true,
                            "mode":"Initial",
                            "curve":{
                                "mode":"Constant",
                                "constant":0.0
                            },
                            "curveMultiplier":0.0
                        },
                        "forceOverLifetime":{
                            "enabled":true,
                            "x":{
                                "mode":"Constant",
                                "constant":0.0
                            },
                            "y":{
                                "mode":"Constant",
                                "constant":0.0
                            },
                            "z":{
                                "mode":"Constant",
                                "constant":0.0
                            },
                            "xMultiplier":0.0,
                            "yMultiplier":0.0,
                            "zMultiplier":0.0,
                            "space":"Local",
                            "randomized":false
                        },
                        "colorOverLifetime":{
                            "enabled":true,
                            "color":{
                                "mode":"Gradient",
                                "gradient":{
                                    "colorKeys":[
                                        {
                                            "color":[ 1.0,1.0,1.0,1.0 ],
                                            "time":0.0
                                        },
                                        {
                                            "color":[ 0.107,0.836,0.207,1.0 ],
                                            "time":0.144121468
                                        },
                                        {
                                            "color":[ 0.7433,0.0423,0.525,1.0 ],
                                            "time":0.8529488
                                        }
                                    ],
                                    "alphaKeys":[
                                        {
                                            "alpha":1.0,
                                            "time":0.0
                                        },
                                        {
                                            "alpha":1.0,
                                            "time":1.0
                                        }
                                    ],
                                    "mode":"Blend"
                                }
                            }
                        },
                        "colorBySpeed":{
                            "enabled":true,
                            "color":{
                                "mode":"Gradient",
                                "gradient":{
                                    "colorKeys":[
                                        {
                                            "color":[ 1.0,1.0,1.0,1.0 ],
                                            "time":0.0
                                        },
                                        {
                                            "color":[ 1.0,1.0,1.0,1.0 ],
                                            "time":1.0
                                        }
                                    ],
                                    "alphaKeys":[
                                        {
                                            "alpha":1.0,
                                            "time":0.0
                                        },
                                        {
                                            "alpha":1.0,
                                            "time":1.0
                                        }
                                    ],
                                    "mode":"Blend"
                                }
                            },
                            "range":[ 0.0,1.0 ]
                        },
                        "sizeOverLifetime":{
                            "enabled":true,
                            "separateAxes":false,
                            "x":{
                                "mode":"Curve",
                                "curveMultiplier":1.0,
                                "curve":{
                                    "keys":[
                                        {
                                            "time":0.0,
                                            "value":1.0,
                                            "inTangent":0.0,
                                            "outTangent":0.0,
                                            "inWeight":0.333333343,
                                            "outWeight":0.333333343,
                                            "weightedMode":"None"
                                        },
                                        {
                                            "time":1.0,
                                            "value":0.354166657,
                                            "inTangent":-0.9374998,
                                            "outTangent":-0.9374998,
                                            "inWeight":0.144444466,
                                            "outWeight":0.333333343,
                                            "weightedMode":"None"
                                        }
                                    ],
                                    "preWrapMode":"ClampForever",
                                    "postWrapMode":"ClampForever"
                                }
                            },
                            "xMultiplier":1.0
                        },
                        "sizeBySpeed":{
                            "enabled":true,
                            "separateAxes":false,
                            "x":{
                                "mode":"Curve",
                                "curveMultiplier":1.0,
                                "curve":{
                                    "keys":[
                                        {
                                            "time":0.0,
                                            "value":0.0,
                                            "inTangent":0.0,
                                            "outTangent":1.0,
                                            "inWeight":0.333333343,
                                            "outWeight":0.333333343,
                                            "weightedMode":"None"
                                        },
                                        {
                                            "time":1.0,
                                            "value":1.0,
                                            "inTangent":1.0,
                                            "outTangent":0.0,
                                            "inWeight":0.333333343,
                                            "outWeight":0.333333343,
                                            "weightedMode":"None"
                                        }
                                    ],
                                    "preWrapMode":"ClampForever",
                                    "postWrapMode":"ClampForever"
                                }
                            },
                            "xMultiplier":1.0,
                            "range":[ 0.0,1.0 ]
                        },
                        "rotationOverLifetime":{
                            "enabled":true,
                            "x":{
                                "mode":"Constant",
                                "constant":0.0
                            },
                            "xMultiplier":0.0,
                            "yMultiplier":0.0,
                            "zMultiplier":0.0,
                            "separateAxes":false
                        },
                        "rotationBySpeed":{
                            "enabled":true,
                            "x":{
                                "mode":"Constant",
                                "constant":0.0
                            },
                            "xMultiplier":0.0,
                            "yMultiplier":0.0,
                            "zMultiplier":0.0,
                            "separateAxes":false,
                            "range":[ 0.0,1.0 ]
                        },
                        "externalForces":{
                            "enabled":true,
                            "multiplierCurve":{
                                "mode":"Constant",
                                "constant":1.0
                            },
                            "multiplier":1.0,
                            "influenceFilter":"LayerMask",
                            "influenceMask":-1
                        },
                        "noise":{
                            "enabled":true,
                            "separateAxes":false,
                            "strengthX":{
                                "mode":"Constant",
                                "constant":0.2
                            },
                            "strengthXMultiplier":0.2,
                            "frequency":0.5,
                            "scrollSpeed":{
                                "mode":"Constant",
                                "constant":0.0
                            },
                            "scrollSpeedMultiplier":0.0,
                            "damping":true,
                            "octaveCount":1,
                            "octaveMultiplier":0.5,
                            "octaveScale":2.0,
                            "quality":"High",
                            "remapEnabled":false,
                            "positionAmount":{
                                "mode":"Constant",
                                "constant":1.0
                            },
                            "rotationAmount":{
                                "mode":"Constant",
                                "constant":0.0
                            },
                            "sizeAmount":{
                                "mode":"Constant",
                                "constant":0.0
                            }
                        },
                        "lights":{
                            "enabled":true,
                            "ratio":0.5,
                            "useRandomDistribution":true,
                            "light":{
                                "type":"Point",
                                "range":0.1,
                                "color":[ 0.876,0.192,0.73,1.0 ],
                                "lightmapBakeType":"Realtime",
                                "intensity":3.0,
                                "bounceIntensity":0.0,
                                "shadowStrength":1.0,
                                "shadowBias":0.05,
                                "shadowNormalBias":0.4,
                                "shadowNearPlane":0.2
                            },
                            "useParticleColor":true,
                            "sizeAffectsRange":true,
                            "alphaAffectsIntensity":false,
                            "range":{
                                "mode":"Constant",
                                "constant":1.0
                            },
                            "rangeMultiplier":1.0,
                            "intensity":{
                                "mode":"Constant",
                                "constant":1.0
                            },
                            "intensityMultiplier":1.0,
                            "maxLights":20
                        },
                        "trails":{
                            "enabled":true,
                            "mode":"PerParticle",
                            "ratio":1.0,
                            "lifetime":{
                                "mode":"Constant",
                                "constant":0.3
                            },
                            "lifetimeMultiplier":0.3,
                            "minVertexDistance":0.2,
                            "worldSpace":false,
                            "dieWithParticles":true,
                            "ribbonCount":1,
                            "splitSubEmitterRibbons":false,
                            "attachRibbonsToTransform":false,
                            "textureMode":"Stretch",
                            "sizeAffectsWidth":true,
                            "sizeAffectsLifetime":false,
                            "useMeinheritParticleColorshColors":true,
                            "colorOverLifetime":{
                                "mode":"Color",
                                "color":[ 1.0,1.0,1.0,1.0 ]
                            },
                            "widthOverTrail":{
                                "mode":"Constant",
                                "constant":0.4
                            },
                            "widthOverTrailMultiplier":0.4,
                            "colorOverTrail":{
                                "mode":"Color",
                                "color":[ 1.0,1.0,1.0,1.0 ]
                            },
                            "generateLightingData":false,
                            "shadowBias":0.5
                        },
                        "renderer":{
                            "enabled":true,
                            "renderMode":"Billboard",
                            "cameraVelocityScale":0.0,
                            "velocityScale":0.0,
                            "lengthScale":2.0,
                            "normalDirection":1.0,
                            "sharedMaterialIndex":3,
                            "trailMaterialIndex":4,
                            "sortMode":"None",
                            "sortingFudge":0.0,
                            "minParticleSize":0.0,
                            "maxParticleSize":0.5,
                            "alignment":"View",
                            "flip":[ 0.0,0.0,0.0 ],
                            "allowRoll":true,
                            "pivot":[ 0.0,0.0,0.0 ],
                            "maskInteraction":"None",
                            "enableGPUInstancing":true,
                            "shadowCastingMode":"Off",
                            "receiveShadows":false,
                            "shadowBias":0.0,
                            "motionVectorGenerationMode":"Object",
                            "forceRenderingOff":false,
                            "rendererPriority":0,
                            "renderingLayerMask":1,
                            "sortingLayerID":0,
                            "sortingOrder":0,
                            "lightProbeUsage":"Off",
                            "reflectionProbeUsage":"Off"
                        }
                    },
                    "skybox":{
                        "materialIndex":1
                    },
                    "right": {
                        "title": "Capsule1",
                        "author": "Izayoi Jiichan",
                        "organization": "",
                        "createdDate": "2020-01-01",
                        "updatedDate": "2020-01-01",
                        "version": "0.1",
                        "distributionUrl": "",
                        "licenseUrl": ""
                    }
                }
            },
            "extras": {}
        }
    ]
}
```
___
Last updated: 15 March, 2020  
Editor: Izayoi Jiichan

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
