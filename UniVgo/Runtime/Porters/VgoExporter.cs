// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : VgoExporter
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

    /// <summary>
    /// VGO Exporter
    /// </summary>
    public class VgoExporter : GlbExporter
    {
        #region Constructors

        /// <summary>
        /// Create a new instance of VgoExporter.
        /// </summary>
        public VgoExporter() : base()
        {
        }

        #endregion
        
        #region Fields

        /// <summary>The JSON serializer settings.</summary>
        protected VgoJsonSerializerSettings _JsonSerializerSettings = new VgoJsonSerializerSettings();

        /// <summary>The ParticleSystem exporter.</summary>
        protected VgoParticleSystemExporter _ParticleSystemExporter = new VgoParticleSystemExporter();

        /// <summary>Whether used VGO_nodes extensions.</summary>
        protected bool _UsedVgoNode = false;

        #endregion

        #region Methods

        /// <summary>
        /// Create list of unity material.
        /// </summary>
        /// <returns>list of unity material.</returns>
        protected override List<Material> CreateUnityMaterialList()
        {
            List<Material> unityMaterialList = base.CreateUnityMaterialList();

            Skybox skybox = ModelAsset.Root.GetComponentInChildren<Skybox>();

            if (skybox != null)
            {
                unityMaterialList.Add(skybox.material);
            }

            return unityMaterialList;
        }

        #endregion

        #region gltf.extensions

        /// <summary>
        /// Set the extension of the glTF root.
        /// </summary>
        protected override void SetGltfRootExtensions()
        {
            base.SetGltfRootExtensions();

            // vgo.meta
            var vgo = new Gltf_VGO
            {
                meta = new Gltf_VGO_Meta()
                {
                    generatorName = Vgo.Generator,
                    generatorVersion = VgoVersion.VERSION,
                    specVersion = Vgo.SpecVersion,
                }
            };

            // vgo.right
            if (ModelAsset.Root.TryGetComponentEx(out VgoRight vgoRight))
            {
                if (vgoRight.Right != null)
                {
                    vgo.right = new Gltf_VGO_Right(vgoRight.Right);
                }
            }

            // vgo.avatar
            if (ModelAsset.Root.TryGetComponentEx(out Animator animator))
            {
                if (animator.avatar != null)
                {
                    if (animator.avatar.isHuman && animator.avatar.isValid)
                    {
                        vgo.avatar = VgoAvatarConverter.CreateVgoHumanAvatar(animator, ModelAsset.Root.name, Nodes);
                    }
                }
            }

            if (Gltf.extensions == null)
            {
                Gltf.extensions = new GltfExtensions();
            }

            Gltf.extensions.JsonSerializerSettings = _JsonSerializerSettings;

            Gltf.extensions.Add(Gltf_VGO.ExtensionName, vgo);
        }

        #endregion

        #region gltf.nodes[*].extensions

        /// <summary>
        /// Set the extension of the glTF node.
        /// </summary>
        /// <param name="gltfNode">A gltf.node.</param>
        /// <param name="srcNode">A game object.</param>
        protected override void SetGltfNodeExtensions(GltfNode gltfNode, GameObject srcNode)
        {
            base.SetGltfNodeExtensions(gltfNode, srcNode);

            var nodeVgo = new VGO_nodes();

            bool existsData = false;

            // vgo.gameObject
            nodeVgo.gameObject = VgoGameObjectConverter.CreateFrom(srcNode);

            if (nodeVgo.gameObject != null)
            {
                existsData = true;
            }

            // vgo.colliders
            if (srcNode.TryGetComponentsEx(out Collider[] colliders))
            {
                nodeVgo.colliders = colliders
                    .Select(collider => VgoColliderConverter.CreateFrom(collider))
                    .Where(vc => vc != null)
                    .ToList();

                existsData = true;
            }

            // vgo.rigidbody
            if (srcNode.TryGetComponentEx(out Rigidbody rigidbody))
            {
                nodeVgo.rigidbody = VgoRigidbodyConverter.CreateFrom(rigidbody);

                existsData = true;
            }

            // vgo.light
            if (srcNode.TryGetComponentEx(out Light light))
            {
                nodeVgo.light = VgoLightConverter.CreateFrom(light);

                existsData = true;
            }

            // vgo.particleSystem
            if (srcNode.TryGetComponentEx(out ParticleSystem particleSystem) &&
                srcNode.TryGetComponentEx(out ParticleSystemRenderer particleSystemRenderer))
            {
                nodeVgo.particleSystem = _ParticleSystemExporter.Create(particleSystem, particleSystemRenderer, GltfStorageAdapter, _ExporterTexture);

                existsData = true;
            }

            // vgo.right
            if (srcNode.TryGetComponentEx(out VgoRight vgoRight))
            {
                nodeVgo.right = new Gltf_VGO_Right(vgoRight.Right);

                existsData = true;
            }

            // vgo.skybox
            if (srcNode.TryGetComponentEx(out Skybox skybox))
            {
                GltfMaterial skyboxMaterial = Gltf.materials.Where(m => m.name == skybox.material.name).FirstOrDefault();

                nodeVgo.skybox = new VGO_Skybox
                {
                    materialIndex = (skyboxMaterial == null) ? -1 : Gltf.materials.IndexOf(skyboxMaterial)
                };

                existsData = true;
            }

            // vgo.blendShape
            if (srcNode.TryGetComponentEx(out VgoBlendShape vgoBlendShape))
            {
                nodeVgo.blendShape = VgoBlendShapeConverter.CreateFrom(vgoBlendShape.BlendShapeConfiguration);

                existsData = true;
            }

            if (existsData)
            {
                if (gltfNode.extensions == null)
                {
                    gltfNode.extensions = new GltfExtensions(_JsonSerializerSettings);
                }

                gltfNode.extensions.Add(VGO_nodes.ExtensionName, nodeVgo);

                _UsedVgoNode = true;
            }
        }

        #endregion

        #region gltf.extensionsUsed

        /// <summary>
        /// Set gltf.extensionsUsed from gltf.extensions.
        /// </summary>
        protected override void SetExtensionsUsedFromGltfRoot()
        {
            base.SetExtensionsUsedFromGltfRoot();

            if (Gltf.extensions.Contains(Gltf_VGO.ExtensionName))
            {
                if (Gltf.extensionsUsed.Contains(Gltf_VGO.ExtensionName) == false)
                {
                    Gltf.extensionsUsed.Add(Gltf_VGO.ExtensionName);
                }
            }
        }

        /// <summary>
        /// Set gltf.extensionsUsed from gltf.nodes.extensions.
        /// </summary>
        protected override void SetExtensionsUsedFromNode()
        {
            base.SetExtensionsUsedFromNode();

            if (_UsedVgoNode)
            {
                if (Gltf.extensionsUsed.Contains(VGO_nodes.ExtensionName) == false)
                {
                    Gltf.extensionsUsed.Add(VGO_nodes.ExtensionName);
                }
            }
        }

        #endregion
    }
}