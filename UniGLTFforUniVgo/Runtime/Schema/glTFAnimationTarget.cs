using Newtonsoft.Json;
using System;

namespace UniGLTFforUniVgo
{
    /// <summary>
    /// The index of the node and TRS property that an animation channel targets.
    /// </summary>
    [Serializable]
    [JsonObject("animation.sampler")]
    public class glTFAnimationTarget
    {
        /// <summary></summary>
        [JsonProperty("node")]
        //[JsonSchema(Minimum = 0)]
        public int node;

        /// <summary></summary>
        [JsonProperty("path", Required = Required.Always)]
        //[JsonSchema(Required = true, EnumValues = new object[] { "translation", "rotation", "scale", "weights" }, EnumSerializationType = EnumSerializationType.AsString)]
        public string path;

        /// <summary></summary>
        [JsonProperty("extensions")]
        public object extensions;

        /// <summary></summary>
        [JsonProperty("extras")]
        public object extras;

        public enum Interpolations
        {
            LINEAR,
            STEP,
            CUBICSPLINE
        }

        public const string PATH_TRANSLATION = "translation";
        public const string PATH_EULER_ROTATION = "rotation";
        public const string PATH_ROTATION = "rotation";
        public const string PATH_SCALE = "scale";
        public const string PATH_WEIGHT = "weights";
        public const string NOT_IMPLEMENTED = "NotImplemented";

        [Obsolete("Use AnimationProperties")]
        public enum AnimationPropertys
        {
            Translation = AnimationProperties.Translation,
            EulerRotation = AnimationProperties.EulerRotation,
            Rotation = AnimationProperties.Rotation,
            Scale = AnimationProperties.Scale,
            Weight = AnimationProperties.Weight,
            BlendShape = AnimationProperties.BlendShape,

            NotImplemented = AnimationProperties.NotImplemented
        }

        [Obsolete]
        internal static AnimationProperties AnimationPropertysToAnimationProperties(AnimationPropertys property)
        {
            if (!Enum.IsDefined(typeof(AnimationProperties), property))
            {
                throw new InvalidCastException("Failed to convert AnimationPropertys '" + property + "' to AnimationProperties");
            }
            return (AnimationProperties)property;
        }

        public enum AnimationProperties
        {
            Translation,
            EulerRotation,
            Rotation,
            Scale,
            Weight,
            BlendShape,

            NotImplemented
        }

        [Obsolete]
        public static string GetPathName(AnimationPropertys property)
        {
            return GetPathName(AnimationPropertysToAnimationProperties(property));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static string GetPathName(AnimationProperties property)
        {
            switch (property)
            {
                case AnimationProperties.Translation:
                    return PATH_TRANSLATION;
                case AnimationProperties.EulerRotation:
                case AnimationProperties.Rotation:
                    return PATH_ROTATION;
                case AnimationProperties.Scale:
                    return PATH_SCALE;
                case AnimationProperties.BlendShape:
                    return PATH_WEIGHT;
                default: throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static AnimationProperties GetAnimationProperty(string path)
        {
            switch (path)
            {
                case PATH_TRANSLATION:
                    return AnimationProperties.Translation;
                case PATH_ROTATION:
                    return AnimationProperties.Rotation;
                case PATH_SCALE:
                    return AnimationProperties.Scale;
                case PATH_WEIGHT:
                    return AnimationProperties.BlendShape;
                default: throw new NotImplementedException();
            }
        }

        [Obsolete]
        public static int GetElementCount(AnimationPropertys property)
        {
            return GetElementCount(AnimationPropertysToAnimationProperties(property));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static int GetElementCount(AnimationProperties property)
        {
            switch (property)
            {
                case AnimationProperties.Translation: return 3;
                case AnimationProperties.EulerRotation: return 3;
                case AnimationProperties.Rotation: return 4;
                case AnimationProperties.Scale: return 3;
                case AnimationProperties.BlendShape: return 1;
                default: throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static int GetElementCount(string path)
        {
            return GetElementCount(GetAnimationProperty(path));
        }
    }
}
