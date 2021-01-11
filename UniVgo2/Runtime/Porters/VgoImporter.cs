// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : VgoImporter
// ----------------------------------------------------------------------
namespace UniVgo2
{
    using NewtonVgo;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using UnityEngine;
    using UniVgo2.Converters;
    using UniVgo2.Porters;

    /// <summary>
    /// VGO Importer
    /// </summary>
    public class VgoImporter
    {
        #region Constructors

        /// <summary>
        /// Create a new instance of VgoImporter.
        /// </summary>
        public VgoImporter()
        {
            Initialize();
        }

        #endregion

        #region Fields

        /// <summary>The VGO storage.</summary>
        protected VgoStorage _Storage;

        /// <summary>The VGO storage adapter.</summary>
        protected VgoStorageAdapter _StorageAdapter;

        /// <summary>The model asset.</summary>
        protected ModelAsset ModelAsset = new ModelAsset();

        /// <summary>List of node.</summary>
        protected List<Transform> Nodes = null;

        /// <summary>The material importer.</summary>
        protected IMaterialImporter _MaterialImporter = null;

        /// <summary>The ParticleSystem importer.</summary>
        protected VgoParticleSystemImporter _ParticleSystemImporter = new VgoParticleSystemImporter();

        /// <summary>The texture converter.</summary>
        protected TextureConverter _TextureConverter = new TextureConverter();

        #endregion

        #region Properties

        /// <summary>The VGO layout.</summary>
        protected VgoLayout Layout => _Storage.Layout;

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
            _MaterialImporter = new VgoMaterialPorter()
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
        /// <param name="vgoFilePath">The file path of the vgo.</param>
        /// <param name="vgkFilePath">The file path of the crypt key.</param>
        /// <returns>A model asset.</returns>
        public virtual ModelAsset Load(string vgoFilePath, string vgkFilePath = null)
        {
            var storage = new VgoStorage(vgoFilePath, vgkFilePath);

            return Load(storage);
        }

        /// <summary>
        /// Load a 3D model from the specified bytes.
        /// </summary>
        /// <param name="allBytes">All bytes of file.</param>
        /// <param name="cryptKey">The crypt key.</param>
        /// <returns>A model asset.</returns>
        public virtual ModelAsset Load(byte[] allBytes, byte[] cryptKey = null)
        {
            var storage = new VgoStorage(allBytes, cryptKey);

            return Load(storage);
        }

        /// <summary>
        /// Load a 3D model from the specified VGO storage.
        /// </summary>
        /// <param name="vgoStorage">A VGO storage.</param>
        /// <returns>A model asset.</returns>
        public virtual ModelAsset Load(VgoStorage vgoStorage)
        {
            _Storage = vgoStorage;

            _StorageAdapter = new VgoStorageAdapter(vgoStorage);

            ModelAsset = new ModelAsset();

            ModelAsset.Layout = _Storage.Layout;

            // UnityEngine.Texture2D
            ModelAsset.Texture2dList = CreateTextureAssets();

            // UnityEngine.Material
            ModelAsset.MaterialList = CreateMaterialAssets();

            // UnityEngine.Mesh
            ModelAsset.MeshAssetList = CreateMeshAssets();

            // UnityEngine.VgoSpringBoneColliderGroup
            ModelAsset.SpringBoneColliderGroupArray = CreateSpringBoneColliderGroupArray();

            // UnityEngine.GameObejct
            Nodes = CreateNodes();

            CreateNodeHierarchy();

            FixCoordinate();

            SetupNodes();

            SetupAssetInfo();

            return ModelAsset;
        }

        /// <summary>
        /// Extract a 3D model asset from the specified file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>A model asset.</returns>
        /// <remarks>for ScriptedImporter</remarks>
        public virtual ModelAsset Extract(string filePath)
        {
            FileInfo vgoFileInfo = new FileInfo(filePath);

            string vgkFileName = vgoFileInfo.Name.Substring(0, vgoFileInfo.Name.Length - vgoFileInfo.Extension.Length) + ".vgk";

            string vgkFilePath = Path.Combine(vgoFileInfo.DirectoryName, vgkFileName);

            FileInfo vgkFileInfo = new FileInfo(vgkFilePath);

            if (vgkFileInfo.Exists)
            {
                _Storage = new VgoStorage(filePath, vgkFilePath);
            }
            else
            {
                _Storage = new VgoStorage(filePath);
            }

            _StorageAdapter = new VgoStorageAdapter(_Storage);

            ModelAsset = new ModelAsset();

            ModelAsset.Layout = _Storage.Layout;

            // UnityEngine.Texture2D
            ModelAsset.Texture2dList = CreateTextureAssets();

            // UnityEngine.Material
            ModelAsset.MaterialList = CreateMaterialAssets();

            return ModelAsset;
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

            if ((Layout.textures == null) || (Layout.textures.Any() == false))
            {
                return texture2dList;
            }

            for (int textureIndex = 0; textureIndex < Layout.textures.Count; textureIndex++)
            {
                VgoTexture vgoTexture = Layout.textures[textureIndex];

                Texture2D texture2d = CreateTexture2D(vgoTexture);

                texture2dList.Add(texture2d);
            }

            return texture2dList;
        }

        /// <summary>
        /// Create a texture 2D.
        /// </summary>
        /// <param name="vgoTexture">A texture.</param>
        /// <returns>A texture 2D.</returns>
        protected virtual Texture2D CreateTexture2D(VgoTexture vgoTexture)
        {
            if (vgoTexture.dimensionType != TextureDimension.Tex2D)
            {
                throw new Exception($"Texture.dimensionType: {vgoTexture.dimensionType}");
            }

            if (vgoTexture.source.IsInRangeOf(_Storage.ResourceAccessors) == false)
            {
                throw new Exception($"Texture.source: {vgoTexture.source}");
            }

            byte[] imageBytes = _StorageAdapter.GetAccessorBytes(vgoTexture.source).ToArray();

            var srcTexture2d = new Texture2D(width: 2, height: 2, TextureFormat.ARGB32, mipChain: false, linear: vgoTexture.IsLinear)
            {
                name = vgoTexture.name
            };

            srcTexture2d.LoadImage(imageBytes);

            Texture2D texture2D = _TextureConverter.GetImportTexture(srcTexture2d, vgoTexture.mapType, vgoTexture.metallicRoughness);

            texture2D.filterMode = (UnityEngine.FilterMode)vgoTexture.filterMode;
            texture2D.wrapMode = (UnityEngine.TextureWrapMode)vgoTexture.wrapMode;
            texture2D.wrapModeU = (UnityEngine.TextureWrapMode)vgoTexture.wrapModeU;
            texture2D.wrapModeV = (UnityEngine.TextureWrapMode)vgoTexture.wrapModeV;

            return texture2D;
        }

        #endregion

        #region Material

        /// <summary>
        /// Create material assets.
        /// </summary>
        /// <returns>List of unity material.</returns>
        protected List<Material> CreateMaterialAssets()
        {
            var materialList = new List<Material>();

            if ((Layout.materials == null) || (Layout.materials.Any() == false))
            {
                return materialList;
            }

            for (int materialIndex = 0; materialIndex < Layout.materials.Count; materialIndex++)
            {
                VgoMaterial vgoMaterial = Layout.materials[materialIndex];

                Material material = _MaterialImporter.CreateMaterialAsset(vgoMaterial, ModelAsset.Texture2dList);

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
        protected virtual List<MeshAsset> CreateMeshAssets()
        {
            var meshAssetList = new List<MeshAsset>();

            if ((Layout.meshes == null) || (Layout.meshes.Any() == false))
            {
                return meshAssetList;
            }

            VgoMeshImporter meshImporter = new VgoMeshImporter
            {
                StorageAdapter = _StorageAdapter,
                UnityMaterialList = ModelAsset.MaterialList,
                ScriptableObjectList = ModelAsset.ScriptableObjectList,
            };

            for (int meshIndex = 0; meshIndex < Layout.meshes.Count; meshIndex++)
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

        #region Spring Bone

        protected virtual VgoSpringBone.VgoSpringBoneColliderGroup[] CreateSpringBoneColliderGroupArray()
        {
            if (ModelAsset.Layout.springBoneInfo == null)
            {
                return null;
            }

            if (ModelAsset.Layout.springBoneInfo.colliderGroups == null)
            {
                return null;
            }

            if (ModelAsset.Layout.springBoneInfo.colliderGroups.Count == 0)
            {
                return null;
            }

            return new VgoSpringBone.VgoSpringBoneColliderGroup[ModelAsset.Layout.springBoneInfo.colliderGroups.Count];
        }

        #endregion

        #region Node

        /// <summary>
        /// Create nodes.
        /// </summary>
        /// <returns>List of transform.</returns>
        protected virtual List<Transform> CreateNodes()
        {
            var nodes = new List<Transform>(Layout.nodes.Count);

            System.Numerics.Matrix4x4[] matrixes = GetVgoNodeTransforms();

            for (int nodeIndex = 0; nodeIndex < Layout.nodes.Count; nodeIndex++)
            {
                string name = Layout.nodes[nodeIndex].name;

                if (string.IsNullOrEmpty(name))
                {
                    name = string.Format("node_{0:000}", nodeIndex);
                }
                else if (name.Contains("/"))
                {
                    name = name.Replace("/", "_");
                }

                // GameObject
                var go = new GameObject(name);

                // Transform
                Matrix4x4 matrix = matrixes[nodeIndex].ToUnityMatrix();

                go.transform.localPosition = matrix.ExtractTransration();
                go.transform.localRotation = matrix.ExtractRotation();
                go.transform.localScale = matrix.ExtractScale();

                nodes.Add(go.transform);
            }

            return nodes;
        }

        /// <summary>
        /// Create node hierarchy.
        /// </summary>
        protected virtual void CreateNodeHierarchy()
        {
            if (Layout.nodes[0].isRoot == false)
            {
                throw new FormatException("nodes[0].isRoot: false");
            }

            for (int nodeIndex = 0; nodeIndex < Nodes.Count; nodeIndex++)
            {
                VgoNode vgoNode = Layout.nodes[nodeIndex];

                if (vgoNode.children == null)
                {
                    continue;
                }

                foreach (int child in vgoNode.children)
                {
                    Nodes[child].transform.SetParent(parent: Nodes[nodeIndex].transform, worldPositionStays: false);
                }
            }

            ModelAsset.Root = Nodes[0].gameObject;
        }

        /// <summary>
        /// Fix node's coordinate. z-back to z-forward
        /// </summary>
        protected virtual void FixCoordinate()
        {
            if (_Storage.GeometryCoordinate == VgoGeometryCoordinate.LeftHanded)
            {
                return;
            }

            Dictionary<Transform, (Vector3, Quaternion)> globalTransformMap
                = Nodes.ToDictionary(x => x, x => (x.position, x.rotation));

            Transform root = Nodes[0];

            foreach (Transform transform in root.Traverse())
            {
                var (position, rotation) = globalTransformMap[transform];

                transform.position = position.ReverseZ();
                transform.rotation = rotation.ReverseZ();
            }
        }

        /// <summary>
        /// Setup nodes.
        /// </summary>
        protected virtual void SetupNodes()
        {
            for (int nodeIndex = 0; nodeIndex < Layout.nodes.Count; nodeIndex++)
            {
                SetupNode(nodeIndex);
            }

            // UnigyEngine.VgoSpringBone
            for (int nodeIndex = 0; nodeIndex < Layout.nodes.Count; nodeIndex++)
            {
                SetupNodeSpringBone(nodeIndex);
            }

            // UnityEngine.Renderer
            for (int nodeIndex = 0; nodeIndex < Layout.nodes.Count; nodeIndex++)
            {
                if (Layout.nodes[nodeIndex].mesh >= 0)
                {
                    AttachMesh(nodeIndex, ShowMesh, UpdateWhenOffscreen);
                }
            }
        }

        /// <summary>
        /// Setup a node.
        /// </summary>
        /// <param name="nodeIndex">The index of layout.nodes.</param>
        protected virtual void SetupNode(int nodeIndex)
        {
            // GameObject
            GameObject go = Nodes[nodeIndex].gameObject;

            VgoNode vgoNode = Layout.nodes[nodeIndex];

            go.SetActive(vgoNode.isActive);

            go.isStatic = vgoNode.isStatic;

            // Tag
            if ((string.IsNullOrEmpty(vgoNode.tag) == false) && (vgoNode.tag != "Untagged"))
            {
                try
                {
                    go.tag = vgoNode.tag;
                }
                catch
                {
                    // UnityException: Tag: <vgoNode.tag> is not defined.
                }
            }

            // Layer
            if ((0 <= vgoNode.layer) && (vgoNode.layer <= 31))
            {
                try
                {
                    go.layer = vgoNode.layer;
                }
                catch
                {
                    //
                }
            }

            // Animator
            if (vgoNode.animator != null)
            {
                Animator animator = go.AddComponent<Animator>();

                VgoAnimatorConverter.SetComponentValue(animator, vgoNode.animator);

                // Avatar
                if (vgoNode.animator.humanAvatar != null)
                {
                    animator.avatar = VgoAvatarConverter.CreateHumanAvatar(vgoNode.animator.humanAvatar, go, Nodes);

                    CreateAndAddAvatarConfiguration(vgoNode.animator.humanAvatar);

                    ModelAsset.Avatar = animator.avatar;
                }
            }

            // Rigidbody
            if (vgoNode.rigidbody != null)
            {
                Rigidbody rigidbody = go.AddComponent<Rigidbody>(); ;

                VgoRigidbodyConverter.SetComponentValue(rigidbody, vgoNode.rigidbody);
            }

            // Colliders
            if (vgoNode.colliders != null)
            {
                foreach (VgoCollider vgoCollider in vgoNode.colliders)
                {
                    Collider collider;

                    switch (vgoCollider.type)
                    {
                        case VgoColliderType.Box:
                            collider = go.AddComponent<BoxCollider>();
                            break;
                        case VgoColliderType.Capsule:
                            collider = go.AddComponent<CapsuleCollider>();
                            break;
                        case VgoColliderType.Sphere:
                            collider = go.AddComponent<SphereCollider>();
                            break;
                        default:
                            continue;
                    }

                    VgoColliderConverter.SetComponentValue(collider, vgoCollider, _Storage.GeometryCoordinate);
                }
            }

            // Skybox
            if (vgoNode.skybox != null)
            {
                var skybox = go.AddComponent<Skybox>();

                if (ModelAsset.MaterialList != null)
                {
                    if (ModelAsset.MaterialList.TryGetValue(vgoNode.skybox.materialIndex, out Material skyboxMaterial))
                    {
                        skybox.material = skyboxMaterial;
                    }
                }
            }

            // Light
            if (vgoNode.light != null)
            {
                Light light = go.AddComponent<Light>();

                VgoLightConverter.SetComponentValue(light, vgoNode.light);
            }

            // Right
            if (vgoNode.right != null)
            {
                VgoRight vgoRightComponent = go.AddComponent<VgoRight>();

                if (vgoRightComponent != null)
                {
                    vgoRightComponent.Right = new NewtonVgo.VgoRight(vgoNode.right);
                }
            }

            // ParticleSystem
            if (vgoNode.particle != -1)
            {
                if (Layout.particles.TryGetValue(vgoNode.particle, out VgoParticleSystem vgoParticleSystem))
                {
                    _ParticleSystemImporter.AddComponent(go, vgoParticleSystem, _StorageAdapter, ModelAsset.MaterialList, ModelAsset.Texture2dList);
                }
            }

            // SpringBoneCollider
            if ((vgoNode.springBoneColliderGroup != -1) && (Layout.springBoneInfo.colliderGroups != null))
            {
                if (Layout.springBoneInfo.colliderGroups.TryGetValue(vgoNode.springBoneColliderGroup, out VgoSpringBoneColliderGroup layoutSpringBoneColliderGroup))
                {
                    var component = go.AddComponent<VgoSpringBone.VgoSpringBoneColliderGroup>();

                    if ((layoutSpringBoneColliderGroup.colliders != null) && (layoutSpringBoneColliderGroup.colliders.Length > 0))
                    {
                        component.colliders = new VgoSpringBone.SpringBoneCollider[layoutSpringBoneColliderGroup.colliders.Length];

                        for (int index = 0; index < layoutSpringBoneColliderGroup.colliders.Length; index++)
                        {
                            VgoSpringBoneCollider layoutCollider = layoutSpringBoneColliderGroup.colliders[index];

                            component.colliders[index] = new VgoSpringBone.SpringBoneCollider
                            {
                                colliderType = (VgoSpringBone.SpringBoneColliderType)layoutCollider.colliderType,
                                offset = layoutCollider.offset.ToUnityVector3(),
                                radius = layoutCollider.radius.GetValueOrDefault(0.0f),
                            };
                        }
                    }

                    component.gizmoColor = layoutSpringBoneColliderGroup.gizmoColor.ToUnityColor();

                    ModelAsset.SpringBoneColliderGroupArray[vgoNode.springBoneColliderGroup] = component;
                }
            }
        }

        /// <summary>
        /// Setup a node spring bone.
        /// </summary>
        /// <param name="nodeIndex">The index of layout.nodes.</param>
        protected virtual void SetupNodeSpringBone(int nodeIndex)
        {
            if (Layout.springBoneInfo?.springBoneGroups == null)
            {
                return;
            }

            GameObject go = Nodes[nodeIndex].gameObject;

            VgoNode vgoNode = Layout.nodes[nodeIndex];

            if (vgoNode.springBoneGroups == null)
            {
                return;
            }

            foreach (var groupIndex in vgoNode.springBoneGroups)
            {
                if (Layout.springBoneInfo.springBoneGroups.TryGetValue(groupIndex, out VgoSpringBoneGroup layoutSpringBoneGroup) == false)
                {
                    continue;
                }

                var component = go.AddComponent<VgoSpringBone.VgoSpringBoneGroup>();

                component.comment = layoutSpringBoneGroup.comment;
                component.dragForce = layoutSpringBoneGroup.dragForce.SafeValue(0.0f, 1.0f, 0.0f);
                component.stiffnessForce = layoutSpringBoneGroup.stiffnessForce.SafeValue(0.0f, 4.0f, 1.0f);
                component.gravityDirection = layoutSpringBoneGroup.gravityDirection.ToUnityVector3();
                component.gravityPower = layoutSpringBoneGroup.gravityPower.SafeValue(0.0f, 2.0f, 1.0f);
                component.hitRadius = layoutSpringBoneGroup.hitRadius.SafeValue(0.0f, 0.5f, 0.1f);
                component.drawGizmo = layoutSpringBoneGroup.drawGizmo;
                component.gizmoColor = layoutSpringBoneGroup.gizmoColor.ToUnityColor();

                // rootBones
                if ((layoutSpringBoneGroup.rootBones != null) && (layoutSpringBoneGroup.rootBones.Length > 0))
                {
                    component.rootBones = new Transform[layoutSpringBoneGroup.rootBones.Length];

                    for (int index = 0; index < layoutSpringBoneGroup.rootBones.Length; index++)
                    {
                        if (Nodes.TryGetValue(layoutSpringBoneGroup.rootBones[index], out Transform rootBoneNode))
                        {
                            component.rootBones[index] = rootBoneNode;
                        }
                    }
                }

                // colliderGroups
                if ((layoutSpringBoneGroup.colliderGroups != null) &&
                    (layoutSpringBoneGroup.colliderGroups.Length > 0) &&
                    (ModelAsset.SpringBoneColliderGroupArray != null))
                {
                    component.colliderGroups = new VgoSpringBone.VgoSpringBoneColliderGroup[layoutSpringBoneGroup.colliderGroups.Length];

                    for (int index = 0; index < layoutSpringBoneGroup.colliderGroups.Length; index++)
                    {
                        if ((0 <= layoutSpringBoneGroup.colliderGroups[index]) && (layoutSpringBoneGroup.colliderGroups[index] < ModelAsset.SpringBoneColliderGroupArray.Length))
                        {
                            var colliderGroup = ModelAsset.SpringBoneColliderGroupArray[layoutSpringBoneGroup.colliderGroups[index]];

                            component.colliderGroups[index] = colliderGroup;
                        }
                    }
                }
            }
        }

        #endregion

        #region Renderer

        /// <summary>
        /// Attach mesh to node.
        /// </summary>
        /// <param name="nodeIndex">The index of layout.node.</param>
        /// <param name="showMesh">Whether show mesh renderer.</param>
        /// <param name="updateWhenOffscreen">Whether update skinned mesh renderer when off screen.</param>
        protected virtual void AttachMesh(int nodeIndex, bool showMesh = true, bool updateWhenOffscreen = false)
        {
            VgoNode vgoNode = Layout.nodes[nodeIndex];

            MeshAsset meshAsset = ModelAsset.MeshAssetList[vgoNode.mesh];

            GameObject go = Nodes[nodeIndex].gameObject;

            Renderer renderer;
            
            if ((meshAsset.Mesh.blendShapeCount == 0) && (vgoNode.skin == -1))
            {
                // without blendshape and bone skinning
                var filter = go.AddComponent<MeshFilter>();

                filter.sharedMesh = meshAsset.Mesh;

                renderer = go.AddComponent<MeshRenderer>();

                renderer.sharedMaterials = meshAsset.Materials;
            }
            else
            {
                var skinnedMeshRenderer = go.AddComponent<SkinnedMeshRenderer>();

                skinnedMeshRenderer.sharedMesh = meshAsset.Mesh;
                skinnedMeshRenderer.sharedMaterials = meshAsset.Materials;

                if (vgoNode.skin >= 0)
                {
                    SetupSkin(skinnedMeshRenderer, vgoNode.skin);
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

            meshAsset.Renderer = renderer;

            if (meshAsset.BlendShapeConfiguration != null)
            {
                var vgoBlendShape = go.AddComponent<VgoBlendShape>();

                vgoBlendShape.BlendShapeConfiguration = meshAsset.BlendShapeConfiguration;
            }
        }

        /// <summary>
        /// Set up a skin.
        /// </summary>
        /// <param name="skinnedMeshRenderer">A skinnedned mesh renderer.</param>
        /// <param name="skinIndex">The index of gltf.skin.</param>
        public void SetupSkin(SkinnedMeshRenderer skinnedMeshRenderer, int skinIndex)
        {
            if (skinnedMeshRenderer.sharedMesh == null)
            {
                throw new Exception();
            }

            if (Layout.skins == null)
            {
                throw new Exception();
            }

            if (Layout.skins.TryGetValue(skinIndex, out VgoSkin skin) == false)
            {
                throw new IndexOutOfRangeException($"skins[{skinIndex}] is out of the range.");
            }

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
                    if (_StorageAdapter.GeometryCoordinate == VgoGeometryCoordinate.RightHanded)
                    {
                        ReadOnlySpan<Matrix4x4> matrixSpan = _StorageAdapter.GetAccessorSpan<Matrix4x4>(skin.inverseBindMatrices);

                        var bindPoses = new Matrix4x4[matrixSpan.Length];

                        for (int i = 0; i < matrixSpan.Length; i++)
                        {
                            bindPoses[i] = matrixSpan[i].ReverseZ();
                        }

                        mesh.bindposes = bindPoses;
                    }
                    else
                    {
                        mesh.bindposes = _StorageAdapter.GetAccessorArrayData<Matrix4x4>(skin.inverseBindMatrices);
                    }
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

        #region Asset Info

        /// <summary>
        /// Setup a asset info to root GameObject.
        /// </summary>
        protected virtual void SetupAssetInfo()
        {
            if (_Storage.AssetInfo == null)
            {
                return;
            }

            VgoAssetInfo vgoAssetInfo = _Storage.AssetInfo;

            if (vgoAssetInfo.right != null)
            {
                VgoRight vgoRightComponent = ModelAsset.Root.AddComponent<VgoRight>();

                vgoRightComponent.Right = new NewtonVgo.VgoRight(vgoAssetInfo.right);
            }

            if (vgoAssetInfo.generator != null)
            {
                VgoGenerator vgoGeneratorComponent = ModelAsset.Root.AddComponent<VgoGenerator>();

                vgoGeneratorComponent.GeneratorInfo = new VgoGeneratorInfo(vgoAssetInfo.generator);
            }
        }

        #endregion

        #region Skybox

        /// <summary>
        /// Reflect VGO skybox to Camera skybox.
        /// </summary>
        public virtual void ReflectSkybox(Camera camera)
        {
            var vgoSkybox = ModelAsset.Root.GetComponentInChildren<Skybox>(includeInactive: false);

            if (vgoSkybox != null)
            {
                if (camera.gameObject.TryGetComponentEx(out Skybox cameraSkybox) == false)
                {
                    cameraSkybox = camera.gameObject.AddComponent<Skybox>();
                }

                cameraSkybox.material = vgoSkybox.material;
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Create and add avatar configuration.
        /// </summary>
        /// <param name="vgoHumanAvatar">The vgo human avatar.</param>
        protected virtual void CreateAndAddAvatarConfiguration(VgoHumanAvatar vgoHumanAvatar)
        {
            var avatarConfiguration = ScriptableObject.CreateInstance<AvatarConfiguration>();

            avatarConfiguration.name = vgoHumanAvatar.name;
            avatarConfiguration.humanBones = vgoHumanAvatar.humanBones;

            ModelAsset.ScriptableObjectList.Add(avatarConfiguration);
        }

        /// <summary>
        /// Get vgo node transforms.
        /// </summary>
        /// <returns>An array of matrix.</returns>
        protected virtual System.Numerics.Matrix4x4[] GetVgoNodeTransforms()
        {
            if (_Storage.ResourceAccessors.Where(x => x.kind == VgoResourceAccessorKind.NodeTransform).Count() != 1)
            {
                throw new Exception();
            }

            VgoResourceAccessor accessor = _Storage.ResourceAccessors.Where(x => x.kind == VgoResourceAccessorKind.NodeTransform).First();

            ArraySegment<byte> transformBytes = _StorageAdapter.GetAccessorBytes(accessor);

            var transforms = new System.Numerics.Matrix4x4[Layout.nodes.Count];

            transformBytes.MarshalCopyTo(transforms);

            return transforms;
        }

        #endregion
    }
}
