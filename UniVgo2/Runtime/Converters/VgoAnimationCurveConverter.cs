﻿// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Converters
// @Class     : VgoAnimationCurveConverter
// ----------------------------------------------------------------------
namespace UniVgo2.Converters
{
    using NewtonVgo;
    using UnityEngine;

    /// <summary>
    /// VGO AnimationCurve Converter
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
            if (animationCurve == null)
            {
                return default;
            }

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
        /// Create AnimationCurve from VGO_AnimationCurve.
        /// </summary>
        /// <param name="vgoAnimationCurve"></param>
        /// <returns></returns>
        public static AnimationCurve CreateAnimationCurve(VgoAnimationCurve vgoAnimationCurve)
        {
            if (vgoAnimationCurve == null)
            {
                return default;
            }

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
    }
}