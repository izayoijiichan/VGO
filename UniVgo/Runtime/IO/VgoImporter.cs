// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : VgoImporter
// ----------------------------------------------------------------------
namespace UniVgo
{
    using System.Collections.Generic;
    using UniGLTFforUniVgo;
    using UnityEngine;

    /// <summary>
    /// VGO Importer
    /// </summary>
    public class VgoImporter : ImporterContext
    {
        #region Properties

        /// <summary>VGO ParticleSystem importer.</summary>
        public virtual VgoParticleSystemImporter VgoParticleSystemImporter { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of VgoImporter.
        /// </summary>
        public VgoImporter()
        {
            SetMaterialImporter(new VgoMaterialImporter(this));
            VgoParticleSystemImporter = new VgoParticleSystemImporter();
        }

        #endregion

        #region Methods

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

                    // Light
                    if (nodeVGO.light != null)
                    {
                        go.AddComponent<Light>(nodeVGO.light);
                    }

                    // ParticleSystem
                    if (nodeVGO.particleSystem != null)
                    {
                        VgoParticleSystemImporter.AddComponent(go, nodeVGO.particleSystem, GetMaterials(), GetTextures());
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

                        List<Material> materials = GetMaterials() as List<Material>;

                        if (materials != null)
                        {
                            int vgoMaterialIndex = nodeVGO.skybox.materialIndex;

                            if ((0 <= vgoMaterialIndex) && (vgoMaterialIndex < materials.Count))
                            {
                                skybox.material = materials[vgoMaterialIndex];
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="showMeshes"></param>
        /// <param name="enableUpdateWhenOffscreen"></param>
        public override void OnLoadModelAfter(bool showMeshes = false, bool enableUpdateWhenOffscreen = false)
        {
            base.OnLoadModelAfter(showMeshes, enableUpdateWhenOffscreen);

            ReflectSkybox();
        }

        /// <summary>
        /// Reflect VGO skybox to Camaera skybox.
        /// </summary>
        protected virtual void ReflectSkybox()
        {
            var vgoSkybox = Root.GetComponentInChildren<Skybox>(includeInactive: false);

            if (vgoSkybox != null)
            {
                GameObject mainCameraGameObject = Camera.main.gameObject;

                var cameraSkybox = mainCameraGameObject.GetOrAddComponent<Skybox>();

                cameraSkybox.material = vgoSkybox.material;
            }
        }

        #endregion
    }
}