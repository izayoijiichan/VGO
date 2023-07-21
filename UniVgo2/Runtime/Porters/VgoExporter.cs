// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : VgoExporter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using NewtonVgo;
    using NewtonVgo.Buffers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UniVgo2.Converters;
    using UniVgo2.Porters;

    /// <summary>
    /// VGO Exporter
    /// </summary>
    public class VgoExporter
    {
        #region Fields

        /// <summary>The vgo exporter option.</summary>
        protected readonly VgoExporterOption _Option;

        /// <summary>The material exporter.</summary>
        protected readonly IVgoMaterialExporter _MaterialExporter;

        /// <summary>The mesh exporter.</summary>
        protected readonly IVgoMeshExporter _MeshExporter;

        /// <summary>The particle system exporter.</summary>
        protected readonly IVgoParticleSystemExporter _ParticleSystemExporter;

        /// <summary>The texture exporter.</summary>
        protected readonly IVgoTextureExporter _TextureExporter;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of VgoExporter.
        /// </summary>
        public VgoExporter() : this(new VgoExporterOption()) { }

        /// <summary>
        /// Create a new instance of VgoExporter with option.
        /// </summary>
        /// <param name="option">The vgo exporter option.</param>
        public VgoExporter(in VgoExporterOption option)
        {
            _Option = option;

            _TextureExporter = new VgoTextureExporter();

            _MaterialExporter = new VgoMaterialPorter()
            {
                MaterialPorterStore = new VgoMaterialPorterStore(),
                ExportTexture = _TextureExporter.ExportTextureDelegate
            };

            _MeshExporter = new VgoMeshExporter(_Option.MeshExporterOption);

            _ParticleSystemExporter = new VgoParticleSystemExporter();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Create a vgo storage.
        /// </summary>
        /// <param name="root">The GameObject of root.</param>
        /// <param name="geometryCoordinate"></param>
        /// <param name="uvCoordinate"></param>
        /// <param name="textureType"></param>
        /// <returns>A vgo storage.</returns>
        public virtual IVgoStorage CreateVgoStorage(
            in GameObject root,
            in VgoGeometryCoordinate geometryCoordinate = VgoGeometryCoordinate.RightHanded,
            in VgoUVCoordinate uvCoordinate = VgoUVCoordinate.TopLeft,
            in ImageType textureType = ImageType.PNG)
        {
            _TextureExporter.TextureType = textureType;

            int initialResourceCapacity = 10 * 1024 * 1024;  // 10MB

            IByteBuffer resource = new ArraySegmentByteBuffer(initialResourceCapacity);

            IVgoStorage vgoStorage = new VgoStorage(resource, geometryCoordinate, uvCoordinate);

            GameObject copy = UnityEngine.Object.Instantiate(root);

            try
            {
                copy.name = root.name;

                if (geometryCoordinate == VgoGeometryCoordinate.RightHanded)
                {
                    copy.transform.ReverseZRecursive();
                }

                var modelAsset = new ExportModelAsset(copy);

                // Materials & Textures
                CreateVgoMaterialsAndExportTextures(vgoStorage, modelAsset);

                // Node transforms
                ExportNodeTransforms(vgoStorage, modelAsset);

                CreateVgoMeshes(vgoStorage, modelAsset);
                CreateVgoSkins(vgoStorage, modelAsset);

                CreateVgoAnimationClips(vgoStorage.Layout, modelAsset, vgoStorage.GeometryCoordinate);

                CreateVgoColliders(vgoStorage.Layout, modelAsset, vgoStorage.GeometryCoordinate);

                CreateVgoClothes(vgoStorage, modelAsset);

                CreateVgoLights(vgoStorage.Layout, modelAsset);

                // ParticleSystems & Textures
                CreateVgoParticlesAndExportTextures(vgoStorage, modelAsset, vgoStorage.GeometryCoordinate);

                // Nodes (include Root)
                CreateVgoNodes(vgoStorage, modelAsset, vgoStorage.GeometryCoordinate);

                CreateSpringBoneInfo(vgoStorage.Layout, modelAsset, vgoStorage.GeometryCoordinate);

                SetVgoAssetInfo(vgoStorage, modelAsset);
            }
            finally
            {
#if UNITY_EDITOR
                if (Application.isPlaying)
                {
                    GameObject.Destroy(copy);
                }
                else
                {
                    GameObject.DestroyImmediate(copy);
                }
#else
                GameObject.Destroy(copy);
#endif
            }

            return vgoStorage;
        }

        #endregion

        #region assetInfo

        /// <summary>
        /// Set a assetInfo.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="modelAsset">A model asset.</param>
        protected virtual void SetVgoAssetInfo(IVgoStorage vgoStorage, in ExportModelAsset modelAsset)
        {
            vgoStorage.AssetInfo = new VgoAssetInfo
            {
                generator = new VgoGeneratorInfo
                {
                    name = Vgo.Generator,
                    version = VgoVersion.VERSION,
                },
            };

            if (modelAsset.Root.TryGetComponentEx(out VgoRight vgoRight))
            {
                if (vgoRight.Right != null)
                {
                    vgoStorage.AssetInfo.right = vgoRight.Right;
                }
            }
        }

        #endregion

        #region layout.colliders

        /// <summary>
        /// Create layout.colliders.
        /// </summary>
        /// <param name="vgoLayout">A vgo layout.</param>
        /// <param name="modelAsset">A model asset.</param>
        /// <param name="geometryCoordinate"></param>
        protected virtual void CreateVgoColliders(VgoLayout vgoLayout, in ExportModelAsset modelAsset, in VgoGeometryCoordinate geometryCoordinate)
        {
            if (modelAsset.ColliderList == null)
            {
                ThrowHelper.ThrowException();

                return;
            }

            if (modelAsset.ColliderList.Any() == false)
            {
                return;
            }

            vgoLayout.colliders = new List<VgoCollider?>(modelAsset.ColliderList.Count);

            for (int colliderIndex = 0; colliderIndex < modelAsset.ColliderList.Count; colliderIndex++)
            {
                Collider collider = modelAsset.ColliderList[colliderIndex];

                VgoCollider? vgoCollider = VgoColliderConverter.CreateOrDefaultFrom(collider, geometryCoordinate);

                if (vgoCollider is null)
                {
                    //ThrowHelper.ThrowException($"Collider create error. {collider.name}");

                    Debug.LogWarning($"The collider type is not supported. {collider.name}");

                    vgoLayout.colliders.Add(null);
                }
                else
                {
                    vgoLayout.colliders.Add(vgoCollider);
                }
            }
        }

        #endregion

        #region layout.materials

        /// <summary>
        /// Create layout.materials and export textures.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="modelAsset">A model asset.</param>
        protected virtual void CreateVgoMaterialsAndExportTextures(IVgoStorage vgoStorage, in ExportModelAsset modelAsset)
        {
            if (modelAsset.MaterialList == null)
            {
                ThrowHelper.ThrowException();

                return;
            }

            if (modelAsset.MaterialList.Any() == false)
            {
                return;
            }

            if (vgoStorage.Layout is null)
            {
                return;
            }

            vgoStorage.Layout.materials = new List<VgoMaterial?>(modelAsset.MaterialList.Count);

            for (int materialIndex = 0; materialIndex < modelAsset.MaterialList.Count; materialIndex++)
            {
                Material material = modelAsset.MaterialList[materialIndex];

                VgoMaterial vgoMaterial = _MaterialExporter.CreateVgoMaterial(material, vgoStorage);

                vgoStorage.Layout.materials.Add(vgoMaterial);
            }
        }

        #endregion

        #region layout.meshes

        /// <summary>
        /// Create layout.meshes.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="modelAsset">A model asset.</param>
        protected virtual void CreateVgoMeshes(IVgoStorage vgoStorage, in ExportModelAsset modelAsset)
        {
            if (modelAsset.MeshAssetList == null)
            {
                ThrowHelper.ThrowException();

                return;
            }

            _MeshExporter.ExportMeshes(vgoStorage, modelAsset.MeshAssetList, modelAsset.MaterialList);
        }

        #endregion

        #region layout.skins

        /// <summary>
        /// Create layout.skins.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="modelAsset">A model asset.</param>
        protected virtual void CreateVgoSkins(IVgoStorage vgoStorage, in ExportModelAsset modelAsset)
        {
            if (modelAsset.SkinnedMeshRendererList == null)
            {
                ThrowHelper.ThrowException();

                return;
            }

            if (modelAsset.TransformList == null)
            {
                ThrowHelper.ThrowException();

                return;
            }

            vgoStorage.Layout.skins = new List<VgoSkin?>(modelAsset.SkinnedMeshRendererList.Count);

            Matrix4x4[] matrices;

            foreach (SkinnedMeshRenderer renderer in modelAsset.SkinnedMeshRendererList)
            {
                if (renderer.sharedMesh is null)
                {
                    ThrowHelper.ThrowException();

                    return;
                }

                if (vgoStorage.GeometryCoordinate == VgoGeometryCoordinate.RightHanded)
                {
                    matrices = renderer.sharedMesh.bindposes.Select(y => y.ReverseZ()).ToArray();
                }
                else
                {
                    matrices = renderer.sharedMesh.bindposes;
                }

                int accessorIndex = vgoStorage.AddAccessorWithoutSparse(matrices, VgoResourceAccessorDataType.Matrix4Float, VgoResourceAccessorKind.SkinData);

                var vgoSkin = new VgoSkin
                {
                    inverseBindMatrices = accessorIndex,
                    skeleton = modelAsset.TransformList.IndexOf(renderer.rootBone),
                };

                int[] joints = new int[renderer.bones.Length];

                for (int boneIndex = 0; boneIndex < renderer.bones.Length; boneIndex++)
                {
                    Transform? boneTransform = renderer.bones[boneIndex];

                    if (boneTransform == null)
                    {
                        Debug.LogWarning($"SkinnedMeshRenderer: {renderer.name} bones[{boneIndex}] is null.");

                        continue;
                    }

                    int jointTransformIndex = modelAsset.TransformList.IndexOf(boneTransform);

                    if (jointTransformIndex == -1)
                    {
                        Debug.LogWarning($"SkinnedMeshRenderer: {renderer.name} bones[{boneIndex}] is not contains in Transform list.");

                        continue;
                    }

                    joints[boneIndex] = jointTransformIndex;
                }

                vgoSkin.joints = joints;

                vgoStorage.Layout.skins.Add(vgoSkin);
            }
        }

        #endregion

        #region layout.nodes

        /// <summary>
        /// Create layout.nodes.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="modelAsset">A model asset.</param>
        /// <param name="geometryCoordinate"></param>
        protected virtual void CreateVgoNodes(IVgoStorage vgoStorage, in ExportModelAsset modelAsset, in VgoGeometryCoordinate geometryCoordinate)
        {
            if (modelAsset.TransformList == null)
            {
                ThrowHelper.ThrowException();

                return;
            }

            if (modelAsset.MeshAssetList == null)
            {
                ThrowHelper.ThrowException();

                return;
            }

            if (modelAsset.SkinnedMeshRendererList == null)
            {
                ThrowHelper.ThrowException();

                return;
            }

            vgoStorage.Layout.nodes = new List<VgoNode?>(modelAsset.TransformList.Count);

            for (int nodeIndex = 0; nodeIndex < modelAsset.TransformList.Count; nodeIndex++)
            {
                VgoNode vgoNode = CreateVgoNode(nodeIndex, vgoStorage, modelAsset, geometryCoordinate);

                vgoStorage.Layout.nodes.Add(vgoNode);
            }
        }

        /// <summary>
        /// Create a layout.node.
        /// </summary>
        /// <param name="nodeIndex">The index of the node.</param>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="modelAsset">A model asset.</param>
        /// <param name="geometryCoordinate"></param>
        /// <returns>A layout.node.</returns>
        protected virtual VgoNode CreateVgoNode(in int nodeIndex, in IVgoStorage vgoStorage, in ExportModelAsset modelAsset, in VgoGeometryCoordinate geometryCoordinate)
        {
            GameObject gameObject = modelAsset.TransformList[nodeIndex].gameObject;

            var vgoNode = new VgoNode
            {
                name = gameObject.name,
                isRoot = nodeIndex == 0,
                isActive = gameObject.activeSelf,
                isStatic = gameObject.isStatic,
                tag = gameObject.tag,
                layer = gameObject.layer,
            };

            // Animator
            if (gameObject.TryGetComponentEx(out Animator animator))
            {
                vgoNode.animator = VgoAnimatorConverter.CreateFrom(animator);

                // Avatar
                if (animator.avatar != null)
                {
                    if (animator.avatar.isHuman)
                    {
                        vgoNode.animator.humanAvatar = VgoAvatarConverter.CreateVgoAvatarOrDefault(animator, vgoNode.name, modelAsset.TransformList);
                    }
                }
            }

            // Animation
            if (gameObject.TryGetComponentEx(out Animation animation))
            {
                vgoNode.animation = VgoAnimationConverter.CreateFrom(animation, modelAsset.AnimationClipList, geometryCoordinate);
            }

            // Rigidbody
            if (gameObject.TryGetComponentEx(out Rigidbody rigidbody))
            {
                vgoNode.rigidbody = VgoRigidbodyConverter.CreateFrom(rigidbody);
            }

            // Colliders
            if (gameObject.TryGetComponentsEx(out Collider[] colliders))
            {
                var colliderList = modelAsset.ColliderList;

                vgoNode.colliders = colliders
                    .Select(collider => colliderList.IndexOf(collider))
                    .Where(index => index != -1)
                    .ToList();
            }

            // Renderer
            var meshRenderer = gameObject.GetComponent<MeshRenderer>();
            var skinnedMeshRenderer = gameObject.GetComponent<SkinnedMeshRenderer>();

            // Mesh
            if (vgoStorage.IsSpecVersion_2_4_orLower)
            {
                if (meshRenderer != null)
                {
                    vgoNode.mesh = modelAsset.MeshAssetList.IndexOf(x => x.Renderer == meshRenderer);
                }

                if (skinnedMeshRenderer != null)
                {
                    vgoNode.mesh = modelAsset.MeshAssetList.IndexOf(x => x.Renderer == skinnedMeshRenderer);
                }
            }
            else
            {
                var particleSystemRenderer = gameObject.GetComponent<ParticleSystemRenderer>();

#if UNITY_2021_2_OR_NEWER
                Renderer? renderer
                    = (meshRenderer != null) ? meshRenderer
                    : (skinnedMeshRenderer != null) ? skinnedMeshRenderer
                    : (particleSystemRenderer != null) ? particleSystemRenderer
                    : null;
#else
                Renderer? renderer;

                if (meshRenderer != null)
                {
                    renderer = meshRenderer;
                }
                else if (skinnedMeshRenderer != null)
                {
                    renderer = skinnedMeshRenderer;
                }
                else if (particleSystemRenderer != null)
                {
                    renderer = particleSystemRenderer;
                }
                else
                {
                    renderer = null;
                }
#endif

                // MeshRenderer
                if (renderer != null)
                {
                    var meshRendererAsset = modelAsset.MeshRendererAssetList
                        .Where(x => x.Renderer == renderer)
                        .FirstOrDefault();

                    if ((meshRendererAsset != null) &&
                        (meshRendererAsset.Mesh != null))
                    {
                        int meshIndex = modelAsset.MeshList.IndexOf(m => m == meshRendererAsset.Mesh);

                        if (meshIndex == -1)
                        {
                            ThrowHelper.ThrowIndexOutOfRangeException(nameof(meshIndex), meshIndex, min: 0, max: modelAsset.MeshList.Count);
                        }

                        var materialList = modelAsset.MaterialList;

                        var vgoMeshRenderer = new VgoMeshRenderer()
                        {
                            name = renderer.name,
                            enabled = renderer.enabled,
                            mesh = meshIndex,
                            materials = renderer.sharedMaterials.Select(m => materialList.IndexOf(m)).ToList(),
                        };

                        if (gameObject.TryGetComponentEx(out VgoBlendShape vgoBlendShapeComponent))
                        {
                            if (vgoBlendShapeComponent.BlendShapeConfiguration != null)
                            {
                                vgoMeshRenderer.blendShapeKind = vgoBlendShapeComponent.BlendShapeConfiguration.Kind;
                                vgoMeshRenderer.blendShapePesets = vgoBlendShapeComponent.BlendShapeConfiguration.Presets;
                            }
                        }

                        vgoNode.meshRenderer = vgoMeshRenderer;
                    }
                }
            }

            // Skin
            if (skinnedMeshRenderer != null)
            {
                vgoNode.skin = modelAsset.SkinnedMeshRendererList.IndexOf(skinnedMeshRenderer);
            }

            // SpringBone
            if (gameObject.TryGetComponentsEx(out VgoSpringBone.VgoSpringBoneGroup[] vgoSpringBoneGroups))
            {
                vgoNode.springBoneGroups = new List<int>(vgoSpringBoneGroups.Length);

                foreach (var group in vgoSpringBoneGroups)
                {
                    vgoNode.springBoneGroups.Add(modelAsset.VgoSpringBoneGroupList.IndexOf(group));
                }
            }

            // SpringBoneCollider
            if (gameObject.TryGetComponentEx(out VgoSpringBone.VgoSpringBoneColliderGroup vgoSpringBoneColliderGroup))
            {
                vgoNode.springBoneColliderGroup = modelAsset.VgoSpringBoneColliderGroupList.IndexOf(vgoSpringBoneColliderGroup);
            }

            // Cloth
            if (gameObject.TryGetComponentEx(out Cloth cloth))
            {
                vgoNode.cloth = modelAsset.ClothList.IndexOf(cloth);
            }

            // Light
            if (gameObject.TryGetComponentEx(out Light light))
            {
                vgoNode.light = modelAsset.LightList.IndexOf(light);
            }

            // ParticleSystem
            if (gameObject.TryGetComponentEx(out ParticleSystem particleSystem))
            {
                vgoNode.particle = modelAsset.ParticleSystemAssetList.IndexOf(x => x.ParticleSystem == particleSystem);
            }

            // Skybox
            if (gameObject.TryGetComponentEx(out Skybox skybox))
            {
                vgoNode.skybox = new VgoSkybox
                {
                    enabled = skybox.enabled,
                };

                var vgoMaterials = vgoStorage.Layout.materials;

                if (vgoMaterials != null && vgoMaterials.Any())
                {
                    VgoMaterial? skyboxMaterial = vgoMaterials.Where(m => m?.name == skybox.material.name).FirstOrDefault();

                    if ((skyboxMaterial != null))
                    {
                        vgoNode.skybox.materialIndex = vgoMaterials.IndexOf(skyboxMaterial);
                    }
                }
            }

            // Right
            if (nodeIndex != 0)
            {
                if (gameObject.TryGetComponentEx(out VgoRight vgoRight))
                {
                    if (vgoRight.Right != null)
                    {
                        vgoNode.right = vgoRight.Right;
                    }
                }
            }

            // Children
            if (gameObject.transform.childCount > 0)
            {
                vgoNode.children = new List<int>(gameObject.transform.childCount);

                foreach (Transform child in gameObject.transform)
                {
                    vgoNode.children.Add(modelAsset.TransformList.IndexOf(child));
                }
            }

            return vgoNode;
        }

        /// <summary>
        /// Export node transforms to storage.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="modelAsset">A model asset.</param>
        protected virtual void ExportNodeTransforms(IVgoStorage vgoStorage, in ExportModelAsset modelAsset)
        {
            if (modelAsset.TransformList == null)
            {
                ThrowHelper.ThrowException();

                return;
            }

            if (modelAsset.TransformList.Any() == false)
            {
                ThrowHelper.ThrowException();

                return;
            }

            var matrixes = new System.Numerics.Matrix4x4[modelAsset.TransformList.Count];

            for (int nodeIndex = 0; nodeIndex < modelAsset.TransformList.Count; nodeIndex++)
            {
                Transform transform = modelAsset.TransformList[nodeIndex];

                var matrix = new Matrix4x4();

                matrix.SetTRS(
                    transform.localPosition,
                    transform.localRotation,
                    transform.localScale
                );

                matrixes[nodeIndex] = matrix.ToNumericsMatrix();
            }

            vgoStorage.AddAccessorWithoutSparse(matrixes, VgoResourceAccessorDataType.Matrix4Float, VgoResourceAccessorKind.NodeTransform);
        }

        #endregion

        #region layout.animationClips

        /// <summary>
        /// Create layout.animationClips.
        /// </summary>
        /// <param name="vgoLayout">A vgo layout.</param>
        /// <param name="modelAsset">A model asset.</param>
        /// <param name="geometryCoordinate"></param>
        protected virtual void CreateVgoAnimationClips(VgoLayout vgoLayout, in ExportModelAsset modelAsset, in VgoGeometryCoordinate geometryCoordinate)
        {
            if (modelAsset.AnimationClipList == null)
            {
                ThrowHelper.ThrowException();

                return;
            }

            if (modelAsset.AnimationClipList.Any())
            {
                vgoLayout.animationClips = new List<VgoAnimationClip?>(modelAsset.AnimationClipList.Count);

                for (int animationClipIndex = 0; animationClipIndex < modelAsset.AnimationClipList.Count; animationClipIndex++)
                {
                    AnimationClip animationClip = modelAsset.AnimationClipList[animationClipIndex];

                    VgoAnimationClip? vgoAnimationClip = VgoAnimationClipConverter.CreateOrDefaultFrom(animationClip, geometryCoordinate);

                    vgoLayout.animationClips.Add(vgoAnimationClip);
                }
            }
            else
            {
                vgoLayout.animationClips = new List<VgoAnimationClip?>();
            }
        }

        #endregion

        #region layout.clothes

        /// <summary>
        /// Create layout.clothes.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="modelAsset">A model asset.</param>
        protected virtual void CreateVgoClothes(IVgoStorage vgoStorage, in ExportModelAsset modelAsset)
        {
            if (modelAsset.ClothList == null)
            {
                ThrowHelper.ThrowException();

                return;
            }

            if (modelAsset.ClothList.Any() == false)
            {
                return;
            }

            vgoStorage.Layout.clothes = new List<VgoCloth?>(modelAsset.ClothList.Count);

            for (int clothIndex = 0; clothIndex < modelAsset.ClothList.Count; clothIndex++)
            {
                Cloth cloth = modelAsset.ClothList[clothIndex];

                VgoCloth? vgoCloth = VgoClothConverter.CreateOrDefaultFrom(cloth, vgoStorage.GeometryCoordinate, modelAsset.ColliderList, vgoStorage);

                vgoStorage.Layout.clothes.Add(vgoCloth);
            }
        }

        #endregion

        #region layout.lights

        /// <summary>
        /// Create layout.lights.
        /// </summary>
        /// <param name="vgoLayout">A vgo layout.</param>
        /// <param name="modelAsset">A model asset.</param>
        protected virtual void CreateVgoLights(VgoLayout vgoLayout, in ExportModelAsset modelAsset)
        {
            if (modelAsset.LightList == null)
            {
                ThrowHelper.ThrowException();

                return;
            }

            if (modelAsset.LightList.Any() == false)
            {
                return;
            }

            vgoLayout.lights = new List<VgoLight?>(modelAsset.LightList.Count);

            for (int lightIndex = 0; lightIndex < modelAsset.LightList.Count; lightIndex++)
            {
                Light light = modelAsset.LightList[lightIndex];

                VgoLight? vgoLight = VgoLightConverter.CreateOrDefaultFrom(light);

                vgoLayout.lights.Add(vgoLight);
            }
        }

        #endregion

        #region layout.particles

        /// <summary>
        /// Create layout.particles and export textures.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="modelAsset">A model asset.</param>
        /// <param name="geometryCoordinate"></param>
        protected virtual void CreateVgoParticlesAndExportTextures(IVgoStorage vgoStorage, in ExportModelAsset modelAsset, in VgoGeometryCoordinate geometryCoordinate)
        {
            if (modelAsset.ParticleSystemAssetList == null)
            {
                ThrowHelper.ThrowException();

                return;
            }

            if (modelAsset.ParticleSystemAssetList.Any() == false)
            {
                return;
            }

            vgoStorage.Layout.particles = new List<VgoParticleSystem?>(modelAsset.ParticleSystemAssetList.Count);

            foreach (var particleSystemAsset in modelAsset.ParticleSystemAssetList)
            {
                if ((particleSystemAsset.ParticleSystem == null) ||
                    (particleSystemAsset.ParticleSystemRenderer == null))
                {
                    continue;
                }

                VgoParticleSystem vgoParticleSystem = _ParticleSystemExporter.Create(
                    particleSystemAsset.ParticleSystem,
                    particleSystemAsset.ParticleSystemRenderer,
                    vgoStorage,
                    _TextureExporter.ExportTextureDelegate
                );

                vgoStorage.Layout.particles.Add(vgoParticleSystem);
            }
        }

        #endregion

        #region layout.springBoneInfo

        /// <summary>
        /// Create layout.springBoneInfo.
        /// </summary>
        /// <param name="vgoLayout">A vgo layout.</param>
        /// <param name="modelAsset">A model asset.</param>
        /// <param name="geometryCoordinate"></param>
        protected virtual void CreateSpringBoneInfo(VgoLayout vgoLayout, in ExportModelAsset modelAsset, in VgoGeometryCoordinate geometryCoordinate)
        {
            if (((modelAsset.VgoSpringBoneGroupList == null) || modelAsset.VgoSpringBoneGroupList.Any() == false) &&
                ((modelAsset.VgoSpringBoneColliderGroupList == null) || modelAsset.VgoSpringBoneColliderGroupList.Any() == false))
            {
                return;
            }

            vgoLayout.springBoneInfo = new VgoSpringBoneInfo();

            // springBoneInfo.springBoneGroups
            if (modelAsset.VgoSpringBoneGroupList != null &&
                modelAsset.VgoSpringBoneGroupList.Any())
            {
                vgoLayout.springBoneInfo.springBoneGroups = new List<VgoSpringBoneGroup?>(modelAsset.VgoSpringBoneGroupList.Count);

                foreach (var component in modelAsset.VgoSpringBoneGroupList)
                {
                    var boneGroup = new VgoSpringBoneGroup
                    {
                        name = component.name,
                        enabled = component.enabled,
                        comment = component.comment,
                        dragForce = component.dragForce,
                        stiffnessForce = component.stiffnessForce,
                        gravityDirection = component.gravityDirection.ToNumericsVector3(geometryCoordinate),
                        gravityPower = component.gravityPower,
                        hitRadius = component.hitRadius,
                        drawGizmo = component.drawGizmo,
                        gizmoColor = component.gizmoColor.ToVgoColor3(),
                    };

                    // boneGroup.rootBones
                    if ((component.rootBones != null) && (component.rootBones.Length > 0))
                    {
                        boneGroup.rootBones = new int[component.rootBones.Length];

                        for (int index = 0; index < component.rootBones.Length; index++)
                        {
                            boneGroup.rootBones[index] = modelAsset.TransformList.IndexOf(component.rootBones[index].transform);
                        }
                    }

                    // boneGroup.colliderGroups
                    if ((component.colliderGroups != null) &&
                        (component.colliderGroups.Length > 0) &&
                        (modelAsset.VgoSpringBoneColliderGroupList != null))
                    {
                        boneGroup.colliderGroups = new int[component.colliderGroups.Length];

                        for (int index = 0; index < component.colliderGroups.Length; index++)
                        {
                            boneGroup.colliderGroups[index] = modelAsset.VgoSpringBoneColliderGroupList.IndexOf(component.colliderGroups[index]);
                        }
                    }

                    vgoLayout.springBoneInfo.springBoneGroups.Add(boneGroup);
                }
            }

            // springBoneInfo.colliderGroups
            if (modelAsset.VgoSpringBoneColliderGroupList != null &&
                modelAsset.VgoSpringBoneColliderGroupList.Any())
            {
                vgoLayout.springBoneInfo.colliderGroups = new List<VgoSpringBoneColliderGroup?>(modelAsset.VgoSpringBoneColliderGroupList.Count);

                foreach (var component in modelAsset.VgoSpringBoneColliderGroupList)
                {
                    var colliderGroup = new VgoSpringBoneColliderGroup
                    {
                        gizmoColor = component.gizmoColor.ToVgoColor3(),
                    };

                    // colliderGroup.colliders
                    if ((component.colliders != null) && (component.colliders.Length > 0))
                    {
                        colliderGroup.colliders = new VgoSpringBoneCollider[component.colliders.Length];

                        for (int index = 0; index < component.colliders.Length; index++)
                        {
                            var collider = component.colliders[index];

                            colliderGroup.colliders[index] = new VgoSpringBoneCollider
                            {
                                colliderType = (VgoSpringBoneColliderType)collider.colliderType,
                                offset = collider.offset.ToNumericsVector3(geometryCoordinate),
                                radius = collider.radius,
                            };
                        }
                    }

                    vgoLayout.springBoneInfo.colliderGroups.Add(colliderGroup);
                }
            }
        }

        #endregion
    }
}
