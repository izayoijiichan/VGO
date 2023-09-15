// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoStorageAdapter
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using NewtonVgo;
    using System;
    using System.Linq;
    using System.Runtime.InteropServices;

#if false
    /// <summary>
    /// VGO Storage Adapter
    /// </summary>
    public class VgoStorageAdapter
    {
    }
#endif

    /// <summary>
    /// VGO Storage
    /// </summary>
    public partial class VgoStorage
    {
        #region Accessor

        /// <summary>
        /// Get a resource accessor.
        /// </summary>
        /// <param name="accessorIndex">The index of the accessor.</param>
        /// <returns>An accessor.</returns>
        public VgoResourceAccessor GetAccessor(in int accessorIndex)
        {
            if (ResourceAccessors == null)
            {
#if NET_STANDARD_2_1
                ThrowHelper.ThrowFormatException("resource accessor is null.");
#else
                throw new FormatException("resource accessor is null.");
#endif
            }

            if (ResourceAccessors.TryGetValue(accessorIndex, out VgoResourceAccessor accessor) == false)
            {
                ThrowHelper.ThrowIndexOutOfRangeException(nameof(accessorIndex), accessorIndex, min: 0, max: ResourceAccessors.Count);
            }

            return accessor;
        }

        #endregion

        #region Accessor (Import)

        /// <summary>
        /// Gets byte array data from the resource through the accessor.
        /// </summary>
        /// <param name="accessorIndex">The index of the accessor.</param>
        /// <returns>Byte array.</returns>
        public byte[] GetResourceDataAsByteArray(in int accessorIndex)
        {
            if (SegmentResourceData is null)
            {
                ThrowHelper.ThrowFormatException($"resource data is null.");

                return Array.Empty<byte>();
            }

            if (SegmentResourceData.TryGetValue(accessorIndex, out VgoResourceData vgoResource) == false)
            {
                ThrowHelper.ThrowFormatException($"resource data is not found. {nameof(accessorIndex)}: {accessorIndex}");

                return Array.Empty<byte>();
            }

            return vgoResource.GetDataAsByteArray();
        }

        /// <summary>
        /// Gets array data from the resource through the accessor.
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <param name="accessorIndex">The index of the accessor.</param>
        /// <returns>Typed array data.</returns>
        public T[] GetResourceDataAsArray<T>(in int accessorIndex) where T : struct
        {
            if (SegmentResourceData is null)
            {
                ThrowHelper.ThrowFormatException($"resource data is null.");

                return Array.Empty<T>();
            }

            if (SegmentResourceData.TryGetValue(accessorIndex, out VgoResourceData vgoResource) == false)
            {
                ThrowHelper.ThrowFormatException($"resource data is not found. {nameof(accessorIndex)}: {accessorIndex}");

                return Array.Empty<T>();
            }

            return vgoResource.GetDataAsArray<T>();
        }

        /// <summary>
        /// Gets span data from the resource through the accessor.
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <param name="accessorIndex">The index of the accessor.</param>
        /// <returns>Typed span data.</returns>
        public ReadOnlySpan<T> GetResourceDataAsSpan<T>(in int accessorIndex) where T : struct
        {
            if (SegmentResourceData is null)
            {
                ThrowHelper.ThrowFormatException($"resource data is null.");

                return Array.Empty<T>().AsSpan();
            }

            if (SegmentResourceData.TryGetValue(accessorIndex, out VgoResourceData vgoResource) == false)
            {
                ThrowHelper.ThrowFormatException($"resource data is not found. {nameof(accessorIndex)}: {accessorIndex}");

                return Array.Empty<T>().AsSpan();
            }

            return vgoResource.GetDataAsSpan<T>();
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
        public int AddAccessorWithoutSparse<T>(in T[] arrayData, in VgoResourceAccessorDataType dataType, in VgoResourceAccessorKind kind)
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

            if (Resource is null)
            {
                ThrowHelper.ThrowException("resource is null.");

                return -1;
            }

            if (ResourceAccessors is null)
            {
                ThrowHelper.ThrowException("resource accessor is null.");

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
        /// <param name="sparseIndices">Array data of sparse indices.</param>
        /// <param name="sparseValues">Array data of sparse values.</param>
        /// <param name="sparseValueDataType">The data type of the sparse values.</param>
        /// <param name="accessorDataType">The data type of the accessor.</param>
        /// <param name="accessorCount">The number of attributes referenced by this accessor.</param>
        /// <param name="kind">The kind of the accessor.</param>
        /// <returns>The index of the accessor.</returns>
        public int AddAccessorWithSparse<TValue>(
            in VgoResourceAccessorSparseType sparseType,
            in int[] sparseIndices,
            in TValue[] sparseValues,
            in VgoResourceAccessorDataType sparseValueDataType,
            in VgoResourceAccessorDataType accessorDataType,
            in int accessorCount,
            in VgoResourceAccessorKind kind)
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
                ThrowHelper.ThrowException();
            }

            if (Resource is null)
            {
                ThrowHelper.ThrowException("resource is null.");

                return -1;
            }

            if (ResourceAccessors is null)
            {
                ThrowHelper.ThrowException("resource accessor is null.");

                return -1;
            }

            int byteOffset = Resource.Length;

            int sparseIndicesByteLength;

            VgoResourceAccessorDataType sparseIndexDataType;

            int maxIndex = sparseIndices.Max();

            if (maxIndex <= byte.MaxValue)
            {
                byte[] byteSparseIndices = sparseIndices.Select(x => (byte)x).ToArray();

                sparseIndicesByteLength = Resource.Append(new ArraySegment<byte>(byteSparseIndices), stride: 1);

                sparseIndexDataType = VgoResourceAccessorDataType.UnsignedByte;
            }
            else if (maxIndex <= ushort.MaxValue)
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

            if ((Resource.Length - byteOffset) != (sparseIndicesByteLength + sparseValuesByteLength))
            {
                ThrowHelper.ThrowException($"byteLength: {Resource.Length - byteOffset} != {sparseIndicesByteLength + sparseValuesByteLength}");
            }

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
