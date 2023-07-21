// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Converters
// @Struct    : ColorSpaceScope
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Converters
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Color Space Scope
    /// </summary>
    public readonly struct ColorSpaceScope : IDisposable
    {
        /// <summary>Controls whether Linear-to-sRGB color conversion is performed while rendering.</summary>
        private readonly bool sRGBWrite;

        /// <summary>
        /// Create a new instance of ColorSpaceScope with sRGBWrite.
        /// </summary>
        /// <param name="sRGBWrite"></param>
        public ColorSpaceScope(bool sRGBWrite)
        {
            this.sRGBWrite = GL.sRGBWrite;
            GL.sRGBWrite = sRGBWrite;
        }

        /// <summary>
        /// Create a new instance of ColorSpaceScope with colorSpace.
        /// </summary>
        /// <param name="colorSpace"></param>
        public ColorSpaceScope(in RenderTextureReadWrite colorSpace)
        {
            sRGBWrite = GL.sRGBWrite;
            GL.sRGBWrite = colorSpace != RenderTextureReadWrite.Linear;
        }

        /// <summary>
        /// Restore Linear-to-sRGB color conversion state.
        /// </summary>
        public readonly void Dispose()
        {
            GL.sRGBWrite = sRGBWrite;
        }
    }
}
