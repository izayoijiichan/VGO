# VGO

VGO is a 3D data format for Unity that can store Collider and Rigidbody information.

## Features
- glTF (GLB) 2.0 extended format.
- Added extended definitions for Collider, Rigidbody, Light and rights information to nodes.  
- Unity GameObject Transform, Rigidbody, Collider, PhysicMaterial, Mesh, Material, Texture, Light can be saved.
- Standard, Unlit and MToon shader settings can be saved.

___
## JSON schema of glTF

The following extended definitions are added:

### glTF.extensionsUsed

|Definition name|Description|
|:---|:---|
|VGO|Declaration to use VGO.|
|VGO_nodes|Declaration to use VGO_nodes.|
|VGO_materials|Declaration to use VGO_materials.|
|KHR_materials_unlit|Declares that Unlit shaders can be used for materials.|
|VRMC_materials_mtoon|Declares that MToon shaders can be used for materials.|

### glTF.extensionsRequired

|Definition name|Description|
|:---|:---|
|VGO|Requires support for VGO extensions.|
|VGO_nodes|Requires support for VGO_nodes extension.|
|VGO_materials|Requires support for VGO_materials extension.|
|KHR_materials_unlit|Requires support for the KHR_materials_unlit extension.|
|VRMC_materials_mtoon|Requires support for the VRMC_materials_mtoon extension.|

### glTF.extensions

|Definition name|Description|
|:---|:---|
|VGO|VGO information|

### glTF.extensions.VGO

|definition name|description|
|:---|:---|
|meta|VGO meta information|
|right|VGO rights information|

### glTF.nodes.[*].extensions

|definition name|description|
|:---|:---|
|VGO_nodes|Node VGO information|

### glTF.nodes.[*].extensions.VGO_nodes

|definition name|description|
|:---|:---|
|gameObject|GameObject information|
|colliders|Collider information|
|rigidbody|Rigid body information|
|light|Light information|
|right|VGO rights information|

### glTF.materials.[*].extensions

|definition name|description|
|:---|:---|
|KHR_materials_unlit|Unlit material information|
|VGO_materials|VGO material information|
|VRMC_materials_mtoon|MToon material information|
___
## Extended definition details

### vgo.meta

|definition name|description|type|fixed value|
|:---|:---|:---:|:---:|
|generatorName|The name of the generation tool.|string|UniVGO|
|generatorVersion|The generation tool version.|string|0.6.0|
|specVersion|VGO specification version.|string|0.4|

### vgo.right

|definition name|description|type|remarks|
|:---|:---|:---:|:---|
|title|The name of the work.|string|Required|
|author|The name of the creator.|string|Required|
|organization|The organization to which the creator belongs.|string||
|createdDate|The creation date of the work.|string|There is no format specification.|
|updatedDate|The update date of the work.|string|There is no format specification.|
|version|The version of the work.|string|There is no format specification.|
|distributionUrl|Distribution URL.|string|URL format|
|licenseUrl|The URL where the license is written.|string|URL format|

### node.vgo.gameobject

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|isActive|Whether the GameObject is active.|bool|true / false|true|
|isStatic|Whether the GameObject is static.|bool|true / false|false|
|tag|Tag attached to GameGbject.|string||Untagged|
|layer|The layer on which the GameObject is located.|int|[0, 31]|0|

### node.vgo.collider

|definition name|description|type|setting value|Box|Capsule|Sphere|
|:---|:---|:---:|:---:|:---:|:---:|:---:|
|enabled|Whether the collider is enable.|bool|true / false|*|*|*|
|type|The type of collider.|string|Box / Capsule / Sphere|*|*|*|
|isTrigger|Whether the collider is a trigger.|bool|true / false|*|*|*|
|center|The center of the collider.（Unit is m）|float[3]|[x, y, z]|*|*|*|
|size|The total size of the collider.（Unit is m）|float[3]|[x, y, z]|*|-|-|
|radius|The radius of the collider.|float|[0, infinity]|-|*|*|
|height|The height of the collider.|float|[0, infinity]|-|*|-|
|direction|The direction of the collider.|int|0:X / 1:Y / 2:Z|-|*|-|
|physicMaterial|The physic material of this collider.|node.vgo.physicMaterial||*|*|*|

### node.vgo.physicMaterial

|definition name|description|type|setting value|
|:---|:---|:---:|:---:|
|dynamicFriction|Friction against a moving object.|float|[0.0, 1.0]|
|staticFriction|Friction used for objects that are stationary on a surface.|float|[0.0, 1.0]|
|bounciness|How elastic is the surface.|float|[0.0, 1.0]|
|frictionCombine|The type of friction handling between colliding objects.|string|Average / Minimum / Maximum / Multiply|
|bounceCombine|Processing type for bounce between colliding objects.|string|Average / Minimum / Maximum / Multiply|

### node.vgo.rigidbody

|definition name|description|type|setting value|
|:---|:---|:---:|:---:|
|mass|The mass of the object. (Unit is kg)|float|[0.0000001, 1000000000]|
|drag|The amount of air resistance that affects an object when it is moved by force.|float|[0.0, infinity]|
|angularDrag|The amount of air resistance that affects the object when rotating by torque.|float|[0.0, infinity]|
|useGravity|Whether the object is affected by gravity.|bool|true / false|
|isKinematic|Whether physics affects the rigid body.|bool|true / false|
|interpolation|The type of completion.|string|None / Interpolate / Extrapolate|
|collisionDetectionMode|Collision detection mode.|string|Discrete / Continuous / ContinuousDynamic / ContinuousSpeculative|
|constraints|The flags that restricts the movement of the rigid body.|int|FreesePositionX(2) \| FreesePositionY(4) \| FreesePositionZ(8) \| FreeseRotationX(16) \| FreeseRotationY(32) \| FreeseRotationZ(64)|

### node.vgo.light

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

### VGO_materials

|definition name|description|type|remarks|
|:---|:---|:---:|:---:|
|shaderName|The name of shader.|string||

### VRMC_materials_mtoon

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|version||string||32|
|renderMode||string|opaque / cutout / transparent / transparentWithZWrite|opaque|
|cullMode||string|off / flont / back|off|
|renderQueueOffsetNumber||int|||
|litFactor||float[4]|[r, g, b, a]||
|litMultiplyTexture||int|||
|shadeFactor||float[4]|[r, g, b, a]||
|shadeMultiplyTexture||int|||
|cutoutThresholdFactor||float|[0.0, 1.0]||
|shadingShiftFactor||float|[-1.0, 1.0]||
|shadingToonyFactor||float|[0.0, 1.0]||
|shadowReceiveMultiplierFactor||float|[0.0, 1.0]||
|shadowReceiveMultiplierMultiplyTexture||int|||
|litAndShadeMixingMultiplierFactor||float|[0.0, 1.0]||
|litAndShadeMixingMultiplierMultiplyTexture||int|||
|lightColorAttenuationFactor||float|[0.0, 1.0]||
|giIntensityFactor||float|[0.0, 1.0]||
|normalTexture||int|||
|normalScaleFactor||float|||
|emissionFactor||float[3]|[r, g, b]||
|emissionMultiplyTexture||int|||
|additiveTexture||int|||
|rimFactor||float[4]|[r, g, b, a]||
|rimMultiplyTexture||int|||
|rimLightingMixFactor||float|[0.0, 1.0]||
|rimFresnelPowerFactor||float|[0.0, 100.0]||
|rimLiftFactor||float|[0.0, 1.0]||
|outlineWidthMode||string|none / worldCoordinates / screenCoordinates|none|
|outlineWidthFactor||float|[0.01, 1.0]||
|outlineWidthMultiplyTexture||int|||
|outlineScaledMaxDistanceFactor||float|[1.0, 10.0]||
|outlineColorMode||string|fixedColor / mixedLighting|fixedColor|
|outlineFactor||float[4]|[r, g, b, a]||
|outlineLightingMixFactor||float|[0.0, 1.0]||
|mainTextureLeftBottomOriginScale||float[2]|[x, y]||
|mainTextureLeftBottomOriginOffset||float[2]|[x, y]||
|uvAnimationMaskTexture||int|||
|uvAnimationScrollXSpeedFactor||float|||
|uvAnimationScrollYSpeedFactor||float|||
|uvAnimationRotationSpeedFactor||float|||

https://github.com/vrm-c/vrm-specification

___
## Example of glTF JSON structure


### glTF.extensions
```json
JSON{
    "asset": {
    },
    "buffers": [
    ],
    "extensionsUsed": [
        "VGO",
        "VGO_nodes",
        "VGO_materials",
        "KHR_materials_unlit",
        "VRMC_materials_mtoon"
    ],
    "extensionsRequired": [
        "VGO",
        "VGO_nodes",
        "VGO_materials",
        "KHR_materials_unlit",
        "VRMC_materials_mtoon"
    ],
    "extensions": {
        "VGO": {
            "meta": {
                "generatorName": "UniVGO",
                "generatorVersion": "0.6.0",
                "specVersion": "0.4"
            },
            "right": {
                "title": "Test Stage",
                "author": "Izayoi Jiichan",
                "organization": "Izayoi",
                "createdDate": "2020-01-01",
                "updatedDate": "2020-01-17",
                "version": "1.3",
                "distributionUrl": "https://github.com/izayoijiichan/VGO",
                "licenseUrl": "https://github.com/izayoijiichan/VGO/blob/master/UniVgo/LICENSE.md"
            }
        }
    },
    "extras": {}
}
```

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
                    "right": {
                        "title": "Capsule1",
                        "author": "Izayoi Jiichan",
                        "organization": "",
                        "createdDate": "2020-01-01",
                        "updatedDate": "2020-01-17",
                        "version": "0.4",
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

### glTF.materials.[*].extensions
```json
JSON{
    "materials": [
        {
            "name": "MtoonMaterial1",
            "pbrMetallicRoughness": {
                "baseColorTexture": {
                    "index": 0,
                    "texCoord": 0
                },
                "baseColorFactor": [ 1, 1, 1, 1 ],
                "metallicFactor": 0,
                "roughnessFactor": 0.9
            },
            "alphaMode": "OPAQUE",
            "alphaCutoff": 0.5,
            "doubleSided": false,
            "extensions": {
                "KHR_materials_unlit": {},
                "VGO_materials":{
                    "shaderName":"VRM/MToon"
                },
                "VRMC_materials_mtoon":{
                    "version":"32",
                    "renderMode":"opaque",
                    "cullMode":"back",
                    "renderQueueOffsetNumber":0,
                    "litFactor":[ 0.811,0.916,0.723,1.0 ],
                    "litMultiplyTexture":0,
                    "shadeFactor":[ 0.933,0.620,0.711,1.0 ],
                    "shadeMultiplyTexture":-1,
                    "cutoutThresholdFactor":0.5,
                    "shadingShiftFactor":0.0,
                    "shadingToonyFactor":0.9,
                    "shadowReceiveMultiplierFactor":1.0,
                    "shadowReceiveMultiplierMultiplyTexture":-1,
                    "litAndShadeMixingMultiplierFactor":1.0,
                    "litAndShadeMixingMultiplierMultiplyTexture":-1,
                    "lightColorAttenuationFactor":0.0,
                    "giIntensityFactor":0.1,
                    "normalTexture":-1,
                    "normalScaleFactor":1.0,
                    "emissionFactor":[ 0.0,0.0,0.0,1.0 ],
                    "emissionMultiplyTexture":-1,
                    "additiveTexture":-1,
                    "rimFactor":[ 0.0,0.0,0.0,1.0 ],
                    "rimMultiplyTexture":-1,
                    "rimLightingMixFactor":0.0,
                    "rimFresnelPowerFactor":1.0,
                    "rimLiftFactor":0.0,
                    "outlineWidthMode":"screenCoordinates",
                    "outlineWidthFactor":0.5,
                    "outlineWidthMultiplyTexture":-1,
                    "outlineScaledMaxDistanceFactor":1.0,
                    "outlineColorMode":"fixedColor",
                    "outlineFactor":[ 0.0,0.0,0.0,1.0 ],
                    "outlineLightingMixFactor":1.0,
                    "mainTextureLeftBottomOriginScale":[ 1.0,1.0 ],
                    "mainTextureLeftBottomOriginOffset":[ 0.0,0.0 ],
                    "uvAnimationMaskTexture":-1,
                    "uvAnimationScrollXSpeedFactor":0.0,
                    "uvAnimationScrollYSpeedFactor":0.0,
                    "uvAnimationRotationSpeedFactor":0.0
                }
            }
        }
    ]
}
```

___
## Tools

### UniVGO

A tool for creating\/exporting\/importing\/loading VGO file.

Please refer to UniVGO [README.md](https://github.com/izayoijiichan/VGO/blob/master/UniVgo/README.md).

### VGO Parameter Viewer

A tool to check inside the VGO file.

It is distributed at the following URL.

https://github.com/izayoijiichan/vgo.parameter.viewer

___
Last updated: 17 January, 2020  
Editor: Izayoi Jiichan

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
