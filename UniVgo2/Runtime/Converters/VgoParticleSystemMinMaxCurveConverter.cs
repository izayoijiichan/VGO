// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Converters
// @Class     : VgoParticleSystemMinMaxCurveConverter
// ----------------------------------------------------------------------
namespace UniVgo2.Converters
{
    using NewtonVgo;
    using NewtonVgo.Schema.ParticleSystems;
    using static UnityEngine.ParticleSystem;

    /// <summary>
    /// VGO ParticleSystem MinMaxCurve Converter
    /// </summary>
    public class VgoParticleSystemMinMaxCurveConverter
    {
        /// <summary>
        /// Create VGO_PS_MinMaxCurve from MinMaxCurve.
        /// </summary>
        /// <param name="minMaxCurve"></param>
        /// <returns></returns>
        public static VGO_PS_MinMaxCurve CreateFrom(MinMaxCurve minMaxCurve)
        {
            switch (minMaxCurve.mode)
            {
                case UnityEngine.ParticleSystemCurveMode.Constant:
                    return new VGO_PS_MinMaxCurve()
                    {
                        mode = NewtonVgo.ParticleSystemCurveMode.Constant,
                        constant = minMaxCurve.constant,
                    };

                case UnityEngine.ParticleSystemCurveMode.Curve:
                    return new VGO_PS_MinMaxCurve()
                    {
                        mode = NewtonVgo.ParticleSystemCurveMode.Curve,
                        curveMultiplier = minMaxCurve.curveMultiplier,
                        curve = VgoAnimationCurveConverter.CreateFrom(minMaxCurve.curve),
                    };

                case UnityEngine.ParticleSystemCurveMode.TwoCurves:
                    return new VGO_PS_MinMaxCurve()
                    {
                        mode = NewtonVgo.ParticleSystemCurveMode.TwoCurves,
                        curveMultiplier = minMaxCurve.curveMultiplier,
                        curveMin = VgoAnimationCurveConverter.CreateFrom(minMaxCurve.curveMin),
                        curveMax = VgoAnimationCurveConverter.CreateFrom(minMaxCurve.curveMax),
                    };

                case UnityEngine.ParticleSystemCurveMode.TwoConstants:
                    return new VGO_PS_MinMaxCurve()
                    {
                        mode = NewtonVgo.ParticleSystemCurveMode.TwoConstants,
                        constantMin = minMaxCurve.constantMin,
                        constantMax = minMaxCurve.constantMax,
                    };

                default:
                    return null;
            }
        }

        /// <summary>
        /// Create MinMaxCurve from VGO_PS_MinMaxCurve.
        /// </summary>
        /// <param name="vgoMinMaxCurve"></param>
        /// <returns></returns>
        public static MinMaxCurve CreateMinMaxCurve(VGO_PS_MinMaxCurve vgoMinMaxCurve)
        {
            if (vgoMinMaxCurve == null)
            {
                return default;
            }

            switch (vgoMinMaxCurve.mode)
            {
                case NewtonVgo.ParticleSystemCurveMode.Constant:
                    return new MinMaxCurve(constant: vgoMinMaxCurve.constant);

                case NewtonVgo.ParticleSystemCurveMode.Curve:
                    return new MinMaxCurve(
                        multiplier: vgoMinMaxCurve.curveMultiplier,
                        curve: VgoAnimationCurveConverter.CreateAnimationCurve(vgoMinMaxCurve.curve)
                    );

                case NewtonVgo.ParticleSystemCurveMode.TwoCurves:
                    return new MinMaxCurve(
                        multiplier: vgoMinMaxCurve.curveMultiplier,
                        min: VgoAnimationCurveConverter.CreateAnimationCurve(vgoMinMaxCurve.curveMin),
                        max: VgoAnimationCurveConverter.CreateAnimationCurve(vgoMinMaxCurve.curveMax)
                    );

                case NewtonVgo.ParticleSystemCurveMode.TwoConstants:
                    return new MinMaxCurve(
                        min: vgoMinMaxCurve.constantMin,
                        max: vgoMinMaxCurve.constantMax
                    );

                default:
                    return default;
            }
        }
    }
}