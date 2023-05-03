// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : VgoImporter
// ----------------------------------------------------------------------
#nullable enable
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
        #region Fields

        /// <summary>The vgo importer option.</summary>
        protected readonly VgoImporterOption _Option;

        /// <summary>The material importer.</summary>
        protected readonly IVgoMaterialImporter _MaterialImporter;

        /// <summary>The mesh importer.</summary>
        protected readonly IVgoMeshImporter _MeshImporter;

        /// <summary>The particle system importer.</summary>
        protected readonly IVgoParticleSystemImporter _ParticleSystemImporter;

        /// <summary>The texture converter.</summary>
        protected readonly ITextureConverter _TextureConverter;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of VgoImporter.
        /// </summary>
        public VgoImporter() : this(new VgoImporterOption()) { }

        /// <summary>
        /// Create a new instance of VgoImporter with option.
        /// </summary>
        /// <param name="option">The vgo importer option.</param>
        public VgoImporter(VgoImporterOption option)
        {
            _Option = option;

            _MaterialImporter = new VgoMaterialPorter()
            {
                MaterialPorterStore = new VgoMaterialPorterStore(),
                ShaderStore = new ShaderStore(),
            };

            _MeshImporter = new VgoMeshImporter(option.MeshImporterOption);

            _ParticleSystemImporter = new VgoParticleSystemImporter();

            _TextureConverter = new TextureConverter();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Load a 3D model from the specified file.
        /// </summary>
        /// <param name="vgoFilePath">The file path of the vgo.</param>
        /// <param name="vgkFilePath">The file path of the crypt key.</param>
        /// <returns>A model asset.</returns>
        public virtual ModelAsset Load(string vgoFilePath, string? vgkFilePath = null)
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
        public virtual ModelAsset Load(byte[] allBytes, byte[]? cryptKey = null)
        {
            var storage = new VgoStorage(allBytes, cryptKey);

            return Load(storage);
        }

        /// <summary>
        /// Load a 3D model from the specified vgo storage.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <returns>A model asset.</returns>
        public virtual ModelAsset Load(IVgoStorage vgoStorage)
        {
            var modelAsset = new ModelAsset();

            modelAsset.Layout = vgoStorage.Layout;

            // UnityEngine.Texture2D
            modelAsset.Texture2dList = CreateTextureAssets(vgoStorage);

            // UnityEngine.Material
            modelAsset.MaterialList = CreateMaterialAssets(vgoStorage, modelAsset.Texture2dList);

            // UnityEngine.Mesh
            if (vgoStorage.IsSpecVersion_2_4_orLower)
            {
                modelAsset.MeshAssetList = CreateMeshAssets(vgoStorage, modelAsset.MaterialList);
            }
            else
            {
                modelAsset.MeshAssetList = CreateMeshAssets(vgoStorage);
            }

            // UnityEngine.AnimationClip
            modelAsset.AnimationClipList = CreateAnimationClipAssets(vgoStorage.Layout, vgoStorage.GeometryCoordinate);

            // UnityEngine.VgoSpringBoneColliderGroup
            modelAsset.SpringBoneColliderGroupArray = CreateSpringBoneColliderGroupArray(vgoStorage.Layout);

            // UnityEngine.GameObejct
            List<Transform> nodes = CreateNodes(vgoStorage);

            CreateNodeHierarchy(nodes, vgoStorage.Layout);

            modelAsset.Root = nodes[0].gameObject;

            if (vgoStorage.GeometryCoordinate == VgoGeometryCoordinate.RightHanded)
            {
                FixCoordinate(nodes);
            }

            SetupNodes(nodes, vgoStorage, modelAsset);

            SetupAssetInfo(vgoStorage, modelAsset);

            return modelAsset;
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

            VgoStorage vgoStorage;

            if (vgkFileInfo.Exists)
            {
                vgoStorage = new VgoStorage(filePath, vgkFilePath);
            }
            else
            {
                vgoStorage = new VgoStorage(filePath);
            }

            var modelAsset = new ModelAsset();

            modelAsset.Layout = vgoStorage.Layout;

            // UnityEngine.Texture2D
            modelAsset.Texture2dList = CreateTextureAssets(vgoStorage);

            // UnityEngine.Material
            modelAsset.MaterialList = CreateMaterialAssets(vgoStorage, modelAsset.Texture2dList);

            return modelAsset;
        }

        #endregion

        #region layout.textures

        /// <summary>
        /// Create texture assets.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <returns>List of unity texture2D.</returns>
        /// <remarks>
        /// After UpdateTextureInfoList()
        /// </remarks>
        protected virtual List<Texture2D?> CreateTextureAssets(IVgoStorage vgoStorage)
        {
            var texture2dList = new List<Texture2D?>();

            if ((vgoStorage.Layout.textures == null) || (vgoStorage.Layout.textures.Any() == false))
            {
                return texture2dList;
            }

            for (int textureIndex = 0; textureIndex < vgoStorage.Layout.textures.Count; textureIndex++)
            {
                VgoTexture? vgoTexture = vgoStorage.Layout.textures[textureIndex];

                if (vgoTexture is null)
                {
                    texture2dList.Add(null);
                }
                else
                {
                    Texture2D texture2d = CreateTexture2D(vgoTexture, vgoStorage);

                    texture2dList.Add(texture2d);
                }
            }

            return texture2dList;
        }

        /// <summary>
        /// Create a unity texture 2D.
        /// </summary>
        /// <param name="vgoTexture">A vgo texture.</param>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <returns>A unity texture 2D.</returns>
        protected virtual Texture2D CreateTexture2D(VgoTexture vgoTexture, IVgoStorage vgoStorage)
        {
            if (vgoTexture.dimensionType != TextureDimension.Tex2D)
            {
                throw new Exception($"Texture.dimensionType: {vgoTexture.dimensionType}");
            }

            if (vgoStorage.ResourceAccessors is null)
            {
                throw new Exception();
            }

            if (vgoTexture.source.IsInRangeOf(vgoStorage.ResourceAccessors) == false)
            {
                throw new Exception($"Texture.source: {vgoTexture.source}");
            }

            byte[] imageBytes = vgoStorage.GetAccessorBytes(vgoTexture.source).ToArray();

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

        #region layout.materials

        /// <summary>
        /// Create material assets.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="texture2dList">List of unity texture 2D.</param>
        /// <returns>List of unity material.</returns>
        protected virtual List<Material?> CreateMaterialAssets(IVgoStorage vgoStorage, List<Texture2D?> texture2dList)
        {
            var materialList = new List<Material?>();

            if ((vgoStorage.Layout.materials == null) || (vgoStorage.Layout.materials.Any() == false))
            {
                return materialList;
            }

            for (int materialIndex = 0; materialIndex < vgoStorage.Layout.materials.Count; materialIndex++)
            {
                VgoMaterial? vgoMaterial = vgoStorage.Layout.materials[materialIndex];

                if (vgoMaterial is null)
                {
                    materialList.Add(null);
                }
                else
                {
                    Material material = _MaterialImporter.CreateMaterialAsset(vgoMaterial, texture2dList);

                    materialList.Add(material);
                }
            }

            return materialList;
        }

        #endregion

        #region layout.meshes

        /// <summary>
        /// Create mesh assets.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="materialList">List of unity material.</param>
        /// <returns>List of mesh asset.</returns>
        protected virtual List<MeshAsset> CreateMeshAssets(IVgoStorage vgoStorage, IList<Material?>? materialList = null)
        {
            var meshAssetList = new List<MeshAsset>();

            if ((vgoStorage.Layout.meshes == null) || (vgoStorage.Layout.meshes.Any() == false))
            {
                return meshAssetList;
            }

            for (int meshIndex = 0; meshIndex < vgoStorage.Layout.meshes.Count; meshIndex++)
            {
                MeshAsset meshAsset = _MeshImporter.CreateMeshAsset(vgoStorage, meshIndex, materialList);

                if (meshAssetList.Where(x => x?.Mesh.name == meshAsset.Mesh.name).Any())
                {
                    meshAsset.Mesh.name = string.Format($"mesh_{meshIndex}");
                }

                meshAssetList.Add(meshAsset);
            }

            return meshAssetList;
        }

        #endregion

        #region layout.springBoneInfo

        /// <summary>
        /// Create vgo spring bone collider groups.
        /// </summary>
        /// <param name="vgoLayout">A vgo layout.</param>
        /// <returns>An array of vgo spring bone collider group.</returns>
        protected virtual VgoSpringBone.VgoSpringBoneColliderGroup[]? CreateSpringBoneColliderGroupArray(VgoLayout vgoLayout)
        {
            if (vgoLayout.springBoneInfo == null)
            {
                return null;
            }

            if (vgoLayout.springBoneInfo.colliderGroups == null)
            {
                return null;
            }

            if (vgoLayout.springBoneInfo.colliderGroups.Any() == false)
            {
                return null;
            }

            return new VgoSpringBone.VgoSpringBoneColliderGroup[vgoLayout.springBoneInfo.colliderGroups.Count];
        }

        #endregion

        #region layout.nodes

        /// <summary>
        /// Create nodes.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <returns>List of transform.</returns>
        protected virtual List<Transform> CreateNodes(IVgoStorage vgoStorage)
        {
            if (vgoStorage.Layout.nodes is null)
            {
                throw new Exception();
            }

            var nodes = new List<Transform>(vgoStorage.Layout.nodes.Count);

            System.Numerics.Matrix4x4[] matrixes = GetVgoNodeTransforms(vgoStorage);

            for (int nodeIndex = 0; nodeIndex < vgoStorage.Layout.nodes.Count; nodeIndex++)
            {
                string? name = vgoStorage.Layout.nodes[nodeIndex]?.name;

                if (name is null || name == string.Empty)
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
        /// <param name="nodes">List of node.</param>
        /// <param name="vgoLayout">A vgo layout.</param>
        protected virtual void CreateNodeHierarchy(List<Transform> nodes, VgoLayout vgoLayout)
        {
            if (vgoLayout.nodes is null)
            {
                throw new Exception();
            }

            var rootNode = vgoLayout.nodes[0];

            if (rootNode is null)
            {
                throw new Exception();
            }

            if (rootNode.isRoot == false)
            {
                throw new FormatException("nodes[0].isRoot: false");
            }

            for (int nodeIndex = 0; nodeIndex < nodes.Count; nodeIndex++)
            {
                VgoNode? vgoNode = vgoLayout.nodes[nodeIndex];

                if (vgoNode == null)
                {
                    continue;
                }

                if (vgoNode.children == null)
                {
                    continue;
                }

                foreach (int child in vgoNode.children)
                {
                    nodes[child].transform.SetParent(parent: nodes[nodeIndex].transform, worldPositionStays: false);
                }
            }
        }

        /// <summary>
        /// Fix node's coordinate. z-back to z-forward
        /// </summary>
        /// <param name="nodes">List of node.</param>
        protected virtual void FixCoordinate(List<Transform> nodes)
        {
            Dictionary<Transform, (Vector3, Quaternion)> globalTransformMap
                = nodes.ToDictionary(x => x, x => (x.position, x.rotation));

            Transform root = nodes[0];

            foreach (Transform transform in root.Traverse())
            {
                var (position, rotation) = globalTransformMap[transform];

                transform.SetPositionAndRotation(position.ReverseZ(), rotation.ReverseZ());
            }
        }

        /// <summary>
        /// Setup nodes.
        /// </summary>
        /// <param name="nodes">List of node.</param>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="modelAsset">A model asset.</param>
        protected virtual void SetupNodes(List<Transform> nodes, IVgoStorage vgoStorage, ModelAsset modelAsset)
        {
            if (vgoStorage.Layout.nodes is null)
            {
               return;
            }

            for (int nodeIndex = 0; nodeIndex < vgoStorage.Layout.nodes.Count; nodeIndex++)
            {
                SetupNode(nodes, nodeIndex, vgoStorage.Layout, vgoStorage.GeometryCoordinate, modelAsset);
            }

            // UnityEngine.Renderer
            for (int nodeIndex = 0; nodeIndex < vgoStorage.Layout.nodes.Count; nodeIndex++)
            {
                var vgoNode = vgoStorage.Layout.nodes[nodeIndex];

                if (vgoNode is null)
                {
                    continue;
                }

                if (vgoStorage.IsSpecVersion_2_4_orLower)
                {
                    if (vgoNode.mesh >= 0)
                    {
                        AttachMeshAndRenderer(nodes, nodeIndex, vgoStorage, modelAsset, _Option.ShowMesh, _Option.UpdateWhenOffscreen);
                    }
                }
                else
                {
                    if (vgoNode.meshRenderer != null)
                    {
                        AttachMeshAndRenderer(nodes, nodeIndex, vgoStorage, modelAsset, _Option.ShowMesh, _Option.UpdateWhenOffscreen);
                    }
                }
            }

            modelAsset.ColliderList = CreateUnityColliderList(nodes);

            // UnigyEngine.Cloth
            for (int nodeIndex = 0; nodeIndex < vgoStorage.Layout.nodes.Count; nodeIndex++)
            {
                SetupNodeCloth(nodes, nodeIndex, vgoStorage, modelAsset.ColliderList);
            }

            // UnigyEngine.VgoSpringBone
            for (int nodeIndex = 0; nodeIndex < vgoStorage.Layout.nodes.Count; nodeIndex++)
            {
                SetupNodeSpringBone(nodes, nodeIndex, vgoStorage.Layout, vgoStorage.GeometryCoordinate, modelAsset);
            }
        }

        /// <summary>
        /// Setup a node.
        /// </summary>
        /// <param name="nodes">List of node.</param>
        /// <param name="nodeIndex">The index of layout.nodes.</param>
        /// <param name="vgoLayout">A vgo layout.</param>
        /// <param name="geometryCoordinate"></param>
        /// <param name="modelAsset">A model asset.</param>
        protected virtual void SetupNode(List<Transform> nodes, int nodeIndex, VgoLayout vgoLayout, VgoGeometryCoordinate geometryCoordinate, ModelAsset modelAsset)
        {
            if (vgoLayout.nodes is null)
            {
                return;
            }

            // GameObject
            GameObject go = nodes[nodeIndex].gameObject;

            VgoNode? vgoNode = vgoLayout.nodes[nodeIndex];

            if (vgoNode is null)
            {
                return;
            }

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
                    animator.avatar = VgoAvatarConverter.CreateHumanAvatar(vgoNode.animator.humanAvatar, go, nodes);

                    AvatarConfiguration avatarConfiguration = CreateAvatarConfiguration(vgoNode.animator.humanAvatar);

                    modelAsset.Avatar = animator.avatar;

                    modelAsset.ScriptableObjectList.Add(avatarConfiguration);
                }
            }

            // Animation
            if (vgoNode.animation != null)
            {
                Animation animation = go.AddComponent<Animation>();

                try
                {
                    VgoAnimationConverter.SetComponentValue(animation, vgoNode.animation, modelAsset.AnimationClipList, geometryCoordinate);

                    if (animation.enabled &&
                        animation.playAutomatically &&
                        animation.isPlaying == false)
                    {
                        animation.Play();
                    }
                }
                catch
                {
                    //
                }
            }

            // Rigidbody
            if (vgoNode.rigidbody != null)
            {
                Rigidbody rigidbody = go.AddComponent<Rigidbody>(); ;

                VgoRigidbodyConverter.SetComponentValue(rigidbody, vgoNode.rigidbody);
            }

            // Colliders
            if ((vgoNode.colliders != null) && (vgoLayout.colliders != null))
            {
                foreach (int colliderIndex in vgoNode.colliders)
                {
                    VgoCollider? vgoCollider = vgoLayout.colliders.GetNullableValueOrDefault(colliderIndex);

                    if (vgoCollider is null)
                    {
                        continue;
                    }

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

                    VgoColliderConverter.SetComponentValue(collider, vgoCollider, geometryCoordinate);
                }
            }

            // SpringBoneCollider
            try
            {
                if (vgoNode.springBoneColliderGroup != -1 &&
                    vgoLayout.springBoneInfo?.colliderGroups != null &&
                    vgoLayout.springBoneInfo.colliderGroups.TryGetValue(vgoNode.springBoneColliderGroup, out var layoutSpringBoneColliderGroup) &&
                    layoutSpringBoneColliderGroup != null)
                {
                    var component = go.AddComponent<VgoSpringBone.VgoSpringBoneColliderGroup>();

                    if ((layoutSpringBoneColliderGroup.colliders != null) && (layoutSpringBoneColliderGroup.colliders.Length > 0))
                    {
                        component.colliders = new VgoSpringBone.SpringBoneCollider[layoutSpringBoneColliderGroup.colliders.Length];

                        for (int index = 0; index < layoutSpringBoneColliderGroup.colliders.Length; index++)
                        {
                            VgoSpringBoneCollider? layoutCollider = layoutSpringBoneColliderGroup.colliders[index];

                            if (layoutCollider == null)
                            {
                                continue;
                            }

                            component.colliders[index] = new VgoSpringBone.SpringBoneCollider
                            {
                                colliderType = (VgoSpringBone.SpringBoneColliderType)layoutCollider.colliderType,
                                offset = layoutCollider.offset.ToUnityVector3(geometryCoordinate),
                                radius = layoutCollider.radius.GetValueOrDefault(0.0f),
                            };
                        }
                    }

                    component.gizmoColor = layoutSpringBoneColliderGroup.gizmoColor.ToUnityColor();

                    if (modelAsset.SpringBoneColliderGroupArray != null)
                    {
                        modelAsset.SpringBoneColliderGroupArray[vgoNode.springBoneColliderGroup] = component;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }

            // Light
            try
            {
                if (vgoNode.light != -1 &&
                    vgoLayout.lights != null &&
                    vgoLayout.lights.TryGetValue(vgoNode.light, out var vgoLight) &&
                    vgoLight != null)
                {
                    Light light = go.AddComponent<Light>();

                    VgoLightConverter.SetComponentValue(light, vgoLight);
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }

            // ParticleSystem
            try
            {
                if (vgoNode.particle != -1 &&
                    vgoLayout.particles != null &&
                    vgoLayout.particles.TryGetValue(vgoNode.particle, out var vgoParticleSystem) &&
                    vgoParticleSystem != null)
                {
                    _ParticleSystemImporter.AddComponent(go, vgoParticleSystem, geometryCoordinate, modelAsset.MaterialList, modelAsset.Texture2dList);
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }

            // Skybox
            if (vgoNode.skybox != null)
            {
                var skybox = go.AddComponent<Skybox>();

                skybox.enabled = vgoNode.skybox.enabled;

                if (modelAsset.MaterialList != null &&
                    modelAsset.MaterialList.TryGetValue(vgoNode.skybox.materialIndex, out var skyboxMaterial) &&
                    skyboxMaterial != null)
                {
                    skybox.material = skyboxMaterial;
                }
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
        }

        #endregion

        #region layout.springBoneInfo

        /// <summary>
        /// Setup a node spring bone.
        /// </summary>
        /// <param name="nodes">List of node.</param>
        /// <param name="nodeIndex">The index of layout.nodes.</param>
        /// <param name="vgoLayout">A vgo layout.</param>
        /// <param name="geometryCoordinate"></param>
        /// <param name="modelAsset">A model asset.</param>
        protected virtual void SetupNodeSpringBone(List<Transform> nodes, int nodeIndex, VgoLayout vgoLayout, VgoGeometryCoordinate geometryCoordinate, ModelAsset modelAsset)
        {
            if (vgoLayout.nodes is null)
            {
                return;
            }

            if (vgoLayout.springBoneInfo?.springBoneGroups == null)
            {
                return;
            }

            VgoNode? vgoNode = vgoLayout.nodes[nodeIndex];

            if (vgoNode == null)
            {
                return;
            }

            if (vgoNode.springBoneGroups == null)
            {
                return;
            }

            GameObject go = nodes[nodeIndex].gameObject;

            foreach (var groupIndex in vgoNode.springBoneGroups)
            {
                if (vgoLayout.springBoneInfo.springBoneGroups.TryGetValue(groupIndex, out var layoutSpringBoneGroup) == false)
                {
                    continue;
                }

                if (layoutSpringBoneGroup == null)
                {
                    continue;
                }

                var component = go.AddComponent<VgoSpringBone.VgoSpringBoneGroup>();

                // @notice component.name indicates gameObject.name
                //component.name = string.IsNullOrEmpty(layoutSpringBoneGroup.name) ? go.name : layoutSpringBoneGroup.name;
                component.enabled = layoutSpringBoneGroup.enabled;
                component.comment = layoutSpringBoneGroup.comment;
                component.dragForce = layoutSpringBoneGroup.dragForce.SafeValue(0.0f, 1.0f, 0.0f);
                component.stiffnessForce = layoutSpringBoneGroup.stiffnessForce.SafeValue(0.0f, 4.0f, 1.0f);
                component.gravityDirection = layoutSpringBoneGroup.gravityDirection.ToUnityVector3(geometryCoordinate);
                component.gravityPower = layoutSpringBoneGroup.gravityPower.SafeValue(0.0f, 2.0f, 1.0f);
                component.hitRadius = layoutSpringBoneGroup.hitRadius.SafeValue(0.0f, 0.5f, 0.1f);
                component.drawGizmo = layoutSpringBoneGroup.drawGizmo;
                component.gizmoColor = layoutSpringBoneGroup.gizmoColor.ToUnityColor();

                // rootBones
                if (layoutSpringBoneGroup.rootBones != null &&
                    layoutSpringBoneGroup.rootBones.Any())
                {
                    component.rootBones = new Transform[layoutSpringBoneGroup.rootBones.Length];

                    for (int index = 0; index < layoutSpringBoneGroup.rootBones.Length; index++)
                    {
                        if (nodes.TryGetValue(layoutSpringBoneGroup.rootBones[index], out Transform rootBoneNode))
                        {
                            component.rootBones[index] = rootBoneNode;
                        }
                    }
                }

                // colliderGroups
                if (layoutSpringBoneGroup.colliderGroups != null &&
                    layoutSpringBoneGroup.colliderGroups.Any() &&
                    modelAsset.SpringBoneColliderGroupArray != null)
                {
                    component.colliderGroups = new VgoSpringBone.VgoSpringBoneColliderGroup[layoutSpringBoneGroup.colliderGroups.Length];

                    for (int index = 0; index < layoutSpringBoneGroup.colliderGroups.Length; index++)
                    {
                        if (layoutSpringBoneGroup.colliderGroups[index].IsInRangeOf(modelAsset.SpringBoneColliderGroupArray))
                        {
                            var colliderGroup = modelAsset.SpringBoneColliderGroupArray[layoutSpringBoneGroup.colliderGroups[index]];

                            component.colliderGroups[index] = colliderGroup;
                        }
                    }
                }
            }
        }

        #endregion

        #region layout.clothes

        /// <summary>
        /// Setup a node cloth.
        /// </summary>
        /// <param name="nodes">List of node.</param>
        /// <param name="nodeIndex">The index of layout.nodes.</param>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="colliderList">List of collider.</param>
        protected virtual void SetupNodeCloth(List<Transform> nodes, int nodeIndex, IVgoStorage vgoStorage, List<Collider?> colliderList)
        {
            if (vgoStorage.Layout.nodes is null)
            {
                return;
            }

            if (vgoStorage.Layout.clothes is null)
            {
                return;
            }

            VgoNode? vgoNode = vgoStorage.Layout.nodes[nodeIndex];

            if (vgoNode is null)
            {
                return;
            }

            if (vgoNode.cloth == -1)
            {
                return;
            }

            VgoCloth? vgoCloth = vgoStorage.Layout.clothes.GetNullableValueOrDefault(vgoNode.cloth);

            if (vgoCloth == null)
            {
                return;
            }

            GameObject go = nodes[nodeIndex].gameObject;

            Cloth cloth = go.AddComponent<Cloth>();

            VgoClothConverter.SetComponentValue(cloth, vgoCloth, vgoStorage.GeometryCoordinate, colliderList, vgoStorage);
        }

        #endregion

        #region Collider

        /// <summary>
        /// Create list of unity collider.
        /// </summary>
        /// <param name="nodes">List of node.</param>
        /// <returns>list of unity collider.</returns>
        /// <remarks>
        /// VgoExporter.CreateUnityColliderList is same logic.
        /// </remarks>
        protected virtual List<Collider?> CreateUnityColliderList(List<Transform> nodes)
        {
            if (nodes == null)
            {
                throw new Exception();
            }

            var colliderList = new List<Collider?>();

            for (int index = 0; index < nodes.Count; index++)
            {
                GameObject node = nodes[index].gameObject;

                if (node.TryGetComponentEx(out Collider collider))
                {
                    if ((collider is BoxCollider) ||
                        (collider is CapsuleCollider) ||
                        (collider is SphereCollider))
                    {
                        colliderList.Add(collider);
                    }
                }
            }

            return colliderList;
        }

        #endregion

        #region Renderer

        /// <summary>
        /// Attach mesh and renderer to node.
        /// </summary>
        /// <param name="nodes">List of node.</param>
        /// <param name="nodeIndex">The index of layout.node.</param>
        /// <param name="showMesh">Whether show mesh renderer.</param>
        /// <param name="updateWhenOffscreen">Whether update skinned mesh renderer when off screen.</param>
        protected virtual void AttachMeshAndRenderer(List<Transform> nodes, int nodeIndex, IVgoStorage vgoStorage, ModelAsset modelAsset, bool showMesh = true, bool updateWhenOffscreen = false)
        {
            if (vgoStorage.Layout.nodes is null)
            {
                return;
            }

            if (modelAsset.MeshAssetList is null)
            {
                return;
            }

            VgoNode? vgoNode = vgoStorage.Layout.nodes[nodeIndex];

            if (vgoNode is null)
            {
                return;
            }

            GameObject go = nodes[nodeIndex].gameObject;

            Renderer renderer;

            if (vgoStorage.IsSpecVersion_2_4_orLower)
            {
                MeshAsset meshAsset = modelAsset.MeshAssetList[vgoNode.mesh];

                Mesh mesh = meshAsset.Mesh;

                Material?[]? materials = meshAsset.Materials;

                if ((meshAsset.Mesh.blendShapeCount == 0) && (vgoNode.skin == -1))
                {
                    // without blendshape and bone skinning
                    var filter = go.AddComponent<MeshFilter>();

                    filter.sharedMesh = mesh;

                    var meshRenderer = go.AddComponent<MeshRenderer>();

                    if (materials != null)
                    {
                        meshRenderer.sharedMaterials = materials;
                    }

                    renderer = meshRenderer;
                }
                else
                {
                    var skinnedMeshRenderer = go.AddComponent<SkinnedMeshRenderer>();

                    skinnedMeshRenderer.sharedMesh = mesh;

                    if (vgoNode.skin >= 0)
                    {
                        SetupSkin(skinnedMeshRenderer, vgoNode.skin, nodes, vgoStorage);
                    }

                    skinnedMeshRenderer.sharedMaterials = materials;

                    if (updateWhenOffscreen)
                    {
                        skinnedMeshRenderer.updateWhenOffscreen = true;
                    }

                    renderer = skinnedMeshRenderer;
                }

                if ((meshAsset.BlendShapeConfig != null) &&
                    (meshAsset.BlendShapeConfig.Kind != VgoBlendShapeKind.None))
                {
                    var vgoBlendShape = go.AddComponent<VgoBlendShape>();

                    BlendShapeConfiguration blendShapeConfiguration = ScriptableObject.CreateInstance<BlendShapeConfiguration>();

                    blendShapeConfiguration.name = meshAsset.BlendShapeConfig.Name;
                    blendShapeConfiguration.kind = meshAsset.BlendShapeConfig.Kind;
                    blendShapeConfiguration.faceParts = meshAsset.BlendShapeConfig.FaceParts;
                    blendShapeConfiguration.blinks = meshAsset.BlendShapeConfig.Blinks;
                    blendShapeConfiguration.visemes = meshAsset.BlendShapeConfig.Visemes;
                    blendShapeConfiguration.presets = meshAsset.BlendShapeConfig.Presets;

                    vgoBlendShape.BlendShapeConfiguration = blendShapeConfiguration;

                    modelAsset.ScriptableObjectList.Add(blendShapeConfiguration);
                }
            }
            else
            {
                if (vgoNode.meshRenderer is null)
                {
                    return;
                }

                VgoMeshRenderer vgoMeshRenderer = vgoNode.meshRenderer;

                MeshAsset meshAsset = modelAsset.MeshAssetList[vgoMeshRenderer.mesh];

                Mesh mesh = meshAsset.Mesh;

                Material?[]? materials = null;

                if (vgoMeshRenderer.materials != null &&
                    vgoMeshRenderer.materials.Any() &&
                    modelAsset.MaterialList != null &&
                    modelAsset.MaterialList.Any())
                {
                    materials = vgoMeshRenderer.materials
                        .Select(materialIndex => modelAsset.MaterialList[materialIndex])
                        .ToArray();
                }

                if (vgoNode.particle >= 0)
                {
                    if (go.TryGetComponentEx(out ParticleSystemRenderer particleSystemRenderer))
                    {
                        if (particleSystemRenderer.renderMode == UnityEngine.ParticleSystemRenderMode.Mesh)
                        {
                            particleSystemRenderer.mesh = mesh;

                            if (materials != null)
                            {
                                particleSystemRenderer.sharedMaterials = materials;
                            }
                        }
                    }

                    return;
                }
                else if (vgoNode.skin >= 0)
                {
                    var skinnedMeshRenderer = go.AddComponent<SkinnedMeshRenderer>();

                    //skinnedMeshRenderer.name = vgoMeshRenderer.name;
                    skinnedMeshRenderer.enabled = vgoMeshRenderer.enabled;
                    skinnedMeshRenderer.sharedMesh = mesh;

                    SetupSkin(skinnedMeshRenderer, vgoNode.skin, nodes, vgoStorage);

                    if (materials != null)
                    {
                        skinnedMeshRenderer.sharedMaterials = materials;
                    }

                    if (updateWhenOffscreen)
                    {
                        skinnedMeshRenderer.updateWhenOffscreen = true;
                    }

                    renderer = skinnedMeshRenderer;
                }
                else
                {
                    // without blendshape and bone skinning
                    var filter = go.AddComponent<MeshFilter>();

                    filter.sharedMesh = mesh;

                    var meshRenderer = go.AddComponent<MeshRenderer>();

                    //meshRenderer.name = vgoMeshRenderer.name;
                    meshRenderer.enabled = vgoMeshRenderer.enabled;

                    if (materials != null)
                    {
                        meshRenderer.sharedMaterials = materials;
                    }

                    renderer = meshRenderer;
                }

                if ((vgoMeshRenderer.blendShapeKind != null) &&
                    (meshAsset.BlendShapeConfig != null))
                {
                    var vgoBlendShape = go.AddComponent<VgoBlendShape>();

                    BlendShapeConfiguration blendShapeConfiguration = ScriptableObject.CreateInstance<BlendShapeConfiguration>();

                    blendShapeConfiguration.name = meshAsset.BlendShapeConfig.Name;

                    blendShapeConfiguration.kind = vgoMeshRenderer.blendShapeKind.Value;

                    blendShapeConfiguration.faceParts = meshAsset.BlendShapeConfig.FaceParts;
                    blendShapeConfiguration.blinks = meshAsset.BlendShapeConfig.Blinks;
                    blendShapeConfiguration.visemes = meshAsset.BlendShapeConfig.Visemes;

                    if (vgoMeshRenderer.blendShapePesets != null &&
                        vgoMeshRenderer.blendShapePesets.Any())
                    {
                        blendShapeConfiguration.presets.AddRange(vgoMeshRenderer.blendShapePesets);
                    }

                    vgoBlendShape.BlendShapeConfiguration = blendShapeConfiguration;

                    modelAsset.ScriptableObjectList.Add(blendShapeConfiguration);
                }
            }

            if (showMesh == false)
            {
                renderer.enabled = false;
            }
        }

        #endregion

        #region layout.skins

        /// <summary>
        /// Set up a skin.
        /// </summary>
        /// <param name="skinnedMeshRenderer">A skinnedned mesh renderer.</param>
        /// <param name="skinIndex">The index of gltf.skin.</param>
        /// <param name="nodes">List of node.</param>
        /// <param name="vgoStorage">A vgo storage.</param>
        protected virtual void SetupSkin(SkinnedMeshRenderer skinnedMeshRenderer, int skinIndex, List<Transform> nodes, IVgoStorage vgoStorage)
        {
            if (skinnedMeshRenderer.sharedMesh == null)
            {
                throw new Exception();
            }

            if (vgoStorage.Layout.skins == null)
            {
                throw new Exception();
            }

            if (vgoStorage.Layout.skins.TryGetValue(skinIndex, out var vgoSkin) == false)
            {
                throw new IndexOutOfRangeException($"skins[{skinIndex}] is out of the range.");
            }

            if (vgoSkin is null)
            {
                return;
            }

            Mesh mesh = skinnedMeshRenderer.sharedMesh;

            // calculate internal values(boundingBox etc...) when sharedMesh assigned ?
            skinnedMeshRenderer.sharedMesh = null;

            if (vgoSkin.joints != null && vgoSkin.joints.Any())
            {
                // have bones

                Transform?[] joints = new Transform?[vgoSkin.joints.Length];

                for (int jointIndex = 0; jointIndex < vgoSkin.joints.Length; jointIndex++)
                {
                    int vgoSkinJointNodeIndex = vgoSkin.joints[jointIndex];

                    if (vgoSkinJointNodeIndex.IsInRangeOf(nodes))
                    {
                        joints[jointIndex] = nodes[vgoSkinJointNodeIndex];
                    }
                    else
                    {
                        Debug.LogWarning($"skins[{skinIndex}].joints[{jointIndex}] is out of the range.");
                        //throw new IndexOutOfRangeException($"skins[{skinIndex}].joints[{jointIndex}] is out of the range.");
                    }
                }

                skinnedMeshRenderer.bones = joints;

                if (vgoSkin.inverseBindMatrices >= 0)
                {
                    if (vgoStorage.GeometryCoordinate == VgoGeometryCoordinate.RightHanded)
                    {
                        ReadOnlySpan<Matrix4x4> matrixSpan = vgoStorage.GetAccessorSpan<Matrix4x4>(vgoSkin.inverseBindMatrices);

                        var bindPoses = new Matrix4x4[matrixSpan.Length];

                        for (int i = 0; i < matrixSpan.Length; i++)
                        {
                            bindPoses[i] = matrixSpan[i].ReverseZ();
                        }

                        mesh.bindposes = bindPoses;
                    }
                    else
                    {
                        mesh.bindposes = vgoStorage.GetAccessorArrayData<Matrix4x4>(vgoSkin.inverseBindMatrices);
                    }
                }
                else
                {
                    // calc default matrices
                    // https://docs.unity3d.com/ScriptReference/Mesh-bindposes.html

                    Transform meshCoords = skinnedMeshRenderer.transform; // ?

                    Matrix4x4[] calculatedBindPoses = new Matrix4x4[joints.Length];

                    for (int index = 0; index < joints.Length; index++)
                    {
                        Transform? jointTransform = joints[index];

                        if (jointTransform == null)
                        {
                            continue;
                        }

                        calculatedBindPoses[index] = jointTransform.worldToLocalMatrix * meshCoords.localToWorldMatrix;
                    }

                    mesh.bindposes = calculatedBindPoses;
                }
            }

            skinnedMeshRenderer.sharedMesh = mesh;

            if (vgoSkin.skeleton.IsInRangeOf(nodes))
            {
                skinnedMeshRenderer.rootBone = nodes[vgoSkin.skeleton];
            }
        }

        #endregion

        #region layout.animationClips

        /// <summary>
        /// Create animation clip assets.
        /// </summary>
        /// <param name="vgoLayout">A vgo layout.</param>
        /// <param name="geometryCoordinate"></param>
        /// <returns>List of unity animation clip.</returns>
        protected virtual List<AnimationClip?> CreateAnimationClipAssets(VgoLayout vgoLayout, VgoGeometryCoordinate geometryCoordinate)
        {
            var animationClipList = new List<AnimationClip?>();

            if ((vgoLayout.animationClips == null) || (vgoLayout.animationClips.Any() == false))
            {
                return animationClipList;
            }

            for (int animationClipIndex = 0; animationClipIndex < vgoLayout.animationClips.Count; animationClipIndex++)
            {
                VgoAnimationClip? vgoAnimationClip = vgoLayout.animationClips[animationClipIndex];

                AnimationClip? animationClip = VgoAnimationClipConverter.CreateAnimationClipOrDefault(vgoAnimationClip, geometryCoordinate);

                animationClipList.Add(animationClip);
            }

            return animationClipList;
        }

        #endregion

        #region Asset Info

        /// <summary>
        /// Setup a asset info to root GameObject.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="modelAsset">A model asset.</param>
        protected virtual void SetupAssetInfo(IVgoStorage vgoStorage, ModelAsset modelAsset)
        {
            if (vgoStorage.AssetInfo == null)
            {
                return;
            }

            if (modelAsset.Root == null)
            {
                return;
            }

            VgoAssetInfo vgoAssetInfo = vgoStorage.AssetInfo;

            if (vgoAssetInfo.right != null)
            {
                VgoRight vgoRightComponent = modelAsset.Root.AddComponent<VgoRight>();

                vgoRightComponent.Right = new NewtonVgo.VgoRight(vgoAssetInfo.right);
            }

            if (vgoAssetInfo.generator != null)
            {
                VgoGenerator vgoGeneratorComponent = modelAsset.Root.AddComponent<VgoGenerator>();

                vgoGeneratorComponent.GeneratorInfo = new VgoGeneratorInfo(vgoAssetInfo.generator);
            }
        }

        #endregion

        #region Skybox

        /// <summary>
        /// Reflect VGO skybox to Camera skybox.
        /// </summary>
        /// <param name="camera">A scene main camera.</param>
        /// <param name="modelAsset">A model asset.</param>
        public virtual void ReflectSkybox(Camera camera, ModelAsset modelAsset)
        {
            if (modelAsset.Root == null)
            {
                return;
            }

            var vgoSkybox = modelAsset.Root.GetComponentInChildren<Skybox>(includeInactive: false);

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
        protected virtual AvatarConfiguration CreateAvatarConfiguration(VgoHumanAvatar vgoHumanAvatar)
        {
            var avatarConfiguration = ScriptableObject.CreateInstance<AvatarConfiguration>();

            avatarConfiguration.name = vgoHumanAvatar.name;
            avatarConfiguration.humanBones = vgoHumanAvatar.humanBones
                .Where(x => x != null)
                .Select(x => x!)
                .ToList();

            return avatarConfiguration;
        }

        /// <summary>
        /// Get vgo node transforms.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <returns>An array of matrix.</returns>
        protected virtual System.Numerics.Matrix4x4[] GetVgoNodeTransforms(IVgoStorage vgoStorage)
        {
            if (vgoStorage.ResourceAccessors.Where(x => x.kind == VgoResourceAccessorKind.NodeTransform).Count() != 1)
            {
                throw new Exception();
            }

            if (vgoStorage.Layout.nodes is null)
            {
                throw new Exception();
            }

            VgoResourceAccessor accessor = vgoStorage.ResourceAccessors.Where(x => x.kind == VgoResourceAccessorKind.NodeTransform).First();

            ArraySegment<byte> transformBytes = vgoStorage.GetAccessorBytes(accessor);

            var transforms = new System.Numerics.Matrix4x4[vgoStorage.Layout.nodes.Count];

            transformBytes.MarshalCopyTo(transforms);

            return transforms;
        }

        #endregion
    }
}
