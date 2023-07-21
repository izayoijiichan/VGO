// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : ExModel
// ----------------------------------------------------------------------
#nullable enable
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
        public ExModel(in string key, in string json)
        {
            ThrowHelper.ThrowExceptionIfArgumentIsNull(nameof(key), key);
            ThrowHelper.ThrowExceptionIfArgumentIsNull(nameof(json), json);

            this.key = key;
            this.json = json;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Determines whether two objects have the same value.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true if the value is the same; otherwise, false.</returns>
        public bool Equals(in ExModel other)
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
