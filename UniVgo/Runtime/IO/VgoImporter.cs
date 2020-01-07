// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : VgoImporter
// ----------------------------------------------------------------------
namespace UniVgo
{
    using UniGLTFforUniVgo;
    using UnityEngine;

    /// <summary>
    /// VGO Importer
    /// </summary>
    public class VgoImporter : ImporterContext
    {
        /// <summary>
        /// Create a new instance of VgoImporter.
        /// </summary>
        public VgoImporter()
        {
        }

        /// <summary>
        /// Called when the model is loaded.
        /// </summary>
        protected override void OnLoadModel()
        {
            base.OnLoadModel();

            SetupComponents();
        }

        /// <summary>
        /// Set up the components.
        /// </summary>
        protected virtual void SetupComponents()
        {
            // root
            SetupRootComponent();

            // nodes
            for (int idx = 0; idx < GLTF.nodes.Count; idx++)
            {
                SetupChildComponent(Nodes[idx].gameObject, GLTF.nodes[idx]);
            }
        }

        /// <summary>
        /// Set up the root GameObject components.
        /// </summary>
        protected virtual void SetupRootComponent()
        {
            if (GLTF.extensions != null)
            {
                if (GLTF.extensions.VGO != null)
                {
                    Root.AddComponent<VgoMeta>(GLTF.extensions.VGO.meta);
                    Root.AddComponent<VgoRight>(GLTF.extensions.VGO.right);
                }
            }
        }

        /// <summary>
        /// Set up the child GameObject components.
        /// </summary>
        /// <param name="go"></param>
        /// <param name="gltfNode"></param>
        protected virtual void SetupChildComponent(GameObject go, glTFNode gltfNode)
        {
            if (gltfNode.extensions != null)
            {
                if (gltfNode.extensions.VGO_nodes != null)
                {
                    var nodeVGO = gltfNode.extensions.VGO_nodes;

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

                    // VgoRight
                    if (nodeVGO.right != null)
                    {
                        go.AddComponent<VgoRight>(nodeVGO.right);
                    }
                }
            }
        }
    }
}