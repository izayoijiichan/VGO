// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Converters
// @Class     : VgoAvatarConverter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Converters
{
    using NewtonVgo;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    /// <summary>
    /// VGO Avatar Converter
    /// </summary>
    public class VgoAvatarConverter
    {
        /// <summary>
        /// Create VgoAvatar.
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="nodeName"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static VgoHumanAvatar? CreateVgoAvatarOrDefault(in Animator animator, in string nodeName, in IList<Transform> nodes)
        {
            if (animator.avatar is null)
            {
                return default;
            }

            if (animator.avatar.isHuman == false)
            {
                return default;
            }

            if (animator.avatar.isValid == false)
            {
                return default;
            }

            Avatar humanAvatar = animator.avatar;

            var vgoHumanBones = new List<VgoHumanBone?>(humanAvatar.humanDescription.human.Length);

            for (int humanBoneIndex = 0; humanBoneIndex < humanAvatar.humanDescription.human.Length; humanBoneIndex++)
            {
                string humanBoneName = humanAvatar.humanDescription.human[humanBoneIndex].humanName;

                if (Enum.TryParse(humanBoneName.Replace(" ", ""), out UnityEngine.HumanBodyBones humanBodyBone) == false)
                {
                    Debug.LogError($"unknown humanBoneName: {humanBoneName}");
                }

                var vgoHumanBone = new VgoHumanBone
                {
                    humanBodyBone = (NewtonVgo.HumanBodyBones)humanBodyBone,
                    nodeIndex = nodes.IndexOf(animator.GetBoneTransform(humanBodyBone)),
                };

                vgoHumanBones.Add(vgoHumanBone);
            }

            var vgoAvatar = new VgoHumanAvatar
            {
                name = nodeName,
                humanBones = vgoHumanBones,
            };

            return vgoAvatar;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vgoAvatar"></param>
        /// <param name="go"></param>
        /// <param name="nodes"></param>
        public static Avatar CreateHumanAvatar(in VgoHumanAvatar vgoAvatar, in GameObject go, in List<Transform> nodes)
        {
            //string[] humanNames = HumanTrait.BoneName;

            HumanBone[] humanBones = new HumanBone[vgoAvatar.humanBones.Count];

            for (int humanBoneIndex = 0; humanBoneIndex < vgoAvatar.humanBones.Count; humanBoneIndex++)
            {
                VgoHumanBone? vgoHumanBone = vgoAvatar.humanBones[humanBoneIndex];

                if (vgoHumanBone is null)
                {
#if NET_STANDARD_2_1
                    ThrowHelper.ThrowFormatException($"vgoAvatar.humanBones[{humanBoneIndex}] is null.");
#else
                    throw new FormatException($"vgoAvatar.humanBones[{humanBoneIndex}] is null.");
#endif
                }

                humanBones[humanBoneIndex] = new HumanBone
                {
                    boneName = nodes[vgoHumanBone.nodeIndex].name,
                    humanName = _HumanBoneNameDic[vgoHumanBone.humanBodyBone],
                    limit = new HumanLimit
                    {
                        useDefaultValues = true,
                    }
                };
            }

            // @notice
            SkeletonBone[] skeletonBones = nodes
                .Select(t => new SkeletonBone
                {
                    name = t.name,
                    position = t.localPosition,
                    rotation = t.localRotation,
                    scale = t.localScale,
                })
                .ToArray();

            var humanDescription = new HumanDescription
            {
                skeleton = skeletonBones,
                human = humanBones,
                armStretch = 0.05f,
                legStretch = 0.05f,
                upperArmTwist = 0.5f,
                lowerArmTwist = 0.5f,
                upperLegTwist = 0.5f,
                lowerLegTwist = 0.5f,
                feetSpacing = 0,
                hasTranslationDoF = false,
            };

            Avatar avatar = AvatarBuilder.BuildHumanAvatar(go, humanDescription);

            avatar.name = vgoAvatar.name;

            return avatar;
        }

        /// <summary></summary>
        protected static readonly Dictionary<NewtonVgo.HumanBodyBones, string> _HumanBoneNameDic = new Dictionary<NewtonVgo.HumanBodyBones, string>()
        {
            { NewtonVgo.HumanBodyBones.Hips, "Hips" },
            { NewtonVgo.HumanBodyBones.LeftUpperLeg, "LeftUpperLeg" },
            { NewtonVgo.HumanBodyBones.RightUpperLeg, "RightUpperLeg" },
            { NewtonVgo.HumanBodyBones.LeftLowerLeg, "LeftLowerLeg" },
            { NewtonVgo.HumanBodyBones.RightLowerLeg, "RightLowerLeg" },
            { NewtonVgo.HumanBodyBones.LeftFoot, "LeftFoot" },
            { NewtonVgo.HumanBodyBones.RightFoot, "RightFoot" },
            { NewtonVgo.HumanBodyBones.Spine, "Spine" },
            { NewtonVgo.HumanBodyBones.Chest, "Chest" },
            { NewtonVgo.HumanBodyBones.Neck, "Neck" },
            { NewtonVgo.HumanBodyBones.Head, "Head" },
            { NewtonVgo.HumanBodyBones.LeftShoulder, "LeftShoulder" },
            { NewtonVgo.HumanBodyBones.RightShoulder, "RightShoulder" },
            { NewtonVgo.HumanBodyBones.LeftUpperArm, "LeftUpperArm" },
            { NewtonVgo.HumanBodyBones.RightUpperArm, "RightUpperArm" },
            { NewtonVgo.HumanBodyBones.LeftLowerArm, "LeftLowerArm" },
            { NewtonVgo.HumanBodyBones.RightLowerArm, "RightLowerArm" },
            { NewtonVgo.HumanBodyBones.LeftHand, "LeftHand" },
            { NewtonVgo.HumanBodyBones.RightHand, "RightHand" },
            { NewtonVgo.HumanBodyBones.LeftToes, "LeftToes" },
            { NewtonVgo.HumanBodyBones.RightToes, "RightToes" },
            { NewtonVgo.HumanBodyBones.LeftEye, "LeftEye" },
            { NewtonVgo.HumanBodyBones.RightEye, "RightEye" },
            { NewtonVgo.HumanBodyBones.Jaw, "Jaw" },
            { NewtonVgo.HumanBodyBones.LeftThumbProximal, "Left Thumb Proximal" },
            { NewtonVgo.HumanBodyBones.LeftThumbIntermediate, "Left Thumb Intermediate" },
            { NewtonVgo.HumanBodyBones.LeftThumbDistal, "Left Thumb Distal" },
            { NewtonVgo.HumanBodyBones.LeftIndexProximal, "Left Index Proximal" },
            { NewtonVgo.HumanBodyBones.LeftIndexIntermediate, "Left Index Intermediate" },
            { NewtonVgo.HumanBodyBones.LeftIndexDistal, "Left Index Distal" },
            { NewtonVgo.HumanBodyBones.LeftMiddleProximal, "Left Middle Proximal" },
            { NewtonVgo.HumanBodyBones.LeftMiddleIntermediate, "Left Middle Intermediate" },
            { NewtonVgo.HumanBodyBones.LeftMiddleDistal, "Left Middle Distal" },
            { NewtonVgo.HumanBodyBones.LeftRingProximal, "Left Ring Proximal" },
            { NewtonVgo.HumanBodyBones.LeftRingIntermediate, "Left Ring Intermediate" },
            { NewtonVgo.HumanBodyBones.LeftRingDistal, "Left Ring Distal" },
            { NewtonVgo.HumanBodyBones.LeftLittleProximal, "Left Little Proximal" },
            { NewtonVgo.HumanBodyBones.LeftLittleIntermediate, "Left Little Intermediate" },
            { NewtonVgo.HumanBodyBones.LeftLittleDistal, "Left Little Distal" },
            { NewtonVgo.HumanBodyBones.RightThumbProximal, "Right Thumb Proximal" },
            { NewtonVgo.HumanBodyBones.RightThumbIntermediate, "Right Thumb Intermediate" },
            { NewtonVgo.HumanBodyBones.RightThumbDistal, "Right Thumb Distal" },
            { NewtonVgo.HumanBodyBones.RightIndexProximal, "Right Index Proximal" },
            { NewtonVgo.HumanBodyBones.RightIndexIntermediate, "Right Index Intermediate" },
            { NewtonVgo.HumanBodyBones.RightIndexDistal, "Right Index Distal" },
            { NewtonVgo.HumanBodyBones.RightMiddleProximal, "Right Middle Proximal" },
            { NewtonVgo.HumanBodyBones.RightMiddleIntermediate, "Right Middle Intermediate" },
            { NewtonVgo.HumanBodyBones.RightMiddleDistal, "Right Middle Distal" },
            { NewtonVgo.HumanBodyBones.RightRingProximal, "Right Ring Proximal" },
            { NewtonVgo.HumanBodyBones.RightRingIntermediate, "Right Ring Intermediate" },
            { NewtonVgo.HumanBodyBones.RightRingDistal, "Right Ring Distal" },
            { NewtonVgo.HumanBodyBones.RightLittleProximal, "Right Little Proximal" },
            { NewtonVgo.HumanBodyBones.RightLittleIntermediate, "Right Little Intermediate" },
            { NewtonVgo.HumanBodyBones.RightLittleDistal, "Right Little Distal" },
            { NewtonVgo.HumanBodyBones.UpperChest, "UpperChest" },
            { NewtonVgo.HumanBodyBones.LastBone, "LastBone" },
        };
    }
}