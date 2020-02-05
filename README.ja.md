# VGO

VGO とは、Collider と Rigidbody の情報を格納可能な Unity 向け3Dデータフォーマットです。

___
## 特徴

- glTF (GLB) 2.0 の拡張フォーマットになります。
- Unity の `GameObject`, `Transform`, `Rigidbody`, `Collider`, `Mesh`, `Material`, `Texture`, `Light`, `ParticleSystem` を保存することができます。
- シェーダー設定は `Standard`, `Particle`, `Unlit`, `MToon` を保存することができます。

___
## 翻訳

[English](https://github.com/izayoijiichan/VGO/blob/master/README.md).

___
## 実装

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
## 仕様

- glTF JSON Schema: [2.0](https://github.com/KhronosGroup/glTF/tree/master/specification/2.0/schema)
- VGO JSON Schema: [0.5](https://github.com/izayoijiichan/VGO/tree/master/Documentation~/VGO/specification/0.5/schema)
- KHR_materials_unlit: [2.0](https://github.com/KhronosGroup/glTF/tree/master/extensions/2.0/Khronos/KHR_materials_unlit)
- VRMC_materials_mtoon: [1.0](https://github.com/vrm-c/vrm-specification/tree/master/specification/VRMC_materials_mtoon-1.0_draft)

___
## 説明書

- [glTF.extensions](https://github.com/izayoijiichan/VGO/blob/master/Documentation~/VGO/instructions/schema.json.md)
- [glTF.materials.extensions](https://github.com/izayoijiichan/VGO/blob/master/Documentation~/VGO/instructions/schema.json.materials.md)
- [glTF.nodes.extensions](https://github.com/izayoijiichan/VGO/blob/master/Documentation~/VGO/instructions/schema.json.nodes.md)

___
## ツール

### UniVGO

VGOファイルを生成／出力／取り込み／ロードするためのツールです。

[インストール方法](https://github.com/izayoijiichan/VGO/blob/master/Documentation~/UniVGO/Installation.ja.md)

[使用方法](https://github.com/izayoijiichan/VGO/blob/master/Documentation~/UniVGO/Usage.ja.md)

### VGO Parameter Viewer

VGOファイルの中身を確認するためのツールです。

以下のURLにて配布しています。

https://github.com/izayoijiichan/vgo.parameter.viewer

___
最終更新日：2020年2月6日  
編集者：十六夜おじいちゃん

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
