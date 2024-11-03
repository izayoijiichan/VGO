// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Converters
// @Class     : VgoParticleSystemCustomDataConverter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Converters
{
    using NewtonVgo.Schema.ParticleSystems;
    using static UnityEngine.ParticleSystem;

    /// <summary>
    /// VGO ParticleSystem CustomData Converter
    /// </summary>
    public class VgoParticleSystemCustomDataConverter
    {
        /// <summary>
        /// Create VGO_PS_CustomDataModule from CustomDataModule.
        /// </summary>
        /// <param name="customDataModule"></param>
        /// <returns></returns>
        public static VGO_PS_CustomDataModule CreateFrom(in CustomDataModule customDataModule)
        {
            VGO_PS_CustomData vgoCustomData1 = CreateFrom(customDataModule, UnityEngine.ParticleSystemCustomData.Custom1);
            VGO_PS_CustomData vgoCustomData2 = CreateFrom(customDataModule, UnityEngine.ParticleSystemCustomData.Custom2);

            return new VGO_PS_CustomDataModule()
            {
                enabled = customDataModule.enabled,
                customData = new VGO_PS_CustomData[]
                {
                    vgoCustomData1,
                    vgoCustomData2,
                },
            };
        }

        /// <summary>
        /// Create VGO_PS_CustomData from CustomDataModule.
        /// </summary>
        /// <param name="customDataModule"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        protected static VGO_PS_CustomData CreateFrom(in CustomDataModule customDataModule, UnityEngine.ParticleSystemCustomData stream)
        {
            UnityEngine.ParticleSystemCustomDataMode mode = customDataModule.GetMode(stream);

            var customData = new VGO_PS_CustomData()
            {
                stream = (NewtonVgo.ParticleSystemCustomData)stream,
                mode = (NewtonVgo.ParticleSystemCustomDataMode)mode,
            };

            if (mode == UnityEngine.ParticleSystemCustomDataMode.Vector)
            {
                int vectorComponentCount = customDataModule.GetVectorComponentCount(stream);

                if (vectorComponentCount > 0)
                {
                    customData.vector = new VGO_PS_MinMaxCurve[vectorComponentCount];

                    // X, Y, Z, W
                    for (int componentIndex = 0; componentIndex < vectorComponentCount; componentIndex++)
                    {
                        MinMaxCurve componentCurve = customDataModule.GetVector(stream, componentIndex);

                        VGO_PS_MinMaxCurve vgoComponentCurve = VgoParticleSystemMinMaxCurveConverter.CreateFrom(componentCurve);

                        customData.vector[componentIndex] = vgoComponentCurve;
                    }
                }
            }
            else if (mode == UnityEngine.ParticleSystemCustomDataMode.Color)
            {
                MinMaxGradient colorGradient = customDataModule.GetColor(stream);

                VGO_PS_MinMaxGradient vgoColorGradient = VgoParticleSystemMinMaxGradientConverter.CreateFrom(colorGradient);

                customData.color = vgoColorGradient;
            }

            return customData;
        }
    }
}