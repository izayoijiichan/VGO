// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Converters
// @Class     : VgoBoundsConverter
// ----------------------------------------------------------------------
namespace UniVgo2.Converters
{
    using NewtonVgo;

    /// <summary>
    /// VGO Bounds Converter
    /// </summary>
    public class VgoBoundsConverter
    {
        /// <summary>
        /// Create VgoBounds from Bounds.
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="geometryCoordinate"></param>
        /// <returns></returns>
        public static NewtonVgo.Bounds? CreateFrom(UnityEngine.Bounds bounds, VgoGeometryCoordinate geometryCoordinate)
        {
            if (bounds == default)
            {
                return null;
            }

            return new NewtonVgo.Bounds(
                bounds.center.ToNumericsVector3(geometryCoordinate),
                bounds.size.ToNumericsVector3()
            );
        }

        /// <summary>
        /// Create Bounds from VgoBounds.
        /// </summary>
        /// <param name="vgoBounds"></param>
        /// <param name="geometryCoordinate"></param>
        public static UnityEngine.Bounds CreateBounds(NewtonVgo.Bounds? vgoBounds, VgoGeometryCoordinate geometryCoordinate)
        {
            if (vgoBounds == null)
            {
                return default;
            }

            return new UnityEngine.Bounds(
                vgoBounds.Value.center.ToUnityVector3(geometryCoordinate),
                vgoBounds.Value.size.ToUnityVector3()
            );
        }
    }
}