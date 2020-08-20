// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : 
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    using System;

    /// <summary></summary>
    public enum CullMode
    {
        /// <summary></summary>
        Off,
        /// <summary></summary>
        Front,
        /// <summary></summary>
        Back,
    }

    /// <summary>Enum describing what part of a light contribution can be baked.</summary>
    [Flags]
    public enum LightmapBakeType
    {
        /// <summary>Mixed lights allow a mix of realtime and baked lighting, based on the Mixed Lighting Mode used.</summary>
        Mixed = 1,
        /// <summary>Baked lights cannot move or change in any way during run time.</summary>
        Baked = 2,
        /// <summary>Realtime lights cast run time light and shadows.</summary>
        Realtime = 4,
    }

    /// <summary>Light probe interpolation type.</summary>
    public enum LightProbeUsage
    {
        /// <summary>Light Probes are not used. The Scene's ambient probe is provided to the shader.</summary>
        Off = 0,
        /// <summary>Simple light probe interpolation is used.</summary>
        BlendProbes = 1,
        /// <summary>Uses a 3D grid of interpolated light probes.</summary>
        UseProxyVolume = 2,
        /// <summary>The light probe shader uniform values are extracted from the material property block set on the renderer.</summary>
        CustomProvided = 4,
    }

    /// <summary>How the Light is rendered.</summary>
    public enum LightRenderMode
    {
        /// <summary>Automatically choose the render mode.</summary>
        Auto = 0,
        /// <summary>Force the Light to be a pixel light.</summary>
        ForcePixel = 1,
        /// <summary>Force the Light to be a vertex light.</summary>
        ForceVertex = 2,
    }

    /// <summary>Shadow resolution options for a Light.</summary>
    public enum LightShadowResolution
    {
        /// <summary>Use resolution from QualitySettings (default).</summary>
        FromQualitySettings = -1,
        /// <summary>Low shadow map resolution.</summary>
        Low = 0,
        /// <summary>Medium shadow map resolution.</summary>
        Medium = 1,
        /// <summary>High shadow map resolution.</summary>
        High = 2,
        /// <summary>Very high shadow map resolution.</summary>
        VeryHigh = 3,
    }

    /// <summary>Shadow casting options for a Light.</summary>
    public enum LightShadows
    {
        /// <summary>Do not cast shadows (default).</summary>
        None = 0,
        /// <summary>Cast "hard" shadows (with no shadow filtering).</summary>
        Hard = 1,
        /// <summary>Cast "soft" shadows (with 4x PCF filtering).</summary>
        Soft = 2,
    }

    /// <summary>Describes the shape of a spot light.</summary>
    public enum LightShape
    {
        /// <summary>The shape of the spot light resembles a cone.</summary>
        Cone = 0,
        /// <summary>The shape of the spotlight resembles a pyramid or frustum.</summary>
        Pyramid = 1,
        /// <summary>The shape of the spot light resembles a box oriented along the ray direction.</summary>
        Box = 2,
    }

    /// <summary>The type of a Light.</summary>
    public enum LightType
    {
        /// <summary>The light is a spot light.</summary>
        Spot = 0,
        /// <summary>The light is a directional light.</summary>
        Directional = 1,
        /// <summary>The light is a point light.</summary>
        Point = 2,
        /// <summary>The light is a area light.</summary>
        Area = 3,
        /// <summary>The light is a rectangle shaped area light.</summary>
        Rectangle = 3,
        /// <summary>The light is a disc shaped area light.</summary>
        Disc = 4,
    }

    /// <summary>Reflection Probe usage.</summary>
    public enum ReflectionProbeUsage
    {
        /// <summary>Reflection probes are disabled, skybox will be used for reflection.</summary>
        Off = 0,
        /// <summary>Reflection probes are enabled. Blending occurs only between probes, useful in indoor environments.</summary>
        BlendProbes = 1,
        /// <summary>Reflection probes are enabled.</summary>
        BlendProbesAndSkybox = 2,
        /// <summary>Reflection probes are enabled, but no blending will occur between probes when there are two overlapping volumes.</summary>
        Simple = 3,
    }

    /// <summary>How shadows are cast from this object.</summary>
    public enum ShadowCastingMode
    {
        /// <summary>No shadows are cast from this object.</summary>
        Off = 0,
        /// <summary>Shadows are cast from this object.</summary>
        On = 1,
        /// <summary>Shadows are cast from this object, treating it as two-sided.</summary>
        TwoSided = 2,
        /// <summary>Object casts shadows, but is otherwise invisible in the Scene.</summary>
        ShadowsOnly = 3,
    }

    /// <summary>Texture "dimension" (type).</summary>
    public enum TextureDimension
    {
        /// <summary>Texture type is not initialized or unknown.</summary>
        Unknown = -1,
        /// <summary>No texture is assigned.</summary>
        None = 0,
        /// <summary>Any texture type.</summary>
        Any = 1,
        /// <summary>2D texture (Texture2D).</summary>
        Tex2D = 2,
        /// <summary>3D volume texture (Texture3D).</summary>
        Tex3D = 3,
        /// <summary>Cubemap texture.</summary>
        Cube = 4,
        /// <summary>2D array texture (Texture2DArray).</summary>
        Tex2DArray = 5,
        /// <summary>Cubemap array texture (CubemapArray).</summary>
        CubeArray = 6,
    }

    /// <summary></summary>
    public enum UVChannelFlags
    {
        /// <summary></summary>
        UV0 = 0,
        /// <summary></summary>
        UV1 = 1,
        /// <summary></summary>
        UV2 = 2,
        /// <summary></summary>
        UV3 = 3,
    }
}
