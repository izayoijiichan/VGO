// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : VgoImporter
// ----------------------------------------------------------------------
namespace UniVgo
{
    using NewtonGltf;
    using System.Collections.Generic;
    using UnityEngine;
    using UniVgo.Converters;
    using UniVgo.Porters;

    /// <summary>
    /// VGO Importer
    /// </summary>
    public class VgoImporter : GlbImporter
    {
        #region Constructors

        /// <summary>
        /// Create a new instance of VgoImporter.
        /// </summary>
        public VgoImporter() : base()
        {
        }

        #endregion

        #region Fields

        /// <summary>The ParticleSystem importer.</summary>
        protected VgoParticleSystemImporter _ParticleSystemImporter = new VgoParticleSystemImporter();

        #endregion

        #region Methods

        /// <summary>
        /// Load a 3D model from the specified glTF storage.
        /// </summary>
        /// <param name="gltfStorage">A glTF storage.</param>
        /// <returns>A model asset.</returns>
        public override ModelAsset Load(GltfStorage gltfStorage)
        {
            ModelAsset modelAsset = base.Load(gltfStorage);

            SetupComponents();

            return modelAsset;
        }

        /// <summary>
        /// Set up the components.
        /// </summary>
        protected virtual void SetupComponents()
        {
            // root
            SetupRootComponent();

            // nodes
            for (int idx = 0; idx < Gltf.nodes.Count; idx++)
            {
                SetupChildComponent(Nodes[idx].gameObject, Gltf.nodes[idx]);
            }
        }

        /// <summary>
        /// Set up the root GameObject components.
        /// </summary>
        protected virtual void SetupRootComponent()
        {
            if (Gltf.extensions != null)
            {
                Gltf.extensions.JsonSerializerSettings = _JsonSerializerSettings;

                if (Gltf.extensions.Contains(Gltf_VGO.ExtensionName))
                {
                    Gltf_VGO vgo = Gltf.extensions.GetValueOrDefault<Gltf_VGO>(Gltf_VGO.ExtensionName);

                    ModelAsset.Root.AddComponent<VgoMeta>(vgo.meta);
                    ModelAsset.Root.AddComponent<VgoRight>(vgo.right);

                    if (vgo.avatar != null)
                    {
                        Animator animator = ModelAsset.Root.AddComponent<Animator>();

                        animator.avatar = VgoAvatarConverter.CreateHumanAvatar(vgo.avatar, ModelAsset.Root, Nodes);

                        CreateAndAddAvatarConfiguration(vgo.avatar);

                        ModelAsset.Avatar = animator.avatar;
                    }
                }
            }
        }

        /// <summary>
        /// Set up the child GameObject components.
        /// </summary>
        /// <param name="go"></param>
        /// <param name="gltfNode"></param>
        protected virtual void SetupChildComponent(GameObject go, GltfNode gltfNode)
        {
            if (gltfNode.extensions != null)
            {
                gltfNode.extensions.JsonSerializerSettings = _JsonSerializerSettings;

                if (gltfNode.extensions.Contains(VGO_nodes.ExtensionName))
                {
                    VGO_nodes nodeVGO = gltfNode.extensions.GetValueOrDefault<VGO_nodes>(VGO_nodes.ExtensionName);

                    if (nodeVGO == null)
                    {
                        Debug.LogWarning("Deseriarize VGO_nodes failed.");
                    }

                    // VgoGameObject
                    if (nodeVGO.gameObject != null)
                    {
                        VgoGameObjectConverter.SetGameObjectValue(go, nodeVGO.gameObject);
                    }

                    // Collider[]
                    if (nodeVGO.colliders != null)
                    {
                        var colliders = nodeVGO.colliders;

                        colliders.ForEach(vc => go.AddComponent<Collider>(vc));
                    }

                    // Rigidbody
                    if (nodeVGO.rigidbody != null)
                    {
                        go.AddComponent<Rigidbody>(nodeVGO.rigidbody);
                    }

                    // Light
                    if (nodeVGO.light != null)
                    {
                        go.AddComponent<Light>(nodeVGO.light);
                    }

                    // ParticleSystem
                    if (nodeVGO.particleSystem != null)
                    {
                        _ParticleSystemImporter.AddComponent(go, nodeVGO.particleSystem, ModelAsset.MaterialList, ModelAsset.Texture2dList);
                    }

                    // VgoRight
                    if (nodeVGO.right != null)
                    {
                        go.AddComponent<VgoRight>(nodeVGO.right);
                    }

                    // VgoSkybox
                    if (nodeVGO.skybox != null)
                    {
                        var skybox = go.AddComponent<Skybox>();

                        if (ModelAsset.MaterialList != null)
                        {
                            if (ModelAsset.MaterialList.TryGetValue(nodeVGO.skybox.materialIndex, out Material skyboxMaterial))
                            {
                                skybox.material = skyboxMaterial;
                            }
                        }
                    }

                    // VgoBlendShape
                    if (nodeVGO.blendShape != null)
                    {
                        //go.AddComponent<VgoBlendShape>(nodeVGO.blendShape);

                        AddBlendShapeComponent(go, nodeVGO.blendShape);
                    }
                }
            }
        }

        /// <summary>
        /// Create and add avatar configuration.
        /// </summary>
        /// <param name="vgoHumanAvatar">The VGO human avatar.</param>
        protected virtual void CreateAndAddAvatarConfiguration(VGO_HumanAvatar vgoHumanAvatar)
        {
            var avatarConfiguration = ScriptableObject.CreateInstance<AvatarConfiguration>();

            avatarConfiguration.name = vgoHumanAvatar.name;
            avatarConfiguration.humanBones = vgoHumanAvatar.humanBones;

            ModelAsset.ScriptableObjectList.Add(avatarConfiguration);
        }

        /// <summary>
        /// Attach VGO_BlendShape component to GameObject.
        /// </summary>
        /// <param name="go">A game object.</param>
        /// <param name="gltfVgoBlendShape">A vgo blend shape.</param>
        protected virtual void AddBlendShapeComponent(GameObject go, VGO_BlendShape gltfVgoBlendShape)
        {
            VgoBlendShape vgoBlendShape = go.GetComponent<VgoBlendShape>();

            if (vgoBlendShape == null)
            {
                vgoBlendShape = go.AddComponent<VgoBlendShape>();
            }

            var blendShapeConfiguration = ScriptableObject.CreateInstance<BlendShapeConfiguration>();

            blendShapeConfiguration.name = gltfVgoBlendShape.name;
            blendShapeConfiguration.kind = gltfVgoBlendShape.kind;
            blendShapeConfiguration.faceParts = gltfVgoBlendShape.faceParts;
            blendShapeConfiguration.blinks = gltfVgoBlendShape.blinks;
            blendShapeConfiguration.visemes = gltfVgoBlendShape.visemes;
            blendShapeConfiguration.presets = new List<BlendShapePreset>();

            if (gltfVgoBlendShape.presets != null)
            {
                foreach (VGO_BlendShapePreset vgoPreset in gltfVgoBlendShape.presets)
                {
                    var preset = new BlendShapePreset
                    {
                        name = vgoPreset.name,
                        type = vgoPreset.type,
                        bindings = new List<BlendShapeBinding>()
                    };

                    foreach (VGO_BlendShapeBinding vgoBinding in vgoPreset.bindings)
                    {
                        var binding = new BlendShapeBinding
                        {
                            index = vgoBinding.index,
                            weight = vgoBinding.weight,
                        };

                        preset.bindings.Add(binding);
                    }

                    blendShapeConfiguration.presets.Add(preset);
                }
            }

            vgoBlendShape.BlendShapeConfiguration = blendShapeConfiguration;

            ModelAsset.ScriptableObjectList.Add(blendShapeConfiguration);
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
    }
}