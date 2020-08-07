# UniVGO インストール マニュアル

Unity Editor に UniVGO をインストールするためのマニュアルです。

___
## システム要件

### Unity のバージョン

|バージョン|Win (Editor)|Win (Mono)|Win (IL2CPP)|Android (IL2CPP)|iOS|
|:---|:---:|:---:|:---:|:---:|:---:|
|Unity 2018.4|未確認|未確認|未確認|未確認|未確認|
|Unity 2019.1|未確認|未確認|未確認|未確認|未確認|
|Unity 2019.2|未確認|未確認|未確認|未確認|未確認|
|Unity 2019.3|○|○|○|○|未確認|
|Unity 2019.4|○|○|○|○|未確認|
|Unity 2020.1|○|未確認|○|未確認|未確認|

2020年8月の時点では `Unity 2019.4` の Windows 環境にて開発＆確認を行っています。

### 必要パッケージ

|パッケージ名|所有者|リポジトリー|仕様バージョン|プログラム バージョン|リリース日|
|:---|:---:|:---:|:---:|:---:|:---:|
|newtonsoft-json-for-unity|jillejr|GitHub|12.0.3|12.0.301|2020年1月20日|
|VgoGltf|IzayoiJiichan|GitHub|-|1.0.0|2020年8月7日|
|NewtonGltf|IzayoiJiichan|GitHub|-|1.0.0|2020年8月7日|
|NewtonGltf.Vgo.Extensions|IzayoiJiichan|GitHub|-|1.0.0|2020年8月7日|
|UniShaders|IzayoiJiichan|GitHub|-|1.0.0|2020年8月7日|
|VRMShaders|vrm-c|GitHub|VRM 0.0|0.56.0|2020年7月3日|
|UniVGO|IzayoiJiichan|GitHub|VGO 0.6|1.0.0|2020年8月7日|

___
## インストール

### インストール手順（サンプルプロジェクトを使用する場合）


#### 1. サンプルプロジェクトのダウンロード

UniVGO のサンプルプロジェクトをダウンロードします。

https://github.com/izayoijiichan/univgo.sample.unity.project

#### 2. Unity のインストール

Unity Hub にて`Unity 2019.4.0f1`をインストールします。

#### 3. プロジェクトを読み込み

Unity Hub にて、1でダウンロードしたサンプルプロジェクトをリストに追加します。  
指定するフォルダーはプロジェクトフォルダーです。


### インストール手順（自分でプロジェクトを作成する場合）


#### 1. 新規プロジェクトの作成

Unity Editor または Unity Hub にて3Dの新規プロジェクトを作成します。

```
    <Project>
        Assets
        Packages
        ProjectSettings
```

#### 2. 必要パッケージ のインストール

UniVGO及び依存パッケージをプロジェクトに取り込みます。  
`<Project>/Packages/package.json` に以下の記述を行います。  
追加する位置に気を付ける必要があります。

```json
{
  "scopedRegistries": [
    {
      "name": "Packages from jillejr",
      "url": "https://npm.cloudsmith.io/jillejr/newtonsoft-json-for-unity/",
      "scopes": ["jillejr"]
    }
  ],
  "dependencies": {
    "com.unity.ugui": "1.0.0",
    "com.vrmc.vrmshaders": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRMShaders#v0.56.0",
    "izayoi.newton.gltf": "https://github.com/izayoijiichan/newton.gltf.git#v1.0.0",
    "izayoi.newton.gltf.vgo.extensions": "https://github.com/izayoijiichan/newton.gltf.vgo.extensions.git#v1.0.0",
    "izayoi.unishaders": "https://github.com/izayoijiichan/UniShaders.git#v1.0.0",
    "izayoi.univgo": "https://github.com/izayoijiichan/VGO.git#v1.0.0",
    "izayoi.vgo.gltf": "https://github.com/izayoijiichan/vgo.gltf.git#v1.0.0",
    "jillejr.newtonsoft.json-for-unity": "12.0.301",
    "com.unity.modules.ai": "1.0.0",
    ...
    "com.unity.modules.xr": "1.0.0"
  }
}
```


### インストール完了の確認

1. プロジェクトを Unity Editor で開きます。
2. Unity Editor のコンソールにエラーが表示されないないことを確認します。
3. Unity Editor のメニューバーに `[Tools]` > `[UniVGO]` の項目が表示されていることを確認します。

### エラーが発生した場合

エラーが発生する原因としては以下のようなことが考えられます。
- パッケージのバージョンが異なる
- ファイルが重複または不足している
- `asmdef` の設定が変更されている
- `asmdef.meta` の設定が変更されている
- コンポーネントの `.meta` の guid が変更されている

___
## その他の情報

### ライブラリー（アセンブリ）について

パッケージをプロジェクトにインストールすると  
スクリプトが自動的にコンパイルされ、以下のDLLが生成されます。

|アセンブリ|説明|UniVgo|UniVgo.Editor|
|:---|:---|:---:|:---:|
|MToon|MToon シェーダー ユーティリティー|*|*|
|MToon.Editor|MToon シェーダー ユーティリティー|-|*|
|NewtonGltf|Newton.JSON用 glTF 基本スキーマ|*|*|
|NewtonGltf.Vgo.Extensions|Newton.JSON用 glTF.VGO 拡張スキーマ|*|*|
|ShaderProperty.Runtime|シェーダーのプロパティー情報|*|*|
|UniShader.Skybox.Utility|Skybox シェーダー ユーティリティー|*|*|
|UniShader.Standard.Particle.Utility|Particle シェーダー ユーティリティー|*|*|
|UniShader.Standard.Utility|Standard シェーダー ユーティリティー|*|*|
|UniUnlit|Unlit シェーダー ユーティリティー|*|*|
|UniUnlit.Editor|Unlit シェーダー ユーティリティー|-|*|
|UniVgo|VGO メインプログラム|*|*|
|UniVgo.Editor|VGO のエディター上での入出力操作|-|*|
|VgoGltf|glTF 基本定義|*|*|

- UniVgo, UniVgo.Editor それぞれについて、依存関係にあるDLLに * を付けています。
- MToon, ShaderProperty, UniUnlit はVRMShaders (©vrm-c) に梱包されているプログラムです。
- UniVRM と UniVGO を併用する場合は、UniVgo を取得した際に梱包されていた重複するファイルを削除する必要があります。UniGLTFフォルダーにあるシェーダー類が該当します。
___
最終更新日：2020年8月7日  
編集者：十六夜おじいちゃん

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
