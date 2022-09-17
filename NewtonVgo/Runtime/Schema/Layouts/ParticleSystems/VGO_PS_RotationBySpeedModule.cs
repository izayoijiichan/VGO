// ----------------------------------------------------------------------
// @Namespace : NewtonVgo.Schema.ParticleSystems
// @Class     : VGO_PS_RotationBySpeedModule
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo.Schema.ParticleSystems
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;
    using System.Numerics;

    /// <summary>
    /// VGO Particle System RotationBySpeedModule
    /// </summary>
    [Serializable]
    [JsonObject("vgo.ps.rotationBySpeedModule")]
    public class VGO_PS_RotationBySpeedModule
    {
        /// <summary>Specifies whether the RotationBySpeedModule is enabled or disabled.</summary>
        [JsonProperty("enabled")]
        public bool enabled;

        /// <summary>Set the rotation by speed on each axis separately.</summary>
        [JsonProperty("separateAxes")]
        public bool separateAxes;

        /// <summary>Rotation by speed curve for the x-axis.</summary>
        [JsonProperty("x")]
        public VGO_PS_MinMaxCurve? x;

        /// <summary>Rotation by speed curve for the y-axis.</summary>
        [JsonProperty("y")]
        public VGO_PS_MinMaxCurve? y;

        /// <summary>Rotation by speed curve for the z-axis.</summary>
        [JsonProperty("z")]
        public VGO_PS_MinMaxCurve? z;

        /// <summary>Rotation multiplier along the x-axis.</summary>
        [JsonProperty("xMultiplier", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float xMultiplier = -1.0f;

        /// <summary>Rotation multiplier along the y-axis.</summary>
        [JsonProperty("yMultiplier", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float yMultiplier = -1.0f;

        /// <summary>Rotation multiplier along the z-axis.</summary>
        [JsonProperty("zMultiplier", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1.0f)]
        public float zMultiplier = -1.0f;

        /// <summary>Set the minimum and maximum speed that this modules applies the rotation curve between.</summary>
        [JsonProperty("range")]
        public Vector2? range;
    }
}
