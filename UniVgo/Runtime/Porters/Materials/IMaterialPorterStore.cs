// ----------------------------------------------------------------------
// @Namespace : UniVgo.Porters
// @Class     : IMaterialPorterStore
// ----------------------------------------------------------------------
namespace UniVgo.Porters
{
    /// <summary>
    /// Material Porter Store Interface
    /// </summary>
    public interface IMaterialPorterStore
    {
        #region Public Methods (MaterialInfo)

        /// <summary>
        /// Get a porter or default.
        /// </summary>
        /// <param name="materialInfo">The material info.</param>
        /// <returns>A material porter instanse.</returns>
        IMaterialPorter GetPorterOrDefault(MaterialInfo materialInfo);

        /// <summary>
        /// Get a porter or standard.
        /// </summary>
        /// <param name="materialInfo">The material info.</param>
        /// <returns>A material porter instanse.</returns>
        IMaterialPorter GetPorterOrStandard(MaterialInfo materialInfo);

        #endregion

        #region Public Methods (ShaderName)

        /// <summary>
        /// Get a porter or default.
        /// </summary>
        /// <param name="shaderName">The shader name.</param>
        /// <returns>A material porter instanse.</returns>
        IMaterialPorter GetPorterOrDefault(string shaderName);

        /// <summary>
        /// Get a porter or standard.
        /// </summary>
        /// <param name="shaderName">The shader name.</param>
        /// <returns>A material porter instanse.</returns>
        IMaterialPorter GetPorterOrStandard(string shaderName);

        #endregion
    }
}
