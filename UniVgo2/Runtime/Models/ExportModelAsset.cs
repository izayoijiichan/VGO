// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : ExportModelAsset
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    /// <summary>
    /// Export Model Asset
    /// </summary>
    public class ExportModelAsset //: IDisposable
    {
        #region Fields

        /// <summary>The game object of root.</summary>
        private readonly GameObject _Root;

        /// <summary>List of unity transform.</summary>
        private readonly List<Transform> _TransformList;

        /// <summary>List of unity animation.</summary>
        private readonly List<Animation> _AnimationList;

        /// <summary>List of unity animation clip.</summary>
        private readonly List<AnimationClip> _AnimationClipList;

        /// <summary>List of unity cloth.</summary>
        private readonly List<Cloth> _ClothList;

        /// <summary>List of unity collider.</summary>
        private readonly List<Collider> _ColliderList;

        /// <summary>List of unity rigid body.</summary>
        private readonly List<Rigidbody> _RigidbodyList;

        /// <summary>List of unity material.</summary>
        private readonly List<Material> _MaterialList;

        /// <summary>List of unity mesh asset.</summary>
        private readonly List<MeshAsset> _MeshAssetList;

        /// <summary>List of unity mesh.</summary>
        private readonly List<Mesh> _MeshList;

        /// <summary>List of unity renderer.</summary>
        private readonly List<Renderer> _RendererList;

        /// <summary>List of unity renderer and mesh.</summary>
        private readonly List<ExportMeshRendererAsset> _MeshRendererAssetList;

        /// <summary>List of unity skinned mesh renderer.</summary>
        private readonly List<SkinnedMeshRenderer> _SkinnedMeshRendererList;

        /// <summary>List of unity skybox.</summary>
        private readonly List<Skybox> _SkyboxList;

        /// <summary>List of unity light.</summary>
        private readonly List<Light> _LightList;

        /// <summary>List of unity particle system asset.</summary>
        private readonly List<ParticleSystemAsset> _ParticleSystemAssetList;

        /// <summary>List of unity VgoSpringBoneGroup.</summary>
        private readonly List<VgoSpringBone.VgoSpringBoneGroup> _VgoSpringBoneGroupList;

        /// <summary>List of unity VgoSpringBoneColliderGroup.</summary>
        private readonly List<VgoSpringBone.VgoSpringBoneColliderGroup> _VgoSpringBoneColliderGroupList;

        ///// <summary>Whether export spec version is 2.4, otherwise 2.5.</summary>
        private readonly bool _IsExportSpecVersion_2_4 = false;

        #endregion

        #region Properties

        /// <summary>The game object of root.</summary>
        public GameObject Root => _Root;

        /// <summary>List of unity transform.</summary>
        public IList<Transform> TransformList => _TransformList;

        /// <summary>List of unity animation.</summary>
        public IList<Animation> AnimationList => _AnimationList;

        /// <summary>List of unity animation clip.</summary>
        public IList<AnimationClip> AnimationClipList => _AnimationClipList;

        /// <summary>List of unity cloth.</summary>
        public IList<Cloth> ClothList => _ClothList;

        /// <summary>List of unity collider.</summary>
        public IList<Collider> ColliderList => _ColliderList;

        /// <summary>List of unity rigid body.</summary>
        public IList<Rigidbody> RigidbodyList => _RigidbodyList;

        /// <summary>List of unity material.</summary>
        public IList<Material> MaterialList => _MaterialList;

        /// <summary>List of unity mesh asset.</summary>
        public IList<MeshAsset> MeshAssetList => _MeshAssetList;

        /// <summary>List of unity mesh.</summary>
        public IList<Mesh> MeshList => _MeshList;

        /// <summary>List of unity renderer.</summary>
        public IList<Renderer> RendererList => _RendererList;

        /// <summary>List of unity renderer and mesh.</summary>
        public IList<ExportMeshRendererAsset> MeshRendererAssetList => _MeshRendererAssetList;

        /// <summary>List of unity skinned mesh renderer.</summary>
        public IList<SkinnedMeshRenderer> SkinnedMeshRendererList => _SkinnedMeshRendererList;

        /// <summary>List of unity skybox.</summary>
        public IList<Skybox> SkyboxList => _SkyboxList;

        /// <summary>List of unity light.</summary>
        public IList<Light> LightList => _LightList;

        /// <summary>List of unity particle system asset.</summary>
        public IList<ParticleSystemAsset> ParticleSystemAssetList => _ParticleSystemAssetList;

        /// <summary>List of unity VgoSpringBoneGroup.</summary>
        public IList<VgoSpringBone.VgoSpringBoneGroup> VgoSpringBoneGroupList => _VgoSpringBoneGroupList;

        /// <summary>List of unity VgoSpringBoneColliderGroup.</summary>
        public IList<VgoSpringBone.VgoSpringBoneColliderGroup> VgoSpringBoneColliderGroupList => _VgoSpringBoneColliderGroupList;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of ExportModelAsset.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        public ExportModelAsset(GameObject gameObject)
        {
            _Root = gameObject;

            _TransformList = GetTransformList(_Root.transform);

            _AnimationList = GetUnityComponentList<Animation>(_TransformList);
            _AnimationClipList = GetUnityAnimationClipList(_AnimationList);

            _ClothList = GetUnityComponentList<Cloth>(_TransformList);

            _ColliderList = GetUnityComponentList<Collider>(_TransformList)
#if UNITY_2021_2_OR_NEWER
                .Where(c => c is BoxCollider or CapsuleCollider or SphereCollider)
#else
                .Where(c => (c is BoxCollider) || (c is CapsuleCollider) || (c is SphereCollider))
#endif
                .ToList();

            _RigidbodyList = GetUnityComponentList<Rigidbody>(_TransformList);

            _RendererList = GetUnityComponentList<Renderer>(_TransformList);

            _SkinnedMeshRendererList = _RendererList
                .Where(r => r is SkinnedMeshRenderer)
                .Select(r => (SkinnedMeshRenderer)r)
                .Where(x => x.bones != null && x.bones.Any())
                .ToList();

            _MeshRendererAssetList = GetMeshRendererAssetList(_RendererList);

            _MeshAssetList = GetMeshAssetList(_MeshRendererAssetList);

            _MeshList = GetMeshList(_MeshRendererAssetList);

            _SkyboxList = GetUnityComponentList<Skybox>(_TransformList);

            _MaterialList = GetUnityMaterialList(_RendererList, _SkyboxList);

            _LightList = GetUnityComponentList<Light>(_TransformList);

            _ParticleSystemAssetList = GetParticleSystemAssetList(_TransformList);

            _VgoSpringBoneGroupList = GetUnityComponentList<VgoSpringBone.VgoSpringBoneGroup>(_TransformList);
            _VgoSpringBoneColliderGroupList = GetUnityComponentList<VgoSpringBone.VgoSpringBoneColliderGroup>(_TransformList);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Get list of transform.
        /// </summary>
        /// <param name="rootTransform">Root transform.</param>
        /// <returns>List of node.</returns>
        protected virtual List<Transform> GetTransformList(Transform rootTransform)
        {
            return rootTransform.Traverse().ToList();
        }

        /// <summary>
        /// Get list of unity component.
        /// </summary>
        /// <param name="transformList">List of unity transform.</param>
        /// <returns>List of unity component.</returns>
        protected virtual List<T> GetUnityComponentList<T>(List<Transform> transformList) where T : Component
        {
            var componentList = new List<T>();

            for (int index = 0; index < transformList.Count; index++)
            {
                GameObject node = transformList[index].gameObject;

                if (node.TryGetComponentEx(out T component))
                {
                    componentList.Add(component);
                }
            }

            return componentList;
        }

        /// <summary>
        /// Get list of unity animation clip.
        /// </summary>
        /// <param name="animationList">List of unity animation.</param>
        /// <returns>List of unity animation clip.</returns>
        protected virtual List<AnimationClip> GetUnityAnimationClipList(List<Animation> animationList)
        {
            var animationClipList = new List<AnimationClip>();

            foreach (Animation animation in animationList)
            {
                if (animation.clip is null)
                {
                    continue;
                }

                int animationClipInstanceId = animation.clip.GetInstanceID();

                if (animationClipList.Exists(c => c.GetInstanceID() == animationClipInstanceId))
                {
                    continue;
                }

                animationClipList.Add(animation.clip);
            }

            return animationClipList;
        }

        /// <summary>
        /// Get list of unity material.
        /// </summary>
        /// <param name="rendererList">List of unity renderer.</param>
        /// <param name="skyboxList">List of unity skybox.</param>
        /// <returns>List of unity material.</returns>
        protected virtual List<Material> GetUnityMaterialList(List<Renderer> rendererList, List<Skybox> skyboxList)
        {
            var materialList = new List<Material>();

            foreach (Renderer renderer in rendererList)
            {
                Material[] sharedMaterials = renderer.sharedMaterials;

                for (int materialIndex = 0; materialIndex < sharedMaterials.Length; materialIndex++)
                {
                    Material material = sharedMaterials[materialIndex];

                    if (material == null)
                    {
                        continue;
                    }

                    int materialInstanceId = material.GetInstanceID();

                    if (materialList.Exists(m => m.GetInstanceID() == materialInstanceId))
                    {
                        continue;
                    }

                    materialList.Add(material);
                }
            }

            // Skybox
            foreach (Skybox skybox in skyboxList)
            {
                materialList.Add(skybox.material);
            }

            return materialList;
        }

        /// <summary>
        /// Get list of mesh renderer asset.
        /// </summary>
        /// <param name="rendererList">List of unity renderer.</param>
        /// <returns>List of mesh renderer asset.</returns>
        protected virtual List<ExportMeshRendererAsset> GetMeshRendererAssetList(List<Renderer> rendererList)
        {
            var meshRendererAssetList = new List<ExportMeshRendererAsset>();

            foreach (Renderer renderer in rendererList)
            {
                Mesh mesh;

                if (renderer is SkinnedMeshRenderer skinnedMeshRenderer)
                {
                    if (skinnedMeshRenderer.sharedMesh is null)
                    {
                        continue;
                    }

                    mesh = skinnedMeshRenderer.sharedMesh;
                }
                else if (renderer is MeshRenderer)
                {
                    if (renderer.gameObject.TryGetComponentEx(out MeshFilter meshFilter) == false)
                    {
                        continue;
                    }

                    if (meshFilter.sharedMesh is null)
                    {
                        continue;
                    }

                    mesh = meshFilter.sharedMesh;
                }
                else if (renderer is ParticleSystemRenderer particleSystemRenderer)
                {
                    if (particleSystemRenderer.renderMode != ParticleSystemRenderMode.Mesh)
                    {
                        continue;
                    }

                    if (particleSystemRenderer.mesh is null)
                    {
                        continue;
                    }

                    // @notice
                    mesh = particleSystemRenderer.mesh;
                }
                else
                {
                    continue;
                }

                var meshRendererAsset = new ExportMeshRendererAsset(renderer, mesh);

                meshRendererAssetList.Add(meshRendererAsset);
            }

            return meshRendererAssetList;
        }

        /// <summary>
        /// Get list of unity mesh.
        /// </summary>
        /// <param name="rendererAssetList">List of export mesh renderer asset.</param>
        /// <returns>List of export mesh asset.</returns>
        protected virtual List<MeshAsset> GetMeshAssetList(List<ExportMeshRendererAsset> meshRendererAssetList)
        {
            var meshAssetList = new List<MeshAsset>();

            foreach (ExportMeshRendererAsset meshRendererAsset in meshRendererAssetList)
            {
                Mesh? mesh = meshRendererAsset.Mesh;

                if (mesh is null)
                {
                    continue;
                }

                MeshAsset meshAsset;

                if (_IsExportSpecVersion_2_4)
                {
                    meshAsset = new MeshAsset(mesh)
                    {
                        Renderer = meshRendererAsset.Renderer,
                    };
                }
                else
                {
                    int meshInstanceId = mesh.GetInstanceID();

                    if (meshAssetList.Exists(x => x.Mesh.GetInstanceID() == meshInstanceId))
                    {
                        continue;
                    }

                    meshAsset = new MeshAsset(mesh);
                }

                if (meshRendererAsset.Renderer is SkinnedMeshRenderer skinnedMeshRenderer)
                {
                    if (skinnedMeshRenderer.gameObject.TryGetComponentEx(out VgoBlendShape vgoBlendShape))
                    {
                        meshAsset.BlendShapeConfiguration = vgoBlendShape.BlendShapeConfiguration;
                    }
                }

                meshAssetList.Add(meshAsset);
            }

            return meshAssetList;
        }

        /// <summary>
        /// Get list of unity mesh.
        /// </summary>
        /// <param name="rendererAssetList">List of mesh renderer asset.</param>
        /// <returns>List of unity mesh.</returns>
        protected virtual List<Mesh> GetMeshList(List<ExportMeshRendererAsset> meshRendererAssetList)
        {
            var meshList = new List<Mesh>();

            foreach (ExportMeshRendererAsset meshRendererAsset in meshRendererAssetList)
            {
                Mesh mesh = meshRendererAsset.Mesh;

                if (mesh is null)
                {
                    continue;
                }

                int meshInstanceId = mesh.GetInstanceID();

                if (meshList.Exists(m => m.GetInstanceID() == meshInstanceId))
                {
                    continue;
                }

                meshList.Add(mesh);
            }

            return meshList;
        }

        /// <summary>
        /// Get list of unity particle system asset.
        /// </summary>
        /// <param name="transformList">List of unity transform.</param>
        /// <returns>List of unity particle system asset.</returns>
        protected virtual List<ParticleSystemAsset> GetParticleSystemAssetList(List<Transform> transformList)
        {
            var particleSystemAssetList = new List<ParticleSystemAsset>();

            List<ParticleSystem> particleSystemList = GetUnityComponentList<ParticleSystem>(_TransformList);

            foreach (ParticleSystem particleSystem in particleSystemList)
            {
                if (particleSystem.gameObject.TryGetComponentEx(out ParticleSystemRenderer particleSystemRenderer) == false)
                {
                    continue;
                }

                var particleSystemAsset = new ParticleSystemAsset(particleSystem, particleSystemRenderer);

                particleSystemAssetList.Add(particleSystemAsset);
            }

            return particleSystemAssetList;
        }

        #endregion
    }
}
