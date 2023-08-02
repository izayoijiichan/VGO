// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoExtensions
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using NewtonVgo.Serialization;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// VGO Extensions.
    /// </summary>
    [Serializable]
    public class VgoExtensions : KeyedCollection<string, ExModel>
    {
        #region Properties

        /// <summary>JsonSerializer settings.</summary>
        [JsonIgnore]
        public JsonSerializerSettings JsonSerializerSettings { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of VgoExtensions.
        /// </summary>
        public VgoExtensions()
        {
            JsonSerializerSettings = new VgoJsonSerializerSettings();
        }

        /// <summary>
        /// Create a new instance of VgoExtensions with jsonSerializerSettings.
        /// </summary>
        /// <param name="jsonSerializerSettings"></param>
        public VgoExtensions(in JsonSerializerSettings jsonSerializerSettings)
        {
            JsonSerializerSettings = jsonSerializerSettings;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Gets the type of an item contained in the collection.
        /// </summary>
        /// <param name="item">The item in the collection whose type is to be retrieved.</param>
        /// <returns>The type of the specified item in the collection.</returns>
        protected override string GetKeyForItem(ExModel item)
        {
            return item.key;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the specified key and value to the collection.
        /// </summary>
        /// <typeparam name="T">The type of value.</typeparam>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        /// <param name="jsonSerializerSettings"></param>
        public void Add<T>(in string key, in T value, JsonSerializerSettings? jsonSerializerSettings = null)
        {
            jsonSerializerSettings ??= JsonSerializerSettings;

            string json = JsonConvert.SerializeObject(value, jsonSerializerSettings);

            ExModel item = new ExModel(key, json);

            Items.Add(item);
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of value.</typeparam>
        /// <param name="key">The key of the element to get.</param>
        /// <param name="jsonSerializerSettings"></param>
        /// <returns>Returns the value associated with the specified key.</returns>
        public T GetValue<T>(in string key, JsonSerializerSettings? jsonSerializerSettings = null)
            where T : class, new()
        {
            ExModel model = this[key];

            jsonSerializerSettings ??= JsonSerializerSettings;

            T? value = JsonConvert.DeserializeObject<T>(model.json, jsonSerializerSettings);

            if (value is null)
            {
                ThrowHelper.ThrowException();

                return new T();
            }

            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">The type of value.</typeparam>
        /// <param name="key">The key of the element to get.</param>
        /// <param name="jsonSerializerSettings"></param>
        /// <returns>
        /// If the key is found, returns the value associated with the specified key.
        /// otherwise, the default value for the type of the value parameter. 
        /// </returns>
        public T? GetValueOrDefault<T>(in string key, JsonSerializerSettings? jsonSerializerSettings = null)
            where T : class, new()
        {
            try
            {
                if (Contains(key))
                {
                    return GetValue<T>(key, jsonSerializerSettings);
                }

                return default;
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        /// Attempts to add the specified key and value to the collection.
        /// </summary>
        /// <typeparam name="T">The type of value.</typeparam>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        /// <param name="jsonSerializerSettings"></param>
        /// <returns></returns>
        public bool TryAdd<T>(in string key, in T value, JsonSerializerSettings? jsonSerializerSettings = null)
        {
            if (Contains(key))
            {
                return false;
            }

            try
            {
                Add(key, value, jsonSerializerSettings);

                return true;
            }
            catch
            {
                return false;
            }
        }

#nullable disable
        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of value.</typeparam>
        /// <param name="key">The key of the element to get.</param>
        /// <param name="value">When this method returns true, contains the value associated with the specified key; otherwise, the default value for the type of the value parameter.</param>
        /// <param name="jsonSerializerSettings"></param>
        /// <returns>true if the collection contains an element with the specified key; otherwise, false.</returns>
        public bool TryGetValue<T>(in string key, out T value, JsonSerializerSettings jsonSerializerSettings = null)
            where T : class, new()
        {
            try
            {
                if (Contains(key))
                {
                    value = GetValue<T>(key, jsonSerializerSettings);

                    return true;
                }

                value = default;

                return false;
            }
            catch
            {
                value = default;

                return false;
            }
        }
#nullable enable

        #endregion

        #region Public Methods (JsonConverter)

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>for JsonConverter</remarks>
        public Dictionary<string, JRaw>? GetConverterDictionary()
        {
            if (Items == null)
            {
                return null;
            }

            var dic = new Dictionary<string, JRaw>();

            foreach (ExModel exMdeol in Items)
            {
                dic.Add(exMdeol.key, new JRaw(exMdeol.json));
            }

            return dic;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <remarks>for JsonConverter</remarks>
        public void SetConverterDictionary(in IDictionary<string, JRaw>? src)
        {
            if (src == null)
            {
                return;
            }

            foreach (var kvp in src)
            {
                Items.Add(new ExModel(kvp.Key, kvp.Value.ToString()));
            }
        }

        #endregion
    }
}
