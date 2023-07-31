// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : UniGltfUnlitDefinition
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using UniGLTF.UniUnlit;
    using UnityEngine;

    /// <summary>
    /// UniGLTF Unlit Definition
    /// </summary>
    public class UniGltfUnlitDefinition
    {
        /// <summary>Color</summary>
        public Color Color { get; set; }

        /// <summary>Main Texture</summary>
        public Texture2D? MainTex { get; set; }

        /// <summary>Main Texture Scale</summary>
        public Vector2 MainTexScale { get; set; }

        /// <summary>Main Texture Offset</summary>
        public Vector2 MainTexOffset { get; set; }

        /// <summary>Alpha Cutoff</summary>
        public float Cutoff { get; set; }

        /// <summary>Cull Mode</summary>
        public UniUnlitCullMode CullMode { get; set; }

        /// <summary>Alpha Blend Mode</summary>
        public UniUnlitRenderMode BlendMode { get; set; }

        /// <summary>Vertex Color Blend Mode</summary>
        public UniUnlitVertexColorBlendOp VColBlendMode { get; set; }

        /// <summary>Src Blend</summary>
        public UnityEngine.Rendering.BlendMode SrcBlend { get; set; }

        /// <summary>Dst Blend</summary>
        public UnityEngine.Rendering.BlendMode DstBlend { get; set; }

        /// <summary>ZWrite</summary>
        public bool ZWrite { get; set; }
    }
}
