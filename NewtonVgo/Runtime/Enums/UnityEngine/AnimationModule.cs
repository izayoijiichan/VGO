// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : 
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    /// <summary>Culling mode for the Animator.</summary>
    public enum AnimatorCullingMode
    {
        /// <summary>Always animate the entire character. Object is animated even when offscreen.</summary>
        AlwaysAnimate = 0,
        /// <summary>Retarget, IK and write of Transforms are disabled when renderers are not visible.</summary>
        CullUpdateTransforms = 1,
        ///// <summary></summary>
        //BasedOnRenderers = 1,
        /// <summary>Animation is completely disabled when renderers are not visible.</summary>
        CullCompletely = 2
    }

    /// <summary>The update mode of the Animator.</summary>
    public enum AnimatorUpdateMode
    {
        /// <summary>Normal update of the animator.</summary>
        Normal = 0,
        /// <summary>Updates the animator during the physic loop in order to have the animation system synchronized with the physics engine.</summary>
        AnimatePhysics = 1,
        /// <summary>Animator updates independently of Time.timeScale.</summary>
        UnscaledTime = 2
    }

    /// <summary>Human Body Bones.</summary>
    public enum HumanBodyBones
    {
        /// <summary>This is the Hips bone.</summary>
        Hips = 0,
        /// <summary>This is the Left Upper Leg bone.</summary>
        LeftUpperLeg = 1,
        /// <summary>This is the Right Upper Leg bone.</summary>
        RightUpperLeg = 2,
        /// <summary>This is the Left Knee bone.</summary>
        LeftLowerLeg = 3,
        /// <summary>This is the Right Knee bone.</summary>
        RightLowerLeg = 4,
        /// <summary>This is the Left Ankle bone.</summary>
        LeftFoot = 5,
        /// <summary>This is the Right Ankle bone.</summary>
        RightFoot = 6,
        /// <summary>This is the first Spine bone.</summary>
        Spine = 7,
        /// <summary>This is the Chest bone.</summary>
        Chest = 8,
        /// <summary>This is the Neck bone.</summary>
        Neck = 9,
        /// <summary>This is the Head bone.</summary>
        Head = 10,
        /// <summary>This is the Left Shoulder bone.</summary>
        LeftShoulder = 11,
        /// <summary>This is the Right Shoulder bone.</summary>
        RightShoulder = 12,
        /// <summary>This is the Left Upper Arm bone.</summary>
        LeftUpperArm = 13,
        /// <summary>This is the Right Upper Arm bone.</summary>
        RightUpperArm = 14,
        /// <summary>This is the Left Elbow bone.</summary>
        LeftLowerArm = 15,
        /// <summary>This is the Right Elbow bone.</summary>
        RightLowerArm = 16,
        /// <summary>This is the Left Wrist bone.</summary>
        LeftHand = 17,
        /// <summary>This is the Right Wrist bone.</summary>
        RightHand = 18,
        /// <summary>This is the Left Toes bone.</summary>
        LeftToes = 19,
        /// <summary>This is the Right Toes bone.</summary>
        RightToes = 20,
        /// <summary>This is the Left Eye bone.</summary>
        LeftEye = 21,
        /// <summary>This is the Right Eye bone.</summary>
        RightEye = 22,
        /// <summary>This is the Jaw bone.</summary>
        Jaw = 23,
        /// <summary>This is the left thumb 1st phalange.</summary>
        LeftThumbProximal = 24,
        /// <summary>This is the left thumb 2nd phalange.</summary>
        LeftThumbIntermediate = 25,
        /// <summary>This is the left thumb 3rd phalange.</summary>
        LeftThumbDistal = 26,
        /// <summary>This is the left index 1st phalange.</summary>
        LeftIndexProximal = 27,
        /// <summary>This is the left index 2nd phalange.</summary>
        LeftIndexIntermediate = 28,
        /// <summary>This is the left index 3rd phalange.</summary>
        LeftIndexDistal = 29,
        /// <summary>This is the left middle 1st phalange.</summary>
        LeftMiddleProximal = 30,
        /// <summary>This is the left middle 2nd phalange.</summary>
        LeftMiddleIntermediate = 31,
        /// <summary>This is the left middle 3rd phalange.</summary>
        LeftMiddleDistal = 32,
        /// <summary>This is the left ring 1st phalange.</summary>
        LeftRingProximal = 33,
        /// <summary>This is the left ring 2nd phalange.</summary>
        LeftRingIntermediate = 34,
        /// <summary>This is the left ring 3rd phalange.</summary>
        LeftRingDistal = 35,
        /// <summary>This is the left little 1st phalange.</summary>
        LeftLittleProximal = 36,
        /// <summary>This is the left little 2nd phalange.</summary>
        LeftLittleIntermediate = 37,
        /// <summary>This is the left little 3rd phalange.</summary>
        LeftLittleDistal = 38,
        /// <summary>This is the right thumb 1st phalange.</summary>
        RightThumbProximal = 39,
        /// <summary>This is the right thumb 2nd phalange.</summary>
        RightThumbIntermediate = 40,
        /// <summary>This is the right thumb 3rd phalange.</summary>
        RightThumbDistal = 41,
        /// <summary>This is the right index 1st phalange.</summary>
        RightIndexProximal = 42,
        /// <summary>This is the right index 2nd phalange.</summary>
        RightIndexIntermediate = 43,
        /// <summary>This is the right index 3rd phalange.</summary>
        RightIndexDistal = 44,
        /// <summary>This is the right middle 1st phalange.</summary>
        RightMiddleProximal = 45,
        /// <summary>This is the right middle 2nd phalange.</summary>
        RightMiddleIntermediate = 46,
        /// <summary>This is the right middle 3rd phalange.</summary>
        RightMiddleDistal = 47,
        /// <summary>This is the right ring 1st phalange.</summary>
        RightRingProximal = 48,
        /// <summary>This is the right ring 2nd phalange.</summary>
        RightRingIntermediate = 49,
        /// <summary>This is the right ring 3rd phalange.</summary>
        RightRingDistal = 50,
        /// <summary>This is the right little 1st phalange.</summary>
        RightLittleProximal = 51,
        /// <summary>This is the right little 2nd phalange.</summary>
        RightLittleIntermediate = 52,
        /// <summary>This is the right little 3rd phalange.</summary>
        RightLittleDistal = 53,
        /// <summary>This is the Upper Chest bone.</summary>
        UpperChest = 54,
        /// <summary>This is the Last bone index delimiter.</summary>
        LastBone = 55,
    }
}
