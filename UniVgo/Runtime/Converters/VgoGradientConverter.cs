// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : VgoGradientConverter
// ----------------------------------------------------------------------
namespace UniVgo
{
    using UniGLTFforUniVgo;
    using UnityEngine;

    /// <summary>
    /// VGO Gradient Converter
    /// </summary>
    public class VgoGradientConverter
    {
        /// <summary>
        /// Create VGO_Gradient from Gradient.
        /// </summary>
        /// <param name="gradient"></param>
        /// <returns></returns>
        public static VGO_Gradient CreateFrom(Gradient gradient)
        {
            if (gradient == null)
            {
                return default;
            }

            var vgoGradient = new VGO_Gradient()
            {
                colorKeys = null,
                alphaKeys = null,
                mode = gradient.mode,
            };

            if (gradient.colorKeys != null)
            {
                vgoGradient.colorKeys = new VGO_GradientColorKey[gradient.colorKeys.Length];

                for (int i = 0; i < gradient.colorKeys.Length; i++)
                {
                    vgoGradient.colorKeys[i] = new VGO_GradientColorKey()
                    {
                        color = gradient.colorKeys[i].color.linear.ToArray(),
                        time = gradient.colorKeys[i].time,
                    };
                }
            }

            if (gradient.alphaKeys != null)
            {
                vgoGradient.alphaKeys = new VGO_GradientAlphaKey[gradient.alphaKeys.Length];

                for (int i = 0; i < gradient.alphaKeys.Length; i++)
                {
                    vgoGradient.alphaKeys[i] = new VGO_GradientAlphaKey()
                    {
                        alpha = gradient.alphaKeys[i].alpha,
                        time = gradient.alphaKeys[i].time,
                    };
                }
            }

            return vgoGradient;
        }

        /// <summary>
        /// Create Gradient from VGO_Gradient.
        /// </summary>
        /// <param name="vgoGradient"></param>
        /// <returns></returns>
        public static Gradient CreateGradient(VGO_Gradient vgoGradient)
        {
            if (vgoGradient == null)
            {
                return default;
            }

            var gradient = new Gradient()
            {
                //colorKeys = null,
                //alphaKeys = null,
                mode = vgoGradient.mode,
            };

            if (vgoGradient.colorKeys != null)
            {
                var colorKeys = new GradientColorKey[vgoGradient.colorKeys.Length];

                for (int i = 0; i < vgoGradient.colorKeys.Length; i++)
                {
                    colorKeys[i] = new GradientColorKey(
                        col: ArrayConverter.ToColor(vgoGradient.colorKeys[i].color, gamma: true),
                        time: vgoGradient.colorKeys[i].time
                    );
                }

                gradient.colorKeys = colorKeys;
            }

            if (vgoGradient.alphaKeys != null)
            {
                var alphaKeys = new GradientAlphaKey[vgoGradient.alphaKeys.Length];

                for (int i = 0; i < vgoGradient.alphaKeys.Length; i++)
                {
                    alphaKeys[i] = new GradientAlphaKey(
                        alpha: vgoGradient.alphaKeys[i].alpha,
                        time: vgoGradient.alphaKeys[i].time
                    );
                }

                gradient.alphaKeys = alphaKeys;
            }

            return gradient;
        }
    }
}