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
                glTF_KHR_materials_unlit.ExtensionName,
                glTF_VGO.ExtensionName,
                glTFNode_VGO.ExtensionName,
            };
        }

        /// <summary>Names of glTF extensions required to properly load this asset.</summary>
        protected override IEnumerable<string> ExtensionsRequired
        {
            get => new string[]
            {
                glTF_KHR_materials_unlit.ExtensionName,
                glTF_VGO.ExtensionName,
                glTFNode_VGO.ExtensionName,
            };
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of VgoExporter with gltf.
        /// </summary>
        /// <param name="gltf"></param>
        public VgoExporter(glTF gltf) : base(gltf)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        public override void Export()
        {
            base.Export();

            SetExtensions();
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
            var nodeVgo = new glTFNode_VGO();

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

            // vgo.right
            if (srcNode.TryGetComponentEx(out VgoRight vgoRight))
            {
                nodeVgo.right = new glTF_VGO_Right(vgoRight.Right);

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