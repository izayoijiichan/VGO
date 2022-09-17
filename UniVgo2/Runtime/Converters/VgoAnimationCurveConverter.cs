// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Converters
// @Class     : VgoAnimationCurveConverter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Converters
{
    using NewtonVgo;
    using UnityEngine;

    /// <summary>
    /// VGO Animation Curve Converter
    /// </summary>
    public class VgoAnimationCurveConverter
    {
        /// <summary>
        /// Create VGO_AnimationCurve from AnimationCurve.
        /// </summary>
        /// <param name="animationCurve"></param>
        /// <returns></returns>
        public static VgoAnimationCurve CreateFrom(AnimationCurve animationCurve)
        {
            var vgoAnimationCurve = new VgoAnimationCurve()
            {
                keys = null,
                preWrapMode = (NewtonVgo.WrapMode)animationCurve.preWrapMode,
                postWrapMode = (NewtonVgo.WrapMode)animationCurve.postWrapMode,
            };

            if (animationCurve.keys != null)
            {
                vgoAnimationCurve.keys = new VgoKeyframe[animationCurve.keys.Length];

                for (int i = 0; i < animationCurve.keys.Length; i++)
                {
                    vgoAnimationCurve.keys[i] = VgoKeyframeConverter.CreateFrom(animationCurve.keys[i]);
                }
            }

            return vgoAnimationCurve;
        }
        /// <summary>
        /// Create VGO_AnimationCurve from AnimationCurve.
        /// </summary>
        /// <param name="animationCurve"></param>
        /// <returns></returns>
        public static VgoAnimationCurve? CreateOrDefaultFrom(AnimationCurve? animationCurve)
        {
            if (animationCurve == null)
            {
                return default;
            }

            return CreateFrom(animationCurve);
        }

        /// <summary>
        /// Create AnimationCurve from VGO_AnimationCurve.
        /// </summary>
        /// <param name="vgoAnimationCurve"></param>
        /// <returns></returns>
        public static AnimationCurve CreateAnimationCurve(VgoAnimationCurve vgoAnimationCurve)
        {
            var animationCurve = new AnimationCurve()
            {
                //keys = null,
                preWrapMode = (UnityEngine.WrapMode)vgoAnimationCurve.preWrapMode,
                postWrapMode = (UnityEngine.WrapMode)vgoAnimationCurve.postWrapMode,
            };

            if (vgoAnimationCurve.keys != null)
            {
                var keys = new Keyframe[vgoAnimationCurve.keys.Length];

                for (int i = 0; i < vgoAnimationCurve.keys.Length; i++)
                {
                    keys[i] = VgoKeyframeConverter.CreateKeyframe(vgoAnimationCurve.keys[i]);
                }

                animationCurve.keys = keys;
            }

            return animationCurve;
        }

        /// <summary>
        /// Create AnimationCurve from VGO_AnimationCurve.
        /// </summary>
        /// <param name="vgoAnimationCurve"></param>
        /// <returns></returns>
        public static AnimationCurve? CreateAnimationCurveOrDefault(VgoAnimationCurve? vgoAnimationCurve)
        {
            if (vgoAnimationCurve is null)
            {
                return default;
            }

            return CreateAnimationCurve(vgoAnimationCurve);
        }
    }
}