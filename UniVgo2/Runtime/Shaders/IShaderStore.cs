// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : IShaderStore
// ----------------------------------------------------------------------
namespace UniVgo2
{
    using NewtonVgo;
    using UnityEngine;

    /// <summary>
    /// Shader Store Interface
    /// </summary>
    public interface IShaderStore
    {
        #region Methods

        /// <summary>
        /// Get a shader or default.
        /// </summary>
        /// <param name="vgoMaterial">The vgo material.</param>
        /// <returns></returns>
        Shader GetShaderOrDefault(VgoMaterial vgoMaterial);

        /// <summary>
        /// Get a shader or standard.
        /// </summary>
        /// <param name="vgoMaterial">The vgo material.</param>
        /// <returns></returns>
        Shader GetShaderOrStandard(VgoMaterial vgoMaterial);

        ///// <summary>
        ///// Try get a shader.
        ///// </summary>
        ///// <param name="materialInfo"></param>
        ///// <param name="shader"></param>
        ///// <returns></returns>
        //bool TryGetShader(MaterialInfo materialInfo, out Shader shader);

        #endregion
    }
}
