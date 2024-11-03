// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : UrpMtoon10MaterialPorter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// URP MToon 1.0 Material Porter
    /// </summary>
    public class UrpMtoon10MaterialPorter : Mtoon10MaterialPorter
    {
        #region Constructors

        /// <summary>
        /// Create a new instance of UrpMtoon10MaterialPorter.
        /// </summary>
        public UrpMtoon10MaterialPorter() : base() { }

        #endregion

        #region Public Methods (Export)

        /// <summary>
        /// Create a vgo material.
        /// </summary>
        /// <param name="material">A MToon material.</param>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <returns>A vgo material.</returns>
        public override VgoMaterial CreateVgoMaterial(in Material material, in IVgoStorage vgoStorage)
        {
            VgoMaterial vgoMaterial = base.CreateVgoMaterial(material, vgoStorage);

            // Tags
            ExportTag(vgoMaterial, material, UniUrpShader.Tag.RenderPipeline);
            ExportTag(vgoMaterial, material, UniUrpShader.Tag.UniversalMaterialType);
            ExportTag(vgoMaterial, material, UniUrpShader.Tag.IgnoreProjector);

            return vgoMaterial;
        }

        #endregion

        #region Public Methods (Import)

        /// <summary>
        /// Create a URP MToon 1.0 material.
        /// </summary>
        /// <param name="vgoMaterial">A vgo material.</param>
        /// <param name="shader">A URP MToon 1.0 shader.</param>
        /// <param name="allTextureList">List of all texture.</param>
        /// <returns>A URP MToon 1.0 material.</returns>
        public override Material CreateMaterialAsset(in VgoMaterial vgoMaterial, in Shader shader, in List<Texture?> allTextureList)
        {
            if (vgoMaterial.shaderName != ShaderName.VRM_URP_MToon10)
            {
                ThrowHelper.ThrowArgumentException($"vgoMaterial.shaderName: {vgoMaterial.shaderName}");
            }

            if (shader.name != ShaderName.VRM_URP_MToon10)
            {
                ThrowHelper.ThrowArgumentException($"shader.name: {shader.name}");
            }

#if UNIVGO_ENABLE_MTOON_1_0
            return CreateMaterialAssetInternal(vgoMaterial, shader, allTextureList);
#else
#if NET_STANDARD_2_1
            ThrowHelper.ThrowNotSupportedException(vgoMaterial.shaderName);
            return default;
#else
            throw new NotSupportedException(vgoMaterial.shaderName);
#endif
#endif
        }

        #endregion
    }
}
