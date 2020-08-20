// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : 
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    /// <summary>The collision detection mode constants used for Rigidbody.collisionDetectionMode.</summary>
    public enum CollisionDetectionMode
    {
        /// <summary>Continuous collision detection is off for this Rigidbody.</summary>
        Discrete = 0,
        /// <summary>Continuous collision detection is on for colliding with static mesh geometry.</summary>
        Continuous = 1,
        /// <summary>Continuous collision detection is on for colliding with static and dynamic geometry.</summary>
        ContinuousDynamic = 2,
        /// <summary>Speculative continuous collision detection is on for static and dynamic geometries.</summary>
        ContinuousSpeculative = 3,
    }

    /// <summary>Describes how physics materials of the colliding objects are combined.</summary>
    public enum PhysicMaterialCombine
    {
        /// <summary>Averages the friction/bounce of the two colliding materials.</summary>
        Average = 0,
        /// <summary>Multiplies the friction/bounce of the two colliding materials.</summary>
        Multiply = 1,
        /// <summary>Uses the smaller friction/bounce of the two colliding materials.</summary>
        Minimum = 2,
        /// <summary>Uses the larger friction/bounce of the two colliding materials.</summary>
        Maximum = 3,
    }

    /// <summary>Use these flags to constrain motion of Rigidbodies.</summary>
    /// <remarks>[Flag]</remarks>
    public enum RigidbodyConstraints
    {
        /// <summary>No constraints.</summary>
        None = 0,
        /// <summary>Freeze motion along the X-axis.</summary>
        FreezePositionX = 2,
        /// <summary>Freeze motion along the Y-axis.</summary>
        FreezePositionY = 4,
        /// <summary>Freeze motion along the Z-axis.</summary>
        FreezePositionZ = 8,
        /// <summary>Freeze motion along all axes.</summary>
        FreezePosition = 14,
        /// <summary>Freeze rotation along the X-axis.</summary>
        FreezeRotationX = 16,
        /// <summary>Freeze rotation along the Y-axis.</summary>
        FreezeRotationY = 32,
        /// <summary>Freeze rotation along the Z-axis.</summary>
        FreezeRotationZ = 64,
        /// <summary>Freeze rotation along all axes.</summary>
        FreezeRotation = 112,
        /// <summary>Freeze rotation and motion along all axes.</summary>
        FreezeAll = 126
    }

    /// <summary>Rigidbody interpolation mode.</summary>
    public enum RigidbodyInterpolation
    {
        /// <summary>No Interpolation.</summary>
        None = 0,
        /// <summary>Interpolation will always lag a little bit behind but can be smoother than extrapolation.</summary>
        Interpolate = 1,
        /// <summary>Extrapolation will predict the position of the rigidbody based on the current velocity.</summary>
        Extrapolate = 2,
    }
}
