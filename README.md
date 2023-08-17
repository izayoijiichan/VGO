# VGO

VGO is a new type of 3D data format.

![3D Model](https://img.shields.io/badge/3D%20Model-VGO-B89A13.svg?style=flat)
![VGO](https://img.shields.io/badge/VGO-2.5-8EAC50.svg?style=flat)
[![UniVGO](https://img.shields.io/github/v/release/izayoijiichan/VGO?label=UniVGO)](https://github.com/izayoijiichan/VGO/releases)
![Unity](https://img.shields.io/badge/Unity-2020%7e2023-2196F3.svg?logo=unity&style=flat)
![C#](https://img.shields.io/badge/C%23-8.0%7e9.0-058E0C.svg?logo=csharp&style=flat)
![license](https://img.shields.io/github/license/izayoijiichan/VGO)
[![wiki](https://img.shields.io/badge/GitHub-wiki-181717.svg?logo=github&style=flat)](https://github.com/izayoijiichan/VGO/wiki)

## Features

- You can save basic information of 3D model such as `Node`, `Transform`, `Rigidbody`, `Collider`, `Mesh`, `Blend Shape`, `Material`, `Texture`.
- You can also save the information of `Human Avatar`, `Spring Bone`, `Animation`, `Cloth`, `Light`, `Particle System`, `Skybox` for use in Unity.
- You can also save the information of `Blend Shape Preset` for use in the application.
- The file format uses IFF chunk as the base.
- Use `JSON`, `BSON`, `Binary` as internal data.
- The geometry coordinate system can have both right-handed and left-handed data.
- It is a specification that allows its own definition extension (chunk, schema).
- It is designed to support partial encryption.
- Resources are basically included in a file, but they can be cut out in another file.
- Resources can compress data with sparse, and have their own definition of the more powerful sparse.
- There is no direct compatibility with glTF.
- It is possible to convert to other formats including glTF by expanding the data in Unity Editor.

## Translation

[Japanese](https://github.com/izayoijiichan/VGO/blob/main/README.ja.md).

## File extension

|extension|description|required|
|:--:|:--|:--:|
|.vgo|This is a VGO file.|true|
|.vgk|It is a key file to decrypt the encrypted VGO file.|false|
|(.bin)|Resource file.|false|

## Tools

### UniVGO

A tool for creating\/exporting\/importing\/loading VGO file.

You can easily export a VGO file with the click of a button.

![image1](https://github.com/izayoijiichan/VGO/blob/main/Documentation~/UniVGO/Images/500_Export.png)

You can easily import and restore VGO files by just placing them in Assets.

![image2](https://github.com/izayoijiichan/VGO/blob/main/Documentation~/UniVGO/Images/620_Import.png)

You can easily run-time load a VGO file by writing a few scripts.


~~~csharp
    using System;
    using UnityEngine;
    using UniVgo2;

    public class VgoRuntimeLoader : MonoBehaviour
    {
        private readonly VgoImporter _VgoImporter = new();

        [SerializeField]
        private string _FilePath = string.Empty;

        private VgoModelAsset? _VgoModelAsset;

        private void Start()
        {
            _VgoModelAsset = _VgoImporter.Load(_FilePath);
        }

        private void OnDestroy()
        {
            _VgoModelAsset?.Dispose();
        }
    }
~~~

[Wiki](https://github.com/izayoijiichan/VGO/wiki)

___
Last updated: 18 August, 2023  
Editor: Izayoi Jiichan

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
