// ----------------------------------------------------------------------
// @Namespace : UniSkybox
// @Class     : SkyboxPanoramicDefinition
// ----------------------------------------------------------------------
namespace UniSkybox
{
    using UnityEngine;

    /// <summary>
    /// Skybox Panoramic Definition
    /// </summary>
    public class SkyboxPanoramicDefinition
    {
        /// <summary>Tint Color</summary>
        public Color Tint { get; set; }

        /// <summary>Exposure</summary>
        //[Range(0.0f, 8.0f)]
        public float Exposure { get; set; }

        /// <summary>Rotation</summary>
        //[Range(0f, 360f)]
        public int Rotation { get; set; }

        /// <summary>Spherical (HDR)</summary>
        public Texture2D MainTex { get; set; }

        /// <summary>Mapping</summary>
        public Mapping Mapping { get; set; }

        /// <summary>Image Type</summary>
        public ImageType ImageType { get; set; }

        /// <summary>Mirror on Back</summary>
        public bool MirrorOnBack { get; set; }

        /// <summary>3D Layout</summary>
        public Layout Layout { get; set; }
    }
}