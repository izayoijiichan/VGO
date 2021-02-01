// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Struct    : Bounds
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    using System;
    using System.Numerics;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Bounds
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Bounds
    {
        /// <summary>The center of the bounding box.</summary>
        public Vector3 center;

        /// <summary>The total size of the box.</summary>
        /// <remarks>This is always twice as large as the extents.</remarks>
        public Vector3 size;

        ///// <summary>The extents of the Bounding Box.</summary>
        ///// <remarks>This is always half of the size of the Bounds.</remarks>
        //public Vector3 extents;

        /// <summary>
        /// Creates a new Bounds with center and size.
        /// </summary>
        /// <param name="center">The location of the origin of the Bounds.</param>
        /// <param name="size">The dimensions of the Bounds.</param>
        public Bounds(Vector3 center, Vector3 size)
        {
            this.center = center;
            this.size = size;
        }
    }
}
