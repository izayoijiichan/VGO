// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : VgoImporter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
    //
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
    using Cysharp.Threading.Tasks;
#else
    using System.Threading.Tasks;
#endif

    using NewtonVgo;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using UnityEngine;
    using UniVgo2.Converters;

    /// <summary>
    /// VGO Importer
    /// </summary>
    public partial class VgoImporter
    {
        #region Protected Methods (Load)

        /// <summary>
        /// Load a 3D model from the specified vgo storage.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <returns>A vgo model asset.</returns>
        protected virtual VgoModelAsset LoadInternal(in IVgoStorage vgoStorage)
        {
            var vgoModelAsset = new VgoModelAsset();

            vgoModelAsset.Layout = vgoStorage.Layout;

            // UnityEngine.Texture
            vgoModelAsset.TextureList = _TextureImporter.CreateTextureAssets(vgoStorage);

            // UnityEngine.Material
            vgoModelAsset.MaterialList = CreateMaterialAssets(vgoStorage, vgoModelAsset.TextureList);

            // UnityEngine.Mesh
            if (vgoStorage.IsSpecVersion_2_4_orLower)
            {
                vgoModelAsset.MeshAssetList = _MeshImporter.CreateMeshAssets(vgoStorage, vgoModelAsset.MaterialList);
            }
            else
            {
                vgoModelAsset.MeshAssetList = _MeshImporter.CreateMeshAssets(vgoStorage);
            }

            // UnityEngine.AnimationClip
            vgoModelAsset.AnimationClipList = CreateAnimationClipAssets(vgoStorage.Layout, vgoStorage.GeometryCoordinate);

            // UnityEngine.VgoSpringBoneColliderGroup
            vgoModelAsset.SpringBoneColliderGroupArray = CreateSpringBoneColliderGroupArray(vgoStorage.Layout);

            // UnityEngine.GameObject
            List<Transform> nodes = CreateNodes(vgoStorage);

            CreateNodeHierarchy(nodes, vgoStorage.Layout);

            vgoModelAsset.Root = nodes[0].gameObject;

            if (vgoStorage.GeometryCoordinate == VgoGeometryCoordinate.RightHanded)
            {
                FixCoordinate(nodes);
            }

            SetupNodes(nodes, vgoStorage, vgoModelAsset);

            SetupAssetInfo(vgoStorage, vgoModelAsset);

            return vgoModelAsset;
        }

        /// <summary>
        /// Load a 3D model from the specified vgo storage.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A vgo model asset.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        protected virtual async Awaitable<VgoModelAsset> LoadInternalAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken)
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        protected virtual async UniTask<VgoModelAsset> LoadInternalAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken)
#else
        protected virtual async Task<VgoModelAsset> LoadInternalAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken)
#endif
        {
            var vgoModelAsset = new VgoModelAsset();

            vgoModelAsset.Layout = vgoStorage.Layout;

            // UnityEngine.Texture
            //vgoModelAsset.TextureList = _TextureImporter.CreateTextureAssets(vgoStorage);
            //vgoModelAsset.TextureList = await _TextureImporter.CreateTextureAssetsAsync(vgoStorage, cancellationToken);
            vgoModelAsset.TextureList = await _TextureImporter.CreateTextureAssetsParallelAsync(vgoStorage, cancellationToken);

            // UnityEngine.Material
            vgoModelAsset.MaterialList = CreateMaterialAssets(vgoStorage, vgoModelAsset.TextureList);

            // UnityEngine.Mesh
            if (vgoStorage.IsSpecVersion_2_4_orLower)
            {
                //vgoModelAsset.MeshAssetList = _MeshImporter.CreateMeshAssets(vgoStorage, modelAsset.MaterialList);
                vgoModelAsset.MeshAssetList = await _MeshImporter.CreateMeshAssetsAsync(vgoStorage, vgoModelAsset.MaterialList, cancellationToken);
                //vgoModelAsset.MeshAssetList = await _MeshImporter.CreateMeshAssetsParallelAsync(vgoStorage, modelAsset.MaterialList, cancellationToken);
            }
            else
            {
                //vgoModelAsset.MeshAssetList = _MeshImporter.CreateMeshAssets(vgoStorage);
                vgoModelAsset.MeshAssetList = await _MeshImporter.CreateMeshAssetsAsync(vgoStorage, null, cancellationToken);
                //vgoModelAsset.MeshAssetList = await _MeshImporter.CreateMeshAssetsParallelAsync(vgoStorage, null, cancellationToken);
            }

            // UnityEngine.AnimationClip
            vgoModelAsset.AnimationClipList = CreateAnimationClipAssets(vgoStorage.Layout, vgoStorage.GeometryCoordinate);

            // UnityEngine.VgoSpringBoneColliderGroup
            vgoModelAsset.SpringBoneColliderGroupArray = CreateSpringBoneColliderGroupArray(vgoStorage.Layout);

            // UnityEngine.GameObject
            List<Transform> nodes = CreateNodes(vgoStorage);

            CreateNodeHierarchy(nodes, vgoStorage.Layout);

            vgoModelAsset.Root = nodes[0].gameObject;

            if (vgoStorage.GeometryCoordinate == VgoGeometryCoordinate.RightHanded)
            {
                FixCoordinate(nodes);
            }

            SetupNodes(nodes, vgoStorage, vgoModelAsset);

            SetupAssetInfo(vgoStorage, vgoModelAsset);

#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
            //await Awaitable.NextFrameAsync(cancellationToken);

            return vgoModelAsset;
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
            return await UniTask.FromResult(vgoModelAsset);
#else
            return await Task.FromResult(vgoModelAsset);
#endif
        }

        #endregion

        #region Protected Methods (Extract)

        /// <summary>
        /// Extract a 3D model asset from the specified vgo storage.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <returns>A vgo model asset.</returns>
        protected virtual VgoModelAsset ExtractInternal(in IVgoStorage vgoStorage)
        {
            var vgoModelAsset = new VgoModelAsset();

            vgoModelAsset.Layout = vgoStorage.Layout;

            // UnityEngine.Texture
            vgoModelAsset.TextureList = _TextureImporter.CreateTextureAssets(vgoStorage);

            // UnityEngine.Material
            vgoModelAsset.MaterialList = CreateMaterialAssets(vgoStorage, vgoModelAsset.TextureList);

            return vgoModelAsset;
        }

        /// <summary>
        /// Extract a 3D model asset from the specified vgo storage.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A vgo model asset.</returns>
#if UNITY_2023_1_OR_NEWER && UNIVGO_USE_UNITY_AWAITABLE
        protected virtual async Awaitable<VgoModelAsset> ExtractInternalAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken)
#elif CYSHARP_UNITASK_2_OR_NEWER && UNIVGO_USE_UNITASK
        protected virtual async UniTask<VgoModelAsset> ExtractInternalAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken)
#else
        protected virtual async Task<VgoModelAsset> ExtractInternalAsync(IVgoStorage vgoStorage, CancellationToken cancellationToken)
#endif
        {
            var vgoModelAsset = new VgoModelAsset();

            vgoModelAsset.Layout = vgoStorage.Layout;

            // UnityEngine.Texture
            //vgoModelAsset.TextureList = _TextureImporter.CreateTextureAssets(vgoStorage);
            //vgoModelAsset.TextureList = await _TextureImporter.CreateTextureAssetsAsync(vgoStorage, cancellationToken);
            vgoModelAsset.TextureList = await _TextureImporter.CreateTextureAssetsParallelAsync(vgoStorage, cancellationToken);

            // UnityEngine.Material
            vgoModelAsset.MaterialList = CreateMaterialAssets(vgoStorage, vgoModelAsset.TextureList);

            return vgoModelAsset;
        }

        #endregion

        #region layout.materials

        /// <summary>
        /// Create material assets.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="textureList">List of unity texture.</param>
        /// <returns>List of unity material.</returns>
        protected virtual List<Material?> CreateMaterialAssets(in IVgoStorage vgoStorage, in List<Texture?> textureList)
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

                    continue;
                }

                try
                {
                    Material material = _MaterialImporter.CreateMaterialAsset(vgoMaterial, textureList);

                    materialList.Add(material);
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);

                    materialList.Add(null);
                }
            }

            return materialList;
        }

        #endregion

        #region layout.springBoneInfo

        /// <summary>
        /// Create vgo spring bone collider groups.
        /// </summary>
        /// <param name="vgoLayout">A vgo layout.</param>
        /// <returns>An array of vgo spring bone collider group.</returns>
        protected virtual VgoSpringBone.VgoSpringBoneColliderGroup[]? CreateSpringBoneColliderGroupArray(in VgoLayout vgoLayout)
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
        protected virtual List<Transform> CreateNodes(in IVgoStorage vgoStorage)
        {
            if (vgoStorage.Layout.nodes is null)
            {
#if NET_STANDARD_2_1
                ThrowHelper.ThrowException();
#else
                throw new Exception();
#endif
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

                go.transform.localPosition = matrix.ExtractTranslation();
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
        protected virtual void CreateNodeHierarchy(List<Transform> nodes, in VgoLayout vgoLayout)
        {
            if (vgoLayout.nodes is null)
            {
                ThrowHelper.ThrowException();

                return;
            }

            var rootNode = vgoLayout.nodes[0];

            if (rootNode is null)
            {
                ThrowHelper.ThrowException();

                return;
            }

            if (rootNode.isRoot == false)
            {
                ThrowHelper.ThrowFormatException("nodes[0].isRoot: false");
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
        /// <param name="vgoModelAsset">A vgo model asset.</param>
        protected virtual void SetupNodes(List<Transform> nodes, in IVgoStorage vgoStorage, VgoModelAsset vgoModelAsset)
        {
            if (vgoStorage.Layout.nodes is null)
            {
               return;
            }

            for (int nodeIndex = 0; nodeIndex < vgoStorage.Layout.nodes.Count; nodeIndex++)
            {
                SetupNode(nodes, nodeIndex, vgoStorage.Layout, vgoStorage.GeometryCoordinate, vgoModelAsset);
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
                        AttachMeshAndRenderer(nodes, nodeIndex, vgoStorage, vgoModelAsset, _Option.ShowMesh, _Option.UpdateWhenOffscreen);
                    }
                }
                else
                {
                    if (vgoNode.meshRenderer != null)
                    {
                        AttachMeshAndRenderer(nodes, nodeIndex, vgoStorage, vgoModelAsset, _Option.ShowMesh, _Option.UpdateWhenOffscreen);
                    }
                }
            }

            vgoModelAsset.ColliderList = CreateUnityColliderList(nodes);

            // UnityEngine.Cloth
            for (int nodeIndex = 0; nodeIndex < vgoStorage.Layout.nodes.Count; nodeIndex++)
            {
                SetupNodeCloth(nodes, nodeIndex, vgoStorage, vgoModelAsset.ColliderList);
            }

            // UnityEngine.VgoSpringBone
            for (int nodeIndex = 0; nodeIndex < vgoStorage.Layout.nodes.Count; nodeIndex++)
            {
                SetupNodeSpringBone(nodes, nodeIndex, vgoStorage.Layout, vgoStorage.GeometryCoordinate, vgoModelAsset);
            }
        }

        /// <summary>
        /// Setup a node.
        /// </summary>
        /// <param name="nodes">List of node.</param>
        /// <param name="nodeIndex">The index of layout.nodes.</param>
        /// <param name="vgoLayout">A vgo layout.</param>
        /// <param name="geometryCoordinate"></param>
        /// <param name="vgoModelAsset">A vgo model asset.</param>
        protected virtual void SetupNode(
            List<Transform> nodes,
            in int nodeIndex,
            in VgoLayout vgoLayout,
            in VgoGeometryCoordinate geometryCoordinate, 
            VgoModelAsset vgoModelAsset)
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

                    vgoModelAsset.Avatar = animator.avatar;

                    vgoModelAsset.ScriptableObjectList.Add(avatarConfiguration);
                }
            }

            // Animation
            if (vgoNode.animation != null)
            {
                Animation animation = go.AddComponent<Animation>();

                try
                {
                    VgoAnimationConverter.SetComponentValue(animation, vgoNode.animation, vgoModelAsset.AnimationClipList, geometryCoordinate);

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

                    if (vgoModelAsset.SpringBoneColliderGroupArray != null)
                    {
                        vgoModelAsset.SpringBoneColliderGroupArray[vgoNode.springBoneColliderGroup] = component;
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
                    _ParticleSystemImporter.AddComponent(
                        go,
                        vgoParticleSystem,
                        geometryCoordinate,
                        vgoModelAsset.MaterialList,
                        vgoModelAsset.TextureList,
                        _Option.ForceSetRuntimeParticleDuration);
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

                if (vgoModelAsset.MaterialList != null &&
                    vgoModelAsset.MaterialList.TryGetValue(vgoNode.skybox.materialIndex, out var skyboxMaterial) &&
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
        /// <param name="vgoModelAsset">A vgo model asset.</param>
        protected virtual void SetupNodeSpringBone(
            List<Transform> nodes,
            in int nodeIndex,
            in VgoLayout vgoLayout,
            in VgoGeometryCoordinate geometryCoordinate,
            VgoModelAsset vgoModelAsset)
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
                    vgoModelAsset.SpringBoneColliderGroupArray != null)
                {
                    component.colliderGroups = new VgoSpringBone.VgoSpringBoneColliderGroup[layoutSpringBoneGroup.colliderGroups.Length];

                    for (int index = 0; index < layoutSpringBoneGroup.colliderGroups.Length; index++)
                    {
                        if (layoutSpringBoneGroup.colliderGroups[index].IsInRangeOf(vgoModelAsset.SpringBoneColliderGroupArray))
                        {
                            var colliderGroup = vgoModelAsset.SpringBoneColliderGroupArray[layoutSpringBoneGroup.colliderGroups[index]];

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
        protected virtual void SetupNodeCloth(List<Transform> nodes, in int nodeIndex, in IVgoStorage vgoStorage, in List<Collider?> colliderList)
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
        protected virtual List<Collider?> CreateUnityColliderList(in List<Transform> nodes)
        {
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
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="vgoModelAsset">A vgo model asset.</param>
        /// <param name="showMesh">Whether show mesh renderer.</param>
        /// <param name="updateWhenOffscreen">Whether update skinned mesh renderer when off screen.</param>
        protected virtual void AttachMeshAndRenderer(
            List<Transform> nodes,
            in int nodeIndex,
            in IVgoStorage vgoStorage,
            VgoModelAsset vgoModelAsset,
#if UNITY_2021_2_OR_NEWER
            in bool showMesh = true,
            in bool updateWhenOffscreen = false
#else
            bool showMesh = true,
            bool updateWhenOffscreen = false
#endif
            )
        {
            if (vgoStorage.Layout.nodes is null)
            {
                return;
            }

            if (vgoModelAsset.MeshAssetList is null)
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
                MeshAsset meshAsset = vgoModelAsset.MeshAssetList[vgoNode.mesh];

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

                    blendShapeConfiguration.Name = meshAsset.BlendShapeConfig.Name ?? string.Empty;
                    blendShapeConfiguration.Kind = meshAsset.BlendShapeConfig.Kind;
                    blendShapeConfiguration.FaceParts = meshAsset.BlendShapeConfig.FaceParts;
                    blendShapeConfiguration.Blinks = meshAsset.BlendShapeConfig.Blinks;
                    blendShapeConfiguration.Visemes = meshAsset.BlendShapeConfig.Visemes;
                    blendShapeConfiguration.Presets = meshAsset.BlendShapeConfig.Presets;

                    vgoBlendShape.BlendShapeConfiguration = blendShapeConfiguration;

                    vgoModelAsset.ScriptableObjectList.Add(blendShapeConfiguration);
                }
            }
            else
            {
                if (vgoNode.meshRenderer is null)
                {
                    return;
                }

                VgoMeshRenderer vgoMeshRenderer = vgoNode.meshRenderer;

                MeshAsset meshAsset = vgoModelAsset.MeshAssetList[vgoMeshRenderer.mesh];

                Mesh mesh = meshAsset.Mesh;

                Material?[]? materials = null;

                if (vgoMeshRenderer.materials != null &&
                    vgoMeshRenderer.materials.Any() &&
                    vgoModelAsset.MaterialList != null &&
                    vgoModelAsset.MaterialList.Any())
                {
                    var materialList = vgoModelAsset.MaterialList;

                    materials = vgoMeshRenderer.materials
                        .Select(materialIndex => materialList[materialIndex])
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

                    blendShapeConfiguration.Name = meshAsset.BlendShapeConfig.Name ?? string.Empty;

                    blendShapeConfiguration.Kind = vgoMeshRenderer.blendShapeKind.Value;

                    blendShapeConfiguration.FaceParts = meshAsset.BlendShapeConfig.FaceParts;
                    blendShapeConfiguration.Blinks = meshAsset.BlendShapeConfig.Blinks;
                    blendShapeConfiguration.Visemes = meshAsset.BlendShapeConfig.Visemes;

                    if (vgoMeshRenderer.blendShapePresets != null &&
                        vgoMeshRenderer.blendShapePresets.Any())
                    {
                        blendShapeConfiguration.Presets.AddRange(vgoMeshRenderer.blendShapePresets);
                    }
                    else if (
                        vgoMeshRenderer.blendShapePesets != null &&
                        vgoMeshRenderer.blendShapePesets.Any())
                    {
                        blendShapeConfiguration.Presets.AddRange(vgoMeshRenderer.blendShapePesets);
                    }

                    vgoBlendShape.BlendShapeConfiguration = blendShapeConfiguration;

                    vgoModelAsset.ScriptableObjectList.Add(blendShapeConfiguration);
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
        /// <param name="skinnedMeshRenderer">A skinned mesh renderer.</param>
        /// <param name="skinIndex">The index of gltf.skin.</param>
        /// <param name="nodes">List of node.</param>
        /// <param name="vgoStorage">A vgo storage.</param>
        protected virtual void SetupSkin(SkinnedMeshRenderer skinnedMeshRenderer, in int skinIndex, in List<Transform> nodes, in IVgoStorage vgoStorage)
        {
            if (skinnedMeshRenderer.sharedMesh == null)
            {
                ThrowHelper.ThrowException();

                return;
            }

            if (vgoStorage.Layout.skins == null)
            {
                ThrowHelper.ThrowException();

                return;
            }

            if (vgoStorage.Layout.skins.TryGetValue(skinIndex, out var vgoSkin) == false)
            {
                ThrowHelper.ThrowIndexOutOfRangeException("skins[i]", skinIndex, min: 0, max: vgoStorage.Layout.skins.Count);
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
                        Debug.LogWarning($"skins[{skinIndex}].joints[{jointIndex}] is out of the range. 0 <= x < {nodes.Count}");

                        //ThrowHelper.ThrowIndexOutOfRangeException($"skins[{skinIndex}].joints[i]", jointIndex, min: 0, max: nodes.Count);
                    }
                }

                skinnedMeshRenderer.bones = joints;

                if (vgoSkin.inverseBindMatrices >= 0)
                {
                    if (vgoStorage.GeometryCoordinate == VgoGeometryCoordinate.RightHanded)
                    {
                        ReadOnlySpan<Matrix4x4> matrixSpan = vgoStorage.GetResourceDataAsSpan<Matrix4x4>(vgoSkin.inverseBindMatrices);

                        var bindPoses = new Matrix4x4[matrixSpan.Length];

                        for (int i = 0; i < matrixSpan.Length; i++)
                        {
                            bindPoses[i] = matrixSpan[i].ReverseZ();
                        }

                        mesh.bindposes = bindPoses;
                    }
                    else
                    {
                        mesh.bindposes = vgoStorage.GetResourceDataAsArray<Matrix4x4>(vgoSkin.inverseBindMatrices);
                    }
                }
                else
                {
                    // calculate default matrices
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
        protected virtual List<AnimationClip?> CreateAnimationClipAssets(in VgoLayout vgoLayout, in VgoGeometryCoordinate geometryCoordinate)
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
        /// <param name="vgoModelAsset">A vgo model asset.</param>
        protected virtual void SetupAssetInfo(in IVgoStorage vgoStorage, VgoModelAsset vgoModelAsset)
        {
            if (vgoStorage.AssetInfo == null)
            {
                return;
            }

            if (vgoModelAsset.Root == null)
            {
                return;
            }

            VgoAssetInfo vgoAssetInfo = vgoStorage.AssetInfo;

            if (vgoAssetInfo.right != null)
            {
                VgoRight vgoRightComponent = vgoModelAsset.Root.AddComponent<VgoRight>();

                vgoRightComponent.Right = new NewtonVgo.VgoRight(vgoAssetInfo.right);
            }

            if (vgoAssetInfo.generator != null)
            {
                VgoGenerator vgoGeneratorComponent = vgoModelAsset.Root.AddComponent<VgoGenerator>();

                vgoGeneratorComponent.GeneratorInfo = new VgoGeneratorInfo(vgoAssetInfo.generator);
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Create and add avatar configuration.
        /// </summary>
        /// <param name="vgoHumanAvatar">The vgo human avatar.</param>
        protected virtual AvatarConfiguration CreateAvatarConfiguration(in VgoHumanAvatar vgoHumanAvatar)
        {
            var avatarConfiguration = ScriptableObject.CreateInstance<AvatarConfiguration>();

            avatarConfiguration.Name = vgoHumanAvatar.name ?? string.Empty;
            avatarConfiguration.HumanBones = vgoHumanAvatar.humanBones
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
        protected virtual System.Numerics.Matrix4x4[] GetVgoNodeTransforms(in IVgoStorage vgoStorage)
        {
            if (vgoStorage.Layout.nodes is null)
            {
                ThrowHelper.ThrowException();

                return Array.Empty<System.Numerics.Matrix4x4>();
            }

            if (vgoStorage.ResourceAccessors is null)
            {
                ThrowHelper.ThrowException();

                return Array.Empty<System.Numerics.Matrix4x4>();
            }

            int resourceAccessorCount = vgoStorage.ResourceAccessors.Count(x => x.kind == VgoResourceAccessorKind.NodeTransform);

            if (resourceAccessorCount == 0)
            {
                ThrowHelper.ThrowFormatException("Resource accessor is not found. kind: NodeTransform");

                return Array.Empty<System.Numerics.Matrix4x4>();
            }
            else if (resourceAccessorCount >= 2)
            {
                ThrowHelper.ThrowFormatException("Multiple resource accessors found.. kind: NodeTransform");

                return Array.Empty<System.Numerics.Matrix4x4>();
            }

            VgoResourceAccessor resourceAccessor = vgoStorage.ResourceAccessors.First(x => x.kind == VgoResourceAccessorKind.NodeTransform);

            int resourceAccessorIndex = vgoStorage.ResourceAccessors.IndexOf(resourceAccessor);

            System.Numerics.Matrix4x4[] transforms = vgoStorage.GetResourceDataAsArray<System.Numerics.Matrix4x4>(resourceAccessorIndex);

            if (transforms.Length != vgoStorage.Layout.nodes.Count)
            {
                ThrowHelper.ThrowFormatException();

                return Array.Empty<System.Numerics.Matrix4x4>();
            }

            return transforms;
        }

        #endregion
    }
}
