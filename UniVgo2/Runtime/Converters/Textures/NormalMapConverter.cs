// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Converters
// @Class     : NormalMapConverter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Converters
{
    using NewtonVgo;
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
        public Texture2D GetImportTexture(in Texture2D source)
        {
            Shader? encoder = Shader.Find("UniGLTF/NormalMapEncoder");

            if (encoder == null)
            {
                ThrowHelper.ThrowFileNotFoundException("UniGLTF/NormalMapEncoder.shader");
            }

            Material converter = new Material(encoder);

            Texture2D copyTexture = CopyTexture2d(source, VgoColorSpaceType.Linear, converter);

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
        public Texture2D GetExportTexture(in Texture2D source)
        {
            Shader? decoder = Shader.Find("UniGLTF/NormalMapDecoder");

            if (decoder == null)
            {
                ThrowHelper.ThrowFileNotFoundException("UniGLTF/NormalMapDecoder.shader");
            }

            Material converter = new Material(decoder);

            Texture2D copyTexture = CopyTexture2d(source, VgoColorSpaceType.Linear, converter);

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
