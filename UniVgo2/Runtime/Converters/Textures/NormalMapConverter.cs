// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Converters
// @Class     : NormalMapConverter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Converters
{
    using NewtonVgo;
    using UnityEngine;

    /// <summary>
    /// Normal Map Converter
    /// </summary>
    public class NormalMapConverter : TextureConverterBase
    {
        #region Fields

        /// <summary>Normal Map Encoder</summary>
        private Shader? _Encoder;

        /// <summary>Normal Map Decoder</summary>
        private Shader? _Decoder;

        /// <summary>Normal Map Exporter</summary>
        /// <remarks>VRMShader 0.84.0 or higher</remarks>
        private Shader? _Exporter;

        #endregion

        #region Properties

        /// <summary>Normal Map Encoder</summary>
        private Shader? Encoder => _Encoder ??= Shader.Find("UniGLTF/NormalMapEncoder");

        /// <summary>Normal Map Decoder</summary>
        private Shader? Decoder => _Decoder ??= Shader.Find("UniGLTF/NormalMapDecoder");

        /// <summary>Normal Map Exporter</summary>
        /// <remarks>VRMShader 0.84.0 or higher</remarks>
        private Shader? Exporter => _Exporter ??= Shader.Find("Hidden/UniGLTF/NormalMapExporter");

        #endregion

        #region Public Methods

        /// <summary>
        /// Get import texture.
        /// </summary>
        /// <param name="source">The source texture.</param>
        /// <param name="colorSpaceType"></param>
        /// <returns></returns>
        public Texture GetImportTexture(in Texture source, in VgoColorSpaceType colorSpaceType)
        {
            if (Encoder == null)
            {
                ThrowHelper.ThrowFileNotFoundException("UniGLTF/NormalMapEncoder.shader");
            }

            var converter = new Material(Encoder);

            Texture copyTexture = CopyTexture(source, colorSpaceType, converter);

            if (Application.isEditor)
            {
                UnityEngine.Object.DestroyImmediate(converter);
            }
            else
            {
                UnityEngine.Object.Destroy(converter);
            }

            return copyTexture;
        }

        /// <summary>
        /// Get export texture.
        /// </summary>
        /// <param name="source">The source texture.</param>
        /// <param name="colorSpaceType"></param>
        /// <returns></returns>
        public Texture GetExportTexture(in Texture source, in VgoColorSpaceType colorSpaceType)
        {
            Shader? exporter = Exporter;

            if (exporter == null)
            {
                if (Decoder == null)
                {
                    ThrowHelper.ThrowFileNotFoundException("UniGLTF/NormalMapDecoder.shader");
                }

                exporter = Decoder;
            }

            var converter = new Material(exporter);

            Texture copyTexture = CopyTexture(source, colorSpaceType, converter);

            if (Application.isEditor)
            {
                UnityEngine.Object.DestroyImmediate(converter);
            }
            else
            {
                UnityEngine.Object.Destroy(converter);
            }

            return copyTexture;
        }

        #endregion
    }
}
