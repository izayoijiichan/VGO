# VGO

## Animation schema

### vgo.animation

The animation.

|definition name|description|type|required|setting value|default value|
|:---|:---|:---|:---:|:---|:---:|
|name|The name of the object.|string||||
|enabled|Whether this component is enabled.|bool||true / false|true|
|clipIndex|The index of default animation clip.|int||||
|localBounds|AABB of this Animation animation component in local space.|vgo.bounds||||
|playAutomatically|Should the default animation clip (the Animation.clip property) automatically start playing on startup?|bool||true / false||
|animatePhysics|When turned on, animations will be executed in the physics loop.|bool||true / false|false|
|cullingType|Controls culling of this Animation component.|enum||0: AlwaysAnimate<br>1: BasedOnRenderers<br>2: BasedOnClipBounds<br>3: BasedOnUserBounds|0|
|wrapMode|The default wrap mode used in the animation state.|enum||0: Default<br>1: Once<br>2: Loop<br>4: PingPong<br>8: ClampForever|0|

### vgo.animationClip

The animation clip.

|definition name|description|type|required|setting value|default value|
|:---|:---|:---|:---:|:---|:---:|
|name|The name of the object.|string|true|||
|legacy|Set to true if the AnimationClip will be used with the Legacy Animation component.|bool||true / false|true|
|localBounds|AABB of this Animation Clip in local space of Animation component that it is attached too.|vgo.bounds||||
|wrapMode|The default wrap mode used in the animation state.|enum||0: Default<br>1: Once<br>2: Loop<br>4: PingPong<br>8: ClampForever|0|
|curveBindings|Defines how a curve is attached to this animation clip.|vgo.animationCurveBinding[]||||

#### vgo.animationCurveBinding

A animation curve binding.

|definition name|description|type|required|setting value|default value|
|:---|:---|:---|:---:|:---:|:---:|
|type|The type of the property to be animated.|string|true|||
|propertyName|The name of the property to be animated.|string|true|||
|animationCurve|The animation curve.|vgo.animationCurve|true|||

#### vgo.animationCurve

A animation curve.

|definition name|description|type|required|setting value|default value|
|:---|:---|:---:|:---:|:---|:---:|
|keys|All keys defined in the animation curve.|vgo.keyframe[]||||
|preWrapMode|The behaviour of the animation before the first keyframe.|enum||0: Default<br>1: Once<br>2: Loop<br>4: PingPong<br>8: ClampForever|0|
|postWrapMode|The behaviour of the animation after the last keyframe.|enum||0: Default<br>1: Once<br>2: Loop<br>4: PingPong<br>8: ClampForever|0|

### vgo.keyframe

|definition name|description|type|required|setting value|default value|
|:---|:---|:---:|:---:|:---|:---:|
|time|The time of the keyframe.|float||||
|value|The value of the curve at keyframe.|float||||
|inTangent|The incoming tangent for this key.|float||||
|outTangent|The outgoing tangent for this key.|float||||
|inWeight|The incoming weight for this key.|float||||
|outWeight|The outgoing weight for this key.|float||||
|weightedMode|Weighted mode for the keyframe.|enum||0: None<br>1: In<br>2: Out<br>3: Both|0|

### vgo.bounds

|definition name|description|type|required|setting value|default value|
|:---|:---|:---:|:---:|:---|:---:|
|center|The center of the bounding box.|float[3]||x, y, z|0.0, 0.0, 0.0|
|size|The total size of the box.|float[3]||x, y, z|0.0, 0.0, 0.0|


#### JSON example (layout)

```json
{
    "nodes": [
        {
            "name": "node1",
            "animation": {
                "name": "node1",
                "enabled": true,
                "clipIndex": 0,
                "playAutomatically": true,
                "animatePhysics": false,
                "cullingType": 0,
                "wrapMode": 0
            },
            "mesh": 0
        },
    ],
    "animationClips": [
        {
            "name": "FloatAnimation",
            "legacy": true,
            "wrapMode": 2,
            "curveBindings": [
                {
                    "type": "Transform",
                    "propertyName": "LocalPosition.x",
                    "animationCurve": {
                        "keys": [
                            {
                                "time": 0.0,
                                "value": 0.0,
                                "inTangent": 0.0,
                                "outTangent": 0.0,
                                "inWeight": 0.333333343,
                                "outWeight": 0.333333343,
                                "weightedMode": 0
                            },
                            {
                                "time": 1.0,
                                "value": 0.0,
                                "inTangent": 0.0,
                                "outTangent": 0.0,
                                "inWeight": 0.333333343,
                                "outWeight": 0.333333343,
                                "weightedMode": 0
                            },
                            {
                                "time": 2.0,
                                "value": 0.0,
                                "inTangent": 0.0,
                                "outTangent": 0.0,
                                "inWeight": 0.333333343,
                                "outWeight": 0.333333343,
                                "weightedMode": 0
                            }
                        ],
                        "preWrapMode": 8,
                        "postWrapMode": 8
                    }
                },
                {
                    "type": "Transform",
                    "propertyName": "LocalPosition.y",
                    "animationCurve": {
                        "keys": [
                            {
                                "time": 0.0,
                                "value": 0.0,
                                "inTangent": 0.0,
                                "outTangent": 0.0,
                                "inWeight": 0.333333343,
                                "outWeight": 0.333333343,
                                "weightedMode": 0
                            },
                            {
                                "time": 1.0,
                                "value": 0.2,
                                "inTangent": 0.0,
                                "outTangent": 0.0,
                                "inWeight": 0.333333343,
                                "outWeight": 0.333333343,
                                "weightedMode": 0
                            },
                            {
                                "time": 2.0,
                                "value": 0.0,
                                "inTangent": 0.0,
                                "outTangent": 0.0,
                                "inWeight": 0.333333343,
                                "outWeight": 0.333333343,
                                "weightedMode": 0
                            }
                        ],
                        "preWrapMode": 8,
                        "postWrapMode": 8
                    }
                },
                {
                    "type": "Transform",
                    "propertyName": "LocalPosition.z",
                    "animationCurve": {
                        "keys": [
                            {
                                "time": 0.0,
                                "value": 0.0,
                                "inTangent": 0.0,
                                "outTangent": 0.0,
                                "inWeight": 0.333333343,
                                "outWeight": 0.333333343,
                                "weightedMode": 0
                            },
                            {
                                "time": 1.0,
                                "value": 0.0,
                                "inTangent": 0.0,
                                "outTangent": 0.0,
                                "inWeight": 0.333333343,
                                "outWeight": 0.333333343,
                                "weightedMode": 0
                            },
                            {
                                "time": 2.0,
                                "value": 0.0,
                                "inTangent": 0.0,
                                "outTangent": 0.0,
                                "inWeight": 0.333333343,
                                "outWeight": 0.333333343,
                                "weightedMode": 0
                            }
                        ],
                        "preWrapMode": 8,
                        "postWrapMode": 8
                    }
                }
            ]
        }
    ]
}
```
___
Last updated: 1 February, 2021  
Editor: Izayoi Jiichan

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
