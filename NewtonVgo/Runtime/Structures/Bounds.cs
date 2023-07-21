// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Struct    : Bounds
// ----------------------------------------------------------------------
#nullable enable
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
        #region Fields

        /// <summary>The center of the bounding box.</summary>
        public Vector3 center;

        /// <summary>The total size of the box.</summary>
        /// <remarks>This is always twice as large as the extents.</remarks>
        public Vector3 size;

        ///// <summary>The extents of the Bounding Box.</summary>
        ///// <remarks>This is always half of the size of the Bounds.</remarks>
        //public Vector3 extents;

        #endregion

        #region Constructors

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

        #endregion

        #region Public Methods

        /// <summary>
        /// Deconstruct Bounds to center and size;
        /// </summary>
        /// <param name="center"></param>
        /// <param name="size"></param>
        public readonly void Deconstruct(out Vector3 center, out Vector3 size)
        {
            center = this.center;
            size = this.size;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public readonly bool Equals(in Bounds other)
        {
            return
                center.Equals(other.center) &&
                size.Equals(other.size);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override readonly bool Equals(object? other)
        {
            if (other is Bounds otherBounds)
            {
                return Equals(otherBounds);
            }

            return false;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override readonly int GetHashCode()
        {
            return
                center.GetHashCode() ^
                (size.GetHashCode() << 2);
        }

        #endregion

        #region Operators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <returns></returns>
        public static bool operator ==(in Bounds b1, in Bounds b2)
        {
            return b1.Equals(b2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <returns></returns>
        public static bool operator !=(in Bounds b1, in Bounds b2)
        {
            return b1.Equals(b2) == false;
        }

        #endregion
    }
}
