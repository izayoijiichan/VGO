// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Converters
// @Class     : VgoPhysicMaterialConverter
// ----------------------------------------------------------------------
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
        public static VgoPhysicMaterial CreateFrom(PhysicMaterial physicMaterial)
        {
            if (physicMaterial == null)
            {
                return null;
            }

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
        /// Convert VgoPhysicMaterial to PhysicMaterial.
        /// </summary>
        /// <param name="vgoPhysicMaterial"></param>
        /// <returns>PhysicMaterial</returns>
        public static PhysicMaterial ToPhysicMaterial(VgoPhysicMaterial vgoPhysicMaterial)
        {
            if (vgoPhysicMaterial == null)
            {
                return default;
            }

            return new PhysicMaterial()
            {
                dynamicFriction = vgoPhysicMaterial.dynamicFriction,
                staticFriction = vgoPhysicMaterial.staticFriction,
                bounciness = vgoPhysicMaterial.bounciness,
                frictionCombine = (UnityEngine.PhysicMaterialCombine)vgoPhysicMaterial.frictionCombine,
                bounceCombine = (UnityEngine.PhysicMaterialCombine)vgoPhysicMaterial.bounceCombine,
            };
        }
    }
}