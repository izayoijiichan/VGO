# UniVGO experimental features manual

Manual for experimental features.

## WebP format texture

### 1-1. Unity Editor

Unity 2021.2 or higher.

### 1-2. Package

`<Project>/Packages/package.json`.  

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

### 1-3. Define symbol

Unity Editor

Menu Bar > `Edit` > `Projcet Settings`

Projcet Settings Window

`Player` > `Other Settings` > `Script Compilation` > `Scripting Define Symbols`

Add the Define symbol.

`UNIVGO_EXPORT_WEBP_TEXTURE_ENABLE`

### 1-4. Export

Select the GameObject you want to output in Hierarchy.

Click the `Export VGO` button of `Vgo Generator` in Inspector.

Set the export condition.

Select the `WebP (expt)` radio button for `Texture Type`.

Finally, click the `Export` button.

### 1-5. Import / Load

There is no difference in the operating procedure from the normal case.

### 1-6. Comparison

Comparison data of `PNG` and `WebP`.

The target is a humanoid avatar with 6 textures.

|Subject|PNG|WebP|Ratio|
|:---|---:|---:|---:|
|File Size|20MB|17.5MB|-13%|
|Export Time|2,500ms|81,000ms|+3240%|
|Import Time|1,500ms|10,200ms|+680%|
|Runtime Load Time (Editor, sync)|1,500ms|14,000ms|+933%|
|Runtime Load Time (Editor, async)|2,200ms|5,700ms|+250%|
|Runtime Load Time (IL2CPP, async)|1,100ms|2,500ms|+227%|

### 1-7. Inspection result

The data size of the VGO file is reduced by high compressing the image data.

On the other hand, the image data encoding / decoding process becomes heavy.

Also, since Unity Engine does not support WebP format, it is necessary to go through format conversion processing.

Due to the specifications of the Unity Editor, export processing cannot be performed asynchronously or in parallel, so export processing takes time.

Synchronous processing may be faster than asynchronous processing because, according to Unity Engine specifications, important processing must be performed on the main thread, and there are almost no cases where parallelization is possible.

Downloading a VGO file using `UnityWebRequest.Get(uri)` is faster, but `VgoImporter.LoadAsync()` is slower.

___
Last updated: 24 August, 2023  
Editor: Izayoi Jiichan

*Copyright (C) 2023 Izayoi Jiichan. All Rights Reserved.*
