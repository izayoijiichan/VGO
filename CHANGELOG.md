# Change Log

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
