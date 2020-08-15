// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : AvatarConfiguration
// ----------------------------------------------------------------------
namespace UniVgo
{
    using NewtonGltf;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// BlendShape Configuration
    /// </summary>
    [CreateAssetMenu(menuName = "VGO/AvatarConfiguration")]
    [Serializable]
    public class AvatarConfiguration : ScriptableObject
    {
        ///// <summary>The name of this human avatar.</summary>
        //public string name;

        /// <summary>List of the human bone.</summary>
        public List<VGO_HumanBone> humanBones = new List<VGO_HumanBone>();
    }
}
