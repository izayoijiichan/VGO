// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : AvatarConfiguration
// ----------------------------------------------------------------------
namespace UniVgo2
{
    using NewtonVgo;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Avatar Configuration
    /// </summary>
    [CreateAssetMenu(menuName = "VGO/AvatarConfiguration")]
    [Serializable]
    public class AvatarConfiguration : ScriptableObject
    {
        ///// <summary>The name of this human avatar.</summary>
        //public string name;

        /// <summary>List of the human bone.</summary>
        public List<VgoHumanBone> humanBones = new List<VgoHumanBone>();
    }
}
