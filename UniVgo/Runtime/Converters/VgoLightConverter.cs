// ----------------------------------------------------------------------
// @Namespace : UniVgo.Converters
// @Class     : VgoLightConverter
// ----------------------------------------------------------------------
namespace UniVgo.Converters
{
    using NewtonGltf;
    using UnityEngine;
    using VgoGltf;

    /// <summary>
    /// VGO Light Converter
    /// </summary>
    public class VgoLightConverter
    {
        /// <summary>
        /// Create glTFNode_VGO_Light from Light.
        /// </summary>
        /// <param name="light"></param>
        /// <returns></returns>
        public static VGO_Light CreateFrom(Light light)
        {
            if (light == null)
            {
                return null;
            }

            var vgoLight = new VGO_Light()
            {
                enabled = light.enabled,
                type = (VgoGltf.LightType)light.type,
                color = light.color.linear.ToGltfColor4(),
                intensity = light.intensity,
                bounceIntensity = light.bounceIntensity,
                renderMode = (VgoGltf.LightRenderMode)light.renderMode,
                cullingMask = light.cullingMask,
            };

            // Lightmap Bake Type
#if UNITY_EDITOR
            vgoLight.lightmapBakeType = (VgoGltf.LightmapBakeType)light.lightmapBakeType;
#else
            switch (vgoLight.type)
            {
                case VgoGltf.LightType.Spot:
                case VgoGltf.LightType.Directional:
                case VgoGltf.LightType.Point:
                    vgoLight.lightmapBakeType = VgoGltf.LightmapBakeType.Realtime;
                    break;
                case VgoGltf.LightType.Rectangle:
                case VgoGltf.LightType.Disc:
                    vgoLight.lightmapBakeType = VgoGltf.LightmapBakeType.Baked;
                    break;
                default:
                    break;
            }
#endif
            switch (light.type)
            {
                case UnityEngine.LightType.Spot:
                    vgoLight.shape = (VgoGltf.LightShape)light.shape;
                    vgoLight.range = light.range;
                    vgoLight.spotAngle = light.spotAngle;
                    break;
                case UnityEngine.LightType.Point:
                    vgoLight.range = light.range;
                    break;
#if UNITY_EDITOR
                case UnityEngine.LightType.Rectangle:
                    vgoLight.areaSize = light.areaSize.ToNumericsVector2();
                    break;
                case UnityEngine.LightType.Disc:
                    vgoLight.areaRadius = light.areaSize.x;
                    break;
#endif
                default:
                    break;
            }

            vgoLight.shadows = (VgoGltf.LightShadows)light.shadows;

#if UNITY_EDITOR
            // Baked Shadows
            if ((light.lightmapBakeType == UnityEngine.LightmapBakeType.Baked) ||
                (light.lightmapBakeType == UnityEngine.LightmapBakeType.Mixed))
            {
                if (light.shadows == UnityEngine.LightShadows.Soft)
                {
                    switch (light.type)
                    {
                        case UnityEngine.LightType.Spot:
                        case UnityEngine.LightType.Point:
                            vgoLight.shadowRadius = light.shadowRadius;
                            break;
                        case UnityEngine.LightType.Directional:
                            vgoLight.shadowAngle = light.shadowAngle;
                            break;
                        default:
                            break;
                    }
                }
            }
#endif

            // Realtime Shadows
#if UNITY_EDITOR
            if ((light.lightmapBakeType == UnityEngine.LightmapBakeType.Realtime) ||
                (light.lightmapBakeType == UnityEngine.LightmapBakeType.Mixed))
#endif
            {
                if ((light.type == UnityEngine.LightType.Directional) ||
                    (light.type == UnityEngine.LightType.Point))
                {
                    vgoLight.shadowStrength = light.shadowStrength;
                    vgoLight.shadowResolution = (LightShadowResolution)light.shadowResolution;
                    vgoLight.shadowBias = light.shadowBias;
                    vgoLight.shadowNormalBias = light.shadowNormalBias;
                    vgoLight.shadowNearPlane = light.shadowNearPlane;
                }
            }

            return vgoLight;
        }

        /// <summary>
        /// Set Light parameter.
        /// </summary>
        /// <param name="light"></param>
        /// <param name="vgoLight"></param>
        public static void SetComponentValue(Light light, VGO_Light vgoLight)
        {
            if (light == null)
            {
                return;
            }

            if (vgoLight == null)
            {
                return;
            }

            switch (vgoLight.type)
            {
                case VgoGltf.LightType.Spot:
                case VgoGltf.LightType.Directional:
                case VgoGltf.LightType.Point:
                case VgoGltf.LightType.Rectangle:
                case VgoGltf.LightType.Disc:
                    break;
                default:
                    return;
            }

            light.enabled = vgoLight.enabled;
            light.type = (UnityEngine.LightType)vgoLight.type;
            light.color = vgoLight.color.GetValueOrDefault(Color4.White).ToUnityColor().gamma;
            light.intensity = vgoLight.intensity;
            light.bounceIntensity = vgoLight.bounceIntensity;
            light.renderMode = (UnityEngine.LightRenderMode)vgoLight.renderMode;
            light.cullingMask = vgoLight.cullingMask;

#if UNITY_EDITOR
            light.lightmapBakeType = (UnityEngine.LightmapBakeType)vgoLight.lightmapBakeType;
#endif

            switch (vgoLight.type)
            {
                case VgoGltf.LightType.Spot:
                    light.shape = (UnityEngine.LightShape)vgoLight.shape;
                    light.range = vgoLight.range;
                    light.spotAngle = vgoLight.spotAngle;
                    break;
                case VgoGltf.LightType.Point:
                    light.range = vgoLight.range;
                    break;
#if UNITY_EDITOR
                case VgoGltf.LightType.Rectangle:
                    light.areaSize = vgoLight.areaSize.GetValueOrDefault(System.Numerics.Vector2.Zero).ToUnityVector2();
                    break;
                case VgoGltf.LightType.Disc:
                    light.areaSize = new Vector2(vgoLight.areaRadius, 1.0f);
                    break;
#endif
                default:
                    break;
            }

            light.shadows = (UnityEngine.LightShadows)vgoLight.shadows;

#if UNITY_EDITOR
            // Baked Shadows
            if ((vgoLight.lightmapBakeType == VgoGltf.LightmapBakeType.Baked) ||
                (vgoLight.lightmapBakeType == VgoGltf.LightmapBakeType.Mixed))
            {
                if (vgoLight.shadows == VgoGltf.LightShadows.Soft)
                {
                    switch (vgoLight.type)
                    {
                        case VgoGltf.LightType.Spot:
                        case VgoGltf.LightType.Point:
                            light.shadowRadius = vgoLight.shadowRadius;
                            break;
                        case VgoGltf.LightType.Directional:
                            light.shadowAngle = vgoLight.shadowAngle;
                            break;
                        default:
                            break;
                    }
                }
            }
#endif
            // Realtime Shadows
            if ((vgoLight.lightmapBakeType == VgoGltf.LightmapBakeType.Realtime) ||
                (vgoLight.lightmapBakeType == VgoGltf.LightmapBakeType.Mixed))
            {
                if ((vgoLight.type == VgoGltf.LightType.Directional) ||
                    (vgoLight.type == VgoGltf.LightType.Point))
                {
                    light.shadowStrength = vgoLight.shadowStrength;
                    light.shadowResolution = (UnityEngine.Rendering.LightShadowResolution)vgoLight.shadowResolution;
                    light.shadowBias = vgoLight.shadowBias;
                    light.shadowNormalBias = vgoLight.shadowNormalBias;
                    light.shadowNearPlane = vgoLight.shadowNearPlane;
                }
            }
        }
    }
}