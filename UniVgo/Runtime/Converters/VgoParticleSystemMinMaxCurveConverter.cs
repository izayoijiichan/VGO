// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : VgoParticleSystemMinMaxCurveConverter
// ----------------------------------------------------------------------
namespace UniVgo
{
    using UniGLTFforUniVgo;
    using UnityEngine;
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
                case ParticleSystemCurveMode.Constant:
                    return new VGO_PS_MinMaxCurve()
                    {
                        mode = ParticleSystemCurveMode.Constant,
                        constant = minMaxCurve.constant,
                    };

                case ParticleSystemCurveMode.Curve:
                    return new VGO_PS_MinMaxCurve()
                    {
                        mode = ParticleSystemCurveMode.Curve,
                        curveMultiplier = minMaxCurve.curveMultiplier,
                        curve = VgoAnimationCurveConverter.CreateFrom(minMaxCurve.curve),
                    };

                case ParticleSystemCurveMode.TwoCurves:
                    return new VGO_PS_MinMaxCurve()
                    {
                        mode = ParticleSystemCurveMode.TwoCurves,
                        curveMultiplier = minMaxCurve.curveMultiplier,
                        curveMin = VgoAnimationCurveConverter.CreateFrom(minMaxCurve.curveMin),
                        curveMax = VgoAnimationCurveConverter.CreateFrom(minMaxCurve.curveMax),
                    };

                case ParticleSystemCurveMode.TwoConstants:
                    return new VGO_PS_MinMaxCurve()
                    {
                        mode = ParticleSystemCurveMode.TwoConstants,
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
                case ParticleSystemCurveMode.Constant:
                    return new MinMaxCurve(constant: vgoMinMaxCurve.constant);

                case ParticleSystemCurveMode.Curve:
                    return new MinMaxCurve(
                        multiplier: vgoMinMaxCurve.curveMultiplier,
                        curve: VgoAnimationCurveConverter.CreateAnimationCurve(vgoMinMaxCurve.curve)
                    );

                case ParticleSystemCurveMode.TwoCurves:
                    return new MinMaxCurve(
                        multiplier: vgoMinMaxCurve.curveMultiplier,
                        min: VgoAnimationCurveConverter.CreateAnimationCurve(vgoMinMaxCurve.curveMin),
                        max: VgoAnimationCurveConverter.CreateAnimationCurve(vgoMinMaxCurve.curveMax)
                    );

                case ParticleSystemCurveMode.TwoConstants:
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