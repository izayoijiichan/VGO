// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : ExModel
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    using System;

    /// <summary>
    /// Ex Model
    /// </summary>
    [Serializable]
    public class ExModel
    {
        #region Fields

        /// <summary>The key.</summary>
        public string key;

        /// <summary>The JSON string.</summary>
        public string json;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of ExModel with key and json.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="json"></param>
        public ExModel(string key, string json)
        {
            this.key = key ?? throw new ArgumentNullException(nameof(key));
            this.json = json ?? throw new ArgumentNullException(nameof(json));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Determines whether two objects have the same value.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true if the value is the same; otherwise, false.</returns>
        public bool Equals(ExModel other)
        {
            if ((key == other.key) &&
                (json == other.json))
            {
                return true;
            }
            return false;
        }

        #endregion
    }
}
