// ----------------------------------------------------------------------
// @Namespace : UniSkybox
// @Class     : SkyboxCubemapDefinition
// ----------------------------------------------------------------------
namespace UniSkybox
{
    using UnityEngine;

    /// <summary>
    /// Skybox Cubemap Definition
    /// </summary>
    public class SkyboxCubemapDefinition
    {
        /// <summary>Tint Color</summary>
        public Color Tint { get; set; }

        /// <summary>Exposure</summary>
        //[Range(0.0f, 8.0f)]
        public float Exposure { get; set; }

        /// <summary>Rotation</summary>
        //[Range(0f, 360f)]
        public int Rotation { get; set; }

        /// <summary>Cubemap (HDR)</summary>
        public Cubemap Tex { get; set; }
    }
}