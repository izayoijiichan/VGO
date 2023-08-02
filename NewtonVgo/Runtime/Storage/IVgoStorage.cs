// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : IVgoStorage
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using NewtonVgo.Buffers;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// VGO Storage Interface
    /// </summary>
    public interface IVgoStorage
    {
        #region Properties

        /// <summary>The type of the geometry coordinates.</summary>
        VgoGeometryCoordinate GeometryCoordinate { get; }

        /// <summary>The type of the UV coordinates.</summary>
        VgoUVCoordinate UVCoordinate { get; }

        /// <summary>The asset info.</summary>
        VgoAssetInfo? AssetInfo { get; set; }

        /// <summary>The layout.</summary>
        VgoLayout Layout { get; }

        /// <summary>List of the resource accessor.</summary>
        List<VgoResourceAccessor>? ResourceAccessors { get; }

        /// <summary>The resource.</summary>
        IByteBuffer? Resource { get; }

        /// <summary>The directory path.</summary>
        string? DirectoryPath { get; }

        /// <summary>The timeout seconds of http request.</summary>
        int HttpTimeoutSeconds { get; set; }

        /// <summary>Whether spec version is 2.4 or lower.</summary>
        bool IsSpecVersion_2_4_orLower { get; }

        #endregion

        #region Methods (Export)

        /// <summary>
        /// Exports a VGO format file.
        /// </summary>
        /// <param name="filePath">The full path of the file.</param>
        /// <param name="exportSetting">A vgo export setting.</param>
        /// <returns>Returns true if the export was successful, false otherwise.</returns>
        bool ExportVgoFile(in string filePath, in VgoExportSetting exportSetting);

        #endregion

        #region Methods (Accessor)

        /// <summary>
        /// Get a resource accessor.
        /// </summary>
        /// <param name="accessorIndex">The index of the accessor.</param>
        /// <returns>An accessor.</returns>
        VgoResourceAccessor GetAccessor(in int accessorIndex);

        /// <summary>
        /// Gets array data from the resource through the accessor.
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <param name="accessorIndex">The index of the accessor.</param>
        /// <returns>Array data.</returns>
        T[] GetAccessorArrayData<T>(in int accessorIndex) where T : struct;

        /// <summary>
        /// Gets span data from the resource through the accessor.
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <param name="accessorIndex">The index of the accessor.</param>
        /// <returns>Span data.</returns>
        ReadOnlySpan<T> GetAccessorSpan<T>(in int accessorIndex) where T : struct;

        /// <summary>
        /// Gets array segment data from the resource through the accessor.
        /// </summary>
        /// <param name="accessorIndex">The index of the accessor.</param>
        /// <returns>Array segment byte.</returns>
        ArraySegment<byte> GetAccessorBytes(in int accessorIndex);

        /// <summary>
        /// Gets array segment data from the resource through the accessor.
        /// </summary>
        /// <param name="accessor">An accessor.</param>
        /// <returns>Array segment byte.</returns>
        ArraySegment<byte> GetAccessorBytes(in VgoResourceAccessor accessor);

        /// <summary>
        /// Add an accessor (non sparse) to resource.
        /// </summary>
        /// <typeparam name="T">Type of arrayData.</typeparam>
        /// <param name="arrayData">Array of data.</param>
        /// <param name="dataType">The type of data.</param>
        /// <param name="kind">The kind of the accessor.</param>
        /// <returns>The index of the accessor.</returns>
        int AddAccessorWithoutSparse<T>(
            in T[] arrayData,
            in VgoResourceAccessorDataType dataType,
            in VgoResourceAccessorKind kind
        ) where T : struct;

        /// <summary>
        /// Add an accessor (with sparse) to resource.
        /// </summary>
        /// <typeparam name="TValue">Type of the sparse values.</typeparam>
        /// <param name="sparseType">The type of the sparse.</param>
        /// <param name="sparseIndices">Array data of sparse indices.</param>
        /// <param name="sparseValues">Array data of sparse values.</param>
        /// <param name="sparseValueDataType">The data type of the sparse values.</param>
        /// <param name="accessorDataType">The data type of the accessor.</param>
        /// <param name="accessorCount">The number of attributes referenced by this accessor.</param>
        /// <param name="kind">The kind of the accessor.</param>
        /// <returns>The index of the accessor.</returns>
        int AddAccessorWithSparse<TValue>(
            in VgoResourceAccessorSparseType sparseType,
            in int[] sparseIndices,
            in TValue[] sparseValues,
            in VgoResourceAccessorDataType sparseValueDataType,
            in VgoResourceAccessorDataType accessorDataType,
            in int accessorCount,
            in VgoResourceAccessorKind kind
        ) where TValue : struct;

        #endregion
    }
}
