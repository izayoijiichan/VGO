// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Converters
// @Class     : VgoAnimationClipConverter
// ----------------------------------------------------------------------
namespace UniVgo2.Converters
{
    using NewtonVgo;
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using UnityEngine;

    /// <summary>
    /// VGO Animation Clip Converter
    /// </summary>
    public class VgoAnimationClipConverter
    {
        /// <summary>
        /// Create VgoAnimationClip from AnimationClip.
        /// </summary>
        /// <param name="animationClip"></param>
        /// <param name="geometryCoordinate"></param>
        /// <returns></returns>
        public static VgoAnimationClip CreateFrom(AnimationClip animationClip, VgoGeometryCoordinate geometryCoordinate)
        {
            if (animationClip == null)
            {
                return null;
            }

            var vgoAnimationClip = new VgoAnimationClip()
            {
                name = animationClip.name,
                legacy = animationClip.legacy,
                localBounds = VgoBoundsConverter.CreateFrom(animationClip.localBounds, geometryCoordinate),
                wrapMode = (NewtonVgo.WrapMode)animationClip.wrapMode,
            };

#if UNITY_EDITOR
            // Animation Curve Binding
            UnityEditor.EditorCurveBinding[] curveBindings = UnityEditor.AnimationUtility.GetCurveBindings(animationClip);

            if ((curveBindings != null) && curveBindings.Any())
            {
                vgoAnimationClip.curveBindings = new List<VgoAnimationCurveBinding>(curveBindings.Length);

                foreach (var curveBinding in curveBindings)
                {
                    AnimationCurve animationCurve = UnityEditor.AnimationUtility.GetEditorCurve(animationClip, curveBinding);

                    var vgoCurveBinding = new VgoAnimationCurveBinding();

                    if (curveBinding.type == typeof(UnityEngine.Transform))
                    {
                        vgoCurveBinding.type = nameof(UnityEngine.Transform);
                    }
                    else if (curveBinding.type == typeof(UnityEngine.MeshRenderer))
                    {
                        vgoCurveBinding.type = nameof(UnityEngine.MeshRenderer);
                    }
                    else
                    {
                        vgoCurveBinding.type = curveBinding.type.ToString();
                    }

                    if (curveBinding.propertyName.StartsWith("m_LocalPosition") ||
                        curveBinding.propertyName.StartsWith("m_LocalRotation") ||
                        curveBinding.propertyName.StartsWith("m_LocalScale"))
                    {
                        vgoCurveBinding.propertyName = curveBinding.propertyName.Remove(0, 2);
                    }
                    else
                    {
                        vgoCurveBinding.propertyName = curveBinding.propertyName;
                    }

                    vgoCurveBinding.animationCurve = VgoAnimationCurveConverter.CreateFrom(animationCurve);

                    vgoAnimationClip.curveBindings.Add(vgoCurveBinding);
                }
            }
#endif

            return vgoAnimationClip;
        }

        /// <summary>
        /// Create VgoAnimationClip from VgoAnimationClip.
        /// </summary>
        /// <param name="vgoAnimationClip"></param>
        /// <param name="geometryCoordinate"></param>
        public static AnimationClip CreateAnimationClip(VgoAnimationClip vgoAnimationClip, VgoGeometryCoordinate geometryCoordinate)
        {
            if (vgoAnimationClip == null)
            {
                return null;
            }

            var animationClip = new AnimationClip()
            {
                name = vgoAnimationClip.name,
                legacy = vgoAnimationClip.legacy,
                localBounds = VgoBoundsConverter.CreateBounds(vgoAnimationClip.localBounds, geometryCoordinate),
                wrapMode = (UnityEngine.WrapMode)vgoAnimationClip.wrapMode,
            };

            // Animation Curve Binding
            if ((vgoAnimationClip.curveBindings != null) && vgoAnimationClip.curveBindings.Any())
            {
                try
                {
                    foreach (VgoAnimationCurveBinding curveBinding in vgoAnimationClip.curveBindings)
                    {
                        if (string.IsNullOrEmpty(curveBinding.type))
                        {
                            continue;
                        }

                        Type type;

                        if (curveBinding.type == nameof(UnityEngine.Transform))
                        {
                            type = typeof(UnityEngine.Transform);
                        }
                        else if (curveBinding.type == nameof(UnityEngine.MeshRenderer))
                        {
                            type = typeof(UnityEngine.MeshRenderer);
                        }
                        else
                        {
                            type = Type.GetType(curveBinding.type);
                        }

                        if (type == null)
                        {
                            continue;
                        }

                        if (string.IsNullOrEmpty(curveBinding.propertyName))
                        {
                            continue;
                        }

                        string propertyName;

                        if (curveBinding.propertyName.StartsWith("LocalPosition") ||
                            curveBinding.propertyName.StartsWith("LocalRotation") ||
                            curveBinding.propertyName.StartsWith("LocalScale"))
                        {
                            propertyName = "m_" + curveBinding.propertyName;
                        }
                        else
                        {
                            propertyName = curveBinding.propertyName;
                        }

                        AnimationCurve animationCurve = VgoAnimationCurveConverter.CreateAnimationCurve(curveBinding.animationCurve);

                        if (animationCurve == null)
                        {
                            continue;
                        }

                        animationClip.SetCurve(relativePath: string.Empty, type, propertyName, animationCurve);
                    }
                }
                catch
                {
                    animationClip.ClearCurves();
                }
            }

            return animationClip;
        }
    }
}