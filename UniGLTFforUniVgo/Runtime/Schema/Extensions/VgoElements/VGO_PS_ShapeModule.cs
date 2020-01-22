// ----------------------------------------------------------------------
// @Namespace : UniGLTFforUniVgo
// @Class     : VGO_PS_ShapeModule
// ----------------------------------------------------------------------
namespace UniGLTFforUniVgo
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;
    using UnityEngine;

    /// <summary>
    /// VGO Particle System ShapeModule
    /// </summary>
    [Serializable]
    [JsonObject("vgo.ps.shapeModule")]
    public class VGO_PS_ShapeModule
    {
        /// <summary>Specifies whether the ShapeModule is enabled or disabled.</summary>
        [JsonProperty("enabled")]
        public bool enabled;

        /// <summary>The type of shape to emit particles from.</summary>
        [JsonProperty("shapeType")]
        public ParticleSystemShapeType shapeType;

        /// <summary>Angle of the cone to emit particles from.</summary>
        [JsonProperty("angle")]
        public float angle;

        /// <summary>Radius of the shape to emit particles from.</summary>
        [JsonProperty("radius")]
        public float radius;

        /// <summary>The thickness of the Donut shape to emit particles from.</summary>
        [JsonProperty("donutRadius")]
        public float donutRadius;

        /// <summary>The mode to use to generate particles along the radius.</summary>
        [JsonProperty("radiusMode")]
        public ParticleSystemShapeMultiModeValue radiusMode;

        /// <summary>Control the gap between particle emission points along the radius.</summary>
        [JsonProperty("radiusSpread")]
        public float radiusSpread;

        /// <summary>In animated modes, this determines how quickly the particle emission position moves along the radius.</summary>
        [JsonProperty("radiusSpeed")]
        public VGO_PS_MinMaxCurve radiusSpeed;

        /// <summary>A multiplier of the radius speed of the particle emission shape.</summary>
        [JsonProperty("radiusSpeedMultiplier")]
        public float radiusSpeedMultiplier;

        /// <summary>Radius thickness of the shape's edge from which to emit particles.</summary>
        [JsonProperty("radiusThickness")]
        public float radiusThickness;

        /// <summary>Thickness of the box to emit particles from.</summary>
        [JsonProperty("boxThickness")]
        //public Vector3 boxThickness;
        public float[] boxThickness;

        /// <summary>Angle of the circle arc to emit particles from.</summary>
        [JsonProperty("arc")]
        public float arc;

        /// <summary>The mode that Unity uses to generate particles around the arc.</summary>
        [JsonProperty("arcMode")]
        public ParticleSystemShapeMultiModeValue arcMode;

        /// <summary>Control the gap between particle emission points around the arc.</summary>
        [JsonProperty("arcSpread")]
        public float arcSpread;

        /// <summary>In animated modes, this determines how quickly the particle emission position moves around the arc.</summary>
        [JsonProperty("arcSpeed")]
        public VGO_PS_MinMaxCurve arcSpeed;

        /// <summary>A multiplier of the arc speed of the particle emission shape.</summary>
        [JsonProperty("arcSpeedMultiplier")]
        public float arcSpeedMultiplier;

        /// <summary>Length of the cone to emit particles from.</summary>
        [JsonProperty("length")]
        public float length;

        /// <summary>Where on the Mesh to emit particles from.</summary>
        [JsonProperty("meshShapeType")]
        public ParticleSystemMeshShapeType meshShapeType;

        /// <summary>The mode to use to generate particles on a Mesh.</summary>
        [JsonProperty("meshSpawnMode")]
        public ParticleSystemShapeMultiModeValue meshSpawnMode;

        /// <summary>Control the gap between particle emission points across the Mesh.</summary>
        [JsonProperty("meshSpawnSpread")]
        public float meshSpawnSpread;

        /// <summary>In animated modes, this determines how quickly the particle emission position moves across the Mesh.</summary>
        [JsonProperty("meshSpawnSpeed")]
        public VGO_PS_MinMaxCurve meshSpawnSpeed;

        /// <summary>A multiplier of the Mesh spawn speed.</summary>
        [JsonProperty("meshSpawnSpeedMultiplier")]
        public float meshSpawnSpeedMultiplier;

        ///// <summary>Mesh to emit particles from.</summary>
        //[JsonProperty("mesh")]
        //public Mesh mesh;

        ///// <summary>MeshRenderer to emit particles from.</summary>
        //[JsonProperty("meshRenderer")]
        //public MeshRenderer meshRenderer;

        ///// <summary>SkinnedMeshRenderer to emit particles from.</summary>
        //[JsonProperty("skinnedMeshRenderer")]
        //public SkinnedMeshRenderer skinnedMeshRenderer;

        /// <summary>Emit particles from a single Material, or the whole Mesh.</summary>
        [JsonProperty("useMeshMaterialIndex")]
        public bool useMeshMaterialIndex;

        /// <summary>Emit particles from a single Material of a Mesh.</summary>
        [JsonProperty("meshMaterialIndex")]
        public int meshMaterialIndex;

        /// <summary>Modulate the particle colors with the vertex colors, or the Material color if no vertex colors exist.</summary>
        [JsonProperty("useMeshColors")]
        public bool useMeshColors;

        ///// <summary>Sprite to emit particles from.</summary>
        //[JsonProperty("sprite")]
        //public Sprite sprite;

        ///// <summary>SpriteRenderer to emit particles from.</summary>
        //[JsonProperty("spriteRenderer")]
        //public SpriteRenderer spriteRenderer;

        /// <summary>Move particles away from the surface of the source Mesh.</summary>
        [JsonProperty("normalOffset")]
        public float normalOffset;

        /// <summary>Specifies a Texture to tint the particle's start colors.</summary>
        [JsonProperty("textureIndex", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        //public Texture2D texture;
        public int textureIndex = -1;

        /// <summary>Selects which channel of the Texture to use for discarding particles.</summary>
        [JsonProperty("textureClipChannel")]
        public ParticleSystemShapeTextureChannel textureClipChannel;

        /// <summary>Discards particles when they spawn on an area of the Texture with a value lower than this threshold.</summary>
        [JsonProperty("textureClipThreshold")]
        public float textureClipThreshold;

        /// <summary>When enabled, the system applies the RGB channels of the Texture to the particle color when the particle spawns.</summary>
        [JsonProperty("textureColorAffectsParticles")]
        public bool textureColorAffectsParticles;

        /// <summary>When enabled, the system applies the alpha channel of the Texture to the particle alpha when the particle spawns.</summary>
        [JsonProperty("textureAlphaAffectsParticles")]
        public bool textureAlphaAffectsParticles;

        /// <summary>When enabled, the system takes four neighboring samples from the Texture then combines them to give the final particle value.</summary>
        [JsonProperty("textureBilinearFiltering")]
        public bool textureBilinearFiltering;

        /// <summary>When using a Mesh as a source shape type, this option controls which UV channel on the Mesh to use for reading the source Texture.</summary>
        [JsonProperty("textureUVChannel")]
        public int textureUVChannel;

        /// <summary>Apply an offset to the position from which the system emits particles.</summary>
        [JsonProperty("position")]
        //public Vector3 position;
        public float[] position;

        /// <summary>Apply a rotation to the shape from which the system emits particles.</summary>
        [JsonProperty("rotation")]
        //public Vector3 rotation;
        public float[] rotation;

        /// <summary>Apply scale to the shape from which the system emits particles.</summary>
        [JsonProperty("scale")]
        //public Vector3 scale;
        public float[] scale;

        /// <summary>Align particles based on their initial direction of travel.</summary>
        [JsonProperty("alignToDirection")]
        public bool alignToDirection;

        /// <summary>Randomizes the starting direction of particles.</summary>
        [JsonProperty("randomDirectionAmount")]
        public float randomDirectionAmount;

        /// <summary>Makes particles move in a spherical direction from their starting point.</summary>
        [JsonProperty("sphericalDirectionAmount")]
        public float sphericalDirectionAmount;

        /// <summary>Randomizes the starting position of particles.</summary>
        [JsonProperty("randomPositionAmount")]
        public float randomPositionAmount;
    }
}
