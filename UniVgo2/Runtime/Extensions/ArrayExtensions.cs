// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : ArrayExtensions
// ----------------------------------------------------------------------
namespace UniVgo2
{
    /// <summary>
    /// Array Extensions
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Flip the triangle edge.
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        /// <remarks>0, 1, 2 to 2, 1, 0</remarks>
        public static int[] FlipTriangle(this int[] src)
        {
            int tmp;
            for (int i = 0; i < src.Length; i += 3)
            {
                tmp = src[i];
                src[i] = src[i + 2];
                src[i + 2] = tmp;
            }
            return src;
        }
    }
}
