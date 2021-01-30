// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : VgoExporter
// ----------------------------------------------------------------------
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

    #region Delegates

    /// <summary>The delegate to ExportTexture method.</summary>
    public delegate int ExportTextureDelegate(Material material, Texture srcTexture, VgoTextureMapType textureMapType = VgoTextureMapType.Default, VgoColorSpaceType colorSpaceType = VgoColorSpaceType.Srgb, float metallicSmoothness = -1.0f);

    #endregion

    /// <summary>
    /// VGO Exporter
    /// </summary>
    public class VgoExporter : IDisposable
    {
        #region Constructors

        /// <summary>
        /// Create a new instance of VgoExporter.
        /// </summary>
        public VgoExporter()
        {
            Initialize();
        }

        //~VgoExporter()
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

        /// <summary>The ParticleSystem exporter.</summary>
        protected VgoParticleSystemExporter _ParticleSystemExporter = new VgoParticleSystemExporter();

        /// <summary>The texture converter.</summary>
        protected TextureConverter _TextureConverter = new TextureConverter();

        #endregion

        #region Properties

        /// <summary>The VGO storage adapter.</summary>
        public VgoStorageAdapter StorageAdapter { get; protected set; }

        /// <summary>The VGO layout.</summary>
        protected VgoLayout Layout => StorageAdapter.Layout;

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

            _MaterialExporter = new VgoMaterialPorter()
            {
                MaterialPorterStore = new MaterialPorterStore(),
                ExportTexture = ExportTexture
            };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Create a vgo storage.
        /// </summary>
        /// <param name="root">The GameObject of root.</param>
        /// <param name="geometryCoordinate"></param>
        /// <param name="uvCoordinate"></param>
        /// <returns>A vgo storage.</returns>
        public virtual VgoStorage CreateVgoStorage(GameObject root, VgoGeometryCoordinate geometryCoordinate = VgoGeometryCoordinate.RightHanded, VgoUVCoordinate uvCoordinate = VgoUVCoordinate.TopLeft)
        {
            int initialResourceSize = 10 * 1024 * 1024;  // 10MB

            IByteBuffer resource = new ArraySegmentByteBuffer(initialResourceSize);

            VgoStorage vgoStorage = new VgoStorage(resource, geometryCoordinate, uvCoordinate);

            StorageAdapter = new VgoStorageAdapter(vgoStorage);

            GameObject copy = UnityEngine.Object.Instantiate(root);

            copy.name = root.name;

            if (geometryCoordinate == VgoGeometryCoordinate.RightHanded)
            {
                copy.transform.ReverseZRecursive();
            }

            GameObject go = copy;

            ModelAsset.Root = go;  // @notice Copy

            Nodes = ModelAsset.Root.transform.Traverse().ToList();

            // Prepare
            ModelAsset.RendererList = CreateUnityRendererList();
            ModelAsset.MaterialList = CreateUnityMaterialList();
            ModelAsset.SkinList = CreateUnitySkinList();
            ModelAsset.ParticleSystemList = CreateUnityParticleSystemList();
            ModelAsset.VgoSpringBoneGroupList = CreateUnityVgoSpringBoneGroupList();
            ModelAsset.VgoSpringBoneColliderGroupList = CreateUnityVgoSpringBoneColliderGroupList();
            CreateMeshAndMeshAssetList();

            // Materials & Textures
            CreateVgoMaterialsAndExportTextures();

            // Node transforms
            ExportNodeTransforms();

            CreateVgoMeshes();
            CreateVgoSkins();

            // ParticleSystems & Textures
            CreateVgoParticlesAndExportTextures();

            // Nodes (include Root)
            CreateVgoNodes();

            CreateSpringBoneInfo();

            SetVgoAssetInfo();

            //GameObject.DestroyImmediate(Copy);

            return vgoStorage;
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

            // Renderer
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

            // Skybox
            Skybox skybox = Nodes[0].GetComponentInChildren<Skybox>();

            if (skybox != null)
            {
                materialList.Add(skybox.material);
            }

            return materialList;
        }

        /// <summary>
        /// Create list of unity VgoSpringBoneGroup.
        /// </summary>
        /// <returns>list of unity VgoSpringBoneGroup.</returns>
        protected virtual List<VgoSpringBone.VgoSpringBoneGroup> CreateUnityVgoSpringBoneGroupList()
        {
            if (Nodes == null)
            {
                throw new Exception();
            }

            var componentList = new List<VgoSpringBone.VgoSpringBoneGroup>();

            for (int index = 0; index < Nodes.Count; index++)
            {
                GameObject node = Nodes[index].gameObject;

                if (node.TryGetComponentsEx(out VgoSpringBone.VgoSpringBoneGroup[] components))
                {
                    foreach (var component in components)
                    {
                        componentList.Add(component);
                    }
                }
            }

            return componentList;
        }

        /// <summary>
        /// Create list of unity VgoSpringBoneColliderGroup.
        /// </summary>
        /// <returns>list of unity VgoSpringBoneColliderGroup.</returns>
        protected virtual List<VgoSpringBone.VgoSpringBoneColliderGroup> CreateUnityVgoSpringBoneColliderGroupList()
        {
            if (Nodes == null)
            {
                throw new Exception();
            }

            var componentList = new List<VgoSpringBone.VgoSpringBoneColliderGroup>();

            for (int index = 0; index < Nodes.Count; index++)
            {
                GameObject node = Nodes[index].gameObject;

                if (node.TryGetComponentEx(out VgoSpringBone.VgoSpringBoneColliderGroup component))
                {
                    componentList.Add(component);
                }
            }

            return componentList;
        }

        #endregion

        #region assetInfo

        /// <summary>
        /// Set a assetInfo.
        /// </summary>
        protected virtual void SetVgoAssetInfo()
        {
            StorageAdapter.AssetInfo = new VgoAssetInfo
            {
                generator = new VgoGeneratorInfo
                {
                    name = Vgo.Generator,
                    version = VgoVersion.VERSION,
                },
            };

            if (ModelAsset.Root.TryGetComponentEx(out VgoRight vgoRight))
            {
                if (vgoRight.Right != null)
                {
                    StorageAdapter.AssetInfo.right = vgoRight.Right;
                }
            }
        }

        #endregion

        #region layout.materials

        /// <summary>
        /// Create layout.materials and export textures.
        /// </summary>
        protected virtual void CreateVgoMaterialsAndExportTextures()
        {
            if (ModelAsset.MaterialList == null)
            {
                throw new Exception();
            }

            if (ModelAsset.MaterialList.Count == 0)
            {
                return;
            }

            Layout.materials = new List<VgoMaterial>(ModelAsset.MaterialList.Count);

            for (int materialIndex = 0; materialIndex < ModelAsset.MaterialList.Count; materialIndex++)
            {
                Material material = ModelAsset.MaterialList[materialIndex];

                VgoMaterial vgoMaterial = _MaterialExporter.CreateVgoMaterial(material);

                Layout.materials.Add(vgoMaterial);
            }
        }

        #endregion

        #region layout.textures

        /// <summary>
        /// Export a texture to vgo storage.
        /// </summary>
        /// <param name="material">A unity material.</param>
        /// <param name="texture">A unity texture.</param>
        /// <param name="textureMapType">The map type of texture.</param>
        /// <param name="colorSpaceType">The color space type of image.</param>
        /// <param name="metallicRoughness">The metallic-roughness value.</param>
        /// <returns>The index of layout.texture.</returns>
        protected virtual int ExportTexture(Material material, Texture texture, VgoTextureMapType textureMapType = VgoTextureMapType.Default, VgoColorSpaceType colorSpaceType = VgoColorSpaceType.Srgb, float metallicRoughness = -1.0f)
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

            if (Layout.textures == null)
            {
                Layout.textures = new List<VgoTexture>();
            }

            int srcTextureInstanceId = srcTexture2d.GetInstanceID();

            VgoTexture vgoTexture = Layout.textures
                .Where(x => x.id == srcTextureInstanceId)
                .FirstOrDefault();

            if (vgoTexture != null)
            {
                return Layout.textures.IndexOf(vgoTexture);
            }

            float metallicSmoothness = (metallicRoughness == -1.0f) ? -1.0f : (1.0f - metallicRoughness);

            Texture2D convertedTexture2d = _TextureConverter.GetExportTexture(srcTexture2d, textureMapType, colorSpaceType, metallicSmoothness);

            string mimeType = "image/png";
            byte[] imageBytes = convertedTexture2d.EncodeToPNG();

            int accessorIndex = StorageAdapter.AddAccessorWithoutSparse(imageBytes, VgoResourceAccessorDataType.UnsignedByte, VgoResourceAccessorKind.ImageData);

            vgoTexture = new VgoTexture
            {
                id = srcTextureInstanceId,
                name = convertedTexture2d.name,
                source = accessorIndex,
                dimensionType = (TextureDimension)srcTexture2d.dimension,
                mapType = textureMapType,
                colorSpace = colorSpaceType,
                mimeType = mimeType,
                filterMode = (NewtonVgo.FilterMode)srcTexture2d.filterMode,
                wrapMode = (NewtonVgo.TextureWrapMode)srcTexture2d.wrapMode,
                wrapModeU = (NewtonVgo.TextureWrapMode)srcTexture2d.wrapModeU,
                wrapModeV = (NewtonVgo.TextureWrapMode)srcTexture2d.wrapModeV,
                metallicRoughness = metallicRoughness,
            };

            Layout.textures.Add(vgoTexture);

            int textureIndex = Layout.textures.Count - 1;

            return textureIndex;
        }

        #endregion

        #region layout.meshes

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

                BlendShapeConfiguration blendShapeConfiguration = null;

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

                    if (renderer.gameObject.TryGetComponentEx(out VgoBlendShape vgoBlendShape))
                    {
                        blendShapeConfiguration = vgoBlendShape.BlendShapeConfiguration;
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
                    BlendShapeConfiguration = blendShapeConfiguration,
                };

                ModelAsset.MeshAssetList.Add(meshAsset);
            }
        }

        /// <summary>
        /// Create layout.meshes.
        /// </summary>
        protected virtual void CreateVgoMeshes()
        {
            if (ModelAsset.MeshAssetList == null)
            {
                throw new Exception();
            }

            if (ModelAsset.MaterialList == null)
            {
                throw new Exception();
            }

            VgoMeshExporter meshExporter = new VgoMeshExporter();

            meshExporter.StorageAdapter = StorageAdapter;

            meshExporter.ExportMeshes(ModelAsset.MeshAssetList, ModelAsset.MaterialList);
        }

        #endregion

        #region layout.skins

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
        /// Create layout.skins.
        /// </summary>
        protected virtual void CreateVgoSkins()
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

            Layout.skins = new List<VgoSkin>(ModelAsset.SkinList.Count);

            Matrix4x4[] matrices;

            foreach (SkinnedMeshRenderer renderer in ModelAsset.SkinList)
            {
                if (StorageAdapter.GeometryCoordinate == VgoGeometryCoordinate.RightHanded)
                {
                    matrices = renderer.sharedMesh.bindposes.Select(y => y.ReverseZ()).ToArray();
                }
                else
                {
                    matrices = renderer.sharedMesh.bindposes;
                }

                int accessorIndex = StorageAdapter.AddAccessorWithoutSparse(matrices, VgoResourceAccessorDataType.Matrix4Float, VgoResourceAccessorKind.SkinData);

                var gltfSkin = new VgoSkin
                {
                    inverseBindMatrices = accessorIndex,
                    joints = renderer.bones.Select(t => Nodes.IndexOf(t)).ToArray(),
                    skeleton = Nodes.IndexOf(renderer.rootBone),
                };

                Layout.skins.Add(gltfSkin);
            }
        }

        #endregion

        #region layout.nodes

        /// <summary>
        /// Create layout.nodes.
        /// </summary>
        protected virtual void CreateVgoNodes()
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

            Layout.nodes = new List<VgoNode>(Nodes.Count);

            List<Renderer> renderers = ModelAsset.MeshAssetList.Select(y => y.Renderer).ToList();

            for (int nodeIndex = 0; nodeIndex < Nodes.Count; nodeIndex++)
            {
                VgoNode vgoNode = CreateVgoNode(nodeIndex, Nodes, renderers, ModelAsset.SkinList);

                Layout.nodes.Add(vgoNode);
            }
        }

        /// <summary>
        /// Create a layout.node.
        /// </summary>
        /// <param name="nodeIndex">The index of the node.</param>
        /// <param name="nodes">List of node.</param>
        /// <param name="renderers">List of renderer.</param>
        /// <param name="skins">List of skin.</param>
        /// <returns>A layout.node.</returns>
        protected virtual VgoNode CreateVgoNode(int nodeIndex, List<Transform> nodes, List<Renderer> renderers, List<SkinnedMeshRenderer> skins)
        {
            GameObject gameObject = Nodes[nodeIndex].gameObject;

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
                        vgoNode.animator.humanAvatar = VgoAvatarConverter.CreateVgoAvatar(animator, vgoNode.name, Nodes);
                    }
                }
            }

            // Rigidbody
            if (gameObject.TryGetComponentEx(out Rigidbody rigidbody))
            {
                vgoNode.rigidbody = VgoRigidbodyConverter.CreateFrom(rigidbody);
            }

            // Colliders
            if (gameObject.TryGetComponentsEx(out Collider[] colliders))
            {
                vgoNode.colliders = colliders
                    .Select(collider => VgoColliderConverter.CreateFrom(collider, StorageAdapter.GeometryCoordinate))
                    .Where(vc => vc != null)
                    .ToList();
            }

            // Skybox
            if (gameObject.TryGetComponentEx(out Skybox skybox))
            {
                VgoMaterial skyboxMaterial = Layout.materials.Where(m => m.name == skybox.material.name).FirstOrDefault();

                vgoNode.skybox = new VgoSkybox
                {
                    materialIndex = (skyboxMaterial == null) ? -1 : Layout.materials.IndexOf(skyboxMaterial)
                };
            }

            // Light
            if (gameObject.TryGetComponentEx(out Light light))
            {
                vgoNode.light = VgoLightConverter.CreateFrom(light);
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

            // ParticleSystem
            if (gameObject.TryGetComponentEx(out ParticleSystem particleSystem))
            {
                vgoNode.particle = ModelAsset.ParticleSystemList.IndexOf(particleSystem);
            }

            // Mesh
            if (gameObject.TryGetComponentEx(out MeshRenderer meshRenderer))
            {
                vgoNode.mesh = renderers.IndexOf(meshRenderer);
            }

            // Mesh and Skin
            if (gameObject.TryGetComponentEx(out SkinnedMeshRenderer skinnedMeshRenderer))
            {
                vgoNode.mesh = renderers.IndexOf(skinnedMeshRenderer);
                vgoNode.skin = skins.IndexOf(skinnedMeshRenderer);
            }

            // SpringBone
            if (gameObject.TryGetComponentsEx(out VgoSpringBone.VgoSpringBoneGroup[] vgoSpringBoneGroups))
            {
                vgoNode.springBoneGroups = new List<int>(vgoSpringBoneGroups.Length);

                foreach (var group in vgoSpringBoneGroups)
                {
                    vgoNode.springBoneGroups.Add(ModelAsset.VgoSpringBoneGroupList.IndexOf(group));
                }
            }

            // SpringBoneCollider
            if (gameObject.TryGetComponentEx(out VgoSpringBone.VgoSpringBoneColliderGroup vgoSpringBoneColliderGroup))
            {
                vgoNode.springBoneColliderGroup = ModelAsset.VgoSpringBoneColliderGroupList.IndexOf(vgoSpringBoneColliderGroup);
            }

            // Children
            if (gameObject.transform.childCount > 0)
            {
                vgoNode.children = new List<int>(gameObject.transform.childCount);

                foreach (Transform child in gameObject.transform)
                {
                    vgoNode.children.Add(nodes.IndexOf(child));
                }
            }

            return vgoNode;
        }

        /// <summary>
        /// Export node transforms to storage.
        /// </summary>
        protected virtual void ExportNodeTransforms()
        {
            if (Nodes == null)
            {
                throw new Exception();
            }

            var matrixes = new System.Numerics.Matrix4x4[Nodes.Count];

            for (int nodeIndex = 0; nodeIndex < Nodes.Count; nodeIndex++)
            {
                Transform transform = Nodes[nodeIndex];

                var matrix = new Matrix4x4();

                matrix.SetTRS(
                    transform.localPosition,
                    transform.localRotation,
                    transform.localScale
                );

                matrixes[nodeIndex] = matrix.ToNumericsMatrix();
            }

            StorageAdapter.AddAccessorWithoutSparse(matrixes, VgoResourceAccessorDataType.Matrix4Float, VgoResourceAccessorKind.NodeTransform);
        }

        #endregion

        #region layout.particles

        /// <summary>
        /// Create particle system list.
        /// </summary>
        /// <returns>List of particle system.</returns>
        protected virtual List<ParticleSystem> CreateUnityParticleSystemList()
        {
            List<ParticleSystem> particleSystemList = null;

            foreach (Transform node in Nodes)
            {
                if (node.gameObject.TryGetComponentEx(out ParticleSystem particleSystem))
                {
                    if (particleSystemList == null)
                    {
                        particleSystemList = new List<ParticleSystem>();
                    }
                    particleSystemList.Add(particleSystem);
                }
            }

            return particleSystemList;
        }

        /// <summary>
        /// Create layout.particles and export textures.
        /// </summary>
        protected virtual void CreateVgoParticlesAndExportTextures()
        {
            if (ModelAsset.ParticleSystemList == null)
            {
                return;
            }

            Layout.particles = new List<VgoParticleSystem>(ModelAsset.ParticleSystemList.Count);

            for (int index = 0; index < ModelAsset.ParticleSystemList.Count; index++)
            {
                ParticleSystem particleSystem = ModelAsset.ParticleSystemList[index];

                if (particleSystem.gameObject.TryGetComponentEx(out ParticleSystemRenderer particleSystemRenderer))
                {
                    VgoParticleSystem vgoParticleSystem = _ParticleSystemExporter.Create(particleSystem, particleSystemRenderer, StorageAdapter, _ExporterTexture);

                    Layout.particles.Add(vgoParticleSystem);
                }
                else
                {
                    throw new Exception($"ParticleSystem[{index}] ParticleSystemRenderer component is not found.");
                }
            }
        }

        #endregion

        #region layout.springBoneInfo

        /// <summary>
        /// Create layout.springBoneInfo.
        /// </summary>
        protected virtual void CreateSpringBoneInfo()
        {
            if (((ModelAsset.VgoSpringBoneGroupList == null) || ModelAsset.VgoSpringBoneGroupList.Any() == false) &&
                ((ModelAsset.VgoSpringBoneColliderGroupList == null) || ModelAsset.VgoSpringBoneColliderGroupList.Any() == false))
            {
                return;
            }

            Layout.springBoneInfo = new VgoSpringBoneInfo();

            // springBoneInfo.springBoneGroups
            if (ModelAsset.VgoSpringBoneGroupList != null &&
                ModelAsset.VgoSpringBoneGroupList.Any())
            {
                Layout.springBoneInfo.springBoneGroups = new List<VgoSpringBoneGroup>(ModelAsset.VgoSpringBoneGroupList.Count);

                foreach (var component in ModelAsset.VgoSpringBoneGroupList)
                {
                    var boneGroup = new VgoSpringBoneGroup
                    {
                        comment = component.comment,
                        dragForce = component.dragForce,
                        stiffnessForce = component.stiffnessForce,
                        gravityDirection = component.gravityDirection.ToNumericsVector3(StorageAdapter.GeometryCoordinate),
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
                            boneGroup.rootBones[index] = Nodes.IndexOf(component.rootBones[index].transform);
                        }
                    }

                    // boneGroup.colliderGroups
                    if ((component.colliderGroups != null) &&
                        (component.colliderGroups.Length > 0) &&
                        (ModelAsset.VgoSpringBoneColliderGroupList != null))
                    {
                        boneGroup.colliderGroups = new int[component.colliderGroups.Length];

                        for (int index = 0; index < component.colliderGroups.Length; index++)
                        {
                            boneGroup.colliderGroups[index] = ModelAsset.VgoSpringBoneColliderGroupList.IndexOf(component.colliderGroups[index]);
                        }
                    }

                    Layout.springBoneInfo.springBoneGroups.Add(boneGroup);
                }
            }

            // springBoneInfo.colliderGroups
            if (ModelAsset.VgoSpringBoneColliderGroupList != null &&
                ModelAsset.VgoSpringBoneColliderGroupList.Any())
            {
                Layout.springBoneInfo.colliderGroups = new List<VgoSpringBoneColliderGroup>(ModelAsset.VgoSpringBoneColliderGroupList.Count);

                foreach (var component in ModelAsset.VgoSpringBoneColliderGroupList)
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
                                offset = collider.offset.ToNumericsVector3(StorageAdapter.GeometryCoordinate),
                                radius = collider.radius,
                            };
                        }
                    }

                    Layout.springBoneInfo.colliderGroups.Add(colliderGroup);
                }
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

                disposedValue = true;
            }
        }

        #endregion
    }
}
