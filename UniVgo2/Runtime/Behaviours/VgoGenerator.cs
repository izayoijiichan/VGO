// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : VgoGenerator
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using NewtonVgo;
    using UnityEngine;

    /// <summary>
    /// VGO Generator
    /// </summary>
    [AddComponentMenu("Vgo/Vgo Generator")]
    [DisallowMultipleComponent]
    //[RequireComponent(typeof(VgoRight))]
    public class VgoGenerator : MonoBehaviour
    {
        /// <summary></summary>
        public VgoGeneratorInfo? GeneratorInfo = null;
    }
}