// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : ModelAsset
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using NewtonVgo;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
#if UNITY_EDITOR
    using UnityEditor;
#endif

    /// <summary>
    /// Model Asset
    /// </summary>
    [Serializable]
    public class ModelAsset : IDisposable
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

        /// <summary>List of unity texture2D.</summary>
        private List<Texture2D?>? _Texture2dList = null;

        /// <summary>List of scriptable object.</summary>
        private readonly List<ScriptableObject> _ScriptableObjectList = new List<ScriptableObject>();

        /// <summary>Array of spring bone collider group.</summary>
        private VgoSpringBone.VgoSpringBoneColliderGroup?[]? _SpringBoneColliderGroupArray = null;

        /// <summary>A vgo layout.</summary>
        private VgoLayout? _Layout = null;

        /// <summary></summary>
        protected bool disposed;

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

        /// <summary>List of unity texture2D.</summary>
        public List<Texture2D?>? Texture2dList { get => _Texture2dList; set => _Texture2dList = value; }

        /// <summary>List of scriptable object.</summary>
        public List<ScriptableObject> ScriptableObjectList => _ScriptableObjectList;

        /// <summary>Array of spring bone collider group.</summary>
        public VgoSpringBone.VgoSpringBoneColliderGroup?[]? SpringBoneColliderGroupArray { get => _SpringBoneColliderGroupArray; set => _SpringBoneColliderGroupArray = value; }

        /// <summary>A vgo layout.</summary>
        public VgoLayout? Layout { get => _Layout; set => _Layout = value; }

        #endregion

        #region Constructors

        // ~ModelAsset()
        // {
        //     Dispose(disposing: false);
        // }

        #endregion

        #region Methods (Dispose)

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            //GC.SuppressFinalize(this);
        }

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
        public virtual void DestroyRootAndResources()
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
            if (Texture2dList != null)
            {
                foreach (var x in Texture2dList) { yield return x; }
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
        public void DestroyRootAndResourcesForEditor()
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
