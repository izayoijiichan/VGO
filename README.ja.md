# VGO

VGO とは、Collider と Rigidbody の情報を格納可能な Unity 向け3Dデータフォーマットです。

## 特徴
- glTF (GLB) 2.0 の拡張フォーマットになります。
- ノードに Collider, Rigidbody, 権利情報に関する拡張定義を追加しています。  
- Unity の GameObject の Transform, Rigidbody, Collider, PhysicMaterial, Mesh, Material, Texture を保存することができます。

___
## glTFのJSONスキーマ

以下の拡張定義が追加されます。

### glTF.extensionsUsed

|定義名|説明|
|:---|:---|
|KHR_materials_unlit|マテリアルに Unlit シェーダーを使用可能であるという宣言です。|
|VGO|VGO を使用するという宣言です。|
|VGO_nodes|VGO_nodes を使用するという宣言です。|

### glTF.extensionsRequired

|定義名|説明|
|:---|:---|
|KHR_materials_unlit|KHR_materials_unlit 拡張へのサポートを必要とします。|
|VGO|VGO 拡張へのサポートを必要とします。|
|VGO_nodes|VGO_nodes 拡張へのサポートを必要とします。|

### glTF.extensions

|定義名|説明|
|:---|:---|
|VGO|VGO情報|

### glTF.extensions.VGO

|定義名|説明|
|:---|:---|
|meta|VGOメタ情報|
|right|VGO権利情報|

### glTF.nodes.[*].extensions

|定義名|説明|
|:---|:---|
|VGO_nodes|ノードVGO情報|

### glTF.nodes.[*].extensions.VGO_nodes

|定義名|説明|
|:---|:---|
|gameObject|ゲームオブジェクト情報|
|colliders|コライダー情報|
|rigidbody|剛体情報|
|right|VGO権利情報|

### glTF.materials.[*].extensions

|定義名|説明|
|:---|:---|
|KHR_materials_unlit|Unlitマテリアル情報|

___
## 拡張定義の詳細

### vgo.meta（VGOメタ情報）

|定義名|説明|型|固定値|
|:---|:---|:---:|:---:|
|generatorName|生成ツールの名前です。|string|UniVGO|
|generatorVersion|生成ツールのバージョンです。|string|0.4.0|
|specVersion|VGOの仕様バージョンです。|string|0.2|

### vgo.right（権利情報）

|定義名|説明|型|備考|
|:---|:---|:---:|:---|
|title|作品の名前です。|string|必須|
|author|クリエイターの名前です。|string|必須|
|organization|クリエイターの所属する組織です。|string||
|createdDate|作品の作成日です。|string|形式の規定はありません。|
|updatedDate|作品の更新日です。|string|形式の規定はありません。|
|version|作品のバージョンです。|string|形式の規定はありません。|
|distributionUrl|配布URLです。|string|URL形式|
|licenseUrl|ライセンスの記載されたURLです。|string|URL形式|

### node.vgo.gameobject（ゲームオブジェクト）

|定義名|説明|型|設定値|既定値|
|:---|:---|:---:|:---:|:---:|
|isActive|ゲームオブジェクトが活性かどうか。|bool|true / false|true|
|isStatic|ゲームオブジェクトが静的かどうか。|bool|true / false|false|
|tag|ゲームオブジェクトに付けられたタグ。|string||Untagged|
|layer|ゲームオブジェクトの位置するレイヤー。|int|[0, 31]|0|

### node.vgo.collier（コライダー）

|定義名|説明|型|設定値|Box|Capsule|Sphere|
|:---|:---|:---:|:---:|:---:|:---:|:---:|
|type|コライダーの種類です。|string|Box / Capsule / Sphere|*|*|*|
|enabled|コライダーが有効かどうか。|bool|true / false|*|*|*|
|isTrigger|コライダーがトリガーかどうか。|bool|true / false|*|*|*|
|center|コライダーの中心座標です。（単位はm）|float[3]|[x, y, z]|*|*|*|
|size|コライダーのサイズです。（単位はm）|float[3]|[x, y, z]|*|-|-|
|radius|コライダーの半径です。|float|[0, infinity]|-|*|*|
|height|コライダーの高さです。|float|[0, infinity]|-|*|-|
|direction|コライダーの向きです。|int|0:X / 1:Y / 2:Z|-|*|-|
|physicMaterial|共有物理特性マテリアル情報です。|node.vgo.physicMaterial||*|*|*|

### node.vgo.physicMaterial（物理特性マテリアル）

|定義名|説明|型|設定値|
|:---|:---|:---:|:---:|
|dynamicFriction|移動している物体に対する摩擦です。|float|[0.0, 1.0]|
|staticFriction|面上で静止しているオブジェクトに使用される摩擦です。|float|[0.0, 1.0]|
|bounciness|表面にどれだけ弾性があるか。|float|[0.0, 1.0]|
|frictionCombine|衝突するオブジェクト間の摩擦の処理タイプです。|string|Average / Minimum / Maximum / Multiply|
|bounceCombine|衝突するオブジェクト間の跳ね返しの処理タイプです。|string|Average / Minimum / Maximum / Multiply|

### node.vgo.rigidbody（剛体）

|定義名|説明|型|設定値|
|:---|:---|:---:|:---:|
|mass|物体の質量です。（単位はkg）|float|[0.0000001, 1000000000]|
|drag|力によって動く際にオブジェクトに影響する空気抵抗の大きさです。|float|[0.0, infinity]|
|angularDrag|トルクによって回転する際にオブジェクトに影響する空気抵抗の大きさです。|float|[0.0, infinity]|
|useGravity|オブジェクトが重力の影響を受けるかどうか。|boolean|true / false|
|isKinematic|物理が剛体に影響を与えるかどうかどうか。|boolean|true / false|
|interpolation|補完のタイプです。|string|none / interpolate / extrapolate|
|collisionDetectionMode|衝突の検知のモードです。|string|Discrete / Continuous / ContinuousDynamic / ContinuousSpeculative|
|constraints|剛体の動きを制限するフラグです。|int|FreesePositionX(2) \| FreesePositionY(4) \| FreesePositionZ(8) \| FreeseRotationX(16) \| FreeseRotationY(32) \| FreeseRotationZ(64)|

___
## glTFのJSONの構造例


### glTF.extensions
```json
JSON{
    "asset": {
    },
    "buffers": [
    ],
    "extensionsUsed": [
        "KHR_materials_unlit",
        "VGO",
        "VGO_nodes"
    ],
    "extensionsRequired": [
        "KHR_materials_unlit",
        "VGO",
        "VGO_nodes"
    ],
    "extensions": {
        "VGO": {
            "meta": {
                "generatorName": "UniVGO",
                "generatorVersion": "0.4.0",
                "specVersion": "0.2"
            },
            "right": {
                "title": "Test Stage",
                "author": "Izayoi Jiichan",
                "organization": "Izayoi",
                "createdDate": "2020-01-01",
                "updatedDate": "2020-01-08",
                "version": "1.1",
                "distributionUrl": "https://github.com/izayoijiichan/VGO",
                "licenseUrl": "https://github.com/izayoijiichan/VGO/blob/master/UniVgo/LICENSE.md"
            }
        }
    },
    "extras": {}
}
```

### glTF.nodes.[*].extensions
```json
JSON{
    "nodes": [
        {
            "name": "Capsule1",
            "translation": [ 1, 1, 1 ],
            "rotation": [ 0, 0, 0, 1 ],
            "scale": [ 0.5, 0.5, 0.5 ],
            "mesh": 0,
            "extensions": {
                "VGO_nodes": {
                    "gameObject": {
                        "isActive": false,
                        "isStatic": true,
                        "tag": "Player",
                        "layer": 2
                    },
                    "colliders": [
                        {
                            "type": "Capsule",
                            "enabled": false,
                            "isTrigger": false,
                            "center": [ 0, 0, 0 ],
                            "radius": 0.5,
                            "height": 2,
                            "direction": 1,
                            "physicMaterial":{
                                "dynamicFriction":0.6,
                                "staticFriction":0.6,
                                "bounciness":0.0,
                                "frictionCombine":"Average",
                                "bounceCombine":"Multiply"
                            }
                        }
                    ],
                    "rigidbody": {
                        "mass": 10,
                        "drag": 0,
                        "angularDrag": 0.05,
                        "useGravity": true,
                        "isKinematic": false,
                        "interpolation": "None",
                        "collisionDetectionMode": "Discrete",
                        "constraints": 36
                    },
                    "right": {
                        "title": "Capsule1",
                        "author": "Izayoi Jiichan",
                        "organization": "",
                        "createdDate": "2020-01-01",
                        "updatedDate": "2020-01-08",
                        "version": "0.2",
                        "distributionUrl": "",
                        "licenseUrl": ""
                    }
                }
            },
            "extras": {}
        }
    ]
}
```

### glTF.materials.[*].extensions
```json
JSON{
    "materials": [
        {
            "name": "UnlitMaterial1",
            "pbrMetallicRoughness": {
                "baseColorTexture": {
                    "index": 0,
                    "texCoord": 0
                },
                "baseColorFactor": [ 1, 1, 1, 1 ],
                "metallicFactor": 0,
                "roughnessFactor": 0.9
            },
            "alphaMode": "OPAQUE",
            "alphaCutoff": 0.5,
            "doubleSided": false,
            "extensions": {
                "KHR_materials_unlit": {}
            }
        }
    ]
}
```

___
## ツール

### UniVGO

VGOファイルを生成／出力／取り込み／ロードするためのツールです。

UniVGO の [README.ja.md](https://github.com/izayoijiichan/VGO/blob/master/UniVgo/README.ja.md) をご覧ください。

### VGO Parameter Viewer

VGOファイルの中身を確認するためのツールです。

以下のURLにて配布しています。

https://github.com/izayoijiichan/vgo.parameter.viewer

___
最終更新日：2020年1月8日  
編集者：十六夜おじいちゃん

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
