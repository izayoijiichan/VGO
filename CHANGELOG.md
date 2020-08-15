# Change Log

## [1.1.0] - 2020-08-15
- Features
  - Unity Humanoid Avatar parameter support.
  - Added common utility to manipulate blendshape.
- Adds
  - Added `avatar` property to gltf.extensions.vgo.
  - Added `blendShape` property to node.extensions.VGO_nodes.
- Improvements
  - Strengthened the check when importing meshes.
- Changes
  - VGO extended schema changed the type of EnumString to Enum.
  - Changed to not automatically reflect skybox to the main camera when loading a model.

## [1.0.1] - 2020-08-10
- Fixes
  - Fixed a bug that the file size becomes large with blank data when exporting.

## [1.0.0] - 2020-08-07
- A complete refactoring of the program has been done.
- I made the program into a library and separated each as a Unity package.
  - VgoGltf
  - NewtonGltf
  - NewtonGltf.Vgo.Extensions
  - UniShaders

## [0.8.3] - 2020-07-06
- The base of UniGTLF is updated to that of VRM v0.56.2.

## [0.8.2] - 2020-07-04
- Changed to use VRMShaders (com.vrmc.vrmshaders) of UnityPackageManager (UPM).

## [0.8.1] - 2020-06-20
- Added Glb import and export from UniGLTF.

## [0.8.0] - 2020-03-15
- Added `VGO_materials_skybox` property to materials extension.
- Added `skybox` property to VGO_nodes extension.

## [0.7.0] - 2020-01-23
- Added `VGO_materials_particle` property to materials extension.
- Added `particleSystem` property to VGO_nodes extension.

## [0.6.1] - 2020-01-20
- Refactored source code

## [0.6.0] - 2020-01-17
- Added `VGO_materials` property to materials extension.
- Added `VRMC_materials_mtoon` property to materials extension.

## [0.5.0] - 2020-01-14
- Added `light` property to VGO_nodes extension.

## [0.4.0] - 2020-01-08
- Added `gameObject` property to VGO_nodes extension.
- Added `enabled` property to collider.

## [0.3.0] - 2020-01-01
Initial release of the UniVGO.
