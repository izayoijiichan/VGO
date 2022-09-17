// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Enum      : VgoChunkTypeID
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    /// <summary>The chunk type ID.</summary>
    public enum VgoChunkTypeID : uint
    {
        /// <summary></summary>
        /// <remarks>"    "</remarks>
        None = 0x00000000,
        /// <summary>The header magic.</summary>
        /// <remarks>"VGO "</remarks>
        Vgo = 0x004F4756,
        /// <summary>The index chunk.</summary>
        /// <remarks>"IDX "</remarks>
        Idx = 0x00584449,
        /// <summary>The composer layout and resource.</summary>
        /// <remarks>"COMP"</remarks>
        COMP = 0x504D4F43,
        /// <summary>The asset info. This is a  plain JSON.</summary>
        /// <remarks>"AIPJ"</remarks>
        AIPJ = 0x4A504941,
        /// <summary>The asset info. This is a  plain BSON.</summary>
        /// <remarks>"AIPB"</remarks>
        AIPB = 0x42504941,
        /// <summary>The layout. This is a  plain JSON.</summary>
        /// <remarks>"LAPJ"</remarks>
        LAPJ = 0x4A50414C,
        /// <summary>The layout. This is a  plain BSON.</summary>
        /// <remarks>"LAPB"</remarks>
        LAPB = 0x4250414C,
        /// <summary>The resource accessor. This is a plain JSON.</summary>
        /// <remarks>"RAPJ"</remarks>
        RAPJ = 0x4A504152,
        /// <summary>The resource accessor. This is a plain BSON.</summary>
        /// <remarks>"RAPB"</remarks>
        RAPB = 0x42504152,
        /// <summary>The resource accessor. This is a crypted JSON.</summary>
        /// <remarks>"RACJ"</remarks>
        RACJ = 0x4A434152,
        /// <summary>The resource accessor. This is a crypted BSON.</summary>
        /// <remarks>"RACB"</remarks>
        RACB = 0x42434152,
        /// <summary>The crypt info of the resource accessor. This is a JSON.</summary>
        /// <remarks>"CRAJ"</remarks>
        CRAJ = 0x4A415243,
        /// <summary>The crypt info of the resource accessor. This is a BSON.</summary>
        /// <remarks>"CRAB"</remarks>
        CRAB = 0x42415243,
        /// <summary>The resource. This is a plain binary.</summary>
        /// <remarks>"REPb"</remarks>
        REPb = 0x62504552,
        /// <summary>The resource. This is a plain JSON.</summary>
        /// <remarks>"REPJ"</remarks>
        REPJ = 0x4A504552,
        /// <summary>The resource. This is a plain BSON.</summary>
        /// <remarks>"REPB"</remarks>
        REPB = 0x42504552,
    }
}
