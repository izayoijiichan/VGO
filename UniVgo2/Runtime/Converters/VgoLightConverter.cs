// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Converters
// @Class     : VgoLightConverter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Converters
{
    using NewtonVgo;
    using UnityEngine;

    /// <summary>
    /// VGO Light Converter
    /// </summary>
    public class VgoLightConverter
    {
        /// <summary>
        /// Create VgoLight from Light.
        /// </summary>
        /// <param name="light"></param>
        /// <returns></returns>
        public static VgoLight CreateFrom(in Light light)
        {
            var vgoLight = new VgoLight()
            {
                enabled = light.enabled,
                color = light.color.linear.ToVgoColor4(),
                intensity = light.intensity,
                bounceIntensity = light.bounceIntensity,
                renderMode = (NewtonVgo.LightRenderMode)light.renderMode,
                cullingMask = light.cullingMask,
            };

            // Type and Shape
#if UNITY_2023_2_OR_NEWER
            switch (light.type)
            {
                case UnityEngine.LightType.Spot:
                    vgoLight.type = NewtonVgo.LightType.Spot;
                    vgoLight.shape = NewtonVgo.LightShape.Cone;
                    break;
                case UnityEngine.LightType.Directional:
                    vgoLight.type = NewtonVgo.LightType.Directional;
                    break;
                case UnityEngine.LightType.Point:
                    vgoLight.type = NewtonVgo.LightType.Point;
                    break;
                case UnityEngine.LightType.Rectangle:
                    vgoLight.type = NewtonVgo.LightType.Rectangle;
                    break;
                case UnityEngine.LightType.Disc:
                    vgoLight.type = NewtonVgo.LightType.Disc;
                    break;
                case UnityEngine.LightType.Pyramid:
                    vgoLight.type = NewtonVgo.LightType.Spot;
                    vgoLight.shape = NewtonVgo.LightShape.Pyramid;
                    break;
                case UnityEngine.LightType.Box:
                    vgoLight.type = NewtonVgo.LightType.Spot;
                    vgoLight.shape = NewtonVgo.LightShape.Box;
                    break;
                case UnityEngine.LightType.Tube:
                    vgoLight.type = NewtonVgo.LightType.Spot;
                    //vgoLight.shape = NewtonVgo.LightShape.Tube;
                    break;
                default:
                    break;
            }
#else
            vgoLight.type = (NewtonVgo.LightType)light.type;

            if (light.type == UnityEngine.LightType.Spot)
            {
                vgoLight.shape = (NewtonVgo.LightShape)light.shape;
            }
#endif

            switch (light.type)
            {
                case UnityEngine.LightType.Spot:
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
#if UNITY_2023_2_OR_NEWER
                case UnityEngine.LightType.Pyramid:
                case UnityEngine.LightType.Box:
                case UnityEngine.LightType.Tube:
                    vgoLight.range = light.range;
                    vgoLight.spotAngle = light.spotAngle;
                    break;
#endif
                default:
                    break;
            }

            // Lightmap Bake Type
#if UNITY_EDITOR
            vgoLight.lightmapBakeType = (NewtonVgo.LightmapBakeType)light.lightmapBakeType;
#else
            switch (light.type)
            {
                case UnityEngine.LightType.Spot:
                case UnityEngine.LightType.Directional:
                case UnityEngine.LightType.Point:
                    vgoLight.lightmapBakeType = NewtonVgo.LightmapBakeType.Realtime;
                    break;
                case UnityEngine.LightType.Rectangle:
                case UnityEngine.LightType.Disc:
                    vgoLight.lightmapBakeType = NewtonVgo.LightmapBakeType.Baked;
                    break;
#if UNITY_2023_2_OR_NEWER
                case UnityEngine.LightType.Pyramid:
                case UnityEngine.LightType.Box:
                case UnityEngine.LightType.Tube:
                    vgoLight.lightmapBakeType = NewtonVgo.LightmapBakeType.Realtime;
                    break;
#endif
                default:
                    break;
            }
#endif

            vgoLight.shadows = (NewtonVgo.LightShadows)light.shadows;

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
#if UNITY_2023_2_OR_NEWER
                        case UnityEngine.LightType.Pyramid:
                        case UnityEngine.LightType.Box:
                        case UnityEngine.LightType.Tube:
                            vgoLight.shadowRadius = light.shadowRadius;
                            break;
#endif
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
        /// Create VgoLight from Light.
        /// </summary>
        /// <param name="light"></param>
        /// <returns></returns>
        public static VgoLight? CreateOrDefaultFrom(in Light? light)
        {
            if (light == null)
            {
                return default;
            }

            return CreateFrom(light);
        }

        /// <summary>
        /// Set Light parameter.
        /// </summary>
        /// <param name="light"></param>
        /// <param name="vgoLight"></param>
        public static void SetComponentValue(Light light, in VgoLight vgoLight)
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
                case NewtonVgo.LightType.Spot:
                case NewtonVgo.LightType.Directional:
                case NewtonVgo.LightType.Point:
                case NewtonVgo.LightType.Rectangle:
                case NewtonVgo.LightType.Disc:
                    break;
                default:
                    return;
            }

            light.enabled = vgoLight.enabled;
            light.color = vgoLight.color.GetValueOrDefault(Color4.White).ToUnityColor().gamma;
            light.intensity = vgoLight.intensity;
            light.bounceIntensity = vgoLight.bounceIntensity;
            light.renderMode = (UnityEngine.LightRenderMode)vgoLight.renderMode;
            light.cullingMask = vgoLight.cullingMask;

            switch (vgoLight.type)
            {
                case NewtonVgo.LightType.Spot:
#if UNITY_2023_2_OR_NEWER
                    switch (vgoLight.shape)
                    {
                        case NewtonVgo.LightShape.Cone:
                            light.type = UnityEngine.LightType.Spot;
                            break;
                        case NewtonVgo.LightShape.Pyramid:
                            light.type = UnityEngine.LightType.Pyramid;
                            break;
                        case NewtonVgo.LightShape.Box:
                            light.type = UnityEngine.LightType.Box;
                            break;
                        default:
                            break;
                    }
#else
                    light.type = UnityEngine.LightType.Spot;
                    light.shape = (UnityEngine.LightShape)vgoLight.shape;
#endif
                    light.range = vgoLight.range;
                    light.spotAngle = vgoLight.spotAngle;
                    break;
                case NewtonVgo.LightType.Point:
                    light.type = UnityEngine.LightType.Point;
                    light.range = vgoLight.range;
                    break;
                case NewtonVgo.LightType.Rectangle:
                    light.type = UnityEngine.LightType.Rectangle;
#if UNITY_2023_2_OR_NEWER && !UNITY_EDITOR  // Unity 2023.2 or higher && Runtime
                    //
#else
                    light.areaSize = vgoLight.areaSize.GetValueOrDefault(System.Numerics.Vector2.Zero).ToUnityVector2();
#endif
                    break;
                case NewtonVgo.LightType.Disc:
                    light.type = UnityEngine.LightType.Disc;
#if UNITY_2023_2_OR_NEWER && !UNITY_EDITOR  // Unity 2023.2 or higher && Runtime
                    //
#else
                    light.areaSize = new Vector2(vgoLight.areaRadius, 1.0f);
#endif
                    break;
                default:
                    break;
            }

#if UNITY_EDITOR
            light.lightmapBakeType = (UnityEngine.LightmapBakeType)vgoLight.lightmapBakeType;
#endif

            light.shadows = (UnityEngine.LightShadows)vgoLight.shadows;

#if UNITY_EDITOR
            // Baked Shadows
            if ((vgoLight.lightmapBakeType == NewtonVgo.LightmapBakeType.Baked) ||
                (vgoLight.lightmapBakeType == NewtonVgo.LightmapBakeType.Mixed))
            {
                if (vgoLight.shadows == NewtonVgo.LightShadows.Soft)
                {
                    switch (vgoLight.type)
                    {
                        case NewtonVgo.LightType.Spot:
                        case NewtonVgo.LightType.Point:
                            light.shadowRadius = vgoLight.shadowRadius;
                            break;
                        case NewtonVgo.LightType.Directional:
                            light.shadowAngle = vgoLight.shadowAngle;
                            break;
                        default:
                            break;
                    }
                }
            }
#endif
            // Realtime Shadows
            if ((vgoLight.lightmapBakeType == NewtonVgo.LightmapBakeType.Realtime) ||
                (vgoLight.lightmapBakeType == NewtonVgo.LightmapBakeType.Mixed))
            {
                if ((vgoLight.type == NewtonVgo.LightType.Directional) ||
                    (vgoLight.type == NewtonVgo.LightType.Point))
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