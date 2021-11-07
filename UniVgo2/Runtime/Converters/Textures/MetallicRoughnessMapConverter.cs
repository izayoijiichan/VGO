// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Converters
// @Class     : MetallicRoughnessMapConverter
// ----------------------------------------------------------------------
namespace UniVgo2.Converters
{
    using NewtonVgo;
    using UnityEngine;

    /// <summary>
    /// Metallic Roughness Map Converter
    /// </summary>
    public class MetallicRoughnessMapConverter : TextureConverter
    {
        /// <summary>The smoothness or roughness.</summary>
        private float _SmoothnessOrRoughness;

        /// <summary>
        /// Create a new instance of MetallicRoughnessMapConverter with smoothnessOrRoughness.
        /// </summary>
        /// <param name="smoothnessOrRoughness">The smoothness or roughness.</param>
        public MetallicRoughnessMapConverter(float smoothnessOrRoughness)
        {
            _SmoothnessOrRoughness = smoothnessOrRoughness;
        }

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
                pixcels[i] = Import(pixcels[i]);
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
                pixcels[i] = Export(pixcels[i]);
            }

            copyTexture.SetPixels32(pixcels);

            copyTexture.Apply();

            return copyTexture;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        /// <remarks>
        /// Roughness(glTF): dst.g -> Smoothness(Unity): src.a (with conversion)
        /// Metallic(glTF) : dst.b -> Metallic(Unity)  : src.r
        /// 
        /// https://github.com/dwango/UniVRM/issues/212.
        /// </remarks>
        public Color32 Import(Color32 src)
        {
            float pixelRoughnessFactor = (src.g * _SmoothnessOrRoughness) / 255.0f; // roughness
            float pixelSmoothness = 1.0f - Mathf.Sqrt(pixelRoughnessFactor);

            return new Color32
            {
                r = src.b,
                g = 0,
                b = 0,
                a = (byte)Mathf.Clamp(pixelSmoothness * 255, 0, 255),
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        /// <remarks>
        /// Smoothness(Unity): src.a -> Roughness(glTF): dst.g (with conversion)
        /// Metallic(Unity)  : src.r -> Metallic(glTF) : dst.b
        /// 
        /// https://blogs.unity3d.com/jp/2016/01/25/ggx-in-unity-5-3/
        /// https://github.com/dwango/UniVRM/issues/212.
        /// </remarks>
        public Color32 Export(Color32 src)
        {
            float pixelSmoothness = (src.a * _SmoothnessOrRoughness) / 255.0f; // smoothness
            float pixelRoughnessFactorSqrt = (1.0f - pixelSmoothness);
            float pixelRoughnessFactor = pixelRoughnessFactorSqrt * pixelRoughnessFactorSqrt;

            return new Color32
            {
                r = 0,
                g = (byte)Mathf.Clamp(pixelRoughnessFactor * 255, 0, 255),
                b = src.r,
                a = 255,
            };
        }
    }
}
