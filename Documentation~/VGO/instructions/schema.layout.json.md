# VGO

## Layout schema

### layout

The vgo layout.

|definition name|description|type|required|remarks|
|:---|:---|:---|:---:|:---|
|nodes|List of nodes.|layout.node[]|true|The root is included at the first of the node.|
|skins|List of skins.|layout.skin[]|||
|meshes|List of meshes.|layout.mesh[]|||
|materials|List of materials.|layout.material[]|||
|textures|List of textures.|layout.texture[]|||
|particles|List of particles.|layout.particle[]|||
|springBoneInfo|The spring bone info.|vgo.springBoneInfo|||
|extensions|The extensions.|object|||

#### layout.node

A node in the node hierarchy.

|definition name|description|type|required|setting value|default value|
|:---|:---|:---|:---:|:---:|:---:|
|name|The user-defined name of this object.|string|true|||
|isRoot|Whether the GameObject is root.|bool||true / false|false|
|isActive|Whether the GameObject is active.|bool||true / false|true|
|isStatic|Whether the GameObject is static.|bool||true / false|false|
|tag|Tag attached to GameGbject.|string|||Untagged|
|layer|The layer on which the GameObject is located.|int||[0, 31]|0|
|animator||node.animator||||
|rigidbody||node.rigidbody||||
|colliders||node.collider[]||||
|skybox||vgo.skybox||||
|light||vgo.light||||
|right||vgo.right||||
|particle|The index of the particle.|int||||
|mesh|The index of the mesh in this node.|int||||
|skin|The index of the skin referenced by this node.|int||||
|springBoneGroups|The indices of the spring bone groups referenced by this node.|int[]||||
|springBoneColliderGroup|The index of the spring bone collider group referenced by this node.|int||||
|children|The indices of this node's children.|int[]||||

#### node.animator

|definition name|description|type|required|setting value|default value|
|:---|:---|:---|:---:|:---|:---:|
|name|The name of the object.|string||||
|enabled|Whether this animator is enabled.|bool||true / false|true|
|humanAvatar|The current human Avatar.|vgo.humanAvatar||||
|applyRootMotion|Should root motion be applied?|bool||true / false|false|
|updateMode|Specifies the update mode of the Animator.|enum||0: Normal<br>1: AnimatePhysics<br>2: UnscaledTime|0|
|cullingMode|Controls culling of this Animator component.|enum||0: AlwaysAnimate<br>1: CullUpdateTransforms<br>2: CullCompletely|0|

##### vgo.humanAvatar

|definition name|description|type|required|setting value|default value|
|:---|:---|:---|:---:|:---|:---:|
|name|The name of this human avatar.|string||||
|humanBones|List of the human bone.|vgo.HumanBone[]|true|||

##### vgo.humanBone

|definition name|description|type|required|setting value|default value|
|:---|:---|:---|:---:|:---|:---:|
|humanBodyBone|The human body bone.|enum|true|[0, 55]||
|nodeIndex|The index of the node.|int|true|||


#### node.rigidbody

|definition name|description|type|required|setting value|default value|
|:---|:---|:---:|:---:|:---|:---:|
|mass|The mass of the object. (Unit is kg)|float||[0.0000001, 1000000000]||
|drag|The amount of air resistance that affects an object when it is moved by force.|float||[0.0, infinity]||
|angularDrag|The amount of air resistance that affects the object when rotating by torque.|float||[0.0, infinity]||
|useGravity|Whether the object is affected by gravity.|bool||true / false|true|
|isKinematic|Whether physics affects the rigid body.|bool||true / false|false|
|interpolation|The type of completion.|enum||0: None<br>1: Interpolate<br>2: Extrapolate|0|
|collisionDetectionMode|Collision detection mode.|enum||0: Discrete<br>1: Continuous<br>2: ContinuousDynamic<br>3: ContinuousSpeculative|0|
|constraints|The flags that restricts the movement of the rigid body.|int||FreesePositionX(2) \|<br>FreesePositionY(4) \|<br>FreesePositionZ(8) \|<br>FreeseRotationX(16) \|<br>FreeseRotationY(32) \|<br>FreeseRotationZ(64)|0|

#### node.collider

|definition name|description|type|required|setting value|default value|Box|Capsule|Sphere|
|:---|:---|:---:|:---|:---:|:---:|:---:|:---:|:---:|
|enabled|Whether the collider is enable.|bool||true / false|true|*|*|*|
|type|The type of collider.|enum||0: Box<br>1: Capsule<br>2: Sphere|0|*|*|*|
|isTrigger|Whether the collider is a trigger.|bool||true / false|false|*|*|*|
|center|The center of the collider. (Unit is m)|float[3]||x, y, z|0.0, 0.0, 0.0|*|*|*|
|size|The total size of the collider. (Unit is m)|float[3]||x, y, z|1.0, 1.0, 1.0|*|-|-|
|radius|The radius of the collider.|float||[0, infinity]||-|*|*|
|height|The height of the collider.|float||[0, infinity]||-|*|-|
|direction|The direction of the collider.|int||0: X<br>1: Y<br>2: Z||-|*|-|
|physicMaterial|The physic material of this collider.|vgo.PhysicMaterial||||*|*|*|

#### vgo.physicMaterial

|definition name|description|type|required|setting value|default value|
|:---|:---|:---:|:---:|:---|:---:|
|dynamicFriction|Friction against a moving object.|float||[0.0, 1.0]||
|staticFriction|Friction used for objects that are stationary on a surface.|float||[0.0, 1.0]||
|bounciness|How elastic is the surface.|float||[0.0, 1.0]||
|frictionCombine|The type of friction handling between colliding objects.|enum||0: Average<br>1: Multiply<br>2: Minimum<br>3: Maximum|0|
|bounceCombine|Processing type for bounce between colliding objects.|enum||0: Average<br>1: Multiply<br>2: Minimum<br>3: Maximum|0|

#### vgo.skybox

|definition name|description|type|required|setting value|default value|
|:---|:---|:---:|:---:|:---:|:---:|
|materialIndex|The index of the material.|int||||

#### vgo.light

|definition name|description|type|required|setting value|default value|Spot|Directional|Point|Rectangle|Disc|
|:---|:---|:---:|:---:|:---|:---:|:---:|:---:|:---:|:---:|:---:|
|enabled|Whether the light is enable.|bool||true / false|true|*|*|*|*|*|
|type|The type of the light.|enum||0: Spot<br>1: Directional<br>2: Point<br>3: Rectangle<br>4: Disc|0|*|*|*|*|*|
|shape|This property describes the shape of the spot light.|enum||0: Cone<br>1: Pyramid<br>2: Box|0|*|-|-|-|-|
|range|The range of the light.|float||[0, infinity]||*|-|*|*|*|
|spotAngle|The angle of the light's spotlight cone in degrees.|float||[0, infinity]||*|-|-|-|-|
|areaSize|The size of the area light.|float[2]||x, y||-|-|-|*|-|
|areaRadius|The radius of the area light|float||[0, infinity]||-|-|-|-|*|
|color|The color of the light.|float[4]||r, g, b, a||*|*|*|*|*|
|lightmapBakeType|This property describes what part of a light's contribution can be baked.|enum||1: Mixed<br>2: Baked<br>4: Realtime||*|*|*|*|*|
|intensity|The Intensity of a light is multiplied with the Light color.|float||[0, infinity]||*|*|*|*|*|
|bounceIntensity|The multiplier that defines the strength of the bounce lighting.|float||[0, infinity]||*|*|*|*|*|
|shadows|How this light casts shadows.|enum||0: None<br>1: Hard<br>2: Soft|0|*|*|*|*|*|
|shadowRadius|Controls the amount of artificial softening applied to the edges of shadows cast by the Point or Spot light.|float||[0, infinity]||*|-|*|-|-|
|shadowAngle|Controls the amount of artificial softening applied to the edges of shadows cast by directional lights.|float||[0, infinity]||-|*|-|-|-|
|shadowStrength|Strength of light's shadows.|float||[0, infinity]||-|*|*|-|-|
|shadowResolution|The resolution of the shadow map.|enum||-1: FromQualitySettings<br>0: Low<br>1: Medium<br>2: High<br>3: VeryHigh|-1|-|*|*|-|-|
|shadowBias|Shadow mapping constant bias.|float||[0, infinity]||-|*|*|-|-|
|shadowNormalBias|Shadow mapping normal-based bias.|float||[0, infinity]||-|*|*|-|-|
|shadowNearPlane|Near plane value to use for shadow frustums.|float||[0, infinity]||-|*|*|-|-|
|renderMode|How to render the light.|enum||0: Auto<br>1: ForcePixel<br>2: ForceVertex|0|*|*|*|*|*|
|cullingMask|This is used to light certain objects in the Scene selectively.|int||[-1, infinity]|-1 (Everything)|*|*|*|*|*|

Cookie, Flare, Halo are not supported.


#### JSON example (layout.nodes)

```json
{
    "nodes": [
        {
            "name": "Node1",
            "isRoot": true,
            "tag": "Player",
            "animator": {
                "humanAvatar": {
                    "name": "",
                    "humanBones": []
                }
            },
            "children": [
                1,
                2,
                3,
                4,
                5
            ]
        },
        {
            "name": "Node2",
            "layer": 1,
            "rigidbody": {
                "mass": 1,
                "drag": 0,
                "angularDrag": 0.05,
                "useGravity": true,
                "isKinematic": false,
                "interpolation": 0,
                "collisionDetectionMode": 0,
                "constraints": 0
            },
            "colliders": [
                {
                    "type": 0,
                    "center": [ 0, 0, 0 ],
                    "size": [ 1, 1, 1 ]
                },
                {
                    "enabled": false,
                    "type": 1,
                    "radius": 0.49999997,
                    "height": 1,
                    "direction": 1,
                    "physicMaterial": {
                        "dynamicFriction": 0.6,
                        "staticFriction": 0.6,
                        "bounciness": 0,
                        "frictionCombine": 0,
                        "bounceCombine": 0
                    }
                }
            ],
            "mesh": 0
        }
    ]
}
```

#### layout.skin

Joints and matrices defining a skin.

|definition name|description|type|required|setting value|default value|
|:---|:---|:---:|:---:|:---:|:---:|
|inverseBindMatrices|The index of the accessor containing the floating-point 4x4 inverse-bind matrices.|int||||
|skeleton|The index of the node used as a skeleton root.|int||||
|joints|Indices of skeleton nodes, used as joints in this skin.|int[]|true|||

#### layout.mesh

A set of primitives to be rendered.

|definition name|description|type|required|setting value|default value|
|:---|:---|:---:|:---:|:---|:---:|
|name|The name of this mesh.|string|true|||
|attributes|A dictionary mapping attributes.|mesh.primitive.attributes|true|||
|subMeshes|The index of the accessor that contains the sub-mesh indices.|int[]||||
|materials|The index list of the material to apply to this primitive when rendering.|int[]||||
|blendShapeKind|The kind of the blend shape.|enum||0: None<br>1: Face<br>2: Face_2<br>3: Kind_3<br>4: Kind_4<br>5: Kind_5|0|
|blendShapes|List of the blend shape.|mesh.blendshape[]||||
|blendShapePesets|List of the blend shape preset.|mesh.blendshape.preset[]||||

#### mesh.primitive.attributes

A dictionary object, where each key corresponds to mesh attribute semantic and each value is the index of the accessor containing attribute's data.

|type|key|value|
|:---:|:---|:---:|
|Dictionary<string, int>|POSITION<br>NORMAL<br>TANGENT<br>COLOR_0<br>TEXCOORD_0<br>JOINTS_0<br>WEIGHTS_0|accessor index|

|key|accessor data type|
|:---|:---|
|POSITION|Vector3 (float)|
|NORMAL|Vector3 (float)|
|TANGENT|Vector4 (float)|
|COLOR_0|Color4 (ubyte)<br>Color4 (float)|
|TEXCOORD_0|Vector2 (float)|
|JOINTS_0|Vector4 (ubyte)<br>Vector4 (ushort)|
|WEIGHTS_0|Vector4 (float)|

#### mesh.blendshape

A mesh blned shape.

|definition name|description|type|required|setting value|default value|
|:---|:---|:---:|:---:|:---|:---:|
|name|The name of the blend shape.|string|true|||
|attributes|A dictionary mapping attributes.<br>Only `POSITION`,`NORMAL`,`TANGENT` can be included.|mesh.primitive.attributes|true|||
|facePartsType|The type of face parts.|enum||0: None<br>10: Forehead<br>11: Eyebrow<br>12: Eyelashes<br>13: Eyelid<br>14: Pupil<br>20: Ear<br>30: Nose<br>40: Cheek<br>50: Mouth<br>51: Teeth<br>52: Tongue<br>60: Hair<br>61: BackHair<br>62: SideHair<br>63: Frizz<br>64: EarHair<br>65: NoseHair<br>66: Mustache<br>67: Beard<br>70: Mole|0|
|blinkType|The type of blink.|enum||0: None<br>1: Left<br>2: Right<br>3: Both|0|
|visemeType|The type of viseme.|enum||-1: None<br>0: Silence<br>1: PP<br>2: FF<br>3: TH<br>4: DD<br>5: kk<br>6: CH<br>7: SS<br>8: nn<br>9: RR<br>10: A<br>11: E<br>12: I<br>13: O<br>14: U|-1|

#### mesh.blendshape.preset

A blend shape preset.

|definition name|description|type|required|setting value|default value|
|:---|:---|:---:|:---:|:---|:---:|
|name|The name of preset.|string||||
|type|The type of preset.|enum|true|0: Custom<br>1: Neutral<br>2: Joy<br>3: Angry<br>4: Sorrow<br>5: Fun<br>6: Confuse<br>7: Nervous<br>8: Sleepy<br>9: Surprise<br>10: WinkL<br>11: WinkR<br>12: Preset_12<br>13: Preset_13<br>14: Preset_14<br>15: Preset_15||
|bindings|List of binding.|mesh.blendshape.binding[]|true|||

#### mesh.blendshape.binding

A blend shape binding for preset.

|definition name|description|type|required|setting value|default value|
|:---|:---|:---:|:---:|:---|:---:|
|index|The index of the BlendShape.|int|true|||
|weight|The weight for this BlendShape.|float||[0.0, 100.0]|0|


#### JSON example (layout.meshes)

```json
{
    "meshes": [
        {
            "name": "face",
            "attributes": {
                "POSITION": 59,
                "NORMAL": 60,
                "TEXCOORD_0": 61,
                "JOINTS_0": 62,
                "WEIGHTS_0": 63
            },
            "subMeshes": [
                64,
                65,
                66
            ],
            "materials": [
                4,
                5,
                6
            ],
            "blendShapeKind": 1,
            "blendShapes": [
                {
                    "name": "face.mouth_a",
                    "attributes": {
                        "POSITION": 67,
                        "NORMAL": 68,
                        "TANGENT": -1
                    },
                    "facePartsType": 50,
                    "blinkType": 0,
                    "visemeType": 10
                }
            ],
            "blendShapePesets": [
                {
                    "name": "Joy",
                    "type": 2,
                    "bindings": [
                        {
                            "index": 24,
                            "weight": 100.0
                        },
                        {
                            "index": 36,
                            "weight": 100.0
                        },
                        {
                            "index": 44,
                            "weight": 100.0
                        }
                    ]
                }
            ]
        }
    ]
}
```

#### layout.material

The material appearance of a primitive.

|definition name|description|type|required|setting value|default value|
|:---|:---|:---|:---:|:---:|:---:|
|name|The user-defined name of this object.|string|true|||
|shaderName|The shader name.|string|true|||
|renderQueue|The render queue.|int||||
|isUnlit|Whether material is unlit.|bool||true / false|false|
|intProperties|key is propety name.|Dictionary<string, int>||||
|floatProperties|key is propety name.|Dictionary<string, float>||||
|colorProperties|key is propety name. value is color[3] or color[4].|Dictionary<string, float[]>||||
|vectorProperties|key is propety name. value is vector.|Dictionary<string, float[]>||||
|matrixProperties|key is propety name. value is matrix.|Dictionary<string, float[]>||||
|textureOffsetProperties|key is propety name. value is vector2.|Dictionary<string, float[]>||||
|textureScaleProperties|key is propety name. value is vector2.|Dictionary<string, float[]>||||
|textureIndexProperties|key is propety name.|Dictionary<string, int>||||
|keywordMap|key is keyword.|Dictionary<string, bool>||||
|tagMap|key is tag name.|Dictionary<string, string>||||
|extensions|The extensions.|object|||

#### JSON example (layout.materials)

```json
{
    "materials": [
        {
            "name": "StandardMaterial",
            "shaderName": "Standard",
            "renderQueue": 2000,
            "intProperties": {
                "_Mode": 0,
                "_SmoothnessTextureChannel": 0,
                "_SpecularHighlights": 1,
                "_GlossyReflections": 1,
                "_UVSec": 0
            },
            "floatProperties": {
                "_Cutoff": 0.5,
                "_Metallic": 0,
                "_Glossiness": 0.5,
                "_GlossMapScale": 1,
                "_BumpScale": 1,
                "_Parallax": 0.02,
                "_OcclusionStrength": 1,
                "_DetailNormalMapScale": 1
            },
            "colorProperties": {
                "_Color": [
                    0.743349731,
                    0.4882486,
                    0.181164235,
                    1
                ],
                "_EmissionColor": [
                    0,
                    0,
                    0
                ]
            },
            "textureScaleProperties": {
                "_MainTex": [
                    5,
                    5
                ]
            },
            "textureIndexProperties": {
                "_MainTex": 1
            },
            "keywordMap": {
                "_NORMALMAP": false,
                "_PARALLAXMAP": false,
                "_EMISSION": false,
                "_DETAIL_MULX2": false,
                "_METALLICGLOSSMAP": false
            }
        }
    ]
}
```

#### layout.texture

A texture.

|definition name|description|type|required|setting value|default value|
|:---|:---|:---|:---:|:---|:---:|
|name|The user-defined name of this object.|string|true|||
|source|The index of the accessor that contains the image.|int||||
|dimensionType|Dimensionality type of the texture.|enum||-1: Unknown<br>0: None<br>1: Any<br>2: Tex2D<br>3: Tex3D<br>4: Cube<br>5: Tex2DArray<br>6: CubeArray|0|
|mapType|The texture type.|enum||-1: Unknown<br>0: Default<br>1: NormalMap<br>2: HeightMap<br>3: OcclusionMap<br>4: EmissionMap<br>5: MetallicRoughnessMap<br>6: SpecularGlossinessMap<br>7: CubeMap|0|
|colorSpace|Image color space.|enum||0: sRGB<br>1: Linear|0|
|mimeType|The image's MIME type.|string||image/jpeg<br>image/png||
|filterMode|Filtering mode of the texture.|enum||0: Point<br>1: Bilinear<br>2: Trilinear|0|
|wrapMode|Texture coordinate wrapping mode.|enum||0: Repeat<br>1: Clamp<br>2: Mirror<br>3: MirrorOnce|0|
|wrapModeU|Texture U coordinate wrapping mode.|enum||0: Repeat<br>1: Clamp<br>2: Mirror<br>3: MirrorOnce|0|
|wrapModeV|Texture V coordinate wrapping mode.|enum||0: Repeat<br>1: Clamp<br>2: Mirror<br>3: MirrorOnce|0|
|metallicRoughness|The metallic-roughness of the material.|float||[0.0, 1.0]||
|extensions|The extensions.|object|||

#### JSON example (layout.textures)

```json
{
    "textures": [
        {
            "name": "MetallicTexture",
            "source": 1,
            "dimensionType": 2,
            "mapType": 5,
            "colorSpace": 1,
            "mimeType": "image/png",
            "filterMode": 1,
            "wrapMode": 0,
            "wrapModeU": 0,
            "wrapModeV": 0,
            "metallicRoughness": 0.5
        }
    ]
}
```
___
Last updated: 20 August, 2020  
Editor: Izayoi Jiichan

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
