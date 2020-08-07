// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : IShaderStore
// ----------------------------------------------------------------------
namespace UniVgo
{
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
        /// <param name="materialInfo"></param>
        /// <returns></returns>
        Shader GetShaderOrDefault(MaterialInfo materialInfo);

        /// <summary>
        /// Get a shader or standard.
        /// </summary>
        /// <param name="materialInfo"></param>
        /// <returns></returns>
        Shader GetShaderOrStandard(MaterialInfo materialInfo);

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
