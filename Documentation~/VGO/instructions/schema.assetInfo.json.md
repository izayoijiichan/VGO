# VGO

## Asset Info schema

### vgo.assetInfo
|definition name|description|type|required|
|:---|:---|:---|:---:|
|generator|Information about the generator for this VGO model.|vgo.generatorInfo||
|right|The rights information.|vgo.right||
|extensions|The extensions.|object||
|extensionsUsed|Names of extensions used somewhere in this asset (include layout).|string[]||

#### vgo.generatorInfo

|definition name|description|type|required|value|
|:---|:---|:---:|:---:|:---|
|name|The name of the generation tool.|string||UniVGO|
|version|The generation tool version.|string||2.0.0|

#### vgo.right

|definition name|description|type|required|remarks|
|:---|:---|:---:|:---:|:---|
|title|The name of the work.|string|||
|author|The name of the creator.|string|||
|organization|The organization to which the creator belongs.|string|||
|createdDate|The creation date of the work.|string||There is no format specification.|
|updatedDate|The update date of the work.|string||There is no format specification.|
|version|The version of the work.|string||There is no format specification.|
|distributionUrl|Distribution URL.|string||URL format|
|licenseUrl|The URL where the license is written.|string||URL format|


#### AssetInfo (example)

```json
{
    "generator": {
        "name": "UniVGO",
        "version": "2.0.0"
    },
    "right": {
        "title": "Sample 3D Model",
        "author": "Izayoi Jiichan",
        "organization": "",
        "createdDate": "2020-08-20",
        "updatedDate": "2020-08-20",
        "version": "1.0.0",
        "distributionUrl": "",
        "licenseUrl": ""
    },
    "extensions": [],
    "extensionsUsed": []
}
```
___
Last updated: 20 August, 2020  
Editor: Izayoi Jiichan

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
