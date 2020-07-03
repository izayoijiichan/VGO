# UniVGO install manual

Manual for installing UniVGO in Unity Editor.

___
## System requirements

### Unity version

|version|Win (Editor)|Win (Mono)|Win (IL2CPP)|Android (IL2CPP)|iOS|
|:---|:---:|:---:|:---:|:---:|:---:|
|Unity 2018.4|unconfirmed|unconfirmed|unconfirmed|unconfirmed|unconfirmed|
|Unity 2019.1|unconfirmed|unconfirmed|unconfirmed|unconfirmed|unconfirmed|
|Unity 2019.2|unconfirmed|unconfirmed|unconfirmed|unconfirmed|unconfirmed|
|Unity 2019.3|OK|OK|OK|OK|unconfirmed|
|Unity 2019.4|OK|OK|OK|OK|unconfirmed|

As of June of 2020, we are developing and confirming in `Unity 2019.4` Windows environment.

### Required package

|package name|owner|Repository|specification version|program version|release date|
|:---:|:---:|:---:|:---:|:---:|:---:|
|newtonsoft-json-for-unity|jillejr|GitHub|12.0.3|12.0.301|20 Jan, 2020|
|UniVGO|IzayoiJiichan|GitHub|VGO 0.6|0.8.2|4 Jul, 2020|

___
## Install

### Installation procedure (Case using the sample project)


#### 1. Download sample project

Download the UniVGO sample project.

https://github.com/izayoijiichan/univgo.sample.unity.project

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

Import Newtonsoft.JSON into your project as a package.  
Write the following in `<Project>/Packages/package.json`.  
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
    "jillejr.newtonsoft.json-for-unity": "12.0.301",
    "com.unity.modules.ai": "1.0.0",
    ...
    "com.unity.modules.xr": "1.0.0"
  }
}
```

#### 3. Installation of VRMShaders

Import VRMShaders into your project as a package.  
Write the following in `<Project>/Packages/package.json`.  
You need to be careful where you add them.

```json
{
  "dependencies": {
    "com.unity.ugui": "1.0.0",
    "com.vrmc.vrmshaders": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRMShaders#v0.56.1",
    "jillejr.newtonsoft.json-for-unity": "12.0.301",
    "com.unity.modules.ai": "1.0.0",
    ...
    "com.unity.modules.xr": "1.0.0"
  }
}
```

#### 3. Installation of UniVGO

Import UniVGO into your project.

Import UniVGO into your project.

##### A) Case managing with package.json

Write the following in `<Project>/Packages/package.json`.

```json
{
  "dependencies": {
    "com.unity.ugui": "1.0.0",
    "com.vrmc.vrmshaders": "https://github.com/vrm-c/UniVRM.git?path=/Assets/VRMShaders#v0.56.1",
    "izayoi.univgo": "https://github.com/izayoijiichan/VGO.git#v0.8.2",
    "jillejr.newtonsoft.json-for-unity": "12.0.301",
    "com.unity.modules.ai": "1.0.0",
    ...
    "com.unity.modules.xr": "1.0.0"
  }
}
```

##### B) Case managing with Packages folder

Download the zip file from the following URL. 

UniVGO  
https://github.com/izayoijiichan/VGO/releases  

Unzip the file and place it in the `Packages` folder.

```
  <Project>
    Packages
      izayoi.univgo@0.8.1-preview
        DepthFirstScheduler
        UniGLTFforUniVgo
        UniSkybox
        UniStandardParticle
        UniVgo
```

You can change the name of the folder.

### Confirmation of installation completion

1. Open the project in Unity Editor.
2. Check that no errors are displayed in the Unity Editor console.
3. Check that the menu item `[Tools]` > `[UniVGO]` is displayed on the menu bar of Unity Editor.

### Causes of installation errors

Possible causes of the error are as follows.

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
|DepthFirstScheduler|Depth first scheduler|*|*|
|MToon|MToon shader utility|*|*|
|MToon.Editor|MToon shader utility|-|*|
|ShaderProperty.Runtime|Shader property information|*|*|
|UniGLTFforUniVgo|UniGLTF for UniVGO|*|*|
|UniSkybox|Skybox shader utility|*|*|
|UniStandardParticle|Particle shader utility|*|*|
|UniUnlit|Unlit shader utility|*|*|
|UniUnlit.Editor|Unlit shader utility|-|*|
|UniVgo|VGO main program|*|*|
|UniVgo.Editor|VGO import / export|-|*|

- For each of UniVgo, and UniVgo.Editor, * is attached to the dependent DLL.
- MToon, ShaderProperty, UniUnlit is a program packed in VRMShaders (© vrm-c).
- DepthFirstScheduler is a program packed in UniVRM (© vrm-c).
- When using UniVRM and UniVGO together, it is necessary to delete the duplicate files (DepthFirstScheduler) that were packed when UniVgo was obtained.
  Also, if the error is displayed by UniVgo, please move UniVgo, UniGLTFforUniVgo, UniSkybox, UniStandardParticle to the `Assets` folder.

___
Last updated: 4 July, 2020  
Editor: Izayoi Jiichan

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
