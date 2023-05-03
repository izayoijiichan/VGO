﻿// ----------------------------------------------------------------------
// @Namespace : UniVgo2
// @Class     : VgoBlendShape
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2
{
    using NewtonVgo;
    using System.Linq;
    using UnityEngine;

    /// <summary>
    /// VGO BlendShape
    /// </summary>
    [AddComponentMenu("Vgo/Vgo Blend Shape")]
    [DisallowMultipleComponent]
    public class VgoBlendShape : MonoBehaviour
    {
        /// <summary>Skinned Mesh Renderer</summary>
        private SkinnedMeshRenderer? _SkinnedMeshRenderer = null;

        /// <summary>BlendShape Configuration</summary>
        public BlendShapeConfiguration? BlendShapeConfiguration = null;

        /// <summary>
        /// Called when an instance of the script is loaded.
        /// </summary>
        private void Awake()
        {
#nullable disable
            if (gameObject.TryGetComponentEx(out _SkinnedMeshRenderer) == false)
            {
                Debug.LogWarning($"{nameof(SkinnedMeshRenderer)} component is not attached.");
            }
#nullable enable
        }

        /// <summary>
        /// Gets the index of a BlendShape for this Renderer.
        /// </summary>
        /// <param name="blinkType"></param>
        /// <returns></returns>
        public int GetVgoBlendShapeIndex(VgoBlendShapeBlinkType blinkType)
        {
            if (blinkType == VgoBlendShapeBlinkType.None)
            {
                return -1;
            }

            if (BlendShapeConfiguration == null)
            {
                return -1;
            }

            BlendShapeBlink? blendShapeBlink = BlendShapeConfiguration.blinks.FirstOrDefault(x => x.type == blinkType);

            if (blendShapeBlink == null)
            {
                return -1;
            }

            return blendShapeBlink.index;
        }

        /// <summary>
        /// Gets the index of a BlendShape for this Renderer.
        /// </summary>
        /// <param name="visemeType"></param>
        /// <returns></returns>
        public int GetVgoBlendShapeIndex(VgoBlendShapeVisemeType visemeType)
        {
            if (visemeType == VgoBlendShapeVisemeType.None)
            {
                return -1;
            }

            if (BlendShapeConfiguration == null)
            {
                return -1;
            }

            BlendShapeViseme? blendShapeViseme = BlendShapeConfiguration.visemes.FirstOrDefault(x => x.type == visemeType);

            if (blendShapeViseme == null)
            {
                return -1;
            }

            return blendShapeViseme.index;
        }

        /// <summary>
        /// Gets the weight of a BlendShape for this Renderer.
        /// </summary>
        /// <param name="index">The index of the BlendShape whose weight you want to retrieve.</param>
        /// <returns>The weight of the BlendShape.</returns>
        public float GetBlendShapeWeight(int index)
        {
            if (_SkinnedMeshRenderer == null)
            {
                return 0.0f;
            }

            return _SkinnedMeshRenderer.GetBlendShapeWeight(index);
        }

        /// <summary>
        /// Sets the weight of a BlendShape for this Renderer.
        /// </summary>
        /// <param name="index">The index of the BlendShape to modify.</param>
        /// <param name="value">The weight for this BlendShape.</param>
        public void SetBlendShapeWeight(int index, float value)
        {
            if (_SkinnedMeshRenderer == null)
            {
                return;
            }

            _SkinnedMeshRenderer.SetBlendShapeWeight(index, value);
        }

        /// <summary>
        /// Sets the weight of a BlendShape using blinkType.
        /// </summary>
        /// <param name="blinkType">The type of blink.</param>
        /// <param name="value">The weight for this BlendShape.</param>
        public void SetBlendShapeWeight(VgoBlendShapeBlinkType blinkType, float value)
        {
            if (_SkinnedMeshRenderer == null)
            {
                return;
            }

            if (BlendShapeConfiguration == null)
            {
                return;
            }

            BlendShapeBlink? vgoBlink = BlendShapeConfiguration.blinks.FirstOrDefault(x => x.type == blinkType);

            if (vgoBlink == null)
            {
                return;
            }

            _SkinnedMeshRenderer.SetBlendShapeWeight(vgoBlink.index, value);
        }

        /// <summary>
        /// Sets the weight of a BlendShape using viseme.
        /// </summary>
        /// <param name="visemeType">The type of viseme.</param>
        /// <param name="value">The weight for this BlendShape.</param>
        public void SetBlendShapeWeight(VgoBlendShapeVisemeType visemeType, float value)
        {
            if (_SkinnedMeshRenderer == null)
            {
                return;
            }

            if (BlendShapeConfiguration == null)
            {
                return;
            }

            BlendShapeViseme? vgoViseme = BlendShapeConfiguration.visemes.FirstOrDefault(x => x.type == visemeType);

            if (vgoViseme == null)
            {
                return;
            }

            _SkinnedMeshRenderer.SetBlendShapeWeight(vgoViseme.index, value);
        }

        /// <summary>
        /// Sets the weight of a BlendShape using preset.
        /// </summary>
        /// <param name="presetType">The preset type.</param>
        /// <param name="ignoreEyelid">Whether to ignore the eyelids.</param>
        /// <param name="ignoreMouth">Whether to ignore the mouth.</param>
        public void SetBlendShapeWeight(VgoBlendShapePresetType presetType, bool ignoreEyelid = false, bool ignoreMouth = false)
        {
            if (_SkinnedMeshRenderer == null)
            {
                return;
            }

            if (BlendShapeConfiguration == null)
            {
                return;
            }

            VgoMeshBlendShapePreset? preset = BlendShapeConfiguration.presets.FirstOrDefault(x => x.type == presetType);

            if (preset == null)
            {
                return;
            }

            foreach (VgoMeshBlendShapeBinding binding in preset.bindings)
            {
                if (ignoreEyelid || ignoreMouth)
                {
                    if (ignoreEyelid)
                    {
                        BlendShapeBlink? blink = BlendShapeConfiguration.blinks.FirstOrDefault(x => x.index == binding.index);

                        if (blink != null)
                        {
                            continue;
                        }
                    }

                    if (ignoreMouth)
                    {
                        BlendShapeViseme? visume = BlendShapeConfiguration.visemes.FirstOrDefault(x => x.index == binding.index);

                        if (visume != null)
                        {
                            continue;
                        }
                    }

                    BlendShapeFacePart? facePart = BlendShapeConfiguration.faceParts.FirstOrDefault(x => x.index == binding.index);

                    if (facePart != null)
                    {
                        if (facePart.type == VgoBlendShapeFacePartsType.Eyelid)
                        {
                            if (ignoreEyelid)
                            {
                                continue;
                            }
                        }
                        else if (facePart.type == VgoBlendShapeFacePartsType.Mouth)
                        {
                            if (ignoreMouth)
                            {
                                continue;
                            }
                        }
                    }
                }

                _SkinnedMeshRenderer.SetBlendShapeWeight(binding.index, binding.weight);
            }
        }
    }
}