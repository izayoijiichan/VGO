// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Struct    : Color4
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Color4
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Color4
    {
        #region Fields
        
        /// <summary>Red</summary>
        public float R;

        /// <summary>Green</summary>
        public float G;

        /// <summary>Blue</summary>
        public float B;

        /// <summary>Alpha</summary>
        public float A;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of Color4 with r, g, b.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public Color4(float r, float g, float b) : this(r, g, b, 1.0f) { }

        /// <summary>
        /// Create a new instance of Color4 with r, g, b, a.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        public Color4(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        /// <summary>
        /// Create a new instance of Color4 with color3 and a.
        /// </summary>
        /// <param name="color3"></param>
        /// <param name="a"></param>
        public Color4(Color3 color3, float a = 1.0f)
        {
            R = color3.R;
            G = color3.G;
            B = color3.B;
            A = a;
        }

        #endregion

        #region Propetties

        /// <summary>Black</summary>
        public static Color4 Black => new Color4(0.0f, 0.0f, 0.0f, 1.0f);

        /// <summary>White</summary>
        public static Color4 White => new Color4(1.0f, 1.0f, 1.0f, 1.0f);

        #endregion

        #region Indexers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return R;
                    case 1:
                        return G;
                    case 2:
                        return B;
                    case 3:
                        return A;
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
                        R = value;
                        break;
                    case 1:
                        G = value;
                        break;
                    case 2:
                        B = value;
                        break;
                    case 3:
                        A = value;
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
        /// Deconstruct Color4 to r, g, b and a;
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        public void Deconstruct(out float r, out float g, out float b, out float a)
        {
            r = R;
            g = G;
            b = B;
            a = A;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public bool Equals(Color4 other)
        {
            return
                R.Equals(other.R) &&
                G.Equals(other.G) &&
                B.Equals(other.B) &&
                A.Equals(other.A);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object? other)
        {
            if (other is Color4 otherColor4)
            {
                return Equals(otherColor4);
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
                R.GetHashCode() ^
                (G.GetHashCode() << 2) ^
                (B.GetHashCode() >> 2) ^
                (A.GetHashCode() >> 1);
        }

        /// <summary>
        /// Set R, G, B and A components of an existing Color4.
        /// </summary>
        /// <param name="newR"></param>
        /// <param name="newG"></param>
        /// <param name="newB"></param>
        /// <param name="newA"></param>
        public void Set(float newR, float newG, float newB, float newA)
        {
            R = newR;
            G = newG;
            B = newB;
            R = newA;
        }

        /// <summary>
        /// Convert Color4 to Color3.
        /// </summary>
        /// <returns></returns>
        public Color3 ToColor3()
        {
            return new Color3(this);
        }

        #endregion

        #region Operators

        public static bool operator ==(Color4 c1, Color4 c2)
        {
            return c1.Equals(c2);
        }

        public static bool operator !=(Color4 c1, Color4 c2)
        {
            return c1.Equals(c2) == false;
        }

        #endregion
    }
}
