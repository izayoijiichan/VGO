// ----------------------------------------------------------------------
// @Namespace : UniVgo
// @Class     : VgoParticleSystemBurstConverter
// ----------------------------------------------------------------------
namespace UniVgo
{
    using UniGLTFforUniVgo;
    using static UnityEngine.ParticleSystem;

    /// <summary>
    /// VGO ParticleSystem Burst Converter
    /// </summary>
    public class VgoParticleSystemBurstConverter
    {
        /// <summary>
        /// Create VGO_PS_Burst from Burst.
        /// </summary>
        /// <param name="minMaxCurve"></param>
        /// <returns></returns>
        public static VGO_PS_Burst CreateFrom(Burst burst)
        {
            return new VGO_PS_Burst()
            {
                time = burst.time,
                count = VgoParticleSystemMinMaxCurveConverter.CreateFrom(burst.count),
                cycleCount = burst.cycleCount,
                repeatInterval = burst.repeatInterval,
                probability = burst.probability,
            };
        }

        /// <summary>
        /// Create Burst from VGO_PS_Burst.
        /// </summary>
        /// <param name="vgoBurst"></param>
        /// <returns></returns>
        public static Burst CreateBurst(VGO_PS_Burst vgoBurst)
        {
            if (vgoBurst == null)
            {
                return default;
            }

            float time = 0.0f;

            if (0.0f <= vgoBurst.time)
            {
                time = vgoBurst.time;
            }

            MinMaxCurve count = default;

            if (vgoBurst.count != null)
            {
                count = VgoParticleSystemMinMaxCurveConverter.CreateMinMaxCurve(vgoBurst.count);
            }

            int cycleCount = 0;

            if (0 <= vgoBurst.cycleCount)
            {
                cycleCount = vgoBurst.cycleCount;
            }

            float repeatInterval = 0.010f;

            if (0.010f <= vgoBurst.repeatInterval)
            {
                repeatInterval = vgoBurst.repeatInterval;
            }

            float probability = 1.0f;

            if ((0.0f <= vgoBurst.probability) && (vgoBurst.probability <= 1.0f))
            {
                probability = vgoBurst.probability;
            }

            Burst burst = new Burst(
                _time: time,
                _count: count,
                _cycleCount: cycleCount,
                _repeatInterval: repeatInterval
            );

            burst.probability = probability;

            return burst;
        }
    }
}