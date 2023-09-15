// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Interface : IVgoModelAsset
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using NewtonVgo;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// VGO Model Asset Interface
    /// </summary>
    public interface IVgoModelAsset : IDisposable
    {
        #region Properties

        /// <summary>A game object of root.</summary>
        GameObject? Root { get; set; }

        /// <summary>An avatar.</summary>
        Avatar? Avatar { get; set; }

        /// <summary>List of unity animation clip.</summary>
        List<AnimationClip?>? AnimationClipList { get; set; }

        /// <summary>List of unity collider.</summary>
        List<Collider?>? ColliderList { get; set; }

        /// <summary>List of unity material.</summary>
        List<Material?>? MaterialList { get; set; }

        /// <summary>List of unity mesh and renderer.</summary>
        List<MeshAsset>? MeshAssetList { get; set; }

        /// <summary>List of unity texture2D.</summary>
        List<Texture2D?>? Texture2dList { get; set; }

        /// <summary>List of scriptable object.</summary>
        List<ScriptableObject> ScriptableObjectList { get; }

        /// <summary>Array of spring bone collider group.</summary>
        VgoSpringBone.VgoSpringBoneColliderGroup?[]? SpringBoneColliderGroupArray { get; set; }

        /// <summary>A vgo layout.</summary>
        VgoLayout? Layout { get; set; }

        #endregion

        #region Methods (Blend Shape)

        /// <summary>
        /// Gets the first components of the VgoBlendShape type, if it exists.
        /// </summary>
        /// <param name="faceOnly"></param>
        /// <param name="includeInactive">Whether to include inactive child GameObjects in the search.</param>
        /// <param name="vgoBlendShapes"></param>
        /// <returns>Returns true if the components is found, false otherwise.</returns>
        /// <remarks>breadth-first search</remarks>
        bool TryGetVgoBlendShapeComponents(in bool faceOnly, in bool includeInactive, out List<VgoBlendShape> vgoBlendShapes);

        /// <summary>
        /// Gets the first component of the VgoBlendShape type, if it exists.
        /// </summary>
        /// <param name="faceOnly">Only the kind Face or Face_2 is retrieved.</param>
        /// <param name="vgoBlendShape"></param>
        /// <returns>Returns true if the components is found, false otherwise.</returns>
        /// <remarks>breadth-first search</remarks>
        bool TryGetFirstVgoBlendShapeComponent(in bool faceOnly, out VgoBlendShape vgoBlendShape);

        /// <summary>
        /// Gets the first component of the VgoBlendShape type, if it exists.
        /// </summary>
        /// <param name="faceOnly">Only the kind Face or Face_2 is retrieved.</param>
        /// <param name="includeInactive">Whether to include inactive child GameObjects in the search.</param>
        /// <param name="vgoBlendShape"></param>
        /// <returns>Returns true if the components is found, false otherwise.</returns>
        /// <remarks>breadth-first search</remarks>
        bool TryGetFirstVgoBlendShapeComponent(in bool faceOnly, in bool includeInactive, out VgoBlendShape vgoBlendShape);

        #endregion

        #region Methods (Skybox)

        /// <summary>
        /// Gets the first component of the Skybox type, if it exists.
        /// </summary>
        /// <param name="skybox"></param>
        /// <returns>Returns true if the components is found, false otherwise.</returns>
        bool TryGetFirstSkyboxComponent(out Skybox skybox);

        /// <summary>
        /// Gets the first component of the Skybox type, if it exists.
        /// </summary>
        /// <param name="includeInactive">Whether to include inactive child GameObjects in the search.</param>
        /// <param name="skybox"></param>
        /// <returns>Returns true if the components is found, false otherwise.</returns>
        bool TryGetFirstSkyboxComponent(in bool includeInactive, out Skybox skybox);

        #endregion

        #region Public Methods (Helper)

        /// <summary>
        /// Reflect VGO skybox to Camera skybox.
        /// </summary>
        /// <param name="camera">A scene camera.</param>
        /// <param name="includeInactive">Whether to include inactive child GameObjects in the search.</param>
        void ReflectSkybox(Camera camera, in bool includeInactive = false);

        #endregion
    }
}
