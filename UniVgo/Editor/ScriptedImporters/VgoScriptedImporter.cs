﻿// ----------------------------------------------------------------------
// @Namespace : UniVgo.Editor
// @Class     : VgoScriptedImporter
// ----------------------------------------------------------------------
#if !VGO_1_DISABLE_SCRIPTED_IMPORTER
namespace UniVgo.Editor
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using UnityEditor;
    using UnityEditor.Experimental.AssetImporters;
    using UnityEngine;

    /// <summary>
    /// VGO Scripted Importer
    /// </summary>
#if VGO_FILE_EXTENSION_1
    [ScriptedImporter(1, "vgo1")]
#else
    [ScriptedImporter(1, "vgo")]
#endif
    public class VgoScriptedImporter : ScriptedImporter
    {
        /// <summary>The blend shape directory name.</summary>
        protected const string BlendShapeDirName = "BlendShapes";

        /// <summary>The material directory name.</summary>
        protected const string MaterialDirectoryName = "Materials";

        /// <summary>The texture directory name.</summary>
        protected const string TextureDirectoryName = "Textures";

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
                    // Texture
                    Dictionary<string, Texture2D> externalTextures = GetExternalUnityObjects<Texture2D>();

                    foreach (Texture2D texture in modelAsset.Texture2dList)
                    {
                        if (texture == null)
                        {
                            continue;
                        }

                        if (externalTextures.ContainsValue(texture) == false)
                        {
                            ctx.AddObjectToAsset(texture.name, texture);
                        }
                    }

                    // Material
                    Dictionary<string, Material> externalMaterials = GetExternalUnityObjects<Material>();

                    foreach (Material material in modelAsset.MaterialList)
                    {
                        if (material == null)
                        {
                            continue;
                        }

                        if (externalMaterials.ContainsValue(material) == false)
                        {
                            ctx.AddObjectToAsset(material.name, material);
                        }
                    }

                    // Mesh
                    foreach (MeshAsset meshAsset in modelAsset.MeshAssetList)
                    {
                        ctx.AddObjectToAsset(meshAsset.Mesh.name, meshAsset.Mesh);
                    }

                    // ScriptableObject

                    // avatar
                    if (modelAsset.Avatar != null)
                    {
                        ctx.AddObjectToAsset("avatar", modelAsset.Avatar);
                    }

                    // BlendShape
                    {
                        var external = GetExternalUnityObjects<BlendShapeConfiguration>().FirstOrDefault();
                        if (external.Value != null)
                        {
                            //
                        }
                        else
                        {
                            if (modelAsset.ScriptableObjectList != null)
                            {
                                var blendShapeConfigurationList = modelAsset.ScriptableObjectList
                                    .Where(x => x != null)
                                    .Where(x => x.GetType() == typeof(BlendShapeConfiguration))
                                    .Select(x => x as BlendShapeConfiguration)
                                    .ToList();

                                if (blendShapeConfigurationList != null)
                                {
                                    foreach (BlendShapeConfiguration blendShapeConfiguration in blendShapeConfigurationList)
                                    {
                                        blendShapeConfiguration.name = blendShapeConfiguration.kind + "BlendShapeConfiguration";
                                        ctx.AddObjectToAsset(blendShapeConfiguration.name, blendShapeConfiguration);
                                    }
                                }
                            }
                        }
                    }

                    // Root
                    ctx.AddObjectToAsset(modelAsset.Root.name, modelAsset.Root);
                    ctx.SetMainObject(modelAsset.Root);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
        }

        /// <summary>
        /// Load 3D model.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>A model asset.</returns>
        protected virtual ModelAsset LoadModel(string filePath)
        {
            var importer = new VgoImporter();

            ModelAsset modelAsset = importer.Load(filePath);

            return modelAsset;
        }

        /// <summary>
        /// Extract 3D model.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>A model asset.</returns>
        protected virtual ModelAsset ExtractModel(string filePath)
        {
            var importer = new VgoImporter();

            ModelAsset modelAsset = importer.Extract(filePath);

            return modelAsset;
        }

        public virtual void ExtractTexturesAndMaterials()
        {
            ExtractTextures(TextureDirectoryName, (path) => { return ExtractModel(path); }, continueMaterial: true);
            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
        }

        public virtual void ExtractTextures()
        {
            ExtractTextures(TextureDirectoryName, (path) => { return ExtractModel(path); });
            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
        }

        public virtual void ExtractMaterials()
        {
            ExtractAssets<Material>(MaterialDirectoryName, ".mat");
            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
        }

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

            SafeCreateDirectory(path);

            Dictionary<TextureInfo, string> targetPaths = new Dictionary<TextureInfo, string>();

            // Reload Model
            using (ModelAsset modelAsset = ExtractModel(assetPath))
            {
                for (int textureIndex = 0; textureIndex < modelAsset.TextureInfoList.Count; textureIndex++)
                {
                    TextureInfo textureInfo = modelAsset.TextureInfoList[textureIndex];

                    Texture2D texture2d = modelAsset.Texture2dList[textureIndex];

                    string fileExt;

                    byte[] imageBytes;

                    if (textureInfo.source.mimeType == "image/jpeg")
                    {
                        fileExt = ".jpg";
                        imageBytes = texture2d.EncodeToJPG();
                    }
                    else if (textureInfo.source.mimeType == "image/png")
                    {
                        fileExt = ".png";
                        imageBytes = texture2d.EncodeToPNG();
                    }
                    else
                    {
                        continue;
                    }

                    string fileName =
                        !string.IsNullOrEmpty(textureInfo.source.name) ? textureInfo.source.name :
                        !string.IsNullOrEmpty(textureInfo.name) ? textureInfo.name :
                        string.Format("Image_{0:000}", textureIndex);

                    string targetPath = Path.Combine(path, fileName + fileExt);

                    File.WriteAllBytes(targetPath, imageBytes);

                    AssetDatabase.ImportAsset(targetPath);

                    targetPaths.Add(textureInfo, targetPath);
                }
            }

            EditorApplication.delayCall += () =>
            {
                foreach (var targetPath in targetPaths)
                {
                    TextureInfo textureInfo = targetPath.Key;

                    TextureImporter targetTextureImporter = AssetImporter.GetAtPath(targetPath.Value) as TextureImporter;

                    targetTextureImporter.sRGBTexture = (textureInfo.colorSpace == ColorSpaceType.Srgb);

                    if (textureInfo.textureType == TextureType.NormalMap)
                    {
                        targetTextureImporter.textureType = TextureImporterType.NormalMap;
                    }

                    targetTextureImporter.SaveAndReimport();

                    UnityEngine.Object externalObject = AssetDatabase.LoadAssetAtPath(targetPath.Value, typeof(Texture2D));

                    AddRemap(new AssetImporter.SourceAssetIdentifier(typeof(Texture2D), textureInfo.name), externalObject);
                }

                //AssetDatabase.WriteImportSettingsIfDirty(assetPath);
                AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);

                if (continueMaterial)
                {
                    ExtractMaterials();
                }
            };
        }

        public void ExtractBlendShapes()
        {
            ExtractAssets<BlendShapeConfiguration>(BlendShapeDirName, ".asset");

            var blendShapeConfiguration = GetExternalUnityObjects<BlendShapeConfiguration>().FirstOrDefault();

            string blendShapeConfigurationPath = AssetDatabase.GetAssetPath(blendShapeConfiguration.Value);

            if (string.IsNullOrEmpty(blendShapeConfigurationPath) == false)
            {
                EditorUtility.SetDirty(blendShapeConfiguration.Value);
                AssetDatabase.WriteImportSettingsIfDirty(blendShapeConfigurationPath);
            }

            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
        }

        public virtual void ClearExtarnalObjects()
        {
            foreach (var extarnalObject in GetExternalObjectMap())
            {
                RemoveRemap(extarnalObject.Key);
            }
            AssetDatabase.WriteImportSettingsIfDirty(assetPath);
            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
        }

        public virtual void ClearExtarnalObjects<T>() where T : UnityEngine.Object
        {
            foreach (var extarnalObject in GetExternalObjectMap().Where(x => x.Key.type == typeof(T)))
            {
                RemoveRemap(extarnalObject.Key);
            }
            AssetDatabase.WriteImportSettingsIfDirty(assetPath);
            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
        }

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

            SafeCreateDirectory(path);

            foreach (T asset in subAssets)
            {
                ExtractFromAsset(asset, string.Format("{0}/{1}{2}", path, asset.name, extension), false);
            }
        }

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

        public virtual Dictionary<string, T> GetExternalUnityObjects<T>() where T : UnityEngine.Object
        {
            return GetExternalObjectMap().Where(x => x.Key.type == typeof(T)).ToDictionary(x => x.Key.name, x => (T)x.Value);
        }

        public virtual void SetExternalUnityObject<T>(SourceAssetIdentifier sourceAssetIdentifier, T obj) where T : UnityEngine.Object
        {
            AddRemap(sourceAssetIdentifier, obj);
            AssetDatabase.WriteImportSettingsIfDirty(assetPath);
            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
        }

        protected virtual IEnumerable<T> GetSubAssets<T>(string assetPath) where T : UnityEngine.Object
        {
            return AssetDatabase
                .LoadAllAssetsAtPath(assetPath)
                .Where(x => AssetDatabase.IsSubAsset(x))
                .Where(x => x is T)
                .Select(x => x as T);
        }

        protected virtual DirectoryInfo SafeCreateDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                return null;
            }
            return Directory.CreateDirectory(path);
        }
    }
}
#endif
