// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Struct    : Vector4Byte
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Vector4 (byte)
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vector4Ubyte
    {
        #region Fields
        
        /// <summary></summary>
        public byte X;

        /// <summary></summary>
        public byte Y;

        /// <summary></summary>
        public byte Z;

        /// <summary></summary>
        public byte W;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of Vector4Ubyte with x and y and z and w.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        public Vector4Ubyte(byte x, byte y, byte z, byte w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        #endregion

        #region Indexers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public byte this[int index]
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
                        throw new IndexOutOfRangeException($"index: {index} is out of range.");
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
                        throw new IndexOutOfRangeException($"index: {index} is out of range.");
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Deconstruct Vector4Ubyte to x, y, z and w;
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        public void Deconstruct(out byte x, out byte y, out byte z, out byte w)
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
        public bool Equals(Vector4Ubyte other)
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
            if (other is Vector4Ubyte otherVector4)
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
        /// Set X, Y, Z and W components of an existing Vector4Ubyte.
        /// </summary>
        /// <param name="newX"></param>
        /// <param name="newY"></param>
        /// <param name="newZ"></param>
        /// <param name="newW"></param>
        public void Set(byte newX, byte newY, byte newZ, byte newW)
        {
            X = newX;
            Y = newY;
            Z = newZ;
            W = newW;
        }

        /// <summary>
        /// Convert Vector4Ubyte to Vector4Uint.
        /// </summary>
        /// <returns></returns>
        public Vector4Uint ToVector4Uint()
        {
            return new Vector4Uint(this);
        }

        /// <summary>
        /// Convert Vector4Ubyte to Vector4Ushort.
        /// </summary>
        /// <returns></returns>
        public Vector4Ushort ToVector4Ushort()
        {
            return new Vector4Ushort(this);
        }

        #endregion

        #region Operators

        public static bool operator ==(Vector4Ubyte v1, Vector4Ubyte v2)
        {
            return v1.Equals(v2);
        }

        public static bool operator !=(Vector4Ubyte v1, Vector4Ubyte v2)
        {
            return v1.Equals(v2) == false;
        }

        #endregion
    }
}
