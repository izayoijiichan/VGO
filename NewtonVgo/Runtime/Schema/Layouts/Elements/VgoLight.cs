// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoLight
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;
    using System.Numerics;

    /// <summary>
    /// VGO Light
    /// </summary>
    [Serializable]
    [JsonObject("light")]
    public class VgoLight
    {
        /// <summary>Whether the light is enable.</summary>
        [JsonProperty("enabled", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(true)]
        public bool enabled = false;

        /// <summary>The type of the light.</summary>
        [JsonProperty("type", Required = Required.Always)]
        public LightType type = LightType.Spot;

        /// <summary>This property describes the shape of the spot light.</summary>
        [JsonProperty("shape", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(LightShape.Cone)]
        public LightShape shape = LightShape.Cone;

        /// <summary>The range of the light.</summary>
        [JsonProperty("range", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float range = -1.0f;

        /// <summary>The angle of the light's spotlight cone in degrees.</summary>
        [JsonProperty("spotAngle", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float spotAngle = -1.0f;

        /// <summary>The size of the area light.</summary>
        [JsonProperty("areaSize")]
        public Vector2? areaSize = null;

        /// <summary>The radius of the area light.</summary>
        [JsonProperty("areaRadius", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float areaRadius = -1.0f;

        /// <summary>The color of the light.</summary>
        [JsonProperty("color")]
        public Color4? color = null;

        /// <summary>This property describes what part of a light's contribution can be baked.</summary>
        [JsonProperty("lightmapBakeType", Required = Required.Always)]
        public LightmapBakeType lightmapBakeType;

        /// <summary>The Intensity of a light is multiplied with the Light color.</summary>
        [JsonProperty("intensity", DefaultValueHandling = DefaultValueHandling.Populate)]
        [DefaultValue(0.0f)]
        public float intensity = 0.0f;

        /// <summary>How this light casts shadows.</summary>
        [JsonProperty("bounceIntensity", DefaultValueHandling = DefaultValueHandling.Populate)]
        [DefaultValue(0.0f)]
        public float bounceIntensity = 0.0f;

        ///// <summary>Cast Shadows</summary>
        //[JsonProperty("castShadows")]
        //public bool castShadows = false;

        /// <summary>How this light casts shadows.</summary>
        [JsonProperty("shadows", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(LightShadows.None)]
        public LightShadows shadows = LightShadows.None;

        // Baked Shadows

        /// <summary>Shadow Radius</summary>
        [JsonProperty("shadowRadius", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float shadowRadius = -1.0f;

        /// <summary>Controls the amount of artificial softening applied to the edges of shadows cast by directional lights.</summary>
        [JsonProperty("shadowAngle", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float shadowAngle = -1.0f;

        // Realtime Shadows

        /// <summary>Strength of light's shadows.</summary>
        [JsonProperty("shadowStrength", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float shadowStrength = -1.0f;

        /// <summary>The resolution of the shadow map.</summary>
        [JsonProperty("shadowResolution", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(LightShadowResolution.FromQualitySettings)]
        public LightShadowResolution shadowResolution = LightShadowResolution.FromQualitySettings;

        /// <summary>Shadow mapping constant bias.</summary>
        [JsonProperty("shadowBias", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float shadowBias = -1.0f;

        /// <summary>Shadow mapping normal-based bias.</summary>
        [JsonProperty("shadowNormalBias", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float shadowNormalBias = -1.0f;

        /// <summary>Near plane value to use for shadow frustums.</summary>
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

        /// <summary>How to render the light.</summary>
        [JsonProperty("renderMode", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(LightRenderMode.Auto)]
        public LightRenderMode renderMode = LightRenderMode.Auto;

        /// <summary>This is used to light certain objects in the Scene selectively.</summary>
        /// <remarks>0: None, -1: Everything</remarks>
        [JsonProperty("cullingMask", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(-1)]
        public int cullingMask = -1;
    }
}
