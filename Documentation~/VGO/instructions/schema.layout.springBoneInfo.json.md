# VGO

## Spring bone information schema

### vgo.springBoneInfo

The vgo spring bone information.

|definition name|description|type|required|remarks|
|:---|:---|:---|:---:|:---|
|springBoneGroups|List of spring bone groups.|vgo.springBoneGroup[]|||
|colliderGroups|List of spring bone collider groups.|vgo.springBoneColliderGroup[]|||

#### vgo.springBoneGroup

A spring bone group.

|definition name|description|type|required|setting value|default value|
|:---|:---|:---|:---:|:---:|:---:|
|comment|TComments on this component.|string||||
|dragForce|The drag force.|float||[0.0, 1.0]|0.0|
|stiffnessForce|The stiffness force.|float||[0.0, 4.0]|1.0|
|gravityDirection|Direction of gravity.|float[3]||x, y, z|0, 0, 0|
|gravityPower||float||[0.0, 2.0]|1.0|
|rootBones|The indices of the root bone nodes.|int[]||||
|hitRadius||float||[0.0, 0.5]|0.1|
|colliderGroups|The indices of the collider groups.|int[]||||
|drawGizmo|Whether to draw Gizmo.|bool||true / false|false|
|gizmoColor|The Gizmo color.|float[3]||r, g, b|1.0, 0.92, 0.016|

#### vgo.springBoneColliderGroup

A spring bone collider group.

|definition name|description|type|required|setting value|default value|
|:---|:---|:---|:---:|:---|:---:|
|colliders|An array of the srping bone collider.|vgo.vgoSpringBoneCollider[]||||
|gizmoColor|The Gizmo color.|float[3]||r, g, b|1.0, 0.0, 1.0|

##### vgo.springBoneCollider

|definition name|description|type|required|setting value|default value|
|:---|:---|:---|:---:|:---|:---:|
|colliderType|The type of the spring bone collider.|enum||0: Sphere|0|
|offset|The offset position from the game object.|float[3]||x, y, z|0, 0, 0|
|radius|The radius of the collider.|float||||


#### JSON example (layout.nodes)

```json
{
    "springBoneInfo": {
        "springBoneGroups": [
            {
                "comment": "スカーフ",
                "dragForce": 0.4,
                "stiffnessForce": 1.1,
                "gravityDirection": [ 0, -1, 0 ],
                "gravityPower": 0.1,
                "rootBones": [
                    296
                ],
                "hitRadius": 0.02,
                "colliderGroups": [
                    0,
                    1,
                    2,
                    3
                ],
                "gizmoColor": [ 1, 0.92, 0.016 ]
            },
            {
                "comment": "マント",
                "dragForce": 0.4,
                "stiffnessForce": 0.1,
                "gravityDirection": [ 0, -1, 0 ],
                "gravityPower": 0.2,
                "rootBones": [
                    193,
                    204,
                    215,
                    226,
                    237,
                    248,
                    259,
                    270,
                    281
                ],
                "hitRadius": 0.02,
                "colliderGroups": [
                    0,
                    1,
                    2,
                    3
                ],
                "drawGizmo": true,
                "gizmoColor": [ 1, 0.92, 0.016 ]
            }
        ],
        "colliderGroups": [
            {
                "colliders": [
                    {
                        "colliderType": 0,
                        "offset": [ 0, 0, 0 ],
                        "radius": 0.1
                    }
                ],
                "gizmoColor": [ 1, 0, 1 ]
            },
            {
                "colliders": [
                    {
                        "colliderType": 0,
                        "offset": [ 0, 0, 0 ],
                        "radius": 0.1
                    }
                ],
                "gizmoColor": [ 1, 0, 1 ]
            },
            {
                "colliders": [
                    {
                        "colliderType": 0,
                        "offset": [ 0, 0, 0 ],
                        "radius": 0.1
                    }
                ],
                "gizmoColor": [ 1, 0, 1 ]
            },
            {
                "colliders": [
                    {
                        "colliderType": 0,
                        "offset": [ 0, 0, 0 ],
                        "radius": 0.1
                    }
                ],
                "gizmoColor": [ 1, 0, 1 ]
            }
        ]
    }
}
```
___
Last updated: 1 December, 2020  
Editor: Izayoi Jiichan

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
