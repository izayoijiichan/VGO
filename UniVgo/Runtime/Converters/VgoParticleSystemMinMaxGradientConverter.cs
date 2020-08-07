// ----------------------------------------------------------------------
// @Namespace : UniVgo.Converters
// @Class     : VgoParticleSystemMinMaxGradientConverter
// ----------------------------------------------------------------------
namespace UniVgo.Converters
{
    using NewtonGltf;
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
                        mode = VgoGltf.ParticleSystemGradientMode.Color,
                        color = minMaxGradient.color.linear.ToGltfColor4(),
                    };

                case UnityEngine.ParticleSystemGradientMode.Gradient:
                    return new VGO_PS_MinMaxGradient()
                    {
                        mode = VgoGltf.ParticleSystemGradientMode.Gradient,
                        gradient = VgoGradientConverter.CreateFrom(minMaxGradient.gradient),
                    };

                case UnityEngine.ParticleSystemGradientMode.TwoColors:
                    return new VGO_PS_MinMaxGradient()
                    {
                        mode = VgoGltf.ParticleSystemGradientMode.TwoColors,
                        colorMin = minMaxGradient.colorMin.linear.ToGltfColor4(),
                        colorMax = minMaxGradient.colorMax.linear.ToGltfColor4(),
                    };

                case UnityEngine.ParticleSystemGradientMode.TwoGradients:
                    return new VGO_PS_MinMaxGradient()
                    {
                        mode = VgoGltf.ParticleSystemGradientMode.TwoGradients,
                        gradientMin = VgoGradientConverter.CreateFrom(minMaxGradient.gradientMin),
                        gradientMax = VgoGradientConverter.CreateFrom(minMaxGradient.gradientMax),
                    };

                case UnityEngine.ParticleSystemGradientMode.RandomColor:
                    // @notice
                    return new VGO_PS_MinMaxGradient()
                    {
                        mode = VgoGltf.ParticleSystemGradientMode.RandomColor,
                        colorMin = minMaxGradient.colorMin.linear.ToGltfColor4(),
                        colorMax = minMaxGradient.colorMax.linear.ToGltfColor4(),
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
                case VgoGltf.ParticleSystemGradientMode.Color:
                    return new MinMaxGradient(color: vgoMinMaxGradient.color.ToUnityColor().gamma);

                case VgoGltf.ParticleSystemGradientMode.Gradient:
                    return new MinMaxGradient(gradient: VgoGradientConverter.CreateGradient(vgoMinMaxGradient.gradient));

                case VgoGltf.ParticleSystemGradientMode.TwoColors:
                    return new MinMaxGradient(
                        min: vgoMinMaxGradient.colorMin.ToUnityColor().gamma,
                        max: vgoMinMaxGradient.colorMax.ToUnityColor().gamma
                    );

                case VgoGltf.ParticleSystemGradientMode.TwoGradients:
                    return new MinMaxGradient(
                        min: VgoGradientConverter.CreateGradient(vgoMinMaxGradient.gradientMin),
                        max: VgoGradientConverter.CreateGradient(vgoMinMaxGradient.gradientMax)
                    );

                case VgoGltf.ParticleSystemGradientMode.RandomColor:
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