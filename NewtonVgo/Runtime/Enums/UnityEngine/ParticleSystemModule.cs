// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : 
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    /// <summary></summary>
    public enum ParticleSystemAnimationMode
    {
        /// <summary></summary>
        Grid = 0,
        /// <summary></summary>
        Sprites = 1,
    }

    /// <summary></summary>
    public enum ParticleSystemAnimationRowMode
    {
        /// <summary></summary>
        Custom = 0,
        /// <summary></summary>
        Random = 1,
        /// <summary></summary>
        MeshIndex = 2,
    }

    /// <summary></summary>
    public enum ParticleSystemAnimationTimeMode
    {
        /// <summary></summary>
        Lifetime = 0,
        /// <summary></summary>
        Speed = 1,
        /// <summary></summary>
        FPS = 2,
    }

    /// <summary></summary>
    public enum ParticleSystemAnimationType
    {
        /// <summary></summary>
        WholeSheet = 0,
        /// <summary></summary>
        SingleRow = 1,
    }

    /// <summary></summary>
    public enum ParticleSystemCollisionMode
    {
        /// <summary></summary>
        Collision3D = 0,
        /// <summary></summary>
        Collision2D = 1,
    }

    /// <summary></summary>
    public enum ParticleSystemCollisionQuality
    {
        /// <summary></summary>
        High = 0,
        /// <summary></summary>
        Medium = 1,
        /// <summary></summary>
        Low = 2,
    }

    /// <summary></summary>
    public enum ParticleSystemCollisionType
    {
        /// <summary></summary>
        Planes = 0,
        /// <summary></summary>
        World = 1,
    }

    /// <summary></summary>
    public enum ParticleSystemCullingMode
    {
        /// <summary></summary>
        Automatic = 0,
        /// <summary></summary>
        PauseAndCatchup = 1,
        /// <summary></summary>
        Pause = 2,
        /// <summary></summary>
        AlwaysSimulate = 3,
    }

    /// <summary></summary>
    public enum ParticleSystemCurveMode
    {
        /// <summary></summary>
        Constant = 0,
        /// <summary></summary>
        Curve = 1,
        /// <summary></summary>
        TwoCurves = 2,
        /// <summary></summary>
        TwoConstants = 3,
    }

    /// <summary></summary>
    public enum ParticleSystemEmitterVelocityMode
    {
        /// <summary></summary>
        Transform = 0,
        /// <summary></summary>
        Rigidbody = 1,
    }

    /// <summary></summary>
    public enum ParticleSystemGameObjectFilter
    {
        /// <summary></summary>
        LayerMask = 0,
        /// <summary></summary>
        List = 1,
        /// <summary></summary>
        LayerMaskAndList = 2,
    }

    /// <summary></summary>
    public enum ParticleSystemGradientMode
    {
        /// <summary></summary>
        Color = 0,
        /// <summary></summary>
        Gradient = 1,
        /// <summary></summary>
        TwoColors = 2,
        /// <summary></summary>
        TwoGradients = 3,
        /// <summary></summary>
        RandomColor = 4,
    }

    /// <summary></summary>
    public enum ParticleSystemInheritVelocityMode
    {
        /// <summary></summary>
        Initial = 0,
        /// <summary></summary>
        Current = 1,
    }

    /// <summary></summary>
    public enum ParticleSystemMeshShapeType
    {
        /// <summary></summary>
        Vertex = 0,
        /// <summary></summary>
        Edge = 1,
        /// <summary></summary>
        Triangle = 2,
    }

    /// <summary></summary>
    public enum ParticleSystemNoiseQuality
    {
        /// <summary></summary>
        Low = 0,
        /// <summary></summary>
        Medium = 1,
        /// <summary></summary>
        High = 2,
    }

    /// <summary></summary>
    public enum ParticleSystemOverlapAction
    {
        /// <summary></summary>
        Ignore = 0,
        /// <summary></summary>
        Kill = 1,
        /// <summary></summary>
        Callback = 2,
    }

    /// <summary></summary>
    public enum ParticleSystemRenderMode
    {
        /// <summary></summary>
        Billboard = 0,
        /// <summary></summary>
        Stretch = 1,
        /// <summary></summary>
        HorizontalBillboard = 2,
        /// <summary></summary>
        VerticalBillboard = 3,
        /// <summary></summary>
        Mesh = 4,
        /// <summary></summary>
        None = 5,
    }

    /// <summary></summary>
    public enum ParticleSystemRenderSpace
    {
        /// <summary></summary>
        View = 0,
        /// <summary></summary>
        World = 1,
        /// <summary></summary>
        Local = 2,
        /// <summary></summary>
        Facing = 3,
        /// <summary></summary>
        Velocity = 4,
    }

    /// <summary></summary>
    public enum ParticleSystemRingBufferMode
    {
        /// <summary></summary>
        Disabled = 0,
        /// <summary></summary>
        PauseUntilReplaced = 1,
        /// <summary></summary>
        LoopUntilReplaced = 2,
    }

    /// <summary></summary>
    public enum ParticleSystemScalingMode
    {
        /// <summary></summary>
        Hierarchy = 0,
        /// <summary></summary>
        Local = 1,
        /// <summary></summary>
        Shape = 2,
    }

    /// <summary></summary>
    public enum ParticleSystemShapeMultiModeValue
    {
        /// <summary></summary>
        Random = 0,
        /// <summary></summary>
        Loop = 1,
        /// <summary></summary>
        PingPong = 2,
        /// <summary></summary>
        BurstSpread = 3,
    }

    /// <summary></summary>
    public enum ParticleSystemShapeTextureChannel
    {
        /// <summary></summary>
        Red = 0,
        /// <summary></summary>
        Green = 1,
        /// <summary></summary>
        Blue = 2,
        /// <summary></summary>
        Alpha = 3,
    }

    /// <summary></summary>
    public enum ParticleSystemShapeType
    {
        /// <summary></summary>
        Sphere = 0,
        /// <summary></summary>
        SphereShell = 1,
        /// <summary></summary>
        Hemisphere = 2,
        /// <summary></summary>
        HemisphereShell = 3,
        /// <summary></summary>
        Cone = 4,
        /// <summary></summary>
        Box = 5,
        /// <summary></summary>
        Mesh = 6,
        /// <summary></summary>
        ConeShell = 7,
        /// <summary></summary>
        ConeVolume = 8,
        /// <summary></summary>
        ConeVolumeShell = 9,
        /// <summary></summary>
        Circle = 10,
        /// <summary></summary>
        CircleEdge = 11,
        /// <summary></summary>
        SingleSidedEdge = 12,
        /// <summary></summary>
        MeshRenderer = 13,
        /// <summary></summary>
        SkinnedMeshRenderer = 14,
        /// <summary></summary>
        BoxShell = 15,
        /// <summary></summary>
        BoxEdge = 16,
        /// <summary></summary>
        Donut = 17,
        /// <summary></summary>
        Rectangle = 18,
        /// <summary></summary>
        Sprite = 19,
        /// <summary></summary>
        SpriteRenderer = 20,
    }

    /// <summary></summary>
    public enum ParticleSystemSimulationSpace
    {
        /// <summary></summary>
        Local = 0,
        /// <summary></summary>
        World = 1,
        /// <summary></summary>
        Custom = 2,
    }

    /// <summary></summary>
    public enum ParticleSystemSortMode
    {
        /// <summary></summary>
        None = 0,
        /// <summary></summary>
        Distance = 1,
        /// <summary></summary>
        OldestInFront = 2,
        /// <summary></summary>
        YoungestInFront = 3,
    }

    /// <summary></summary>
    public enum ParticleSystemStopAction
    {
        /// <summary></summary>
        None = 0,
        /// <summary></summary>
        Disable = 1,
        /// <summary></summary>
        Destroy = 2,
        /// <summary></summary>
        Callback = 3,
    }

    /// <summary></summary>
    public enum ParticleSystemTrailMode
    {
        /// <summary></summary>
        PerParticle = 0,
        /// <summary></summary>
        Ribbon = 1,
    }

    /// <summary></summary>
    public enum ParticleSystemTrailTextureMode
    {
        /// <summary></summary>
        Stretch = 0,
        /// <summary></summary>
        Tile = 1,
        /// <summary></summary>
        DistributePerSegment = 2,
        /// <summary></summary>
        RepeatPerSegment = 3,
    }
}
