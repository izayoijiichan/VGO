// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : 
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    /// <summary>Filtering mode for textures. Corresponds to the settings in a.</summary>
    public enum FilterMode
    {
        /// <summary>Point filtering - texture pixels become blocky up close.</summary>
        Point = 0,
        /// <summary>Bilinear filtering - texture samples are averaged.</summary>
        Bilinear = 1,
        /// <summary>Trilinear filtering - texture samples are averaged and also blended between mipmap levels.</summary>
        Trilinear = 2,
    }

    /// <summary>Select how gradients will be evaluated.</summary>
    public enum GradientMode
    {
        /// <summary>Find the 2 keys adjacent to the requested evaluation time, and linearly interpolate between them to obtain a blended color.</summary>
        Blend = 0,
        /// <summary>Return a fixed color, by finding the first key whose time value is greater than the requested evaluation time.</summary>
        Fixed = 1,
    }

    /// <summary>The type of motion vectors that should be generated.</summary>
    public enum MotionVectorGenerationMode
    {
        /// <summary>Use only camera movement to track motion.</summary>
        Camera = 0,
        /// <summary>Use a specific pass (if required) to track motion.</summary>
        Object = 1,
        /// <summary>Do not track motion. Motion vectors will be 0.</summary>
        ForceNoMotion = 2,
    }

    /// <summary>This enum controls the mode under which the sprite will interact with the masking system.</summary>
    public enum SpriteMaskInteraction
    {
        /// <summary>The sprite will not interact with the masking system.</summary>
        None = 0,
        /// <summary>The sprite will be visible only in areas where a mask is present.</summary>
        VisibleInsideMask = 1,
        /// <summary>The sprite will be visible only in areas where no mask is present.</summary>
        VisibleOutsideMask = 2,
    }

    /// <summary>Wrap mode for textures.</summary>
    public enum TextureWrapMode
    {
        /// <summary>Tiles the texture, creating a repeating pattern.</summary>
        Repeat = 0,
        /// <summary>Clamps the texture to the last pixel at the edge.</summary>
        Clamp = 1,
        /// <summary>Tiles the texture, creating a repeating pattern by mirroring it at every integer boundary.</summary>
        Mirror = 2,
        /// <summary>Mirrors the texture once, then clamps to edge pixels.</summary>
        MirrorOnce = 3
    }

    /// <summary>Determines how time is treated outside of the keyframed range of an AnimationClip or AnimationCurve.</summary>
    public enum WrapMode
    {
        /// <summary> Reads the default repeat mode set higher up.</summary>
        Default = 0,
        /// <summary>When time reaches the end of the animation clip, the clip will automatically stop playing and time will be reset to beginning of the clip.</summary>
        Once = 1,
        //Clamp = 1,
        /// <summary>When time reaches the end of the animation clip, time will continue at the beginning.</summary>
        Loop = 2,
        /// <summary>When time reaches the end of the animation clip, time will ping pong back between beginning and end.</summary>
        PingPong = 4,
        /// <summary>Plays back the animation. When it reaches the end, it will keep playing the last frame and never stop playing.</summary>
        ClampForever = 8,
    }

    /// <summary>Sets which weights to use when calculating curve segments.</summary>
    public enum WeightedMode
    {
        /// <summary>Exclude both inWeight or outWeight when calculating curve segments.</summary>
        None = 0,
        /// <summary>Include inWeight when calculating the previous curve segment.</summary>
        In = 1,
        /// <summary>Include outWeight when calculating the next curve segment.</summary>
        Out = 2,
        /// <summary>Include inWeight and outWeight when calculating curve segments.</summary>
        Both = 3,
    }
}
