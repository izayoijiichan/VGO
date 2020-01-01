// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : VgoMeta
// ----------------------------------------------------------------------
namespace UniVgo
{
    using UniGLTFforUniVgo;
    using UnityEngine;

    /// <summary>
    /// VGO Meta
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(VgoRight))]
    public class VgoMeta : MonoBehaviour
    {
        /// <summary>glTF VGO Meta</summary>
        public glTF_VGO_Meta Meta = null;
    }
}