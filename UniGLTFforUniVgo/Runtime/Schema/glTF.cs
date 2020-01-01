using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace UniGLTFforUniVgo
{
    /// <summary>
    /// The root object for a glTF asset.
    /// </summary>
    [Serializable]
    [JsonObject("glTF")]
    public partial class glTF : IEquatable<glTF>
    {
        #region Fields

        /// <summary>Names of glTF extensions used somewhere in this asset.</summary>
        [JsonProperty("extensionsUsed")]
        //[JsonSchema(MinItems = 1, ExplicitIgnorableItemLength = 0)]
        public List<string> extensionsUsed = new List<string>();

        /// <summary>Names of glTF extensions required to properly load this asset.</summary>
        [JsonProperty("extensionsRequired")]
        //[JsonSchema(MinItems = 1, ExplicitIgnorableItemLength = 0)]
        public List<string> extensionsRequired = new List<string>();

        /// <summary></summary>
        [JsonProperty("accessors")]
        //[JsonSchema(MinItems = 1, ExplicitIgnorableItemLength = 0)]
        public List<glTFAccessor> accessors = new List<glTFAccessor>();

        /// <summary></summary>
        [JsonProperty("asset", Required = Required.Always)]
        //[JsonSchema(Required = true)]
        public glTFAssets asset = new glTFAssets();

        /// <summary></summary>
        [JsonProperty("animations")]
        //[JsonSchema(MinItems = 1, ExplicitIgnorableItemLength = 0)]
        public List<glTFAnimation> animations = new List<glTFAnimation>();

        /// <summary></summary>
        [JsonProperty("buffers")]
        //[JsonSchema(MinItems = 1, ExplicitIgnorableItemLength = 0)]
        public List<glTFBuffer> buffers = new List<glTFBuffer>();

        /// <summary></summary>
        [JsonProperty("bufferViews")]
        //[JsonSchema(MinItems = 1, ExplicitIgnorableItemLength = 0)]
        public List<glTFBufferView> bufferViews = new List<glTFBufferView>();

        /// <summary></summary>
        [JsonProperty("cameras")]
        //[JsonSchema(MinItems = 1, ExplicitIgnorableItemLength = 0)]
        public List<glTFCamera> cameras = new List<glTFCamera>();

        /// <summary></summary>
        [JsonProperty("images")]
        //[JsonSchema(MinItems = 1, ExplicitIgnorableItemLength = 0)]
        public List<glTFImage> images = new List<glTFImage>();

        /// <summary></summary>
        [JsonProperty("materials")]
        //[JsonSchema(MinItems = 1, ExplicitIgnorableItemLength = 0)]
        public List<glTFMaterial> materials = new List<glTFMaterial>();

        /// <summary></summary>
        [JsonProperty("meshes")]
        //[JsonSchema(MinItems = 1, ExplicitIgnorableItemLength = 0)]
        public List<glTFMesh> meshes = new List<glTFMesh>();

        /// <summary></summary>
        [JsonProperty("nodes")]
        //[JsonSchema(MinItems = 1, ExplicitIgnorableItemLength = 0)]
        public List<glTFNode> nodes = new List<glTFNode>();

        /// <summary></summary>
        [JsonProperty("samplers")]
        //[JsonSchema(MinItems = 1, ExplicitIgnorableItemLength = 0)]
        public List<glTFTextureSampler> samplers = new List<glTFTextureSampler>();

        /// <summary></summary>
        [JsonProperty("scene")]
        //[JsonSchema(Dependencies = new string[] { "scenes" }, Minimum = 0)]
        public int scene;

        /// <summary></summary>
        [JsonProperty("scenes")]
        //[JsonSchema(MinItems = 1, ExplicitIgnorableItemLength = 0)]
        public List<glTFScene> scenes = new List<glTFScene>();

        /// <summary></summary>
        [JsonProperty("skins")]
        //[JsonSchema(MinItems = 1, ExplicitIgnorableItemLength = 0)]
        public List<glTFSkin> skins = new List<glTFSkin>();

        /// <summary></summary>
        [JsonProperty("textures")]
        //[JsonSchema(MinItems = 1, ExplicitIgnorableItemLength = 0)]
        public List<glTFTexture> textures = new List<glTFTexture>();

        /// <summary></summary>
        [JsonProperty("extensions")]
        public glTF_extensions extensions = new glTF_extensions();

        /// <summary></summary>
        [JsonProperty("extras")]
        public gltf_extras extras = new gltf_extras();

        #endregion

        #region Properties

        [JsonIgnore]
        /// <summary></summary>
        public int[] rootnodes
        {
            get
            {
                return scenes[scene].nodes;
            }
        }

        #endregion
    }
}
