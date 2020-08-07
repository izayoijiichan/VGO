// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : GlbImporter
// ----------------------------------------------------------------------
namespace UniVgo
{
    using NewtonGltf;
    using NewtonGltf.Serialization;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UniVgo.Converters;
    using UniVgo.Porters;
    using VgoGltf;

    /// <summary>
    /// GLB Importer
    /// </summary>
    public class GlbImporter
    {
        #region Constructors

        /// <summary>
        /// Create a new instance of GlbImporter.
        /// </summary>
        public GlbImporter()
        {
            Initialize();
        }

        #endregion

        #region Fields

        /// <summary>glTF storage</summary>
        protected GltfStorage _GltfStorage;

        /// <summary>glTF storage adapter</summary>
        protected GltfStorageAdapter _GltfStorageAdapter;

        /// <summary>The model asset.</summary>
        protected ModelAsset ModelAsset = new ModelAsset();

        /// <summary>List of node.</summary>
        protected List<Transform> Nodes = null;

        /// <summary>The material importer.</summary>
        protected IMaterialImporter _MaterialImporter = null;

        /// <summary>The texture converter.</summary>
        protected TextureConverter _TextureConverter = new TextureConverter();

        /// <summary>The JSON serializer settings.</summary>
        protected VgoJsonSerializerSettings _JsonSerializerSettings = new VgoJsonSerializerSettings();

        #endregion

        #region Properties

        /// <summary>glTF</summary>
        protected Gltf Gltf => _GltfStorage.Gltf;

        /// <summary>Whether show mesh renderer.</summary>
        protected bool ShowMesh { get; set; } = true;

        /// <summary>Whether update skinned mesh renderer when off screen.</summary>
        protected bool UpdateWhenOffscreen { get; set; } = false;

        #endregion

        #region Methods (Initialize)

        /// <summary>
        /// Initialize this instance.
        /// </summary>
        public virtual void Initialize()
        {
            _MaterialImporter = new VgoMaterialImporter()
            {
                MaterialPorterStore = new MaterialPorterStore(),
                ShaderStore = new ShaderStore(),
            };
        }

        #endregion

        #region Methods (Build)

        /// <summary>
        /// Load a 3D model from the specified file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>A model asset.</returns>
        public virtual ModelAsset Load(string filePath)
        {
            var gltfStorage = new GltfStorage(filePath);

            return Load(gltfStorage);
        }

        /// <summary>
        /// Load a 3D model from the specified bytes.
        /// </summary>
        /// <param name="allBytes">All bytes of file.</param>
        /// <returns>A model asset.</returns>
        /// <remarks>.glb only</remarks>
        public virtual ModelAsset Load(byte[] allBytes)
        {
            var gltfStorage = new GltfStorage(allBytes);

            return Load(gltfStorage);
        }

        /// <summary>
        /// Load a 3D model from the specified glTF storage.
        /// </summary>
        /// <param name="gltfStorage">A glTF storage.</param>
        /// <returns>A model asset.</returns>
        public virtual ModelAsset Load(GltfStorage gltfStorage)
        {
            _GltfStorage = gltfStorage;

            _GltfStorageAdapter = new GltfStorageAdapter(gltfStorage);

            ModelAsset = new ModelAsset();

            ModelAsset.TextureInfoList = CreateTextureInfoList();
            ModelAsset.MaterialInfoList = CreateMaterialInfoList();

            UpdateTextureInfoList();

            // UnityEngine.Texture2D
            ModelAsset.Texture2dList = CreateTextureAssets();

            // UnityEngine.Material
            ModelAsset.MaterialList = CreateMaterialAssets();

            // UnityEngine.Mesh
            ModelAsset.MeshAssetList = CreateMeshAssets();

            // UnityEngine.GameObejct
            Nodes = CreateNodes();

            ModelAsset.Root = new GameObject("Root");

            CreateNodeHierarchy(ModelAsset.Root);

            FixCoordinate();

            // UnityEngine.Renderer
            AttachMeshes();

            return ModelAsset;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>A model asset.</returns>
        /// <remarks>for ScriptedImporter</remarks>
        public virtual ModelAsset Extract(string filePath)
        {
            _GltfStorage = new GltfStorage(filePath);

            _GltfStorageAdapter = new GltfStorageAdapter(_GltfStorage);

            ModelAsset = new ModelAsset();

            ModelAsset.TextureInfoList = CreateTextureInfoList();
            ModelAsset.MaterialInfoList = CreateMaterialInfoList();

            UpdateTextureInfoList();

            // UnityEngine.Texture2D
            ModelAsset.Texture2dList = CreateTextureAssets();

            // UnityEngine.Material
            ModelAsset.MaterialList = CreateMaterialAssets();

            return ModelAsset;
        }

        #endregion

        #region Texture Info

        /// <summary>
        /// Create list of texture info.
        /// </summary>
        protected virtual List<TextureInfo> CreateTextureInfoList()
        {
            var textureInfoList = new List<TextureInfo>();

            for (int textureIndex = 0; textureIndex < Gltf.textures.Count; textureIndex++)
            {
                TextureInfo textureInfo = CreateTextureInfo(textureIndex);

                textureInfoList.Add(textureInfo);
            }

            return textureInfoList;
        }

        /// <summary>
        /// Create a texture info.
        /// </summary>
        /// <param name="textureIndex">The index of gltf.texture.</param>
        /// <returns>A texture info.</returns>
        /// <remarks>
        /// gltf.textures
        /// gltf.images
        /// gltf.samplers
        /// </remarks>
        protected virtual TextureInfo CreateTextureInfo(int textureIndex)
        {
            GltfTexture texture = Gltf.textures[textureIndex];

            if (Gltf.images.TryGetValue(texture.source, out GltfImage image) == false)
            {
                throw new IndexOutOfRangeException($"gltf.textures[{textureIndex}].source: {texture.source}");
            }

            GltfTextureSampler sampler = Gltf.samplers.GetValueOrDefault(texture.sampler);

            string name;

            if (string.IsNullOrEmpty(texture.name) == false)
            {
                name = texture.name;
            }
            else if (string.IsNullOrEmpty(image.name) == false)
            {
                name = image.name;
            }
            else
            {
                name = string.Format("Texture_{0:000}", textureIndex);
            }

            TextureInfo textureInfo = new TextureInfo(textureIndex, name, image, sampler);

            return textureInfo;
        }

        /// <summary>
        /// Update list of material info.
        /// </summary>
        /// <remarks>
        /// After CreateMaterialInfoList()
        /// </remarks>
        protected virtual void UpdateTextureInfoList()
        {
            for (int textureIndex = 0; textureIndex < ModelAsset.TextureInfoList.Count; textureIndex++)
            {
                TextureInfo curTextureInfo = ModelAsset.TextureInfoList[textureIndex];

                for (int materialIndex = 0; materialIndex < ModelAsset.MaterialInfoList.Count; materialIndex++)
                {
                    MaterialInfo curMaterialInfo = ModelAsset.MaterialInfoList[materialIndex];

                    if (curMaterialInfo.TextureInfoList.Any(ti => ti == curTextureInfo))
                    {
                        curTextureInfo.MaterialInfoList.Add(curMaterialInfo);
                    }
                }
            }
        }

        #endregion

        #region Texture2D

        /// <summary>
        /// Create texture assets.
        /// </summary>
        /// <returns>List of texture2D.</returns>
        /// <remarks>
        /// After UpdateTextureInfoList()
        /// </remarks>
        protected virtual List<Texture2D> CreateTextureAssets()
        {
            var texture2dList = new List<Texture2D>();

            for (int textureIndex = 0; textureIndex < ModelAsset.TextureInfoList.Count; textureIndex++)
            {
                TextureInfo textureInfo = ModelAsset.TextureInfoList[textureIndex];

                Texture2D texture2d = CreateTexture2D(textureInfo);

                texture2dList.Add(texture2d);
            }

            return texture2dList;
        }

        /// <summary>
        /// Create a texture 2D.
        /// </summary>
        /// <param name="textureInfo">A texture info.</param>
        /// <returns>A texture 2D.</returns>
        /// <remarks>
        /// gltf.buffers
        /// gltf.bufferViews
        /// gltf.images
        /// gltf.samplers
        /// </remarks>
        protected virtual Texture2D CreateTexture2D(TextureInfo textureInfo)
        {
            var srcTexture2d = new Texture2D(width: 2, height: 2, TextureFormat.ARGB32, mipChain: false, linear: textureInfo.IsLinear)
            {
                name = textureInfo.name
            };

            byte[] imageBytes;

            if (textureInfo.source.bufferView == -1)
            {
                imageBytes = _GltfStorageAdapter.GetUriData(textureInfo.source.uri);
            }
            else
            {
                imageBytes = _GltfStorageAdapter.GetBufferViewBytes(textureInfo.source.bufferView).ToArray();
            }

            srcTexture2d.LoadImage(imageBytes);

            Texture2D texture2D = _TextureConverter.GetImportTexture(srcTexture2d, textureInfo.textureType, textureInfo.metallicRoughness);

            if (textureInfo.sampler != null)
            {
                texture2D.filterMode = textureInfo.sampler.minFilter.ToUnityMode();
                SetWrapMode(texture2D, textureInfo.sampler.wrapS, textureInfo.sampler.wrapT);
            }

            return texture2D;
        }

        /// <summary>
        /// Set texture wrap mode.
        /// </summary>
        /// <param name="texture2d">A texture 2D.</param>
        /// <param name="wrapS"></param>
        /// <param name="wrapT"></param>
        /// <returns></returns>
        /// <remarks>@todo</remarks>
        protected virtual void SetWrapMode(Texture2D texture2d, GltfTextureWrapMode wrapS, GltfTextureWrapMode wrapT)
        {
            TextureWrapType wrapType = GetWrapType(wrapS, wrapT);

            switch (wrapType)
            {
                case TextureWrapType.All:
                    texture2d.wrapMode = wrapS.ToUnityMode();
                    break;

                case TextureWrapType.U:
                    texture2d.wrapModeU = wrapS.ToUnityMode();
                    break;

                case TextureWrapType.V:
                    texture2d.wrapModeV = wrapT.ToUnityMode();
                    break;

                case TextureWrapType.W:
                    texture2d.wrapModeW = wrapT.ToUnityMode();
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Get texture wrap type.
        /// </summary>
        /// <param name="wrapS"></param>
        /// <param name="wrapT"></param>
        /// <returns></returns>
        /// <remarks>@todo</remarks>
        protected virtual TextureWrapType GetWrapType(GltfTextureWrapMode wrapS, GltfTextureWrapMode wrapT)
        {
            if (wrapS == wrapT)
            {
                return TextureWrapType.All;
            }
            if (wrapT == GltfTextureWrapMode.NONE)
            {
                return TextureWrapType.U;
            }
            if (wrapS == GltfTextureWrapMode.NONE)
            {
                return TextureWrapType.V;
            }
            else
            {
                return TextureWrapType.W;
            }
        }

        #endregion

        #region Material

        /// <summary>
        /// Create material info list.
        /// </summary>
        /// <returns>List of material info.</returns>
        /// <remarks>
        /// gltf.materials
        /// gltf.meshes
        /// </remarks>
        protected virtual List<MaterialInfo> CreateMaterialInfoList()
        {
            var materialInfoList = new List<MaterialInfo>();

            for (int materialIndex = 0; materialIndex < Gltf.materials.Count; materialIndex++)
            {
                GltfMaterial gltfMaterial = Gltf.materials[materialIndex];

                MaterialInfo materialInfo = _MaterialImporter.CreateMaterialInfo(materialIndex, gltfMaterial, Gltf.meshes, ModelAsset.TextureInfoList);

                materialInfoList.Add(materialInfo);
            }

            return materialInfoList;
        }

        /// <summary>
        /// Create material assets.
        /// </summary>
        /// <returns>List of unity material.</returns>
        protected List<Material> CreateMaterialAssets()
        {
            var materialList = new List<Material>();

            for (int materialIndex = 0; materialIndex < ModelAsset.MaterialInfoList.Count; materialIndex++)
            {
                MaterialInfo materialInfo = ModelAsset.MaterialInfoList[materialIndex];

                Material material = _MaterialImporter.CreateMaterialAsset(materialInfo, ModelAsset.Texture2dList);

                materialList.Add(material);
            }

            return materialList;
        }

        #endregion

        #region Mesh

        /// <summary>
        /// Create mesh assets.
        /// </summary>
        /// <returns>List of mesh asset.</returns>
        /// <remarks>
        /// gltf.buffers
        /// gltf.bufferViews
        /// gltf.accessors
        /// gltf.meshes
        /// </remarks>
        protected virtual List<MeshAsset> CreateMeshAssets()
        {
            GltfMeshImporter meshImporter = new GltfMeshImporter
            {
                GltfStorageAdapter = _GltfStorageAdapter,
                UnityMaterialList = ModelAsset.MaterialList
            };

            var meshAssetList = new List<MeshAsset>();

            for (int meshIndex = 0; meshIndex < Gltf.meshes.Count; meshIndex++)
            {
                MeshAsset meshAsset = meshImporter.CreateMeshAsset(meshIndex);

                if (meshAssetList.Where(x => x.Mesh.name == meshAsset.Mesh.name).Any())
                {
                    meshAsset.Mesh.name = string.Format($"mesh_{meshIndex}");
                }

                meshAssetList.Add(meshAsset);
            }

            return meshAssetList;
        }

        #endregion

        #region Node

        /// <summary>
        /// Create nodes.
        /// </summary>
        /// <returns>List of transform.</returns>
        /// <remarks>
        /// gltf.nodes
        /// </remarks>
        protected virtual List<Transform> CreateNodes()
        {
            var nodes = new List<Transform>();

            for (int nodeIndex = 0; nodeIndex < Gltf.nodes.Count; nodeIndex++)
            {
                GameObject go = CreateNode(nodeIndex);

                nodes.Add(go.transform);
            }

            return nodes;
        }

        /// <summary>
        /// Create a node.
        /// </summary>
        /// <param name="nodeIndex">The index of gltf.node.</param>
        /// <returns>A game object.</returns>
        protected virtual GameObject CreateNode(int nodeIndex)
        {
            GltfNode gltfNode = Gltf.nodes[nodeIndex];

            var nodeName = gltfNode.name;

            if (string.IsNullOrEmpty(nodeName))
            {
                nodeName = string.Format("node{0:000}", nodeIndex);
            }
            else if (nodeName.Contains("/"))
            {
                nodeName = nodeName.Replace("/", "_");
            }

            var go = new GameObject(nodeName);

            if (gltfNode.matrix.HasValue)
            {
                var m = gltfNode.matrix.Value.ToUnityMatrix();

                go.transform.localRotation = m.ExtractRotation();
                go.transform.localPosition = m.ExtractTransration();
                go.transform.localScale = m.ExtractScale();
            }
            else
            {
                if (gltfNode.translation.HasValue)
                {
                    go.transform.localPosition = gltfNode.translation.Value.ToUnityVector3();
                }
                if (gltfNode.rotation.HasValue)
                {
                    go.transform.localRotation = gltfNode.rotation.Value.ToUnityQuaternion();
                }
                if (gltfNode.scale.HasValue)
                {
                    go.transform.localScale = gltfNode.scale.Value.ToUnityVector3();
                }
            }

            return go;
        }

        /// <summary>
        /// Create node hierarchy.
        /// </summary>
        /// <param name="root">The game object of root.</param>
        protected virtual void CreateNodeHierarchy(GameObject root)
        {
            for (int nodeIndex = 0; nodeIndex < Nodes.Count; nodeIndex++)
            {
                Transform node = Nodes[nodeIndex];

                if (node.parent == null)
                {
                    node.SetParent(root.transform, worldPositionStays: false);
                }
            }

            for (int nodeIndex = 0; nodeIndex < Nodes.Count; nodeIndex++)
            {
                GltfNode gltfNode = Gltf.nodes[nodeIndex];

                if (gltfNode.children == null)
                {
                    continue;
                }

                foreach (int child in gltfNode.children)
                {
                    Nodes[child].transform.SetParent(parent: Nodes[nodeIndex].transform, worldPositionStays: false);
                }
            }
        }

        /// <summary>
        /// Fix node's coordinate. z-back to z-forward
        /// </summary>
        protected virtual void FixCoordinate()
        {
            Dictionary<Transform, (Vector3, Quaternion)> globalTransformMap
                = Nodes.ToDictionary(x => x, x => (x.position, x.rotation));

            foreach (int index in Gltf.rootnodes)
            {
                Transform t = Nodes[index];

                foreach (Transform transform in t.Traverse())
                {
                    var (position, rotation) = globalTransformMap[transform];

                    transform.position = position.ReverseZ();
                    transform.rotation = rotation.ReverseZ();
                }
            }
        }

        #endregion

        #region Renderer

        /// <summary>
        /// Attach meshes to each nodes.
        /// </summary>
        /// <remarks>
        /// gltf.nodes
        /// gltf.skins
        /// </remarks>
        protected virtual void AttachMeshes()
        {
            for (int nodeIndex = 0; nodeIndex < Gltf.nodes.Count; nodeIndex++)
            {
                if (Gltf.nodes[nodeIndex].mesh >= 0)
                {
                    AttachMesh(nodeIndex, ShowMesh, UpdateWhenOffscreen);
                }
            }
        }

        /// <summary>
        /// Attach mesh to node.
        /// </summary>
        /// <param name="nodeIndex">The index of gltf.node.</param>
        /// <param name="showMesh">Whether show mesh renderer.</param>
        /// <param name="updateWhenOffscreen">Whether update skinned mesh renderer when off screen.</param>
        protected virtual void AttachMesh(int nodeIndex, bool showMesh = true, bool updateWhenOffscreen = false)
        {
            GltfNode gltfNode = Gltf.nodes[nodeIndex];

            MeshAsset meshInfo = ModelAsset.MeshAssetList[gltfNode.mesh];

            GameObject go = Nodes[nodeIndex].gameObject;

            Renderer renderer;
            
            if ((meshInfo.Mesh.blendShapeCount == 0) && (gltfNode.skin == -1))
            {
                // without blendshape and bone skinning
                var filter = go.AddComponent<MeshFilter>();

                filter.sharedMesh = meshInfo.Mesh;

                renderer = go.AddComponent<MeshRenderer>();

                renderer.sharedMaterials = meshInfo.Materials;
            }
            else
            {
                var skinnedMeshRenderer = go.AddComponent<SkinnedMeshRenderer>();

                skinnedMeshRenderer.sharedMesh = meshInfo.Mesh;
                skinnedMeshRenderer.sharedMaterials = meshInfo.Materials;

                if (gltfNode.skin >= 0)
                {
                    SetupSkin(skinnedMeshRenderer, gltfNode.skin);
                }

                if (updateWhenOffscreen)
                {
                    skinnedMeshRenderer.updateWhenOffscreen = true;
                }

                renderer = skinnedMeshRenderer;
            }

            if (showMesh == false)
            {
                renderer.enabled = false;
            }

            meshInfo.Renderer = renderer;
        }

        /// <summary>
        /// Set up a skin.
        /// </summary>
        /// <param name="skinnedMeshRenderer">A skinnedned mesh renderer.</param>
        /// <param name="skinIndex">The index of gltf.skin.</param>
        /// <remarks>
        /// gltf.skins
        /// </remarks>
        public void SetupSkin(SkinnedMeshRenderer skinnedMeshRenderer, int skinIndex)
        {
            if (skinnedMeshRenderer.sharedMesh == null)
            {
                throw new Exception();
            }

            GltfSkin skin = Gltf.skins[skinIndex];

            Mesh mesh = skinnedMeshRenderer.sharedMesh;

            Transform[] joints = skin.joints.Select(n => Nodes[n]).ToArray();

            // calculate internal values(boundingBox etc...) when sharedMesh assigned ?
            skinnedMeshRenderer.sharedMesh = null;

            if (joints.Any())
            {
                // have bones
                skinnedMeshRenderer.bones = joints;

                if (skin.inverseBindMatrices >= 0)
                {
                    Matrix4x4[] bindPoses = _GltfStorageAdapter
                        .GetAccessorArrayData<Matrix4x4>(skin.inverseBindMatrices)
                        .Select(m => m.ReverseZ())
                        .ToArray();

                    mesh.bindposes = bindPoses;
                }
                else
                {
                    // calc default matrices
                    // https://docs.unity3d.com/ScriptReference/Mesh-bindposes.html

                    Transform meshCoords = skinnedMeshRenderer.transform; // ?

                    Matrix4x4[] calculatedBindPoses = joints
                        .Select(t => t.worldToLocalMatrix * meshCoords.localToWorldMatrix)
                        .ToArray();

                    mesh.bindposes = calculatedBindPoses;
                }
            }

            skinnedMeshRenderer.sharedMesh = mesh;

            if (skin.skeleton.IsInRangeOf(Nodes))
            {
                skinnedMeshRenderer.rootBone = Nodes[skin.skeleton];
            }
        }

        #endregion
    }
}
