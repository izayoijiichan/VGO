// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : UnityColorExtensions
// ----------------------------------------------------------------------
namespace UniVgo2
{
    /// <summary>
    /// Unity Color Extensions
    /// </summary>
    public static class UnityColorExtensions
    {
        #region UnityEngine -> NewtonVgo

        public static NewtonVgo.Color3 ToVgoColor3(this UnityEngine.Color unityColor)
        {
            return new NewtonVgo.Color3(
                unityColor.r,
                unityColor.g,
                unityColor.b
            );
        }

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

        public static UnityEngine.Color ToUnityColor(this NewtonVgo.Color3 vgoColor, float alpha = 1.0f)
        {
            return new UnityEngine.Color(
                vgoColor.R,
                vgoColor.G,
                vgoColor.B,
                alpha
            );
        }

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
