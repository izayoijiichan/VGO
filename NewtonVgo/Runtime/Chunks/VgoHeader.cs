// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoHeader
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>Header</summary>
    /// <remarks>16-byte</remarks>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct VgoHeader
    {
        #region Fields

        /// <summary>File magic.</summary>
        public uint Magic;

        /// <summary>Length of chunk data.</summary>
        public uint DataLength;

        /// <summary>Major version of VGO.</summary>
        public byte MajorVersion;

        /// <summary>Minor version of VGO.</summary>
        public byte MinorVersion;

        /// <summary>Geometry coordinates.</summary>
        /// <remarks>1-byte</remarks>
        public VgoGeometryCoordinate GeometryCoordinate;

        /// <summary>UV coordinates.</summary>
        /// <remarks>1-byte</remarks>
        public VgoUVCoordinate UVCoordinate;

        /// <summary>Whether the resource accessor is crypted.</summary>
        /// <remarks>1-byte (not bool)</remarks>
        public byte ResourceAccessorIsCrypted;

        /// <summary>Whether the resource accessor external crypt key is required.</summary>
        /// <remarks>1-byte (not bool)</remarks>
        public byte IsRequireResourceAccessorExternalCryptKey;

        /// <summary></summary>
        public byte Reserved1;

        /// <summary></summary>
        public byte Reserved2;

        #endregion
    }
}
