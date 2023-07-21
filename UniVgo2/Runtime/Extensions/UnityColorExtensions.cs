// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : UnityColorExtensions
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    /// <summary>
    /// Unity Color Extensions
    /// </summary>
    public static class UnityColorExtensions
    {
        #region UnityEngine -> NewtonVgo

        /// <summary>
        /// Convert UnityEngine.Color to NewtonVgo.Color3.
        /// </summary>
        /// <param name="unityColor"></param>
        /// <returns></returns>
        public static NewtonVgo.Color3 ToVgoColor3(this UnityEngine.Color unityColor)
        {
            return new NewtonVgo.Color3(
                unityColor.r,
                unityColor.g,
                unityColor.b
            );
        }

        /// <summary>
        /// Convert UnityEngine.Color to NewtonVgo.Color4.
        /// </summary>
        /// <param name="unityColor"></param>
        /// <returns></returns>
        public static NewtonVgo.Color4 ToVgoColor4(this UnityEngine.Color unityColor)
        {
            return new NewtonVgo.Color4(
                unityColor.r,
                unityColor.g,
                unityColor.b,
                unityColor.a
            );
        }

        #endregion

        #region NewtonVgo -> UnityEngine

        /// <summary>
        /// Convert NewtonVgo.Color3 to UnityEngine.Color.
        /// </summary>
        /// <param name="vgoColor"></param>
        /// <param name="alpha"></param>
        /// <returns></returns>
        public static UnityEngine.Color ToUnityColor(this NewtonVgo.Color3 vgoColor, in float alpha = 1.0f)
        {
            return new UnityEngine.Color(
                vgoColor.R,
                vgoColor.G,
                vgoColor.B,
                alpha
            );
        }

        /// <summary>
        /// Convert NewtonVgo.Color4 to UnityEngine.Color.
        /// </summary>
        /// <param name="vgoColor"></param>
        /// <returns></returns>
        public static UnityEngine.Color ToUnityColor(this NewtonVgo.Color4 vgoColor)
        {
            return new UnityEngine.Color(
                vgoColor.R,
                vgoColor.G,
                vgoColor.B,
                vgoColor.A
            );
        }

        #endregion
    }
}
