# UniVGO Use manual

How to use UniVGO.

___
## Basic matters

### Version

The contents described in this manual are for the following versions.

|No|item|value|
|:---:|:---|:---:|
|1|Unity version|2019.3|
|2|UniVGO version|0.7.0|
|3|VGO spec version|0.5|

### Supported Unity components

The following Unity components are supported by VGO

|No|item|place|details|
|:---:|:---|:---|:---|
|1|Vgo Meta|Root|It is for managing VGO information.|
|2|Vgo Right|Root / Child|You can add rights information to VGO.|
|3|Collider|Child|You can set collision judgment for GameObject.|
|4|Rigidbody|Child|You can set physics to GameObject and move it|
|5|Light|Child|You can set the light source for GameObject.|
|6|Particle System|Child|You can set particles to GameObject.|

### Usable shaders

The supported shaders are as follows.

|No|shader name|descriptoin|
|:---:|:---|:---|
|1|Standard|Standard shader|
|2|Particles/Standard Surface|Particle System dedicated shader|
|3|Particles/Standard Unlit|Particle System dedicated shader|
|4|Unlit/Color||
|5|Unlit/Texture||
|6|Unlit/Transparent||
|7|Unlit/Transparent Cutout||
|8|UniGLTF/Unlit||
|9|VRM/MToon||

Unlit shaders are not affected by light sources. Instead, the processing load is reduced.

___
## Create VGO

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

[Vgo Meta]

|No|item|description|value|
|:---:|:---|:---|:---:|
|1|Generator Name|The name of the generation tool.|UniVGO|
|2|Generator Version|Version of the generation tool.|0.7.0|
|3|Spec Version|This is the specification version of VGO.|0.5|

There are no user-configurable items.  
If the meta information is old, delete the component once and attach it again.


### 3. Creation and setting of Child

Place `GameObject` as a child of" VGO ".  
You can place them freely.

#### 3-1. Light

Only `Realtime` settings work, not `Baked`.  
Also, if you want to include `Directional Light` in VGO, delete or deactivate` Directional Light` which is initially placed in the scene.

#### 3-2. Particle System

You can set while checking the effect in `Scene View`.  
Shaders for particles can be used.

#### 3-3. Any other regular object

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

![image1](https://github.com/izayoijiichan/vgo/blob/master/Documentation~/UniVGO/Images/500_Export.png)

Export in either A or B method.

A) From the menu at the top of the Unity Editor, select `Tools` > `UniVGO` > `Export`.
B) Click `Export VGO` button of VgoMeta component displayed in Inspector.  

If there are no errors, the VGO file will be output to the specified folder.  
If an error has occurred, the Console will display the details of the error.

___
## VGO Import

### 1. A Preparation of VGO file

Prepare a VGO file (.vgo).

* If you place it in the project `Assets`, it will be imported automatically.

The following is an explanation when not allocating to `Assets`.

### 2. Import

From the menu at the top of the Unity Editor, select `Tools` > `UniVGO` > `Import`.  
You can work any scene.

![image1](https://github.com/izayoijiichan/vgo/blob/master/Documentation~/UniVGO/Images/620_Import.png)

In the first dialog, select the VGO file (.vgo) you want to import.

In the next dialog, select the destination folder.  
It is necessary to specify the folder under `Assets` of Project as the import destination.

If there are no errors, a prefab file (.prefab) has been generated.  
If an error has occurred, the Console will display the details of the error.

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
## Apps that can use VGO

Currently two applications are supported.

### VOVOLA

It is a simple 3D virtual YouTuber application that does not require VR-HMD (head mounted display).  
You can specify a VGO file for a room.  
Multiple play allows multiple people to enter the same room and make calls.

![image1](https://github.com/izayoijiichan/vgo/blob/master/Documentation~/UniVGO/Images/810_vovola.jpg)

https://vovola.wixsite.com/website

### vishop

It is a virtual shopping system.  
You can specify a VGO file for your shop.

![image1](https://github.com/izayoijiichan/vgo/blob/master/Documentation~/UniVGO/Images/820_vishop.jpg)

https://izayoi16.wixsite.com/vishop

___
Last updated: 6 February, 2020  
Editor: Izayoi Jiichan

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
