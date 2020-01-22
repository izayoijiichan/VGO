# VGO

VGO is a 3D data format for Unity that can store Collider and Rigidbody information.

## Features

- glTF (GLB) 2.0 extended format.
- Unity `GameObject`, `Transform`, `Rigidbody`, `Collider`, `Mesh`, `Material`, `Texture`, `Light` and `ParticleSystem` can be saved.
- Shader settings of `Standard`, `Particle`, `Unlit` and `MToon` can be saved.

___
## Implementation

- glTF
  - accessors
  - animations (unimplemented)
  - asset
  - buffers
  - bufferViews
  - cameras (unimplemented)
  - images
  - materials
    - extensions
      - VGO_materials
      - VGO_materials_particle
      - KHR_materials_unlit
      - VRMC_materials_mtoon
  - meshes
  - nodes
    - extensions
      - VGO_nodes
        - gameObject
        - colliders
          - collider
        - rigidbody
        - light
        - particleSystem
        - right
  - samplers
  - scene
  - scenes
  - skins
  - textures
  - extensions
    - VGO
      - meta
      - right
  - extras

___
## Specification

- glTF JSON Schema: [2.0](https://github.com/KhronosGroup/glTF/tree/master/specification/2.0/schema)
- VGO JSON Schema: [0.5](https://github.com/izayoijiichan/VGO/tree/master/Documentation~/VGO/specification/0.5/schema)
- KHR_materials_unlit: [2.0](https://github.com/KhronosGroup/glTF/tree/master/extensions/2.0/Khronos/KHR_materials_unlit)
- VRMC_materials_mtoon: [1.0](https://github.com/vrm-c/vrm-specification/tree/master/specification/VRMC_materials_mtoon-1.0_draft)

___
## Instructions

- [glTF.extensions](https://github.com/izayoijiichan/VGO/blob/master/Documentation~/VGO/instructions/schema.json.md)
- [glTF.materials.extensions](https://github.com/izayoijiichan/VGO/blob/master/Documentation~/VGO/instructions/schema.json.materials.md)
- [glTF.nodes.extensions](https://github.com/izayoijiichan/VGO/blob/master/Documentation~/VGO/instructions/schema.json.nodes.md)

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
Last updated: 23 January, 2020  
Editor: Izayoi Jiichan

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
