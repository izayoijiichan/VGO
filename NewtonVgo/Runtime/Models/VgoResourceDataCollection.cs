// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoResourceDataCollection
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// VGO Resource Data Collection
    /// </summary>
    public class VgoResourceDataCollection : KeyedCollection<int, VgoResourceData>
    {
        #region Protected Methods

        /// <summary>
        /// Gets the type of an item contained in the collection.
        /// </summary>
        /// <param name="item">The item in the collection whose type is to be retrieved.</param>
        /// <returns>The type of the specified item in the collection.</returns>
        protected override int GetKeyForItem(VgoResourceData item)
        {
            return item.ResourceIndex;
        }

        #endregion
    }
}
