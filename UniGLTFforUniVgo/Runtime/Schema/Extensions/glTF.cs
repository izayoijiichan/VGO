using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;

namespace UniGLTFforUniVgo
{
    #region Base
    public class JsonSerializeMembersAttribute : Attribute { }

    public class PartialExtensionBase<T>
    {
        [JsonIgnore]
        public int __count
        {
            get
            {
                return typeof(T).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(x => x.GetCustomAttributes(typeof(JsonSerializeMembersAttribute), true).Any())
                    .Count();
            }
        }
    }

    public partial class ExtensionsBase<T> : PartialExtensionBase<T>
    {
    }

    public partial class ExtraBase<T> : PartialExtensionBase<T>
    {
    }
    #endregion

    /// <summary>
    /// gltf.extensions
    /// </summary>
    [JsonObject("glTF.extensions")]
    public partial class glTF_extensions : ExtensionsBase<glTF_extensions>
    {
        /// <summary>VGO</summary>
        [JsonProperty("VGO")]
        public glTF_VGO VGO = null;
    }

    [Serializable]
    public partial class gltf_extras : ExtraBase<gltf_extras> { }
}
