// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : UnityTransformExtensions
// ----------------------------------------------------------------------
namespace UniVgo2
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    /// <summary>
    /// Unity Transform Extensions
    /// </summary>
    public static class UnityTransformExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static IEnumerable<Transform> Traverse(this Transform t)
        {
            yield return t;
            foreach (Transform x in t)
            {
                foreach (Transform y in x.Traverse())
                {
                    yield return y;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="root"></param>
        public static void ReverseZRecursive(this Transform root)
        {
            var globalMap = root.Traverse().ToDictionary(x => x, x => (x.position, x.rotation));

            foreach (var x in root.Traverse())
            {
                x.position = globalMap[x].position.ReverseZ();
                x.rotation = globalMap[x].rotation.ReverseZ();
            }
        }
    }
}
