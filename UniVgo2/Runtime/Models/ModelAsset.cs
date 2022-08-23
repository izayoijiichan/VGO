// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : ModelAsset
// ----------------------------------------------------------------------
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
        public GameObject Root = null;

        /// <summary>An avatar.</summary>
        public Avatar Avatar = null;

        /// <summary>List of unity animation clip.</summary>
        public List<AnimationClip> AnimationClipList = null;

        /// <summary>List of unity collider.</summary>
        public List<Collider> ColliderList = null;

        /// <summary>List of unity material.</summary>
        public List<Material> MaterialList = null;

        /// <summary>List of unity mesh and renderer.</summary>
        public List<MeshAsset> MeshAssetList = null;

        /// <summary>List of unity texture2D.</summary>
        public List<Texture2D> Texture2dList = null;

        /// <summary>List of scriptable object.</summary>
        public readonly List<ScriptableObject> ScriptableObjectList = new List<ScriptableObject>();

        /// <summary>Array of spring bone collider group.</summary>
        public VgoSpringBone.VgoSpringBoneColliderGroup[] SpringBoneColliderGroupArray = null;

        /// <summary>A vgo layout.</summary>
        public VgoLayout Layout = null;

        /// <summary></summary>
        protected bool disposed;

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

            foreach (UnityEngine.Object obj in ObjectsForSubAsset())
            {
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
        protected virtual IEnumerable<UnityEngine.Object> ObjectsForSubAsset()
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
                foreach (var x in MeshAssetList) { yield return x.Mesh; }
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
            if (Application.isPlaying == false)
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

                foreach (UnityEngine.Object obj in ObjectsForSubAsset())
                {
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
