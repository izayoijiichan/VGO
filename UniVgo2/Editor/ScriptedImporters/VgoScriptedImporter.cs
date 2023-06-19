// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Editor
// @Class     : VgoScriptedImporter
// ----------------------------------------------------------------------
#nullable enable
#if !VGO_2_DISABLE_SCRIPTED_IMPORTER
namespace UniVgo2.Editor
{
    using NewtonVgo;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using UnityEditor;
#if UNITY_2020_2_OR_NEWER
    using UnityEditor.AssetImporters;
#else
    using UnityEditor.Experimental.AssetImporters;
#endif
    using UnityEngine;

    /// <summary>
    /// VGO Scripted Importer
    /// </summary>
#if VGO_FILE_EXTENSION_2
    [ScriptedImporter(1, "vgo2")]
#else
    [ScriptedImporter(1, "vgo")]
#endif
    public class VgoScriptedImporter : ScriptedImporter
    {
        /// <summary>The animation directory name.</summary>
        protected readonly string AnimationDirectoryName = "Animations";

        /// <summary>The avatar directory name.</summary>
        protected readonly string AvatarDirectoryName = "Avatars";

        /// <summary>The blend shape directory name.</summary>
        protected readonly string BlendShapeDirectoryName = "BlendShapes";

        /// <summary>The material directory name.</summary>
        protected readonly string MaterialDirectoryName = "Materials";

        /// <summary>The mesh directory name.</summary>
        protected readonly string MeshDirectoryName = "Meshes";

        /// <summary>The texture directory name.</summary>
        protected readonly string TextureDirectoryName = "Textures";

        /// <summary>
        /// This method must by overriden by the derived class and is called by the Asset pipeline to import files.
        /// </summary>
        /// <param name="ctx">
        /// This argument contains all the contextual information needed to process the import event
        /// and is also used by the custom importer to store the resulting Unity Asset.
        /// </param>
        public override void OnImportAsset(AssetImportContext ctx)
        {
            try
            {
                ModelAsset modelAsset = LoadModel(ctx.assetPath);
                {
                    // Animation
                    if (modelAsset.AnimationClipList != null)
                    {
                        Dictionary<string, AnimationClip> externalObjects = GetExternalUnityObjects<AnimationClip>();

                        foreach (AnimationClip? animationClip in modelAsset.AnimationClipList)
                        {
                            if (animationClip == null)
                            {
                                continue;
                            }

                            if (externalObjects.ContainsValue(animationClip))
                            {
                                continue;
                            }

                            ctx.AddObjectToAsset(animationClip.name, animationClip);
                        }
                    }

                    // Avatar
                    if (modelAsset.Avatar != null)
                    {
                        var externalObjects = GetExternalUnityObjects<Avatar>();

                        if (externalObjects.ContainsValue(modelAsset.Avatar) == false)
                        {
                            ctx.AddObjectToAsset(modelAsset.Avatar.name ?? "avatar", modelAsset.Avatar);
                        }
                    }

                    // Material
                    if (modelAsset.MaterialList != null)
                    {
                        Dictionary<string, Material> externalObjects = GetExternalUnityObjects<Material>();

                        foreach (Material? material in modelAsset.MaterialList)
                        {
                            if (material == null)
                            {
                                continue;
                            }

                            if (externalObjects.ContainsValue(material))
                            {
                                continue;
                            }

                            ctx.AddObjectToAsset(material.name, material);
                        }
                    }

                    // Mesh
                    if (modelAsset.MeshAssetList != null)
                    {
                        Dictionary<string, Mesh> externalObjects = GetExternalUnityObjects<Mesh>();

                        foreach (MeshAsset meshAsset in modelAsset.MeshAssetList)
                        {
                            if (meshAsset?.Mesh == null)
                            {
                                continue;
                            }

                            if (externalObjects.ContainsValue(meshAsset.Mesh))
                            {
                                continue;
                            }

                            ctx.AddObjectToAsset(meshAsset.Mesh.name, meshAsset.Mesh);
                        }
                    }

                    // Texture
                    if (modelAsset.Texture2dList != null)
                    {
                        Dictionary<string, Texture2D> externalObjects = GetExternalUnityObjects<Texture2D>();

                        foreach (Texture2D? texture in modelAsset.Texture2dList)
                        {
                            if (texture == null)
                            {
                                continue;
                            }

                            if (externalObjects.ContainsValue(texture))
                            {
                                continue;
                            }

                            ctx.AddObjectToAsset(texture.name, texture);
                        }
                    }

                    // ScriptableObject
                    if (modelAsset.ScriptableObjectList != null)
                    {
                        // BlendShape
                        var blendShapeConfigurationList = modelAsset.ScriptableObjectList
                            .Where(x => x != null)
                            .Where(x => x.GetType() == typeof(BlendShapeConfiguration))
                            .Select(x => x as BlendShapeConfiguration)
                            .ToList();

                        var externals = GetExternalUnityObjects<BlendShapeConfiguration>();

                        foreach (BlendShapeConfiguration? blendShapeConfiguration in blendShapeConfigurationList)
                        {
                            if (blendShapeConfiguration == null)
                            {
                                continue;
                            }

                            if (externals.ContainsValue(blendShapeConfiguration))
                            {
                                continue;
                            }

                            blendShapeConfiguration.name = blendShapeConfiguration.kind + "BlendShapeConfiguration";
                            ctx.AddObjectToAsset(blendShapeConfiguration.name, blendShapeConfiguration);
                        }
                    }

                    // Root
                    if (modelAsset.Root != null)
                    {
                        ctx.AddObjectToAsset(modelAsset.Root.name, modelAsset.Root);
                        ctx.SetMainObject(modelAsset.Root);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        /// <summary>
        /// Load 3D model.
        /// </summary>
        /// <param name="vgoFilePath">The file path of the vgo.</param>
        /// <returns>A model asset.</returns>
        protected virtual ModelAsset LoadModel(string vgoFilePath)
        {
            string? vgkFilePath = FindVgkFilePath(vgoFilePath);

            var importer = new VgoImporter();

            ModelAsset modelAsset = importer.Load(vgoFilePath, vgkFilePath);

            return modelAsset;
        }

        /// <summary>
        /// Extract 3D model.
        /// </summary>
        /// <param name="vgoFilePath">The file path of the vgo.</param>
        /// <returns>A model asset.</returns>
        protected virtual ModelAsset ExtractModel(string vgoFilePath)
        {
            string? vgkFilePath = FindVgkFilePath(vgoFilePath);

            var importer = new VgoImporter();

            ModelAsset modelAsset = importer.Extract(vgoFilePath, vgkFilePath);

            return modelAsset;
        }

        /// <summary>
        /// Find the file path of the vgk.
        /// </summary>
        /// <param name="vgoFilePath">The file path of the vgo.</param>
        /// <returns>The file path of the vgk.</returns>
        protected virtual string? FindVgkFilePath(string vgoFilePath)
        {
            var vgoFileInfo = new FileInfo(vgoFilePath);

            string vgkFileName = vgoFileInfo.Name.Substring(0, vgoFileInfo.Name.Length - vgoFileInfo.Extension.Length) + ".vgk";

            string? vgkFilePath = Path.Combine(vgoFileInfo.DirectoryName, vgkFileName);

            var vgkFileInfo = new FileInfo(vgkFilePath);

            if (vgkFileInfo.Exists == false)
            {
                vgkFilePath = null;
            }

            return vgkFilePath;
        }

        /// <summary>
        /// Extract animation clips.
        /// </summary>
        public virtual void ExtractAnimationClips()
        {
            ExtractAssets<AnimationClip>(AnimationDirectoryName, ".anim");

            Dictionary<string, AnimationClip> externalObjects = GetExternalUnityObjects<AnimationClip>();

            foreach ((_, AnimationClip animationClip) in externalObjects)
            {
                string assetPath = AssetDatabase.GetAssetPath(animationClip);

                if (string.IsNullOrEmpty(assetPath) == false)
                {
                    EditorUtility.SetDirty(animationClip);
                    AssetDatabase.WriteImportSettingsIfDirty(assetPath);
                }
            }

            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
        }

        /// <summary>
        /// Extract avatars.
        /// </summary>
        public void ExtractAvatars()
        {
            ExtractAssets<Avatar>(AvatarDirectoryName, ".asset");

            Dictionary<string, Avatar> externalObjects = GetExternalUnityObjects<Avatar>();

            foreach ((_, Avatar avatar) in externalObjects)
            {
                string assetPath = AssetDatabase.GetAssetPath(avatar);

                if (string.IsNullOrEmpty(assetPath) == false)
                {
                    EditorUtility.SetDirty(avatar);
                    AssetDatabase.WriteImportSettingsIfDirty(assetPath);
                }
            }

            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
        }

        /// <summary>
        /// Extract blendshapes.
        /// </summary>
        public void ExtractBlendShapes()
        {
            ExtractAssets<BlendShapeConfiguration>(BlendShapeDirectoryName, ".asset");

            Dictionary<string, BlendShapeConfiguration> externalObjects = GetExternalUnityObjects<BlendShapeConfiguration>();

            foreach ((_, BlendShapeConfiguration blendShapeConfiguration) in externalObjects)
            {
                string assetPath = AssetDatabase.GetAssetPath(blendShapeConfiguration);

                if (string.IsNullOrEmpty(assetPath) == false)
                {
                    EditorUtility.SetDirty(blendShapeConfiguration);
                    AssetDatabase.WriteImportSettingsIfDirty(assetPath);
                }
            }

            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
        }

        /// <summary>
        /// Extract materials.
        /// </summary>
        public virtual void ExtractMaterials()
        {
            ExtractAssets<Material>(MaterialDirectoryName, ".mat");
            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
        }

        /// <summary>
        /// Extract meshes.
        /// </summary>
        public void ExtractMeshes()
        {
            ExtractAssets<Mesh>(MeshDirectoryName, ".mesh");
            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
        }

        /// <summary>
        /// Extract textures and materials.
        /// </summary>
        public virtual void ExtractTexturesAndMaterials()
        {
            ExtractTextures(TextureDirectoryName, (path) => { return ExtractModel(path); }, continueMaterial: true);
            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
        }

        /// <summary>
        /// Extract textures.
        /// </summary>
        public virtual void ExtractTextures()
        {
            ExtractTextures(TextureDirectoryName, (path) => { return ExtractModel(path); });
            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
        }

        /// <summary>
        /// Extract textures.
        /// </summary>
        /// <param name="dirName"></param>
        /// <param name="ExtractModel"></param>
        /// <param name="continueMaterial"></param>
        public virtual void ExtractTextures(string dirName, Func<string, ModelAsset> ExtractModel, bool continueMaterial = false)
        {
            if (string.IsNullOrEmpty(assetPath))
            {
                return;
            }

            IEnumerable<Texture2D> subAssets = GetSubAssets<Texture2D>(assetPath);

            string path = string.Format("{0}/{1}.{2}",
                Path.GetDirectoryName(assetPath),
                Path.GetFileNameWithoutExtension(assetPath),
                dirName
                );

            CreateDirectoryIfNotExists(path);

            Dictionary<VgoTexture, string> targetPaths = new Dictionary<VgoTexture, string>();

            // Reload Model
            using (ModelAsset modelAsset = ExtractModel(assetPath))
            {
                if (modelAsset.Layout != null &&
                    modelAsset.Layout.textures != null &&
                    modelAsset.Layout.textures.Any() &&
                    modelAsset.Texture2dList != null &&
                    modelAsset.Texture2dList.Any())
                {
                    for (int textureIndex = 0; textureIndex < modelAsset.Layout.textures.Count; textureIndex++)
                    {
                        VgoTexture? vgoTexture = modelAsset.Layout.textures[textureIndex];

                        if (vgoTexture == null)
                        {
                            continue;
                        }

                        Texture2D? texture2d = modelAsset.Texture2dList[textureIndex];

                        if (texture2d == null)
                        {
                            continue;
                        }

                        string fileExt;

                        byte[] imageBytes;

                        if (vgoTexture.mimeType == MimeType.Image_Jpeg)
                        {
                            fileExt = ".jpg";

                            byte[]? jpgBytes = ImageConversion.EncodeToJPG(texture2d, quality: 100);

                            if (jpgBytes == null)
                            {
                                continue;
                            }

                            imageBytes = jpgBytes;
                        }
                        else if (vgoTexture.mimeType == MimeType.Image_Png)
                        {
                            fileExt = ".png";

                            byte[]? pngBytes = ImageConversion.EncodeToPNG(texture2d);

                            if (pngBytes == null)
                            {
                                continue;
                            }

                            imageBytes = pngBytes;
                        }
                        else
                        {
                            continue;
                        }

                        string fileName = vgoTexture.name is null || vgoTexture.name == string.Empty
                            ? string.Format("Image_{0:000}", textureIndex)
                            : vgoTexture.name;

                        string targetPath = Path.Combine(path, fileName + fileExt);

                        File.WriteAllBytes(targetPath, imageBytes);

                        AssetDatabase.ImportAsset(targetPath);

                        targetPaths.Add(vgoTexture, targetPath);
                    }
                }
            }

            EditorApplication.delayCall += () =>
            {
                foreach ((VgoTexture vgoTexture, string targetPath) in targetPaths)
                {
                    AssetImporter assetImporter = AssetImporter.GetAtPath(targetPath);

                    if (assetImporter is TextureImporter targetTextureImporter)
                    {
                        //
                    }
                    else
                    {
                        continue;
                    }

                    targetTextureImporter.sRGBTexture = (vgoTexture.colorSpace == VgoColorSpaceType.Srgb);

                    if (vgoTexture.mapType == VgoTextureMapType.NormalMap)
                    {
                        targetTextureImporter.textureType = TextureImporterType.NormalMap;
                    }

                    targetTextureImporter.SaveAndReimport();

                    UnityEngine.Object externalObject = AssetDatabase.LoadAssetAtPath<Texture2D>(targetPath);

                    AddRemap(new AssetImporter.SourceAssetIdentifier(typeof(Texture2D), vgoTexture.name), externalObject);
                }

                //AssetDatabase.WriteImportSettingsIfDirty(assetPath);
                AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);

                if (continueMaterial)
                {
                    ExtractMaterials();
                }
            };
        }

        /// <summary>
        /// Clear external objects.
        /// </summary>
        public virtual void ClearExtarnalObjects()
        {
            foreach (var extarnalObject in GetExternalObjectMap())
            {
                RemoveRemap(extarnalObject.Key);
            }
            AssetDatabase.WriteImportSettingsIfDirty(assetPath);
            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
        }

        /// <summary>
        /// Clear external objects.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public virtual void ClearExtarnalObjects<T>() where T : UnityEngine.Object
        {
            foreach (var extarnalObject in GetExternalObjectMap().Where(x => x.Key.type == typeof(T)))
            {
                RemoveRemap(extarnalObject.Key);
            }
            AssetDatabase.WriteImportSettingsIfDirty(assetPath);
            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
        }

        /// <summary>
        /// Extract assets.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dirName"></param>
        /// <param name="extension"></param>
        public virtual void ExtractAssets<T>(string dirName, string extension) where T : UnityEngine.Object
        {
            if (string.IsNullOrEmpty(assetPath))
            {
                return;
            }

            IEnumerable<T> subAssets = GetSubAssets<T>(assetPath);

            string path = string.Format("{0}/{1}.{2}",
                Path.GetDirectoryName(assetPath),
                Path.GetFileNameWithoutExtension(assetPath),
                dirName
                );

            CreateDirectoryIfNotExists(path);

            foreach (T asset in subAssets)
            {
                ExtractFromAsset(asset, string.Format("{0}/{1}{2}", path, asset.name, extension), false);
            }
        }

        /// <summary>
        /// Extract from asset.
        /// </summary>
        /// <param name="subAsset"></param>
        /// <param name="destinationPath"></param>
        /// <param name="isForceUpdate"></param>
        protected virtual void ExtractFromAsset(UnityEngine.Object subAsset, string destinationPath, bool isForceUpdate)
        {
            string assetPath = AssetDatabase.GetAssetPath(subAsset);

            UnityEngine.Object clone = Instantiate(subAsset);

            AssetDatabase.CreateAsset(clone, destinationPath);

            AssetImporter assetImporter = GetAtPath(assetPath);

            assetImporter.AddRemap(new SourceAssetIdentifier(subAsset), clone);

            if (isForceUpdate)
            {
                AssetDatabase.WriteImportSettingsIfDirty(assetPath);
                AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
            }
        }

        /// <summary>
        /// Gets the external unity objects.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual Dictionary<string, T> GetExternalUnityObjects<T>() where T : UnityEngine.Object
        {
            return GetExternalObjectMap()
                .Where(x => x.Key.type == typeof(T))
                .ToDictionary(x => x.Key.name, x => (T)x.Value);
        }

        /// <summary>
        /// Set a external unity object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual void SetExternalUnityObject<T>(SourceAssetIdentifier sourceAssetIdentifier, T obj) where T : UnityEngine.Object
        {
            AddRemap(sourceAssetIdentifier, obj);
            AssetDatabase.WriteImportSettingsIfDirty(assetPath);
            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
        }

        /// <summary>
        /// Gets the sub assets.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetPath"></param>
        /// <returns></returns>
        protected virtual IEnumerable<T> GetSubAssets<T>(string assetPath) where T : UnityEngine.Object
        {
            return AssetDatabase
                .LoadAllAssetsAtPath(assetPath)
                .Where(x => AssetDatabase.IsSubAsset(x))
                .Where(x => x is T)
                .Select(x => (T)x);
        }

        /// <summary>
        /// Create a directory if it does not exist.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        protected virtual DirectoryInfo CreateDirectoryIfNotExists(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            if (directoryInfo.Exists == false)
            {
                directoryInfo.Create();
            }

            return directoryInfo;
        }
    }
}
#endif
