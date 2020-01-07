# VGO

VGO is a 3D data format for Unity that can store Collider and Rigidbody information.

## Features
- glTF (GLB) 2.0 extended format.
- Added extended definitions for Collider, Rigidbody and rights information to nodes.  
- Unity GameObject Transform, Rigidbody, Collider, PhysicMaterial, Mesh, Material, Texture can be saved.
___
## JSON schema of glTF

The following extended definitions are added:

### glTF.extensionsUsed

|Definition name|Description|
|:---|:---|
|KHR_materials_unlit|Declares that Unlit shaders can be used for materials.|
|VGO|Declaration to use VGO.|
|VGO_nodes|Declaration to use VGO_nodes.|

### glTF.extensionsRequired

|Definition name|Description|
|:---|:---|
|KHR_materials_unlit|Requires support for the KHR_materials_unlit extension.|
|VGO|Requires support for VGO extensions.|
|VGO_nodes|Requires support for VGO_nodes extension.|

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
|right|VGO rights information|

### glTF.materials.[*].extensions

|definition name|description|
|:---|:---|
|KHR_materials_unlit|Unlit material information|

___
## Extended definition details

### vgo.meta

|definition name|description|type|fixed value|
|:---|:---|:---:|:---:|
|generatorName|The name of the generation tool.|string|UniVGO|
|generatorVersion|The generation tool version.|string|0.4.0|
|specVersion|VGO specification version.|string|0.2|

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

### node.vgo.collier

|definition name|description|type|setting value|Box|Capsule|Sphere|
|:---|:---|:---:|:---:|:---:|:---:|:---:|
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
|useGravity|Whether the object is affected by gravity.|boolean|true / false|
|isKinematic|Whether physics affects the rigid body.|boolean|true / false|
|interpolation|The type of completion.|string|None / Interpolate / Extrapolate|
|collisionDetectionMode|Collision detection mode.|string|Discrete / Continuous / ContinuousDynamic / ContinuousSpeculative|
|constraints|The flags that restricts the movement of the rigid body.|int|FreesePositionX(2) \| FreesePositionY(4) \| FreesePositionZ(8) \| FreeseRotationX(16) \| FreeseRotationY(32) \| FreeseRotationZ(64)|

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
        "KHR_materials_unlit",
        "VGO",
        "VGO_nodes"
    ],
    "extensionsRequired": [
        "KHR_materials_unlit",
        "VGO",
        "VGO_nodes"
    ],
    "extensions": {
        "VGO": {
            "meta": {
                "generatorName": "UniVGO",
                "generatorVersion": "0.4.0",
                "specVersion": "0.2"
            },
            "right": {
                "title": "Test Stage",
                "author": "Izayoi Jiichan",
                "organization": "Izayoi",
                "createdDate": "2020-01-01",
                "updatedDate": "2020-01-08",
                "version": "1.1",
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
                            "type": "Capsule",
                            "enabled": false,
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
                    "right": {
                        "title": "Capsule1",
                        "author": "Izayoi Jiichan",
                        "organization": "",
                        "createdDate": "2020-01-01",
                        "updatedDate": "2020-01-08",
                        "version": "0.2",
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
                "KHR_materials_unlit": {}
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
Last updated: 8 January, 2020  
Editor: Izayoi Jiichan

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
