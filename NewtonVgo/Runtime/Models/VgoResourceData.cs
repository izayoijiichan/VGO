// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoResourceData
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using System;
    using System.Numerics;
    using System.Runtime.InteropServices;

    /// <summary>
    /// VGO Resource Data
    /// </summary>
    public class VgoResourceData
    {
        #region Fields

        /// <summary>The resource index.</summary>
        private readonly int _ResourceIndex;
        
        /// <summary>The resource accessor.</summary>
        private readonly VgoResourceAccessor _ResourceAccessor;

        /// <summary>The resource data.</summary>
        private readonly byte[] _ResourceBytes;

        #endregion

        #region Properties

        /// <summary>The resource index.</summary>
        public int ResourceIndex => _ResourceIndex;

        ///// <summary>The resource accessor.</summary>
        //public VgoResourceAccessor ResourceAccessor => _ResourceAccessor;

        ///// <summary>The resource data.</summary>
        //public byte[] ResourceBytes => _ResourceBytes;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of VgoResourceData.
        /// </summary>
        /// <param name="resourceIndex">The resource index.</param>
        /// <param name="resourceAccessor">The resource accessor.</param>
        /// <remarks>No data.</remarks>
        public VgoResourceData(int resourceIndex, VgoResourceAccessor resourceAccessor)
            : this(resourceIndex, resourceAccessor, Array.Empty<Byte>())
        {
        }

        /// <summary>
        /// Create a new instance of VgoResourceData.
        /// </summary>
        /// <param name="resourceIndex">The resource index.</param>
        /// <param name="resourceAccessor">The resource accessor.</param>
        /// <param name="resourceBytes">The resource data.</param>
        public VgoResourceData(int resourceIndex, VgoResourceAccessor resourceAccessor, byte[] resourceBytes)
        {
            if (resourceIndex < 0)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(nameof(resourceIndex));
            }

            _ResourceIndex = resourceIndex;

            _ResourceAccessor = resourceAccessor;

            _ResourceBytes = resourceBytes;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets byte array data.
        /// </summary>
        /// <returns>Byte array data.</returns>
        public byte[] GetDataAsByteArray()
        {
            return GetOrRestoreData();
        }

        /// <summary>
        /// Gets typed array data.
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <returns>Typed array data.</returns>
        public T[] GetDataAsArray<T>() where T : struct
        {
            byte[] byteArray = GetOrRestoreData();

            var arraySegmentByte = new ArraySegment<byte>(byteArray);

            T[] dataArray = new T[_ResourceAccessor.count];

            arraySegmentByte.MarshalCopyTo(dataArray);

            return dataArray;
        }

        /// <summary>
        /// Gets typed span data.
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <returns>Typed span data.</returns>
        public ReadOnlySpan<T> GetDataAsSpan<T>() where T : struct
        {
            byte[] byteArray = GetOrRestoreData();

            Span<T> typedSpanData = MemoryMarshal.Cast<byte, T>(byteArray.AsSpan());

            return typedSpanData;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Get or restore resource data.
        /// </summary>
        /// <returns>Byte array data.</returns>
        protected byte[] GetOrRestoreData()
        {
            if (_ResourceAccessor.sparseType == VgoResourceAccessorSparseType.None)
            {
                return _ResourceBytes;
            }
            else
            {
                return GetDataWithSparse(_ResourceBytes, _ResourceAccessor);
            }
        }

        /// <summary>
        /// Restore resource data through the accessor with sparse.
        /// </summary>
        /// <param name="resourceBytes">A resource bytes.</param>
        /// <param name="accessor">A resource accessor.</param>
        /// <returns>Restored byte array data.</returns>
        protected byte[] GetDataWithSparse(in byte[] resourceBytes, in VgoResourceAccessor accessor)
        {
            if (accessor.count <= 0)
            {
                ThrowHelper.ThrowFormatException("accessor.count: 0");

                return Array.Empty<byte>();
            }

            if (accessor.sparseCount <= 0)
            {
                ThrowHelper.ThrowFormatException($"accessor.sparseCount: {accessor.sparseCount}");

                return Array.Empty<byte>();
            }

            byte[] restoreBytes;

            // @notice
            //  - uint: sub mesh
            //  - Vector3: morph target
            if (accessor.sparseType == VgoResourceAccessorSparseType.General)
            {
                if (accessor.sparseCount > accessor.count)
                {
                    ThrowHelper.ThrowFormatException($"accessor.sparseCount: {accessor.sparseCount}, accessor.count: {accessor.count}");

                    return Array.Empty<byte>();
                }

                if (accessor.dataType != accessor.sparseValueDataType)
                {
                    ThrowHelper.ThrowFormatException($"accessorDataType: {accessor.dataType} != SparseValueDataType: {accessor.sparseValueDataType}");

                    return Array.Empty<byte>();
                }

                if (accessor.dataType == VgoResourceAccessorDataType.UnsignedInt)
                {
                    restoreBytes = RestoreDataFromSparse<uint>(resourceBytes, accessor);
                }
                else if (accessor.dataType == VgoResourceAccessorDataType.Vector2Float)
                {
                    restoreBytes = RestoreDataFromSparse<Vector2>(resourceBytes, accessor);
                }
                else if (accessor.dataType == VgoResourceAccessorDataType.Vector3Float)
                {
                    restoreBytes = RestoreDataFromSparse<Vector3>(resourceBytes, accessor);
                }
                else if (accessor.dataType == VgoResourceAccessorDataType.Vector4Float)
                {
                    restoreBytes = RestoreDataFromSparse<Vector4>(resourceBytes, accessor);
                }
                else
                {
                    // @todo
                    ThrowHelper.ThrowNotImplementedException($"SparseValueDataType: {accessor.dataType}");

                    return Array.Empty<byte>();
                }
            }
            else if (accessor.sparseType == VgoResourceAccessorSparseType.Powerful)
            {
                if (accessor.dataType == accessor.sparseValueDataType)
                {
                    ThrowHelper.ThrowFormatException($"accessorDataType: {accessor.dataType} == SparseValueDataType: {accessor.sparseValueDataType}");
                }

                if (
                    (accessor.dataType == VgoResourceAccessorDataType.Vector2Float) ||
                    (accessor.dataType == VgoResourceAccessorDataType.Vector3Float) ||
                    (accessor.dataType == VgoResourceAccessorDataType.Vector4Float))
                {
                    if (accessor.sparseValueDataType != VgoResourceAccessorDataType.Float)
                    {
                        ThrowHelper.ThrowFormatException($"SparseValueDataType: {accessor.sparseValueDataType}");
                    }

                    if (accessor.dataType == VgoResourceAccessorDataType.Vector2Float)
                    {
                        restoreBytes = RestoreDataFromSparse<float, Vector2>(resourceBytes, accessor);
                    }
                    else if (accessor.dataType == VgoResourceAccessorDataType.Vector3Float)
                    {
                        restoreBytes = RestoreDataFromSparse<float, Vector3>(resourceBytes, accessor);
                    }
                    else if (accessor.dataType == VgoResourceAccessorDataType.Vector4Float)
                    {
                        restoreBytes = RestoreDataFromSparse<float, Vector4>(resourceBytes, accessor);
                    }
                    else
                    {
                        ThrowHelper.ThrowException($"SparseValueDataType: {accessor.dataType}");

                        return Array.Empty<byte>();
                    }
                }
                else
                {
                    // @todo
                    ThrowHelper.ThrowNotImplementedException($"SparseValueDataType: {accessor.dataType}");

                    return Array.Empty<byte>();
                }
            }
            else
            {
                ThrowHelper.ThrowFormatException($"SparseType: {accessor.sparseType}");

                return Array.Empty<byte>();
            }

            return restoreBytes;
        }

        /// <summary>
        /// Restore the resource data from sparse.
        /// </summary>
        /// <typeparam name="TValue">The data type of sparse value.</typeparam>
        /// <param name="resourceBytes">A resource bytes.</param>
        /// <param name="accessor">A resource accessor.</param>
        /// <returns>Restored byte array data.</returns>
        protected byte[] RestoreDataFromSparse<TValue>(in byte[] resourceBytes, in VgoResourceAccessor accessor)
            where TValue : struct
        {
            return RestoreDataFromSparse<TValue, TValue>(resourceBytes, accessor);
        }

        /// <summary>
        /// Restore the array segment from sparse.
        /// </summary>
        /// <typeparam name="TValue">The data type of sparse value.</typeparam>
        /// <typeparam name="TData">The data type of accessor.</typeparam>
        /// <param name="resourceBytes">A resource bytes.</param>
        /// <param name="accessor">A resource accessor.</param>
        /// <returns>restored byte array data.</returns>
        protected byte[] RestoreDataFromSparse<TValue, TData>(in byte[] resourceBytes, in VgoResourceAccessor accessor)
            where TValue : struct
            where TData : struct
        {

            int dataByteSize = accessor.dataType.ByteSize();

            int marshalDataByteSize = Marshal.SizeOf<TData>();

            if (dataByteSize != marshalDataByteSize)
            {
                ThrowHelper.ThrowFormatException($"accessorDataTypeByteSize: {dataByteSize} != marshalDataByteSize: {marshalDataByteSize}");
            }

            int totalDataSize = dataByteSize * accessor.count;

            int totalSparseIndexSize = accessor.sparseIndexDataType.ByteSize() * accessor.sparseCount;
            int totalSparseValueSize = accessor.sparseValueDataType.ByteSize() * accessor.sparseCount;

            if (totalSparseIndexSize != accessor.sparseValueOffset)
            {
                ThrowHelper.ThrowFormatException($"totalSparseIndexSize: {totalSparseIndexSize} != sparseValueOffset: {accessor.sparseValueOffset}");
            }

            if (totalSparseIndexSize + totalSparseValueSize != accessor.byteLength)
            {
                ThrowHelper.ThrowFormatException($"totalSparseIndexSize + totalSparseValueSize: {totalSparseIndexSize + totalSparseValueSize} != accessor.byteLength: {accessor.byteLength}");
            }

            var resourceByteArraySegment = new ArraySegment<byte>(resourceBytes);

            ArraySegment<byte> indexByteArraySegment = resourceByteArraySegment.Slice(
                offset: 0,
                count: totalSparseIndexSize
            );

            ArraySegment<byte> valueByteArraySegment = resourceByteArraySegment.Slice(
                offset: accessor.sparseValueOffset,
                count: totalSparseValueSize
            );

            byte[] restoreByteArray = new byte[totalDataSize];

            var restoreByteArraySegment = new ArraySegment<byte>(restoreByteArray);

            Span<TValue> restoreTypedSpan = MemoryMarshal.Cast<byte, TValue>(restoreByteArraySegment.AsSpan());

            Span<TValue> valueTypedSpan = MemoryMarshal.Cast<byte, TValue>(valueByteArraySegment.AsSpan());

            if (accessor.sparseIndexDataType == VgoResourceAccessorDataType.UnsignedByte)
            {
                Span<byte> indexTypedSpan = indexByteArraySegment.AsSpan();

                for (int sparseIndex = 0; sparseIndex < accessor.sparseCount; sparseIndex++)
                {
                    restoreTypedSpan[indexTypedSpan[sparseIndex]] = valueTypedSpan[sparseIndex];
                }
            }
            else if (accessor.sparseIndexDataType == VgoResourceAccessorDataType.UnsignedShort)
            {
                Span<ushort> indexTypedSpan = MemoryMarshal.Cast<byte, ushort>(indexByteArraySegment.AsSpan());

                for (int sparseIndex = 0; sparseIndex < accessor.sparseCount; sparseIndex++)
                {
                    restoreTypedSpan[indexTypedSpan[sparseIndex]] = valueTypedSpan[sparseIndex];
                }
            }
            else if (accessor.sparseIndexDataType == VgoResourceAccessorDataType.UnsignedInt)
            {
                Span<uint> indexTypedSpan = MemoryMarshal.Cast<byte, uint>(indexByteArraySegment.AsSpan());

                for (int sparseIndex = 0; sparseIndex < accessor.sparseCount; sparseIndex++)
                {
                    restoreTypedSpan[(int)indexTypedSpan[sparseIndex]] = valueTypedSpan[sparseIndex];
                }
            }
            else
            {
                ThrowHelper.ThrowNotSupportedException($"accessor.sparseIndexDataType: {accessor.sparseIndexDataType}");
            }

            return restoreByteArray;
        }

        #endregion
    }
}
