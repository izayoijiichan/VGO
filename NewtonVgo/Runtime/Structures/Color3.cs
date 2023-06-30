// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Struct    : Color3
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Color3
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Color3
    {
        #region Fields

        /// <summary>Red</summary>
        public float R;

        /// <summary>Green</summary>
        public float G;

        /// <summary>Blue</summary>
        public float B;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of Color3 with r, g, b.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public Color3(float r, float g, float b)
        {
            R = r;
            G = g;
            B = b;
        }

        /// <summary>
        /// Create a new instance of Color3 with color4.
        /// </summary>
        /// <param name="color4"></param>
        public Color3(Color4 color4)
        {
            R = color4.R;
            G = color4.G;
            B = color4.B;
        }

        #endregion

        #region Propetties

        /// <summary>Black</summary>
        public static Color3 Black => new Color3(0.0f, 0.0f, 0.0f);

        /// <summary>White</summary>
        public static Color3 White => new Color3(1.0f, 1.0f, 1.0f);

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
                    default:
                        throw new IndexOutOfRangeException($"index: {index} is out of range.");
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
                    default:
                        throw new IndexOutOfRangeException($"index: {index} is out of range.");
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Deconstruct Color4 to r, g, and b;
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public void Deconstruct(out float r, out float g, out float b)
        {
            r = R;
            g = G;
            b = B;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public bool Equals(Color3 other)
        {
            return
                R.Equals(other.R) &&
                G.Equals(other.G) &&
                B.Equals(other.B);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object? other)
        {
            if (other is Color3 otherColor3)
            {
                return Equals(otherColor3);
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
                (B.GetHashCode() >> 2);
        }

        /// <summary>
        /// Set R, G and B components of an existing Color3.
        /// </summary>
        /// <param name="newR"></param>
        /// <param name="newG"></param>
        /// <param name="newB"></param>
        public void Set(float newR, float newG, float newB)
        {
            R = newR;
            G = newG;
            B = newB;
        }

        /// <summary>
        /// Convert Color3 to Color4.
        /// </summary>
        /// <returns></returns>
        public Color4 ToColor4(float a = 1.0f)
        {
            return new Color4(this, a);
        }

        #endregion

        #region Operators

        public static bool operator ==(Color3 c1, Color3 c2)
        {
            return c1.Equals(c2);
        }

        public static bool operator !=(Color3 c1, Color3 c2)
        {
            return c1.Equals(c2) == false;
        }

        #endregion
    }
}
