// ----------------------------------------------------------------------
// @Namespace : UniSkybox
// @Class     : Utils
// ----------------------------------------------------------------------
using System;

namespace UniSkybox
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class Utils
    {
        #region ShaderNames

        /// <summary>Skybox/6 Sided</summary>
        public const string ShaderNameSkybox6Sided = "Skybox/6 Sided";

        /// <summary>Skybox/Cubemap</summary>
        public const string ShaderNameSkyboxCubemap = "Skybox/Cubemap";

        /// <summary>Skybox/Panoramic</summary>
        public const string ShaderNameSkyboxPanoramic = "Skybox/Panoramic";

        /// <summary>Skybox/Procedural</summary>
        public const string ShaderNameSkyboxProcedural = "Skybox/Procedural";

        #endregion

        #region Types

        /// <summary>Skybox/6 Sided</summary>
        public static Type Skybox6SidedDefinitionType = typeof(Skybox6SidedDefinition);

        /// <summary>Skybox/Cubemap</summary>
        public static Type SkyboxCubemapDefinitionType = typeof(SkyboxCubemapDefinition);

        /// <summary>Skybox/Panoramic</summary>
        public static Type SkyboxPanoramicDefinitionType = typeof(SkyboxPanoramicDefinition);

        /// <summary>Skybox/Procedural</summary>
        public static Type SkyboxProceduralDefinitionType = typeof(SkyboxProceduralDefinition);

        #endregion

        #region Properties

        /// <summary>Tint</summary>
        public const string PropTint= "_Tint";

        /// <summary>Exposure</summary>
        public const string PropExposure = "_Exposure";

        /// <summary>Rotation</summary>
        public const string PropRotation = "_Rotation";

        #endregion

        #region Properties (6 Sided)

        /// <summary>FrontTex</summary>
        public const string PropFrontTex = "_FrontTex";

        /// <summary>BackTex</summary>
        public const string PropBackTex = "_BackTex";

        /// <summary>LeftTex</summary>
        public const string PropLeftTex = "_LeftTex";

        /// <summary>RightTex</summary>
        public const string PropRightTex = "_RightTex";

        /// <summary>UpTex</summary>
        public const string PropUpTex = "_UpTex";

        /// <summary>DownTex</summary>
        public const string PropDownTex = "_DownTex";

        #endregion

        #region Properties (Cubemap)

        /// <summary>Tex</summary>
        public const string PropTex = "_Tex";

        #endregion

        #region Properties (Panoramic)

        /// <summary>MainTex</summary>
        public const string PropMainTex = "_MainTex";

        /// <summary>Mapping</summary>
        public const string PropMapping = "_Mapping";

        /// <summary>ImageType</summary>
        public const string PropImageType = "_ImageType";

        /// <summary>MirrorOnBack</summary>
        public const string PropMirrorOnBack = "_MirrorOnBack";

        /// <summary>Layout</summary>
        public const string PropLayout = "_Layout";

        #endregion

        #region Properties (Procedural)

        /// <summary></summary>
        public const string PropSunDisk = "_SunDisk";

        /// <summary></summary>
        public const string PropSunSize = "_SunSize";

        /// <summary></summary>
        public const string PropSunSizeConvergence = "_SunSizeConvergence";

        /// <summary></summary>
        public const string PropAtmosphereThickness = "_AtmosphereThickness";

        /// <summary></summary>
        public const string PropSkyTint = "_SkyTint";

        /// <summary></summary>
        public const string PropGroundColor = "_GroundColor";

        #endregion

        #region Keywords

        /// <summary>Layout</summary>
        public const string KeyMapping6FramesLayout = "_MAPPING_6_FRAMES_LAYOUT";

        /// <summary>SunDisk None</summary>
        public const string KeySundiskNone = "_SUNDISK_NONE";

        /// <summary>SunDisk Simple</summary>
        public const string KeySundiskSimple = "_SUNDISK_SIMPLE";

        /// <summary>SunDisk High Quality</summary>
        public const string KeySundiskHighQuality = "_SUNDISK_HIGH_QUALITY";

        #endregion
    }
}