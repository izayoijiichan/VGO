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
|Unity 2019.3|○|○|○|未確認|未確認|
|Unity 2020.1a|未確認|未確認|未確認|未確認|未確認|

2020年の開始時点では `Unity 2019.3` の Windows 環境にて開発＆確認を行っています。

### 必要パッケージ

|パッケージ名|所有者|リポジトリー|仕様バージョン|プログラム バージョン|リリース日|
|:---:|:---:|:---:|:---:|:---:|:---:|
|newtonsoft-json-for-unity|jillejr|GitHub|12.0.3|12.0.301|2020年1月20日|
|UniVGO|IzayoiJiichan|GitHub|VGO 0.5|0.7.0|2020年1月23日|

___
## インストール

### インストール手順（サンプルプロジェクトを使用する場合）


#### 1. サンプルプロジェクトのダウンロード

UniVGO のサンプルプロジェクトをダウンロードします。

https://github.com/izayoijiichan/univgo.sample.unity.project

#### 2. Unity のインストール

Unity Hub にて`Unity 2019.3.0f6`をインストールします。

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

#### 2. Newtonsoft.JSON のインストール

Newtonsoft.JSON をパッケージとしてプロジェクトに取り込みます。  
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
    "jillejr.newtonsoft.json-for-unity": "12.0.301",
    "com.unity.modules.ai": "1.0.0",
    ...
    "com.unity.modules.xr": "1.0.0"
  }
}
```

#### 3. UniVGO のインストール

UniVGO をプロジェクトに取り込みます。  

AまたはBのいずれかを行ってください。

##### A) package.json にて管理する場合

`<Project>/Packages/package.json` に以下の記述を行います。  

```json
{
  "dependencies": {
    "com.unity.ugui": "1.0.0",
    "izayoi.univgo": "https://github.com/izayoijiichan/VGO.git#v0.7.0",
    "jillejr.newtonsoft.json-for-unity": "12.0.301",
    "com.unity.modules.ai": "1.0.0",
    ...
    "com.unity.modules.xr": "1.0.0"
  }
}
```

##### B) Packages フォルダーにて管理する場合  

以下のURLより圧縮ファイルをダウンロードします。  

UniVGO  
https://github.com/izayoijiichan/VGO/releases  

ファイルを解凍して`Packages`フォルダーに格納します。

```
  <Project>
    Packages
      izayoi.univgo@0.7.0-preview
        DepthFirstScheduler
        MToon
        ShaderProperty
        UniGLTFforUniVgo
        UniStandardParticle
        UniUnlit
        UniVgo
```

フォルダーの名前は変更しても構いません。

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
|DepthFirstScheduler|深さ優先スケジューラー|*|*|
|MToon|MToon シェーダー ユーティリティー|*|*|
|MToon.Editor|MToon シェーダー ユーティリティー|-|*|
|ShaderProperty.Runtime|シェーダーのプロパティー情報|*|*|
|UniGLTFforUniVgo|UniGLTF（UniVGO用）|*|*|
|UniStandardParticle|Particle シェーダー ユーティリティー|*|*|
|UniUnlit.Editor|Unlit シェーダー ユーティリティー|-|*|
|UniVgo|VGO メインプログラム|*|*|
|UniVgo.Editor|VGO の入出力|-|*|

- UniVgo, UniVgo.Editor それぞれについて、依存関係にあるDLLに * を付けています。
- DepthFirstScheduler, MToon, ShaderProperty, UniUnlit は UniVRM (©vrm-c) に梱包されているプログラムです。
- UniVRM と UniVGO を併用する場合は、UniVgo を取得した際に梱包されていた 重複するファイル（DepthFirstScheduler, MToon, ShaderProperty, UniUnlit）を削除する必要があります。
  また、それにより UniVgo にてエラーが表示される場合、UniVgo, UniGLTFforUniVgo を `Assets`フォルダーに移動してください。

___
最終更新日：2020年2月6日  
編集者：十六夜おじいちゃん

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
