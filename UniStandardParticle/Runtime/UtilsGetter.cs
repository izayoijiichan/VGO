// ----------------------------------------------------------------------
// @Namespace : UniStandardParticle
// @Class     : Utils
// ----------------------------------------------------------------------
namespace UniStandardParticle
{
    using UnityEngine;
    using UnityEngine.Rendering;

    public static partial class Utils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        public static ParticleDefinition GetParticleParametersFromMaterial(Material material)
        {
            return new ParticleDefinition
            {
                RenderMode = GetBlendMode(material),
                ColorMode = GetColorMode(material),
                FlipBookMode = GetFlipBookMode(material),
                CullMode = GetCullMode(material),
                SoftParticlesEnabled = GetInt(material, PropSoftParticlesEnabled) == 1,
                SoftParticleFadeParams = GetVector(material, PropSoftParticleFadeParams),
                CameraFadingEnabled = GetInt(material, PropCameraFadingEnabled) == 1,
                CameraFadeParams = GetVector(material, PropCameraFadeParams),
                DistortionEnabled = GetInt(material, PropDistortionEnabled) == 1,
                GrabTexture = GetTexture(material, PropGrabTexture),
                DistortionStrengthScaled = GetFloat(material, PropDistortionStrengthScaled),
                DistortionBlend = GetFloat(material, PropDistortionBlend),
                ColorAddSubDiff = GetColor(material, PropColorAddSubDiff),
                MainTex = GetTexture(material, PropMainTex),
                MainTexSt = GetVector(material, PropMainTexSt),
                Color = GetColor(material, PropColor),
                Cutoff = GetFloat(material, PropCutoff),
                MetallicGlossMap = GetTexture(material, PropMetallicGlossMap),
                Metallic = GetFloat(material, PropMetallic),
                Glossiness = GetFloat(material, PropGlossiness),
                BumpMap = GetTexture(material, PropBumpMap),
                BumpScale = material.GetFloat(PropBumpScale),
                LightingEnabled = (material.shader.name.Contains("Unlit") == false),
                EmissionEnabled = material.IsKeywordEnabled(KeyEmission),
                EmissionColor = GetColor(material, PropEmissionColor),
                EmissionMap = GetTexture(material, PropEmissionMap),
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private static Color GetColor(Material material, string propertyName)
        {
            if (material.HasProperty(propertyName))
            {
                return material.GetColor(propertyName);
            }
            else
            {
                return default;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private static float GetFloat(Material material, string propertyName)
        {
            if (material.HasProperty(propertyName))
            {
                return material.GetFloat(propertyName);
            }
            else
            {
                return 0.0f;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private static int GetInt(Material material, string propertyName)
        {
            if (material.HasProperty(propertyName))
            {
                return material.GetInt(propertyName);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private static Texture2D GetTexture(Material material, string propertyName)
        {
            if (material.HasProperty(propertyName))
            {
                return (Texture2D)material.GetTexture(propertyName);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private static Vector4 GetVector(Material material, string propertyName)
        {
            if (material.HasProperty(propertyName))
            {
                return material.GetVector(propertyName);
            }
            else
            {
                return Vector4.zero;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        private static BlendMode GetBlendMode(Material material)
        {
            BlendMode blendMode = BlendMode.Opaque;

            if (material.IsKeywordEnabled(KeyAlphaTestOn))
            {
                blendMode = BlendMode.Cutout;
            }
            else if (material.IsKeywordEnabled(KeyAlphaModulateOn))
            {
                blendMode = BlendMode.Modulate;
            }
            else if (material.IsKeywordEnabled(KeyAlphaPremultiplyOn))
            {
                blendMode = BlendMode.Transparent;
            }
            else if (material.IsKeywordEnabled(KeyAlphaBlendOn))
            {
                Color colorAddSubDiff = GetColor(material, PropColorAddSubDiff);

                if (colorAddSubDiff == new Color(1.0f, 0.0f, 0.0f, 0.0f))
                {
                    blendMode = BlendMode.Additive;
                }
                else if (colorAddSubDiff == new Color(-1.0f, 0.0f, 0.0f, 0.0f))
                {
                    blendMode = BlendMode.Subtractive;
                }
                else
                {
                    blendMode = BlendMode.Fade;
                }
            }

            return blendMode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        private static ColorMode GetColorMode(Material material)
        {
            ColorMode colorMode = ColorMode.Multiply;

            if (material.IsKeywordEnabled(KeyColorOverlayOn))
            {
                colorMode = ColorMode.Overlay;
            }
            else if (material.IsKeywordEnabled(KeyColorColorOn))
            {
                colorMode = ColorMode.Color;
            }
            else if (material.IsKeywordEnabled(KeyColorAddSubDiffOn))
            {
                Color colorAddSubDiff = GetColor(material, PropColorAddSubDiff);

                if (colorAddSubDiff == new Color(1.0f, 0.0f, 0.0f, 0.0f))
                {
                    colorMode = ColorMode.Additive;
                }
                else if (colorAddSubDiff == new Color(-1.0f, 0.0f, 0.0f, 0.0f))
                {
                    colorMode = ColorMode.Subtractive;
                }
                else if (colorAddSubDiff == new Color(-1.0f, 1.0f, 0.0f, 0.0f))
                {
                    colorMode = ColorMode.Difference;
                }
                else
                {
                    colorMode = ColorMode.Additive;
                }
            }

            return colorMode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        private static FlipBookMode GetFlipBookMode(Material material)
        {
            if (GetInt(material, PropFlipbookMode) == 1)
            {
                return FlipBookMode.Blended;
            }
            else
            {
                return FlipBookMode.Simple;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        private static CullMode GetCullMode(Material material)
        {
            if (material.IsKeywordEnabled(KeyTwoSided))
            {
                return CullMode.Off;
            }
            else
            {
                return CullMode.Back;
            }
        }
    }
}