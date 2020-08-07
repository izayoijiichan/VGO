// ----------------------------------------------------------------------
// @Namespace : UniVgo.Converters
// @Class     : NormalMapConverter
// ----------------------------------------------------------------------
namespace UniVgo.Converters
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Normal Map Converter
    /// </summary>
    public class NormalMapConverter : TextureConverter
    {
        /// <summary>
        /// Get import texture.
        /// </summary>
        /// <param name="source">The source texture.</param>
        /// <returns></returns>
        public Texture2D GetImportTexture(Texture2D source)
        {
            Shader encoder = Shader.Find("UniGLTF/NormalMapEncoder");

            if (encoder == null)
            {
                throw new Exception();
            }

            Material converter = new Material(encoder);

            Texture2D copyTexture = CopyTexture2d(source, ColorSpaceType.Linear, converter);

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
        /// <returns></returns>
        public Texture2D GetExportTexture(Texture2D source)
        {
            Shader decoder = Shader.Find("UniGLTF/NormalMapDecoder");

            if (decoder == null)
            {
                throw new Exception();
            }

            Material converter = new Material(decoder);

            Texture2D copyTexture = CopyTexture2d(source, ColorSpaceType.Linear, converter);

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
    }
}
