using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace UniGLTFforUniVgo
{
    /// <summary>
    /// Geometry to be rendered with the given material.
    /// </summary>
    [Serializable]
    [JsonObject("mesh.primitive")]
    public class glTFPrimitives
    {
        /// <summary></summary>
        [JsonProperty("attributes", Required = Required.Always)]
        //[JsonSchema(Required = true, SkipSchemaComparison = true)]
        public glTFAttributes attributes;

        /// <summary></summary>
        [JsonProperty("indices", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        //[JsonSchema(Minimum = 0, ExplicitIgnorableValue = -1)]
        public int indices = -1;

        /// <summary></summary>
        [JsonProperty("material", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        //[JsonSchema(Minimum = 0)]
        public int material = -1;

        /// <summary></summary>
        [JsonProperty("mode")]
        //[JsonSchema(EnumValues = new object[] { 0, 1, 2, 3, 4, 5, 6 })]
        public int mode;

        /// <summary></summary>
        [JsonProperty("targets")]
        //[JsonSchema(MinItems = 1, ExplicitIgnorableItemLength = 0)]
        //[ItemJsonSchema(SkipSchemaComparison = true)]
        public List<gltfMorphTarget> targets = null;

        /// <summary></summary>
        [JsonProperty("extensions")]
        //[JsonSchema(SkipSchemaComparison = true)]
        public object extensions = null;

        /// <summary></summary>
        [JsonProperty("extras")]
        public glTFPrimitives_extras extras = null;

        /// <summary></summary>
        [JsonIgnore]
        public bool HasVertexColor
        {
            get
            {
                return attributes.COLOR_0 != -1;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    //[JsonObject("")]
    public class glTFAttributes
    {
        /// <summary></summary>
        [JsonProperty("POSITION", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        //[JsonSchema(Minimum = 0, ExplicitIgnorableValue = -1)]
        public int POSITION = -1;

        /// <summary></summary>
        [JsonProperty("NORMAL", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        //[JsonSchema(Minimum = 0, ExplicitIgnorableValue = -1)]
        public int NORMAL = -1;

        /// <summary></summary>
        [JsonProperty("TANGENT", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        //[JsonSchema(Minimum = 0, ExplicitIgnorableValue = -1)]
        public int TANGENT = -1;

        /// <summary></summary>
        [JsonProperty("TEXCOORD_0", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        //[JsonSchema(Minimum = 0, ExplicitIgnorableValue = -1)]
        public int TEXCOORD_0 = -1;

        /// <summary></summary>
        [JsonProperty("COLOR_0", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        //[JsonSchema(Minimum = 0, ExplicitIgnorableValue = -1)]
        public int COLOR_0 = -1;

        /// <summary></summary>
        [JsonProperty("JOINTS_0", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        //[JsonSchema(Minimum = 0, ExplicitIgnorableValue = -1)]
        public int JOINTS_0 = -1;

        /// <summary></summary>
        [JsonProperty("WEIGHTS_0", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        //[JsonSchema(Minimum = 0, ExplicitIgnorableValue = -1)]
        public int WEIGHTS_0 = -1;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var rhs = obj as glTFAttributes;
            if (rhs == null)
            {
                return base.Equals(obj);
            }

            return POSITION == rhs.POSITION
                && NORMAL == rhs.NORMAL
                && TANGENT == rhs.TANGENT
                && TEXCOORD_0 == rhs.TEXCOORD_0
                && COLOR_0 == rhs.COLOR_0
                && JOINTS_0 == rhs.JOINTS_0
                && WEIGHTS_0 == rhs.WEIGHTS_0
                ;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    //[JsonObject("")]
    public class gltfMorphTarget
    {
        /// <summary></summary>
        [JsonProperty("POSITION", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        //[JsonSchema(Minimum = 0, ExplicitIgnorableValue = -1)]
        public int POSITION = -1;

        /// <summary></summary>
        [JsonProperty("NORMAL", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        //[JsonSchema(Minimum = 0, ExplicitIgnorableValue = -1)]
        public int NORMAL = -1;

        /// <summary></summary>
        [JsonProperty("TANGENT", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(-1)]
        //[JsonSchema(Minimum = 0, ExplicitIgnorableValue = -1)]
        public int TANGENT = -1;
    }
}
