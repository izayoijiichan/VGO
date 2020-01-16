# VGO

VGO とは、Collider と Rigidbody の情報を格納可能な Unity 向け3Dデータフォーマットです。

## 特徴
- glTF (GLB) 2.0 の拡張フォーマットになります。
- ノードに Collider, Rigidbody, Light, 権利情報に関する拡張定義を追加しています。  
- Unity の GameObject の Transform, Rigidbody, Collider, PhysicMaterial, Mesh, Material, Texture, Light を保存することができます。
- Standard, Unlit, MToon のシェーダー設定を保存することができます。

___
## glTFのJSONスキーマ

以下の拡張定義が追加されます。

### glTF.extensionsUsed

|定義名|説明|
|:---|:---|
|VGO|VGO を使用するという宣言です。|
|VGO_nodes|VGO_nodes を使用するという宣言です。|
|VGO_materials|VGO_materials を使用するという宣言です。|
|KHR_materials_unlit|マテリアルに Unlit シェーダーを使用可能であるという宣言です。|
|VRMC_materials_mtoon|マテリアルに MToon シェーダーを使用可能であるという宣言です。|

### glTF.extensionsRequired

|定義名|説明|
|:---|:---|
|VGO|VGO 拡張へのサポートを必要とします。|
|VGO_nodes|VGO_nodes 拡張へのサポートを必要とします。|
|VGO_materials|VGO_materials 拡張へのサポートを必要とします。|
|KHR_materials_unlit|KHR_materials_unlit 拡張へのサポートを必要とします。|
|VRMC_materials_mtoon|VRMC_materials_mtoon 拡張へのサポートを必要とします。|

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
|light|ライト情報|
|right|VGO権利情報|

### glTF.materials.[*].extensions

|定義名|説明|
|:---|:---|
|KHR_materials_unlit|Unlit マテリアル情報|
|VGO_materials|VGO マテリアル情報|
|VRMC_materials_mtoon|MToon マテリアル情報|

___
## 拡張定義の詳細

### vgo.meta（VGOメタ情報）

|定義名|説明|型|固定値|
|:---|:---|:---:|:---:|
|generatorName|生成ツールの名前です。|string|UniVGO|
|generatorVersion|生成ツールのバージョンです。|string|0.6.0|
|specVersion|VGOの仕様バージョンです。|string|0.4|

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

### node.vgo.collider（コライダー）

|定義名|説明|型|設定値|Box|Capsule|Sphere|
|:---|:---|:---:|:---:|:---:|:---:|:---:|
|enabled|コライダーが有効かどうか。|bool|true / false|*|*|*|
|type|コライダーの種類です。|string|Box / Capsule / Sphere|*|*|*|
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
|useGravity|オブジェクトが重力の影響を受けるかどうか。|bool|true / false|
|isKinematic|物理が剛体に影響を与えるかどうかどうか。|bool|true / false|
|interpolation|補完のタイプです。|string|None / Interpolate / Extrapolate|
|collisionDetectionMode|衝突の検知のモードです。|string|Discrete / Continuous / ContinuousDynamic / ContinuousSpeculative|
|constraints|剛体の動きを制限するフラグです。|int|FreesePositionX(2) \| FreesePositionY(4) \| FreesePositionZ(8) \| FreeseRotationX(16) \| FreeseRotationY(32) \| FreeseRotationZ(64)|

### node.vgo.light（ライト）

|定義名|説明|型|設定値|既定値|Spot|Directional|Point|Rectangle|Disc|
|:---|:---|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|
|enabled|ライトが有効かどうか。|bool|true / false|true|*|*|*|*|*|
|type|ライトのタイプ。|string|Spot / Directional / Point / Rectangle / Disc|Spot|*|*|*|*|*|
|shape|スポットライトの形状。|string|Cone / Pyramid / Box|Cone|*|-|-|-|-|
|range|光の範囲。|float|[0, infinity]||*|-|*|*|*|
|spotAngle|ライトのスポットライトコーンの角度（度単位）。|float|[0, infinity]||*|-|-|-|-|
|areaSize|エリアライトのサイズ。|float[2]|x, y||-|-|-|*|-|
|areaRadius|エリアライトの半径。|float|[0, infinity]||-|-|-|-|*|
|color|ライトの色。|float[4]|r, g, b, a||*|*|*|*|*|
|lightmapBakeType|ライトマップのベイクタイプ。|string|Baked / Realtime / Mixed||*|*|*|*|*|
|intensity|ライトの輝度。|float|[0, infinity]||*|*|*|*|*|
|bounceIntensity|バウンス照明の輝度を定義する乗数。|float|[0, infinity]||*|*|*|*|*|
|shadows|この光が影を落とす方法。|string|None / Hard / Soft|None|*|*|*|*|*|
|shadowRadius|影の半径。|float|[0, infinity]||*|-|*|-|-|
|shadowAngle|影の角度。|float|[0, infinity]||-|*|-|-|-|
|shadowStrength|ライトの影の強さ。|float|[0, infinity]||-|*|*|-|-|
|shadowResolution|シャドウマップの解像度。|string|FromQualitySettings / Low / Medium / High / VeryHigh|FromQualitySettings|-|*|*|-|-|
|shadowBias|シャドウマッピング定数バイアス。|float|[0, infinity]||-|*|*|-|-|
|shadowNormalBias|シャドウマッピング法線ベースのバイアス。|float|[0, infinity]||-|*|*|-|-|
|shadowNearPlane|シャドウ ニア プレーン。|float|[0, infinity]||-|*|*|-|-|
|renderMode|レンダー モード。|string|Auto / ForcePixel / ForceVertex|Auto|*|*|*|*|*|
|cullingMask|カリング マスク。|int|[-1, infinity]|-1 (Everything)|*|*|*|*|*|

Cookie, Flare, Halo は対象外です。

### VGO_materials

|定義名|説明|型|備考|
|:---|:---|:---:|:---:|
|shaderName|シェーダー名。|string||

### VRMC_materials_mtoon

|定義名|説明|型|設定値|既定値|
|:---|:---|:---:|:---:|:---:|
|version||string||32|
|renderMode||string|opaque / cutout / transparent / transparentWithZWrite|opaque|
|cullMode||string|off / flont / back|off|
|renderQueueOffsetNumber||int|||
|litFactor||float[4]|[r, g, b, a]||
|litMultiplyTexture||int|||
|shadeFactor||float[4]|[r, g, b, a]||
|shadeMultiplyTexture||int|||
|cutoutThresholdFactor||float|[0.0, 1.0]||
|shadingShiftFactor||float|[-1.0, 1.0]||
|shadingToonyFactor||float|[0.0, 1.0]||
|shadowReceiveMultiplierFactor||float|[0.0, 1.0]||
|shadowReceiveMultiplierMultiplyTexture||int|||
|litAndShadeMixingMultiplierFactor||float|[0.0, 1.0]||
|litAndShadeMixingMultiplierMultiplyTexture||int|||
|lightColorAttenuationFactor||float|[0.0, 1.0]||
|giIntensityFactor||float|[0.0, 1.0]||
|normalTexture||int|||
|normalScaleFactor||float|||
|emissionFactor||float[3]|[r, g, b]||
|emissionMultiplyTexture||int|||
|additiveTexture||int|||
|rimFactor||float[4]|[r, g, b, a]||
|rimMultiplyTexture||int|||
|rimLightingMixFactor||float|[0.0, 1.0]||
|rimFresnelPowerFactor||float|[0.0, 100.0]||
|rimLiftFactor||float|[0.0, 1.0]||
|outlineWidthMode||string|none / worldCoordinates / screenCoordinates|none|
|outlineWidthFactor||float|[0.01, 1.0]||
|outlineWidthMultiplyTexture||int|||
|outlineScaledMaxDistanceFactor||float|[1.0, 10.0]||
|outlineColorMode||string|fixedColor / mixedLighting|fixedColor|
|outlineFactor||float[4]|[r, g, b, a]||
|outlineLightingMixFactor||float|[0.0, 1.0]||
|mainTextureLeftBottomOriginScale||float[2]|[x, y]||
|mainTextureLeftBottomOriginOffset||float[2]|[x, y]||
|uvAnimationMaskTexture||int|||
|uvAnimationScrollXSpeedFactor||float|||
|uvAnimationScrollYSpeedFactor||float|||
|uvAnimationRotationSpeedFactor||float|||

https://github.com/vrm-c/vrm-specification

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
        "VGO",
        "VGO_nodes",
        "VGO_materials",
        "KHR_materials_unlit",
        "VRMC_materials_mtoon"
    ],
    "extensionsRequired": [
        "VGO",
        "VGO_nodes",
        "VGO_materials",
        "KHR_materials_unlit",
        "VRMC_materials_mtoon"
    ],
    "extensions": {
        "VGO": {
            "meta": {
                "generatorName": "UniVGO",
                "generatorVersion": "0.6.0",
                "specVersion": "0.4"
            },
            "right": {
                "title": "Test Stage",
                "author": "Izayoi Jiichan",
                "organization": "Izayoi",
                "createdDate": "2020-01-01",
                "updatedDate": "2020-01-17",
                "version": "1.3",
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
                            "enabled": false,
                            "type": "Capsule",
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
                    "light":{
                        "enabled":true,
                        "type":"Point",
                        "shape":"",
                        "range":1.0,
                        "spotAngle":0.0,
                        "areaSize":[ 0.0, 0.0 ],
                        "areaRadius":0.0,
                        "color":[ 0.122,0.404,0.637,1.0 ],
                        "lightmapBakeType":"Realtime",
                        "intensity":1.0,
                        "bounceIntensity":1.0,
                        "shadows":"Soft",
                        "shadowRadius":1.0,
                        "shadowAngle":0.0,
                        "shadowStrength":1.0,
                        "shadowResolution":"Low",
                        "shadowBias":0.004,
                        "shadowNormalBias":0.26,
                        "shadowNearPlane":0.1,
                        "renderMode":"Auto",
                        "cullingMask":-1
                    },
                    "right": {
                        "title": "Capsule1",
                        "author": "Izayoi Jiichan",
                        "organization": "",
                        "createdDate": "2020-01-01",
                        "updatedDate": "2020-01-17",
                        "version": "0.4",
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
            "name": "MtoonMaterial1",
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
                "KHR_materials_unlit": {},
                "VGO_materials":{
                    "shaderName":"VRM/MToon"
                },
                "VRMC_materials_mtoon":{
                    "version":"32",
                    "renderMode":"opaque",
                    "cullMode":"back",
                    "renderQueueOffsetNumber":0,
                    "litFactor":[ 0.811,0.916,0.723,1.0 ],
                    "litMultiplyTexture":0,
                    "shadeFactor":[ 0.933,0.620,0.711,1.0 ],
                    "shadeMultiplyTexture":-1,
                    "cutoutThresholdFactor":0.5,
                    "shadingShiftFactor":0.0,
                    "shadingToonyFactor":0.9,
                    "shadowReceiveMultiplierFactor":1.0,
                    "shadowReceiveMultiplierMultiplyTexture":-1,
                    "litAndShadeMixingMultiplierFactor":1.0,
                    "litAndShadeMixingMultiplierMultiplyTexture":-1,
                    "lightColorAttenuationFactor":0.0,
                    "giIntensityFactor":0.1,
                    "normalTexture":-1,
                    "normalScaleFactor":1.0,
                    "emissionFactor":[ 0.0,0.0,0.0,1.0 ],
                    "emissionMultiplyTexture":-1,
                    "additiveTexture":-1,
                    "rimFactor":[ 0.0,0.0,0.0,1.0 ],
                    "rimMultiplyTexture":-1,
                    "rimLightingMixFactor":0.0,
                    "rimFresnelPowerFactor":1.0,
                    "rimLiftFactor":0.0,
                    "outlineWidthMode":"screenCoordinates",
                    "outlineWidthFactor":0.5,
                    "outlineWidthMultiplyTexture":-1,
                    "outlineScaledMaxDistanceFactor":1.0,
                    "outlineColorMode":"fixedColor",
                    "outlineFactor":[ 0.0,0.0,0.0,1.0 ],
                    "outlineLightingMixFactor":1.0,
                    "mainTextureLeftBottomOriginScale":[ 1.0,1.0 ],
                    "mainTextureLeftBottomOriginOffset":[ 0.0,0.0 ],
                    "uvAnimationMaskTexture":-1,
                    "uvAnimationScrollXSpeedFactor":0.0,
                    "uvAnimationScrollYSpeedFactor":0.0,
                    "uvAnimationRotationSpeedFactor":0.0
                }
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
最終更新日：2020年1月17日  
編集者：十六夜おじいちゃん

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
