# UniVGO

UniVGOとは、UnityでVGOファイルを使えるようにしたパッケージです。

## 特徴

- VGOファイルの設定、出力、取込を画面UIにて行うことができます。
- UnityのスクリプトからVGOファイルの操作（動的読込等）を行うことができます。

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
|newtonsoft-json-for-unity|jillejr|GitHub|12.0.1|12.0.101|2019年11月27日|
|UniVGO|IzayoiJiichan|GitHub| VGO 0.1|0.3.0|2020年1月1日|

___
## インストール

### インストール手順

1. UnityEditor または UnityHub にて3Dの新規プロジェクトを作成します。

```
    <Project>
        Assets
        Packages
        ProjectSettings
```

2. Newtonsoft.JSON (Json.NET) をパッケージとしてプロジェクトに取り込みます。

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
    "jillejr.newtonsoft.json-for-unity": "12.0.101",
    "com.unity.modules.ai": "1.0.0",
    ...
    "com.unity.modules.xr": "1.0.0"
  }
}
```

3. UniVGO をプロジェクトに取り込みます。

AまたはBのいずれかを行ってください。

A) `package.json`にて管理する場合

`<Project>/Packages/package.json` に以下の記述を行います。  

```json
{
  "dependencies": {
    "com.unity.ugui": "1.0.0",
    "izayoi.univgo": "https://github.com/izayoijiichan/VGO.git#v0.3.0",
    "jillejr.newtonsoft.json-for-unity": "12.0.101",
    "com.unity.modules.ai": "1.0.0",
    ...
    "com.unity.modules.xr": "1.0.0"
  }
}
```

B) `Packages`フォルダーにて管理する場合  

以下のURLより圧縮ファイルをダウンロードします。  

UniVGO  
https://github.com/izayoijiichan/VGO/releases  

ファイルを解凍して`Packages`フォルダーに格納します。

```
  <Project>
    Packages
      izayoi.univgo@0.3.0-preview
        DepthFirstScheduler
        ShaderProperty
        UniGLTFforUniVgo
        UniUnlit
        UniVgo
```

フォルダーの名前は変更しても構いません。

### インストール完了の確認

1. プロジェクトを UnityEditor で開きます。
2. UnityEditor のコンソールにエラーが表示されないないことを確認します。
3. UnityEditor のメニューバーに `[Tools]` > `[UniVGO]` の項目が表示されていることを確認します。

___
## VGOに関連する Unity コンポーネントの説明

### VGO Meta

VGO のメタ情報です。

|定義名|説明|型|固定値|
|:---|:---|:---:|:---:|
|Generator Name|生成ツールの名前です。|string|UniVGO|
|Generator Version|生成ツールのバージョンです。|string|0.3.0|
|Spec Version|VGOの仕様バージョンです。|string|0.1|

- Root の GameObject に１つ付与しておく必要があります。  
- ユーザーが設定可能な項目はありません。

### VGO Right

オブジェクトの権利情報です。

|定義名|説明|型|備考|
|:---|:---|:---:|:---|
|Title|作品の名前です。|string|必須|
|Author|クリエイターの名前です。|string|必須|
|Organization|クリエイターの所属する組織です。|string||
|Created Date|作品の作成日です。|string|形式の規定はありません。|
|Updated Date|作品の更新日です。|string|形式の規定はありません。|
|Version|作品のバージョンです。|string|形式の規定はありません。|
|Distribution Url|配布URLです。|string||
|License Url|ライセンスの記載されたURLです。|string||

- Root の GameObject に１つ付与しておく必要があります。  
- それ以外に 任意の GameObject に自由に付与することができます。  
- 物体の運動には何ら影響しません。

### Collider

物体同士の衝突を検知するための設定です。  
Box, Capsule, Sphere タイプに対応しています。  
１つのゲームオブジェクトに複数のコライダーを付与することが可能です。

詳細は Unity 公式のマニュアルをご覧ください。

### Rigidbody

物体を物理特性によって制御するための設定です。  
１つの GameObject に対し１つまで付与することが可能です。  

詳細は Unity 公式のマニュアルをご覧ください。

___
## Shader

対応しているシェーダーは以下の通りです。

|シェーダー名|説明|
|:---|:---|
|Standard|標準シェーダー|
|UniGLTF/Unlit|Unlitシェーダー|
___
## VGO のセットアップ

### Root の GameObject

|コンポーネント|名前|説明|
|:---|:---|:---|
|(Name)|名前|任意の名前を設定します。|
|Transform|トランスフォーム|Position (0, 0, 0) Rotation (0, 0, 0) Scale (1, 1, 1)|
|VGO Meta|VGOメタ情報|付与するだけでOKです。|
|VGO Right|VGO権利情報|自由に設定します。|

コンポーネントの順序は動作に関係ありません。

### 子の GameObject

Root の配下には GameObject を自由に配置することができます。  
VGO Meta コンポーネントは自由に付与可能です。  
以下、タイプが A か B か C かに分かれます。

#### A) 静止物

床や建物など、衝突しても動くことのない物体です。  
基本的にはこちらを設定していきます。

|コンポーネント|名前|説明|
|:---|:---|:---|
|(Name)|オブジェクトの名前|任意の名前を設定します。|
|Transform|トランスフォーム|自由に設定します|
|Collider|コライダー|Box / Capsule / Sphere|

- Collider
  - Is Trigger：□（チェックオフ）

必ずしも物体の形状と同じにする必要はありません。

#### B) 衝突時に動いてよい物

アバターやその他物体が衝突した際に動く物体です。  
設定するオブジェクトの個数は必要範囲内に留めてください。

|コンポーネント|名前|説明|
|:---|:---|:---|
|(Name)|オブジェクトの名前|重複は避けたほうがよいです。|
|Transform|トランスフォーム|自由に設定します|
|Collider|コライダー|Box / Capsule / Sphere|
|Rigidbody|剛体|必ず付与します。|

- Collider
  - Is Trigger：□（チェックオフ）
- Rigidbody
  - Mass：適切な重さ
  - Use Gravity：■（特性によってはチェックオフも可）
  - Is Kinematic：□（チェックオフ）
  - Constraints：
    - Freese Position：自由に設定可
    - Freese Rotation：自由に設定可

#### C) 衝突判定の必要ない物

|コンポーネント|名前|説明|
|:---|:---|:---|
|(Name)|オブジェクトの名前|任意の名前を設定します。|
|Transform|トランスフォーム|自由に設定します|

衝突判定の必要ない物については
Collider, Rigidbody のどちらのコンポーネントも付与しません。

親のオブジェクトに付与したコライダーが判定の範囲をカバーしている場合もこのパターンになります。

### 設定の仕様

エクスポート時、非活性な GameObject は活性扱いで出力されます。  

___
## VGO の出力（エクスポート）

1. UnityEditor の Hierarchy にて VgoMeta が付与されているゲームオブジェクトを選択します。
2. A Inspector に表示される VgoMeta コンポーネントの `[Export VGO]` ボタンを押下します。  
   B メニューバーから `[Tools]` > `[UniVGO]` > `[Export]` を選択します。
3. ファイル保存ダイアログにて出力先をしてして保存ボタンを押下します。
4. 処理に成功すると指定していた出力先にVGOファイルが出力されます。

## VGO の取込（インポート）

1. メニューバーから `[Tools]` > `[UniVGO]` > `[Import]` を選択します。
2. ファイル選択ダイアログにて取り込みたいVGOファイルを指定し、開くボタンを押下します。
3. ファイル保存ダイアログにて格納したい Assets 配下のフォルダーを選択し保存ボタンを押下します。
4. 処理に成功するとデータが Prefab として取り込まれます。

___

## スクリプトによる VGO のロード

スクリプトを実行することで、動的にVGOファイルを Game シーンにロードすることができます。

以下が簡単なサンプルプログラムです。

~~~csharp
    using UnityEngine;
    using UniVgo;

    public class RuntimeLoadBehaviour : MonoBehaviour
    {
        private void Start()
        {
            var importer = new VgoImporter();
            importer.Load(filePath, true);
        }
    }
~~~

___
## その他の情報

### インストールにおけるエラーの原因

エラーが発生する原因としては以下のようなことが考えられます。
- パッケージのバージョンが異なる
- ファイルが重複または不足している
- `asmdef` の設定が変更されている
- `asmdef.meta` の設定が変更されている
- コンポーネントの `.meta` の guid が変更されている

### ライブラリー（アセンブリ）について

パッケージをプロジェクトにインストールすると  
スクリプトが自動的にコンパイルされ、以下のDLLが生成されます。

|アセンブリ|説明|UniVgo|UniVgo.Editor|
|:---|:---|:---:|:---:|
|DepthFirstScheduler|深さ優先スケジューラー|*|*|
|ShaderProperty.Runtime|シェーダーのプロパティー情報|*|*|
|UniGLTFforUniVgo|UniGLTF（UniVGO用）|*|*|
|UniUnlit|Unlit シェーダー ユーティリティー|-|-|
|UniUnlit.Editor|Unlit シェーダー ユーティリティー|-|*|
|UniVgo|VGO メインプログラム|*|*|
|UniVgo.Editor|VGO の入出力|-|*|

- UniVgo, UniVgo.Editor それぞれについて、依存関係にあるDLLに * を付けています。
- DepthFirstScheduler, ShaderProperty, UniUnlit は UniVRM (©vrm-c) に梱包されているプログラムです。
- UniVRM と UniVGO を併用する場合は、UniVgo を取得した際に梱包されていた 重複するファイル（DepthFirstScheduler, ShaderProperty, UniUnlit）を削除する必要があります。
  また、それにより UniVgo にてエラーが表示される場合、UniVgo, UniGLTFforUniVgo を `Assets`フォルダーに移動してください。

___
## VGO の仕様について

VGO の [README.ja.md](https://github.com/izayoijiichan/VGO/blob/master/README.ja.md) に記載いたしております。

___
## 謝辞

glTFに関する仕様を公開してくださっている KhronosGroup 様をはじめとする皆様、  
glTFに関するプログラムを開発・公開してくださっている ousttrue 様、  
VRMに関する仕様及び関連プログラムを公開・配布してくださっているVRMコンソーシアム様、株式会社ドワンゴ様、  
Unityを開発してくださっている Unity Technologies 様、  
その他関連する皆様には心より感謝いたします。  
この場を借りて御礼申し上げます。

___
最終更新日：2020年1月1日  
編集者：十六夜おじいちゃん

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
