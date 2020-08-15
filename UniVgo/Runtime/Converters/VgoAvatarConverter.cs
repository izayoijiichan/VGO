// ----------------------------------------------------------------------
// @Namespace : UniVgo.Converters
// @Class     : VgoAvatarConverter
// ----------------------------------------------------------------------
namespace UniVgo.Converters
{
    using NewtonGltf;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    /// <summary>
    /// VGO Avator Converter
    /// </summary>
    public class VgoAvatarConverter
    {
        /// <summary>
        /// Create VGO HumanAvatar.
        /// </summary>
        /// <param name="animator">The animator.</param>
        /// <param name="nodeName">The name of node.</param>
        /// <param name="nodes">List of node.</param>
        /// <returns></returns>
        public static VGO_HumanAvatar CreateVgoHumanAvatar(Animator animator, string nodeName, List<Transform> nodes)
        {
            if (animator.avatar is null)
            {
                return null;
            }

            if (animator.avatar.isHuman == false)
            {
                return null;
            }

            if (animator.avatar.isValid == false)
            {
                return null;
            }

            Avatar humanAvatar = animator.avatar;

            var vgoHumanBones = new List<VGO_HumanBone>();

            for (int humanBoneIndex = 0; humanBoneIndex < humanAvatar.humanDescription.human.Length; humanBoneIndex++)
            {
                string humanBoneName = humanAvatar.humanDescription.human[humanBoneIndex].humanName;

                if (Enum.TryParse(humanBoneName.Replace(" ", ""), out UnityEngine.HumanBodyBones humanBodyBone) == false)
                {
                    Debug.LogError($"unknown humanBoneName: {humanBoneName}");
                }

                var vgoHumanBone = new VGO_HumanBone
                {
                    humanBodyBone = (VgoGltf.HumanBodyBones)humanBodyBone,
                    nodeIndex = nodes.IndexOf(animator.GetBoneTransform(humanBodyBone)),
                };

                vgoHumanBones.Add(vgoHumanBone);
            }

            var vgoAvatar = new VGO_HumanAvatar
            {
                name = nodeName,
                humanBones = vgoHumanBones,
            };

            return vgoAvatar;
        }

        /// <summary>
        /// Create HumanAvatar.
        /// </summary>
        /// <param name="vgoHumanAvatar">The VGO human avatar.</param>
        /// <param name="go">The game object.</param>
        /// <param name="nodes">List of node.</param>
        public static Avatar CreateHumanAvatar(VGO_HumanAvatar vgoHumanAvatar, GameObject go, List<Transform> nodes)
        {
            //string[] humanNames = HumanTrait.BoneName;

            HumanBone[] humanBones = vgoHumanAvatar.humanBones
                .Select(x => new HumanBone
                {
                    boneName = nodes[x.nodeIndex].name,
                    humanName = _HumanBoneNameDic[x.humanBodyBone],
                    limit = new HumanLimit
                    {
                        useDefaultValues = true,
                    }
                }).ToArray();

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

            avatar.name = vgoHumanAvatar.name;

            return avatar;
        }

        /// <summary>The dictionary of human bone name.</summary>
        protected static readonly Dictionary<VgoGltf.HumanBodyBones, string> _HumanBoneNameDic = new Dictionary<VgoGltf.HumanBodyBones, string>()
        {
            { VgoGltf.HumanBodyBones.Hips, "Hips" },
            { VgoGltf.HumanBodyBones.LeftUpperLeg, "LeftUpperLeg" },
            { VgoGltf.HumanBodyBones.RightUpperLeg, "RightUpperLeg" },
            { VgoGltf.HumanBodyBones.LeftLowerLeg, "LeftLowerLeg" },
            { VgoGltf.HumanBodyBones.RightLowerLeg, "RightLowerLeg" },
            { VgoGltf.HumanBodyBones.LeftFoot, "LeftFoot" },
            { VgoGltf.HumanBodyBones.RightFoot, "RightFoot" },
            { VgoGltf.HumanBodyBones.Spine, "Spine" },
            { VgoGltf.HumanBodyBones.Chest, "Chest" },
            { VgoGltf.HumanBodyBones.Neck, "Neck" },
            { VgoGltf.HumanBodyBones.Head, "Head" },
            { VgoGltf.HumanBodyBones.LeftShoulder, "LeftShoulder" },
            { VgoGltf.HumanBodyBones.RightShoulder, "RightShoulder" },
            { VgoGltf.HumanBodyBones.LeftUpperArm, "LeftUpperArm" },
            { VgoGltf.HumanBodyBones.RightUpperArm, "RightUpperArm" },
            { VgoGltf.HumanBodyBones.LeftLowerArm, "LeftLowerArm" },
            { VgoGltf.HumanBodyBones.RightLowerArm, "RightLowerArm" },
            { VgoGltf.HumanBodyBones.LeftHand, "LeftHand" },
            { VgoGltf.HumanBodyBones.RightHand, "RightHand" },
            { VgoGltf.HumanBodyBones.LeftToes, "LeftToes" },
            { VgoGltf.HumanBodyBones.RightToes, "RightToes" },
            { VgoGltf.HumanBodyBones.LeftEye, "LeftEye" },
            { VgoGltf.HumanBodyBones.RightEye, "RightEye" },
            { VgoGltf.HumanBodyBones.Jaw, "Jaw" },
            { VgoGltf.HumanBodyBones.LeftThumbProximal, "Left Thumb Proximal" },
            { VgoGltf.HumanBodyBones.LeftThumbIntermediate, "Left Thumb Intermediate" },
            { VgoGltf.HumanBodyBones.LeftThumbDistal, "Left Thumb Distal" },
            { VgoGltf.HumanBodyBones.LeftIndexProximal, "Left Index Proximal" },
            { VgoGltf.HumanBodyBones.LeftIndexIntermediate, "Left Index Intermediate" },
            { VgoGltf.HumanBodyBones.LeftIndexDistal, "Left Index Distal" },
            { VgoGltf.HumanBodyBones.LeftMiddleProximal, "Left Middle Proximal" },
            { VgoGltf.HumanBodyBones.LeftMiddleIntermediate, "Left Middle Intermediate" },
            { VgoGltf.HumanBodyBones.LeftMiddleDistal, "Left Middle Distal" },
            { VgoGltf.HumanBodyBones.LeftRingProximal, "Left Ring Proximal" },
            { VgoGltf.HumanBodyBones.LeftRingIntermediate, "Left Ring Intermediate" },
            { VgoGltf.HumanBodyBones.LeftRingDistal, "Left Ring Distal" },
            { VgoGltf.HumanBodyBones.LeftLittleProximal, "Left Little Proximal" },
            { VgoGltf.HumanBodyBones.LeftLittleIntermediate, "Left Little Intermediate" },
            { VgoGltf.HumanBodyBones.LeftLittleDistal, "Left Little Distal" },
            { VgoGltf.HumanBodyBones.RightThumbProximal, "Right Thumb Proximal" },
            { VgoGltf.HumanBodyBones.RightThumbIntermediate, "Right Thumb Intermediate" },
            { VgoGltf.HumanBodyBones.RightThumbDistal, "Right Thumb Distal" },
            { VgoGltf.HumanBodyBones.RightIndexProximal, "Right Index Proximal" },
            { VgoGltf.HumanBodyBones.RightIndexIntermediate, "Right Index Intermediate" },
            { VgoGltf.HumanBodyBones.RightIndexDistal, "Right Index Distal" },
            { VgoGltf.HumanBodyBones.RightMiddleProximal, "Right Middle Proximal" },
            { VgoGltf.HumanBodyBones.RightMiddleIntermediate, "Right Middle Intermediate" },
            { VgoGltf.HumanBodyBones.RightMiddleDistal, "Right Middle Distal" },
            { VgoGltf.HumanBodyBones.RightRingProximal, "Right Ring Proximal" },
            { VgoGltf.HumanBodyBones.RightRingIntermediate, "Right Ring Intermediate" },
            { VgoGltf.HumanBodyBones.RightRingDistal, "Right Ring Distal" },
            { VgoGltf.HumanBodyBones.RightLittleProximal, "Right Little Proximal" },
            { VgoGltf.HumanBodyBones.RightLittleIntermediate, "Right Little Intermediate" },
            { VgoGltf.HumanBodyBones.RightLittleDistal, "Right Little Distal" },
            { VgoGltf.HumanBodyBones.UpperChest, "UpperChest" },
            { VgoGltf.HumanBodyBones.LastBone, "LastBone" },
        };
    }
}