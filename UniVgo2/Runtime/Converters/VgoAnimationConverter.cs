// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Converters
// @Class     : VgoAnimationConverter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Converters
{
    using NewtonVgo;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// VGO Animation Converter
    /// </summary>
    public class VgoAnimationConverter
    {
        /// <summary>
        /// Create VgoAnimation from Animation.
        /// </summary>
        /// <param name="animation"></param>
        /// <param name="animationClips"></param>
        /// <param name="geometryCoordinate"></param>
        /// <returns></returns>
        public static VgoAnimation CreateFrom(in Animation animation, in IList<AnimationClip> animationClips, in VgoGeometryCoordinate geometryCoordinate)
        {
            var vgoAnimation = new VgoAnimation()
            {
                name = animation.name,
                enabled = animation.enabled,
                clipIndex = animationClips.IndexOf(animation.clip),
                localBounds = VgoBoundsConverter.CreateFrom(animation.localBounds, geometryCoordinate),
                playAutomatically = animation.playAutomatically,
                animatePhysics = animation.animatePhysics,
                cullingType = (NewtonVgo.AnimationCullingType)animation.cullingType,
                wrapMode = (NewtonVgo.WrapMode)animation.wrapMode,
            };

            return vgoAnimation;
        }

        /// <summary>
        /// Set Animation parameter.
        /// </summary>
        /// <param name="animation"></param>
        /// <param name="vgoAnimation"></param>
        /// <param name="animationClips"></param>
        /// <param name="geometryCoordinate"></param>
        public static void SetComponentValue(in Animation animation, in VgoAnimation vgoAnimation, in List<AnimationClip?>? animationClips, in VgoGeometryCoordinate geometryCoordinate)
        {
            animation.name = vgoAnimation.name;
            animation.enabled = vgoAnimation.enabled;
            animation.localBounds = VgoBoundsConverter.CreateBounds(vgoAnimation.localBounds, geometryCoordinate);
            animation.playAutomatically = vgoAnimation.playAutomatically;
            animation.animatePhysics = vgoAnimation.animatePhysics;
            animation.cullingType = (UnityEngine.AnimationCullingType)vgoAnimation.cullingType;
            animation.wrapMode = (UnityEngine.WrapMode)vgoAnimation.wrapMode;

            if (animationClips == null)
            {
                return;
            }

            if (animationClips.TryGetValue(vgoAnimation.clipIndex, out AnimationClip? animationClip) == false)
            {
                return;
            }

            if (animationClip == null)
            {
                return;
            }

            animation.clip = animationClip;

            animation.AddClip(animationClip, animationClip.name);
        }
    }
}