// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Converters
// @Class     : VgoKeyframeConverter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Converters
{
    using NewtonVgo;
    using UnityEngine;

    /// <summary>
    /// VGO Keyframe Converter
    /// </summary>
    public class VgoKeyframeConverter
    {
        /// <summary>
        /// Create VgoKeyframe from Keyframe.
        /// </summary>
        /// <param name="keyframe"></param>
        /// <returns></returns>
        public static VgoKeyframe CreateFrom(Keyframe keyframe)
        {
            return new VgoKeyframe()
            {
                time = keyframe.time,
                value = keyframe.value,
                inTangent = keyframe.inTangent,
                outTangent = keyframe.outTangent,
                inWeight = keyframe.inWeight,
                outWeight = keyframe.outWeight,
                weightedMode = (NewtonVgo.WeightedMode)keyframe.weightedMode,
            };
        }

        /// <summary>
        /// Create Keyframe from VgoKeyframe.
        /// </summary>
        /// <param name="vgoKeyframe"></param>
        /// <returns></returns>
        public static Keyframe CreateKeyframe(VgoKeyframe vgoKeyframe)
        {
            if (vgoKeyframe == null)
            {
                return default;
            }

            switch (vgoKeyframe.weightedMode)
            {
                case NewtonVgo.WeightedMode.None:
                    return new Keyframe(
                        time: vgoKeyframe.time,
                        value: vgoKeyframe.value,
                        inTangent: vgoKeyframe.inTangent,
                        outTangent: vgoKeyframe.outTangent
                    );

                case NewtonVgo.WeightedMode.In:
                case NewtonVgo.WeightedMode.Out:
                case NewtonVgo.WeightedMode.Both:
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