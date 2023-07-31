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
        #region Public Methods

        /// <summary>
        /// Get import texture.
        /// </summary>
        /// <param name="texture">The source texture.</param>
        /// <param name="roughnessFactor"></param>
        /// <returns></returns>
        public Texture2D GetImportTexture(in Texture2D texture, in float roughnessFactor)
        {
            Texture2D copyTexture = CopyTexture2d(texture, VgoColorSpaceType.Linear);

            Color32[] pixcels = copyTexture.GetPixels32();

            for (int i = 0; i < pixcels.Length; i++)
            {
                pixcels[i] = Import(pixcels[i], roughnessFactor);
            }

            copyTexture.SetPixels32(pixcels);

            copyTexture.Apply();

            return copyTexture;
        }

        /// <summary>
        /// Get export texture.
        /// </summary>
        /// <param name="texture">The source texture.</param>
        /// <param name="smoothnessFactor"></param>
        /// <returns></returns>
        public Texture2D GetExportTexture(in Texture2D texture, in float smoothnessFactor)
        {
            Texture2D copyTexture = CopyTexture2d(texture, VgoColorSpaceType.Linear);

            Color32[] pixcels = copyTexture.GetPixels32();

            for (int i = 0; i < pixcels.Length; i++)
            {
                pixcels[i] = Export(pixcels[i], smoothnessFactor);
            }

            copyTexture.SetPixels32(pixcels);

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
