// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : VgoKeyframeConverter
// ----------------------------------------------------------------------
namespace UniVgo
{
    using UniGLTFforUniVgo;
    using UnityEngine;

    /// <summary>
    /// VGO Keyframe Converter
    /// </summary>
    public class VgoKeyframeConverter
    {
        /// <summary>
        /// Create VGO_Keyframe from Keyframe.
        /// </summary>
        /// <param name="keyframe"></param>
        /// <returns></returns>
        public static VGO_Keyframe CreateFrom(Keyframe keyframe)
        {
            return new VGO_Keyframe()
            {
                time = keyframe.time,
                value = keyframe.value,
                inTangent = keyframe.inTangent,
                outTangent = keyframe.outTangent,
                inWeight = keyframe.inWeight,
                outWeight = keyframe.outWeight,
                weightedMode = keyframe.weightedMode,
            };
        }

        /// <summary>
        /// Create Keyframe from VGO_Keyframe.
        /// </summary>
        /// <param name="vgoKeyframe"></param>
        /// <returns></returns>
        public static Keyframe CreateKeyframe(VGO_Keyframe vgoKeyframe)
        {
            if (vgoKeyframe == null)
            {
                return default;
            }

            switch (vgoKeyframe.weightedMode)
            {
                case WeightedMode.None:
                    return new Keyframe(
                        time: vgoKeyframe.time,
                        value: vgoKeyframe.value,
                        inTangent: vgoKeyframe.inTangent,
                        outTangent: vgoKeyframe.outTangent
                    );

                case WeightedMode.In:
                case WeightedMode.Out:
                case WeightedMode.Both:
                    return new Keyframe(
                        time: vgoKeyframe.time,
                        value: vgoKeyframe.value,
                        inTangent: vgoKeyframe.inTangent,
                        outTangent: vgoKeyframe.outTangent,
                        inWeight: vgoKeyframe.inWeight,
                        outWeight: vgoKeyframe.outWeight
                    );

                default:
                    return new Keyframe(
                        time: vgoKeyframe.time,
                        value: vgoKeyframe.value
                    );
            }
        }
    }
}