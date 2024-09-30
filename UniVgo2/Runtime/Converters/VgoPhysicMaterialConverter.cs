// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Converters
// @Class     : VgoPhysicMaterialConverter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Converters
{
    using NewtonVgo;
    using UnityEngine;

    /// <summary>
    /// VGO Physic Material Converter
    /// </summary>
    public class VgoPhysicMaterialConverter
    {
        /// <summary>
        /// Create VgoPhysicMaterial from PhysicMaterial.
        /// </summary>
        /// <param name="physicMaterial"></param>
        /// <returns></returns>
#if UNITY_6000_0_OR_NEWER
        public static VgoPhysicMaterial CreateFrom(in PhysicsMaterial physicMaterial)
#else
        public static VgoPhysicMaterial CreateFrom(in PhysicMaterial physicMaterial)
#endif
        {
            return new VgoPhysicMaterial
            {
                dynamicFriction = physicMaterial.dynamicFriction,
                staticFriction = physicMaterial.staticFriction,
                bounciness = physicMaterial.bounciness,
                frictionCombine = (NewtonVgo.PhysicMaterialCombine)physicMaterial.frictionCombine,
                bounceCombine = (NewtonVgo.PhysicMaterialCombine)physicMaterial.bounceCombine,
            };
        }

        /// <summary>
        /// Create VgoPhysicMaterial from PhysicMaterial.
        /// </summary>
        /// <param name="physicMaterial"></param>
        /// <returns></returns>
#if UNITY_6000_0_OR_NEWER
        public static VgoPhysicMaterial? CreateOrDefaultFrom(in PhysicsMaterial? physicMaterial)
#else
        public static VgoPhysicMaterial? CreateOrDefaultFrom(in PhysicMaterial? physicMaterial)
#endif
        {
            if (physicMaterial == null)
            {
                return default;
            }

            return CreateFrom(physicMaterial);
        }

        /// <summary>
        /// Convert VgoPhysicMaterial to PhysicMaterial.
        /// </summary>
        /// <param name="vgoPhysicMaterial"></param>
        /// <returns>PhysicMaterial</returns>
#if UNITY_6000_0_OR_NEWER
        public static PhysicsMaterial ToPhysicMaterial(in VgoPhysicMaterial vgoPhysicMaterial)
#else
        public static PhysicMaterial ToPhysicMaterial(in VgoPhysicMaterial vgoPhysicMaterial)
#endif
        {
#if UNITY_6000_0_OR_NEWER
            return new PhysicsMaterial()
#else
            return new PhysicMaterial()
#endif
            {
                dynamicFriction = vgoPhysicMaterial.dynamicFriction,
                staticFriction = vgoPhysicMaterial.staticFriction,
                bounciness = vgoPhysicMaterial.bounciness,
#if UNITY_6000_0_OR_NEWER
                frictionCombine = (PhysicsMaterialCombine)vgoPhysicMaterial.frictionCombine,
                bounceCombine = (PhysicsMaterialCombine)vgoPhysicMaterial.bounceCombine,
#else
                frictionCombine = (UnityEngine.PhysicMaterialCombine)vgoPhysicMaterial.frictionCombine,
                bounceCombine = (UnityEngine.PhysicMaterialCombine)vgoPhysicMaterial.bounceCombine,
#endif
            };
        }
    }
}