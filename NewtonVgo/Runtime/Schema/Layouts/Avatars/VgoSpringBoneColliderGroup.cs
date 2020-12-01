// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoSpringBoneColliderGroup
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// VGO Spring Bone Collider Group
    /// </summary>
    [Serializable]
    public class VgoSpringBoneColliderGroup
    {
        /// <summary>An array of the srping bone collider.</summary>
        [JsonProperty("colliders")]
        public VgoSpringBoneCollider[] colliders;

        /// <summary>The Gizmo color.</summary>
        [JsonProperty("gizmoColor")]
        public Color3 gizmoColor = new Color3(1.0f, 0.0f, 1.0f);
    }
}