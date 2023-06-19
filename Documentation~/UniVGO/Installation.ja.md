# UniVGO インストール マニュアル

Unity Editor に UniVGO をインストールするためのマニュアルです。

___

## システム要件

### Unity のバージョン

|バージョン|Win (Editor)|Win (Mono)|Win (IL2CPP)|Android (IL2CPP)|iOS|WebGL|
|:---|:---:|:---:|:---:|:---:|:---:|:---:|
|Unity 2020.3|○|○|○|○|未確認|○|
|Unity 2021.1|○|○|○|○|未確認|○|
|Unity 2021.2|○|○|○|○|未確認|○|
|Unity 2021.3|○|○|○|○|未確認|○|
|Unity 2022.1|○|○|○|未確認|未確認|○|
|Unity 2022.2|○|○|○|未確認|未確認|○|
|Unity 2022.3|○|○|○|未確認|未確認|未確認|
|Unity 2023.1|○|○|○|未確認|未確認|未確認|

2023年6月の時点では `Unity 2023.1` の `Windows` `.NET Standard 2.1` 環境にて開発＆確認を行っています。

### 必要パッケージ

#### 基本システムパッケージ

Unity 2021.1 以下のバージョンを使用する場合

|パッケージ名|所有者|リポジトリー|仕様バージョン|プログラム バージョン|リリース日|
|:---|:---:|:---:|:---:|:---:|:---:|
|org.nuget.system.buffers|Microsoft|NuGet||4.4.0|2017年8月11日|
|org.nuget.system.memory|Microsoft|NuGet||4.5.0|2018年5月29日|
|org.nuget.system.numerics.vectors|Microsoft|NuGet||4.4.0|2017年8月11日|

#### システムパッケージ

いずれの Unity のバージョンを使用する場合でも必要

|パッケージ名|所有者|リポジトリー|仕様バージョン|プログラム バージョン|リリース日|
|:---|:---:|:---:|:---:|:---:|:---:|
|com.unity.nuget.newtonsoft-json|Unity Technologies|Nuget|13.0.2|3.2.1|2023年5月2日|
|VRMShaders|vrm-c|GitHub||0.105.0|2022年10月7日|
|LilToonShader.Utility|IzayoiJiichan|GitHub||1.4.0|2023年5月30日|
|UniShaders|IzayoiJiichan|GitHub||1.4.0|2022年5月20日|
|VgoSpringBone|IzayoiJiichan|GitHub||1.1.2|2022年8月24日|
|UniVGO2|IzayoiJiichan|GitHub|VGO 2.5|2.5.6|2023年6月20日|

#### 追加パッケージ

必要であれば追加してください。

|パッケージ名|所有者|リポジトリー|仕様バージョン|プログラム バージョン|リリース日|備考|
|:---|:---:|:---:|:---:|:---:|:---:|:---:|
|jp.lilxyzw.liltoon|lilxyzw|GitHub||1.4.0|2023年5月12日||
|com.unity.render-pipelines.universal|Unity Technologies|Unity Registry||11.0.0|2021年10月26日|URP only|
|com.unity.render-pipelines.high-definition|Unity Technologies|Unity Registry||11.0.0|2021年10月26日|HDRP only|

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
|2022.3.0f1|BRP|[Link](https://github.com/izayoijiichan/univgo2.sample.unity.project/tree/unity2022.3.brp)|

#### 2. Unity Editor のインストール

Unity Hub にて Unity Editor `2021.1.28f1`、`2021.2.0f1`、`2021.3.0f1`、`2022.1.0f1`、`2022.2.0f1`、`2022.3.0f1` のいずれかをインストールします。

探しているバージョンが Unity Hub に表示されない場合は、[Unity ダウンロード アーカイブ](https://unity3d.com/jp/get-unity/download/archive) 経由でインストールしてください。

#### 3. プロジェクトを読み込み

Unity Hub にて、1でダウンロードしたサンプルプロジェクトをリストに追加します。  
指定するフォルダーはプロジェクトフォルダーです。

#### 4. プロジェクトのアップデート

必要であれば、Unity Editor やパッケージのバージョンをアップデートしてください。  
特段の注記がない限りは UniVGO は最新バージョンを使用することをおすすめします。

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
`<Project>/Packages/package.json` に設定を行います。  
追加する位置に気を付ける必要があります。

#### 2-1. Basic Sytem Packages

Unity 2021.1.28f1以下のバージョンを使用する場合、以下の設定を追加してください。  
それ以上のバージョンを使用する場合には設定は必要ありません。

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
    "org.nuget.system.buffers": "4.4.0",
    "org.nuget.system.memory": "4.5.0",
    "org.nuget.system.numerics.vectors": "4.4.0",
  }
}
```

#### 2-2. Basic Packages

UniVGOを使用するために、以下の設定を追加してください。

```json
{
  "dependencies": {
    "com.izayoi.liltoon.shader.utility": "https://github.com/izayoijiichan/lilToonShaderUtility.git#v1.4.0",
    "com.izayoi.unishaders": "https://github.com/izayoijiichan/UniShaders.git#v1.4.0",
    "com.izayoi.univgo": "https://github.com/izayoijiican/VGO.git#v2.5.6",
    "com.izayoi.vgospringbone": "https://github.com/izayoijiichan/VgoSpringBone.git#v1.1.2",
    "com.unity.nuget.newtonsoft-json": "3.2.1",
    "com.unity.ugui": "1.0.0",
    "com.vrmc.vrmshaders": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRMShaders#v0.105.0",
  }
}
```

#### 2-3. Addtional Packages

lilToonを使用する場合、"jp.lilxyzw.liltoon" の行を追加してください。

```json
{
  "dependencies": {
    "jp.lilxyzw.liltoon": "https://github.com/lilxyzw/lilToon.git?path=Assets/lilToon#1.4.0",
  }
}
```

URPを使用する場合、"com.unity.render-pipelines.universal" の行を追加してください。

```json
{
  "dependencies": {
    "com.unity.render-pipelines.universal": "11.0.0",
  }
}
```

HDRPを使用する場合、"com.unity.render-pipelines.high-definition" の行を追加してください。

```json
{
  "dependencies": {
    "com.unity.render-pipelines.high-definition": "11.0.0",
  }
}
```

### インストール完了の確認

1. プロジェクトを Unity Editor で開きます。
2. Unity Editor のコンソールにエラーが表示されないないことを確認します。
3. Unity Editor のメニューバーに `[Tools]` > `[UniVGO]` の項目が表示されていることを確認します。

### エラーが発生した場合

エラーが発生する原因としては以下のようなことが考えられます。

- Unity Editor のバージョンが異なる
- パッケージのバージョンが異なる
- ファイルが重複または不足している
- `asmdef` の設定が変更されている
- `asmdef.meta` の設定が変更されている
- コンポーネントの `.meta` の guid が変更されている
- `System.Buffers.dll`, `System.Memory.dll`, `System.Numerics.Vectors.dll` が重複して配置されている
- `NewtonSoft.Json.dll` が重複して配置されている
- GitHub から `LFS` のデータが取得できていない

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

### UniVRM と UniVGO の併用方法

バージョンの組み合わせは以下の通りです。

|UniVRM|UniVGO|min Unity|
|:---:|:---:|:---:|
|0.100.0|2.5.6|2020.3|
|0.101.0|2.5.6|2020.3|
|0.102.0|2.5.6|2020.3|
|0.103.2|2.5.6|2020.3|
|0.104.2|2.5.6|2020.3|
|0.105.0|2.5.6|2020.3|
|0.106.0|2.5.6|2020.3|
|0.107.2|2.5.6|2020.3|
|0.108.0|2.5.6|2020.3|
|0.109.0|2.5.6|2020.3|
|0.110.0|2.5.6|2020.3|

`<Project>/Packages/package.json` に以下の記述を行います。  

```json
{
  "dependencies": {
    ...
    "com.vrmc.gltf": "https://github.com/vrm-c/UniVRM.git?path=/Assets/UniGLTF#v0.105.0",
    "com.vrmc.univrm": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRM#v0.105.0",
    "com.vrmc.vrm": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRM10#v0.105.0",
    "com.vrmc.vrmshaders": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRMShaders#v0.105.0",
    ...
  }
}
```

上記以外の組み合わせはwikiをご覧ください。

https://github.com/izayoijiichan/VGO/wiki/How-to-use-UniVRM-and-UniVGO-together


### セットアップ済みサンプルプロジェクト

|unity version|rendering pipeline|package|link|
|:--|:--:|:--:|:--:|
|2021.1.28f1|BRP|UniVGO + UniVRM|[Link](https://github.com/izayoijiichan/univgo2.sample.unity.project/tree/unity2021.1.brp.univrm)|
|2021.3.0f1|BRP|UniVGO + UniVRM|[Link](https://github.com/izayoijiichan/univgo2.sample.unity.project/tree/unity2021.3.brp.univrm)|
|2022.3.0f1|BRP|UniVGO + UniVRM|[Link](https://github.com/izayoijiichan/univgo2.sample.unity.project/tree/unity2022.3.brp.univrm)|

___
最終更新日：2023年6月20日  
編集者：十六夜おじいちゃん

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
