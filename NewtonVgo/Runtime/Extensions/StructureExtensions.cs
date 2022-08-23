// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : StructureExtensions
// ----------------------------------------------------------------------
namespace NewtonVgo
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Structure Extensions
    /// </summary>
    public static class StructureExtensions
    {
        /// <summary>
        /// Convert this structure to byte array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static byte[] ConvertToByteArray<T>(this T source) where T : struct
        {
            Type structureType = typeof(T);

            int structureSize = Marshal.SizeOf(structureType);

            byte[] byteArray = new byte[structureSize];

            IntPtr structurePointer = Marshal.AllocHGlobal(structureSize);

            try
            {
                Marshal.StructureToPtr(structure: source, ptr: structurePointer, fDeleteOld: true);

                Marshal.Copy(source: structurePointer, destination: byteArray, startIndex: 0, length: structureSize);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Marshal.FreeHGlobal(structurePointer);
            }

            return byteArray;
        }

        /// <summary>
        /// Convert this array to a structure.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T ConvertToStructure<T>(this byte[] source) where T : struct
        {
            Type structureType = typeof(T);

            int structureSize = Marshal.SizeOf(structureType);

            IntPtr structurePointer = Marshal.AllocHGlobal(structureSize);

            T structure = default;

            try
            {
                Marshal.Copy(source: source, startIndex: 0, destination: structurePointer, length: structureSize);

                //structure = Marshal.PtrToStructure<T>(structurePointer);
                structure = (T)Marshal.PtrToStructure(structurePointer, structureType);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Marshal.FreeHGlobal(structurePointer);
            }

            return structure;
        }
    }
}
