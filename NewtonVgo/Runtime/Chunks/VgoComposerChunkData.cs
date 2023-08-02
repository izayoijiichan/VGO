// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoComposerChunkData
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>The composer chunk data.</summary>
    /// <remarks>32-byte</remarks>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct VgoComposerChunkData
    {
        #region Fields

        /// <summary>Asset information chunk type ID.</summary>
        /// <remarks>4-byte</remarks>
        public VgoChunkTypeID AssetInfoChunkTypeId;

        /// <summary></summary>
        /// <remarks>4-byte</remarks>
        public VgoChunkTypeID AssetInfoCryptChunkTypeId;

        /// <summary>Layout chunk type ID.</summary>
        /// <remarks>4-byte</remarks>
        public VgoChunkTypeID LayoutChunkTypeId;

        /// <summary></summary>
        /// <remarks>4-byte</remarks>
        public VgoChunkTypeID LayoutCryptChunkTypeId;

        /// <summary>Resource accessor chunk type ID.</summary>
        /// <remarks>4-byte</remarks>
        public VgoChunkTypeID ResourceAccessorChunkTypeId;

        /// <summary>Resource accessor crypt chunk type ID.</summary>
        /// <remarks>4-byte</remarks>
        public VgoChunkTypeID ResourceAccessorCryptChunkTypeId;

        /// <summary>Resource chunk type ID.</summary>
        /// <remarks>4-byte</remarks>
        public VgoChunkTypeID ResourceChunkTypeId;

        /// <summary></summary>
        /// <remarks>4-byte</remarks>
        public VgoChunkTypeID ResourceCryptChunkTypeId;

        #endregion
    }
}
