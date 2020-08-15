// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : ModelAsset
// ----------------------------------------------------------------------
namespace UniVgo
{
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
        #region Fields (Common)

        /// <summary>The game object of root.</summary>
        public GameObject Root = null;

        /// <summary>List of unity material.</summary>
        public List<Material> MaterialList = null;

        /// <summary>List of unity mesh and renderer.</summary>
        public List<MeshAsset> MeshAssetList = null;

        #endregion

        #region Fields (Import)

        /// <summary>List of unity texture2D.</summary>
        public List<Texture2D> Texture2dList = null;

        /// <summary>List of texture info.</summary>
        public List<TextureInfo> TextureInfoList = null;

        /// <summary>List of material info.</summary>
        public List<MaterialInfo> MaterialInfoList = null;

        /// <summary>List of scriptable object.</summary>
        public List<ScriptableObject> ScriptableObjectList = new List<ScriptableObject>();

        /// <summary>The avatar.</summary>
        public Avatar Avatar = null;

        #endregion

        #region Fields (Export)

        /// <summary>List of unity mesh.</summary>
        public List<Mesh> MeshList = null;

        /// <summary>List of unity renderer.</summary>
        public List<Renderer> RendererList = null;

        /// <summary>List of unity skinned mesh renderer.</summary>
        public List<SkinnedMeshRenderer> SkinList = null;

        #endregion

        #region Fields (Dispose)

        /// <summary></summary>
        protected bool disposedValue;

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
            if (disposedValue == false)
            {
                if (disposing)
                {
                    //
                }

#if UNITY_EDITOR
                DestroyRootAndResourcesForEditor();
#else
                DestroyRootAndResources();
#endif
                Root = null;

                disposedValue = true;
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
                GameObject.Destroy(Root);
            }

            if (Avatar != null)
            {
                UnityEngine.Object.Destroy(Avatar);
            }

            foreach (UnityEngine.Object obj in ObjectsForSubAsset())
            {
                UnityEngine.Object.DestroyImmediate(obj, true);
            }

            foreach (ScriptableObject sObj in ScriptableObjectList)
            {
                ScriptableObject.DestroyImmediate(sObj);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<UnityEngine.Object> ObjectsForSubAsset()
        {
            foreach (var x in MaterialList) { yield return x; }
            foreach (var x in MeshAssetList) { yield return x.Mesh; }
            foreach (var x in Texture2dList) { yield return x; }

            if (MeshList != null)
            {
                foreach (var x in MeshList) { yield return x; }
            }
            if (RendererList != null)
            {
                foreach (var x in RendererList) { yield return x; }
            }
            if (SkinList != null)
            {
                foreach (var x in SkinList) { yield return x; }
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
                    if (string.IsNullOrEmpty(AssetDatabase.GetAssetPath(Root)))
                    {
                        UnityEngine.Object.DestroyImmediate(Root);
                    }
                }

                if (Avatar != null)
                {
                    if (string.IsNullOrEmpty(AssetDatabase.GetAssetPath(Avatar)))
                    {
                        UnityEngine.Object.DestroyImmediate(Avatar);
                    }
                }

                if (Texture2dList != null)
                {
                    foreach (Texture2D texture in Texture2dList)
                    {
                        if (string.IsNullOrEmpty(AssetDatabase.GetAssetPath(texture)))
                        {
                            UnityEngine.Object.DestroyImmediate(texture);
                        }
                    }
                }

                if (MaterialList != null)
                {
                    foreach (Material material in MaterialList)
                    {
                        if (string.IsNullOrEmpty(AssetDatabase.GetAssetPath(material)))
                        {
                            UnityEngine.Object.DestroyImmediate(material);
                        }
                    }
                }

                if (MeshAssetList != null)
                {
                    foreach (MeshAsset meshAsset in MeshAssetList)
                    {
                        if (string.IsNullOrEmpty(AssetDatabase.GetAssetPath(meshAsset.Mesh)))
                        {
                            UnityEngine.Object.DestroyImmediate(meshAsset.Mesh);
                        }
                    }
                }

                if (MeshList != null)
                {
                    foreach (Mesh mesh in MeshList)
                    {
                        if (string.IsNullOrEmpty(AssetDatabase.GetAssetPath(mesh)))
                        {
                            UnityEngine.Object.DestroyImmediate(mesh);
                        }
                    }
                }

                if (RendererList != null)
                {
                    foreach (Renderer renderer in RendererList)
                    {
                        if (string.IsNullOrEmpty(AssetDatabase.GetAssetPath(renderer)))
                        {
                            UnityEngine.Object.DestroyImmediate(renderer);
                        }
                    }
                }

                if (SkinList != null)
                {
                    foreach (SkinnedMeshRenderer skin in SkinList)
                    {
                        if (string.IsNullOrEmpty(AssetDatabase.GetAssetPath(skin)))
                        {
                            UnityEngine.Object.DestroyImmediate(skin);
                        }
                    }
                }

                if (ScriptableObjectList != null)
                {
                    foreach (ScriptableObject sObj in ScriptableObjectList)
                    {
                        if (string.IsNullOrEmpty(AssetDatabase.GetAssetPath(sObj)))
                        {
                            UnityEngine.Object.DestroyImmediate(sObj);
                        }
                    }
                }
            }
        }
#endif
        #endregion
    }
}
