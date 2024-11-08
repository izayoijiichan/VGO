# UniVGO install manual

Manual for installing UniVGO in Unity Editor.

___

## System requirements

### Unity version

|version|Win (Editor)|Win (Mono)|Win (IL2CPP)|Android (IL2CPP)|iOS|WebGL|
|:---|:---:|:---:|:---:|:---:|:---:|:---:|
|Unity 2020.3|OK|OK|OK|OK|unconfirmed|OK|
|Unity 2021.1|OK|OK|OK|OK|unconfirmed|OK|
|Unity 2021.2|OK|OK|OK|OK|unconfirmed|OK|
|Unity 2021.3|OK|OK|OK|OK|unconfirmed|OK|
|Unity 2022.1|OK|OK|OK|unconfirmed|unconfirmed|OK|
|Unity 2022.2|OK|OK|OK|unconfirmed|unconfirmed|OK|
|Unity 2022.3|OK|OK|OK|unconfirmed|unconfirmed|OK|
|Unity 2023.1|OK|OK|OK|OK|unconfirmed|OK|
|Unity 2023.2|OK|OK|OK|unconfirmed|unconfirmed|unconfirmed|
|Unity 6000.0|OK|OK|OK|unconfirmed|unconfirmed|unconfirmed|

As of October of 2024, we are developing and confirming in `Unity 6000.0`, `Windows`, `.NET Standard 2.1` environment.

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
|com.izayoi.unishaders|IzayoiJiichan|GitHub||1.6.1|1 Aug, 2023|
|com.izayoi.vgospringbone|IzayoiJiichan|GitHub||1.1.2|24 Aug, 2022|
|com.izayoi.univgo|IzayoiJiichan|GitHub|VGO 2.5|2.5.22|4 Nov, 2024|

#### Additional Packages

Add if necessary.

|package name|owner|Repository|specification version|program version|release date|remarks|
|:---|:---:|:---:|:---:|:---:|:---:|:---:|
|com.izayoi.liltoon.shader.utility|IzayoiJiichan|GitHub||1.7.0|18 Jan, 2024||
|com.izayoi.nova.shader.utility|IzayoiJiichan|GitHub||2.4.0|4 Nov, 2024||
|jp.co.cyberagent.nova|Cyber Agent|GitHub||2.4.0|11, Oct, 2024||
|jp.lilxyzw.liltoon|lilxyzw|GitHub||1.7.3|8 Aug, 2024||
|com.vrmc.vrmshaders|vrm-c|GitHub||0.124.2|23 Jul, 2024||
|org.nuget.sixlabors.imagesharp|SixLabors|Unity NuGet||2.1.5|14 Aug, 2023|for WebP|
|com.unity.render-pipelines.universal|Unity Technologies|Unity Registry||14.0.0|17 Nov, 2021|URP only|
|com.unity.render-pipelines.high-definition|Unity Technologies|Unity Registry||14.0.0|17 Nov, 2021|HDRP only|

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
|2022.3.0f1|BRP|[Link](https://github.com/izayoijiichan/univgo2.sample.unity.project/tree/unity2022.3.brp)|

#### 2. Install Unity Editor

Install Unity Editor on Unity Hub.

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
    "com.izayoi.unishaders": "https://github.com/izayoijiichan/UniShaders.git#v1.6.1",
    "com.izayoi.univgo": "https://github.com/izayoijiican/VGO.git#v2.5.22",
    "com.izayoi.vgospringbone": "https://github.com/izayoijiichan/VgoSpringBone.git#v1.1.2",
    "com.unity.nuget.newtonsoft-json": "3.2.1",
  }
}
```

#### 2-3. Addtional Packages

If you want to use lilToon, add the following line.

```json
{
  "dependencies": {
    "com.izayoi.liltoon.shader.utility": "https://github.com/izayoijiichan/lilToonShaderUtility.git#v1.7.0",
    "jp.lilxyzw.liltoon": "https://github.com/lilxyzw/lilToon.git?path=Assets/lilToon#1.7.3",
  }
}
```

If you want to use NOVA shader, add the following line.

```json
{
  "dependencies": {
    "com.izayoi.nova.shader.utility": "https://github.com/izayoijiichan/NovaShaderUtility.git#v2.4.0",
    "jp.co.cyberagent.nova": "https://github.com/CyberAgentGameEntertainment/NovaShader.git?path=Assets/Nova#2.4.0",
  }
}
```

If you use UniUnlit, add the following line.

```json
{
  "dependencies": {
    "com.vrmc.vrmshaders": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRMShaders#v0.124.2",
  }
}
```

If you are using UniVRM 0.125 or higher, add the following instead of the above.

```json
{
  "dependencies": {
    "com.vrmc.gltf": "https://github.com/vrm-c/UniVRM.git?path=/Assets/UniGLTF#v0.125.0",
  }
}
```

If you use MToon 0.x, add the following line.

```json
{
  "dependencies": {
    "com.vrmc.vrmshaders": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRMShaders#v0.124.2",
  }
}
```

If you are using UniVRM 0.125 or higher, add the following instead of the above.

```json
{
  "dependencies": {
    "com.vrmc.univrm": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRM#v0.125.0",
  }
}
```

If you use MToon 1.0, add the following line.

```json
{
  "dependencies": {
    "com.vrmc.vrmshaders": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRMShaders#v0.124.2",
  }
}
```

If you are using UniVRM 0.125 or higher, add the following instead of the above.

```json
{
  "dependencies": {
    "com.vrmc.vrm": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRM10#v0.125.0",
  }
}
```

If you are using Unity 2021.2 or higher and you want to use WebP, append the line "org.nuget.sixlabors.imagesharp".

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
    "org.nuget.sixlabors.imagesharp": "2.1.5",
  }
}
```

If you want to use URP, append the line "com.unity.render-pipelines.universal".

```json
{
  "dependencies": {
    "com.unity.render-pipelines.universal": "14.0.0",
  }
}
```

- [Universal RP 10.10 for Unity 2020.3](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@10.10/changelog/CHANGELOG.html)
- [Universal RP 12.1 for Unity 2021.3](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@12.1/changelog/CHANGELOG.html)
- [Universal RP 14.0 for Unity 2022.3](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@14.0/changelog/CHANGELOG.html)
- [Universal RP 15.0 for Unity 2023.1](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@15.0/changelog/CHANGELOG.html)
- [Universal RP 16.0 for Unity 2023.2](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@16.0/changelog/CHANGELOG.html)
- [Universal RP 17.0 for Unity 6000.0](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@17.0/changelog/CHANGELOG.html)

If you want to use HDRP, append the line "com.unity.render-pipelines.high-definition".

```json
{
  "dependencies": {
    "com.unity.render-pipelines.high-definition": "14.0.0",
  }
}
```

- [High Definition RP 10.10 for Unity 2020.3](https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@10.10/changelog/CHANGELOG.html)
- [High Definition RP 12.1 for Unity 2021.3](https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@12.1/changelog/CHANGELOG.html)
- [High Definition RP 14.0 for Unity 2022.3](https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@14.0/changelog/CHANGELOG.html)
- [High Definition RP 15.0 for Unity 2023.1](https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@15.0/changelog/CHANGELOG.html)
- [High Definition RP 16.0 for Unity 2023.2](https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@16.0/changelog/CHANGELOG.html)
- [High Definition RP 17.0 for Unity 6000.0](https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@17.0/changelog/CHANGELOG.html)

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
- `System.Buffers.dll`, `System.Memory.dll`, `System.Numerics.Vectors.dll` is duplicated.
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
|NOVA|NOVA shader utility|*|*|
|NOVA.Editor|NOVA shader utility|-|*|
|ShaderProperty.Runtime|Shader property information|*|*|
|UniShader.Hdrp.Utility|HDRP shader utility|*|*|
|UniShader.Shared|Unity shader shared utility|*|*|
|UniShader.Skybox.Utility|Skybox shader utility|*|*|
|UniShader.Standard.Particle.Utility|Standard Particle shader utility|*|*|
|UniShader.Standard.Utility|Standard shader utility|*|*|
|UniShader.Urp.Particle.Utility|URP Particle shader utility|*|*|
|UniShader.Urp.Utility|URP shader utility|*|*|
|UniVgo2|VGO2 main program|*|*|
|UniVgo2.Editor|VGO2 import / export|-|*|
|VgoSpringBone|VGO Spring Bone|*|*|
|VRMShaders.GLTF.IO.Editor||-|-|
|VRMShaders.GLTF.IO.Runtime||*|*|
|VRMShaders.GLTF.UniUnlit.Editor|Unlit shader utility|-|-|
|VRMShaders.GLTF.UniUnlit.Runtime|Unlit shader utility|*|*|
|VRMShaders.VRM10.MToon10.Editor|MToon 1.0 shader utility|-|-|
|VRMShaders.VRM10.MToon10.Runtime|MToon 1.0 shader utility|*|*|

- For each of UniVgo2, and UniVgo2.Editor, * is attached to the dependent DLL.

### How to use UniVRM and UniVGO together

The version combinations are as follows.

|UniVRM|UniVGO|min Unity|
|:---:|:---:|:---:|
|0.100.0|2.5.22|2020.3|
|0.101.0|2.5.22|2020.3|
|0.102.0|2.5.22|2020.3|
|0.103.2|2.5.22|2020.3|
|0.104.2|2.5.22|2020.3|
|0.105.0|2.5.22|2020.3|
|0.106.0|2.5.22|2020.3|
|0.107.2|2.5.22|2020.3|
|0.108.0|2.5.22|2020.3|
|0.109.0|2.5.22|2020.3|
|0.110.0|2.5.22|2020.3|
|0.111.0|2.5.22|2020.3|
|0.112.0|2.5.22|2021.3|
|0.113.0|2.5.22|2021.3|
|0.114.0|2.5.22|2021.3|
|0.115.0|2.5.22|2021.3|
|0.116.0|2.5.22|2021.3|
|0.117.0|2.5.22|2021.3|
|0.118.0|2.5.22|2021.3|
|0.119.0|2.5.22|2021.3|
|0.120.0|2.5.22|2021.3|
|0.121.0|2.5.22|2021.3|
|0.122.0|2.5.22|2021.3|
|0.123.0|2.5.22|2021.3|
|0.124.2|2.5.22|2021.3|
|0.125.0|2.5.22|2021.3|

Write the following in `<Project> /Packages/package.json`.

```json
{
  "dependencies": {
    ...
    "com.vrmc.gltf": "https://github.com/vrm-c/UniVRM.git?path=/Assets/UniGLTF#v0.124.2",
    "com.vrmc.univrm": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRM#v0.124.2",
    "com.vrmc.vrm": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRM10#v0.124.2",
    "com.vrmc.vrmshaders": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRMShaders#v0.124.2",
    ...
  }
}
```

If you are using UniVRM 0.125 or higher, add the following instead of the above.

```json
{
  "dependencies": {
    ...
    "com.vrmc.gltf": "https://github.com/vrm-c/UniVRM.git?path=/Assets/UniGLTF#v0.125.0",
    "com.vrmc.univrm": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRM#v0.125.0",
    "com.vrmc.vrm": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRM10#v0.125.0",
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
|2022.3.0f1|BRP|UniVGO + UniVRM|[Link](https://github.com/izayoijiichan/univgo2.sample.unity.project/tree/unity2022.3.brp.univrm)|

___
Last updated: 4 November, 2024  
Editor: Izayoi Jiichan

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
