// ----------------------------------------------------------------------
// @Namespace : UniVgo.Converters
// @Class     : VgoBlendShapeConverter
// ----------------------------------------------------------------------
namespace UniVgo.Converters
{
    using NewtonGltf;
    using System.Collections.Generic;

    /// <summary>
    /// VGO BlendShape Converter
    /// </summary>
    public class VgoBlendShapeConverter
    {
        /// <summary>
        /// Create VGO_BlendShape from BlendShapeInfo.
        /// </summary>
        /// <param name="blendShapeInfo"></param>
        /// <returns></returns>
        public static VGO_BlendShape CreateFrom(BlendShapeConfiguration blendShapeInfo)
        {
            if (blendShapeInfo == null)
            {
                return null;
            }

            var gltfVgoBlendShape = new VGO_BlendShape()
            {
                name = blendShapeInfo.name,
                kind = blendShapeInfo.kind,
                faceParts = blendShapeInfo.faceParts,
                blinks = blendShapeInfo.blinks,
                visemes = blendShapeInfo.visemes,
                presets = new List<VGO_BlendShapePreset>()
            };

            if (blendShapeInfo.presets != null)
            {
                foreach (BlendShapePreset preset in blendShapeInfo.presets)
                {
                    var gltfVgoPreset = new VGO_BlendShapePreset
                    {
                        name = preset.name,
                        type = preset.type,
                        bindings = new List<VGO_BlendShapeBinding>()
                    };

                    foreach (BlendShapeBinding binding in preset.bindings)
                    {
                        var gltfBinding = new VGO_BlendShapeBinding
                        {
                            index = binding.index,
                            weight = binding.weight,
                        };

                        gltfVgoPreset.bindings.Add(gltfBinding);
                    }

                    gltfVgoBlendShape.presets.Add(gltfVgoPreset);
                }
            }

            return gltfVgoBlendShape;
        }
    }
}