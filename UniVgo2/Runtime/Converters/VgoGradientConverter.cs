// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Converters
// @Class     : VgoGradientConverter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Converters
{
    using NewtonVgo;
    using UnityEngine;

    /// <summary>
    /// VGO Gradient Converter
    /// </summary>
    public class VgoGradientConverter
    {
        /// <summary>
        /// Create VgoGradient from Gradient.
        /// </summary>
        /// <param name="gradient"></param>
        /// <returns></returns>
        public static VgoGradient CreateFrom(in Gradient gradient)
        {
            var vgoGradient = new VgoGradient()
            {
                colorKeys = null,
                alphaKeys = null,
                mode = (NewtonVgo.GradientMode)gradient.mode,
            };

            if (gradient.colorKeys != null)
            {
                vgoGradient.colorKeys = new VgoGradientColorKey[gradient.colorKeys.Length];

                for (int i = 0; i < gradient.colorKeys.Length; i++)
                {
                    vgoGradient.colorKeys[i] = new VgoGradientColorKey()
                    {
                        color = gradient.colorKeys[i].color.linear.ToVgoColor4(),
                        time = gradient.colorKeys[i].time,
                    };
                }
            }

            if (gradient.alphaKeys != null)
            {
                vgoGradient.alphaKeys = new VgoGradientAlphaKey[gradient.alphaKeys.Length];

                for (int i = 0; i < gradient.alphaKeys.Length; i++)
                {
                    vgoGradient.alphaKeys[i] = new VgoGradientAlphaKey()
                    {
                        alpha = gradient.alphaKeys[i].alpha,
                        time = gradient.alphaKeys[i].time,
                    };
                }
            }

            return vgoGradient;
        }

        /// <summary>
        /// Create VgoGradient from Gradient.
        /// </summary>
        /// <param name="gradient"></param>
        /// <returns></returns>
        public static VgoGradient? CreateOrDefaultFrom(in Gradient? gradient)
        {
            if (gradient == null)
            {
                return default;
            }

            return CreateFrom(gradient);
        }

        /// <summary>
        /// Create Gradient from VgoGradient.
        /// </summary>
        /// <param name="vgoGradient"></param>
        /// <returns></returns>
        public static Gradient CreateGradient(in VgoGradient vgoGradient)
        {
            var gradient = new Gradient()
            {
                //colorKeys = null,
                //alphaKeys = null,
                mode = (UnityEngine.GradientMode)vgoGradient.mode,
            };

            if (vgoGradient.colorKeys != null)
            {
                var colorKeys = new GradientColorKey[vgoGradient.colorKeys.Length];

                for (int i = 0; i < vgoGradient.colorKeys.Length; i++)
                {
                    colorKeys[i] = new GradientColorKey(
                        col: vgoGradient.colorKeys[i].color.ToUnityColor().gamma,
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

        /// <summary>
        /// Create Gradient from VgoGradient.
        /// </summary>
        /// <param name="vgoGradient"></param>
        /// <returns></returns>
        public static Gradient? CreateGradientOrDefault(in VgoGradient? vgoGradient)
        {
            if (vgoGradient == null)
            {
                return default;
            }

            return CreateGradient(vgoGradient);
        }
    }
}