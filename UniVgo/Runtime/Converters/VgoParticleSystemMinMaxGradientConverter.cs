// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : VgoParticleSystemMinMaxGradientConverter
// ----------------------------------------------------------------------
namespace UniVgo
{
    using UniGLTFforUniVgo;
    using UnityEngine;
    using static UnityEngine.ParticleSystem;

    /// <summary>
    /// VGO ParticleSystem MinMaxGradient Converter
    /// </summary>
    public class VgoParticleSystemMinMaxGradientConverter
    {
        /// <summary>
        /// Create VGO_PS_MinMaxGradient from MinMaxGradient.
        /// </summary>
        /// <param name="minMaxGradient"></param>
        /// <returns></returns>
        public static VGO_PS_MinMaxGradient CreateFrom(MinMaxGradient minMaxGradient)
        {
            switch (minMaxGradient.mode)
            {
                case ParticleSystemGradientMode.Color:
                    return new VGO_PS_MinMaxGradient()
                    {
                        mode = ParticleSystemGradientMode.Color,
                        color = minMaxGradient.color.linear.ToArray(),
                    };

                case ParticleSystemGradientMode.Gradient:
                    return new VGO_PS_MinMaxGradient()
                    {
                        mode = ParticleSystemGradientMode.Gradient,
                        gradient = VgoGradientConverter.CreateFrom(minMaxGradient.gradient),
                    };

                case ParticleSystemGradientMode.TwoColors:
                    return new VGO_PS_MinMaxGradient()
                    {
                        mode = ParticleSystemGradientMode.TwoColors,
                        colorMin = minMaxGradient.colorMin.linear.ToArray(),
                        colorMax = minMaxGradient.colorMax.linear.ToArray(),
                    };

                case ParticleSystemGradientMode.TwoGradients:
                    return new VGO_PS_MinMaxGradient()
                    {
                        mode = ParticleSystemGradientMode.TwoGradients,
                        gradientMin = VgoGradientConverter.CreateFrom(minMaxGradient.gradientMin),
                        gradientMax = VgoGradientConverter.CreateFrom(minMaxGradient.gradientMax),
                    };

                case ParticleSystemGradientMode.RandomColor:
                    // @notice
                    return new VGO_PS_MinMaxGradient()
                    {
                        mode = ParticleSystemGradientMode.RandomColor,
                        colorMin = minMaxGradient.colorMin.linear.ToArray(),
                        colorMax = minMaxGradient.colorMax.linear.ToArray(),
                        gradientMin = VgoGradientConverter.CreateFrom(minMaxGradient.gradientMin),
                        gradientMax = VgoGradientConverter.CreateFrom(minMaxGradient.gradientMax),
                    };

                default:
                    return default;
            }
        }

        /// <summary>
        /// Create MinMaxGradient from VGO_PS_MinMaxGradient.
        /// </summary>
        /// <param name="vgoMinMaxGradient"></param>
        /// <returns></returns>
        public static MinMaxGradient CreateMinMaxGradient(VGO_PS_MinMaxGradient vgoMinMaxGradient)
        {
            if (vgoMinMaxGradient == null)
            {
                return default;
            }

            switch (vgoMinMaxGradient.mode)
            {
                case ParticleSystemGradientMode.Color:
                    return new MinMaxGradient(color: ArrayConverter.ToColor(vgoMinMaxGradient.color, gamma: true));

                case ParticleSystemGradientMode.Gradient:
                    return new MinMaxGradient(gradient: VgoGradientConverter.CreateGradient(vgoMinMaxGradient.gradient));

                case ParticleSystemGradientMode.TwoColors:
                    return new MinMaxGradient(
                        min: ArrayConverter.ToColor(vgoMinMaxGradient.colorMin, gamma: true),
                        max: ArrayConverter.ToColor(vgoMinMaxGradient.colorMax, gamma: true)
                    );

                case ParticleSystemGradientMode.TwoGradients:
                    return new MinMaxGradient(
                        min: VgoGradientConverter.CreateGradient(vgoMinMaxGradient.gradientMin),
                        max: VgoGradientConverter.CreateGradient(vgoMinMaxGradient.gradientMax)
                    );

                case ParticleSystemGradientMode.RandomColor:
                    // @notice
                    //var minMaxGradient = new MinMaxGradient(
                    //    min: VgoGradientConverter.CreateGradient(vgoMinMaxGradient.GradientMin),
                    //    max: VgoGradientConverter.CreateGradient(vgoMinMaxGradient.GradientMax)
                    //);
                    var minMaxGradient = new MinMaxGradient(
                        min: ArrayConverter.ToColor(vgoMinMaxGradient.colorMin, gamma: true),
                        max: ArrayConverter.ToColor(vgoMinMaxGradient.colorMax, gamma: true)
                    );
                    minMaxGradient.mode = ParticleSystemGradientMode.RandomColor;
                    return minMaxGradient;

                default:
                    return default;
            }
        }
    }
}