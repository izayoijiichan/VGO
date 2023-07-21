// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : 
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    /// <summary>The animation mode.</summary>
    public enum ParticleSystemAnimationMode
    {
        /// <summary>Use a regular grid to construct a sequence of animation frames.</summary>
        Grid = 0,
        /// <summary>Use a list of sprites to construct a sequence of animation frames.</summary>
        Sprites = 1,
    }

    /// <summary>The mode used for selecting rows of an animation in the Texture Sheet Animation Module.</summary>
    public enum ParticleSystemAnimationRowMode
    {
        /// <summary>Use a specific row for all particles.</summary>
        Custom = 0,
        /// <summary>Use a random row for each particle.</summary>
        Random = 1,
        /// <summary>Use the mesh index as the row, so that meshes can be mapped to specific animation frames.</summary>
        MeshIndex = 2,
    }

    /// <summary>Control how animation frames are selected.</summary>
    public enum ParticleSystemAnimationTimeMode
    {
        /// <summary>Select animation frames based on the particle ages.</summary>
        Lifetime = 0,
        /// <summary>Select animation frames based on the particle speeds.</summary>
        Speed = 1,
        /// <summary>Select animation frames sequentially at a constant rate of the specified frames per second.</summary>
        FPS = 2,
    }

    /// <summary>The animation type.</summary>
    public enum ParticleSystemAnimationType
    {
        /// <summary>Animate over the whole texture sheet from left to right, top to bottom.</summary>
        WholeSheet = 0,
        /// <summary>Animate a single row in the sheet from left to right.</summary>
        SingleRow = 1,
    }

    /// <summary>Whether to use 2D or 3D colliders for particle collisions.</summary>
    public enum ParticleSystemCollisionMode
    {
        /// <summary>Use 3D colliders to collide particles against.</summary>
        Collision3D = 0,
        /// <summary>Use 2D colliders to collide particles against.</summary>
        Collision2D = 1,
    }

    /// <summary>Quality of world collisions. Medium and low quality are approximate and may leak particles.</summary>
    public enum ParticleSystemCollisionQuality
    {
        /// <summary>The most accurate world collisions.</summary>
        High = 0,
        /// <summary>Approximate world collisions.</summary>
        Medium = 1,
        /// <summary>Fastest and most approximate world collisions.</summary>
        Low = 2,
    }

    /// <summary>The type of collisions to use for a given Particle System.</summary>
    public enum ParticleSystemCollisionType
    {
        /// <summary>Collide with a list of planes.</summary>
        Planes = 0,
        /// <summary>Collide with the world geometry.</summary>
        World = 1,
    }

    /// <summary>The action to perform when the Particle System is offscreen.</summary>
    public enum ParticleSystemCullingMode
    {
        /// <summary>For looping effects, the simulation is paused when offscreen, and for one-shot effects, the simulation will continue playing.</summary>
        Automatic = 0,
        /// <summary>Pause the Particle System simulation when it is offscreen, and perform an extra simulation when the system comes back onscreen, creating the impression that it was never paused.</summary>
        PauseAndCatchup = 1,
        /// <summary>Pause the Particle System simulation when it is offscreen.</summary>
        Pause = 2,
        /// <summary>Continue simulating the Particle System when it is offscreen.</summary>
        AlwaysSimulate = 3,
    }

    /// <summary>The particle curve mode.</summary>
    public enum ParticleSystemCurveMode
    {
        /// <summary>Use a single constant for the MinMaxCurve.</summary>
        Constant = 0,
        /// <summary>Use a single curve for the MinMaxCurve.</summary>
        Curve = 1,
        /// <summary>Use a random value between 2 curves for the MinMaxCurve.</summary>
        TwoCurves = 2,
        /// <summary>Use a random value between 2 constants for the MinMaxCurve.</summary>
        TwoConstants = 3,
    }

    /// <summary>Control how a Particle System calculates its velocity.</summary>
    public enum ParticleSystemEmitterVelocityMode
    {
        /// <summary>Calculate the Particle System velocity by using the Transform component.</summary>
        Transform = 0,
        /// <summary>Calculate the Particle System velocity by using a Rigidbody or Rigidbody2D component, if one exists on the GameObject.</summary>
        Rigidbody = 1,
        /// <summary>When the Particle System calculates its velocity, it instead uses the custom value set in ParticleSystem.MainModule.emitterVelocity.</summary>
        Custom = 2,
    }

    /// <summary>The particle GameObject filtering mode that specifies which objects are used by specific Particle System modules.</summary>
    public enum ParticleSystemGameObjectFilter
    {
        /// <summary>Include objects based on a layer mask, where all objects that match the mask are included.</summary>
        LayerMask = 0,
        /// <summary>Include objects based on an explicitly provided list.</summary>
        List = 1,
        /// <summary>Include objects based on both a layer mask and an explicitly provided list.</summary>
        LayerMaskAndList = 2,
    }

    /// <summary>The particle gradient mode.</summary>
    public enum ParticleSystemGradientMode
    {
        /// <summary>Use a single color for the MinMaxGradient.</summary>
        Color = 0,
        /// <summary>Use a single color gradient for the MinMaxGradient.</summary>
        Gradient = 1,
        /// <summary>Use a random value between 2 colors for the MinMaxGradient.</summary>
        TwoColors = 2,
        /// <summary>Use a random value between 2 color gradients for the MinMaxGradient.</summary>
        TwoGradients = 3,
        /// <summary>Define a list of colors in the MinMaxGradient, to be chosen from at random.</summary>
        RandomColor = 4,
    }

    /// <summary>How to apply emitter velocity to particles.</summary>
    public enum ParticleSystemInheritVelocityMode
    {
        /// <summary>Each particle inherits the emitter's velocity on the frame when it was initially emitted.</summary>
        Initial = 0,
        /// <summary>Each particle's velocity is set to the emitter's current velocity value, every frame.</summary>
        Current = 1,
    }

    /// <summary>The mesh emission type.</summary>
    public enum ParticleSystemMeshShapeType
    {
        /// <summary>Emit from the vertices of the mesh.</summary>
        Vertex = 0,
        /// <summary>Emit from the edges of the mesh.</summary>
        Edge = 1,
        /// <summary>Emit from the surface of the mesh.</summary>
        Triangle = 2,
    }

    /// <summary>The quality of the generated noise.</summary>
    public enum ParticleSystemNoiseQuality
    {
        /// <summary>Low quality 1D noise.</summary>
        Low = 0,
        /// <summary>Medium quality 2D noise.</summary>
        Medium = 1,
        /// <summary>High quality 3D noise.</summary>
        High = 2,
    }

    /// <summary>What action to perform when the particle trigger module passes a test.</summary>
    public enum ParticleSystemOverlapAction
    {
        /// <summary>Do nothing.</summary>
        Ignore = 0,
        /// <summary>Kill all particles that pass this test.</summary>
        Kill = 1,
        /// <summary>Send the OnParticleTrigger command to the Particle System's script.</summary>
        Callback = 2,
    }

    /// <summary>The rendering mode for particle systems.</summary>
    public enum ParticleSystemRenderMode
    {
        /// <summary>Render particles as billboards facing the active camera. (Default)</summary>
        Billboard = 0,
        /// <summary>Stretch particles in the direction of motion.</summary>
        Stretch = 1,
        /// <summary>Render particles as billboards always facing up along the y-Axis.</summary>
        HorizontalBillboard = 2,
        /// <summary>Render particles as billboards always facing the player, but not pitching along the x-Axis.</summary>
        VerticalBillboard = 3,
        /// <summary>Render particles as meshes.</summary>
        Mesh = 4,
        /// <summary>Do not render particles.</summary>
        None = 5,
    }

    /// <summary>How particles are aligned when rendered.</summary>
    public enum ParticleSystemRenderSpace
    {
        /// <summary>Particles face the camera plane.</summary>
        View = 0,
        /// <summary>Particles align with the world.</summary>
        World = 1,
        /// <summary>Particles align with their local transform.</summary>
        Local = 2,
        /// <summary>Particles face the eye position.</summary>
        Facing = 3,
        /// <summary>Particles are aligned to their direction of travel.</summary>
        Velocity = 4,
    }

    /// <summary>Control how particles are removed from the Particle System.</summary>
    public enum ParticleSystemRingBufferMode
    {
        /// <summary>Particles are removed when their age exceeds their lifetime.</summary>
        Disabled = 0,
        /// <summary>Particle ages pause at the end of their lifetime until they need to be removed. Particles are removed when creating new particles would exceed the Max Particles property.</summary>
        PauseUntilReplaced = 1,
        /// <summary>Particle ages loop until they need to be removed. Particles are removed when creating new particles would exceed the Max Particles property.</summary>
        LoopUntilReplaced = 2,
    }

    /// <summary>Control how particle systems apply transform scale.</summary>
    public enum ParticleSystemScalingMode
    {
        /// <summary>Scale the Particle System using the entire transform hierarchy.</summary>
        Hierarchy = 0,
        /// <summary>Scale the Particle System using only its own transform scale. (Ignores parent scale).</summary>
        Local = 1,
        /// <summary>Only apply transform scale to the shape component, which controls where particles are spawned, but does not affect their size or movement.</summary>
        Shape = 2,
    }

    /// <summary>The mode used to generate new points in a shape.</summary>
    public enum ParticleSystemShapeMultiModeValue
    {
        /// <summary>Generate points randomly. (Default)</summary>
        Random = 0,
        /// <summary>Animate the emission point around the shape.</summary>
        Loop = 1,
        /// <summary>Animate the emission point around the shape, alternating between clockwise and counter-clockwise directions.</summary>
        PingPong = 2,
        /// <summary>Distribute new particles around the shape evenly.</summary>
        BurstSpread = 3,
    }

    /// <summary>The texture channel.</summary>
    public enum ParticleSystemShapeTextureChannel
    {
        /// <summary>The red channel.</summary>
        Red = 0,
        /// <summary>The green channel.</summary>
        Green = 1,
        /// <summary>The blue channel.</summary>
        Blue = 2,
        /// <summary>The alpha channel.</summary>
        Alpha = 3,
    }

    /// <summary>The emission shape.</summary>
    public enum ParticleSystemShapeType
    {
        /// <summary>Emit from a sphere.</summary>
        Sphere = 0,
        /// <summary></summary>
        SphereShell = 1,
        /// <summary>Emit from a half-sphere.</summary>
        Hemisphere = 2,
        /// <summary></summary>
        HemisphereShell = 3,
        /// <summary>Emit from the base of a cone.</summary>
        Cone = 4,
        /// <summary>Emit from the volume of a box.</summary>
        Box = 5,
        /// <summary>Emit from a mesh.</summary>
        Mesh = 6,
        /// <summary></summary>
        ConeShell = 7,
        /// <summary>Emit from a cone.</summary>
        ConeVolume = 8,
        /// <summary></summary>
        ConeVolumeShell = 9,
        /// <summary>Emit from a circle.</summary>
        Circle = 10,
        /// <summary></summary>
        CircleEdge = 11,
        /// <summary>Emit from an edge.</summary>
        SingleSidedEdge = 12,
        /// <summary>Emit from a mesh renderer.</summary>
        MeshRenderer = 13,
        /// <summary>Emit from a skinned mesh renderer.</summary>
        SkinnedMeshRenderer = 14,
        /// <summary>Emit from the surface of a box.</summary>
        BoxShell = 15,
        /// <summary>Emit from the edges of a box.</summary>
        BoxEdge = 16,
        /// <summary>Emit from a Donut.</summary>
        Donut = 17,
        /// <summary>Emit from a rectangle.</summary>
        Rectangle = 18,
        /// <summary>Emit from a sprite.</summary>
        Sprite = 19,
        /// <summary>Emit from a sprite renderer.</summary>
        SpriteRenderer = 20,
    }

    /// <summary>The space to simulate particles in.</summary>
    public enum ParticleSystemSimulationSpace
    {
        /// <summary>Simulate particles in local space.</summary>
        Local = 0,
        /// <summary>Simulate particles in world space.</summary>
        World = 1,
        /// <summary>Simulate particles relative to a custom transform component, defined by ParticleSystem.MainModule.customSimulationSpace.</summary>
        Custom = 2,
    }

    /// <summary>The sorting mode for particle systems.</summary>
    public enum ParticleSystemSortMode
    {
        /// <summary>No sorting.</summary>
        None = 0,
        /// <summary>Sort based on distance to the camera position. For orthographic cameras, this mode is the same as sorting by depth.</summary>
        Distance = 1,
        /// <summary>Sort the oldest particles to the front.</summary>
        OldestInFront = 2,
        /// <summary>Sort the youngest particles to the front.</summary>
        YoungestInFront = 3,
        /// <summary>Sort based on depth from the camera plane.</summary>
        Depth = 4,
    }

    /// <summary>The action to perform when the Particle System stops.</summary>
    public enum ParticleSystemStopAction
    {
        /// <summary>Do nothing.</summary>
        None = 0,
        /// <summary>Disable the GameObject containing the Particle System.</summary>
        Disable = 1,
        /// <summary>Destroy the GameObject containing the Particle System.</summary>
        Destroy = 2,
        /// <summary>Call MonoBehaviour.OnParticleSystemStopped on all scripts attached to the same GameObject.</summary>
        Callback = 3,
    }

    /// <summary>Choose how Particle Trails are generated.</summary>
    public enum ParticleSystemTrailMode
    {
        /// <summary>Makes a trail behind each particle as the particle moves.</summary>
        PerParticle = 0,
        /// <summary>Draws a line between each particle, connecting the youngest particle to the oldest.</summary>
        Ribbon = 1,
    }

    /// <summary>Choose how textures are applied to Particle Trails.</summary>
    public enum ParticleSystemTrailTextureMode
    {
        /// <summary>Map the texture once along the entire length of the trail.</summary>
        Stretch = 0,
        /// <summary>Repeat the texture along the trail. To set the tiling rate, use Material.SetTextureScale.</summary>
        Tile = 1,
        /// <summary>Map the texture once along the entire length of the trail, assuming all vertices are evenly spaced.</summary>
        DistributePerSegment = 2,
        /// <summary>Repeat the texture along the trail, repeating at a rate of once per trail segment. To adjust the tiling rate, use Material.SetTextureScale.</summary>
        RepeatPerSegment = 3,
        /// <summary>Trails do not change the texture coordinates of existing points when they add or remove points.</summary>
        Static = 4,
    }
}
