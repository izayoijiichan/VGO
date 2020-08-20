// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : IMaterialPorterStore
// ----------------------------------------------------------------------
namespace UniVgo2.Porters
{
    using NewtonVgo;

    /// <summary>
    /// Material Porter Store Interface
    /// </summary>
    public interface IMaterialPorterStore
    {
        #region Public Methods (MaterialInfo)

        /// <summary>
        /// Get a porter or default.
        /// </summary>
        /// <param name="vgoMaterial">The vgo material.</param>
        /// <returns>A material porter instanse.</returns>
        IMaterialPorter GetPorterOrDefault(VgoMaterial vgoMaterial);

        /// <summary>
        /// Get a porter or standard.
        /// </summary>
        /// <param name="vgoMaterial">The vgo material.</param>
        /// <returns>A material porter instanse.</returns>
        IMaterialPorter GetPorterOrStandard(VgoMaterial vgoMaterial);

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
