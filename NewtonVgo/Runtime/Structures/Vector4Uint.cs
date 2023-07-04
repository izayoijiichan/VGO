// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Struct    : Vector4Uint
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Vector4 (uint)
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vector4Uint
    {
        #region Fields
        
        /// <summary></summary>
        public uint X;

        /// <summary></summary>
        public uint Y;

        /// <summary></summary>
        public uint Z;

        /// <summary></summary>
        public uint W;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of Vector4Uint with x and y and z and w.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        public Vector4Uint(uint x, uint y, uint z, uint w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Create a new instance of Vector4Uint with Vector4Ubyte.
        /// </summary>
        /// <param name="source"></param>
        public Vector4Uint(Vector4Ubyte source)
        {
            X = source.X;
            Y = source.Y;
            Z = source.Z;
            W = source.W;
        }

        /// <summary>
        /// Create a new instance of Vector4Uint with Vector4Ushort.
        /// </summary>
        /// <param name="source"></param>
        public Vector4Uint(Vector4Ushort source)
        {
            X = source.X;
            Y = source.Y;
            Z = source.Z;
            W = source.W;
        }

        #endregion

        #region Indexers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public uint this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return X;
                    case 1:
                        return Y;
                    case 2:
                        return Z;
                    case 3:
                        return W;
                    default:
                        ThrowHelper.ThrowIndexOutOfRangeException(nameof(index), index, min: 0, max: 4);
                        return default;
                }
            }

            set
            {
                switch (index)
                {
                    case 0:
                        X = value;
                        break;
                    case 1:
                        Y = value;
                        break;
                    case 2:
                        Z = value;
                        break;
                    case 3:
                        W = value;
                        break;
                    default:
                        ThrowHelper.ThrowIndexOutOfRangeException(nameof(index), index, min: 0, max: 4);
                        break;
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Deconstruct Vector4Uint to x, y, z and w;
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        public void Deconstruct(out uint x, out uint y, out uint z, out uint w)
        {
            x = X;
            y = Y;
            z = Z;
            w = W;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public bool Equals(Vector4Uint other)
        {
            return
                X.Equals(other.X) &&
                Y.Equals(other.Y) &&
                Z.Equals(other.Z) &&
                W.Equals(other.W);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object? other)
        {
            if (other is Vector4Uint otherVector4)
            {
                return Equals(otherVector4);
            }

            return false;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return
                X.GetHashCode() ^
                (Y.GetHashCode() << 2) ^
                (Z.GetHashCode() >> 2) ^
                (W.GetHashCode() >> 1);
        }

        /// <summary>
        /// Set X, Y, Z and W components of an existing Vector4Uint.
        /// </summary>
        /// <param name="newX"></param>
        /// <param name="newY"></param>
        /// <param name="newZ"></param>
        /// <param name="newW"></param>
        public void Set(uint newX, uint newY, uint newZ, uint newW)
        {
            X = newX;
            Y = newY;
            Z = newZ;
            W = newW;
        }

        /// <summary>
        /// Try convert Vector4Uint to Vector4Ubyte.
        /// </summary>
        /// <returns>true if the operation is successful; otherwise, false.</returns>
        public bool TryConvertTo(out Vector4Ubyte destination)
        {
            if ((X <= byte.MaxValue) &&
                (Y <= byte.MaxValue) &&
                (Z <= byte.MaxValue) &&
                (W <= byte.MaxValue))
            {
                destination = new Vector4Ubyte(
                    (byte)X,
                    (byte)Y,
                    (byte)Z,
                    (byte)W
                );

                return true;
            }

            destination = default;

            return false;
        }

        /// <summary>
        /// Try convert Vector4Uint to Vector4Ushort.
        /// </summary>
        /// <returns>true if the operation is successful; otherwise, false.</returns>
        public bool TryConvertTo(out Vector4Ushort destination)
        {
            if ((X <= ushort.MaxValue) &&
                (Y <= ushort.MaxValue) &&
                (Z <= ushort.MaxValue) &&
                (W <= ushort.MaxValue))
            {
                destination = new Vector4Ushort(
                    (ushort)X,
                    (ushort)Y,
                    (ushort)Z,
                    (ushort)W
                );

                return true;
            }

            destination = default;

            return false;
        }

        #endregion

        #region Operators

        public static bool operator ==(Vector4Uint v1, Vector4Uint v2)
        {
            return v1.Equals(v2);
        }

        public static bool operator !=(Vector4Uint v1, Vector4Uint v2)
        {
            return v1.Equals(v2) == false;
        }

        #endregion
    }
}
