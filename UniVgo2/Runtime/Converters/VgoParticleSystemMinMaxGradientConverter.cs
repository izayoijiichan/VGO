// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Converters
// @Class     : VgoParticleSystemMinMaxGradientConverter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Converters
{
    using NewtonVgo.Schema.ParticleSystems;
    using System;
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
                case UnityEngine.ParticleSystemGradientMode.Color:
                    return new VGO_PS_MinMaxGradient()
                    {
                        mode = NewtonVgo.ParticleSystemGradientMode.Color,
                        color = minMaxGradient.color.linear.ToVgoColor4(),
                    };

                case UnityEngine.ParticleSystemGradientMode.Gradient:
                    return new VGO_PS_MinMaxGradient()
                    {
                        mode = NewtonVgo.ParticleSystemGradientMode.Gradient,
                        gradient = VgoGradientConverter.CreateOrDefaultFrom(minMaxGradient.gradient),
                    };

                case UnityEngine.ParticleSystemGradientMode.TwoColors:
                    return new VGO_PS_MinMaxGradient()
                    {
                        mode = NewtonVgo.ParticleSystemGradientMode.TwoColors,
                        colorMin = minMaxGradient.colorMin.linear.ToVgoColor4(),
                        colorMax = minMaxGradient.colorMax.linear.ToVgoColor4(),
                    };

                case UnityEngine.ParticleSystemGradientMode.TwoGradients:
                    return new VGO_PS_MinMaxGradient()
                    {
                        mode = NewtonVgo.ParticleSystemGradientMode.TwoGradients,
                        gradientMin = VgoGradientConverter.CreateOrDefaultFrom(minMaxGradient.gradientMin),
                        gradientMax = VgoGradientConverter.CreateOrDefaultFrom(minMaxGradient.gradientMax),
                    };

                case UnityEngine.ParticleSystemGradientMode.RandomColor:
                    // @notice
                    return new VGO_PS_MinMaxGradient()
                    {
                        mode = NewtonVgo.ParticleSystemGradientMode.RandomColor,
                        colorMin = minMaxGradient.colorMin.linear.ToVgoColor4(),
                        colorMax = minMaxGradient.colorMax.linear.ToVgoColor4(),
                        gradientMin = VgoGradientConverter.CreateOrDefaultFrom(minMaxGradient.gradientMin),
                        gradientMax = VgoGradientConverter.CreateOrDefaultFrom(minMaxGradient.gradientMax),
                    };

                default:
                    throw new IndexOutOfRangeException($"{minMaxGradient.mode}");
            }
        }

        /// <summary>
        /// Create MinMaxGradient from VGO_PS_MinMaxGradient.
        /// </summary>
        /// <param name="vgoMinMaxGradient"></param>
        /// <returns></returns>
        public static MinMaxGradient CreateMinMaxGradient(VGO_PS_MinMaxGradient? vgoMinMaxGradient)
        {
            if (vgoMinMaxGradient == null)
            {
                return default;
            }

            switch (vgoMinMaxGradient.mode)
            {
                case NewtonVgo.ParticleSystemGradientMode.Color:
                    return new MinMaxGradient(color: vgoMinMaxGradient.color.ToUnityColor().gamma);

                case NewtonVgo.ParticleSystemGradientMode.Gradient:
                    return new MinMaxGradient(gradient: VgoGradientConverter.CreateGradientOrDefault(vgoMinMaxGradient.gradient));

                case NewtonVgo.ParticleSystemGradientMode.TwoColors:
                    return new MinMaxGradient(
                        min: vgoMinMaxGradient.colorMin.ToUnityColor().gamma,
                        max: vgoMinMaxGradient.colorMax.ToUnityColor().gamma
                    );

                case NewtonVgo.ParticleSystemGradientMode.TwoGradients:
                    return new MinMaxGradient(
                        min: VgoGradientConverter.CreateGradientOrDefault(vgoMinMaxGradient.gradientMin),
                        max: VgoGradientConverter.CreateGradientOrDefault(vgoMinMaxGradient.gradientMax)
                    );

                case NewtonVgo.ParticleSystemGradientMode.RandomColor:
                    // @notice
                    //var minMaxGradient = new MinMaxGradient(
                    //    min: VgoGradientConverter.CreateGradient(vgoMinMaxGradient.gradientMin),
                    //    max: VgoGradientConverter.CreateGradient(vgoMinMaxGradient.gradientMax)
                    //);
                    var minMaxGradient = new MinMaxGradient(
                        min: vgoMinMaxGradient.colorMin.ToUnityColor().gamma,
                        max: vgoMinMaxGradient.colorMax.ToUnityColor().gamma
                    );
                    minMaxGradient.mode = UnityEngine.ParticleSystemGradientMode.RandomColor;
                    return minMaxGradient;

                default:
                    return default;
            }
        }
    }
}