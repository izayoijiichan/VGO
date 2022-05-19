# VGO 2

VGO version 2.0 is a new type of 3D data format.

## Features

- A proprietary format developed from VGO 1.0.
- You can save basic information of 3D model such as `Node`, `Transform`, `Rigidbody`, `Collider`, `Mesh`, `BlendShape`, `Material`, `Texture`.
- You can also save the information of `Human Avatar`, `SpringBone`, `Animation`, `Cloth`, `Light`, `ParticleSystem`, `Skybox` for use in Unity.
- You can also save the information of `BlendShapePreset` for use in the application.
- The file format uses IFF chunk as the base.
- Use `JSON`, `BSON`, `Binary` as internal data.
- The geometry coordinate system can have both right-handed and left-handed data.
- It is a specification that allows its own definition extension (chunk, schema).
- It is designed to support partial encryption.
- Resources are basically included in a file, but they can be cut out in another file.
- Resources can compress data with sparse, and have their own definition of the more powerful sparse.
- There is no direct compatibility with glTF.
- It is possible to convert to other formats including glTF by expanding the data in Unity Editor.

## Translation

[Japanese](https://github.com/izayoijiichan/VGO2/blob/main/README.ja.md).

## File extension

|extension|description|required|
|:--:|:--|:--:|
|.vgo|This is a VGO file.|true|
|.vgk|It is a key file to decrypt the encrypted VGO file.|false|
|(.bin)|Resource file.|false|

## Chunk

|type|name|description|
|:--|:--|:--|
|VGO|Header|File header.|
|IDX|Index|Holds the index of the chunk.|
|COMP|Composer|Holds a combination of chunks to build a 3D model.|
|AIXX|Asset Info|Holds asset information.|
|LAXX|Layout|Holds 3D model layout information.|
|RAXX|Resource Accessor|Holds access information to the resource.|
|REXX|Resource|Holds a resource.|
|CXXX|Crypt|Holds cryptographic information.|

[Chunk details](https://github.com/izayoijiichan/VGO2/blob/main/Documentation~/VGO/instructions/chunk.md)

## Data schema

- assetInfo
  - generator
  - right
  - extensions
  - extensionsUsed

- layout
  - nodes
    - animator
      - humanAvatar
    - animation
    - rigidbody
    - colliders
      - collider
    - skybox
    - light
    - right
  - skins
  - meshes
  - materials
  - textures
  - animationClips
  - colliders
  - clothes
  - lights
  - particles
  - springBoneInfo
  - extensions

## Data schema description

- [Asset Info](https://github.com/izayoijiichan/VGO2/blob/main/Documentation~/VGO/instructions/schema.assetInfo.json.md)
- [Layout](https://github.com/izayoijiichan/VGO2/blob/main/Documentation~/VGO/instructions/schema.layout.json.md)
- [Layout (animation)](https://github.com/izayoijiichan/VGO2/blob/main/Documentation~/VGO/instructions/schema.layout.animation.json.md)
- [Layout (cloth)](https://github.com/izayoijiichan/VGO2/blob/main/Documentation~/VGO/instructions/schema.layout.cloth.json.md)
- [Layout (particle)](https://github.com/izayoijiichan/VGO2/blob/main/Documentation~/VGO/instructions/schema.layout.particle.json.md)
- [Layout (spring bone)](https://github.com/izayoijiichan/VGO2/blob/main/Documentation~/VGO/instructions/schema.layout.springBoneInfo.json.md)
- [Resource](https://github.com/izayoijiichan/VGO2/blob/main/Documentation~/VGO/instructions/schema.resource.json.md)

## JSON specification

- VGO JSON Schema: [2.4](https://github.com/izayoijiichan/VGO2/tree/main/Documentation~/VGO/specification/2.4/schema)

## Tools

### UniVGO2

A tool for creating\/exporting\/importing\/loading VGO file.

You can easily export a VGO file with the click of a button.

![image1](https://github.com/izayoijiichan/vgo2/blob/main/Documentation~/UniVGO/Images/500_Export.png)

[Installation.md](https://github.com/izayoijiichan/VGO2/blob/main/Documentation~/UniVGO/Installation.md)

[Usage.md](https://github.com/izayoijiichan/VGO2/blob/main/Documentation~/UniVGO/Usage.md)

___
Last updated: 20 May, 2022  
Editor: Izayoi Jiichan

*Copyright (C) 2020-2022 Izayoi Jiichan. All Rights Reserved.*
