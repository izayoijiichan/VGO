// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : UniGltfUnlitDefinitionExtensions
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using UniUrpShader;
    using UniGLTF.UniUnlit;

    /// <summary>
    /// UniGLTF Unlit Definition Extensions
    /// </summary>
    public static class UniGltfUnlitDefinitionExtensions
    {
        /// <summary>
        /// Convert UniGLTF unlit definition to URP unlit definition.
        /// </summary>
        /// <param name="uniGltfUnlitDefinition">A UniGLTF unlit definition.</param>
        /// <returns>A URP unlit definition.</returns>
        public static UrpUnlitDefinition ToUrpUnlitDefinition(this UniGltfUnlitDefinition uniGltfUnlitDefinition)
        {
            return new UniUrpShader.UrpUnlitDefinition
            {
                Surface = uniGltfUnlitDefinition.BlendMode switch
                {
                    UniUnlitRenderMode.Opaque => UniUrpShader.SurfaceType.Opaque,
                    UniUnlitRenderMode.Cutout => UniUrpShader.SurfaceType.Opaque,
                    UniUnlitRenderMode.Transparent => UniUrpShader.SurfaceType.Transparent,
                    _ => UniUrpShader.SurfaceType.Opaque,
                },

                Blend = uniGltfUnlitDefinition.BlendMode switch
                {
                    UniUnlitRenderMode.Opaque => UniUrpShader.BlendMode.Alpha,
                    UniUnlitRenderMode.Cutout => UniUrpShader.BlendMode.Alpha,
                    UniUnlitRenderMode.Transparent => UniUrpShader.BlendMode.Alpha,  // @notice
                    _ => UniUrpShader.BlendMode.Alpha,
                },

                BlendOp = UnityEngine.Rendering.BlendOp.Add,

                SrcBlend = uniGltfUnlitDefinition.SrcBlend,
                DstBlend = uniGltfUnlitDefinition.DstBlend,
                ZWrite = uniGltfUnlitDefinition.ZWrite,

                Cull = (UniUrpShader.CullMode)uniGltfUnlitDefinition.CullMode,

                AlphaClip = uniGltfUnlitDefinition.BlendMode == UniUnlitRenderMode.Cutout,
                Cutoff = uniGltfUnlitDefinition.Cutoff,

                BaseColor = uniGltfUnlitDefinition.Color,

                BaseMap = uniGltfUnlitDefinition.MainTex,
                BaseMapScale = uniGltfUnlitDefinition.MainTexScale,
                BaseMapOffset = uniGltfUnlitDefinition.MainTexOffset,

                QueueOffset = 0,

                // Obsolete Properties
                //Color = uniGltfUnlitDefinition.Color,
                //MainTex = uniGltfUnlitDefinition.MainTex,
                //SampleGI = default,
            };
        }
    }
}
