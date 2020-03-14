# VGO materials

## JSON schema of glTF

### glTF.materials.[*].extensions

|definition name|description|type|
|:---|:---|:---|
|VGO_materials|VGO material information|VGO_materials|
|VGO_materials_particle|Particle material information|VGO_materials_particle|
|KHR_materials_unlit|Unlit material information|KHR_materials_unlit|
|VRMC_materials_mtoon|MToon material information|VRMC_materials_mtoon|

### VGO_materials

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|shaderName|The name of shader.|string|||

### VGO_materials_particle

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|renderMode||string|Opaque / Cutout / Fade / Transparent / Additive / Subtractive / Modulate|Opaque|
|colorMode||string|Multiply / Additive / Subtractive / Overlay / Color/ Difference|Multiply|
|flipBookMode||string|Simple / Blended|Simple|
|cullMode||string|Off / (Front) / Back|Off|
|softParticlesEnabled||bool|true / false|false|
|softParticleFadeParams||float[4]|||
|cameraFadingEnabled||bool|true / false|false|
|cameraFadeParams||float[4]|||
|distortionEnabled||bool|true / false|false|
|grabTextureIndex||int|||
|distortionStrengthScaled||float|[0.0, 1.0]||
|distortionBlend||float|[0.0, 1.0]||
|colorAddSubDiff||float[4]|r, g, b, a||
|mainTexIndex||int|||
|mainTexSt||float[4]|||
|color||float[4]|r, g, b, a||
|cutoff||float|[0.0, 1.0]||
|metallicGlossMapIndex||int|||
|metallic||float|[0.0, 1.0]||
|glossiness||float|[0.0, 1.0]||
|bumpMapIndex||int|||
|bumpScale||float|[0.0, 1.0]||
|lightingEnabled||bool|true / false||
|emissionEnabled||bool|true / false||
|emissionColor||float[3]|r, g, b||
|emissionMapIndex||int|||

### VRMC_materials_skybox

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|sunDisk||string|None / Simple / HighQuality|None|
|sunSize||float|[0.0, 1.0]||
|sunSizeConvergence||int|[1, 10]||
|atmosphereThickness||float|[0.0, 5.0]||
|tint||float[4]|r, g, b, a||
|skyTint||float[4]|r, g, b, a||
|groundColor||float[4]|r, g, b, a||
|exposure||float|[0.0, 8.0]||
|rotation||int|[0, 360]||
|frontTexIndex||int|||
|backTexIndex||int|||
|leftTexIndex||int|||
|rightTexIndex||int|||
|upTexIndex||int|||
|downTexIndex||int|||
|texIndex||int|||
|mainTexIndex||int|||
|mapping||string|SixFramesLayout / LatitudeLongitudeLayout|SixFramesLayout|
|imageType||string|Degrees360 / Degrees180|Degrees360|
|mirrorOnBack||bool|||
|layout||string|None / SideBySide / OverUnder|None|

### KHR_materials_unlit

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|

https://github.com/KhronosGroup/glTF/tree/master/extensions/2.0/Khronos/KHR_materials_unlit

### VRMC_materials_mtoon

|definition name|description|type|setting value|default value|
|:---|:---|:---:|:---:|:---:|
|version||string||32|
|renderMode||string|opaque / cutout / transparent / transparentWithZWrite|opaque|
|cullMode||string|off / flont / back|off|
|renderQueueOffsetNumber||int|||
|litFactor||float[4]|r, g, b, a||
|litMultiplyTexture||int|||
|shadeFactor||float[4]|r, g, b, a||
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
|emissionFactor||float[3]|r, g, b||
|emissionMultiplyTexture||int|||
|additiveTexture||int|||
|rimFactor||float[4]|r, g, b, a||
|rimMultiplyTexture||int|||
|rimLightingMixFactor||float|[0.0, 1.0]||
|rimFresnelPowerFactor||float|[0.0, 100.0]||
|rimLiftFactor||float|[0.0, 1.0]||
|outlineWidthMode||string|none / worldCoordinates / screenCoordinates|none|
|outlineWidthFactor||float|[0.01, 1.0]||
|outlineWidthMultiplyTexture||int|||
|outlineScaledMaxDistanceFactor||float|[1.0, 10.0]||
|outlineColorMode||string|fixedColor / mixedLighting|fixedColor|
|outlineFactor||float[4]|r, g, b, a||
|outlineLightingMixFactor||float|[0.0, 1.0]||
|mainTextureLeftBottomOriginScale||float[2]|x, y||
|mainTextureLeftBottomOriginOffset||float[2]|x, y||
|uvAnimationMaskTexture||int|||
|uvAnimationScrollXSpeedFactor||float|||
|uvAnimationScrollYSpeedFactor||float|||
|uvAnimationRotationSpeedFactor||float|||

https://github.com/vrm-c/vrm-specification

___
## Example of glTF JSON structure

### glTF.materials.[*].extensions

```json
JSON{
    "materials": [
        {
            "name": "UnlitMaterial1",
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
                "VGO_materials":{
                    "shaderName":"Unlit/Texture"
                },
                "KHR_materials_unlit": {}
            },
            "extras": {}
        },
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
            },
            "extras": {}
        },
        {
            "name": "ParticleMaterial1",
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
                "VGO_materials":{
                    "shaderName":"Particles/Standard Surface"
                },
                "VGO_materials_particle":{
                    "renderMode":"Fade",
                    "colorMode":"Multiply",
                    "flipBookMode":"Simple",
                    "cullMode":"Back",
                    "softParticlesEnabled":true,
                    "softParticleFadeParams":[ 0.0,1.0,0.0,0.0 ],
                    "cameraFadingEnabled":true,
                    "cameraFadeParams":[ 1.0,1.0,0.0,0.0 ],
                    "grabTextureIndex":-1,
                    "distortionEnabled":true,
                    "distortionStrengthScaled":0.1,
                    "distortionBlend":0.5,
                    "colorAddSubDiff":[ 0.0,0.0,0.0,0.0 ],
                    "mainTexIndex":4,
                    "mainTexSt":[ 1.0,1.0,0.0,0.0 ],
                    "color":[ 0.0,0.0,0.0,1.0 ],
                    "cutoff":1.0,
                    "metallicGlossMapIndex":-1,
                    "metallic":0.0,
                    "glossiness":0.5,
                    "bumpMapIndex":-1,
                    "bumpScale":1.0,
                    "lightingEnabled":true,
                    "emissionEnabled":true,
                    "emissionColor":[ 1.0,0.0,1.0,1.0 ],
                    "emissionMapIndex":-1,
                }
            },
            "extras": {}
        },
        {
            "name":"Skybox6SidedMaterial",
            "pbrMetallicRoughness":{},
            "alphaCutoff":0.5,
            "doubleSided":false,
            "extensions":{
                "VGO_materials":{
                    "shaderName":"Skybox/6 Sided"
                },
                "VGO_materials_skybox":{
                    "tint":[ 0.214,0.214,0.214,0.5 ],
                    "exposure":1.0,
                    "rotation":0,
                    "frontTexIndex":1,
                    "backTexIndex":2,
                    "leftTexIndex":3,
                    "rightTexIndex":4,
                    "upTexIndex":5,
                    "downTexIndex":6
                }
            }
        }
    ]
}
```
___
Last updated: 15 March, 2020  
Editor: Izayoi Jiichan

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
