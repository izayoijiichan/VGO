// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoMeshPrimitiveAttributes
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// A dictionary object, where each key corresponds to mesh attribute semantic and each value is the index of the accessor containing attribute's data.
    /// </summary>
    [Serializable]
    [JsonObject("mesh.primitive.attributes")]
    //[JsonConverter(typeof(VgoMeshPrimitiveAttributesJsonConverter))]
    public class VgoMeshPrimitiveAttributes : IDictionary<string, int>
    {
        #region Fields

        /// <summary>The mesh primitive attribute dictionary.</summary>
        /// <remarks>key: VertexKey, value: accessor index</remarks>
        private readonly IDictionary<string, int> _attributeDictionary;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of VgoMeshPrimitiveAttributes.
        /// </summary>
        public VgoMeshPrimitiveAttributes()
        {
            _attributeDictionary = new Dictionary<string, int>();
        }

        /// <summary>
        /// Create a new instance of VgoMeshPrimitiveAttributes with dictionary.
        /// </summary>
        /// <param name="dictionary"></param>
        public VgoMeshPrimitiveAttributes(IDictionary<string, int> dictionary)
        {
            _attributeDictionary = dictionary;
        }

        #endregion

        #region Accessors

        ///// <summary>Gets or sets the element with the specified key.</summary>

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int this[string key] => _attributeDictionary[key];

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        int IDictionary<string, int>.this[string key]
        {
            get => _attributeDictionary[key];
            set => _attributeDictionary[key] = value;
        }

        #endregion

        #region Properties

        /// <summary>Gets an ICollection string containing the keys of the attributes.</summary>
        ICollection<string> IDictionary<string, int>.Keys => _attributeDictionary.Keys;

        /// <summary>Gets an ICollection int containing the values in the attributes.</summary>
        ICollection<int> IDictionary<string, int>.Values => _attributeDictionary.Values;

        /// <summary>Gets an IEnumerable string containing the keys of the attributes.</summary>
        public IEnumerable<string> Keys => _attributeDictionary.Keys;

        /// <summary>Gets an IEnumerable int containing the values in the attributes.</summary>
        public IEnumerable<int> Values => _attributeDictionary.Values;

        /// <summary></summary>
        public int Count => _attributeDictionary.Count;

        /// <summary></summary>
        public int COLOR_0 => GetValueOrDefault(VertexKey.Color0);

        /// <summary></summary>
        public int JOINTS_0 => GetValueOrDefault(VertexKey.Joints0);

        /// <summary></summary>
        public int NORMAL => GetValueOrDefault(VertexKey.Normal);

        /// <summary></summary>
        public int POSITION => GetValueOrDefault(VertexKey.Position);

        /// <summary></summary>
        public int TANGENT => GetValueOrDefault(VertexKey.Tangent);

        /// <summary></summary>
        public int TEXCOORD_0 => GetValueOrDefault(VertexKey.TexCoord0);

        /// <summary></summary>
        public int TEXCOORD_1 => GetValueOrDefault(VertexKey.TexCoord1);

        /// <summary></summary>
        public int TEXCOORD_2 => GetValueOrDefault(VertexKey.TexCoord2);

        /// <summary></summary>
        public int TEXCOORD_3 => GetValueOrDefault(VertexKey.TexCoord3);

        /// <summary></summary>
        public int WEIGHTS_0 => GetValueOrDefault(VertexKey.Weights0);

        /// <summary></summary>
        public bool IsReadOnly => _attributeDictionary.IsReadOnly;

        #endregion

        #region Enumerators

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _attributeDictionary.GetEnumerator();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds an attribute with the provided key and value.
        /// </summary>
        /// <param name="key">The vertex key to add.</param>
        /// <param name="value">The accessor index.</param>
        public void Add(string key, int value)
        {
            _attributeDictionary.Add(key, value);
        }

        /// <summary>
        /// Adds an attribute with the provided item.
        /// </summary>
        /// <param name="item"></param>
        public void Add(KeyValuePair<string, int> item)
        {
            _attributeDictionary.Add(item);
        }

        /// <summary>
        /// Removes all attribute.
        /// </summary>
        public void Clear()
        {
            _attributeDictionary.Clear();
        }

        /// <summary>
        /// Determines whether the attributes contains an attribute with the specified item.
        /// </summary>
        /// <param name="item">The attribute to locate.</param>
        /// <returns>true if the attributes contains an attribute with the item; otherwise, false.</returns>
        public bool Contains(KeyValuePair<string, int> item)
        {
            return _attributeDictionary.Contains(item);
        }

        /// <summary>
        /// Determines whether the attributes contains an attribute with the specified vertex key.
        /// </summary>
        /// <param name="key">The vertex key to locate.</param>
        /// <returns>true if the attributes contains an attribute with the key; otherwise, false.</returns>
        public bool ContainsKey(string key)
        {
            return _attributeDictionary.ContainsKey(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(KeyValuePair<string, int>[] array, int arrayIndex)
        {
            _attributeDictionary.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(VgoMeshPrimitiveAttributes other)
        {
            if (Count != other.Count)
            {
                return false;
            }

            foreach (string key in other.Keys)
            {
                if (other.ContainsKey(key) == false)
                {
                    return false;
                }
                if (this[key] != other[key])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<string, int>> GetEnumerator()
        {
            return _attributeDictionary.GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetValueOrDefault(string key)
        {
            if (_attributeDictionary.ContainsKey(key))
            {
                return _attributeDictionary[key];
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Removes the attribute with the specified key from the attributes.
        /// </summary>
        /// <param name="key">The vertex key of the attribute to remove.</param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            return _attributeDictionary.Remove(key);
        }

        /// <summary>
        /// Removes the attribute with the specified item from the attributes.
        /// </summary>
        /// <param name="item">The attribute to remove.</param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<string, int> item)
        {
            return _attributeDictionary.Remove(item);
        }

        /// <summary>
        /// Attempts to add the specified key and value to the attributes.
        /// </summary>
        /// <param name="key">The vertex key to add.</param>
        /// <param name="value">The accessor index.</param>
        /// <returns></returns>
        public bool TryAdd(string key, int value)
        {
            if (_attributeDictionary.ContainsKey(key))
            {
                return false;
            }
            else
            {
                _attributeDictionary.Add(key, value);

                return true;
            }
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The vertex key to get.</param>
        /// <param name="value">When this method returns true, contains the value associated with the specified key; otherwise, the default value for the type of the value parameter.</param>
        /// <returns>true if the attributes contains an attribute with the specified key; otherwise, false.</returns>
        public bool TryGetValue(string key, out int value)
        {
            if (_attributeDictionary.TryGetValue(key, out value))
            {
                return true;
            }
            else
            {
                value = -1;

                return false;
            }
        }

        #endregion
    }
}
