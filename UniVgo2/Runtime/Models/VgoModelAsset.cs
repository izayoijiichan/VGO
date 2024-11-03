// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : VgoModelAsset
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using NewtonVgo;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
#if UNITY_EDITOR
    using UnityEditor;
#endif

    /// <summary>
    /// VGO Model Asset
    /// </summary>
    [Serializable]
    public class VgoModelAsset : IVgoModelAsset, IDisposable
    {
        #region Fields

        /// <summary>A game object of root.</summary>
        private GameObject? _Root = null;

        /// <summary>An avatar.</summary>
        private Avatar? _Avatar = null;

        /// <summary>List of unity animation clip.</summary>
        private List<AnimationClip?>? _AnimationClipList = null;

        /// <summary>List of unity collider.</summary>
        private List<Collider?>? _ColliderList = null;

        /// <summary>List of unity material.</summary>
        private List<Material?>? _MaterialList = null;

        /// <summary>List of unity mesh and renderer.</summary>
        private List<MeshAsset>? _MeshAssetList = null;

        /// <summary>List of unity texture.</summary>
        private List<Texture?>? _TextureList = null;

        /// <summary>List of scriptable object.</summary>
        private readonly List<ScriptableObject> _ScriptableObjectList = new List<ScriptableObject>();

        /// <summary>Array of spring bone collider group.</summary>
        private VgoSpringBone.VgoSpringBoneColliderGroup?[]? _SpringBoneColliderGroupArray = null;

        /// <summary>A vgo layout.</summary>
        private VgoLayout? _Layout = null;

        /// <summary></summary>
        protected bool disposed;

        #endregion

        #region Properties

        /// <summary>A game object of root.</summary>
        public GameObject? Root { get => _Root; set => _Root = value; }

        /// <summary>An avatar.</summary>
        public Avatar? Avatar { get => _Avatar; set => _Avatar = value; }

        /// <summary>List of unity animation clip.</summary>
        public List<AnimationClip?>? AnimationClipList { get => _AnimationClipList; set => _AnimationClipList = value; }

        /// <summary>List of unity collider.</summary>
        public List<Collider?>? ColliderList { get => _ColliderList; set => _ColliderList = value; }

        /// <summary>List of unity material.</summary>
        public List<Material?>? MaterialList { get => _MaterialList; set => _MaterialList = value; }

        /// <summary>List of unity mesh and renderer.</summary>
        public List<MeshAsset>? MeshAssetList { get => _MeshAssetList; set => _MeshAssetList = value; }

        /// <summary>List of unity texture.</summary>
        public List<Texture?>? TextureList { get => _TextureList; set => _TextureList = value; }

        /// <summary>List of scriptable object.</summary>
        public List<ScriptableObject> ScriptableObjectList => _ScriptableObjectList;

        /// <summary>Array of spring bone collider group.</summary>
        public VgoSpringBone.VgoSpringBoneColliderGroup?[]? SpringBoneColliderGroupArray { get => _SpringBoneColliderGroupArray; set => _SpringBoneColliderGroupArray = value; }

        /// <summary>A vgo layout.</summary>
        public VgoLayout? Layout { get => _Layout; set => _Layout = value; }

        #endregion

        #region Constructors

        // ~VgoModelAsset()
        // {
        //     Dispose(disposing: false);
        // }

        #endregion

        #region Public Methods (Blend Shape)

        /// <summary>
        /// Gets the first components of the VgoBlendShape type, if it exists.
        /// </summary>
        /// <param name="faceOnly"></param>
        /// <param name="includeInactive">Whether to include inactive child GameObjects in the search.</param>
        /// <param name="vgoBlendShapes"></param>
        /// <returns>Returns true if the components is found, false otherwise.</returns>
        /// <remarks>breadth-first search</remarks>
        public bool TryGetVgoBlendShapeComponents(in bool faceOnly, in bool includeInactive, out List<VgoBlendShape> vgoBlendShapes)
        {
            if (_Root == null)
            {
                vgoBlendShapes = new List<VgoBlendShape>(0);

                return false;
            }

            if (faceOnly)
            {
                bool isFound = _Root.TryGetComponentsInChildren(includeInactive, out vgoBlendShapes);

                if (isFound == false)
                {
                    return false;
                }

                vgoBlendShapes = vgoBlendShapes
                    .Where(x => (x.BlendShapeConfiguration != null) &&
                        ((x.BlendShapeConfiguration.Kind == VgoBlendShapeKind.Face) ||
                        (x.BlendShapeConfiguration.Kind == VgoBlendShapeKind.Face_2)))
                    .ToList();

                return vgoBlendShapes.Any();
            }
            else
            {
                return _Root.TryGetComponentsInChildren(includeInactive, out vgoBlendShapes);
            }
        }

        /// <summary>
        /// Gets the first component of the VgoBlendShape type, if it exists.
        /// </summary>
        /// <param name="faceOnly">Only the kind Face or Face_2 is retrieved.</param>
        /// <param name="vgoBlendShape"></param>
        /// <returns>Returns true if the components is found, false otherwise.</returns>
        /// <remarks>breadth-first search</remarks>
        public bool TryGetFirstVgoBlendShapeComponent(in bool faceOnly, out VgoBlendShape vgoBlendShape)
        {
            return TryGetFirstVgoBlendShapeComponent(faceOnly, includeInactive: false, out vgoBlendShape);
        }

        /// <summary>
        /// Gets the first component of the VgoBlendShape type, if it exists.
        /// </summary>
        /// <param name="faceOnly">Only the kind Face or Face_2 is retrieved.</param>
        /// <param name="includeInactive">Whether to include inactive child GameObjects in the search.</param>
        /// <param name="vgoBlendShape"></param>
        /// <returns>Returns true if the components is found, false otherwise.</returns>
        /// <remarks>breadth-first search</remarks>
        public bool TryGetFirstVgoBlendShapeComponent(in bool faceOnly, in bool includeInactive, out VgoBlendShape vgoBlendShape)
        {
#pragma warning disable CS8625
            vgoBlendShape = default;
#pragma warning restore CS8625

            if (_Root == null)
            {
                return false;
            }

            if (faceOnly)
            {
                if (TryGetFirstVgoBlendShapeComponentRecursive(_Root, includeInactive, VgoBlendShapeKind.Face, out vgoBlendShape))
                {
                    return true;
                }

                if (TryGetFirstVgoBlendShapeComponentRecursive(_Root, includeInactive, VgoBlendShapeKind.Face_2, out vgoBlendShape))
                {
                    return true;
                }

                return false;
            }

            return _Root.TryGetFirstComponentInChildren(includeInactive, out vgoBlendShape);
        }

        #endregion

        #region Protected Methods (Blend Shape)

        /// <summary>
        /// Gets the first component of the VgoBlendShape type, if it exists.
        /// </summary>
        /// <param name="go"></param>
        /// <param name="includeInactive">Whether to include inactive child GameObjects in the search.</param>
        /// <param name="vgoBendShapeKind"></param>
        /// <param name="vgoBlendShape"></param>
        /// <returns>Returns true if the components is found, false otherwise.</returns>
        /// <remarks>breadth-first search</remarks>
        protected virtual bool TryGetFirstVgoBlendShapeComponentRecursive(in GameObject go, in bool includeInactive, in VgoBlendShapeKind vgoBendShapeKind, out VgoBlendShape vgoBlendShape)
        {
            GameObject[] children = go.GetChildren().ToArray();

            VgoBlendShapeKind kind = vgoBendShapeKind;

            // Sibling
            for (int index = 0; index < children.Length; index++)
            {
                GameObject child = children[index];

                if (includeInactive)
                {
                    if (child.activeSelf == false)
                    {
                        continue;
                    }
                }

                if (child.TryGetComponentsEx(out VgoBlendShape[] vgoBlendShapes) == false)
                {
                    continue;
                }

                VgoBlendShape? faceBlendShape = vgoBlendShapes
                    .FirstOrDefault(x =>
                        x.BlendShapeConfiguration != null &&
                        x.BlendShapeConfiguration.Kind == kind);

                if (faceBlendShape != null)
                {
                    vgoBlendShape = faceBlendShape;

                    return true;
                }
            }

            // Child
            for (int index = 0; index < children.Length; index++)
            {
                GameObject child = children[index];

                if (includeInactive)
                {
                    if (child.activeSelf == false)
                    {
                        continue;
                    }
                }

                if (TryGetFirstVgoBlendShapeComponentRecursive(child, includeInactive, vgoBendShapeKind, out vgoBlendShape))
                {
                    return true;
                }
            }

#pragma warning disable CS8625
            vgoBlendShape = default;
#pragma warning restore CS8625

            return false;
        }

        #endregion

        #region Public Methods (Skybox)

        /// <summary>
        /// Gets the first component of the Skybox type, if it exists.
        /// </summary>
        /// <param name="skybox"></param>
        /// <returns>Returns true if the components is found, false otherwise.</returns>
        public bool TryGetFirstSkyboxComponent(out Skybox skybox)
        {
            return TryGetFirstSkyboxComponent(includeInactive: false, out skybox);
        }

        /// <summary>
        /// Gets the first component of the Skybox type, if it exists.
        /// </summary>
        /// <param name="includeInactive">Whether to include inactive child GameObjects in the search.</param>
        /// <param name="skybox"></param>
        /// <returns>Returns true if the components is found, false otherwise.</returns>
        public bool TryGetFirstSkyboxComponent(in bool includeInactive, out Skybox skybox)
        {
#pragma warning disable CS8625
            skybox = default;
#pragma warning restore CS8625

            if (_Root == null)
            {
                return false;
            }

            return _Root.TryGetFirstComponentInChildren(includeInactive, out skybox);
        }

        #endregion

        #region Public Methods (Helper)

        /// <summary>
        /// Reflect VGO skybox to Camera skybox.
        /// </summary>
        /// <param name="camera">A scene camera.</param>
        /// <param name="includeInactive">Whether to include inactive child GameObjects in the search.</param>
        public void ReflectSkybox(Camera camera, in bool includeInactive = false)
        {
            if (TryGetFirstSkyboxComponent(includeInactive, out Skybox vgoSkybox))
            {
                if (camera.gameObject.TryGetComponentEx(out Skybox cameraSkybox) == false)
                {
                    cameraSkybox = camera.gameObject.AddComponent<Skybox>();
                }

                cameraSkybox.material = vgoSkybox.material;
            }
        }

        #endregion

        #region Public Methods (Dispose)

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            //GC.SuppressFinalize(this);
        }

        #endregion

        #region Protected Methods (Dispose)

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed == false)
            {
                if (disposing)
                {
#if UNITY_EDITOR
                    DestroyRootAndResourcesForEditor();
#else
                    DestroyRootAndResources();
#endif
                    Root = null;
                }

                disposed = true;
            }
        }

        /// <summary>
        /// Destroy root GameObject and unity resources.
        /// </summary>
        protected virtual void DestroyRootAndResources()
        {
            if (Application.isPlaying == false)
            {
                Debug.LogWarningFormat("Dispose called in editor mode. This function is for runtime");
            }

            if (Root != null)
            {
                try
                {
                    GameObject.Destroy(Root);
                }
                catch
                {
                    //
                }
            }

            if (Avatar != null)
            {
                try
                {
                    UnityEngine.Object.Destroy(Avatar);
                }
                catch
                {
                    //
                }
            }

            foreach (UnityEngine.Object? obj in ObjectsForSubAsset())
            {
                if (obj == null)
                {
                    continue;
                }

                try
                {
                    UnityEngine.Object.Destroy(obj);
                }
                catch
                {
                    //
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<UnityEngine.Object?> ObjectsForSubAsset()
        {
            if (AnimationClipList != null)
            {
                foreach (var x in AnimationClipList) { yield return x; }
            }
            if (ColliderList != null)
            {
                foreach (var x in ColliderList) { yield return x; }
            }
            if (MaterialList != null)
            {
                foreach (var x in MaterialList) { yield return x; }
            }
            if (MeshAssetList != null)
            {
                foreach (var x in MeshAssetList) { yield return x?.Mesh; }
            }
            if (TextureList != null)
            {
                foreach (var x in TextureList) { yield return x; }
            }
            if (SpringBoneColliderGroupArray != null)
            {
                foreach (var x in SpringBoneColliderGroupArray) { yield return x; }
            }
            if (ScriptableObjectList != null)
            {
                foreach (var x in ScriptableObjectList) { yield return x; }
            }
        }

#if UNITY_EDITOR
        /// <summary>
        /// Destroy root GameObject and unity resources.
        /// </summary>
        protected void DestroyRootAndResourcesForEditor()
        {
            if (Application.isPlaying)
            {
                DestroyRootAndResources();
            }
            else
            {
                if (Root != null)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(AssetDatabase.GetAssetPath(Root)))
                        {
                            UnityEngine.Object.DestroyImmediate(Root);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.LogException(ex);
                    }
                }

                if (Avatar != null)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(AssetDatabase.GetAssetPath(Avatar)))
                        {
                            UnityEngine.Object.DestroyImmediate(Avatar);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.LogException(ex);
                    }
                }

                foreach (UnityEngine.Object? obj in ObjectsForSubAsset())
                {
                    if (obj == null)
                    {
                        continue;
                    }

                    try
                    {
                        if (string.IsNullOrEmpty(AssetDatabase.GetAssetPath(obj)))
                        {
                            UnityEngine.Object.DestroyImmediate(obj, allowDestroyingAssets: true);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.LogException(ex);
                    }
                }
            }
        }
#endif
        #endregion
    }
}
