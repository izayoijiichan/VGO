# VGO

VGOとは、新しい形の3Dデータフォーマットです。

![3D Model](https://img.shields.io/badge/3D%20Model-VGO-B89A13.svg?style=flat)
![VGO](https://img.shields.io/badge/VGO-2.5-8EAC50.svg?style=flat)
[![UniVGO](https://img.shields.io/github/v/release/izayoijiichan/VGO?label=UniVGO)](https://github.com/izayoijiichan/VGO/releases)
![Unity](https://img.shields.io/badge/Unity-2020%7e2023-2196F3.svg?logo=unity&style=flat)
![C#](https://img.shields.io/badge/C%23-8.0%7e9.0-058E0C.svg?logo=csharp&style=flat)
![license](https://img.shields.io/github/license/izayoijiichan/VGO)
[![wiki](https://img.shields.io/badge/GitHub-wiki-181717.svg?logo=github&style=flat)](https://github.com/izayoijiichan/VGO/wiki)

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

## 実験

- テクスチャー内のイメージ タイプ (メディア タイプ / MIME タイプ) として、通常の`PNG`、`JPEG`に加えて`WebP`形式をサポートします。

## 翻訳

[English](https://github.com/izayoijiichan/VGO/blob/main/README.md).

## ファイル拡張子

|拡張子|説明|必要|
|:--:|:--|:--:|
|.vgo|VGOファイルです。|必要|
|.vgk|暗号化したVGOファイルを復号するためのキーファイルです。|任意|
|(.bin)|リソース ファイルです。|任意|

## ツール

### UniVGO

VGOファイルを生成／出力／取り込み／ロードするためのツールです。

ボタンをクリックするだけで簡単にVGOファイルを出力できます。

![image1](https://github.com/izayoijiichan/VGO/blob/main/Documentation~/UniVGO/Images/500_Export.png)

VGOファイルをアセット内に配置するだけで簡単に取り込み、復元することができます。

![image2](https://github.com/izayoijiichan/VGO/blob/main/Documentation~/UniVGO/Images/620_Import.png)

少しのスクリプトを書くだけでVGOファイルをランタイムロードすることができます。


~~~csharp
    using System;
    using UnityEngine;
    using UniVgo2;

    public class VgoRuntimeLoader : MonoBehaviour
    {
        private readonly VgoImporter _VgoImporter = new();

        [SerializeField]
        private string _FilePath = string.Empty;

        private VgoModelAsset? _VgoModelAsset;

        private void Start()
        {
            _VgoModelAsset = _VgoImporter.Load(_FilePath);
        }

        private void OnDestroy()
        {
            _VgoModelAsset?.Dispose();
        }
    }
~~~

[Wiki](https://github.com/izayoijiichan/VGO/wiki)

___
最終更新日：2023年8月24日  
編集者：十六夜おじいちゃん

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
