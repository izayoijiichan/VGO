# UniVGO インストール マニュアル

Unity Editor に UniVGO をインストールするためのマニュアルです。

___
## システム要件

### Unity のバージョン

|バージョン|Win (Editor)|Win (Mono)|Win (IL2CPP)|Android (IL2CPP)|iOS|
|:---|:---:|:---:|:---:|:---:|:---:|
|Unity 2019.1|未確認|未確認|未確認|未確認|未確認|
|Unity 2019.2|未確認|未確認|未確認|未確認|未確認|
|Unity 2019.3|未確認|未確認|未確認|未確認|未確認|
|Unity 2019.4|○|○|○|○|未確認|
|Unity 2020.1|○|○|○|○|未確認|

2020年12月の時点では `Unity 2020.1` の `Windows` `.NET Standard 2.0` 環境にて開発＆確認を行っています。


### 必要パッケージ

|パッケージ名|所有者|リポジトリー|仕様バージョン|プログラム バージョン|リリース日|
|:---|:---:|:---:|:---:|:---:|:---:|
|org.nuget.system.buffers|Microsoft|NuGet||4.4.0|2017年8月11日|
|org.nuget.system.memory|Microsoft|NuGet||4.5.0|2018年5月29日|
|org.nuget.system.numerics.vectors|Microsoft|NuGet||4.4.0|2017年8月11日|
|org.nuget.system.runtime.compilerservices.unsafe|Microsoft|NuGet||4.5.0|2018年5月29日|
|newtonsoft-json-for-unity|jillejr|GitHub|12.0.3|12.0.301|2020年1月20日|
|VRMShaders|vrm-c|GitHub||0.56.0|2020年7月3日|
|UniShaders|IzayoiJiichan|GitHub||1.0.1|2020年8月13日|
|VgoSpringBone|IzayoiJiichan|GitHub||1.0.0|2020年12月1日|
|UniVGO2|IzayoiJiichan|GitHub|VGO 2.1|2.1.0|2020年12月1日|

___
## インストール

### インストール手順（サンプルプロジェクトを使用する場合）


#### 1. サンプルプロジェクトのダウンロード

UniVGO のサンプルプロジェクトをダウンロードします。

https://github.com/izayoijiichan/univgo2.sample.unity.project

#### 2. Unity のインストール

Unity Hub にて`Unity 2019.4.10f1`をインストールします。

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
      "name": "Unity NuGet",
      "url": "https://unitynuget-registry.azurewebsites.net",
      "scopes": ["org.nuget"]
    },
    {
      "name": "Packages from jillejr",
      "url": "https://npm.cloudsmith.io/jillejr/newtonsoft-json-for-unity/",
      "scopes": ["jillejr"]
    }
  ],
  "dependencies": {
    "com.unity.ugui": "1.0.0",
    "com.vrmc.vrmshaders": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRMShaders#v0.56.0",
    "izayoi.unishaders": "https://github.com/izayoijiichan/UniShaders.git#v1.0.1",
    "izayoi.univgo2": "https://github.com/izayoijiichan/VGO2.git#v2.0.1",
    "izayoi.vgospringbone": "https://github.com/izayoijiichan/VgoSpringBone.git#v1.0.0",
    "jillejr.newtonsoft.json-for-unity": "12.0.301",
    "com.unity.modules.ai": "1.0.0",
    ...
    "com.unity.modules.xr": "1.0.0"
  }
}
```

この時点では UnityEditor のコンソールにコンパイルエラーが表示されます。

#### 3. 必要パッケージ の追加インストール

パッケージ マネージャーで必要パッケージの追加インストールを行います。

UnityEditor のメニューバーより `[Window]` > `[Package Manager]` をクリックします。

![image1](https://github.com/izayoijiichan/vgo2/blob/master/Documentation~/UniVGO/Images/201_PackageManager.png)

`Package Manager`にて`My Repositries`を選択します。

`System.Memory (NuGet)`を選択し、`4.5.0`をインストールします。

`System.Buffers`, `System.Numerics.Vectors`, `System.Runtime.CompilerServices.Unsage` は自動でインストールされます。


### インストール完了の確認

1. プロジェクトを Unity Editor で開きます。
2. Unity Editor のコンソールにエラーが表示されないないことを確認します。
3. Unity Editor のメニューバーに `[Tools]` > `[UniVGO]` の項目が表示されていることを確認します。

### エラーが発生した場合

エラーが発生する原因としては以下のようなことが考えられます。

- Unityのバージョンが異なる
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

|アセンブリ|説明|UniVgo2|UniVgo2.Editor|
|:---|:---|:---:|:---:|
|MToon|MToon シェーダー ユーティリティー|*|*|
|MToon.Editor|MToon シェーダー ユーティリティー|-|*|
|NewtonVgo|Newton.JSON向け VGOプログラム|*|*|
|ShaderProperty.Runtime|シェーダーのプロパティー情報|*|*|
|UniShader.Skybox.Utility|Skybox シェーダー ユーティリティー|*|*|
|UniShader.Standard.Particle.Utility|Particle シェーダー ユーティリティー|*|*|
|UniShader.Standard.Utility|Standard シェーダー ユーティリティー|*|*|
|UniUnlit|Unlit シェーダー ユーティリティー|*|*|
|UniUnlit.Editor|Unlit シェーダー ユーティリティー|-|*|
|UniVgo2|VGO2 メインプログラム|*|*|
|UniVgo2.Editor|VGO2 のエディター上での入出力操作|-|*|
|VgoSpringBone|VGO Spring Bone|*|*|

- UniVgo2, UniVgo2.Editor それぞれについて、依存関係にあるDLLに * を付けています。
- MToon, ShaderProperty, UniUnlit はVRMShaders (©vrm-c) に梱包されているプログラムです。

### UniVGO version 1.0 と version 2.0 の併用方法

Unity Editor に UniVGO と UniVGO2 のパッケージを両方同時にインストールするには  
大きく２つのエラーを回避する必要があります。

- 1. 重複シェーダー

どちらかの`UniGLTF`フォルダーを削除します。

- 2. Scripted Importer

`VgoScriptedImporter`がどちらも`.vgo`という拡張子を処理しようとするため、そのままでは競合によりエラーが発生します。

UniVGO (v1.1.1以降) と UniVGO2 (v2.0.1以降) では  
Script Define Symbols に定義を追加することで、処理を変更しエラーを回避することができます。

|Script Define Symbols|説明|
|:---|:---|
|VGO_FILE_EXTENSION_1|version 1 の ファイル拡張子を`.vgo1`として判定するよう変更します。|
|VGO_FILE_EXTENSION_2|version 2 の ファイル拡張子を`.vgo2`として判定するよう変更します。|
|VGO_1_DISABLE_SCRIPTED_IMPORTER|version 1 の Scripted Importer を無効にします。|
|VGO_2_DISABLE_SCRIPTED_IMPORTER|version 2 の Scripted Importer を無効にします。|

用途に応じて定義を追加・削除してください。

設定がすぐに反映されない場合には Unity Editor の再起動してください。

###  UniVRM と UniVGO の併用方法

Unity Editor に UniVRM と UniVGO2 のパッケージを両方同時にインストールするには  
UniVgo を取得した際に梱包されていた重複するファイルを削除する必要があります。

UniGLTFフォルダーにあるシェーダー類が該当します。

___
最終更新日：2020年12月1日  
編集者：十六夜おじいちゃん

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
