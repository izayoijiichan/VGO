// ----------------------------------------------------------------------
// @Namespace : UniGLTFforUniVgo
// @Class     : VGO_PS_Renderer
// ----------------------------------------------------------------------
namespace UniGLTFforUniVgo
{
    using Newtonsoft.Json;
    using System;
    using UnityEngine;
    using UnityEngine.Rendering;

    /// <summary>
    /// VGO Particle System Renderer
    /// </summary>
    [Serializable]
    [JsonObject("vgo.ps.renderer")]
    public class VGO_PS_Renderer
    {
        /// <summary>Makes the rendered 3D object visible if enabled.</summary>
        [JsonProperty("enabled")]
        public bool enabled;

        /// <summary>Specifies how the system draws particles.</summary>
        [JsonProperty("renderMode")]
        public ParticleSystemRenderMode renderMode;

        /// <summary>How much do the particles stretch depending on the Camera's speed.</summary>
        [JsonProperty("cameraVelocityScale")]
        public float cameraVelocityScale;

        /// <summary>Specifies how much particles stretch depending on their velocity.</summary>
        [JsonProperty("velocityScale")]
        public float velocityScale;

        /// <summary>How much are the particles stretched in their direction of motion.</summary>
        [JsonProperty("lengthScale")]
        public float lengthScale;

        /// <summary>Specifies how much a billboard particle orients its normals towards the Camera.</summary>
        [JsonProperty("normalDirection")]
        public float normalDirection;

        ///// <summary>The Mesh that the particle uses instead of a billboarded Texture.</summary>
        //[JsonProperty("mesh")]
        //public Mesh mesh;

        ///// <summary>The number of Meshes the system uses for particle rendering.</summary>
        //[JsonProperty("meshCount")]
        //public int meshCount { get; }

        ///// <summary>Returns the first instantiated Material assigned to the renderer.</summary>
        //public Material material;

        /// <summary>The shared material of this object.</summary>
        [JsonProperty("sharedMaterialIndex")]
        //public Material sharedMaterial;
        public int sharedMaterial;

        /// <summary>Set the Material that the TrailModule uses to attach trails to particles.</summary>
        [JsonProperty("trailMaterialIndex")]
        //public Material trailMaterial;
        public int trailMaterialIndex;

        /// <summary>Specifies how to sort particles within a system.</summary>
        [JsonProperty("sortMode")]
        public ParticleSystemSortMode sortMode;

        /// <summary>Biases Particle System sorting amongst other transparencies.</summary>
        [JsonProperty("sortingFudge")]
        public float sortingFudge;

        /// <summary>Clamp the minimum particle size.</summary>
        [JsonProperty("minParticleSize")]
        public float minParticleSize;

        /// <summary>Clamp the maximum particle size.</summary>
        [JsonProperty("maxParticleSize")]
        public float maxParticleSize;

        /// <summary>Control the direction that particles face.</summary>
        [JsonProperty("alignment")]
        //[NativeName("RenderAlignment")]
        public ParticleSystemRenderSpace alignment;

        /// <summary>Flip a percentage of the particles, along each axis.</summary>
        [JsonProperty("flip")]
        //public Vector3 flip;
        public float[] flip;

        /// <summary>Allow billboard particles to roll around their z-axis.</summary>
        [JsonProperty("allowRoll")]
        public bool allowRoll;

        /// <summary>Modify the pivot point used for rotating particles.</summary>
        [JsonProperty("pivot")]
        //public Vector3 pivot;
        public float[] pivot;

        /// <summary>Specifies how the Particle System Renderer interacts with SpriteMask.</summary>
        [JsonProperty("maskInteraction")]
        public SpriteMaskInteraction maskInteraction;

        /// <summary>Enables GPU Instancing on platforms that support it.</summary>
        [JsonProperty("enableGPUInstancing")]
        public bool enableGPUInstancing;

        /// <summary>Does this object cast shadows?</summary>
        [JsonProperty("shadowCastingMode")]
        public ShadowCastingMode shadowCastingMode;

        /// <summary>Does this object receive shadows?</summary>
        [JsonProperty("receiveShadows")]
        public bool receiveShadows;

        /// <summary>Apply a shadow bias to prevent self-shadowing artifacts. The specified value is the proportion of the particle size.</summary>
        [JsonProperty("shadowBias")]
        public float shadowBias;

        /// <summary>Specifies the mode for motion vector rendering.</summary>
        [JsonProperty("motionVectorGenerationMode")]
        public MotionVectorGenerationMode motionVectorGenerationMode;

        /// <summary>Allows turning off rendering for a specific component.</summary>
        [JsonProperty("forceRenderingOff")]
        public bool forceRenderingOff;

        /// <summary>This value sorts renderers by priority. Lower values are rendered first and higher values are rendered last.</summary>
        [JsonProperty("rendererPriority")]
        public int rendererPriority;

        /// <summary>Determines which rendering layer this renderer lives on.</summary>
        [JsonProperty("renderingLayerMask")]
        public uint renderingLayerMask;

        /// <summary>Unique ID of the Renderer's sorting layer.</summary>
        [JsonProperty("sortingLayerID")]
        public int sortingLayerID;

        /// <summary>Renderer's order within a sorting layer.</summary>
        [JsonProperty("sortingOrder")]
        public int sortingOrder;

        /// <summary>The light probe interpolation type.</summary>
        [JsonProperty("lightProbeUsage")]
        public LightProbeUsage lightProbeUsage;

        /// <summary>Should reflection probes be used for this Renderer?</summary>
        [JsonProperty("reflectionProbeUsage")]
        public ReflectionProbeUsage reflectionProbeUsage;

        /// <summary>If set, Renderer will use this Transform's position to find the light or reflection probe.</summary>
        [JsonProperty("probeAnchor")]
        public VGO_Transform probeAnchor;

        ///// <summary>The number of currently active custom vertex streams.</summary>
        //[JsonProperty("activeVertexStreamsCount")]
        //public int activeVertexStreamsCount { get; }
    }
}
