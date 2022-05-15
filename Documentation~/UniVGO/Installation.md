# UniVGO install manual

Manual for installing UniVGO in Unity Editor.

___
## System requirements

### Unity version

|version|Win (Editor)|Win (Mono)|Win (IL2CPP)|Android (IL2CPP)|iOS|
|:---|:---:|:---:|:---:|:---:|:---:|
|Unity 2019.4|OK|OK|OK|OK|unconfirmed|
|Unity 2020.1|OK|OK|OK|OK|unconfirmed|
|Unity 2020.2|OK|OK|OK|OK|unconfirmed|
|Unity 2020.3|OK|OK|OK|OK|unconfirmed|
|Unity 2021.1|OK|OK|OK|OK|unconfirmed|
|Unity 2021.2|OK|OK|OK|OK|unconfirmed|
|Unity 2021.3|OK|OK|OK|OK|unconfirmed|

As of May of 2022, we are developing and confirming in `Unity 2021.3` `Windows` `.NET Standard 2.1` environment.

### Required package

#### Common (required regardless of which Unity version you use)

|package name|owner|Repository|specification version|program version|release date|
|:---|:---:|:---:|:---:|:---:|:---:|
|newtonsoft-json-for-unity|jillejr|GitHub|13.0.1|13.0.102|25 Mar, 2021|
|VRMShaders|vrm-c|GitHub|VRM 0.0|0.72.0|13 Apr, 2020|
|UniShaders|IzayoiJiichan|GitHub|-|1.3.0|27 Feb, 2022|
|VgoSpringBone|IzayoiJiichan|GitHub|-|1.1.1|1 June, 2021|
|UniVGO2|IzayoiJiichan|GitHub|VGO 2.4|2.4.7|16 May, 2022|

#### Added (when using Unity 2021.1 or lower version)

|package name|owner|Repository|specification version|program version|release date|
|:---|:---:|:---:|:---:|:---:|:---:|
|org.nuget.system.buffers|Microsoft|NuGet||4.4.0|11 Aug, 2017|
|org.nuget.system.memory|Microsoft|NuGet||4.5.0|29 May, 2018|
|org.nuget.system.numerics.vectors|Microsoft|NuGet||4.4.0|11 Aug, 2017|
|org.nuget.system.runtime.compilerservices.unsafe|Microsoft|NuGet||4.5.0|29 May, 2018|

#### Add (when used in HDRP projects)

|package name|owner|Repository|specification version|program version|release date|
|:---|:---:|:---:|:---:|:---:|:---:|
|com.unity.render-pipelines.high-definition|Unity Technologies|Unity Registry||11.0.0|26 Oct, 2021|

#### Add (when used in URP projects)

|package name|owner|Repository|specification version|program version|release date|
|:---|:---:|:---:|:---:|:---:|:---:|
|com.unity.render-pipelines.universal|Unity Technologies|Unity Registry||11.0.0|26 Oct, 2021|

___
## Install

### Installation procedure (Case using the sample project)

#### 1. Download sample project

Download the UniVGO sample project.

- [Unity 2021.1.28f1](https://github.com/izayoijiichan/univgo2.sample.unity2021.1.project)
- [Unity 2021.1.28f1 and HDRP project](https://github.com/izayoijiichan/univgo2.sample.unity2021.1.hdrp.project)
- [Unity 2021.1.28f1 and URP project](https://github.com/izayoijiichan/univgo2.sample.unity2021.1.urp.project)
- [Unity 2021.2.0f1](https://github.com/izayoijiichan/univgo2.sample.unity2021.2.project)
- [Unity 2021.2.0f1 and HDRP project](https://github.com/izayoijiichan/univgo2.sample.unity2021.2.hdrp.project)
- [Unity 2021.2.0f1 and URP project](https://github.com/izayoijiichan/univgo2.sample.unity2021.2.urp.project)

#### 2. Install Unity Editor

Install Unity Editor `2021.1.28f1`,` 2021.2.0f1`, or` 2021.3.0f1` on Unity Hub.

If you don't see the version you're looking for in Unity Hub, install it via the [Unity Download Archive](https://unity3d.com/jp/get-unity/download/archive).

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

#### 2. Add Define symbol

`Projcet Settings` > `Player` > `Other Settings` > `Script Compilation` > `Scripting Define Symbols`

Add the Define symbol to match the version of VRMShaders (com.vrmc.vrmshaders) you are using.

- When using VRMShaders 0.72.0 to 0.84.0

`VRMC_VRMSHADERS_0_72_OR_NEWER`

- When using VRMShaders 0.85.0 or higher

`VRMC_VRMSHADERS_0_85_OR_NEWER`

#### 3. Installation of Newtonsoft.JSON

Import UniVGO and dependent packages into your project.  
Write the following in `<Project>/Packages/package.json`.  
You need to be careful where you add them.

- Unity 2021.1.28f1

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
    "com.izayoi.unishaders": "https://github.com/izayoijiichan/UniShaders.git#v1.3.0",
    "com.izayoi.univgo2": "https://github.com/izayoijiican/VGO2.git#v2.4.7",
    "com.izayoi.vgospringbone": "https://github.com/izayoijiichan/VgoSpringBone.git#v1.1.1",
    "com.unity.ugui": "1.0.0",
    "com.vrmc.vrmshaders": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRMShaders#v0.72.0",
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

- Unity 2021.2.0f1 or heigher

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
    "com.izayoi.unishaders": "https://github.com/izayoijiichan/UniShaders.git#v1.3.0",
    "com.izayoi.univgo2": "https://github.com/izayoijiican/VGO2.git#v2.4.7",
    "com.izayoi.vgospringbone": "https://github.com/izayoijiichan/VgoSpringBone.git#v1.1.1",
    "com.unity.ugui": "1.0.0",
    "com.vrmc.vrmshaders": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRMShaders#v0.72.0",
    "jillejr.newtonsoft.json-for-unity": "13.0.102",
    "com.unity.modules.ai": "1.0.0",
    ...
    "com.unity.modules.xr": "1.0.0"
  }
}
```

If you want to use HDRP, append the line "com.unity.render-pipelines.high-definition".

```json
{
  "dependencies": {
    ...
    "com.unity.render-pipelines.high-definition": "11.0.0",
    "com.unity.ugui": "1.0.0",
    ...
  }
}
```

If you want to use URP, append the line "com.unity.render-pipelines.universal".

```json
{
  "dependencies": {
    ...
    "com.unity.render-pipelines.universal": "11.0.0",
    "com.unity.ugui": "1.0.0",
    ...
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
|UniShader.Hdrp.Utility|HDRP shader utility|*|*|
|UniShader.Skybox.Utility|Skybox shader utility|*|*|
|UniShader.Standard.Particle.Utility|Particle shader utility|*|*|
|UniShader.Standard.Utility|Standard shader utility|*|*|
|UniShader.Urp.Utility|URP shader utility|*|*|
|UniVgo2|VGO2 main program|*|*|
|UniVgo2.Editor|VGO2 import / export|-|*|
|VgoSpringBone|VGO Spring Bone|*|*|
|VRMShaders.GLTF.IO.Editor||-|*|
|VRMShaders.GLTF.IO.Runtime||*|*|
|VRMShaders.GLTF.UniUnlit.Editor|Unlit shader utility|-|*|
|VRMShaders.GLTF.UniUnlit.Runtime|Unlit shader utility|*|*|

- For each of UniVgo2, and UniVgo2.Editor, * is attached to the dependent DLL.

### How to use UniVGO version 1.0 and version 2.0 together

To install both UniVGO and UniVGO2 packages in Unity Editor at the same time, you need to avoid two major errors.

1. Duplicate shader

Delete either `UniGLTF` folder.

2. Scripted Importer

Both `VgoScriptedImporter`s try to handle the extension `.vgo`, so the conflict will result in an error.

In UniVGO (v1.1.1 and later) and UniVGO2 (v2.0.1 and later), you can change the process and avoid errors by adding definitions to Scripting Define Symbols.

|Scripting Define Symbols|description|
|:---|:---|
|VGO_FILE_EXTENSION_1|Change the version 1 file extension to be judged as `.vgo1`.|
|VGO_FILE_EXTENSION_2|Change the version 2 file extension to be judged as `.vgo2`.|
|VGO_1_DISABLE_SCRIPTED_IMPORTER|Disable the version 1 Scripted Importer.|
|VGO_2_DISABLE_SCRIPTED_IMPORTER|Disable the version 2 Scripted Importer.|

Add / delete definitions according to the purpose.

If the settings do not take effect immediately, restart Unity Editor.

### How to use UniVRM and UniVGO together

The version combinations are as follows.

UniVRM|UniVGO|Result|
|:---:|:---:|:---:|
|0.66.0|2.4.4|OK|
|0.68.2|2.4.4|OK|
|0.70.0|2.4.4|OK|
|0.71.0|2.4.4|OK|
|0.72.0|2.4.5|OK|
|0.73.0|2.4.5|OK|
|0.74.0|2.4.5|OK|
|0.75.0|2.4.5|OK|
|0.76.0|2.4.5|(OK)|
|0.77.0|2.4.5|(OK)|
|0.78.0|2.4.5|(OK)|
|0.79.0|2.4.5|(OK)|
|0.80.0|2.4.5|(OK)|
|0.81.0|2.4.5|OK|
|0.82.0|2.4.5|OK|
|0.83.0|2.4.5|OK|
|0.84.0|2.4.5|OK|
|0.85.0|2.4.5|OK|
|0.86.0|2.4.5|OK|
|0.87.0|2.4.5|OK|
|0.88.0|2.4.5|OK|
|0.89.0|2.4.5|OK|
|0.90.0|2.4.5|OK|

When using Unity 2020.2 or higher, 0.76.0 to 0.80.0 will cause compilation errors and is not recommended.

- When using UniVRM 0.66.0 to 0.71.0

Please use UniVGO 2.4.4.

Write the following in `<Project> /Packages/package.json`.

```json
{
  "dependencies": {
    ...
    "com.vrmc.unigltf": "https://github.com/vrm-c/UniVRM.git?path=/Assets/UniGLTF#v0.66.0",
    "com.vrmc.univrm": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRM#v0.66.0",
    "com.vrmc.vrmshaders": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRMShaders#v0.66.0",
    ...
  }
}
```

- When using UniVRM 0.72.0 to 0.80.0

Write the following in `<Project> /Packages/package.json`.

```json
{
  "dependencies": {
    ...
    "com.vrmc.unigltf": "https://github.com/vrm-c/UniVRM.git?path=/Assets/UniGLTF#v0.72.0",
    "com.vrmc.univrm": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRM#v0.72.0",
    "com.vrmc.vrmshaders": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRMShaders#v0.72.0",
    ...
  }
}
```

- When using UniVRM 0.81.0 to 0.90.0

Write the following in `<Project> /Packages/package.json`.

```json
{
  "dependencies": {
    ...
    "com.vrmc.gltf": "https://github.com/vrm-c/UniVRM.git?path=/Assets/UniGLTF#v0.81.0",
    "com.vrmc.univrm": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRM#v0.81.0",
    "com.vrmc.vrm": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRM#v0.81.0",
    "com.vrmc.vrmshaders": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRMShaders#v0.81.0",
    ...
  }
}
```

Modify the define symbol to match the version of VRMShaders (com.vrmc.vrmshaders) you are using.

`Projcet Settings` > `Player` > `Other Settings` > `Script Compilation` > `Scripting Define Symbols`

- When using VRMShaders 0.72.0 to 0.84.0

`VRMC_VRMSHADERS_0_72_OR_NEWER`

- When using VRMShaders 0.85.0 or higher

`VRMC_VRMSHADERS_0_85_OR_NEWER`

### Pre-set up sample project

[Unity 2021.1.28f1 UniVGO + UniVRM](https://github.com/izayoijiichan/univgo2.univrm.sample.unity2021.1.project)

___
Last updated: 16 May, 2022  
Editor: Izayoi Jiichan

*Copyright (C) 2020-2021 Izayoi Jiichan. All Rights Reserved.*
