# UniVGO Use manual

How to use UniVGO.

___

## Basic matters

### Version

The contents described in this manual are for the following versions.

|No|item|value|
|:---:|:---|:---:|
|1|Unity version|2022.1|
|2|UniVGO version|2.4.11|
|3|VGO spec version|2.4|

### Supported Unity components

The following Unity components are supported by VGO

|No|item|place|details|
|:---:|:---|:---|:---|
|1|Vgo Generator|Root|It is for managing VGO information.|
|2|Vgo Right|Root / Child|You can add rights information to VGO.|
|3|Animator|Root|You can set Human Avatar in GameObject.|
|4|Animattion|Child|You can set animation in GameObject.|
|5|Collider|Child|You can set collision judgment for GameObject.|
|6|Rigidbody|Child|You can set physics to GameObject and move it|
|7|Light|Child|You can set the light source for GameObject.|
|8|Particle System|Child|You can set particles to GameObject.|
|9|Skybox|Child|You can set skybox to Scene.|
|10|Vgo Spring Bone Group|Child|You will be able to set the spring bone (swaying object).|
|11|Vgo Spring Bone Collider Group|Child|You can set the collider for the spring bone.|
|12|Cloth|Child|You can set cloth for GameObject.|

### Usable shaders

The supported shaders are as follows.

|No|render pipeline|shader name|descriptoin|
|:---:|:---:|:---|:---|
|1|BRP|Standard|Built-in Standard shader|
|2|BRP|Particles/Standard Surface|Particle System dedicated shader|
|3|BRP|Particles/Standard Unlit|Particle System dedicated unlit shader|
|4|BRP|Skybox/6 Sided|Skybox 6 sided shader|
|5|BRP|Skybox/Panoramic|Skybox panoramic shader|
|6|BRP|Skybox/Procedural|Skybox procedural shader|
|7|BRP|Unlit/Color||
|8|BRP|Unlit/Texture||
|9|BRP|Unlit/Transparent||
|10|BRP|Unlit/Transparent Cutout||
|11|BRP|UniGLTF/StandardVColor|Vertex Color shader|
|12|BRP|UniGLTF/Unlit||
|13|BRP|VRM/MToon||
|14|URP|Universal Render Pipeline/Lit|Universal Render Pipeline Lit shader|
|15|URP|Universal Render Pipeline/Simple Lit|Universal Render Pipeline Simple Lit shader|
|16|URP|Universal Render Pipeline/Unlit|Universal Render Pipeline Unlit shader|
|17|HDRP|HDRP/Lit|HD Render Pipeline Lit shader|
|18|HDRP|HDRP/Eye|HD Render Pipeline Eye shader|
|19|HDRP|HDRP/Hair|HD Render Pipeline Hair shader|
|20|BRP/URP/HDRP|lilToon|lilToon shader|

- Unlit shaders are not affected by light sources. Instead, the processing load is reduced.
- Skybox / Cubemap is not supported.

___

## Create VGO (for humanoid avatar)

### 1. Load scene

If you use the UniVGO sample project, load `ExportScene`.  
If you want to create a new one, work on any scene.

### 2. setting a model

Use a model with `Human Avatar` set in the `Avatar` of the `Animator` component.

[GameObject]

|No|component|description|
|:---:|:---|:---|
|1|(Name)|Set any name.|
|2|Transform|Must be the initial value.|
|3|Animator|Attach it.|
|4|Vgo Right|Set freely.|
|5|Vgo Generator|It is OK just to attach.|

The order of the components does not matter.

[Transform]

|No|item|value|
|:---:|:---|:---|
|1|Position|X: 0, Y: 0, Z: 0|
|2|Rotation|X: 0, Y: 0, Z: 0|
|3|Scale|X: 1, Y: 1, Z: 1|

[Animator]

|No|item|value|
|:---:|:---|:---|
|1|Avatar|(Human Avatar)|

[Vgo Right]

|No|item|description|remarks|
|:---:|:---|:---|:---|
|1|Title|The name of the work.|Required|
|2|Author|The name of the creator.|Required|
|3|Organization|The organization to which the creator belongs.||
|4|Created Date|The creation date of the work.|There is no format specification.|
|5|Updated Date|Update date of the work.|There is no format specification.|
|6|Version|Version of the work.|There is no format specification.|
|7|Distribution Url|Distribution URL.||
|8|License Url|The URL where the license is described.||

[Vgo Generator]

|No|item|description|value|
|:---:|:---|:---|:---:|
|1|Name|The name of the generation tool.|UniVGO|
|2|Version|Version of the generation tool.|2.4.11|

There are no user-configurable items.  
If the meta information is old, delete the component once and attach it again.

### 3. BlendShape

If necessary, set the BlendShape of the face.  
(Apps using UniVGO may refer to this setting)

Select the GameObject of the face in the `Hierarchy` tab.

Make sure the `Skinned Mesh Renderer` component has a parameter of `BlendShapes`.

Attath a new `Vgo BlendShape` component.

Then right-click on the `Project` tab,  
Create a configuration file with `Create > VGO2 > BlendShapeConfiguration`.

[Vgo BlendShape]

|No|item|description|remarks|
|:---:|:---|:---|:---|
|1|Kind|The kind of BlendShape. Select `Face`.|Required|
|2|Face Parts|Associate which part of the face the BlendShape is.|Option|
|3|Blinks|Register the BlendShape to use when blinking.|Option|
|4|Visemes|Associate the viseme. Only vowels are OK.|Option|
|5|Presets|Register the preset.|Option|

BlendShape's index is counted from 0 at the top.

After setting, set the configuration file in `BlendShapeConfiguration` of the `Vgo BlendShape` component.

### 4. SpringBone

Set up SpringBone if necessary.

On the `Hierarchy` tab, select any child (other than Root) GameObject.  
(Here we name the GameObject `SpringBoneManager`.)

Attach a new `Vgo Spring Bone Group` component to `SpringBoneManager`.  
(If you use multiple groups, attach more components.)

[Vgo Spring Bone Group]

|No|item|description|required|select value|default value|
|:---:|:---|:---|:---:|:---:|:---:|
|1|Comment|A name to identify this group as a user.|option|||
|2|Drag Force|The larger the value, the less likely it is to swing.|required|[0.0 - 1.0]|0.4|
|3|Stiffness Force|The higher the value, the easier it is to return to the original state.|required|[0.0 - 4.0]|1.0|
|4|Gravity Direction|The direction of gravity.|required||x: 0.0, y: -1.0, z: 0.0|
|5|Gravity Power|The magnitude of gravity.|required|[0.0 - 2.0]|0.2|
|6|Root Bones|Specify the game object at the base of the bone you want to swing.<br>By specifying multiple root bones, the settings can be grouped together.|required|||
|7|Hit Radius|The radius of the sphere that determines the collision of each bone.|required|[0.0 - 0.5]|0.02|
|8|Collider Groups|This is a group of colliders detected by this spring bone group.|option|||
|9|Draw Gizmo|The Editor draws the Spring Bone when it draws the Gizmo.|required|true / false|false|
|10|Gizmo Color|The drawing color of Spring Bone.|required||yellow|

If necessary, make additional settings for SpringBoneCollider.

Select the GameObject where you want to place the collider.  
(For hair, head, etc.)

Attach a new `Vgo Spring Bone Collider Group` component to the GameObject.

[Vgo Spring Bone Collider Group]

|No|item|description|required|select value|default value|
|:---:|:---|:---|:---:|:---:|:---:|
|1|Colliders|Set the spring bone collider.|required|||
|2|Gizmo Color|The drawing color of the spring bone collider.|required||magenta|

[Vgo Spring Bone Collider]

|No|item|description|required|select value|default value|
|:---:|:---|:---|:---:|:---:|:---:|
|1|Collider Type|The type of collider.|required|Sphere|Sphere|
|2|Offset|Relative to the GameObject.|required||x: 0.0, y: 0.0, z: 0.0|
|3|Radius|The radius of the sphere.|required|[0.0 - 1.0]|0.0|

#### 5. Cloth

Set the Cloth if necessary.  

#### 6. Particle System

Set the ParticleSystem if necessary.  
You can set it while checking the effect in `Scene View`.  
Shaders for particles can be used as shaders.

#### 7. Remarks

Animation is not available for avatars (Deprecated).  
The reason is that the avatar moves in position.

___

## Create VGO (other than humanoid avatar)

### 1. Load scene

If you use the UniVGO sample project, load `ExportScene`.  
If you want to create a new one, work on any scene.

### 2. Creating and setting Root

Create a `GameObject` for VGO in the scene.  
The name is arbitrary, but here it is "VGO".

![image1](https://github.com/izayoijiichan/vgo/blob/main/Documentation~/UniVGO/Images/421_Hierarchy_VGO.png)

I will make the settings for "VGO".

![image1](https://github.com/izayoijiichan/vgo/blob/main/Documentation~/UniVGO/Images/422_Inspector_VGO.png)

[GameObject]

|No|component|description|
|:---:|:---|:---|
|1|(Name)|Set any name.|
|2|Transform|Must be the initial value.|
|3|Vgo Right|Set freely.|
|4|Vgo Generator|It is OK just to attach.|

The order of the components does not matter.

[Transform]

|No|item|value|
|:---:|:---|:---|
|1|Position|X: 0, Y: 0, Z: 0|
|2|Rotation|X: 0, Y: 0, Z: 0|
|3|Scale|X: 1, Y: 1, Z: 1|

[Vgo Right]

|No|item|description|remarks|
|:---:|:---|:---|:---|
|1|Title|The name of the work.|Required|
|2|Author|The name of the creator.|Required|
|3|Organization|The organization to which the creator belongs.||
|4|Created Date|The creation date of the work.|There is no format specification.|
|5|Updated Date|Update date of the work.|There is no format specification.|
|6|Version|Version of the work.|There is no format specification.|
|7|Distribution Url|Distribution URL.||
|8|License Url|The URL where the license is described.||

[Vgo Generator]

|No|item|description|value|
|:---:|:---|:---|:---:|
|1|Name|The name of the generation tool.|UniVGO|
|2|Version|Version of the generation tool.|2.4.11|

There are no user-configurable items.  
If the meta information is old, delete the component once and attach it again.

### 3. Creation and setting of Child

Place `GameObject` as a child of" VGO ".  
You can place them freely.

#### 3-1. Skybox

Only one can be placed in a scene.  
Set Skybox material.  
The script needs to be processed in order to reflect the setting value in the scene.  
`Cubemap` is not supported.

#### 3-2. Light

Only `Realtime` settings work, not `Baked`.  
Also, if you want to include `Directional Light` in VGO, delete or deactivate `Directional Light` which is initially placed in the scene.

#### 3-3. Particle System

You can set while checking the effect in `Scene View`.  
Shaders for particles can be used.

#### 3-4. Animation

You can set animations for GameObjects.  
Animation clip must be created with legacy animation.
After setting, check that the animation works in `Scene View` or `Game View`.

#### 3-5. Any other regular object

Normal objects other than `Light` and `Particle System`.  
It is divided into A, B and C by type.

##### A) Stationary object

An object that does not move when touched, such as a floor or a building.  
Basically, this is set.

[GameObject]

|No|component|description|
|:---:|:---|:---|
|1|(Name)|et any name.|
|2|Transform|Set it freely.|
|3|Collider|Box / Capsule / Sphere|

[Collider]

|No|item|description|
|:---:|:---|:---|
|1|Is Trigger|Check off.|

The following is an example of setting the floor.

![image1](https://github.com/izayoijiichan/vgo/blob/main/Documentation~/UniVGO/Images/431_Hierarchy_Floor.png)

![image1](https://github.com/izayoijiichan/vgo/blob/main/Documentation~/UniVGO/Images/432_Inspector_Floor.png)

The size of the floor is free, but it is better to adjust the thickness of the collider to be 0.3m or more.  
If the object is not thick, it may penetrate the floor when the object falls at high speed.

##### B) Things that can move in a collision

An object that moves when an avatar or other object collides.  
Keep the number of objects to be set within the required range.

[GameObject]

|No|component|description|
|:---:|:---|:---|
|1|(Name)|You should avoid duplication.|
|2|Transform|Set it freely.|
|3|Collider|Box / Capsule / Sphere|
|4|Rigidbody|Must be attached.|

[Collider]

|No|item|description|
|:---:|:---|:---|
|1|Is Trigger|Check off.|

[Rigidbody]

|No|item|description|
|:---:|:---|:---|
|1|Mass|Suitable weight.|
|2|Use Gravity|Check on (Check off is possible depending on the characteristics)|
|3|Is Kinematic|Check off.|
|4|Constraints|Set it freely.|

##### C) Objects that do not require collision judgment

[GameObject]

|No|component|description|
|:---:|:---|:---|
|1|(Name)|Set any name.|
|2|Transform|Set it freely.|

Neither the Collider nor the Rigidbody component is assigned to objects that do not require collision detection.

This pattern is also used when the collider assigned to the parent object covers the judgment range.

___

## VGO export

When all settings are completed, export the VGO file (.vgo).

![image1](https://github.com/izayoijiichan/vgo2/blob/main/Documentation~/UniVGO/Images/501_Export.png)

Select the GameObject you want to output in Hierarchy.

Set the export condition of Inspector's `Vgo Generator`.

|name|choices|
|:---:|:---|
|Geometry Coordinates|Left Hand<br>Right Hand|
|UV Coordinates|Bottom Left<br>Top Left|
|JSON or BSON|JSON<br>BSON|
|Crypt Algorithms|None<br>AES<br>Base64|

When optimizing for Unity, set `Geometry Coordinates` to `Left Hand` and `UV Coordinates` to `Bottom Left`.

(The default value is so.)

Finally, click the `Export VGO` button.

If there are no errors, the VGO file will be output to the specified folder.  
If an error has occurred, the Console will display the details of the error.

___

## VGO Import

### 1. A Preparation of VGO file

Prepare a VGO file (.vgo).

### 1-1. If there is no VGO file

Initial import using UniVGO is not possible.

You need to prepare a file of a file format (.fbx, .dae, .3ds, .dxf, .obj, .blender, .max, etc..) that Unity supports Humanoid import.

Import the file using Unity's functionality.

At this time, Rig must be set correctly.

### 2. Import

Place the VGO file anywhere under `Assets`.

If an error has occurred, the Console will display the details of the error.  
If there are no errors, the import is complete.

You can place the object by selecting the VGO file from the `Project` window (tab) of the Unity Editor and dropping it on the `Hierarchy`.

### 3. Asset extraction

Locate the VGO file in the Unity Editor's `Project` window (tab) and select it.

![image1](https://github.com/izayoijiichan/vgo/blob/main/Documentation~/UniVGO/Images/620_Import.png)

The `Vgo Scripted Importer` is displayed in the `Inspector` window (tab).

Click the `Extract` button of `Material and Textures` to start extracting textures and materials.

The extraction is confirmed by clicking the `Apply` button.

___

## VGO runtime loading

### 1. Load scene

Load "LoadScene" when using the UniVGO sample project.

![image1](https://github.com/izayoijiichan/vgo/blob/main/Documentation~/UniVGO/Images/710_Load.png)

### 2. File settings

Select the `VgoLoader` game object in" Hierarchy ",  
In "Inspector", enter the full path of the VGO file you want to load at runtime in `Local File Path`.

![image1](https://github.com/izayoijiichan/vgo/blob/main/Documentation~/UniVGO/Images/721_Hierarchy.png)

![image1](https://github.com/izayoijiichan/vgo/blob/main/Documentation~/UniVGO/Images/722_Inspector.png)

### 3. Run game

Press the play button to run the game.  
Confirm that the VGO file is loaded.  
If an error has occurred, the Console will display the details of the error.

### Script

If you write your own script, write as follows.

~~~csharp
    using System;
    using UnityEngine;
    using UniVgo2;

    public class RuntimeLoadBehaviour : MonoBehaviour
    {
        private IDisposable _ModelAssetDisposer;

        private void Start()
        {
            VgoImporter importer = new();

            ModelAsset modelAsset = importer.Load(filePath);

            importer.ReflectSkybox(Camera.main, modelAsset);

            _ModelAssetDisposer = modelAsset;
        }

        private void OnDestroy()
        {
            _ModelAssetDisposer?.Dispose();
        }
    }
~~~

___

## Services that can use VGO

### VGO Hub

A service that allows you to upload and manage VGO files.

You can use the VGO (avatar or world) that you uploaded  
or the VGO (avatar or world) that others have set to be available in the linked app.

https://vgohub.azurewebsites.net

___

## Apps that can use VGO

Currently two applications are supported.

### VISHOP

It is a virtual shopping system.  
You can specify a VGO file for the player and shop.  
It is an application linked with VGO Hub.

![image1](https://github.com/izayoijiichan/vgo/blob/main/Documentation~/UniVGO/Images/820_vishop.jpg)

https://vishop.azurewebsites.net

### VOVOLA

It is a simple 3D virtual YouTuber application that does not require VR-HMD (head mounted display).  
You can specify a VGO file for the player and room.  
Multiple play allows multiple people to enter the same room and make calls.

![image1](https://github.com/izayoijiichan/vgo/blob/main/Documentation~/UniVGO/Images/810_vovola.jpg)

https://vovola.wixsite.com/website

___
Last updated: 24 August, 2022  
Editor: Izayoi Jiichan

*Copyright (C) 2020-2022 Izayoi Jiichan. All Rights Reserved.*
