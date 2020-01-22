# VGO

## JSON schema of glTF

### glTF.extensionsUsed

|Definition name|Description|
|:---|:---|
|VGO|Declaration to use VGO.|
|VGO_nodes|Declaration to use VGO_nodes.|
|VGO_materials|Declaration to use VGO_materials.|
|VGO_materials_particle|Declares that Particle shaders can be used for materials.|
|KHR_materials_unlit|Declares that Unlit shaders can be used for materials.|
|VRMC_materials_mtoon|Declares that MToon shaders can be used for materials.|

### glTF.extensionsRequired

|Definition name|Description|
|:---|:---|
|VGO|Requires support for VGO extensions.|
|VGO_nodes|Requires support for VGO_nodes extension.|
|VGO_materials|Requires support for VGO_materials extension.|
|VGO_materials_particle|Requires support for the VGO_materials_particle extension.|
|KHR_materials_unlit|Requires support for the KHR_materials_unlit extension.|
|VRMC_materials_mtoon|Requires support for the VRMC_materials_mtoon extension.|

### glTF.extensions

|definition name|description|type|
|:---|:---|:---|
|VGO|VGO information|glTF.extensions.VGO|

### glTF.extensions.VGO

|definition name|description|type|
|:---|:---|:---|
|meta|VGO meta information|vgo.meta|
|right|VGO rights information|vgo.right|

### vgo.meta

|definition name|description|type|fixed value|
|:---|:---|:---:|:---:|
|generatorName|The name of the generation tool.|string|UniVGO|
|generatorVersion|The generation tool version.|string|0.7.0|
|specVersion|VGO specification version.|string|0.5|

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
        "VGO_materials_particle",
        "KHR_materials_unlit",
        "VRMC_materials_mtoon"
    ],
    "extensionsRequired": [
        "VGO",
        "VGO_nodes",
        "VGO_materials",
        "VGO_materials_particle",
        "KHR_materials_unlit",
        "VRMC_materials_mtoon"
    ],
    "extensions": {
        "VGO": {
            "meta": {
                "generatorName": "UniVGO",
                "generatorVersion": "0.7.0",
                "specVersion": "0.5"
            },
            "right": {
                "title": "Test Stage",
                "author": "Izayoi Jiichan",
                "organization": "Izayoi",
                "createdDate": "2020-01-01",
                "updatedDate": "2020-01-23",
                "version": "1.4",
                "distributionUrl": "https://github.com/izayoijiichan/VGO",
                "licenseUrl": "https://github.com/izayoijiichan/VGO/blob/master/UniVgo/LICENSE.md"
            }
        }
    },
    "extras": {}
}
```
___
Last updated: 23 January, 2020  
Editor: Izayoi Jiichan

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
