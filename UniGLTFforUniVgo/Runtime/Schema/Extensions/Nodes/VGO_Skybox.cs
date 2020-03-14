// ----------------------------------------------------------------------
// @Namespace : UniGLTFforUniVgo
// @Class     : VGO_Skybox
// ----------------------------------------------------------------------
namespace UniGLTFforUniVgo
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;
    using UnityEngine;

    /// <summary>
    /// glTF Node VGO Skybox
    /// </summary>
    [Serializable]
    [JsonObject("node.vgo.skybox")]
    public class VGO_Skybox
    {
        #region Fields

        /// <summary>Material Index</summary>
        [JsonProperty("materialIndex", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        public int materialIndex = -1;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of VGO_Skybox.
        /// </summary>
        public VGO_Skybox() { }

        /// <summary>
        /// Create a new instance of VGO_Skybox by specifying Skybox and glTF.
        /// </summary>
        /// <param name="skybox"></param>
        /// <param name="gltf"></param>
        public VGO_Skybox(Skybox skybox, glTF gltf)
        {
            if (skybox == null)
            {
                return;
            }

            if (gltf == null)
            {
                return;
            }

            if (skybox.material == null)
            {
                return;
            }

            if (gltf.materials == null)
            {
                return;
            }

            string skyboxMaterialName = skybox.material.name;

            for (int index = 0; index < gltf.materials.Count; index++)
            {
                if (gltf.materials[index].name == skyboxMaterialName)
                {
                    materialIndex = index;

                    break;
                }
            }
        }

        #endregion

    }
}