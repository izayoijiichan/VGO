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
|Unity 2022.3|○|○|○|未確認|未確認|○|
|Unity 2023.1|○|○|○|○|未確認|○|
|Unity 2023.2|○|○|○|未確認|未確認|未確認|
|Unity 6000.0|○|○|○|未確認|未確認|未確認|

2024年10月の時点では `Unity 6000.0` の `Windows`、`.NET Standard 2.1` 環境にて開発＆確認を行っています。

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
|com.izayoi.unishaders|IzayoiJiichan|GitHub||1.6.1|2023年8月1日|
|com.izayoi.vgospringbone|IzayoiJiichan|GitHub||1.1.2|2022年8月24日|
|com.izayoi.univgo|IzayoiJiichan|GitHub|VGO 2.5|2.5.22|2024年11月4日|

#### 追加パッケージ

必要であれば追加してください。

|パッケージ名|所有者|リポジトリー|仕様バージョン|プログラム バージョン|リリース日|備考|
|:---|:---:|:---:|:---:|:---:|:---:|:---:|
|com.izayoi.liltoon.shader.utility|IzayoiJiichan|GitHub||1.7.0|2024年1月18日||
|com.izayoi.nova.shader.utility|IzayoiJiichan|GitHub||2.4.0|2024年11月4日||
|jp.co.cyberagent.nova|Cyber Agent|GitHub||2.4.0|2024年10月11日||
|jp.lilxyzw.liltoon|lilxyzw|GitHub||1.7.3|2024年8月8日||
|com.vrmc.vrmshaders|vrm-c|GitHub||0.124.2|2024年7月23日||
|org.nuget.sixlabors.imagesharp|SixLabors|Unity NuGet||2.1.5|2023年8月14日|for WebP|
|com.unity.render-pipelines.universal|Unity Technologies|Unity Registry||14.0.0|2021年11月17日|URP only|
|com.unity.render-pipelines.high-definition|Unity Technologies|Unity Registry||14.0.0|2021年11月17日|HDRP only|

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

Unity Hub にて Unity Editor をインストールします。

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
    "com.izayoi.unishaders": "https://github.com/izayoijiichan/UniShaders.git#v1.6.1",
    "com.izayoi.univgo": "https://github.com/izayoijiican/VGO.git#v2.5.22",
    "com.izayoi.vgospringbone": "https://github.com/izayoijiichan/VgoSpringBone.git#v1.1.2",
    "com.unity.nuget.newtonsoft-json": "3.2.1",
  }
}
```

#### 2-3. Addtional Packages

lilToon を使用する場合、以下の行を追加してください。

```json
{
  "dependencies": {
    "com.izayoi.liltoon.shader.utility": "https://github.com/izayoijiichan/lilToonShaderUtility.git#v1.7.0",
    "jp.lilxyzw.liltoon": "https://github.com/lilxyzw/lilToon.git?path=Assets/lilToon#1.7.3",
  }
}
```

NOVA shader を使用する場合、以下の行を追加してください。

```json
{
  "dependencies": {
    "com.izayoi.nova.shader.utility": "https://github.com/izayoijiichan/NovaShaderUtility.git#v2.4.0",
    "jp.co.cyberagent.nova": "https://github.com/CyberAgentGameEntertainment/NovaShader.git?path=Assets/Nova#2.4.0",
  }
}
```

UniUnlit を使用する場合、以下の行を追加してください。

```json
{
  "dependencies": {
    "com.vrmc.vrmshaders": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRMShaders#v0.124.2",
  }
}
```

UniVRM 0.125 以上の場合は上記の代わりに以下を追加してください。

```json
{
  "dependencies": {
    "com.vrmc.gltf": "https://github.com/vrm-c/UniVRM.git?path=/Assets/UniGLTF#v0.125.0",
  }
}
```

MToon 0.x を使用する場合、以下の行を追加してください。

```json
{
  "dependencies": {
    "com.vrmc.vrmshaders": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRMShaders#v0.124.2",
  }
}
```

UniVRM 0.125 以上の場合は上記の代わりに以下を追加してください。

```json
{
  "dependencies": {
    "com.vrmc.univrm": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRM#v0.125.0",
  }
}
```

MToon 1.0 を使用する場合、以下の行を追加してください。

```json
{
  "dependencies": {
    "com.vrmc.vrmshaders": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRMShaders#v0.124.2",
  }
}
```

UniVRM 0.125 以上の場合は上記の代わりに以下を追加してください。

```json
{
  "dependencies": {
    "com.vrmc.vrm": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRM10#v0.125.0",
  }
}
```

Unity 2021.2 以上のバージョンを使用していて、WebPを使用する場合、"org.nuget.sixlabors.imagesharp" の行を追加してください。

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
    "org.nuget.sixlabors.imagesharp": "2.1.5",
  }
}
```

URPを使用する場合、"com.unity.render-pipelines.universal" の行を追加してください。

```json
{
  "dependencies": {
    "com.unity.render-pipelines.universal": "14.0.0",
  }
}
```

- [Universal RP 10.10 for Unity 2020.3](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@10.10/changelog/CHANGELOG.html)
- [Universal RP 12.1 for Unity 2021.3](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@12.1/changelog/CHANGELOG.html)
- [Universal RP 14.0 for Unity 2022.3](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@14.0/changelog/CHANGELOG.html)
- [Universal RP 15.0 for Unity 2023.1](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@15.0/changelog/CHANGELOG.html)
- [Universal RP 16.0 for Unity 2023.2](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@16.0/changelog/CHANGELOG.html)
- [Universal RP 17.0 for Unity 6000.0](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@17.0/changelog/CHANGELOG.html)

HDRPを使用する場合、"com.unity.render-pipelines.high-definition" の行を追加してください。

```json
{
  "dependencies": {
    "com.unity.render-pipelines.high-definition": "14.0.0",
  }
}
```

- [High Definition RP 10.10 for Unity 2020.3](https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@10.10/changelog/CHANGELOG.html)
- [High Definition RP 12.1 for Unity 2021.3](https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@12.1/changelog/CHANGELOG.html)
- [High Definition RP 14.0 for Unity 2022.3](https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@14.0/changelog/CHANGELOG.html)
- [High Definition RP 15.0 for Unity 2023.1](https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@15.0/changelog/CHANGELOG.html)
- [High Definition RP 16.0 for Unity 2023.2](https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@16.0/changelog/CHANGELOG.html)
- [High Definition RP 17.0 for Unity 6000.0](https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@17.0/changelog/CHANGELOG.html)

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
|NOVA|NOVA シェーダー ユーティリティー|*|*|
|NOVA.Editor|NOVA シェーダー ユーティリティー|-|-|
|ShaderProperty.Runtime|シェーダーのプロパティー情報|*|*|
|UniShader.Hdrp.Utility|HDRP シェーダー ユーティリティー|*|*|
|UniShader.Shared|Unity シェーダー 共有 ユーティリティー|*|*|
|UniShader.Skybox.Utility|Skybox シェーダー ユーティリティー|*|*|
|UniShader.Standard.Particle.Utility|Standard Particle シェーダー ユーティリティー|*|*|
|UniShader.Standard.Utility|Standard シェーダー ユーティリティー|*|*|
|UniShader.Urp.Particle.Utility|URP Particle シェーダー ユーティリティー|*|*|
|UniShader.Urp.Utility|URP シェーダー ユーティリティー|*|*|
|UniVgo2|VGO2 メインプログラム|*|*|
|UniVgo2.Editor|VGO2 のエディター上での入出力操作|-|*|
|VgoSpringBone|VGO Spring Bone|*|*|
|VRMShaders.GLTF.IO.Editor||-|-|
|VRMShaders.GLTF.IO.Runtime||*|*|
|VRMShaders.GLTF.UniUnlit.Editor|Unlit シェーダー ユーティリティー|-|-|
|VRMShaders.GLTF.UniUnlit.Runtime|Unlit シェーダー ユーティリティー|*|*|
|VRMShaders.VRM10.MToon10.Editor|MToon 1.0 シェーダー ユーティリティー|-|-|
|VRMShaders.VRM10.MToon10.Runtime|MToon 1.0 シェーダー ユーティリティー|*|*|

- UniVgo2, UniVgo2.Editor それぞれについて、依存関係にあるDLLに * を付けています。

### UniVRM と UniVGO の併用方法

バージョンの組み合わせは以下の通りです。

|UniVRM|UniVGO|min Unity|
|:---:|:---:|:---:|
|0.100.0|2.5.22|2020.3|
|0.101.0|2.5.22|2020.3|
|0.102.0|2.5.22|2020.3|
|0.103.2|2.5.22|2020.3|
|0.104.2|2.5.22|2020.3|
|0.105.0|2.5.22|2020.3|
|0.106.0|2.5.22|2020.3|
|0.107.2|2.5.22|2020.3|
|0.108.0|2.5.22|2020.3|
|0.109.0|2.5.22|2020.3|
|0.110.0|2.5.22|2020.3|
|0.111.0|2.5.22|2020.3|
|0.112.0|2.5.22|2021.3|
|0.113.0|2.5.22|2021.3|
|0.114.0|2.5.22|2021.3|
|0.115.0|2.5.22|2021.3|
|0.116.0|2.5.22|2021.3|
|0.117.0|2.5.22|2021.3|
|0.118.0|2.5.22|2021.3|
|0.119.0|2.5.22|2021.3|
|0.120.0|2.5.22|2021.3|
|0.121.0|2.5.22|2021.3|
|0.122.0|2.5.22|2021.3|
|0.123.0|2.5.22|2021.3|
|0.124.2|2.5.22|2021.3|
|0.125.0|2.5.22|2021.3|

`<Project>/Packages/package.json` に以下の記述を行います。  

```json
{
  "dependencies": {
    ...
    "com.vrmc.gltf": "https://github.com/vrm-c/UniVRM.git?path=/Assets/UniGLTF#v0.124.2",
    "com.vrmc.univrm": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRM#v0.124.2",
    "com.vrmc.vrm": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRM10#v0.124.2",
    "com.vrmc.vrmshaders": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRMShaders#v0.124.2",
    ...
  }
}
```

UniVRM 0.125 以上の場合は上記の代わりに以下を追加してください。

```json
{
  "dependencies": {
    ...
    "com.vrmc.gltf": "https://github.com/vrm-c/UniVRM.git?path=/Assets/UniGLTF#v0.125.0",
    "com.vrmc.univrm": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRM#v0.125.0",
    "com.vrmc.vrm": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRM10#v0.125.0",
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
最終更新日：2024年11月4日  
編集者：十六夜おじいちゃん

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
