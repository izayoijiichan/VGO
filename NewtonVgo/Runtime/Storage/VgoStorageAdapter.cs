// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoStorageAdapter
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    using NewtonVgo;
    using NewtonVgo.Buffers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Runtime.InteropServices;

    /// <summary>
    /// VGO Storage Adapter
    /// </summary>
    public class VgoStorageAdapter
    {
        #region Constructors

        /// <summary>
        /// Create a new instance of VgoStorageAdapter.
        /// </summary>
        /// <param name="storage"></param>
        public VgoStorageAdapter(VgoStorage storage)
        {
            Storage = storage;
        }

        #endregion

        #region Properties

        /// <summary>The storage.</summary>
        public VgoStorage Storage { get; }

        /// <summary>The type of the geometry coodinates.</summary>
        public VgoGeometryCoordinate GeometryCoordinate => Storage.GeometryCoordinate;

        /// <summary>The type of the UV coodinates.</summary>
        public VgoUVCoordinate UVCoordinate => Storage.UVCoordinate;

        /// <summary>The asset info.</summary>
        public VgoAssetInfo AssetInfo { get => Storage.AssetInfo; set => Storage.AssetInfo = value; }

        /// <summary>The layout.</summary>
        public VgoLayout Layout => Storage.Layout;

        /// <summary>List of the resource accessor.</summary>
        public List<VgoResourceAccessor> ResourceAccessors => Storage.ResourceAccessors;

        /// <summary>The resource.</summary>
        protected IByteBuffer Resource => Storage.Resource;

        #endregion

        #region Accessor

        /// <summary>
        /// Get a resource accessor.
        /// </summary>
        /// <param name="accessorIndex">The index of the accessor.</param>
        /// <returns>An accessor.</returns>
        public VgoResourceAccessor GetAccessor(int accessorIndex)
        {
            if (ResourceAccessors.TryGetValue(accessorIndex, out VgoResourceAccessor accessor) == false)
            {
                throw new IndexOutOfRangeException($"accessors[{accessorIndex}]");
            }

            return accessor;
        }

        #endregion

        #region Accessor (Import)

        /// <summary>
        /// Gets array data from the resource through the accessor.
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <param name="accessorIndex">The index of the accessor.</param>
        /// <returns>Array data.</returns>
        public T[] GetAccessorArrayData<T>(int accessorIndex) where T : struct
        {
            VgoResourceAccessor accessor = GetAccessor(accessorIndex);

            ArraySegment<byte> accessorBytes;

            if (accessor.sparseType == VgoResourceAccessorSparseType.None)
            {
                accessorBytes = GetAccessorBytesWithoutSparse(accessor);
            }
            else
            {
                accessorBytes = GetAccessorBytesWithSparse(accessor);
            }

            T[] data = new T[accessor.count];

            accessorBytes.MarshalCopyTo(data);

            return data;
        }

        /// <summary>
        /// Gets span data from the resource through the accessor.
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <param name="accessorIndex">The index of the accessor.</param>
        /// <returns>Span data.</returns>
        public ReadOnlySpan<T> GetAccessorSpan<T>(int accessorIndex) where T : struct
        {
            VgoResourceAccessor accessor = GetAccessor(accessorIndex);

            ArraySegment<byte> accessorBytes;

            if (accessor.sparseType == VgoResourceAccessorSparseType.None)
            {
                accessorBytes = GetAccessorBytesWithoutSparse(accessor);
            }
            else
            {
                accessorBytes = GetAccessorBytesWithSparse(accessor);
            }

            ReadOnlySpan<T> accessorTypedSpan = MemoryMarshal.Cast<byte, T>(accessorBytes.AsSpan());

            return accessorTypedSpan;
        }

        /// <summary>
        /// Gets array segment data from the resource through the accessor.
        /// </summary>
        /// <param name="accessorIndex">The index of the accessor.</param>
        /// <returns>Array segment byte.</returns>
        public ArraySegment<byte> GetAccessorBytes(int accessorIndex)
        {
            VgoResourceAccessor accessor = GetAccessor(accessorIndex);

            return GetAccessorBytes(accessor);
        }

        /// <summary>
        /// Gets array segment data from the resource through the accessor.
        /// </summary>
        /// <param name="accessor">An accessor.</param>
        /// <returns>Array segment byte.</returns>
        public ArraySegment<byte> GetAccessorBytes(VgoResourceAccessor accessor)
        {
            if (accessor.sparseType == VgoResourceAccessorSparseType.None)
            {
                return GetAccessorBytesWithoutSparse(accessor);
            }
            else
            {
                return GetAccessorBytesWithSparse(accessor);
            }
        }

        /// <summary>
        /// Gets array segment byte from the resource through the accessor (non sparse).
        /// </summary>
        /// <param name="accessor">An accessor.</param>
        /// <returns>Array segment byte.</returns>
        protected ArraySegment<byte> GetAccessorBytesWithoutSparse(VgoResourceAccessor accessor)
        {
            if (accessor.count <= 0)
            {
                throw new FormatException("accessor.count: 0");
            }

            int byteStride = accessor.byteStride;

            if (byteStride == 0)
            {
                byteStride = accessor.dataType.ByteSize();
            }

            int byteLength = accessor.byteLength;

            if (byteLength == 0)
            {
                byteLength = byteStride * accessor.count;
            }

            ArraySegment<byte> accessorBytes = Resource.GetBytes()
                .Slice(accessor.byteOffset, byteLength);

            return accessorBytes;
        }

        /// <summary>
        /// Gets array segment byte from the resource through the accessor with sparse.
        /// </summary>
        /// <param name="accessor">An accessor.</param>
        /// <returns>Array segment byte.</returns>
        protected ArraySegment<byte> GetAccessorBytesWithSparse(VgoResourceAccessor accessor)
        {
            if (accessor.count <= 0)
            {
                throw new FormatException("accessor.count: 0");
            }

            if (accessor.sparseCount <= 0)
            {
                throw new FormatException($"accessor.sparseCount: {accessor.sparseCount}");
            }

            ArraySegment<byte> restoreBytes;

            // @notice
            //  - uint: sub mesh
            //  - Vector3: morph target
            if (accessor.sparseType == VgoResourceAccessorSparseType.General)
            {
                if (accessor.sparseCount > accessor.count)
                {
                    throw new FormatException($"accessor.sparseCount: {accessor.sparseCount}, accessor.count: {accessor.count}");
                }

                if (accessor.dataType == VgoResourceAccessorDataType.UnsignedInt)
                {
                    restoreBytes = RestoreArraySegmentFromSparse<uint>(accessor);
                }
                else if (accessor.dataType == VgoResourceAccessorDataType.Vector2Float)
                {
                    restoreBytes = RestoreArraySegmentFromSparse<Vector2>(accessor);
                }
                else if (accessor.dataType == VgoResourceAccessorDataType.Vector3Float)
                {
                    restoreBytes = RestoreArraySegmentFromSparse<Vector3>(accessor);
                }
                else if (accessor.dataType == VgoResourceAccessorDataType.Vector4Float)
                {
                    restoreBytes = RestoreArraySegmentFromSparse<Vector4>(accessor);
                }
                else
                {
                    // @todo
                    throw new NotImplementedException($"SparseValueDataType: {accessor.dataType}");
                }
            }
            else if (accessor.sparseType == VgoResourceAccessorSparseType.Powerful)
            {
                if (accessor.dataType == accessor.sparseValueDataType)
                {
                    throw new FormatException($"accessorDataType: {accessor.dataType} == SparseValueDataType: {accessor.sparseValueDataType}");
                }

                if (
                    (accessor.dataType == VgoResourceAccessorDataType.Vector2Float) ||
                    (accessor.dataType == VgoResourceAccessorDataType.Vector3Float) ||
                    (accessor.dataType == VgoResourceAccessorDataType.Vector4Float))
                {
                    if (accessor.sparseValueDataType != VgoResourceAccessorDataType.Float)
                    {
                        throw new FormatException($"SparseValueDataType: {accessor.sparseValueDataType}");
                    }

                    if (accessor.dataType == VgoResourceAccessorDataType.Vector2Float)
                    {
                        restoreBytes = RestoreArraySegmentFromSparse<float, Vector2>(accessor);
                    }
                    else if (accessor.dataType == VgoResourceAccessorDataType.Vector3Float)
                    {
                        restoreBytes = RestoreArraySegmentFromSparse<float, Vector3>(accessor);
                    }
                    else if (accessor.dataType == VgoResourceAccessorDataType.Vector4Float)
                    {
                        restoreBytes = RestoreArraySegmentFromSparse<float, Vector4>(accessor);
                    }
                }
                else
                {
                    // @todo
                    throw new NotImplementedException($"SparseValueDataType: {accessor.dataType}");
                }
            }
            else
            {
                throw new Exception($"SparseType: {accessor.sparseType}");
            }

            return restoreBytes;
        }

        /// <summary>
        /// Restore the array segment from sparse.
        /// </summary>
        /// <typeparam name="TValue">The data type of sparse value.</typeparam>
        /// <param name="accessor">An accessor.</param>
        /// <returns>Array segment byte.</returns>
        protected ArraySegment<byte> RestoreArraySegmentFromSparse<TValue>(VgoResourceAccessor accessor)
            where TValue : struct
        {
            return RestoreArraySegmentFromSparse<TValue, TValue>(accessor);
        }

        /// <summary>
        /// Restore the array segment from sparse.
        /// </summary>
        /// <typeparam name="TValue">The data type of sparse value.</typeparam>
        /// <typeparam name="TData">The data type of accessor.</typeparam>
        /// <param name="accessor">An accessor.</param>
        /// <returns>Array segment byte.</returns>
        protected ArraySegment<byte> RestoreArraySegmentFromSparse<TValue, TData>(VgoResourceAccessor accessor)
            where TValue : struct
            where TData : struct
        {
            ArraySegment<byte> indexByteArray = Resource.GetBytes().Slice(
                offset: accessor.byteOffset,
                count: accessor.sparseIndexDataType.ByteSize() * accessor.sparseCount
            );

            ArraySegment<byte> valueByteArray = Resource.GetBytes().Slice(
                offset: accessor.byteOffset + accessor.sparseValueOffset,
                count: accessor.dataType.ByteSize() * accessor.sparseCount
            );

            int dataByteSize = Marshal.SizeOf<TData>();

            byte[] restoreByteArray = new byte[dataByteSize * accessor.count];

            var restoreByteArraySegment = new ArraySegment<byte>(restoreByteArray);

            Span<TValue> restoreTypedSpan = MemoryMarshal.Cast<byte, TValue>(restoreByteArray.AsSpan());

            Span<TValue> valueTypedSpan = MemoryMarshal.Cast<byte, TValue>(valueByteArray.AsSpan());

            if (accessor.sparseIndexDataType == VgoResourceAccessorDataType.UnsignedByte)
            {
                for (int sparseIndex = 0; sparseIndex < accessor.sparseCount; sparseIndex++)
                {
                    restoreTypedSpan[restoreByteArray[sparseIndex]] = valueTypedSpan[sparseIndex];
                }
            }
            else if (accessor.sparseIndexDataType == VgoResourceAccessorDataType.UnsignedShort)
            {
                Span<ushort> indexTypedSpan = MemoryMarshal.Cast<byte, ushort>(indexByteArray.AsSpan());

                for (int sparseIndex = 0; sparseIndex < accessor.sparseCount; sparseIndex++)
                {
                    restoreTypedSpan[indexTypedSpan[sparseIndex]] = valueTypedSpan[sparseIndex];
                }
            }
            else if (accessor.sparseIndexDataType == VgoResourceAccessorDataType.UnsignedInt)
            {
                Span<uint> indexTypedSpan = MemoryMarshal.Cast<byte, uint>(indexByteArray.AsSpan());

                for (int sparseIndex = 0; sparseIndex < accessor.sparseCount; sparseIndex++)
                {
                    restoreTypedSpan[(int)indexTypedSpan[sparseIndex]] = valueTypedSpan[sparseIndex];
                }
            }
            else
            {
                throw new NotSupportedException($"accessor.sparseIndexDataType: {accessor.sparseIndexDataType}");
            }

            return restoreByteArraySegment;
        }

        #endregion

        #region Accessor (Export)

        /// <summary>
        /// Add an accessor (non sparse) to resource.
        /// </summary>
        /// <typeparam name="T">Type of arrayData.</typeparam>
        /// <param name="arrayData">Array of data.</param>
        /// <param name="dataType">The type of data.</param>
        /// <param name="kind">The kind of the accessor.</param>
        /// <returns>The index of the accessor.</returns>
        public int AddAccessorWithoutSparse<T>(T[] arrayData, VgoResourceAccessorDataType dataType, VgoResourceAccessorKind kind)
            where T : struct
        {
            if (arrayData == null)
            {
                return -1;
            }

            if (arrayData.Length == 0)
            {
                return -1;
            }

            int byteStride = Marshal.SizeOf(typeof(T));

            ArraySegment<T> segment = new ArraySegment<T>(arrayData);
            
            int byteOffset = Resource.Length;

            int byteLength = Resource.Append(segment, byteStride);

            var accessor = new VgoResourceAccessor
            {
                kind = kind,
                byteOffset = byteOffset,
                byteLength = byteLength,
                byteStride = byteStride,
                dataType = dataType,
                count = arrayData.Length,
            };

            ResourceAccessors.Add(accessor);

            int accessorIndex = ResourceAccessors.Count - 1;

            return accessorIndex;
        }

        /// <summary>
        /// Add an accessor (with sparse) to resource.
        /// </summary>
        /// <typeparam name="TValue">Type of the sparse values.</typeparam>
        /// <param name="sparseType">The type of the sparse.</param>
        /// <param name="sparseIndices">Array data of sparce indeces.</param>
        /// <param name="sparseValues">Array data of sparce values.</param>
        /// <param name="sparseValueDataType">The data type of the sparse values.</param>
        /// <param name="accessorDataType">The data type of the accessor.</param>
        /// <param name="accessorCount">The number of attributes referenced by this accessor.</param>
        /// <param name="kind">The kind of the accessor.</param>
        /// <returns>The index of the accessor.</returns>
        public int AddAccessorWithSparse<TValue>(VgoResourceAccessorSparseType sparseType, int[] sparseIndices, TValue[] sparseValues, VgoResourceAccessorDataType sparseValueDataType, VgoResourceAccessorDataType accessorDataType, int accessorCount, VgoResourceAccessorKind kind)
            where TValue : struct
        {
            if (accessorCount == 0)
            {
                return -1;
            }

            if ((sparseValues == null) || (sparseValues.Length == 0))
            {
                return -1;
            }

            if ((sparseIndices == null) || (sparseIndices.Length == 0))
            {
                return -1;
            }

            if (sparseIndices.Length != sparseValues.Length)
            {
                throw new Exception();
            }

            int byteOffset = Resource.Length;

            int sparseIndicesByteLength;

            VgoResourceAccessorDataType sparseIndexDataType;

            if (sparseIndices.Length <= byte.MaxValue)
            {
                byte[] byteSparseIndices = sparseIndices.Select(x => (byte)x).ToArray();

                sparseIndicesByteLength = Resource.Append(new ArraySegment<byte>(byteSparseIndices), stride: 1);

                sparseIndexDataType = VgoResourceAccessorDataType.UnsignedByte;
            }
            else if (sparseIndices.Length <= ushort.MaxValue)
            {
                ushort[] ushortSparseIndices = sparseIndices.Select(x => (ushort)x).ToArray();

                sparseIndicesByteLength = Resource.Append(new ArraySegment<ushort>(ushortSparseIndices), stride: 2);

                sparseIndexDataType = VgoResourceAccessorDataType.UnsignedShort;
            }
            else
            {
                uint[] uintSparseIndices = sparseIndices.Select(x => (uint)x).ToArray();

                sparseIndicesByteLength = Resource.Append(new ArraySegment<uint>(uintSparseIndices), stride: 4);

                sparseIndexDataType = VgoResourceAccessorDataType.UnsignedInt;
            }

            int sparseValuesByteLength = Resource.Append(new ArraySegment<TValue>(sparseValues), stride: 0);

            VgoResourceAccessor accessor = new VgoResourceAccessor
            {
                kind = kind,
                byteOffset = byteOffset,
                byteLength = sparseIndicesByteLength + sparseValuesByteLength,
                byteStride = 0,  // @notice mixed
                dataType = accessorDataType,
                count = accessorCount,
                sparseType = sparseType,
                sparseCount = sparseIndices.Length,
                sparseIndexDataType = sparseIndexDataType,
                sparseValueDataType = sparseValueDataType,
                sparseValueOffset = sparseIndicesByteLength,
            };

            ResourceAccessors.Add(accessor);

            int accessorIndex = ResourceAccessors.Count - 1;

            return accessorIndex;
        }

        #endregion
    }
}
