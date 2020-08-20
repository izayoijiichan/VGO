# UniVGO Use manual

How to use UniVGO.

___
## Basic matters

### Version

The contents described in this manual are for the following versions.

|No|item|value|
|:---:|:---|:---:|
|1|Unity version|2019.4|
|2|UniVGO version|2.0.0|
|3|VGO spec version|2.0|

### Supported Unity components

The following Unity components are supported by VGO

|No|item|place|details|
|:---:|:---|:---|:---|
|1|Vgo Meta|Root|It is for managing VGO information.|
|2|Vgo Right|Root / Child|You can add rights information to VGO.|
|3|Animator|Root|You can set Human Avatar in GameObject.|
|4|Collider|Child|You can set collision judgment for GameObject.|
|5|Rigidbody|Child|You can set physics to GameObject and move it|
|6|Light|Child|You can set the light source for GameObject.|
|7|Particle System|Child|You can set particles to GameObject.|
|8|Skybox|Child|You can set skybox to Scene.|

### Usable shaders

The supported shaders are as follows.

|No|shader name|descriptoin|
|:---:|:---|:---|
|1|Standard|Standard shader|
|2|Particles/Standard Surface|Particle System dedicated shader|
|3|Particles/Standard Unlit|Particle System dedicated shader|
|4|Skybox/6 Sided|Skybox 6 sided shader|
|5|Skybox/Panoramic|Skybox panoramic shader|
|6|Skybox/Procedural|Skybox procedural shader|
|7|Unlit/Color||
|8|Unlit/Texture||
|9|Unlit/Transparent||
|10|Unlit/Transparent Cutout||
|11|UniGLTF/StandardVColor|Vertex color shader|
|12|UniGLTF/Unlit||
|13|VRM/MToon||

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
|5|Vgo Meta|It is OK just to attach.|

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
|2|Version|Version of the generation tool.|2.0.0|

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

___
## Create VGO (other than humanoid avatar)

### 1. Load scene

If you use the UniVGO sample project, load `ExportScene`.  
If you want to create a new one, work on any scene.


### 2. Creating and setting Root

Create a `GameObject` for VGO in the scene.  
The name is arbitrary, but here it is "VGO".

![image1](https://github.com/izayoijiichan/vgo/blob/master/Documentation~/UniVGO/Images/421_Hierarchy_VGO.png)

I will make the settings for "VGO".

![image1](https://github.com/izayoijiichan/vgo/blob/master/Documentation~/UniVGO/Images/422_Inspector_VGO.png)

[GameObject]

|No|component|description|
|:---:|:---|:---|
|1|(Name)|Set any name.|
|2|Transform|Must be the initial value.|
|3|Vgo Right|Set freely.|
|4|Vgo Meta|It is OK just to attach.|

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
|2|Version|Version of the generation tool.|2.0.0|

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
Also, if you want to include `Directional Light` in VGO, delete or deactivate` Directional Light` which is initially placed in the scene.

#### 3-3. Particle System

You can set while checking the effect in `Scene View`.  
Shaders for particles can be used.

#### 3-4. Any other regular object

Normal objects other than `Light` and` Particle System`.  
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

![image1](https://github.com/izayoijiichan/vgo/blob/master/Documentation~/UniVGO/Images/431_Hierarchy_Floor.png)

![image1](https://github.com/izayoijiichan/vgo/blob/master/Documentation~/UniVGO/Images/432_Inspector_Floor.png)

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

![image1](https://github.com/izayoijiichan/vgo2/blob/master/Documentation~/UniVGO/Images/501_Export.png)

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

![image1](https://github.com/izayoijiichan/vgo/blob/master/Documentation~/UniVGO/Images/620_Import.png)

The `Vgo Scripted Importer` is displayed in the `Inspector` window (tab).

Click the `Extract` button of `Material and Textures` to start extracting textures and materials.

The extraction is confirmed by clicking the `Apply` button.

___
## VGO runtime loading

### 1. Load scene

Load "LoadScene" when using the UniVGO sample project.

![image1](https://github.com/izayoijiichan/vgo/blob/master/Documentation~/UniVGO/Images/710_Load.png)


### 2. File settings

Select the `VgoLoader` game object in" Hierarchy ",  
In "Inspector", enter the full path of the VGO file you want to load at runtime in `Local File Path`.

![image1](https://github.com/izayoijiichan/vgo/blob/master/Documentation~/UniVGO/Images/721_Hierarchy.png)

![image1](https://github.com/izayoijiichan/vgo/blob/master/Documentation~/UniVGO/Images/722_Inspector.png)

### 3. Run game

Press the play button to run the game.  
Confirm that the VGO file is loaded.  
If an error has occurred, the Console will display the details of the error.

### Script

If you write your own script, write as follows.

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
## Apps that can use VGO

Currently two applications are supported.

### VOVOLA

It is a simple 3D virtual YouTuber application that does not require VR-HMD (head mounted display).  
You can specify a VGO file for a room.  
Multiple play allows multiple people to enter the same room and make calls.

![image1](https://github.com/izayoijiichan/vgo/blob/master/Documentation~/UniVGO/Images/810_vovola.jpg)

https://vovola.wixsite.com/website

### VISHOP

It is a virtual shopping system.  
You can specify a VGO file for your shop.

![image1](https://github.com/izayoijiichan/vgo/blob/master/Documentation~/UniVGO/Images/820_vishop.jpg)

https://vishop.azurewebsites.net

___
Last updated: 20 August, 2020  
Editor: Izayoi Jiichan

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
