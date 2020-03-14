// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : VgoExporter
// ----------------------------------------------------------------------
namespace UniVgo
{
    using System.Collections.Generic;
    using System.Linq;
    using UniGLTFforUniVgo;
    using UnityEngine;

    /// <summary>
    /// VGO Exporter
    /// </summary>
    public class VgoExporter : gltfExporter
    {
        #region Fields

        /// <summary>Names of glTF extensions used somewhere in this asset.</summary>
        protected override IEnumerable<string> ExtensionUsed
        {
            get => new string[]
            {
                glTF_VGO.ExtensionName,
                VGO_nodes.ExtensionName,
                VGO_materials.ExtensionName,
                VGO_materials_particle.ExtensionName,
                VGO_materials_skybox.ExtensionName,
                KHR_materials_unlit.ExtensionName,
                VRMC_materials_mtoon.ExtensionName,
            };
        }

        /// <summary>Names of glTF extensions required to properly load this asset.</summary>
        protected override IEnumerable<string> ExtensionsRequired
        {
            get => new string[]
            {
                glTF_VGO.ExtensionName,
                VGO_nodes.ExtensionName,
                VGO_materials.ExtensionName,
                VGO_materials_particle.ExtensionName,
                VGO_materials_skybox.ExtensionName,
                KHR_materials_unlit.ExtensionName,
                VRMC_materials_mtoon.ExtensionName,
            };
        }

        #endregion

        #region Properties

        /// <summary>VGO ParticleSystem exporter.</summary>
        public virtual VgoParticleSystemExporter VgoParticleSystemExporter { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of VgoExporter with gltf.
        /// </summary>
        /// <param name="gltf"></param>
        public VgoExporter(glTF gltf) : base(gltf)
        {
            VgoParticleSystemExporter = new VgoParticleSystemExporter();
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override IMaterialExporter CreateMaterialExporter()
        {
            return new VgoMaterialExporter();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Export()
        {
            base.Export();

            SetExtensions();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gltf"></param>
        /// <param name="go"></param>
        /// <param name="bufferIndex"></param>
        protected override void FromGameObjectMaterialsAndTextures(glTF gltf, GameObject go, int bufferIndex)
        {
            Materials = Nodes.SelectMany(x => x.GetSharedMaterials()).Where(x => x != null).Distinct().ToList();

            var skybox = go.GetComponentInChildren<Skybox>();

            if (skybox != null)
            {
                Materials.Add(skybox.material);
            }

            var unityTextures = Materials.SelectMany(x => TextureIO.GetTextures(x)).Where(x => x.Texture != null).Distinct().ToList();

            TextureManager = new TextureExportManager(unityTextures.Select(x => x.Texture));

            var materialExporter = CreateMaterialExporter();
            gltf.materials = Materials.Select(x => materialExporter.ExportMaterial(x, TextureManager)).ToList();

            for (int i = 0; i < unityTextures.Count; ++i)
            {
                var unityTexture = unityTextures[i];
                TextureIO.ExportTexture(gltf, bufferIndex, TextureManager.GetExportTexture(i), unityTexture.TextureType);
            }
        }

        /// <summary>
        /// Set the extensions.
        /// </summary>
        protected virtual void SetExtensions()
        {
            // gltf.extensions
            SetGltfRootExtensions(glTF, Copy);

            // glft.nodes.[*].extensions
            for (int idx = 0; idx < Nodes.Count; idx++)
            {
                SetGltfNodeExtensions(glTF.nodes[idx], Nodes[idx].gameObject);
            }
        }

        /// <summary>
        /// Set the extension of the glTF root.
        /// </summary>
        /// <param name="gltfNode"></param>
        /// <param name="rootGameObject"></param>
        protected virtual void SetGltfRootExtensions(glTF gltf, GameObject rootGameObject)
        {
            if (gltf.extensions == null)
            {
                gltf.extensions = new glTF_extensions();
            }

            if (gltf.extensions.VGO == null)
            {
                gltf.extensions.VGO = new glTF_VGO();
            }

            // vgo.meta
            gltf.extensions.VGO.meta = new glTF_VGO_Meta()
            {
                generatorName = Vgo.Generator,
                generatorVersion = VgoVersion.VERSION,
                specVersion = Vgo.SpecVersion,
            };

            // vgo.right
            if (rootGameObject.TryGetComponentEx(out VgoRight vgoRight))
            {
                if (vgoRight.Right != null)
                {
                    gltf.extensions.VGO.right = new glTF_VGO_Right(vgoRight.Right);
                }
            }
        }

        /// <summary>
        /// Set the extension of the glTF node.
        /// </summary>
        /// <param name="gltfNode"></param>
        /// <param name="srcNode"></param>
        protected virtual void SetGltfNodeExtensions(glTFNode gltfNode, GameObject srcNode)
        {
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
                nodeVgo.particleSystem = VgoParticleSystemExporter.Create(particleSystem, particleSystemRenderer, glTF);

                existsData = true;
            }

            // vgo.right
            if (srcNode.TryGetComponentEx(out VgoRight vgoRight))
            {
                nodeVgo.right = new glTF_VGO_Right(vgoRight.Right);

                existsData = true;
            }

            // vgo.skybox
            if (srcNode.TryGetComponentEx(out Skybox skybox))
            {
                nodeVgo.skybox = new VGO_Skybox(skybox, glTF);

                existsData = true;
            }

            if (existsData)
            {
                if (gltfNode.extensions == null)
                {
                    gltfNode.extensions = new glTFNode_extensions();
                }

                gltfNode.extensions.VGO_nodes = nodeVgo;
            }
        }

        #endregion
    }
}