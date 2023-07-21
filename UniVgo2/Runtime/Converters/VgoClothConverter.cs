// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Converters
// @Class     : VgoClothConverter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Converters
{
    using NewtonVgo;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    /// <summary>
    /// VGO Cloth Converter
    /// </summary>
    public class VgoClothConverter
    {
        /// <summary>
        /// Create VgoCloth from Cloth.
        /// </summary>
        /// <param name="cloth"></param>
        /// <param name="geometryCoordinate"></param>
        /// <param name="colliderList"></param>
        /// <param name="vgoStorage"></param>
        /// <returns></returns>
        public static VgoCloth CreateFrom(in Cloth cloth, in VgoGeometryCoordinate geometryCoordinate, IList<Collider> colliderList, in IVgoStorage vgoStorage)
        {
            var vgoCloth = new VgoCloth()
            {
                name = cloth.name,
                enabled = cloth.enabled,
                stretchingStiffness = cloth.stretchingStiffness,
                bendingStiffness = cloth.bendingStiffness,
                useTethers = cloth.useTethers,
                useGravity = cloth.useGravity,
                damping = cloth.damping,
                externalAcceleration = cloth.externalAcceleration.ToNullableNumericsVector3(Vector3.zero, geometryCoordinate),
                randomAcceleration = cloth.randomAcceleration.ToNullableNumericsVector3(Vector3.zero, geometryCoordinate),
                worldVelocityScale = cloth.worldVelocityScale,
                worldAccelerationScale = cloth.worldAccelerationScale,
                friction = cloth.friction,
                collisionMassScale = cloth.collisionMassScale,
                enableContinuousCollision = cloth.enableContinuousCollision,
                useVirtualParticles = cloth.useVirtualParticles,
                clothSolverFrequency = cloth.clothSolverFrequency,
                stiffnessFrequency = cloth.stiffnessFrequency,
                selfCollisionDistance = cloth.selfCollisionDistance,
                selfCollisionStiffness = cloth.selfCollisionStiffness,
                sleepThreshold = cloth.sleepThreshold,
            };

            vgoCloth.sphereColliders = cloth.sphereColliders
                .Select(x => (x.first is null) && (x.second is null) ? null :
                new VgoClothSphereColliderPair()
                {
                    first = (x.first is null) ? -1 : colliderList.IndexOf(x.first),
                    second = (x.second is null) ? -1 : colliderList.IndexOf(x.second),
                })
                .ToList();

            vgoCloth.capsuleColliders = cloth.capsuleColliders
                .Select(x => (x is null) ? -1 : colliderList.IndexOf(x))
                .ToList();

            if (cloth.coefficients.Any())
            {
                vgoCloth.coefficients = vgoStorage.AddAccessorWithoutSparse(cloth.coefficients, VgoResourceAccessorDataType.Vector2Float, VgoResourceAccessorKind.ClothCoefficients);
            }
            else
            {
                vgoCloth.coefficients = -1;
            }

            return vgoCloth;
        }

        /// <summary>
        /// Create VgoCloth from Cloth.
        /// </summary>
        /// <param name="cloth"></param>
        /// <param name="geometryCoordinate"></param>
        /// <param name="colliderList"></param>
        /// <param name="vgoStorage"></param>
        /// <returns></returns>
        public static VgoCloth? CreateOrDefaultFrom(in Cloth? cloth, in VgoGeometryCoordinate geometryCoordinate, IList<Collider> colliderList, in IVgoStorage vgoStorage)
        {
            if (cloth == null)
            {
                return default;
            }

            return CreateFrom(cloth, geometryCoordinate, colliderList, vgoStorage);
        }

        /// <summary>
        /// Set Cloth field value.
        /// </summary>
        /// <param name="cloth"></param>
        /// <param name="vgoCloth"></param>
        /// <param name="geometryCoordinate"></param>
        /// <param name="colliderList"></param>
        /// <param name="vgoStorage"></param>
        public static void SetComponentValue(Cloth cloth, in VgoCloth vgoCloth, in VgoGeometryCoordinate geometryCoordinate, List<Collider?> colliderList, in IVgoStorage vgoStorage)
        {
            //cloth.enabled = vgoCloth.enabled;
            cloth.enabled = false;

            cloth.stretchingStiffness = vgoCloth.stretchingStiffness;
            cloth.bendingStiffness = vgoCloth.bendingStiffness;
            cloth.useTethers = vgoCloth.useTethers;
            cloth.useGravity = vgoCloth.useGravity;
            cloth.damping = vgoCloth.damping;
            cloth.externalAcceleration = vgoCloth.externalAcceleration.ToUnityVector3(Vector3.zero, geometryCoordinate);
            cloth.randomAcceleration = vgoCloth.randomAcceleration.ToUnityVector3(Vector3.zero, geometryCoordinate);
            cloth.worldVelocityScale = vgoCloth.worldVelocityScale;
            cloth.worldAccelerationScale = vgoCloth.worldAccelerationScale;
            cloth.friction = vgoCloth.friction;
            cloth.collisionMassScale = vgoCloth.collisionMassScale;
            cloth.enableContinuousCollision = vgoCloth.enableContinuousCollision;
            cloth.useVirtualParticles = vgoCloth.useVirtualParticles;
            cloth.clothSolverFrequency = vgoCloth.clothSolverFrequency;
            cloth.stiffnessFrequency = vgoCloth.stiffnessFrequency;
            cloth.selfCollisionDistance = vgoCloth.selfCollisionDistance;
            cloth.selfCollisionStiffness = vgoCloth.selfCollisionStiffness;
            cloth.sleepThreshold = vgoCloth.sleepThreshold;

            // sphereColliders
            if (vgoCloth.sphereColliders != null)
            {
                List<ClothSphereColliderPair> sphereColliderPairList = new List<ClothSphereColliderPair>(vgoCloth.sphereColliders.Count);

                bool sphereResult = true;

                for (int index = 0; index < vgoCloth.sphereColliders.Count; index++)
                {
                    VgoClothSphereColliderPair? vgoSphereColliderPair = vgoCloth.sphereColliders[index];

                    if (vgoSphereColliderPair is null)
                    {
                        // @notice
                        sphereColliderPairList.Add(new ClothSphereColliderPair());

                        continue;
                    }

                    SphereCollider? firstSphereCollider;
                    SphereCollider? secondSphereCollider;

                    if (vgoSphereColliderPair.first == -1)
                    {
                        firstSphereCollider = null;
                    }
                    else
                    {
                        Collider? firstCollider = colliderList.GetNullableValueOrDefault(vgoSphereColliderPair.first);

                        if (firstCollider is null)
                        {
                            Debug.LogWarningFormat("{0}[{1}].first is not found in list. value: {2}", nameof(VgoCloth.sphereColliders), index, vgoSphereColliderPair.first);

                            sphereResult = false;

                            firstSphereCollider = null;
                        }
                        else if (firstCollider is SphereCollider sphereCollider)
                        {
                            firstSphereCollider = sphereCollider;
                        }
                        else
                        {
                            Debug.LogWarningFormat("{0}[{1}].first is not SphereCollider. name: {2}", nameof(VgoCloth.sphereColliders), index, firstCollider.name);

                            sphereResult = false;

                            firstSphereCollider = null;
                        }
                    }

                    if (vgoSphereColliderPair.second == -1)
                    {
                        secondSphereCollider = null;
                    }
                    else
                    {
                        Collider? secondCollider = colliderList.GetNullableValueOrDefault(vgoSphereColliderPair.second);

                        if (secondCollider is null)
                        {
                            Debug.LogWarningFormat("{0}[{1}].second is not found in list. value: {2}", nameof(VgoCloth.sphereColliders), index, vgoSphereColliderPair.second);

                            sphereResult = false;

                            secondSphereCollider = null;
                        }
                        else if (secondCollider is SphereCollider sphereCollider)
                        {
                            secondSphereCollider = sphereCollider;
                        }
                        else
                        {
                            Debug.LogWarningFormat("{0}[{1}].second is not SphereCollider. name: {2}", nameof(VgoCloth.sphereColliders), index, secondCollider.name);

                            sphereResult = false;

                            secondSphereCollider = null;
                        }
                    }

                    sphereColliderPairList.Add(new ClothSphereColliderPair(firstSphereCollider, secondSphereCollider));
                }

                if (sphereResult == true)
                {
                    cloth.sphereColliders = sphereColliderPairList.ToArray();
                }
                else
                {
                    // @notice
                }
            }

            // capsuleColliders
            if (vgoCloth.capsuleColliders != null)
            {
                List<CapsuleCollider?> capsuleColliderList = new List<CapsuleCollider?>(vgoCloth.capsuleColliders.Count);

                bool capsuleResult = true;

                for (int index = 0; index < vgoCloth.capsuleColliders.Count; index++)
                {
                    int idx = vgoCloth.capsuleColliders[index];

                    if (idx == -1)
                    {
                        // @notice
                        capsuleColliderList.Add(null);

                        continue;
                    }

                    Collider? collider = colliderList.GetNullableValueOrDefault(idx);

                    if (collider is null)
                    {
                        Debug.LogWarningFormat("{0}[{1}] is not found in list. value: {2}", nameof(VgoCloth.capsuleColliders), index, idx);

                        capsuleResult = false;
                    }
                    else if (collider is CapsuleCollider capsuleCollider)
                    {
                        capsuleColliderList.Add(capsuleCollider);
                    }
                    else
                    {
                        Debug.LogWarningFormat("{0}[{1}] is not CapsuleCollider. name: {2}", nameof(VgoCloth.capsuleColliders), index, collider.name);

                        capsuleResult = false;
                    }
                }

                if (capsuleResult == true)
                {
                    cloth.capsuleColliders = capsuleColliderList.ToArray();
                }
                else
                {
                    // @notice
                }
            }

            // coefficients
            if (vgoCloth.coefficients > -1)
            {
                cloth.coefficients = vgoStorage.GetAccessorArrayData<ClothSkinningCoefficient>(vgoCloth.coefficients);
            }

            cloth.enabled = vgoCloth.enabled;
        }
    }
}