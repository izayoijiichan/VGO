// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : VgoMeta
// ----------------------------------------------------------------------
namespace UniVgo
{
    using NewtonGltf;
    using UnityEngine;

    /// <summary>
    /// VGO Meta
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(VgoRight))]
    public class VgoMeta : MonoBehaviour
    {
        /// <summary>glTF VGO Meta</summary>
        public Gltf_VGO_Meta Meta = null;
    }
}