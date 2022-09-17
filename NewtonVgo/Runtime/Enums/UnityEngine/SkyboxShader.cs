// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : 
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    /// <summary>Image Type</summary>
    public enum SkyboxImageType
    {
        /// <summary>360 Degrees</summary>
        Degrees360 = 0,
        /// <summary>180 Degrees</summary>
        Degrees180 = 1,
    }

    /// <summary>3D Layout</summary>
    public enum SkyboxLayout
    {
        /// <summary>None</summary>
        None = 0,
        /// <summary>Side by Side</summary>
        SideBySide = 1,
        /// <summary>Over Under</summary>
        OverUnder = 2,
    }

    /// <summary>Mapping</summary>
    public enum SkyboxMapping
    {
        /// <summary>6 Frames Layout</summary>
        SixFramesLayout = 0,
        /// <summary>Latitude Longitude Layout</summary>
        LatitudeLongitudeLayout = 1,
    }

    /// <summary>Sun</summary>
    public enum SkyboxSunDisk
    {
        /// <summary>None</summary>
        None = 0,
        /// <summary>Simple</summary>
        Simple = 1,
        /// <summary>High Quality</summary>
        HighQuality = 2,
    }
}
