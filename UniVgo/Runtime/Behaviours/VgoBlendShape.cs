// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : VgoBlendShape
// ----------------------------------------------------------------------
namespace UniVgo
{
    using NewtonGltf;
    using System.Linq;
    using UnityEngine;
    using VgoGltf;

    /// <summary>
    /// VGO BlendShape
    /// </summary>
    [DisallowMultipleComponent]
    public class VgoBlendShape : MonoBehaviour
    {
        /// <summary>Skinned Mesh Renderer</summary>
        private SkinnedMeshRenderer _SkinnedMeshRenderer = null;

        /// <summary>BlendShape Configuration</summary>
        public BlendShapeConfiguration BlendShapeConfiguration;

        /// <summary>
        /// Called when an instance of the script is loaded.
        /// </summary>
        private void Awake()
        {
            _SkinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

            if (_SkinnedMeshRenderer == null)
            {
                Debug.LogWarning("SkinnedMeshRenderer component is not attached.");
            }
        }

        /// <summary>
        /// Gets the weight of a BlendShape for this Renderer.
        /// </summary>
        /// <param name="index">The index of the BlendShape whose weight you want to retrieve.</param>
        /// <returns>The weight of the BlendShape.</returns>
        public float SetBlendShapeWeight(int index)
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

            VGO_BlendShapeBlink vgoBlink = BlendShapeConfiguration.blinks.Where(x => x.type == blinkType).FirstOrDefault();

            if (vgoBlink == null)
            {
                return;
            }

            _SkinnedMeshRenderer.SetBlendShapeWeight(vgoBlink.index, value);
        }

        /// <summary>
        /// Sets the weight of a BlendShape using viseme.
        /// </summary>
        /// <param name="viseme">The viseme.</param>
        /// <param name="value">The weight for this BlendShape.</param>
        public void SetBlendShapeWeight(VgoBlendShapeViseme viseme, float value)
        {
            if (_SkinnedMeshRenderer == null)
            {
                return;
            }

            if (BlendShapeConfiguration == null)
            {
                return;
            }

            VGO_BlendShapeViseme vgoViseme = BlendShapeConfiguration.visemes.Where(x => x.type == viseme).FirstOrDefault();

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

            BlendShapePreset preset = BlendShapeConfiguration.presets.Where(x => x.type == presetType).FirstOrDefault();

            if (preset == null)
            {
                return;
            }

            foreach (BlendShapeBinding binding in preset.bindings)
            {
                if (ignoreEyelid || ignoreMouth)
                {
                    if (ignoreEyelid)
                    {
                        var blink = BlendShapeConfiguration.blinks.Where(x => x.index == binding.index).FirstOrDefault();

                        if (blink != null)
                        {
                            continue;
                        }
                    }

                    if (ignoreMouth)
                    {
                        var visume = BlendShapeConfiguration.visemes.Where(x => x.index == binding.index).FirstOrDefault();

                        if (visume != null)
                        {
                            continue;
                        }
                    }

                    var parts = BlendShapeConfiguration.faceParts.Where(x => x.index == binding.index).FirstOrDefault();

                    if (parts != null)
                    {
                        if (parts.type == VgoBlendShapeFacePartsType.Eyelid)
                        {
                            if (ignoreEyelid)
                            {
                                continue;
                            }
                        }
                        else if (parts.type == VgoBlendShapeFacePartsType.Mouth)
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