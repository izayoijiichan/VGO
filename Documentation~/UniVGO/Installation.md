# UniVGO install manual

Manual for installing UniVGO in Unity Editor.

___
## System requirements

### Unity version

|version|Win (Editor)|Win (Mono)|Win (IL2CPP)|Android (IL2CPP)|iOS|
|:---|:---:|:---:|:---:|:---:|:---:|
|Unity 2019.1|unconfirmed|unconfirmed|unconfirmed|unconfirmed|unconfirmed|
|Unity 2019.2|unconfirmed|unconfirmed|unconfirmed|unconfirmed|unconfirmed|
|Unity 2019.3|unconfirmed|unconfirmed|unconfirmed|unconfirmed|unconfirmed|
|Unity 2019.4|OK|OK|OK|OK|unconfirmed|
|Unity 2020.1|OK|OK|OK|OK|unconfirmed|
|Unity 2020.2|OK|OK|OK|OK|unconfirmed|
|Unity 2020.3|OK|OK|OK|OK|unconfirmed|
|Unity 2021.1|OK|OK|OK|OK|unconfirmed|

As of November of 2021, we are developing and confirming in `Unity 2021.1` `Windows` `.NET Standard 2.0` environment.

### Required package

|package name|owner|Repository|specification version|program version|release date|
|:---|:---:|:---:|:---:|:---:|:---:|
|org.nuget.system.buffers|Microsoft|NuGet||4.4.0|11 Aug, 2017|
|org.nuget.system.memory|Microsoft|NuGet||4.5.0|29 May, 2018|
|org.nuget.system.numerics.vectors|Microsoft|NuGet||4.4.0|11 Aug, 2017|
|org.nuget.system.runtime.compilerservices.unsafe|Microsoft|NuGet||4.5.0|29 May, 2018|
|newtonsoft-json-for-unity|jillejr|GitHub|13.0.1|13.0.102|25 Mar, 2021|
|VRMShaders|vrm-c|GitHub|VRM 0.0|0.62.0|17 Nov, 2020|
|UniShaders|IzayoiJiichan|GitHub|-|1.1.0|18 Mar, 2021|
|VgoSpringBone|IzayoiJiichan|GitHub|-|1.1.1|1 June, 2021|
|UniVGO2|IzayoiJiichan|GitHub|VGO 2.4|2.4.1|8 Nov, 2021|

___
## Install

### Installation procedure (Case using the sample project)


#### 1. Download sample project

Download the UniVGO sample project.

https://github.com/izayoijiichan/univgo2.sample.unity.project

#### 2. Install Unity

Install `Unity 2021.1.0f1` on Unity Hub.

#### 3. Load project

At the Unity Hub, add the sample project downloaded in step 1 to the list.  
The specified folder is the project folder.


### Installation procedure (Case you create your own project)


#### 1. Creating a new project

Create a new 3D project in Unity Editor or Unity Hub.

```
    <Project>
        Assets
        Packages
        ProjectSettings
```

#### 2. Installation of Newtonsoft.JSON

Import UniVGO and dependent packages into your project.  
Write the following in `<Project>/Packages/package.json`.  
You need to be careful where you add them.

```json
{
  "scopedRegistries": [
    {
      "name": "Unity NuGet",
      "url": "https://unitynuget-registry.azurewebsites.net",
      "scopes": ["org.nuget"]
    },
    {
      "name": "Packages from jillejr",
      "url": "https://npm.cloudsmith.io/jillejr/newtonsoft-json-for-unity/",
      "scopes": ["jillejr"]
    }
  ],
  "dependencies": {
    "com.izayoi.unishaders": "https://github.com/izayoijiichan/UniShaders.git#v1.1.0",
    "com.izayoi.univgo2": "https://github.com/izayoijiican/VGO2.git#v2.4.1",
    "com.izayoi.vgospringbone": "https://github.com/izayoijiichan/VgoSpringBone.git#v1.1.1",
    "com.unity.ugui": "1.0.0",
    "com.vrmc.vrmshaders": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRMShaders#v0.62.0",
    "jillejr.newtonsoft.json-for-unity": "13.0.102",
    "org.nuget.system.buffers": "4.4.0",
    "org.nuget.system.memory": "4.5.0",
    "org.nuget.system.numerics.vectors": "4.4.0",
    "org.nuget.system.runtime.compilerservices.unsafe": "4.5.0",
    "com.unity.modules.ai": "1.0.0",
    ...
    "com.unity.modules.xr": "1.0.0"
  }
}
```

### Confirmation of installation completion

1. Open the project in Unity Editor.
2. Check that no errors are displayed in the Unity Editor console.
3. Check that the menu item `[Tools]` > `[UniVGO]` is displayed on the menu bar of Unity Editor.

### Causes of installation errors

Possible causes of the error are as follows.

- Unity version is different
- Different package versions
- Duplicate or missing file
- `asmdef` settings have been changed
- `asmdef.meta` settings have been changed
- The `.meta` guid of the component has changed
- `System.Buffers.dll`, `System.Memory.dll`, `System.Numerics.Vectors.dll`, `System.Runtime.CompilerServices.Unsage.dll` is duplicated.

### Error avoidance method

If you encounter an error related to duplication of `System.Buffers.dll`,` System.Memory.dll`, `System.Numerics.Vectors.dll`,` System.Runtime.CompilerServices.Unsage.dll`, You can avoid the error in a way.

Download the UniVgo2 source code from GitHub and place it in the `Packages` folder of your Unity project.

Open UniVgo2's `package.json` and edit it.

Delete the description of `org.nuget.system.memory`.

```diff
{
  "name": "com.izayoi.univgo2",
  ...
  "dependencies": {
    "com.izayoi.unishaders": "1.1.0",
    "com.izayoi.vgospringbone": "1.1.1",
    "com.vrmc.vrmshaders": "0.62.0",
-   "jillejr.newtonsoft.json-for-unity": "13.0.102",
-   "org.nuget.system.memory": "4.5.0"
+   "jillejr.newtonsoft.json-for-unity": "13.0.102"
  }
}
```

Open PackageManager from UnityEditor and delete the following libraries.

- org.nuget.system.buffers
- org.nuget.system.memory
- org.nuget.system.numerics.vectors
- org.nuget.system.runtime.compilerservices.unsafe

This will eliminate the duplication.

___
## Other information


### About libraries (assemblies)

When the package is installed in the project, the script is automatically compiled and the following DLL is generated.

|assemply|description|UniVgo|UniVgo.Editor|
|:---|:---|:---:|:---:|
|MToon|MToon shader utility|*|*|
|MToon.Editor|MToon shader utility|-|*|
|NewtonVgo|for Newton.JSON vgo program|*|*|
|ShaderProperty.Runtime|Shader property information|*|*|
|UniShader.Skybox.Utility|Skybox shader utility|*|*|
|UniShader.Standard.Particle.Utility|Particle shader utility|*|*|
|UniShader.Standard.Utility|Standard shader utility|*|*|
|UniUnlit|Unlit shader utility|*|*|
|UniUnlit.Editor|Unlit shader utility|-|*|
|UniVgo2|VGO2 main program|*|*|
|UniVgo2.Editor|VGO2 import / export|-|*|
|VgoSpringBone|VGO Spring Bone|*|*|

- For each of UniVgo2, and UniVgo2.Editor, * is attached to the dependent DLL.
- MToon, ShaderProperty, UniUnlit is a program packed in VRMShaders (©vrm-c).
- When using UniVGO (ver1) and UniVGO2 together, UniVgo (ver1) UniGLTF folder and UniVgo (ver1) Editor folder must be deleted. Conflicts occur because both have the extension `.vgo`.
- When using UniVRM and UniVGO together, it is necessary to delete the duplicate files that were packed when UniVgo was obtained. The shaders in the UniGLTF folder are relevant.

### How to use UniVGO version 1.0 and version 2.0 together

To install both UniVGO and UniVGO2 packages in Unity Editor at the same time, you need to avoid two major errors.

1. Duplicate shader

Delete either `UniGLTF` folder.

2. Scripted Importer

Both `VgoScriptedImporter`s try to handle the extension` .vgo`, so the conflict will result in an error.

In UniVGO (v1.1.1 and later) and UniVGO2 (v2.0.1 and later), you can change the process and avoid errors by adding definitions to Script Define Symbols.

|Script Define Symbols|description|
|:---|:---|
|VGO_FILE_EXTENSION_1|Change the version 1 file extension to be judged as `.vgo1`.|
|VGO_FILE_EXTENSION_2|Change the version 2 file extension to be judged as `.vgo2`.|
|VGO_1_DISABLE_SCRIPTED_IMPORTER|Disable the version 1 Scripted Importer.|
|VGO_2_DISABLE_SCRIPTED_IMPORTER|Disable the version 2 Scripted Importer.|

Add / delete definitions according to the purpose.

If the settings do not take effect immediately, restart Unity Editor.

###  How to use UniVRM and UniVGO together

To install both UniVRM and UniVGO2 packages in the Unity Editor at the same time, you need to remove the duplicate files that were packed when you got UniVgo.

This applies to shaders in the UniGLTF folder.

UniVRM version 0.66.0 is recommended.

___
Last updated: 8 Nov, 2021  
Editor: Izayoi Jiichan

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
