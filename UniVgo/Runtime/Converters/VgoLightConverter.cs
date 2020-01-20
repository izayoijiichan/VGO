// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : VgoLightConverter
// ----------------------------------------------------------------------
namespace UniVgo
{
    using UniGLTFforUniVgo;
    using UnityEngine;

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
                type = light.type,
                color = light.color.linear.ToArray(),
                intensity = light.intensity,
                bounceIntensity = light.bounceIntensity,
                renderMode = light.renderMode,
                cullingMask = light.cullingMask,
            };

            // Lightmap Bake Type
#if UNITY_EDITOR
            vgoLight.lightmapBakeType = light.lightmapBakeType;
#else
            switch (vgoLight.type)
            {
                case LightType.Spot:
                case LightType.Directional:
                case LightType.Point:
                    vgoLight.lightmapBakeType = LightmapBakeType.Realtime;
                    break;
                case LightType.Rectangle:
                case LightType.Disc:
                    vgoLight.lightmapBakeType = LightmapBakeType.Baked;
                    break;
                default:
                    break;
            }
#endif
            switch (light.type)
            {
                case LightType.Spot:
                    vgoLight.shape = light.shape;
                    vgoLight.range = light.range;
                    vgoLight.spotAngle = light.spotAngle;
                    break;
                case LightType.Point:
                    vgoLight.range = light.range;
                    break;
#if UNITY_EDITOR
                case LightType.Rectangle:
                    vgoLight.areaSize = light.areaSize.ToArray();
                    break;
                case LightType.Disc:
                    vgoLight.areaRadius = light.areaSize.x;
                    break;
#endif
                default:
                    break;
            }

            vgoLight.shadows = light.shadows;

#if UNITY_EDITOR
            // Baked Shadows
            if ((light.lightmapBakeType == LightmapBakeType.Baked) ||
                (light.lightmapBakeType == LightmapBakeType.Mixed))
            {
                if (light.shadows == LightShadows.Soft)
                {
                    switch (light.type)
                    {
                        case LightType.Spot:
                        case LightType.Point:
                            vgoLight.shadowRadius = light.shadowRadius;
                            break;
                        case LightType.Directional:
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
            if ((light.lightmapBakeType == LightmapBakeType.Realtime) ||
                (light.lightmapBakeType == LightmapBakeType.Mixed))
#endif
            {
                if ((light.type == LightType.Directional) ||
                    (light.type == LightType.Point))
                {
                    vgoLight.shadowStrength = light.shadowStrength;
                    vgoLight.shadowResolution = light.shadowResolution;
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
                case LightType.Spot:
                case LightType.Directional:
                case LightType.Point:
                case LightType.Rectangle:
                case LightType.Disc:
                    break;
                default:
                    return;
            }

            light.enabled = vgoLight.enabled;
            light.type = vgoLight.type;
            light.color = ArrayConverter.ToColor(vgoLight.color, gamma: true);
            light.intensity = vgoLight.intensity;
            light.bounceIntensity = vgoLight.bounceIntensity;
            light.renderMode = vgoLight.renderMode;
            light.cullingMask = vgoLight.cullingMask;

#if UNITY_EDITOR
            light.lightmapBakeType = vgoLight.lightmapBakeType;
#endif

            switch (vgoLight.type)
            {
                case LightType.Spot:
                    light.shape = vgoLight.shape;
                    light.range = vgoLight.range;
                    light.spotAngle = vgoLight.spotAngle;
                    break;
                case LightType.Point:
                    light.range = vgoLight.range;
                    break;
#if UNITY_EDITOR
                case LightType.Rectangle:
                    light.areaSize = ArrayConverter.ToVector2(vgoLight.areaSize);
                    break;
                case LightType.Disc:
                    light.areaSize = new Vector2(vgoLight.areaRadius, 1.0f);
                    break;
#endif
                default:
                    break;
            }

            light.shadows = vgoLight.shadows;

#if UNITY_EDITOR
            // Baked Shadows
            if ((vgoLight.lightmapBakeType == LightmapBakeType.Baked) ||
                (vgoLight.lightmapBakeType == LightmapBakeType.Mixed))
            {
                if (vgoLight.shadows == LightShadows.Soft)
                {
                    switch (vgoLight.type)
                    {
                        case LightType.Spot:
                        case LightType.Point:
                            light.shadowRadius = vgoLight.shadowRadius;
                            break;
                        case LightType.Directional:
                            light.shadowAngle = vgoLight.shadowAngle;
                            break;
                        default:
                            break;
                    }
                }
            }
#endif
            // Realtime Shadows
            if ((vgoLight.lightmapBakeType == LightmapBakeType.Realtime) ||
                (vgoLight.lightmapBakeType == LightmapBakeType.Mixed))
            {
                if ((vgoLight.type == LightType.Directional) ||
                    (vgoLight.type == LightType.Point))
                {
                    light.shadowStrength = vgoLight.shadowStrength;
                    light.shadowResolution = vgoLight.shadowResolution;
                    light.shadowBias = vgoLight.shadowBias;
                    light.shadowNormalBias = vgoLight.shadowNormalBias;
                    light.shadowNearPlane = vgoLight.shadowNearPlane;
                }
            }
        }
    }
}