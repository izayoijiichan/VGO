# VGO

## Particle schema

### layout.particle

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
|:---|:---|:---:|:---|:---:|
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
|simulationSpace||string|0: Local<br>1: World<br>2: Custom|0|
|simulationSpeed||float|[0, infinity]||
|customSimulationSpace||VGO_Transform|||
|useUnscaledTime||bool|true / false||
|scalingMode||string|0: Hierarchy<br>1: Local<br>2: Shape|0|
|playOnAwake||bool|true / false||
|emitterVelocityMode||enum|0: Transform<br>2: Rigidbody|0|
|maxParticles||int|[0, infinity]||
|stopAction||enum|0: None<br>1: Disable<br>2: Destroy<br>3: Callback|0|
|cullingMode||enum|0: Automatic<br>1: PauseAndCatchup<br>2: Pause<br>3: AlwaysSimulate|0|
|ringBufferMode||enum|0: Disabled<br>1: PauseUntilReplaced<br>2: LoopUntilReplaced|0|
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
|:---|:---|:---:|:---|:---:|
|enabled||bool|true / false||
|shapeType||enum|0: Sphere<br>1: SphereShell<br>2: Hemisphere<br>3: HemisphereShell<br>4: Cone<br>5: Box<br>6: Mesh<br>7: ConeShell<br>8: ConeVolume<br>9: ConeVolumeShell<br>10: Circle<br>11: CircleEdge<br>12: SingleSidedEdge<br>13: MeshRenderer<br>14: SkinnedMeshRenderer<br>15: BoxShell<br>16: BoxEdge<br>17: Donut<br>18: Rectangle<br>19: Sprite<br>20: SpriteRenderer|0|
|angle||float|[0, 90]||
|radius||float|[0.0001, infinity]||
|donutRadius||float|[0.0001, infinity]||
|radiusMode||enum|0: Random<br>1: Loop<br>2: PingPong<br>3: BurstSpread|0|
|radiusSpread||float|[0, 1]||
|radiusSpeed||VGO_PS_MinMaxCurve|||
|radiusSpeedMultiplier||float|[0, infinity]||
|radiusThickness||float|[0, 1]||
|boxThickness||float[3]|x, y, z||
|arc||float|[0, 360]||
|arcMode||enum|0: Random<br>1: Loop<br>2: PingPong<br>3: BurstSpread|0|
|arcSpread||float|||
|arcSpeed||VGO_PS_MinMaxCurve|||
|arcSpeedMultiplier||float|[0, infinity]||
|length||float|||
|meshShapeType||enum|0: Vertex<br>1: Edge<br>2: Triangle|0|
|meshSpawnMode||enum|0: Random<br>1: Loop<br>2: PingPong<br>3: BurstSpread|0||
|meshSpawnSpread||float|||
|meshSpawnSpeed||VGO_PS_MinMaxCurve|||
|meshSpawnSpeedMultiplier||float|[0, infinity]||
|useMeshMaterialIndex||bool|true / false||
|meshMaterialIndex||int|||
|useMeshColors||bool|true / false||
|normalOffset||float|||
|textureIndex||int|||
|textureClipChannel||enum|0: Red<br>1: Green<br>2: Blue<br>3: Alpha|0|
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
|:---|:---|:---:|:---|:---:|
|enabled||bool|true / false||
|x||VGO_PS_MinMaxCurve|||
|y||VGO_PS_MinMaxCurve|||
|z||VGO_PS_MinMaxCurve|||
|xMultiplier||float|[0, infinity]||
|yMultiplier||float|[0, infinity]||
|zMultiplier||float|[0, infinity]||
|space||enum|0: Local<br>1: World<br>2: Custom|0|
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
|:---|:---|:---:|:---|:---:|
|enabled||bool|true / false||
|separateAxes||bool|true / false||
|limitX||VGO_PS_MinMaxCurve|||
|limitY||VGO_PS_MinMaxCurve|||
|limitZ||VGO_PS_MinMaxCurve|||
|limitXMultiplier||float|[0, infinity]||
|limitYMultiplier||float|[0, infinity]||
|limitZMultiplier||float|[0, infinity]||
|space||enum|0: Local<br>1: World<br>2: Custom|0|
|dampen||float|[0, 1]||
|drag||VGO_PS_MinMaxCurve|||
|dragMultiplier||float|[0, infinity]||
|multiplyDragByParticleSize||bool|true / false||
|multiplyDragByParticleVelocity||bool|true / false||

### VGO_PS_InheritVelocityModule

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---|:---:|
|enabled||bool|true / false||
|mode||enum|0: Initial<br>1: Current|0|
|curve||VGO_PS_MinMaxCurve|||
|curveMultiplier||float|[0, infinity]||

### VGO_PS_ForceOverLifetimeModule

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---|:---:|
|enabled||bool|true / false||
|x||VGO_PS_MinMaxCurve|||
|y||VGO_PS_MinMaxCurve|||
|z||VGO_PS_MinMaxCurve|||
|xMultiplier||float|[0, infinity]||
|yMultiplier||float|[0, infinity]||
|zMultiplier||float|[0, infinity]||
|space||enum|0: Local<br>1: World<br>2: Custom|0|
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
|:---|:---|:---:|:---|:---:|
|enabled||bool|true / false||
|multiplierCurve||VGO_PS_MinMaxCurve|||
|Multiplier||float|[0, infinity]||
|influenceFilter||enum|0: LayerMask<br>1: List<br>2: LayerMaskAndList|0|
|influenceMask||int|||

### VGO_PS_NoiseModule

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---|:---:|
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
|quality||enum|0: Low<br>1: Medium<br>2: High|0|
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
|:---|:---|:---:|:---|:---:|
|enabled||bool|true / false||
|mode||enum|0: PerParticle<br>1: Ribbon|0|
|ratio||float|[0, 1]||
|lifetime||VGO_PS_MinMaxCurve|||
|lifetimeMultiplier||float|[0, infinity]||
|minVertexDistance||float|[0, infinity]||
|worldSpace||bool|true / false||
|dieWithParticles||bool|true / false||
|ribbonCount||int|[1, infinity]||
|splitSubEmitterRibbons||bool|true / false||
|attachRibbonsToTransform||bool|true / false||
|textureMode||enum|0: Stretch<br>1: Tile<br>2: DistributePerSegment<br>3: RepeatPerSegment|0|
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
|:---|:---|:---:|:---|:---:|
|enabled||bool|true / false||
|renderMode||enum|0: Billboard<br>1: Stretch<br>2: HorizontalBillboard<br>3: VerticalBillboard<br>4: Mesh<br>5: None|0|
|cameraVelocityScale||float|||
|velocityScale||float|||
|lengthScale||float|||
|normalDirection||float|[0, 1]||
|sharedMaterialIndex||int|||
|trailMaterialIndex||int|||
|sortMode||enum|0: None<br>1: Distance<br>2: OldestInFront<br>3: YoungestInFront|0|
|sortingFudge||float|||
|minParticleSize||float|[0, infinity]||
|maxParticleSize||float|[0, infinity]||
|alignment||enum|0: View<br>1: World<br>2: Local<br>3: Facing<br>4: Velocity|0|
|flip||float[]|x, y, z||
|allowRoll||bool|true / false||
|pivot||float[]|x, y, z||
|maskInteraction||enum|0: None<br>1: VisibleInsideMask<br>2: VisibleOutsideMask|0|
|enableGPUInstancing||bool|true / false||
|shadowCastingMode||enum|0: Off<br>1: On<br>2: TwoSided<br>3: ShadowsOnly|0|
|receiveShadows||bool|true / false||
|shadowBias||float|||
|motionVectorGenerationMode||enum|0: Camera<br>1: Object<br>2: ForceNoMotion|0|
|forceRenderingOff||bool|true / false||
|rendererPriority||int|||
|renderingLayerMask||int|||
|sortingLayerID||int|||
|sortingOrder||int|||
|lightProbeUsage||enum|0: Off<br>1: BlendProbes<br>2: UseProxyVolume<br>4: CustomProvided|0|
|reflectionProbeUsage||enum|0: Off<br>1: BlendProbes<br>2: BlendProbesAndSkybox<br>3: Simple|0|
|probeAnchor||VGO_Transform|||

### VGO_PS_MinMaxCurve

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---|:---:|
|mode||enum|0: Constant<br>1: Curve<br>2: TwoCurves<br>3: TwoConstants|0|
|constant||float|||
|constantMin||float|||
|constantMax||float|||
|curveMultiplier||float|[0, infinity]||
|curve||VGO_AnimationCurve|||
|curveMin||VGO_AnimationCurve|||
|curveMax||VGO_AnimationCurve|||

### VGO_AnimationCurve

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---|:---:|
|keys||VGO_Keyframe[]|||
|preWrapMode||enum|0: Default<br>1: Once<br>2: Loop<br>4: PingPong<br>8: ClampForever|0|
|postWrapMode||enum|0: Default<br>1: Once<br>2: Loop<br>4: PingPong<br>8: ClampForever|0|

### VGO_Keyframe

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---|:---:|
|time||float|||
|value||float|||
|inTangent||float|||
|outTangent||float|||
|inWeight||float|||
|outWeight||float|||
|weightedMode||enum|0: None<br>1: In<br>2: Out<br>3: Both|0|

### VGO_PS_MinMaxGradient

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---|:---:|
|mode||enum|0: Color<br>1: Gradient<br>2: TwoColors<br>3: TwoGradients<br>4: RandomColor|0|
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
|mode||enum|0: Blend<br>1: Fixed|0|

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
#### JSON example (layout.particles)

```json
{
    "particles": [
        {
           "main":{
               "duration":5.0,
               "loop":true,
               "prewarm":false,
               "startDelay":{
                   "mode":0,
                   "constant":0.0
               },
               "startDelayMultiplier":0.0,
               "startLifetime":{
                   "mode":1,
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
                               "weightedMode":0
                           },
                           {
                               "time":1.0,
                               "value":1.0,
                               "inTangent":0.0,
                               "outTangent":0.0,
                               "inWeight":0.333333343,
                               "outWeight":0.333333343,
                               "weightedMode":0
                           }
                       ],
                       "preWrapMode":8,
                       "postWrapMode":8
                   }
               },
               "startLifetimeMultiplier":5.0,
               "startSpeed":{
                   "mode":0,
                   "constant":1.0
               },
               "startSpeedMultiplier":1.0,
               "startSize3D":false,
               "startSize":{
                   "mode":0,
                   "constant":0.08
               },
               "startSizeMultiplier":0.08,
               "startRotation3D":true,
               "startRotationX":{
                   "mode":0,
                   "constant":0.0174532924
               },
               "startRotationY":{
                   "mode":0,
                   "constant":0.0349065848
               },
               "startRotationZ":{
                   "mode":0,
                   "constant":0.0523598753
               },
               "startRotationXMultiplier":0.0174532924,
               "startRotationYMultiplier":0.0349065848,
               "startRotationZMultiplier":0.0523598753,
               "flipRotation":0.0,
               "startColor":{
                   "mode":0,
                   "color":[
                       0.743349731,0.534801245,0.0394535959,1.0
                   ]
               },
               "gravityModifier":{
                   "mode":0,
                   "constant":0.0
               },
               "gravityModifierMultiplier":0.0,
               "simulationSpace":0,
               "simulationSpeed":1.0,
               "useUnscaledTime":false,
               "scalingMode":0,
               "playOnAwake":true,
               "emitterVelocityMode":1,
               "maxParticles":1000,
               "stopAction":0,
               "cullingMode":0,
               "ringBufferMode":0
           },
           "emission":{
               "enabled":true,
               "rateOverTime":{
                   "mode":0,
                   "constant":10.0
               },
               "rateOverTimeMultiplier":10.0,
               "rateOverDistance":{
                   "mode":0,
                   "constant":0.0
               },
               "rateOverDistanceMultiplier":0.0,
               "bursts":[
                   {
                       "time":0.0,
                       "count":{
                           "mode":0,
                           "constant":3.0
                       },
                       "cycleCount":2,
                       "repeatInterval":0.01,
                       "probability":1.0
                   },
                   {
                       "time":1.0,
                       "count":{
                           "mode":3,
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
                           "mode":1,
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
                                       "weightedMode":0
                                   },
                                   {
                                       "time":0.9888916,
                                       "value":0.552083969,
                                       "inTangent":0.0,
                                       "outTangent":0.0,
                                       "inWeight":0.0,
                                       "outWeight":0.0,
                                       "weightedMode":0
                                   }
                               ],
                               "preWrapMode":8,
                               "postWrapMode":8
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
               "shapeType":0,
               "angle":25.0,
               "radius":1.0,
               "donutRadius":0.2,
               "radiusMode":0,
               "radiusSpread":0.0,
               "radiusSpeed":{
                   "mode":0,
                   "constant":1.0
               },
               "radiusSpeedMultiplier":1.0,
               "radiusThickness":1.0,
               "boxThickness":[ 0.0,0.0,0.0 ],
               "arc":360.0,
               "arcMode":0,
               "arcSpread":0.0,
               "arcSpeed":{
                   "mode":0,
                   "constant":1.0
               },
               "arcSpeedMultiplier":1.0,
               "length":5.0,
               "meshShapeType":1,
               "meshSpawnMode":0,
               "meshSpawnSpread":0.0,
               "meshSpawnSpeed":{
                   "mode":0,
                   "constant":0.0
               },
               "meshSpawnSpeedMultiplier":1.0,
               "useMeshMaterialIndex":false,
               "meshMaterialIndex":0,
               "useMeshColors":true,
               "normalOffset":0.0,
               "textureClipChannel":3,
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
                   "mode":0,
                   "constant":0.0
               },
               "y":{
                   "mode":0,
                   "constant":0.0
               },
               "z":{
                   "mode":0,
                   "constant":0.0
               },
               "xMultiplier":0.0,
               "yMultiplier":0.0,
               "zMultiplier":0.0,
               "space":0,
               "orbitalX":{
                   "mode":0,
                   "constant":0.0
               },
               "orbitalY":{
                   "mode":0,
                   "constant":0.0
               },
               "orbitalZ":{
                   "mode":0,
                   "constant":0.0
               },
               "orbitalXMultiplier":0.0,
               "orbitalYMultiplier":0.0,
               "orbitalZMultiplier":0.0,
               "orbitalOffsetX":{
                   "mode":0,
                   "constant":0.0
               },
               "orbitalOffsetY":{
                   "mode":0,
                   "constant":0.0
               },
               "orbitalOffsetZ":{
                   "mode":0,
                   "constant":0.0
               },
               "orbitalOffsetXMultiplier":0.0,
               "orbitalOffsetYMultiplier":0.0,
               "orbitalOffsetZMultiplier":0.0,
               "radial":{
                   "mode":0,
                   "constant":0.0
               },
               "radialMultiplier":0.0,
               "speedModifier":{
                   "mode":0,
                   "constant":1.2
               },
               "speedModifierMultiplier":1.2
           },
           "limitVelocityOverLifetime":{
               "enabled":true,
               "separateAxes":false,
               "limitX":{
                   "mode":0,
                   "constant":1.0
               },
               "limitXMultiplier":1.0,
               "space":0,
               "dampen":0.0,
               "drag":{
                   "mode":0,
                   "constant":0.0
               },
               "dragMultiplier":0.0,
               "multiplyDragByParticleSize":true,
               "multiplyDragByParticleVelocity":true
           },
           "inheritVelocity":{
               "enabled":true,
               "mode":0,
               "curve":{
                   "mode":0,
                   "constant":0.0
               },
               "curveMultiplier":0.0
           },
           "forceOverLifetime":{
               "enabled":true,
               "x":{
                   "mode":0,
                   "constant":0.0
               },
               "y":{
                   "mode":0,
                   "constant":0.0
               },
               "z":{
                   "mode":0,
                   "constant":0.0
               },
               "xMultiplier":0.0,
               "yMultiplier":0.0,
               "zMultiplier":0.0,
               "space":0,
               "randomized":false
           },
           "colorOverLifetime":{
               "enabled":true,
               "color":{
                   "mode":1,
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
                       "mode":0
                   }
               }
           },
           "colorBySpeed":{
               "enabled":true,
               "color":{
                   "mode":1,
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
                       "mode":0
                   }
               },
               "range":[ 0.0,1.0 ]
           },
           "sizeOverLifetime":{
               "enabled":true,
               "separateAxes":false,
               "x":{
                   "mode":1,
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
                               "weightedMode":0
                           },
                           {
                               "time":1.0,
                               "value":0.354166657,
                               "inTangent":-0.9374998,
                               "outTangent":-0.9374998,
                               "inWeight":0.144444466,
                               "outWeight":0.333333343,
                               "weightedMode":0
                           }
                       ],
                       "preWrapMode":8,
                       "postWrapMode":8
                   }
               },
               "xMultiplier":1.0
           },
           "sizeBySpeed":{
               "enabled":true,
               "separateAxes":false,
               "x":{
                   "mode":1,
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
                               "weightedMode":0
                           },
                           {
                               "time":1.0,
                               "value":1.0,
                               "inTangent":1.0,
                               "outTangent":0.0,
                               "inWeight":0.333333343,
                               "outWeight":0.333333343,
                               "weightedMode":0
                           }
                       ],
                       "preWrapMode":8,
                       "postWrapMode":8
                   }
               },
               "xMultiplier":1.0,
               "range":[ 0.0,1.0 ]
           },
           "rotationOverLifetime":{
               "enabled":true,
               "x":{
                   "mode":0,
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
                   "mode":0,
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
                   "mode":0,
                   "constant":1.0
               },
               "multiplier":1.0,
               "influenceFilter":0,
               "influenceMask":-1
           },
           "noise":{
               "enabled":true,
               "separateAxes":false,
               "strengthX":{
                   "mode":0,
                   "constant":0.2
               },
               "strengthXMultiplier":0.2,
               "frequency":0.5,
               "scrollSpeed":{
                   "mode":0,
                   "constant":0.0
               },
               "scrollSpeedMultiplier":0.0,
               "damping":true,
               "octaveCount":1,
               "octaveMultiplier":0.5,
               "octaveScale":2.0,
               "quality":2,
               "remapEnabled":false,
               "positionAmount":{
                   "mode":0,
                   "constant":1.0
               },
               "rotationAmount":{
                   "mode":0,
                   "constant":0.0
               },
               "sizeAmount":{
                   "mode":0,
                   "constant":0.0
               }
           },
           "lights":{
               "enabled":true,
               "ratio":0.5,
               "useRandomDistribution":true,
               "light":{
                   "type":2,
                   "range":0.1,
                   "color":[ 0.876,0.192,0.73,1.0 ],
                   "lightmapBakeType":4,
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
                   "mode":0,
                   "constant":1.0
               },
               "rangeMultiplier":1.0,
               "intensity":{
                   "mode":0,
                   "constant":1.0
               },
               "intensityMultiplier":1.0,
               "maxLights":20
           },
           "trails":{
               "enabled":true,
               "mode":0,
               "ratio":1.0,
               "lifetime":{
                   "mode":0,
                   "constant":0.3
               },
               "lifetimeMultiplier":0.3,
               "minVertexDistance":0.2,
               "worldSpace":false,
               "dieWithParticles":true,
               "ribbonCount":1,
               "splitSubEmitterRibbons":false,
               "attachRibbonsToTransform":false,
               "textureMode":1,
               "sizeAffectsWidth":true,
               "sizeAffectsLifetime":false,
               "useMeinheritParticleColorshColors":true,
               "colorOverLifetime":{
                   "mode":0,
                   "color":[ 1.0,1.0,1.0,1.0 ]
               },
               "widthOverTrail":{
                   "mode":0,
                   "constant":0.4
               },
               "widthOverTrailMultiplier":0.4,
               "colorOverTrail":{
                   "mode":0,
                   "color":[ 1.0,1.0,1.0,1.0 ]
               },
               "generateLightingData":false,
               "shadowBias":0.5
           },
           "renderer":{
               "enabled":true,
               "renderMode":0,
               "cameraVelocityScale":0.0,
               "velocityScale":0.0,
               "lengthScale":2.0,
               "normalDirection":1.0,
               "sharedMaterialIndex":3,
               "trailMaterialIndex":4,
               "sortMode":0,
               "sortingFudge":0.0,
               "minParticleSize":0.0,
               "maxParticleSize":0.5,
               "alignment":0,
               "flip":[ 0.0,0.0,0.0 ],
               "allowRoll":true,
               "pivot":[ 0.0,0.0,0.0 ],
               "maskInteraction":0,
               "enableGPUInstancing":true,
               "shadowCastingMode":0,
               "receiveShadows":false,
               "shadowBias":0.0,
               "motionVectorGenerationMode":1,
               "forceRenderingOff":false,
               "rendererPriority":0,
               "renderingLayerMask":1,
               "sortingLayerID":0,
               "sortingOrder":0,
               "lightProbeUsage":0,
               "reflectionProbeUsage":0
           }
        }
    ]
}
```
___
Last updated: 20 August, 2020  
Editor: Izayoi Jiichan

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
