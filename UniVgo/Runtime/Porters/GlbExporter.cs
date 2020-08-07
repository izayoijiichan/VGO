// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : GlbExporter
// ----------------------------------------------------------------------
namespace UniVgo
{
    using NewtonGltf;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UniVgo.Converters;
    using UniVgo.Porters;
    using VgoGltf;
    using VgoGltf.Buffers;

    #region Delegates

    /// <summary>The delegate to ExportTexture method.</summary>
    public delegate int ExportTextureDelegate(Material material, Texture srcTexture, string propertyName, TextureType textureType = TextureType.Default, ColorSpaceType colorSpace = ColorSpaceType.Srgb, float metallicSmoothness = -1.0f);

    #endregion

    /// <summary>
    /// GLB Exporter
    /// </summary>
    public class GlbExporter : IDisposable
    {
        #region Constructors

        /// <summary>
        /// Create a new instance of GlbExporter.
        /// </summary>
        public GlbExporter()
        {
            Initialize();
        }

        //~GlbExporter()
        //{
        //    Dispose(disposing: false);
        //}

        #endregion

        #region Fields

        /// <summary>Whether this instance has been disposed.</summary>
        protected bool disposedValue = false;

        /// <summary>The model asset.</summary>
        protected ModelAsset ModelAsset = new ModelAsset();

        /// <summary>List of node.</summary>
        protected List<Transform> Nodes = null;

        /// <summary>The material exporter.</summary>
        protected IMaterialExporter _MaterialExporter = null;

        /// <summary>The texture converter.</summary>
        protected TextureConverter _TextureConverter = new TextureConverter();

        /// <summary>List of export texture item.</summary>
        protected List<ExportTextureItem> _ExportTextureItemList = null;

        /// <summary>The index of texture buffer.</summary>
        protected int _TextureBufferIndex = 0;

        #endregion

        #region Properties

        /// <summary>glTF storage adapter</summary>
        public GltfStorageAdapter GltfStorageAdapter { get; protected set; }

        /// <summary>glTF</summary>
        protected Gltf Gltf => GltfStorageAdapter.Gltf;

        #endregion

        #region Delegates

        /// <summary>The delegate to ExportTexture method.</summary>
        protected ExportTextureDelegate _ExporterTexture;

        #endregion

        #region Methods (Initialize)

        /// <summary>
        /// Initialize this instance.
        /// </summary>
        public virtual void Initialize()
        {
            if (_ExporterTexture == null)
            {
                _ExporterTexture = new ExportTextureDelegate(ExportTexture);
            }

            _MaterialExporter = new VgoMaterialExporter()
            {
                MaterialPorterStore = new MaterialPorterStore(),
                ExportTexture = ExportTexture
            };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Create a glTF storage.
        /// </summary>
        /// <param name="root">The GameObject of root.</param>
        /// <returns>A glTF storage.</returns>
        public virtual GltfStorage CreateGltfStorage(GameObject root)
        {
            GltfStorage gltfStorage = new GltfStorage(new Gltf());

            GltfStorageAdapter = new GltfStorageAdapter(gltfStorage);

            int initialBufferSize = 50 * 1024 * 1024;

            IByteBuffer binBuffer = new ArraySegmentByteBuffer(initialBufferSize);

            int bufferIndex = GltfStorageAdapter.AddBuffer(binBuffer);

            GameObject copy = UnityEngine.Object.Instantiate(root);

            copy.transform.ReverseZRecursive();

            GameObject go = copy;

            GameObject tmpParent;
            if (go.transform.childCount == 0)
            {
                tmpParent = new GameObject("tmpParent");
                go.transform.SetParent(tmpParent.transform, true);
                go = tmpParent;
            }

            ModelAsset.Root = go;  // @notice Copy

            Nodes = ModelAsset.Root.transform
                .Traverse()
                .Skip(1)
                .ToList();

            // Prepare
            ModelAsset.RendererList = CreateUnityRendererList();
            ModelAsset.MaterialList = CreateUnityMaterialList();
            ModelAsset.SkinList = CreateUnitySkinList();
            CreateMeshAndMeshAssetList();

            // Materials & Textures
            CreateGltfMaterialsAndExportTextures(bufferIndex);

            CreateGltfMeshes(bufferIndex);
            CreateGltfSkins(bufferIndex);
            CreateGltfNodes();
            CreateGltfScenes();

            SetGltfRootExtensions();

            SetExtensionsUsedFromGltfRoot();
            SetExtensionsUsedFromNode();
            SetExtensionsUsedFromMaterial();
            SetExtensionsUsedFromTexture();

            SetGltfAsset();

            //GameObject.DestroyImmediate(Copy);

            return gltfStorage;
        }

        #endregion

        #region Prepare

        /// <summary>
        /// Create list of unity renderer.
        /// </summary>
        /// <returns>list of unity renderer.</returns>
        protected virtual List<Renderer> CreateUnityRendererList()
        {
            if (Nodes == null)
            {
                throw new Exception();
            }

            var rendererList = new List<Renderer>();

            for (int index = 0; index < Nodes.Count; index++)
            {
                GameObject node = Nodes[index].gameObject;

                if (node.TryGetComponentEx(out Renderer renderer))
                {
                    rendererList.Add(renderer);
                }
            }

            return rendererList;
        }

        /// <summary>
        /// Create list of unity material.
        /// </summary>
        /// <returns>list of unity material.</returns>
        protected virtual List<Material> CreateUnityMaterialList()
        {
            if (ModelAsset.RendererList == null)
            {
                throw new Exception();
            }

            var materialList = new List<Material>();

            foreach (Renderer renderer in ModelAsset.RendererList)
            {
                Material[] sharedMaterials = renderer.sharedMaterials;

                for (int materialIndex = 0; materialIndex < sharedMaterials.Length; materialIndex++)
                {
                    Material material = sharedMaterials[materialIndex];

                    if (material == null)
                    {
                        continue;
                    }

                    if (materialList.Where(m => m.GetInstanceID() == material.GetInstanceID()).FirstOrDefault() != null)
                    {
                        continue;
                    }

                    materialList.Add(material);
                }
            }

            return materialList;
        }

        #endregion

        #region gltf.asset

        /// <summary>
        /// Set a gltf.asset.
        /// </summary>
        protected virtual void SetGltfAsset()
        {
            Gltf.asset = new GltfAsset
            {
                copyright = Vgo.Generator,
                generator = Vgo.Generator,
                version = "2.0",  // glTF version
                minVersion = null,
            };
        }

        #endregion

        #region gltf.materials

        /// <summary>
        /// Create gltf.materials and export textures.
        /// </summary>
        /// <param name="bufferIndex">The index of buffer for textures.</param>
        protected virtual void CreateGltfMaterialsAndExportTextures(int bufferIndex)
        {
            if (ModelAsset.MaterialList == null)
            {
                throw new Exception();
            }

            if (ModelAsset.MaterialList.Count == 0)
            {
                return;
            }

            _ExportTextureItemList = new List<ExportTextureItem>();

            _TextureBufferIndex = bufferIndex;

            for (int materialIndex = 0; materialIndex < ModelAsset.MaterialList.Count; materialIndex++)
            {
                Material material = ModelAsset.MaterialList[materialIndex];

                GltfMaterial gltfMaterial = _MaterialExporter.CreateGltfMaterial(material);

                Gltf.materials.Add(gltfMaterial);
            }
        }

        #endregion

        #region gltf.textures

        /// <summary>
        /// Export a texture to glTF storage.
        /// </summary>
        /// <param name="material">A unity material.</param>
        /// <param name="texture">A unity texture.</param>
        /// <param name="propertyName">The texture property name of material.</param>
        /// <param name="textureType">The type of texture.</param>
        /// <param name="colorSpace">The color space of image.</param>
        /// <param name="metallicRoughness">The metallic-roughness value.</param>
        /// <returns>The index of gltf.texture.</returns>
        /// <remarks>
        /// gltf.bufferViews
        /// gltf.textures
        /// gltf.images
        /// gltf.samplers
        /// </remarks>
        protected virtual int ExportTexture(Material material, Texture texture, string propertyName = null, TextureType textureType = TextureType.Default, ColorSpaceType colorSpace = ColorSpaceType.Srgb, float metallicRoughness = -1.0f)
        {
            if (texture is null)
            {
                return -1;
            }

            var srcTexture2d = texture as Texture2D;

            if (srcTexture2d is null)
            {
                return -1;
            }

            int srcTextureInstanceId = srcTexture2d.GetInstanceID();

            ExportTextureItem textureItem = _ExportTextureItemList
                .Where(item => item.instanceId == srcTextureInstanceId)
                .FirstOrDefault();

            if (textureItem != null)
            {
                return textureItem.textureIndex;
            }

            float metallicSmoothness = (metallicRoughness == -1.0f) ? -1.0f : (1.0f - metallicRoughness);

            Texture2D convertedTexture2d = _TextureConverter.GetExportTexture(srcTexture2d, textureType, colorSpace, metallicSmoothness);

            textureItem = new ExportTextureItem
            {
                instanceId = srcTextureInstanceId,
                name = texture.name,
                texture2dName = srcTexture2d.name,
                textureType = textureType,
                colorSpace = colorSpace,
                imageName = convertedTexture2d.name,
                uri = null,
                mimeType = "image/png",
                imageBytes = convertedTexture2d.EncodeToPNG(),
                magFilter = convertedTexture2d.filterMode.ToGltfMagMode(),
                minFilter = convertedTexture2d.filterMode.ToGltfMinMode(),
                wrapS = convertedTexture2d.wrapMode.ToGltfMode(),
                wrapT = convertedTexture2d.wrapMode.ToGltfMode(),
            };

            if ((material != null) && string.IsNullOrEmpty(propertyName) == false)
            {
                Vector2 textureOffset = material.GetTextureOffset(propertyName);
                Vector2 textureScale = material.GetTextureScale(propertyName);

                if (textureOffset != Vector2.zero)
                {
                    textureItem.offset = textureOffset.ToNumericsVector2();
                }
                if (textureScale != Vector2.one)
                {
                    textureItem.scale = textureScale.ToNumericsVector2();
                }
            }

            // @notice
            // textureItem.texcoord

            int textureIndex = GltfStorageAdapter.AddTexture(_TextureBufferIndex, textureItem);

            textureItem.textureIndex = textureIndex;

            _ExportTextureItemList.Add(textureItem);

            return textureIndex;
        }

        #endregion

        #region gltf.meshes

        /// <summary>
        /// Create list of Mesh and list of MeshAsset.
        /// </summary>
        protected virtual void CreateMeshAndMeshAssetList()
        {
            if (ModelAsset.RendererList == null)
            {
                throw new Exception();
            }

            ModelAsset.MeshList = new List<Mesh>();

            ModelAsset.MeshAssetList = new List<MeshAsset>();

            foreach (Renderer renderer in ModelAsset.RendererList)
            {
                Mesh mesh = null;

                if (renderer is SkinnedMeshRenderer skinnedMeshRenderer)
                {
                    if (skinnedMeshRenderer.sharedMesh is null)
                    {
                        continue;
                    }

                    mesh = ModelAsset.MeshList.Where(m => m.GetInstanceID() == skinnedMeshRenderer.sharedMesh.GetInstanceID()).FirstOrDefault();

                    if (mesh == null)
                    {
                        mesh = skinnedMeshRenderer.sharedMesh;

                        ModelAsset.MeshList.Add(mesh);
                    }
                }
                else if (renderer is MeshRenderer meshRenderer)
                {
                    if (renderer.gameObject.TryGetComponentEx(out MeshFilter meshFilter) == false)
                    {
                        continue;
                    }

                    if (meshFilter.sharedMesh is null)
                    {
                        continue;
                    }

                    mesh = ModelAsset.MeshList.Where(m => m.GetInstanceID() == meshFilter.sharedMesh.GetInstanceID()).FirstOrDefault();

                    if (mesh == null)
                    {
                        mesh = meshFilter.sharedMesh;

                        ModelAsset.MeshList.Add(mesh);
                    }
                }

                if (mesh == null)
                {
                    continue;
                }

                //if (renderer.sharedMaterial == null)
                //{
                //    continue;
                //}

                MeshAsset meshAsset = new MeshAsset
                {
                    Mesh = mesh,
                    Renderer = renderer,
                };

                ModelAsset.MeshAssetList.Add(meshAsset);
            }
        }

        /// <summary>
        /// Create gltf.meshes.
        /// </summary>
        /// <param name="bufferIndex">The index of buffer for mesh accessor.</param>
        protected virtual void CreateGltfMeshes(int bufferIndex)
        {
            if (ModelAsset.MeshAssetList == null)
            {
                throw new Exception();
            }

            if (ModelAsset.MaterialList == null)
            {
                throw new Exception();
            }

            GltfMeshExporter meshExporter = new GltfMeshExporter();

            meshExporter.GltfStorageAdapter = GltfStorageAdapter;

            meshExporter.ExportMeshes(ModelAsset.MeshAssetList, ModelAsset.MaterialList, bufferIndex);
        }

        #endregion

        #region gltf.skins

        /// <summary>
        /// Create skin list.
        /// </summary>
        /// <returns>List of skin.</returns>
        protected virtual List<SkinnedMeshRenderer> CreateUnitySkinList()
        {
            if (ModelAsset.RendererList == null)
            {
                throw new Exception();
            }

            var skinList = new List<SkinnedMeshRenderer>();

            foreach (Renderer renderer in ModelAsset.RendererList)
            {
                if (renderer is SkinnedMeshRenderer skinnedMeshRenderer)
                {
                    if (skinnedMeshRenderer.bones == null)
                    {
                        continue;
                    }

                    if (skinnedMeshRenderer.bones.Length == 0)
                    {
                        continue;
                    }

                    skinList.Add(skinnedMeshRenderer);
                }
            }

            return skinList;
        }

        /// <summary>
        /// Create gltf.skins.
        /// </summary>
        /// <param name="bufferIndex">The index of buffer for inverseBindMatrices accessor.</param>
        protected virtual void CreateGltfSkins(int bufferIndex)
        {
            if (ModelAsset.RendererList == null)
            {
                throw new Exception();
            }
            var skinList = new List<SkinnedMeshRenderer>();

            foreach (Renderer renderer in ModelAsset.RendererList)
            {
                if (renderer is SkinnedMeshRenderer skinnedMeshRenderer)
                {
                    if (skinnedMeshRenderer.bones == null)
                    {
                        continue;
                    }

                    if (skinnedMeshRenderer.bones.Length == 0)
                    {
                        continue;
                    }

                    ModelAsset.SkinList.Add(skinnedMeshRenderer);
                }
            }

            foreach (SkinnedMeshRenderer renderer in ModelAsset.SkinList)
            {
                Matrix4x4[] matrices = renderer.sharedMesh.bindposes.Select(y => y.ReverseZ()).ToArray();

                int accessorIndex = GltfStorageAdapter.AddAccessorWithoutSparse(bufferIndex, matrices, GltfBufferViewTarget.ARRAY_BUFFER);

                var gltfSkin = new GltfSkin
                {
                    inverseBindMatrices = accessorIndex,
                    joints = renderer.bones.Select(t => Nodes.IndexOf(t)).ToArray(),
                    skeleton = Nodes.IndexOf(renderer.rootBone),
                };

                Gltf.skins.Add(gltfSkin);
            }
        }

        #endregion

        #region gltf.nodes

        /// <summary>
        /// Create gltf.nodes.
        /// </summary>
        protected virtual void CreateGltfNodes()
        {
            if (Nodes == null)
            {
                throw new Exception();
            }

            if (ModelAsset.MeshAssetList == null)
            {
                throw new Exception();
            }

            if (ModelAsset.SkinList == null)
            {
                throw new Exception();
            }

            List<Renderer> renderers = ModelAsset.MeshAssetList.Select(y => y.Renderer).ToList();

            for (int index = 0; index < Nodes.Count; index++)
            {
                Transform node = Nodes[index];

                GltfNode gltfNode = CreateGltfNode(node.gameObject, Nodes, renderers, ModelAsset.SkinList);

                Gltf.nodes.Add(gltfNode);
            }
        }

        /// <summary>
        /// Create a gltf.node.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <param name="nodes">List of node.</param>
        /// <param name="renderers">List of renderer.</param>
        /// <param name="skins">List of skin.</param>
        /// <returns>A gltf.node.</returns>
        protected virtual GltfNode CreateGltfNode(GameObject gameObject, List<Transform> nodes, List<Renderer> renderers, List<SkinnedMeshRenderer> skins)
        {
            GltfNode gltfNode = new GltfNode
            {
                name = gameObject.name,
                children = gameObject.transform.GetChildren().Select(y => nodes.IndexOf(y)).ToArray(),
                rotation = gameObject.transform.localRotation.ToNumericsVector4(),
                translation = gameObject.transform.localPosition.ToNumericsVector3(),
                scale = gameObject.transform.localScale.ToNumericsVector3(),
            };

            if (gltfNode.children.Any() == false)
            {
                gltfNode.children = null;
            }

            if (gameObject.TryGetComponentEx(out MeshRenderer meshRenderer))
            {
                gltfNode.mesh = renderers.IndexOf(meshRenderer);
            }

            if (gameObject.TryGetComponentEx(out SkinnedMeshRenderer skinnedMeshRenderer))
            {
                gltfNode.mesh = renderers.IndexOf(skinnedMeshRenderer);
                gltfNode.skin = skins.IndexOf(skinnedMeshRenderer);
            }

            SetGltfNodeExtensions(gltfNode, gameObject);

            return gltfNode;
        }

        /// <summary>
        /// Set the extension of the glTF node.
        /// </summary>
        /// <param name="gltfNode">A gltf.node.</param>
        /// <param name="srcNode">A game object.</param>
        protected virtual void SetGltfNodeExtensions(GltfNode gltfNode, GameObject srcNode)
        {
            return;
        }

        #endregion

        #region gltf.scenes

        /// <summary>
        /// Create gltf.scenes.
        /// </summary>
        protected virtual void CreateGltfScenes()
        {
            var gltfScene = new GltfScene
            {
                nodes = ModelAsset.Root.transform.GetChildren().Select(x => Nodes.IndexOf(x)).ToArray(),
            };

            Gltf.scenes.Add(gltfScene);
        }

        #endregion

        #region gltf.extensions

        /// <summary>
        /// Set the extensions of the glTF root.
        /// </summary>
        protected virtual void SetGltfRootExtensions()
        {
            return;
        }

        #endregion

        #region gltf.extensionsUsed

        /// <summary>
        /// Set gltf.extensionsUsed from gltf.extensions.
        /// </summary>
        protected virtual void SetExtensionsUsedFromGltfRoot()
        {
            return;
        }

        /// <summary>
        /// Set gltf.extensionsUsed from gltf.nodes.extensions.
        /// </summary>
        protected virtual void SetExtensionsUsedFromNode()
        {
            return;
        }

        /// <summary>
        /// Set gltf.extensionsUsed from gltf.materials.extensions.
        /// </summary>
        protected virtual void SetExtensionsUsedFromMaterial()
        {
            List<GltfMaterial> hasExtensionMaterialList = Gltf.materials.Where(m => (m.extensions != null)).ToList();

            if (hasExtensionMaterialList.Where(m => m.extensions.Contains(VGO_materials.ExtensionName)).Any())
            {
                if (Gltf.extensionsUsed.Contains(VGO_materials.ExtensionName) == false)
                {
                    Gltf.extensionsUsed.Add(VGO_materials.ExtensionName);
                }
            }

            if (hasExtensionMaterialList.Where(m => m.extensions.Contains(VGO_materials_particle.ExtensionName)).Any())
            {
                if (Gltf.extensionsUsed.Contains(VGO_materials_particle.ExtensionName) == false)
                {
                    Gltf.extensionsUsed.Add(VGO_materials_particle.ExtensionName);
                }
            }

            if (hasExtensionMaterialList.Where(m => m.extensions.Contains(VGO_materials_skybox.ExtensionName)).Any())
            {
                if (Gltf.extensionsUsed.Contains(VGO_materials_skybox.ExtensionName) == false)
                {
                    Gltf.extensionsUsed.Add(VGO_materials_skybox.ExtensionName);
                }
            }

            if (hasExtensionMaterialList.Where(m => m.extensions.Contains(VRMC_materials_mtoon.ExtensionName)).Any())
            {
                if (Gltf.extensionsUsed.Contains(VRMC_materials_mtoon.ExtensionName) == false)
                {
                    Gltf.extensionsUsed.Add(VRMC_materials_mtoon.ExtensionName);
                }
            }

            if (hasExtensionMaterialList.Where(m => m.extensions.Contains(KHR_materials_unlit.ExtensionName)).Any())
            {
                if (Gltf.extensionsUsed.Contains(KHR_materials_unlit.ExtensionName) == false)
                {
                    Gltf.extensionsUsed.Add(KHR_materials_unlit.ExtensionName);
                }
            }
        }

        /// <summary>
        /// Set gltf.extensionsUsed from gltf.textures.extensions.
        /// </summary>
        /// <remarks>
        /// It just changes the order.
        /// </remarks>
        protected virtual void SetExtensionsUsedFromTexture()
        {
            if (Gltf.extensionsUsed.Contains(KHR_texture_transform.ExtensionName))
            {
                Gltf.extensionsUsed.Remove(KHR_texture_transform.ExtensionName);
                Gltf.extensionsUsed.Add(KHR_texture_transform.ExtensionName);
            }
        }

        #endregion

        #region Methods (Dispose)

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposedValue == false)
            {
                if (disposing)
                {
                    ModelAsset.Dispose();
                }

                foreach (ExportTextureItem exportTextureItem in _ExportTextureItemList)
                {
                    exportTextureItem.imageBytes = null;
                }

                _ExportTextureItemList = null;

                disposedValue = true;
            }
        }

        #endregion
    }
}
