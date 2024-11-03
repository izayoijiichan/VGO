// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Converters
// @Class     : OrmMapConverter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Converters
{
    using NewtonVgo;
    using UnityEngine;

    /// <summary>
    /// ORM (Occlusion Roughness Metallic) Map Converter
    /// </summary>
    /// <remarks>
    /// Unity Color
    ///   R: Metallic
    ///   G: Occlusion
    ///   B: Heightmap
    ///   A: Smoothness
    ///
    /// glTF Color
    ///   R: Occlusion
    ///   G: Roughness
    ///   B: Metallic
    ///   A: (not used)
    /// </remarks>
    public class OrmMapConverter : TextureConverterBase
    {
        #region Public Methods (Import)

        /// <summary>
        /// Get import texture.
        /// </summary>
        /// <param name="texture">The source texture.</param>
        /// <param name="colorSpaceType"></param>
        /// <param name="roughnessFactor"></param>
        /// <returns></returns>
        public Texture GetImportTexture(in Texture texture, in VgoColorSpaceType colorSpaceType, in float roughnessFactor)
        {
            if (texture is Texture2D texture2D)
            {
                return GetImportTexture2D(texture2D, colorSpaceType, roughnessFactor);
            }
            else if (texture is Texture2DArray texture2DArray)
            {
                return GetImportTexture2DArray(texture2DArray, colorSpaceType, roughnessFactor);
            }
            else if (texture is Texture3D texture3D)
            {
                return GetImportTexture3D(texture3D, colorSpaceType, roughnessFactor);
            }
            else
            {
#if NET_STANDARD_2_1
                ThrowHelper.ThrowNotSupportedException($"{texture.dimension}, {texture.name}");
                return default;
#else
                throw new NotSupportedException($"{source.dimension}, {source.name}");
#endif
            }
        }

        /// <summary>
        /// Get import texture.
        /// </summary>
        /// <param name="texture">The source texture.</param>
        /// <param name="colorSpaceType"></param>
        /// <param name="roughnessFactor"></param>
        /// <returns></returns>
        public Texture2D GetImportTexture2D(in Texture2D texture, in VgoColorSpaceType colorSpaceType, in float roughnessFactor)
        {
            Texture2D copyTexture = CopyTexture2D(texture, colorSpaceType);

            Color32[] pixels = copyTexture.GetPixels32();

            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = Import(pixels[i], roughnessFactor);
            }

            copyTexture.SetPixels32(pixels);

            copyTexture.Apply();

            return copyTexture;
        }

        /// <summary>
        /// Get import texture.
        /// </summary>
        /// <param name="texture">The source texture.</param>
        /// <param name="colorSpaceType"></param>
        /// <param name="roughnessFactor"></param>
        /// <returns></returns>
        public Texture2DArray GetImportTexture2DArray(in Texture2DArray texture, in VgoColorSpaceType colorSpaceType, in float roughnessFactor)
        {
            Texture2DArray copyTexture = CopyTexture2DArray(texture, colorSpaceType);

            for (int arrayElement = 0; arrayElement < texture.depth; arrayElement++)
            {
                Color32[] pixels = copyTexture.GetPixels32(arrayElement);

                for (int i = 0; i < pixels.Length; i++)
                {
                    pixels[i] = Import(pixels[i], roughnessFactor);
                }

                copyTexture.SetPixels32(pixels, arrayElement);
            }

            copyTexture.Apply();

            return copyTexture;
        }

        /// <summary>
        /// Get import texture.
        /// </summary>
        /// <param name="texture">The source texture.</param>
        /// <param name="colorSpaceType"></param>
        /// <param name="roughnessFactor"></param>
        /// <returns></returns>
        public Texture3D GetImportTexture3D(in Texture3D texture, in VgoColorSpaceType colorSpaceType, in float roughnessFactor)
        {
            Texture3D copyTexture = CopyTexture3D(texture, colorSpaceType);

            Color32[] pixels = copyTexture.GetPixels32();

            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = Import(pixels[i], roughnessFactor);
            }

            copyTexture.SetPixels32(pixels);

            copyTexture.Apply();

            return copyTexture;
        }

        #endregion

        #region Public Methods (Export)

        /// <summary>
        /// Get export texture.
        /// </summary>
        /// <param name="texture">The source texture.</param>
        /// <param name="colorSpaceType"></param>
        /// <param name="smoothnessFactor"></param>
        /// <returns></returns>
        public Texture GetExportTexture(in Texture texture, in VgoColorSpaceType colorSpaceType, in float smoothnessFactor)
        {
            if (texture is Texture2D texture2D)
            {
                return GetExportTexture2D(texture2D, colorSpaceType, smoothnessFactor);
            }
            else if (texture is Texture2DArray texture2DArray)
            {
                return GetExportTexture2DArray(texture2DArray, colorSpaceType, smoothnessFactor);
            }
            else if (texture is Texture3D texture3D)
            {
                return GetExportTexture3D(texture3D, colorSpaceType, smoothnessFactor);
            }
            else
            {
#if NET_STANDARD_2_1
                ThrowHelper.ThrowNotSupportedException($"{texture.dimension}, {texture.name}");
                return default;
#else
                throw new NotSupportedException($"{source.dimension}, {source.name}");
#endif
            }
        }

        /// <summary>
        /// Get export texture.
        /// </summary>
        /// <param name="texture">The source texture.</param>
        /// <param name="colorSpaceType"></param>
        /// <param name="smoothnessFactor"></param>
        /// <returns></returns>
        public Texture2D GetExportTexture2D(in Texture2D texture, in VgoColorSpaceType colorSpaceType, in float smoothnessFactor)
        {
            Texture2D copyTexture = CopyTexture2D(texture, colorSpaceType);

            Color32[] pixels = copyTexture.GetPixels32();

            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = Export(pixels[i], smoothnessFactor);
            }

            copyTexture.SetPixels32(pixels);

            copyTexture.Apply();

            return copyTexture;
        }

        /// <summary>
        /// Get export texture.
        /// </summary>
        /// <param name="texture">The source texture.</param>
        /// <param name="colorSpaceType"></param>
        /// <param name="smoothnessFactor"></param>
        /// <returns></returns>
        public Texture2DArray GetExportTexture2DArray(in Texture2DArray texture, in VgoColorSpaceType colorSpaceType, in float smoothnessFactor)
        {
            Texture2DArray copyTexture = CopyTexture2DArray(texture, colorSpaceType);

            for (int arrayElement = 0; arrayElement < texture.depth; arrayElement++)
            {
                Color32[] pixels = copyTexture.GetPixels32(arrayElement);

                for (int i = 0; i < pixels.Length; i++)
                {
                    pixels[i] = Export(pixels[i], smoothnessFactor);
                }

                copyTexture.SetPixels32(pixels, arrayElement);
            }

            copyTexture.Apply();

            return copyTexture;
        }

        /// <summary>
        /// Get export texture.
        /// </summary>
        /// <param name="texture">The source texture.</param>
        /// <param name="colorSpaceType"></param>
        /// <param name="smoothnessFactor"></param>
        /// <returns></returns>
        public Texture3D GetExportTexture3D(in Texture3D texture, in VgoColorSpaceType colorSpaceType, in float smoothnessFactor)
        {
            Texture3D copyTexture = CopyTexture3D(texture, colorSpaceType);

            Color32[] pixels = copyTexture.GetPixels32();

            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = Export(pixels[i], smoothnessFactor);
            }

            copyTexture.SetPixels32(pixels);

            copyTexture.Apply();

            return copyTexture;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="roughnessFactor"></param>
        /// <returns></returns>
        private Color32 Import(in Color32 src, in float roughnessFactor)
        {
            // glTF
            byte occlusion = src.r;
            byte roughness = src.g;
            byte metallic = src.b;

            int pixelRoughness = (int)(roughness * roughnessFactor);

            byte smoothness = (byte)Mathf.Clamp(255 - pixelRoughness, 0, 255);

            // Unity
            return new Color32
            {
                r = metallic,
                g = occlusion,
                b = 0,
                a = smoothness,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="smoothnessFactor"></param>
        /// <returns></returns>
        private Color32 Export(in Color32 src, in float smoothnessFactor)
        {
            // Unity
            byte metallic = src.r;
            byte occlusion = src.g;
            byte smoothness = src.a;

            int pixelSmoothness = (int)(smoothness * smoothnessFactor);

            byte roughness = (byte)Mathf.Clamp(255 - pixelSmoothness, 0, 255);

            // glTF
            return new Color32
            {
                r = occlusion,
                g = roughness,
                b = metallic,
                a = 255,
            };
        }

        #endregion
    }
}
