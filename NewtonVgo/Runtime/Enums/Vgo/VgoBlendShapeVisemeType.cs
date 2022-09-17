// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Enum      : VgoBlendShapeVisemeType
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    /// <summary>The type of Viseme.</summary>
    public enum VgoBlendShapeVisemeType
    {
        /// <summary></summary>
        None = -1,
        /// <summary></summary>
        Silence = 0,
        /// <summary></summary>
        /// <remarks>p、b、m</remarks>
        PP = 1,
        /// <summary></summary>
        /// <remarks>f、v</remarks>
        FF = 2,
        /// <summary></summary>
        /// <remarks>th</remarks>
        TH = 3,
        /// <summary></summary>
        /// <remarks>t、d</remarks>
        DD = 4,
        /// <summary></summary>
        /// <remarks>k、g</remarks>
        kk = 5,
        /// <summary></summary>
        /// <remarks>tS、dZ、S</remarks>
        CH = 6,
        /// <summary></summary>
        /// <remarks>s、z</remarks>
        SS = 7,
        /// <summary></summary>
        /// <remarks>n、l</remarks>
        nn = 8,
        /// <summary></summary>
        /// <remarks>r</remarks>
        RR = 9,
        /// <summary></summary>
        /// <remarks>A:</remarks>
        A = 10,
        /// <summary></summary>
        /// <remarks>e</remarks>
        E = 11,
        /// <summary></summary>
        /// <remarks>ih</remarks>
        I = 12,
        /// <summary></summary>
        /// <remarks>oh</remarks>
        O = 13,
        /// <summary></summary>
        /// <remarks>ou</remarks>
        U = 14,
    }
}
