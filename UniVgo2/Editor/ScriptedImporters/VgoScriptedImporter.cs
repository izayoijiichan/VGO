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
    using System.Threading;
    using System.Threading.Tasks;
    using UnityEditor;
#if UNITY_2020_2_OR_NEWER
    using UnityEditor.AssetImporters;
#else
    using UnityEditor.Experimental.AssetImporters;
#endif
    using UnityEngine;
    using UniVgo2.Converters;

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
        #region Fields

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

        /// <summary>The vgo importer.</summary>
        protected readonly VgoImporter _VgoImporter = new VgoImporter();

        /// <summary>Whether to force extraction of textures as png.</summary>
        protected readonly bool _ExtractTexturePngForce
#if UNIVGO_SCRIPTED_IMPORTER_FORCE_PNG
            = true;
#else
            = false;
#endif

        /// <summary>The image converter.</summary>
        protected IImageConverter? _ImageConverter;

        #endregion

        #region Properties

        /// <summary>The image converter.</summary>
        protected IImageConverter ImageConverter => _ImageConverter ??= new ImageConverter();

        #endregion

        #region Public Methods

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
                var stopwatch = new System.Diagnostics.Stopwatch();

                stopwatch.Start();

                VgoModelAsset modelAsset = LoadModel(ctx.assetPath);
                {
#if !SIXLABORS_IMAGESHARP_2_OR_NEWER
                    if (modelAsset.Layout?.textures != null)
                    {
                        if (modelAsset.Layout.textures.Any(t => t?.mimeType == MimeType.Image_WebP))
                        {
                            Debug.LogWarningFormat("{0} contains WebP texture. (require SixLabors.ImageSharp)", ctx.assetPath);
                        }
                    }
#endif
                    // Animation
                    if (modelAsset.AnimationClipList != null)
                    {
                        Dictionary<SourceAssetIdentifier, AnimationClip> externalObjects = GetExternalUnityObjects<AnimationClip>();

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

                            try
                            {
                                ctx.AddObjectToAsset(animationClip.name, animationClip);
                            }
                            catch (Exception ex)
                            {
                                Debug.LogException(ex);
                            }
                        }
                    }

                    // Avatar
                    if (modelAsset.Avatar != null)
                    {
                        Dictionary<SourceAssetIdentifier, Avatar> externalObjects = GetExternalUnityObjects<Avatar>();

                        if (externalObjects.ContainsValue(modelAsset.Avatar) == false)
                        {
                            try
                            {
                                ctx.AddObjectToAsset(modelAsset.Avatar.name ?? "avatar", modelAsset.Avatar);
                            }
                            catch (Exception ex)
                            {
                                Debug.LogException(ex);
                            }
                        }
                    }

                    // Material
                    if (modelAsset.MaterialList != null)
                    {
                        Dictionary<SourceAssetIdentifier, Material> externalObjects = GetExternalUnityObjects<Material>();

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

                            try
                            {
                                ctx.AddObjectToAsset(material.name, material);
                            }
                            catch (Exception ex)
                            {
                                Debug.LogException(ex);
                            }
                        }
                    }

                    // Mesh
                    if (modelAsset.MeshAssetList != null)
                    {
                        Dictionary<SourceAssetIdentifier, Mesh> externalObjects = GetExternalUnityObjects<Mesh>();

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

                            try
                            {
                                ctx.AddObjectToAsset(meshAsset.Mesh.name, meshAsset.Mesh);
                            }
                            catch (Exception ex)
                            {
                                Debug.LogException(ex);
                            }
                        }
                    }

                    // Texture
                    if (modelAsset.TextureList != null)
                    {
                        Dictionary<SourceAssetIdentifier, Texture2D> externalObjects = GetExternalUnityObjects<Texture2D>();

                        foreach (Texture2D? texture in modelAsset.TextureList)
                        {
                            if (texture == null)
                            {
                                continue;
                            }

                            if (externalObjects.ContainsValue(texture))
                            {
                                continue;
                            }

                            try
                            {
                                ctx.AddObjectToAsset(texture.name, texture);
                            }
                            catch (Exception ex)
                            {
                                Debug.LogException(ex);
                            }
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

                        Dictionary<SourceAssetIdentifier, BlendShapeConfiguration> externals
                            = GetExternalUnityObjects<BlendShapeConfiguration>();

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

                            try
                            {
                                blendShapeConfiguration.name = blendShapeConfiguration.Kind + "BlendShapeConfiguration";

                                ctx.AddObjectToAsset(blendShapeConfiguration.name, blendShapeConfiguration);
                            }
                            catch (Exception ex)
                            {
                                Debug.LogException(ex);
                            }
                        }
                    }

                    // Root
                    if (modelAsset.Root != null)
                    {
                        try
                        {
                            ctx.AddObjectToAsset(modelAsset.Root.name, modelAsset.Root);
                            ctx.SetMainObject(modelAsset.Root);
                        }
                        catch (Exception ex)
                        {
                            Debug.LogException(ex);
                        }
                    }
                }

                stopwatch.Stop();

                Debug.LogFormat("Import VGO file.\nGameObject: {0}, input: {1}, {2:#,0}ms", modelAsset.Root?.name, ctx.assetPath, stopwatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Load 3D model.
        /// </summary>
        /// <param name="vgoFilePath">The file path of the vgo.</param>
        /// <returns>A vgo model asset.</returns>
        protected virtual VgoModelAsset LoadModel(in string vgoFilePath)
        {
            string? vgkFilePath = FindVgkFilePath(vgoFilePath);

            VgoModelAsset vgoModelAsset = _VgoImporter.Load(vgoFilePath, vgkFilePath);

            return vgoModelAsset;
        }

        /// <summary>
        /// Load 3D model.
        /// </summary>
        /// <param name="vgoFilePath">The file path of the vgo.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A vgo model asset.</returns>
        protected virtual async Task<VgoModelAsset> LoadModelAsync(string vgoFilePath, CancellationToken cancellationToken)
        {
            string? vgkFilePath = FindVgkFilePath(vgoFilePath);

            VgoModelAsset vgoModelAsset;

            if (vgkFilePath is null || vgkFilePath == string.Empty)
            {
                vgoModelAsset = await _VgoImporter.LoadAsync(vgoFilePath, cancellationToken);
            }
            else
            {
                vgoModelAsset = await _VgoImporter.LoadAsync(vgoFilePath, vgkFilePath, cancellationToken);
            }

            return vgoModelAsset;
        }

        /// <summary>
        /// Extract 3D model.
        /// </summary>
        /// <param name="vgoFilePath">The file path of the vgo.</param>
        /// <returns>A vgo model asset.</returns>
        protected virtual VgoModelAsset ExtractModel(in string vgoFilePath)
        {
            string? vgkFilePath = FindVgkFilePath(vgoFilePath);

            VgoModelAsset vgoModelAsset = _VgoImporter.Extract(vgoFilePath, vgkFilePath);

            return vgoModelAsset;
        }

        /// <summary>
        /// Extract 3D model.
        /// </summary>
        /// <param name="vgoFilePath">The file path of the vgo.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A vgo model asset.</returns>
        protected virtual async Task<VgoModelAsset> ExtractModelAsync(string vgoFilePath, CancellationToken cancellationToken)
        {
            string? vgkFilePath = FindVgkFilePath(vgoFilePath);

            VgoModelAsset vgoModelAsset;

            vgoModelAsset = await _VgoImporter.ExtractAsync(vgoFilePath, vgkFilePath, cancellationToken);

            return vgoModelAsset;
        }

        /// <summary>
        /// Find the file path of the vgk.
        /// </summary>
        /// <param name="vgoFilePath">The file path of the vgo.</param>
        /// <returns>The file path of the vgk.</returns>
        protected virtual string? FindVgkFilePath(in string vgoFilePath)
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

        #endregion

        #region Public Methods (for Inspector)

        /// <summary>
        /// Extract animation clips.
        /// </summary>
        public virtual void ExtractAnimationClips()
        {
            ExtractAssets<AnimationClip>(AnimationDirectoryName, ".anim");

            Dictionary<SourceAssetIdentifier, AnimationClip> externalObjects = GetExternalUnityObjects<AnimationClip>();

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

            Dictionary<SourceAssetIdentifier, Avatar> externalObjects = GetExternalUnityObjects<Avatar>();

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

            Dictionary<SourceAssetIdentifier, BlendShapeConfiguration> externalObjects = GetExternalUnityObjects<BlendShapeConfiguration>();

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
            ExtractTextures(TextureDirectoryName, continueMaterial: true);

            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
        }

        /// <summary>
        /// Extract textures.
        /// </summary>
        public virtual void ExtractTextures()
        {
            ExtractTextures(TextureDirectoryName);

            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
        }

        /// <summary>
        /// Extract textures.
        /// </summary>
        /// <param name="dirName"></param>
        /// <param name="continueMaterial"></param>
        public virtual void ExtractTextures(in string dirName, bool continueMaterial = false)
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
            using (VgoModelAsset modelAsset = ExtractModel(assetPath))
            {
                if (modelAsset.Layout != null &&
                    modelAsset.Layout.textures != null &&
                    modelAsset.Layout.textures.Any() &&
                    modelAsset.TextureList != null &&
                    modelAsset.TextureList.Any())
                {
                    for (int textureIndex = 0; textureIndex < modelAsset.Layout.textures.Count; textureIndex++)
                    {
                        VgoTexture? vgoTexture = modelAsset.Layout.textures[textureIndex];

                        if (vgoTexture == null)
                        {
                            continue;
                        }

                        Texture? texture = modelAsset.TextureList[textureIndex];

                        if (!(texture is Texture2D texture2d))
                        {
                            continue;
                        }

                        string fileExt;

                        byte[] imageBytes;

                        if (vgoTexture.mimeType == MimeType.Image_Png || _ExtractTexturePngForce)
                        {
                            fileExt = ".png";

                            byte[]? pngBytes = ImageConversion.EncodeToPNG(texture2d);

                            if (pngBytes == null)
                            {
                                continue;
                            }

                            imageBytes = pngBytes;
                        }
                        else if (vgoTexture.mimeType == MimeType.Image_Jpeg)
                        {
                            fileExt = ".jpg";

                            byte[]? jpgBytes = ImageConversion.EncodeToJPG(texture2d, quality: 100);

                            if (jpgBytes == null)
                            {
                                continue;
                            }

                            imageBytes = jpgBytes;
                        }
                        else if (vgoTexture.mimeType == MimeType.Image_WebP)
                        {
                            fileExt = ".webp";

                            byte[]? pngBytes = ImageConversion.EncodeToPNG(texture2d);

                            if (pngBytes == null)
                            {
                                continue;
                            }

                            try
                            {
                                string fileName = vgoTexture.name is null || vgoTexture.name == string.Empty
                                    ? string.Format("Image_{0:000}", textureIndex)
                                    : vgoTexture.name;

                                // @heavy
                                ImageConverter.SaveAsWebp(pngBytes, ImageType.PNG, path, fileName);

                                string targetPath = Path.Combine(path, fileName + fileExt);

                                AssetDatabase.ImportAsset(targetPath);

                                targetPaths.Add(vgoTexture, targetPath);

                                continue;
                            }
                            catch (Exception ex)
                            {
                                Debug.LogException(ex);

                                continue;
                            }
                        }
                        else
                        {
                            continue;
                        }

                        {
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
        public virtual void ClearExternalObjects()
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
        public virtual void ClearExternalObjects<T>() where T : UnityEngine.Object
        {
            foreach (var extarnalObject in GetExternalUnityObjects<T>())
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
        protected virtual void ExtractAssets<T>(in string dirName, in string extension) where T : UnityEngine.Object
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
        protected virtual void ExtractFromAsset(in UnityEngine.Object subAsset, in string destinationPath, in bool isForceUpdate)
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
        public virtual Dictionary<SourceAssetIdentifier, T> GetExternalUnityObjects<T>() where T : UnityEngine.Object
        {
            return GetExternalObjectMap()
                .Where(x => x.Key.type == typeof(T))
                .ToDictionary(x => x.Key, x => (T)x.Value);
        }

        /// <summary>
        /// Set a external unity object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual void SetExternalUnityObject<T>(in SourceAssetIdentifier sourceAssetIdentifier, T obj) where T : UnityEngine.Object
        {
            AddRemap(sourceAssetIdentifier, obj);

            AssetDatabase.WriteImportSettingsIfDirty(assetPath);

            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Gets the sub assets.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetPath"></param>
        /// <returns></returns>
        protected virtual IEnumerable<T> GetSubAssets<T>(in string assetPath) where T : UnityEngine.Object
        {
            UnityEngine.Object[] allAssets = AssetDatabase.LoadAllAssetsAtPath(assetPath);

            return allAssets
                .Where(x => x != null)
                .Where(x => AssetDatabase.IsSubAsset(x))
                .Where(x => x is T)
                .Select(x => (T)x);
        }

        /// <summary>
        /// Create a directory if it does not exist.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        protected virtual DirectoryInfo CreateDirectoryIfNotExists(in string path)
        {
            var directoryInfo = new DirectoryInfo(path);

            if (directoryInfo.Exists == false)
            {
                directoryInfo.Create();
            }

            return directoryInfo;
        }

        #endregion
    }
}
#endif
