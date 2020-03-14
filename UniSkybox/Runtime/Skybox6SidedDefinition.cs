// ----------------------------------------------------------------------
// @Namespace : UniSkybox
// @Class     : Skybox6SidedDefinition
// ----------------------------------------------------------------------
namespace UniSkybox
{
    using UnityEngine;

    /// <summary>
    /// Skybox 6 Sided Definition
    /// </summary>
    public class Skybox6SidedDefinition
    {
        /// <summary>Tint Color</summary>
        public Color Tint { get; set; }

        /// <summary>Exposure</summary>
        //[Range(0.0f, 8.0f)]
        public float Exposure { get; set; }

        /// <summary>Rotation</summary>
        //[Range(0f, 360f)]
        public int Rotation { get; set; }

        /// <summary>Front [+Z] (HDR)</summary>
        public Texture2D FrontTex { get; set; }

        /// <summary>Back [-Z] (HDR)</summary>
        public Texture2D BackTex { get; set; }

        /// <summary>Left [+X] (HDR)</summary>
        public Texture2D LeftTex { get; set; }

        /// <summary>Right [-X] (HDR)</summary>
        public Texture2D RightTex { get; set; }

        /// <summary>Up [+Y] (HDR)</summary>
        public Texture2D UpTex { get; set; }

        /// <summary>Down [-Y] (HDR)</summary>
        public Texture2D DownTex { get; set; }
    }
}