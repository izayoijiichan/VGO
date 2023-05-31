# UniVGO install manual

Manual for installing UniVGO in Unity Editor.

___

## System requirements

### Unity version

|version|Win (Editor)|Win (Mono)|Win (IL2CPP)|Android (IL2CPP)|iOS|
|:---|:---:|:---:|:---:|:---:|:---:|
|Unity 2020.3|OK|OK|OK|OK|unconfirmed|
|Unity 2021.1|OK|OK|OK|OK|unconfirmed|
|Unity 2021.2|OK|OK|OK|OK|unconfirmed|
|Unity 2021.3|OK|OK|OK|OK|unconfirmed|
|Unity 2022.1|OK|OK|unconfirmed|unconfirmed|unconfirmed|
|Unity 2022.2|OK|OK|unconfirmed|unconfirmed|unconfirmed|

As of September of 2022, we are developing and confirming in `Unity 2022.2` `Windows` `.NET Standard 2.1` environment.

### Required package

#### Basic System Packages

If you are using Unity 2021.1.28f1 or lower.

|package name|owner|Repository|specification version|program version|release date|
|:---|:---:|:---:|:---:|:---:|:---:|
|org.nuget.system.buffers|Microsoft|NuGet||4.4.0|11, Aug, 2017|
|org.nuget.system.memory|Microsoft|NuGet||4.5.0|29, May, 2018|
|org.nuget.system.numerics.vectors|Microsoft|NuGet||4.4.0|11, Aug, 2017|

#### Basic Packages

This package is required for any Unity version.

|package name|owner|Repository|specification version|program version|release date|
|:---|:---:|:---:|:---:|:---:|:---:|
|com.unity.nuget.newtonsoft-json|Unity Technologies|Nuget|13.0.2|3.2.1|2 May, 2023|
|VRMShaders|vrm-c|GitHub||0.105.0|7 Oct, 2022|
|LilToonShader.Utility|IzayoiJiichan|GitHub||1.4.0|30 May, 2023|
|UniShaders|IzayoiJiichan|GitHub||1.4.0|20 May, 2022|
|VgoSpringBone|IzayoiJiichan|GitHub||1.1.2|24 Aug, 2022|
|UniVGO2|IzayoiJiichan|GitHub|VGO 2.5|2.5.5|1 Jun, 2023|

#### Additional Packages

Add if necessary.

|package name|owner|Repository|specification version|program version|release date|remarks|
|:---|:---:|:---:|:---:|:---:|:---:|:---:|
|jp.lilxyzw.liltoon|lilxyzw|GitHub||1.4.0|12 May, 2023||
|com.unity.render-pipelines.universal|Unity Technologies|Unity Registry||11.0.0|26 Oct, 2021|URP only|
|com.unity.render-pipelines.high-definition|Unity Technologies|Unity Registry||11.0.0|26 Oct, 2021|HDRP only|

___

## Install

### Installation procedure (Case using the sample project)

#### 1. Download sample project

Download the UniVGO sample project.

|unity version|rendering pipeline|link|
|:--|:--:|:--:|
|2021.1.28f1|BRP|[Link](https://github.com/izayoijiichan/univgo2.sample.unity.project/tree/unity2021.1.brp)|
|2021.1.28f1|URP|[Link](https://github.com/izayoijiichan/univgo2.sample.unity.project/tree/unity2021.1.urp)|
|2021.1.28f1|HDRP|[Link](https://github.com/izayoijiichan/univgo2.sample.unity.project/tree/unity2021.1.hdrp)|
|2021.3.0f1|BRP|[Link](https://github.com/izayoijiichan/univgo2.sample.unity.project/tree/unity2021.3.brp)|
|2021.3.0f1|URP|[Link](https://github.com/izayoijiichan/univgo2.sample.unity.project/tree/unity2021.3.urp)|
|2021.3.0f1|HDRP|[Link](https://github.com/izayoijiichan/univgo2.sample.unity.project/tree/unity2021.3.hdrp)|

#### 2. Install Unity Editor

Install Unity Editor `2021.1.28f1`, `2021.2.0f1`, `2021.3.0f1`, `2022.1.0f1`, or `2022.2.0f1` on Unity Hub.

If you don't see the version you're looking for in Unity Hub, install it via the [Unity Download Archive](https://unity3d.com/jp/get-unity/download/archive).

#### 3. Load project

At the Unity Hub, add the sample project downloaded in step 1 to the list.  
The specified folder is the project folder.

#### 4. Update project

If necessary, update the version of the Unity Editor or package.  
Unless otherwise noted, we recommend that you use the latest version of UniVGO.

### Installation procedure (Case you create your own project)

#### 1. Creating a new project

Create a new 3D project in Unity Editor or Unity Hub.

```
    <Project>
        Assets
        Packages
        ProjectSettings
```

#### 2. Installation of required packages

Import UniVGO and dependent packages into your project.  
Configure settings in `<Project>/Packages/package.json`.  
You need to be careful where you add them.

#### 2-1. Basic Sytem Packages

If you are using Unity 2021.1.28f1 or lower, add the following settings. Otherwise, do nothing.

```json
{
  "scopedRegistries": [
    {
      "name": "Unity NuGet",
      "url": "https://unitynuget-registry.azurewebsites.net",
      "scopes": ["org.nuget"]
    }
  ],
  "dependencies": {
    "org.nuget.system.buffers": "4.4.0",
    "org.nuget.system.memory": "4.5.0",
    "org.nuget.system.numerics.vectors": "4.4.0",
  }
}
```

#### 2-2. Basic Packages

To use UniVGO, add the following settings.

```json
{
  "dependencies": {
    "com.izayoi.liltoon.shader.utility": "https://github.com/izayoijiichan/lilToonShaderUtility.git#v1.4.0",
    "com.izayoi.unishaders": "https://github.com/izayoijiichan/UniShaders.git#v1.4.0",
    "com.izayoi.univgo": "https://github.com/izayoijiican/VGO.git#v2.5.5",
    "com.izayoi.vgospringbone": "https://github.com/izayoijiichan/VgoSpringBone.git#v1.1.2",
    "com.unity.nuget.newtonsoft-json": "3.2.1",
    "com.unity.ugui": "1.0.0",
    "com.vrmc.vrmshaders": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRMShaders#v0.105.0",
  }
}
```

#### 2-3. Addtional Packages

If you want to use lilToon, append the line "jp.lilxyzw.liltoon".

```json
{
  "dependencies": {
    "jp.lilxyzw.liltoon": "https://github.com/lilxyzw/lilToon.git?path=Assets/lilToon#1.4.0",
  }
}
```

If you want to use URP, append the line "com.unity.render-pipelines.universal".

```json
{
  "dependencies": {
    "com.unity.render-pipelines.universal": "11.0.0",
  }
}
```

If you want to use HDRP, append the line "com.unity.render-pipelines.high-definition".

```json
{
  "dependencies": {
    "com.unity.render-pipelines.high-definition": "11.0.0",
  }
}
```

### Confirmation of installation completion

1. Open the project in Unity Editor.
2. Check that no errors are displayed in the Unity Editor console.
3. Check that the menu item `[Tools]` > `[UniVGO]` is displayed on the menu bar of Unity Editor.

### Causes of installation errors

Possible causes of the error are as follows.

- Unity Editor version is different
- Different package versions
- Duplicate or missing file
- `asmdef` settings have been changed
- `asmdef.meta` settings have been changed
- The `.meta` guid of the component has changed
- `System.Buffers.dll`, `System.Memory.dll`, `System.Numerics.Vectors.dll`, `System.Runtime.CompilerServices.Unsage.dll` is duplicated.
- `NewtonSoft.Json.dll` is duplicated.
- Not getting `LFS` data from GitHub.

___

## Other information

### About libraries (assemblies)

When the package is installed in the project, the script is automatically compiled and the following DLL is generated.

|assemply|description|UniVgo|UniVgo.Editor|
|:---|:---|:---:|:---:|
|lilToon.Editor|lilToon shader utility|-|-|
|LilToonShader.Utility|lilToon shader utility|*|*|
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
|VRMShaders.GLTF.IO.Editor||-|-|
|VRMShaders.GLTF.IO.Runtime||*|*|
|VRMShaders.GLTF.UniUnlit.Editor|Unlit shader utility|-|-|
|VRMShaders.GLTF.UniUnlit.Runtime|Unlit shader utility|*|*|

- For each of UniVgo2, and UniVgo2.Editor, * is attached to the dependent DLL.

### How to use UniVRM and UniVGO together

The version combinations are as follows.

|UniVRM|UniVGO|min Unity|
|:---:|:---:|:---:|
|0.100.0|2.5.5|2020.3|
|0.101.0|2.5.5|2020.3|
|0.102.0|2.5.5|2020.3|
|0.103.2|2.5.5|2020.3|
|0.104.2|2.5.5|2020.3|
|0.105.0|2.5.5|2020.3|
|0.106.0|2.5.5|2020.3|
|0.107.2|2.5.5|2020.3|
|0.108.0|2.5.5|2020.3|
|0.109.0|2.5.5|2020.3|
|0.110.0|2.5.5|2020.3|

Write the following in `<Project> /Packages/package.json`.

```json
{
  "dependencies": {
    ...
    "com.vrmc.gltf": "https://github.com/vrm-c/UniVRM.git?path=/Assets/UniGLTF#v0.105.0",
    "com.vrmc.univrm": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRM#v0.105.0",
    "com.vrmc.vrm": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRM10#v0.105.0",
    "com.vrmc.vrmshaders": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRMShaders#v0.105.0",
    ...
  }
}
```

For other combinations not listed above, please see the wiki.

https://github.com/izayoijiichan/VGO/wiki/How-to-use-UniVRM-and-UniVGO-together

### Pre-set up sample project

|unity version|rendering pipeline|package|link|
|:--|:--:|:--:|:--:|
|2021.1.28f1|BRP|UniVGO + UniVRM|[Link](https://github.com/izayoijiichan/univgo2.sample.unity.project/tree/unity2021.1.brp.univrm)|
|2021.3.0f1|BRP|UniVGO + UniVRM|[Link](https://github.com/izayoijiichan/univgo2.sample.unity.project/tree/unity2021.3.brp.univrm)|

___
Last updated: 1 Jun, 2023  
Editor: Izayoi Jiichan

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
