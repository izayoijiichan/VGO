// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : StreamExtensions
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using System;
    using System.Threading.Tasks;
    using System.Threading;
    using System.IO;

    /// <summary>
    /// Stream Extensions
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>An array of bytes.</returns>
        public static byte[] ReadBytes(this Stream stream, int count)
        {
            if (count < 0)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(nameof(count));
            }

            if (count == 0)
            {
                return Array.Empty<byte>();
            }

            byte[] buffer = new byte[count];

            int numBytesToRead = count;

            int numBytesRead = 0;

            do
            {
                int n = stream.Read(buffer, offset: numBytesRead, count: numBytesToRead);

                numBytesRead += n;

                numBytesToRead -= n;
            }
            while (numBytesToRead > 0);

            if (numBytesRead != count)
            {
                ThrowHelper.ThrowIOException();
            }

            return buffer;
        }

        /// <summary>
        /// Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="count">The number of bytes to read.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>An array of bytes.</returns>
        public static async Task<byte[]> ReadBytesAsync(this Stream stream, int count, CancellationToken cancellationToken)
        {
            if (count < 0)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(nameof(count));
            }

            if (count == 0)
            {
                return Array.Empty<byte>();
            }

            byte[] buffer = new byte[count];

            int numBytesToRead = count;

            int numBytesRead = 0;

            do
            {
                int n = await stream.ReadAsync(buffer, offset: numBytesRead, count: numBytesToRead, cancellationToken);

                numBytesRead += n;

                numBytesToRead -= n;
            }
            while (numBytesToRead > 0);

            if (numBytesRead != count)
            {
                ThrowHelper.ThrowIOException();
            }

            return buffer;
        }
    }
}
