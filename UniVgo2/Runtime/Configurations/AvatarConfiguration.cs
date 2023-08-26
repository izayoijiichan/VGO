// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : AvatarConfiguration
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using NewtonVgo;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Avatar Configuration
    /// </summary>
    [CreateAssetMenu(menuName = "Vgo/Avatar Configuration")]
    [Serializable]
    public class AvatarConfiguration : ScriptableObject
    {
        ///// <summary>The name of this human avatar.</summary>
        //public string name;

        /// <summary>The name of this human avatar.</summary>
        public string Name
        {
            get => name;
            set => name = value;
        }

        /// <summary>List of the human bone.</summary>
        /// <remarks>For VgoScriptedImporter it is set to public.</remarks>
        [SerializeField]
        public List<VgoHumanBone> humanBones = new List<VgoHumanBone>();
        //private List<VgoHumanBone> humanBones = new List<VgoHumanBone>();

        /// <summary>List of the human bone.</summary>
        public List<VgoHumanBone> HumanBones
        {
            get => humanBones;
            set => humanBones = value;
        }
    }
}
