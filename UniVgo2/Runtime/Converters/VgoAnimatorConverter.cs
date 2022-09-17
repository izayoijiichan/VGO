// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Converters
// @Class     : VgoAnimatorConverter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Converters
{
    using NewtonVgo;
    using UnityEngine;

    /// <summary>
    /// VGO Animator Converter
    /// </summary>
    public class VgoAnimatorConverter
    {
        /// <summary>
        /// Create VgoAnimator from Animator.
        /// </summary>
        /// <param name="animator"></param>
        /// <returns></returns>
        public static VgoAnimator CreateFrom(Animator animator)
        {
            var vgoAnimator = new VgoAnimator()
            {
                name = animator.name,
                enabled = animator.enabled,
                //avatar = VgoAvatarConverter.CreateVgoAvatar(animator, nodeName, nodes),
                applyRootMotion = animator.applyRootMotion,
                updateMode = (NewtonVgo.AnimatorUpdateMode)animator.updateMode,
                cullingMode = (NewtonVgo.AnimatorCullingMode)animator.cullingMode,
            };

            return vgoAnimator;
        }

        /// <summary>
        /// Set Animator parameter.
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="vgoAnimator"></param>
        public static void SetComponentValue(Animator animator, VgoAnimator vgoAnimator)
        {
            animator.name = vgoAnimator.name;
            animator.enabled = vgoAnimator.enabled;
            //animator.avatar = VgoAvatarConverter.CreateHumanAvatar(go, vgoAnimator.avatar, nodes);
            animator.applyRootMotion = vgoAnimator.applyRootMotion;
            animator.updateMode = (UnityEngine.AnimatorUpdateMode)vgoAnimator.updateMode;
            animator.cullingMode = (UnityEngine.AnimatorCullingMode)vgoAnimator.cullingMode;
        }
    }
}