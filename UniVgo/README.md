# UniVGO

UniVGO is a package that allows you to use VGO files in Unity.

## Features

- VGO file settings, export, and import can be performed on the screen UI.
- You can operate VGO files (such as dynamic loading) from Unity scripts.

___
## System requirements

### Unity version

|version|Win (Editor)|Win (Mono)|Win (IL2CPP)|Android (IL2CPP)|iOS|
|:---|:---:|:---:|:---:|:---:|:---:|
|Unity 2018.4|unconfirmed|unconfirmed|unconfirmed|unconfirmed|unconfirmed|
|Unity 2019.1|unconfirmed|unconfirmed|unconfirmed|unconfirmed|unconfirmed|
|Unity 2019.2|unconfirmed|unconfirmed|unconfirmed|unconfirmed|unconfirmed|
|Unity 2019.3|OK|OK|OK|unconfirmed|unconfirmed|
|Unity 2020.1a|unconfirmed|unconfirmed|unconfirmed|unconfirmed|unconfirmed|

As of the start of 2020, we are developing and confirming in `Unity 2019.3` Windows environment.

### Required package

|package name|owner|Repository|specification version|program version|release date|
|:---:|:---:|:---:|:---:|:---:|:---:|
|newtonsoft-json-for-unity|jillejr|GitHub|12.0.1|12.0.101|27 Nov, 2019|
|UniVGO|IzayoiJiichan|GitHub| VGO 0.1|0.3.0|1 Jan, 2020|

___
## Install

1. Create a new 3D project in UnityEditor or UnityHub.
```
    <Project>
        Assets
        Packages
        ProjectSettings
```

2. Import Newtonsoft.JSON (Json.NET) into the project as a package.

Write the following in `<Project>/Packages/package.json`  
You need to be careful where you add them.

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

3. Import UniVGO into your project.

Do either A or B.

A) Case managing with `package.json`

Write the following in `<Project>/Packages/package.json`

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

B) Case managing with `Packages` folder

Download the zip file from the following URL.

UniVGO  
https://github.com/izayoijiichan/VGO/releases  

Unzip the file and place it in the `Packages` folder.

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

You can change the name of the folder.

### Confirmation of installation completion

1. Open the project in UnityEditor.
2. Check that no errors are displayed in the UnityEditor console.
3. Check that the menu item `[Tools]` > `[UniVGO]` is displayed on the menu bar of UnityEditor.

___
## Explanation of Unity components related to VGO

### VGO Meta

Meta information of VGO.

|definition name|description|type|fixed value|
|:---|:---|:---:|:---:|
|Generator Name|The name of the generation tool.|string|UniVGO|
|Generator Version|The generation tool version.|string|0.3.0|
|Spec Version|VGO specification version.|string|0.1|

- It is necessary to give one to Root GameObject.
- There are no user configurable items.

### VGO Right

Object rights information.

|definition name|description|type|remarks|
|:---|:---|:---:|:---|
|Title|The name of the work.|string|Required|
|Author|The name of the creator.|string|Required|
|Organization|The organization to which the creator belongs.|string||
|Created Date|The creation date of the work.|string|There is no format specification.|
|Updated Date|The update date of the work.|string|There is no format specification.|
|Version|The version of the work.|string|There is no format specification.|
|Distribution Url|Distribution URL.|string|URL format|
|License Url|The URL where the license is written.|string|URL format|

- It is necessary to give one to Root GameObject.
- Besides that, it can be freely assigned to any GameObject.
- It has no effect on the movement of the object.

### Collider

This setting is used to detect collisions between objects.  
Box, Capsule, Sphere types are supported.  
Multiple colliders can be assigned to one game object.

See the official Unity manual for details.

### Rigidbody

This is a setting for controlling an object by physical characteristics.。  
It is possible to add up to one for each GameObject.  

See the official Unity manual for details.

___
## Shader

The supported shaders are as follows.

|shader name|descriptoin|
|:---|:---|
|Standard|Standard shader|
|UniGLTF/Unlit|Unlit shader|
___
## VGO setup

### Root GameObject

|component|description|
|:---|:---|
|(Name)|Set any name.|
|Transform|Position (0, 0, 0) Rotation (0, 0, 0) Scale (1, 1, 1)|
|VGO Meta|Just attach it.|
|VGO Right|Set it freely.|

The order of components is not related to behavior.

### Child GameObject

You can place GameObject freely under Root.
VGO Meta components can be freely assigned.
The types are divided into A, B, and C below.

#### A) Stationary object

An object that does not move when touched, such as a floor or a building.  
Basically, this is set.

|component|description|
|:---|:---|
|(Name)|Set any name.|
|Transform|Set it freely.|
|Collider|Box / Capsule / Sphere|

- Collider
  - Is Trigger：Check off

It does not have to be the same as the shape of the object.

#### B) Things that can move in a collision

An object that does not move when it collides, such as a floor or a building.  
Keep the number of objects to be set within the required range.

|component|description|
|:---|:---|
|(Name)|You should avoid duplication.|
|Transform|Set it freely.|
|Collider|Box / Capsule / Sphere|
|Rigidbody|Must be attached.|

- Collider
  - Is Trigger：Check off
- Rigidbody
  - Mass：Suitable weight
  - Use Gravity：Check on (Check off is possible depending on the characteristics)
  - Is Kinematic：Check off
  - Constraints：
    - Freese Position：Can be set freely
    - Freese Rotation：Can be set freely

#### C) Objects that do not require collision judgment

|component|description|
|:---|:---|
|(Name)|Set any name.|
|Transform|Set it freely.|

Neither the Collider nor the Rigidbody component is assigned to objects that do not require collision detection.

This pattern is also used when the collider assigned to the parent object covers the judgment range.

### Specification of setting

When exporting, inactive GameObjects are output as active. 

___
## Export VGO

1. Select the game object to which VgoMeta is assigned in Hierarchy of UnityEditor.
2. A Click `[Export VGO]` button of VgoMeta component displayed in Inspector.  
   B Select `[Tools]` > `[UniVGO]` > `[Export]` from the menu bar.
3. Specify the output destination in the file save dialog and press the save button.
4. If the process is successful, the VGO file will be export to the specified export destination.

## Import VGO

1. Select `[Tools]` > `[UniVGO]` > `[Import]` from the menu bar.
2. Specify the VGO file you want to import in the file selection dialog and press the Open button.
3. Select the folder under Assets you want to store in the file save dialog and press the save button.
4. If processing is successful, the data will be imported as Prefab.

___

## Load VGO with script

You can dynamically load a VGO file into the Game scene by running a script.

The following is a simple sample program.

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

VgoImporter also supports asynchronous processing using Coroutine and Task.

___
## Other information

### Causes of installation errors

Possible causes of the error are as follows.

- Different package versions
- Duplicate or missing file
- `asmdef` settings have been changed
- `asmdef.meta` settings have been changed
- The `.meta` guid of the component has changed


### About libraries (assemblies)

When the package is installed in the project, the script is automatically compiled and the following DLL is generated.

|assemply|description|UniVgo|UniVgo.Editor|
|:---|:---|:---:|:---:|
|DepthFirstScheduler|Depth first scheduler|*|*|
|ShaderProperty.Runtime|Shader property information|*|*|
|UniGLTFforUniVgo|UniGLTF for UniVGO|*|*|
|UniUnlit|Unlit shader unitlity|-|-|
|UniUnlit.Editor|Unlit shader unitlity|-|*|
|UniVgo|VGO main program|*|*|
|UniVgo.Editor|VGO import / export|-|*|

- For each of UniVgo, and UniVgo.Editor, * is attached to the dependent DLL.
- DepthFirstScheduler, ShaderProperty, UniUnlit is a program packed in UniVRM (© vrm-c).
- When using UniVRM and UniVGO together, it is necessary to delete the duplicate files (DepthFirstScheduler, ShaderProperty, UniUnlit) that were packed when UniVgo was obtained.
  Also, if the error is displayed by UniVgo, please move UniVgo, UniGLTFforUniVgo to the` Assets` folder.
___
## About VGO specifications

It is described in VGO [README.md](https://github.com/izayoijiichan/VGO/blob/master/README.md).

___
## Acknowledgment

I would like to express my sincere appreciation to
Everyone, including KhronosGroup, who published the specifications about glTF,  
Dear ousttrue for developing and publishing a program about glTF,
VRM Consortium, Dwango Co., Ltd., who has published and distributed VRM specifications and related programs,
Unity Technologies, who is developing Unity, and other related people.  
I would like to take this opportunity to thank you.

___
Last updated: 1 January, 2020 
Editor: Izayoi Jiichan

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
