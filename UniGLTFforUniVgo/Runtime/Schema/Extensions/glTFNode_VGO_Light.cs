// ----------------------------------------------------------------------
// @Namespace : UniGLTFforUniVgo
// @Class     : glTFNode_VGO_Light
// ----------------------------------------------------------------------
namespace UniGLTFforUniVgo
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;
    using UnityEngine;
    using UnityEngine.Rendering;

    /// <summary>
    /// glTF Node VGO Light
    /// </summary>
    [Serializable]
    public class glTFNode_VGO_Light
    {
        /// <summary>Enabled</summary>
        [JsonProperty("enabled", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(true)]
        public bool enabled = false;

        /// <summary></summary>
        [JsonProperty("type", Required = Required.Always)]
        public LightType type = LightType.Spot;

        /// <summary>Shape</summary>
        [JsonProperty("shape", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(LightShape.Cone)]
        public LightShape shape = LightShape.Cone;

        /// <summary>Range</summary>
        [JsonProperty("range", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float range = -1.0f;

        /// <summary>Spot Angle</summary>
        [JsonProperty("spotAngle", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float spotAngle = -1.0f;

        /// <summary>Area Size</summary>
        [JsonProperty("areaSize")]
        public float[] areaSize = null;

        /// <summary>Area Radius</summary>
        [JsonProperty("areaRadius", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float areaRadius = -1.0f;

        /// <summary>Color</summary>
        [JsonProperty("color")]
        public float[] color = null;

        /// <summary>Lightmap Bake Type</summary>
        [JsonProperty("lightmapBakeType", Required = Required.Always)]
        public LightmapBakeType lightmapBakeType;

        /// <summary>Intensity</summary>
        [JsonProperty("intensity", DefaultValueHandling = DefaultValueHandling.Populate)]
        [DefaultValue(0.0f)]
        public float intensity = 0.0f;

        /// <summary>Bounce Intensity</summary>
        [JsonProperty("bounceIntensity", DefaultValueHandling = DefaultValueHandling.Populate)]
        [DefaultValue(0.0f)]
        public float bounceIntensity = 0.0f;

        ///// <summary>Cast Shadows</summary>
        //[JsonProperty("castShadows")]
        //public bool castShadows = false;

        /// <summary>Shadows</summary>
        [JsonProperty("shadows", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(LightShadows.None)]
        public LightShadows shadows = LightShadows.None;

        // Baked Shadows

        /// <summary>Shadow Radius</summary>
        [JsonProperty("shadowRadius", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float shadowRadius = -1.0f;

        /// <summary>Shadow Angle</summary>
        [JsonProperty("shadowAngle", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float shadowAngle = -1.0f;

        // Realtime Shadows

        /// <summary>Shadow Strength</summary>
        [JsonProperty("shadowStrength", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float shadowStrength = -1.0f;

        /// <summary>Shadow Resolution</summary>
        [JsonProperty("shadowResolution", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(LightShadowResolution.FromQualitySettings)]
        public LightShadowResolution shadowResolution = LightShadowResolution.FromQualitySettings;

        /// <summary>Shadow Bias</summary>
        [JsonProperty("shadowBias", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float shadowBias = -1.0f;

        /// <summary>Shadow Normal Bias</summary>
        [JsonProperty("shadowNormalBias", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float shadowNormalBias = -1.0f;

        /// <summary>Shadow Near Plane</summary>
        [JsonProperty("shadowNearPlane", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float shadowNearPlane = -1.0f;

        ///// <summary>Cookie</summary>
        //[JsonProperty("cookie")]
        //public Texture cookie = null;

        ///// <summary>Cookie Size</summary>
        //[JsonProperty("cookieSize", DefaultValueHandling = DefaultValueHandling.Ignore)]
        //[DefaultValue(-1.0f)]
        //public float cookieSize = -1.0f;

        ///// <summary>Draw Halo</summary>
        //[JsonProperty("drawHalo")]
        //public bool drawHalo = false;

        ///// <summary>Flare</summary>
        //[JsonProperty("flare")]
        //public Flare flare = null;

        /// <summary>Render Mode</summary>
        [JsonProperty("renderMode", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(LightRenderMode.Auto)]
        public LightRenderMode renderMode = LightRenderMode.Auto;

        /// <summary>Culling Mask</summary>
        /// <remarks>0: None, -1: Everything</remarks>
        [JsonProperty("cullingMask", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(-1)]
        public int cullingMask = -1;
    }
}
