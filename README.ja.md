# VGO

VGOとは、新しい形の3Dデータフォーマットです。

___
## 特徴

- `Node`, `Transform`, `Rigidbody`, `Collider`, `Mesh`, `Blend Shape`, `Material`, `Texture` といった3Dモデルの基本情報を保存することができます。
- Unityでの使用を意識して `Human Avatar`, `Spring Bone`, `Animation`, `Cloth`, `Light`, `Particle System`, `Skybox` の情報も保存することができます。
- アプリケーションでの使用を意識して `Blend Shape Preset` の情報も保存することができます。
- ファイルフォーマットはベースにIFFチャンクを採用しています。
- 内部データとして `JSON`, `BSON`, `Binary` を使用します。
- 座標系は右手系、左手系のどちらのデータも持つことができます。
- 独自の定義拡張（チャンク、スキーマ）が可能な仕様となっています。
- 部分暗号化に対応した仕様となっています。
- リソースはファイル内に含めることを基本としていますが、別ファイルに切り出すこともできます。
- リソースは sparse でデータを圧縮することができ、またより強力な sparse を独自に定義しています。
- glTFとの直接の互換性はありません。
- Unity Editorへデータを展開することで glTF を含む他の形式への変換は可能です。

## 翻訳

[English](https://github.com/izayoijiichan/VGO/blob/main/README.md).

## ファイル拡張子

|拡張子|説明|必要|
|:--:|:--|:--:|
|.vgo|VGOファイルです。|必要|
|.vgk|暗号化したVGOファイルを復号するためのキーファイルです。|任意|
|(.bin)|リソース ファイルです。|任意|

## チャンク

|タイプ|名前|説明|
|:--|:--|:--|
|VGO|Header|ファイルヘッダーです。|
|IDX|Index|チャンクのインデックスを保持します。|
|COMP|Composer|3Dモデルを構築するためのチャンクの組み合わせを保持します。|
|AIXX|Asset Info|アセット情報を保持します。|
|LAXX|Layout|3Dモデル設計情報を保持します。|
|RAXX|Resource Accessor|リソースへのアクセス情報を保持します。|
|REXX|Resource|リソースを保持します。|
|CXXX|Crypt|暗号情報を保持します。|

[チャンク詳細](https://github.com/izayoijiichan/VGO/blob/main/Documentation~/VGO/instructions/chunk.md)

## データ スキーマ

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
    - meshRenderer
    - skybox
    - light
    - right
    - extensions
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

## データ スキーマの説明

- [アセット情報](https://github.com/izayoijiichan/VGO/blob/main/Documentation~/VGO/instructions/schema.assetInfo.json.md)
- [レイアウト](https://github.com/izayoijiichan/VGO/blob/main/Documentation~/VGO/instructions/schema.layout.json.md)
- [レイアウト（アニメーション）](https://github.com/izayoijiichan/VGO/blob/main/Documentation~/VGO/instructions/schema.layout.animation.json.md)
- [レイアウト（クロス）](https://github.com/izayoijiichan/VGO/blob/main/Documentation~/VGO/instructions/schema.layout.cloth.json.md)
- [レイアウト（パーティクル）](https://github.com/izayoijiichan/VGO/blob/main/Documentation~/VGO/instructions/schema.layout.particle.json.md)
- [レイアウト（スプリングボーン）](https://github.com/izayoijiichan/VGO/blob/main/Documentation~/VGO/instructions/schema.layout.springBoneInfo.json.md)
- [リソース](https://github.com/izayoijiichan/VGO/blob/main/Documentation~/VGO/instructions/schema.resource.json.md)

## JSON 仕様

- VGO JSON Schema: [2.5](https://github.com/izayoijiichan/VGO/tree/main/Documentation~/VGO/specification/2.5/schema)

## ツール

### UniVGO

VGOファイルを生成／出力／取り込み／ロードするためのツールです。

ボタンをクリックするだけで簡単にVGOファイルを出力できます。

![image1](https://github.com/izayoijiichan/VGO/blob/main/Documentation~/UniVGO/Images/500_Export.png)

[Wiki](https://github.com/izayoijiichan/VGO/wiki)

___
最終更新日：2023年7月17日  
編集者：十六夜おじいちゃん

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
