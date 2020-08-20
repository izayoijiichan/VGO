// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : VgoGenerator
// ----------------------------------------------------------------------
namespace UniVgo2
{
    using NewtonVgo;
    using UnityEngine;

    /// <summary>
    /// VGO Generator
    /// </summary>
    [DisallowMultipleComponent]
    //[RequireComponent(typeof(VgoRight))]
    public class VgoGenerator : MonoBehaviour
    {
        /// <summary></summary>
        public VgoGeneratorInfo GeneratorInfo = null;
    }
}