# UniVGO 使用 マニュアル

UniVGO の使用方法です。

___
## 基本事項

### バージョン

このマニュアルが説明する内容は以下のバージョンに対してのものとなります。

|No|項目|値|
|:---:|:---|:---:|
|1|Unity バージョン|2019.4|
|2|UniVGO バージョン|2.1.0|
|3|VGO 仕様バージョン|2.1|

### 対応 Unity コンポーネント

VGOが対応する Unity コンポーネントは以下の通りです

|No|項目|場所|詳細|
|:---:|:---|:---|:---|
|1|Vgo Generator|Root|VGO情報を管理するためのものです。|
|2|Vgo Right|Root / Child|VGOに権利情報を付与することができます。|
|3|Animator|Root|GameObjectに Human Avatar を設定することができます。|
|4|Collider|Child|GameObjectに衝突判定を設定することができます。|
|5|Rigidbody|Child|GameObjectに物理特性を設定し、動かすことができるようになります。|
|6|Light|Child|GameObjectに光源を設定することができます。|
|7|Particle System|Child|GameObjectにパーティクルを設定することができます。|
|8|Skybox|Child|シーンにスカイボックスを設定することができます。|
|9|Vgo Spring Bone Group|Child|スプリング ボーン（揺れもの）の設定ができるようになります。|
|10|Vgo Spring Bone Collider Group|Child|スプリング ボーンに対するコライダーを設定することができます。|

### 使用可能シェーダー

対応しているシェーダーは以下の通りです。

|No|シェーダー名|備考|
|:---:|:---|:---|
|1|Standard|標準シェーダー|
|2|Particles/Standard Surface|Particle System 専用シェーダー|
|3|Particles/Standard Unlit|Particle System 専用 Unlit シェーダー|
|4|Skybox/6 Sided|Skybox 6面 シェーダー|
|5|Skybox/Panoramic|Skybox パノラマ シェーダー|
|6|Skybox/Procedural|Skybox 手続型 シェーダー|
|7|Unlit/Color||
|8|Unlit/Texture||
|9|Unlit/Transparent||
|10|Unlit/Transparent Cutout||
|11|UniGLTF/StandardVColor|Vertex Color シェーダー|
|12|UniGLTF/Unlit||
|13|VRM/MToon||

- Unlit系のシェーダーは光源の影響を受けません。その代わり処理負荷が小さくなります。
- Skybox/Cubemap には対応していません。

___
## VGO の作成（人型アバターの場合）

### 1. シーンの読み込み

UniVGO サンプル プロジェクトを使用する場合は`ExportScene`を読み込みます。  
それ以外の新規で作成する場合は任意のシーンで作業します。


### 2. モデルの設定

`Animator` コンポーネントの `Avatar` に `Human Avatar` が設定されたモデルを使用します。

【GameObject】

|No|コンポーネント|説明|
|:---:|:---|:---|
|1|(Name)|任意の名前を設定します。|
|2|Transform|初期値である必要があります。|
|3|Animator|付与しておきます。|
|4|Vgo Right|自由に設定します。|
|5|Vgo Generator|付与するだけでOKです。|

コンポーネントの順序は動作に関係ありません。

【Transform】

|No|項目|値|
|:---:|:---|:---|
|1|Position|X: 0, Y: 0, Z: 0|
|2|Rotation|X: 0, Y: 0, Z: 0|
|3|Scale|X: 1, Y: 1, Z: 1|

【Animator】

|No|項目|値|
|:---:|:---|:---|
|1|Avatar|(Human Avatar)|

【Vgo Right】

|No|項目|説明|備考|
|:---:|:---|:---|:---|
|1|Title|作品の名前です。|必須|
|2|Author|クリエイターの名前です。|必須|
|3|Organization|クリエイターの所属する組織です。||
|4|Created Date|作品の作成日です。|形式の規定はありません。|
|5|Updated Date|作品の更新日です。|形式の規定はありません。|
|6|Version|作品のバージョンです。|形式の規定はありません。|
|7|Distribution Url|配布URLです。||
|8|License Url|ライセンスの記載されたURLです。||

【Vgo Generator】

|No|項目|説明|値|
|:---:|:---|:---|:---:|
|1|Name|生成ツールの名前です。|UniVGO|
|2|Version|生成ツールのバージョンです。|2.1.0|

ユーザーが設定可能な項目はありません。  
ジェネレーター情報が古い場合にはコンポーネントを一度削除して、再度付与してください。

### 3. BlendShape

必要があれば顔の BlendShape の設定を行います。  
（UniVGOを使用したアプリがこの設定を参照することがあります）

`Hierarchy` タブにて 顔の GameObject を選択します。

`Skinned Mesh Renderer` コンポーネントが `BlendShapes` のパラメーターを持っていることを確認します。

`Vgo BlendShape` コンポーネントを新しく付与します。

次に `Project` タブ上で右クリックをし、  
`Create > VGO2 > BlendShapeConfiguration` で設定ファイルを作成します。

【Vgo BlendShape】

|No|項目|説明|備考|
|:---:|:---|:---|:---|
|1|Kind|BlendShapeの種類です。`Face`を選択します。|必須|
|2|Face Parts|BlendShapeが顔のどのパーツであるか関連付けます。|任意|
|3|Blinks|瞬きの際に使用する BlendShape を登録します。|任意|
|4|Visemes|口形素を関連付けます。母音のみでもOKです。|任意|
|5|Presets|プリセットを登録します。|任意|

BlendShape の index は 一番上を 0 として順にカウントします。

設定が終わりましたら、`Vgo BlendShape` コンポーネントの `BlendShapeConfiguration` に設定ファイルを設定します。

### 4. SpringBone

必要があれば SpringBone の設定を行います。

`Hierarchy` タブにて 任意の子（Root以外）の GameObject を選択します。  
（ここでは `SpringBoneManager` という名前のゲームオブジェクトとします。）

`SpringBoneManager` に `Vgo Spring Bone Group` コンポーネントを新しく付与します。  
（複数のグループを使用する場合にはさらにコンポーネント付与してください。）

【Vgo Spring Bone Group】

|No|項目|説明|必須|選択値|既定値|
|:---:|:---|:---|:---:|:---:|:---:|
|1|Comment|このグループをユーザー識別するための名前です。|任意|||
|2|Drag Force|抗力です。値が大きいほど揺れにくくなります。|必須|[0.0 - 1.0]|0.2|
|3|Stiffness Force|剛性です。値が大きいほど元の状態に戻りやすくなります。|必須|[0.0 - 4.0]|1.0|
|4|Gravity Direction|重力の向きです。|必須||x: 0.0, y: -1.0, z: 0.0|
|5|Gravity Power|重力の大きさです。|必須|[0.0 - 2.0]|0.2|
|6|Root Bones|揺らしたいボーンの根元のゲームオブジェクトを指定します。<br>ルートボーンを複数指定することで設定を同じグループとしてまとめることができます。|必須|||
|7|Hit Radius|各ボーンの当たり判定となる球の半径です。|必須|[0.0 - 0.5]|0.02|
|8|Collider Groups|このスプリング ボーン グループが検知するコライダーのグループです。|任意|||
|9|Draw Gizmo|Editor が Gizmo を描画する際に Spring Bone を描画します。|必須|true / false|false|
|10|Gizmo Color|Spring Bone の描画色です。|必須||yellow|

必要があれば SpringBoneCollider の設定を追加で行います。

コライダーを設置したい箇所の GameObject を選択します。  
（髪であれば頭など。）

GameObject に `Vgo Spring Bone Collider Group` コンポーネントを新しく付与します。

【Vgo Spring Bone Collider Group】

|No|項目|説明|必須|選択値|既定値|
|:---:|:---|:---|:---:|:---:|:---:|
|1|Colliders|スプリング ボーン コライダーを設定します。|必須|||
|2|Gizmo Color|スプリング ボーン コライダー の描画色です。|必須||magenta|

【Vgo Spring Bone Collider】

|No|項目|説明|必須|選択値|既定値|
|:---:|:---|:---|:---:|:---:|:---:|
|1|Collider Type|コライダーの種類です。|必須|Sphere|Sphere|
|2|Offset|GameObject からの相対位置です。|必須||x: 0.0, y: 0.0, z: 0.0|
|3|Radius|球の半径です。|必須|[0.0 - 1.0]|0.0|

___
## VGO の作成（人型アバター以外）

### 1. シーンの読み込み

UniVGO サンプル プロジェクトを使用する場合は`ExportScene`を読み込みます。  
それ以外の新規で作成する場合は任意のシーンで作業します。


### 2. Rootの作成と設定

シーンにVGO用の`GameObject`を作成します。  
名前は任意ですが、ここでは「VGO」とします。

![image1](https://github.com/izayoijiichan/vgo2/blob/master/Documentation~/UniVGO/Images/421_Hierarchy_VGO.png)

「VGO」の設定をしていきます。

![image1](https://github.com/izayoijiichan/vgo2/blob/master/Documentation~/UniVGO/Images/422_Inspector_VGO.png)

【GameObject】

|No|コンポーネント|説明|
|:---:|:---|:---|
|1|(Name)|任意の名前を設定します。|
|2|Transform|初期値である必要があります。|
|3|Vgo Right|自由に設定します。|
|4|Vgo Meta|付与するだけでOKです。|

コンポーネントの順序は動作に関係ありません。

【Transform】

|No|項目|値|
|:---:|:---|:---|
|1|Position|X: 0, Y: 0, Z: 0|
|2|Rotation|X: 0, Y: 0, Z: 0|
|3|Scale|X: 1, Y: 1, Z: 1|

【Vgo Right】

|No|項目|説明|備考|
|:---:|:---|:---|:---|
|1|Title|作品の名前です。|必須|
|2|Author|クリエイターの名前です。|必須|
|3|Organization|クリエイターの所属する組織です。||
|4|Created Date|作品の作成日です。|形式の規定はありません。|
|5|Updated Date|作品の更新日です。|形式の規定はありません。|
|6|Version|作品のバージョンです。|形式の規定はありません。|
|7|Distribution Url|配布URLです。||
|8|License Url|ライセンスの記載されたURLです。||

【Vgo Generator】

|No|項目|説明|値|
|:---:|:---|:---|:---:|
|1|Generator Name|生成ツールの名前です。|UniVGO|
|2|Generator Version|生成ツールのバージョンです。|2.1.0|

ユーザーが設定可能な項目はありません。  
メタ情報が古い場合にはコンポーネントを一度削除して、再度付与してください。


### 3. Childの作成と設定

「VGO」の子に`GameObject`を配置していきます。  
自由に配置することができます。  

#### 3-1. Skybox

シーン内に1つだけ配置することができます。  
Skyboxマテリアルを設定します。  
設定値をシーンに反映するためにはスクリプトに処理が必要です。  
`Cubemap`はサポートされていません。

#### 3-2. Light

`Realtime`設定のみ機能し、`Baked`は反映されません。  
また、`Directional Light`をVGOに含める場合、シーン内に初期配置されている`Directional Light`を削除するか非アクティブにしておきます。

#### 3-3. Particle System

`Scene View`にてエフェクトを確認しながら設定することができます。  
シェーダーはパーティクル用のシェーダーが使用できます。

#### 3-4. それ以外の通常オブジェクト

`Light`, `Particle System`以外の通常のオブジェクトです。  
タイプ別に A か B か C かに分かれます。

##### A) 静止物

床や建物など、衝突しても動くことのない物体です。  
基本的にはこちらを設定していきます。

【GameObject】

|No|コンポーネント|説明|
|:---:|:---|:---|
|1|(Name)|任意の名前を設定します。|
|2|Transform|自由に設定します。|
|3|Collider|Box / Capsule / Sphere|

【Collider】

|No|項目|説明|
|:---:|:---|:---|
|1|Is Trigger|□（チェックオフ）|

以下は床（Floor）の設定例です。

![image1](https://github.com/izayoijiichan/vgo2/blob/master/Documentation~/UniVGO/Images/431_Hierarchy_Floor.png)

![image1](https://github.com/izayoijiichan/vgo2/blob/master/Documentation~/UniVGO/Images/432_Inspector_Floor.png)

床の大きさは自由ですが、コライダーの厚さは0.3m以上となるよう調整したほうがよいです。  
厚みがないと物体が高速で落下した際に床を貫通することがあります。

##### B) 衝突時に動いてよい物

アバターやその他物体が衝突した際に動く物体です。  
設定するオブジェクトの個数は必要範囲内に留めてください。

【GameObject】

|No|コンポーネント|説明|
|:---:|:---|:---|
|1|(Name)|重複は避けたほうがよいです。|
|2|Transform|自由に設定します。|
|3|Collider|Box / Capsule / Sphere|
|4|Rigidbody|必ず付与します。|

【Collider】

|No|項目|説明|
|:---:|:---|:---|
|1|Is Trigger|□（チェックオフ）|

【Rigidbody】

|No|項目|説明|
|:---:|:---|:---|
|1|Mass|適切な重さを設定します。|
|2|Use Gravity|■（特性によってはチェックオフも可）|
|3|Is Kinematic|□（チェックオフ）|
|4|Constraints|自由に設定します。|

##### C) 衝突判定の必要ない物

【GameObject】

|No|コンポーネント|説明|
|:---:|:---|:---|
|1|(Name)|任意の名前を設定します。|
|2|Transform|自由に設定します。|

衝突判定の必要ない物については  
Collider, Rigidbody のどちらのコンポーネントも付与しません。

親のオブジェクトに付与したコライダーが判定の範囲をカバーしている場合もこのパターンになります。

___
## VGO の出力（エクスポート）

すべての設定が完了しましたらVGOファイル (.vgo) を出力します。

![image1](https://github.com/izayoijiichan/vgo2/blob/master/Documentation~/UniVGO/Images/501_Export.png)

Hierarchy にて 出力したい GameObject を選択します。

Inspector の`Vgo Generator`の出力条件を設定します。

|名前|選択肢|
|:---:|:---|
|Geometry Coordinates|Left Hand: 左手系<br>Right Hand: 右手系|
|UV Coordinates|Bottom Left: 左下<br>Top Left: 左上|
|JSON or BSON|JSON<br>BSON|
|Crypt Algorithms|None: 暗号化なし<br>AES<br>Base64|

Unity用に最適化する場合には  
`Geometry Coordinates` を `Left Hand` に、  
`UV Coordinates` を `Bottom Left` にします。

（既定値がそうなっています。）

最後に`Export VGO`ボタンを押下します。

エラーがなければ、VGOファイルが指定したフォルダーに出力されます。  
エラーが発生している場合は、Console にエラー内容が表示されます。

___
## VGO の取込（インポート）

### 1. VGOファイルの準備

VGOファイル (.vgo) を準備します。

### 1-1. VGOファイルがない場合

UniVGOを使用しての初期インポートはできません。

Unityが Humanoid のインポートをサポートしているファイル フォーマットのファイル（.fbx, .dae, .3ds, .dxf, .obj, .blender, .max, etc..）を準備する必要があります。

そのファイルを Unity の機能を使ってインポートします。

人型アバターの場合、このタイミングにて Rig を正しく設定しておく必要があります。

### 2. 取込

VGOファイルを `Assets` 配下の任意の場所に配置します。

エラーが発生した場合は、Console にエラー内容が表示されます。  
エラーが発生していなければ取込は完了しています。

Unity Editor の `Project` ウィンドウ（タブ）から VGOファイルを選択し、`Hierarchy` にドロップすることでオブジェクトを配置できます。

### 3. アセットの抽出

Unity Editor の `Project` ウィンドウ（タブ）から VGOファイルを探し、選択します。  

![image1](https://github.com/izayoijiichan/vgo2/blob/master/Documentation~/UniVGO/Images/620_Import.png)

`Inspector` ウィンドウ（タブ）に `Vgo Scripted Importer` が表示されます。  

`Material and Textures` の `Extract` ボタンをクリックすると、  
テクスチャーとマテリアルの抽出が開始されます。

`Apply` ボタンをクリックすることで抽出が確定します。

___
## VGO のランタイムロード

### 1. シーンの読み込み

UniVGOサンプルプロジェクトを使用する場合は「LoadScene」を読み込みます。

![image1](https://github.com/izayoijiichan/vgo2/blob/master/Documentation~/UniVGO/Images/710_Load.png)


### 2. ファイルの設定

「Hierarchy」にて`VgoLoader`ゲームオブジェクトを選択し、  
「Inspector」にて`Local File Path`に実行時に読み込みたいVGOファイルのフルパスを入力します。

![image1](https://github.com/izayoijiichan/vgo2/blob/master/Documentation~/UniVGO/Images/721_Hierarchy.png)

![image1](https://github.com/izayoijiichan/vgo2/blob/master/Documentation~/UniVGO/Images/722_Inspector.png)

### 3. ゲーム実行

プレイボタンを押し、ゲームを実行します。  
VGOファイルが読み込まれることを確認します。  
エラーが発生している場合は、Console にエラー内容が表示されます。

### スクリプト

自分でスクリプトを書く場合には以下のように記述します。

~~~csharp
    using UnityEngine;
    using UniVgo2;

    public class RuntimeLoadBehaviour : MonoBehaviour
    {
        private void Start()
        {
            var importer = new VgoImporter();

            importer.Load(filePath);

            importer.ReflectSkybox(Camera.main);
        }
    }
~~~

___
## VGOが使用できるアプリ

現在２つのアプリが対応しています。

### VOVOLA

VR-HMD（ヘッドマウントディスプレイ）が不要な簡易3D バーチャルYouTuber アプリです。  
ルームにVGOファイルを指定することができます。  
マルチプレイでは複数人が同じルームに入り通話をすることができます。

![image1](https://github.com/izayoijiichan/vgo2/blob/master/Documentation~/UniVGO/Images/810_vovola.jpg)

https://vovola.wixsite.com/website

### VISHOP

バーチャルショッピングシステムです。  
ショップにVGOファイルを指定することができます。

![image1](https://github.com/izayoijiichan/vgo2/blob/master/Documentation~/UniVGO/Images/820_vishop.jpg)

https://vishop.azurewebsites.net

___
最終更新日：2020年12月1日  
編集者：十六夜おじいちゃん

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
