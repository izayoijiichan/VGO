// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : IMaterialPorterStore
// ----------------------------------------------------------------------
#nullable enable
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
        IMaterialPorter? GetPorterOrDefault(in VgoMaterial vgoMaterial);

        /// <summary>
        /// Get a porter or standard.
        /// </summary>
        /// <param name="vgoMaterial">The vgo material.</param>
        /// <param name="renderPipelineType">Type of render pipeline.</param>
        /// <returns>A material porter instanse.</returns>
        IMaterialPorter GetPorterOrStandard(in VgoMaterial vgoMaterial, in RenderPipelineType renderPipelineType);

        #endregion

        #region Public Methods (ShaderName)

        /// <summary>
        /// Get a porter or default.
        /// </summary>
        /// <param name="shaderName">The shader name.</param>
        /// <returns>A material porter instanse.</returns>
        IMaterialPorter? GetPorterOrDefault(in string shaderName);

        /// <summary>
        /// Get a porter or standard.
        /// </summary>
        /// <param name="shaderName">The shader name.</param>
        /// <param name="renderPipelineType">Type of render pipeline.</param>
        /// <returns>A material porter instanse.</returns>
        IMaterialPorter GetPorterOrStandard(in string shaderName, in RenderPipelineType renderPipelineType);

        /// <summary>
        /// Get a porter or standard.
        /// </summary>
        /// <param name="shaderName">The shader name.</param>
        /// <param name="vgoMaterial">The vgo material.</param>
        /// <param name="renderPipelineType">Type of render pipeline.</param>
        /// <returns>A material porter instanse.</returns>
        IMaterialPorter GetPorterOrStandard(in string shaderName, in VgoMaterial vgoMaterial, in RenderPipelineType renderPipelineType);

        #endregion
    }
}
