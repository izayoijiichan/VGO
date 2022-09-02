# UniVGO インストール マニュアル

Unity Editor に UniVGO をインストールするためのマニュアルです。

___

## システム要件

### Unity のバージョン

|バージョン|Win (Editor)|Win (Mono)|Win (IL2CPP)|Android (IL2CPP)|iOS|
|:---|:---:|:---:|:---:|:---:|:---:|
|Unity 2019.4|○|○|○|○|未確認|
|Unity 2020.1|○|○|○|○|未確認|
|Unity 2020.2|○|○|○|○|未確認|
|Unity 2020.3|○|○|○|○|未確認|
|Unity 2021.1|○|○|○|○|未確認|
|Unity 2021.2|○|○|○|○|未確認|
|Unity 2021.3|○|○|○|○|未確認|
|Unity 2022.1|○|○|○|未確認|未確認|

2022年9月の時点では `Unity 2022.1` の `Windows` `.NET Standard 2.1` 環境にて開発＆確認を行っています。

### 必要パッケージ

#### 共通（いずれの Unity のバージョンを使用する場合でも必要）

|パッケージ名|所有者|リポジトリー|仕様バージョン|プログラム バージョン|リリース日|
|:---|:---:|:---:|:---:|:---:|:---:|
|com.unity.nuget.newtonsoft-json|Unity Technologies|Nuget|-|3.0.2|2022年3月29日|
|VRMShaders|vrm-c|GitHub||0.85.0|2021年9月30日|
|LilToonShader.Utility|IzayoiJiichan|GitHub||1.0.1|2022年8月21日|
|UniShaders|IzayoiJiichan|GitHub||1.4.0|2022年5月20日|
|VgoSpringBone|IzayoiJiichan|GitHub||1.1.2|2022年8月24日|
|UniVGO2|IzayoiJiichan|GitHub|VGO 2.5|2.5.0|2022年9月3日|

#### 追加（Unity 2021.1 以下のバージョンを使用する場合）

|パッケージ名|所有者|リポジトリー|仕様バージョン|プログラム バージョン|リリース日|
|:---|:---:|:---:|:---:|:---:|:---:|
|org.nuget.system.buffers|Microsoft|NuGet||4.4.0|2017年8月11日|
|org.nuget.system.memory|Microsoft|NuGet||4.5.0|2018年5月29日|
|org.nuget.system.numerics.vectors|Microsoft|NuGet||4.4.0|2017年8月11日|

#### 追加（HDRP のプロジェクトで使用する場合）

|パッケージ名|所有者|リポジトリー|仕様バージョン|プログラム バージョン|リリース日|
|:---|:---:|:---:|:---:|:---:|:---:|
|com.unity.render-pipelines.high-definition|Unity Technologies|Unity Registry||11.0.0|2021年10月26日|

#### 追加（URP のプロジェクトで使用する場合）

|パッケージ名|所有者|リポジトリー|仕様バージョン|プログラム バージョン|リリース日|
|:---|:---:|:---:|:---:|:---:|:---:|
|com.unity.render-pipelines.universal|Unity Technologies|Unity Registry||11.0.0|2021年10月26日|

___

## インストール

### インストール手順（サンプルプロジェクトを使用する場合）

#### 1. サンプルプロジェクトのダウンロード

次のサンプルプロジェクトのいずれかをダウンロードします。

|unity version|rendering pipeline|link|
|:--|:--:|:--:|
|2021.1.28f1|BRP|[Link](https://github.com/izayoijiichan/univgo2.sample.unity.project/tree/unity2021.1.brp)|
|2021.1.28f1|URP|[Link](https://github.com/izayoijiichan/univgo2.sample.unity.project/tree/unity2021.1.urp)|
|2021.1.28f1|HDRP|[Link](https://github.com/izayoijiichan/univgo2.sample.unity.project/tree/unity2021.1.hdrp)|
|2021.3.0f1|BRP|[Link](https://github.com/izayoijiichan/univgo2.sample.unity.project/tree/unity2021.3.brp)|
|2021.3.0f1|URP|[Link](https://github.com/izayoijiichan/univgo2.sample.unity.project/tree/unity2021.3.urp)|
|2021.3.0f1|HDRP|[Link](https://github.com/izayoijiichan/univgo2.sample.unity.project/tree/unity2021.3.hdrp)|

#### 2. Unity Editor のインストール

Unity Hub にて Unity Editor `2021.1.28f1`、`2021.2.0f1`、`2021.3.0f1`、`2022.1.0f1` のいずれかをインストールします。

探しているバージョンが Unity Hub に表示されない場合は、[Unity ダウンロード アーカイブ](https://unity3d.com/jp/get-unity/download/archive) 経由でインストールしてください。

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

#### 2. Define シンボルの追加

`Projcet Settings` > `Player` > `Other Settings` > `Script Compilation` > `Scripting Define Symbols`

使用する VRMShaders (com.vrmc.vrmshaders) のバージョンに合わせて、Define シンボルを追加します。

|symbol value|min version|max version|
|:--|:--:|:--:|
|VRMC_VRMSHADERS_0_72_OR_NEWER|0.72.0|0.84.0|
|VRMC_VRMSHADERS_0_85_OR_NEWER|0.85.0|(0.101.0)|

Add the Define symbol to match the version of lilToon (jp.lilxyzw.liltoon) you are using.使用する lilToon (jp.lilxyzw.liltoon) のバージョンに合わせて、Define シンボルを追加します。
|symbol value|min version|max version|
|:--|:--:|:--:|
|LILTOON_1_2_12_OR_OLDER|(1.2.0)|1.2.12|
|LILTOON_1_3_0_OR_NEWER|1.3.0|(1.3.3)|

`Apply` ボタンをクリックします.

#### 3. 必要パッケージ のインストール

UniVGO及び依存パッケージをプロジェクトに取り込みます。  
`<Project>/Packages/package.json` に以下の記述を行います。  
追加する位置に気を付ける必要があります。

- Unity 2021.1.28f1 以下

```json
{
  "scopedRegistries": [
    {
      "name": "Unity NuGet",
      "url": "https://unitynuget-registry.azurewebsites.net",
      "scopes": ["org.nuget"]
    }
  ],
  "dependencies": {
    "com.izayoi.liltoon.shader.utility": "https://github.com/izayoijiichan/lilToonShaderUtility.git#v1.0.1",
    "com.izayoi.unishaders": "https://github.com/izayoijiichan/UniShaders.git#v1.4.0",
    "com.izayoi.univgo2": "https://github.com/izayoijiican/VGO2.git#v2.5.0",
    "com.izayoi.vgospringbone": "https://github.com/izayoijiichan/VgoSpringBone.git#v1.1.2",
    "com.unity.nuget.newtonsoft-json": "3.0.2",
    "com.unity.ugui": "1.0.0",
    "com.vrmc.vrmshaders": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRMShaders#v0.85.0",
    "org.nuget.system.buffers": "4.4.0",
    "org.nuget.system.memory": "4.5.0",
    "org.nuget.system.numerics.vectors": "4.4.0",
    "com.unity.modules.ai": "1.0.0",
    ...
    "com.unity.modules.xr": "1.0.0"
  }
}
```

- Unity 2021.2.0f1 以上 (2021.3, 2022.1)

```json
{
  "dependencies": {
    "com.izayoi.liltoon.shader.utility": "https://github.com/izayoijiichan/lilToonShaderUtility.git#v1.0.1",
    "com.izayoi.unishaders": "https://github.com/izayoijiichan/UniShaders.git#v1.4.0",
    "com.izayoi.univgo2": "https://github.com/izayoijiican/VGO2.git#v2.5.0",
    "com.izayoi.vgospringbone": "https://github.com/izayoijiichan/VgoSpringBone.git#v1.1.2",
    "com.unity.nuget.newtonsoft-json": "3.0.2",
    "com.unity.ugui": "1.0.0",
    "com.vrmc.vrmshaders": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRMShaders#v0.85.0",
    "com.unity.modules.ai": "1.0.0",
    ...
    "com.unity.modules.xr": "1.0.0"
  }
}
```

URPを使用する場合、"com.unity.render-pipelines.universal" の行を追加してください。

```json
{
  "dependencies": {
    ...
    "com.unity.render-pipelines.universal": "11.0.0",
    "com.unity.ugui": "1.0.0",
    ...
  }
}
```

HDRPを使用する場合、"com.unity.render-pipelines.high-definition" の行を追加してください。

```json
{
  "dependencies": {
    ...
    "com.unity.render-pipelines.high-definition": "11.0.0",
    "com.unity.ugui": "1.0.0",
    ...
  }
}
```

lilToonを使用する場合、"jp.lilxyzw.liltoon" の行を追加してください。

```json
{
  "dependencies": {
    ...
    "jp.lilxyzw.liltoon": "https://github.com/lilxyzw/lilToon.git?path=Assets/lilToon#1.3.1",
    "com.unity.modules.ai": "1.0.0",
    ...
  }
}
```

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
- `System.Buffers.dll`, `System.Memory.dll`, `System.Numerics.Vectors.dll` が重複して配置されている
- `NewtonSoft.Json.dll` が重複して配置されている

___

## その他の情報

### ライブラリー（アセンブリ）について

パッケージをプロジェクトにインストールすると  
スクリプトが自動的にコンパイルされ、以下のDLLが生成されます。

|アセンブリ|説明|UniVgo2|UniVgo2.Editor|
|:---|:---|:---:|:---:|
|lilToon.Editor|lilToon シェーダー ユーティリティー|-|-|
|LilToonShader.Utility|lilToon シェーダー ユーティリティー|*|*|
|MToon|MToon シェーダー ユーティリティー|*|*|
|MToon.Editor|MToon シェーダー ユーティリティー|-|-|
|NewtonVgo|Newton.JSON向け VGOプログラム|*|*|
|ShaderProperty.Runtime|シェーダーのプロパティー情報|*|*|
|UniShader.Hdrp.Utility|HDRP シェーダー ユーティリティー|*|*|
|UniShader.Skybox.Utility|Skybox シェーダー ユーティリティー|*|*|
|UniShader.Standard.Particle.Utility|Particle シェーダー ユーティリティー|*|*|
|UniShader.Standard.Utility|Standard シェーダー ユーティリティー|*|*|
|UniShader.Urp.Utility|URP シェーダー ユーティリティー|*|*|
|UniVgo2|VGO2 メインプログラム|*|*|
|UniVgo2.Editor|VGO2 のエディター上での入出力操作|-|*|
|VgoSpringBone|VGO Spring Bone|*|*|
|VRMShaders.GLTF.IO.Editor||-|-|
|VRMShaders.GLTF.IO.Runtime||*|*|
|VRMShaders.GLTF.UniUnlit.Editor|Unlit シェーダー ユーティリティー|-|-|
|VRMShaders.GLTF.UniUnlit.Runtime|Unlit シェーダー ユーティリティー|*|*|

- UniVgo2, UniVgo2.Editor それぞれについて、依存関係にあるDLLに * を付けています。

### UniVGO version 1.0 と version 2.0 の併用方法

Unity Editor に UniVGO と UniVGO2 のパッケージを両方同時にインストールするには  
大きく２つのエラーを回避する必要があります。

1. 重複シェーダー

どちらかの`UniGLTF`フォルダーを削除します。

2. Scripted Importer

`VgoScriptedImporter`がどちらも`.vgo`という拡張子を処理しようとするため、そのままでは競合によりエラーが発生します。

UniVGO (v1.1.1以降) と UniVGO2 (v2.0.1以降) では  
Scripting Define Symbols に定義を追加することで、処理を変更しエラーを回避することができます。

|Scripting Define Symbols|説明|
|:---|:---|
|VGO_FILE_EXTENSION_1|version 1 の ファイル拡張子を`.vgo1`として判定するよう変更します。|
|VGO_FILE_EXTENSION_2|version 2 の ファイル拡張子を`.vgo2`として判定するよう変更します。|
|VGO_1_DISABLE_SCRIPTED_IMPORTER|version 1 の Scripted Importer を無効にします。|
|VGO_2_DISABLE_SCRIPTED_IMPORTER|version 2 の Scripted Importer を無効にします。|

用途に応じて定義を追加・削除してください。

設定がすぐに反映されない場合には Unity Editor の再起動してください。

### UniVRM と UniVGO の併用方法

バージョンの組み合わせは以下の通りです。

|UniVRM|min UniVGO|min Unity|Result|
|:---:|:---:|:---:|:---:|
|0.66.0|2.4.4|2019.4|○|
|0.68.2|2.4.4|2019.4|○|
|0.70.0|2.4.4|2019.4|○|
|0.71.0|2.4.4|2019.4|○|
|0.72.0|2.4.5|2019.4|○|
|0.73.0|2.4.5|2019.4|○|
|0.74.0|2.4.5|2019.4|○|
|0.75.0|2.4.5|2019.4|○|
|0.76.0|2.4.5|2019.4|(○)|
|0.77.0|2.4.5|2019.4|(○)|
|0.78.0|2.4.5|2019.4|(○)|
|0.79.0|2.4.5|2019.4|(○)|
|0.80.0|2.4.5|2019.4|(○)|
|0.81.0|2.4.5|2019.4|○|
|0.82.0|2.4.5|2019.4|○|
|0.83.0|2.4.5|2019.4|○|
|0.84.0|2.4.5|2019.4|○|
|0.85.0|2.4.5|2019.4|○|
|0.86.0|2.4.5|2019.4|○|
|0.87.0|2.4.5|2019.4|○|
|0.88.0|2.4.5|2019.4|○|
|0.89.0|2.4.5|2019.4|○|
|0.90.0|2.4.5|2019.4|○|
|0.91.0|2.4.5|2019.4|○|
|0.92.0|2.4.5|2019.4|○|
|0.93.0|2.4.5|2019.4|○|
|0.94.0|2.4.5|2019.4|○|
|0.95.1|2.4.5|2019.4|○|
|0.96.2|2.4.5|2019.4|○|
|0.97.0|2.4.5|2019.4|○|
|0.98.0|2.4.5|2019.4|○|
|0.99.4|2.4.5|2019.4|○|
|0.100.0|2.4.5|2020.3|○|
|0.101.0|2.4.5|2020.3|○|

Unity 2020.2 以上を使用する場合、0.76.0 から 0.80.0 ではコンパイルエラーが発生しますので非推奨です。

- UniVRM 0.66.0 ～ 0.71.0 を使用する場合

UniVGO 2.4.4 を使用してください。

`<Project>/Packages/package.json` に以下の記述を行います。  

```json
{
  "dependencies": {
    ...
    "com.vrmc.unigltf": "https://github.com/vrm-c/UniVRM.git?path=/Assets/UniGLTF#v0.66.0",
    "com.vrmc.univrm": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRM#v0.66.0",
    "com.vrmc.vrmshaders": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRMShaders#v0.66.0",
    ...
  }
}
```

- UniVRM 0.72.0 ～ 0.80.0 を使用する場合

`<Project>/Packages/package.json` に以下の記述を行います。  

```json
{
  "dependencies": {
    ...
    "com.vrmc.unigltf": "https://github.com/vrm-c/UniVRM.git?path=/Assets/UniGLTF#v0.72.0",
    "com.vrmc.univrm": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRM#v0.72.0",
    "com.vrmc.vrmshaders": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRMShaders#v0.72.0",
    ...
  }
}
```

- UniVRM 0.81.0 ～ 0.90.0 を使用する場合

`<Project>/Packages/package.json` に以下の記述を行います。  

```json
{
  "dependencies": {
    ...
    "com.vrmc.gltf": "https://github.com/vrm-c/UniVRM.git?path=/Assets/UniGLTF#v0.81.0",
    "com.vrmc.univrm": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRM#v0.81.0",
    "com.vrmc.vrm": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRM10#v0.81.0",
    "com.vrmc.vrmshaders": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRMShaders#v0.81.0",
    ...
  }
}
```

使用する VRMShaders (com.vrmc.vrmshaders) のバージョンに合わせて、Define シンボルを修正します。

`Projcet Settings` > `Player` > `Other Settings` > `Script Compilation` > `Scripting Define Symbols`

|symbol value|min version|max version|
|:--|:--:|:--:|
|VRMC_VRMSHADERS_0_72_OR_NEWER|0.72.0|0.84.0|
|VRMC_VRMSHADERS_0_85_OR_NEWER|0.85.0|(0.101.0)|

### セットアップ済みサンプルプロジェクト

|unity version|rendering pipeline|package|link|
|:--|:--:|:--:|:--:|
|2021.1.28f1|BRP|UniVGO + UniVRM|[Link](https://github.com/izayoijiichan/univgo2.sample.unity.project/tree/unity2021.1.brp.univrm)|
|2021.3.0f1|BRP|UniVGO + UniVRM|[Link](https://github.com/izayoijiichan/univgo2.sample.unity.project/tree/unity2021.3.brp.univrm)|

___
最終更新日：2022年9月3日  
編集者：十六夜おじいちゃん

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
