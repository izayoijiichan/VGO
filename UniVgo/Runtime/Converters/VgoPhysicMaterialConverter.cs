// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : VgoPhysicMaterialConverter
// ----------------------------------------------------------------------
namespace UniVgo
{
    using UniGLTFforUniVgo;
    using UnityEngine;

    /// <summary>
    /// VGO Physic Material Converter
    /// </summary>
    public class VgoPhysicMaterialConverter
    {
        /// <summary>
        /// Create glTF_VGO_PhysicMaterial from PhysicMaterial.
        /// </summary>
        /// <param name="physicMaterial"></param>
        /// <returns></returns>
        public static glTFNode_VGO_PhysicMaterial CreateFrom(PhysicMaterial physicMaterial)
        {
            if (physicMaterial == null)
            {
                return null;
            }

            return new glTFNode_VGO_PhysicMaterial
            {
                dynamicFriction = physicMaterial.dynamicFriction,
                staticFriction = physicMaterial.staticFriction,
                bounciness = physicMaterial.bounciness,
                frictionCombine = physicMaterial.frictionCombine,
                bounceCombine = physicMaterial.bounceCombine,
            };
        }

        /// <summary>
        /// Convert glTF_VGO_PhysicMaterial to PhysicMaterial.
        /// </summary>
        /// <param name="vgoPhysicMaterial"></param>
        /// <returns>PhysicMaterial</returns>
        public static PhysicMaterial ToPhysicMaterial(glTFNode_VGO_PhysicMaterial vgoPhysicMaterial)
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
                frictionCombine = vgoPhysicMaterial.frictionCombine,
                bounceCombine = vgoPhysicMaterial.bounceCombine,
            };
        }
    }
}