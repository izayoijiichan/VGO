# Change Log

## [2.5.22] - 2024-11-04

- Features
  - The following Particle System modules are now available.
    - `TextureSheetAnimation`
    - `CustomData`
  - NOVA shader version 2.4.0 is now supported.
    - `jp.co.cyberagent.novas`
    - `com.izayoi.nova.shader.utility`
    - However, `Texture2DAarray` and `Texture3D` are not supported.

## [2.5.21] - 2024-10-01

- Features
  - Unity 6000.0 is now supported.
  - The following packages are optional.
    - `com.izayoi.liltoon.shader.utility`
    - `com.vrmc.vrmshaders`
- Changes
  - The source of `UniUnlit`, `MToon` and `MToon10` shaders varies depending on the package version.
    - UniGLTF/UniUnlit
      - `com.vrmc.vrmshaders` 0.112.0 to 0.124.2
      - `com.vrmc.gltf` 0.125.0 or higher
    - MToon 0.x
      - `com.vrmc.vrmshaders` 0.112.0 to 0.124.2
      - `com.vrmc.univrm` 0.125.0 or higher
    - MToon 1.0
      - `com.vrmc.vrmshaders` 0.112.0 to 0.124.2
      - `com.vrmc.vrm` 0.125.0 or higher

## [2.5.20] - 2024-01-20

- Features
  - Unity 2023.2 is now supported.
  - lilToon shader version 1.7.2 is now supported.

## [2.5.19] - 2023-11-08

- Features
  - lilToon shader version 1.4.1 is now supported.

## [2.5.18] - 2023-09-16

- Improvements
  - Reduced the amount of memory used temporarily when loading.
- Changes
  - `VgoImporter.ReflectSkybox()` method has been deprecated. Use `VgoModelAsset.ReflectSkybox()` method instead.

## [2.5.17] - 2023-08-27

- Features
  - Enabled to use Stream argument for Load method of VgoImporter.
- Fixes
  - [Editor] Fixed a bug that occurred in BlendShape of Scripted Importer.
  - [Editor] Fixed a bug in v2.5.16 that caused error when declaring `UNIVGO_USE_UNITASK`.

## [2.5.16] - 2023-08-24

- Experiments
  - Support `WebP` format for image type (media type / MIME type) in texture.

## [2.5.15] - 2023-08-18

- Fixes
  - Fixed a bug that caused an error when building IL2CPP with Unity 2021.1 or lower.
  - Fixed a bug that processing was interrupted when an error occurred in BlendShape when importing in Editor.
  - Fixed typo.

## [2.5.14] - 2023-08-03

- Features
  - Added some methods to `VgoModelAsset` that can be used after runtime loading.
- Changes
  - The version of `com.izayoi.unishaders` is now `1.6.1`.
- Fixes
  - Fixed typo.

## [2.5.13] - 2023-08-01

- Improvements
  - Improved import and export handling of some materials.
- Changes
  - The conversion processes of Occulusion map and Metallic Roughness map were integrated as the conversion process of ORM map.

## [2.5.12] - 2023-07-28

- Features
  - In the URP environment, BRP shaders are converted to URP shaders as much as possible and loaded.
- Changes
  - The minimum version of `com.izayoi.unishaders` is now `1.6.0`.

## [2.5.11] - 2023-07-22

- Changes
  - ScriptableObject fields are now accessed by properties.
- Improvements
  - Program safety is enhanced.
- Fixes
  - The MeshExporter export option was not done correctly.

## [2.5.10] - 2023-07-17

- Features
  - Unity URP (Universal Render Pipeline) Particle shaders will be available.
- Changes
  - The minimum version of `com.izayoi.unishaders` is now `1.5.0`.
- Fixes
  - Fixed a bug that caused Export to fail when certain shaders were used.

## [2.5.9] - 2023-07-10

- Features
  - MToon 1.0 `VRM10/MToon10` shader will be available.
- Changes
  - The minimum version of `com.vrmc.vrmshaders` is now `0.79.0`. Recommended version is `0.104.0` or higher.

## [2.5.8] - 2023-07-05

- Improvements
  - Minor improvements were made to processing speed.

## [2.5.7] - 2023-07-01

- Features
  - VgoImporter.LoadAsync() method is added.
    - If you are using `Unity 2023.1` or higher, you can declare `UNIVGO_USE_UNITY_AWAITABLE` script define symbol.
    - If you are using `UniTask 2.0` or higher, you can declare `UNIVGO_USE_UNITASK` script define symbol.
    - None of these may be used without declaration.
    - Asynchronous methods load models more slowly than synchronous methods.
    - This method is not provided in `WebGL`.
- Changes
  - The class name of `ModelAsset` was changed to `VgoModelAsset`.

## [2.5.6] - 2023-06-20

- Fixes
  - Fixed script errors in Unity `2020.3`.

## [2.5.5] - 2023-06-01

- Features
  - lilToon shader version 1.4.0 is now supported.
- Improvements
  - A pop-up window is now displayed during export operations.
- Fixes
  - [Editor] Fixed a bug where the elements `Face Parts`, `Blinks`, `Visemes`, and `Presets` in `Vgo Blend Shape` only increased by 1 in one operation.

## [2.5.4] - 2023-05-04

- Features
  - lilToon shader version 1.3.7 is now supported.
  - [Editor] Blendshape indices of `Vgo Blend Shape` are now selectable in dropdown format.
- Fixes
  - Fixed a setting error for lilToon in versionDefines in UniVgo2.asmdef.
  - [Editor] Fixed a problem in which `ModelAsset` was not properly disposed when played in the Editor.
- Changes
  - Public fields have been changed to properties.

## [2.5.3] - 2023-04-04

- Fixes
  - Fixed Vgo Blend Shape component being attached to all skinned mesh renderers on import.
  - Fixed export and import failures when skinned mesh renderer's bones settings are incorrect.

## [2.5.2] - 2022-09-18

- Changes
  - The minimum version of unity is now `2020.3`.

## [2.5.1] - 2022-09-15

- Changes
  - Changed the package name from `com.izayoi.univgo2` to `com.izayoi.univgo`.
- Improvements
  - Scripting Define Symbol, which used to be done manually for each version of dependent packages, is now done automatically.
- Fixes
  - Fixed a bug that caused an error in Unity 2020.

## [2.5.0] - 2022-09-03

- Features
  - Updated the specification version of VGO to `2.5`.
  - 3D models from the 2.4 spec version can continue to be imported or loaded.
  - The spec version of the 3D model at the time of export will be 2.5.
- Improvements
  - Duplicate meshes are now referenced to the same one.
- Changes
  - Mesh configuration information has changed.

## [2.4.11] - 2022-08-24

- Improvements
  - Refactoring of the program was done.
  - Improved menu display in editor.

## [2.4.10] - 2022-07-11

- Features
  - lilToon Shader will be available.
- Adds
  - Add `com.izayoi.liltoon.shader.utility` package.

## [2.4.9] - 2022-05-20

- Features
  - Unity URP (Universal Render Pipeline) shaders (Simple Lit, Unlit) will be available.
- Changes
  - Updated the version of `com.izayoi.unishaders` to `1.4.0`.

## [2.4.8] - 2022-05-18

- Changes
  - Changed the library of NewtonSoft.JSON from `jillejr.newtonsoft.json-for-unity` to `com.unity.nuget.newtonsoft-json`.

## [2.4.7] - 2022-05-16

- Fixes
  - Fixed script errors in Unity `2020.3` and lower versions.

## [2.4.6] - 2022-02-27

- Features
  - Unity URP (Universal Render Pipeline) shaders (Lit) will be available.
- Changes
  - Updated the version of `com.izayoi.unishaders` to `1.3.0`.

## [2.4.5] - 2021-12-10

- Changes
  - The minimum version of `com.vrmc.vrmshaders` is now `0.72.0`.

## [2.4.4] - 2021-12-03

- Fixes
  - Fixed a bug where blend shape meshes could not be exported correctly.
- Changes
  - The minimum version of unity is now `2019.4`.
    The `package.json` settings are for Unity `2021.2`.
    If you want to use the version from Unity `2019.4` to` 2021.1`, please append the following packages or dlls.
    - `org.nuget.system.buffers`: `4.4.0` (System.Buffers.dll)
    - `org.nuget.system.memory`: `4.5.0` (System.Memory.dll)
    - `org.nuget.system.numerics.vectors`: `4.4.0` (System.Numerics.Vectors.dll)
    - `org.nuget.system.runtime.compilerservices.unsafe`: `4.5.0` (System.Runtime.CompilerServices.Unsafe.dll)

## [2.4.3] - 2021-11-20

- Changes
  - The minimum version of unity is now `2021.2`.
    If you want to use the version from Unity `2019.3` to` 2021.1`, please use the version of UniVGO `2.4.1` or `2.4.2`.

## [2.4.2] - 2021-11-10

- Features
  - Unity HDRP (High Definition Render Pipeline) shaders (Lit, Hair, Eye) will be available.
- Changes
  - The minimum version of unity is now `2019.3`.
  - Updated the version of `com.izayoi.unishaders` to `1.2.0`.

## [2.4.1] - 2021-11-08

- Fixes
  - Fixed a bug that could cause an error when importing if sparse was applied to the mesh resource accessor.
  - Fixed a bug that could cause an error when importing a mesh blend shape.
  - Fixed a bug where material normal maps were not imported correctly.
  - Fixed the character code of the file to `Shift_JIS` to `UTF-8`.

## [2.4.0] - 2021-06-05

- Features
  - Updated the specification version of VGO to `2.4`.
  - Cloth is now available.
  - It is not compatible with `2.3` or earlier due to changes in the configuration of collider and light.
- Changes
  - Added `clothes`, `colliders` and `lights` properties to `layout` schema.
  - Added `cloth` properties to `node` schema.
  - Changed the type of `colliders` property from an array of `vgo.collider` to an array of `int` in `node` schema.
  - Changed the type of `light` property from `vgo.light` to `int` in `node` schema.
  - Added `enabled` properties to `vgo.skybox` schema.

## [2.3.2] - 2021-06-01

- Fixes
  - Fixed a bug that an error may occur when importing or loading VgoSpringBone.

## [2.3.1] - 2021-05-10

- Features
  - Updated the specification version of VGO to `2.3`.
- Changes
  - Added `name` and `enabled` properties to `springBoneGroup` in layout schema.
- Fixes
  - Fixed a bug that even if `VgoSpringBoneGroup` is disabled and export, it will be import in the enabled state.

## [2.3.0] - 2021-03-18

- Changes
  - Changed the package name from `izayoi.univgo2` to `com.izayoi.univgo2`.

## [2.2.1] - 2021-03-12

- Fixes
  - Fixed a bug where mesh UV1, UV2 and UV3 were not set correctly when importing.

## [2.2.0] - 2021-02-01

- Features
  - Updated the specification version of VGO to `2.2`.
  - Animation is now available (Unity legacy animation).

## [2.1.2] - 2021-01-31

- Improvements
  - Updated Importer.
  - Fixed not to output springBoneInfo definition when the model does not use SpringBone.
- Fixes
  - Fixed a bug where the Spring Bone's gravity direction and collider offset were not set to values according to geometry coordinates.

## [2.1.1] - 2021-01-12

- Features
  - Added `Unity 2020.2` to the supported Unity versions.
- Fixes
  - Fixed a bug that caused an error on models that do not use VGO SpringBone.

## [2.1.0] - 2020-12-01

- Features
  - Updated the specification version of VGO to `2.1`.
  - VGO SpringBone is now available.

## [2.0.1] - 2020-09-17

- Fixes
  - Fixed a bug that importing vgo files that do not use blendshape fails.
- Adds
  - Added Script Define Symbols for use with version 1.0.
    VGO_2_DISABLE_SCRIPTED_IMPORTER and VGO_FILE_EXTENSION_2

## [2.0.0] - 2020-08-20
Initial release of the UniVGO2.
