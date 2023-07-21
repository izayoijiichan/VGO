// ----------------------------------------------------------------------
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
        public int GetVgoBlendShapeIndex(in VgoBlendShapeBlinkType blinkType)
        {
            if (blinkType == VgoBlendShapeBlinkType.None)
            {
                return -1;
            }

            if (BlendShapeConfiguration == null)
            {
                return -1;
            }

            var type = blinkType;

            BlendShapeBlink? blendShapeBlink = BlendShapeConfiguration.Blinks.FirstOrDefault(x => x.Type == type);

            if (blendShapeBlink == null)
            {
                return -1;
            }

            return blendShapeBlink.Index;
        }

        /// <summary>
        /// Gets the index of a BlendShape for this Renderer.
        /// </summary>
        /// <param name="visemeType"></param>
        /// <returns></returns>
        public int GetVgoBlendShapeIndex(in VgoBlendShapeVisemeType visemeType)
        {
            if (visemeType == VgoBlendShapeVisemeType.None)
            {
                return -1;
            }

            if (BlendShapeConfiguration == null)
            {
                return -1;
            }

            var type = visemeType;

            BlendShapeViseme? blendShapeViseme = BlendShapeConfiguration.Visemes.FirstOrDefault(x => x.Type == type);

            if (blendShapeViseme == null)
            {
                return -1;
            }

            return blendShapeViseme.Index;
        }

        /// <summary>
        /// Gets the weight of a BlendShape for this Renderer.
        /// </summary>
        /// <param name="index">The index of the BlendShape whose weight you want to retrieve.</param>
        /// <returns>The weight of the BlendShape.</returns>
        public float GetBlendShapeWeight(in int index)
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
        public void SetBlendShapeWeight(in int index, in float value)
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
        public void SetBlendShapeWeight(in VgoBlendShapeBlinkType blinkType, in float value)
        {
            if (_SkinnedMeshRenderer == null)
            {
                return;
            }

            if (BlendShapeConfiguration == null)
            {
                return;
            }

            var type = blinkType;

            BlendShapeBlink? vgoBlink = BlendShapeConfiguration.Blinks.FirstOrDefault(x => x.Type == type);

            if (vgoBlink == null)
            {
                return;
            }

            _SkinnedMeshRenderer.SetBlendShapeWeight(vgoBlink.Index, value);
        }

        /// <summary>
        /// Sets the weight of a BlendShape using viseme.
        /// </summary>
        /// <param name="visemeType">The type of viseme.</param>
        /// <param name="value">The weight for this BlendShape.</param>
        public void SetBlendShapeWeight(in VgoBlendShapeVisemeType visemeType, in float value)
        {
            if (_SkinnedMeshRenderer == null)
            {
                return;
            }

            if (BlendShapeConfiguration == null)
            {
                return;
            }

            var type = visemeType;

            BlendShapeViseme? vgoViseme = BlendShapeConfiguration.Visemes.FirstOrDefault(x => x.Type == type);

            if (vgoViseme == null)
            {
                return;
            }

            _SkinnedMeshRenderer.SetBlendShapeWeight(vgoViseme.Index, value);
        }

        /// <summary>
        /// Sets the weight of a BlendShape using preset.
        /// </summary>
        /// <param name="presetType">The preset type.</param>
        /// <param name="ignoreEyelid">Whether to ignore the eyelids.</param>
        /// <param name="ignoreMouth">Whether to ignore the mouth.</param>
        public void SetBlendShapeWeight(in VgoBlendShapePresetType presetType, in bool ignoreEyelid = false, in bool ignoreMouth = false)
        {
            if (_SkinnedMeshRenderer == null)
            {
                return;
            }

            if (BlendShapeConfiguration == null)
            {
                return;
            }

            var type = presetType;

            VgoMeshBlendShapePreset? preset = BlendShapeConfiguration.Presets.FirstOrDefault(x => x.type == type);

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
                        BlendShapeBlink? blink = BlendShapeConfiguration.Blinks.FirstOrDefault(x => x.Index == binding.index);

                        if (blink != null)
                        {
                            continue;
                        }
                    }

                    if (ignoreMouth)
                    {
                        BlendShapeViseme? visume = BlendShapeConfiguration.Visemes.FirstOrDefault(x => x.Index == binding.index);

                        if (visume != null)
                        {
                            continue;
                        }
                    }

                    BlendShapeFacePart? facePart = BlendShapeConfiguration.FaceParts.FirstOrDefault(x => x.Index == binding.index);

                    if (facePart != null)
                    {
                        if (facePart.Type == VgoBlendShapeFacePartsType.Eyelid)
                        {
                            if (ignoreEyelid)
                            {
                                continue;
                            }
                        }
                        else if (facePart.Type == VgoBlendShapeFacePartsType.Mouth)
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