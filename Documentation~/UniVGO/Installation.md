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
|Unity 2020.1|unconfirmed|unconfirmed|unconfirmed|unconfirmed|unconfirmed|

As of June of 2020, we are developing and confirming in `Unity 2019.4` `Windows` `.NET Standard 2.0` environment.

### Required package

|package name|owner|Repository|specification version|program version|release date|
|:---|:---:|:---:|:---:|:---:|:---:|
|org.nuget.system.buffers|Microsoft|NuGet||4.4.0|11 Aug, 2017|
|org.nuget.system.memory|Microsoft|NuGet||4.5.0|29 May, 2018|
|org.nuget.system.numerics.vectors|Microsoft|NuGet||4.4.0|11 Aug, 2017|
|org.nuget.system.runtime.compilerservices.unsafe|Microsoft|NuGet||4.5.0|29 May, 2018|
|newtonsoft-json-for-unity|jillejr|GitHub|12.0.3|12.0.301|20 Jan, 2020|
|VRMShaders|vrm-c|GitHub|VRM 0.0|0.56.0|3 Jul, 2020|
|UniShaders|IzayoiJiichan|GitHub|-|1.0.1|13 Aug, 2020|
|UniVGO2|IzayoiJiichan|GitHub|VGO 2.0|2.0.0|20 Aug, 2020|

___
## Install

### Installation procedure (Case using the sample project)


#### 1. Download sample project

Download the UniVGO sample project.

https://github.com/izayoijiichan/univgo2.sample.unity.project

#### 2. Install Unity

Install `Unity 2019.4.0f1` on Unity Hub.

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
    "com.unity.ugui": "1.0.0",
    "com.vrmc.vrmshaders": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRMShaders#v0.56.0",
    "izayoi.unishaders": "https://github.com/izayoijiichan/UniShaders.git#v1.0.1",
    "izayoi.univgo2": "https://github.com/izayoijiichan/VGO2.git#v2.0.0",
    "jillejr.newtonsoft.json-for-unity": "12.0.301",
    "com.unity.modules.ai": "1.0.0",
    ...
    "com.unity.modules.xr": "1.0.0"
  }
}
```
At this point you will see a compilation error in the UnityEditor console.

#### 3. Additional installation of required packages

Use the package manager to install additional required packages.

Click `[Window]`> `[Package Manager]` on the menu bar of Unity Editor.

![image1](https://github.com/izayoijiichan/vgo2/blob/master/Documentation~/UniVGO/Images/201_PackageManager.png)

In the `Package Manager`, select `My Repositries`.

Select `System.Memory (NuGet)` and install `4.5.0`.

`System.Buffers`, `System.Numerics.Vectors`, `System.Runtime.CompilerServices.Unsage` are automatically installed.

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

- For each of UniVgo2, and UniVgo2.Editor, * is attached to the dependent DLL.
- MToon, ShaderProperty, UniUnlit is a program packed in VRMShaders (©vrm-c).
- When using UniVGO (ver1) and UniVGO2 together, UniVgo (ver1) UniGLTF folder and UniVgo (ver1) Editor folder must be deleted. Conflicts occur because both have the extension `.vgo`.
- When using UniVRM and UniVGO together, it is necessary to delete the duplicate files that were packed when UniVgo was obtained. The shaders in the UniGLTF folder are relevant.

___
Last updated: 20 August, 2020  
Editor: Izayoi Jiichan

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
