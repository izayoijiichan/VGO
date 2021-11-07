// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Converters
// @Class     : OcclusionMapConverter
// ----------------------------------------------------------------------
namespace UniVgo2.Converters
{
    using NewtonVgo;
    using UnityEngine;

    /// <summary>
    /// Occlusion Map Converter
    /// </summary>
    public class OcclusionMapConverter : TextureConverter
    {
        /// <summary>
        /// Get import texture.
        /// </summary>
        /// <param name="source">The source texture.</param>
        /// <returns></returns>
        public Texture2D GetImportTexture(Texture2D texture)
        {
            Texture2D copyTexture = CopyTexture2d(texture, VgoColorSpaceType.Linear);

            Color32[] pixcels = copyTexture.GetPixels32();

            for (int i = 0; i < pixcels.Length; i++)
            {
                pixcels[i] = new Color32(r: 0, g: pixcels[i].r, b: 0, a: 255);
            }

            copyTexture.SetPixels32(pixcels);

            copyTexture.Apply();

            return copyTexture;
        }

        /// <summary>
        /// Get export texture.
        /// </summary>
        /// <param name="source">The source texture.</param>
        /// <returns></returns>
        public Texture2D GetExportTexture(Texture2D texture)
        {
            Texture2D copyTexture = CopyTexture2d(texture, VgoColorSpaceType.Linear);

            Color32[] pixcels = copyTexture.GetPixels32();

            for (int i = 0; i < pixcels.Length; i++)
            {
                pixcels[i] = new Color32(r: pixcels[i].g, g: 0, b: 0, a: 255);
            }

            copyTexture.SetPixels32(pixcels);

            copyTexture.Apply();

            return copyTexture;
        }
    }
}
